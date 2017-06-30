using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunisoft.IrisSkin;

namespace MotionCalc
{
    public partial class FormFirst : Form
    {
        private SkinEngine skin = null;

        public FormFirst()
        {
            InitializeComponent();

            //this.skin = new SkinEngine(this);
            //this.skin.SkinFile = "Wave.ssk";
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.ShowDialog();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            FormLine formLine = new FormLine();
            formLine.ShowDialog();
        }
    }
}
