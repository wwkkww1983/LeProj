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
    public partial class FormMain : Form
    {
        Command cmd = new Command();
        public FormMain()
        {
            InitializeComponent();
        }

        private void wifiSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmd.ConnectWifi();
        }

        private void wifiConnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void wifiBreakToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void wifiSetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void videoPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void videoStopToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void audioPlayToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void audioStopToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
