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

namespace PcVedio
{
    public partial class FormPath : Form
    {
        private string lan = string.Empty;
        public FormPath(string lan)
        {
            InitializeComponent();

            this.lan = lan;
            if (this.lan == "en")
            {
                btnImg.Text = "Photo Folder";
                btnOpen.Text = "Open";
                btnVideoPath.Text = "Record Folder";
                btnVideoOpen.Text = "Open";
            }

            this.tbPath.Text = Ini.GetItemValue("img");
            this.tbVideoPath.Text = Ini.GetItemValue("video");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = Ini.GetItemValue("img");
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tbPath.Text = dialog.SelectedPath;
                Ini.SetItemValue("img", this.tbPath.Text);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", this.tbPath.Text);
        }

        private void btnVideoPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = Ini.GetItemValue("video");
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tbVideoPath.Text = dialog.SelectedPath;
                Ini.SetItemValue("video", this.tbVideoPath.Text);
            }
        }

        private void btnVideoOpen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", this.tbVideoPath.Text);
        }
    }
}
