namespace MotionCalc
{
    partial class SetRecogPoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetRecogPoint));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.lbColorLine = new System.Windows.Forms.Label();
            this.lbWidth = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbRadius = new System.Windows.Forms.TextBox();
            this.lbColorPoint = new System.Windows.Forms.Label();
            this.lbRadius = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbRadiusLine = new System.Windows.Forms.Label();
            this.tbRadiusLine = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(202, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(48, 242);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 30);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "确定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbRadiusLine);
            this.groupBox2.Controls.Add(this.tbWidth);
            this.groupBox2.Controls.Add(this.lbRadiusLine);
            this.groupBox2.Controls.Add(this.lbColorLine);
            this.groupBox2.Controls.Add(this.lbWidth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(37, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 99);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线条属性";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(71, 62);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(60, 25);
            this.tbWidth.TabIndex = 4;
            // 
            // lbColorLine
            // 
            this.lbColorLine.Location = new System.Drawing.Point(177, 31);
            this.lbColorLine.Name = "lbColorLine";
            this.lbColorLine.Size = new System.Drawing.Size(63, 24);
            this.lbColorLine.TabIndex = 6;
            this.lbColorLine.Click += new System.EventHandler(this.lbColorLine_Click);
            // 
            // lbWidth
            // 
            this.lbWidth.AutoSize = true;
            this.lbWidth.Location = new System.Drawing.Point(28, 67);
            this.lbWidth.Name = "lbWidth";
            this.lbWidth.Size = new System.Drawing.Size(37, 15);
            this.lbWidth.TabIndex = 3;
            this.lbWidth.Text = "粗细";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "颜色";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbRadius);
            this.groupBox1.Controls.Add(this.lbColorPoint);
            this.groupBox1.Controls.Add(this.lbRadius);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(37, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 72);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标识点属性";
            // 
            // tbRadius
            // 
            this.tbRadius.Location = new System.Drawing.Point(69, 31);
            this.tbRadius.Name = "tbRadius";
            this.tbRadius.Size = new System.Drawing.Size(60, 25);
            this.tbRadius.TabIndex = 9;
            // 
            // lbColorPoint
            // 
            this.lbColorPoint.Location = new System.Drawing.Point(177, 31);
            this.lbColorPoint.Name = "lbColorPoint";
            this.lbColorPoint.Size = new System.Drawing.Size(63, 24);
            this.lbColorPoint.TabIndex = 10;
            this.lbColorPoint.Click += new System.EventHandler(this.lbColorPoint_Click);
            // 
            // lbRadius
            // 
            this.lbRadius.AutoSize = true;
            this.lbRadius.Location = new System.Drawing.Point(26, 36);
            this.lbRadius.Name = "lbRadius";
            this.lbRadius.Size = new System.Drawing.Size(37, 15);
            this.lbRadius.TabIndex = 7;
            this.lbRadius.Text = "半径";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "颜色";
            // 
            // lbRadiusLine
            // 
            this.lbRadiusLine.AutoSize = true;
            this.lbRadiusLine.Location = new System.Drawing.Point(28, 36);
            this.lbRadiusLine.Name = "lbRadiusLine";
            this.lbRadiusLine.Size = new System.Drawing.Size(37, 15);
            this.lbRadiusLine.TabIndex = 3;
            this.lbRadiusLine.Text = "半径";
            // 
            // tbRadiusLine
            // 
            this.tbRadiusLine.Location = new System.Drawing.Point(71, 31);
            this.tbRadiusLine.Name = "tbRadiusLine";
            this.tbRadiusLine.Size = new System.Drawing.Size(60, 25);
            this.tbRadiusLine.TabIndex = 4;
            // 
            // SetRecogPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 290);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetRecogPoint";
            this.Text = "动作捕捉分析系统 - 标识点";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label lbColorLine;
        private System.Windows.Forms.Label lbWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbRadius;
        private System.Windows.Forms.Label lbColorPoint;
        private System.Windows.Forms.Label lbRadius;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRadiusLine;
        private System.Windows.Forms.Label lbRadiusLine;
    }
}