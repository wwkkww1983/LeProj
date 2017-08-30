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
using Emgu.CV.Structure;
using Core;

namespace MotionCalc
{
    public delegate void HanlderNoParams();

    public partial class FormLine : Form
    {
        #region 初始化
        private const int MARGIN_BOUNDARY = 20, PLAY_SPEED_STEP = 10, PLAY_SPPED_DEFAULT = 100;
        private int playInterSleep = PLAY_SPPED_DEFAULT;
        private double imgScale;
        private bool pulsePlayFlag;
        private string recordFileName;
        private object captureLocker = new object ();
        private Mat videoFrame,testFrame;
        private OpenFileDialog fileDialog = null;
        private VideoCapture capture = null;
        private UcPanel pnNetLine = null;
        private HanlderNoParams delegateDrawInfo = null, delegateDrawNet = null, delegateVideoScroll = null;
        private Action<double> delegateChangeShowScale = null;
        private Algorithm algoHandler = null;

        public FormLine()
        {
            InitializeComponent();

            this.InitialInfo();
        }

        private void InitialInfo()
        {
            this.fileDialog = new OpenFileDialog();
            this.fileDialog.Title = "请选择待分析的文件（视频在video，图片在image）";
            this.fileDialog.Filter = "待分析文件(*.*)|*.*";

            this.imgBox.Location = new Point(12, 31);
            this.imgBox.Size = new Size(973, 752);
            this.imgBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;

            this.pnNetLine = new UcPanel(this.ShowLineAngle);
            this.pnNetLine.Location = this.imgBox.Location;
            this.pnNetLine.Size = this.imgBox.Size;
            this.imgScale = this.pnNetLine.ImageScale;
            this.Controls.Add(this.pnNetLine);
            this.pnNetLine.BringToFront();
            //this.lbAngle.BackColor = 

            this.delegateDrawInfo = new HanlderNoParams(this.DrawRecognizedInfo);
            this.delegateDrawNet = new HanlderNoParams(this.DrawNetLine);
            this.delegateVideoScroll = new HanlderNoParams(this.moveVideoScroll);
            this.delegateChangeShowScale = new Action<double>(childSetImgShowScale);

            this.videoFrame = new Mat();
            this.testFrame = new Mat();
            this.algoHandler = new Algorithm();

            Constants.MinRecogRectArea = int.Parse(Ini.GetItemValue("general", "minLabelArea"));
            Constants.MaxRecogRectArea = int.Parse(Ini.GetItemValue("general", "maxLabelArea"));
            Constants.MinRecogRectWHRatio = float.Parse(Ini.GetItemValue("general", "minLabelWHRatio"));
            Constants.MaxRecogRectWHRatio = float.Parse(Ini.GetItemValue("general", "maxLabelWHRatio"));
            Constants.AutoRecogColorFlag = bool.Parse(Ini.GetItemValue("general", "autoColorRecog"));
            Constants.LabelColor = int.Parse(Ini.GetItemValue("general", "labelColor"));

            Constants.ShowNetFlag= bool.Parse(Ini.GetItemValue("general", "showNet"));
            Constants.NetLineWidth = int.Parse(Ini.GetItemValue("general", "netLineWidth"));
            Constants.LineSeperationHeight = int.Parse(Ini.GetItemValue("general", "netHeight"));
            Constants.LineSeperationWidth = int.Parse(Ini.GetItemValue("general", "netWidth"));
            Constants.ColorNetLine = Color.FromArgb(int.Parse(Ini.GetItemValue("general", "netLineColor")));
            
            Constants.RecogCircleWidthInner = int.Parse(Ini.GetItemValue("general", "selectedLineWidth"));
            Constants.RecogCircleColorInner = Color.FromArgb(int.Parse(Ini.GetItemValue("general", "selectedLineColor")));
            
            Constants.RecogCircleRadiusBK = int.Parse(Ini.GetItemValue("general", "recogPointRadius"));
            Constants.RecogCircleRadiusInner = int.Parse(Ini.GetItemValue("general", "recogPointLineRadius"));
            Constants.RecogCircleWidthInner = int.Parse(Ini.GetItemValue("general", "recogPointLineWidth"));
            Constants.RecogCircleColorBK = Color.FromArgb(int.Parse(Ini.GetItemValue("general", "recogPointColor")));
            Constants.RecogCircleColorInner = Color.FromArgb(int.Parse(Ini.GetItemValue("general", "recogPointLineColor")));
        }
        #endregion

