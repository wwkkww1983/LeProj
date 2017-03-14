/// <summary>
/// copyright:  Zac (suoxd123@126.com)
/// 2017-03-14
/// </summary>
namespace PcVedio
{
    partial class FormPath
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPath));
            this.btnImg = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnVideoPath = new System.Windows.Forms.Button();
            this.tbVideoPath = new System.Windows.Forms.TextBox();
            this.btnVideoOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImg
            // 
            this.btnImg.Location = new System.Drawing.Point(6, 34);
            this.btnImg.Name = "btnImg";
            this.btnImg.Size = new System.Drawing.Size(113, 23);
            this.btnImg.TabIndex = 0;
            this.btnImg.Text = "图片路径";
            this.btnImg.UseVisualStyleBackColor = true;
            this.btnImg.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(128, 35);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(269, 25);
            this.tbPath.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(403, 37);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(64, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnVideoPath
            // 
            this.btnVideoPath.Location = new System.Drawing.Point(6, 79);
            this.btnVideoPath.Name = "btnVideoPath";
            this.btnVideoPath.Size = new System.Drawing.Size(113, 23);
            this.btnVideoPath.TabIndex = 0;
            this.btnVideoPath.Text = "录像路径";
            this.btnVideoPath.UseVisualStyleBackColor = true;
            this.btnVideoPath.Click += new System.EventHandler(this.btnVideoPath_Click);
            // 
            // tbVideoPath
            // 
            this.tbVideoPath.Location = new System.Drawing.Point(128, 80);
            this.tbVideoPath.Name = "tbVideoPath";
            this.tbVideoPath.Size = new System.Drawing.Size(269, 25);
            this.tbVideoPath.TabIndex = 1;
            // 
            // btnVideoOpen
            // 
            this.btnVideoOpen.Location = new System.Drawing.Point(403, 82);
            this.btnVideoOpen.Name = "btnVideoOpen";
            this.btnVideoOpen.Size = new System.Drawing.Size(64, 23);
            this.btnVideoOpen.TabIndex = 2;
            this.btnVideoOpen.Text = "打开";
            this.btnVideoOpen.UseVisualStyleBackColor = true;
            this.btnVideoOpen.Click += new System.EventHandler(this.btnVideoOpen_Click);
            // 
            // FormPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 143);
            this.Controls.Add(this.btnVideoOpen);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.tbVideoPath);
            this.Controls.Add(this.btnVideoPath);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btnImg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(497, 149);
            this.Name = "FormPath";
            this.Text = "文件路径";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImg;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnVideoPath;
        private System.Windows.Forms.TextBox tbVideoPath;
        private System.Windows.Forms.Button btnVideoOpen;
    }
}