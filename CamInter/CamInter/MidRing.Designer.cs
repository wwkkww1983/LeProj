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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MidRing));
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
            resources.ApplyResources(this.lbInterB, "lbInterB");
            this.lbInterB.Name = "lbInterB";
            // 
            // tbInRadius
            // 
            resources.ApplyResources(this.tbInRadius, "tbInRadius");
            this.tbInRadius.Name = "tbInRadius";
            // 
            // lbLenRange
            // 
            resources.ApplyResources(this.lbLenRange, "lbLenRange");
            this.lbLenRange.Name = "lbLenRange";
            // 
            // tbLength
            // 
            resources.ApplyResources(this.tbLength, "tbLength");
            this.tbLength.Name = "tbLength";
            this.tbLength.TextChanged += new System.EventHandler(this.tbLength_TextChanged);
            // 
            // lbLength
            // 
            resources.ApplyResources(this.lbLength, "lbLength");
            this.lbLength.Name = "lbLength";
            // 
            // lbInterA
            // 
            resources.ApplyResources(this.lbInterA, "lbInterA");
            this.lbInterA.Name = "lbInterA";
            // 
            // lbWeight
            // 
            resources.ApplyResources(this.lbWeight, "lbWeight");
            this.lbWeight.Name = "lbWeight";
            // 
            // tbWeight
            // 
            resources.ApplyResources(this.tbWeight, "tbWeight");
            this.tbWeight.Name = "tbWeight";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbName
            // 
            resources.ApplyResources(this.lbName, "lbName");
            this.lbName.Name = "lbName";
            // 
            // tbName
            // 
            resources.ApplyResources(this.tbName, "tbName");
            this.tbName.Name = "tbName";
            // 
            // lbNumber
            // 
            resources.ApplyResources(this.lbNumber, "lbNumber");
            this.lbNumber.Name = "lbNumber";
            // 
            // tbNumber
            // 
            resources.ApplyResources(this.tbNumber, "tbNumber");
            this.tbNumber.Name = "tbNumber";
            // 
            // lbInner
            // 
            resources.ApplyResources(this.lbInner, "lbInner");
            this.lbInner.Name = "lbInner";
            // 
            // tbLenMin
            // 
            resources.ApplyResources(this.tbLenMin, "tbLenMin");
            this.tbLenMin.Name = "tbLenMin";
            // 
            // tbLenMax
            // 
            resources.ApplyResources(this.tbLenMax, "tbLenMax");
            this.tbLenMax.Name = "tbLenMax";
            // 
            // lbRangeInter
            // 
            resources.ApplyResources(this.lbRangeInter, "lbRangeInter");
            this.lbRangeInter.Name = "lbRangeInter";
            // 
            // cbInterA
            // 
            this.cbInterA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterA.FormattingEnabled = true;
            resources.ApplyResources(this.cbInterA, "cbInterA");
            this.cbInterA.Name = "cbInterA";
            // 
            // cbInterB
            // 
            this.cbInterB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterB.FormattingEnabled = true;
            resources.ApplyResources(this.cbInterB, "cbInterB");
            this.cbInterB.Name = "cbInterB";
            // 
            // MidRing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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