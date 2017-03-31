using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunisoft.IrisSkin;
using Core;

namespace CamInter
{
    public partial class MainView : Form
    {
        #region 初始化
        private bool initialConditionFlag = false, areaBoardSelected = true;
        private int resolutionLength, resolutionWidth;
        private float fovLengthValue, fovWidthValue;
        private const string PRE_PROJECT_NAME = "preProjectName", POST_RING_NUMBER = "postRingNumber", NAME_NONE = "nameNone",
                ADD_SUCCESS = "addSuccessInfo", ADD_FAIL = "addFailInfo";
        private DataTable dtInter = null;
        private DBUtility dbHandler = new DBUtility(true);
        private Algorithm alg = null;
        private SkinEngine skin = null;
        private List<RingResults> resultList = null;

        public MainView()
        {
            InitializeComponent();

            //dbHandler.InitialTable();
            this.InitialCamInter();
        }

        private void InitialCamInter()
        {
            this.dtInter = dbHandler.GetDropDownListInfo(enumProductType.Interface);
            this.cbCamInter.DataSource = this.dtInter;
            this.cbCamInter.DisplayMember = "Name";
            this.cbCamInter.ValueMember = "Idx";
            this.cbCamInter.SelectedIndex = 4;
            this.tcCamera.SelectedIndex = 1;

            this.initialConditionFlag = true;
            List<ValueType> ringList = dbHandler.GetAllDevices(enumProductType.Focus);
            List<ValueType> cameList = dbHandler.GetAllDevices(enumProductType.CamLens);
            this.alg = new Algorithm(ringList, cameList);
            this.dgvProjDetail.AutoGenerateColumns = false;
            this.dgvProjList.AutoGenerateColumns = false;

            this.skin = new SkinEngine(this);
            this.skin.SkinFile = "Wave.ssk";
        }

        #endregion

        #region 回调
        private void HanderAfterAddItem(Form window, enumProductType type, bool isSuccess)
        {
            if (isSuccess)
            {
                MessageBox.Show(this.GetConstantsString(ADD_SUCCESS));
                window.Close();
            }
            else
            {
                MessageBox.Show(this.GetConstantsString(ADD_FAIL));
            }
            if (type == enumProductType.Interface)
            {
                this.InitialCamInter();
            }
        }
        #endregion

        #region 菜单
        private void CamLensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewLens viewShow = new ViewLens(this.HanderAfterAddItem);
            viewShow.ShowDialog();
        }

