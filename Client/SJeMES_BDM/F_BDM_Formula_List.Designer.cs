
namespace SJeMES_BDM
{
    partial class F_BDM_Formula_List
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_BDM_Formula_List));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.formula_code_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formula_name_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formula_type_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formula_content_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txt_remarks = new System.Windows.Forms.TextBox();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.lab_remarks = new System.Windows.Forms.Label();
            this.lab_formula_name = new System.Windows.Forms.Label();
            this.btn_addformula = new System.Windows.Forms.Button();
            this.btn_select = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
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
            this.formula_code_1,
            this.formula_name_1,
            this.formula_type_1,
            this.formula_content_1,
            this.remarks_1});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1097, 495);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.FillWeight = 80F;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem1.Name = "UPDATE";
            dataGridViewOperationItem1.Text = "UPDATE";
            dataGridViewOperationItem2.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem2.Name = "DELETE";
            dataGridViewOperationItem2.Text = "DELETE";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            this.operation.Width = 80;
            // 
            // formula_code_1
            // 
            this.formula_code_1.HeaderText = "公式编号";
            this.formula_code_1.MinimumWidth = 120;
            this.formula_code_1.Name = "formula_code_1";
            this.formula_code_1.ReadOnly = true;
            this.formula_code_1.Width = 120;
            // 
            // formula_name_1
            // 
            this.formula_name_1.HeaderText = "公式名称";
            this.formula_name_1.Name = "formula_name_1";
            this.formula_name_1.ReadOnly = true;
            this.formula_name_1.Width = 99;
            // 
            // formula_type_1
            // 
            this.formula_type_1.HeaderText = "公式类型";
            this.formula_type_1.Name = "formula_type_1";
            this.formula_type_1.ReadOnly = true;
            this.formula_type_1.Width = 99;
            // 
            // formula_content_1
            // 
            this.formula_content_1.HeaderText = "公式内容";
            this.formula_content_1.Name = "formula_content_1";
            this.formula_content_1.ReadOnly = true;
            this.formula_content_1.Width = 99;
            // 
            // remarks_1
            // 
            this.remarks_1.HeaderText = "备注";
            this.remarks_1.Name = "remarks_1";
            this.remarks_1.ReadOnly = true;
            this.remarks_1.Width = 67;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_remarks);
            this.splitContainer1.Panel1.Controls.Add(this.txt_code);
            this.splitContainer1.Panel1.Controls.Add(this.lab_remarks);
            this.splitContainer1.Panel1.Controls.Add(this.lab_formula_name);
            this.splitContainer1.Panel1.Controls.Add(this.btn_addformula);
            this.splitContainer1.Panel1.Controls.Add(this.btn_select);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1097, 680);
            this.splitContainer1.SplitterDistance = 102;
            this.splitContainer1.TabIndex = 25;
            // 
            // txt_remarks
            // 
            this.txt_remarks.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_remarks.Location = new System.Drawing.Point(395, 32);
            this.txt_remarks.Name = "txt_remarks";
            this.txt_remarks.Size = new System.Drawing.Size(142, 33);
            this.txt_remarks.TabIndex = 28;
            // 
            // txt_code
            // 
            this.txt_code.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_code.Location = new System.Drawing.Point(156, 32);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(142, 33);
            this.txt_code.TabIndex = 29;
            // 
            // lab_remarks
            // 
            this.lab_remarks.AutoSize = true;
            this.lab_remarks.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_remarks.Location = new System.Drawing.Point(323, 36);
            this.lab_remarks.Name = "lab_remarks";
            this.lab_remarks.Size = new System.Drawing.Size(69, 25);
            this.lab_remarks.TabIndex = 26;
            this.lab_remarks.Text = "备注：";
            // 
            // lab_formula_name
            // 
            this.lab_formula_name.AutoSize = true;
            this.lab_formula_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_formula_name.Location = new System.Drawing.Point(46, 36);
            this.lab_formula_name.Name = "lab_formula_name";
            this.lab_formula_name.Size = new System.Drawing.Size(107, 25);
            this.lab_formula_name.TabIndex = 27;
            this.lab_formula_name.Text = "公式名称：";
            // 
            // btn_addformula
            // 
            this.btn_addformula.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_addformula.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addformula.Location = new System.Drawing.Point(756, 32);
            this.btn_addformula.Name = "btn_addformula";
            this.btn_addformula.Size = new System.Drawing.Size(111, 32);
            this.btn_addformula.TabIndex = 24;
            this.btn_addformula.Text = "新建公式";
            this.btn_addformula.UseVisualStyleBackColor = true;
            this.btn_addformula.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btn_select
            // 
            this.btn_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_select.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_select.Location = new System.Drawing.Point(623, 32);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(85, 33);
            this.btn_select.TabIndex = 25;
            this.btn_select.Text = "搜索";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
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
            this.splitContainer2.Size = new System.Drawing.Size(1097, 574);
            this.splitContainer2.SplitterDistance = 495;
            this.splitContainer2.TabIndex = 0;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(485, 5);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(608, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.FillWeight = 80F;
            this.dataGridViewOperationColumn1.Frozen = true;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem3.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem3.Name = "UPDATE";
            dataGridViewOperationItem3.Text = "UPDATE";
            dataGridViewOperationItem4.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem4.Name = "DELETE";
            dataGridViewOperationItem4.Text = "DELETE";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem3);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            this.dataGridViewOperationColumn1.Width = 80;
            // 
            // F_BDM_Formula_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 747);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_BDM_Formula_List";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "计算公式维护";
            this.Load += new System.EventHandler(this.Frm_bdm_formulaList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txt_remarks;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Label lab_remarks;
        private System.Windows.Forms.Label lab_formula_name;
        private System.Windows.Forms.Button btn_addformula;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn formula_code_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn formula_name_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn formula_type_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn formula_content_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks_1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
    }
}