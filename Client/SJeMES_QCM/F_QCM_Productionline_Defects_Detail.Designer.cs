
namespace SJeMES_QCM
{
    partial class F_QCM_Productionline_Defects_Detail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Productionline_Defects_Detail));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txt_pro_name = new System.Windows.Forms.TextBox();
            this.lab_prodline_name = new System.Windows.Forms.Label();
            this.txt_pro_no = new System.Windows.Forms.TextBox();
            this.lab_prodline_no = new System.Windows.Forms.Label();
            this.txt_depart_name = new System.Windows.Forms.TextBox();
            this.lab_department_name = new System.Windows.Forms.Label();
            this.txt_depart_no = new System.Windows.Forms.TextBox();
            this.lab_department_no = new System.Windows.Forms.Label();
            this.btnadd = new System.Windows.Forms.Button();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.defect_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defect_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(-1, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.txt_pro_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_prodline_name);
            this.splitContainer1.Panel1.Controls.Add(this.txt_pro_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_prodline_no);
            this.splitContainer1.Panel1.Controls.Add(this.txt_depart_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_department_name);
            this.splitContainer1.Panel1.Controls.Add(this.txt_depart_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_department_no);
            this.splitContainer1.Panel1.Controls.Add(this.btnadd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(935, 468);
            this.splitContainer1.SplitterDistance = 61;
            this.splitContainer1.TabIndex = 0;
            // 
            // txt_pro_name
            // 
            this.txt_pro_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_pro_name.Location = new System.Drawing.Point(658, 20);
            this.txt_pro_name.Name = "txt_pro_name";
            this.txt_pro_name.ReadOnly = true;
            this.txt_pro_name.Size = new System.Drawing.Size(127, 29);
            this.txt_pro_name.TabIndex = 2;
            // 
            // lab_prodline_name
            // 
            this.lab_prodline_name.AutoSize = true;
            this.lab_prodline_name.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_prodline_name.Location = new System.Drawing.Point(592, 27);
            this.lab_prodline_name.Name = "lab_prodline_name";
            this.lab_prodline_name.Size = new System.Drawing.Size(65, 20);
            this.lab_prodline_name.TabIndex = 1;
            this.lab_prodline_name.Text = "产线名称";
            // 
            // txt_pro_no
            // 
            this.txt_pro_no.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_pro_no.Location = new System.Drawing.Point(459, 20);
            this.txt_pro_no.Name = "txt_pro_no";
            this.txt_pro_no.ReadOnly = true;
            this.txt_pro_no.Size = new System.Drawing.Size(127, 29);
            this.txt_pro_no.TabIndex = 2;
            // 
            // lab_prodline_no
            // 
            this.lab_prodline_no.AutoSize = true;
            this.lab_prodline_no.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_prodline_no.Location = new System.Drawing.Point(393, 27);
            this.lab_prodline_no.Name = "lab_prodline_no";
            this.lab_prodline_no.Size = new System.Drawing.Size(65, 20);
            this.lab_prodline_no.TabIndex = 1;
            this.lab_prodline_no.Text = "产线代号";
            // 
            // txt_depart_name
            // 
            this.txt_depart_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_depart_name.Location = new System.Drawing.Point(259, 20);
            this.txt_depart_name.Name = "txt_depart_name";
            this.txt_depart_name.ReadOnly = true;
            this.txt_depart_name.Size = new System.Drawing.Size(127, 29);
            this.txt_depart_name.TabIndex = 2;
            // 
            // lab_department_name
            // 
            this.lab_department_name.AutoSize = true;
            this.lab_department_name.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_department_name.Location = new System.Drawing.Point(194, 27);
            this.lab_department_name.Name = "lab_department_name";
            this.lab_department_name.Size = new System.Drawing.Size(65, 20);
            this.lab_department_name.TabIndex = 1;
            this.lab_department_name.Text = "部门名称";
            // 
            // txt_depart_no
            // 
            this.txt_depart_no.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_depart_no.Location = new System.Drawing.Point(63, 20);
            this.txt_depart_no.Name = "txt_depart_no";
            this.txt_depart_no.ReadOnly = true;
            this.txt_depart_no.Size = new System.Drawing.Size(127, 29);
            this.txt_depart_no.TabIndex = 2;
            // 
            // lab_department_no
            // 
            this.lab_department_no.AutoSize = true;
            this.lab_department_no.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_department_no.Location = new System.Drawing.Point(2, 27);
            this.lab_department_no.Name = "lab_department_no";
            this.lab_department_no.Size = new System.Drawing.Size(65, 20);
            this.lab_department_no.TabIndex = 1;
            this.lab_department_no.Text = "部门代号";
            // 
            // btnadd
            // 
            this.btnadd.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnadd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnadd.Location = new System.Drawing.Point(834, 17);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(80, 30);
            this.btnadd.TabIndex = 0;
            this.btnadd.Text = "添加";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // pageControl1
            // 
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(214, 359);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 44);
            this.pageControl1.TabIndex = 3;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
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
            this.operation,
            this.defect_no,
            this.defect_name});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 33;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(929, 350);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem1.Name = "MODIFY";
            dataGridViewOperationItem1.Text = "MODIFY";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem2.Name = "DELETE";
            dataGridViewOperationItem2.Text = "DELETE";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            // 
            // defect_no
            // 
            this.defect_no.HeaderText = "不良问题代号";
            this.defect_no.Name = "defect_no";
            this.defect_no.ReadOnly = true;
            this.defect_no.Width = 140;
            // 
            // defect_name
            // 
            this.defect_name.HeaderText = "不良问题";
            this.defect_name.Name = "defect_name";
            this.defect_name.ReadOnly = true;
            this.defect_name.Width = 250;
            // 
            // F_QCM_Productionline_Defects_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 534);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Productionline_Defects_Detail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产线不良问题记录添加";
            this.Load += new System.EventHandler(this.F_QCM_Productionline_Defects_Edit_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_pro_name;
        private System.Windows.Forms.Label lab_prodline_name;
        private System.Windows.Forms.TextBox txt_pro_no;
        private System.Windows.Forms.Label lab_prodline_no;
        private System.Windows.Forms.TextBox txt_depart_name;
        private System.Windows.Forms.Label lab_department_name;
        private System.Windows.Forms.TextBox txt_depart_no;
        private System.Windows.Forms.Label lab_department_no;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn defect_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn defect_name;
    }
}