        private void FocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(enumProductType.Focus, this.HanderAfterAddItem);
            viewShow.ShowDialog();
        }

        private void AdapterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(enumProductType.Adapter, this.HanderAfterAddItem);
            viewShow.ShowDialog();
        }

        private void ExtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MidRing viewShow = new MidRing(enumProductType.Extend, this.HanderAfterAddItem);
            viewShow.ShowDialog();
        }

        private void connToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewConnctor viewConn = new ViewConnctor(this.HanderAfterAddItem);
            viewConn.ShowDialog();
        }

        private void lanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lang = (sender as ToolStripMenuItem).Text;
            if (lang == "中文")
            {
                SetLanguage.SetLang("zh", this, typeof(MainView));
            }
            else
            {
                SetLanguage.SetLang("en", this, typeof(MainView));
            }
        }
        #endregion

        #region 用户行为
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (!this.PreCheckUserDataIsEnough()) return;
            float ratio = areaBoardSelected ? this.PreFilterUserAreaData() : this.PreFilterUserLineData();
            int camInter = Convert.ToInt32(this.cbCamInter.SelectedValue);
            float flange = Convert.ToSingle(this.tbFlange.Text);
            float target = this.tbTarget.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToSingle(this.tbTarget.Text);
            float workDistance = this.tbDistance.Text.Trim().Equals(string.Empty)?0:Convert.ToSingle(this.tbDistance.Text);
            float workDistanceRange = this.tbDistanRange.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToSingle(this.tbDistanRange.Text);

            this.resultList = this.alg.GetDevicesByBaseInfo(camInter, flange, target, resolutionLength, resolutionWidth, ratio, workDistance, workDistanceRange);
            this.showResults(this.resultList, ratio);
        }

        private void tbSensor_TextChanged(object sender, EventArgs e)
        {
            if (this.tbSensorSide.Text.Trim().Equals(string.Empty) || this.tbSensorOther.Text.Trim().Equals(string.Empty))
                return;
            float sensorSide = Convert.ToSingle(this.tbSensorSide.Text);
            float sensorOther = Convert.ToSingle(this.tbSensorOther.Text);
            this.tbTarget.Text = Math.Sqrt(sensorSide * sensorSide + sensorOther * sensorOther).ToString();
        }

        private void showResults(List<RingResults> result,float ratio)
        {
            string strNone = this.GetConstantsString(NAME_NONE);
            string strProj = this.GetConstantsString(PRE_PROJECT_NAME);
            string strNumber = this.GetConstantsString(POST_RING_NUMBER);

            DataTable dt = new DataTable();
             dt.Columns.Add(new DataColumn("Idx"));
             dt.Columns.Add(new DataColumn("lens"));
             dt.Columns.Add(new DataColumn("focus"));
             dt.Columns.Add(new DataColumn("adapter"));
             dt.Columns.Add(new DataColumn("extend"));
             dt.Columns.Add(new DataColumn("ratio"));
             dt.Columns.Add(new DataColumn("workDistance"));
             dt.Columns.Add(new DataColumn("fovLength"));
             dt.Columns.Add(new DataColumn("fovWidth"));
            foreach (RingResults ring in result)
            {
                RingMedium focus = ring.ringList.Find(item => item.RingType == enumProductType.Focus);
                List<RingMedium> adapter = ring.ringList.FindAll(item => item.RingType == enumProductType.Adapter);
                List<RingMedium> extend = ring.ringList.FindAll(item => item.RingType == enumProductType.Extend);
                DataRow dr = dt.NewRow();
                dr["Idx"] = strProj + ring.Idx;
                dr["lens"] = ring.Lens.Name;
                dr["focus"] = focus.Name == string.Empty ? strNone : focus.Name;
                dr["adapter"] = adapter.Count == 1 ? adapter[0].Name : adapter.Count.ToString() + strNumber;
                dr["extend"] = extend.Count == 1 ? extend[0].Name : extend.Count.ToString() + strNumber; ;
                dr["ratio"] = ratio;
                dr["workDistance"] = ring.Lens.Focus * (2 + ratio + 1/ratio) + ring.Lens.HH - ring.Lens.Length;
                dr["fovLength"] = this.fovLengthValue;
                dr["fovWidth"] = this.fovWidthValue;
                dt.Rows.Add(dr);
            }
            this.dgvProjList.DataSource = dt;
        }

        private string GetRingName(enumProductType type)
        {
            return SetLanguage.GetStringByFormKey(typeof(MidRing), type.ToString());
        }

        private string GetConstantsString(string key)
        {
            return SetLanguage.GetStringByFormKey(typeof(MainView), key);
        }

        private void dgvProjList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string strNone = this.GetConstantsString(NAME_NONE);
            string strProj = this.GetConstantsString(PRE_PROJECT_NAME);

            if(e.RowIndex <= 0) return;
            int projIdx = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString().Substring(strProj.Length));
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("detailIdx"));
            dt.Columns.Add(new DataColumn("type"));
            dt.Columns.Add(new DataColumn("name"));
            dt.Columns.Add(new DataColumn("number"));
            dt.Columns.Add(new DataColumn("interA"));
            dt.Columns.Add(new DataColumn("interB"));
            dt.Columns.Add(new DataColumn("length"));

            RingResults ring = this.resultList.Find(item => item.Idx == projIdx);
            int idx=1;
            DataRow dr = dt.NewRow();
            dr["detailIdx"] = idx;
            dr["type"] = this.GetRingName(enumProductType.Camera);
            dr["name"] = strNone;// NAME_NONE;
            dr["number"] = strNone;//NAME_NONE;
            dr["interA"] = this.GetInterNameById(Convert.ToInt32(this.cbCamInter.SelectedValue));
            dr["interB"] = strNone;//NAME_NONE;
            dr["length"] = this.tbFlange.Text;
            dt.Rows.Add(dr); 

            foreach (RingMedium item in ring.ringList)
            {
                dr = dt.NewRow();
                dr["detailIdx"] = idx++;
                dr["type"] = this.GetRingName(item.RingType);// Constants.GetNameByType(item.RingType);
                dr["name"] = item.Name;
                dr["number"] = item.Number;
                dr["interA"] = this.GetInterNameById(item.InterDown);
                dr["interB"] = this.GetInterNameById(item.InterUp);
                dr["length"] = item.LengthMin == item.LengthMax ? item.Length.ToString () : string.Format("[{0},{1}]", item.LengthMin, item.LengthMax);
                dt.Rows.Add(dr);
            }

            dr = dt.NewRow();
            dr["detailIdx"] = idx++;
            dr["type"] = this.GetRingName(enumProductType.CamLens);
            dr["name"] = ring.Lens.Name;
            dr["number"] = ring.Lens.Number;
            dr["interA"] = strNone;//NAME_NONE;
            dr["interB"] = this.GetInterNameById(ring.Lens.Connector);
            dr["length"] = ring.Lens.Flange;
            dt.Rows.Add(dr);

            this.dgvProjDetail.DataSource = dt;
        }

        private bool PreCheckUserDataIsEnough()
        {
            areaBoardSelected = this.tcCamera.SelectedIndex == 0;
            return initialConditionFlag &&
                   (areaBoardSelected &&
                        StringValidator.HasContent(this.tbSensorSide, this.lbSensor.Text) &&
                        StringValidator.HasContent(this.tbSensorOther, this.lbSensor.Text) &&
                        StringValidator.HasContent(this.tbFovSide, this.lbFov.Text) &&
                        StringValidator.HasContent(this.tbFovOther, this.lbFov.Text) &&
                        StringValidator.IsUnsignedRealNumber(this.tbSensorSide) &&
                        StringValidator.IsUnsignedRealNumber(this.tbSensorOther) &&
                        StringValidator.IsUnsignedRealNumber(this.tbFovSide) &&
                        StringValidator.IsUnsignedRealNumber(this.tbFovOther) &&
                        StringValidator.IsEmptyOrUnsignedRealNumber(this.tbResolutionSide) &&
                        StringValidator.IsEmptyOrUnsignedRealNumber(this.tbResolutionOther)
                    ||
                        !areaBoardSelected &&
                        StringValidator.HasContent(this.tbLineSensor, this.lbLineSensor.Text) &&
                        StringValidator.HasContent(this.tbLineFov, this.lbLineFov.Text) &&
                        StringValidator.IsUnsignedRealNumber(this.tbLineSensor) &&
                        StringValidator.IsUnsignedRealNumber(this.tbLineFov) &&
                        StringValidator.IsEmptyOrUnsignedRealNumber(this.tbLineResolution)
                    ) &&
                    StringValidator.HasContent(this.cbCamInter, this.lbCamInter.Text) &&
                    StringValidator.HasContent(this.tbFlange, this.lbFlange.Text) &&
                    StringValidator.IsUnsignedRealNumber(this.tbFlange) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbTarget) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbDistance) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbDistanRange);
        }

        private float PreFilterUserAreaData()
        {
            float sensorSide = Convert.ToSingle(this.tbSensorSide.Text);
            float sensorOther = Convert.ToSingle(this.tbSensorOther.Text);
            this.fovLengthValue = Convert.ToSingle(this.tbFovSide.Text);
            this.fovWidthValue = Convert.ToSingle(this.tbFovOther.Text);
            if (!this.tbResolutionSide.Text.Trim().Equals(string.Empty))
            {
                this.resolutionLength = Convert.ToInt32(this.tbResolutionSide.Text);
                this.resolutionWidth = Convert.ToInt32(this.tbResolutionOther.Text);
            }

            return Math.Min(sensorSide / this.fovLengthValue, sensorOther / this.fovWidthValue);
        }

        private float PreFilterUserLineData()
        {
            float sensor = Convert.ToSingle(this.tbLineSensor.Text);
            float fov = Convert.ToSingle(this.tbLineFov.Text);
            if (!this.tbLineResolution.Text.Trim().Equals(string.Empty))
            {
                int resolution = Convert.ToInt32(this.tbLineResolution.Text);
                if (this.rbLineLength.Checked)
                    this.resolutionLength = resolution;
                else this.resolutionWidth = resolution;
            }
            if (this.rbLineLength.Checked)
                this.fovLengthValue = fov;
            else
                this.fovWidthValue = fov;
            
            return sensor / fov;
        }

        private string GetInterNameById(int idx)
        {
            string name = string.Empty;
            foreach (DataRow dr in dtInter.Rows)
            {
                if (Convert.ToInt32(dr["Idx"]) == idx)
                {
                    name = dr["Name"].ToString();
                    break;
                }
            }
            return name;
        }
        #endregion
    }
}
