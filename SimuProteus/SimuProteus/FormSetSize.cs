using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class FormSetSize : Form
    {
        public FormSetSize()
        {
            InitializeComponent();

            this.tbWidth.Text = Ini.GetItemValue("sizeInfo", "netWidth");
            this.tbLength.Text = Ini.GetItemValue("sizeInfo", "netHeight");
            this.tbFootSize.Text = Ini.GetItemValue("sizeInfo", "pixelFootSize");
            this.tbLineWidth.Text = Ini.GetItemValue("sizeInfo", "pixelLineWidth");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!(int.Parse(this.tbWidth.Text) > 0 && int.Parse(this.tbLength.Text) > 0 &&
                int.Parse(this.tbFootSize.Text) > 0 && int.Parse(this.tbLineWidth.Text) > 0))
            {
                MessageBox.Show("仅可输入正整数");
                return;
            }
            Ini.SetItemValue("sizeInfo", "netWidth", this.tbWidth.Text);
            Ini.SetItemValue("sizeInfo", "netHeight", this.tbLength.Text);
            Ini.SetItemValue("sizeInfo", "pixelFootSize", this.tbFootSize.Text);
            Ini.SetItemValue("sizeInfo", "pixelLineWidth", this.tbLineWidth.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
