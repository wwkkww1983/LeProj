namespace SimuProteus
{
    partial class FormSerial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSerial));
            this.label1 = new System.Windows.Forms.Label();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearSend = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.rtbHistory = new System.Windows.Forms.RichTextBox();
            this.rbHex = new System.Windows.Forms.RadioButton();
            this.rbAssic = new System.Windows.Forms.RadioButton();
            this.rbDouble = new System.Windows.Forms.RadioButton();
            this.rbSingle = new System.Windows.Forms.RadioButton();
            this.tbTimeout = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbStopbits = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDatabits = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnFresh = new System.Windows.Forms.Button();
            this.cbBaudrate = new System.Windows.Forms.ComboBox();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "消息记录";
            // 
            // tbSend
            // 
            this.tbSend.Location = new System.Drawing.Point(205, 334);
            this.tbSend.Multiline = true;
            this.tbSend.Name = "tbSend";
            this.tbSend.Size = new System.Drawing.Size(684, 124);
            this.tbSend.TabIndex = 2;
            this.tbSend.Text = "ab00fef";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "待发送内容";
            // 
            // btnClearSend
            // 
            this.btnClearSend.Location = new System.Drawing.Point(689, 464);
            this.btnClearSend.Name = "btnClearSend";
            this.btnClearSend.Size = new System.Drawing.Size(97, 23);
            this.btnClearSend.TabIndex = 4;
            this.btnClearSend.Text = "清空发送区";
            this.btnClearSend.UseVisualStyleBackColor = true;
            this.btnClearSend.Click += new System.EventHandler(this.btnClearSend_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(792, 464);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(97, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rtbHistory
            // 
            this.rtbHistory.Location = new System.Drawing.Point(208, 32);
            this.rtbHistory.Name = "rtbHistory";
            this.rtbHistory.Size = new System.Drawing.Size(681, 278);
            this.rtbHistory.TabIndex = 5;
            this.rtbHistory.Text = "";
            this.rtbHistory.TextChanged += new System.EventHandler(this.rtbHistory_TextChanged);
            // 
            // rbHex
            // 
            this.rbHex.AutoSize = true;
            this.rbHex.Checked = true;
            this.rbHex.Location = new System.Drawing.Point(42, 46);
            this.rbHex.Name = "rbHex";
            this.rbHex.Size = new System.Drawing.Size(52, 19);
            this.rbHex.TabIndex = 6;
            this.rbHex.TabStop = true;
            this.rbHex.Text = "HEX";
            this.rbHex.UseVisualStyleBackColor = true;
            // 
            // rbAssic
            // 
            this.rbAssic.AutoSize = true;
            this.rbAssic.Location = new System.Drawing.Point(100, 46);
            this.rbAssic.Name = "rbAssic";
            this.rbAssic.Size = new System.Drawing.Size(68, 19);
            this.rbAssic.TabIndex = 6;
            this.rbAssic.Text = "ASCII";
            this.rbAssic.UseVisualStyleBackColor = true;
            // 
            // rbDouble
            // 
            this.rbDouble.AutoSize = true;
            this.rbDouble.Location = new System.Drawing.Point(160, 111);
            this.rbDouble.Name = "rbDouble";
            this.rbDouble.Size = new System.Drawing.Size(43, 19);
            this.rbDouble.TabIndex = 59;
            this.rbDouble.TabStop = true;
            this.rbDouble.Text = "偶";
            this.rbDouble.UseVisualStyleBackColor = true;
            // 
            // rbSingle
            // 
            this.rbSingle.AutoSize = true;
            this.rbSingle.Location = new System.Drawing.Point(103, 111);
            this.rbSingle.Name = "rbSingle";
            this.rbSingle.Size = new System.Drawing.Size(43, 19);
            this.rbSingle.TabIndex = 60;
            this.rbSingle.TabStop = true;
            this.rbSingle.Text = "奇";
            this.rbSingle.UseVisualStyleBackColor = true;
            // 
            // tbTimeout
            // 
            this.tbTimeout.Location = new System.Drawing.Point(103, 205);
            this.tbTimeout.Name = "tbTimeout";
            this.tbTimeout.Size = new System.Drawing.Size(100, 25);
            this.tbTimeout.TabIndex = 56;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-16, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 15);
            this.label6.TabIndex = 52;
            this.label6.Text = "超时时间（ms）";
            // 
            // tbStopbits
            // 
            this.tbStopbits.Location = new System.Drawing.Point(103, 172);
            this.tbStopbits.Name = "tbStopbits";
            this.tbStopbits.Size = new System.Drawing.Size(100, 25);
            this.tbStopbits.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 53;
            this.label5.Text = "停止位";
            // 
            // tbDatabits
            // 
            this.tbDatabits.Location = new System.Drawing.Point(103, 139);
            this.tbDatabits.Name = "tbDatabits";
            this.tbDatabits.Size = new System.Drawing.Size(100, 25);
            this.tbDatabits.TabIndex = 58;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 54;
            this.label4.Text = "数据位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 55;
            this.label3.Text = "奇偶校验";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 48);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 15);
            this.label7.TabIndex = 51;
            this.label7.Text = "串口";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 80);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 50;
            this.label8.Text = "波特率";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(123, 254);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 29);
            this.btnConnect.TabIndex = 49;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnFresh
            // 
            this.btnFresh.Location = new System.Drawing.Point(44, 254);
            this.btnFresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnFresh.Name = "btnFresh";
            this.btnFresh.Size = new System.Drawing.Size(53, 29);
            this.btnFresh.TabIndex = 47;
            this.btnFresh.Text = "刷新";
            this.btnFresh.UseVisualStyleBackColor = true;
            this.btnFresh.Click += new System.EventHandler(this.btnFresh_Click);
            // 
            // cbBaudrate
            // 
            this.cbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudrate.FormattingEnabled = true;
            this.cbBaudrate.Items.AddRange(new object[] {
            "2400",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBaudrate.Location = new System.Drawing.Point(103, 75);
            this.cbBaudrate.Margin = new System.Windows.Forms.Padding(4);
            this.cbBaudrate.Name = "cbBaudrate";
            this.cbBaudrate.Size = new System.Drawing.Size(100, 23);
            this.cbBaudrate.TabIndex = 45;
            // 
            // cbPorts
            // 
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Location = new System.Drawing.Point(103, 44);
            this.cbPorts.Margin = new System.Windows.Forms.Padding(4);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(100, 23);
            this.cbPorts.TabIndex = 46;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAssic);
            this.groupBox1.Controls.Add(this.rbHex);
            this.groupBox1.Location = new System.Drawing.Point(3, 358);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据格式";
            // 
            // FormSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 498);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rbDouble);
            this.Controls.Add(this.rbSingle);
            this.Controls.Add(this.tbTimeout);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbStopbits);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDatabits);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnFresh);
            this.Controls.Add(this.cbBaudrate);
            this.Controls.Add(this.cbPorts);
            this.Controls.Add(this.rtbHistory);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClearSend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSend);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSerial";
            this.Text = "串口调试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSerial_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClearSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox rtbHistory;
        private System.Windows.Forms.RadioButton rbHex;
        private System.Windows.Forms.RadioButton rbAssic;
        private System.Windows.Forms.RadioButton rbDouble;
        private System.Windows.Forms.RadioButton rbSingle;
        private System.Windows.Forms.TextBox tbTimeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbStopbits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDatabits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnFresh;
        private System.Windows.Forms.ComboBox cbBaudrate;
        private System.Windows.Forms.ComboBox cbPorts;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}