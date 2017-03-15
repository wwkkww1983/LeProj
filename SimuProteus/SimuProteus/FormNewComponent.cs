using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace SimuProteus
{
    public partial class FormNewComponent : Form
    {
        private string sourcePath = string.Empty;
        private string filePath = "img\\none.png";
        private int netSize = int.Parse(Ini.GetItemValue("sizeInfo", "pixelPointGap"));

        public FormNewComponent()
        {
            InitializeComponent();

            this.picBoxImg.Image = Image.FromFile(filePath);
            this.tbWidth.Text = "1";
            this.tbHeight.Text = "1";
            this.UpdateComponentSize(1, 1);
        }

        private void UpdateComponentSize(int width,int height)
        {
            this.lbLoc.Text = string.Format("({0},{1})", netSize * width, netSize * height);
        }

        private void FormNewComponent_Load(object sender, EventArgs e)
        {
            dgvFoot.Rows[0].Cells[0].Value = 1;
            dgvFoot.Rows[0].Cells[2].Value = "Input";
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
            List<LineFoot> lineList = new List<LineFoot>();
            if (tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("名称不可为空"); 
                return;
            }
            if (!this.CheckLocation(lineList))
            {
                return;
            }

            int imgWidth = netSize * int.Parse(tbWidth.Text),
                imgHeight = netSize * int.Parse(tbHeight.Text);
            if (sourcePath != string.Empty)
            {
                filePath = "img\\" + tbName.Text.Trim() + Path.GetExtension(sourcePath);
                if(File.Exists(filePath))
                {
                    MessageBox.Show("类型名称重复"); 
                    return;
                }
                Image newImg = Draw.ResizeImage(Image.FromFile(sourcePath), imgWidth, imgHeight);
                newImg.Save(filePath);
            }

            ElementInfo info = new ElementInfo();
            info.Name = tbName.Text.Trim ();
            info.FootType = rbComponent.Checked? enumComponentType.NormalComponent: enumComponentType.Chips;
            info.Size = new System.Drawing.Size(imgWidth, imgHeight);
            info.BackColor = Color.Gray;
            info.LineFoots = lineList;
            info.BackImage = filePath;


            //AddComponentFoots(1, enumComponentType.Chips, new List<LineFoot>(){
            //    new LineFoot(){LocX=20,LocY=1},
            //    new LineFoot(){LocX=38,LocY=1},
            //    new LineFoot(){LocX=57,LocY=1},
            //    new LineFoot(){LocX=75,LocY=1},
            //    new LineFoot(){LocX=94,LocY=1},
            //    new LineFoot(){LocX=112,LocY=1},
            //    new LineFoot(){LocX=130,LocY=1},
            //    new LineFoot(){LocX=149,LocY=1},
            //    new LineFoot(){LocX=167,LocY=1},
            //    new LineFoot(){LocX=185,LocY=1},

            //    new LineFoot(){LocX=18,LocY=150},
            //    new LineFoot(){LocX=36,LocY=150},
            //    new LineFoot(){LocX=56,LocY=150},
            //    new LineFoot(){LocX=75,LocY=150},
            //    new LineFoot(){LocX=94,LocY=150},
            //    new LineFoot(){LocX=112,LocY=150},
            //    new LineFoot(){LocX=130,LocY=150},
            //    new LineFoot(){LocX=149,LocY=150},
            //    new LineFoot(){LocX=167,LocY=150},
            //    new LineFoot(){LocX=185,LocY=150}
            //});

            DBUtility dbhandler = new DBUtility();
            if (dbhandler.AddNewBaseComponent(info))
            {
                MessageBox.Show("添加成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }

        private bool CheckLocation(List<LineFoot> lineList)
        {
            bool result = true;
            if (dgvFoot.Rows.Count <= 1)
            {
                MessageBox.Show("未添加管脚信息");
                return false;
            }

            for (int i = 1; i < dgvFoot.Rows.Count;i++ )
            {
                DataGridViewRow row = dgvFoot.Rows[i-1];
                LineFoot foot = new LineFoot();
                foot.Name = row.Cells[1].Value.ToString();
                foot.PinsType = (enumPinsType)Enum.Parse(typeof(enumPinsType), row.Cells[2].Value.ToString());
                foot.LocX = Convert.ToInt32(row.Cells[3].Value);
                foot.LocY = Convert.ToInt32(row.Cells[4].Value);
                foot.NameLocX = Convert.ToInt32(row.Cells[5].Value);
                foot.NameLocY = Convert.ToInt32(row.Cells[6].Value);

                lineList.Add(foot);
            }

            LineFoot origin = lineList[0];
            foreach(LineFoot foot in lineList)
            {
                if (Math.Abs(foot.LocX - origin.LocX) % netSize != 0 || Math.Abs(foot.LocY - origin.LocY) % netSize != 0)
                {
                    MessageBox.Show("管脚坐标不能都在节点上");
                    result = false;
                    break;
                }
            }

            return result;
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbWidth_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbInfo.Text = string.Format( "图片在画板中宽度：（单位为网格间距【{0}px】）",netSize);
        }

        private void tbHeight_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbInfo.Text = string.Format("图片在画板中高度：（单位为网格间距【{0}px】）", netSize);
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
                case 2: strInfo = "管脚类型"; break;
                case 3: strInfo = "管脚相对于元器件最左边的像素距离（管脚 X 轴坐标）"; break;
                case 4: strInfo = "管脚相对于元器件最上边的像素距离（管脚 Y 轴坐标）"; break;
                case 5: strInfo = "显示管脚名称 X 轴相对坐标（空表示不显示）"; break;
                case 6: strInfo = "显示管脚名称 Y 轴相对坐标（空表示不显示）"; break;
                case 7: strInfo = "显示管脚编号 X 轴相对坐标（空表示不显示）"; break;
                case 8: strInfo = "显示管脚编号 Y 轴相对坐标（空表示不显示）"; break;
                default: break;
            }

            this.lbInfo.Text = strInfo;
        }

        private void dgvFoot_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView tmpDgv = sender as DataGridView;
            if (e.ColumnIndex > 2)
            {
                object objValue = tmpDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (objValue != null && !StringValidator.CheckNumber(objValue.ToString()))
                {
                    //tmpDgv.CurrentCell = tmpDgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //tmpDgv.EditMode = DataGridViewEditMode.EditOnEnter;
                    //tmpDgv.BeginEdit(true);
                }
            }
        }

        private void dgvFoot_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView tmpDgv = sender as DataGridView;
            tmpDgv.Rows[e.RowIndex].Cells[0].Value = tmpDgv.Rows.Count;
            tmpDgv.Rows[e.RowIndex].Cells[2].Value = "Input";
        }

        private void tbSize_TextChanged(object sender, EventArgs e)
        {
            if (tbWidth.Text == string.Empty || tbHeight.Text == string.Empty)
                return;
            if (StringValidator.CheckUnsignedNumber((sender as TextBox).Text))
            {
                int width = int.Parse(tbWidth.Text);
                int height = int.Parse(tbHeight.Text);
                this.UpdateComponentSize(width, height);
            }
        }

       

    }
}
