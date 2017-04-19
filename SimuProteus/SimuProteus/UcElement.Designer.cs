namespace SimuProteus
{
    partial class UcElement
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
            this.picbElement = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerDel = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picbElement)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picbElement
            // 
            this.picbElement.Location = new System.Drawing.Point(0, 0);
            this.picbElement.Name = "picbElement";
            this.picbElement.Size = new System.Drawing.Size(100, 50);
            this.picbElement.TabIndex = 0;
            this.picbElement.TabStop = false;
            this.picbElement.Click += new System.EventHandler(this.picbElement_Click);
            this.picbElement.DoubleClick += new System.EventHandler(this.picbElement_DoubleClick);
            this.picbElement.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picbElement_MouseDown);
            this.picbElement.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picbElement_MouseMove);
            this.picbElement.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picbElement_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.delToolStripMenuItem.Text = "删除";
            this.delToolStripMenuItem.Click += new System.EventHandler(this.delToolStripMenuItem_Click);
            // 
            // timerDel
            // 
            this.timerDel.Enabled = true;
            // 
            // UcElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.picbElement);
            this.Name = "UcElement";
            this.Click += new System.EventHandler(this.UcElement_Click);
            this.DoubleClick += new System.EventHandler(this.UcElement_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UcElement_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UcElement_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UcElement_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picbElement)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picbElement;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem delToolStripMenuItem;
        private System.Windows.Forms.Timer timerDel;
    }
}
