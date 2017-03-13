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
            this.SuspendLayout();
            // 
            // btnImg
            // 
            this.btnImg.Location = new System.Drawing.Point(25, 34);
            this.btnImg.Name = "btnImg";
            this.btnImg.Size = new System.Drawing.Size(134, 23);
            this.btnImg.TabIndex = 0;
            this.btnImg.Text = "图片路径";
            this.btnImg.UseVisualStyleBackColor = true;
            this.btnImg.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(165, 35);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(269, 25);
            this.tbPath.TabIndex = 1;
            // 
            // FormPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 104);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btnImg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(497, 149);
            this.MinimumSize = new System.Drawing.Size(497, 149);
            this.Name = "FormPath";
            this.Text = "文件路径";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImg;
        private System.Windows.Forms.TextBox tbPath;
    }
}