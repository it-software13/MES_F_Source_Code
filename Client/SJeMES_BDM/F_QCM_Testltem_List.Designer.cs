
namespace SJeMES_BDM
{
    partial class F_QCM_Testltem_List
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem13 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem14 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Testltem_List));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem15 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem16 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_testitem_name = new System.Windows.Forms.TextBox();
            this.lab_testitem_name = new System.Windows.Forms.Label();
            this.txt_testitem_code = new System.Windows.Forms.TextBox();
            this.lab_testitem_code = new System.Windows.Forms.Label();
            this.txt_testtype_name = new System.Windows.Forms.TextBox();
            this.lab_testtype_name = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.testtype_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testtype_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AQL_LEVEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testitem_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testitem_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sample_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formula_name_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formula_name_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enum_value_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
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
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 66);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_add);
            this.splitContainer1.Panel1.Controls.Add(this.btn_search);
            this.splitContainer1.Panel1.Controls.Add(this.txt_testitem_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_testitem_name);
            this.splitContainer1.Panel1.Controls.Add(this.txt_testitem_code);
            this.splitContainer1.Panel1.Controls.Add(this.lab_testitem_code);
            this.splitContainer1.Panel1.Controls.Add(this.txt_testtype_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_testtype_name);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1236, 653);
            this.splitContainer1.SplitterDistance = 124;
            this.splitContainer1.TabIndex = 8;
            // 
            // btn_add
            // 
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(1116, 44);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(78, 31);
            this.btn_add.TabIndex = 37;
            this.btn_add.Text = "新增";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_search
            // 
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Location = new System.Drawing.Point(1007, 44);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(78, 31);
            this.btn_search.TabIndex = 35;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_testitem_name
            // 
            this.txt_testitem_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_testitem_name.Location = new System.Drawing.Point(804, 43);
            this.txt_testitem_name.Name = "txt_testitem_name";
            this.txt_testitem_name.Size = new System.Drawing.Size(158, 33);
            this.txt_testitem_name.TabIndex = 34;
            // 
            // lab_testitem_name
            // 
            this.lab_testitem_name.AutoSize = true;
            this.lab_testitem_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_testitem_name.Location = new System.Drawing.Point(675, 47);
            this.lab_testitem_name.Name = "lab_testitem_name";
            this.lab_testitem_name.Size = new System.Drawing.Size(126, 25);
            this.lab_testitem_name.TabIndex = 36;
            this.lab_testitem_name.Text = "检测项名称：";
            // 
            // txt_testitem_code
            // 
            this.txt_testitem_code.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_testitem_code.Location = new System.Drawing.Point(497, 43);
            this.txt_testitem_code.Name = "txt_testitem_code";
            this.txt_testitem_code.Size = new System.Drawing.Size(158, 33);
            this.txt_testitem_code.TabIndex = 32;
            // 
            // lab_testitem_code
            // 
            this.lab_testitem_code.AutoSize = true;
            this.lab_testitem_code.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_testitem_code.Location = new System.Drawing.Point(368, 47);
            this.lab_testitem_code.Name = "lab_testitem_code";
            this.lab_testitem_code.Size = new System.Drawing.Size(126, 25);
            this.lab_testitem_code.TabIndex = 33;
            this.lab_testitem_code.Text = "检测项编号：";
            // 
            // txt_testtype_name
            // 
            this.txt_testtype_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_testtype_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_testtype_name.Location = new System.Drawing.Point(176, 43);
            this.txt_testtype_name.Name = "txt_testtype_name";
            this.txt_testtype_name.Size = new System.Drawing.Size(158, 33);
            this.txt_testtype_name.TabIndex = 31;
            this.txt_testtype_name.DoubleClick += new System.EventHandler(this.txt_testtype_name_DoubleClick);
            // 
            // lab_testtype_name
            // 
            this.lab_testtype_name.AutoSize = true;
            this.lab_testtype_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_testtype_name.Location = new System.Drawing.Point(47, 47);
            this.lab_testtype_name.Name = "lab_testtype_name";
            this.lab_testtype_name.Size = new System.Drawing.Size(126, 25);
            this.lab_testtype_name.TabIndex = 30;
            this.lab_testtype_name.Text = "检测项类型：";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
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
            this.splitContainer2.Size = new System.Drawing.Size(1236, 525);
            this.splitContainer2.SplitterDistance = 444;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.testtype_no,
            this.testtype_name,
            this.AQL_LEVEL,
            this.testitem_code,
            this.testitem_name,
            this.sample_num,
            this.formula_name_1,
            this.formula_name_2,
            this.enum_value_1,
            this.remarks});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1236, 444);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem13.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem13.Name = "UPDATE";
            dataGridViewOperationItem13.Text = "UPDATE";
            dataGridViewOperationItem14.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem14.Name = "DELETE";
            dataGridViewOperationItem14.Text = "DELETE";
            this.operation.Items.Add(dataGridViewOperationItem13);
            this.operation.Items.Add(dataGridViewOperationItem14);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            // 
            // testtype_no
            // 
            this.testtype_no.HeaderText = "测试类型代号";
            this.testtype_no.MinimumWidth = 130;
            this.testtype_no.Name = "testtype_no";
            this.testtype_no.ReadOnly = true;
            this.testtype_no.Width = 131;
            // 
            // testtype_name
            // 
            this.testtype_name.HeaderText = "测试类型名称";
            this.testtype_name.Name = "testtype_name";
            this.testtype_name.ReadOnly = true;
            this.testtype_name.Width = 131;
            // 
            // AQL_LEVEL
            // 
            this.AQL_LEVEL.HeaderText = "AQL级别";
            this.AQL_LEVEL.Name = "AQL_LEVEL";
            this.AQL_LEVEL.ReadOnly = true;
            this.AQL_LEVEL.Width = 99;
            // 
            // testitem_code
            // 
            this.testitem_code.HeaderText = "检测项编号";
            this.testitem_code.Name = "testitem_code";
            this.testitem_code.ReadOnly = true;
            this.testitem_code.Width = 115;
            // 
            // testitem_name
            // 
            this.testitem_name.HeaderText = "检测项名称";
            this.testitem_name.Name = "testitem_name";
            this.testitem_name.ReadOnly = true;
            this.testitem_name.Width = 115;
            // 
            // sample_num
            // 
            this.sample_num.HeaderText = "试样数量";
            this.sample_num.Name = "sample_num";
            this.sample_num.ReadOnly = true;
            this.sample_num.Width = 99;
            // 
            // formula_name_1
            // 
            this.formula_name_1.HeaderText = "通用公式类型";
            this.formula_name_1.Name = "formula_name_1";
            this.formula_name_1.ReadOnly = true;
            this.formula_name_1.Width = 131;
            // 
            // formula_name_2
            // 
            this.formula_name_2.HeaderText = "自定义公式类别";
            this.formula_name_2.Name = "formula_name_2";
            this.formula_name_2.ReadOnly = true;
            this.formula_name_2.Width = 147;
            // 
            // enum_value_1
            // 
            this.enum_value_1.HeaderText = "结果引用类别";
            this.enum_value_1.Name = "enum_value_1";
            this.enum_value_1.ReadOnly = true;
            this.enum_value_1.Width = 131;
            // 
            // remarks
            // 
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.Width = 67;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(619, 4);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(616, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.Frozen = true;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem15.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem15.Name = "UPDATE";
            dataGridViewOperationItem15.Text = "UPDATE";
            dataGridViewOperationItem16.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem16.Name = "DELETE";
            dataGridViewOperationItem16.Text = "DELETE";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem15);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem16);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // F_QCM_Testltem_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 719);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Testltem_List";
            this.Text = "测试项目库";
            this.Load += new System.EventHandler(this.F_QCM_Testltem_List_Load);
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_testitem_name;
        private System.Windows.Forms.Label lab_testitem_name;
        private System.Windows.Forms.TextBox txt_testitem_code;
        private System.Windows.Forms.Label lab_testitem_code;
        private System.Windows.Forms.TextBox txt_testtype_name;
        private System.Windows.Forms.Label lab_testtype_name;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn testtype_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn testtype_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn AQL_LEVEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn testitem_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn testitem_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn sample_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn formula_name_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn formula_name_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn enum_value_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
    }
}