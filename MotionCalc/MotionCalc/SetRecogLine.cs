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
    public partial class SetRecogLine : Form
    {
        private HanlderNoParams refreshImageShow = null;
        public SetRecogLine(HanlderNoParams refreshImageShow)
        {
            this.refreshImageShow = refreshImageShow;

            InitializeComponent();

            this.InitialInfo();
        }

        private void InitialInfo()
        {
            this.tbWidth.Text = Constants.RecogCircleWidthInner.ToString ();
            this.lbColorLine.BackColor = Constants.RecogCircleColorInner;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!(StringValidator.HasContent(this.tbWidth, this.lbWidth.Text) &&

               StringValidator.IsUnsignedNumber(this.tbWidth)
               ))
                return;

            Constants.RecogCircleWidthInner = int.Parse(this.tbWidth.Text);
            Constants.RecogCircleColorInner = this.lbColorLine.BackColor;

            Ini.SetItemValue("general", "selectedLineWidth", this.tbWidth.Text);
            Ini.SetItemValue("general", "selectedLineColor", this.lbColorLine.BackColor.ToArgb().ToString());

            this.refreshImageShow();
            this.Close();

        }

        private void lbColorLine_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.lbColorLine.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.lbColorLine.BackColor = dialog.Color;
            }
        }
    }
}
