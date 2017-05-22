using System;
using System.Windows;  
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace MotionCalc
{
    public partial class Form1 : Form
    {
        #region 初始化
        private const string VIDEO_NAME_FORMAT = "yyyyMMddHHmmss"; 
        private bool recordFlag = false;
        private int secondCount = 0;        
        private VideoCapture captureLU, captureRU, captureLD, captureRD;

        public Form1()
        {
            InitializeComponent();

            this.InitialCamera();
        }

        private void InitialCamera()
        {            
            captureLU = new VideoCapture(0);
            captureRU = new VideoCapture(1);
            captureLD = new VideoCapture(2);
            captureRD = new VideoCapture(3);

            CvInvoke.UseOpenCL = false;
            if (captureLU.IsOpened)
            {
                captureLU.ImageGrabbed += captureLU_ImageGrabbed;
                captureLU.Start();
            }

            if (captureRU.IsOpened)
            {
                captureRU.ImageGrabbed += captureRU_ImageGrabbed;
                captureRU.Start();
            }

            if (captureLD.IsOpened)
            {
                captureLD.ImageGrabbed += captureLD_ImageGrabbed;
                captureLD.Start();
            }

            if (captureRD.IsOpened)
            {
                captureRD.ImageGrabbed += captureRD_ImageGrabbed;
                captureRD.Start();
            }
        }

        private void captureLU_ImageGrabbed(object sender, EventArgs e)
        {
            this.ShowVideo(captureLU, imgBoxLU);
        }

        private void captureRU_ImageGrabbed(object sender, EventArgs e)
        {
            this.ShowVideo(captureRU, imgBoxRU);
        }

        private void captureLD_ImageGrabbed(object sender, EventArgs e)
        {
            this.ShowVideo(captureLD, imgBoxLD);
        }

        private void captureRD_ImageGrabbed(object sender, EventArgs e)
        {
            this.ShowVideo(captureRD,imgBoxRD);
        }

        private void ShowVideo(VideoCapture capture,Emgu.CV.UI.ImageBox imgBox)
        { 
            Mat frame = new Mat();
            capture.Read(frame);
            imgBox.Image = frame;
            frame.Dispose();
        }
        #endregion 

        #region 窗体事件
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (this.recordFlag)
            {
                this.btnRecord.Text = "录制视频";
                this.btnRecord.ForeColor = SystemColors.ControlText;
                this.btnRecord.BackColor = SystemColors.Control;
                this.recordFlag = false;
                this.timerRecord.Enabled = false;
            }
            else
            {
                this.btnRecord.Text = "停止录制";
                this.btnRecord.ForeColor = Color.White;
                this.btnRecord.BackColor = Color.Red;
                this.recordFlag = true;
                this.timerRecord.Enabled = true;
                this.secondCount = 0;
            }
        }

        private void timerRecord_Tick(object sender, EventArgs e)
        {
            this.secondCount++;
            string strTime = string.Format("（{0}:{1}）",this.secondCount / 60, this.secondCount%60);
            this.btnRecord.Text = "停止录制" + strTime;
        }
        #endregion
    }
}
