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
        private EnumType typeName;
        public MidRing(EnumType type)
        {
            this.typeName = type;
            InitializeComponent();
        }

        private void MidRing_Load(object sender, EventArgs e)
        {
            string strTypeName = string.Empty;
            switch (this.typeName)
            {
                case EnumType.Focus: strTypeName = Constants.FOCUS; break;
                case EnumType.Adapter: strTypeName = Constants.ADAPTER; break;
                case EnumType.Extend: strTypeName = Constants.EXTEND; break;
                default: MessageBox.Show("参数异常"); return;
            }
            this.Text = strTypeName;
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
