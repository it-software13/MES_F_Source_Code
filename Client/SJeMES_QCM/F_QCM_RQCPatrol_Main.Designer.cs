
namespace SJeMES_QCM
{
    partial class F_QCM_RQCPatrol_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_RQCPatrol_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnEntry = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnGit = new System.Windows.Forms.Button();
            this.txtart = new System.Windows.Forms.TextBox();
            this.txtProductionLine = new System.Windows.Forms.TextBox();
            this.txtVendor = new System.Windows.Forms.TextBox();
            this.labart = new System.Windows.Forms.Label();
            this.labProductionLine = new System.Windows.Forms.Label();
            this.labVendor = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.vendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.region = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Productionline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timequantum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codenumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.art = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shoes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Theoperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendorhead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnEntry);
            this.splitContainer1.Panel1.Controls.Add(this.btnImport);
            this.splitContainer1.Panel1.Controls.Add(this.btnGit);
            this.splitContainer1.Panel1.Controls.Add(this.txtart);
            this.splitContainer1.Panel1.Controls.Add(this.txtProductionLine);
            this.splitContainer1.Panel1.Controls.Add(this.txtVendor);
            this.splitContainer1.Panel1.Controls.Add(this.labart);
            this.splitContainer1.Panel1.Controls.Add(this.labProductionLine);
            this.splitContainer1.Panel1.Controls.Add(this.labVendor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1115, 536);
            this.splitContainer1.SplitterDistance = 77;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnEntry
            // 
            this.btnEntry.Location = new System.Drawing.Point(125, 42);
            this.btnEntry.Name = "btnEntry";
            this.btnEntry.Size = new System.Drawing.Size(81, 32);
            this.btnEntry.TabIndex = 8;
            this.btnEntry.Text = "录入";
            this.btnEntry.UseVisualStyleBackColor = true;
            this.btnEntry.Click += new System.EventHandler(this.btnEntry_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 42);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(81, 32);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // btnGit
            // 
            this.btnGit.Location = new System.Drawing.Point(726, 5);
            this.btnGit.Name = "btnGit";
            this.btnGit.Size = new System.Drawing.Size(81, 32);
            this.btnGit.TabIndex = 6;
            this.btnGit.Text = "搜索";
            this.btnGit.UseVisualStyleBackColor = true;
            this.btnGit.Click += new System.EventHandler(this.btnGit_Click);
            // 
            // txtart
            // 
            this.txtart.Location = new System.Drawing.Point(578, 8);
            this.txtart.Name = "txtart";
            this.txtart.Size = new System.Drawing.Size(100, 29);
            this.txtart.TabIndex = 5;
            // 
            // txtProductionLine
            // 
            this.txtProductionLine.Location = new System.Drawing.Point(329, 8);
            this.txtProductionLine.Name = "txtProductionLine";
            this.txtProductionLine.Size = new System.Drawing.Size(100, 29);
            this.txtProductionLine.TabIndex = 4;
            // 
            // txtVendor
            // 
            this.txtVendor.Location = new System.Drawing.Point(73, 8);
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.Size = new System.Drawing.Size(100, 29);
            this.txtVendor.TabIndex = 3;
            // 
            // labart
            // 
            this.labart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labart.AutoSize = true;
            this.labart.Location = new System.Drawing.Point(517, 11);
            this.labart.Name = "labart";
            this.labart.Size = new System.Drawing.Size(40, 21);
            this.labart.TabIndex = 2;
            this.labart.Text = "ART";
            // 
            // labProductionLine
            // 
            this.labProductionLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labProductionLine.AutoSize = true;
            this.labProductionLine.Location = new System.Drawing.Point(268, 11);
            this.labProductionLine.Name = "labProductionLine";
            this.labProductionLine.Size = new System.Drawing.Size(42, 21);
            this.labProductionLine.TabIndex = 1;
            this.labProductionLine.Text = "产线";
            // 
            // labVendor
            // 
            this.labVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labVendor.AutoSize = true;
            this.labVendor.Location = new System.Drawing.Point(12, 11);
            this.labVendor.Name = "labVendor";
            this.labVendor.Size = new System.Drawing.Size(42, 21);
            this.labVendor.TabIndex = 0;
            this.labVendor.Text = "厂商";
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
            this.splitContainer2.Size = new System.Drawing.Size(1115, 455);
            this.splitContainer2.SplitterDistance = 393;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.vendor,
            this.inspection_no,
            this.inspection_type,
            this.date,
            this.region,
            this.Productionline,
            this.machine,
            this.timequantum,
            this.order,
            this.Codenumber,
            this.art,
            this.shoes,
            this.parts,
            this.Theoperator,
            this.vendorhead,
            this.QIP,
            this.state});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 33;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1115, 393);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(406, 4);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(706, 58);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem4.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem4.Name = "SELECT";
            dataGridViewOperationItem4.Text = "查看";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem3.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem3.Name = "SELECT";
            dataGridViewOperationItem3.Text = "查看";
            this.operation.Items.Add(dataGridViewOperationItem3);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.operation.Width = 90;
            // 
            // vendor
            // 
            this.vendor.HeaderText = "厂商";
            this.vendor.Name = "vendor";
            this.vendor.ReadOnly = true;
            this.vendor.Width = 67;
            // 
            // inspection_no
            // 
            this.inspection_no.HeaderText = "检验单号";
            this.inspection_no.Name = "inspection_no";
            this.inspection_no.ReadOnly = true;
            this.inspection_no.Width = 99;
            // 
            // inspection_type
            // 
            this.inspection_type.HeaderText = "检验类型";
            this.inspection_type.Name = "inspection_type";
            this.inspection_type.ReadOnly = true;
            this.inspection_type.Width = 99;
            // 
            // date
            // 
            this.date.HeaderText = "日期";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 67;
            // 
            // region
            // 
            this.region.HeaderText = "地区";
            this.region.Name = "region";
            this.region.ReadOnly = true;
            this.region.Width = 67;
            // 
            // Productionline
            // 
            this.Productionline.HeaderText = "产线";
            this.Productionline.Name = "Productionline";
            this.Productionline.ReadOnly = true;
            this.Productionline.Width = 67;
            // 
            // machine
            // 
            this.machine.HeaderText = "机台";
            this.machine.Name = "machine";
            this.machine.ReadOnly = true;
            this.machine.Width = 67;
            // 
            // timequantum
            // 
            this.timequantum.HeaderText = "时间段";
            this.timequantum.Name = "timequantum";
            this.timequantum.ReadOnly = true;
            this.timequantum.Width = 83;
            // 
            // order
            // 
            this.order.HeaderText = "制令";
            this.order.Name = "order";
            this.order.ReadOnly = true;
            this.order.Width = 67;
            // 
            // Codenumber
            // 
            this.Codenumber.HeaderText = "码数";
            this.Codenumber.Name = "Codenumber";
            this.Codenumber.ReadOnly = true;
            this.Codenumber.Width = 67;
            // 
            // art
            // 
            this.art.HeaderText = "ART";
            this.art.Name = "art";
            this.art.ReadOnly = true;
            this.art.Width = 65;
            // 
            // shoes
            // 
            this.shoes.HeaderText = "鞋型";
            this.shoes.Name = "shoes";
            this.shoes.ReadOnly = true;
            this.shoes.Width = 67;
            // 
            // parts
            // 
            this.parts.HeaderText = "部件";
            this.parts.Name = "parts";
            this.parts.ReadOnly = true;
            this.parts.Width = 67;
            // 
            // Theoperator
            // 
            this.Theoperator.HeaderText = "操作员";
            this.Theoperator.Name = "Theoperator";
            this.Theoperator.ReadOnly = true;
            this.Theoperator.Width = 83;
            // 
            // vendorhead
            // 
            this.vendorhead.HeaderText = "厂商负责人";
            this.vendorhead.Name = "vendorhead";
            this.vendorhead.ReadOnly = true;
            this.vendorhead.Width = 115;
            // 
            // QIP
            // 
            this.QIP.HeaderText = "QIP确认";
            this.QIP.Name = "QIP";
            this.QIP.ReadOnly = true;
            this.QIP.Width = 95;
            // 
            // state
            // 
            this.state.HeaderText = "状态";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Width = 67;
            // 
            // F_QCM_RQCPatrol_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 602);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_RQCPatrol_Main";
            this.Text = "巡线";
            this.Load += new System.EventHandler(this.F_QCM_RQCPatrol_Main_Load);
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
        private System.Windows.Forms.Button btnGit;
        private System.Windows.Forms.TextBox txtart;
        private System.Windows.Forms.TextBox txtProductionLine;
        private System.Windows.Forms.TextBox txtVendor;
        private System.Windows.Forms.Label labart;
        private System.Windows.Forms.Label labProductionLine;
        private System.Windows.Forms.Label labVendor;
        private System.Windows.Forms.Button btnEntry;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn region;
        private System.Windows.Forms.DataGridViewTextBoxColumn Productionline;
        private System.Windows.Forms.DataGridViewTextBoxColumn machine;
        private System.Windows.Forms.DataGridViewTextBoxColumn timequantum;
        private System.Windows.Forms.DataGridViewTextBoxColumn order;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codenumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn art;
        private System.Windows.Forms.DataGridViewTextBoxColumn shoes;
        private System.Windows.Forms.DataGridViewTextBoxColumn parts;
        private System.Windows.Forms.DataGridViewTextBoxColumn Theoperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendorhead;
        private System.Windows.Forms.DataGridViewTextBoxColumn QIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
    }
}