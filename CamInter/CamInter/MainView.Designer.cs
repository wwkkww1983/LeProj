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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.dgvProjList = new System.Windows.Forms.DataGridView();
            this.Idx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lens = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.focus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adapter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fovLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fovWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CamLensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enlishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvProjDetail = new System.Windows.Forms.DataGridView();
            this.detailIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.interA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.interB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcCamera = new System.Windows.Forms.TabControl();
            this.tpArea = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbValueWidth = new System.Windows.Forms.Label();
            this.lbValueLength = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbFovSide = new System.Windows.Forms.TextBox();
            this.tbFovOther = new System.Windows.Forms.TextBox();
            this.tbResolutionSide = new System.Windows.Forms.TextBox();
            this.tbResolutionOther = new System.Windows.Forms.TextBox();
            this.lbFov = new System.Windows.Forms.Label();
            this.tbSensorSide = new System.Windows.Forms.TextBox();
            this.lbResolution = new System.Windows.Forms.Label();
            this.tbSensorOther = new System.Windows.Forms.TextBox();
            this.lbSensor = new System.Windows.Forms.Label();
            this.tpLine = new System.Windows.Forms.TabPage();
            this.rbLineWidth = new System.Windows.Forms.RadioButton();
            this.rbLineLength = new System.Windows.Forms.RadioButton();
            this.tbLineFov = new System.Windows.Forms.TextBox();
            this.tbLineResolution = new System.Windows.Forms.TextBox();
            this.lbLineFov = new System.Windows.Forms.Label();
            this.tbLineSensor = new System.Windows.Forms.TextBox();
            this.lbLineResolution = new System.Windows.Forms.Label();
            this.lbLineSensor = new System.Windows.Forms.Label();
            this.cbCamInter = new System.Windows.Forms.ComboBox();
            this.lbCamInter = new System.Windows.Forms.Label();
            this.lbFlange = new System.Windows.Forms.Label();
            this.tbFlange = new System.Windows.Forms.TextBox();
            this.lbSensorDiagonal = new System.Windows.Forms.Label();
            this.tbTarget = new System.Windows.Forms.TextBox();
            this.lbWork = new System.Windows.Forms.Label();
            this.tbDistance = new System.Windows.Forms.TextBox();
            this.tbDistanRange = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjDetail)).BeginInit();
            this.tcCamera.SuspendLayout();
            this.tpArea.SuspendLayout();
            this.tpLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProjList
            // 
            this.dgvProjList.AllowUserToAddRows = false;
            this.dgvProjList.AllowUserToDeleteRows = false;
            this.dgvProjList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Idx,
            this.lens,
            this.focus,
            this.adapter,
            this.extend,
            this.ratio,
            this.workDistance,
            this.fovLength,
            this.fovWidth});
            resources.ApplyResources(this.dgvProjList, "dgvProjList");
            this.dgvProjList.Name = "dgvProjList";
            this.dgvProjList.ReadOnly = true;
            this.dgvProjList.RowTemplate.Height = 27;
            this.dgvProjList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProjList_CellClick);
            // 
            // Idx
            // 
            this.Idx.DataPropertyName = "Idx";
            this.Idx.Frozen = true;
            resources.ApplyResources(this.Idx, "Idx");
            this.Idx.Name = "Idx";
            this.Idx.ReadOnly = true;
            // 
            // lens
            // 
            this.lens.DataPropertyName = "lens";
            resources.ApplyResources(this.lens, "lens");
            this.lens.Name = "lens";
            this.lens.ReadOnly = true;
            // 
            // focus
            // 
            this.focus.DataPropertyName = "focus";
            resources.ApplyResources(this.focus, "focus");
            this.focus.Name = "focus";
            this.focus.ReadOnly = true;
            // 
            // adapter
            // 
            this.adapter.DataPropertyName = "adapter";
            resources.ApplyResources(this.adapter, "adapter");
            this.adapter.Name = "adapter";
            this.adapter.ReadOnly = true;
            // 
            // extend
            // 
            this.extend.DataPropertyName = "extend";
            resources.ApplyResources(this.extend, "extend");
            this.extend.Name = "extend";
            this.extend.ReadOnly = true;
            // 
            // ratio
            // 
            this.ratio.DataPropertyName = "ratio";
            resources.ApplyResources(this.ratio, "ratio");
            this.ratio.Name = "ratio";
            this.ratio.ReadOnly = true;
            // 
            // workDistance
            // 
            this.workDistance.DataPropertyName = "workDistance";
            resources.ApplyResources(this.workDistance, "workDistance");
            this.workDistance.Name = "workDistance";
            this.workDistance.ReadOnly = true;
            // 
            // fovLength
            // 
            this.fovLength.DataPropertyName = "fovLength";
            resources.ApplyResources(this.fovLength, "fovLength");
            this.fovLength.Name = "fovLength";
            this.fovLength.ReadOnly = true;
            // 
            // fovWidth
            // 
            this.fovWidth.DataPropertyName = "fovWidth";
            resources.ApplyResources(this.fovWidth, "fovWidth");
            this.fovWidth.Name = "fovWidth";
            this.fovWidth.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addItemToolStripMenuItem,
            this.lanToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // addItemToolStripMenuItem
            // 
            this.addItemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CamLensToolStripMenuItem,
            this.FocusToolStripMenuItem,
            this.AdapterToolStripMenuItem,
            this.ExtendToolStripMenuItem,
            this.connToolStripMenuItem});
            this.addItemToolStripMenuItem.Name = "addItemToolStripMenuItem";
            resources.ApplyResources(this.addItemToolStripMenuItem, "addItemToolStripMenuItem");
            // 
            // CamLensToolStripMenuItem
            // 
            this.CamLensToolStripMenuItem.Name = "CamLensToolStripMenuItem";
            resources.ApplyResources(this.CamLensToolStripMenuItem, "CamLensToolStripMenuItem");
            this.CamLensToolStripMenuItem.Click += new System.EventHandler(this.CamLensToolStripMenuItem_Click);
            // 
            // FocusToolStripMenuItem
            // 
            this.FocusToolStripMenuItem.Name = "FocusToolStripMenuItem";
            resources.ApplyResources(this.FocusToolStripMenuItem, "FocusToolStripMenuItem");
            this.FocusToolStripMenuItem.Click += new System.EventHandler(this.FocusToolStripMenuItem_Click);
            // 
            // AdapterToolStripMenuItem
            // 
            this.AdapterToolStripMenuItem.Name = "AdapterToolStripMenuItem";
            resources.ApplyResources(this.AdapterToolStripMenuItem, "AdapterToolStripMenuItem");
            this.AdapterToolStripMenuItem.Click += new System.EventHandler(this.AdapterToolStripMenuItem_Click);
            // 
            // ExtendToolStripMenuItem
            // 
            this.ExtendToolStripMenuItem.Name = "ExtendToolStripMenuItem";
            resources.ApplyResources(this.ExtendToolStripMenuItem, "ExtendToolStripMenuItem");
            this.ExtendToolStripMenuItem.Click += new System.EventHandler(this.ExtendToolStripMenuItem_Click);
            // 
            // connToolStripMenuItem
            // 
            this.connToolStripMenuItem.Name = "connToolStripMenuItem";
            resources.ApplyResources(this.connToolStripMenuItem, "connToolStripMenuItem");
            this.connToolStripMenuItem.Click += new System.EventHandler(this.connToolStripMenuItem_Click);
            // 
            // lanToolStripMenuItem
            // 
            this.lanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chineseToolStripMenuItem,
            this.enlishToolStripMenuItem});
            this.lanToolStripMenuItem.Name = "lanToolStripMenuItem";
            resources.ApplyResources(this.lanToolStripMenuItem, "lanToolStripMenuItem");
            // 
            // chineseToolStripMenuItem
            // 
            this.chineseToolStripMenuItem.Name = "chineseToolStripMenuItem";
            resources.ApplyResources(this.chineseToolStripMenuItem, "chineseToolStripMenuItem");
            this.chineseToolStripMenuItem.Click += new System.EventHandler(this.lanToolStripMenuItem_Click);
            // 
            // enlishToolStripMenuItem
            // 
            this.enlishToolStripMenuItem.Name = "enlishToolStripMenuItem";
            resources.ApplyResources(this.enlishToolStripMenuItem, "enlishToolStripMenuItem");
            this.enlishToolStripMenuItem.Click += new System.EventHandler(this.lanToolStripMenuItem_Click);
            // 
            // dgvProjDetail
            // 
            this.dgvProjDetail.AllowUserToAddRows = false;
            this.dgvProjDetail.AllowUserToDeleteRows = false;
            this.dgvProjDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detailIdx,
            this.type,
            this.name,
            this.number,
            this.interA,
            this.interB,
            this.length});
            resources.ApplyResources(this.dgvProjDetail, "dgvProjDetail");
            this.dgvProjDetail.Name = "dgvProjDetail";
            this.dgvProjDetail.ReadOnly = true;
            this.dgvProjDetail.RowTemplate.Height = 27;
            // 
            // detailIdx
            // 
            this.detailIdx.DataPropertyName = "detailIdx";
            this.detailIdx.Frozen = true;
            resources.ApplyResources(this.detailIdx, "detailIdx");
            this.detailIdx.Name = "detailIdx";
            this.detailIdx.ReadOnly = true;
            // 
            // type
            // 
            this.type.DataPropertyName = "type";
            resources.ApplyResources(this.type, "type");
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            resources.ApplyResources(this.name, "name");
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // number
            // 
            this.number.DataPropertyName = "number";
            resources.ApplyResources(this.number, "number");
            this.number.Name = "number";
            this.number.ReadOnly = true;
            // 
            // interA
            // 
            this.interA.DataPropertyName = "interA";
            resources.ApplyResources(this.interA, "interA");
            this.interA.Name = "interA";
            this.interA.ReadOnly = true;
            // 
            // interB
            // 
            this.interB.DataPropertyName = "interB";
            resources.ApplyResources(this.interB, "interB");
            this.interB.Name = "interB";
            this.interB.ReadOnly = true;
            // 
            // length
            // 
            this.length.DataPropertyName = "length";
            resources.ApplyResources(this.length, "length");
            this.length.Name = "length";
            this.length.ReadOnly = true;
            // 
            // tcCamera
            // 
            this.tcCamera.Controls.Add(this.tpArea);
            this.tcCamera.Controls.Add(this.tpLine);
            resources.ApplyResources(this.tcCamera, "tcCamera");
            this.tcCamera.Name = "tcCamera";
            this.tcCamera.SelectedIndex = 0;
            // 
            // tpArea
            // 
            this.tpArea.Controls.Add(this.label16);
            this.tpArea.Controls.Add(this.label14);
            this.tpArea.Controls.Add(this.lbValueWidth);
            this.tpArea.Controls.Add(this.lbValueLength);
            this.tpArea.Controls.Add(this.label11);
            this.tpArea.Controls.Add(this.tbFovSide);
            this.tpArea.Controls.Add(this.tbFovOther);
            this.tpArea.Controls.Add(this.tbResolutionSide);
            this.tpArea.Controls.Add(this.tbResolutionOther);
            this.tpArea.Controls.Add(this.lbFov);
            this.tpArea.Controls.Add(this.tbSensorSide);
            this.tpArea.Controls.Add(this.lbResolution);
            this.tpArea.Controls.Add(this.tbSensorOther);
            this.tpArea.Controls.Add(this.lbSensor);
            resources.ApplyResources(this.tpArea, "tpArea");
            this.tpArea.Name = "tpArea";
            this.tpArea.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // lbValueWidth
            // 
            resources.ApplyResources(this.lbValueWidth, "lbValueWidth");
            this.lbValueWidth.Name = "lbValueWidth";
            // 
            // lbValueLength
            // 
            resources.ApplyResources(this.lbValueLength, "lbValueLength");
            this.lbValueLength.Name = "lbValueLength";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // tbFovSide
            // 
            resources.ApplyResources(this.tbFovSide, "tbFovSide");
            this.tbFovSide.Name = "tbFovSide";
            // 
            // tbFovOther
            // 
            resources.ApplyResources(this.tbFovOther, "tbFovOther");
            this.tbFovOther.Name = "tbFovOther";
            // 
            // tbResolutionSide
            // 
            resources.ApplyResources(this.tbResolutionSide, "tbResolutionSide");
            this.tbResolutionSide.Name = "tbResolutionSide";
            // 
            // tbResolutionOther
            // 
            resources.ApplyResources(this.tbResolutionOther, "tbResolutionOther");
            this.tbResolutionOther.Name = "tbResolutionOther";
            // 
            // lbFov
            // 
            resources.ApplyResources(this.lbFov, "lbFov");
            this.lbFov.Name = "lbFov";
            // 
            // tbSensorSide
            // 
            resources.ApplyResources(this.tbSensorSide, "tbSensorSide");
            this.tbSensorSide.Name = "tbSensorSide";
            this.tbSensorSide.TextChanged += new System.EventHandler(this.tbSensor_TextChanged);
            // 
            // lbResolution
            // 
            resources.ApplyResources(this.lbResolution, "lbResolution");
            this.lbResolution.Name = "lbResolution";
            // 
            // tbSensorOther
            // 
            resources.ApplyResources(this.tbSensorOther, "tbSensorOther");
            this.tbSensorOther.Name = "tbSensorOther";
            this.tbSensorOther.TextChanged += new System.EventHandler(this.tbSensor_TextChanged);
            // 
            // lbSensor
            // 
            resources.ApplyResources(this.lbSensor, "lbSensor");
            this.lbSensor.Name = "lbSensor";
            // 
            // tpLine
            // 
            this.tpLine.Controls.Add(this.rbLineWidth);
            this.tpLine.Controls.Add(this.rbLineLength);
            this.tpLine.Controls.Add(this.tbLineFov);
            this.tpLine.Controls.Add(this.tbLineResolution);
            this.tpLine.Controls.Add(this.lbLineFov);
            this.tpLine.Controls.Add(this.tbLineSensor);
            this.tpLine.Controls.Add(this.lbLineResolution);
            this.tpLine.Controls.Add(this.lbLineSensor);
            resources.ApplyResources(this.tpLine, "tpLine");
            this.tpLine.Name = "tpLine";
            this.tpLine.UseVisualStyleBackColor = true;
            // 
            // rbLineWidth
            // 
            resources.ApplyResources(this.rbLineWidth, "rbLineWidth");
            this.rbLineWidth.Name = "rbLineWidth";
            this.rbLineWidth.UseVisualStyleBackColor = true;
            // 
            // rbLineLength
            // 
            resources.ApplyResources(this.rbLineLength, "rbLineLength");
            this.rbLineLength.Checked = true;
            this.rbLineLength.Name = "rbLineLength";
            this.rbLineLength.TabStop = true;
            this.rbLineLength.UseVisualStyleBackColor = true;
            // 
            // tbLineFov
            // 
            resources.ApplyResources(this.tbLineFov, "tbLineFov");
            this.tbLineFov.Name = "tbLineFov";
            // 
            // tbLineResolution
            // 
            resources.ApplyResources(this.tbLineResolution, "tbLineResolution");
            this.tbLineResolution.Name = "tbLineResolution";
            // 
            // lbLineFov
            // 
            resources.ApplyResources(this.lbLineFov, "lbLineFov");
            this.lbLineFov.Name = "lbLineFov";
            // 
            // tbLineSensor
            // 
            resources.ApplyResources(this.tbLineSensor, "tbLineSensor");
            this.tbLineSensor.Name = "tbLineSensor";
            // 
            // lbLineResolution
            // 
            resources.ApplyResources(this.lbLineResolution, "lbLineResolution");
            this.lbLineResolution.Name = "lbLineResolution";
            // 
            // lbLineSensor
            // 
            resources.ApplyResources(this.lbLineSensor, "lbLineSensor");
            this.lbLineSensor.Name = "lbLineSensor";
            // 
            // cbCamInter
            // 
            this.cbCamInter.FormattingEnabled = true;
            resources.ApplyResources(this.cbCamInter, "cbCamInter");
            this.cbCamInter.Name = "cbCamInter";
            // 
            // lbCamInter
            // 
            resources.ApplyResources(this.lbCamInter, "lbCamInter");
            this.lbCamInter.Name = "lbCamInter";
            // 
            // lbFlange
            // 
            resources.ApplyResources(this.lbFlange, "lbFlange");
            this.lbFlange.Name = "lbFlange";
            // 
            // tbFlange
            // 
            resources.ApplyResources(this.tbFlange, "tbFlange");
            this.tbFlange.Name = "tbFlange";
            // 
            // lbSensorDiagonal
            // 
            resources.ApplyResources(this.lbSensorDiagonal, "lbSensorDiagonal");
            this.lbSensorDiagonal.Name = "lbSensorDiagonal";
            // 
            // tbTarget
            // 
            resources.ApplyResources(this.tbTarget, "tbTarget");
            this.tbTarget.Name = "tbTarget";
            // 
            // lbWork
            // 
            resources.ApplyResources(this.lbWork, "lbWork");
            this.lbWork.Name = "lbWork";
            // 
            // tbDistance
            // 
            resources.ApplyResources(this.tbDistance, "tbDistance");
            this.tbDistance.Name = "tbDistance";
            // 
            // tbDistanRange
            // 
            resources.ApplyResources(this.tbDistanRange, "tbDistanRange");
            this.tbDistanRange.Name = "tbDistanRange";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // btnSelect
            // 
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // MainView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tcCamera);
            this.Controls.Add(this.dgvProjDetail);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.dgvProjList);
            this.Controls.Add(this.tbTarget);
            this.Controls.Add(this.tbFlange);
            this.Controls.Add(this.tbDistanRange);
            this.Controls.Add(this.tbDistance);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.cbCamInter);
            this.Controls.Add(this.lbSensorDiagonal);
            this.Controls.Add(this.lbWork);
            this.Controls.Add(this.lbCamInter);
            this.Controls.Add(this.lbFlange);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainView";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjList)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjDetail)).EndInit();
            this.tcCamera.ResumeLayout(false);
            this.tpArea.ResumeLayout(false);
            this.tpArea.PerformLayout();
            this.tpLine.ResumeLayout(false);
            this.tpLine.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProjList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CamLensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FocusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AdapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvProjDetail;
        private System.Windows.Forms.ToolStripMenuItem lanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enlishToolStripMenuItem;
        private System.Windows.Forms.TabControl tcCamera;
        private System.Windows.Forms.TabPage tpArea;
        private System.Windows.Forms.TabPage tpLine;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbSensorSide;
        private System.Windows.Forms.TextBox tbSensorOther;
        private System.Windows.Forms.Label lbSensor;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbResolutionSide;
        private System.Windows.Forms.TextBox tbResolutionOther;
        private System.Windows.Forms.Label lbResolution;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbFovSide;
        private System.Windows.Forms.TextBox tbFovOther;
        private System.Windows.Forms.Label lbFov;
        private System.Windows.Forms.TextBox tbLineFov;
        private System.Windows.Forms.TextBox tbLineResolution;
        private System.Windows.Forms.Label lbLineFov;
        private System.Windows.Forms.TextBox tbLineSensor;
        private System.Windows.Forms.Label lbLineResolution;
        private System.Windows.Forms.Label lbLineSensor;
        private System.Windows.Forms.RadioButton rbLineLength;
        private System.Windows.Forms.RadioButton rbLineWidth;
        private System.Windows.Forms.ComboBox cbCamInter;
        private System.Windows.Forms.Label lbCamInter;
        private System.Windows.Forms.Label lbFlange;
        private System.Windows.Forms.TextBox tbFlange;
        private System.Windows.Forms.Label lbSensorDiagonal;
        private System.Windows.Forms.TextBox tbTarget;
        private System.Windows.Forms.Label lbWork;
        private System.Windows.Forms.TextBox tbDistance;
        private System.Windows.Forms.TextBox tbDistanRange;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label lbValueLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn Idx;
        private System.Windows.Forms.DataGridViewTextBoxColumn lens;
        private System.Windows.Forms.DataGridViewTextBoxColumn focus;
        private System.Windows.Forms.DataGridViewTextBoxColumn adapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn extend;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratio;
        private System.Windows.Forms.DataGridViewTextBoxColumn workDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn fovLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn fovWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn detailIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn interA;
        private System.Windows.Forms.DataGridViewTextBoxColumn interB;
        private System.Windows.Forms.DataGridViewTextBoxColumn length;
        private System.Windows.Forms.Label lbValueWidth;
    }
}

