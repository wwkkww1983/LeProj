/// <summary>
/// copyright:  Zac (suoxd123@126.com)
/// 2017-03-14
/// </summary>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Core;

namespace PcVedio
{
    public partial class FormMain : Form
    {
        private bool photo = false, record = false,active=false;
        private bool NewFlag = false,InitialFlag = false;
        private string photoName = "yyyy-MM-dd HH：mm：ss：fff.",recordName=string.Empty;
        private string imgPath = string.Empty, videoPath = string.Empty,lan = string.Empty;
        private string VideoTmpFolder = "\\tmp\\", currentFolder = System.Environment.CurrentDirectory;
        private string strPhotoSuccess = "拍照成功";
        private byte[] buffVedio = new byte[] { }, buffNew;
        private int rIdx = 1;
        private Command cmd = new Command();
        private Process p = new Process();
        

        public FormMain()
        {
            InitializeComponent();
            lan = Ini.GetItemValue("lan");
            imgPath = Ini.GetItemValue("img");
            videoPath = Ini.GetItemValue("video");
            if (lan == "en")
            {
                SetLan(true);
            }
        }

        private void wifiConnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmd.ConnectWifi();
            System.Threading.Thread.Sleep(500);
            int iCount = 1;
            this.active = true;
            while (buffVedio.Length < 10)
            {
                if (!cmd.Status && iCount > 10)
                {
                    MessageBox.Show("网络连接失败");
                    break;
                }
                if (cmd.Status)
                {
                    buffVedio = cmd.PlayVideo(buffVedio, ref NewFlag);
                    buffVedio = cmd.PlayVideo(buffVedio, ref NewFlag);
                    InitialFlag = true;
                    break;
                }
                else
                {
                    cmd.ConnectWifi();
                    System.Threading.Thread.Sleep(100);
                    iCount++;
                }
            }

            this.timerPlay.Enabled = true;
            this.timerAlive.Enabled = true;
        }

