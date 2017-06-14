namespace MotionCalc
{
    partial class SetColorLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetColorLabel));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tbMaxArea = new System.Windows.Forms.TextBox();
            this.lbNetHeight = new System.Windows.Forms.Label();
            this.tbMinArea = new System.Windows.Forms.TextBox();
            this.gbArea = new System.Windows.Forms.GroupBox();
            this.gbRatio = new System.Windows.Forms.GroupBox();
            this.tbMaxRatio = new System.Windows.Forms.TextBox();
            this.tbMinRatio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbWhite = new System.Windows.Forms.RadioButton();
            this.rbBlack = new System.Windows.Forms.RadioButton();
            this.rbPurple = new System.Windows.Forms.RadioButton();
            this.rbGray = new System.Windows.Forms.RadioButton();
            this.rbYellow = new System.Windows.Forms.RadioButton();
            this.rbGreen = new System.Windows.Forms.RadioButton();
            this.rbBlue = new System.Windows.Forms.RadioButton();
            this.rbRed = new System.Windows.Forms.RadioButton();
            this.gbArea.SuspendLayout();
            this.gbRatio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(339, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(251, 211);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 30);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "确定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // tbMaxArea
            // 
            this.tbMaxArea.Location = new System.Drawing.Point(174, 34);
            this.tbMaxArea.Name = "tbMaxArea";
            this.tbMaxArea.Size = new System.Drawing.Size(60, 25);
            this.tbMaxArea.TabIndex = 7;
            // 
            // lbNetHeight
            // 
            this.lbNetHeight.AutoSize = true;
            this.lbNetHeight.Location = new System.Drawing.Point(146, 45);
            this.lbNetHeight.Name = "lbNetHeight";
            this.lbNetHeight.Size = new System.Drawing.Size(15, 15);
            this.lbNetHeight.TabIndex = 6;
            this.lbNetHeight.Text = "~";
            // 
            // tbMinArea
            // 
            this.tbMinArea.Location = new System.Drawing.Point(74, 34);
            this.tbMinArea.Name = "tbMinArea";
            this.tbMinArea.Size = new System.Drawing.Size(60, 25);
            this.tbMinArea.TabIndex = 8;
            // 
            // gbArea
            // 
            this.gbArea.Controls.Add(this.tbMaxArea);
            this.gbArea.Controls.Add(this.tbMinArea);
            this.gbArea.Controls.Add(this.lbNetHeight);
            this.gbArea.Location = new System.Drawing.Point(31, 40);
            this.gbArea.Name = "gbArea";
            this.gbArea.Size = new System.Drawing.Size(280, 72);
            this.gbArea.TabIndex = 9;
            this.gbArea.TabStop = false;
            this.gbArea.Text = "颜色标签成像面积范围";
            // 
            // gbRatio
            // 
            this.gbRatio.Controls.Add(this.tbMaxRatio);
            this.gbRatio.Controls.Add(this.tbMinRatio);
            this.gbRatio.Controls.Add(this.label1);
            this.gbRatio.Location = new System.Drawing.Point(344, 40);
            this.gbRatio.Name = "gbRatio";
            this.gbRatio.Size = new System.Drawing.Size(280, 72);
            this.gbRatio.TabIndex = 9;
            this.gbRatio.TabStop = false;
            this.gbRatio.Text = "颜色标签成像宽高比（W/H）";
            // 
            // tbMaxRatio
            // 
            this.tbMaxRatio.Location = new System.Drawing.Point(174, 34);
            this.tbMaxRatio.Name = "tbMaxRatio";
            this.tbMaxRatio.Size = new System.Drawing.Size(60, 25);
            this.tbMaxRatio.TabIndex = 7;
            // 
            // tbMinRatio
            // 
            this.tbMinRatio.Location = new System.Drawing.Point(74, 34);
            this.tbMinRatio.Name = "tbMinRatio";
            this.tbMinRatio.Size = new System.Drawing.Size(60, 25);
            this.tbMinRatio.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "~";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "-1  表示不限制";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbWhite);
            this.groupBox1.Controls.Add(this.rbBlack);
            this.groupBox1.Controls.Add(this.rbPurple);
            this.groupBox1.Controls.Add(this.rbGray);
            this.groupBox1.Controls.Add(this.rbYellow);
            this.groupBox1.Controls.Add(this.rbGreen);
            this.groupBox1.Controls.Add(this.rbBlue);
            this.groupBox1.Controls.Add(this.rbRed);
            this.groupBox1.Location = new System.Drawing.Point(31, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 65);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标签颜色";
            // 
            // rbWhite
            // 
            this.rbWhite.AutoSize = true;
            this.rbWhite.BackColor = System.Drawing.Color.White;
            this.rbWhite.Location = new System.Drawing.Point(446, 32);
            this.rbWhite.Name = "rbWhite";
            this.rbWhite.Size = new System.Drawing.Size(58, 19);
            this.rbWhite.TabIndex = 0;
            this.rbWhite.Text = "白色";
            this.rbWhite.UseVisualStyleBackColor = false;
            this.rbWhite.CheckedChanged += new System.EventHandler(this.rbColor_CheckedChanged);
            // 
            // rbBlack
            // 
            this.rbBlack.AutoSize = true;
            this.rbBlack.BackColor = System.Drawing.Color.Black;
            this.rbBlack.ForeColor = System.Drawing.SystemColors.Control;
            this.rbBlack.Location = new System.Drawing.Point(377, 32);
            this.rbBlack.Name = "rbBlack";
            this.rbBlack.Size = new System.Drawing.Size(58, 19);
            this.rbBlack.TabIndex = 0;
            this.rbBlack.Text = "黑色";
            this.rbBlack.UseVisualStyleBackColor = false;
            this.rbBlack.CheckedChanged += new System.EventHandler(this.rbColor_CheckedChanged);
            // 
            // rbPurple
            // 
            this.rbPurple.AutoSize = true;
            this.rbPurple.BackColor = System.Drawing.Color.Purple;
            this.rbPurple.ForeColor = System.Drawing.SystemColors.Control;
            this.rbPurple.Location = new System.Drawing.Point(308, 32);
            this.rbPurple.Name = "rbPurple";
            this.rbPurple.Size = new System.Drawing.Size(58, 19);
            this.rbPurple.TabIndex = 0;
            this.rbPurple.Text = "紫色";
            this.rbPurple.UseVisualStyleBackColor = false;
            this.rbPurple.CheckedChanged += new System.EventHandler(this.rbColor_CheckedChanged);
            // 
            // rbGray
            // 
            this.rbGray.AutoSize = true;
            this.rbGray.BackColor = System.Drawing.Color.Gray;
            this.rbGray.Location = new System.Drawing.Point(515, 32);
            this.rbGray.Name = "rbGray";
            this.rbGray.Size = new System.Drawing.Size(58, 19);
            this.rbGray.TabIndex = 0;
            this.rbGray.Text = "灰色";
            this.rbGray.UseVisualStyleBackColor = false;
            this.rbGray.CheckedChanged += new System.EventHandler(this.rbColor_CheckedChanged);
            // 
            // rbYellow
            // 
            this.rbYellow.AutoSize = true;
            this.rbYellow.BackColor = System.Drawing.Color.Yellow;
            this.rbYellow.Location = new System.Drawing.Point(239, 32);
            this.rbYellow.Name = "rbYellow";
            this.rbYellow.Size = new System.Drawing.Size(58, 19);
            this.rbYellow.TabIndex = 0;
            this.rbYellow.Text = "黄色";
            this.rbYellow.UseVisualStyleBackColor = false;
            this.rbYellow.CheckedChanged += new System.EventHandler(this.rbColor_CheckedChanged);
            // 
            // rbGreen
            // 
            this.rbGreen.AutoSize = true;
            this.rbGreen.BackColor = System.Drawing.Color.Green;
            this.rbGreen.ForeColor = System.Drawing.SystemColors.Control;
            this.rbGreen.Location = new System.Drawing.Point(170, 32);
            this.rbGreen.Name = "rbGreen";
            this.rbGreen.Size = new System.Drawing.Size(58, 19);
            this.rbGreen.TabIndex = 0;
            this.rbGreen.Text = "绿色";
            this.rbGreen.UseVisualStyleBackColor = false;
            this.rbGreen.CheckedChanged += new System.EventHandler(this.rbColor_CheckedChanged);
            // 
            // rbBlue
            // 
            this.rbBlue.AutoSize = true;
            this.rbBlue.BackColor = System.Drawing.Color.Blue;
            this.rbBlue.ForeColor = System.Drawing.SystemColors.Control;
            this.rbBlue.Location = new System.Drawing.Point(101, 32);
            this.rbBlue.Name = "rbBlue";
            this.rbBlue.Size = new System.Drawing.Size(58, 19);
            this.rbBlue.TabIndex = 0;
            this.rbBlue.Text = "蓝色";
            this.rbBlue.UseVisualStyleBackColor = false;
            this.rbBlue.CheckedChanged += new System.EventHandler(this.rbColor_CheckedChanged);
            // 
            // rbRed
            // 
            this.rbRed.AutoSize = true;
            this.rbRed.BackColor = System.Drawing.Color.Red;
            this.rbRed.Checked = true;
            this.rbRed.Location = new System.Drawing.Point(32, 32);
            this.rbRed.Name = "rbRed";
            this.rbRed.Size = new System.Drawing.Size(58, 19);
            this.rbRed.TabIndex = 0;
            this.rbRed.TabStop = true;
            this.rbRed.Text = "红色";
            this.rbRed.UseVisualStyleBackColor = false;
            this.rbRed.CheckedChanged += new System.EventHandler(this.rbColor_CheckedChanged);
            // 
            // SetColorLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 263);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbRatio);
            this.Controls.Add(this.gbArea);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetColorLabel";
            this.Text = "动作捕捉分析系统 - 颜色标签";
            this.gbArea.ResumeLayout(false);
            this.gbArea.PerformLayout();
            this.gbRatio.ResumeLayout(false);
            this.gbRatio.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox tbMaxArea;
        private System.Windows.Forms.Label lbNetHeight;
        private System.Windows.Forms.TextBox tbMinArea;
        private System.Windows.Forms.GroupBox gbArea;
        private System.Windows.Forms.GroupBox gbRatio;
        private System.Windows.Forms.TextBox tbMaxRatio;
        private System.Windows.Forms.TextBox tbMinRatio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbWhite;
        private System.Windows.Forms.RadioButton rbBlack;
        private System.Windows.Forms.RadioButton rbPurple;
        private System.Windows.Forms.RadioButton rbGray;
        private System.Windows.Forms.RadioButton rbYellow;
        private System.Windows.Forms.RadioButton rbGreen;
        private System.Windows.Forms.RadioButton rbBlue;
        private System.Windows.Forms.RadioButton rbRed;
    }
}