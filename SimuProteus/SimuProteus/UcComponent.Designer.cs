namespace SimuProteus
{
    partial class UcComponent
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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.contextMsElement = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.elementDelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.contextMsElement.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox.Location = new System.Drawing.Point(0, 0);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(50, 50);
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            this.picBox.Click += new System.EventHandler(this.picBox_Click);
            // 
            // contextMsElement
            // 
            this.contextMsElement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem,
            this.elementDelToolStripMenuItem});
            this.contextMsElement.Name = "contextMsElement";
            this.contextMsElement.Size = new System.Drawing.Size(153, 74);
            // 
            // elementDelToolStripMenuItem
            // 
            this.elementDelToolStripMenuItem.Name = "elementDelToolStripMenuItem";
            this.elementDelToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.elementDelToolStripMenuItem.Text = "删除";
            this.elementDelToolStripMenuItem.Click += new System.EventHandler(this.elementDelToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.updateToolStripMenuItem.Text = "修改";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // UcComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMsElement;
            this.Controls.Add(this.picBox);
            this.Name = "UcComponent";
            this.Size = new System.Drawing.Size(50, 50);
            this.Click += new System.EventHandler(this.UcComponent_Click);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.contextMsElement.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.ContextMenuStrip contextMsElement;
        private System.Windows.Forms.ToolStripMenuItem elementDelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;

    }
}
