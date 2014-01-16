using System;
using System.Collections.Generic;
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
using DirectShowLib;
using CSIL;

namespace VFF
{
    public partial class MainForm : Form
    {
        SkinInterface Iface;
        SignalParams SignalPars;
        SkinInterface.SoundDevice[] Devices;
        private Capture Camera;
        private HaarCascade Hand;
        private GpuCascadeClassifier GpuHand;
        private bool GPU;
        bool HandDetected;
        PointF HandPos;

        private class SignalParams : SkinInterface.SignalFunctionParams
        {
            public float Amplitude;
            public float Frequency;
        }

        static float Signal(float t, SkinInterface.SignalFunctionParams pars)
        {
            SignalParams Parameters = (SignalParams)pars;

            return Parameters.Amplitude * (float)Math.Sign(Math.Sin(2 * Math.PI * t * Parameters.Frequency));
        }

        public MainForm()
        {
            InitializeComponent();
            if (GpuInvoke.HasCuda)
                GPU = true;
            else
                GPU = false;

            if (GPU)
                GpuHand = new GpuCascadeClassifier("hand.xml");
            else
                Hand = new HaarCascade("hand.xml");

            HandDetected = false;
            HandPos = new PointF(0, 0);
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            try
            {
                Camera = new Capture(cbCameras.SelectedIndex);
                Camera.FlipHorizontal = true;
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
            DsDevice[] Cameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            if (Cameras.Length == 0)
            {
                cbCameras.Items.Add("Camera not found");
                cbCameras.Enabled = false;
                btStart.Enabled = false;
            }
            else
                for (int i = 0; i < Cameras.Length; i++)
                    cbCameras.Items.Add(String.Format("[{0}] {1}", i, Cameras[i].Name));
            
            Devices = SkinInterface.getSoundDevices();

            if (Devices.Length == 0)
            {
                cbCSIL.Items.Add("Audio not found");
                cbCSIL.Enabled = false;
                btStart.Enabled = false;
            }

            for (int i = 0; i < Devices.Length; i++)
                cbCSIL.Items.Add(Devices[i].getName());

            SignalPars = new SignalParams();
            SignalPars.Amplitude = 0.0f;
            SignalPars.Frequency = 200f;

            cbCameras.SelectedIndex = 0;
            cbCSIL.SelectedIndex = 0;
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> Frame = Camera.QueryFrame();

            Frame = Frame.Resize(pbCamera.Width, pbCamera.Height, INTER.CV_INTER_CUBIC);
            if (GPU)
            {
                GpuImage<Bgr, Byte> GpuFrame = new GpuImage<Bgr, byte>(Frame);
                GpuImage<Gray, Byte> GpuGrayFrame = GpuFrame.Convert<Gray, Byte>();
                Rectangle[] Hands = GpuHand.DetectMultiScale<Gray>(GpuGrayFrame, 1.1, 10, Size.Empty);
                if (Hands.Length != 0)
                {
                    HandDetected = true;
                    HandPos = new PointF((Hands[0].X + Hands[0].X + Hands[0].Width) / 2, (Hands[0].Y + Hands[0].Y + Hands[0].Height) / 2);
                }
            }
            else
            {
                Image<Gray, Byte> grayFrame = Frame.Convert<Gray, Byte>();
                var Hands = Hand.Detect(grayFrame);
                if (Hands.Length != 0)
                {
                    HandDetected = true;
                    HandPos = new PointF((Hands[0].rect.X + Hands[0].rect.X + Hands[0].rect.Width) / 2, (Hands[0].rect.Y + Hands[0].rect.Y + Hands[0].rect.Height) / 2);
                }
            }

            Frame.Draw(new LineSegment2D(new Point(pbCamera.Width / 2 - 100, 0), new Point(pbCamera.Width / 2 - 100, pbCamera.Height)), new Bgr(0, 255, 0), 5);
            
            if (HandDetected)
            {
                Frame.Draw(new CircleF(HandPos, 5), new Bgr(0, 255, 0), 2);
                if (Math.Abs(HandPos.X - (pbCamera.Width / 2 - 100)) < 30)
                    SignalPars.Amplitude = 0.7f;
                else
                    SignalPars.Amplitude = 0.0f;
            }

            pbCamera.Image = Frame.ToBitmap();

        }

        private void cbCSIL_SelectedIndexChanged(object sender, EventArgs e)
        {
            Iface = new SkinInterface(Devices[cbCSIL.SelectedIndex], new SkinInterface.SignalFunction(Signal), SignalPars);
        }

        private void udFreq_ValueChanged(object sender, EventArgs e)
        {
            SignalPars.Frequency = (float)udFreq.Value;
        }
    }
}