        #region 用户操作

        private void FormLine_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space) this.pulsePlayFlag = true;
            switch (e.KeyCode)
            {
                case Keys.Left: this.playInterSleep += PLAY_SPEED_STEP; break;
                case Keys.Right: this.playInterSleep -= PLAY_SPEED_STEP; break;
                case Keys.Up:
                case Keys.Down: this.playInterSleep = PLAY_SPPED_DEFAULT; break;
                case Keys.Space: this.pulsePlayFlag = !this.pulsePlayFlag; break;
                default: break;
            }
            if (e.KeyCode != Keys.Space) this.pulsePlayFlag = false;

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
            lock (captureLocker)
            {
                this.capture.Retrieve(this.videoFrame);
            }
            this.imgBox.Image = this.videoFrame;

            System.Threading.Thread.Sleep(10);
            this.childSetImgShowScale(this.imgScale);
            this.DrawNetLine();
            this.DrawRecognizedInfo();
            this.moveVideoScroll();

            System.Threading.Thread.Sleep(this.playInterSleep);
        }

        private void childSetImgShowScale(double scale)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(delegateChangeShowScale, new object[] { scale });
                return;
            }

            this.imgBox.SetZoomScale(scale, new Point());
        }

        private void moveVideoScroll()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(this.delegateVideoScroll);
                return;
            }

            if(this.hSBarVideo.Value<this.hSBarVideo.Maximum) this.hSBarVideo.Value++;
        }

        private void drawNetLineForImage(Mat frameImg)
        {
            if (!Constants.ShowNetFlag) return;
            int lineOneHeight = Constants.NetLineWidth + Constants.LineSeperationHeight;
            int lineOneWidth = Constants.NetLineWidth + Constants.LineSeperationWidth;

            MCvScalar scalar = new MCvScalar(Constants.ColorNetLine.B, Constants.ColorNetLine.G, Constants.ColorNetLine.R);

            for (int i = 0; i < this.Height; i += lineOneHeight)
            {
                Point start = new Point(0, i);
                Point end = new Point(this.Width, i);
                CvInvoke.Line(frameImg, start, end, scalar, Constants.NetLineWidth);
            }

            for (int i = 0; i < this.Width; i += lineOneWidth)
            {
                Point start = new Point(i, 0);
                Point end = new Point(i, this.Height);
                CvInvoke.Line(frameImg, start, end, scalar, Constants.NetLineWidth);
            }
        }

        private void drawRecogPointsForImage(Mat frameImg)
        {
            List<Point> recgPointBoardList = this.pnNetLine.RecgPointBoardList;

            MCvScalar scalarCircleBK = new MCvScalar(Constants.RecogCircleColorBK.B, Constants.RecogCircleColorBK.G, Constants.RecogCircleColorBK.R);
            MCvScalar scalarCircleInner = new MCvScalar(Constants.RecogCircleColorInner.B, Constants.RecogCircleColorInner.G, Constants.RecogCircleColorInner.R);
            foreach (Point locBoard in recgPointBoardList)
            {
                Point circleCenter = new Point(locBoard.X, locBoard.Y);

                CvInvoke.Circle(frameImg, circleCenter, Constants.RecogCircleRadiusBK, scalarCircleBK, -1);
                CvInvoke.Circle(frameImg, circleCenter, Constants.RecogCircleRadiusInner, scalarCircleInner, Constants.RecogCircleWidthInner);
            }
        }

        private void drawUserLineForImage(Mat frameImg)
        {
            List<LineInfo> currentLinePoints = this.pnNetLine.CurrentLinePoints;

            for (int i = 0; i < currentLinePoints.Count; i++)
            {
                LineInfo info = currentLinePoints[i];
                MCvScalar scalar = new MCvScalar(info.Color.B, info.Color.G, info.Color.R);
                CvInvoke.Line(frameImg, info.One, info.Other, scalar, info.Width);
            }
        }

        public void RefreshImageShow()
        {
            this.imgBox.Refresh();

            if (Constants.ShowNetFlag)
            {
                this.pnNetLine.DrawNetLine();
            }
            this.pnNetLine.DrawRecogPoints();
            this.pnNetLine.DrawLinesDefault();
        }

        private void ShowLineAngle(double angle)
        {
            this.lbAngle.Text = angle.ToString("f2");
        }

        private void hSBarVideo_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > this.hSBarVideo.Maximum) return;

            this.pulsePlayFlag = true;

            lock (captureLocker)
            {
                this.capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, e.NewValue);
            }
        }

        private void hSBarVideo_MouseLeave(object sender, EventArgs e)
        {
            this.pulsePlayFlag = false;
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

            if (Constants.ShowNetFlag)
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

            List<Point> locList =new List<Point> ();
            if (Constants.AutoRecogColorFlag)
            {
                locList = this.algoHandler.RecognizeColor(this.videoFrame, Constants.LabelColor, ref this.testFrame);
            }
            
            this.pnNetLine.DrawRecogPoints(locList);
            this.pnNetLine.DrawLines();

            //this.imageBox1.Image = this.testFrame;
        }
        #endregion

        #region 菜单事件
        private void setNetLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetNetLine formSet = new SetNetLine(this.RefreshImageShow);
            formSet.ShowDialog();
        }

        private void setRecogPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRecogPoint formSet = new SetRecogPoint(this.RefreshImageShow);
            formSet.ShowDialog();
        }

        private void setRecogLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetRecogLine formSet = new SetRecogLine(this.RefreshImageShow);
            formSet.ShowDialog();
        }

        private void setColorLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetColorLabel formSet = new SetColorLabel(this.RefreshImageShow);
            formSet.ShowDialog();
        }
        
        private void openVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pnNetLine.BringToFront();

            this.fileDialog.InitialDirectory = System.Environment.CurrentDirectory;
            if (this.fileDialog.ShowDialog() != DialogResult.OK) return;

            this.recordFileName = this.fileDialog.FileName;

            string strExtension = System.IO.Path.GetExtension(this.recordFileName).ToLower();
            if (strExtension == ".jpg")
            {
                this.videoFrame = CvInvoke.Imread(this.recordFileName, Emgu.CV.CvEnum.ImreadModes.Unchanged);
                this.imgBox.Image = this.videoFrame;
                this.hSBarVideo.Visible = false;
                Constants.IMAGE_WIDTH = this.videoFrame.Width;
                Constants.IMAGE_HEIGHT = this.videoFrame.Height;
                this.imgScale = this.pnNetLine.ImageScale;
                this.childSetImgShowScale(this.imgScale);
            }
            else
            {
                this.hSBarVideo.Visible = true;
                this.capture = new VideoCapture(this.recordFileName);
                this.hSBarVideo.Maximum = (int)this.capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);

              //  this.capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, 100);

                this.hSBarVideo.Value = 0;
                this.capture.ImageGrabbed += this.capture_ImageGrabbed;
                this.capture.Start();
                Constants.IMAGE_WIDTH = this.capture.Width;
                Constants.IMAGE_HEIGHT = this.capture.Height;
                this.imgScale = this.pnNetLine.ImageScale;
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.videoFrame.Height == 0 || this.videoFrame.Width == 0)
            {
                MessageBox.Show("未选择文件", "无内容可保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = ".jpg";
            fileDialog.Filter = "视频图像文件(*.jpg)|*.jpg";
            fileDialog.InitialDirectory = System.Environment.CurrentDirectory + "\\" + Constants.IMAGE_FOLDER_NAME;
            if (!(fileDialog.ShowDialog() == DialogResult.OK)) return;

            string fileName = fileDialog.FileName;

            Mat frameImg = new Mat();
            Size imageSize = new Size((int)(this.imgScale * Constants.IMAGE_WIDTH), (int)(this.imgScale * Constants.IMAGE_HEIGHT));
            CvInvoke.Resize(this.videoFrame, frameImg, imageSize);
            this.drawNetLineForImage(frameImg);
            this.drawRecogPointsForImage(frameImg);
            this.drawUserLineForImage(frameImg);

            frameImg.Save(fileName);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }
        #endregion

    }
}