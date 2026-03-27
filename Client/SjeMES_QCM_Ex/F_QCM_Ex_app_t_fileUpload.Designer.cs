
namespace SjeMES_QCM_Ex
{
    partial class F_QCM_Ex_app_t_fileUpload
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Ex_app_t_fileUpload));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem5 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cb_checkall = new System.Windows.Forms.CheckBox();
            this.btn_plsx = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.endtime = new System.Windows.Forms.DateTimePicker();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_FILE_NAME = new System.Windows.Forms.TextBox();
            this.btn_uploadfile = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.curr_upload_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(2, 66);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cb_checkall);
            this.splitContainer1.Panel1.Controls.Add(this.btn_plsx);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.endtime);
            this.splitContainer1.Panel1.Controls.Add(this.start_date);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_search);
            this.splitContainer1.Panel1.Controls.Add(this.txt_FILE_NAME);
            this.splitContainer1.Panel1.Controls.Add(this.btn_uploadfile);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1092, 630);
            this.splitContainer1.SplitterDistance = 72;
            this.splitContainer1.TabIndex = 1;
            // 
            // cb_checkall
            // 
            this.cb_checkall.AutoSize = true;
            this.cb_checkall.Location = new System.Drawing.Point(58, 45);
            this.cb_checkall.Name = "cb_checkall";
            this.cb_checkall.Size = new System.Drawing.Size(56, 24);
            this.cb_checkall.TabIndex = 1;
            this.cb_checkall.Text = "全选";
            this.cb_checkall.UseVisualStyleBackColor = true;
            this.cb_checkall.CheckedChanged += new System.EventHandler(this.cb_checkall_CheckedChanged);
            // 
            // btn_plsx
            // 
            this.btn_plsx.Location = new System.Drawing.Point(977, 23);
            this.btn_plsx.Name = "btn_plsx";
            this.btn_plsx.Size = new System.Drawing.Size(104, 30);
            this.btn_plsx.TabIndex = 23;
            this.btn_plsx.Text = "批量确认有效";
            this.btn_plsx.UseVisualStyleBackColor = true;
            this.btn_plsx.Click += new System.EventHandler(this.btn_plsx_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(570, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "-";
            // 
            // endtime
            // 
            this.endtime.CustomFormat = "";
            this.endtime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endtime.Location = new System.Drawing.Point(591, 24);
            this.endtime.Name = "endtime";
            this.endtime.Size = new System.Drawing.Size(124, 26);
            this.endtime.TabIndex = 21;
            // 
            // start_date
            // 
            this.start_date.CustomFormat = "";
            this.start_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.start_date.Location = new System.Drawing.Point(440, 23);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(124, 26);
            this.start_date.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(362, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "文件名称";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(746, 24);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 30);
            this.btn_search.TabIndex = 6;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_FILE_NAME
            // 
            this.txt_FILE_NAME.Location = new System.Drawing.Point(162, 20);
            this.txt_FILE_NAME.Name = "txt_FILE_NAME";
            this.txt_FILE_NAME.Size = new System.Drawing.Size(175, 26);
            this.txt_FILE_NAME.TabIndex = 3;
            // 
            // btn_uploadfile
            // 
            this.btn_uploadfile.Location = new System.Drawing.Point(863, 23);
            this.btn_uploadfile.Name = "btn_uploadfile";
            this.btn_uploadfile.Size = new System.Drawing.Size(75, 30);
            this.btn_uploadfile.TabIndex = 2;
            this.btn_uploadfile.Text = "上传文件";
            this.btn_uploadfile.UseVisualStyleBackColor = true;
            this.btn_uploadfile.Click += new System.EventHandler(this.btn_uploadfile_Click);
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
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer2.Size = new System.Drawing.Size(1092, 554);
            this.splitContainer2.SplitterDistance = 483;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.ID,
            this.file_name,
            this.curr_upload_time});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1072, 483);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // operation
            // 
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SjeMES_QCM_Ex.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem1.Name = "delete";
            dataGridViewOperationItem1.Text = "delete";
            dataGridViewOperationItem2.Image = global::SjeMES_QCM_Ex.Properties.Resources.ic_select_24;
            dataGridViewOperationItem2.Name = "select";
            dataGridViewOperationItem2.Text = "select";
            dataGridViewOperationItem3.Image = global::SjeMES_QCM_Ex.Properties.Resources.ic_update_24;
            dataGridViewOperationItem3.Name = "update";
            dataGridViewOperationItem3.Text = "update";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.Items.Add(dataGridViewOperationItem3);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // file_name
            // 
            this.file_name.FillWeight = 200F;
            this.file_name.HeaderText = "文件名称";
            this.file_name.Name = "file_name";
            this.file_name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.file_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.file_name.Width = 200;
            // 
            // curr_upload_time
            // 
            this.curr_upload_time.FillWeight = 200F;
            this.curr_upload_time.HeaderText = "上传时间";
            this.curr_upload_time.Name = "curr_upload_time";
            this.curr_upload_time.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.curr_upload_time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.curr_upload_time.Width = 200;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(443, 10);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(638, 53);
            this.pageControl1.TabIndex = 4;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem4.Image = null;
            dataGridViewOperationItem4.Name = "select";
            dataGridViewOperationItem4.Text = "select";
            dataGridViewOperationItem5.Image = null;
            dataGridViewOperationItem5.Name = "delete";
            dataGridViewOperationItem5.Text = "delete";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem5);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // F_QCM_Ex_app_t_fileUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 693);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Ex_app_t_fileUpload";
            this.Text = "APP2报告上传";
            this.Load += new System.EventHandler(this.F_QCM_Ex_app_t_fileUpload_Load);
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

        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_FILE_NAME;
        private System.Windows.Forms.Button btn_uploadfile;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker endtime;
        private System.Windows.Forms.DateTimePicker start_date;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn curr_upload_time;
        private System.Windows.Forms.Button btn_plsx;
        private System.Windows.Forms.CheckBox cb_checkall;
    }
}