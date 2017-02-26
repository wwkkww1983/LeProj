using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class FormMain : Form
    {
        #region 初始化
        private int elementIdx = 0;
        private enumComponent currentSelectedComponent = enumComponent.NONE;
        private int currentChips = -1;
        private Point clickPositionForLine ;
        List<ElementInfo> elementList = null;
        List<ElementLine> createLinePoint = new List<ElementLine>(2);
        ProjectDetails currentBoardInfo = new ProjectDetails() { 
            Project=new ProjectInfo (), 
            elementList = new List<ElementInfo> (), 
            linesList = new List<ElementLine> () };

        public FormMain()
        {
            InitializeComponent();

            DBUtility dbHandler = new DBUtility();
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
                ToolStripItem projItem = new ToolStripMenuItem(item.Name);
                projItem.Tag = item.Idx;
                projItem.Click += projectNameToolStripMenuItem_Click;
                this.ProjToolStripMenuItem.DropDownItems.Add(projItem);
            }
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

        private void SaveProjectName(int projIdx, string projName)
        {
            if (this.InvokeRequired)
            {
                Action<int, string> delegateSaveProjectName = new Action<int, string>(SaveProjectName);
                this.Invoke(delegateSaveProjectName, new object[] { projIdx });
                return;
            }
            this.lbProjName.Tag = projIdx;
            this.lbProjName.Text = projName;
        }

        private void CreateLineForElement(int footIdx, int locX, int locY)
        {
            if (createLinePoint.Count == 0)
            {
                createLinePoint.Add(new ElementLine()
                {
                    Idx = elementIdx++,
                    oneFoot = footIdx,
                    LocX = locX,
                    LocY = locY
                });
                clickPositionForLine.X = 0;
                clickPositionForLine.Y = 0;
                this.Cursor = Cursors.Cross;
                return;
            }
            if (locX == createLinePoint[0].LocX && locY == createLinePoint[0].LocY) return;
            ElementLine eleOne = createLinePoint[0];
            ElementLine eleOther = new ElementLine()
            {
                Idx = elementIdx++,
                oneFoot = footIdx,
                LocX = locX,
                LocY = locY
            };
            eleOne.otherFoot = footIdx;
            eleOther.otherFoot = eleOne.oneFoot;
            if (eleOne.LocX == locX || eleOne.LocY == locY)
            {//在同一个水平/竖直方向上
                eleOne.LocOtherX = locX;
                eleOne.LocOtherY = locY;
            }
            else
            {//统一参考坐标系
                int xLeft, yLeft, xRight, yRihgt;
                if (eleOne.LocY < locY)
                {//下面点为参考点
                    xLeft = eleOne.LocX; yLeft = eleOne.LocY;
                    xRight = locX; yRihgt = locY;
                }
                else
                {
                    xLeft = locX; yLeft = locY;
                    xRight = eleOne.LocX; yRihgt = eleOne.LocY;
                }
                if (((xRight-xLeft) * (clickPositionForLine.Y - yLeft) - (yRihgt - yLeft) * (clickPositionForLine.X - xLeft)) > 0)
                {//左边
                    eleOne.LocOtherX = Math.Min(eleOne.LocX, locX);
                    //绘图坐标系跟直角坐标是沿X轴对称的，所以斜率是反的但左右不变
                    eleOne.LocOtherY = (eleOne.LocY * 0.1 - locY * 0.1) / (eleOne.LocX * 0.1 - locX * 0.1) > 0 ? Math.Max(eleOne.LocY, locY) : Math.Min(eleOne.LocY, locY);
                }
                else
                {
                    eleOne.LocOtherX = Math.Max(eleOne.LocX, locX);
                    eleOne.LocOtherY = (eleOne.LocY * 0.1 - locY * 0.1) / (eleOne.LocX * 0.1 - locX * 0.1) < 0 ? Math.Max(eleOne.LocY, locY) : Math.Min(eleOne.LocY, locY);
                }
            }
            eleOther.LocOtherX = eleOne.LocOtherX;
            eleOther.LocOtherY = eleOne.LocOtherY;
            createLinePoint.Clear();
            this.currentBoardInfo.linesList.Add(eleOne);
            this.currentBoardInfo.linesList.Add(eleOther);
            UcLine lineOne = new UcLine(eleOne, DeleteLineLink);
            UcLine lineOther = new UcLine(eleOther, DeleteLineLink);
            lineOne.OtherLine = lineOther;
            lineOther.OtherLine = lineOne;
            this.pnBoard.Controls.Add(lineOne);
            lineOne.BringToFront();
            this.pnBoard.Controls.Add(lineOther);
            lineOther.BringToFront();
            this.Cursor = Cursors.Arrow;
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
                if (tmpInfo.ID == idx)
                {
                    this.currentBoardInfo.elementList.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < this.pnBoard.Controls.Count; i++)
            {
                Control item = this.pnBoard.Controls[i];
                if (item.GetType() == typeof(UcElement) && (item as UcElement).ViewInfo.ID == idx)
                {
                    this.pnBoard.Controls.RemoveAt(i);
                    break;
                }
            }
        }

        #endregion

        #region 面板事件

        private void pnBoard_Click(object sender, EventArgs e)
        {
            MouseEventArgs clickArgs = (MouseEventArgs)e;
            ElementInfo infoTmp = GetElementByName(this.currentSelectedComponent.ToString());
            ElementInfo info =(ElementInfo) infoTmp.Clone();
            info.Location = new Point(clickArgs.X, clickArgs.Y);
            if (info.Name == enumComponent.NONE.ToString())
            {
                if (this.createLinePoint.Count == 1)
                    this.clickPositionForLine = info.Location;
                return;
            }
            info.ID = elementIdx++;
            this.pnBoard.Controls.Add(new UcElement(info, CreateLineForElement, DeleteElement));
            this.currentBoardInfo.elementList.Add(info);
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
                this.currentChips = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
                this.pnBoard.Controls.Add(this.InitialChipOnBoard(currentChips));
                currentBoardInfo.Project.Chips = this.currentChips;
            }
        }

        private void lS221ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckLoadChip())
            {
                this.currentChips = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
                this.pnBoard.Controls.Add(this.InitialChipOnBoard(currentChips));
                currentBoardInfo.Project.Chips = this.currentChips;
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

            return new UcElement(info, CreateLineForElement, DeleteElement);
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

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("待做");

        }

        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("待做");

        }

        private void projectNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem projItem = sender as ToolStripItem;
            MessageBox.Show(projItem.Text + "==待做");
        }
        #endregion 

    }
}
