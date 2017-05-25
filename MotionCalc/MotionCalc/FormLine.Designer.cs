namespace MotionCalc
{
    partial class FormLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLine));
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbWhite = new System.Windows.Forms.RadioButton();
            this.rbBlace = new System.Windows.Forms.RadioButton();
            this.rbPurple = new System.Windows.Forms.RadioButton();
            this.rbYellow = new System.Windows.Forms.RadioButton();
            this.rbGreen = new System.Windows.Forms.RadioButton();
            this.rbBlue = new System.Windows.Forms.RadioButton();
            this.rbRed = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(141, 35);
            this.btnOpen.TabIndex = 8;
            this.btnOpen.Text = "打开视频";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbWhite);
            this.groupBox1.Controls.Add(this.rbBlace);
            this.groupBox1.Controls.Add(this.rbPurple);
            this.groupBox1.Controls.Add(this.rbYellow);
            this.groupBox1.Controls.Add(this.rbGreen);
            this.groupBox1.Controls.Add(this.rbBlue);
            this.groupBox1.Controls.Add(this.rbRed);
            this.groupBox1.Location = new System.Drawing.Point(331, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 45);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标签颜色";
            // 
            // rbWhite
            // 
            this.rbWhite.AutoSize = true;
            this.rbWhite.BackColor = System.Drawing.Color.White;
            this.rbWhite.Location = new System.Drawing.Point(555, 18);
            this.rbWhite.Name = "rbWhite";
            this.rbWhite.Size = new System.Drawing.Size(58, 19);
            this.rbWhite.TabIndex = 0;
            this.rbWhite.Text = "白色";
            this.rbWhite.UseVisualStyleBackColor = false;
            // 
            // rbBlace
            // 
            this.rbBlace.AutoSize = true;
            this.rbBlace.BackColor = System.Drawing.Color.Black;
            this.rbBlace.ForeColor = System.Drawing.SystemColors.Control;
            this.rbBlace.Location = new System.Drawing.Point(477, 18);
            this.rbBlace.Name = "rbBlace";
            this.rbBlace.Size = new System.Drawing.Size(58, 19);
            this.rbBlace.TabIndex = 0;
            this.rbBlace.Text = "黑色";
            this.rbBlace.UseVisualStyleBackColor = false;
            // 
            // rbPurple
            // 
            this.rbPurple.AutoSize = true;
            this.rbPurple.BackColor = System.Drawing.Color.Purple;
            this.rbPurple.ForeColor = System.Drawing.SystemColors.Control;
            this.rbPurple.Location = new System.Drawing.Point(399, 18);
            this.rbPurple.Name = "rbPurple";
            this.rbPurple.Size = new System.Drawing.Size(58, 19);
            this.rbPurple.TabIndex = 0;
            this.rbPurple.Text = "紫色";
            this.rbPurple.UseVisualStyleBackColor = false;
            // 
            // rbYellow
            // 
            this.rbYellow.AutoSize = true;
            this.rbYellow.BackColor = System.Drawing.Color.Yellow;
            this.rbYellow.Location = new System.Drawing.Point(321, 18);
            this.rbYellow.Name = "rbYellow";
            this.rbYellow.Size = new System.Drawing.Size(58, 19);
            this.rbYellow.TabIndex = 0;
            this.rbYellow.Text = "黄色";
            this.rbYellow.UseVisualStyleBackColor = false;
            // 
            // rbGreen
            // 
            this.rbGreen.AutoSize = true;
            this.rbGreen.BackColor = System.Drawing.Color.Green;
            this.rbGreen.ForeColor = System.Drawing.SystemColors.Control;
            this.rbGreen.Location = new System.Drawing.Point(243, 18);
            this.rbGreen.Name = "rbGreen";
            this.rbGreen.Size = new System.Drawing.Size(58, 19);
            this.rbGreen.TabIndex = 0;
            this.rbGreen.Text = "绿色";
            this.rbGreen.UseVisualStyleBackColor = false;
            // 
            // rbBlue
            // 
            this.rbBlue.AutoSize = true;
            this.rbBlue.BackColor = System.Drawing.Color.Blue;
            this.rbBlue.ForeColor = System.Drawing.SystemColors.Control;
            this.rbBlue.Location = new System.Drawing.Point(165, 18);
            this.rbBlue.Name = "rbBlue";
            this.rbBlue.Size = new System.Drawing.Size(58, 19);
            this.rbBlue.TabIndex = 0;
            this.rbBlue.Text = "蓝色";
            this.rbBlue.UseVisualStyleBackColor = false;
            // 
            // rbRed
            // 
            this.rbRed.AutoSize = true;
            this.rbRed.BackColor = System.Drawing.Color.Red;
            this.rbRed.Checked = true;
            this.rbRed.Location = new System.Drawing.Point(87, 18);
            this.rbRed.Name = "rbRed";
            this.rbRed.Size = new System.Drawing.Size(58, 19);
            this.rbRed.TabIndex = 0;
            this.rbRed.TabStop = true;
            this.rbRed.Text = "红色";
            this.rbRed.UseVisualStyleBackColor = false;
            // 
            // FormLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 935);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormLine";
            this.Text = "动作捕捉分析系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLine_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormLine_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbWhite;
        private System.Windows.Forms.RadioButton rbBlace;
        private System.Windows.Forms.RadioButton rbPurple;
        private System.Windows.Forms.RadioButton rbYellow;
        private System.Windows.Forms.RadioButton rbGreen;
        private System.Windows.Forms.RadioButton rbBlue;
        private System.Windows.Forms.RadioButton rbRed;
    }
}