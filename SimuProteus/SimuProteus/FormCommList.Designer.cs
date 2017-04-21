namespace SimuProteus
{
    partial class FormCommList
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
            this.dgvDataList = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDataList
            // 
            this.dgvDataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.direct,
            this.Content,
            this.time});
            this.dgvDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataList.Location = new System.Drawing.Point(0, 0);
            this.dgvDataList.Name = "dgvDataList";
            this.dgvDataList.RowTemplate.Height = 27;
            this.dgvDataList.Size = new System.Drawing.Size(860, 407);
            this.dgvDataList.TabIndex = 0;
            // 
            // number
            // 
            this.number.DataPropertyName = "number";
            this.number.Frozen = true;
            this.number.HeaderText = "序号";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            // 
            // direct
            // 
            this.direct.DataPropertyName = "direct";
            this.direct.HeaderText = "方向";
            this.direct.Name = "direct";
            this.direct.ReadOnly = true;
            // 
            // Content
            // 
            this.Content.DataPropertyName = "content";
            this.Content.HeaderText = "内容";
            this.Content.Name = "Content";
            this.Content.ReadOnly = true;
            this.Content.Width = 500;
            // 
            // time
            // 
            this.time.DataPropertyName = "time";
            this.time.HeaderText = "时间";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            // 
            // FormCommList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 407);
            this.Controls.Add(this.dgvDataList);
            this.Name = "FormCommList";
            this.Text = "串口通信历史";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDataList;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn direct;
        private System.Windows.Forms.DataGridViewTextBoxColumn Content;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
    }
}