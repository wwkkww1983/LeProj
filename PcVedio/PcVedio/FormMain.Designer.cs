/// <summary>
/// copyright:  Zac (suoxd123@126.com)
/// 2017-03-14
/// </summary>
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wifiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wifiConnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wifiBreakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picBoxVideo = new System.Windows.Forms.PictureBox();
            this.timerPlay = new System.Windows.Forms.Timer(this.components);
            this.timerAlive = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wifiToolStripMenuItem,
            this.pictureToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.LanguageToolStripMenuItem,
            this.setToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(817, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wifiToolStripMenuItem
            // 
            this.wifiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wifiConnToolStripMenuItem,
            this.wifiBreakToolStripMenuItem});
            this.wifiToolStripMenuItem.Name = "wifiToolStripMenuItem";
            this.wifiToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.wifiToolStripMenuItem.Text = "视频";
            // 
            // wifiConnToolStripMenuItem
            // 
            this.wifiConnToolStripMenuItem.Name = "wifiConnToolStripMenuItem";
            this.wifiConnToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.wifiConnToolStripMenuItem.Text = "连接";
            this.wifiConnToolStripMenuItem.Click += new System.EventHandler(this.wifiConnToolStripMenuItem_Click);
            // 
            // wifiBreakToolStripMenuItem
            // 
            this.wifiBreakToolStripMenuItem.Name = "wifiBreakToolStripMenuItem";
            this.wifiBreakToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.wifiBreakToolStripMenuItem.Text = "断开";
            this.wifiBreakToolStripMenuItem.Click += new System.EventHandler(this.wifiBreakToolStripMenuItem_Click);
            // 
            // pictureToolStripMenuItem
            // 
            this.pictureToolStripMenuItem.Name = "pictureToolStripMenuItem";
            this.pictureToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.pictureToolStripMenuItem.Text = "拍照";
            this.pictureToolStripMenuItem.Click += new System.EventHandler(this.pictureToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.videoToolStripMenuItem.Text = "录像";
            this.videoToolStripMenuItem.Visible = false;
            this.videoToolStripMenuItem.Click += new System.EventHandler(this.videoToolStripMenuItem_Click);
            // 
            // LanguageToolStripMenuItem
            // 
            this.LanguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.chineseToolStripMenuItem});
            this.LanguageToolStripMenuItem.Name = "LanguageToolStripMenuItem";
            this.LanguageToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.LanguageToolStripMenuItem.Text = "语言";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // chineseToolStripMenuItem
            // 
            this.chineseToolStripMenuItem.Name = "chineseToolStripMenuItem";
            this.chineseToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.chineseToolStripMenuItem.Text = "简体中文";
            this.chineseToolStripMenuItem.Click += new System.EventHandler(this.chineseToolStripMenuItem_Click);
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            this.setToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.setToolStripMenuItem.Text = "文件路径";
            this.setToolStripMenuItem.Click += new System.EventHandler(this.setToolStripMenuItem_Click);
            // 
            // picBoxVideo
            // 
            this.picBoxVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxVideo.Location = new System.Drawing.Point(0, 28);
            this.picBoxVideo.Name = "picBoxVideo";
            this.picBoxVideo.Size = new System.Drawing.Size(817, 557);
            this.picBoxVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxVideo.TabIndex = 2;
            this.picBoxVideo.TabStop = false;
            // 
            // timerPlay
            // 
            this.timerPlay.Interval = 10;
            this.timerPlay.Tick += new System.EventHandler(this.timerPlay_Tick);
            // 
            // timerAlive
            // 
            this.timerAlive.Interval = 5000;
            this.timerAlive.Tick += new System.EventHandler(this.timerAlive_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 585);
            this.Controls.Add(this.picBoxVideo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "moqo";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wifiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wifiConnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wifiBreakToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBoxVideo;
        private System.Windows.Forms.Timer timerPlay;
        private System.Windows.Forms.Timer timerAlive;
        private System.Windows.Forms.ToolStripMenuItem LanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
    }
}

