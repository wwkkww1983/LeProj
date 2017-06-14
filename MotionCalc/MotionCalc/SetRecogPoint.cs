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
    public partial class SetRecogPoint : Form
    {
        private HanlderNoParams refreshImageShow = null;
        public SetRecogPoint(HanlderNoParams refreshImageShow)
        {
            this.refreshImageShow = refreshImageShow;

            InitializeComponent();

            this.InitialInfo();
        }

        private void InitialInfo()
        {
            this.tbRadius.Text = Constants.RecogCircleRadiusBK.ToString ();
            this.tbRadiusLine.Text = Constants.RecogCircleRadiusInner.ToString ();
            this.tbWidth.Text = Constants.RecogCircleWidthInner.ToString ();
            this.lbColorPoint.BackColor = Constants.RecogCircleColorBK;
            this.lbColorLine.BackColor = Constants.RecogCircleColorInner;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!(StringValidator.HasContent(this.tbWidth, this.lbWidth.Text) &&
               StringValidator.HasContent(this.tbRadiusLine, this.lbRadiusLine.Text) &&
               StringValidator.HasContent(this.tbRadius, this.lbRadius.Text) &&

               StringValidator.IsUnsignedNumber(this.tbWidth) &&
               StringValidator.IsUnsignedNumber(this.tbRadiusLine) &&
               StringValidator.IsUnsignedNumber(this.tbRadius))
               )
                return;


            Constants.RecogCircleRadiusBK = int.Parse(this.tbRadius.Text);
            Constants.RecogCircleRadiusInner = int.Parse(this.tbRadiusLine.Text);
            Constants.RecogCircleWidthInner = int.Parse(this.tbWidth.Text);
            Constants.RecogCircleColorBK = this.lbColorPoint.BackColor;
            Constants.RecogCircleColorInner = this.lbColorLine.BackColor;

            Ini.SetItemValue("general", "recogPointRadius", this.tbRadius.Text);
            Ini.SetItemValue("general", "recogPointLineRadius", this.tbRadiusLine.Text);
            Ini.SetItemValue("general", "recogPointLineWidth", this.tbWidth.Text);
            Ini.SetItemValue("general", "recogPointColor", this.lbColorPoint.BackColor.ToArgb().ToString());
            Ini.SetItemValue("general", "recogPointLineColor", this.lbColorLine.BackColor.ToArgb().ToString());

            this.refreshImageShow();
            this.Close();
        }

        private void lbColorPoint_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = this.lbColorPoint.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.lbColorPoint.BackColor = dialog.Color;
            }

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
