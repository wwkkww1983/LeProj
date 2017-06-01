using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Sunisoft.IrisSkin;
using System.Runtime.InteropServices;


namespace NetControl
{
    public partial class Form1 : Form
    {
        private const int BUFFER_SIZE = 1024;
        private const string PICKUP_CMD = "ATZ", HANGUP_CMD = "ATH", PHONE_CMD = "ATB",START_CMD="5",END_CMD="9",FINISH_CMD="2";
        private bool commandSended = false;
        private int workTime = 0, countTime = 0, webFailCount = 0;
        private float mink11, maxk11, mink12, maxk12, minl11, maxl11, minl12, maxl12, mind11, maxd11, mind12, maxd12;
        private float mink21, maxk21, mink22, maxk22, minl21, maxl21, minl22, maxl22, mind21, maxd21, mind22, maxd22;
        private float mink31, maxk31, mink32, maxk32, minl31, maxl31, minl32, maxl32, mind31, maxd31, mind32, maxd32;
        private string currentCommand = string.Empty;
        private SkinEngine skin = null;
        private SerialPort com = new SerialPort();

        #region 窗口事件
        public Form1()
        {
            InitializeComponent();

            this.InitialInfo();
            this.EnableComponent(true);

            this.skin = new SkinEngine(this);
            this.skin.SkinFile = "Wave.ssk";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!this.CheckDataValid()) return;

            this.EnableComponent(false);
            this.SaveCurrentConfig();
            this.lbStatus.Text = "录波准备中。。。";

            this.workTime = int.Parse(this.tbTime.Text);
            this.countTime = this.workTime;
            this.currentCommand = START_CMD;//开始录波命令
            this.commandSended = false;
            if (!this.com.IsOpen)
            {
                this.com.PortName = this.cbPorts.Text;
                this.com.Open();
                this.com.DiscardInBuffer();
                this.com.DiscardOutBuffer();
            }

            this.PickUpPhone();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.tbTime.Text = this.workTime.ToString();

            this.currentCommand = END_CMD;//结束录波命令
            this.commandSended = false;

