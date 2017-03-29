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
            this.cbInter.DataSource = dbHandler.GetDropDownListInfo(enumProductType.Interface);
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
                    Connector = Convert.ToInt32(this.cbInter.SelectedValue),
                    Focus = Convert.ToSingle(this.tbFocus.Text),
                    Flange = Convert.ToSingle(this.tbFlange.Text),
                    Length = Convert.ToSingle(this.tbLength.Text),
                    HH = Convert.ToSingle(this.tbHH.Text),
                    RatioMin = Convert.ToSingle(this.tbRatioMin.Text),
                    RatioMax = Convert.ToSingle(this.tbRatioMax.Text),
                    Target = Convert.ToSingle(this.tbTarget.Text),
                    Weight = this.tbWeight.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(this.tbWeight.Text),
                    Contrast = this.tbFreq.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(this.tbFreq.Text),
                    Distort = this.tbDistort.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(this.tbDistort.Text),
                    ResolutionLength = this.tbResoLength.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(this.tbResoLength.Text),
                    ResolutionWidth = this.tbResoWidth.Text.Trim().Equals(string.Empty) ? 0 : Convert.ToInt32(this.tbResoWidth.Text)
                };
                this.handlerAfterSave(this, enumProductType.CamLens, dbHandler.InsertItem(item));
            }
        }

        private bool CheckDataBeforeSave()
        {
            return StringValidator.HasContent(this.tbName, this.lbName.Text) && 
                  StringValidator.HasContent(this.tbNumber, this.lbNumber.Text) &&
                  StringValidator.HasContent(this.cbInter, this.lbInter.Text) &&
                  StringValidator.HasContent(this.tbFocus, this.lbFocus.Text) && StringValidator.IsUnsignedRealNumber(this.tbFocus) &&
                  StringValidator.HasContent(this.tbFlange, this.lbFlange.Text) && StringValidator.IsUnsignedRealNumber(this.tbFlange) &&
                  StringValidator.HasContent(this.tbTarget, this.lbTarget.Text) && StringValidator.IsUnsignedRealNumber(this.tbTarget) &&
                  StringValidator.HasContent(this.tbRatioMin, this.lbRatioRange.Text) && StringValidator.IsUnsignedRealNumber(this.tbRatioMin) &&
                  StringValidator.HasContent(this.tbRatioMax, this.lbRatioRange.Text) && StringValidator.IsUnsignedRealNumber(this.tbRatioMax) &&
                  StringValidator.HasContent(this.tbLength, this.lbLength.Text) && StringValidator.IsUnsignedRealNumber(this.tbLength) &&
                  StringValidator.HasContent(this.tbHH, this.lbLength.Text) && StringValidator.IsUnsignedRealNumber(this.tbHH) &&
                  //非必须
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbFreq)  &&
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbWeight) &&
                  StringValidator.IsEmptyOrUnsignedRealNumber(this.tbDistort) &&
                  StringValidator.IsEmptyOrUnsignedInteger(this.tbResoLength) &&
                  StringValidator.IsEmptyOrUnsignedInteger(this.tbResoWidth);
        }
    }
}
