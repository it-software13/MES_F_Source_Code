
namespace SJeMES_BDM
{
    partial class F_BDM_FittingsampleLocation_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem31 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem32 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem29 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem30 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_BDM_FittingsampleLocation_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Add = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimeP_end_date = new System.Windows.Forms.DateTimePicker();
            this.dateTimeP_putin_date = new System.Windows.Forms.DateTimePicker();
            this.btn_Select = new System.Windows.Forms.Button();
            this.lab_art_number = new System.Windows.Forms.Label();
            this.txt_PARENT_ITEM_NO = new System.Windows.Forms.TextBox();
            this.txt_ITEM_NO = new System.Windows.Forms.TextBox();
            this.lab_art = new System.Windows.Forms.Label();
            this.lab_art_name = new System.Windows.Forms.Label();
            this.lab_supplier = new System.Windows.Forms.Label();
            this.lab_Inbound_date = new System.Windows.Forms.Label();
            this.txt_NAME_S = new System.Windows.Forms.TextBox();
            this.txt_SUPPLIERS_NAME = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.item_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name_s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.location_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suppliers_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent_item_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.putin_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(1, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.txt_ITEM_NO);
            this.splitContainer1.Panel1.Controls.Add(this.txt_NAME_S);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Add);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimeP_end_date);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimeP_putin_date);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Select);
            this.splitContainer1.Panel1.Controls.Add(this.txt_PARENT_ITEM_NO);
            this.splitContainer1.Panel1.Controls.Add(this.txt_SUPPLIERS_NAME);
            this.splitContainer1.Panel1.Controls.Add(this.lab_art);
            this.splitContainer1.Panel1.Controls.Add(this.lab_supplier);
            this.splitContainer1.Panel1.Controls.Add(this.lab_Inbound_date);
            this.splitContainer1.Panel1.Controls.Add(this.lab_art_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_art_number);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1111, 578);
            this.splitContainer1.SplitterDistance = 126;
            this.splitContainer1.TabIndex = 1;
            // 
            // btn_Add
            // 
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(862, 65);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(85, 30);
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
            this.textBox2.Location = new System.Drawing.Point(837, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(45, 34);
            this.textBox2.TabIndex = 106;
            this.textBox2.Text = "～";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTimeP_end_date
            // 
            this.dateTimeP_end_date.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_end_date.Location = new System.Drawing.Point(888, 22);
            this.dateTimeP_end_date.Name = "dateTimeP_end_date";
            this.dateTimeP_end_date.Size = new System.Drawing.Size(159, 29);
            this.dateTimeP_end_date.TabIndex = 6;
            // 
            // dateTimeP_putin_date
            // 
            this.dateTimeP_putin_date.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_putin_date.Location = new System.Drawing.Point(672, 22);
            this.dateTimeP_putin_date.Name = "dateTimeP_putin_date";
            this.dateTimeP_putin_date.Size = new System.Drawing.Size(159, 29);
            this.dateTimeP_putin_date.TabIndex = 5;
            // 
            // btn_Select
            // 
            this.btn_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Select.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Select.Location = new System.Drawing.Point(962, 65);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(85, 30);
            this.btn_Select.TabIndex = 7;
            this.btn_Select.Text = "搜索";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // lab_art_number
            // 
            this.lab_art_number.AutoSize = true;
            this.lab_art_number.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_art_number.Location = new System.Drawing.Point(32, 24);
            this.lab_art_number.Name = "lab_art_number";
            this.lab_art_number.Size = new System.Drawing.Size(69, 25);
            this.lab_art_number.TabIndex = 92;
            this.lab_art_number.Text = "品号：";
            // 
            // txt_PARENT_ITEM_NO
            // 
            this.txt_PARENT_ITEM_NO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_PARENT_ITEM_NO.Location = new System.Drawing.Point(342, 63);
            this.txt_PARENT_ITEM_NO.Name = "txt_PARENT_ITEM_NO";
            this.txt_PARENT_ITEM_NO.Size = new System.Drawing.Size(141, 33);
            this.txt_PARENT_ITEM_NO.TabIndex = 4;
            // 
            // txt_ITEM_NO
            // 
            this.txt_ITEM_NO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_ITEM_NO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ITEM_NO.Location = new System.Drawing.Point(110, 21);
            this.txt_ITEM_NO.Name = "txt_ITEM_NO";
            this.txt_ITEM_NO.Size = new System.Drawing.Size(141, 33);
            this.txt_ITEM_NO.TabIndex = 1;
            // 
            // lab_art
            // 
            this.lab_art.AutoSize = true;
            this.lab_art.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_art.Location = new System.Drawing.Point(271, 67);
            this.lab_art.Name = "lab_art";
            this.lab_art.Size = new System.Drawing.Size(67, 25);
            this.lab_art.TabIndex = 100;
            this.lab_art.Text = "ART：";
            // 
            // lab_art_name
            // 
            this.lab_art_name.AutoSize = true;
            this.lab_art_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_art_name.Location = new System.Drawing.Point(267, 24);
            this.lab_art_name.Name = "lab_art_name";
            this.lab_art_name.Size = new System.Drawing.Size(69, 25);
            this.lab_art_name.TabIndex = 97;
            this.lab_art_name.Text = "品名：";
            // 
            // lab_supplier
            // 
            this.lab_supplier.AutoSize = true;
            this.lab_supplier.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_supplier.Location = new System.Drawing.Point(15, 67);
            this.lab_supplier.Name = "lab_supplier";
            this.lab_supplier.Size = new System.Drawing.Size(88, 25);
            this.lab_supplier.TabIndex = 99;
            this.lab_supplier.Text = "供应商：";
            // 
            // lab_Inbound_date
            // 
            this.lab_Inbound_date.AutoSize = true;
            this.lab_Inbound_date.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Inbound_date.Location = new System.Drawing.Point(563, 24);
            this.lab_Inbound_date.Name = "lab_Inbound_date";
            this.lab_Inbound_date.Size = new System.Drawing.Size(107, 25);
            this.lab_Inbound_date.TabIndex = 94;
            this.lab_Inbound_date.Text = "入库日期：";
            // 
            // txt_NAME_S
            // 
            this.txt_NAME_S.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_NAME_S.Location = new System.Drawing.Point(342, 21);
            this.txt_NAME_S.Name = "txt_NAME_S";
            this.txt_NAME_S.Size = new System.Drawing.Size(141, 33);
            this.txt_NAME_S.TabIndex = 2;
            // 
            // txt_SUPPLIERS_NAME
            // 
            this.txt_SUPPLIERS_NAME.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SUPPLIERS_NAME.Location = new System.Drawing.Point(110, 63);
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
            this.splitContainer2.Size = new System.Drawing.Size(1111, 448);
            this.splitContainer2.SplitterDistance = 380;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridView1.ColumnHeadersHeight = 33;
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
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle31;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1111, 380);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(443, 8);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(662, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.Frozen = true;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem31.Image = null;
            dataGridViewOperationItem31.Name = "UPDATE";
            dataGridViewOperationItem31.Text = "UPDATE";
            dataGridViewOperationItem32.Image = null;
            dataGridViewOperationItem32.Name = "DELETE";
            dataGridViewOperationItem32.Text = "DELETE";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem31);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem32);
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
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem29.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem29.Name = "UPDATE";
            dataGridViewOperationItem29.Text = "UPDATE";
            dataGridViewOperationItem30.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem30.Name = "DELETE";
            dataGridViewOperationItem30.Text = "DELETE";
            this.operation.Items.Add(dataGridViewOperationItem29);
            this.operation.Items.Add(dataGridViewOperationItem30);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.MinimumWidth = 90;
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.operation.Width = 90;
            // 
            // item_no
            // 
            this.item_no.HeaderText = "品号";
            this.item_no.Name = "item_no";
            this.item_no.ReadOnly = true;
            this.item_no.Width = 67;
            // 
            // name_s
            // 
            this.name_s.HeaderText = "品名";
            this.name_s.Name = "name_s";
            this.name_s.ReadOnly = true;
            this.name_s.Width = 67;
            // 
            // location_name
            // 
            this.location_name.HeaderText = "存放位置";
            this.location_name.Name = "location_name";
            this.location_name.ReadOnly = true;
            this.location_name.Width = 99;
            // 
            // suppliers_name
            // 
            this.suppliers_name.HeaderText = "供应商";
            this.suppliers_name.Name = "suppliers_name";
            this.suppliers_name.ReadOnly = true;
            this.suppliers_name.Width = 83;
            // 
            // parent_item_no
            // 
            this.parent_item_no.HeaderText = "相关ART";
            this.parent_item_no.Name = "parent_item_no";
            this.parent_item_no.ReadOnly = true;
            this.parent_item_no.Width = 97;
            // 
            // putin_date
            // 
            this.putin_date.HeaderText = "入库日期";
            this.putin_date.Name = "putin_date";
            this.putin_date.ReadOnly = true;
            this.putin_date.Width = 99;
            // 
            // end_date
            // 
            this.end_date.HeaderText = "预计到期日期";
            this.end_date.Name = "end_date";
            this.end_date.ReadOnly = true;
            this.end_date.Width = 131;
            // 
            // F_BDM_FittingsampleLocation_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 644);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_BDM_FittingsampleLocation_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试穿部样品存放管理";
            this.Load += new System.EventHandler(this.F_BDM_FittingsampleLocation_Main_Load);
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
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker dateTimeP_end_date;
        private System.Windows.Forms.DateTimePicker dateTimeP_putin_date;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Label lab_art_number;
        private System.Windows.Forms.TextBox txt_PARENT_ITEM_NO;
        private System.Windows.Forms.TextBox txt_ITEM_NO;
        private System.Windows.Forms.Label lab_art;
        private System.Windows.Forms.Label lab_art_name;
        private System.Windows.Forms.Label lab_supplier;
        private System.Windows.Forms.Label lab_Inbound_date;
        private System.Windows.Forms.TextBox txt_NAME_S;
        private System.Windows.Forms.TextBox txt_SUPPLIERS_NAME;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
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