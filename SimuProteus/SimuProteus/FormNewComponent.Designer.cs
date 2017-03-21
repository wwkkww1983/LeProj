namespace SimuProteus
{
    partial class FormNewComponent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewComponent));
            this.picBoxImg = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.lbInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.dgvFoot = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.locX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameLocX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameLocY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numLocX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numLocY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbLoc = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbChips = new System.Windows.Forms.RadioButton();
            this.rbComponent = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoot)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxImg
            // 
            this.picBoxImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxImg.Location = new System.Drawing.Point(43, 56);
            this.picBoxImg.Name = "picBoxImg";
            this.picBoxImg.Size = new System.Drawing.Size(95, 94);
            this.picBoxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxImg.TabIndex = 0;
            this.picBoxImg.TabStop = false;
            this.picBoxImg.Click += new System.EventHandler(this.picBoxImg_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(326, 452);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(447, 452);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "宽度";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(323, 116);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(71, 25);
            this.tbWidth.TabIndex = 4;
            this.tbWidth.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbWidth_MouseClick);
            this.tbWidth.TextChanged += new System.EventHandler(this.tbSize_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(495, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "高度";
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(538, 116);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(71, 25);
            this.tbHeight.TabIndex = 5;
            this.tbHeight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbHeight_MouseClick);
            this.tbHeight.TextChanged += new System.EventHandler(this.tbSize_TextChanged);
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Location = new System.Drawing.Point(40, 493);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(37, 15);
            this.lbInfo.TabIndex = 4;
            this.lbInfo.Text = "说明";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(465, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "类型名称";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(538, 56);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(237, 25);
            this.tbName.TabIndex = 3;
            this.tbName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbName_MouseClick);
            // 
            // dgvFoot
            // 
            this.dgvFoot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFoot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.txtName,
            this.type,
            this.locX,
            this.locY,
            this.nameLocX,
            this.nameLocY,
            this.numLocX,
            this.numLocY});
            this.dgvFoot.Location = new System.Drawing.Point(43, 177);
            this.dgvFoot.Name = "dgvFoot";
            this.dgvFoot.RowTemplate.Height = 27;
            this.dgvFoot.Size = new System.Drawing.Size(775, 251);
            this.dgvFoot.TabIndex = 6;
            this.dgvFoot.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFoot_CellEndEdit);
            this.dgvFoot.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFoot_CellMouseClick);
            this.dgvFoot.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvFoot_RowsAdded);
            // 
            // Number
            // 
            this.Number.HeaderText = "编号";
            this.Number.MinimumWidth = 20;
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Width = 60;
            // 
            // txtName
            // 
            this.txtName.HeaderText = "名称";
            this.txtName.Name = "txtName";
            // 
            // type
            // 
            this.type.HeaderText = "类型";
            this.type.Items.AddRange(new object[] {
            "Input",
            "Output",
            "OC",
            "OD",
            "Power"});
            this.type.Name = "type";
            // 
            // locX
            // 
            this.locX.HeaderText = "X坐标";
            this.locX.MinimumWidth = 20;
            this.locX.Name = "locX";
            this.locX.Width = 70;
            // 
            // locY
            // 
            this.locY.HeaderText = "Y坐标";
            this.locY.MinimumWidth = 20;
            this.locY.Name = "locY";
            this.locY.Width = 70;
            // 
            // nameLocX
            // 
            this.nameLocX.HeaderText = "名称X坐标";
            this.nameLocX.Name = "nameLocX";
            // 
            // nameLocY
            // 
            this.nameLocY.HeaderText = "名称Y坐标";
            this.nameLocY.Name = "nameLocY";
            // 
            // numLocX
            // 
            this.numLocX.HeaderText = "编号X坐标";
            this.numLocX.Name = "numLocX";
            // 
            // numLocY
            // 
            this.numLocY.HeaderText = "编号Y坐标";
            this.numLocY.Name = "numLocY";
            // 
            // lbLoc
            // 
            this.lbLoc.AutoSize = true;
            this.lbLoc.Location = new System.Drawing.Point(136, 135);
            this.lbLoc.Name = "lbLoc";
            this.lbLoc.Size = new System.Drawing.Size(37, 15);
            this.lbLoc.TabIndex = 6;
            this.lbLoc.Text = "坐标";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "(0,0)";
            // 
            // rbChips
            // 
            this.rbChips.AutoSize = true;
            this.rbChips.Location = new System.Drawing.Point(336, 59);
            this.rbChips.Name = "rbChips";
            this.rbChips.Size = new System.Drawing.Size(58, 19);
            this.rbChips.TabIndex = 2;
            this.rbChips.Text = "芯片";
            this.rbChips.UseVisualStyleBackColor = true;
            // 
            // rbComponent
            // 
            this.rbComponent.AutoSize = true;
            this.rbComponent.Checked = true;
            this.rbComponent.Location = new System.Drawing.Point(228, 59);
            this.rbComponent.Name = "rbComponent";
            this.rbComponent.Size = new System.Drawing.Size(73, 19);
            this.rbComponent.TabIndex = 1;
            this.rbComponent.TabStop = true;
            this.rbComponent.Text = "元器件";
            this.rbComponent.UseVisualStyleBackColor = true;
            // 
            // FormNewComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 526);
            this.Controls.Add(this.rbComponent);
            this.Controls.Add(this.rbChips);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbLoc);
            this.Controls.Add(this.dgvFoot);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.picBoxImg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(869, 571);
            this.MinimumSize = new System.Drawing.Size(869, 571);
            this.Name = "FormNewComponent";
            this.Text = "新增元器件";
            this.Load += new System.EventHandler(this.FormNewComponent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxImg;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.DataGridView dgvFoot;
        private System.Windows.Forms.Label lbLoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbChips;
        private System.Windows.Forms.RadioButton rbComponent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtName;
        private System.Windows.Forms.DataGridViewComboBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn locX;
        private System.Windows.Forms.DataGridViewTextBoxColumn locY;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameLocX;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameLocY;
        private System.Windows.Forms.DataGridViewTextBoxColumn numLocX;
        private System.Windows.Forms.DataGridViewTextBoxColumn numLocY;
    }
}