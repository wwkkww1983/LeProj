namespace WordInsert
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
            this.btnSource = new System.Windows.Forms.Button();
            this.tbSource = new System.Windows.Forms.TextBox();
            this.btnTarget = new System.Windows.Forms.Button();
            this.tbTarget = new System.Windows.Forms.TextBox();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.lbResult = new System.Windows.Forms.Label();
            this.btnExe = new System.Windows.Forms.Button();
            this.pbInsert = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar4 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar5 = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar6 = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.progressBar7 = new System.Windows.Forms.ProgressBar();
            this.label9 = new System.Windows.Forms.Label();
            this.progressBar8 = new System.Windows.Forms.ProgressBar();
            this.progressBar9 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(24, 19);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(111, 32);
            this.btnSource.TabIndex = 0;
            this.btnSource.Text = "初始文件夹";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // tbSource
            // 
            this.tbSource.Location = new System.Drawing.Point(136, 22);
            this.tbSource.Name = "tbSource";
            this.tbSource.Size = new System.Drawing.Size(600, 25);
            this.tbSource.TabIndex = 1;
            // 
            // btnTarget
            // 
            this.btnTarget.Location = new System.Drawing.Point(24, 70);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(111, 32);
            this.btnTarget.TabIndex = 0;
            this.btnTarget.Text = "目标文件夹";
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // tbTarget
            // 
            this.tbTarget.Location = new System.Drawing.Point(136, 73);
            this.tbTarget.Name = "tbTarget";
            this.tbTarget.Size = new System.Drawing.Size(600, 25);
            this.tbTarget.TabIndex = 1;
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(24, 276);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(813, 116);
            this.rtbResult.TabIndex = 2;
            this.rtbResult.Text = "";
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(21, 211);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(67, 15);
            this.lbResult.TabIndex = 3;
            this.lbResult.Text = "执行结果";
            // 
            // btnExe
            // 
            this.btnExe.Location = new System.Drawing.Point(742, 18);
            this.btnExe.Name = "btnExe";
            this.btnExe.Size = new System.Drawing.Size(95, 80);
            this.btnExe.TabIndex = 4;
            this.btnExe.Text = "生成";
            this.btnExe.UseVisualStyleBackColor = true;
            this.btnExe.Click += new System.EventHandler(this.btnExe_Click);
            // 
            // pbInsert
            // 
            this.pbInsert.Location = new System.Drawing.Point(197, 207);
            this.pbInsert.Name = "pbInsert";
            this.pbInsert.Size = new System.Drawing.Size(182, 23);
            this.pbInsert.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "执行结果";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(197, 178);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(182, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "执行结果";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(197, 149);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(182, 23);
            this.progressBar2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "执行结果";
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(197, 120);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(182, 23);
            this.progressBar3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "执行结果";
            // 
            // progressBar4
            // 
            this.progressBar4.Location = new System.Drawing.Point(197, 236);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(182, 23);
            this.progressBar4.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "执行结果";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(459, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "执行结果";
            // 
            // progressBar5
            // 
            this.progressBar5.Location = new System.Drawing.Point(652, 207);
            this.progressBar5.Name = "progressBar5";
            this.progressBar5.Size = new System.Drawing.Size(182, 23);
            this.progressBar5.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(459, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "执行结果";
            // 
            // progressBar6
            // 
            this.progressBar6.Location = new System.Drawing.Point(652, 178);
            this.progressBar6.Name = "progressBar6";
            this.progressBar6.Size = new System.Drawing.Size(182, 23);
            this.progressBar6.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(459, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "执行结果";
            // 
            // progressBar7
            // 
            this.progressBar7.Location = new System.Drawing.Point(652, 149);
            this.progressBar7.Name = "progressBar7";
            this.progressBar7.Size = new System.Drawing.Size(182, 23);
            this.progressBar7.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(459, 240);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "执行结果";
            // 
            // progressBar8
            // 
            this.progressBar8.Location = new System.Drawing.Point(652, 120);
            this.progressBar8.Name = "progressBar8";
            this.progressBar8.Size = new System.Drawing.Size(182, 23);
            this.progressBar8.TabIndex = 5;
            // 
            // progressBar9
            // 
            this.progressBar9.Location = new System.Drawing.Point(652, 236);
            this.progressBar9.Name = "progressBar9";
            this.progressBar9.Size = new System.Drawing.Size(182, 23);
            this.progressBar9.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 417);
            this.Controls.Add(this.progressBar9);
            this.Controls.Add(this.progressBar4);
            this.Controls.Add(this.progressBar8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.progressBar7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.progressBar5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pbInsert);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnExe);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.tbTarget);
            this.Controls.Add(this.tbSource);
            this.Controls.Add(this.btnTarget);
            this.Controls.Add(this.btnSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Word字符插入";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.TextBox tbSource;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.TextBox tbTarget;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.Button btnExe;
        private System.Windows.Forms.ProgressBar pbInsert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBar6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ProgressBar progressBar7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ProgressBar progressBar8;
        private System.Windows.Forms.ProgressBar progressBar9;
    }
}

