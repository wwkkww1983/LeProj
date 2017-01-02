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
            this.tbSource.Size = new System.Drawing.Size(706, 25);
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
            this.tbTarget.Size = new System.Drawing.Size(706, 25);
            this.tbTarget.TabIndex = 1;
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(24, 146);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(828, 449);
            this.rtbResult.TabIndex = 2;
            this.rtbResult.Text = "";
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(775, 117);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(67, 15);
            this.lbResult.TabIndex = 3;
            this.lbResult.Text = "执行结果";
            // 
            // btnExe
            // 
            this.btnExe.Location = new System.Drawing.Point(152, 104);
            this.btnExe.Name = "btnExe";
            this.btnExe.Size = new System.Drawing.Size(75, 36);
            this.btnExe.TabIndex = 4;
            this.btnExe.Text = "生成";
            this.btnExe.UseVisualStyleBackColor = true;
            this.btnExe.Click += new System.EventHandler(this.btnExe_Click);
            // 
            // pbInsert
            // 
            this.pbInsert.Location = new System.Drawing.Point(292, 113);
            this.pbInsert.Name = "pbInsert";
            this.pbInsert.Size = new System.Drawing.Size(428, 23);
            this.pbInsert.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 607);
            this.Controls.Add(this.pbInsert);
            this.Controls.Add(this.btnExe);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.tbTarget);
            this.Controls.Add(this.tbSource);
            this.Controls.Add(this.btnTarget);
            this.Controls.Add(this.btnSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(882, 652);
            this.MinimumSize = new System.Drawing.Size(882, 652);
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
    }
}

