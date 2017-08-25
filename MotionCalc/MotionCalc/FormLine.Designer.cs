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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLine));
            this.imgBox = new Emgu.CV.UI.ImageBox();
            this.lbAngle = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setNetLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setRecogPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setRecogLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setColorLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSBarVideo = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgBox
            // 
            this.imgBox.Location = new System.Drawing.Point(12, 31);
            this.imgBox.Name = "imgBox";
            this.imgBox.Size = new System.Drawing.Size(973, 748);
            this.imgBox.TabIndex = 2;
            this.imgBox.TabStop = false;
            // 
            // lbAngle
            // 
            this.lbAngle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAngle.Location = new System.Drawing.Point(886, -1);
            this.lbAngle.Name = "lbAngle";
            this.lbAngle.Size = new System.Drawing.Size(96, 29);
            this.lbAngle.TabIndex = 10;
            this.lbAngle.Text = "夹角";
            this.lbAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openVideoToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.saveImageToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(993, 28);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openVideoToolStripMenuItem
            // 
            this.openVideoToolStripMenuItem.Name = "openVideoToolStripMenuItem";
            this.openVideoToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.openVideoToolStripMenuItem.Text = "打开文件";
            this.openVideoToolStripMenuItem.Click += new System.EventHandler(this.openVideoToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setNetLineToolStripMenuItem,
            this.setRecogPointToolStripMenuItem,
            this.setRecogLineToolStripMenuItem,
            this.setColorLabelToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.settingToolStripMenuItem.Text = "设置";
            // 
            // setNetLineToolStripMenuItem
            // 
            this.setNetLineToolStripMenuItem.Name = "setNetLineToolStripMenuItem";
            this.setNetLineToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.setNetLineToolStripMenuItem.Text = "网格线";
            this.setNetLineToolStripMenuItem.Click += new System.EventHandler(this.setNetLineToolStripMenuItem_Click);
            // 
            // setRecogPointToolStripMenuItem
            // 
            this.setRecogPointToolStripMenuItem.Name = "setRecogPointToolStripMenuItem";
            this.setRecogPointToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.setRecogPointToolStripMenuItem.Text = "标识点";
            this.setRecogPointToolStripMenuItem.Click += new System.EventHandler(this.setRecogPointToolStripMenuItem_Click);
            // 
            // setRecogLineToolStripMenuItem
            // 
            this.setRecogLineToolStripMenuItem.Name = "setRecogLineToolStripMenuItem";
            this.setRecogLineToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.setRecogLineToolStripMenuItem.Text = "夹角线条";
            this.setRecogLineToolStripMenuItem.Click += new System.EventHandler(this.setRecogLineToolStripMenuItem_Click);
            // 
            // setColorLabelToolStripMenuItem
            // 
            this.setColorLabelToolStripMenuItem.Name = "setColorLabelToolStripMenuItem";
            this.setColorLabelToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.setColorLabelToolStripMenuItem.Text = "颜色标签";
            this.setColorLabelToolStripMenuItem.Click += new System.EventHandler(this.setColorLabelToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.saveImageToolStripMenuItem.Text = "截屏";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.aboutToolStripMenuItem.Text = "关于";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // hSBarVideo
            // 
            this.hSBarVideo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hSBarVideo.Location = new System.Drawing.Point(0, 798);
            this.hSBarVideo.Name = "hSBarVideo";
            this.hSBarVideo.Size = new System.Drawing.Size(993, 21);
            this.hSBarVideo.TabIndex = 14;
            this.hSBarVideo.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hSBarVideo_Scroll);
            this.hSBarVideo.MouseLeave += new System.EventHandler(this.hSBarVideo_MouseLeave);
            // 
            // FormLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 819);
            this.Controls.Add(this.hSBarVideo);
            this.Controls.Add(this.lbAngle);
            this.Controls.Add(this.imgBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1031, 880);
            this.Name = "FormLine";
            this.Text = "动作捕捉分析系统";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormLine_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imgBox;
        private System.Windows.Forms.Label lbAngle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setNetLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setRecogPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setRecogLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setColorLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.HScrollBar hSBarVideo;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}