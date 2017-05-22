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
        private int workTime = 0, countTime = 0;
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
            this.workTime = int.Parse(this.tbTime.Text);
            this.countTime = this.workTime;

            this.EnableComponent(false);
            this.SaveCurrentConfig();

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
            this.EnableComponent(true);
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
        private void timerCount_Tick(object sender, EventArgs e)
        {
            this.countTime--;
            this.tbTime.Text = this.countTime.ToString();
            if (this.countTime <= 0)
                this.btnStop_Click(null, null);
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
            int idx = this.rbOne.Checked ? 1 : 0;
            idx = this.rbTwo.Checked ? 2 : idx;
            idx = this.rbThree.Checked ? 3 : idx;
            idx = this.rbFour.Checked ? 4 : idx;
            idx = this.rbFive.Checked ? 5 : idx;
            idx = this.rbSix.Checked ? 6 : idx;
            Ini.SetItemValue("general", "device", idx.ToString());
        }

        private void InitialInfo()
        {
            this.cbPorts.Text = Ini.GetItemValue("general", "serialport");
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
            this.timerCount.Enabled = !status;
            this.btnStop.Enabled = !status;
            this.btnStart.Enabled = status;
            this.tbPhone.Enabled = status;
            this.tbTime.Enabled = status;
            this.btnRefresh.Enabled = status;
            this.cbPorts.Enabled = status;
            this.gbDevices.Enabled = status;
        }
        #endregion

    }
}
