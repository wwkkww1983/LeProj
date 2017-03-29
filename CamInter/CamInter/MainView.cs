using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace CamInter
{
    public partial class MainView : Form
    {
        #region 实例化
        private bool initialConditionFlag = false, areaBoardSelected = true;
        private int resolutionSide, resolutionOther;
        private List<ValueType> cameList = null;
        private DBUtility dbHandler = new DBUtility(true);
        private Algorithm alg = null;

        public MainView()
        {
            InitializeComponent();

            dbHandler.InitialTable();
            this.InitialCamInter();
        }

        private void InitialCamInter()
        {
            DataTable dt = dbHandler.GetDropDownListInfo(enumProductType.Interface);
            this.cbCamInter.DataSource = dt;
            this.cbCamInter.DisplayMember = "Name";
            this.cbCamInter.ValueMember = "Idx";
            this.cbCamInter.SelectedIndex = -1;

            this.initialConditionFlag = true;
            List<ValueType> ringList = dbHandler.GetAllDevices(enumProductType.Focus);
            this.cameList = dbHandler.GetAllDevices(enumProductType.CamLens);
            this.alg = new Algorithm(ringList, this.cameList);
        }

        #endregion

        #region 回调
        private void HanderAfterAddItem(Form window, enumProductType type, bool isSuccess)
        {
            if (isSuccess)
            {
                MessageBox.Show("增加成功");
                window.Close();
            }
            else
            {
                MessageBox.Show("操作失败");
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
        #endregion

        #region 用户行为
        private void btnSelect_Click(object sender, EventArgs e)
        {

        }

        private void tbSensor_TextChanged(object sender, EventArgs e)
        {
            if (this.tbSensorSide.Text.Trim().Equals(string.Empty) || this.tbSensorOther.Text.Trim().Equals(string.Empty))
                return;
            float sensorSide = Convert.ToSingle(this.tbSensorSide.Text);
            float sensorOther = Convert.ToSingle(this.tbSensorOther.Text);
            this.tbTarget.Text = Math.Sqrt(sensorSide * sensorSide + sensorOther * sensorOther).ToString();
        }

        private void selectPatchItems(object sender, EventArgs e)
        {
            if (!this.PreCheckUserDataIsEnough()) return;
            float ratio = areaBoardSelected ? this.PreFilterUserAreaData() : this.PreFilterUserLineData();
            int camInter = Convert.ToInt32(this.cbCamInter.SelectedValue);
            float flange = Convert.ToSingle(this.tbFlange.Text);
            float target = Convert.ToSingle(this.tbTarget.Text);
            float workDistance = Convert.ToSingle(this.tbDistance.Text);
            float workDistanceRange = Convert.ToSingle(this.tbDistanRange.Text);

            List<RingResult> resultList = this.alg.GetDevicesByBaseInfo(camInter, flange, target, resolutionSide, resolutionOther, ratio, workDistance, workDistanceRange);
            
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
            float fovSide = Convert.ToSingle(this.tbFovSide.Text);
            float fovOther = Convert.ToSingle(this.tbFovOther.Text);
            if (!this.tbResolutionSide.Text.Trim().Equals(string.Empty))
            {
                resolutionSide = Convert.ToInt32(this.tbResolutionSide.Text);
                resolutionOther = Convert.ToInt32(this.tbResolutionOther.Text);
            }

            return Math.Min(sensorSide / fovSide, sensorOther / fovOther);
        }

        private float PreFilterUserLineData()
        {
            float sensor = Convert.ToSingle(this.tbLineSensor.Text);
            float fov = Convert.ToSingle(this.tbLineFov.Text);
            if (!this.tbLineResolution.Text.Trim().Equals(string.Empty))
            {
                int resolution = Convert.ToInt32(this.tbLineResolution.Text);
                if (this.rbLineLength.Checked)
                    resolutionSide = resolution;
                else resolutionOther = resolution;
            }

            return sensor / fov;
        }

        private CameraLens GetCamInfoById(int idx)
        {
            CameraLens cam=new CameraLens ();

            foreach (ValueType item in cameList)
            {
                if (((CameraLens)item).Idx == idx)
                {
                    cam = (CameraLens)item;
                    break;
                }
            }
            
            return cam;
        }
        #endregion


    }
}
