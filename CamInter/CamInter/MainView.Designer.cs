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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbRatio = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFov = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSensor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.tbTarget = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbDistanRange = new System.Windows.Forms.TextBox();
            this.tbDistance = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLensInter = new System.Windows.Forms.ComboBox();
            this.cbCamInter = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CamLensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbLength);
            this.groupBox1.Controls.Add(this.tbTarget);
            this.groupBox1.Controls.Add(this.tbWidth);
            this.groupBox1.Controls.Add(this.tbDistanRange);
            this.groupBox1.Controls.Add(this.tbDistance);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbLensInter);
            this.groupBox1.Controls.Add(this.cbCamInter);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 489);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbRatio);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbFov);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tbSensor);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(37, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 140);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机规格（mm）";
            // 
            // lbRatio
            // 
            this.lbRatio.AutoSize = true;
            this.lbRatio.Location = new System.Drawing.Point(127, 112);
            this.lbRatio.Name = "lbRatio";
            this.lbRatio.Size = new System.Drawing.Size(15, 15);
            this.lbRatio.TabIndex = 14;
            this.lbRatio.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "放大倍率：";
            // 
            // tbFov
            // 
            this.tbFov.Location = new System.Drawing.Point(127, 71);
            this.tbFov.Name = "tbFov";
            this.tbFov.Size = new System.Drawing.Size(91, 25);
            this.tbFov.TabIndex = 13;
            this.tbFov.TextChanged += new System.EventHandler(this.selectPatchItems);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(42, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "视野(Fov)";
            // 
            // tbSensor
            // 
            this.tbSensor.Location = new System.Drawing.Point(127, 40);
            this.tbSensor.Name = "tbSensor";
            this.tbSensor.Size = new System.Drawing.Size(91, 25);
            this.tbSensor.TabIndex = 13;
            this.tbSensor.TextChanged += new System.EventHandler(this.selectPatchItems);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "相机芯片";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(208, 436);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "×";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 385);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "±";
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(139, 431);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(64, 25);
            this.tbLength.TabIndex = 14;
            // 
            // tbTarget
            // 
            this.tbTarget.Location = new System.Drawing.Point(139, 318);
            this.tbTarget.Name = "tbTarget";
            this.tbTarget.Size = new System.Drawing.Size(147, 25);
            this.tbTarget.TabIndex = 13;
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(236, 431);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(50, 25);
            this.tbWidth.TabIndex = 12;
            // 
            // tbDistanRange
            // 
            this.tbDistanRange.Location = new System.Drawing.Point(236, 380);
            this.tbDistanRange.Name = "tbDistanRange";
            this.tbDistanRange.Size = new System.Drawing.Size(50, 25);
            this.tbDistanRange.TabIndex = 12;
            // 
            // tbDistance
            // 
            this.tbDistance.Location = new System.Drawing.Point(139, 380);
            this.tbDistance.Name = "tbDistance";
            this.tbDistance.Size = new System.Drawing.Size(64, 25);
            this.tbDistance.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 436);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "相机分辨率(像素)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "最小靶面(mm)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 385);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "工作距离(mm)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "相机镜头";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "匹配接口";
            // 
            // cbLensInter
            // 
            this.cbLensInter.FormattingEnabled = true;
            this.cbLensInter.Location = new System.Drawing.Point(139, 37);
            this.cbLensInter.Name = "cbLensInter";
            this.cbLensInter.Size = new System.Drawing.Size(147, 23);
            this.cbLensInter.TabIndex = 0;
            this.cbLensInter.SelectedIndexChanged += new System.EventHandler(this.selectPatchItems);
            // 
            // cbCamInter
            // 
            this.cbCamInter.FormattingEnabled = true;
            this.cbCamInter.Location = new System.Drawing.Point(139, 93);
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
            this.dataGridView1.Size = new System.Drawing.Size(601, 256);
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
            this.ExtendToolStripMenuItem,
            this.connToolStripMenuItem});
            this.录入ToolStripMenuItem.Name = "录入ToolStripMenuItem";
            this.录入ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.录入ToolStripMenuItem.Text = "新增";
            // 
            // CamLensToolStripMenuItem
            // 
            this.CamLensToolStripMenuItem.Name = "CamLensToolStripMenuItem";
            this.CamLensToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.CamLensToolStripMenuItem.Text = "镜头";
            this.CamLensToolStripMenuItem.Click += new System.EventHandler(this.CamLensToolStripMenuItem_Click);
            // 
            // FocusToolStripMenuItem
            // 
            this.FocusToolStripMenuItem.Name = "FocusToolStripMenuItem";
            this.FocusToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.FocusToolStripMenuItem.Text = "调焦环";
            this.FocusToolStripMenuItem.Click += new System.EventHandler(this.FocusToolStripMenuItem_Click);
            // 
            // AdapterToolStripMenuItem
            // 
            this.AdapterToolStripMenuItem.Name = "AdapterToolStripMenuItem";
            this.AdapterToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.AdapterToolStripMenuItem.Text = "转接环";
            this.AdapterToolStripMenuItem.Click += new System.EventHandler(this.AdapterToolStripMenuItem_Click);
            // 
            // ExtendToolStripMenuItem
            // 
            this.ExtendToolStripMenuItem.Name = "ExtendToolStripMenuItem";
            this.ExtendToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.ExtendToolStripMenuItem.Text = "延长环";
            this.ExtendToolStripMenuItem.Click += new System.EventHandler(this.ExtendToolStripMenuItem_Click);
            // 
            // connToolStripMenuItem
            // 
            this.connToolStripMenuItem.Name = "connToolStripMenuItem";
            this.connToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.connToolStripMenuItem.Text = "接口";
            this.connToolStripMenuItem.Click += new System.EventHandler(this.connToolStripMenuItem_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(351, 315);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 27;
            this.dataGridView2.Size = new System.Drawing.Size(601, 217);
            this.dataGridView2.TabIndex = 1;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 544);
            this.Controls.Add(this.dataGridView2);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCamInter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.TextBox tbTarget;
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
        private System.Windows.Forms.ToolStripMenuItem connToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLensInter;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbSensor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFov;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbRatio;
    }
}

