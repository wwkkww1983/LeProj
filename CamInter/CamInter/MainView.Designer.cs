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
            this.dgvProjList = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CamLensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AdapterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExtendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.语言ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.中文ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enlishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvProjDetail = new System.Windows.Forms.DataGridView();
            this.tcCamera = new System.Windows.Forms.TabControl();
            this.tpArea = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbFovSide = new System.Windows.Forms.TextBox();
            this.tbFovOther = new System.Windows.Forms.TextBox();
            this.tbResolutionSide = new System.Windows.Forms.TextBox();
            this.tbResolutionOther = new System.Windows.Forms.TextBox();
            this.lbFov = new System.Windows.Forms.Label();
            this.tbSensorSide = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbSensorOther = new System.Windows.Forms.TextBox();
            this.lbSensor = new System.Windows.Forms.Label();
            this.tpLine = new System.Windows.Forms.TabPage();
            this.rbLineWidth = new System.Windows.Forms.RadioButton();
            this.rbLineLength = new System.Windows.Forms.RadioButton();
            this.tbLineFov = new System.Windows.Forms.TextBox();
            this.tbLineResolution = new System.Windows.Forms.TextBox();
            this.lbLineFov = new System.Windows.Forms.Label();
            this.tbLineSensor = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lbLineSensor = new System.Windows.Forms.Label();
            this.cbCamInter = new System.Windows.Forms.ComboBox();
            this.lbCamInter = new System.Windows.Forms.Label();
            this.lbFlange = new System.Windows.Forms.Label();
            this.tbFlange = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbTarget = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tbDistance = new System.Windows.Forms.TextBox();
            this.tbDistanRange = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.Idx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.camera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.focus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adapter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fovLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fovWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detailIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dgvProjList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Idx,
            this.camera,
            this.focus,
            this.adapter,
            this.extend,
            this.ratio,
            this.workDistance,
            this.fovLength,
            this.fovWidth});
            this.dgvProjList.Location = new System.Drawing.Point(335, 53);
            this.dgvProjList.Name = "dgvProjList";
            this.dgvProjList.RowTemplate.Height = 27;
            this.dgvProjList.Size = new System.Drawing.Size(601, 256);
            this.dgvProjList.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.录入ToolStripMenuItem,
            this.语言ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(946, 28);
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
            // 语言ToolStripMenuItem
            // 
            this.语言ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.中文ToolStripMenuItem,
            this.enlishToolStripMenuItem});
            this.语言ToolStripMenuItem.Name = "语言ToolStripMenuItem";
            this.语言ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.语言ToolStripMenuItem.Text = "语言";
            // 
            // 中文ToolStripMenuItem
            // 
            this.中文ToolStripMenuItem.Name = "中文ToolStripMenuItem";
            this.中文ToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.中文ToolStripMenuItem.Text = "中文";
            // 
            // enlishToolStripMenuItem
            // 
            this.enlishToolStripMenuItem.Name = "enlishToolStripMenuItem";
            this.enlishToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.enlishToolStripMenuItem.Text = "Enlish";
            // 
            // dgvProjDetail
            // 
            this.dgvProjDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detailIdx,
            this.name,
            this.number});
            this.dgvProjDetail.Location = new System.Drawing.Point(335, 315);
            this.dgvProjDetail.Name = "dgvProjDetail";
            this.dgvProjDetail.RowTemplate.Height = 27;
            this.dgvProjDetail.Size = new System.Drawing.Size(601, 217);
            this.dgvProjDetail.TabIndex = 1;
            // 
            // tcCamera
            // 
            this.tcCamera.Controls.Add(this.tpArea);
            this.tcCamera.Controls.Add(this.tpLine);
            this.tcCamera.Location = new System.Drawing.Point(12, 46);
            this.tcCamera.Name = "tcCamera";
            this.tcCamera.SelectedIndex = 0;
            this.tcCamera.Size = new System.Drawing.Size(317, 252);
            this.tcCamera.TabIndex = 3;
            // 
            // tpArea
            // 
            this.tpArea.Controls.Add(this.label16);
            this.tpArea.Controls.Add(this.label14);
            this.tpArea.Controls.Add(this.label1);
            this.tpArea.Controls.Add(this.label11);
            this.tpArea.Controls.Add(this.tbFovSide);
            this.tpArea.Controls.Add(this.tbFovOther);
            this.tpArea.Controls.Add(this.tbResolutionSide);
            this.tpArea.Controls.Add(this.tbResolutionOther);
            this.tpArea.Controls.Add(this.lbFov);
            this.tpArea.Controls.Add(this.tbSensorSide);
            this.tpArea.Controls.Add(this.label13);
            this.tpArea.Controls.Add(this.tbSensorOther);
            this.tpArea.Controls.Add(this.lbSensor);
            this.tpArea.Location = new System.Drawing.Point(4, 25);
            this.tpArea.Name = "tpArea";
            this.tpArea.Padding = new System.Windows.Forms.Padding(3);
            this.tpArea.Size = new System.Drawing.Size(309, 223);
            this.tpArea.TabIndex = 0;
            this.tpArea.Text = "面阵";
            this.tpArea.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(209, 110);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 15);
            this.label16.TabIndex = 19;
            this.label16.Text = "×";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(209, 165);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 15);
            this.label14.TabIndex = 19;
            this.label14.Text = "×";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "长    ×    宽";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(209, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 15);
            this.label11.TabIndex = 19;
            this.label11.Text = "×";
            // 
            // tbFovSide
            // 
            this.tbFovSide.Location = new System.Drawing.Point(147, 105);
            this.tbFovSide.Name = "tbFovSide";
            this.tbFovSide.Size = new System.Drawing.Size(60, 25);
            this.tbFovSide.TabIndex = 5;
            // 
            // tbFovOther
            // 
            this.tbFovOther.Location = new System.Drawing.Point(236, 105);
            this.tbFovOther.Name = "tbFovOther";
            this.tbFovOther.Size = new System.Drawing.Size(60, 25);
            this.tbFovOther.TabIndex = 6;
            // 
            // tbResolutionSide
            // 
            this.tbResolutionSide.Location = new System.Drawing.Point(147, 160);
            this.tbResolutionSide.Name = "tbResolutionSide";
            this.tbResolutionSide.Size = new System.Drawing.Size(60, 25);
            this.tbResolutionSide.TabIndex = 3;
            // 
            // tbResolutionOther
            // 
            this.tbResolutionOther.Location = new System.Drawing.Point(236, 160);
            this.tbResolutionOther.Name = "tbResolutionOther";
            this.tbResolutionOther.Size = new System.Drawing.Size(60, 25);
            this.tbResolutionOther.TabIndex = 4;
            // 
            // lbFov
            // 
            this.lbFov.AutoSize = true;
            this.lbFov.Location = new System.Drawing.Point(31, 110);
            this.lbFov.Name = "lbFov";
            this.lbFov.Size = new System.Drawing.Size(109, 15);
            this.lbFov.TabIndex = 16;
            this.lbFov.Text = "视野Fov(mm) *";
            // 
            // tbSensorSide
            // 
            this.tbSensorSide.Location = new System.Drawing.Point(147, 50);
            this.tbSensorSide.Name = "tbSensorSide";
            this.tbSensorSide.Size = new System.Drawing.Size(60, 25);
            this.tbSensorSide.TabIndex = 1;
            this.tbSensorSide.TextChanged += new System.EventHandler(this.tbSensor_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 165);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 15);
            this.label13.TabIndex = 16;
            this.label13.Text = "相机分辨率(mm)";
            // 
            // tbSensorOther
            // 
            this.tbSensorOther.Location = new System.Drawing.Point(236, 50);
            this.tbSensorOther.Name = "tbSensorOther";
            this.tbSensorOther.Size = new System.Drawing.Size(60, 25);
            this.tbSensorOther.TabIndex = 2;
            this.tbSensorOther.TextChanged += new System.EventHandler(this.tbSensor_TextChanged);
            // 
            // lbSensor
            // 
            this.lbSensor.AutoSize = true;
            this.lbSensor.Location = new System.Drawing.Point(7, 55);
            this.lbSensor.Name = "lbSensor";
            this.lbSensor.Size = new System.Drawing.Size(133, 15);
            this.lbSensor.TabIndex = 16;
            this.lbSensor.Text = "Sensor尺寸(mm) *";
            // 
            // tpLine
            // 
            this.tpLine.Controls.Add(this.rbLineWidth);
            this.tpLine.Controls.Add(this.rbLineLength);
            this.tpLine.Controls.Add(this.tbLineFov);
            this.tpLine.Controls.Add(this.tbLineResolution);
            this.tpLine.Controls.Add(this.lbLineFov);
            this.tpLine.Controls.Add(this.tbLineSensor);
            this.tpLine.Controls.Add(this.label18);
            this.tpLine.Controls.Add(this.lbLineSensor);
            this.tpLine.Location = new System.Drawing.Point(4, 25);
            this.tpLine.Name = "tpLine";
            this.tpLine.Padding = new System.Windows.Forms.Padding(3);
            this.tpLine.Size = new System.Drawing.Size(309, 223);
            this.tpLine.TabIndex = 1;
            this.tpLine.Text = "线阵";
            this.tpLine.UseVisualStyleBackColor = true;
            // 
            // rbLineWidth
            // 
            this.rbLineWidth.AutoSize = true;
            this.rbLineWidth.Location = new System.Drawing.Point(185, 33);
            this.rbLineWidth.Name = "rbLineWidth";
            this.rbLineWidth.Size = new System.Drawing.Size(43, 19);
            this.rbLineWidth.TabIndex = 2;
            this.rbLineWidth.Text = "宽";
            this.rbLineWidth.UseVisualStyleBackColor = true;
            // 
            // rbLineLength
            // 
            this.rbLineLength.AutoSize = true;
            this.rbLineLength.Checked = true;
            this.rbLineLength.Location = new System.Drawing.Point(85, 33);
            this.rbLineLength.Name = "rbLineLength";
            this.rbLineLength.Size = new System.Drawing.Size(43, 19);
            this.rbLineLength.TabIndex = 1;
            this.rbLineLength.TabStop = true;
            this.rbLineLength.Text = "长";
            this.rbLineLength.UseVisualStyleBackColor = true;
            // 
            // tbLineFov
            // 
            this.tbLineFov.Location = new System.Drawing.Point(147, 119);
            this.tbLineFov.Name = "tbLineFov";
            this.tbLineFov.Size = new System.Drawing.Size(149, 25);
            this.tbLineFov.TabIndex = 5;
            this.tbLineFov.Text = "60";
            // 
            // tbLineResolution
            // 
            this.tbLineResolution.Location = new System.Drawing.Point(147, 160);
            this.tbLineResolution.Name = "tbLineResolution";
            this.tbLineResolution.Size = new System.Drawing.Size(149, 25);
            this.tbLineResolution.TabIndex = 4;
            // 
            // lbLineFov
            // 
            this.lbLineFov.AutoSize = true;
            this.lbLineFov.Location = new System.Drawing.Point(31, 124);
            this.lbLineFov.Name = "lbLineFov";
            this.lbLineFov.Size = new System.Drawing.Size(109, 15);
            this.lbLineFov.TabIndex = 18;
            this.lbLineFov.Text = "视野Fov(mm) *";
            // 
            // tbLineSensor
            // 
            this.tbLineSensor.Location = new System.Drawing.Point(147, 79);
            this.tbLineSensor.Name = "tbLineSensor";
            this.tbLineSensor.Size = new System.Drawing.Size(149, 25);
            this.tbLineSensor.TabIndex = 3;
            this.tbLineSensor.Text = "28";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(26, 165);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(114, 15);
            this.label18.TabIndex = 19;
            this.label18.Text = "相机分辨率(mm)";
            // 
            // lbLineSensor
            // 
            this.lbLineSensor.AutoSize = true;
            this.lbLineSensor.Location = new System.Drawing.Point(7, 84);
            this.lbLineSensor.Name = "lbLineSensor";
            this.lbLineSensor.Size = new System.Drawing.Size(133, 15);
            this.lbLineSensor.TabIndex = 20;
            this.lbLineSensor.Text = "Sensor尺寸(mm) *";
            // 
            // cbCamInter
            // 
            this.cbCamInter.FormattingEnabled = true;
            this.cbCamInter.Location = new System.Drawing.Point(178, 318);
            this.cbCamInter.Name = "cbCamInter";
            this.cbCamInter.Size = new System.Drawing.Size(147, 23);
            this.cbCamInter.TabIndex = 10;
            // 
            // lbCamInter
            // 
            this.lbCamInter.AutoSize = true;
            this.lbCamInter.Location = new System.Drawing.Point(59, 322);
            this.lbCamInter.Name = "lbCamInter";
            this.lbCamInter.Size = new System.Drawing.Size(113, 15);
            this.lbCamInter.TabIndex = 1;
            this.lbCamInter.Text = "相机法兰接口 *";
            // 
            // lbFlange
            // 
            this.lbFlange.AutoSize = true;
            this.lbFlange.Location = new System.Drawing.Point(39, 368);
            this.lbFlange.Name = "lbFlange";
            this.lbFlange.Size = new System.Drawing.Size(133, 15);
            this.lbFlange.TabIndex = 6;
            this.lbFlange.Text = "Sensor深度(mm) *";
            // 
            // tbFlange
            // 
            this.tbFlange.Location = new System.Drawing.Point(178, 363);
            this.tbFlange.Name = "tbFlange";
            this.tbFlange.Size = new System.Drawing.Size(147, 25);
            this.tbFlange.TabIndex = 13;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(10, 415);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(162, 15);
            this.label22.TabIndex = 6;
            this.label22.Text = "Sensor对角线长度(mm)";
            // 
            // tbTarget
            // 
            this.tbTarget.Location = new System.Drawing.Point(178, 410);
            this.tbTarget.Name = "tbTarget";
            this.tbTarget.Size = new System.Drawing.Size(147, 25);
            this.tbTarget.TabIndex = 15;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(73, 462);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(99, 15);
            this.label23.TabIndex = 5;
            this.label23.Text = "工作距离(mm)";
            // 
            // tbDistance
            // 
            this.tbDistance.Location = new System.Drawing.Point(178, 457);
            this.tbDistance.Name = "tbDistance";
            this.tbDistance.Size = new System.Drawing.Size(60, 25);
            this.tbDistance.TabIndex = 17;
            // 
            // tbDistanRange
            // 
            this.tbDistanRange.Location = new System.Drawing.Point(264, 457);
            this.tbDistanRange.Name = "tbDistanRange";
            this.tbDistanRange.Size = new System.Drawing.Size(60, 25);
            this.tbDistanRange.TabIndex = 18;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(240, 462);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(22, 15);
            this.label24.TabIndex = 15;
            this.label24.Text = "±";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(12, 498);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(313, 34);
            this.btnSelect.TabIndex = 20;
            this.btnSelect.Text = "查询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // Idx
            // 
            this.Idx.Frozen = true;
            this.Idx.HeaderText = "序号";
            this.Idx.Name = "Idx";
            this.Idx.ReadOnly = true;
            this.Idx.Width = 60;
            // 
            // camera
            // 
            this.camera.HeaderText = "镜头";
            this.camera.Name = "camera";
            this.camera.ReadOnly = true;
            // 
            // focus
            // 
            this.focus.HeaderText = "调焦环";
            this.focus.Name = "focus";
            this.focus.ReadOnly = true;
            // 
            // adapter
            // 
            this.adapter.HeaderText = "转接环数";
            this.adapter.Name = "adapter";
            this.adapter.ReadOnly = true;
            // 
            // extend
            // 
            this.extend.HeaderText = "延长环数";
            this.extend.Name = "extend";
            this.extend.ReadOnly = true;
            // 
            // ratio
            // 
            this.ratio.HeaderText = "放大倍率";
            this.ratio.Name = "ratio";
            this.ratio.ReadOnly = true;
            // 
            // workDistance
            // 
            this.workDistance.HeaderText = "工作距离";
            this.workDistance.Name = "workDistance";
            this.workDistance.ReadOnly = true;
            // 
            // fovLength
            // 
            this.fovLength.HeaderText = "视野长";
            this.fovLength.Name = "fovLength";
            this.fovLength.ReadOnly = true;
            // 
            // fovWidth
            // 
            this.fovWidth.HeaderText = "视野宽";
            this.fovWidth.Name = "fovWidth";
            this.fovWidth.ReadOnly = true;
            // 
            // detailIdx
            // 
            this.detailIdx.Frozen = true;
            this.detailIdx.HeaderText = "序号";
            this.detailIdx.Name = "detailIdx";
            this.detailIdx.ReadOnly = true;
            // 
            // name
            // 
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 150;
            // 
            // number
            // 
            this.number.HeaderText = "货号";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 200;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 539);
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
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.lbCamInter);
            this.Controls.Add(this.lbFlange);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainView";
            this.Text = "相机镜头配对";
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
        private System.Windows.Forms.ToolStripMenuItem 录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CamLensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FocusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AdapterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExtendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvProjDetail;
        private System.Windows.Forms.ToolStripMenuItem 语言ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 中文ToolStripMenuItem;
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
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbFovSide;
        private System.Windows.Forms.TextBox tbFovOther;
        private System.Windows.Forms.Label lbFov;
        private System.Windows.Forms.TextBox tbLineFov;
        private System.Windows.Forms.TextBox tbLineResolution;
        private System.Windows.Forms.Label lbLineFov;
        private System.Windows.Forms.TextBox tbLineSensor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lbLineSensor;
        private System.Windows.Forms.RadioButton rbLineLength;
        private System.Windows.Forms.RadioButton rbLineWidth;
        private System.Windows.Forms.ComboBox cbCamInter;
        private System.Windows.Forms.Label lbCamInter;
        private System.Windows.Forms.Label lbFlange;
        private System.Windows.Forms.TextBox tbFlange;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbTarget;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbDistance;
        private System.Windows.Forms.TextBox tbDistanRange;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Idx;
        private System.Windows.Forms.DataGridViewTextBoxColumn camera;
        private System.Windows.Forms.DataGridViewTextBoxColumn focus;
        private System.Windows.Forms.DataGridViewTextBoxColumn adapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn extend;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratio;
        private System.Windows.Forms.DataGridViewTextBoxColumn workDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn fovLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn fovWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn detailIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
    }
}

