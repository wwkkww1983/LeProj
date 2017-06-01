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
        private const int LINE_WIDTH = 5, LINE_SEPERATION = 50, CIRCLE_RADIUS_BK=8, CIRCLE_RADIUS_INNER=4,CIRCLE_WIDTH_INNER=2;
        private const int LINE_ONE_HEIGHT = LINE_WIDTH + LINE_SEPERATION;

        public UcPanel()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override CreateParams CreateParams//v1.10 
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //0x20;  // 开启 WS_EX_TRANSPARENT,使控件支持透明
                return cp;
            }
        }
        
        public void InitialDisplayInfo()
        {
            for (int i = 0; i < this.Height; i += LINE_ONE_HEIGHT)
            {
                Point start = new Point(0, i);
                Point end = new Point(this.Width, i);
                this.DrawSolidLine(start, end);
            }

            for (int i = 0; i < this.Width; i += LINE_ONE_HEIGHT)
            {
                Point start = new Point(i, 0);
                Point end = new Point(i, this.Height);
                this.DrawSolidLine(start, end);
            }
        }

        public void DrawLabelCircle(Point loc,double ratio)
        {
            float tmpRatio = (float)ratio;
            PointF tmpLoc = new PointF(loc.X * tmpRatio, loc.Y * tmpRatio);

            Graphics g = this.CreateGraphics();
            Brush brushBk = new SolidBrush(Color.Yellow);
            g.FillEllipse(brushBk, tmpLoc.X - CIRCLE_RADIUS_BK, tmpLoc.Y - CIRCLE_RADIUS_BK, CIRCLE_RADIUS_BK * 2, CIRCLE_RADIUS_BK * 2);

            Pen penInner = new Pen(Color.Black, CIRCLE_WIDTH_INNER);
            g.DrawEllipse(penInner, tmpLoc.X - CIRCLE_RADIUS_INNER, tmpLoc.Y - CIRCLE_RADIUS_INNER, CIRCLE_RADIUS_INNER * 2, CIRCLE_RADIUS_INNER * 2);
        }

        private void DrawSolidLine(Point start, Point end)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Gray, LINE_WIDTH);

            g.DrawLine(pen, start, end);
        }
    }
}
