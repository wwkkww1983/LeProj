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

namespace MotionCalc
{
    public partial class SetColorLabel : Form
    {
        private int colorLabelSelected;
        private HanlderNoParams refreshImageShow = null;

        public SetColorLabel(HanlderNoParams refreshImageShow)
        {
            this.refreshImageShow = refreshImageShow;

            InitializeComponent();

            this.InitialInfo();
        }

        private void InitialInfo()
        {
            this.tbMinArea.Text = Constants.MinRecogRectArea.ToString();
            this.tbMaxArea.Text = Constants.MaxRecogRectArea.ToString();
            this.tbMinRatio.Text = Constants.MinRecogRectWHRatio.ToString();
            this.tbMaxRatio.Text = Constants.MaxRecogRectWHRatio.ToString();
            this.colorLabelSelected = Constants.LabelColor;
            bool selectedRatio = this.initialRatioSelection(this.rbBlack) || this.initialRatioSelection(this.rbBlue) ||
               this.initialRatioSelection(this.rbGreen) || this.initialRatioSelection(this.rbPurple) ||
               this.initialRatioSelection(this.rbWhite) || this.initialRatioSelection(this.rbYellow) ||
               this.initialRatioSelection(this.rbRed) || this.initialRatioSelection(this.rbGray);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!(StringValidator.HasContent(this.tbMinArea, this.gbArea.Text) &&
                  StringValidator.HasContent(this.tbMaxArea, this.gbArea.Text) &&
                  StringValidator.HasContent(this.tbMinRatio, this.gbRatio.Text) &&
                  StringValidator.HasContent(this.tbMaxRatio, this.gbRatio.Text) &&

                  StringValidator.IsUnsignedNumber(this.tbMinArea) &&
                  StringValidator.IsUnsignedNumber(this.tbMaxArea) &&
                  StringValidator.IsUnsignedRealNumber(this.tbMinRatio) &&
                  StringValidator.IsUnsignedRealNumber(this.tbMaxRatio))
                  )
                return;

            Constants.MinRecogRectArea = int.Parse(this.tbMinArea.Text);
            Constants.MaxRecogRectArea = int.Parse(this.tbMaxArea.Text);
            Constants.MinRecogRectWHRatio = float.Parse(this.tbMinRatio.Text);
            Constants.MaxRecogRectWHRatio = float.Parse(this.tbMaxRatio.Text);
            Constants.LabelColor = this.colorLabelSelected;

            Ini.SetItemValue("general", "minLabelArea", this.tbMinArea.Text);
            Ini.SetItemValue("general", "maxLabelArea", this.tbMaxArea.Text);
            Ini.SetItemValue("general", "minLabelWHRatio", this.tbMinRatio.Text);
            Ini.SetItemValue("general", "minLabelWHRatio", this.tbMaxRatio.Text);
            Ini.SetItemValue("general", "labelColor", this.colorLabelSelected.ToString());

            this.refreshImageShow();
            this.Close();
        }

        private void rbColor_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbTemp = sender as RadioButton;
            if (!rbTemp.Checked) return;

            this.colorLabelSelected = rbTemp.BackColor.ToArgb();
        }

        private bool initialRatioSelection(RadioButton rbItem)
        {
            if (this.colorLabelSelected == rbItem.BackColor.ToArgb())
            {
                rbItem.Checked = true;
                return true;
            }
            return false;
        }
    }
}