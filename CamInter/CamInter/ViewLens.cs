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
        public ViewLens(Action<Form, enumProductType, bool> afterSave)
        {
            this.handlerAfterSave = afterSave;

            InitializeComponent();
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
                    InterUp = Convert.ToInt32(this.cbInterA.SelectedValue),
                    InterDown = Convert.ToInt32(this.cbInterB.SelectedValue),
                    Length = int.Parse(this.tbLength.Text)
                };
                this.handlerAfterSave(this, enumProductType.CamLens, new DBUtility().InsertItem(item));
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
                  //非必须
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbFreq)  &&
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbWeight) &&
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbDistort);
        }
    }
}
