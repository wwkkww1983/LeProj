using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Core;

namespace MotionCalc
{
    public partial class UCImageBox : UserControl
    {
        private bool imageSaveFlag,recordSaveFlag,pulsePlayFlag;
        private int VideoIdx;
        private string VideoIdxString, imageFileName, recordFileName;
        private VideoCapture capture = null;
        private FormFullScreen fullScreen = null;
        private VideoWriter vwRecord = null;
        private object recordLock = new object();
        
        public UCImageBox(int idx)
        {
            this.VideoIdx = idx;

            InitializeComponent();
            
            this.InitialCamera();
            this.VideoIdxString =( (char)('A' + this.VideoIdx)).ToString ();            
        }

        private void InitialCamera()
        {
            this.capture = new VideoCapture(this.VideoIdx);
            this.fullScreen = new FormFullScreen(this.capture);
            this.capture.ImageGrabbed += this.capture_ImageGrabbed;

            //this.capture.SetCaptureProperty(CapProp.FrameWidth, 1600);
            //this.capture.SetCaptureProperty(CapProp.FrameHeight, 1200);

            this.capture.Start();

            this.imgBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imgBox.DoubleClick += imgBox_DoubleClick;
        }

        private void capture_ImageGrabbed(object sender, EventArgs e)
        {
            if (this.pulsePlayFlag) return;

            Mat frame = new Mat();
            capture.Read(frame);

            this.imgBox.Image = frame;
            double imgScale = this.getImgZoomScale(frame);
            this.imgBox.SetZoomScale(imgScale, new Point());
            if (this.imageSaveFlag)
            {
                this.SaveImage(frame);
            }
            else if (this.recordSaveFlag)
            {
                this.StartRecord(frame);
            }
            frame.Dispose();
        }

        private double getImgZoomScale(Mat frame)
        {
            double scaleWidth = (double)this.imgBox.Width / (double)frame.Size.Width;
            double scaleHeight = (double)this.imgBox.Height / (double)frame.Size.Height;

            return Math.Min(scaleWidth, scaleHeight);
        }

        public void SaveImage(string fileName)
        {
            this.imageFileName = fileName;
            this.imageSaveFlag = true;
        }

        private void SaveImage(Mat frame)
        {
            frame.Save(this.imageFileName + "_" + this.VideoIdxString + Constants.IMAGE_FORMAT);
            this.imageFileName = string.Empty;
            this.imageSaveFlag = false;
        }

        public void StartRecord(string fileName)
        {
            this.recordFileName = fileName;
            this.recordSaveFlag = true;
        }

        private void StartRecord(Mat frame)
        {
            lock (this.recordLock)
            {
                if (this.vwRecord == null)
                {
                    this.vwRecord = new VideoWriter(this.recordFileName + "_" + this.VideoIdxString + Constants.VIDEO_FORMAT, 18, new Size(frame.Width, frame.Height), true);
                }
                this.vwRecord.Write(frame);
            }
        }

        public void StopRecord()
        {
            this.recordSaveFlag = false;
            lock (this.recordLock)
            {
                if (this.vwRecord != null)
                {
                    this.vwRecord.Dispose();
                    this.vwRecord = null;
                }
            }
        }

        public void PulsePlay()
        {
            this.pulsePlayFlag = false;
        }

        public void RestartPlay()
        {
            this.pulsePlayFlag = true;
        }


        private void imgBox_DoubleClick(object sender, EventArgs e)
        {
            if (!capture.IsOpened) return;

            fullScreen.Show();
            fullScreen.BringToFront();
        }

    }
}
