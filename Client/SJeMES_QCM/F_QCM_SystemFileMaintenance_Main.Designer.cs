
namespace SJeMES_QCM
{
    partial class F_QCM_SystemFileMaintenance_Main
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_SystemFileMaintenance_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Searchbtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.txt_file_name = new System.Windows.Forms.TextBox();
            this.lab_file_name = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lab_file_type = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.文件路径 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.文件类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.有效文件 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.上传时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(-2, 61);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Searchbtn);
            this.splitContainer1.Panel1.Controls.Add(this.addbtn);
            this.splitContainer1.Panel1.Controls.Add(this.txt_file_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_file_name);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.lab_file_type);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1010, 515);
            this.splitContainer1.SplitterDistance = 90;
            this.splitContainer1.TabIndex = 0;
            // 
            // Searchbtn
            // 
            this.Searchbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Searchbtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Searchbtn.Location = new System.Drawing.Point(628, 31);
            this.Searchbtn.Name = "Searchbtn";
            this.Searchbtn.Size = new System.Drawing.Size(77, 33);
            this.Searchbtn.TabIndex = 5;
            this.Searchbtn.Text = "搜索";
            this.Searchbtn.UseVisualStyleBackColor = true;
            this.Searchbtn.Click += new System.EventHandler(this.Searchbtn_Click);
            // 
            // addbtn
            // 
            this.addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addbtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addbtn.Location = new System.Drawing.Point(734, 31);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(77, 33);
            this.addbtn.TabIndex = 4;
            this.addbtn.Text = "录入";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // txt_file_name
            // 
            this.txt_file_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_file_name.Location = new System.Drawing.Point(462, 31);
            this.txt_file_name.Name = "txt_file_name";
            this.txt_file_name.Size = new System.Drawing.Size(132, 33);
            this.txt_file_name.TabIndex = 3;
            // 
            // lab_file_name
            // 
            this.lab_file_name.AutoSize = true;
            this.lab_file_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_file_name.Location = new System.Drawing.Point(351, 35);
            this.lab_file_name.Name = "lab_file_name";
            this.lab_file_name.Size = new System.Drawing.Size(107, 25);
            this.lab_file_name.TabIndex = 2;
            this.lab_file_name.Text = "文件名称：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "品质目标",
            "品质流程",
            "组织架构",
            "WI",
            "培训文件",
            "Adidas文件",
            "品质制度",
            "品质报告"});
            this.comboBox1.Location = new System.Drawing.Point(163, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(153, 33);
            this.comboBox1.TabIndex = 1;
            // 
            // lab_file_type
            // 
            this.lab_file_type.AutoSize = true;
            this.lab_file_type.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_file_type.Location = new System.Drawing.Point(53, 35);
            this.lab_file_type.Name = "lab_file_type";
            this.lab_file_type.Size = new System.Drawing.Size(107, 25);
            this.lab_file_type.TabIndex = 0;
            this.lab_file_type.Text = "文件类型：";
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
            this.splitContainer2.Size = new System.Drawing.Size(1010, 421);
            this.splitContainer2.SplitterDistance = 358;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.文件路径,
            this.文件类型,
            this.有效文件,
            this.上传时间});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1010, 358);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(286, 1);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem3.Image = global::SJeMES_QCM.Properties.Resources.ic_select_file;
            dataGridViewOperationItem3.Name = "selectfile";
            dataGridViewOperationItem3.Text = "查看文件";
            dataGridViewOperationItem4.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem4.Name = "delete";
            dataGridViewOperationItem4.Text = "删除";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem3);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            this.dataGridViewOperationColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOperationColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_select_file;
            dataGridViewOperationItem1.Name = "selectfile";
            dataGridViewOperationItem1.Text = "查看文件";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem2.Name = "delete";
            dataGridViewOperationItem2.Text = "删除";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.MinimumWidth = 80;
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.operation.Width = 90;
            // 
            // 文件路径
            // 
            this.文件路径.HeaderText = "文件路径";
            this.文件路径.Name = "文件路径";
            this.文件路径.ReadOnly = true;
            this.文件路径.Visible = false;
            this.文件路径.Width = 99;
            // 
            // 文件类型
            // 
            this.文件类型.HeaderText = "文件类型";
            this.文件类型.Name = "文件类型";
            this.文件类型.ReadOnly = true;
            this.文件类型.Width = 99;
            // 
            // 有效文件
            // 
            this.有效文件.HeaderText = "有效文件";
            this.有效文件.Name = "有效文件";
            this.有效文件.ReadOnly = true;
            this.有效文件.Width = 99;
            // 
            // 上传时间
            // 
            this.上传时间.HeaderText = "上传时间";
            this.上传时间.Name = "上传时间";
            this.上传时间.ReadOnly = true;
            this.上传时间.Width = 99;
            // 
            // F_QCM_SystemFileMaintenance_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 575);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_SystemFileMaintenance_Main";
            this.Text = "体系文件维护";
            this.Load += new System.EventHandler(this.F_QCM_SystemFileMaintenance_Main_Load);
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
        private System.Windows.Forms.Button Searchbtn;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.TextBox txt_file_name;
        private System.Windows.Forms.Label lab_file_name;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lab_file_type;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文件路径;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文件类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 有效文件;
        private System.Windows.Forms.DataGridViewTextBoxColumn 上传时间;
    }
}