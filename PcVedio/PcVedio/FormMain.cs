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

namespace PcVedio
{
    public partial class FormMain : Form
    {
        private bool photo = false, record = false;
        private bool NewFlag = false,InitialFlag = false;
        private string photoName = "yyyy-MM-dd HH：mm：ss：fff.",recordName=string.Empty;
        private string imgPath = string.Empty, videoPath = string.Empty,lan = string.Empty;
        private byte[] buffVedio = new byte[] { }, buffNew;
        private Command cmd =null;
        

        public FormMain()
        {
            InitializeComponent();

            //FFmpegSharp.Executor.Encoder.Create()
            //    .WidthInput("img\\13-03-2017_23-04-01-222.jpg")
            //    .To<Mp4>("14-03-2017.mp4")
            //    .Execute();
                
            //VideoInfo input1 = VideoInfo.FromFileInfo(new FileInfo("img\\13-03-2017_23-04-01-222.jpg"));
            //encoder.ToMp4(input1, new FileInfo("img\\13-03-2017_23-04-01-222.mp4"));
            //VideoInfo output1 = VideoInfo.FromFileInfo(new FileInfo("img\\13-03-2017_23-04-01-222.mp4"));

            //VideoInfo input2 = VideoInfo.FromFileInfo(new FileInfo("img\\13-03-2017_2304-02-536.jpg"));
            //encoder.ToMp4(input1, new FileInfo("img\\13-03-2017_2304-02-536.mp4"));
            //VideoInfo output2 = VideoInfo.FromFileInfo(new FileInfo("img\\13-03-2017_2304-02-536.mp4"));



            //encoder.Join(new FileInfo("14-03-2017.mp4"), new VideoInfo[] { output1 }); 
            //VideoInfo output = VideoInfo.FromFileInfo(new FileInfo("14-03-2017.mp4"));

            //encoder.Join(new FileInfo("15-03-2017.mp4"), new VideoInfo[] { output,output2 }); 
            ////VideoInfo input2 = VideoInfo.FromFileInfo(new FileInfo("img\\13-03-2017_2304-02-536.jpg"));
            

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
            cmd = new Command();
            System.Threading.Thread.Sleep(500);
            this.timerPlay.Enabled = true;
            this.timerAlive.Enabled = true;
            cmd.ConnectWifi();
            while (buffVedio.Length < 10)
            {
                if (cmd.Status)
                {
                    buffVedio = cmd.PlayVideo(buffVedio, ref NewFlag);
                    buffVedio = cmd.PlayVideo(buffVedio, ref NewFlag);
                    InitialFlag = true;
                    break;
                }
            }
        }

        private void wifiBreakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timerPlay.Enabled = false;
            this.timerAlive.Enabled = false;
            cmd.CloseWifi();
        }

        private void timerPlay_Tick(object sender, EventArgs e)
        {
            if (InitialFlag)
            {
                buffNew = cmd.PlayVideo(buffVedio, ref NewFlag);

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
                        else if (record)
                        {
                            //var input1 = VideoInfo.FromFileInfo(new FileInfo("13-03-2017_23-04-01-222.jpg"));
                            //var input2 = VideoInfo.FromFileInfo(new FileInfo("13-03-2017_23-04-01-222.jpg"));
                            //encoder.ToMp4(input1, new FileInfo("13-03-2017.mp4"));
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
                lan = "cn";
                Ini.SetItemValue("lan", lan);
            }
        }

        private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            photo = true;
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

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (videoToolStripMenuItem.Text != "Record" || videoToolStripMenuItem.Text != "录像")
            {
                record = true;
                recordName = imgPath + DateTime.Now.ToString(photoName) + "mp4";
            }
        }
    }
}
