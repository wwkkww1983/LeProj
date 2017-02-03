namespace web2Excel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.dtpDeal = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pgbProj = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnFolder = new System.Windows.Forms.Button();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.lbItemCount = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbClose = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbEnd = new System.Windows.Forms.TextBox();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.tbToday = new System.Windows.Forms.TextBox();
            this.tbYesterday = new System.Windows.Forms.TextBox();
            this.fileToday = new System.Windows.Forms.OpenFileDialog();
            this.fileYesterday = new System.Windows.Forms.OpenFileDialog();
            this.gbHouseType = new System.Windows.Forms.GroupBox();
            this.rbCompare = new System.Windows.Forms.RadioButton();
            this.rbSell = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.gbHouseType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(376, 136);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 45);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始读取";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dtpDeal
            // 
            this.dtpDeal.Location = new System.Drawing.Point(104, 146);
            this.dtpDeal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpDeal.Name = "dtpDeal";
            this.dtpDeal.Size = new System.Drawing.Size(208, 25);
            this.dtpDeal.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 150);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "成交日期";
            // 
            // pgbProj
            // 
            this.pgbProj.Location = new System.Drawing.Point(104, 34);
            this.pgbProj.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pgbProj.Name = "pgbProj";
            this.pgbProj.Size = new System.Drawing.Size(476, 29);
            this.pgbProj.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "完成进度";
            // 
            // folderDialog
            // 
            this.folderDialog.Description = "请选择Excel存放路径";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(24, 34);
            this.btnFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(71, 29);
            this.btnFolder.TabIndex = 5;
            this.btnFolder.Text = "目录";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(104, 35);
            this.tbFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(420, 25);
            this.tbFolder.TabIndex = 6;
            // 
            // lbItemCount
            // 
            this.lbItemCount.AutoSize = true;
            this.lbItemCount.Location = new System.Drawing.Point(265, 151);
            this.lbItemCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbItemCount.Name = "lbItemCount";
            this.lbItemCount.Size = new System.Drawing.Size(47, 15);
            this.lbItemCount.TabIndex = 7;
            this.lbItemCount.Text = "00000";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(453, 84);
            this.tbTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(69, 25);
            this.tbTime.TabIndex = 8;
            this.tbTime.Leave += new System.EventHandler(this.tbTime_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(316, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "网页读取间隔(秒)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(203, 151);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "记录数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbClose);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbEnd);
            this.groupBox1.Controls.Add(this.tbStart);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbFolder);
            this.groupBox1.Controls.Add(this.tbTime);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbItemCount);
            this.groupBox1.Location = new System.Drawing.Point(28, 174);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(552, 204);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "抓取数据";
            // 
            // cbClose
            // 
            this.cbClose.AutoSize = true;
            this.cbClose.Location = new System.Drawing.Point(47, 149);
            this.cbClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(104, 19);
            this.cbClose.TabIndex = 13;
            this.cbClose.Text = "完成后关机";
            this.cbClose.UseVisualStyleBackColor = true;
            this.cbClose.CheckedChanged += new System.EventHandler(this.cbClose_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(172, 98);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "~";
            // 
            // tbEnd
            // 
            this.tbEnd.Location = new System.Drawing.Point(196, 85);
            this.tbEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbEnd.Name = "tbEnd";
            this.tbEnd.Size = new System.Drawing.Size(55, 25);
            this.tbEnd.TabIndex = 11;
            this.tbEnd.TextChanged += new System.EventHandler(this.tbEnd_TextChanged);
            // 
            // tbStart
            // 
            this.tbStart.Location = new System.Drawing.Point(104, 85);
            this.tbStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(55, 25);
            this.tbStart.TabIndex = 11;
            this.tbStart.TextChanged += new System.EventHandler(this.tbStart_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "抓取页码";
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(376, 138);
            this.btnCompare.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(101, 45);
            this.btnCompare.TabIndex = 13;
            this.btnCompare.Text = "开始对比";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(24, 39);
            this.btnToday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(71, 29);
            this.btnToday.TabIndex = 14;
            this.btnToday.Text = "今天";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.Location = new System.Drawing.Point(24, 82);
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(71, 29);
            this.btnYesterday.TabIndex = 15;
            this.btnYesterday.Text = "昨天";
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // tbToday
            // 
            this.tbToday.Location = new System.Drawing.Point(104, 41);
            this.tbToday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbToday.Name = "tbToday";
            this.tbToday.Size = new System.Drawing.Size(420, 25);
            this.tbToday.TabIndex = 16;
            // 
            // tbYesterday
            // 
            this.tbYesterday.Location = new System.Drawing.Point(104, 85);
            this.tbYesterday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbYesterday.Name = "tbYesterday";
            this.tbYesterday.Size = new System.Drawing.Size(420, 25);
            this.tbYesterday.TabIndex = 17;
            // 
            // fileToday
            // 
            this.fileToday.FileName = "openFileDialog1";
            this.fileToday.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            // 
            // fileYesterday
            // 
            this.fileYesterday.FileName = "openFileDialog1";
            this.fileYesterday.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            // 
            // gbHouseType
            // 
            this.gbHouseType.Controls.Add(this.rbCompare);
            this.gbHouseType.Controls.Add(this.rbSell);
            this.gbHouseType.Controls.Add(this.rbAll);
            this.gbHouseType.Controls.Add(this.rbNone);
            this.gbHouseType.Location = new System.Drawing.Point(28, 85);
            this.gbHouseType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbHouseType.Name = "gbHouseType";
            this.gbHouseType.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbHouseType.Size = new System.Drawing.Size(552, 81);
            this.gbHouseType.TabIndex = 18;
            this.gbHouseType.TabStop = false;
            this.gbHouseType.Text = "户型信息";
            // 
            // rbCompare
            // 
            this.rbCompare.AutoSize = true;
            this.rbCompare.Location = new System.Drawing.Point(427, 40);
            this.rbCompare.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbCompare.Name = "rbCompare";
            this.rbCompare.Size = new System.Drawing.Size(103, 19);
            this.rbCompare.TabIndex = 0;
            this.rbCompare.TabStop = true;
            this.rbCompare.Tag = "COMPARE";
            this.rbCompare.Text = "查对比结果";
            this.rbCompare.UseVisualStyleBackColor = true;
            this.rbCompare.CheckedChanged += new System.EventHandler(this.rbHouseType_CheckedChanged);
            // 
            // rbSell
            // 
            this.rbSell.AutoSize = true;
            this.rbSell.Location = new System.Drawing.Point(300, 40);
            this.rbSell.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbSell.Name = "rbSell";
            this.rbSell.Size = new System.Drawing.Size(73, 19);
            this.rbSell.TabIndex = 0;
            this.rbSell.TabStop = true;
            this.rbSell.Tag = "CANSELL";
            this.rbSell.Text = "查可售";
            this.rbSell.UseVisualStyleBackColor = true;
            this.rbSell.CheckedChanged += new System.EventHandler(this.rbHouseType_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(157, 40);
            this.rbAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(88, 19);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Tag = "ALL";
            this.rbAll.Text = "全部查看";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbHouseType_CheckedChanged);
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(47, 40);
            this.rbNone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(58, 19);
            this.rbNone.TabIndex = 0;
            this.rbNone.TabStop = true;
            this.rbNone.Tag = "NONE";
            this.rbNone.Text = "不查";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbHouseType_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCompare);
            this.groupBox2.Controls.Add(this.dtpDeal);
            this.groupBox2.Controls.Add(this.btnToday);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnYesterday);
            this.groupBox2.Controls.Add(this.tbToday);
            this.groupBox2.Controls.Add(this.tbYesterday);
            this.groupBox2.Location = new System.Drawing.Point(28, 385);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(552, 208);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "对比数据";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 598);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbHouseType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pgbProj);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "住房信息获取";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbHouseType.ResumeLayout(false);
            this.gbHouseType.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DateTimePicker dtpDeal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pgbProj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Label lbItemCount;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.TextBox tbToday;
        private System.Windows.Forms.TextBox tbYesterday;
        private System.Windows.Forms.OpenFileDialog fileToday;
        private System.Windows.Forms.OpenFileDialog fileYesterday;
        private System.Windows.Forms.GroupBox gbHouseType;
        private System.Windows.Forms.RadioButton rbCompare;
        private System.Windows.Forms.RadioButton rbSell;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.TextBox tbEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbClose;
    }
}

