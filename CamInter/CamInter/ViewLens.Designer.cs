namespace CamInter
{
    partial class ViewLens
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbFocus = new System.Windows.Forms.Label();
            this.tbFocus = new System.Windows.Forms.TextBox();
            this.lbInter = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.lbFreq = new System.Windows.Forms.Label();
            this.tbFreq = new System.Windows.Forms.TextBox();
            this.lbTarget = new System.Windows.Forms.Label();
            this.tbTarget = new System.Windows.Forms.TextBox();
            this.lbDistort = new System.Windows.Forms.Label();
            this.tbDistort = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbInter = new System.Windows.Forms.ComboBox();
            this.lbSensor = new System.Windows.Forms.Label();
            this.tbSensor = new System.Windows.Forms.TextBox();
            this.lbFov = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbNumber = new System.Windows.Forms.Label();
            this.tbFov = new System.Windows.Forms.TextBox();
            this.lbFlange = new System.Windows.Forms.Label();
            this.tbFlange = new System.Windows.Forms.TextBox();
            this.lbRatio = new System.Windows.Forms.Label();
            this.tbRatio = new System.Windows.Forms.TextBox();
            this.tbRatioMax = new System.Windows.Forms.TextBox();
            this.tbRatioMin = new System.Windows.Forms.TextBox();
            this.lbRangeInter = new System.Windows.Forms.Label();
            this.lbRatioRange = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbFocus
            // 
            this.lbFocus.AutoSize = true;
            this.lbFocus.Location = new System.Drawing.Point(287, 123);
            this.lbFocus.Name = "lbFocus";
            this.lbFocus.Size = new System.Drawing.Size(85, 15);
            this.lbFocus.TabIndex = 0;
            this.lbFocus.Text = "焦距(mm) *";
            // 
            // tbFocus
            // 
            this.tbFocus.Location = new System.Drawing.Point(378, 118);
            this.tbFocus.Name = "tbFocus";
            this.tbFocus.Size = new System.Drawing.Size(100, 25);
            this.tbFocus.TabIndex = 8;
            // 
            // lbInter
            // 
            this.lbInter.AutoSize = true;
            this.lbInter.Location = new System.Drawing.Point(87, 123);
            this.lbInter.Name = "lbInter";
            this.lbInter.Size = new System.Drawing.Size(53, 15);
            this.lbInter.TabIndex = 0;
            this.lbInter.Text = "接口 *";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Location = new System.Drawing.Point(103, 255);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(37, 15);
            this.lbWeight.TabIndex = 0;
            this.lbWeight.Text = "重量";
            // 
            // tbWeight
            // 
            this.tbWeight.Location = new System.Drawing.Point(146, 250);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(100, 25);
            this.tbWeight.TabIndex = 20;
            // 
            // lbFreq
            // 
            this.lbFreq.AutoSize = true;
            this.lbFreq.Location = new System.Drawing.Point(305, 211);
            this.lbFreq.Name = "lbFreq";
            this.lbFreq.Size = new System.Drawing.Size(67, 15);
            this.lbFreq.TabIndex = 0;
            this.lbFreq.Text = "空间频率";
            // 
            // tbFreq
            // 
            this.tbFreq.Location = new System.Drawing.Point(378, 206);
            this.tbFreq.Name = "tbFreq";
            this.tbFreq.Size = new System.Drawing.Size(100, 25);
            this.tbFreq.TabIndex = 17;
            // 
            // lbTarget
            // 
            this.lbTarget.AutoSize = true;
            this.lbTarget.Location = new System.Drawing.Point(25, 211);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(115, 15);
            this.lbTarget.TabIndex = 0;
            this.lbTarget.Text = "最大靶面(mm) *";
            // 
            // tbTarget
            // 
            this.tbTarget.Location = new System.Drawing.Point(146, 206);
            this.tbTarget.Name = "tbTarget";
            this.tbTarget.Size = new System.Drawing.Size(100, 25);
            this.tbTarget.TabIndex = 15;
            // 
            // lbDistort
            // 
            this.lbDistort.AutoSize = true;
            this.lbDistort.Location = new System.Drawing.Point(320, 255);
            this.lbDistort.Name = "lbDistort";
            this.lbDistort.Size = new System.Drawing.Size(52, 15);
            this.lbDistort.TabIndex = 0;
            this.lbDistort.Text = "畸变量";
            // 
            // tbDistort
            // 
            this.tbDistort.Location = new System.Drawing.Point(378, 250);
            this.tbDistort.Name = "tbDistort";
            this.tbDistort.Size = new System.Drawing.Size(100, 25);
            this.tbDistort.TabIndex = 22;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(176, 348);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(262, 348);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbInter
            // 
            this.cbInter.FormattingEnabled = true;
            this.cbInter.Location = new System.Drawing.Point(146, 119);
            this.cbInter.Name = "cbInter";
            this.cbInter.Size = new System.Drawing.Size(100, 23);
            this.cbInter.TabIndex = 7;
            // 
            // lbSensor
            // 
            this.lbSensor.AutoSize = true;
            this.lbSensor.Location = new System.Drawing.Point(39, 79);
            this.lbSensor.Name = "lbSensor";
            this.lbSensor.Size = new System.Drawing.Size(101, 15);
            this.lbSensor.TabIndex = 0;
            this.lbSensor.Text = "相机Sensor *";
            // 
            // tbSensor
            // 
            this.tbSensor.Location = new System.Drawing.Point(146, 74);
            this.tbSensor.Name = "tbSensor";
            this.tbSensor.Size = new System.Drawing.Size(100, 25);
            this.tbSensor.TabIndex = 4;
            // 
            // lbFov
            // 
            this.lbFov.AutoSize = true;
            this.lbFov.Location = new System.Drawing.Point(279, 79);
            this.lbFov.Name = "lbFov";
            this.lbFov.Size = new System.Drawing.Size(93, 15);
            this.lbFov.TabIndex = 0;
            this.lbFov.Text = "视野(Fov) *";
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(378, 30);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(100, 25);
            this.tbNumber.TabIndex = 2;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(87, 35);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(53, 15);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "名称 *";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(146, 30);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 25);
            this.tbName.TabIndex = 1;
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(319, 35);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(53, 15);
            this.lbNumber.TabIndex = 0;
            this.lbNumber.Text = "货号 *";
            // 
            // tbFov
            // 
            this.tbFov.Location = new System.Drawing.Point(378, 74);
            this.tbFov.Name = "tbFov";
            this.tbFov.Size = new System.Drawing.Size(100, 25);
            this.tbFov.TabIndex = 5;
            // 
            // lbFlange
            // 
            this.lbFlange.AutoSize = true;
            this.lbFlange.Location = new System.Drawing.Point(10, 167);
            this.lbFlange.Name = "lbFlange";
            this.lbFlange.Size = new System.Drawing.Size(130, 15);
            this.lbFlange.TabIndex = 0;
            this.lbFlange.Text = "相机法兰距(mm) *";
            // 
            // tbFlange
            // 
            this.tbFlange.Location = new System.Drawing.Point(146, 162);
            this.tbFlange.Name = "tbFlange";
            this.tbFlange.Size = new System.Drawing.Size(100, 25);
            this.tbFlange.TabIndex = 10;
            // 
            // lbRatio
            // 
            this.lbRatio.AutoSize = true;
            this.lbRatio.Location = new System.Drawing.Point(73, 299);
            this.lbRatio.Name = "lbRatio";
            this.lbRatio.Size = new System.Drawing.Size(67, 15);
            this.lbRatio.TabIndex = 0;
            this.lbRatio.Text = "放大倍率";
            // 
            // tbRatio
            // 
            this.tbRatio.Enabled = false;
            this.tbRatio.Location = new System.Drawing.Point(146, 294);
            this.tbRatio.Name = "tbRatio";
            this.tbRatio.Size = new System.Drawing.Size(100, 25);
            this.tbRatio.TabIndex = 23;
            // 
            // tbRatioMax
            // 
            this.tbRatioMax.Location = new System.Drawing.Point(442, 162);
            this.tbRatioMax.Name = "tbRatioMax";
            this.tbRatioMax.Size = new System.Drawing.Size(37, 25);
            this.tbRatioMax.TabIndex = 12;
            // 
            // tbRatioMin
            // 
            this.tbRatioMin.Location = new System.Drawing.Point(378, 162);
            this.tbRatioMin.Name = "tbRatioMin";
            this.tbRatioMin.Size = new System.Drawing.Size(37, 25);
            this.tbRatioMin.TabIndex = 11;
            // 
            // lbRangeInter
            // 
            this.lbRangeInter.AutoSize = true;
            this.lbRangeInter.Location = new System.Drawing.Point(421, 167);
            this.lbRangeInter.Name = "lbRangeInter";
            this.lbRangeInter.Size = new System.Drawing.Size(15, 15);
            this.lbRangeInter.TabIndex = 14;
            this.lbRangeInter.Text = "-";
            // 
            // lbRatioRange
            // 
            this.lbRatioRange.AutoSize = true;
            this.lbRatioRange.Location = new System.Drawing.Point(259, 167);
            this.lbRatioRange.Name = "lbRatioRange";
            this.lbRatioRange.Size = new System.Drawing.Size(113, 15);
            this.lbRatioRange.TabIndex = 15;
            this.lbRatioRange.Text = "放大倍率范围 *";
            // 
            // ViewLens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 389);
            this.Controls.Add(this.tbRatioMax);
            this.Controls.Add(this.tbRatioMin);
            this.Controls.Add(this.lbRangeInter);
            this.Controls.Add(this.lbRatioRange);
            this.Controls.Add(this.cbInter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbDistort);
            this.Controls.Add(this.lbDistort);
            this.Controls.Add(this.tbRatio);
            this.Controls.Add(this.lbRatio);
            this.Controls.Add(this.tbFlange);
            this.Controls.Add(this.lbFlange);
            this.Controls.Add(this.tbTarget);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.tbFreq);
            this.Controls.Add(this.lbFreq);
            this.Controls.Add(this.tbFov);
            this.Controls.Add(this.lbNumber);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.lbFov);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbSensor);
            this.Controls.Add(this.lbSensor);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.lbWeight);
            this.Controls.Add(this.lbInter);
            this.Controls.Add(this.tbFocus);
            this.Controls.Add(this.lbFocus);
            this.Name = "ViewLens";
            this.Text = "镜头";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFocus;
        private System.Windows.Forms.TextBox tbFocus;
        private System.Windows.Forms.Label lbInter;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.Label lbFreq;
        private System.Windows.Forms.TextBox tbFreq;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.TextBox tbTarget;
        private System.Windows.Forms.Label lbDistort;
        private System.Windows.Forms.TextBox tbDistort;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbInter;
        private System.Windows.Forms.Label lbSensor;
        private System.Windows.Forms.TextBox tbSensor;
        private System.Windows.Forms.Label lbFov;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.TextBox tbFov;
        private System.Windows.Forms.Label lbFlange;
        private System.Windows.Forms.TextBox tbFlange;
        private System.Windows.Forms.Label lbRatio;
        private System.Windows.Forms.TextBox tbRatio;
        private System.Windows.Forms.TextBox tbRatioMax;
        private System.Windows.Forms.TextBox tbRatioMin;
        private System.Windows.Forms.Label lbRangeInter;
        private System.Windows.Forms.Label lbRatioRange;
    }
}