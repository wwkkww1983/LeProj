namespace NetControl
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.gbDevices = new System.Windows.Forms.GroupBox();
            this.rbSix = new System.Windows.Forms.RadioButton();
            this.rbFive = new System.Windows.Forms.RadioButton();
            this.rbThree = new System.Windows.Forms.RadioButton();
            this.rbFour = new System.Windows.Forms.RadioButton();
            this.rbTwo = new System.Windows.Forms.RadioButton();
            this.rbOne = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.timerCount = new System.Windows.Forms.Timer(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.btnBreak = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.gbDevices.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(313, 140);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(87, 38);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始录波";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(313, 188);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(87, 38);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "立即停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // gbDevices
            // 
            this.gbDevices.Controls.Add(this.rbSix);
            this.gbDevices.Controls.Add(this.rbFive);
            this.gbDevices.Controls.Add(this.rbThree);
            this.gbDevices.Controls.Add(this.rbFour);
            this.gbDevices.Controls.Add(this.rbTwo);
            this.gbDevices.Controls.Add(this.rbOne);
            this.gbDevices.Location = new System.Drawing.Point(12, 12);
            this.gbDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDevices.Name = "gbDevices";
            this.gbDevices.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDevices.Size = new System.Drawing.Size(413, 114);
            this.gbDevices.TabIndex = 1;
            this.gbDevices.TabStop = false;
            this.gbDevices.Text = "机组信息";
            // 
            // rbSix
            // 
            this.rbSix.AutoSize = true;
            this.rbSix.Location = new System.Drawing.Point(301, 80);
            this.rbSix.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbSix.Name = "rbSix";
            this.rbSix.Size = new System.Drawing.Size(51, 19);
            this.rbSix.TabIndex = 0;
            this.rbSix.TabStop = true;
            this.rbSix.Text = "6号";
            this.rbSix.UseVisualStyleBackColor = true;
            // 
            // rbFive
            // 
            this.rbFive.AutoSize = true;
            this.rbFive.Location = new System.Drawing.Point(187, 80);
            this.rbFive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbFive.Name = "rbFive";
            this.rbFive.Size = new System.Drawing.Size(51, 19);
            this.rbFive.TabIndex = 0;
            this.rbFive.TabStop = true;
            this.rbFive.Text = "5号";
            this.rbFive.UseVisualStyleBackColor = true;
            // 
            // rbThree
            // 
            this.rbThree.AutoSize = true;
            this.rbThree.Location = new System.Drawing.Point(301, 44);
            this.rbThree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbThree.Name = "rbThree";
            this.rbThree.Size = new System.Drawing.Size(51, 19);
            this.rbThree.TabIndex = 0;
            this.rbThree.TabStop = true;
            this.rbThree.Text = "3号";
            this.rbThree.UseVisualStyleBackColor = true;
            // 
            // rbFour
            // 
            this.rbFour.AutoSize = true;
            this.rbFour.Location = new System.Drawing.Point(73, 80);
            this.rbFour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbFour.Name = "rbFour";
            this.rbFour.Size = new System.Drawing.Size(51, 19);
            this.rbFour.TabIndex = 0;
            this.rbFour.TabStop = true;
            this.rbFour.Text = "4号";
            this.rbFour.UseVisualStyleBackColor = true;
            // 
            // rbTwo
            // 
            this.rbTwo.AutoSize = true;
            this.rbTwo.Location = new System.Drawing.Point(187, 44);
            this.rbTwo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbTwo.Name = "rbTwo";
            this.rbTwo.Size = new System.Drawing.Size(51, 19);
            this.rbTwo.TabIndex = 0;
            this.rbTwo.TabStop = true;
            this.rbTwo.Text = "2号";
            this.rbTwo.UseVisualStyleBackColor = true;
            // 
            // rbOne
            // 
            this.rbOne.AutoSize = true;
            this.rbOne.Location = new System.Drawing.Point(73, 44);
            this.rbOne.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbOne.Name = "rbOne";
            this.rbOne.Size = new System.Drawing.Size(51, 19);
            this.rbOne.TabIndex = 0;
            this.rbOne.TabStop = true;
            this.rbOne.Text = "1号";
            this.rbOne.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "电话号码";
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(115, 199);
            this.tbPhone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(153, 25);
            this.tbPhone.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "录波时间(秒)";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(144, 245);
            this.tbTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(123, 25);
            this.tbTime.TabIndex = 3;
            this.tbTime.Text = "0";
            // 
            // timerCount
            // 
            this.timerCount.Interval = 1000;
            this.timerCount.Tick += new System.EventHandler(this.timerCount_Tick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(21, 145);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(87, 28);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "刷新串口";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cbPorts
            // 
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Location = new System.Drawing.Point(115, 148);
            this.cbPorts.Margin = new System.Windows.Forms.Padding(4);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(152, 23);
            this.cbPorts.TabIndex = 69;
            // 
            // btnBreak
            // 
            this.btnBreak.Location = new System.Drawing.Point(313, 236);
            this.btnBreak.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBreak.Name = "btnBreak";
            this.btnBreak.Size = new System.Drawing.Size(87, 38);
            this.btnBreak.TabIndex = 0;
            this.btnBreak.Text = "挂断电话";
            this.btnBreak.UseVisualStyleBackColor = true;
            this.btnBreak.Click += new System.EventHandler(this.btnBreak_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(19, 294);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 15);
            this.lbStatus.TabIndex = 70;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 325);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.cbPorts);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPhone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbDevices);
            this.Controls.Add(this.btnBreak);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "基于网络控制的示波器远程自动录波系统";
            this.gbDevices.ResumeLayout(false);
            this.gbDevices.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox gbDevices;
        private System.Windows.Forms.RadioButton rbOne;
        private System.Windows.Forms.RadioButton rbSix;
        private System.Windows.Forms.RadioButton rbFive;
        private System.Windows.Forms.RadioButton rbThree;
        private System.Windows.Forms.RadioButton rbFour;
        private System.Windows.Forms.RadioButton rbTwo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Timer timerCount;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cbPorts;
        private System.Windows.Forms.Button btnBreak;
        private System.Windows.Forms.Label lbStatus;
    }
}

