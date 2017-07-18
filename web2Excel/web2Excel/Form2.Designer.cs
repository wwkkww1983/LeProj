namespace web2Excel
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.dtpDeal = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pgbProj = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.tbToday = new System.Windows.Forms.TextBox();
            this.tbYesterday = new System.Windows.Forms.TextBox();
            this.fileToday = new System.Windows.Forms.OpenFileDialog();
            this.fileYesterday = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDeal
            // 
            this.dtpDeal.Location = new System.Drawing.Point(104, 146);
            this.dtpDeal.Margin = new System.Windows.Forms.Padding(4);
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
            this.pgbProj.Margin = new System.Windows.Forms.Padding(4);
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
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(376, 138);
            this.btnCompare.Margin = new System.Windows.Forms.Padding(4);
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
            this.btnToday.Margin = new System.Windows.Forms.Padding(4);
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
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(4);
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
            this.tbToday.Margin = new System.Windows.Forms.Padding(4);
            this.tbToday.Name = "tbToday";
            this.tbToday.Size = new System.Drawing.Size(420, 25);
            this.tbToday.TabIndex = 16;
            // 
            // tbYesterday
            // 
            this.tbYesterday.Location = new System.Drawing.Point(104, 85);
            this.tbYesterday.Margin = new System.Windows.Forms.Padding(4);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCompare);
            this.groupBox2.Controls.Add(this.dtpDeal);
            this.groupBox2.Controls.Add(this.btnToday);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnYesterday);
            this.groupBox2.Controls.Add(this.tbToday);
            this.groupBox2.Controls.Add(this.tbYesterday);
            this.groupBox2.Location = new System.Drawing.Point(28, 88);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(552, 208);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "对比数据";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 312);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pgbProj);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form2";
            this.Text = "住房信息对比";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDeal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pgbProj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.TextBox tbToday;
        private System.Windows.Forms.TextBox tbYesterday;
        private System.Windows.Forms.OpenFileDialog fileToday;
        private System.Windows.Forms.OpenFileDialog fileYesterday;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

