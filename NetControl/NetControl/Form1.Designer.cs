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
            this.dgvMachine = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.k = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.l = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachine)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(257, 42);
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
            this.btnStop.Location = new System.Drawing.Point(257, 82);
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
            this.gbDevices.Controls.Add(this.dgvMachine);
            this.gbDevices.Location = new System.Drawing.Point(21, 12);
            this.gbDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDevices.Name = "gbDevices";
            this.gbDevices.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDevices.Size = new System.Drawing.Size(448, 114);
            this.gbDevices.TabIndex = 1;
            this.gbDevices.TabStop = false;
            this.gbDevices.Text = "机组信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "电话号码";
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(83, 45);
            this.tbPhone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(153, 25);
            this.tbPhone.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "录波时间(秒)";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(112, 91);
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
            this.btnBreak.Location = new System.Drawing.Point(350, 82);
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
            this.lbStatus.Location = new System.Drawing.Point(16, 344);
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
            this.dgvMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMachine.Location = new System.Drawing.Point(3, 20);
            this.dgvMachine.Name = "dgvMachine";
            this.dgvMachine.ReadOnly = true;
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
            this.name.Width = 90;
            // 
            // k
            // 
            this.k.DataPropertyName = "k";
            this.k.Frozen = true;
            this.k.HeaderText = "K";
            this.k.Name = "k";
            this.k.ReadOnly = true;
            // 
            // l
            // 
            this.l.DataPropertyName = "l";
            this.l.HeaderText = "L";
            this.l.Name = "l";
            this.l.ReadOnly = true;
            // 
            // d
            // 
            this.d.DataPropertyName = "d";
            this.d.HeaderText = "D";
            this.d.Name = "d";
            this.d.ReadOnly = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(278, 138);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(87, 41);
            this.btnOpen.TabIndex = 71;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.btnBreak);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbTime);
            this.groupBox1.Controls.Add(this.tbPhone);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(24, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 146);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "手动录波";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 683);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.cbPorts);
            this.Controls.Add(this.gbDevices);
            this.Controls.Add(this.btnRefresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "基于网络控制的示波器远程自动录波系统";
            this.gbDevices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachine)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox gbDevices;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn k;
        private System.Windows.Forms.DataGridViewTextBoxColumn l;
        private System.Windows.Forms.DataGridViewTextBoxColumn d;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

