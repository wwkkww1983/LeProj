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
        private const int USER_LINK_LINE_WIDTH = 4;
        private const int  MAX_DISTANCE_FOR_DIFF_TWO_POINTS = 20;
        private const double DOUBLE_MAX_DIFF = 1e-6;
        private bool onePointClickFlag = false, userMovingLineFlag = false;
        private int selectedLineIdx = -1, imgRealWidth,imgRealHeight;
        private float imgScale;
        private Point rightClickPosition, lastMousePosition, selectedPoint,originPoint;
        //绘制线条的第一个点
        private Point OnePointForNewLine;
        private Color colorUserLine;
        private List<int> selectedLinePoints = null;
        /// <summary>
        /// recgPointBoardList 包括了识别的点和用户增加的点，每次绘图前讲用户增加的点加入
        /// </summary>
        private List<Point> recgPointBoardList = null,userAddPointsList = null, userHidePointsList = null;

        private List<LineInfo> currentLinePoints = null;
        private ContextMenuStrip cmsHorizonVertical = null, cmsUserLineHanler = null, cmsUserPointHandler = null;
        private Action<double> showAngleForTwoLine;

        public UcPanel(Action<double> showAngleForTwoLine)
        {
            this.showAngleForTwoLine = showAngleForTwoLine;

            SetStyle(ControlStyles.Opaque | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

            this.selectedLinePoints = new List<int>();
            this.recgPointBoardList = new List<Point>();
            this.userAddPointsList = new List<Point>();
            this.userHidePointsList = new List<Point>();
            this.currentLinePoints = new List<LineInfo>();

            this.colorUserLine = Color.LightGreen;
            this.originPoint = new Point(0, 0);

            this.InitialContextMenu();
        }

        public List<LineInfo> CurrentLinePoints
        {
            get
            {
                return this.currentLinePoints;
            }
        }

        public List<Point> RecgPointBoardList
        {
            get
            {
                return this.recgPointBoardList;
            }
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
            ToolStripMenuItem horizonMenu, verticalMenu, addPointMenu, refreshMenu;
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

            addPointMenu = new ToolStripMenuItem();
            addPointMenu.Name = "addPointMenu";
            addPointMenu.Size = new System.Drawing.Size(108, 24);
            addPointMenu.Text = "标记点";
            addPointMenu.Click += new System.EventHandler(this.addPointMenuItem_Click);

            refreshMenu = new ToolStripMenuItem();
            refreshMenu.Name = "refreshMenu";
            refreshMenu.Size = new System.Drawing.Size(108, 24);
            refreshMenu.Text = "刷新";
            refreshMenu.Click += new System.EventHandler(this.refreshMenuItem_Click);

            this.cmsHorizonVertical.Name = "cmsHorizonVertical";
            this.cmsHorizonVertical.Size = new Size(109, 100);
            this.cmsHorizonVertical.Items.AddRange(new ToolStripItem[] { horizonMenu, verticalMenu, addPointMenu, refreshMenu });
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


            ToolStripMenuItem pointHide;
            this.cmsUserPointHandler = new ContextMenuStrip();
            this.cmsUserPointHandler.SuspendLayout();

            pointHide = new ToolStripMenuItem();
            pointHide.Name = "pointHide";
            pointHide.Size = new System.Drawing.Size(108, 24);
            pointHide.Text = "隐藏";
            pointHide.Click += new System.EventHandler(this.pointHide_Click);

            this.cmsUserPointHandler.Name = "cmsUserLineHanler";
            this.cmsUserPointHandler.Size = new Size(109, 100);
            this.cmsUserPointHandler.Items.Add( pointHide );
            this.cmsUserPointHandler.ResumeLayout();
        }

        public float ImageScale
        {
            get
            {
                this.imgScale = this.getImgZoomScale();
                return this.imgScale;
            }
        }

        public float CalcImageScaleBySize(int width, int height)
        {
            return this.getImgZoomScale(width, height);
        }
        #endregion

        #region 用户交互

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (this.selectedLineIdx != -1)
            {
                bool selectedSameLineFlag = this.selectedLinePoints.Contains(this.selectedLineIdx);
                if (this.selectedLinePoints.Count > 2 || this.selectedLinePoints.Count == 2 && !selectedSameLineFlag)
                    this.selectedLinePoints.Clear();
                if (selectedSameLineFlag)//双击线条，删除选中
                    this.selectedLinePoints.Remove(this.selectedLineIdx);
                else//增加选中
                    this.selectedLinePoints.Add(this.selectedLineIdx);
                
                this.refreshView();
            }
            
            if (this.selectedPoint.X < 0) return;

            if (this.onePointClickFlag && (this.OnePointForNewLine.X > 0 || this.OnePointForNewLine.Y > 0))
            {
                if (this.OnePointForNewLine.X == this.selectedPoint.X && this.OnePointForNewLine.Y == this.selectedPoint.Y) 
                    return;
                LineInfo info = new LineInfo()
                {
                    Idx = this.currentLinePoints.Count,
                    One = this.selectedPoint,
                    Other = this.OnePointForNewLine,
                    Color = this.colorUserLine,
                    Width = USER_LINK_LINE_WIDTH
                };
                this.currentLinePoints.Add(info);
                this.drawOneLine(info);
                this.OnePointForNewLine.X = 0;
                this.OnePointForNewLine.Y = 0;
            }
            else
            {
                this.OnePointForNewLine = this.selectedPoint;
            }
            this.onePointClickFlag = !this.onePointClickFlag;
        }

        private void horizonMenuItem_Click(object sender, EventArgs e)
        {
            LineInfo info = new LineInfo()
            {
                Idx = this.currentLinePoints.Count,
                One = new Point(0, this.rightClickPosition.Y),
                Other = new Point(this.imgRealWidth, this.rightClickPosition.Y),
                Color = this.colorUserLine,
                Width = USER_LINK_LINE_WIDTH
            };
            this.currentLinePoints.Add(info);

            this.drawOneLine(info);
        }

        private void verticalMenuItem_Click(object sender, EventArgs e)
        {
            LineInfo info = new LineInfo()
            {
                Idx = this.currentLinePoints.Count,
                One = new Point(this.rightClickPosition.X, 0),
                Other = new Point(this.rightClickPosition.X, this.imgRealHeight),
                Color = this.colorUserLine,
                Width = USER_LINK_LINE_WIDTH
            };
            this.currentLinePoints.Add(info);

            this.drawOneLine(info);
        }

        private void addPointMenuItem_Click(object sender, EventArgs e)
        {
            this.userAddPointsList.Add(this.rightClickPosition);
            this.recgPointBoardList.Add(this.rightClickPosition);
            this.refreshView();
        }

        private void refreshMenuItem_Click(object sender, EventArgs e)
        {
            this.refreshView();
        }

        private LineInfo getCurrentSelectedLine()
        {
            return this.currentLinePoints[this.selectedLineIdx];
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
            SetLineWidth formWidth = new SetLineWidth(USER_LINK_LINE_WIDTH, this.resetLineWidth);
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
            this.currentLinePoints.RemoveAt(this.selectedLineIdx);
            this.selectedLinePoints.Remove(this.selectedLineIdx);

            for (int i = 0; i < this.selectedLinePoints.Count;i++ )
            {
                if (this.selectedLinePoints[i] == this.selectedLineIdx)
                {
                    this.selectedLinePoints.RemoveAt(i);
                    i--;
                }
                else if (selectedLinePoints[i] > this.selectedLineIdx)
                {
                    this.selectedLinePoints[i]--;
                }
            }

            this.refreshView();
        }

        private void lineExtend_Click(object sender, EventArgs e)
        {
            LineInfo selectedLine = this.getCurrentSelectedLine();
            List<Point> linePoints = this.calcLineCrossRectangle(selectedLine.One, selectedLine.Other, new Point(0, 0), new Point(this.imgRealWidth, this.imgRealHeight));

            if (linePoints.Count < 2) return;
            selectedLine.One = linePoints[0];
            selectedLine.Other = linePoints[1];

            this.refreshView();
        }

        private void pointHide_Click(object sender, EventArgs e)
        {
            //bool foundFlag = false;
            //for (int i = 0; i < this.userAddPointsList.Count; i++)
            //{
            //    if (this.selectedPoint == this.userAddPointsList[i])
            //    {
            //        foundFlag = true;
            //        this.userAddPointsList.RemoveAt(i);
            //        break;
            //    }
            //}
            //if (!foundFlag)
            //{
            this.userHidePointsList.Add(this.selectedPoint);
            //}
            if (this.OnePointForNewLine.X == this.selectedPoint.X && this.OnePointForNewLine.Y == this.selectedPoint.Y) 
            {//删除选中、用于连线的节点
                this.OnePointForNewLine.X = 0;
                this.OnePointForNewLine.Y = 0;
                this.onePointClickFlag = false;
            }

            for (int i = 0; i < this.recgPointBoardList.Count; i++)
            {
                if (this.selectedPoint == this.recgPointBoardList[i])
                {
                    this.recgPointBoardList.RemoveAt(i);
                    break;
                }
            }
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
            this.selectedPoint = this.checkClickPoint(e.Location);

            if (e.Button == MouseButtons.Right)
            {
                this.ContextMenuStrip = this.selectedLineIdx >= 0 && this.currentLinePoints[this.selectedLineIdx].Other.X > 0 ? this.cmsUserLineHanler : (this.selectedPoint.X >= 0 ? this.cmsUserPointHandler : this.cmsHorizonVertical);
            }
            else
            {
                if (this.selectedLineIdx > -1)
                {
                    this.userMovingLineFlag = true;
                    this.lastMousePosition = e.Location;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!this.userMovingLineFlag) return;

            LineInfo selectedLine = this.getCurrentSelectedLine();
            Point one = selectedLine.One, other = selectedLine.Other;
            int diffX = e.X - this.lastMousePosition.X, diffY = e.Y - this.lastMousePosition.Y;
            this.lastMousePosition = e.Location;

            selectedLine.One = new Point(one.X + diffX, one.Y + diffY);
            selectedLine.Other = new Point(other.X + diffX, other.Y + diffY);

            this.refreshView();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.rightClickPosition = e.Location;
            this.userMovingLineFlag = false;
        }
        #endregion

        #region 绘图

        public void DrawRecogPoints(List<Point> locList)
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen penCircleInner = new Pen(Constants.RecogCircleColorInner, Constants.RecogCircleWidthInner);
                Brush brushCircleBK = new SolidBrush(Constants.RecogCircleColorBK);
                this.recgPointBoardList.Clear();

                for (int i = 0; i < this.userAddPointsList.Count; i++)
                {//更新用户增加的点
                    foreach (Point loc in locList)
                    {
                        double distanceHide = this.calcTwoPointsDistance(loc, this.userAddPointsList[i]);
                        if (distanceHide < MAX_DISTANCE_FOR_DIFF_TWO_POINTS)
                        {
                            this.userAddPointsList.RemoveAt(i);
                            this.userAddPointsList.Add(loc);                           
                            break;
                        }
                    }
                }

                foreach (Point loc in locList)
                {//绘制识别的点
                    Point locBoard = this.exchangeRecon_Board(loc);                    
                    this.DrawRecogPoints(g,locBoard,penCircleInner,brushCircleBK);
                }
                
                foreach (Point loc in this.userAddPointsList)
                {//绘制用户增加的点
                    this.DrawRecogPoints(g, loc, penCircleInner, brushCircleBK);
                }


                //for (int i = 0; i < this.userAddPointsList.Count; i++)
                //{
                //    bool foundFlag = false;
                //    foreach (Point loc in locList)
                //    {
                //        double distanceHide = this.calcTwoPointsDistance(loc, this.userAddPointsList[i]);
                //        if (distanceHide < MAX_DISTANCE_FOR_DIFF_TWO_POINTS)
                //        {
                //            this.userAddPointsList.RemoveAt(i);
                //            this.userAddPointsList.Add(loc);
                //            foundFlag = true;
                //            break;
                //        }
                //    }
                //    if (!foundFlag)
                //    {
                //        this.recgPointBoardList.Add(this.userAddPointsList[i]);
                //        this.DrawRecogPoints(penCircleInner, brushCircleBK, g, this.userAddPointsList[i]);
                //    }
                //}

                //foreach (Point loc in locList)
                //{
                //    Point locBoard = this.exchangeRecon_Board(loc);
                //    bool hideFlag = false;
                //    for (int i = 0; i < this.userHidePointsList.Count;i++ )
                //    {
                //        double distanceHide = this.calcTwoPointsDistance(locBoard, this.userHidePointsList[i]);
                //        hideFlag = distanceHide < MAX_DISTANCE_FOR_DIFF_TWO_POINTS;
                //        if (hideFlag)
                //        {
                //            this.userHidePointsList.RemoveAt(i);
                //            this.userHidePointsList.Add(locBoard);
                //            break;
                //        }
                //    }
                //    if (!hideFlag)
                //    {
                //        this.recgPointBoardList.Add(locBoard);
                //        this.DrawRecogPoints(penCircleInner, brushCircleBK, g, locBoard);
                //    }
                //}
            }
        }

        private void DrawRecogPoints(Graphics g, Point locBoard, Pen penCircleInner, Brush brushCircleBK)
        {
            bool hideFlag = false;
            for (int i = 0; i < this.userHidePointsList.Count; i++)
            {
                double distanceHide = this.calcTwoPointsDistance(locBoard, this.userHidePointsList[i]);
                hideFlag = distanceHide < MAX_DISTANCE_FOR_DIFF_TWO_POINTS;
                if (hideFlag)
                {
                    this.userHidePointsList.RemoveAt(i);
                    this.userHidePointsList.Add(locBoard);
                    break;
                }
            }
            if (!hideFlag)
            {
                this.recgPointBoardList.Add(locBoard);
                this.DrawRecogPoints(penCircleInner, brushCircleBK, g, locBoard);
            }
        }

        public void DrawRecogPoints()
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen penCircleInner = new Pen(Constants.RecogCircleColorInner, Constants.RecogCircleWidthInner);
                Brush brushCircleBK = new SolidBrush(Constants.RecogCircleColorBK);
                foreach (Point locBoard in this.recgPointBoardList)
                {
                    this.DrawRecogPoints(penCircleInner, brushCircleBK, g, locBoard);
                }
            }
        }

        private void DrawRecogPoints(Pen penCircleInner,Brush brushCircleBK,Graphics g, Point loc)
        {
            g.FillEllipse(brushCircleBK, loc.X - Constants.RecogCircleRadiusBK, loc.Y - Constants.RecogCircleRadiusBK, Constants.RecogCircleRadiusBK * 2, Constants.RecogCircleRadiusBK * 2);
            g.DrawEllipse(penCircleInner, loc.X - Constants.RecogCircleRadiusInner, loc.Y - Constants.RecogCircleRadiusInner, Constants.RecogCircleRadiusInner * 2, Constants.RecogCircleRadiusInner * 2);
        }

        public void DrawNetLine()
        {
            using (Graphics g = this.CreateGraphics())
            {
                int lineOneHeight = Constants.NetLineWidth + Constants.LineSeperationHeight;
                int lineOneWidth = Constants.NetLineWidth + Constants.LineSeperationWidth;
                Pen pen = new Pen(Constants.ColorNetLine, Constants.NetLineWidth);
                for (int i = 0; i < this.imgRealHeight; i += lineOneHeight)
                {
                    Point start = new Point(0, i);
                    Point end = new Point(this.imgRealWidth, i);
                    g.DrawLine(pen, start, end);
                }

                for (int i = 0; i < this.imgRealWidth; i += lineOneWidth)
                {
                    Point start = new Point(i, 0);
                    Point end = new Point(i, this.imgRealHeight);
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

        private void DrawLinesDefault(Graphics g)
        {
            this.drawLines(g, this.currentLinePoints);

            this.drawSelectedLine(g);
        }

        private void drawLines(Graphics g,List<LineInfo> lineList)
        {
            foreach (LineInfo info in lineList)
            {
                Pen pen = new Pen(info.Color, info.Width);
                if (info.One == this.originPoint || info.Other == this.originPoint) continue;

                g.DrawLine(pen, info.One, info.Other);
            }
        }

        private void drawSelectedLine(Graphics g)
        {
            Pen pen = new Pen(Constants.LineColorSelected, Constants.LineWidthSelected);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            foreach (int lineIdx in this.selectedLinePoints)
            {
                if (lineIdx >= this.currentLinePoints.Count || lineIdx < 0) continue;

                LineInfo info = this.currentLinePoints[lineIdx];

                if (info.Other == Point.Empty) continue;

                g.DrawLine(pen, info.One, info.Other);
            }
            double angle = 0.0d;
            if (this.selectedLinePoints.Count >= 2)
            {
                angle = this.CalcLineAngle(this.selectedLinePoints[0], this.selectedLinePoints[1]);
            }
            this.showAngleForTwoLine(angle);
        }

        #endregion

        #region 坐标变换/计算

        private double CalcLineAngle(int lineOne, int lineTwo)
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
            int lineIdx = -1, lineCount = this.currentLinePoints.Count;
            for (int i = 0; i < lineCount; i++)
            {
                if (this.checkPointOnLine(clickPoint, this.currentLinePoints[i]))
                {
                    lineIdx = i;
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
            if (minDistance >= MAX_DISTANCE_FOR_DIFF_TWO_POINTS)
                minPoint = oldPoint;// new Point(-1, -1);

            return minPoint;
        }

        private Point checkClickPoint(Point loc)
        {
            Point clickPoint = new Point(-1, -1);
            foreach (Point item in this.recgPointBoardList)
            {
                double circle = Math.Pow(loc.X - item.X, 2) + Math.Pow(loc.Y - item.Y, 2);

                if (circle <= Constants.RecogCircleRadiusBK * Constants.RecogCircleRadiusBK)
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
            return this.getImgZoomScale(Constants.IMAGE_WIDTH, Constants.IMAGE_HEIGHT);
        }

        private float getImgZoomScale(int width,int height)
        {
            double scaleWidth = (double)this.Width / width;
            double scaleHeight = (double)this.Height / height;

            double minSacle = Math.Min(scaleWidth, scaleHeight);

            if (Math.Abs(scaleWidth - minSacle) < 1e-6)
            {
                this.imgRealWidth = this.Width;
                this.imgRealHeight = (int)(1.0 * this.Width * height / width);
            }
            else
            {
                this.imgRealWidth = (int)(1.0 * this.Height * width / height);
                this.imgRealHeight = this.Height;
            }

            return (float)minSacle;
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