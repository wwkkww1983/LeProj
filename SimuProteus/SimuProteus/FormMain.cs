using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class FormMain : Form
    {
        #region 初始化
        private bool serialReadFlag = false;
        private int elementIdx = 1;
        private enumComponent currentSelectedComponent = enumComponent.NONE;
        private Point clickPositionForLine ;
        private SerialCom serial = new SerialCom();
        DBUtility dbHandler = new DBUtility();
        List<ElementInfo> elementList = null;
        List<ElementLine> createLinePoint = new List<ElementLine>(2);
        ProjectDetails currentBoardInfo = new ProjectDetails() { 
            Project=new ProjectInfo (), 
            elementList = new List<ElementInfo> (), 
            linesList = new List<ElementLine> () ,
            footsList = new List<LineFootView> ()
        };

        public FormMain()
        {
            InitializeComponent();

            dbHandler.InitialTable();
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

        private void AddProjectHistory(string name, int idx)
        {
            ToolStripItem projItem = new ToolStripMenuItem(name);
            projItem.Tag = idx;
            projItem.Click += projectNameToolStripMenuItem_Click;
            this.ProjToolStripMenuItem.DropDownItems.Insert(0,projItem);
        }

        private void InitialNetPoint()
        {

        }
        #endregion

        #region 回调
        /// <summary>
        /// 改变光标
        /// </summary>
        /// <param name="newCursor"></param>
        private void ChangeCursor(Cursor newCursor,enumComponent clickedCompo)
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
                    Color = Color.FromArgb(int.Parse(Ini.GetItemValue("colorInfo", "colorLine")))
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
                Color = Color.FromArgb(int.Parse(Ini.GetItemValue("colorInfo", "colorLine")))
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
            for (int i = 0; i < this.currentBoardInfo.elementList.Count; i++)
            {
                ElementInfo tmpInfo = this.currentBoardInfo.elementList[i];
                if (tmpInfo.InnerIdx == idx)
                {
                    tmpInfo.Location = new Point(locX, locY);
                    break;
                }
            }

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
            ElementInfo info = GetElementInfoOnBoardByName(this.currentSelectedComponent.ToString(),new Point(clickArgs.X, clickArgs.Y));
            if (info.Name == enumComponent.NONE.ToString())
            {
                if (this.createLinePoint.Count == 1)
                    this.clickPositionForLine = info.Location;
                return;
            }
            if (clickArgs.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.Cursor = Cursors.Arrow;
                this.currentSelectedComponent = enumComponent.NONE;
            }
            else
            {
                info.InnerIdx = elementIdx++;
                this.pnBoard.Controls.Add(this.getElementView(info));
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


        #endregion 

        #region 菜单事件
        private void hC244ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckLoadChip())
            {
                int currentChips = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
                this.pnBoard.Controls.Add(this.InitialChipOnBoard(currentChips));
                currentBoardInfo.Project.Chips = currentChips;
            }
        }

        private void lS221ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckLoadChip())
            {
                int currentChips = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
                this.pnBoard.Controls.Add(this.InitialChipOnBoard(currentChips));
                currentBoardInfo.Project.Chips = currentChips;
            }
        }

        private UcElement InitialChipOnBoard(int chipIdx)
        {
            string chipName = string.Empty;
            switch (chipIdx)
            {
                case 1: chipName = "\\img\\74HC244.jpg"; break;
                case 2: chipName = "\\img\\HD74LS221P.jpg"; break;
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
                LineFoots = new DBUtility().GetChipFoots(chipIdx)
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
            if (this.pnBoard.Controls.Count > 1)
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

            MessageBox.Show("==待做");
        }

        private void freeSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serial.IsOpen)
            {
                serial.Close();
            }
            else
            {
                MessageBox.Show("串口未开启");
            }
        }

        private void serialStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetSerial formSerial = new FormSetSerial(serial);
            formSerial.ShowDialog();
        }

        private void projectNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckLoadChip()) return;
            ToolStripItem projItem = sender as ToolStripItem;

            ProjectDetails projInfo = dbHandler.getProjectDetail(Convert.ToInt32(projItem.Tag));
            this.currentBoardInfo = projInfo;
            this.LoadElementByHistoryItem(projInfo);
        }

        private void LoadElementByHistoryItem(ProjectDetails projInfo)
        {
            this.lbProjName.Text = projInfo.Project.Name;
            this.lbProjName.Tag = projInfo.Project.Idx;
            if (projInfo.Project.Chips > 0)
            {
                this.pnBoard.Controls.Add(this.InitialChipOnBoard(projInfo.Project.Chips));
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
        }

        private void colorSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetColor formColor = new FormSetColor();
            formColor.ShowDialog();
        }

        private void sizeSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetSize formSize = new FormSetSize();
            formSize.ShowDialog();
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
