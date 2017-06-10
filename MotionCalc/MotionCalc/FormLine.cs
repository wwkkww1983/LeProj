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
        #region 初始化
        private delegate void HanlderNoParams();

        private const int MARGIN_BOUNDARY = 20, PLAY_SPEED_STEP = 10, PLAY_SPPED_DEFAULT = 100;
        private int playInterSleep = PLAY_SPPED_DEFAULT;
        private double imgScale;
        private bool pulsePlayFlag;
        private string recordFileName;
        private Mat videoFrame;
        private OpenFileDialog fileDialog = null;
        private VideoCapture capture = null;
        private UcPanel pnNetLine = null;
        private HanlderNoParams delegateDrawInfo, delegateDrawNet;

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

            this.imgBox.Location = new Point(12, 147);
            this.imgBox.Size = new Size(973, 776);
            this.imgBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;

            this.pnNetLine = new UcPanel();
            this.pnNetLine.Location = this.imgBox.Location;
            this.pnNetLine.Size = this.imgBox.Size;
            this.imgScale = this.pnNetLine.ImageScale;
            //this.pnNetLine.Paint += pnNetLine_Paint;
            this.Controls.Add(this.pnNetLine);
            this.pnNetLine.BringToFront();

            this.delegateDrawInfo = new HanlderNoParams(DrawRecognizedInfo);
            this.delegateDrawNet = new HanlderNoParams(DrawNetLine);

            this.videoFrame = new Mat();
        }
        #endregion

        #region 用户操作

        private void btnOpen_MouseClick(object sender, MouseEventArgs e)
        {
            this.pnNetLine.BringToFront();

            if (this.fileDialog.ShowDialog() != DialogResult.OK) return;

            this.recordFileName = this.fileDialog.FileName;

            this.capture = new VideoCapture(this.recordFileName);
            this.capture.ImageGrabbed += this.capture_ImageGrabbed;
            this.capture.Start();
        }

        private RadioButton GetCurrentColorButton()
        {
            RadioButton rbTemp = this.rbRed;

            if (this.rbBlace.Checked)
                rbTemp = this.rbBlace;
            else if (this.rbBlue.Checked)
                rbTemp = this.rbBlue;
            else if (this.rbGreen.Checked)
                rbTemp = this.rbGreen;
            else if (this.rbPurple.Checked)
                rbTemp = this.rbPurple;
            else if (this.rbWhite.Checked)
                rbTemp = this.rbWhite;
            else if (this.rbYellow.Checked)
                rbTemp = this.rbYellow;

            return rbTemp;
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

        private void capture_ImageGrabbed(object sender, EventArgs e)
        {
            while (this.pulsePlayFlag)
            {
                System.Threading.Thread.Sleep(PLAY_SPPED_DEFAULT);
            }
            this.capture.Read(this.videoFrame);
            this.imgBox.Image = this.videoFrame;
            System.Threading.Thread.Sleep(10);
            this.imgBox.SetZoomScale(this.imgScale, new Point());

            this.refreshUserInfo();

            System.Threading.Thread.Sleep(this.playInterSleep);
        }

        private void btnSaveImage_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = ".jpg";
            fileDialog.Filter = "视频图像文件(*.jpg)|*.jpg";
            fileDialog.RestoreDirectory = true;
            if (!(fileDialog.ShowDialog() == DialogResult.OK)) return;

            string fileName = fileDialog.FileName;

            Mat frameImg = new Mat();
            Size imageSize = new Size((int)(this.imgScale * Core.Constants.IMAGE_WIDTH), (int)(this.imgScale * Core.Constants.IMAGE_HEIGHT));
            CvInvoke.Resize(this.videoFrame, frameImg, imageSize);

            //CvInvoke.Line(frameImg,new Point (),new Point (),


            frameImg.Save(fileName);
        }

        public void RefreshImageShow()
        {
            this.imgBox.Refresh();

            if (this.ckbNet.Checked)
            {
                this.pnNetLine.DrawNetLine();
            }
            this.pnNetLine.DrawRecogPoints();
            this.pnNetLine.DrawLinesDefault();
            this.lbAngle.Text = this.pnNetLine.CalcLineAngle(0, 1).ToString("f2");
        }

        private void refreshUserInfo()
        {
            this.DrawNetLine();

            this.DrawRecognizedInfo();
        }
        #endregion

        #region 绘制
        private void DrawNetLine()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(this.delegateDrawNet);
                return;
            }

            if (this.ckbNet.Checked)
            {
                this.pnNetLine.DrawNetLine();
                //this.pnNetLine.BringToFront();
            }
        }

        private void DrawRecognizedInfo()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(this.delegateDrawInfo);
                return;
            }

            int[] locList = this.getLabelPoint();
            this.pnNetLine.DrawRecogPoints(locList);
            this.pnNetLine.DrawLines();
            this.lbAngle.Text = this.pnNetLine.CalcLineAngle(0, 1).ToString("f2");
        }

        private int[] getLabelPoint()
        {
            int[] result = new int[] { 50, 50, 200, 200, 22, 55, 600, 55 };
            //int directIdx = 1;
            //for (int i = 0; i < result.Length; i++)
            //{
            //    directIdx = (i+1) % 2 == 0 ? 3 : -1;
            //    result[i] += directIdx*inscreamIdx;
            //}
            //inscreamIdx++;
            return result;
        }
        #endregion
    }
}