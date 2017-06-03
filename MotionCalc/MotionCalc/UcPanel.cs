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
        private const int LINE_ONE_HEIGHT = NET_LINE_WIDTH + LINE_SEPERATION;
        private const int CIRCLE_RADIUS_BK=8, CIRCLE_RADIUS_INNER=4,CIRCLE_WIDTH_INNER=2;
        private const int POINT_MAX_JUMP = 100;
        private bool onePointClickFlag = false;
        private float imgScale;
        private Point rightClickPosition;
        private Color colorNetLine , colorUserLine;
        private List<Point> pixelBoardList = null, currentLinePoints = null, middleHVLinePoints=null;
        private ContextMenuStrip cmsHorizonVertical = null;


        public UcPanel()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

            this.middleHVLinePoints = new List<Point>();
            this.pixelBoardList = new List<Point>();
            this.currentLinePoints = new List<Point>();

            this.colorNetLine = Color.DarkRed;
            this.colorUserLine = Color.LightGray;

            this.InitialContextMenu();
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

        private void InitialContextMenu()
        {
            ToolStripMenuItem horizonMenu, verticalMenu;
            this.cmsHorizonVertical = new ContextMenuStrip(); 
            this.cmsHorizonVertical.SuspendLayout();
            
            horizonMenu = new ToolStripMenuItem();
            horizonMenu.Name = "horizonMenu";
            horizonMenu.Size = new System.Drawing.Size(108, 24);
            horizonMenu.Text = "水平";
            horizonMenu.Click += new System.EventHandler(this.horizonMenuItem_Click);

            verticalMenu = new ToolStripMenuItem();
            verticalMenu.Name = "verticalMenu";
            verticalMenu.Size = new System.Drawing.Size(108, 24);
            verticalMenu.Text = "垂直";
            verticalMenu.Click += new System.EventHandler(this.verticalMenuItem_Click);

            this.cmsHorizonVertical.Name = "cmsHorizonVertical";
            this.cmsHorizonVertical.Size = new Size(109, 100);
            this.cmsHorizonVertical.Items.AddRange(new ToolStripItem[] { horizonMenu, verticalMenu });
            this.ContextMenuStrip = this.cmsHorizonVertical;
            this.cmsHorizonVertical.ResumeLayout();
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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

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

        private void horizonMenuItem_Click(object sender, EventArgs e)
        {
            Point start = new Point(0, this.rightClickPosition.Y);
            Point end = new Point(this.Width, this.rightClickPosition.Y);

            this.middleHVLinePoints.Add(start);
            this.middleHVLinePoints.Add(end);

            this.drawOneMiddleLine(start, end);
        }

        private void verticalMenuItem_Click(object sender, EventArgs e)
        {
            Point start = new Point(this.rightClickPosition.X, 0);
            Point end = new Point(this.rightClickPosition.X, this.Height);

            this.middleHVLinePoints.Add(start);
            this.middleHVLinePoints.Add(end);

            this.drawOneMiddleLine(start, end);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Right) return;

            this.rightClickPosition=e.Location;
        }
        #endregion 

        #region 绘图

        public void DrawRecogPoints(int[] locList)
        {
            for (int i = 0; i < locList.Length; i += 2)
            {
                Point loc = new Point(locList[i], locList[i + 1]);
                Point locBoard = this.exchangeRecon_Board(loc);

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

        public void DrawNetLine()
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(Color.LightGray, NET_LINE_WIDTH);
                for (int i = 0; i < this.Height; i += LINE_ONE_HEIGHT)
                {
                    Point start = new Point(0, i);
                    Point end = new Point(this.Width, i);
                    g.DrawLine(pen, start, end);
                }

                for (int i = 0; i < this.Width; i += LINE_ONE_HEIGHT)
                {
                    Point start = new Point(i, 0);
                    Point end = new Point(i, this.Height);
                    g.DrawLine(pen, start, end);
                }
            }
        }

        private void drawOneMiddleLine(Point start,Point end)
        {
            this.drawOneLine(this.colorUserLine, start, end);
        }
        
        private void drawMiddleLines()
        {
            if (this.middleHVLinePoints.Count < 2) return;

            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(this.colorUserLine, USER_LINK_LINE_WIDTH);
                for (int i = 0; i < this.middleHVLinePoints.Count; i += 2)
                {
                    g.DrawLine(pen, this.middleHVLinePoints[i], this.middleHVLinePoints[i + 1]);
                }
            }
        }

        private void drawOneLine(PointF start, PointF end)
        {
            this.drawOneLine(this.colorNetLine, start, end);
        }

        private void drawOneLine(Color color, PointF start, PointF end)
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(color, USER_LINK_LINE_WIDTH);

                g.DrawLine(pen, start, end);
            }
        }

        public void DrawLines(int[] locList)
        {
            if (this.currentLinePoints.Count < 2) return;

            this.findLatestPairs(locList);

            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(this.colorNetLine, USER_LINK_LINE_WIDTH);

                for (int i = 0; i < this.currentLinePoints.Count; i += 2)
                {
                    g.DrawLine(pen, this.currentLinePoints[i], this.currentLinePoints[i + 1]);
                }
            }
        }
        #endregion

        #region 坐标变换/计算

        public double CalcLineAngle(int lineOne, int lineTwo)
        {
            double angle = 0d;

            if (this.currentLinePoints.Count < (lineOne+1)*2 || this.currentLinePoints.Count < (lineTwo+1)*2) return 0d;

            Point oneStart = this.currentLinePoints[lineOne * 2], oneEnd = this.currentLinePoints[lineOne * 2 + 1];
            Point twoStart = this.currentLinePoints[lineTwo * 2], twoEnd = this.currentLinePoints[lineTwo * 2 + 1];

            Point vectorOne = new Point (oneEnd.X-oneStart.X,oneEnd.Y-oneStart.Y);
            Point vectorTwo = new Point (twoEnd.X - twoStart.X,twoEnd.Y - twoStart.Y);

            double vectorMulti = vectorOne.X * vectorTwo.X + vectorOne.Y * vectorTwo.Y;
            double moduleMulti = Math.Sqrt(vectorOne.X * vectorOne.X + vectorOne.Y *vectorOne.Y) * Math.Sqrt(vectorTwo.X * vectorTwo.X + vectorTwo.Y * vectorTwo.Y);

            angle = Math.Acos(vectorMulti / moduleMulti) * 180d / Math.PI;
            angle = angle > 90 ? 180 - angle : angle;

            return angle;
        }

        private bool checkPointOnLine(Point clickPoint, int lineIdx)
        {
            if (this.currentLinePoints.Count < lineIdx - 2) return false;
            Point start = this.currentLinePoints[lineIdx];
            Point end = this.currentLinePoints[lineIdx + 1];

            return true;
        }

        private void findLatestPairs(int[] currentPoints)
        {
            List<Point> currentBoardList = new List<Point>();
            for (int i = 0; i < currentPoints.Length; i += 2)
            {
                Point loc = new Point(currentPoints[i], currentPoints[i + 1]);
                Point locBoard = this.exchangeRecon_Board(loc);

                currentBoardList.Add(locBoard);
            }

            this.calcLatestPointsForLine(currentBoardList);
        }

        private void calcLatestPointsForLine(List<Point> currentBoardList)
        {
            //TODO多个点对应到同一个点的情况没考虑
            List<Point> tempLinePoint = new List<Point>();
            foreach (Point oldPoint in this.currentLinePoints)
            {
                Point minPoint= new Point(-1, -1);
                double minDistance = double.MaxValue, tmpDistance = 0;
                foreach (Point newPoint in currentBoardList)
                {
                    tmpDistance = Math.Pow(oldPoint.X - newPoint.X, 2) + Math.Pow(oldPoint.Y - newPoint.Y, 2);
                    if (tmpDistance < minDistance)
                    {
                        minDistance = tmpDistance;
                        minPoint = newPoint;
                    }
                }
                //表示当前点消失，删除线条
                if (minDistance >= POINT_MAX_JUMP)
                    minPoint = oldPoint;// new Point(-1, -1);
                tempLinePoint.Add(minPoint);
            }

            this.currentLinePoints = tempLinePoint;
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
