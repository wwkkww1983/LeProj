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
    public partial class UcElement : UserControl
    {
        private bool mouseDownFlag = false;
        private int currentX = 0, currentY = 0;
        private DBUtility dbHandler = new DBUtility();
        private List<LineFootView> pinsList = null;
        private Action<int, int, int,int> ClickFoot = null;
        private Action<int, int, int> dragElement=null;
        private Action<int, string> updateElementName = null;
        private Action<int>  RemoveElement = null;
        private Action<LineFootView> updateElementFoots = null;

        /// <summary>
        /// 元器件显示信息
        /// </summary>
        public ElementInfo ViewInfo
        {
            get;
            set;
        }

        public UcElement()
        {
            InitializeComponent();
        }

        public UcElement(ElementInfo viewInfo, Action<int, int, int, int> linkLine, Action<int> removeSelf, Action<int, int, int> moveElement, Action<int, string> updateElementName, Action<LineFootView> updateElementFoots, List<LineFootView> pinsList)
        {
            this.ViewInfo = viewInfo;
            this.ClickFoot = linkLine;
            this.RemoveElement = removeSelf;
            this.dragElement = moveElement;
            this.updateElementName = updateElementName;
            this.updateElementFoots = updateElementFoots;
            this.pinsList = pinsList;

            InitializeComponent();

            this.InitialShow();
        }

        /// <summary>
        /// 初始化显示
        /// </summary>
        private void InitialShow()
        {
            this.picbElement.Location = new Point(Constants.FOOT_SIZE_PIXEL, Constants.FOOT_SIZE_PIXEL);
            this.picbElement.Size = this.ViewInfo.Size;
            this.picbElement.Image = Image.FromFile(this.ViewInfo.BackImage);
            //this.BackColor = this.ViewInfo.BackColor;

            foreach (LineFoot item in this.ViewInfo.LineFoots)
            {
                UcFoot footView = new UcFoot(item.Idx, item.LocX, item.LocY);
                footView.ClickFoot = this.ExchangeCoordinate;
                this.Controls.Add(footView);
                footView.BringToFront();
            }
            this.Width = this.ViewInfo.Size.Width + Constants.FOOT_SIZE_PIXEL * 2;
            this.Height = this.ViewInfo.Size.Height + Constants.FOOT_SIZE_PIXEL * 2;
            this.Location = this.ViewInfo.Location;

            if (this.ViewInfo.FootType == enumComponentType.Chips)
                this.ContextMenuStrip = null;
        }


        private void ExchangeCoordinate(int footIdx,int locX, int locY)
        {
            this.ClickFoot(this.ViewInfo.InnerIdx,footIdx, locX + this.Location.X + Constants.FOOT_SIZE_PIXEL / 2, locY + this.Location.Y + Constants.FOOT_SIZE_PIXEL / 2);
        }


        /// <summary>
        /// 移动组件
        /// </summary>
        /// <param name="location"></param>
        public void MoveElement(Point location)
        {
            this.ViewInfo.Location = location;
            dbHandler.MoveComponentShow(this.ViewInfo);
        }

        private void UcElement_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownFlag = true;
            currentX = e.X;
            currentY = e.Y;
        }

        private void UcElement_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownFlag = false;
            this.dragElement(this.ViewInfo.InnerIdx, this.Location.X, this.Location.Y);
        }

        private void UcElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDownFlag) return;

            Point pTemp = new Point(Cursor.Position.X, Cursor.Position.Y);
            pTemp = this.Parent.PointToClient(pTemp);
            this.Location = new Point(pTemp.X - currentX, pTemp.Y - currentY);
        }

        private void picbElement_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownFlag = true;
            //坐标补偿
            currentX = e.X + Constants.FOOT_SIZE_PIXEL;
            currentY = e.Y + Constants.FOOT_SIZE_PIXEL;
        }

        private void picbElement_MouseUp(object sender, MouseEventArgs e)
        {
            this.UcElement_MouseUp(sender, e);
        }

        private void picbElement_MouseMove(object sender, MouseEventArgs e)
        {
            this.UcElement_MouseMove(sender, e);
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveElement(this.ViewInfo.InnerIdx);
        }

        private void UcElement_DoubleClick(object sender, EventArgs e)
        {
            FormFootParam formFoot = new FormFootParam(this.ViewInfo, this.updateElementName, this.updateElementFoots, this.pinsList);
            formFoot.ShowDialog();
        }

        private void picbElement_DoubleClick(object sender, EventArgs e)
        {
            this.UcElement_DoubleClick(sender, e);
        }
        
    }
}
