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
using Core;

namespace MotionCalc
{
    public partial class FormMain : Form
    {
        #region 初始化
        private bool isPlaying = true,recordSaveFlag = false;
        private int secondRecordCount = 0;
        private string imageFileName = string.Empty, recordFileName = string.Empty;
        private UCImageBox[] imgBoxArray = null;

        public FormMain()
        {
            InitializeComponent();

            this.InitialCamera();
        }

        private void InitialCamera()
        {
            CvInvoke.UseOpenCL = false;
            UCImageBox imgBoxLU, imgBoxRU, imgBoxLD, imgBoxRD;

            imgBoxLU = new UCImageBox(0);
            imgBoxLU.Location = new Point(12, 33);
            imgBoxLU.Size = new Size(483, 431);
            this.Controls.Add(imgBoxLU);

            imgBoxRU = new UCImageBox(1);
            imgBoxRU.Location = new Point(501, 33);
            imgBoxRU.Size = new Size(483, 431);
            this.Controls.Add(imgBoxRU);

            imgBoxLD = new UCImageBox(2);
            imgBoxLD.Location = new Point(12, 470);
            imgBoxLD.Size = new Size(483, 431);
            this.Controls.Add(imgBoxLD);

            imgBoxRD = new UCImageBox(3);
            imgBoxRD.Location = new Point(501, 470);
            imgBoxRD.Size = new Size(483, 431);
            this.Controls.Add(imgBoxRD);

            this.imgBoxArray = new UCImageBox[] { imgBoxLU, imgBoxRU, imgBoxLD, imgBoxRD };
        }
        #endregion 

        #region 窗体事件

        private void recordVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.recordSaveFlag)
            {
                this.recordVideoToolStripMenuItem.Text = "录制视频";
                this.recordVideoToolStripMenuItem.ForeColor = SystemColors.ControlText;
                this.recordVideoToolStripMenuItem.BackColor = SystemColors.Control;
                this.recordSaveFlag = false;
                this.timerRecord.Enabled = false;

                foreach (UCImageBox imgBox in this.imgBoxArray)
                {
                    imgBox.StopRecord();
                }
            }
            else
            {
                this.recordVideoToolStripMenuItem.Text = "停止录制";
                this.recordVideoToolStripMenuItem.ForeColor = Color.White;
                this.recordVideoToolStripMenuItem.BackColor = Color.Red;
                this.recordSaveFlag = true;
                this.timerRecord.Enabled = true;
                this.secondRecordCount = 0;

                string recordFileName = Constants.VIDEO_FOLDER + DateTime.Now.ToString(Constants.Second_NAME_FORMAT);
                foreach (UCImageBox imgBox in this.imgBoxArray)
                {
                    imgBox.StartRecord(recordFileName);
                }
            }
        }

        private void saveImgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string imageFileName = Constants.IMAGE_FOLDER + DateTime.Now.ToString(Constants.MiloSecond_NAME_FORMAT);
            foreach (UCImageBox imgBox in this.imgBoxArray)
            {
                imgBox.SaveImage(imageFileName);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();
        }

        private void timerRecord_Tick(object sender, EventArgs e)
        {
            this.secondRecordCount++;
            string strTime = string.Format("（{0}:{1}）", this.secondRecordCount / 60, this.secondRecordCount % 60);
            this.recordVideoToolStripMenuItem.Text = "停止录制" + strTime;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space: this.ClickKeyboardSpace(); break;
                default: break;
            }
        }

        private void ClickKeyboardSpace()
        {
            foreach (UCImageBox imgBox in this.imgBoxArray)
            {
                if (this.isPlaying)
                {
                    imgBox.PulsePlay();
                }
                else
                {
                    imgBox.RestartPlay();
                }
            }

            this.isPlaying = !this.isPlaying;
        }

        #endregion

    }

}
