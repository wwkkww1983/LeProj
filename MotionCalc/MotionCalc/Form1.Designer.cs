namespace MotionCalc
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imgBoxLU = new Emgu.CV.UI.ImageBox();
            this.imgBoxRU = new Emgu.CV.UI.ImageBox();
            this.imgBoxLD = new Emgu.CV.UI.ImageBox();
            this.imgBoxRD = new Emgu.CV.UI.ImageBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.timerRecord = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxLU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxRU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxLD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxRD)).BeginInit();
            this.SuspendLayout();
            // 
            // imgBoxLU
            // 
            this.imgBoxLU.Location = new System.Drawing.Point(12, 53);
            this.imgBoxLU.Name = "imgBoxLU";
            this.imgBoxLU.Size = new System.Drawing.Size(483, 431);
            this.imgBoxLU.TabIndex = 2;
            this.imgBoxLU.TabStop = false;
            // 
            // imgBoxRU
            // 
            this.imgBoxRU.Location = new System.Drawing.Point(501, 53);
            this.imgBoxRU.Name = "imgBoxRU";
            this.imgBoxRU.Size = new System.Drawing.Size(483, 431);
            this.imgBoxRU.TabIndex = 2;
            this.imgBoxRU.TabStop = false;
            // 
            // imgBoxLD
            // 
            this.imgBoxLD.Location = new System.Drawing.Point(12, 490);
            this.imgBoxLD.Name = "imgBoxLD";
            this.imgBoxLD.Size = new System.Drawing.Size(483, 431);
            this.imgBoxLD.TabIndex = 2;
            this.imgBoxLD.TabStop = false;
            // 
            // imgBoxRD
            // 
            this.imgBoxRD.Location = new System.Drawing.Point(501, 490);
            this.imgBoxRD.Name = "imgBoxRD";
            this.imgBoxRD.Size = new System.Drawing.Size(483, 431);
            this.imgBoxRD.TabIndex = 2;
            this.imgBoxRD.TabStop = false;
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(12, 12);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(141, 35);
            this.btnRecord.TabIndex = 3;
            this.btnRecord.Text = "录制视频";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // timerRecord
            // 
            this.timerRecord.Interval = 1000;
            this.timerRecord.Tick += new System.EventHandler(this.timerRecord_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 935);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.imgBoxRD);
            this.Controls.Add(this.imgBoxRU);
            this.Controls.Add(this.imgBoxLD);
            this.Controls.Add(this.imgBoxLU);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "动作捕捉分析系统";
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxLU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxRU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxLD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBoxRD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imgBoxLU;
        private Emgu.CV.UI.ImageBox imgBoxRU;
        private Emgu.CV.UI.ImageBox imgBoxLD;
        private Emgu.CV.UI.ImageBox imgBoxRD;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Timer timerRecord;

    }
}

