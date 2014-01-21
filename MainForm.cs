using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.GPU;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using DirectShowLib; // нужно чтобы найти все камеры на компе
using CSIL;

// VFF - Virtual Force Field
namespace VFF
{
    public partial class MainForm : Form
    {
        SkinInterface Iface; // экземпляр интерфейса
        SignalParams SignalPars; // экземпляр класса параметров сигнала
        SkinInterface.SoundDevice[] Devices; // устройства аудиовывода
        Capture Camera; // экземпляр камеры
        HaarCascade Hand; // каскад для распознавания руки
        GpuCascadeClassifier GpuHand; // каскад для распознавания руки на видеокарте
        bool GPU; // флаг присутствия видокарты с технологией CUDA
        String FormTitle; // здесь будем хранить изначальный заголовок формы
        int FPS; // количество кадров в секунду
        PointF NewHandPos; // новая позиция руки
        PointF HandPos; // текущая позиция руки

        // класс параметров сигнала
        private class SignalParams : SkinInterface.SignalFunctionParams
        {
            public float Amplitude;
            public float Frequency;
        }

        // функция сигнала. вшита формула для прямоугольного сигнала
        static float Signal(float t, SkinInterface.SignalFunctionParams pars)
        {
            SignalParams Parameters = (SignalParams)pars;

            return Parameters.Amplitude * (float)Math.Sign(Math.Sin(2 * Math.PI * t * Parameters.Frequency));
        }

        // Расстояние между двумя точками
        private int Distance(PointF a, PointF b)
        {
            return (int)Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public MainForm()
        {
            InitializeComponent();
            if (GpuInvoke.HasCuda) // проверяем, есть ли поддержка  CUDA
                GPU = true;
            else
                GPU = false;

            if (GPU)
                GpuHand = new GpuCascadeClassifier("hand.xml"); // загружаем каскад для руки
                // GpuHand = new GpuCascadeClassifier("palm.xml"); // или для ладони
                // GpuHand = new GpuCascadeClassifier("fist.xml"); // или для кулака
            else
                Hand = new HaarCascade("hand.xml");
                // Hand = new HaarCascade("palm.xml"); // или для ладони
                // Hand = new HaarCascade("fist.xml"); // или для кулака
            NewHandPos = new PointF(pbCamera.Width / 2, pbCamera.Height / 2); // устанавливаем "руку" изначально на центр экрана
            HandPos = new PointF(pbCamera.Width / 2, pbCamera.Height / 2); // то же
            FormTitle = Text; // сохраняем изначальный заголовок формы
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            try
            {
                Camera = new Capture(cbCameras.SelectedIndex); // включаем камеру с номером указанном в комбобоксе
                Camera.FlipHorizontal = true; // меняем местами право и лево в кадре, иначе очень неудобно - двигаешь рукой вправо, в кадре движение идёт влево
            }
            catch (Exception except)
            {
                MessageBox.Show("Cannot capture from " + cbCameras.SelectedItem + " :\n" + except.Message);
                return;
            }
            cbCSIL.Enabled = false;
            cbCameras.Enabled = false;
            btStart.Enabled = false;
            btStop.Enabled = true;
            FrameTimer.Enabled = true;
            Iface.StartSignal();
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            FrameTimer.Enabled = false;
            Camera.Dispose();
            Iface.StopSignal();
            cbCSIL.Enabled = true;
            cbCameras.Enabled = true;
            btStart.Enabled = true;
            btStop.Enabled = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DsDevice[] Cameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice); // ищем все видеоустройства с функцией ввода

            if (Cameras.Length == 0)
            {
                cbCameras.Items.Add("Camera not found");
                cbCameras.Enabled = false;
                btStart.Enabled = false;
            }
            else
                for (int i = 0; i < Cameras.Length; i++)
                    cbCameras.Items.Add(String.Format("[{0}] {1}", i, Cameras[i].Name)); // добавляем их в список, индекс массива как раз будет совпадать с номером устройства
            
            Devices = SkinInterface.getSoundDevices(); // ищем все аудоустройства с функцией вывода

            if (Devices.Length == 0)
            {
                cbCSIL.Items.Add("Audio not found");
                cbCSIL.Enabled = false;
                btStart.Enabled = false;
            }

            for (int i = 0; i < Devices.Length; i++)
                cbCSIL.Items.Add(Devices[i].getName()); // тоже добавляем их по именам в список

            SignalPars = new SignalParams();
            SignalPars.Amplitude = 0.0f; // амплитуду уводим в ноль 
            SignalPars.Frequency = 40f; // частоту устанавливаем такой же как выбрана по умолчанию на форме

            cbCameras.SelectedIndex = 0;
            cbCSIL.SelectedIndex = 0;
        }

