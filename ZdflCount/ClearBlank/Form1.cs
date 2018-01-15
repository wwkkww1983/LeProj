using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearBlank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string str = this.tbSource.Text.Trim();
            byte[] byteStr = System.Text.Encoding.GetEncoding("GBK").GetBytes(str);
            string result = string.Empty;
            foreach (byte item in byteStr)
            {
                result += string.Format("{0:X}", item);
            }
            this.tbTarget.Text = result;            
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            this.tbSource.Text = string.Empty;
            this.tbTarget.Text = string.Empty;
        }
    }
}
