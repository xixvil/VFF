using System;
using System.Threading;
using System.Collections.Generic;
using NAudio.CoreAudioApi;
using NAudio.Wave;

// Библиотека для взаимодействия с интерфейсом компьютер-кожа через звуковое устройство (Computer-Skin Interface Library - CSIL)
// Special for atomic314
namespace CSIL
{
    public class SkinInterface // класс интерфейса компьютер-кожа
    {
        private const int DEFAULT_DEVICE_SAMPLE_RATE = 8000; // Sample Rate по умолчанию - частота дискретизации в сэмплах в секунду ( http://ru.wikipedia.org/wiki/Частота_дискретизации )
        private const int DEFAULT_LATENCY_TIME = 200; // Время заполнения буффера аудиоустройства
        public delegate float SignalFunction(float time, SignalFunctionParams pars); // функция-делегат для формирования сигнала
        protected IWavePlayer WavePlayer; // экземпляр плеера из библиотеки NAudio
        protected int Duration = 0; // переменная для хранения продолжительности сигнала при в асинхронном режиме
        protected WaveProvider WaveClass; // экземпляр класса провайдера сигнала
       
        public abstract class SignalFunctionParams {} // абстрактный класс для параметров функции, реальный класс должен быть отнаследован от него

        protected class WaveProvider : IWaveProvider // класс провайдера сигнала для WavePlayer
        {
            private WaveFormat waveFormat; // формат аудиопотока
            public SignalFunction function; // функция сигнала
            private SignalFunctionParams WaveParams; // параметры функции сигнала
            long sample = 0; // текущий сэмпл (номер числового значения уровня сигнала на выходе)
            private Object thisLock = new Object(); //нужно для критической секции

            // конструктор класса
            public WaveProvider(int SampleRate, int NumOfChannels, SignalFunction function, SignalFunctionParams WaveParams)
            {
                this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(SampleRate, NumOfChannels);
                this.function = function;
                this.WaveParams = WaveParams;
            }
            
            // метод, с помощью которого WavePlayer узнаёт формат аудиопотока. его не нежелательно менять
            public WaveFormat WaveFormat
            {
                get
                {
                    return waveFormat;
                }
            }

            // метод, с помощью которого плеер получает очередной буфер для проигрывания
            public int Read(byte[] buffer, int offset, int count)
            {
                WaveBuffer waveBuffer = new WaveBuffer(buffer); // создаём новый буффер

                lock (thisLock) // критическая секция
                {
                    for (int n = 0; n < count / 4; n++) // в одном float-значении четыре байта, поэтому длинна буффера в байтах = длина в float * 4
                    {
                        waveBuffer.FloatBuffer[n + (offset / 4)] = (float)(function((float)sample / WaveFormat.SampleRate, WaveParams)); // вызываем для каждого значения нашу функцию, передавая ей текущее время в секундах от начала проигрывания и параметры
                        sample++; // увеличиваем номер сэмпла
                        if (sample >= (Int64.MaxValue - 1)) sample = 0; // дошли до предела Int64 - сбрасываем в ноль
                    }
                }
                return count; // возвращаем количество сгенерированных сэмплов
            }
        }

        // класс-обёртка для класса устройства MMDevice
        public class SoundDevice
        {
            internal MMDevice Device; // эекземпляр класса устройства

            // конструктор
            public SoundDevice(MMDevice Device)
            {
                this.Device = Device;
            }

            // метод для получения тектового имени устройства
            public string getName()
            {
                return Device.DeviceFriendlyName;
            }
        }

        // конструктор класса
        public SkinInterface(SoundDevice OutputDevice, SignalFunction SignalFunction, SignalFunctionParams SignalParams)
        {
            WavePlayer = new WasapiOut(OutputDevice.Device, AudioClientShareMode.Shared, false, DEFAULT_LATENCY_TIME); // создаём экземпляр плеера
            WaveClass = new WaveProvider(DEFAULT_DEVICE_SAMPLE_RATE, 1, SignalFunction, SignalParams); // создаём экземпляр провайдера потока сигнала
            WavePlayer.Init(WaveClass); // инициализируем плеер
        }

        public static SoundDevice[] getSoundDevices() // метод для получения списка устройств
        {
            MMDeviceCollection Devices = new MMDeviceEnumerator()
                .EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active); // создаём перечислитель устройств вывода и выбираем только активные устройства
            List<SoundDevice> DevicesList = new List<SoundDevice>(); // создаём список для экземпляров класса SoundDevice

            foreach (MMDevice dev in Devices) // для каждого устройства
                DevicesList.Add(new SoundDevice(dev)); // вызываем конструктор нашего класса обёртки SoundDevice и помещаем в список

            SoundDevice[] DevicesArray = DevicesList.ToArray(); // конвертируем список в массив (с массивами легче работать :))
            return DevicesArray;
        }

        // функция для треда при асинхронном выводе сигнала
        private void SignalThread()
        {
            WavePlayer.Play(); // начать проигрывание
            Thread.Sleep(Duration); // ждать Duration миллисекунд
            WavePlayer.Stop(); // остановить проигрывание
        }

        // метод для асинхронного вывода сигнала
        public void SendSignalAsync(int DurationMilliseconds)
        {
            Duration = DurationMilliseconds; // запоминаем в поле Duration время
            Thread ThreadForPlayer = new Thread(new ThreadStart(this.SignalThread)); // создаём тред
            ThreadForPlayer.Start(); // запускаем на исполнение
        }

        // метод для синхронного вывода сигнала
        public void SendSignal(int DurationMilliseconds)
        {
            WavePlayer.Play();
            Thread.Sleep(DurationMilliseconds);
            WavePlayer.Stop();
        }

        // начать вывод сигнала
        public void StartSignal()
        {
            WavePlayer.Play();
        }

        // остановить вывод сигнала
        public void StopSignal()
        {
            WavePlayer.Stop();
        }

        // деструктор. тут могут быть проблемы, если он не вызовется (например программа свалится с эксепшном), может произойти переключение устройства по умолчению в винде и звук пропадёт. TODO: как-то решить эту проблему
        ~SkinInterface()
        {
            StopSignal();
            WavePlayer.Dispose();
        }
    }
}