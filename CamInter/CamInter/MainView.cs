using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamInter
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void CamLensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewLens viewShow = new ViewLens();
            viewShow.ShowDialog();
        }

        private void FocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(EnumType.Focus);
            viewShow.ShowDialog();
        }

        private void AdapterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(EnumType.Adapter);
            viewShow.ShowDialog();
        }

        private void ExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(EnumType.Extend);
            viewShow.ShowDialog();
        }
    }
}
