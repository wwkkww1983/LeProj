using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CamInter
{
    public partial class FormMain : Form
    {
        private bool leftButtonPress = false;
        private Point mouseMoveLocation;
        private Color colorBorder = Color.FromArgb(100, 100, 100), colorBackground = Color.FromArgb(27, 30, 35);
        public FormMain()
        {
            InitializeComponent();

            this.Initial();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("item"));
            dt.Columns.Add(new DataColumn("lens"));
            dt.Columns.Add(new DataColumn("focus"));
            dt.Columns.Add(new DataColumn("adapter"));
            dt.Columns.Add(new DataColumn("ext"));
            dt.Columns.Add(new DataColumn("wd"));
            dt.Columns.Add(new DataColumn("fov"));
            DataRow dr = dt.NewRow();
            dr["item"] = "1";
            dr["lens"] = "Makro-Symmar 5.6/120-005";
            dr["focus"] = "Smart Focus 23";
            dr["adapter"] = "V38 to M42";
            dr["ext"] = "Ext.Tube M90 x 1.0 ";
            dr["wd"] = "500";
            dr["fov"] = "25";
            dt.Rows.Add(dr);
            this.dgvProj.DataSource = dt;



            DataTable dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("item"));
            dt1.Columns.Add(new DataColumn("model"));
            dt1.Columns.Add(new DataColumn("name"));
            dt1.Columns.Add(new DataColumn("code"));
            dt1.Columns.Add(new DataColumn("mountA"));
            dt1.Columns.Add(new DataColumn("mountB"));
            dt1.Columns.Add(new DataColumn("length"));
            DataRow dr1 = dt1.NewRow();
            dr1["item"] = "1";
            dr1["model"] = "lens";
            dr1["name"] = "Makro-Symmar 5.6/120-005";
            dr1["code"] = "1002647";
            dr1["mountA"] = "V38";
            dr1["mountB"] = "none";
            dr1["length"] = "283";
            dt1.Rows.Add(dr1);
            this.dgvDetail.DataSource = dt1;
        }

        private void Initial()
        {
            this.CheckFinishedApplication();
            this.TextBoxRemind();

            this.BackColor = this.colorBackground;            
            this.dgvProj.ColumnHeadersDefaultCellStyle.BackColor = this.colorBackground;
            this.dgvProj.DefaultCellStyle.BackColor = this.colorBorder;
            this.dgvDetail.ColumnHeadersDefaultCellStyle.BackColor = this.colorBackground;
            this.dgvDetail.DefaultCellStyle.BackColor = this.colorBorder;
            

            this.pnSplit1.BackColor = this.colorBorder;
            this.pnSplit2.BackColor = this.colorBorder;
            this.pnSplit3.BackColor = this.colorBorder;
            this.pnSplit4.BackColor = this.colorBorder;
            this.pnSplit5.BackColor = this.colorBorder;
            this.pnSplit6.BackColor = this.colorBorder;
            this.lbFoot.ForeColor= this.colorBorder;
            this.pnDraw.BackColor = this.colorBorder;
            this.dgvDetail.BackgroundColor = this.colorBorder;
            this.dgvProj.BackgroundColor = this.colorBorder;

            this.tbResolutionH.AutoSize = false;
            this.tbResolutionH.Height = 15;
            this.tbResolutionV.AutoSize = false;
            this.tbResolutionV.Height = 15;
            this.tbSize.AutoSize = false;
            this.tbSize.Height = 15;
            this.tbInterface.AutoSize = false;
            this.tbInterface.Height = 15;
            this.tbFormat.AutoSize = false;
            this.tbFormat.Height = 15;
            this.tbFlange.AutoSize = false;
            this.tbFlange.Height = 15;
            this.tbFovH.AutoSize = false;
            this.tbFovH.Height = 15;
            this.tbFovV.AutoSize = false;
            this.tbFovV.Height = 15;
            this.tbMagnifi.AutoSize = false;
            this.tbMagnifi.Height = 15;
            this.tbWD.AutoSize = false;
            this.tbWD.Height = 15;
            this.tbWDrange.AutoSize = false;
            this.tbWDrange.Height = 15;

            this.tbResolutionH.GotFocus += this.tbResolutionH_MouseEnter;
            this.tbResolutionH.LostFocus += this.tbResolutionH_MouseLeave;
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            
            Rectangle myRectangle = new Rectangle(0, 0, this.Width, this.Height);
            ControlPaint.DrawBorder(e.Graphics, myRectangle,
                colorBorder, 6, ButtonBorderStyle.Solid,
                colorBorder, 6, ButtonBorderStyle.Solid,
                colorBorder, 6, ButtonBorderStyle.Solid,
                colorBorder, 6, ButtonBorderStyle.Solid
            );  
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.leftButtonPress = true;
                this.mouseMoveLocation = new Point(-e.X, -e.Y);
            }
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (!leftButtonPress) return;
            Point locationTemp = Control.MousePosition;
            locationTemp.Offset(this.mouseMoveLocation);
            this.Location = locationTemp;

        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            this.leftButtonPress = false;
        }

        private void CheckFinishedApplication()
        {
            Process[] pa = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);//获取当前进程数组。
            if (pa.Length > 1)
            {
                MessageBox.Show(Process.GetCurrentProcess().ProcessName + "The application is already running", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
            }
        }

        private void TextBoxRemind()
        {
            AutoCompleteStringCollection strRemind = new AutoCompleteStringCollection();
            strRemind.Add("");
            this.tbResolutionH.AutoCompleteCustomSource = strRemind;
            this.tbResolutionH.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.tbResolutionH.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        private void tbResolutionH_MouseEnter(object sender, EventArgs e)
        {

        }

        private void tbResolutionH_MouseLeave(object sender, EventArgs e)
        {
            this.lbRemind.Visible = false;
        }

        private void tbResolution_MouseEnter(object sender, EventArgs e)
        {
            TextBox item = sender as TextBox;
            Point loc = item.Location;
            loc.Y += item.Height;
            this.lbRemind.Text = "if it is a line scan camera, Please input  number 1";
            this.lbRemind.Location = loc;
            this.lbRemind.Visible = true;
        }

        private void tbResolution_MouseLeave(object sender, EventArgs e)
        {
            this.lbRemind.Visible = false;
        }

    }
}


//1.在“Camera Information ”模块，当鼠标滑到“Resolution”的输入框时，弹出提示“if it is a line scan camera, Please input  number 1”。
//2.在“Camera Information ”模块，“Interface”时，用户选择C\CS\F时，“Flange distance”输入框自动显示相应数值，并变成灰色不可填；用户选择非C\CS\F时，“Flange distance”输入框留白，可填；
//3.在”Requirement”模块，FOV(H*V)时，H和V输入框只能选填一个，当为面阵相机时，另一个输入框根据已输入的数据自动算出并变成灰色；当为线阵相机时，直接为1并变成灰色。
//4.在”Requirement”模块，当用户在“FOV”输入框输入数据后，”Magnification”输入框自动计算出数值并变成灰色；当用户在”Magnification”输入框输入数据后，“FOV”输入框自动计算出数值并变成灰色；
//5.在”Requirement”模块，当查找按钮按下后，“search”变成“searching...”，后面的三个点会闪现，让使用者知道软件正在运行。
//6.点击“search”后，若没有结果，或结果很少，给出提示“If you want more solutions, please extend the working range.”
//7.目前发现软件可以同时运行（打开一个，还可以继续打开），请修正为：只能单任务运行，当用户尝试多任务运行时，给出提示：”The application is already running”.

