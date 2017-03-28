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
        private bool initialConditionFlag = false;
        private List<ValueType> cameList = null;
        private DBUtility dbHandler = new DBUtility(true);
        private Algorithm alg = null;

        public MainView()
        {
            InitializeComponent();

            //dbHandler.InitialTable();
            this.InitialCamInter();
        }

        private void InitialCamInter()
        {
            DataTable dtCam = dbHandler.GetDropDownListInfo(enumProductType.CamLens);
            this.cbCamInter.DataSource = dtCam;
            this.cbCamInter.DisplayMember = "Name";
            this.cbCamInter.ValueMember = "Idx";
            this.cbCamInter.SelectedIndex = -1;

            DataTable dtLens = dbHandler.GetDropDownListInfo(enumProductType.Interface);
            this.cbLensInter.DataSource = dtLens;
            this.cbLensInter.DisplayMember = "Name";
            this.cbLensInter.ValueMember = "Idx";
            this.cbLensInter.SelectedIndex = -1;

            this.initialConditionFlag = true;
            List<ValueType> ringList = dbHandler.GetAllDevices(enumProductType.Focus);
            this.cameList = dbHandler.GetAllDevices(enumProductType.CamLens);
            this.alg = new Algorithm(ringList);
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


        private void selectPatchItems(object sender, EventArgs e)
        {
            if (!this.PreCheckUserDataIsEnough()) return;

            int lenInter = Convert.ToInt32(this.cbLensInter.SelectedValue);
            int camIdx = Convert.ToInt32(this.cbCamInter.SelectedValue);
            float sensor = Convert.ToSingle(this.tbSensor.Text);
            float fov = Convert.ToSingle(this.tbFov.Text);
            float ratio = sensor / fov;
            this.lbRatio.Text = ratio.ToString();

            CameraLens cam= this.GetCamInfoById(camIdx);
            float allLength = cam.Fov * ratio - cam.Flange;
            List<RingResult> resultList= this.alg.GetDevicesByBaseInfo(lenInter, cam.Connector, allLength);
        }

        private bool PreCheckUserDataIsEnough()
        {
            return initialConditionFlag &&
                    this.cbLensInter.SelectedIndex > 0 &&
                    this.cbCamInter.SelectedIndex > 0 &&
                    !this.tbSensor.Text.Trim().Equals(string.Empty) &&
                    !this.tbFov.Text.Trim().Equals(string.Empty) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbSensor) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbFov) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbTarget) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbDistance) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbDistanRange) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbLength) &&
                    StringValidator.IsEmptyOrUnsignedRealNumber(this.tbWidth);
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
