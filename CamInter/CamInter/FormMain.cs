using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Core;

namespace CamInter
{
    public partial class FormMain : Form
    {
        #region 初始化
        private delegate void DelegateNoneParam();

        private const string NAME_NONE = "none", PRE_PROJECT_NAME = "NO.", POST_RING_NUMBER = " total";
        private bool leftButtonPress = false, isMeshCamera = false;
        private int searchCount = 0;
        private Point mouseMoveLocation;
        private Color colorBorder = Color.FromArgb(100, 100, 100), colorBackground = Color.FromArgb(27, 30, 35);
        private DataTable dtInter = null;
        private DBUtility dbHandler = new DBUtility(true);
        private Algorithm alg = null;
        private List<RingResults> resultList = null;
        private List<ValueType> connList;

        public FormMain()
        {
            InitializeComponent();

            //dbHandler.InitialTable();
            this.Initial();
        }

        private void Initial()
        {
            this.CheckFinishedApplication();
            this.TextBoxRemind();
            this.dtInter = dbHandler.GetDropDownListInfo(enumProductType.Interface);
            List<ValueType> ringList = dbHandler.GetAllDevices(enumProductType.Focus);
            List<ValueType> cameList = dbHandler.GetAllDevices(enumProductType.CamLens);
            this.connList = dbHandler.GetAllDevices(enumProductType.Interface);
            this.alg = new Algorithm(ringList, cameList, this.connList);
            this.cbInterface.DataSource = this.dtInter;
            this.cbInterface.DisplayMember = "Name";
            this.cbInterface.ValueMember = "Idx";

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
            this.lbFoot.ForeColor = this.colorBorder;
            this.pnDraw.BackColor = this.colorBorder;
            this.dgvDetail.BackgroundColor = this.colorBorder;
            this.dgvProj.BackgroundColor = this.colorBorder;

            this.InitialSize();
        }

