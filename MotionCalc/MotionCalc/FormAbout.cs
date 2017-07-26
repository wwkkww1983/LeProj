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
    public partial class FormAbout : Form
    {
        int sizeIcon = 60;
        public FormAbout()
        {
            InitializeComponent();

            this.pbLogo.Size = new Size(sizeIcon, sizeIcon);
            int xValue = (this.Width - sizeIcon) / 2;
            this.pbLogo.Location = new Point(xValue, 20);
        }
    }
}
