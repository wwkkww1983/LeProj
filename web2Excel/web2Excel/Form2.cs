using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace web2Excel
{
    public partial class Form2 : Form
    {
        private string strTodayFileName = DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
        private string strYestdayFileName = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + ".xlsx";
        private HouseType houseType;

        public Form2()
        {
            InitializeComponent();
            fileToday.FileName = strTodayFileName;
            fileYesterday.FileName = strYestdayFileName;
            dtpDeal.Value = DateTime.Now;
        }


        /// <summary>
        /// 校验数字
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns>是否为数字</returns>
        private bool ValidNumber(string strValue)
        {
            string strPattern = @"^\+?[1-9][0-9]*$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(strPattern);
            return regex.IsMatch(strValue);
        }

        /// <summary>
        /// 更新进度条最大值
        /// </summary>
        /// <param name="value"></param>
        private void UpdateProgressMax(int value)
        {
            this.pgbProj.Maximum = value;
        }

        /// <summary>
        /// 更新项目进度
        /// </summary>
        /// <param name="isNumber">模块</param>
        /// <param name="value">值</param>
        private void UpdateProjProgress(bool isNumber, int value)
        {
            this.pgbProj.Value = value;
        }

        /// <summary>
        /// 更新当前执行任务名称
        /// </summary>
        /// <param name="strProgress"></param>
        private void UpdateProgressInfo(string strProgress)
        {
            this.Text = strProgress;
        }

        /// <summary>
        /// 修改控件可用性
        /// </summary>
        /// <param name="status"></param>
        private void EnableSetting(bool status)
        {
            this.dtpDeal.Enabled = status;
            this.btnCompare.Enabled = status;
            this.btnToday.Enabled = status;
            this.btnYesterday.Enabled = status;
            this.tbToday.Enabled = status;
            this.tbYesterday.Enabled = status;
        }

        private void tbTime_Leave(object sender, EventArgs e)
        {
            float timeValue = float.Parse(((TextBox)sender).Text.Trim());
            if (timeValue > 0.005f)
            {
                WebInfo.SleepOnePage = timeValue;
                IniFile.WriteIniData("General", "timeInter", timeValue.ToString());
            }
            else
            {
                MessageBox.Show("间隔太小会被查封");
                ((TextBox)sender).Text = "1";
                WebInfo.SleepOnePage = 1;
                IniFile.WriteIniData("General", "timeInter", "1");
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            try
            {
                EnableSetting(false);                
                string strResult = "对比完成";

                List<string> projComparedList = new List<string>();

                ExcelInfo.updateItemCount = new UpdateProjCount(this.UpdateProgressMax);
                ExcelInfo.UpdateProgess = new UpdateBoardValue(this.UpdateProjProgress);
                ExcelInfo.updateProgressInfo = new UpdateProgressInfo(this.UpdateProgressInfo);
                string strTime = dtpDeal.Value.ToString("yyyy年MM月dd日");
                switch (ExcelInfo.CompareData(tbToday.Text, tbYesterday.Text, strTime, projComparedList))
                {
                    case ErrorInfo.YesterFileNoExists:
                        strResult = "昨天的文件不存在";
                        break;
                    case ErrorInfo.TodayFileNoExists:
                        strResult = "今天的文件丢失";
                        break;
                    default: break;
                }
                MessageBox.Show(strResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常:" + ex.Message + "\r\n" + ex.StackTrace);
            }
            EnableSetting(true);
            this.Text = "住房信息对比";
        }

        private void rbHouseType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbHouseItem = sender as RadioButton;
            if (!rbHouseItem.Checked) return;

            houseType = (HouseType)Enum.Parse(typeof(HouseType), rbHouseItem.Tag.ToString());
            IniFile.WriteIniData("General", "houseType", rbHouseItem.Tag.ToString());
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            if (fileToday.ShowDialog() == DialogResult.OK)
            {
                tbToday.Text = fileToday.FileName;
            }
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            if (fileYesterday.ShowDialog() == DialogResult.OK)
            {
                tbYesterday.Text = fileYesterday.FileName;
            }
        }
        
        private void cbClose_CheckedChanged(object sender, EventArgs e)
        {
            IniFile.WriteIniData("General", "ShutDown", (sender as CheckBox).Checked.ToString ());
        }

    }
}
