namespace CamInter
{
    partial class MidRing
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
            this.lbInterB = new System.Windows.Forms.Label();
            this.tbInRadius = new System.Windows.Forms.TextBox();
            this.lbLenRange = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.lbLength = new System.Windows.Forms.Label();
            this.lbInterA = new System.Windows.Forms.Label();
            this.lbWeight = new System.Windows.Forms.Label();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbNumber = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lbInner = new System.Windows.Forms.Label();
            this.tbLenMin = new System.Windows.Forms.TextBox();
            this.tbLenMax = new System.Windows.Forms.TextBox();
            this.lbRangeInter = new System.Windows.Forms.Label();
            this.cbInterA = new System.Windows.Forms.ComboBox();
            this.cbInterB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbInterB
            // 
            this.lbInterB.AutoSize = true;
            this.lbInterB.Location = new System.Drawing.Point(221, 71);
            this.lbInterB.Name = "lbInterB";
            this.lbInterB.Size = new System.Drawing.Size(69, 15);
            this.lbInterB.TabIndex = 7;
            this.lbInterB.Text = "接口 B *";
            // 
            // tbInRadius
            // 
            this.tbInRadius.Location = new System.Drawing.Point(296, 153);
            this.tbInRadius.Name = "tbInRadius";
            this.tbInRadius.Size = new System.Drawing.Size(100, 25);
            this.tbInRadius.TabIndex = 9;
            // 
            // lbLenRange
            // 
            this.lbLenRange.AutoSize = true;
            this.lbLenRange.Location = new System.Drawing.Point(223, 114);
            this.lbLenRange.Name = "lbLenRange";
            this.lbLenRange.Size = new System.Drawing.Size(67, 15);
            this.lbLenRange.TabIndex = 8;
            this.lbLenRange.Text = "长度范围";
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(104, 109);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(100, 25);
            this.tbLength.TabIndex = 5;
            // 
            // lbLength
            // 
            this.lbLength.AutoSize = true;
            this.lbLength.Location = new System.Drawing.Point(13, 114);
            this.lbLength.Name = "lbLength";
            this.lbLength.Size = new System.Drawing.Size(85, 15);
            this.lbLength.TabIndex = 9;
            this.lbLength.Text = "长度(mm) *";
            // 
            // lbInterA
            // 
            this.lbInterA.AutoSize = true;
            this.lbInterA.Location = new System.Drawing.Point(29, 71);
            this.lbInterA.Name = "lbInterA";
            this.lbInterA.Size = new System.Drawing.Size(69, 15);
            this.lbInterA.TabIndex = 10;
            this.lbInterA.Text = "接口 A *";
            // 
            // lbWeight
            // 
            this.lbWeight.AutoSize = true;
            this.lbWeight.Location = new System.Drawing.Point(61, 158);
            this.lbWeight.Name = "lbWeight";
            this.lbWeight.Size = new System.Drawing.Size(37, 15);
            this.lbWeight.TabIndex = 9;
            this.lbWeight.Text = "重量";
            // 
            // tbWeight
            // 
            this.tbWeight.Location = new System.Drawing.Point(104, 153);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(100, 25);
            this.tbWeight.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(146, 216);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(240, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(45, 28);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(53, 15);
            this.lbName.TabIndex = 10;
            this.lbName.Text = "名称 *";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(104, 23);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 25);
            this.tbName.TabIndex = 1;
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(237, 28);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(53, 15);
            this.lbNumber.TabIndex = 7;
            this.lbNumber.Text = "货号 *";
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(296, 23);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(100, 25);
            this.tbNumber.TabIndex = 2;
            // 
            // lbInner
            // 
            this.lbInner.AutoSize = true;
            this.lbInner.Location = new System.Drawing.Point(253, 158);
            this.lbInner.Name = "lbInner";
            this.lbInner.Size = new System.Drawing.Size(37, 15);
            this.lbInner.TabIndex = 8;
            this.lbInner.Text = "内径";
            // 
            // tbLenMin
            // 
            this.tbLenMin.Location = new System.Drawing.Point(296, 109);
            this.tbLenMin.Name = "tbLenMin";
            this.tbLenMin.Size = new System.Drawing.Size(37, 25);
            this.tbLenMin.TabIndex = 6;
            // 
            // tbLenMax
            // 
            this.tbLenMax.Location = new System.Drawing.Point(360, 109);
            this.tbLenMax.Name = "tbLenMax";
            this.tbLenMax.Size = new System.Drawing.Size(37, 25);
            this.tbLenMax.TabIndex = 7;
            // 
            // lbRangeInter
            // 
            this.lbRangeInter.AutoSize = true;
            this.lbRangeInter.Location = new System.Drawing.Point(339, 114);
            this.lbRangeInter.Name = "lbRangeInter";
            this.lbRangeInter.Size = new System.Drawing.Size(15, 15);
            this.lbRangeInter.TabIndex = 8;
            this.lbRangeInter.Text = "-";
            // 
            // cbInterA
            // 
            this.cbInterA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterA.FormattingEnabled = true;
            this.cbInterA.Location = new System.Drawing.Point(104, 67);
            this.cbInterA.Name = "cbInterA";
            this.cbInterA.Size = new System.Drawing.Size(100, 23);
            this.cbInterA.TabIndex = 12;
            // 
            // cbInterB
            // 
            this.cbInterB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterB.FormattingEnabled = true;
            this.cbInterB.Location = new System.Drawing.Point(296, 67);
            this.cbInterB.Name = "cbInterB";
            this.cbInterB.Size = new System.Drawing.Size(100, 23);
            this.cbInterB.TabIndex = 12;
            // 
            // MidRing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 259);
            this.Controls.Add(this.cbInterB);
            this.Controls.Add(this.cbInterA);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.lbNumber);
            this.Controls.Add(this.lbInterB);
            this.Controls.Add(this.tbLenMax);
            this.Controls.Add(this.tbLenMin);
            this.Controls.Add(this.lbRangeInter);
            this.Controls.Add(this.lbInner);
            this.Controls.Add(this.tbInRadius);
            this.Controls.Add(this.lbLenRange);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.lbWeight);
            this.Controls.Add(this.tbLength);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbLength);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbInterA);
            this.Name = "MidRing";
            this.Text = "MidRing";
            this.Load += new System.EventHandler(this.MidRing_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInterB;
        private System.Windows.Forms.TextBox tbInRadius;
        private System.Windows.Forms.Label lbLenRange;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.Label lbLength;
        private System.Windows.Forms.Label lbInterA;
        private System.Windows.Forms.Label lbWeight;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label lbInner;
        private System.Windows.Forms.TextBox tbLenMin;
        private System.Windows.Forms.TextBox tbLenMax;
        private System.Windows.Forms.Label lbRangeInter;
        private System.Windows.Forms.ComboBox cbInterA;
        private System.Windows.Forms.ComboBox cbInterB;
    }
}