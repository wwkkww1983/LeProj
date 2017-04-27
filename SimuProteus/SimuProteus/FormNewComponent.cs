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
        private bool isCreateNewComponent = true,isFinishInitial = true;
        private string sourcePath = string.Empty;
        private string filePath = "img\\none.png";
        private int netSize = int.Parse(Ini.GetItemValue("sizeInfo", "pixelPointGap"));
        private int netPointSize = int.Parse(Ini.GetItemValue("sizeInfo", "pixelNetPoint"));
        private int netInterval = 0;
        private Action<FormNewComponent, ElementInfo> createComponent = null, updateComponent = null;
        private ElementInfo sourceInfo = null;

        public FormNewComponent(Action<FormNewComponent, ElementInfo> createComponent,Action<FormNewComponent, ElementInfo> updateComponent, ElementInfo info)
        {
            this.createComponent = createComponent;
            this.updateComponent = updateComponent;
            this.sourceInfo = info;

            InitializeComponent();

            this.netInterval = this.netSize + this.netPointSize;
            int width = netSize, height = netSize;
            if (this.sourceInfo != null)
            {
                this.isCreateNewComponent = false;
                filePath = this.sourceInfo.BackImage;
                width = this.sourceInfo.Size.Width;
                height = this.sourceInfo.Size.Height;
                this.rbComponent.Checked = true;
                this.tbNumber.Text = this.sourceInfo.Number;
                this.tbName.Text = this.sourceInfo.Name;
                this.isFinishInitial = false;
                DataTable dt = this.constructLineFoot(this.sourceInfo.LineFoots);
                this.dgvFoot.DataSource = dt;
                
            }
            this.picBoxImg.Image = Image.FromFile(filePath);
            this.tbWidth.Text = width.ToString();
            this.tbHeight.Text = height.ToString();
            this.UpdateComponentSize(netSize, netSize);
        }

        private DataTable constructLineFoot(List<LineFoot> foots)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Number");
            dt.Columns.Add(dc);
            dc = new DataColumn("txtName");
            dt.Columns.Add(dc);
            dc = new DataColumn("type");
            dt.Columns.Add(dc);
            dc = new DataColumn("locX");
            dt.Columns.Add(dc);
            dc = new DataColumn("locY");
            dt.Columns.Add(dc);
            dc = new DataColumn("nameLocX");
            dt.Columns.Add(dc);
            dc = new DataColumn("nameLocY");
            dt.Columns.Add(dc);
            dc = new DataColumn("numLocX");
            dt.Columns.Add(dc);
            dc = new DataColumn("numLocY");
            dt.Columns.Add(dc);
            foreach (LineFoot foot in foots)
            {
                DataRow dr = dt.NewRow();
                dr["Number"] = foot.InnerIdx;
                dr["txtName"] = foot.Name;
                dr["type"] = foot.PinsType.ToString();
                dr["locX"] = foot.LocX;
                dr["locY"] = foot.LocY;
                dr["nameLocX"] = foot.NameLocX;
                dr["nameLocY"] = foot.NameLocY;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void UpdateComponentSize(int width, int height)
        {
            this.lbLoc.Text = string.Format("({0},{1})", width, height);
        }

        private void FormNewComponent_Load(object sender, EventArgs e)
        {
            if (this.isCreateNewComponent)
            {
                dgvFoot.Rows[0].Cells[0].Value = 1;
                dgvFoot.Rows[0].Cells[2].Value = "Input";
            }
            this.isFinishInitial = true;
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
            if (!StringValidator.HasContent(this.tbName, this.lbName.Text) || !StringValidator.HasContent(this.tbNumber, this.lbNumber.Text) ||
                !this.CheckLocation(lineList))
            {
                return;
            }

            if (this.tbNumber.Text.Trim().Length != 8)
            {
                MessageBox.Show("ID号要求为8位");
                return;
            }

            int imgWidth = int.Parse(tbWidth.Text),
                imgHeight = int.Parse(tbHeight.Text);
            if (sourcePath != string.Empty)
            {
                filePath = "img\\" + tbName.Text.Trim() + Path.GetExtension(sourcePath);
                if (File.Exists(filePath))
                {
                    filePath = "img\\" + tbName.Text.Trim() + DateTime.Now.ToString("-yyMMddhhmmss") + Path.GetExtension(sourcePath);
                }
                Image newImg = Draw.ResizeImage(Image.FromFile(sourcePath), imgWidth, imgHeight);
                newImg.Save(filePath);
            }

            ElementInfo info = new ElementInfo();
            info.Number = tbNumber.Text.Trim();
            info.Name = tbName.Text.Trim();
            info.FootType = rbComponent.Checked ? enumComponentType.NormalComponent : enumComponentType.Chips;
            info.Size = new System.Drawing.Size(imgWidth, imgHeight);
            info.BackColor = Color.Gray;
            info.LineFoots = lineList;
            info.BackImage = filePath;
            if (this.isCreateNewComponent)
            {
                this.createComponent(this, info);
            }
            else
            {
                info.ID = this.sourceInfo.ID;
                this.sourceInfo.Number = info.Number;
                this.sourceInfo.Name = info.Name;
                this.sourceInfo.FootType = info.FootType;
                this.sourceInfo.Size = info.Size;
                this.sourceInfo.BackColor = info.BackColor;
                this.sourceInfo.LineFoots = null;
                this.sourceInfo.LineFoots = info.LineFoots;
                this.sourceInfo.BackImage = info.BackImage;
                
                this.updateComponent(this, info);
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

            int imgWidth = int.Parse(tbWidth.Text),
                imgHeight = int.Parse(tbHeight.Text);
            int minX = int.MaxValue, minY = int.MaxValue, maxX = 0, maxY = 0;
            for (int i = 1; i < dgvFoot.Rows.Count; i++)
            {
                DataGridViewRow row = dgvFoot.Rows[i - 1];
                LineFoot foot = new LineFoot();
                foot.InnerIdx = Convert.ToInt32(row.Cells[0].Value);
                foot.Name = row.Cells[1].Value.ToString();
                foot.PinsType = (enumPinsType)Enum.Parse(typeof(enumPinsType), row.Cells[2].Value.ToString());
                foot.LocX = Convert.ToInt32(row.Cells[3].Value);
                foot.LocY = Convert.ToInt32(row.Cells[4].Value);
                if (!(foot.LocX >= 0 && foot.LocX <= imgWidth && foot.LocY >= 0 && foot.LocY <= imgHeight &&
                    (foot.LocX == 0 || foot.LocX == imgWidth || foot.LocY == 0 || foot.LocY == imgHeight)))
                {
                    MessageBox.Show("管脚仅可以在元器件边界上");
                    result = false;
                    break;
                }
                foot.NameLocX = Convert.ToInt32(row.Cells[5].Value);
                foot.NameLocY = Convert.ToInt32(row.Cells[6].Value);

                lineList.Add(foot);
                minX = Math.Min(foot.LocX, minX);
                minY = Math.Min(foot.LocY, minY);
                maxX = Math.Max(foot.LocX, maxX);
                maxY = Math.Max(foot.LocY, maxY);
            }

            result = result && !(maxX - minX > 0 && (maxX - minX) % this.netInterval != 0 ||
                               maxY - minY > 0 && (maxY - minY) % this.netInterval != 0);
            if (!result)
            {
                MessageBox.Show("管脚不在节点上");
            }

            for (int i = 0; result && i < lineList.Count; i++)
            {
                LineFoot foot = lineList[i];
                if (foot.LocX == imgWidth && (foot.LocX) % this.netInterval != 0 ||
                    foot.LocY == imgHeight && (foot.LocY) % this.netInterval != 0)
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
            this.lbInfo.Text = "图片在画板中宽度：（单位为像素px）";
        }

        private void tbHeight_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbInfo.Text = "图片在画板中高度：（单位为像素px）";
        }

        private void tbName_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbInfo.Text = "元器件类型名称（比如：电容，电阻等）";
        }

        private void tbNumber_MouseClick(object sender, MouseEventArgs e)
        {
            this.lbInfo.Text = "元器件编号（8字节的ID）";
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
                if (objValue != null && StringValidator.IsNumber(objValue.ToString()))
                {
                    int maxValue = int.Parse(e.ColumnIndex % 2 == 0 ? tbWidth.Text : tbHeight.Text);
                    if (maxValue < Convert.ToInt32(objValue))
                    {
                        MessageBox.Show("管脚坐标超出了元器件尺寸");
                    }
                    //tmpDgv.CurrentCell = tmpDgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //tmpDgv.EditMode = DataGridViewEditMode.EditOnEnter;
                    //tmpDgv.BeginEdit(true);
                }
            }
        }

        private void dgvFoot_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!this.isFinishInitial) return;
            DataGridView tmpDgv = sender as DataGridView;
            tmpDgv.Rows[e.RowIndex].Cells[0].Value = tmpDgv.Rows.Count;
            tmpDgv.Rows[e.RowIndex].Cells[2].Value = "Input";
        }

        private void tbSize_TextChanged(object sender, EventArgs e)
        {
            if (tbWidth.Text == string.Empty || tbHeight.Text == string.Empty)
                return;
            if (StringValidator.IsUnsignedNumber((sender as TextBox).Text))
            {
                int width = int.Parse(tbWidth.Text);
                int height = int.Parse(tbHeight.Text);
                this.UpdateComponentSize(width, height);
            }
        }

        private void dgvFoot_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (!this.isFinishInitial) return;
            DataGridView tmpDgv = sender as DataGridView;
            for (int i = 0; i < tmpDgv.Rows.Count; i++)
            {
                tmpDgv.Rows[i].Cells[0].Value = i + 1;
            }

        }
    }
}