        private void InitialSize()
        {
            #region 整体框架
            this.Size = new Size(1200, 800);
            this.pnSplit1.Size = new Size(1100,3);
            this.pnSplit1.Location = new Point(50, 80);
            this.pnSplit2.Size = new Size(850, 3);
            this.pnSplit2.Location = new Point(50, 230);
            this.pnSplit3.Size = new Size(850, 3);
            this.pnSplit3.Location = new Point(50, 450);
            this.pnSplit4.Size = new Size(1100, 3);
            this.pnSplit4.Location = new Point(50, 720);
            this.pnSplit5.Size = new Size(3,640);
            this.pnSplit5.Location = new Point(900, 80);
            this.pnSplit6.Size = new Size(3,150);
            this.pnSplit6.Location = new Point(475, 80);
            #endregion

            #region 标签位置
            this.lbHead.Location = new Point(441, 23);
            this.lbCamInfo.Location = new Point(50, 90);
            this.lbCamInfoChild.Location = new Point(202, 93);
            this.lbRequirement.Location = new Point(485,90);
            this.lbSuggestion.Location = new Point(50,240);
            this.lbDetail.Location = new Point(50,460);
            this.lbDimensional.Location = new Point(915, 90);
            this.lbFoot.Location = new Point(50,750);

            this.lbResolution.Location = new Point(98, 118);
            this.lbSize.Location = new Point(135, 138);
            this.lbFormat.Location = new Point(146, 158);
            this.lbInterface.Location = new Point(138, 179);
            this.lbFlange.Location = new Point(96, 201);
            this.lbResolutionHV.Location = new Point(302,119);
            this.lbSizeUnit.Location = new Point(282, 138);
            this.lbFormatUnit.Location = new Point(282, 158);
            this.lbInterfaceUnit.Location = new Point(282, 179);
            this.lbFlangeUnit.Location = new Point(282, 201);

            this.lbFov.Location = new Point(559,120);
            this.lbMagni.Location = new Point(504,140);
            this.lbWD.Location = new Point(559, 160);
            this.lbFovHUnit.Location = new Point(704,118);
            this.lbFovHV.Location = new Point(735,119);
            this.lbFovVUnit.Location = new Point(844,118);
            this.lbWDUnit.Location = new Point(704,158);
            this.lbWDRange.Location = new Point(735,159);
            this.lbWDRangeUnit.Location = new Point(844,158);
            #endregion

            #region 输入框
            this.tbResolutionH.AutoSize = false;
            this.tbResolutionH.Location = new Point(200, 120);
            this.tbResolutionH.Size = new Size (80,15);
            this.tbResolutionV.AutoSize = false;
            this.tbResolutionV.Location = new Point(340, 120);
            this.tbResolutionV.Size = new Size(80, 15);
            this.tbSize.AutoSize = false;
            this.tbSize.Location = new Point(200, 140);
            this.tbSize.Size = new Size(80, 15);
            this.tbFormat.AutoSize = false;
            this.tbFormat.Location = new Point(200, 160);
            this.tbFormat.Size = new Size(80, 15);
            this.cbInterface.Location = new Point(200, 180);
            this.cbInterface.Size = new Size(80, 15);
            
            this.tbFlange.AutoSize = false;
            this.tbFlange.Location = new Point(200, 202);
            this.tbFlange.Size = new Size(80, 15);

            this.tbFovH.AutoSize = false;
            this.tbFovH.Location = new Point(620, 120);
            this.tbFovH.Size = new Size(80, 15);
            this.tbFovV.AutoSize = false;
            this.tbFovV.Location = new Point(760, 120);
            this.tbFovV.Size = new Size(80, 15);
            this.tbMagnifi.AutoSize = false;
            this.tbMagnifi.Location = new Point(620, 140);
            this.tbMagnifi.Size = new Size(80, 15);
            this.tbWD.AutoSize = false;
            this.tbWD.Location = new Point(620, 160);
            this.tbWD.Size = new Size(80, 15);
            this.tbWDrange.AutoSize = false;
            this.tbWDrange.Location = new Point(760, 160);
            this.tbWDrange.Size = new Size(80, 15);
            #endregion

            #region 点击按钮
            this.btnSearch.BackColor = Color.FromArgb(0x7f, 0xc0, 0x41);
            this.btnSearch.Size = new Size(100,25);
            this.btnSearch.Location = new Point(547, 190);
            this.btnStop.BackColor = Color.FromArgb(0xc0, 0x41, 0x41);
            this.btnStop.Size = new Size(100, 25);
            this.btnStop.Location = new Point(726,190);
            this.btnMin.Location = new Point(1148,12);
            this.btnMin.Size = new Size(18,18);
            this.btnClose.Location = new Point(1171, 12);
            this.btnClose.Size = new Size(18, 18);
            #endregion

            #region 结果列表
            this.dgvProj.Location = new Point(75, 273);
            this.dgvProj.Size = new Size(800,157);
            this.dgvDetail.Location = new Point(75,493);
            this.dgvDetail.Size = new Size(800, 207);
            this.pnDraw.Size = new Size(200,580);
            this.pnDraw.Location = new Point(925,120);
            #endregion
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
        #endregion

        #region 界面交互
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!this.btnSearch.Enabled)
            {
                MessageBox.Show("Searching now, please wait.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.btnSearch.Enabled)
            {
                MessageBox.Show("Searching now, please wait.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void CalcRingByChildThread(object obj)
        {
            float[] objFloat = obj as float[];  
            int camInter=(int)objFloat[1];
            float flange = objFloat[2], target = objFloat[3], ratio = objFloat[0], workLength = objFloat[4], workRange = objFloat[5];

            this.resultList = this.alg.GetDevicesByBaseInfo(camInter, flange, target, ratio, workLength, workRange);
            this.showResults(this.resultList, ratio);
        }

        private void clearDataGridView()
        {
            DataTable dt = (DataTable)this.dgvProj.DataSource;
            if (dt != null)
            {
                dt.Rows.Clear();
                this.dgvProj.DataSource = dt;
            }

            dt = (DataTable)this.dgvDetail.DataSource;
            if (dt != null)
            {
                dt.Rows.Clear();
                this.dgvDetail.DataSource = dt;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.PreCheckUserDataIsEnough()) return;

            Button btnTemp = sender as Button;
            btnTemp.Enabled = false;
            this.timerSearch.Enabled = true;

            this.clearDataGridView();
            this.pnDraw.Controls.Clear();

            float ratio = float.Parse(this.tbMagnifi.Text);
            int camInter = Convert.ToInt32(this.cbInterface.SelectedValue);
            float flange = Convert.ToSingle(this.tbFlange.Text);
            float target = float.Parse(this.tbFormat.Text);
            float workDistance = this.tbWD.Text.Trim().Equals(string.Empty) ? 0f : float.Parse(this.tbWD.Text);
            float workDistanceRange = this.tbWDrange.Text.Trim().Equals(string.Empty) ? 0f : float.Parse(this.tbWDrange.Text);

            float[] threadParams = new float[] { ratio,camInter,flange,target,workDistance,workDistanceRange };
            Thread threadCalc = new Thread(new ParameterizedThreadStart(this.CalcRingByChildThread));
            threadCalc.Start(threadParams);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.alg.NormalSearchFlag = false;
        }

        private void timerSearch_Tick(object sender, EventArgs e)
        {
            string strSearch = "search ";
            for (int i = 0; i < this.searchCount; i++)
            {
                strSearch += ".";
            }
            this.btnSearch.Text = strSearch;

            this.searchCount = (this.searchCount + 1) % 4;
        }

        private bool PreCheckUserDataIsEnough()
        {
            return StringValidator.HasContent(this.tbResolutionH, this.lbResolution.Text) &&
                        StringValidator.HasContent(this.tbResolutionV, this.lbResolution.Text) &&
                        StringValidator.HasContent(this.tbFovH, this.lbFov.Text) &&
                        StringValidator.HasContent(this.tbFovV, this.lbFov.Text) &&
                        StringValidator.HasContent(this.tbFlange, this.lbFlange.Text) &&
                        StringValidator.HasContent(this.tbMagnifi, this.lbMagni.Text) &&
                        StringValidator.HasContent(this.tbFormat, this.lbFormat.Text) &&

                        StringValidator.IsUnsignedRealNumber(this.tbResolutionH) &&
                        StringValidator.IsUnsignedRealNumber(this.tbResolutionV) &&
                        StringValidator.IsUnsignedRealNumber(this.tbFovH) &&
                        StringValidator.IsUnsignedRealNumber(this.tbFovV) &&
                        StringValidator.IsUnsignedRealNumber(this.tbFlange) &&
                        StringValidator.IsUnsignedRealNumber(this.tbFormat) &&
                        StringValidator.IsUnsignedRealNumber(this.tbMagnifi);
        }

        private void showResults(List<RingResults> result, float ratio)
        {
            if (this.InvokeRequired)
            {
                Action<List<RingResults>, float> delegateChangeCursor = new Action<List<RingResults>, float>(showResults);
                this.Invoke(delegateChangeCursor, new object[] { result, ratio });
                return;
            }

            this.searchCount = 0;
            this.timerSearch.Enabled = false;
            this.btnSearch.Text = "search";
            this.btnSearch.Enabled = true;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("item"));
            dt.Columns.Add(new DataColumn("lens"));
            dt.Columns.Add(new DataColumn("focus"));
            dt.Columns.Add(new DataColumn("adapter"));
            dt.Columns.Add(new DataColumn("ext"));
            dt.Columns.Add(new DataColumn("wd"));
            dt.Columns.Add(new DataColumn("fov"));
            foreach (RingResults ring in result)
            {
                RingMedium focus = ring.ringList.Find(item => item.RingType == enumProductType.Focus);
                List<RingMedium> adapter = ring.ringList.FindAll(item => item.RingType == enumProductType.Adapter);
                List<RingMedium> extend = ring.ringList.FindAll(item => item.RingType == enumProductType.Extend);
                DataRow dr = dt.NewRow();
                dr["item"] = PRE_PROJECT_NAME + ring.Idx;
                dr["lens"] = ring.Lens.Name;
                dr["focus"] = focus.Name == string.Empty ? NAME_NONE : focus.Name;
                dr["adapter"] = adapter.Count == 1 ? adapter[0].Name : adapter.Count.ToString() + POST_RING_NUMBER;
                dr["ext"] = extend.Count == 1 ? extend[0].Name : extend.Count.ToString() + POST_RING_NUMBER; ;
                dr["wd"] = ring.Lens.Focus * (2 + ratio + 1 / ratio) + ring.Lens.HH - ring.Lens.Length - ring.Lens.Focus * ratio - ring.Lens.Flange;
                dr["fov"] = this.isMeshCamera ? this.tbFovH.Text+"×"+this.tbFovV.Text : this.tbFovH.Text;
                dt.Rows.Add(dr);
            }
            this.dgvProj.DataSource = dt;
            this.alg.NormalSearchFlag = true;

            if (this.resultList.Count <= 2)
            {
                MessageBox.Show("If you want more solutions, please extend the working range.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.leftButtonPress = true;
                this.mouseMoveLocation = new Point(-e.X, -e.Y);
            }
        }

        private void cbInterface_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font myFont = new Font("Arial", 12, GraphicsUnit.Pixel);
            string strContent = this.dtInter.Rows[e.Index]["Name"].ToString();

            e.Graphics.DrawString(strContent, myFont, Brushes.Black, new RectangleF(0, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
        }

        private void cbInterface_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 15;
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

        private void cbInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbTemp = sender as ComboBox;
            if (cbTemp.Text == "C" || cbTemp.Text == "F" || cbTemp.Text == "CS")
            {
                this.tbFlange.Enabled = false;
                //C口17.526mm，CS口12.526mm，F口46.5mm
                this.tbFlange.Text = cbTemp.Text == "C" ? "17.526" : (cbTemp.Text == "F" ? "46.5" : "12.526");
            }
            else
            {
                this.tbFlange.Enabled = true;
                this.tbFlange.Text = string.Empty;
            }

            if (this.tbFormat.Text.Trim().Equals(string.Empty)) return;
            float camTarget = float.Parse(this.tbFormat.Text);
            float interSize = 0;
            int connIdx = Convert.ToInt32( this.dtInter.Rows[cbTemp.SelectedIndex]["Idx"]);
            foreach (ValueType item in this.connList)
            {
                Connectors connItem = (Connectors)item;
                if (connItem.Idx == connIdx)
                {
                    interSize = connItem.Length;
                    break;
                }
            }
            if (interSize <= camTarget)
            {
                MessageBox.Show("interface size is larger than camera format");
            }
        }

        private void tbResolutionV_Leave(object sender, EventArgs e)
        {
            TextBox tbTemp = sender as TextBox;
            this.isMeshCamera = !tbTemp.Text.Trim().Equals("1");
            this.CalcFormat();
        }

        private void tbFov_TextChanged(object sender, EventArgs e)
        {
            if (this.tbResolutionH.Text.Equals(string.Empty) || this.tbResolutionV.Text.Equals(string.Empty)) return;
            TextBox tbTemp = sender as TextBox;
            if (!tbTemp.Enabled) return;

            TextBox other = tbTemp == this.tbFovH ? this.tbFovV : this.tbFovH;
            other.Enabled = tbTemp.Text.Trim().Equals(string.Empty);
            if (other.Enabled)
            {
                other.Text = string.Empty;
                this.tbMagnifi.Enabled = true;
                this.tbMagnifi.Text = string.Empty;
                return;
            }

            double sizeH, sizeV, fovH, fovV;
            sizeH = double.Parse(this.tbResolutionH.Text);
            sizeV = double.Parse(this.tbResolutionV.Text);
            fovH = double.Parse(tbTemp.Text);
            fovV = fovH * sizeV / sizeH;
            if (tbTemp == this.tbFovV)
            {
                fovV = double.Parse(tbTemp.Text);
                fovH = fovV * sizeH / sizeV;
            }

            this.tbMagnifi.Enabled = false;
            double fov = double.Parse(tbTemp.Text);
            if (!this.isMeshCamera)
            {
                other.Text = "1";
                double target = double.Parse(this.tbFormat.Text);
                this.tbMagnifi.Text = (target / fov).ToString("f4") ;
            }
            else
            {
                double otherValue, resolution;
                if (tbTemp == this.tbFovH)
                {
                    otherValue = fovV;
                    resolution = sizeH;
                }
                else
                {
                    otherValue = fovH;
                    resolution = sizeV;
                }
                double pixel = double.Parse(this.tbSize.Text);
                this.tbMagnifi.Text = (resolution * pixel / fov / 1000f).ToString("f4");
                other.Text = otherValue.ToString("f4");
            }
        }

        private void tbMagnifi_TextChanged(object sender, EventArgs e)
        {
            if (this.tbResolutionH.Text.Equals(string.Empty) ||
                this.tbResolutionV.Text.Equals(string.Empty) ||
                !this.tbMagnifi.Enabled) return;

            TextBox tbTemp = sender as TextBox;
            bool InputNONE = tbTemp.Text.Trim().Equals(string.Empty);
            this.tbFovH.Enabled = InputNONE;
            this.tbFovV.Enabled = InputNONE;
            if (InputNONE) return;

            double sizeH = double.Parse(this.tbResolutionH.Text);
            double sizeV = double.Parse(this.tbResolutionV.Text);
            double pixel = double.Parse(this.tbSize.Text);
            double magni = double.Parse(tbTemp.Text);
            double fovH = sizeH * pixel /1000f/ magni;
            double fovV = sizeV * pixel / 1000f / magni;
            this.tbFovH.Text = fovH.ToString();
            this.tbFovV.Text = fovV.ToString("f4");
        }

        private void tbSize_TextChanged(object sender, EventArgs e)
        {
            this.CalcFormat();
        }

        private void tbResolutionH_TextChanged(object sender, EventArgs e)
        {
            this.CalcFormat();
        }

        private void CalcFormat()
        {
            if (this.tbSize.Text.Equals(string.Empty)) return;

            float pixel = float.Parse(this.tbSize.Text);
            if (!this.isMeshCamera &&
                !this.tbResolutionH.Text.Trim().Equals(string.Empty) &&
                !this.tbSize.Text.Trim().Equals(string.Empty))
            {
                float resolution = float.Parse(this.tbResolutionH.Text);
                this.tbFormat.Text = (resolution * pixel / 1000f).ToString("f4");
            }
            else if (this.isMeshCamera &&
               !this.tbResolutionH.Text.Trim().Equals(string.Empty) &&
               !this.tbResolutionV.Text.Trim().Equals(string.Empty))
            {
                int h = int.Parse(this.tbResolutionH.Text);
                int v = int.Parse(this.tbResolutionV.Text);
                this.tbFormat.Text = (Math.Sqrt(h * h + v * v) * pixel / 1000f).ToString("f4");
            }

            this.tbFov_TextChanged(this.tbFovH, null);
            this.tbMagnifi_TextChanged(this.tbMagnifi, null);
        }

        private void dgvProj_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int projIdx = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString().Substring(PRE_PROJECT_NAME.Length));
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("item"));
            dt.Columns.Add(new DataColumn("model"));
            dt.Columns.Add(new DataColumn("name"));
            dt.Columns.Add(new DataColumn("code"));
            dt.Columns.Add(new DataColumn("mountA"));
            dt.Columns.Add(new DataColumn("mountB"));
            dt.Columns.Add(new DataColumn("length"));

            RingResults ring = this.resultList.Find(item => item.Idx == projIdx);
            int idx = 1;
            DataRow dr = dt.NewRow();
            dr["item"] = idx++;
            dr["model"] = "camera";
            dr["name"] = NAME_NONE;// NAME_NONE;
            dr["code"] = NAME_NONE;//NAME_NONE;
            dr["mountA"] = this.GetInterNameById(Convert.ToInt32(this.cbInterface.SelectedValue));
            dr["mountB"] = NAME_NONE;//NAME_NONE;
            dr["length"] = this.tbFlange.Text;
            dt.Rows.Add(dr);

            int imgIdx = 0;
            foreach (RingMedium item in ring.ringList)
            {
                dr = dt.NewRow();
                dr["item"] = idx++;
                dr["model"] = item.RingType.ToString();// Constants.GetNameByType(item.RingType);
                dr["name"] = item.Name;
                dr["code"] = item.Number;
                dr["mountA"] = this.GetInterNameById(item.InterDown);
                dr["mountB"] = this.GetInterNameById(item.InterUp);
                dr["length"] = item.LengthMin == item.LengthMax ? item.Length.ToString() : string.Format("[{0},{1}]", item.LengthMin, item.LengthMax);

                dt.Rows.Add(dr);
                this.AddImgShow(item.ImgName, ref imgIdx);
            }

            dr = dt.NewRow();
            dr["item"] = idx++;
            dr["model"] = "lens";
            dr["name"] = ring.Lens.Name;
            dr["code"] = ring.Lens.Number;
            dr["mountA"] = NAME_NONE;//NAME_NONE;
            dr["mountB"] = this.GetInterNameById(ring.Lens.Connector);
            dr["length"] = ring.Lens.Flange;
            dt.Rows.Add(dr);
            this.AddImgShow(ring.Lens.ImgName, ref imgIdx);

            this.dgvDetail.DataSource = dt;
        }

        private void AddImgShow(string imgName,ref int idx)
        {
            if (imgName == string.Empty) return;

            string fullName = "img/" + imgName;
            if(!System.IO.File.Exists(fullName)) return;

            PictureBox pbImg = new PictureBox();
            pbImg.Image = Image.FromFile(fullName);
            pbImg.Size = new Size(150,150);
            pbImg.Location = new Point(25, 160 * idx + 10);

            this.pnDraw.Controls.Add(pbImg);
            idx++;
        }

        private string GetInterNameById(int idx)
        {
            string name = string.Empty;
            foreach (ValueType item in this.connList)
            {
                Connectors connItem = (Connectors)item;
                if (connItem.Idx == idx)
                {
                    name = connItem.Name;
                    break;
                }
            }
            return name;
        }
        #endregion
    }
}
