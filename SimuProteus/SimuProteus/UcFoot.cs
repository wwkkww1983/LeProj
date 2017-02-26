using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class UcFoot : UserControl
    {
        public Action<int, int, int> ClickFoot
        {
            get;
            set;
        }

        private int index, locX, locY;

        public UcFoot(int idx,int x,int y)
        {
            this.index = idx;
            this.locX = x;
            this.locY = y;

            InitializeComponent();

            this.Width = Constants.FOOT_SIZE_PIXEL;
            this.Height = Constants.FOOT_SIZE_PIXEL;
            int hsize = Constants.FOOT_SIZE_PIXEL / 2;
            this.Location = new Point(this.locX + hsize, this.locY + hsize);
        }

        private void UcFoot_Click(object sender, EventArgs e)
        {
            if (this.ClickFoot != null)
                this.ClickFoot(this.index, this.locX, this.locY);
        }
    }
}
