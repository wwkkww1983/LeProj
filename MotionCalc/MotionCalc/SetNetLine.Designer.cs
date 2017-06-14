namespace MotionCalc
{
    partial class SetNetLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetNetLine));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.lbWidth = new System.Windows.Forms.Label();
            this.lbNetWidth = new System.Windows.Forms.Label();
            this.tbNetWidth = new System.Windows.Forms.TextBox();
            this.lbNetHeight = new System.Windows.Forms.Label();
            this.tbNetHeight = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbColor = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ckbShow = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(214, 262);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(60, 262);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 30);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "确定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(69, 34);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(60, 25);
            this.tbWidth.TabIndex = 4;
            // 
            // lbWidth
            // 
            this.lbWidth.AutoSize = true;
            this.lbWidth.Location = new System.Drawing.Point(26, 39);
            this.lbWidth.Name = "lbWidth";
            this.lbWidth.Size = new System.Drawing.Size(37, 15);
            this.lbWidth.TabIndex = 3;
            this.lbWidth.Text = "粗细";
            // 
            // lbNetWidth
            // 
            this.lbNetWidth.AutoSize = true;
            this.lbNetWidth.Location = new System.Drawing.Point(41, 38);
            this.lbNetWidth.Name = "lbNetWidth";
            this.lbNetWidth.Size = new System.Drawing.Size(22, 15);
            this.lbNetWidth.TabIndex = 3;
            this.lbNetWidth.Text = "宽";
            // 
            // tbNetWidth
            // 
            this.tbNetWidth.Location = new System.Drawing.Point(69, 33);
            this.tbNetWidth.Name = "tbNetWidth";
            this.tbNetWidth.Size = new System.Drawing.Size(60, 25);
            this.tbNetWidth.TabIndex = 4;
            // 
            // lbNetHeight
            // 
            this.lbNetHeight.AutoSize = true;
            this.lbNetHeight.Location = new System.Drawing.Point(152, 38);
            this.lbNetHeight.Name = "lbNetHeight";
            this.lbNetHeight.Size = new System.Drawing.Size(22, 15);
            this.lbNetHeight.TabIndex = 3;
            this.lbNetHeight.Text = "高";
            // 
            // tbNetHeight
            // 
            this.tbNetHeight.Location = new System.Drawing.Point(180, 33);
            this.tbNetHeight.Name = "tbNetHeight";
            this.tbNetHeight.Size = new System.Drawing.Size(60, 25);
            this.tbNetHeight.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNetHeight);
            this.groupBox1.Controls.Add(this.lbNetWidth);
            this.groupBox1.Controls.Add(this.lbNetHeight);
            this.groupBox1.Controls.Add(this.tbNetWidth);
            this.groupBox1.Location = new System.Drawing.Point(49, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 72);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单个网格属性";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "颜色";
            // 
            // lbColor
            // 
            this.lbColor.Location = new System.Drawing.Point(177, 34);
            this.lbColor.Name = "lbColor";
            this.lbColor.Size = new System.Drawing.Size(63, 24);
            this.lbColor.TabIndex = 6;
            this.lbColor.Click += new System.EventHandler(this.lbColor_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbWidth);
            this.groupBox2.Controls.Add(this.lbColor);
            this.groupBox2.Controls.Add(this.lbWidth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(49, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 72);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线条属性";
            // 
            // ckbShow
            // 
            this.ckbShow.AutoSize = true;
            this.ckbShow.Location = new System.Drawing.Point(189, 28);
            this.ckbShow.Name = "ckbShow";
            this.ckbShow.Size = new System.Drawing.Size(89, 19);
            this.ckbShow.TabIndex = 8;
            this.ckbShow.Text = "显示网格";
            this.ckbShow.UseVisualStyleBackColor = true;
            // 
            // SetNetLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 318);
            this.Controls.Add(this.ckbShow);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetNetLine";
            this.Text = "动作捕捉分析系统 - 网格线";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label lbWidth;
        private System.Windows.Forms.Label lbNetWidth;
        private System.Windows.Forms.TextBox tbNetWidth;
        private System.Windows.Forms.Label lbNetHeight;
        private System.Windows.Forms.TextBox tbNetHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbColor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbShow;
    }
}