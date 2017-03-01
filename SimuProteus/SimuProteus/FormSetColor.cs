using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class FormSetColor : Form
    {
        public FormSetColor()
        {
            InitializeComponent();



            this.btnGnd.BackColor = Color.FromArgb(int.Parse(Ini.GetItemValue("colorInfo", "colorGND")));
            this.btnVcc.BackColor = Color.FromArgb(int.Parse(Ini.GetItemValue("colorInfo", "colorVCC")));
            this.btnNone.BackColor = Color.FromArgb(int.Parse(Ini.GetItemValue("colorInfo", "colorNONE")));
            this.btnFoots.BackColor = Color.FromArgb(int.Parse(Ini.GetItemValue("colorInfo", "colorFoot")));
            this.btnLine.BackColor = Color.FromArgb(int.Parse(Ini.GetItemValue("colorInfo", "colorLine")));
        }

        private void btnGnd_Click(object sender, EventArgs e)
        {
            SetItemColor(this.btnGnd, "colorGND");
        }

        private void btnVcc_Click(object sender, EventArgs e)
        {

            SetItemColor(this.btnVcc, "colorVCC");
        }

        private void btnNone_Click(object sender, EventArgs e)
        {

            SetItemColor(this.btnNone, "colorNONE");
        }

        private void btnFoots_Click(object sender, EventArgs e)
        {

            SetItemColor(this.btnFoots, "colorFoot");
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            SetItemColor(this.btnLine, "colorLine");
        }

        private void SetItemColor(Button btn,string config)
        {
            dialog.Color = btn.BackColor;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btn.BackColor = dialog.Color;
                Ini.SetItemValue("colorInfo", config, btn.BackColor.ToArgb().ToString());
            }
        }
    }
}
