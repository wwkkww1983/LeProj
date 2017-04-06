namespace SimuProteus
{
    partial class FormFootParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFootParam));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIdx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvFoots = new System.Windows.Forms.DataGridView();
            this.innerIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.footName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.footType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoots)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(331, 454);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbNumber);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbIdx);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tbName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(323, 425);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "元器件";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbNumber
            // 
            this.tbNumber.Enabled = false;
            this.tbNumber.Location = new System.Drawing.Point(89, 143);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(174, 25);
            this.tbNumber.TabIndex = 1;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "ID";
            // 
            // tbIdx
            // 
            this.tbIdx.Enabled = false;
            this.tbIdx.Location = new System.Drawing.Point(89, 98);
            this.tbIdx.Name = "tbIdx";
            this.tbIdx.Size = new System.Drawing.Size(174, 25);
            this.tbIdx.TabIndex = 1;
            this.tbIdx.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "编号";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(89, 50);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(174, 25);
            this.tbName.TabIndex = 1;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvFoots);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(323, 425);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "管脚";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvFoots
            // 
            this.dgvFoots.AllowUserToAddRows = false;
            this.dgvFoots.AllowUserToDeleteRows = false;
            this.dgvFoots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFoots.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.innerIdx,
            this.footName,
            this.idx,
            this.footType});
            this.dgvFoots.Location = new System.Drawing.Point(3, 6);
            this.dgvFoots.Name = "dgvFoots";
            this.dgvFoots.RowTemplate.Height = 27;
            this.dgvFoots.Size = new System.Drawing.Size(317, 413);
            this.dgvFoots.TabIndex = 0;
            this.dgvFoots.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFoots_CellEndEdit);
            // 
            // innerIdx
            // 
            this.innerIdx.DataPropertyName = "innerIdx";
            this.innerIdx.HeaderText = "编号";
            this.innerIdx.Name = "innerIdx";
            this.innerIdx.ReadOnly = true;
            // 
            // footName
            // 
            this.footName.DataPropertyName = "footName";
            this.footName.HeaderText = "名称";
            this.footName.Name = "footName";
            // 
            // idx
            // 
            this.idx.DataPropertyName = "idx";
            this.idx.HeaderText = "idx";
            this.idx.Name = "idx";
            this.idx.Visible = false;
            // 
            // footType
            // 
            this.footType.DataPropertyName = "footType";
            this.footType.HeaderText = "类型";
            this.footType.Items.AddRange(new object[] {
            "Input",
            "Output",
            "Power",
            "OD",
            "OC"});
            this.footType.Name = "footType";
            this.footType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.footType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // FormFootParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 478);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFootParam";
            this.Text = "管脚设置";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoots)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.DataGridView dgvFoots;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbIdx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn innerIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn footName;
        private System.Windows.Forms.DataGridViewTextBoxColumn idx;
        private System.Windows.Forms.DataGridViewComboBoxColumn footType;
    }
}