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
    /// <summary>
    /// 更新界面进度显示
    /// </summary>
    /// <param name="isNumber">类型</param>
    /// <param name="value">值</param>
    public delegate void UpdateBoardValue(bool isNumber, int value);

    public delegate void UpdateProjCount(int value);

    public delegate void UpdateProgressInfo(string strPro);

    public partial class Form1 : Form
    {
        private string strTodayFileName = DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
        private string strYestdayFileName = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + ".xlsx";
        private HouseType houseType;
        int start=0, end=0;

        public Form1()
        {
            InitializeComponent();
            tbStart.Text = IniFile.ReadIniData("General", "PageStart", "1");
            tbEnd.Text = IniFile.ReadIniData("General", "PageEnd", "1");
            cbClose.Checked = bool.Parse(IniFile.ReadIniData("General", "ShutDown", "true"));
            tbFolder.Text = IniFile.ReadIniData("General", "folderPath", "d:/");
            fileToday.InitialDirectory = tbFolder.Text;
            fileYesterday.InitialDirectory = tbFolder.Text;
            fileToday.FileName = strTodayFileName;
            fileYesterday.FileName = strYestdayFileName;
            tbToday.Text = string.Format("{0}\\{1}", tbFolder.Text, strTodayFileName);
            tbYesterday.Text = string.Format("{0}\\{1}", tbFolder.Text, strYestdayFileName);
            tbTime.Text = IniFile.ReadIniData("General", "timeInter", "1");
            string tmpHouseType = IniFile.ReadIniData("General", "houseType", "NONE");
            houseType = (HouseType)Enum.Parse(typeof(HouseType), tmpHouseType);
            switch (houseType)
            {
                case HouseType.NONE: rbNone.Checked = true; break;
                case HouseType.ALL: rbAll.Checked = true; break;
                case HouseType.CANSELL: rbSell.Checked = true; break;
                case HouseType.COMPARE: rbCompare.Checked = true; break;
                default: break;
            }
            dtpDeal.Value = DateTime.Now;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                EnableSetting(false);
                this.Text = "查看网页信息中。。。";
                if (!ValidNumber(tbStart.Text) || !ValidNumber(tbEnd.Text))
                {
                    MessageBox.Show("页码非正整数");
                    EnableSetting(true);
                    this.Text = "住房信息获取";
                    return;
                }
                start = int.Parse(tbStart.Text);
                end = int.Parse(tbEnd.Text);
                if (end < start)
                {
                    MessageBox.Show("页码写反了");
                    EnableSetting(true);
                    this.Text = "住房信息获取";
                    return;
                }
                Controller ctl = new Controller(houseType, start, end);
                //ctl.updateItemCount = new UpdateProjCount(this.UpdateProgressMax);
                this.UpdateProgressMax((end - start + 1) * 10);
                ctl.UpdateProgess = new UpdateBoardValue(this.UpdateProjProgress);

                string fileName = string.Format("{0}\\{1}.xlsx", tbFolder.Text,dtpDeal.Value.ToString("yyyyMMdd"));
                ctl.ExplainNormalInfo(tbFolder.Text, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常:" + ex.Message + "\r\n" + ex.StackTrace);
            }
            EnableSetting(true);
            this.Text = "住房信息获取";
            if (this.cbClose.Checked)
            {
                Shutdown.ShutDown();
            }
            else
            {
                MessageBox.Show("信息获取完毕");
            }
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

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = folderDialog.SelectedPath;
                tbFolder.Text = foldPath;
                IniFile.WriteIniData("General", "folderPath", foldPath);

                fileToday.InitialDirectory = tbFolder.Text;
                fileYesterday.InitialDirectory = tbFolder.Text;                
                fileToday.FileName = strTodayFileName;
                fileYesterday.FileName = strYestdayFileName;
                tbToday.Text = string.Format("{0}\\{1}", tbFolder.Text, strTodayFileName);
                tbYesterday.Text = string.Format("{0}\\{1}.xlsx", tbFolder.Text, strYestdayFileName);
            }
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
            if (isNumber)
                this.lbItemCount.Text = value.ToString ();
            else
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
            this.btnFolder.Enabled = status;
            this.tbFolder.Enabled = status;
            this.btnStart.Enabled = status;
            this.tbTime.Enabled = status;
            this.gbHouseType.Enabled = status;
            this.dtpDeal.Enabled = status;
            this.btnCompare.Enabled = status;
            this.btnToday.Enabled = status;
            this.btnYesterday.Enabled = status;
            this.tbToday.Enabled = status;
            this.tbYesterday.Enabled = status;
            this.tbStart.Enabled = status;
            this.tbEnd.Enabled = status;
            this.cbClose.Enabled = status;
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
                if (rbCompare.Checked)
                {
                    this.Text = "查看户型中。。。";
                    this.pgbProj.Value = 0;
                    Controller ctl = new Controller(houseType, start, end);
                    ctl.updateItemCount = new UpdateProjCount(this.UpdateProgressMax);
                    ctl.UpdateProgess = new UpdateBoardValue(this.UpdateProjProgress);
                    ctl.UpdateComparedProjectInfo(tbToday.Text, projComparedList);
                    strResult = "处理完成";
                }
                MessageBox.Show(strResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常:" + ex.Message + "\r\n" + ex.StackTrace);
            }
            EnableSetting(true);
            this.Text = "住房信息获取";
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

        private void tbStart_TextChanged(object sender, EventArgs e)
        {
            IniFile.WriteIniData("General", "PageStart", tbStart.Text.Trim());
        }

        private void tbEnd_TextChanged(object sender, EventArgs e)
        {
            IniFile.WriteIniData("General", "PageEnd", tbEnd.Text.Trim());
        }

        private void cbClose_CheckedChanged(object sender, EventArgs e)
        {
            IniFile.WriteIniData("General", "ShutDown", (sender as CheckBox).Checked.ToString ());
        }

    }
}
