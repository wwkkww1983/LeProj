using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Core;

namespace MotionCalc
{
    public partial class FormFullScreen : Form
    {
        private bool paulsePlayFlag = false, saveOneImageFlag = false;
        private VideoCapture capture;
        public FormFullScreen(VideoCapture capture)
        {
            this.capture = capture;

            InitializeComponent();

            this.capture.ImageGrabbed += this.capture_ImageGrabbed;
            this.imgBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;            
        }


        private void capture_ImageGrabbed(object sender, EventArgs e)
        {
            if (this.paulsePlayFlag) return;

            Mat frame = new Mat();
            this.capture.Read(frame);
            this.imgBox.Image = frame;
            double imgScale = this.getImgZoomScale(frame);
            this.imgBox.SetZoomScale(imgScale, new Point());
            if (this.saveOneImageFlag)
            {
                string imageFileNmae = Constants.IMAGE_FOLDER + DateTime.Now.ToString(Constants.MiloSecond_NAME_FORMAT) + Constants.IMAGE_FORMAT;
                frame.Save(imageFileNmae);
                this.saveOneImageFlag = false;
            }
            frame.Dispose();
        }

        private double getImgZoomScale(Mat frame)
        {
            double scaleWidth = (double)this.imgBox.Width / (double)frame.Size.Width;
            double scaleHeight = (double)this.imgBox.Height / (double)frame.Size.Height;

            return Math.Min(scaleWidth, scaleHeight);
        }

        private void FormFullScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space: this.paulsePlayFlag = !this.paulsePlayFlag; break;
                case Keys.Enter: this.saveOneImageFlag = true; break;
                case Keys.Escape: this.Hide(); break;
                default: break;
            }
        }

        private void FormFullScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
