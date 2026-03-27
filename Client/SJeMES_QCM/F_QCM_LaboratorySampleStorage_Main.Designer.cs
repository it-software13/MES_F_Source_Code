
namespace SJeMES_QCM
{
    partial class F_QCM_LaboratorySampleStorage_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_LaboratorySampleStorage_Main));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTime_end_expect = new System.Windows.Forms.DateTimePicker();
            this.dateTime_putin_expect = new System.Windows.Forms.DateTimePicker();
            this.lab_EndDate = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimeP_end_date = new System.Windows.Forms.DateTimePicker();
            this.dateTimeP_putin_date = new System.Windows.Forms.DateTimePicker();
            this.btn_Select = new System.Windows.Forms.Button();
            this.lab_item_no = new System.Windows.Forms.Label();
            this.txt_PARENT_ITEM_NO = new System.Windows.Forms.TextBox();
            this.txt_ITEM_NO = new System.Windows.Forms.TextBox();
            this.lab_prod_no = new System.Windows.Forms.Label();
            this.lab_item_name = new System.Windows.Forms.Label();
            this.lab_vend_name = new System.Windows.Forms.Label();
            this.lab_putin_date = new System.Windows.Forms.Label();
            this.txt_NAME_S = new System.Windows.Forms.TextBox();
            this.txt_SUPPLIERS_NAME = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.item_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name_s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.location_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suppliers_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent_item_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.putin_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dateTime_end_expect);
            this.splitContainer1.Panel1.Controls.Add(this.dateTime_putin_expect);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Add);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimeP_end_date);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimeP_putin_date);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Select);
            this.splitContainer1.Panel1.Controls.Add(this.txt_PARENT_ITEM_NO);
            this.splitContainer1.Panel1.Controls.Add(this.txt_ITEM_NO);
            this.splitContainer1.Panel1.Controls.Add(this.txt_NAME_S);
            this.splitContainer1.Panel1.Controls.Add(this.txt_SUPPLIERS_NAME);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.lab_EndDate);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.lab_item_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_prod_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_item_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_vend_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_putin_date);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1297, 671);
            this.splitContainer1.SplitterDistance = 121;
            this.splitContainer1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(859, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(39, 34);
            this.textBox1.TabIndex = 110;
            this.textBox1.Text = "～";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTime_end_expect
            // 
            this.dateTime_end_expect.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTime_end_expect.Location = new System.Drawing.Point(901, 66);
            this.dateTime_end_expect.Name = "dateTime_end_expect";
            this.dateTime_end_expect.Size = new System.Drawing.Size(185, 33);
            this.dateTime_end_expect.TabIndex = 108;
            // 
            // dateTime_putin_expect
            // 
            this.dateTime_putin_expect.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTime_putin_expect.Location = new System.Drawing.Point(672, 66);
            this.dateTime_putin_expect.Name = "dateTime_putin_expect";
            this.dateTime_putin_expect.Size = new System.Drawing.Size(185, 33);
            this.dateTime_putin_expect.TabIndex = 107;
            // 
            // lab_EndDate
            // 
            this.lab_EndDate.AutoSize = true;
            this.lab_EndDate.BackColor = System.Drawing.Color.White;
            this.lab_EndDate.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_EndDate.Location = new System.Drawing.Point(540, 70);
            this.lab_EndDate.Name = "lab_EndDate";
            this.lab_EndDate.Size = new System.Drawing.Size(126, 25);
            this.lab_EndDate.TabIndex = 109;
            this.lab_EndDate.Text = "预计到期日：";
            // 
            // btn_Add
            // 
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(1200, 64);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(85, 33);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "新增";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(300, 67);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(39, 34);
            this.textBox2.TabIndex = 106;
            this.textBox2.Text = "～";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTimeP_end_date
            // 
            this.dateTimeP_end_date.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_end_date.Location = new System.Drawing.Point(342, 68);
            this.dateTimeP_end_date.Name = "dateTimeP_end_date";
            this.dateTimeP_end_date.Size = new System.Drawing.Size(185, 33);
            this.dateTimeP_end_date.TabIndex = 6;
            // 
            // dateTimeP_putin_date
            // 
            this.dateTimeP_putin_date.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_putin_date.Location = new System.Drawing.Point(113, 68);
            this.dateTimeP_putin_date.Name = "dateTimeP_putin_date";
            this.dateTimeP_putin_date.Size = new System.Drawing.Size(185, 33);
            this.dateTimeP_putin_date.TabIndex = 5;
            // 
            // btn_Select
            // 
            this.btn_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Select.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Select.Location = new System.Drawing.Point(1109, 64);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(85, 33);
            this.btn_Select.TabIndex = 7;
            this.btn_Select.Text = "搜索";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // lab_item_no
            // 
            this.lab_item_no.AutoSize = true;
            this.lab_item_no.BackColor = System.Drawing.Color.White;
            this.lab_item_no.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_item_no.Location = new System.Drawing.Point(42, 22);
            this.lab_item_no.Name = "lab_item_no";
            this.lab_item_no.Size = new System.Drawing.Size(69, 25);
            this.lab_item_no.TabIndex = 92;
            this.lab_item_no.Text = "品号：";
            // 
            // txt_PARENT_ITEM_NO
            // 
            this.txt_PARENT_ITEM_NO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_PARENT_ITEM_NO.Location = new System.Drawing.Point(901, 19);
            this.txt_PARENT_ITEM_NO.Name = "txt_PARENT_ITEM_NO";
            this.txt_PARENT_ITEM_NO.Size = new System.Drawing.Size(141, 33);
            this.txt_PARENT_ITEM_NO.TabIndex = 4;
            // 
            // txt_ITEM_NO
            // 
            this.txt_ITEM_NO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_ITEM_NO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ITEM_NO.Location = new System.Drawing.Point(114, 18);
            this.txt_ITEM_NO.Name = "txt_ITEM_NO";
            this.txt_ITEM_NO.Size = new System.Drawing.Size(141, 33);
            this.txt_ITEM_NO.TabIndex = 1;
            // 
            // lab_prod_no
            // 
            this.lab_prod_no.AutoSize = true;
            this.lab_prod_no.BackColor = System.Drawing.Color.White;
            this.lab_prod_no.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_prod_no.Location = new System.Drawing.Point(831, 23);
            this.lab_prod_no.Name = "lab_prod_no";
            this.lab_prod_no.Size = new System.Drawing.Size(67, 25);
            this.lab_prod_no.TabIndex = 100;
            this.lab_prod_no.Text = "ART：";
            // 
            // lab_item_name
            // 
            this.lab_item_name.AutoSize = true;
            this.lab_item_name.BackColor = System.Drawing.Color.White;
            this.lab_item_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_item_name.Location = new System.Drawing.Point(298, 22);
            this.lab_item_name.Name = "lab_item_name";
            this.lab_item_name.Size = new System.Drawing.Size(69, 25);
            this.lab_item_name.TabIndex = 97;
            this.lab_item_name.Text = "品名：";
            // 
            // lab_vend_name
            // 
            this.lab_vend_name.AutoSize = true;
            this.lab_vend_name.BackColor = System.Drawing.Color.White;
            this.lab_vend_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_vend_name.Location = new System.Drawing.Point(578, 21);
            this.lab_vend_name.Name = "lab_vend_name";
            this.lab_vend_name.Size = new System.Drawing.Size(88, 25);
            this.lab_vend_name.TabIndex = 99;
            this.lab_vend_name.Text = "供应商：";
            // 
            // lab_putin_date
            // 
            this.lab_putin_date.AutoSize = true;
            this.lab_putin_date.BackColor = System.Drawing.Color.White;
            this.lab_putin_date.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_putin_date.Location = new System.Drawing.Point(3, 72);
            this.lab_putin_date.Name = "lab_putin_date";
            this.lab_putin_date.Size = new System.Drawing.Size(107, 25);
            this.lab_putin_date.TabIndex = 94;
            this.lab_putin_date.Text = "入库日期：";
            // 
            // txt_NAME_S
            // 
            this.txt_NAME_S.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_NAME_S.Location = new System.Drawing.Point(370, 18);
            this.txt_NAME_S.Name = "txt_NAME_S";
            this.txt_NAME_S.Size = new System.Drawing.Size(141, 33);
            this.txt_NAME_S.TabIndex = 2;
            // 
            // txt_SUPPLIERS_NAME
            // 
            this.txt_SUPPLIERS_NAME.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SUPPLIERS_NAME.Location = new System.Drawing.Point(671, 19);
            this.txt_SUPPLIERS_NAME.Name = "txt_SUPPLIERS_NAME";
            this.txt_SUPPLIERS_NAME.Size = new System.Drawing.Size(141, 33);
            this.txt_SUPPLIERS_NAME.TabIndex = 3;
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
            this.splitContainer2.Size = new System.Drawing.Size(1297, 546);
            this.splitContainer2.SplitterDistance = 477;
            this.splitContainer2.TabIndex = 1;
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
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.item_no,
            this.name_s,
            this.location_name,
            this.suppliers_name,
            this.parent_item_no,
            this.putin_date,
            this.end_date});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1297, 477);
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
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem1.Name = "UPDATE";
            dataGridViewOperationItem1.Text = "UPDATE";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem2.Name = "DELETE";
            dataGridViewOperationItem2.Text = "DELETE";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.operation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // item_no
            // 
            this.item_no.HeaderText = "品号";
            this.item_no.Name = "item_no";
            // 
            // name_s
            // 
            this.name_s.HeaderText = "品名";
            this.name_s.Name = "name_s";
            // 
            // location_name
            // 
            this.location_name.HeaderText = "存放位置";
            this.location_name.Name = "location_name";
            // 
            // suppliers_name
            // 
            this.suppliers_name.HeaderText = "供应商";
            this.suppliers_name.Name = "suppliers_name";
            // 
            // parent_item_no
            // 
            this.parent_item_no.HeaderText = "相关ART";
            this.parent_item_no.Name = "parent_item_no";
            // 
            // putin_date
            // 
            this.putin_date.HeaderText = "入库日期";
            this.putin_date.Name = "putin_date";
            // 
            // end_date
            // 
            this.end_date.HeaderText = "预计到期日期";
            this.end_date.Name = "end_date";
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(186, 8);
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
            this.dataGridViewOperationColumn1.Frozen = true;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem3.Image = global::SJeMES_QCM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem3.Name = "UPDATE";
            dataGridViewOperationItem3.Text = "UPDATE";
            dataGridViewOperationItem4.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem4.Name = "DELETE";
            dataGridViewOperationItem4.Text = "DELETE";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem3);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            this.dataGridViewOperationColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOperationColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // F_QCM_LaboratorySampleStorage_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 735);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_LaboratorySampleStorage_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实验室样品存放管理";
            this.Load += new System.EventHandler(this.F_QCM_LaboratorySampleStorage_Main_Load);
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
        private System.Windows.Forms.DateTimePicker dateTimeP_end_date;
        private System.Windows.Forms.DateTimePicker dateTimeP_putin_date;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Label lab_item_no;
        private System.Windows.Forms.TextBox txt_PARENT_ITEM_NO;
        private System.Windows.Forms.TextBox txt_ITEM_NO;
        private System.Windows.Forms.Label lab_prod_no;
        private System.Windows.Forms.Label lab_item_name;
        private System.Windows.Forms.Label lab_vend_name;
        private System.Windows.Forms.Label lab_putin_date;
        private System.Windows.Forms.TextBox txt_NAME_S;
        private System.Windows.Forms.TextBox txt_SUPPLIERS_NAME;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Add;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTime_end_expect;
        private System.Windows.Forms.DateTimePicker dateTime_putin_expect;
        private System.Windows.Forms.Label lab_EndDate;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_s;
        private System.Windows.Forms.DataGridViewTextBoxColumn location_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn suppliers_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent_item_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn putin_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn end_date;
    }
}