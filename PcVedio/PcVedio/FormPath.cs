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
            }

            this.tbPath.Text = Ini.GetItemValue("img");
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
    }
}
