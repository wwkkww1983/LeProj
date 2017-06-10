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
    public partial class FormLineWidth : Form
    {
        Action<int> setLineWidth;
        public FormLineWidth(int width,Action<int> setLineWidth)
        {
            InitializeComponent();

            this.tbWidth.Text = width.ToString();
            this.setLineWidth = setLineWidth;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!StringValidator.HasContent(this.tbWidth,this.lbWidth.Text) || !StringValidator.IsUnsignedNumber(this.tbWidth)) return;

            this.setLineWidth(int.Parse(this.tbWidth.Text));

            this.Close();
        }
    }
}
