
namespace SJeMES_BDM
{
    partial class F_QCM_Testltem_Edit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Testltem_Edit));
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Standard_measurement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbo_reference_level = new System.Windows.Forms.ComboBox();
            this.txt_testitem_name = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_testitem_code = new System.Windows.Forms.TextBox();
            this.lab_unit = new System.Windows.Forms.Label();
            this.lab_type = new System.Windows.Forms.Label();
            this.lab_currency_formula = new System.Windows.Forms.Label();
            this.lab_reference_level = new System.Windows.Forms.Label();
            this.cbo_type = new System.Windows.Forms.ComboBox();
            this.lab_testitem_name = new System.Windows.Forms.Label();
            this.lab_testitem_code = new System.Windows.Forms.Label();
            this.txt_unit = new System.Windows.Forms.TextBox();
            this.cbo_currency_formula = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lab_sample_num = new System.Windows.Forms.Label();
            this.cbo_custom_formula = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_testtype_no = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lab_custom_formula = new System.Windows.Forms.Label();
            this.txt_sample_num = new System.Windows.Forms.TextBox();
            this.lab_testtype_no = new System.Windows.Forms.Label();
            this.richTextBox_remarks = new System.Windows.Forms.RichTextBox();
            this.lab_remarks = new System.Windows.Forms.Label();
            this.lab_Notrequired = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_keep = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbo_AQL_LEVEL = new System.Windows.Forms.ComboBox();
            this.lab_AQL_LEVEL = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1240, 2);
            this.panel2.TabIndex = 21;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.ID,
            this.Standard_measurement,
            this.unit,
            this.remarks});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1240, 348);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem1.Name = "DELETE";
            dataGridViewOperationItem1.Text = "DELETE";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            // 
            // ID
            // 
            this.ID.HeaderText = "编号";
            this.ID.Name = "ID";
            // 
            // Standard_measurement
            // 
            this.Standard_measurement.HeaderText = "标准测量";
            this.Standard_measurement.Name = "Standard_measurement";
            // 
            // unit
            // 
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            // 
            // remarks
            // 
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            // 
            // cbo_reference_level
            // 
            this.cbo_reference_level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_reference_level.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_reference_level.FormattingEnabled = true;
            this.cbo_reference_level.Location = new System.Drawing.Point(185, 85);
            this.cbo_reference_level.Name = "cbo_reference_level";
            this.cbo_reference_level.Size = new System.Drawing.Size(167, 33);
            this.cbo_reference_level.TabIndex = 36;
            // 
            // txt_testitem_name
            // 
            this.txt_testitem_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_testitem_name.Location = new System.Drawing.Point(943, 41);
            this.txt_testitem_name.Name = "txt_testitem_name";
            this.txt_testitem_name.Size = new System.Drawing.Size(167, 33);
            this.txt_testitem_name.TabIndex = 44;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(355, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 24);
            this.label14.TabIndex = 57;
            this.label14.Text = "*";
            // 
            // txt_testitem_code
            // 
            this.txt_testitem_code.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_testitem_code.Location = new System.Drawing.Point(571, 41);
            this.txt_testitem_code.Name = "txt_testitem_code";
            this.txt_testitem_code.Size = new System.Drawing.Size(167, 33);
            this.txt_testitem_code.TabIndex = 39;
            // 
            // lab_unit
            // 
            this.lab_unit.AutoSize = true;
            this.lab_unit.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_unit.Location = new System.Drawing.Point(499, 89);
            this.lab_unit.Name = "lab_unit";
            this.lab_unit.Size = new System.Drawing.Size(69, 25);
            this.lab_unit.TabIndex = 50;
            this.lab_unit.Text = "单位：";
            // 
            // lab_type
            // 
            this.lab_type.AutoSize = true;
            this.lab_type.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_type.Location = new System.Drawing.Point(872, 140);
            this.lab_type.Name = "lab_type";
            this.lab_type.Size = new System.Drawing.Size(69, 25);
            this.lab_type.TabIndex = 60;
            this.lab_type.Text = "类型：";
            // 
            // lab_currency_formula
            // 
            this.lab_currency_formula.AutoSize = true;
            this.lab_currency_formula.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_currency_formula.Location = new System.Drawing.Point(36, 134);
            this.lab_currency_formula.Name = "lab_currency_formula";
            this.lab_currency_formula.Size = new System.Drawing.Size(145, 25);
            this.lab_currency_formula.TabIndex = 42;
            this.lab_currency_formula.Text = "选择通用公式：";
            // 
            // lab_reference_level
            // 
            this.lab_reference_level.AutoSize = true;
            this.lab_reference_level.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_reference_level.Location = new System.Drawing.Point(36, 89);
            this.lab_reference_level.Name = "lab_reference_level";
            this.lab_reference_level.Size = new System.Drawing.Size(145, 25);
            this.lab_reference_level.TabIndex = 40;
            this.lab_reference_level.Text = "结果引用级别：";
            // 
            // cbo_type
            // 
            this.cbo_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_type.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_type.FormattingEnabled = true;
            this.cbo_type.Location = new System.Drawing.Point(943, 136);
            this.cbo_type.Name = "cbo_type";
            this.cbo_type.Size = new System.Drawing.Size(167, 33);
            this.cbo_type.TabIndex = 59;
            // 
            // lab_testitem_name
            // 
            this.lab_testitem_name.AutoSize = true;
            this.lab_testitem_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_testitem_name.Location = new System.Drawing.Point(818, 46);
            this.lab_testitem_name.Name = "lab_testitem_name";
            this.lab_testitem_name.Size = new System.Drawing.Size(126, 25);
            this.lab_testitem_name.TabIndex = 51;
            this.lab_testitem_name.Text = "检测项名称：";
            // 
            // lab_testitem_code
            // 
            this.lab_testitem_code.AutoSize = true;
            this.lab_testitem_code.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_testitem_code.Location = new System.Drawing.Point(444, 46);
            this.lab_testitem_code.Name = "lab_testitem_code";
            this.lab_testitem_code.Size = new System.Drawing.Size(126, 25);
            this.lab_testitem_code.TabIndex = 49;
            this.lab_testitem_code.Text = "检测项编号：";
            // 
            // txt_unit
            // 
            this.txt_unit.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_unit.Location = new System.Drawing.Point(571, 84);
            this.txt_unit.Name = "txt_unit";
            this.txt_unit.Size = new System.Drawing.Size(167, 33);
            this.txt_unit.TabIndex = 41;
            // 
            // cbo_currency_formula
            // 
            this.cbo_currency_formula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_currency_formula.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_currency_formula.FormattingEnabled = true;
            this.cbo_currency_formula.Location = new System.Drawing.Point(185, 130);
            this.cbo_currency_formula.Name = "cbo_currency_formula";
            this.cbo_currency_formula.Size = new System.Drawing.Size(167, 33);
            this.cbo_currency_formula.TabIndex = 38;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(354, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 24);
            this.label13.TabIndex = 56;
            this.label13.Text = "*";
            // 
            // lab_sample_num
            // 
            this.lab_sample_num.AutoSize = true;
            this.lab_sample_num.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_sample_num.Location = new System.Drawing.Point(834, 95);
            this.lab_sample_num.Name = "lab_sample_num";
            this.lab_sample_num.Size = new System.Drawing.Size(107, 25);
            this.lab_sample_num.TabIndex = 52;
            this.lab_sample_num.Text = "试样数量：";
            // 
            // cbo_custom_formula
            // 
            this.cbo_custom_formula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_custom_formula.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_custom_formula.FormattingEnabled = true;
            this.cbo_custom_formula.Location = new System.Drawing.Point(571, 130);
            this.cbo_custom_formula.Name = "cbo_custom_formula";
            this.cbo_custom_formula.Size = new System.Drawing.Size(167, 33);
            this.cbo_custom_formula.TabIndex = 43;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(741, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 24);
            this.label12.TabIndex = 55;
            this.label12.Text = "*";
            // 
            // txt_testtype_no
            // 
            this.txt_testtype_no.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_testtype_no.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_testtype_no.Location = new System.Drawing.Point(185, 40);
            this.txt_testtype_no.Name = "txt_testtype_no";
            this.txt_testtype_no.ReadOnly = true;
            this.txt_testtype_no.Size = new System.Drawing.Size(167, 33);
            this.txt_testtype_no.TabIndex = 62;
            this.txt_testtype_no.Click += new System.EventHandler(this.txt_testtype_no_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(741, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 24);
            this.label10.TabIndex = 53;
            this.label10.Text = "*";
            // 
            // lab_custom_formula
            // 
            this.lab_custom_formula.AutoSize = true;
            this.lab_custom_formula.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_custom_formula.Location = new System.Drawing.Point(404, 134);
            this.lab_custom_formula.Name = "lab_custom_formula";
            this.lab_custom_formula.Size = new System.Drawing.Size(164, 25);
            this.lab_custom_formula.TabIndex = 46;
            this.lab_custom_formula.Text = "选择自定义公式：";
            // 
            // txt_sample_num
            // 
            this.txt_sample_num.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_sample_num.Location = new System.Drawing.Point(943, 89);
            this.txt_sample_num.Name = "txt_sample_num";
            this.txt_sample_num.Size = new System.Drawing.Size(167, 33);
            this.txt_sample_num.TabIndex = 63;
            this.txt_sample_num.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_sample_num_KeyPress);
            // 
            // lab_testtype_no
            // 
            this.lab_testtype_no.AutoSize = true;
            this.lab_testtype_no.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_testtype_no.Location = new System.Drawing.Point(36, 45);
            this.lab_testtype_no.Name = "lab_testtype_no";
            this.lab_testtype_no.Size = new System.Drawing.Size(145, 25);
            this.lab_testtype_no.TabIndex = 37;
            this.lab_testtype_no.Text = "检测项目类型：";
            // 
            // richTextBox_remarks
            // 
            this.richTextBox_remarks.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_remarks.Location = new System.Drawing.Point(571, 177);
            this.richTextBox_remarks.Name = "richTextBox_remarks";
            this.richTextBox_remarks.Size = new System.Drawing.Size(539, 59);
            this.richTextBox_remarks.TabIndex = 64;
            this.richTextBox_remarks.Text = "";
            // 
            // lab_remarks
            // 
            this.lab_remarks.AutoSize = true;
            this.lab_remarks.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_remarks.Location = new System.Drawing.Point(499, 180);
            this.lab_remarks.Name = "lab_remarks";
            this.lab_remarks.Size = new System.Drawing.Size(69, 25);
            this.lab_remarks.TabIndex = 65;
            this.lab_remarks.Text = "备注：";
            // 
            // lab_Notrequired
            // 
            this.lab_Notrequired.AutoSize = true;
            this.lab_Notrequired.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Notrequired.Location = new System.Drawing.Point(476, 211);
            this.lab_Notrequired.Name = "lab_Notrequired";
            this.lab_Notrequired.Size = new System.Drawing.Size(81, 25);
            this.lab_Notrequired.TabIndex = 66;
            this.lab_Notrequired.Text = "(非必填)";
            // 
            // btn_add
            // 
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(16, 250);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(118, 36);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "新增明细";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_keep
            // 
            this.btn_keep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_keep.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_keep.Location = new System.Drawing.Point(140, 4);
            this.btn_keep.Name = "btn_keep";
            this.btn_keep.Size = new System.Drawing.Size(94, 33);
            this.btn_keep.TabIndex = 48;
            this.btn_keep.Text = "保存";
            this.btn_keep.UseVisualStyleBackColor = true;
            this.btn_keep.Click += new System.EventHandler(this.btn_keep_Click);
            // 
            // btn_close
            // 
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(40, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(94, 33);
            this.btn_close.TabIndex = 49;
            this.btn_close.Text = "返回";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbo_AQL_LEVEL);
            this.splitContainer1.Panel1.Controls.Add(this.btn_add);
            this.splitContainer1.Panel1.Controls.Add(this.btn_close);
            this.splitContainer1.Panel1.Controls.Add(this.btn_keep);
            this.splitContainer1.Panel1.Controls.Add(this.richTextBox_remarks);
            this.splitContainer1.Panel1.Controls.Add(this.cbo_reference_level);
            this.splitContainer1.Panel1.Controls.Add(this.txt_sample_num);
            this.splitContainer1.Panel1.Controls.Add(this.txt_testitem_name);
            this.splitContainer1.Panel1.Controls.Add(this.label14);
            this.splitContainer1.Panel1.Controls.Add(this.txt_testitem_code);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.txt_testtype_no);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.cbo_custom_formula);
            this.splitContainer1.Panel1.Controls.Add(this.cbo_type);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.cbo_currency_formula);
            this.splitContainer1.Panel1.Controls.Add(this.txt_unit);
            this.splitContainer1.Panel1.Controls.Add(this.lab_AQL_LEVEL);
            this.splitContainer1.Panel1.Controls.Add(this.lab_testtype_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_custom_formula);
            this.splitContainer1.Panel1.Controls.Add(this.lab_unit);
            this.splitContainer1.Panel1.Controls.Add(this.lab_type);
            this.splitContainer1.Panel1.Controls.Add(this.lab_currency_formula);
            this.splitContainer1.Panel1.Controls.Add(this.lab_reference_level);
            this.splitContainer1.Panel1.Controls.Add(this.lab_sample_num);
            this.splitContainer1.Panel1.Controls.Add(this.lab_testitem_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_testitem_code);
            this.splitContainer1.Panel1.Controls.Add(this.lab_Notrequired);
            this.splitContainer1.Panel1.Controls.Add(this.lab_remarks);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1240, 700);
            this.splitContainer1.SplitterDistance = 290;
            this.splitContainer1.TabIndex = 65;
            // 
            // cbo_AQL_LEVEL
            // 
            this.cbo_AQL_LEVEL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_AQL_LEVEL.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_AQL_LEVEL.FormattingEnabled = true;
            this.cbo_AQL_LEVEL.Location = new System.Drawing.Point(185, 184);
            this.cbo_AQL_LEVEL.Name = "cbo_AQL_LEVEL";
            this.cbo_AQL_LEVEL.Size = new System.Drawing.Size(167, 33);
            this.cbo_AQL_LEVEL.TabIndex = 72;
            // 
            // lab_AQL_LEVEL
            // 
            this.lab_AQL_LEVEL.AutoSize = true;
            this.lab_AQL_LEVEL.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_AQL_LEVEL.Location = new System.Drawing.Point(82, 187);
            this.lab_AQL_LEVEL.Name = "lab_AQL_LEVEL";
            this.lab_AQL_LEVEL.Size = new System.Drawing.Size(107, 25);
            this.lab_AQL_LEVEL.TabIndex = 71;
            this.lab_AQL_LEVEL.Text = "AQL级别：";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel2_Paint);
            this.splitContainer2.Size = new System.Drawing.Size(1240, 406);
            this.splitContainer2.SplitterDistance = 348;
            this.splitContainer2.TabIndex = 0;
            // 
            // F_QCM_Testltem_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 766);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_QCM_Testltem_Edit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增测试项";
            this.Load += new System.EventHandler(this.F_QCM_Testltem_Edit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label lab_Notrequired;
        private System.Windows.Forms.Label lab_remarks;
        private System.Windows.Forms.RichTextBox richTextBox_remarks;
        private System.Windows.Forms.Label lab_testtype_no;
        private System.Windows.Forms.TextBox txt_sample_num;
        private System.Windows.Forms.Label lab_custom_formula;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_testtype_no;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbo_custom_formula;
        private System.Windows.Forms.Label lab_sample_num;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbo_currency_formula;
        private System.Windows.Forms.TextBox txt_unit;
        private System.Windows.Forms.Label lab_testitem_code;
        private System.Windows.Forms.Label lab_testitem_name;
        private System.Windows.Forms.ComboBox cbo_type;
        private System.Windows.Forms.Label lab_reference_level;
        private System.Windows.Forms.Label lab_currency_formula;
        private System.Windows.Forms.Label lab_type;
        private System.Windows.Forms.Label lab_unit;
        private System.Windows.Forms.TextBox txt_testitem_code;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_testitem_name;
        private System.Windows.Forms.ComboBox cbo_reference_level;
        private System.Windows.Forms.Button btn_keep;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lab_AQL_LEVEL;
        private System.Windows.Forms.ComboBox cbo_AQL_LEVEL;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Standard_measurement;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
    }
}