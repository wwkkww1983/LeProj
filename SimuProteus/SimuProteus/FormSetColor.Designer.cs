namespace SimuProteus
{
    partial class FormSetColor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetColor));
            this.dialog = new System.Windows.Forms.ColorDialog();
            this.btnGnd = new System.Windows.Forms.Button();
            this.btnVcc = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnFoots = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGnd
            // 
            this.btnGnd.Location = new System.Drawing.Point(55, 50);
            this.btnGnd.Name = "btnGnd";
            this.btnGnd.Size = new System.Drawing.Size(91, 72);
            this.btnGnd.TabIndex = 1;
            this.btnGnd.Text = "GND";
            this.btnGnd.UseVisualStyleBackColor = true;
            this.btnGnd.Click += new System.EventHandler(this.btnGnd_Click);
            // 
            // btnVcc
            // 
            this.btnVcc.Location = new System.Drawing.Point(182, 50);
            this.btnVcc.Name = "btnVcc";
            this.btnVcc.Size = new System.Drawing.Size(91, 72);
            this.btnVcc.TabIndex = 1;
            this.btnVcc.Text = "VCC";
            this.btnVcc.UseVisualStyleBackColor = true;
            this.btnVcc.Click += new System.EventHandler(this.btnVcc_Click);
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(309, 50);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(91, 72);
            this.btnNone.TabIndex = 1;
            this.btnNone.Text = "悬空";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // btnFoots
            // 
            this.btnFoots.Location = new System.Drawing.Point(55, 166);
            this.btnFoots.Name = "btnFoots";
            this.btnFoots.Size = new System.Drawing.Size(91, 72);
            this.btnFoots.TabIndex = 1;
            this.btnFoots.Text = "管脚节点";
            this.btnFoots.UseVisualStyleBackColor = true;
            this.btnFoots.Click += new System.EventHandler(this.btnFoots_Click);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(182, 166);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(91, 72);
            this.btnLine.TabIndex = 1;
            this.btnLine.Text = "连线";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // FormSetColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 275);
            this.Controls.Add(this.btnNone);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.btnVcc);
            this.Controls.Add(this.btnFoots);
            this.Controls.Add(this.btnGnd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(469, 320);
            this.MinimumSize = new System.Drawing.Size(469, 320);
            this.Name = "FormSetColor";
            this.Text = "设置颜色";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog dialog;
        private System.Windows.Forms.Button btnGnd;
        private System.Windows.Forms.Button btnVcc;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnFoots;
        private System.Windows.Forms.Button btnLine;
    }
}