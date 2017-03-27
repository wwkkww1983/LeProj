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
    public partial class ViewLens : Form
    {
        private Action<Form, enumProductType, bool> handlerAfterSave = null;
        private DBUtility dbHandler = new DBUtility();

        public ViewLens(Action<Form, enumProductType, bool> afterSave)
        {
            this.handlerAfterSave = afterSave;

            InitializeComponent();
            this.InitialCamInter();
        }

        private void InitialCamInter()
        {
            this.cbInter.DataSource = dbHandler.GetAllConnector();
            this.cbInter.DisplayMember = "Name";
            this.cbInter.ValueMember = "Idx";
            this.cbInter.SelectedIndex = -1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.CheckDataBeforeSave())
            {
                CameraLens item = new CameraLens()
                {
                    Name = this.tbName.Text.Trim(),
                    Number = this.tbNumber.Text.Trim(),
                    Sensor = Convert.ToInt32(this.tbSensor.Text),
                    Fov = Convert.ToInt32(this.tbFov.Text),
                    Connector = Convert.ToInt32(this.cbInter.SelectedValue),
                    Focus = Convert.ToSingle(this.tbFocus.Text),
                    Flange = Convert.ToSingle(this.tbFlange.Text),
                    Ratio = Convert.ToSingle(this.tbRatio.Text),
                    RatioMin = Convert.ToSingle(this.tbRatioMin.Text),
                    RatioMax = Convert.ToSingle(this.tbRatioMax.Text),
                    Target = Convert.ToSingle(this.tbTarget.Text),
                    Weight =this.tbWeight.Text.Trim().Equals(string.Empty)?0: Convert.ToInt32(this.tbWeight.Text),
                    Contrast = this.tbFreq.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(this.tbFreq.Text),
                    Distort = this.tbDistort.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(this.tbDistort.Text)
                };
                this.handlerAfterSave(this, enumProductType.CamLens, dbHandler.InsertItem(item));
            }
        }

        private bool CheckDataBeforeSave()
        {
            return StringValidator.HasContent(this.tbName, this.lbName.Text) && 
                  StringValidator.HasContent(this.tbNumber, this.lbNumber.Text) &&
                  StringValidator.HasContent(this.tbSensor, this.lbSensor.Text) && StringValidator.IsUnsignedRealNumber(this.tbSensor) &&
                  StringValidator.HasContent(this.tbFov, this.lbFov.Text) && StringValidator.IsUnsignedRealNumber(this.tbFov) &&
                  StringValidator.HasContent(this.cbInter, this.lbInter.Text) &&
                  StringValidator.HasContent(this.tbFocus, this.lbFocus.Text) && StringValidator.IsUnsignedRealNumber(this.tbFocus) &&
                  StringValidator.HasContent(this.tbFlange, this.lbFlange.Text) && StringValidator.IsUnsignedRealNumber(this.tbFlange) &&
                  StringValidator.HasContent(this.tbTarget, this.lbTarget.Text) && StringValidator.IsUnsignedRealNumber(this.tbTarget) &&
                  StringValidator.HasContent(this.tbRatioMin, this.lbRatioRange.Text) && StringValidator.IsUnsignedRealNumber(this.tbRatioMin) &&
                  StringValidator.HasContent(this.tbRatioMax, this.lbRatioRange.Text) && StringValidator.IsUnsignedRealNumber(this.tbRatioMax) &&
                  StringValidator.HasContent(this.tbRatio, this.lbRatio.Text) && StringValidator.IsUnsignedRealNumber(this.tbRatio) &&
                  //非必须
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbFreq)  &&
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbWeight) &&
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbDistort);
        }

        private void tbRatioCondition_TextChanged(object sender, EventArgs e)
        {
            if (this.tbSensor.Text.Trim().Equals(string.Empty) || this.tbFov.Text.Trim().Equals(string.Empty))
                return;

            float fov = Convert.ToSingle(this.tbFov.Text);
            float sensor = Convert.ToSingle(this.tbSensor.Text);

            this.tbRatio.Text =(sensor / fov).ToString("F2");
        }
    }
}
