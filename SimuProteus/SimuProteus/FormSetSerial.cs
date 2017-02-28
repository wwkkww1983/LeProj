using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class FormSetSerial : Form
    {
        DBUtility dbhandler = new DBUtility();
        SerialCom serial = null;
        SerialInfo serialInfo;

        public FormSetSerial(SerialCom com)
        {
            this.serial = com;

            InitializeComponent();
            this.serialInfo = dbhandler.GetSerialInfo();

            //用配置中的连接信息初始化界面展示
            this.cbPorts.Text = this.serialInfo.PortName;
            this.tbDatabits.Text = this.serialInfo.DataBits.ToString ();
            if(this.serialInfo.Parity > 0) this.rbSingle.Checked = true;
            else this.rbDouble.Checked = true;
            this.tbStopbits.Text = this.serialInfo.StopBits.ToString();
            this.tbTimeout.Text = this.serialInfo.TimeOut.ToString();
            for (int i = 0; i < this.cbBaudrate.Items.Count; i++)
            {
                if (Convert.ToInt32(this.cbBaudrate.Items[i]) == this.serialInfo.BaudRate)
                {
                    this.cbBaudrate.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnFresh_Click(object sender, EventArgs e)
        {
            cbPorts.Items.Clear();
            string[] portStringA = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string portName in portStringA)
            {
                cbPorts.Items.Add(portName);
            }

            btnConnect.Enabled = (cbPorts.Items.Count > 0);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.serial.PortName = this.serialInfo.PortName;
            this.serial.BaudRate = this.serialInfo.BaudRate;
            this.serial.Parity = (Parity)this.serialInfo.Parity;
            this.serial.DataBits = this.serialInfo.DataBits;
            this.serial.StopBits = (StopBits)this.serialInfo.StopBits;
            this.serial.TimeOut = this.serialInfo.TimeOut;

            if (!serial.IsOpen)
            {
                serial.Open();
            }

            if (!serial.IsOpen)
            {
                MessageBox.Show(string.Format("打开串口{0}失败！", this.serial.PortName));
                return;
            }

            MessageBox.Show("成功打开串口，可以开始通信");
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.serialInfo.PortName = this.cbPorts.Text;
            this.serialInfo.DataBits = int.Parse(this.tbDatabits.Text);
            this.serialInfo.Parity = this.rbDouble.Checked?0:1;
            this.serialInfo.StopBits = int.Parse(this.tbStopbits.Text);
            this.serialInfo.TimeOut = int.Parse(this.tbTimeout.Text);
            this.serialInfo.BaudRate = Convert.ToInt32(this.cbBaudrate.SelectedItem);

            if (!dbhandler.UpdateSerialInfo(this.serialInfo))
                MessageBox.Show("保存失败");
        }
    }
}
