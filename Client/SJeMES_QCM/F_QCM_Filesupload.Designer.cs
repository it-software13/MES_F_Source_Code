namespace SJeMES_QCM
{
    partial class F_QCM_Filesupload
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem5 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem6 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Filesupload));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.file_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.curr_valid_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.curr_upload_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.endtime1 = new System.Windows.Forms.DateTimePicker();
            this.lab_expirydate = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.endtime2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_Art = new System.Windows.Forms.TextBox();
            this.lab_ART = new System.Windows.Forms.Label();
            this.starttime2 = new System.Windows.Forms.DateTimePicker();
            this.starttime1 = new System.Windows.Forms.DateTimePicker();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.starttime1);
            this.splitContainer1.Panel1.Controls.Add(this.starttime2);
            this.splitContainer1.Panel1.Controls.Add(this.txt_Art);
            this.splitContainer1.Panel1.Controls.Add(this.lab_ART);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.endtime2);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.endtime1);
            this.splitContainer1.Panel1.Controls.Add(this.lab_expirydate);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(964, 578);
            this.splitContainer1.SplitterDistance = 99;
            this.splitContainer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(877, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "上传文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "筛选";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(64, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(126, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
            this.splitContainer2.Size = new System.Drawing.Size(964, 475);
            this.splitContainer2.SplitterDistance = 415;
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
            this.file_type,
            this.prod_no,
            this.curr_valid_time,
            this.curr_upload_time});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(944, 415);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // operation
            // 
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem5.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem5.Name = "select";
            dataGridViewOperationItem5.Text = "select";
            dataGridViewOperationItem6.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem6.Name = "delete";
            dataGridViewOperationItem6.Text = "delete";
            this.operation.Items.Add(dataGridViewOperationItem5);
            this.operation.Items.Add(dataGridViewOperationItem6);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            // 
            // file_type
            // 
            this.file_type.FillWeight = 200F;
            this.file_type.HeaderText = "文件类型";
            this.file_type.Name = "file_type";
            this.file_type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.file_type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.file_type.Width = 200;
            // 
            // prod_no
            // 
            this.prod_no.FillWeight = 200F;
            this.prod_no.HeaderText = "ART";
            this.prod_no.Name = "prod_no";
            this.prod_no.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.prod_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.prod_no.Width = 200;
            // 
            // curr_valid_time
            // 
            this.curr_valid_time.HeaderText = "有效期";
            this.curr_valid_time.Name = "curr_valid_time";
            this.curr_valid_time.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.curr_valid_time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.curr_valid_time.Width = 200;
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
            this.pageControl1.Location = new System.Drawing.Point(243, 3);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 49);
            this.pageControl1.TabIndex = 4;
            this.pageControl1.TotalCount = 0;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(612, 61);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(20, 34);
            this.textBox2.TabIndex = 125;
            this.textBox2.Text = "-";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // endtime1
            // 
            this.endtime1.CalendarFont = new System.Drawing.Font("宋体", 12F);
            this.endtime1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.endtime1.Location = new System.Drawing.Point(639, 61);
            this.endtime1.Name = "endtime1";
            this.endtime1.Size = new System.Drawing.Size(132, 26);
            this.endtime1.TabIndex = 123;
            // 
            // lab_expirydate
            // 
            this.lab_expirydate.AutoSize = true;
            this.lab_expirydate.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_expirydate.Location = new System.Drawing.Point(394, 67);
            this.lab_expirydate.Name = "lab_expirydate";
            this.lab_expirydate.Size = new System.Drawing.Size(77, 14);
            this.lab_expirydate.TabIndex = 124;
            this.lab_expirydate.Text = "有效日期：";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(612, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(20, 34);
            this.textBox1.TabIndex = 128;
            this.textBox1.Text = "-";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // endtime2
            // 
            this.endtime2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.endtime2.Location = new System.Drawing.Point(639, 23);
            this.endtime2.Name = "endtime2";
            this.endtime2.Size = new System.Drawing.Size(132, 26);
            this.endtime2.TabIndex = 127;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(390, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 129;
            this.label2.Text = "上传日期：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(796, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 28);
            this.button2.TabIndex = 130;
            this.button2.Text = "搜索";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_Art
            // 
            this.txt_Art.Location = new System.Drawing.Point(257, 24);
            this.txt_Art.Name = "txt_Art";
            this.txt_Art.Size = new System.Drawing.Size(129, 26);
            this.txt_Art.TabIndex = 132;
            // 
            // lab_ART
            // 
            this.lab_ART.AutoSize = true;
            this.lab_ART.Location = new System.Drawing.Point(209, 27);
            this.lab_ART.Name = "lab_ART";
            this.lab_ART.Size = new System.Drawing.Size(50, 20);
            this.lab_ART.TabIndex = 131;
            this.lab_ART.Text = "ART：";
            // 
            // starttime2
            // 
            this.starttime2.Location = new System.Drawing.Point(469, 24);
            this.starttime2.Name = "starttime2";
            this.starttime2.Size = new System.Drawing.Size(132, 26);
            this.starttime2.TabIndex = 133;
            // 
            // starttime1
            // 
            this.starttime1.Location = new System.Drawing.Point(469, 63);
            this.starttime1.Name = "starttime1";
            this.starttime1.Size = new System.Drawing.Size(132, 26);
            this.starttime1.TabIndex = 134;
            // 
            // F_QCM_Filesupload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 643);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Filesupload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "联名产品生产授权文件管理";
            this.Load += new System.EventHandler(this.F_QCM_Filesupload_Load);
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
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn curr_valid_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn curr_upload_time;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker endtime2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker endtime1;
        private System.Windows.Forms.Label lab_expirydate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_Art;
        private System.Windows.Forms.Label lab_ART;
        private System.Windows.Forms.DateTimePicker starttime1;
        private System.Windows.Forms.DateTimePicker starttime2;
    }
}