namespace SJeMES_QCM
{
    partial class F_QCM_ATR_File3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_ATR_File3));
            this.btn_add = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.txt_art = new System.Windows.Forms.TextBox();
            this.lab_ART = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimeP_putin_date = new System.Windows.Forms.DateTimePicker();
            this.lab_end_date = new System.Windows.Forms.Label();
            this.ddl_emun_file_type = new System.Windows.Forms.ComboBox();
            this.lab_Wenjian_Type = new System.Windows.Forms.Label();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.operation1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.文件类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.报告类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.有效文件 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.有效时长 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.有效日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.绑定日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FILE_URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FILE_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("宋体", 14F);
            this.btn_add.Location = new System.Drawing.Point(10, 3);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(80, 30);
            this.btn_add.TabIndex = 15;
            this.btn_add.Text = "录入";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btn_add);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer3.Size = new System.Drawing.Size(1253, 542);
            this.splitContainer3.SplitterDistance = 38;
            this.splitContainer3.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
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
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation1,
            this.文件类型,
            this.报告类型,
            this.有效文件,
            this.ART,
            this.有效时长,
            this.有效日期,
            this.绑定日期,
            this.FILE_URL,
            this.ID,
            this.FILE_TYPE});
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
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1251, 498);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer2.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel2_Paint);
            this.splitContainer2.Size = new System.Drawing.Size(1253, 607);
            this.splitContainer2.SplitterDistance = 542;
            this.splitContainer2.TabIndex = 0;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(537, 6);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(709, 49);
            this.pageControl1.TabIndex = 1;
            this.pageControl1.TotalCount = 0;
            // 
            // txt_art
            // 
            this.txt_art.Font = new System.Drawing.Font("宋体", 14F);
            this.txt_art.Location = new System.Drawing.Point(67, 14);
            this.txt_art.Name = "txt_art";
            this.txt_art.Size = new System.Drawing.Size(114, 29);
            this.txt_art.TabIndex = 17;
            // 
            // lab_ART
            // 
            this.lab_ART.AutoSize = true;
            this.lab_ART.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lab_ART.Location = new System.Drawing.Point(5, 15);
            this.lab_ART.Name = "lab_ART";
            this.lab_ART.Size = new System.Drawing.Size(67, 25);
            this.lab_ART.TabIndex = 16;
            this.lab_ART.Text = "ART：";
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("宋体", 14F);
            this.btn_search.Location = new System.Drawing.Point(1045, 9);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(72, 33);
            this.btn_search.TabIndex = 14;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(2, 65);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker1);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimeP_putin_date);
            this.splitContainer1.Panel1.Controls.Add(this.lab_end_date);
            this.splitContainer1.Panel1.Controls.Add(this.ddl_emun_file_type);
            this.splitContainer1.Panel1.Controls.Add(this.lab_Wenjian_Type);
            this.splitContainer1.Panel1.Controls.Add(this.txt_art);
            this.splitContainer1.Panel1.Controls.Add(this.lab_ART);
            this.splitContainer1.Panel1.Controls.Add(this.btn_search);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1253, 665);
            this.splitContainer1.SplitterDistance = 54;
            this.splitContainer1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(785, 7);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(39, 34);
            this.textBox2.TabIndex = 111;
            this.textBox2.Text = "～";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1.Location = new System.Drawing.Point(826, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(186, 33);
            this.dateTimePicker1.TabIndex = 110;
            // 
            // dateTimeP_putin_date
            // 
            this.dateTimeP_putin_date.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_putin_date.Location = new System.Drawing.Point(597, 8);
            this.dateTimeP_putin_date.Name = "dateTimeP_putin_date";
            this.dateTimeP_putin_date.Size = new System.Drawing.Size(186, 33);
            this.dateTimeP_putin_date.TabIndex = 108;
            // 
            // lab_end_date
            // 
            this.lab_end_date.AutoSize = true;
            this.lab_end_date.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lab_end_date.Location = new System.Drawing.Point(487, 12);
            this.lab_end_date.Name = "lab_end_date";
            this.lab_end_date.Size = new System.Drawing.Size(107, 25);
            this.lab_end_date.TabIndex = 109;
            this.lab_end_date.Text = "有效日期：";
            // 
            // ddl_emun_file_type
            // 
            this.ddl_emun_file_type.Font = new System.Drawing.Font("宋体", 14F);
            this.ddl_emun_file_type.FormattingEnabled = true;
            this.ddl_emun_file_type.Location = new System.Drawing.Point(295, 14);
            this.ddl_emun_file_type.Name = "ddl_emun_file_type";
            this.ddl_emun_file_type.Size = new System.Drawing.Size(166, 27);
            this.ddl_emun_file_type.TabIndex = 36;
            // 
            // lab_Wenjian_Type
            // 
            this.lab_Wenjian_Type.AutoSize = true;
            this.lab_Wenjian_Type.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lab_Wenjian_Type.Location = new System.Drawing.Point(205, 14);
            this.lab_Wenjian_Type.Name = "lab_Wenjian_Type";
            this.lab_Wenjian_Type.Size = new System.Drawing.Size(93, 25);
            this.lab_Wenjian_Type.TabIndex = 35;
            this.lab_Wenjian_Type.Text = "文件类型:";
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            this.dataGridViewOperationColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOperationColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // operation1
            // 
            this.operation1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation1.Description = null;
            this.operation1.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem1.Name = "edit";
            dataGridViewOperationItem1.Text = "编辑";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem2.Name = "delete";
            dataGridViewOperationItem2.Text = "删除";
            this.operation1.Items.Add(dataGridViewOperationItem1);
            this.operation1.Items.Add(dataGridViewOperationItem2);
            this.operation1.ItemSize = new System.Drawing.Size(24, 24);
            this.operation1.Name = "operation1";
            this.operation1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation1.OverflowImage")));
            this.operation1.ReadOnly = true;
            this.operation1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.operation1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 文件类型
            // 
            this.文件类型.HeaderText = "文件类型";
            this.文件类型.Name = "文件类型";
            this.文件类型.ReadOnly = true;
            // 
            // 报告类型
            // 
            this.报告类型.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.报告类型.HeaderText = "报告类型";
            this.报告类型.Name = "报告类型";
            this.报告类型.ReadOnly = true;
            this.报告类型.Width = 150;
            // 
            // 有效文件
            // 
            this.有效文件.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.有效文件.HeaderText = "有效文件";
            this.有效文件.Name = "有效文件";
            this.有效文件.ReadOnly = true;
            this.有效文件.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.有效文件.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ART
            // 
            this.ART.HeaderText = "ART";
            this.ART.Name = "ART";
            this.ART.ReadOnly = true;
            // 
            // 有效时长
            // 
            this.有效时长.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.有效时长.HeaderText = "有效时长";
            this.有效时长.Name = "有效时长";
            this.有效时长.ReadOnly = true;
            // 
            // 有效日期
            // 
            this.有效日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.有效日期.HeaderText = "有效日期";
            this.有效日期.Name = "有效日期";
            this.有效日期.ReadOnly = true;
            this.有效日期.Width = 150;
            // 
            // 绑定日期
            // 
            this.绑定日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.绑定日期.HeaderText = "绑定日期";
            this.绑定日期.Name = "绑定日期";
            this.绑定日期.ReadOnly = true;
            this.绑定日期.Width = 150;
            // 
            // FILE_URL
            // 
            this.FILE_URL.HeaderText = "FILE_URL";
            this.FILE_URL.Name = "FILE_URL";
            this.FILE_URL.ReadOnly = true;
            this.FILE_URL.Visible = false;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // FILE_TYPE
            // 
            this.FILE_TYPE.HeaderText = "FILE_TYPE";
            this.FILE_TYPE.Name = "FILE_TYPE";
            this.FILE_TYPE.ReadOnly = true;
            this.FILE_TYPE.Visible = false;
            // 
            // F_QCM_ATR_File3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 741);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_ATR_File3";
            this.Text = "ART测试文件绑定";
            this.Load += new System.EventHandler(this.F_QCM_ATR_File3_Load);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.TextBox txt_art;
        private System.Windows.Forms.Label lab_ART;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.ComboBox ddl_emun_file_type;
        private System.Windows.Forms.Label lab_Wenjian_Type;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimeP_putin_date;
        private System.Windows.Forms.Label lab_end_date;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 文件类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 报告类型;
        private System.Windows.Forms.DataGridViewLinkColumn 有效文件;
        private System.Windows.Forms.DataGridViewTextBoxColumn ART;
        private System.Windows.Forms.DataGridViewTextBoxColumn 有效时长;
        private System.Windows.Forms.DataGridViewTextBoxColumn 有效日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 绑定日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn FILE_URL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FILE_TYPE;
    }
}