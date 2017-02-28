namespace SimuProteus
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
            this.gbComponent = new System.Windows.Forms.GroupBox();
            this.pnBoard = new System.Windows.Forms.Panel();
            this.lbProjName = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hC244ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lS221ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnBoard.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbComponent
            // 
            this.gbComponent.Location = new System.Drawing.Point(12, 33);
            this.gbComponent.Name = "gbComponent";
            this.gbComponent.Size = new System.Drawing.Size(60, 655);
            this.gbComponent.TabIndex = 0;
            this.gbComponent.TabStop = false;
            // 
            // pnBoard
            // 
            this.pnBoard.Controls.Add(this.lbProjName);
            this.pnBoard.Location = new System.Drawing.Point(78, 45);
            this.pnBoard.Name = "pnBoard";
            this.pnBoard.Size = new System.Drawing.Size(790, 643);
            this.pnBoard.TabIndex = 1;
            this.pnBoard.Click += new System.EventHandler(this.pnBoard_Click);
            // 
            // lbProjName
            // 
            this.lbProjName.AutoSize = true;
            this.lbProjName.Location = new System.Drawing.Point(576, 0);
            this.lbProjName.Name = "lbProjName";
            this.lbProjName.Size = new System.Drawing.Size(52, 15);
            this.lbProjName.TabIndex = 0;
            this.lbProjName.Text = "未命名";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateToolStripMenuItem,
            this.saveSToolStripMenuItem,
            this.SerialToolStripMenuItem,
            this.ProjToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(880, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hC244ToolStripMenuItem,
            this.lS221ToolStripMenuItem});
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.CreateToolStripMenuItem.Text = "新建";
            // 
            // hC244ToolStripMenuItem
            // 
            this.hC244ToolStripMenuItem.Name = "hC244ToolStripMenuItem";
            this.hC244ToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.hC244ToolStripMenuItem.Tag = "1";
            this.hC244ToolStripMenuItem.Text = "74HC244";
            this.hC244ToolStripMenuItem.Click += new System.EventHandler(this.hC244ToolStripMenuItem_Click);
            // 
            // lS221ToolStripMenuItem
            // 
            this.lS221ToolStripMenuItem.Name = "lS221ToolStripMenuItem";
            this.lS221ToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.lS221ToolStripMenuItem.Tag = "2";
            this.lS221ToolStripMenuItem.Text = "74LS221";
            this.lS221ToolStripMenuItem.Click += new System.EventHandler(this.lS221ToolStripMenuItem_Click);
            // 
            // saveSToolStripMenuItem
            // 
            this.saveSToolStripMenuItem.Name = "saveSToolStripMenuItem";
            this.saveSToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.saveSToolStripMenuItem.Text = "保存(&S)";
            this.saveSToolStripMenuItem.Click += new System.EventHandler(this.saveSToolStripMenuItem_Click);
            // 
            // SerialToolStripMenuItem
            // 
            this.SerialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serialStatusToolStripMenuItem,
            this.readToolStripMenuItem,
            this.writeToolStripMenuItem});
            this.SerialToolStripMenuItem.Name = "SerialToolStripMenuItem";
            this.SerialToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.SerialToolStripMenuItem.Text = "串口数据";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.readToolStripMenuItem.Text = "读取";
            this.readToolStripMenuItem.Click += new System.EventHandler(this.readToolStripMenuItem_Click);
            // 
            // writeToolStripMenuItem
            // 
            this.writeToolStripMenuItem.Name = "writeToolStripMenuItem";
            this.writeToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.writeToolStripMenuItem.Text = "写入";
            this.writeToolStripMenuItem.Click += new System.EventHandler(this.writeToolStripMenuItem_Click);
            // 
            // ProjToolStripMenuItem
            // 
            this.ProjToolStripMenuItem.Name = "ProjToolStripMenuItem";
            this.ProjToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.ProjToolStripMenuItem.Text = "历史项目";
            // 
            // serialStatusToolStripMenuItem
            // 
            this.serialStatusToolStripMenuItem.Name = "serialStatusToolStripMenuItem";
            this.serialStatusToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.serialStatusToolStripMenuItem.Text = "串口设置";
            this.serialStatusToolStripMenuItem.Click += new System.EventHandler(this.serialStatusToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 700);
            this.Controls.Add(this.pnBoard);
            this.Controls.Add(this.gbComponent);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Proteus仿真";
            this.pnBoard.ResumeLayout(false);
            this.pnBoard.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbComponent;
        private System.Windows.Forms.Panel pnBoard;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hC244ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lS221ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SerialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProjToolStripMenuItem;
        private System.Windows.Forms.Label lbProjName;
        private System.Windows.Forms.ToolStripMenuItem serialStatusToolStripMenuItem;
    }
}

