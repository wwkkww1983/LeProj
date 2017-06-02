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
            this.dgvMachine = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.k = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.l = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.timerCount = new System.Windows.Forms.Timer(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.btnBreak = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.timerWeb = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.tcHandler = new System.Windows.Forms.TabControl();
            this.tpAuto = new System.Windows.Forms.TabPage();
            this.btnAutoStop = new System.Windows.Forms.Button();
            this.btnAutoStart = new System.Windows.Forms.Button();
            this.tpUser = new System.Windows.Forms.TabPage();
            this.gbDevices = new System.Windows.Forms.GroupBox();
            this.gbSet = new System.Windows.Forms.GroupBox();
            this.rbUser = new System.Windows.Forms.RadioButton();
            this.rbAuto = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachine)).BeginInit();
            this.tcHandler.SuspendLayout();
            this.tpAuto.SuspendLayout();
            this.tpUser.SuspendLayout();
            this.gbDevices.SuspendLayout();
            this.gbSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(29, 20);
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
            this.btnStop.Location = new System.Drawing.Point(122, 20);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(87, 38);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "立即停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // dgvMachine
            // 
            this.dgvMachine.AllowUserToAddRows = false;
            this.dgvMachine.AllowUserToDeleteRows = false;
            this.dgvMachine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMachine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.k,
            this.l,
            this.d});
            this.dgvMachine.EnableHeadersVisualStyles = false;
            this.dgvMachine.Location = new System.Drawing.Point(3, 20);
            this.dgvMachine.Name = "dgvMachine";
            this.dgvMachine.ReadOnly = true;
            this.dgvMachine.RowHeadersVisible = false;
            this.dgvMachine.RowTemplate.Height = 27;
            this.dgvMachine.Size = new System.Drawing.Size(442, 92);
            this.dgvMachine.TabIndex = 0;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.Frozen = true;
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 88;
            // 
            // k
            // 
            this.k.DataPropertyName = "k";
            this.k.Frozen = true;
            this.k.HeaderText = "岭澳三号机";
            this.k.Name = "k";
            this.k.ReadOnly = true;
            this.k.Width = 110;
            // 
            // l
            // 
            this.l.DataPropertyName = "l";
            this.l.HeaderText = "岭澳一号机";
            this.l.Name = "l";
            this.l.ReadOnly = true;
            this.l.Width = 110;
            // 
            // d
            // 
            this.d.DataPropertyName = "d";
            this.d.HeaderText = "大亚湾一号机";
            this.d.Name = "d";
            this.d.ReadOnly = true;
            this.d.Width = 130;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "电话号码";
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(271, 75);
            this.tbPhone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(153, 25);
            this.tbPhone.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "录波时间(秒)";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(271, 116);
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
            this.btnRefresh.Location = new System.Drawing.Point(178, 33);
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
            this.cbPorts.Location = new System.Drawing.Point(271, 36);
            this.cbPorts.Margin = new System.Windows.Forms.Padding(4);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(152, 23);
            this.cbPorts.TabIndex = 69;
            // 
            // btnBreak
            // 
            this.btnBreak.Location = new System.Drawing.Point(333, 20);
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
            this.lbStatus.Location = new System.Drawing.Point(30, 128);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 15);
            this.lbStatus.TabIndex = 70;
            // 
            // timerWeb
            // 
            this.timerWeb.Enabled = true;
            this.timerWeb.Interval = 1000;
            this.timerWeb.Tick += new System.EventHandler(this.timerWeb_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(333, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 38);
            this.btnClose.TabIndex = 71;
            this.btnClose.Text = "关闭串口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tcHandler
            // 
            this.tcHandler.Controls.Add(this.tpAuto);
            this.tcHandler.Controls.Add(this.tpUser);
            this.tcHandler.Location = new System.Drawing.Point(14, 300);
            this.tcHandler.Name = "tcHandler";
            this.tcHandler.SelectedIndex = 0;
            this.tcHandler.Size = new System.Drawing.Size(478, 110);
            this.tcHandler.TabIndex = 73;
            this.tcHandler.SelectedIndexChanged += new System.EventHandler(this.tcHandler_SelectedIndexChanged);
            // 
            // tpAuto
            // 
            this.tpAuto.Controls.Add(this.btnAutoStop);
            this.tpAuto.Controls.Add(this.btnAutoStart);
            this.tpAuto.Controls.Add(this.btnClose);
            this.tpAuto.Location = new System.Drawing.Point(4, 25);
            this.tpAuto.Name = "tpAuto";
            this.tpAuto.Padding = new System.Windows.Forms.Padding(3);
            this.tpAuto.Size = new System.Drawing.Size(470, 81);
            this.tpAuto.TabIndex = 0;
            this.tpAuto.Text = "自动录波";
            this.tpAuto.UseVisualStyleBackColor = true;
            // 
            // btnAutoStop
            // 
            this.btnAutoStop.Location = new System.Drawing.Point(122, 20);
            this.btnAutoStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAutoStop.Name = "btnAutoStop";
            this.btnAutoStop.Size = new System.Drawing.Size(87, 38);
            this.btnAutoStop.TabIndex = 72;
            this.btnAutoStop.Text = "停止";
            this.btnAutoStop.UseVisualStyleBackColor = true;
            this.btnAutoStop.Click += new System.EventHandler(this.btnAutoStop_Click);
            // 
            // btnAutoStart
            // 
            this.btnAutoStart.Location = new System.Drawing.Point(29, 20);
            this.btnAutoStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAutoStart.Name = "btnAutoStart";
            this.btnAutoStart.Size = new System.Drawing.Size(87, 38);
            this.btnAutoStart.TabIndex = 73;
            this.btnAutoStart.Text = "启动";
            this.btnAutoStart.UseVisualStyleBackColor = true;
            this.btnAutoStart.Click += new System.EventHandler(this.btnAutoStart_Click);
            // 
            // tpUser
            // 
            this.tpUser.Controls.Add(this.btnStop);
            this.tpUser.Controls.Add(this.btnStart);
            this.tpUser.Controls.Add(this.btnBreak);
            this.tpUser.Location = new System.Drawing.Point(4, 25);
            this.tpUser.Name = "tpUser";
            this.tpUser.Padding = new System.Windows.Forms.Padding(3);
            this.tpUser.Size = new System.Drawing.Size(470, 81);
            this.tpUser.TabIndex = 1;
            this.tpUser.Text = "手动录波";
            this.tpUser.UseVisualStyleBackColor = true;
            // 
            // gbDevices
            // 
            this.gbDevices.Controls.Add(this.dgvMachine);
            this.gbDevices.Location = new System.Drawing.Point(14, 181);
            this.gbDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDevices.Name = "gbDevices";
            this.gbDevices.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDevices.Size = new System.Drawing.Size(474, 114);
            this.gbDevices.TabIndex = 1;
            this.gbDevices.TabStop = false;
            this.gbDevices.Text = "机组信息";
            // 
            // gbSet
            // 
            this.gbSet.Controls.Add(this.rbUser);
            this.gbSet.Controls.Add(this.rbAuto);
            this.gbSet.Controls.Add(this.lbStatus);
            this.gbSet.Controls.Add(this.tbPhone);
            this.gbSet.Controls.Add(this.btnRefresh);
            this.gbSet.Controls.Add(this.label2);
            this.gbSet.Controls.Add(this.label1);
            this.gbSet.Controls.Add(this.tbTime);
            this.gbSet.Controls.Add(this.cbPorts);
            this.gbSet.Location = new System.Drawing.Point(14, 12);
            this.gbSet.Name = "gbSet";
            this.gbSet.Size = new System.Drawing.Size(474, 164);
            this.gbSet.TabIndex = 74;
            this.gbSet.TabStop = false;
            this.gbSet.Text = "信息设置";
            // 
            // rbUser
            // 
            this.rbUser.AutoSize = true;
            this.rbUser.Location = new System.Drawing.Point(20, 81);
            this.rbUser.Name = "rbUser";
            this.rbUser.Size = new System.Drawing.Size(88, 19);
            this.rbUser.TabIndex = 71;
            this.rbUser.Text = "手动录波";
            this.rbUser.UseVisualStyleBackColor = true;
            this.rbUser.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbAuto
            // 
            this.rbAuto.AutoSize = true;
            this.rbAuto.Location = new System.Drawing.Point(20, 46);
            this.rbAuto.Name = "rbAuto";
            this.rbAuto.Size = new System.Drawing.Size(88, 19);
            this.rbAuto.TabIndex = 70;
            this.rbAuto.Text = "自动录波";
            this.rbAuto.UseVisualStyleBackColor = true;
            this.rbAuto.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 422);
            this.Controls.Add(this.gbDevices);
            this.Controls.Add(this.gbSet);
            this.Controls.Add(this.tcHandler);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(521, 487);
            this.MinimumSize = new System.Drawing.Size(521, 467);
            this.Name = "Form1";
            this.Text = "基于网络控制的示波器远程自动录波系统";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachine)).EndInit();
            this.tcHandler.ResumeLayout(false);
            this.tpAuto.ResumeLayout(false);
            this.tpUser.ResumeLayout(false);
            this.gbDevices.ResumeLayout(false);
            this.gbSet.ResumeLayout(false);
            this.gbSet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Timer timerCount;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cbPorts;
        private System.Windows.Forms.Button btnBreak;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Timer timerWeb;
        private System.Windows.Forms.DataGridView dgvMachine;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tcHandler;
        private System.Windows.Forms.TabPage tpAuto;
        private System.Windows.Forms.TabPage tpUser;
        private System.Windows.Forms.GroupBox gbDevices;
        private System.Windows.Forms.GroupBox gbSet;
        private System.Windows.Forms.RadioButton rbUser;
        private System.Windows.Forms.RadioButton rbAuto;
        private System.Windows.Forms.Button btnAutoStop;
        private System.Windows.Forms.Button btnAutoStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn k;
        private System.Windows.Forms.DataGridViewTextBoxColumn l;
        private System.Windows.Forms.DataGridViewTextBoxColumn d;
    }
}

