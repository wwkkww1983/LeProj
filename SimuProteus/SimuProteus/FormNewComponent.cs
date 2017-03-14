using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class FormNewComponent : Form
    {
        private string sourcePath = string.Empty;
        private string filePath = "img/none.png";

        public FormNewComponent()
        {
            InitializeComponent();

            this.picBoxImg.Image = Image.FromFile("img/none.png");
            this.tbWidth.Text = "1";
            this.tbHeight.Text = "1";
        }

        private void picBoxImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "图片文件|*.jpg;*.png;*.gif";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sourcePath = dialog.FileName;
                this.picBoxImg.Image = Image.FromFile(sourcePath);                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("名称不可为空"); return;
            }
            if (sourcePath != string.Empty)
            {
                filePath = "img/" + tbName.Text.Trim() + Path.GetExtension(sourcePath);
                File.Copy(sourcePath, filePath);
            }

            List<LineFoot> lineList = new List<LineFoot>();
            foreach (DataGridViewRow row in dgvFoot.Rows)
            {
                foreach (DataGridViewColumn column in dgvFoot.Columns)
                {
                    Console.WriteLine(column.Name +" = " +row.Cells[column.Name]);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbWidth_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbInfo.Text = "图片在画板中宽度：（单位为网格间距）";
        }

        private void tbHeight_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbInfo.Text = "图片在画板中高度：（单位为网格间距）";
        }

        private void tbName_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbInfo.Text = "元器件类型名称（比如：电容，电阻等）";
        }

        private void dgvFoot_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string strInfo = string.Empty;
            switch (e.ColumnIndex)
            {
                case 0: strInfo = "管脚编号：仅数字"; break;
                case 1: strInfo = "管脚标注内容"; break;
                case 2: strInfo = "管脚相对于元器件最左边的像素距离（管脚 X 轴坐标）"; break;
                case 3: strInfo = "管脚相对于元器件最上边的像素距离（管脚 Y 轴坐标）"; break;
                case 4: strInfo = "显示管脚名称 X 轴相对坐标（空表示不显示）"; break;
                case 5: strInfo = "显示管脚名称 Y 轴相对坐标（空表示不显示）"; break;
                case 6: strInfo = "显示管脚编号 X 轴相对坐标（空表示不显示）"; break;
                case 7: strInfo = "显示管脚编号 Y 轴相对坐标（空表示不显示）"; break;
                default: break;
            }

            this.lbInfo.Text = strInfo;
        }
    }
}
