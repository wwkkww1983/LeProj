namespace CamInter
{
    partial class MainView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.tbPixelSize = new System.Windows.Forms.TextBox();
            this.tbDistance = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbShort = new System.Windows.Forms.RadioButton();
            this.rbLong = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFov = new System.Windows.Forms.TextBox();
            this.tbSensor = new System.Windows.Forms.TextBox();
            this.tbCamFlange = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCamInter = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CamLensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.tbDistanRange = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbLength);
            this.groupBox1.Controls.Add(this.tbPixelSize);
            this.groupBox1.Controls.Add(this.tbWidth);
            this.groupBox1.Controls.Add(this.tbDistanRange);
            this.groupBox1.Controls.Add(this.tbDistance);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tbCamFlange);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbCamInter);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 489);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(139, 435);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(64, 25);
            this.tbLength.TabIndex = 14;
            // 
            // tbPixelSize
            // 
            this.tbPixelSize.Location = new System.Drawing.Point(139, 384);
            this.tbPixelSize.Name = "tbPixelSize";
            this.tbPixelSize.Size = new System.Drawing.Size(147, 25);
            this.tbPixelSize.TabIndex = 13;
            // 
            // tbDistance
            // 
            this.tbDistance.Location = new System.Drawing.Point(139, 329);
            this.tbDistance.Name = "tbDistance";
            this.tbDistance.Size = new System.Drawing.Size(64, 25);
            this.tbDistance.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbShort);
            this.groupBox2.Controls.Add(this.rbLong);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbFov);
            this.groupBox2.Controls.Add(this.tbSensor);
            this.groupBox2.Location = new System.Drawing.Point(27, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 155);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "长度（mm）";
            // 
            // rbShort
            // 
            this.rbShort.AutoSize = true;
            this.rbShort.Location = new System.Drawing.Point(184, 42);
            this.rbShort.Name = "rbShort";
            this.rbShort.Size = new System.Drawing.Size(58, 19);
            this.rbShort.TabIndex = 10;
            this.rbShort.TabStop = true;
            this.rbShort.Text = "短边";
            this.rbShort.UseVisualStyleBackColor = true;
            // 
            // rbLong
            // 
            this.rbLong.AutoSize = true;
            this.rbLong.Location = new System.Drawing.Point(76, 42);
            this.rbLong.Name = "rbLong";
            this.rbLong.Size = new System.Drawing.Size(58, 19);
            this.rbLong.TabIndex = 10;
            this.rbLong.TabStop = true;
            this.rbLong.Text = "长边";
            this.rbLong.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "视野Fov";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "相机Sensor尺寸";
            // 
            // tbFov
            // 
            this.tbFov.Location = new System.Drawing.Point(159, 113);
            this.tbFov.Name = "tbFov";
            this.tbFov.Size = new System.Drawing.Size(100, 25);
            this.tbFov.TabIndex = 9;
            // 
            // tbSensor
            // 
            this.tbSensor.Location = new System.Drawing.Point(159, 82);
            this.tbSensor.Name = "tbSensor";
            this.tbSensor.Size = new System.Drawing.Size(100, 25);
            this.tbSensor.TabIndex = 9;
            // 
            // tbCamFlange
            // 
            this.tbCamFlange.Location = new System.Drawing.Point(139, 92);
            this.tbCamFlange.Name = "tbCamFlange";
            this.tbCamFlange.Size = new System.Drawing.Size(147, 25);
            this.tbCamFlange.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 440);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "相机分辨率(像素)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 389);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "相机像素尺寸(um)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "工作距离(mm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "相机法兰距离(mm)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "相机接口";
            // 
            // cbCamInter
            // 
            this.cbCamInter.FormattingEnabled = true;
            this.cbCamInter.Location = new System.Drawing.Point(139, 36);
            this.cbCamInter.Name = "cbCamInter";
            this.cbCamInter.Size = new System.Drawing.Size(147, 23);
            this.cbCamInter.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(351, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(622, 479);
            this.dataGridView1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.录入ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(985, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 录入ToolStripMenuItem
            // 
            this.录入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CamLensToolStripMenuItem,
            this.FocusToolStripMenuItem,
            this.AdapterToolStripMenuItem,
            this.ExtendToolStripMenuItem});
            this.录入ToolStripMenuItem.Name = "录入ToolStripMenuItem";
            this.录入ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.录入ToolStripMenuItem.Text = "录入";
            // 
            // CamLensToolStripMenuItem
            // 
            this.CamLensToolStripMenuItem.Name = "CamLensToolStripMenuItem";
            this.CamLensToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.CamLensToolStripMenuItem.Text = "镜头";
            this.CamLensToolStripMenuItem.Click += new System.EventHandler(this.CamLensToolStripMenuItem_Click);
            // 
            // FocusToolStripMenuItem
            // 
            this.FocusToolStripMenuItem.Name = "FocusToolStripMenuItem";
            this.FocusToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.FocusToolStripMenuItem.Text = "调焦环";
            this.FocusToolStripMenuItem.Click += new System.EventHandler(this.FocusToolStripMenuItem_Click);
            // 
            // AdapterToolStripMenuItem
            // 
            this.AdapterToolStripMenuItem.Name = "AdapterToolStripMenuItem";
            this.AdapterToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.AdapterToolStripMenuItem.Text = "转接环";
            this.AdapterToolStripMenuItem.Click += new System.EventHandler(this.AdapterToolStripMenuItem_Click);
            // 
            // ExtendToolStripMenuItem
            // 
            this.ExtendToolStripMenuItem.Name = "ExtendToolStripMenuItem";
            this.ExtendToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.ExtendToolStripMenuItem.Text = "延长环";
            this.ExtendToolStripMenuItem.Click += new System.EventHandler(this.ExtendToolStripMenuItem_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 334);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "±";
            // 
            // tbDistanRange
            // 
            this.tbDistanRange.Location = new System.Drawing.Point(236, 329);
            this.tbDistanRange.Name = "tbDistanRange";
            this.tbDistanRange.Size = new System.Drawing.Size(50, 25);
            this.tbDistanRange.TabIndex = 12;
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(236, 435);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(50, 25);
            this.tbWidth.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(208, 440);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "×";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 544);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainView";
            this.Text = "相机镜头配对";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCamInter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbCamFlange;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbLong;
        private System.Windows.Forms.TextBox tbSensor;
        private System.Windows.Forms.RadioButton rbShort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFov;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.TextBox tbPixelSize;
        private System.Windows.Forms.TextBox tbDistance;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CamLensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FocusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AdapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExtendToolStripMenuItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbDistanRange;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbWidth;
    }
}

