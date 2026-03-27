
namespace SJeMES_QCM
{
    partial class F_QCM_Productionline_Defects_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Productionline_Defects_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_select = new System.Windows.Forms.Button();
            this.txt_productionline_name = new System.Windows.Forms.TextBox();
            this.lab_production_name = new System.Windows.Forms.Label();
            this.txt_productionline_no = new System.Windows.Forms.TextBox();
            this.lab_productionline_no = new System.Windows.Forms.Label();
            this.txt_Department_name = new System.Windows.Forms.TextBox();
            this.lab_bmmc = new System.Windows.Forms.Label();
            this.txt_Department_no = new System.Windows.Forms.TextBox();
            this.lab_bmdh = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.department_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.department_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productionline_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productionline_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
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
            this.splitContainer1.Location = new System.Drawing.Point(1, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_select);
            this.splitContainer1.Panel1.Controls.Add(this.txt_productionline_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_production_name);
            this.splitContainer1.Panel1.Controls.Add(this.txt_productionline_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_productionline_no);
            this.splitContainer1.Panel1.Controls.Add(this.txt_Department_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_bmmc);
            this.splitContainer1.Panel1.Controls.Add(this.txt_Department_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_bmdh);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1135, 693);
            this.splitContainer1.SplitterDistance = 116;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_select
            // 
            this.btn_select.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_select.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_select.Location = new System.Drawing.Point(701, 69);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(93, 30);
            this.btn_select.TabIndex = 5;
            this.btn_select.Text = "搜索";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // txt_productionline_name
            // 
            this.txt_productionline_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_productionline_name.Location = new System.Drawing.Point(413, 67);
            this.txt_productionline_name.Name = "txt_productionline_name";
            this.txt_productionline_name.Size = new System.Drawing.Size(207, 29);
            this.txt_productionline_name.TabIndex = 4;
            // 
            // lab_production_name
            // 
            this.lab_production_name.AutoSize = true;
            this.lab_production_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_production_name.Location = new System.Drawing.Point(333, 72);
            this.lab_production_name.Name = "lab_production_name";
            this.lab_production_name.Size = new System.Drawing.Size(74, 21);
            this.lab_production_name.TabIndex = 0;
            this.lab_production_name.Text = "产线名称";
            // 
            // txt_productionline_no
            // 
            this.txt_productionline_no.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_productionline_no.Location = new System.Drawing.Point(413, 23);
            this.txt_productionline_no.Name = "txt_productionline_no";
            this.txt_productionline_no.Size = new System.Drawing.Size(207, 29);
            this.txt_productionline_no.TabIndex = 3;
            // 
            // lab_productionline_no
            // 
            this.lab_productionline_no.AutoSize = true;
            this.lab_productionline_no.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_productionline_no.Location = new System.Drawing.Point(333, 27);
            this.lab_productionline_no.Name = "lab_productionline_no";
            this.lab_productionline_no.Size = new System.Drawing.Size(74, 21);
            this.lab_productionline_no.TabIndex = 0;
            this.lab_productionline_no.Text = "产线代号";
            // 
            // txt_Department_name
            // 
            this.txt_Department_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Department_name.Location = new System.Drawing.Point(96, 69);
            this.txt_Department_name.Name = "txt_Department_name";
            this.txt_Department_name.Size = new System.Drawing.Size(207, 29);
            this.txt_Department_name.TabIndex = 2;
            // 
            // lab_bmmc
            // 
            this.lab_bmmc.AutoSize = true;
            this.lab_bmmc.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_bmmc.Location = new System.Drawing.Point(16, 74);
            this.lab_bmmc.Name = "lab_bmmc";
            this.lab_bmmc.Size = new System.Drawing.Size(74, 21);
            this.lab_bmmc.TabIndex = 0;
            this.lab_bmmc.Text = "部门名称";
            // 
            // txt_Department_no
            // 
            this.txt_Department_no.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Department_no.Location = new System.Drawing.Point(96, 23);
            this.txt_Department_no.Name = "txt_Department_no";
            this.txt_Department_no.Size = new System.Drawing.Size(207, 29);
            this.txt_Department_no.TabIndex = 1;
            // 
            // lab_bmdh
            // 
            this.lab_bmdh.AutoSize = true;
            this.lab_bmdh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_bmdh.Location = new System.Drawing.Point(16, 27);
            this.lab_bmdh.Name = "lab_bmdh";
            this.lab_bmdh.Size = new System.Drawing.Size(74, 21);
            this.lab_bmdh.TabIndex = 0;
            this.lab_bmdh.Text = "部门代号";
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
            this.splitContainer2.Size = new System.Drawing.Size(1135, 573);
            this.splitContainer2.SplitterDistance = 511;
            this.splitContainer2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Operation,
            this.department_no,
            this.department_name,
            this.productionline_no,
            this.productionline_name});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 30;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1135, 511);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // Operation
            // 
            this.Operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Operation.Description = null;
            this.Operation.Frozen = true;
            this.Operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem1.Name = "UPDATE";
            dataGridViewOperationItem1.Text = "UPDATE";
            this.Operation.Items.Add(dataGridViewOperationItem1);
            this.Operation.ItemSize = new System.Drawing.Size(24, 24);
            this.Operation.Name = "Operation";
            this.Operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("Operation.OverflowImage")));
            this.Operation.ReadOnly = true;
            this.Operation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Operation.Width = 80;
            // 
            // department_no
            // 
            this.department_no.HeaderText = "部门代号";
            this.department_no.MinimumWidth = 120;
            this.department_no.Name = "department_no";
            this.department_no.ReadOnly = true;
            this.department_no.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.department_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.department_no.Width = 120;
            // 
            // department_name
            // 
            this.department_name.HeaderText = "部门名称";
            this.department_name.MinimumWidth = 120;
            this.department_name.Name = "department_name";
            this.department_name.ReadOnly = true;
            this.department_name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.department_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.department_name.Width = 120;
            // 
            // productionline_no
            // 
            this.productionline_no.HeaderText = "产线代号";
            this.productionline_no.MinimumWidth = 120;
            this.productionline_no.Name = "productionline_no";
            this.productionline_no.ReadOnly = true;
            this.productionline_no.Width = 120;
            // 
            // productionline_name
            // 
            this.productionline_name.HeaderText = "产线名称";
            this.productionline_name.MinimumWidth = 120;
            this.productionline_name.Name = "productionline_name";
            this.productionline_name.ReadOnly = true;
            this.productionline_name.Width = 120;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(411, 7);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 44);
            this.pageControl1.TabIndex = 2;
            this.pageControl1.TotalCount = 0;
            // 
            // F_QCM_Productionline_Defects_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1139, 757);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Productionline_Defects_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产线不良问题点";
            this.Load += new System.EventHandler(this.F_QCM_Productionline_Defects_Main_Load);
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
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.TextBox txt_productionline_name;
        private System.Windows.Forms.Label lab_production_name;
        private System.Windows.Forms.TextBox txt_productionline_no;
        private System.Windows.Forms.Label lab_productionline_no;
        private System.Windows.Forms.TextBox txt_Department_name;
        private System.Windows.Forms.Label lab_bmmc;
        private System.Windows.Forms.TextBox txt_Department_no;
        private System.Windows.Forms.Label lab_bmdh;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn Operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn department_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn department_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn productionline_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn productionline_name;
    }
}