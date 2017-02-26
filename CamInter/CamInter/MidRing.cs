using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamInter
{
    public partial class MidRing : Form
    {
        private enumProductType typeName;
        public MidRing(enumProductType type)
        {
            this.typeName = type;
            InitializeComponent();
        }

        private void MidRing_Load(object sender, EventArgs e)
        {
            string strTypeName = string.Empty;
            switch (this.typeName)
            {
                case enumProductType.Focus: strTypeName = Constants.FOCUS; break;
                case enumProductType.Adapter: strTypeName = Constants.ADAPTER; break;
                case enumProductType.Extend: strTypeName = Constants.EXTEND; break;
                default: MessageBox.Show("参数异常"); return;
            }
            this.Text = strTypeName;

            if (this.typeName == enumProductType.Extend)
            {
                this.tbInterB.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
