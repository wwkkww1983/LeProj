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
        #region 初始化
        private const int NET_LINE_WIDTH = 4, LINE_SEPERATION = 50, USER_LINK_LINE_WIDTH = 4;
        private const int CIRCLE_RADIUS_BK=8, CIRCLE_RADIUS_INNER=4,CIRCLE_WIDTH_INNER=2;
        private const int LINE_ONE_HEIGHT = NET_LINE_WIDTH + LINE_SEPERATION;
        private bool onePointClickFlag = false;
        private float imgScale;
        private List<Point> pixelRecogPointList = null,pixelBoardList = null, currentLinePoints = null;

        public UcPanel()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            
            this.pixelRecogPointList = new List<Point>();
            this.pixelBoardList = new List<Point>();
            this.currentLinePoints = new List<Point>();

            this.MouseClick += UcPanel_MouseClick;
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
                this.drawNetLine(start, end);
            }

            for (int i = 0; i < this.Width; i += LINE_ONE_HEIGHT)
            {
                Point start = new Point(i, 0);
                Point end = new Point(i, this.Height);
                this.drawNetLine(start, end);
            }
        }

        public float ImageScale
        {
            get
            {
                this.imgScale = this.getImgZoomScale();
                return this.imgScale;
            }
        }
        #endregion 

        #region 用户交互
        void UcPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Point clickPoint = this.checkClickPoint(e.Location);
            if (clickPoint.X < 0) return;

            if (this.onePointClickFlag)
            {
                Point start = this.currentLinePoints[this.currentLinePoints.Count - 1];
                Point end = clickPoint;

                if (start.X == end.X && start.Y == end.Y) return;

                this.drawOneLine(start, end);
            }
            this.currentLinePoints.Add(clickPoint);
            this.onePointClickFlag = !this.onePointClickFlag;
        }
        #endregion 

        #region 绘图

        public void DrawRecogPoints(int[] locList)
        {
            for (int i = 0; i < locList.Length; i += 2)
            {
                Point loc = new Point(locList[i], locList[i + 1]);
                Point locBoard = this.exchangeRecon_Board(loc);

                this.pixelRecogPointList.Add(loc);
                this.pixelBoardList.Add(locBoard);

                this.drawLabelCircle(loc);
            }
        }

        private void drawLabelCircle(Point loc)
        {
            Point tmpLoc = this.exchangeRecon_Board(loc);

            Graphics g = this.CreateGraphics();
            Brush brushBk = new SolidBrush(Color.Yellow);
            g.FillEllipse(brushBk, tmpLoc.X - CIRCLE_RADIUS_BK, tmpLoc.Y - CIRCLE_RADIUS_BK, CIRCLE_RADIUS_BK * 2, CIRCLE_RADIUS_BK * 2);

            Pen penInner = new Pen(Color.Black, CIRCLE_WIDTH_INNER);
            g.DrawEllipse(penInner, tmpLoc.X - CIRCLE_RADIUS_INNER, tmpLoc.Y - CIRCLE_RADIUS_INNER, CIRCLE_RADIUS_INNER * 2, CIRCLE_RADIUS_INNER * 2);
        }

        private void drawNetLine(Point start, Point end)
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(Color.Gray, NET_LINE_WIDTH);

                g.DrawLine(pen, start, end);
            }
        }

        private void drawOneLine(PointF start, PointF end)
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(Color.Green, USER_LINK_LINE_WIDTH);

                g.DrawLine(pen, start, end);
            }
        }

        //private void DrawLines(int[] locList)
        //{
        //    this.
        //    using (Graphics g = this.CreateGraphics())
        //    {
        //        Pen pen = new Pen(Color.Green, USER_LINK_LINE_WIDTH);

        //        g.DrawLine(pen, start, end);
        //    }


        //    if (this.pixelRecogPointList.Count == 0)
        //    {
        //        this.pixelRecogPointList = pointList;
        //    }
        //}
        #endregion

        #region 坐标变换/计算



        private void findLatestPairs(int[] currentPoints)
        {
            List<Point> currentList = new List<Point>(), currentBoardList = new List<Point>();
            for (int i = 0; i < currentPoints.Length; i += 2)
            {
                Point loc = new Point(currentPoints[i], currentPoints[i + 1]);
                Point locBoard = this.exchangeRecon_Board(loc);

                currentList.Add(loc);
                currentBoardList.Add(locBoard);
            }



            Dictionary<int, Point> lineTOcurrent = new Dictionary<int, Point>();
            foreach (Point oldPoint in this.currentLinePoints)
            {
                Point minPoint;
                double minDistance = double.MaxValue, tmpDistance = 0 ;
                foreach (Point newPoint in currentBoardList)
                {
                    tmpDistance = Math.Pow(oldPoint.X - newPoint.X, 2) + Math.Pow(oldPoint.Y - newPoint.Y, 2);
                    if (tmpDistance < minDistance)
                    {
                        minDistance = tmpDistance;
                        minPoint = newPoint;
                    }
                }
                //if(minDistance >= 99) 表示当前点消失，删除线条
                //lineTOcurrent.Add(lineIdx, minPoint);
            }
        }

        private Point checkClickPoint(Point loc)
        {
            Point clickPoint = new Point (-1,-1);
            foreach( Point item in this.pixelBoardList)
            {
                double circle = Math.Pow(loc.X - item.X, 2) + Math.Pow(loc.Y - item.Y, 2);

                if (circle <= CIRCLE_RADIUS_BK * CIRCLE_RADIUS_BK)
                {
                    clickPoint = item;
                    break;
                }
            }
            return clickPoint;
        }

        private float getImgZoomScale(Size frame)
        {
            double scaleWidth = (double)this.Width / (double)frame.Width;
            double scaleHeight = (double)this.Height / (double)frame.Height;

            return (float)Math.Min(scaleWidth, scaleHeight);
        }

        private float getImgZoomScale()
        {
            double scaleWidth = (double)this.Width / 640d;
            double scaleHeight = (double)this.Height / 480d;

            return (float)Math.Min(scaleWidth, scaleHeight);
        }

        private int exchangeRecon_Board(int loc)
        {
            return  (int)(loc * this.imgScale);
        }

        private Point exchangeRecon_Board(Point loc)
        {
            return new Point (this.exchangeRecon_Board(loc.X),this.exchangeRecon_Board(loc.Y)) ;
        }
        
        private int exchangeBoard_Recognition(int loc)
        {
            return (int)(loc / this.imgScale);
        }

        public Point exchangeBoard_Recognition(Point loc)
        {
            return new Point(this.exchangeBoard_Recognition(loc.X), this.exchangeBoard_Recognition(loc.Y));
        }

        #endregion
    }
}
