
namespace SJeMES_BDM
{
    partial class F_BDM_QualityStandard_Main
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_BDM_QualityStandard_Main));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem5 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem6 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_add = new System.Windows.Forms.Button();
            this.tab__type_standard = new System.Windows.Forms.TabControl();
            this.lab_type_standard = new System.Windows.Forms.Label();
            this.lab_current_class = new System.Windows.Forms.Label();
            this.lab_null = new System.Windows.Forms.Label();
            this.btn_import_data = new System.Windows.Forms.Button();
            this.lab_systematic_name = new System.Windows.Forms.Label();
            this.btn_Download_template = new System.Windows.Forms.Button();
            this.txt_systematic_name = new System.Windows.Forms.TextBox();
            this.btn_select = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.general_testtype_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quality_category_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quality_category_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(-7, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_add);
            this.splitContainer1.Panel1.Controls.Add(this.tab__type_standard);
            this.splitContainer1.Panel1.Controls.Add(this.lab_null);
            this.splitContainer1.Panel1.Controls.Add(this.btn_import_data);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Download_template);
            this.splitContainer1.Panel1.Controls.Add(this.txt_systematic_name);
            this.splitContainer1.Panel1.Controls.Add(this.btn_select);
            this.splitContainer1.Panel1.Controls.Add(this.lab_type_standard);
            this.splitContainer1.Panel1.Controls.Add(this.lab_current_class);
            this.splitContainer1.Panel1.Controls.Add(this.lab_systematic_name);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1324, 618);
            this.splitContainer1.SplitterDistance = 147;
            this.splitContainer1.TabIndex = 13;
            // 
            // btn_add
            // 
            this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_add.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_add.Location = new System.Drawing.Point(382, 99);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(96, 37);
            this.btn_add.TabIndex = 20;
            this.btn_add.Text = "新建分类";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn5_Click);
            // 
            // tab__type_standard
            // 
            this.tab__type_standard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tab__type_standard.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tab__type_standard.Location = new System.Drawing.Point(148, 20);
            this.tab__type_standard.Name = "tab__type_standard";
            this.tab__type_standard.SelectedIndex = 0;
            this.tab__type_standard.Size = new System.Drawing.Size(826, 25);
            this.tab__type_standard.TabIndex = 21;
            this.tab__type_standard.SelectedIndexChanged += new System.EventHandler(this.tab1_SelectedIndexChanged);
            // 
            // lab_type_standard
            // 
            this.lab_type_standard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lab_type_standard.AutoSize = true;
            this.lab_type_standard.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_type_standard.Location = new System.Drawing.Point(37, 21);
            this.lab_type_standard.Name = "lab_type_standard";
            this.lab_type_standard.Size = new System.Drawing.Size(110, 21);
            this.lab_type_standard.TabIndex = 12;
            this.lab_type_standard.Text = "通用类型标准:";
            // 
            // lab_current_class
            // 
            this.lab_current_class.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lab_current_class.AutoSize = true;
            this.lab_current_class.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_current_class.Location = new System.Drawing.Point(37, 61);
            this.lab_current_class.Name = "lab_current_class";
            this.lab_current_class.Size = new System.Drawing.Size(78, 21);
            this.lab_current_class.TabIndex = 13;
            this.lab_current_class.Text = "当前分类:";
            // 
            // lab_null
            // 
            this.lab_null.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lab_null.AutoSize = true;
            this.lab_null.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_null.Location = new System.Drawing.Point(124, 61);
            this.lab_null.Name = "lab_null";
            this.lab_null.Size = new System.Drawing.Size(26, 21);
            this.lab_null.TabIndex = 14;
            this.lab_null.Text = "空";
            // 
            // btn_import_data
            // 
            this.btn_import_data.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_import_data.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_import_data.Location = new System.Drawing.Point(749, 95);
            this.btn_import_data.Name = "btn_import_data";
            this.btn_import_data.Size = new System.Drawing.Size(96, 37);
            this.btn_import_data.TabIndex = 19;
            this.btn_import_data.Text = "导入数据";
            this.btn_import_data.UseVisualStyleBackColor = true;
            this.btn_import_data.Visible = false;
            // 
            // lab_systematic_name
            // 
            this.lab_systematic_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lab_systematic_name.AutoSize = true;
            this.lab_systematic_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_systematic_name.Location = new System.Drawing.Point(37, 103);
            this.lab_systematic_name.Name = "lab_systematic_name";
            this.lab_systematic_name.Size = new System.Drawing.Size(78, 21);
            this.lab_systematic_name.TabIndex = 15;
            this.lab_systematic_name.Text = "分类名称:";
            // 
            // btn_Download_template
            // 
            this.btn_Download_template.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Download_template.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Download_template.Location = new System.Drawing.Point(643, 95);
            this.btn_Download_template.Name = "btn_Download_template";
            this.btn_Download_template.Size = new System.Drawing.Size(96, 37);
            this.btn_Download_template.TabIndex = 18;
            this.btn_Download_template.Text = "下载模板";
            this.btn_Download_template.UseVisualStyleBackColor = true;
            this.btn_Download_template.Visible = false;
            // 
            // txt_systematic_name
            // 
            this.txt_systematic_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_systematic_name.Location = new System.Drawing.Point(117, 104);
            this.txt_systematic_name.Name = "txt_systematic_name";
            this.txt_systematic_name.Size = new System.Drawing.Size(116, 29);
            this.txt_systematic_name.TabIndex = 16;
            // 
            // btn_select
            // 
            this.btn_select.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_select.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_select.Location = new System.Drawing.Point(245, 99);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(96, 37);
            this.btn_select.TabIndex = 17;
            this.btn_select.Text = "搜索";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn2_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
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
            this.splitContainer2.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer2.Size = new System.Drawing.Size(1324, 467);
            this.splitContainer2.SplitterDistance = 401;
            this.splitContainer2.TabIndex = 12;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.id,
            this.general_testtype_no,
            this.quality_category_no,
            this.quality_category_name,
            this.remarks});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1324, 401);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem1.Name = "UPDATE";
            dataGridViewOperationItem1.Text = "修改";
            dataGridViewOperationItem2.Image = global::SJeMES_BDM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem2.Name = "DETAIL";
            dataGridViewOperationItem2.Text = "编辑";
            dataGridViewOperationItem3.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem3.Name = "DELETE";
            dataGridViewOperationItem3.Text = "删除";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.Items.Add(dataGridViewOperationItem3);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 49;
            // 
            // general_testtype_no
            // 
            this.general_testtype_no.DataPropertyName = "general_testtype_no";
            this.general_testtype_no.HeaderText = "通用类型代号";
            this.general_testtype_no.Name = "general_testtype_no";
            this.general_testtype_no.ReadOnly = true;
            this.general_testtype_no.Visible = false;
            this.general_testtype_no.Width = 131;
            // 
            // quality_category_no
            // 
            this.quality_category_no.DataPropertyName = "quality_category_no";
            this.quality_category_no.HeaderText = "分类代号";
            this.quality_category_no.Name = "quality_category_no";
            this.quality_category_no.ReadOnly = true;
            this.quality_category_no.Width = 99;
            // 
            // quality_category_name
            // 
            this.quality_category_name.DataPropertyName = "quality_category_name";
            this.quality_category_name.HeaderText = "分类名称";
            this.quality_category_name.Name = "quality_category_name";
            this.quality_category_name.ReadOnly = true;
            this.quality_category_name.Width = 99;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Width = 67;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(344, 2);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(977, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem4.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem4.Name = "Update";
            dataGridViewOperationItem4.Text = "修改";
            dataGridViewOperationItem5.Image = global::SJeMES_BDM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem5.Name = "Detail";
            dataGridViewOperationItem5.Text = "编辑";
            dataGridViewOperationItem6.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem6.Name = "Delete";
            dataGridViewOperationItem6.Text = "删除";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem5);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem6);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "修改";
            this.dataGridViewButtonColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewButtonColumn1.HeaderText = "修改";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Text = "修改";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            // 
            // dataGridViewButtonColumn2
            // 
            this.dataGridViewButtonColumn2.HeaderText = "编辑";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.Text = "编辑";
            this.dataGridViewButtonColumn2.UseColumnTextForButtonValue = true;
            // 
            // dataGridViewButtonColumn3
            // 
            this.dataGridViewButtonColumn3.HeaderText = "删除";
            this.dataGridViewButtonColumn3.Name = "dataGridViewButtonColumn3";
            this.dataGridViewButtonColumn3.Text = "删除";
            this.dataGridViewButtonColumn3.UseColumnTextForButtonValue = true;
            // 
            // F_BDM_QualityStandard_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1317, 683);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_BDM_QualityStandard_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "通用品质标准";
            this.Load += new System.EventHandler(this.F_BDM_QualityStandard_Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.TabControl tab__type_standard;
        private System.Windows.Forms.Label lab_type_standard;
        private System.Windows.Forms.Label lab_current_class;
        private System.Windows.Forms.Label lab_null;
        private System.Windows.Forms.Button btn_import_data;
        private System.Windows.Forms.Label lab_systematic_name;
        private System.Windows.Forms.Button btn_Download_template;
        private System.Windows.Forms.TextBox txt_systematic_name;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn general_testtype_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn quality_category_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn quality_category_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
    }
}