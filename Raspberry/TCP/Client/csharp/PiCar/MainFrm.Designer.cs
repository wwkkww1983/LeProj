namespace PiCar
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblPostResultInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(31, 110);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(100, 71);
            this.btnLeft.TabIndex = 4;
            this.btnLeft.Text = "左转";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.ctlButton_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(139, 110);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 71);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.ctlButton_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(247, 110);
            this.btnRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(100, 71);
            this.btnRight.TabIndex = 2;
            this.btnRight.Text = "右转";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.ctlButton_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(139, 31);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(100, 71);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "前进";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.ctlButton_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(139, 189);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(100, 71);
            this.btnDown.TabIndex = 3;
            this.btnDown.Text = "后退";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.ctlButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPostResultInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 299);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(383, 25);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblPostResultInfo
            // 
            this.lblPostResultInfo.Name = "lblPostResultInfo";
            this.lblPostResultInfo.Size = new System.Drawing.Size(66, 20);
            this.lblPostResultInfo.Text = "Success";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 324);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnLeft);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainFrm";
            this.Text = "小车控制";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblPostResultInfo;
    }
}

