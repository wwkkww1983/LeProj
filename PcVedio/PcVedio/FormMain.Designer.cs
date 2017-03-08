namespace PcVedio
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCam = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wIFIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wifiSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wifiSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视频ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wifiConnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wifiBreakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.音频ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioPlayToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.audioStopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnCam);
            this.panel1.Location = new System.Drawing.Point(12, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 542);
            this.panel1.TabIndex = 0;
            // 
            // btnCam
            // 
            this.btnCam.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCam.Location = new System.Drawing.Point(0, 519);
            this.btnCam.Name = "btnCam";
            this.btnCam.Size = new System.Drawing.Size(793, 23);
            this.btnCam.TabIndex = 0;
            this.btnCam.Text = "拍照";
            this.btnCam.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wIFIToolStripMenuItem,
            this.视频ToolStripMenuItem,
            this.音频ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(817, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wIFIToolStripMenuItem
            // 
            this.wIFIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wifiSearchToolStripMenuItem,
            this.wifiConnToolStripMenuItem,
            this.wifiBreakToolStripMenuItem,
            this.wifiSetToolStripMenuItem});
            this.wIFIToolStripMenuItem.Name = "wIFIToolStripMenuItem";
            this.wIFIToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.wIFIToolStripMenuItem.Text = "WIFI";
            // 
            // wifiSearchToolStripMenuItem
            // 
            this.wifiSearchToolStripMenuItem.Name = "wifiSearchToolStripMenuItem";
            this.wifiSearchToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.wifiSearchToolStripMenuItem.Text = "搜索";
            this.wifiSearchToolStripMenuItem.Click += new System.EventHandler(this.wifiSearchToolStripMenuItem_Click);
            // 
            // wifiSetToolStripMenuItem
            // 
            this.wifiSetToolStripMenuItem.Name = "wifiSetToolStripMenuItem";
            this.wifiSetToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.wifiSetToolStripMenuItem.Text = "参数设置";
            this.wifiSetToolStripMenuItem.Click += new System.EventHandler(this.wifiSetToolStripMenuItem_Click);
            // 
            // 视频ToolStripMenuItem
            // 
            this.视频ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoPlayToolStripMenuItem,
            this.videoStopToolStripMenuItem});
            this.视频ToolStripMenuItem.Name = "视频ToolStripMenuItem";
            this.视频ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.视频ToolStripMenuItem.Text = "视频";
            // 
            // wifiConnToolStripMenuItem
            // 
            this.wifiConnToolStripMenuItem.Name = "wifiConnToolStripMenuItem";
            this.wifiConnToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.wifiConnToolStripMenuItem.Text = "连接";
            this.wifiConnToolStripMenuItem.Click += new System.EventHandler(this.wifiConnToolStripMenuItem_Click);
            // 
            // wifiBreakToolStripMenuItem
            // 
            this.wifiBreakToolStripMenuItem.Name = "wifiBreakToolStripMenuItem";
            this.wifiBreakToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.wifiBreakToolStripMenuItem.Text = "断开";
            this.wifiBreakToolStripMenuItem.Click += new System.EventHandler(this.wifiBreakToolStripMenuItem_Click);
            // 
            // videoPlayToolStripMenuItem
            // 
            this.videoPlayToolStripMenuItem.Name = "videoPlayToolStripMenuItem";
            this.videoPlayToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.videoPlayToolStripMenuItem.Text = "播放";
            this.videoPlayToolStripMenuItem.Click += new System.EventHandler(this.videoPlayToolStripMenuItem_Click);
            // 
            // videoStopToolStripMenuItem
            // 
            this.videoStopToolStripMenuItem.Name = "videoStopToolStripMenuItem";
            this.videoStopToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.videoStopToolStripMenuItem.Text = "停止";
            this.videoStopToolStripMenuItem.Click += new System.EventHandler(this.videoStopToolStripMenuItem_Click);
            // 
            // 音频ToolStripMenuItem
            // 
            this.音频ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.audioPlayToolStripMenuItem1,
            this.audioStopToolStripMenuItem1});
            this.音频ToolStripMenuItem.Name = "音频ToolStripMenuItem";
            this.音频ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.音频ToolStripMenuItem.Text = "音频";
            // 
            // audioPlayToolStripMenuItem1
            // 
            this.audioPlayToolStripMenuItem1.Name = "audioPlayToolStripMenuItem1";
            this.audioPlayToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.audioPlayToolStripMenuItem1.Text = "播放";
            this.audioPlayToolStripMenuItem1.Click += new System.EventHandler(this.audioPlayToolStripMenuItem1_Click);
            // 
            // audioStopToolStripMenuItem1
            // 
            this.audioStopToolStripMenuItem1.Name = "audioStopToolStripMenuItem1";
            this.audioStopToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.audioStopToolStripMenuItem1.Text = "停止";
            this.audioStopToolStripMenuItem1.Click += new System.EventHandler(this.audioStopToolStripMenuItem1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 585);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "moqo";
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCam;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wIFIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wifiSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wifiSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视频ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wifiConnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wifiBreakToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 音频ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioPlayToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem audioStopToolStripMenuItem1;
    }
}

