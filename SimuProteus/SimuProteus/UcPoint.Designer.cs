namespace SimuProteus
{
    partial class UcPoint
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.vCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gNDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMS
            // 
            this.contextMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vCCToolStripMenuItem,
            this.gNDToolStripMenuItem,
            this.noneToolStripMenuItem});
            this.contextMS.Name = "contextMS";
            this.contextMS.Size = new System.Drawing.Size(153, 98);
            // 
            // vCCToolStripMenuItem
            // 
            this.vCCToolStripMenuItem.Name = "vCCToolStripMenuItem";
            this.vCCToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.vCCToolStripMenuItem.Text = "VCC";
            this.vCCToolStripMenuItem.Click += new System.EventHandler(this.vCCToolStripMenuItem_Click);
            // 
            // gNDToolStripMenuItem
            // 
            this.gNDToolStripMenuItem.Name = "gNDToolStripMenuItem";
            this.gNDToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.gNDToolStripMenuItem.Text = "GND";
            this.gNDToolStripMenuItem.Click += new System.EventHandler(this.gNDToolStripMenuItem_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.noneToolStripMenuItem.Text = "悬空";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // UcPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMS;
            this.Name = "UcPoint";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UcPoint_Paint);
            this.contextMS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMS;
        private System.Windows.Forms.ToolStripMenuItem vCCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gNDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
    }
}