            this.PickUpPhone();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.com.Close();
            cbPorts.Items.Clear();
            string[] portStringA = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string portName in portStringA)
            {
                cbPorts.Items.Add(portName);
            }
        }

        private void btnBreak_Click(object sender, EventArgs e)
        {
            this.SendCommand(HANGUP_CMD);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!this.com.IsOpen)
            {
                this.com.PortName = this.cbPorts.Text;
                this.com.Open();
                this.com.DiscardInBuffer();
                this.com.DiscardOutBuffer();
            }
            this.timerWeb.Enabled = true;
            this.lbStatus.Text = "串口打开";
            this.EnableComponent(false);
            this.SaveCurrentConfig();
        }

        private void timerCount_Tick(object sender, EventArgs e)
        {
            this.countTime--;
            this.tbTime.Text = this.countTime.ToString();
            if (this.countTime <= 0)
            {
                this.timerCount.Enabled = false;
                this.btnStop_Click(null, null);
            }
        }

        private List<MachineInfo> getWebData()
        {
            string strJson = string.Empty;
            try
            {
                strJson = WebInfo.GetPageInfo("http://pcis/projects/power/correct_power.asp?callback=?");
                //strJson = Ini.GetItemValue("general", "json");
                strJson = strJson.Replace('(', ' ').Replace(')', ' ');
            }
            catch
            {
            }
            if (!strJson.Contains("POWER"))
            {
                this.webFailCount++;
                if (this.webFailCount >= 5)
                {
                    this.timerWeb.Enabled = false;
                    MessageBox.Show("网络读取数据失败");
                }
                return null;
            }

            List<MachineInfo> infoList = JSON.parse<List<MachineInfo>>(strJson);
            this.webFailCount = 0;           

            return infoList;
        }

        private void timerWeb_Tick(object sender, EventArgs e)
        {
            List<MachineInfo> infoList = this.getWebData();
            if (infoList == null || infoList.Count == 0) return;

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("name"); dt.Columns.Add(dc);
            dc = new DataColumn("k");            dt.Columns.Add(dc);
            dc = new DataColumn("l");            dt.Columns.Add(dc);
            dc = new DataColumn("d");            dt.Columns.Add(dc);
            DataRow dr1 = dt.NewRow();
            DataRow dr2 = dt.NewRow();
            dr1[0] = "GONGLI1";
            dr2[0] = "GONGLI2";
            for (int i = 0; i < infoList.Count; i++)
            {
                MachineInfo item = infoList[i];
                dr1[i + 1] = item.GONGLI1;
                dr2[i + 1] = item.GONGLI2;
            }
            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            this.dgvMachine.DataSource = dt;

            if (this.checkDataValid(infoList))
            {
                this.btnStart_Click(null, null);
            }
        }

        private bool checkDataValid(List<MachineInfo> infoList)
        {
            MachineInfo infoK = infoList[0], infoL = infoList[0], infoD = infoList[0];
            float k1, k2, l1, l2, d1, d2;
            k1 = float.Parse(infoK.GONGLI1);
            k2 = float.Parse(infoK.GONGLI2);
            l1 = float.Parse(infoL.GONGLI1);
            l2 = float.Parse(infoL.GONGLI2);
            d1 = float.Parse(infoD.GONGLI1);
            d2 = float.Parse(infoD.GONGLI2);

            return this.mink11 <= k1 && k1 <= this.maxk11 || this.mink21 <= k1 && k1 <= this.maxk21 || this.mink31 <= k1 && k1 <= this.maxk31 ||
                    this.mink12 <= k1 && k2 <= this.maxk12 || this.mink22 <= k1 && k2 <= this.maxk22 || this.mink31 <= k2 && k2 <= this.maxk32 ||
                    this.minl11 <= l1 && l1 <= this.maxl11 || this.minl21 <= l1 && l1 <= this.maxl21 || this.minl31 <= l1 && l1 <= this.maxl31 ||
                    this.minl12 <= l1 && l2 <= this.maxl12 || this.minl22 <= l1 && l2 <= this.maxl22 || this.minl31 <= l2 && l2 <= this.maxl32 ||
                    this.mind11 <= d1 && d1 <= this.maxd11 || this.mind21 <= d1 && d1 <= this.maxd21 || this.mind31 <= d1 && d1 <= this.maxd31 ||
                    this.mind12 <= d1 && d2 <= this.maxd12 || this.mind22 <= d1 && d2 <= this.maxd22 || this.mind31 <= d2 && d2 <= this.maxd32;

        }


        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbTemp = sender as RadioButton;
            if (!rbTemp.Checked) return;

            if (rbTemp == this.rbAuto)
            {
                this.tcHandler.SelectedIndex = 0;
                this.webFailCount = 0;
            }
            else
            {
                this.tcHandler.SelectedIndex = 1;
            }
        }

        #endregion

        #region 子函数

        private void PickUpPhone()
        {
            this.SendCommand(HANGUP_CMD);
            System.Threading.Thread.Sleep(1000);
            this.SendCommand(PICKUP_CMD);
        }
        private void SaveCurrentConfig()
        {
            Ini.SetItemValue("general", "serialport", this.cbPorts.Text);
            Ini.SetItemValue("general", "number", this.tbPhone.Text);
            Ini.SetItemValue("general", "time", this.tbTime.Text);
        }

        private void InitialInfo()
        {
            this.rbUser.Checked = true;

            this.cbPorts.Text = Ini.GetItemValue("general", "serialport");
            this.tbPhone.Text = Ini.GetItemValue("general", "number");
            this.tbTime.Text = Ini.GetItemValue("general", "time");
            this.mink11 = float.Parse(Ini.GetItemValue("boundary", "mink11"));
            this.mink21 = float.Parse(Ini.GetItemValue("boundary", "mink21"));
            this.mink31 = float.Parse(Ini.GetItemValue("boundary", "mink31"));
            this.minl11 = float.Parse(Ini.GetItemValue("boundary", "minl11"));
            this.minl21 = float.Parse(Ini.GetItemValue("boundary", "minl21"));
            this.minl31 = float.Parse(Ini.GetItemValue("boundary", "minl31"));
            this.mind11 = float.Parse(Ini.GetItemValue("boundary", "mind11"));
            this.mind21 = float.Parse(Ini.GetItemValue("boundary", "mind21"));
            this.mind31 = float.Parse(Ini.GetItemValue("boundary", "mind31"));
            this.maxk11 = float.Parse(Ini.GetItemValue("boundary", "maxk11"));
            this.maxk21 = float.Parse(Ini.GetItemValue("boundary", "maxk21"));
            this.maxk31 = float.Parse(Ini.GetItemValue("boundary", "maxk31"));
            this.maxl11 = float.Parse(Ini.GetItemValue("boundary", "maxl11"));
            this.maxl21 = float.Parse(Ini.GetItemValue("boundary", "maxl21"));
            this.maxl31 = float.Parse(Ini.GetItemValue("boundary", "maxl31"));
            this.maxd11 = float.Parse(Ini.GetItemValue("boundary", "maxd11"));
            this.maxd21 = float.Parse(Ini.GetItemValue("boundary", "maxd21"));
            this.maxd31 = float.Parse(Ini.GetItemValue("boundary", "maxd31"));

            this.mink12 = float.Parse(Ini.GetItemValue("boundary", "mink12"));
            this.mink22 = float.Parse(Ini.GetItemValue("boundary", "mink22"));
            this.mink32 = float.Parse(Ini.GetItemValue("boundary", "mink32"));
            this.minl12 = float.Parse(Ini.GetItemValue("boundary", "minl12"));
            this.minl22 = float.Parse(Ini.GetItemValue("boundary", "minl22"));
            this.minl32 = float.Parse(Ini.GetItemValue("boundary", "minl32"));
            this.mind12 = float.Parse(Ini.GetItemValue("boundary", "mind12"));
            this.mind22 = float.Parse(Ini.GetItemValue("boundary", "mind22"));
            this.mind32 = float.Parse(Ini.GetItemValue("boundary", "mind32"));
            this.maxk12 = float.Parse(Ini.GetItemValue("boundary", "maxk12"));
            this.maxk22 = float.Parse(Ini.GetItemValue("boundary", "maxk22"));
            this.maxk32 = float.Parse(Ini.GetItemValue("boundary", "maxk32"));
            this.maxl12 = float.Parse(Ini.GetItemValue("boundary", "maxl12"));
            this.maxl22 = float.Parse(Ini.GetItemValue("boundary", "maxl22"));
            this.maxl32 = float.Parse(Ini.GetItemValue("boundary", "maxl32"));
            this.maxd12 = float.Parse(Ini.GetItemValue("boundary", "maxd12"));
            this.maxd22 = float.Parse(Ini.GetItemValue("boundary", "maxd22"));
            this.maxd32 = float.Parse(Ini.GetItemValue("boundary", "maxd32"));
            
            this.com.BaudRate = 1200;
            this.com.DataBits = 8;
            this.com.StopBits = StopBits.One;
            this.com.Parity = Parity.None;
            this.com.DtrEnable = true;
            this.com.RtsEnable = true;
            this.com.DataReceived += new SerialDataReceivedEventHandler(this.ReceiveInfo);

            this.btnRefresh_Click(null, null);
        }

        private void SetStatusLabel(string strContent)
        {
            if (this.InvokeRequired)
            {
                Action<string> delegateSetStatus = new Action<string>(SetStatusLabel);
                this.Invoke(delegateSetStatus, new object[] { strContent });
                return;
            }
            this.lbStatus.Text = strContent;
        }
        #endregion

        #region 收发
        private void ReceiveInfo(Object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] readBuffer = new byte[BUFFER_SIZE];
                //int count = this.com.ReadBuffer(readBuffer, BUFFER_SIZE);
                int count = this.com.Read(readBuffer, 0, BUFFER_SIZE);
                string strFeedback = System.Text.ASCIIEncoding.ASCII.GetString(readBuffer);
                this.HandlerByFeedback(strFeedback);
                //if (count >2 && readBuffer[count-1] == 0x0A && readBuffer[count-2] == 0x0D)
                //{
                //    this.HandlerByFeedback(strFeedback.Substring(0, count - 2));
                //}
            }
            catch (TimeoutException timeEx)
            {
                Console.WriteLine("接受超时：" + timeEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("接收返回消息异常！具体原因：" + ex.Message);
            }
        }

        private void HandlerByFeedback(string strInfo)
        {
            string[] codeArray = strInfo.Split('\r');
            foreach (string code in codeArray)
            {
                if (!code.Contains("AT")) continue;
                int nrIdx = code.IndexOf('\n');
                string tempCode = code;
                if (nrIdx >= 0)
                    tempCode = code.Remove(nrIdx, 1);
                Record.WriteLogFile("接受：" + tempCode);
                switch (tempCode)
                {
                    case "ATZOK": //摘机成功后，开始拨号
                        this.SendCommand(PHONE_CMD, this.tbPhone.Text.Trim());
                        break;
                    case "ATBOK": //拨号成功 
                        System.Threading.Thread.Sleep(1000);
                        break;
                    case "ATS2":// 线路繁忙时，挂断电话
                                //case "ATS4":// 线路信号为60秒静音时（电话连线但没人讲话）
                        this.SendCommand(HANGUP_CMD);
                        break;
                    case "ATHOK"://挂机成功后，关闭串口
                        this.SetStatusLabel("电话挂断成功");
                        break;
                    case "ATCMDERROR"://上位机命令出错
                        this.SetStatusLabel("命令出错");
                        break;
                    case "ATN1"://对方摘机，开始发命令
                        System.Threading.Thread.Sleep(1000);
                        this.SendCommand(PHONE_CMD, this.currentCommand);
                        this.SetStatusLabel("通讯成功");
                        break;
                    case "ATN4"://命令操作成功，开始录播                        
                    case "ATN8"://结束录播
                        this.SendCommand(PHONE_CMD, FINISH_CMD);
                        System.Threading.Thread.Sleep(1000);
                        this.SendCommand(HANGUP_CMD);
                        this.SetStatusLabel("操作执行成功");
                        break;
                    default: break;
                }
                if (tempCode == "ATN4")
                {
                    this.timerCount.Enabled = true;
                    this.lbStatus.Text = "正在录波";
                }
                else if (tempCode == "ATN8")
                {
                    this.EnableComponent(true);
                    this.lbStatus.Text = "录波完成";
                }
            }
        }

        private void SendCommand(string cmd, string info = "")
        {
            string strCode = cmd + info;
            Record.WriteLogFile("发：" + strCode);
            byte[] byteCode = System.Text.ASCIIEncoding.ASCII.GetBytes(strCode);
            byte[] buff = new byte[byteCode.Length + 2];
            Array.Copy(byteCode, buff, byteCode.Length);
            buff[buff.Length - 2] = 13;
            buff[buff.Length - 1] = 10;

            this.com.Write(buff, 0, buff.Length);
            System.Threading.Thread.Sleep(1000);
        }
        #endregion

        #region 校验
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
            this.btnStop.Enabled = !status;
            this.btnStart.Enabled = status;
            this.tbPhone.Enabled = status;
            this.tbTime.Enabled = status;
            this.btnRefresh.Enabled = status;
            this.cbPorts.Enabled = status;
            this.gbDevices.Enabled = status;
        }
        #endregion

        private void tcHandler_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tcTemp = sender as TabControl;

            tcTemp.SelectedIndex = this.rbAuto.Checked ? 0 : 1;
        }
    }
}
