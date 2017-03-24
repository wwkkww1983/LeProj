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
    public partial class MidRing : Form
    {
        private enumProductType typeName;
        private DBUtility dbHandler = new DBUtility();
        private Action<Form, enumProductType, bool> handlerAfterSave = null;

        public MidRing(enumProductType type, Action<Form, enumProductType, bool> afterSave)
        {
            this.typeName = type;
            this.handlerAfterSave = afterSave;

            InitializeComponent();
        }

        private void MidRing_Load(object sender, EventArgs e)
        {
            DataTable dt = dbHandler.GetAllConnector();
            this.cbInterA.DataSource = dt;
            this.cbInterA.DisplayMember = "Name";
            this.cbInterA.ValueMember = "Idx";
            DataTable dtB = null;

            string strTypeName = string.Empty;
            switch (this.typeName)
            {
                case enumProductType.Focus: strTypeName = Constants.FOCUS; break;
                case enumProductType.Adapter: strTypeName = Constants.ADAPTER; break;
                case enumProductType.Extend: strTypeName = Constants.EXTEND;
                    dtB = dt;
                    break;
                default: break;
            }
            this.Text = strTypeName;
            this.ShowLengthRange(this.typeName == enumProductType.Focus);

            this.cbInterB.DataSource = dtB == null ? dt.Copy() : dtB;
            this.cbInterB.DisplayMember = "Name";
            this.cbInterB.ValueMember = "Idx";
        }

        private void ShowLengthRange(bool needShow)
        {
            this.lbLenRange.Visible = needShow;
            this.lbRangeInter.Visible = needShow;
            this.tbLenMin.Visible = needShow;
            this.tbLenMax.Visible = needShow;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.CheckDataBeforeSave())
            {
                RingMedium item = new RingMedium()
                {
                    Name = this.tbName.Text.Trim(),
                    Number = this.tbNumber.Text.Trim(),
                    InterUp = Convert.ToInt32(this.cbInterA.SelectedValue),
                    InterDown = Convert.ToInt32(this.cbInterB.SelectedValue),
                    Length = int.Parse(this.tbLength.Text)
                };
                if (this.typeName == enumProductType.Focus)
                {
                    item.LengthMin = int.Parse(this.tbLenMin.Text);
                    item.LengthMax = int.Parse(this.tbLenMax.Text);
                }
                this.handlerAfterSave(this, this.typeName, new DBUtility().InsertItem(item));
            }
        }

        private bool CheckDataBeforeSave()
        {
            bool emptyFlag = StringValidator.HasContent(this.tbName, this.lbName.Text) && StringValidator.HasContent(this.tbNumber, this.lbNumber.Text) &&
                  StringValidator.HasContent(this.cbInterA, this.lbInterA.Text) && StringValidator.HasContent(this.cbInterB, this.lbInterB.Text) &&
                  StringValidator.HasContent(this.tbLength, this.lbLength.Text) && StringValidator.IsUnsignedRealNumber(this.tbLength);

            if (emptyFlag && this.typeName == enumProductType.Focus)
            {
                emptyFlag = StringValidator.HasContent(this.tbLenMin, this.lbLenRange.Text) && StringValidator.HasContent(this.tbLenMax, this.lbLenRange.Text) &&
                     StringValidator.IsUnsignedRealNumber(this.tbLenMin) && StringValidator.IsUnsignedRealNumber(this.tbLenMax);
            }
            return emptyFlag;
        }
    }
}