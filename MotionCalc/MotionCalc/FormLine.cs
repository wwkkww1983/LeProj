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

namespace MotionCalc
{
    public partial class FormLine : Form
    {
        private const int MARGIN_BOUNDARY = 20, PLAY_SPEED_STEP = 10, PLAY_SPPED_DEFAULT = 100;
        private int playInterSleep = PLAY_SPPED_DEFAULT;
        private bool pulsePlayFlag;
        private string recordFileName;
        private OpenFileDialog fileDialog = null;
        private VideoCapture capture = null;
        private UcPanel pnNetLine = null;

        public FormLine()
        {
            InitializeComponent();

            this.InitialInfo();
        }

        private void InitialInfo()
        {
            this.fileDialog = new OpenFileDialog();
            this.fileDialog.Title = "请选择待分析的视频文件";
            this.fileDialog.Filter = "视频文件(*.avi)|*.AVI";

            this.imgBox.Location = new Point(12,73);
            this.imgBox.Size = new Size(973, 850);

            this.pnNetLine = new UcPanel();
            this.pnNetLine.Location = this.imgBox.Location;
            this.pnNetLine.Size = this.imgBox.Size;
            this.pnNetLine.InitialDisplayInfo();
            this.Controls.Add(this.pnNetLine);
            this.pnNetLine.BringToFront();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.fileDialog.ShowDialog() != DialogResult.OK) return;

            this.recordFileName = this.fileDialog.FileName;

            this.capture = new VideoCapture(this.recordFileName);
            this.capture.ImageGrabbed += this.capture_ImageGrabbed;
            this.capture.Start();
        }

        private void FormLine_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: this.playInterSleep += PLAY_SPEED_STEP; break;
                case Keys.Right: this.playInterSleep -= PLAY_SPEED_STEP; break;
                case Keys.Up:
                case Keys.Down: this.playInterSleep = PLAY_SPPED_DEFAULT; break;
                case Keys.Space: this.pulsePlayFlag = !this.pulsePlayFlag; break;
                default: break;
            }

            if (this.playInterSleep <= PLAY_SPEED_STEP)
            {
                this.playInterSleep = PLAY_SPEED_STEP;
            }
        }

        private double getImgZoomScale(Mat frame)
        {
            double scaleWidth = (double)this.imgBox.Width / (double)frame.Size.Width;
            double scaleHeight = (double)this.imgBox.Height / (double)frame.Size.Height;

            return Math.Min(scaleWidth, scaleHeight);
        }

        private void capture_ImageGrabbed(object sender, EventArgs e)
        {
            if (this.pulsePlayFlag) return;

            Mat frame = new Mat();
            this.capture.Read(frame);

            this.imgBox.Image = frame;

            double imgScale = this.getImgZoomScale(frame);
            this.imgBox.SetZoomScale(imgScale, new Point());

            System.Threading.Thread.Sleep(this.playInterSleep);

            frame.Dispose();
        }

        private void ckbNet_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckbTemp = sender as CheckBox;
            
        }
    }
}