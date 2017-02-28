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

        private Action<int,int > RemoveElement = null,ChangeColor = null;
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

        public UcLine(ElementLine info,Action<int,int> removeLine, Action<int,int> changeColor)
        {
            this.LineInfo = info;
            this.RemoveElement = removeLine;
            this.ChangeColor = changeColor;

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
            this.BackColor = this.LineInfo.Color;
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveElement(this.LineInfo.Idx, this.OtherLine.LineInfo.Idx);
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color lineColor = dialog.Color;
                this.BackColor = lineColor;
                this.OtherLine.BackColor = lineColor;
                this.ChangeColor(this.LineInfo.Idx, lineColor.ToArgb());
                this.ChangeColor(this.OtherLine.LineInfo.Idx, lineColor.ToArgb());
            }
        }
    }
}
