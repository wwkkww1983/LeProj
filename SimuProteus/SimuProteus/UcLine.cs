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
    public partial class UcLine : UserControl
    {

        private Action<int,int > RemoveElement = null;
        public ElementLine LineInfo
        {
            get;
            set;
        }

        public UcLine OtherLine
        {
            get;
            set;
        }

        public UcLine(ElementLine info,Action<int,int> removeLine)
        {
            this.LineInfo = info;
            this.RemoveElement = removeLine;

            InitializeComponent();

            if (info.LocOtherX == info.LocX)
            {
                this.Height = Math.Abs(info.LocY - info.LocOtherY);
                this.Width = Constants.LINE_LINK_WIDTH;
            }
            else
            {
                this.Width = Math.Abs(info.LocX - info.LocOtherX);
                this.Height = Constants.LINE_LINK_WIDTH;
            }
            this.Location = new Point(Math.Min(info.LocX,info.LocOtherX),Math.Min(info.LocY,info.LocOtherY));
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveElement(this.LineInfo.Idx, this.OtherLine.LineInfo.Idx);
        }
    }
}
