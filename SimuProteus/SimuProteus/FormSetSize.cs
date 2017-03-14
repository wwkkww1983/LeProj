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
    public partial class FormSetSize : Form
    {
        Action updateChangeSize;
        public FormSetSize(Action updateChangeSize)
        {
            this.updateChangeSize = updateChangeSize;

            InitializeComponent();

            this.tbWidthBoard.Text = Ini.GetItemValue("sizeInfo", "pixelBoardWidth");
            this.tbHeightBoard.Text = Ini.GetItemValue("sizeInfo", "pixelBoardHeight");
            this.tbWidthWindow.Text = Ini.GetItemValue("sizeInfo", "pixelInitialWidth");
            this.tbHeightWindow.Text = Ini.GetItemValue("sizeInfo", "pixelInitialHeight");
            this.tbFootSize.Text = Ini.GetItemValue("sizeInfo", "pixelFootSize");
            this.tbLineWidth.Text = Ini.GetItemValue("sizeInfo", "pixelLineWidth");
            this.tbNetPoint.Text = Ini.GetItemValue("sizeInfo", "pixelNetPoint");
            this.tbMargin.Text = Ini.GetItemValue("sizeInfo", "pixelBoardMargin");
            this.tbDragDistance.Text = Ini.GetItemValue("sizeInfo", "pixelDragDistance");
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int boardWidth= int.Parse(this.tbWidthBoard.Text) ;
            int boardHeight = int.Parse(this.tbHeightBoard.Text) ;
            int windowWidth = int.Parse(this.tbWidthWindow.Text) ;
            int widowHeight = int.Parse(this.tbHeightWindow.Text) ;

            if (!(boardWidth > 0 && boardHeight > 0 &&windowWidth > 0 && widowHeight > 0 &&
                int.Parse(this.tbFootSize.Text) > 0 && int.Parse(this.tbLineWidth.Text) > 0 &&
                int.Parse(this.tbNetPoint.Text) > 0 && int.Parse(this.tbMargin.Text) > 0 && 
                int.Parse(this.tbDragDistance.Text) > 0))
            {
                MessageBox.Show("仅可输入正整数");
            }
            else if (boardWidth < 500 || boardWidth > 5000 || boardHeight < 500 || boardHeight > 5000)
            {
                MessageBox.Show("画板尺寸有效范围【500,5000】");
                this.tbWidthBoard.Text = Ini.GetItemValue("sizeInfo", "pixelBoardWidth");
                this.tbHeightBoard.Text = Ini.GetItemValue("sizeInfo", "pixelBoardHeight");
            }
            else if (windowWidth < 500 || windowWidth > 5000 || widowHeight < 500 || widowHeight > 5000)
            {
                MessageBox.Show("窗口尺寸有效范围【500,5000】");
                this.tbWidthWindow.Text = Ini.GetItemValue("sizeInfo", "pixelInitialWidth");
                this.tbHeightWindow.Text = Ini.GetItemValue("sizeInfo", "pixelInitialHeight");
            }
            else
            {
                Ini.SetItemValue("sizeInfo", "pixelBoardWidth", this.tbWidthBoard.Text);
                Ini.SetItemValue("sizeInfo", "pixelBoardHeight", this.tbHeightBoard.Text);
                Ini.SetItemValue("sizeInfo", "pixelInitialWidth", this.tbWidthWindow.Text);
                Ini.SetItemValue("sizeInfo", "pixelInitialHeight", this.tbHeightWindow.Text);
                Ini.SetItemValue("sizeInfo", "pixelFootSize", this.tbFootSize.Text);
                Ini.SetItemValue("sizeInfo", "pixelLineWidth", this.tbLineWidth.Text);
                Ini.SetItemValue("sizeInfo", "pixelNetPoint", this.tbNetPoint.Text);
                Ini.SetItemValue("sizeInfo", "pixelBoardMargin", this.tbMargin.Text);
                Ini.SetItemValue("sizeInfo", "pixelDragDistance", this.tbDragDistance.Text);

                this.updateChangeSize();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
