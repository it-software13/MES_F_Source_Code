
namespace SJeMES_QCM
{
    partial class F_QCM_ComplianceManagement_Main
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_ComplianceManagement_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnedit = new System.Windows.Forms.Button();
            this.btnsc = new System.Windows.Forms.Button();
            this.btndc = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.date2 = new System.Windows.Forms.DateTimePicker();
            this.date1 = new System.Windows.Forms.DateTimePicker();
            this.comzt = new System.Windows.Forms.ComboBox();
            this.txtgys = new System.Windows.Forms.TextBox();
            this.txtpm = new System.Windows.Forms.TextBox();
            this.txtph = new System.Windows.Forms.TextBox();
            this.labRQ = new System.Windows.Forms.Label();
            this.labZT = new System.Windows.Forms.Label();
            this.labGYS = new System.Windows.Forms.Label();
            this.labPM = new System.Windows.Forms.Label();
            this.labPH = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.Item_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnedit);
            this.splitContainer1.Panel1.Controls.Add(this.btnsc);
            this.splitContainer1.Panel1.Controls.Add(this.btndc);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelect);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.date2);
            this.splitContainer1.Panel1.Controls.Add(this.date1);
            this.splitContainer1.Panel1.Controls.Add(this.comzt);
            this.splitContainer1.Panel1.Controls.Add(this.txtgys);
            this.splitContainer1.Panel1.Controls.Add(this.txtpm);
            this.splitContainer1.Panel1.Controls.Add(this.txtph);
            this.splitContainer1.Panel1.Controls.Add(this.labRQ);
            this.splitContainer1.Panel1.Controls.Add(this.labGYS);
            this.splitContainer1.Panel1.Controls.Add(this.labPM);
            this.splitContainer1.Panel1.Controls.Add(this.labPH);
            this.splitContainer1.Panel1.Controls.Add(this.labZT);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1178, 569);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnedit
            // 
            this.btnedit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnedit.Location = new System.Drawing.Point(316, 106);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(109, 29);
            this.btnedit.TabIndex = 15;
            this.btnedit.Text = "编辑";
            this.btnedit.UseVisualStyleBackColor = true;
            this.btnedit.Visible = false;
            // 
            // btnsc
            // 
            this.btnsc.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnsc.Location = new System.Drawing.Point(187, 106);
            this.btnsc.Name = "btnsc";
            this.btnsc.Size = new System.Drawing.Size(109, 29);
            this.btnsc.TabIndex = 14;
            this.btnsc.Text = "上传数据";
            this.btnsc.UseVisualStyleBackColor = true;
            this.btnsc.Click += new System.EventHandler(this.btnsc_Click);
            // 
            // btndc
            // 
            this.btndc.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btndc.Location = new System.Drawing.Point(40, 107);
            this.btndc.Name = "btndc";
            this.btndc.Size = new System.Drawing.Size(129, 29);
            this.btndc.TabIndex = 13;
            this.btndc.Text = "导出品号模板";
            this.btndc.UseVisualStyleBackColor = true;
            this.btndc.Click += new System.EventHandler(this.btndc_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Location = new System.Drawing.Point(495, 62);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(92, 29);
            this.btnSelect.TabIndex = 12;
            this.btnSelect.Text = "搜索";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(273, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "~";
            // 
            // date2
            // 
            this.date2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.date2.Location = new System.Drawing.Point(301, 62);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(145, 29);
            this.date2.TabIndex = 10;
            // 
            // date1
            // 
            this.date1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.date1.Location = new System.Drawing.Point(123, 60);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(144, 29);
            this.date1.TabIndex = 9;
            // 
            // comzt
            // 
            this.comzt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comzt.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comzt.FormattingEnabled = true;
            this.comzt.Location = new System.Drawing.Point(945, 16);
            this.comzt.Name = "comzt";
            this.comzt.Size = new System.Drawing.Size(106, 29);
            this.comzt.TabIndex = 8;
            // 
            // txtgys
            // 
            this.txtgys.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtgys.Location = new System.Drawing.Point(666, 19);
            this.txtgys.Name = "txtgys";
            this.txtgys.Size = new System.Drawing.Size(129, 29);
            this.txtgys.TabIndex = 7;
            // 
            // txtpm
            // 
            this.txtpm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtpm.Location = new System.Drawing.Point(405, 20);
            this.txtpm.Name = "txtpm";
            this.txtpm.Size = new System.Drawing.Size(129, 29);
            this.txtpm.TabIndex = 6;
            // 
            // txtph
            // 
            this.txtph.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtph.Location = new System.Drawing.Point(124, 19);
            this.txtph.Name = "txtph";
            this.txtph.Size = new System.Drawing.Size(129, 29);
            this.txtph.TabIndex = 5;
            // 
            // labRQ
            // 
            this.labRQ.AutoSize = true;
            this.labRQ.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRQ.Location = new System.Drawing.Point(35, 66);
            this.labRQ.Name = "labRQ";
            this.labRQ.Size = new System.Drawing.Size(74, 21);
            this.labRQ.TabIndex = 4;
            this.labRQ.Text = "到期日期";
            // 
            // labZT
            // 
            this.labZT.AutoSize = true;
            this.labZT.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labZT.Location = new System.Drawing.Point(851, 22);
            this.labZT.Name = "labZT";
            this.labZT.Size = new System.Drawing.Size(78, 21);
            this.labZT.TabIndex = 3;
            this.labZT.Text = "A-01状态";
            // 
            // labGYS
            // 
            this.labGYS.AutoSize = true;
            this.labGYS.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labGYS.Location = new System.Drawing.Point(602, 23);
            this.labGYS.Name = "labGYS";
            this.labGYS.Size = new System.Drawing.Size(58, 21);
            this.labGYS.TabIndex = 2;
            this.labGYS.Text = "供应商";
            // 
            // labPM
            // 
            this.labPM.AutoSize = true;
            this.labPM.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPM.Location = new System.Drawing.Point(346, 24);
            this.labPM.Name = "labPM";
            this.labPM.Size = new System.Drawing.Size(42, 21);
            this.labPM.TabIndex = 1;
            this.labPM.Text = "品名";
            // 
            // labPH
            // 
            this.labPH.AutoSize = true;
            this.labPH.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPH.Location = new System.Drawing.Point(67, 23);
            this.labPH.Name = "labPH";
            this.labPH.Size = new System.Drawing.Size(42, 21);
            this.labPH.TabIndex = 0;
            this.labPH.Text = "品号";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer2.Size = new System.Drawing.Size(1178, 417);
            this.splitContainer2.SplitterDistance = 357;
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
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.Item_no,
            this.item_name,
            this.supplier,
            this.state,
            this.start_date,
            this.end_date});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 33;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1178, 357);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(481, 7);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(694, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "selectImg";
            dataGridViewOperationItem1.Text = "查看";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_upload_img_24;
            dataGridViewOperationItem2.Name = "UploadIMG";
            dataGridViewOperationItem2.Text = "上传";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Item_no
            // 
            this.Item_no.HeaderText = "品号";
            this.Item_no.Name = "Item_no";
            this.Item_no.ReadOnly = true;
            this.Item_no.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Item_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Item_no.Width = 48;
            // 
            // item_name
            // 
            this.item_name.HeaderText = "品名";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            this.item_name.Width = 67;
            // 
            // supplier
            // 
            this.supplier.HeaderText = "供应商";
            this.supplier.Name = "supplier";
            this.supplier.ReadOnly = true;
            this.supplier.Width = 83;
            // 
            // state
            // 
            this.state.HeaderText = "A-01状态";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Width = 103;
            // 
            // start_date
            // 
            this.start_date.HeaderText = "A-01起始时间";
            this.start_date.Name = "start_date";
            this.start_date.ReadOnly = true;
            this.start_date.Width = 135;
            // 
            // end_date
            // 
            this.end_date.HeaderText = "A-01到期时间";
            this.end_date.Name = "end_date";
            this.end_date.ReadOnly = true;
            this.end_date.Width = 135;
            // 
            // F_QCM_ComplianceManagement_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 632);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_ComplianceManagement_Main";
            this.Text = "A-01合规管理";
            this.Load += new System.EventHandler(this.F_QCM_ComplianceManagement_Main_Load);
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
        private System.Windows.Forms.Button btnedit;
        private System.Windows.Forms.Button btnsc;
        private System.Windows.Forms.Button btndc;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker date2;
        private System.Windows.Forms.DateTimePicker date1;
        private System.Windows.Forms.ComboBox comzt;
        private System.Windows.Forms.TextBox txtgys;
        private System.Windows.Forms.TextBox txtpm;
        private System.Windows.Forms.TextBox txtph;
        private System.Windows.Forms.Label labRQ;
        private System.Windows.Forms.Label labZT;
        private System.Windows.Forms.Label labGYS;
        private System.Windows.Forms.Label labPM;
        private System.Windows.Forms.Label labPH;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn start_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn end_date;
    }
}