        private void wifiBreakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.active = false;
            this.timerPlay.Enabled = false;
            this.timerAlive.Enabled = false;
            //cmd.CloseWifi();
        }
        int falseCount;
        private void timerPlay_Tick(object sender, EventArgs e)
        {
            if (InitialFlag && this.active)
            {
                buffNew = cmd.PlayVideo(buffVedio, ref NewFlag);
                //if (NewFlag)
                //{
                //    Console.WriteLine(falseCount);
                //    falseCount = 0;
                //}
                //else
                //    falseCount++;

                if (NewFlag)
                {
                    using (Stream s = new MemoryStream(buffVedio))
                    {
                        Image img = Image.FromStream(s);
                        this.picBoxVideo.Image = img;
                        if (photo)
                        {
                            img.Save(imgPath + "/"+ DateTime.Now.ToString(photoName) + "jpg");
                            photo = false;
                        }                        
                        if (record)
                        {
                            img.Save(videoPath + VideoTmpFolder + string.Format("{0:0000}", rIdx++) + ".jpg");
                            if (rIdx >= 9999)
                            {
                                MessageBox.Show("Time Over");
                            }
                        }
                    }
                }

                buffVedio = buffNew;
            }
        }

        private void timerAlive_Tick(object sender, EventArgs e)
        {
            if (InitialFlag)
            {
                cmd.KeepAlive();
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLan(true);
        }

        private void chineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLan(false);
        }

        private void SetLan(bool isEnglish)
        {
            if (isEnglish)
            {
                this.wifiToolStripMenuItem.Text = "Video";
                this.LanguageToolStripMenuItem.Text = "Language";
                this.pictureToolStripMenuItem.Text = "Photograph";
                this.videoToolStripMenuItem.Text = "Record";
                this.wifiBreakToolStripMenuItem.Text = "Disconnect";
                this.wifiConnToolStripMenuItem.Text = "Connect";
                this.photoName = "dd-MM-yyyy_HH-mm-ss-fff.";
                this.setToolStripMenuItem.Text = "Picture Path";
                this.strPhotoSuccess = "Photo Saved";
                lan = "en";
                Ini.SetItemValue("lan", lan);
            }
            else
            {
                this.wifiToolStripMenuItem.Text = "视频";
                this.LanguageToolStripMenuItem.Text = "语言";
                this.pictureToolStripMenuItem.Text = "拍照";
                this.videoToolStripMenuItem.Text = "录像";
                this.wifiBreakToolStripMenuItem.Text = "断开";
                this.wifiConnToolStripMenuItem.Text = "连接";
                this.photoName = "yyyy-MM-dd HH：mm：ss：fff.";
                this.setToolStripMenuItem.Text = "文件路径";
                this.strPhotoSuccess = "拍照成功";
                lan = "cn";
                Ini.SetItemValue("lan", lan);
            }
        }

        private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckVideo())
            {
                photo = true;
                this.lbStatus.Text = this.strPhotoSuccess;
                timerStatus.Enabled = true;
            }
        }

        private void ClearStatusShow()
        {
            if (this.InvokeRequired)
            {
                Action delegateClearStatus = new Action(ClearStatusShow);
                this.Invoke(delegateClearStatus);
                return;
            }
            System.Threading.Thread.Sleep(3000);
            this.lbStatus.Text = string.Empty;
        }

        private void setToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPath form = new FormPath(lan);
            if (lan == "en")
            {
                form.Text = "Change Photo Path";
            }
            form.ShowDialog();
        }

        private bool CheckVideo()
        {
            if (!this.active)
            {
                MessageBox.Show("No Data");
            }
            return this.active;
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckVideo()) return;

            if (videoToolStripMenuItem.Text == "Record" || videoToolStripMenuItem.Text == "录像")
            {
                if (lan == "en")
                {
                    videoToolStripMenuItem.Text = "Stop";
                }
                else
                {
                    videoToolStripMenuItem.Text = "停止";
                }
                rIdx = 1;
                if (this.p.StartInfo.Arguments.Length > 10 && !this.p.HasExited)
                    this.p.Kill();
                videoToolStripMenuItem.BackColor = Color.Red;
                record = true;
                Directory.CreateDirectory(videoPath + "\\tmp");
            }
            else
            {
                if (lan == "en")
                {
                    videoToolStripMenuItem.Text = "Record";
                }
                else
                {
                    videoToolStripMenuItem.Text = "录像";
                }
                videoToolStripMenuItem.BackColor = this.BackColor;
                record = false;
                this.CreateVideo();
            }
        }

        private void CreateVideo()
        {
            string tmpImgPath = videoPath, videoName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff.") + "mp4";
            if (!videoPath.Contains(':'))
                tmpImgPath = currentFolder + "\\" + videoPath;
            string ffmpegPath = string.Format("{0}\\x{1}\\ffmpeg.exe", currentFolder,OperateSystem.Is64Bits?"64":"86");
            ProcessStartInfo startInfo = new ProcessStartInfo(ffmpegPath);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            string outFilePath = tmpImgPath + "\\" + videoName;
            startInfo.Arguments = string.Format("-i {0}{1}%4d.jpg -vcodec libx264 {2}", tmpImgPath, VideoTmpFolder, outFilePath);
            p.StartInfo = startInfo;
            p.Exited += RenewTmpFolder;
            p.Start();
        }

        private void RenewTmpFolder(object sender, EventArgs e)
        {
            if (!videoPath.Contains(':'))
            {
                videoPath = currentFolder + "\\" + videoPath;
            }
            if (Directory.Exists(videoPath + VideoTmpFolder))
            {
                Directory.Delete(videoPath + VideoTmpFolder, true);
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            System.Environment.Exit(0); 
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            this.lbStatus.Text = string.Empty;
            timerStatus.Enabled = false;
        }
    }
}