        // таймер, который вызывается 10 раз в секунду (т.е. Interval у него 100 мс, если это много можно уменьшить в его свойствах)
        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> Frame = Camera.QueryFrame(); // получаем очередной цветной кадр
            Stopwatch Timer = Stopwatch.StartNew(); // запускаем новый таймер

            Frame = Frame.Resize(pbCamera.Width, pbCamera.Height, INTER.CV_INTER_CUBIC); // масштабируем его на размеры PictureBox'а который стоит на форме
            if (GPU) // если поддержка CUDA имеется
            {
                GpuImage<Bgr, Byte> GpuFrame = new GpuImage<Bgr, byte>(Frame); // конвертируем тип Image в GpuImage
                GpuImage<Gray, Byte> GpuGrayFrame = GpuFrame.Convert<Gray, Byte>(); // преобразуем кадр в серый (каскады обучались на серых кадрах)
                Rectangle[] Hands = GpuHand.DetectMultiScale<Gray>(GpuGrayFrame, 1.1, 10, Size.Empty); // находим все прямоугольники, содержащие руку
                if (Hands.Length != 0) // если их нашлось больше нуля
                    NewHandPos = new PointF((Hands[0].X + Hands[0].X + Hands[0].Width) / 2, (Hands[0].Y + Hands[0].Y + Hands[0].Height) / 2); // находим и сохраняем центр прямоугольника
            }
            else // если поддержки CUDA нету, то всё тоже самое, с учётом типов
            {
                Image<Gray, Byte> grayFrame = Frame.Convert<Gray, Byte>();
                MCvAvgComp[] Hands = Hand.Detect(grayFrame);
                if (Hands.Length != 0)
                    NewHandPos = new PointF((Hands[0].rect.X + Hands[0].rect.X + Hands[0].rect.Width) / 2, (Hands[0].rect.Y + Hands[0].rect.Y + Hands[0].rect.Height) / 2); // находим и сохраняем центр прямоугольника
            }

            if (Distance(HandPos, NewHandPos) < 30) // проверяем расстояние
                HandPos = NewHandPos; // устанавливаем новую позицию руки на экране

            // рисуем вертикальную линию зелёного цвета - нашу виртуальную стенку
            Frame.Draw(new LineSegment2D(new Point(pbCamera.Width / 2 - 100, 0), new Point(pbCamera.Width / 2 - 100, pbCamera.Height)), new Bgr(0, 255, 0), 5);
            
            Frame.Draw(new CircleF(HandPos, 5), new Bgr(0, 255, 0), 2); // рисуем на месте руки кружок
            if (Math.Abs(HandPos.X - (pbCamera.Width / 2 - 100)) < 30) // если это место в кадре отстоит не более чем на 30 пикселей от линии, то...
                SignalPars.Amplitude = 0.7f; // вклчаем сигнал на амплитуду 0.7
            else
                SignalPars.Amplitude = 0.0f; // иначе отключаем сигнал

            pbCamera.Image = Frame.ToBitmap(); // выдаём кадр на PictureBox

            Timer.Stop(); // останавливаем таймер
            FPS = 1000 / (int) Timer.ElapsedMilliseconds; // получаем сколько это кадров в секунду
            if (Math.Abs(Timer.ElapsedMilliseconds - FrameTimer.Interval) > 10) // если таймер срабатывает слишком быстро или слишком медленно, то корректируем его
            {
                if (FrameTimer.Interval < Timer.ElapsedMilliseconds)
                    FrameTimer.Interval += 10;
                if (FrameTimer.Interval > Timer.ElapsedMilliseconds)
                    FrameTimer.Interval -= 10;
            }
            
            Text = FormTitle + " FPS = " + FPS + " (" + FrameTimer.Interval + " ms) Using: " + (GPU ? "CUDA GPU" : "CPU"); // обновляем заголовок
        }

        // смена устройства вывода для интерфейса
        private void cbCSIL_SelectedIndexChanged(object sender, EventArgs e)
        {
            Iface = new SkinInterface(Devices[cbCSIL.SelectedIndex], new SkinInterface.SignalFunction(Signal), SignalPars);
        }

        // изменение частоты сигнала
        private void udFreq_ValueChanged(object sender, EventArgs e)
        {
            SignalPars.Frequency = (float)udFreq.Value;
        }
    }
}
