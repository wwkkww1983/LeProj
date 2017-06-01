using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionCalc
{
    public partial class UcPanel : Panel
    {
        private const int LINE_WIDTH = 5, LINE_SEPERATION = 50;
        private const int LINE_ONE_HEIGHT = LINE_WIDTH + LINE_SEPERATION;

        //public UcPanel()
        //{
        //    SetStyle(ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
        //}

        //protected override CreateParams CreateParams//v1.10 
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x00000020; //0x20;  // 开启 WS_EX_TRANSPARENT,使控件支持透明
        //        return cp;
        //    }
        //}

        public void InitialDisplayInfo()
        {
            Point start = new Point(0, i * LINE_ONE_HEIGHT);
            Point end = new Point(this.Width, i * LINE_ONE_HEIGHT);
            this.DrawSolidLine(start, end);
            //for (int i = 0; i < this.Height; i += LINE_ONE_HEIGHT)
            //{
            //    Point start = new Point(0, i * LINE_ONE_HEIGHT);
            //    Point end = new Point(this.Width, i * LINE_ONE_HEIGHT );
            //    this.DrawSolidLine(start, end);
            //}
        }

        private void DrawSolidLine(Point start, Point end)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Blue, LINE_WIDTH);

            g.DrawLine(pen, start, end);
        }
    }
}
