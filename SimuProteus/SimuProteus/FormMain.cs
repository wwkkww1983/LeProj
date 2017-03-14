using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace SimuProteus
{
    public partial class FormMain : Form
    {
        #region 初始化
        private bool serialReadFlag = false;
        private const char COORDINATE_SEPERATOR = '#';
        private const int ORIGIN_ARROW_LEN = 10;
        private Color ORIGIN_ARROW_COLOR = Color.Black;
        private int elementIdx = 1, pointRadius=0;
        private int boardMargin = int.Parse(Ini.GetItemValue("sizeInfo", "pixelBoardMargin"));
        private int DragDistance = int.Parse(Ini.GetItemValue("sizeInfo", "pixelDragDistance"));
        private int netPointGap = int.Parse(Ini.GetItemValue("sizeInfo", "pixelPointGap"));
        private int netPointSize = int.Parse(Ini.GetItemValue("sizeInfo", "pixelNetPoint"));
        private int boardWidth = int.Parse(Ini.GetItemValue("sizeInfo", "pixelBoardWidth"));
        private int boardHeight = int.Parse(Ini.GetItemValue("sizeInfo", "pixelBoardHeight"));
        private int defaultWidth = int.Parse(Ini.GetItemValue("sizeInfo", "pixelInitialWidth"));
        private int defaultHeight = int.Parse(Ini.GetItemValue("sizeInfo", "pixelInitialHeight"));
        private Color defaultLineColor = Color.FromArgb(int.Parse(Ini.GetItemValue("colorInfo", "colorLine")));
        private int defaultLeftMenuHeight = -1, defaultProjNameCoordX = -1,oneNetPointLength = -1;
        private enumComponent currentSelectedComponent = enumComponent.NONE;
        private Point clickPositionForLine;
        private SerialCom serial = new SerialCom();
        DBUtility dbHandler = new DBUtility(true);
        List<Point> newLinePoints = new List<Point>();
        List<ElementInfo> elementList = null;
        List<ElementLine> createLinePoint = new List<ElementLine>(2);
        ProjectDetails currentBoardInfo = new ProjectDetails()
        {
            Project = new ProjectInfo() { 
             OriginX = 1, OriginY=1//默认原点为起点
            },
            elementList = new List<ElementInfo>(),
            linesList = new List<ElementLine>(),
            footsList = new List<LineFootView>(),
            pointsList = new List<NetPoint>()
        };

        public FormMain()
        {
            InitializeComponent();

            this.InitialControl();
            //dbHandler.InitialTable();
            this.elementList = dbHandler.GetBaseComponents();
            int idx = 0;
            foreach (ElementInfo item in elementList)
            {
                UcComponent compo = new UcComponent(++idx, item, ChangeCursor);
                this.gbComponent.Controls.Add(compo);  
            }
            List<ProjectInfo> projectList = dbHandler.GetAllProjects();
            foreach (ProjectInfo item in projectList)
            {
                this.AddProjectHistory(item.Name, item.Idx);
            }
        }

        private void InitialControl()
        {
            this.Size = new Size(defaultWidth, defaultHeight);
            this.defaultLeftMenuHeight = this.gbComponent.Height;
            this.defaultProjNameCoordX = this.lbProjName.Location.X;
            this.oneNetPointLength = this.netPointGap + this.netPointSize;
            this.pointRadius = this.netPointSize / 2;

            this.pnWorkPlace.Size = this.pnBoard.Size;
            this.pnWorkPlace.Location = this.pnBoard.Location;
            this.pnBoard.Size = new Size(boardWidth, boardHeight);
            this.pnBoard.Location = new Point(0, 0);
            this.pnBoard.Parent = this.pnWorkPlace;
            //this.pnLineDemo.Size = this.pnBoard.Size;
            //this.pnLineDemo.BackColor = this.pnBoard.BackColor;
            //this.pnLineDemo.Location = this.pnBoard.Location;
            //this.pnLineDemo.SendToBack();

            
            //this.pnLineTmp.Size = new Size(190,1200) ;
            ////this.pnLineTmp.BackColor = Color.Transparent;
            //this.pnLineTmp.Location = this.pnBoard.Location;
            //this.pnLineTmp.Parent = this.pnBoard;
            //this.pnLineTmp.SendToBack();

            //this.pnBoard.BringToFront();
        }

        private void AddProjectHistory(string name, int idx)
        {
            ToolStripItem projItem = new ToolStripMenuItem(name);
            projItem.Tag = idx;
            projItem.Click += projectNameToolStripMenuItem_Click;
            this.ProjToolStripMenuItem.DropDownItems.Insert(0,projItem);
        }

        private void DrawOrigin(int x, int y)
        {
            Point coor = this.CalcCoordinateByLocIdx(x, y);
            Point origin = new Point(coor.X + this.pointRadius, coor.Y + this.pointRadius);
            Point coorX = new Point(origin.X + ORIGIN_ARROW_LEN, origin.Y);
            Point coorY = new Point(origin.X, origin.Y + ORIGIN_ARROW_LEN);

            Draw.DrawArrawLine(this.pnBoard, origin, coorX, ORIGIN_ARROW_COLOR);
            Draw.DrawArrawLine(this.pnBoard, origin, coorY, ORIGIN_ARROW_COLOR);
        }
        #endregion

        #region 回调
        /// <summary>
        /// 改变光标
        /// </summary>
        /// <param name="newCursor"></param>
        private void ChangeCursor(Cursor newCursor, enumComponent clickedCompo)
        {
            if (this.InvokeRequired)
            {
                Action<Cursor, enumComponent> delegateChangeCursor = new Action<Cursor, enumComponent>(ChangeCursor);
                this.Invoke(delegateChangeCursor, new object[] { newCursor });
                return;
            } 
            
            this.Cursor = newCursor;
            this.currentSelectedComponent = clickedCompo;
        }

        private void SaveProjectName(bool newFlag,int projIdx, string projName)
        {
            if (this.InvokeRequired)
            {
                Action<bool, int, string> delegateSaveProjectName = new Action<bool, int, string>(SaveProjectName);
                this.Invoke(delegateSaveProjectName, new object[] { projIdx });
                return;
            }

            if (!newFlag)
            {
                this.AddProjectHistory(projName, projIdx);
            }
            else
            {
                foreach (ToolStripItem item in this.ProjToolStripMenuItem.DropDownItems)
                {
                    if (Convert.ToInt32(item.Tag) == Convert.ToInt32(this.lbProjName.Tag))
                    {
                        item.Tag = projIdx;
                        item.Text = projName;
                        break;
                    }
                }
            }
            this.lbProjName.Tag = projIdx;
            this.lbProjName.Text = projName;
        }

        private void CreateLineForElement(int idx,int footIdx, int locX, int locY)
        {
            if (createLinePoint.Count == 0)
            {
                createLinePoint.Add(new ElementLine()
                {
                    Idx = elementIdx++,
                    oneFoot = footIdx,
                    oneElement = idx,
                    LocX = locX,
                    LocY = locY,
                    Color = this.defaultLineColor
                });
                clickPositionForLine.X = 0;
                clickPositionForLine.Y = 0;
                this.Cursor = Cursors.Cross;
                return;
            }
            if (locX == createLinePoint[0].LocX && locY == createLinePoint[0].LocY) return;
            ElementLine eleOne = createLinePoint[0];
            eleOne.otherElement = idx;
            ElementLine eleOther = new ElementLine()
            {
                Idx = elementIdx++,
                oneFoot = footIdx,
                oneElement = idx,
                otherElement = eleOne.oneElement,
                LocX = locX,
                LocY = locY,
                Color = this.defaultLineColor
            };
            eleOne.otherFoot = footIdx;
            eleOther.otherFoot = eleOne.oneFoot;
            int midX,midY;
            this.CalcTurnLocation(eleOne.LocX, eleOne.LocY, locX, locY, out midX, out midY);
            eleOne.LocOtherX = midX;
            eleOne.LocOtherY = midY;
            eleOther.LocOtherX = midX;
            eleOther.LocOtherY = midY;
            createLinePoint.Clear();
            this.currentBoardInfo.linesList.Add(eleOne);
            this.currentBoardInfo.linesList.Add(eleOther);
            UcLine lineOne = new UcLine(eleOne, DeleteLineLink, ChangeLineColor);
            UcLine lineOther = new UcLine(eleOther, DeleteLineLink, ChangeLineColor);
            lineOne.OtherLine = lineOther;
            lineOther.OtherLine = lineOne;
            this.pnBoard.Controls.Add(lineOne);
            lineOne.BringToFront();
            this.pnBoard.Controls.Add(lineOther);
            lineOther.BringToFront();
            this.Cursor = Cursors.Arrow;
        }

        private void CalcTurnLocation(int locX, int locY, int locOtherX, int locOtherY, out int midX, out int midY)
        {
            if (locX == locOtherX || locY == locOtherY)
            {//在同一个水平/竖直方向上
                midX = locOtherX;
                midY = locOtherY;
            }
            else
            {//统一参考坐标系
                int xLeft, yLeft, xRight, yRihgt;
                if (locY < locOtherY)
                {//下面点为参考点
                    xLeft = locX; yLeft = locY;
                    xRight = locOtherX; yRihgt = locOtherY;
                }
                else
                {
                    xLeft = locOtherX; yLeft = locOtherY;
                    xRight = locX; yRihgt = locY;
                }
                if (((xRight - xLeft) * (clickPositionForLine.Y - yLeft) - (yRihgt - yLeft) * (clickPositionForLine.X - xLeft)) > 0)
                {//左边
                    midX = Math.Min(locX, locOtherX);
                    //绘图坐标系跟直角坐标是沿X轴对称的，所以斜率是反的但左右不变
                    midY = (locY * 0.1 - locOtherY * 0.1) / (locX * 0.1 - locOtherX * 0.1) > 0 ? Math.Max(locY, locOtherY) : Math.Min(locY, locOtherY);
                }
                else
                {
                    midX = Math.Max(locX, locOtherX);
                    midY = (locY * 0.1 - locOtherY * 0.1) / (locX * 0.1 - locOtherX * 0.1) < 0 ? Math.Max(locY, locOtherY) : Math.Min(locY, locOtherY);
                }
            }
        }

        private void DeleteLineLink(int idx, int otherIdx)
        {
            int delCount = 0;
            for (int i = 0; i < this.currentBoardInfo.linesList.Count; i++)
            {
                ElementLine tmpInfo = this.currentBoardInfo.linesList[i];
                if (tmpInfo.Idx == idx || tmpInfo.Idx == otherIdx)
                {
                    this.currentBoardInfo.linesList.RemoveAt(i);
                    i--;
                    delCount++;
                    if (delCount % 2 == 0) break;
                }
            }
            delCount = 0;
            for (int i = 0; i < this.pnBoard.Controls.Count; i++)
            {
                Control item = this.pnBoard.Controls[i];
                if (item.GetType() == typeof(UcLine) && ((item as UcLine).LineInfo.Idx == idx || (item as UcLine).LineInfo.Idx == otherIdx))
                {
                    this.pnBoard.Controls.RemoveAt(i);
                    i--; delCount++;
                    if (delCount % 2 == 0) break;
                }
            }
        }

        private void DeleteElement(int idx)
        {
            for (int i = 0; i < this.currentBoardInfo.elementList.Count;i++ )
            {
                ElementInfo tmpInfo = this.currentBoardInfo.elementList[i];
                if (tmpInfo.InnerIdx == idx)
                {
                    this.currentBoardInfo.elementList.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < this.pnBoard.Controls.Count; i++)
            {
                Control item = this.pnBoard.Controls[i];
                if (item.GetType() == typeof(UcElement) && (item as UcElement).ViewInfo.InnerIdx == idx)
                {
                    this.pnBoard.Controls.RemoveAt(i);
                    break;
                }
            }
        }

        private void MoveElement(int idx, int locX,int locY)
        {
            bool moveFlag = false;
            for (int i = 0; i < this.currentBoardInfo.elementList.Count; i++)
            {
                ElementInfo tmpInfo = this.currentBoardInfo.elementList[i];
                if (tmpInfo.InnerIdx == idx)
                {
                    tmpInfo.Location = new Point(locX, locY);
                    if (Math.Sqrt((tmpInfo.Location.X - locX) * (tmpInfo.Location.X - locX) + (tmpInfo.Location.Y - locY) * (tmpInfo.Location.Y - locY)) > DragDistance)
                    {
                        moveFlag = true;
                    }
                    break;
                }
            }
            if (!moveFlag) return;
            for (int i = 0; i < this.currentBoardInfo.linesList.Count; i++)
            {
                ElementLine tmpInfo = this.currentBoardInfo.linesList[i];
                if (tmpInfo.oneElement == idx || tmpInfo.otherElement == idx)
                {
                    this.currentBoardInfo.linesList.Remove(tmpInfo);
                    i--;
                }
            }
            for (int i = 0; i < this.pnBoard.Controls.Count; i++)
            {
                Control item = this.pnBoard.Controls[i];
                if (item.GetType() == typeof(UcLine) && ((item as UcLine).LineInfo.oneElement == idx || (item as UcLine).LineInfo.otherElement == idx))
                {
                    this.pnBoard.Controls.Remove(item);
                    i--; 
                }
            }
        }

        private void ChangeLineColor(int idx, int color)
        {
            for (int i = 0; i < this.currentBoardInfo.linesList.Count; i++)
            {
                ElementLine tmpInfo = this.currentBoardInfo.linesList[i];
                if (tmpInfo.Idx == idx)
                {
                    this.currentBoardInfo.linesList.RemoveAt(i);
                    tmpInfo.Color = Color.FromArgb(color);
                    this.currentBoardInfo.linesList.Add(tmpInfo);
                    break;
                }
            }
        }

        private void UpdateElementName(int idx, string name)
        {
            for (int i = 0; i < this.currentBoardInfo.elementList.Count; i++)
            {
                ElementInfo tmpInfo = this.currentBoardInfo.elementList[i];
                if (tmpInfo.InnerIdx == idx)
                {
                    tmpInfo.Name = name;
                    break;
                }
            }
        }

        private void updateElementFoots(LineFootView foot)
        {
            bool foundFlag = false;
            for (int i = 0; i < this.currentBoardInfo.footsList.Count; i++)
            {
                LineFootView item = this.currentBoardInfo.footsList[i];
                if (item.Element == foot.Element && item.Foot == foot.Foot)
                {
                    this.currentBoardInfo.footsList.RemoveAt(i);
                    this.currentBoardInfo.footsList.Add(foot);
                    foundFlag = true;
                    break;
                }
            }

            if (!foundFlag)
            {
                for (int i = 0; i < this.currentBoardInfo.elementList.Count; i++)
                {
                    ElementInfo elementTmp = this.currentBoardInfo.elementList[i];
                    if (elementTmp.InnerIdx != foot.Element)
                        continue;

                    foreach (LineFoot footItem in elementTmp.LineFoots)
                    {
                        this.currentBoardInfo.footsList.Add(new LineFootView()
                        {
                            Component = elementTmp.ID,
                            Element = elementTmp.InnerIdx,
                            Foot = footItem.Idx,
                            PinsName = footItem.Idx == foot.Foot ? foot.PinsName : footItem.Name,
                            PinsType = footItem.Idx == foot.Foot ? foot.PinsType : footItem.PinsType
                        });
                    }
                    break;
                }
            }
        }

        private void UpdateShowSize()
        {
           this.boardWidth = int.Parse(Ini.GetItemValue("sizeInfo", "pixelBoardWidth"));
           this.boardHeight = int.Parse(Ini.GetItemValue("sizeInfo", "pixelBoardHeight"));
           this.defaultWidth = int.Parse(Ini.GetItemValue("sizeInfo", "pixelInitialWidth"));
           this.defaultHeight = int.Parse(Ini.GetItemValue("sizeInfo", "pixelInitialHeight"));

           this.pnBoard.Size = new Size(boardWidth, boardHeight);
           this.Size = new Size(defaultWidth, defaultHeight);
        }
        #endregion

        #region 面板事件

        private ElementInfo GetElementInfoOnBoardByName(string component, Point location)
        {
            ElementInfo infoTmp = GetElementByName(component);
            ElementInfo info = (ElementInfo)infoTmp.Clone();
            info.Location = location;

            return info;
        }

        private void pnBoard_Click(object sender, EventArgs e)
        {
            MouseEventArgs clickArgs = (MouseEventArgs)e;
            if (this.newLinePoints.Count > 0)
            {
                if (clickArgs.Button == MouseButtons.Right)
                {
                    this.newLinePoints.Clear();
                    //删除已绘线条
                    //pnLineTmp.Invalidate();
                    //pnLineTmp.Update();
                    //pnLineTmp.Refresh();                    
                }
                else
                {
                    Point clickLocIdx = this.CalcNearestLocIdxByCoordinate(clickArgs.X, clickArgs.Y);
                    Point locOther = this.newLinePoints[this.newLinePoints.Count - 1];
                    Point locOne = new Point ();
                    if (Math.Abs(clickLocIdx.X - locOther.X) > Math.Abs(clickLocIdx.Y - locOther.Y))
                    {
                        locOne.Y = locOther.Y;
                        locOne.X = clickLocIdx.X;
                    }
                    else
                    {
                        locOne.X = locOther.X;
                        locOne.Y = clickLocIdx.Y;
                    }
                    Point coordiOne = this.CalcCoordinateByLocIdx(locOne.X, locOne.Y);
                    Point coordiOther = this.CalcCoordinateByLocIdx(locOther.X,locOther.Y);
                    this.newLinePoints.Add(locOne);
                    Draw.DrawSolidLine(this.pnBoard, coordiOne, coordiOther, true);
                }
                return;
            }
            if (clickArgs.Button == MouseButtons.Right)
            {
                this.Cursor = Cursors.Arrow;
                this.currentSelectedComponent = enumComponent.NONE;
            }
            else
            {
                if (this.currentSelectedComponent == enumComponent.NONE)
                    return;
                //添加元器件
                ElementInfo info = GetElementInfoOnBoardByName(this.currentSelectedComponent.ToString(), new Point(clickArgs.X, clickArgs.Y));
                info.InnerIdx = elementIdx++;
                UcElement elementItem = this.getElementView(info);
                this.pnBoard.Controls.Add(elementItem);
                elementItem.BringToFront();
                this.currentBoardInfo.elementList.Add(info);
            }
        }

        private ElementInfo GetElementByName(string name)
        {
            foreach (ElementInfo item in elementList)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }

        private void pnBoard_Paint(object sender, PaintEventArgs e)
        {
            for (int x = boardMargin; x < boardWidth; x += oneNetPointLength)
            {
                for (int y = boardMargin; y < boardHeight; y += oneNetPointLength)
                {
                    Draw.DrawSolidCircle(this.pnBoard, enumNetPointType.NONE, x, y);
                }
            }
            foreach (NetPoint point in this.currentBoardInfo.pointsList)
            {
                Point coord = this.CalcCoordinateByLocIdx(point.X, point.Y);
                Draw.DrawSolidCircle(this.pnBoard, point.Type, coord.X, coord.Y);
            }
            this.DrawOrigin(this.currentBoardInfo.Project.OriginX, this.currentBoardInfo.Project.OriginY);

            foreach (ElementLine line in this.currentBoardInfo.linesList)
            {
                Point coordiOne = this.CalcCoordinateByLocIdx(line.LocX, line.LocY);
                Point coordiOther = this.CalcCoordinateByLocIdx(line.LocOtherX, line.LocOtherY);
                Draw.DrawSolidLine(this.pnBoard, coordiOne, coordiOther,line.Color);
            }
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            int diffWidth = this.Size.Width - defaultWidth, diffHeight = this.Size.Height - defaultHeight;

            this.gbComponent.Height = defaultLeftMenuHeight + diffHeight;
            this.pnBoard.Width = defaultWidth + diffWidth;
            this.pnBoard.Height = defaultHeight + diffHeight;
            this.lbProjName.Location = new Point(defaultProjNameCoordX + diffWidth, this.lbProjName.Location.Y);
        }

        private void pnBoard_MouseMove(object sender, MouseEventArgs e)
        {
            int lineIdx = this.CalcLineIdxByCoordinate(e.X, e.Y);
            Point pointIdx = this.CalcLocIdxByCoordinate(e.X, e.Y);
            if (lineIdx >= 0)
            {
                this.contextMsLine.Text = lineIdx.ToString();
                this.ContextMenuStrip = this.contextMsLine;
            }
            else if (pointIdx.X > 0 && pointIdx.Y > 0)
            {
                this.contextMsPoint.Text = string.Format("{0}{1}{2}", pointIdx.X, COORDINATE_SEPERATOR, pointIdx.Y);
                this.ContextMenuStrip = this.contextMsPoint;
            }
            else
            {
                this.ContextMenuStrip = null;
            }

            //实时展示预览图
            //由于在绘制新线时，无法删除之前的线，所以这个功能没能实现
            //if (this.newLinePoints.Count > 0)
            //{
            //    Point clickCoordi = this.CalcNearestLocIdxByCoordinate(e.X, e.Y);
            //    Point coordiOne = this.CalcCoordinateByLocIdx(clickCoordi.X, clickCoordi.Y);
            //    Point locOther = this.newLinePoints[this.newLinePoints.Count - 1];
            //    Point coordiOther = this.CalcCoordinateByLocIdx(locOther.X, locOther.Y);

            //    int midX, midY;
            //    this.CalcTurnLocation(coordiOne.X, coordiOne.Y, coordiOther.X, coordiOther.Y, out midX, out midY);
            //    Point coordiMid = new Point(midX, midY);
            //}
        }

        private Point CalcLocIdxByCoordinate(int x, int y)
        {
            Point pointIdx = new Point();

            int xReal = x - boardMargin, yReal = y - boardMargin;
            if (xReal % oneNetPointLength < netPointSize && yReal % oneNetPointLength < netPointSize)
            {
                pointIdx.X = xReal / oneNetPointLength + 1;
                pointIdx.Y = yReal / oneNetPointLength + 1;
            }

            return pointIdx;
        }

        private Point CalcCoordinateByLocIdx(int x, int y)
        {
            return new Point()
            {
                X = boardMargin + (x - 1) * oneNetPointLength,
                Y = boardMargin + (y - 1) * oneNetPointLength
            };
        }

        private int CalcLineIdxByCoordinate(int x, int y)
        {
            int lineIdx = -1;

            foreach (ElementLine line in this.currentBoardInfo.linesList)
            {
                Point coorOne = this.CalcCoordinateByLocIdx(line.LocX, line.LocY),
                    coorOther = this.CalcCoordinateByLocIdx(line.LocOtherX, line.LocOtherY);
                int x1, y1;
                if (line.LocX == line.LocOtherX)
                {//竖线
                    x1 = coorOther.X + Constants.LINE_LINK_WIDTH;
                    y1 = coorOther.Y;
                }
                else
                {
                    x1 = coorOther.X;
                    y1 = coorOther.Y + Constants.LINE_LINK_WIDTH;
                }
                if ((coorOne.X - x) * (x1 - x) <= 0 && (coorOne.Y - y) * (y1 - y) <= 0)
                {
                    lineIdx = line.InnerIdx;
                    break;
                }
            }

            return lineIdx;
        }

        private Point CalcNearestLocIdxByCoordinate(int x, int y)
        {
            Point pointIdx = new Point();

            int xReal = x - boardMargin, yReal = y - boardMargin;

            pointIdx.X = xReal / oneNetPointLength + 1;
            pointIdx.Y = yReal / oneNetPointLength + 1;

            if(xReal % oneNetPointLength > netPointSize + netPointGap / 2){
                pointIdx.X += 1;
            }
            if (yReal % oneNetPointLength > netPointSize + netPointGap / 2)
            {
                pointIdx.Y += 1;
            }
            return pointIdx;

        }

        private void ChangeNetPoint(Point locIdx, enumNetPointType pointType)
        {
            bool foundFlag = false;
            for (int i = 0; i < this.currentBoardInfo.pointsList.Count; i++)
            {
                NetPoint point = this.currentBoardInfo.pointsList[i];
                if (point.X == locIdx.X && point.Y == locIdx.Y)
                {
                    point.Type = pointType;
                    this.currentBoardInfo.pointsList.RemoveAt(i);
                    if (pointType != enumNetPointType.NONE)
                    {
                        this.currentBoardInfo.pointsList.Add(point);
                    }
                    foundFlag = true;
                    break;
                }
            }
            if (!foundFlag)
            {
                this.currentBoardInfo.pointsList.Add(new NetPoint()
                {
                    X = locIdx.X,
                    Y = locIdx.Y,
                    Type = pointType
                });
            }

            this.UpdatePointName(locIdx, pointType);
        }

        private void UpdatePointName(Point locIdx, enumNetPointType status)
        {
            bool foundFlag = false;
            string labelTag = string.Format("{0}{2}{1}", locIdx.X, locIdx.Y, COORDINATE_SEPERATOR);
            for (int i = 0; i < this.pnBoard.Controls.Count; i++)
            {
                Control item = this.pnBoard.Controls[i];
                if (item.GetType() == typeof(Label) && (Convert.ToString((item as Label).Tag) == labelTag))
                {
                    if (status == enumNetPointType.NONE)
                    {
                        this.pnBoard.Controls.RemoveAt(i);
                    }
                    else
                    {
                        (item as Label).Text = status.ToString();
                    }
                    foundFlag = true;
                    break;
                }
            }
            if (!foundFlag && status != enumNetPointType.NONE)
            {
                Label lbTmp = new Label();
                lbTmp.Tag = labelTag;
                lbTmp.Text = status.ToString();
                lbTmp.Size = new Size(31,13);
                Point coordi = this.CalcCoordinateByLocIdx(locIdx.X, locIdx.Y);
                lbTmp.Location = new Point(coordi.X - 15, coordi.Y + 5);
                this.pnBoard.Controls.Add(lbTmp);
            }
        }

        private void pnBoard_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point pointIdx = CalcLocIdxByCoordinate(e.X, e.Y);
            if (pointIdx.X > 0 && pointIdx.Y > 0)
            {
                if (this.newLinePoints.Count == 0)
                {
                    this.newLinePoints.Add(pointIdx);
                    this.Cursor = Cursors.Cross;
                }
                else
                {//结束绘线
                    this.Cursor = Cursors.Arrow;
                    if(this.newLinePoints.Count <= 1)
                    {
                        this.newLinePoints.Clear();
                        return;
                    }
                    this.SaveDemoLine();
                    this.DrawLinesByLastPoints();
                    this.newLinePoints.Clear();
                }
            }
        }

        private void SaveDemoLine()
        {
            int lineIdx = 1;
            Point onePoint = this.newLinePoints[0];
            for (int i = 1; i < this.newLinePoints.Count; i++)
            {
                Point otherPoint = this.newLinePoints[i];
                this.currentBoardInfo.linesList.Add(new ElementLine()
                {
                    InnerIdx = elementIdx,
                    LocX = onePoint.X,
                    LocY = onePoint.Y,
                    LocOtherX = otherPoint.X,
                    LocOtherY = otherPoint.Y,
                    LineIdx = lineIdx++,
                    Color = this.defaultLineColor
                });
                onePoint = otherPoint;
            }
            elementIdx++;
        }

        private void DrawLinesByLastPoints()
        {
            Point onePoint = this.newLinePoints[0];
            Point coordiOne = this.CalcCoordinateByLocIdx(onePoint.X, onePoint.Y);

            for (int i = 1; i < this.newLinePoints.Count; i++)
            {
                Point otherPoint = this.newLinePoints[i];
                Point coordiOther = this.CalcCoordinateByLocIdx(otherPoint.X, otherPoint.Y);
                Draw.DrawSolidLine(this.pnBoard, coordiOne, coordiOther, false);
                coordiOne = coordiOther;
            }
        }
        #endregion

        #region 窗口菜单
        private void hC244ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckLoadChip())
            {
                //this.InitialNetPoint(countWidth, countHeight);
                int currentChips = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
                UcElement chipElement = this.InitialChipOnBoard(currentChips);
                this.pnBoard.Controls.Add(chipElement);
                chipElement.BringToFront();
                currentBoardInfo.Project.Chips = currentChips;
            }
        }

        private void lS221ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckLoadChip())
            {
                //this.InitialNetPoint(countWidth, countHeight);
                int currentChips = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
                UcElement chipElement = this.InitialChipOnBoard(currentChips);
                this.pnBoard.Controls.Add(chipElement);
                chipElement.BringToFront();
                currentBoardInfo.Project.Chips = currentChips;
            }
        }

        private UcElement InitialChipOnBoard(int chipIdx)
        {
            string chipName = string.Empty;
            switch (chipIdx)
            {
                case 1: chipName = "\\img\\74HC244.jpg"; break;
                case 2: chipName = "\\img\\74Serial.png"; break;
                default: break;
            }
            Image imgChip = Image.FromFile(Constants.CurrentDirectory + chipName);

            int locX, locY;
            locX = this.pnBoard.Width - imgChip.Width;
            locY = this.pnBoard.Height - imgChip.Height;
            ElementInfo info = new ElementInfo()
            {
                Location = new Point(locX / 2, locY / 2),
                Size = imgChip.Size,
                BackColor = Color.Gray,
                BackImage = chipName,
                FootType = enumComponentType.Chips,
                LineFoots = dbHandler.GetChipFoots(chipIdx)
            };

            return this.getElementView(info);
        }


        private UcElement getElementView(ElementInfo info)
        {
            return new UcElement(info, CreateLineForElement, DeleteElement, MoveElement, UpdateElementName, updateElementFoots,this.currentBoardInfo.footsList);
        }

        private bool CheckLoadChip()
        {
            bool result = true;
            if (this.pnBoard.Controls.Count > 1 )
            {
                DialogResult dResult = MessageBox.Show("画板上有内容，确定清空？", "清空当前画布", MessageBoxButtons.OKCancel);
                if (dResult == DialogResult.OK)
                {
                    this.pnBoard.Controls.Clear();
                    this.lbProjName.Tag = null;
                    this.lbProjName.Text = "未命名";
                    this.pnBoard.Controls.Add(this.lbProjName);
                    currentBoardInfo.linesList.Clear();
                    currentBoardInfo.elementList.Clear();
                    currentBoardInfo.footsList.Clear();
                    currentBoardInfo.pointsList.Clear();
                }
                else
                {//不清空
                    result = false;
                }
            }
            return result;
        }

        private void saveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSave saveWindow = new FormSave();
            saveWindow.Project = this.currentBoardInfo;
            saveWindow.HandlerAfterSave = this.SaveProjectName;
            saveWindow.NewFlag = Convert.ToInt32(this.lbProjName.Tag) > 0;
            saveWindow.ShowDialog();
        }

        private bool CheckSerialStatus()
        {
            if (!serial.IsOpen)
            {
                MessageBox.Show("串口未打开");
                return false;
            }
            return true;
        }


        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.CheckSerialStatus()) return;

            if (serialReadFlag)
            {
                MessageBox.Show("已经在监听串口数据");
                return;
            }

            Thread threadSerial = new Thread(ListenSerialPort);
            threadSerial.Start();
            
            serialReadFlag = true;
        }

        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.CheckSerialStatus()) return;

            MessageBox.Show("接口完成，无数据");
        }

        private void serialStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetSerial formSerial = new FormSetSerial(serial);
            formSerial.ShowDialog();
        }


        private void freeSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.CheckSerialStatus()) return;

            this.serial.Close();
            if (serial.IsOpen)
            {
                MessageBox.Show("串口操作失败");
            }
            else
            {
                MessageBox.Show("串口释放成功");
            }
        }

        private void projectNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckLoadChip()) return;
            ToolStripItem projItem = sender as ToolStripItem;

            ProjectDetails projInfo = dbHandler.getProjectDetail(Convert.ToInt32(projItem.Tag));
            this.currentBoardInfo = projInfo;
            //this.InitialNetPoint(projInfo.Project.Width, projInfo.Project.Length);
            this.LoadElementByHistoryItem(projInfo);
        }

        private void LoadElementByHistoryItem(ProjectDetails projInfo)
        {
            this.lbProjName.Text = projInfo.Project.Name;
            this.lbProjName.Tag = projInfo.Project.Idx;
            //this.InitialNetPoint(projInfo.Project.Width,projInfo.Project.Length);

            if (projInfo.Project.Chips > 0)
            {
                UcElement chipElement = this.InitialChipOnBoard(projInfo.Project.Chips);
                this.pnBoard.Controls.Add(chipElement);
                chipElement.BringToFront();
            }
            foreach (ElementInfo info in projInfo.elementList)
            {
                UcElement ucTmp =this.getElementView(info);
                this.pnBoard.Controls.Add(ucTmp);
                ucTmp.BringToFront();
            }
            List<int> addedLine = new List<int>();
            foreach (ElementLine line in projInfo.linesList)
            {
                if (addedLine.Contains(line.Idx)) continue;
                ElementLine eleOne = new ElementLine()
                {   
                    Color = line.Color,
                    LocX = line.LocX,
                    LocY = line.LocY,
                    LocOtherX = line.LocOtherX,
                    LocOtherY = line.LocOtherY,
                    Idx = line.Idx,
                    oneFoot = line.oneFoot,
                    otherFoot = line.otherFoot,
                    Name = line.Name
                };
                addedLine.Add(line.Idx);
                ElementLine eleOther = new ElementLine();
                foreach (ElementLine lineChild in projInfo.linesList)
                {
                    if (addedLine.Contains(lineChild.Idx)) continue;
                    if (lineChild.LocOtherX == line.LocOtherX && lineChild.LocOtherY == line.LocOtherY)
                    {
                        eleOther.Color = lineChild.Color;
                        eleOther.LocX = lineChild.LocX;
                        eleOther.LocY = lineChild.LocY;
                        eleOther.LocOtherX = lineChild.LocOtherX;
                        eleOther.LocOtherY = lineChild.LocOtherY;
                        eleOther.Idx = lineChild.Idx;
                        eleOther.oneFoot = lineChild.oneFoot;
                        eleOther.otherFoot = lineChild.otherFoot;
                        eleOther.Name = lineChild.Name;
                    }
                }
                UcLine lineOne = new UcLine(eleOne, DeleteLineLink, ChangeLineColor);
                UcLine lineOther = new UcLine(eleOther, DeleteLineLink, ChangeLineColor);
                lineOne.OtherLine = lineOther;
                lineOther.OtherLine = lineOne;
                this.pnBoard.Controls.Add(lineOne);
                this.pnBoard.Controls.Add(lineOther);
                lineOne.BringToFront();
                lineOther.BringToFront();
            }
            foreach (NetPoint point in projInfo.pointsList)
            {
                if (point.Type == enumNetPointType.NONE) continue;
                for (int i = 0; i < this.pnBoard.Controls.Count; i++)
                {
                    Control item = this.pnBoard.Controls[i];
                    if (item.GetType() == typeof(UcPoint) && (item as UcPoint).X == point.X && (item as UcPoint).Y == point.Y)
                    {
                        //UcPoint itemPoint = (item as UcPoint);
                        //itemPoint.UpdateStatusOnInitialLoad(point.Type);
                        //UpdatePointName(itemPoint, point.Type);
                    }
                }
            }
        }

        private void colorSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetColor formColor = new FormSetColor();
            formColor.ShowDialog();
        }

        private void sizeSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetSize formSize = new FormSetSize(UpdateShowSize);
            formSize.ShowDialog();
        }

        #endregion 

        #region 右键菜单
        private void vCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setNetPointValue(sender as ToolStripItem, enumNetPointType.VCC);
        }

        private void gNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setNetPointValue(sender as ToolStripItem, enumNetPointType.GND);
        }

        private void nonePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.setNetPointValue(sender as ToolStripItem, enumNetPointType.NONE);
        }
        
        private void setNetPointValue(ToolStripItem stripItem, enumNetPointType pointType)
        {
            string strXY = stripItem.Owner.Text;
            Point locIdx = this.DecodeIndexByStr(strXY);
            Point luCoordi = this.CalcCoordinateByLocIdx(locIdx.X, locIdx.Y);

            Draw.DrawSolidCircle(this.pnBoard, pointType, luCoordi.X, luCoordi.Y);
            this.ChangeNetPoint(locIdx, pointType);
        }

        private void setOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strXY = (sender as ToolStripItem).Owner.Text;
            Point locIdx = this.DecodeIndexByStr(strXY);

            this.currentBoardInfo.Project.OriginX = locIdx.X;
            this.currentBoardInfo.Project.OriginY = locIdx.Y;

            this.Refresh();
        }

        private Point DecodeIndexByStr(string strXY)
        {
            Point locIdx = new Point();
            int sepIdx = strXY.IndexOf(COORDINATE_SEPERATOR); 
            locIdx.X = int.Parse(strXY.Substring(0,sepIdx));
            locIdx.Y = int.Parse(strXY.Substring(sepIdx + 1, strXY.Length - sepIdx - 1));

            return locIdx;
        }

        private void colorLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int lineIdx = int.Parse((sender as ToolStripItem).Owner.Text);
            Color beforeColor = defaultLineColor;
            foreach (ElementLine line in this.currentBoardInfo.linesList)
            {
                if (line.InnerIdx == lineIdx)
                {
                    beforeColor = line.Color;
                    break;
                }
            }
            ColorDialog dialog = new ColorDialog();
            dialog.Color = beforeColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for( int i=this.currentBoardInfo.linesList.Count - 1;i>=0 ;i--)
                {
                    ElementLine line = this.currentBoardInfo.linesList[i];
                    if (line.InnerIdx == lineIdx)
                    {
                        line.Color = dialog.Color;
                        this.currentBoardInfo.linesList.RemoveAt(i);
                        this.currentBoardInfo.linesList.Add(line);

                        Point coorOne = this.CalcCoordinateByLocIdx(line.LocX,line.LocY),
                            coorOther = this.CalcCoordinateByLocIdx(line.LocOtherX,line.LocOtherY);

                        Draw.DrawSolidLine(this.pnBoard, coorOne, coorOther, line.Color);
                    }
                }
            }
        }

        #endregion

        #region 串口事件
        private void ListenSerialPort()
        {
            byte[] byteRecvive = new byte[1024];
            while(true)
            {
                string strMsg = string.Empty;

                serial.DiscardInBuffer();
                serial.DiscardOutBuffer();
                if (!serial.ReadBuffer(byteRecvive, 5))
                {
                    Console.WriteLine("数据标志位失败");
                }
                handlerSerialData(byteRecvive);
            }
        }

        private void handlerSerialData(byte[] buff)
        {
            //GetElementInfoOnBoardByName:根据名称获取显示信息
            //getElementView:根据显示显示创建显示对象
        }

        private void WriteSerialPort(string strSend)
        {
            serial.DiscardInBuffer();
            serial.DiscardOutBuffer();

            serial.Write(strSend);
        }
        #endregion
    }
}
