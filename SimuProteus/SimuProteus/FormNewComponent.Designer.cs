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
            this.locX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameLocX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameLocY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numLocX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numLocY = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnAdd.Location = new System.Drawing.Point(168, 451);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(289, 451);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "高度";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(262, 118);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(71, 25);
            this.tbWidth.TabIndex = 3;
            this.tbWidth.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbWidth_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "宽度";
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(428, 118);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(71, 25);
            this.tbHeight.TabIndex = 3;
            this.tbHeight.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbHeight_MouseClick);
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Location = new System.Drawing.Point(9, 499);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(37, 15);
            this.lbInfo.TabIndex = 4;
            this.lbInfo.Text = "说明";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "类型名称";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(292, 56);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(207, 25);
            this.tbName.TabIndex = 3;
            this.tbName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbName_MouseClick);
            // 
            // dgvFoot
            // 
            this.dgvFoot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFoot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.txtName,
            this.locX,
            this.locY,
            this.nameLocX,
            this.nameLocY,
            this.numLocX,
            this.numLocY});
            this.dgvFoot.Location = new System.Drawing.Point(43, 177);
            this.dgvFoot.Name = "dgvFoot";
            this.dgvFoot.RowTemplate.Height = 27;
            this.dgvFoot.Size = new System.Drawing.Size(456, 251);
            this.dgvFoot.TabIndex = 5;
            this.dgvFoot.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFoot_CellMouseClick);
            // 
            // Number
            // 
            this.Number.HeaderText = "编号";
            this.Number.Name = "Number";
            // 
            // txtName
            // 
            this.txtName.HeaderText = "名称";
            this.txtName.Name = "txtName";
            // 
            // locX
            // 
            this.locX.HeaderText = "X像素";
            this.locX.Name = "locX";
            // 
            // locY
            // 
            this.locY.HeaderText = "Y像素";
            this.locY.Name = "locY";
            // 
            // nameLocX
            // 
            this.nameLocX.HeaderText = "名称X像素";
            this.nameLocX.Name = "nameLocX";
            // 
            // nameLocY
            // 
            this.nameLocY.HeaderText = "名称Y像素";
            this.nameLocY.Name = "nameLocY";
            // 
            // numLocX
            // 
            this.numLocX.HeaderText = "编号X像素";
            this.numLocX.Name = "numLocX";
            // 
            // numLocY
            // 
            this.numLocY.HeaderText = "编号Y像素";
            this.numLocY.Name = "numLocY";
            // 
            // FormNewComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 526);
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
            this.Name = "FormNewComponent";
            this.Text = "新增元器件";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtName;
        private System.Windows.Forms.DataGridViewTextBoxColumn locX;
        private System.Windows.Forms.DataGridViewTextBoxColumn locY;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameLocX;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameLocY;
        private System.Windows.Forms.DataGridViewTextBoxColumn numLocX;
        private System.Windows.Forms.DataGridViewTextBoxColumn numLocY;
    }
}