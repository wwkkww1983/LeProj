using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace MotionCalc
{
    public partial class SetNetLine : Form
    {
        private HanlderNoParams refreshImageShow = null;
        public SetNetLine(HanlderNoParams refreshImageShow)
        {
            this.refreshImageShow = refreshImageShow;

            InitializeComponent();
            
            this.InitialInfo();
        }

        private void InitialInfo()
        {
            this.ckbShow.Checked = Constants.ShowNetFlag;
            this.tbNetWidth.Text = Constants.LineSeperationWidth.ToString ();
            this.tbNetHeight.Text = Constants.LineSeperationHeight.ToString ();
            this.tbWidth.Text = Constants.NetLineWidth.ToString();
            this.lbColor.BackColor = Constants.ColorNetLine;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!(StringValidator.HasContent(this.tbWidth, this.lbWidth.Text) &&
                StringValidator.HasContent(this.tbNetWidth, this.lbNetWidth.Text) &&
                StringValidator.HasContent(this.tbNetHeight, this.lbNetHeight.Text) &&

                StringValidator.IsUnsignedNumber(this.tbWidth) &&
                StringValidator.IsUnsignedNumber(this.tbNetWidth) &&
                StringValidator.IsUnsignedNumber(this.tbNetHeight))
                )
                return;

            Constants.ShowNetFlag = this.ckbShow.Checked;
            Constants.NetLineWidth = int.Parse(this.tbWidth.Text);
            Constants.LineSeperationHeight = int.Parse(this.tbNetHeight.Text);
            Constants.LineSeperationWidth = int.Parse(this.tbNetWidth.Text);
            Constants.ColorNetLine = this.lbColor.BackColor;

            Ini.SetItemValue("general", "showNet", this.ckbShow.Checked.ToString());
            Ini.SetItemValue("general", "netWidth", this.tbNetWidth.Text);
            Ini.SetItemValue("general", "netHeight", this.tbNetHeight.Text);
            Ini.SetItemValue("general", "netLineWidth", this.tbWidth.Text);
            Ini.SetItemValue("general", "netLineColor", this.lbColor.BackColor.ToArgb().ToString());

            this.refreshImageShow();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            dialog.Color = this.lbColor.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.lbColor.BackColor = dialog.Color;
            }
        }
    }
}
