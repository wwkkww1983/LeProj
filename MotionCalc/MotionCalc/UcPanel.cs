using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace MotionCalc
{
    public partial class UcPanel : Panel
    {
        #region 初始化
        private const int NET_LINE_WIDTH = 4, LINE_SEPERATION = 50, USER_LINK_LINE_WIDTH = 4;
        private const int LINE_ONE_HEIGHT = NET_LINE_WIDTH + LINE_SEPERATION;
        private const int CIRCLE_RADIUS_BK = 8, CIRCLE_RADIUS_INNER = 4, CIRCLE_WIDTH_INNER = 2;
        private const int POINT_MAX_JUMP = 100;
        private const double DOUBLE_MAX_DIFF = 1e-6;
        private bool onePointClickFlag = false;
        private EnumLineType selectedLineType;
        private int selectedLineIdx = -1;
        private float imgScale;
        private Point rightClickPosition;
        private Pen penCircleInner = null;
        private Brush brushCircleBK = null;
        private Color colorNetLine, colorUserLine, colorCircleInner, colorCircleBK;
        private List<Point> recgPointBoardList = null;
        private List<LineInfo> currentLinePoints = null, middleHVLinePoints = null;
        private ContextMenuStrip cmsHorizonVertical = null, cmsUserLineHanler = null;


        public UcPanel()
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

            this.recgPointBoardList = new List<Point>();
            this.middleHVLinePoints = new List<LineInfo>();
            this.currentLinePoints = new List<LineInfo>();

            this.colorNetLine = Color.DarkRed;
            this.colorUserLine = Color.LightGreen;
            this.colorCircleInner = Color.Black;
            this.colorCircleBK = Color.Yellow;

            this.InitialContextMenu();
            this.penCircleInner = new Pen(this.colorCircleInner, CIRCLE_WIDTH_INNER);
            this.brushCircleBK = new SolidBrush(this.colorCircleBK);
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


            ToolStripMenuItem lineColor, lineWidth, lineDel, lineExtend;
            this.cmsUserLineHanler = new ContextMenuStrip();
            this.cmsUserLineHanler.SuspendLayout();

            lineColor = new ToolStripMenuItem();
            lineColor.Name = "lineColor";
            lineColor.Size = new System.Drawing.Size(108, 24);
            lineColor.Text = "颜色";
            lineColor.Click += new System.EventHandler(this.lineColor_Click);

            lineWidth = new ToolStripMenuItem();
            lineWidth.Name = "lineWidth";
            lineWidth.Size = new System.Drawing.Size(108, 24);
            lineWidth.Text = "粗细";
            lineWidth.Click += new System.EventHandler(this.lineWidth_Click);

            lineDel = new ToolStripMenuItem();
            lineDel.Name = "lineDel";
            lineDel.Size = new System.Drawing.Size(108, 24);
            lineDel.Text = "删除";
            lineDel.Click += new System.EventHandler(this.lineDel_Click);

            lineExtend = new ToolStripMenuItem();
            lineExtend.Name = "lineExtend";
            lineExtend.Size = new System.Drawing.Size(108, 24);
            lineExtend.Text = "延长";
            lineExtend.Click += new System.EventHandler(this.lineExtend_Click);

            this.cmsUserLineHanler.Name = "cmsUserLineHanler";
            this.cmsUserLineHanler.Size = new Size(109, 100);
            this.cmsUserLineHanler.Items.AddRange(new ToolStripItem[] { lineColor, lineWidth, lineDel, lineExtend });
            this.cmsUserLineHanler.ResumeLayout();
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

            int lineIdx = this.getSelectedLine(e.Location);

            Point clickPoint = this.checkClickPoint(e.Location);
            if (clickPoint.X < 0) return;

            if (this.onePointClickFlag)
            {
                int idx = this.currentLinePoints.Count - 1;
                LineInfo info = this.currentLinePoints[idx];

                if (info.One.X == clickPoint.X && info.One.Y == clickPoint.Y) return;
                info.Other = clickPoint;

                this.drawOneLine(info);
            }
            else
            {
                LineInfo info = new LineInfo()
                {
                    Idx = this.currentLinePoints.Count,
                    One = clickPoint,
                    Color = this.colorUserLine,
                    Width = USER_LINK_LINE_WIDTH
                };
                this.currentLinePoints.Add(info);
            }
            this.onePointClickFlag = !this.onePointClickFlag;
        }

        private void horizonMenuItem_Click(object sender, EventArgs e)
        {
            LineInfo info = new LineInfo()
            {
                Idx = this.currentLinePoints.Count,
                One = new Point(0, this.rightClickPosition.Y),
                Other = new Point(this.Width, this.rightClickPosition.Y),
                Color = this.colorUserLine,
                Width = USER_LINK_LINE_WIDTH
            };
            this.middleHVLinePoints.Add(info);

            this.drawOneLine(info);
        }

        private void verticalMenuItem_Click(object sender, EventArgs e)
        {
            LineInfo info = new LineInfo()
            {
                Idx = this.middleHVLinePoints.Count,
                One = new Point(this.rightClickPosition.X, 0),
                Other = new Point(this.rightClickPosition.X, this.Height),
                Color = this.colorUserLine,
                Width = USER_LINK_LINE_WIDTH
            };
            this.middleHVLinePoints.Add(info);

            this.drawOneLine(info);
        }

        private LineInfo getCurrentSelectedLine()
        {
            LineInfo tempLine = null;
            switch (this.selectedLineType)
            {
                case EnumLineType.UserLink:
                    tempLine = this.currentLinePoints[this.selectedLineIdx];
                    break;
                case EnumLineType.HVLine:
                    tempLine = this.middleHVLinePoints[this.selectedLineIdx];
                    break;
                default:
                    tempLine = new LineInfo();
                    break;
            }
            return tempLine;
        }

        private void lineColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color lineColor = dialog.Color;

                LineInfo selectedLine = this.getCurrentSelectedLine();
                selectedLine.Color = lineColor;
                this.refreshView();
            }
        }

        private void lineWidth_Click(object sender, EventArgs e)
        {
            FormLineWidth formWidth = new FormLineWidth(USER_LINK_LINE_WIDTH, this.resetLineWidth);
            formWidth.ShowDialog();
        }

        private void resetLineWidth(int width)
        {
            LineInfo selectedLine = this.getCurrentSelectedLine();
            selectedLine.Width = width;
            this.refreshView();
        }

        private void lineDel_Click(object sender, EventArgs e)
        {
            switch (this.selectedLineType)
            {
                case EnumLineType.UserLink:
                    this.currentLinePoints.RemoveAt(this.selectedLineIdx);
                    break;
                case EnumLineType.HVLine:
                    this.middleHVLinePoints.RemoveAt(this.selectedLineIdx);
                    break;
                default:
                    break;
            }
            this.refreshView();
        }

        private void lineExtend_Click(object sender, EventArgs e)
        {
            LineInfo selectedLine = this.getCurrentSelectedLine();
            List<Point> linePoints = this.calcLineCrossRectangle(selectedLine.One, selectedLine.Other, new Point(0, 0), new Point(this.Width, this.Height));

            if (linePoints.Count < 2) return;
            selectedLine.One = linePoints[0];
            selectedLine.Other = linePoints[1];

            this.refreshView();
        }

        private void refreshView()
        {
            (this.Parent as FormLine).RefreshImageShow();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.selectedLineIdx = this.getSelectedLine(e.Location);

            this.ContextMenuStrip = this.selectedLineIdx >= 0 ? this.cmsUserLineHanler : this.cmsHorizonVertical;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Right) return;

            this.rightClickPosition = e.Location;
        }
        #endregion

        #region 绘图

        public void DrawRecogPoints(int[] locList)
        {
            using (Graphics g = this.CreateGraphics())
            {

                for (int i = 0; i < locList.Length; i += 2)
                {
                    Point loc = new Point(locList[i], locList[i + 1]);
                    Point locBoard = this.exchangeRecon_Board(loc);

                    this.recgPointBoardList.Add(locBoard);
                    this.DrawRecogPoints(g, locBoard);
                }
            }
        }

        public void DrawRecogPoints()
        {
            using (Graphics g = this.CreateGraphics())
            {
                foreach (Point locBoard in this.recgPointBoardList)
                {
                    this.DrawRecogPoints(g, locBoard);
                }
            }
        }

        private void DrawRecogPoints(Graphics g, Point loc)
        {
            g.FillEllipse(this.brushCircleBK, loc.X - CIRCLE_RADIUS_BK, loc.Y - CIRCLE_RADIUS_BK, CIRCLE_RADIUS_BK * 2, CIRCLE_RADIUS_BK * 2);
            g.DrawEllipse(this.penCircleInner, loc.X - CIRCLE_RADIUS_INNER, loc.Y - CIRCLE_RADIUS_INNER, CIRCLE_RADIUS_INNER * 2, CIRCLE_RADIUS_INNER * 2);
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

        private void drawOneLine(LineInfo info)
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(info.Color, info.Width);

                g.DrawLine(pen, info.One, info.Other);
            }
        }

        private void drawMiddleLines()
        {
            if (this.middleHVLinePoints.Count < 2) return;

            using (Graphics g = this.CreateGraphics())
            {
                for (int i = 0; i < this.middleHVLinePoints.Count; i++)
                {
                    LineInfo info = this.middleHVLinePoints[i];
                    Pen pen = new Pen(info.Color, info.Width);
                    g.DrawLine(pen, info.One, info.Other);
                }
            }
        }

        public void DrawLines()
        {
            this.calcLatestPointsForLine();

            this.DrawLinesDefault();
        }

        public void DrawLines(Graphics g)
        {
            this.calcLatestPointsForLine();

            this.DrawLinesDefault(g);
        }

        public void DrawLinesDefault()
        {
            using (Graphics g = this.CreateGraphics())
            {
                this.DrawLinesDefault(g);
            }
        }

        public void DrawLinesDefault(Graphics g)
        {
            if (this.currentLinePoints.Count < 1) return;

            for (int i = 0; i < this.currentLinePoints.Count; i++)
            {
                LineInfo info = this.currentLinePoints[i];
                Pen pen = new Pen(info.Color, info.Width);
                g.DrawLine(pen, info.One, info.Other);
            }
            for (int i = 0; i < this.middleHVLinePoints.Count; i++)
            {
                LineInfo info = this.middleHVLinePoints[i];
                Pen pen = new Pen(info.Color, info.Width);
                g.DrawLine(pen, info.One, info.Other);
            }
        }

        #endregion

        #region 坐标变换/计算

        public double CalcLineAngle(int lineOne, int lineTwo)
        {
            double angle = 0d;

            if (this.currentLinePoints.Count <= lineOne || this.currentLinePoints.Count <= lineTwo) return 0d;

            Point oneStart = this.currentLinePoints[lineOne].One, oneEnd = this.currentLinePoints[lineOne].Other;
            Point twoStart = this.currentLinePoints[lineTwo].One, twoEnd = this.currentLinePoints[lineTwo].Other;

            Point vectorOne = new Point(oneEnd.X - oneStart.X, oneEnd.Y - oneStart.Y);
            Point vectorTwo = new Point(twoEnd.X - twoStart.X, twoEnd.Y - twoStart.Y);

            double vectorMulti = vectorOne.X * vectorTwo.X + vectorOne.Y * vectorTwo.Y;
            double moduleMulti = Math.Sqrt(vectorOne.X * vectorOne.X + vectorOne.Y * vectorOne.Y) * Math.Sqrt(vectorTwo.X * vectorTwo.X + vectorTwo.Y * vectorTwo.Y);

            angle = Math.Acos(vectorMulti / moduleMulti) * 180d / Math.PI;
            angle = angle > 90 ? 180 - angle : angle;

            return angle;
        }

        private double calcTwoPointsDistance(Point one, Point other)
        {
            return Math.Sqrt((one.X - other.X) * (one.X - other.X) + (one.Y - other.Y) * (one.Y - other.Y));
        }

        private bool checkPointOnLine(Point clickPoint, LineInfo info)
        {
            double distance = double.MaxValue;
            double distanceLine = this.calcTwoPointsDistance(info.One, info.Other);
            double distanceStart = this.calcTwoPointsDistance(info.One, clickPoint);
            double distanceEnd = this.calcTwoPointsDistance(info.Other, clickPoint);

            // 当水平或竖直时，区间的判断不合理
            //if (!((start.X <= clickPoint.X && clickPoint.X < end.X || end.X <= clickPoint.X && clickPoint.X < start.X) &&
            //    (start.Y <= clickPoint.Y && clickPoint.Y < end.Y || end.Y <= clickPoint.Y && clickPoint.Y < start.Y)))
            //    return false;

            if (distanceStart <= DOUBLE_MAX_DIFF || distanceEnd <= DOUBLE_MAX_DIFF)
            {//选中线段两个端点
                distance = 0d;
            }
            else if (distanceLine <= DOUBLE_MAX_DIFF)
            {//线段本身长度为0
                distance = distanceStart;
            }
            else if (distanceStart * distanceStart >= distanceLine * distanceLine + distanceEnd * distanceEnd)
            {//点在线段Other(End)这边的外侧
                distance = distanceEnd;
            }
            else if (distanceEnd * distanceEnd >= distanceLine * distanceLine + distanceStart * distanceStart)
            {//点在线段One(Start)这边的外侧
                distance = distanceStart;
            }
            else
            {//点在线段范围内
                double halfPerimeter = (distanceLine + distanceStart + distanceEnd) / 2d;
                double area = Math.Sqrt(halfPerimeter * (halfPerimeter - distanceLine) * (halfPerimeter - distanceStart) * (halfPerimeter - distanceEnd));
                distance = 2 * area / distanceLine;
            }
            return distance <= info.Width / 2d;
        }

        private int getSelectedLine(Point clickPoint)
        {
            this.selectedLineType = EnumLineType.None;

            int lineIdx = -1, lineCount = this.currentLinePoints.Count;
            for (int i = 0; i < lineCount; i++)
            {
                if (this.checkPointOnLine(clickPoint, this.currentLinePoints[i]))
                {
                    lineIdx = i;
                    this.selectedLineType = EnumLineType.UserLink;
                    break;
                }
            }
            if (lineIdx != -1) return lineIdx;

            lineCount = this.middleHVLinePoints.Count;
            for (int i = 0; i < lineCount; i++)
            {
                if (this.checkPointOnLine(clickPoint, this.middleHVLinePoints[i]))
                {
                    lineIdx = i;
                    this.selectedLineType = EnumLineType.HVLine;
                    break;
                }
            }

            return lineIdx;
        }

        private List<Point> calcLineCrossRectangle(Point lineOne, Point lineOther, Point rectLU, Point rectRD)
        {
            List<Point> crossPoints = new List<Point>();
            if (lineOne.X == lineOther.X)
            {//垂直线段
                if (!(rectLU.X <= lineOne.X && lineOne.X <= rectRD.X)) return crossPoints;
                crossPoints.Add(new Point(lineOne.X, rectLU.Y));
                crossPoints.Add(new Point(lineOne.X, rectRD.Y));
                return crossPoints;
            }
            else if (lineOne.Y == lineOther.Y)
            {//水平线段
                if (!(rectLU.Y <= lineOne.Y && lineOne.Y <= rectRD.Y)) return crossPoints;
                crossPoints.Add(new Point(rectLU.X, lineOne.Y));
                crossPoints.Add(new Point(rectRD.X, lineOne.Y));
                return crossPoints;
            }

            double lineK = ((double)lineOther.Y - (double)lineOne.Y) / ((double)lineOther.X - (double)lineOne.X);
            Point crossXL = new Point(rectLU.X, 0), crossXR = new Point(rectRD.X, 0), crossYU = new Point(0, rectLU.Y), crossYD = new Point(0, rectRD.Y);
            crossXL.Y = (int)(lineK * (rectLU.X - lineOne.X) + lineOne.Y);
            crossXR.Y = (int)(lineK * (crossXR.X - lineOne.X) + lineOne.Y);
            crossYU.X = (int)((crossYU.Y - lineOne.Y) / lineK + lineOne.X);
            crossYD.X = (int)((crossYD.Y - lineOne.Y) / lineK + lineOne.X);

            if (this.checkOnePointInRectangle(crossXL, rectLU, rectRD))
                crossPoints.Add(crossXL);
            if (this.checkOnePointInRectangle(crossXR, rectLU, rectRD))
                crossPoints.Add(crossXR);
            if (this.checkOnePointInRectangle(crossYU, rectLU, rectRD))
                crossPoints.Add(crossYU);
            if (this.checkOnePointInRectangle(crossYD, rectLU, rectRD))
                crossPoints.Add(crossYD);

            this.filterRepeateItems(crossPoints);
            return crossPoints;
        }

        private bool checkOnePointInRectangle(Point target, Point rectLU, Point rectRD)
        {
            return rectLU.X <= target.X && target.X <= rectRD.X && rectLU.Y <= target.Y && target.Y <= rectRD.Y;
        }

        private void filterRepeateItems(List<Point> itemList)
        {
            int itemCount = itemList.Count;

            for (int i = 0; i < itemCount; i++)
            {
                Point first = itemList[i];
                for (int j = i + 1; j < itemCount; j++)
                {
                    Point second = itemList[j];
                    if (first.X != second.X || first.Y != second.Y) continue;
                    itemList.RemoveAt(j);
                    itemCount--;
                }
            }
        }

        private void calcLatestPointsForLine()
        {
            //TODO多个点对应到同一个点的情况没考虑
            List<LineInfo> tempLinePoint = new List<LineInfo>();
            foreach (LineInfo oldLine in this.currentLinePoints)
            {
                LineInfo newInfo = new LineInfo()
                {
                    Idx = oldLine.Idx,
                    Color = oldLine.Color,
                    Width = oldLine.Width
                };
                newInfo.One = this.calcLatestPointsForLine(oldLine.One);
                newInfo.Other = this.calcLatestPointsForLine(oldLine.Other);

                tempLinePoint.Add(newInfo);
            }

            this.currentLinePoints = tempLinePoint;
        }

        private Point calcLatestPointsForLine(Point oldPoint)
        {
            Point minPoint = new Point(-1, -1);
            double minDistance = double.MaxValue, tmpDistance = 0;
            foreach (Point newPoint in this.recgPointBoardList)
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

            return minPoint;
        }

        private Point checkClickPoint(Point loc)
        {
            Point clickPoint = new Point(-1, -1);
            foreach (Point item in this.recgPointBoardList)
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
            double scaleWidth = (double)this.Width / Constants.IMAGE_WIDTH;
            double scaleHeight = (double)this.Height / Constants.IMAGE_HEIGHT;

            return (float)Math.Min(scaleWidth, scaleHeight);
        }

        private int exchangeRecon_Board(int loc)
        {
            return (int)(loc * this.imgScale);
        }

        private Point exchangeRecon_Board(Point loc)
        {
            return new Point(this.exchangeRecon_Board(loc.X), this.exchangeRecon_Board(loc.Y));
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