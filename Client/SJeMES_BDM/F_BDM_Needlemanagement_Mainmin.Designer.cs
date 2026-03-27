namespace SJeMES_BDM
{
    partial class F_BDM_Needlemanagement_Mainmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_BDM_Needlemanagement_Mainmin));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_add = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lab_needle_category_name = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lab_production_line_name = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lab_org_name = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.collar_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collar_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
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
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(10, 78);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.Panel1.Controls.Add(this.btn_add);
            this.splitContainer1.Panel1.Controls.Add(this.panel8);
            this.splitContainer1.Panel1.Controls.Add(this.panel7);
            this.splitContainer1.Panel1.Controls.Add(this.panel6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(859, 531);
            this.splitContainer1.SplitterDistance = 122;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_add.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_add.Location = new System.Drawing.Point(678, 28);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(74, 38);
            this.btn_add.TabIndex = 29;
            this.btn_add.Text = "新增";
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lab_needle_category_name);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Location = new System.Drawing.Point(434, 27);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(238, 38);
            this.panel8.TabIndex = 6;
            // 
            // lab_needle_category_name
            // 
            this.lab_needle_category_name.AutoSize = true;
            this.lab_needle_category_name.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_needle_category_name.Location = new System.Drawing.Point(91, 8);
            this.lab_needle_category_name.Name = "lab_needle_category_name";
            this.lab_needle_category_name.Size = new System.Drawing.Size(37, 20);
            this.lab_needle_category_name.TabIndex = 1;
            this.lab_needle_category_name.Text = "厂区";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(6, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "用针类型：";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lab_production_line_name);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Location = new System.Drawing.Point(223, 27);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(205, 38);
            this.panel7.TabIndex = 5;
            // 
            // lab_production_line_name
            // 
            this.lab_production_line_name.AutoSize = true;
            this.lab_production_line_name.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_production_line_name.Location = new System.Drawing.Point(73, 8);
            this.lab_production_line_name.Name = "lab_production_line_name";
            this.lab_production_line_name.Size = new System.Drawing.Size(37, 20);
            this.lab_production_line_name.TabIndex = 1;
            this.lab_production_line_name.Text = "厂区";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "产线：";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lab_org_name);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Location = new System.Drawing.Point(12, 27);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(205, 38);
            this.panel6.TabIndex = 4;
            // 
            // lab_org_name
            // 
            this.lab_org_name.AutoSize = true;
            this.lab_org_name.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_org_name.Location = new System.Drawing.Point(63, 9);
            this.lab_org_name.Name = "lab_org_name";
            this.lab_org_name.Size = new System.Drawing.Size(37, 20);
            this.lab_org_name.TabIndex = 1;
            this.lab_org_name.Text = "厂区";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "厂区：";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer2.Size = new System.Drawing.Size(859, 405);
            this.splitContainer2.SplitterDistance = 330;
            this.splitContainer2.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.operation,
            this.collar_qty,
            this.collar_date,
            this.remarks});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 14.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 14.25F);
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(859, 330);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // operation
            // 
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem1.Name = "delete";
            dataGridViewOperationItem1.Text = "delete";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            // 
            // collar_qty
            // 
            this.collar_qty.HeaderText = "领用数量";
            this.collar_qty.Name = "collar_qty";
            this.collar_qty.ReadOnly = true;
            // 
            // collar_date
            // 
            this.collar_date.HeaderText = "领用日期";
            this.collar_date.Name = "collar_date";
            this.collar_date.ReadOnly = true;
            // 
            // remarks
            // 
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(315, 8);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(540, 53);
            this.pageControl1.TabIndex = 2;
            this.pageControl1.TotalCount = 0;
            // 
            // F_BDM_Needlemanagement_Mainmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 620);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_BDM_Needlemanagement_Mainmin";
            this.Padding = new System.Windows.Forms.Padding(10, 11, 10, 11);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "领用记录";
            this.Load += new System.EventHandler(this.F_BDM_Needlemanagement_Mainmin_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lab_needle_category_name;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lab_production_line_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lab_org_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn collar_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn collar_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
    }
}