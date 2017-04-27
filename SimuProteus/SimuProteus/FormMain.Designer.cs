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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.gbComponent = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freeSerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugSerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMsPoint = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.vCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gNDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nonePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setOriginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMsLine = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnWorkPlace = new System.Windows.Forms.Panel();
            this.pnBoard = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.contextMsPoint.SuspendLayout();
            this.contextMsLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbComponent
            // 
            this.gbComponent.Location = new System.Drawing.Point(12, 35);
            this.gbComponent.Name = "gbComponent";
            this.gbComponent.Size = new System.Drawing.Size(60, 655);
            this.gbComponent.TabIndex = 0;
            this.gbComponent.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateToolStripMenuItem,
            this.setToolStripMenuItem,
            this.SerialToolStripMenuItem,
            this.serialListToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(880, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.CreateToolStripMenuItem.Text = "新增元器件";
            this.CreateToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorSetToolStripMenuItem,
            this.sizeSetToolStripMenuItem});
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            this.setToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.setToolStripMenuItem.Text = "设置";
            // 
            // colorSetToolStripMenuItem
            // 
            this.colorSetToolStripMenuItem.Name = "colorSetToolStripMenuItem";
            this.colorSetToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.colorSetToolStripMenuItem.Text = "颜色";
            this.colorSetToolStripMenuItem.Click += new System.EventHandler(this.colorSetToolStripMenuItem_Click);
            // 
            // sizeSetToolStripMenuItem
            // 
            this.sizeSetToolStripMenuItem.Name = "sizeSetToolStripMenuItem";
            this.sizeSetToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.sizeSetToolStripMenuItem.Text = "尺寸";
            this.sizeSetToolStripMenuItem.Click += new System.EventHandler(this.sizeSetToolStripMenuItem_Click);
            // 
            // SerialToolStripMenuItem
            // 
            this.SerialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serialStatusToolStripMenuItem,
            this.freeSerialToolStripMenuItem,
            this.debugSerialToolStripMenuItem});
            this.SerialToolStripMenuItem.Name = "SerialToolStripMenuItem";
            this.SerialToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.SerialToolStripMenuItem.Text = "串口数据";
            // 
            // serialStatusToolStripMenuItem
            // 
            this.serialStatusToolStripMenuItem.Name = "serialStatusToolStripMenuItem";
            this.serialStatusToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.serialStatusToolStripMenuItem.Text = "串口设置";
            this.serialStatusToolStripMenuItem.Click += new System.EventHandler(this.serialStatusToolStripMenuItem_Click);
            // 
            // freeSerialToolStripMenuItem
            // 
            this.freeSerialToolStripMenuItem.Name = "freeSerialToolStripMenuItem";
            this.freeSerialToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.freeSerialToolStripMenuItem.Text = "释放串口";
            this.freeSerialToolStripMenuItem.Click += new System.EventHandler(this.freeSerialToolStripMenuItem_Click);
            // 
            // debugSerialToolStripMenuItem
            // 
            this.debugSerialToolStripMenuItem.Name = "debugSerialToolStripMenuItem";
            this.debugSerialToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.debugSerialToolStripMenuItem.Text = "串口调试";
            this.debugSerialToolStripMenuItem.Click += new System.EventHandler(this.debugSerialToolStripMenuItem_Click);
            // 
            // serialListToolStripMenuItem
            // 
            this.serialListToolStripMenuItem.Name = "serialListToolStripMenuItem";
            this.serialListToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.serialListToolStripMenuItem.Text = "查看";
            this.serialListToolStripMenuItem.Click += new System.EventHandler(this.serialListToolStripMenuItem_Click);
            // 
            // contextMsPoint
            // 
            this.contextMsPoint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vCCToolStripMenuItem,
            this.gNDToolStripMenuItem,
            this.nonePointToolStripMenuItem,
            this.setOriginToolStripMenuItem});
            this.contextMsPoint.Name = "contextMsPoint";
            this.contextMsPoint.Size = new System.Drawing.Size(139, 100);
            // 
            // vCCToolStripMenuItem
            // 
            this.vCCToolStripMenuItem.Name = "vCCToolStripMenuItem";
            this.vCCToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.vCCToolStripMenuItem.Text = "VCC";
            this.vCCToolStripMenuItem.Click += new System.EventHandler(this.vCCToolStripMenuItem_Click);
            // 
            // gNDToolStripMenuItem
            // 
            this.gNDToolStripMenuItem.Name = "gNDToolStripMenuItem";
            this.gNDToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.gNDToolStripMenuItem.Text = "GND";
            this.gNDToolStripMenuItem.Click += new System.EventHandler(this.gNDToolStripMenuItem_Click);
            // 
            // nonePointToolStripMenuItem
            // 
            this.nonePointToolStripMenuItem.Name = "nonePointToolStripMenuItem";
            this.nonePointToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.nonePointToolStripMenuItem.Text = "悬空";
            this.nonePointToolStripMenuItem.Click += new System.EventHandler(this.nonePointToolStripMenuItem_Click);
            // 
            // setOriginToolStripMenuItem
            // 
            this.setOriginToolStripMenuItem.Name = "setOriginToolStripMenuItem";
            this.setOriginToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.setOriginToolStripMenuItem.Text = "设为原点";
            this.setOriginToolStripMenuItem.Click += new System.EventHandler(this.setOriginToolStripMenuItem_Click);
            // 
            // contextMsLine
            // 
            this.contextMsLine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorLineToolStripMenuItem,
            this.delLineToolStripMenuItem});
            this.contextMsLine.Name = "contextMsLine";
            this.contextMsLine.Size = new System.Drawing.Size(109, 52);
            // 
            // colorLineToolStripMenuItem
            // 
            this.colorLineToolStripMenuItem.Name = "colorLineToolStripMenuItem";
            this.colorLineToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.colorLineToolStripMenuItem.Text = "颜色";
            this.colorLineToolStripMenuItem.Click += new System.EventHandler(this.colorLineToolStripMenuItem_Click);
            // 
            // delLineToolStripMenuItem
            // 
            this.delLineToolStripMenuItem.Name = "delLineToolStripMenuItem";
            this.delLineToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.delLineToolStripMenuItem.Text = "删除";
            this.delLineToolStripMenuItem.Click += new System.EventHandler(this.delLineToolStripMenuItem_Click);
            // 
            // pnWorkPlace
            // 
            this.pnWorkPlace.AutoScroll = true;
            this.pnWorkPlace.Location = new System.Drawing.Point(78, 35);
            this.pnWorkPlace.Name = "pnWorkPlace";
            this.pnWorkPlace.Size = new System.Drawing.Size(200, 100);
            this.pnWorkPlace.TabIndex = 3;
            // 
            // pnBoard
            // 
            this.pnBoard.Location = new System.Drawing.Point(78, 45);
            this.pnBoard.Name = "pnBoard";
            this.pnBoard.Size = new System.Drawing.Size(798, 645);
            this.pnBoard.TabIndex = 2;
            this.pnBoard.Click += new System.EventHandler(this.pnBoard_Click);
            this.pnBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnBoard_Paint);
            this.pnBoard.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnBoard_MouseDoubleClick);
            this.pnBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnBoard_MouseMove);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 720);
            this.Controls.Add(this.pnWorkPlace);
            this.Controls.Add(this.pnBoard);
            this.Controls.Add(this.gbComponent);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Proteus仿真";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMsPoint.ResumeLayout(false);
            this.contextMsLine.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbComponent;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SerialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serialStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freeSerialToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMsPoint;
        private System.Windows.Forms.ToolStripMenuItem vCCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gNDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nonePointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setOriginToolStripMenuItem;
        private System.Windows.Forms.Panel pnBoard;
        private System.Windows.Forms.ContextMenuStrip contextMsLine;
        private System.Windows.Forms.ToolStripMenuItem colorLineToolStripMenuItem;
        private System.Windows.Forms.Panel pnWorkPlace;
        private System.Windows.Forms.ToolStripMenuItem delLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugSerialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serialListToolStripMenuItem;
    }
}

