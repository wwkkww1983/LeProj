using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionCalc
{
    public partial class FormLine : Form
    {
        private string recordFileName;
        private OpenFileDialog fileDialog = null;
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
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.fileDialog.ShowDialog() != DialogResult.OK) return;

            this.recordFileName = this.fileDialog.FileName;
        }

        private void FormLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void FormLine_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: break;
                case Keys.Right: break;
                case Keys.Space: break;
                default: break;
            }
        }
    }
}
