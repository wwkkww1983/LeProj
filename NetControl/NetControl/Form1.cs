using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Core;

namespace NetControl
{
    public partial class Form1 : Form
    {
        int workTime = 0, countTime=0;
        public Form1()
        {
            InitializeComponent();

            this.InitialInfo();
            this.EnableComponent(true);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!this.CheckDataValid()) return;
            this.workTime = int.Parse(this.tbTime.Text);
            this.countTime = this.workTime;

            this.EnableComponent(false);

            Ini.SetItemValue("general", "number", this.tbPhone.Text);
            Ini.SetItemValue("general", "time", this.tbTime.Text);
            int idx = this.rbOne.Checked ? 1 : 0;
            idx = this.rbTwo.Checked ? 2 : idx;
            idx = this.rbThree.Checked ? 3 : idx;
            idx = this.rbFour.Checked ? 4 : idx;
            idx = this.rbFive.Checked ? 5 : idx;
            idx = this.rbSix.Checked ? 6 : idx;
            Ini.SetItemValue("general", "device", idx.ToString ());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.EnableComponent(true);
            this.tbTime.Text = this.workTime.ToString();
        }

        private void InitialInfo()
        {
            this.tbPhone.Text = Ini.GetItemValue("general", "number");
            this.tbTime.Text = Ini.GetItemValue("general", "time");
            int idx = int.Parse(Ini.GetItemValue("general", "device"));
            RadioButton rbTmp = null;
            switch (idx)
            {
                case 2: rbTmp = this.rbTwo; break;
                case 3: rbTmp = this.rbThree; break;
                case 4: rbTmp = this.rbFour; break;
                case 5: rbTmp = this.rbFive; break;
                case 6: rbTmp = this.rbSix; break;
                default: rbTmp = this.rbOne; break;
            }
            rbTmp.Checked = true;
        }

        private bool CheckDataValid()
        {
            bool isValid = true;
            if (!this.IsPhoneNumber(this.tbPhone.Text))
            {
                MessageBox.Show("电话号码有误","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                isValid = false;
            }

            if (!this.isUnsignedNumber(this.tbTime.Text))
            {
                MessageBox.Show("录波时间尽可为正整数", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isValid = false;
            }

            return isValid;
        }

        private bool IsPhoneNumber(string input)
        {
             string strPattern = @"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)" ;
             return Regex.IsMatch(input, strPattern, RegexOptions.IgnoreCase);
        }

        private bool isUnsignedNumber(string input)
        {
            string strPattern = @"^\d*$";
            return Regex.IsMatch(input, strPattern, RegexOptions.IgnoreCase);
        }

        private void EnableComponent(bool status)
        {
            this.timerCount.Enabled = !status;
            this.btnStop.Enabled = !status;
            this.btnStart.Enabled = status;
            this.tbPhone.Enabled = status;
            this.tbTime.Enabled = status;
            this.gbDevices.Enabled = status;
        }

        private void timerCount_Tick(object sender, EventArgs e)
        {
            this.countTime--;
            this.tbTime.Text = this.countTime.ToString();
            if (this.countTime <= 0)
                this.btnStop_Click(null, null);
        }
    }
}
