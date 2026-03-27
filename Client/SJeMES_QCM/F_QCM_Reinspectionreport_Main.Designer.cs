
namespace SJeMES_QCM
{
    partial class F_QCM_Reinspectionreport_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem5 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem6 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Reinspectionreport_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_modo = new System.Windows.Forms.Button();
            this.btn_check = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_exadd = new System.Windows.Forms.Button();
            this.btn_ex = new System.Windows.Forms.Button();
            this.btn_Select = new System.Windows.Forms.Button();
            this.txt_PROD_NO = new System.Windows.Forms.TextBox();
            this.txt_PO_ORDER = new System.Windows.Forms.TextBox();
            this.txt_OUTSOURCING_INSPECTION_NO = new System.Windows.Forms.TextBox();
            this.lab_Art = new System.Windows.Forms.Label();
            this.lab_order = new System.Windows.Forms.Label();
            this.lab_detection_no = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTSOURCING_INSPECTION_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUPPLIERS_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUPPLIERS_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUPPLIERS_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PO_ORDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROD_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WH_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPOT_CHECK_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAD_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BAD_RATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOT_ACCEPT_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHOE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACCEPT_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GENERAL_TESTTYPE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CATEGORY_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_modo);
            this.splitContainer1.Panel1.Controls.Add(this.btn_check);
            this.splitContainer1.Panel1.Controls.Add(this.btn_add);
            this.splitContainer1.Panel1.Controls.Add(this.btn_exadd);
            this.splitContainer1.Panel1.Controls.Add(this.btn_ex);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Select);
            this.splitContainer1.Panel1.Controls.Add(this.txt_PROD_NO);
            this.splitContainer1.Panel1.Controls.Add(this.txt_PO_ORDER);
            this.splitContainer1.Panel1.Controls.Add(this.txt_OUTSOURCING_INSPECTION_NO);
            this.splitContainer1.Panel1.Controls.Add(this.lab_Art);
            this.splitContainer1.Panel1.Controls.Add(this.lab_order);
            this.splitContainer1.Panel1.Controls.Add(this.lab_detection_no);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1121, 603);
            this.splitContainer1.SplitterDistance = 101;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_modo
            // 
            this.btn_modo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_modo.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_modo.Location = new System.Drawing.Point(233, 59);
            this.btn_modo.Name = "btn_modo";
            this.btn_modo.Size = new System.Drawing.Size(90, 32);
            this.btn_modo.TabIndex = 122;
            this.btn_modo.Text = "模板下载";
            this.btn_modo.UseVisualStyleBackColor = true;
            this.btn_modo.Click += new System.EventHandler(this.btn_modo_Click);
            // 
            // btn_check
            // 
            this.btn_check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_check.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_check.Location = new System.Drawing.Point(425, 59);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(90, 32);
            this.btn_check.TabIndex = 117;
            this.btn_check.Text = "审核";
            this.btn_check.UseVisualStyleBackColor = true;
            // 
            // btn_add
            // 
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_add.Location = new System.Drawing.Point(329, 59);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(90, 32);
            this.btn_add.TabIndex = 116;
            this.btn_add.Text = "录入";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_exadd
            // 
            this.btn_exadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exadd.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_exadd.Location = new System.Drawing.Point(137, 59);
            this.btn_exadd.Name = "btn_exadd";
            this.btn_exadd.Size = new System.Drawing.Size(90, 32);
            this.btn_exadd.TabIndex = 115;
            this.btn_exadd.Text = "EXCEL导入";
            this.btn_exadd.UseVisualStyleBackColor = true;
            this.btn_exadd.Click += new System.EventHandler(this.btn_exadd_Click);
            // 
            // btn_ex
            // 
            this.btn_ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ex.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_ex.Location = new System.Drawing.Point(41, 59);
            this.btn_ex.Name = "btn_ex";
            this.btn_ex.Size = new System.Drawing.Size(90, 32);
            this.btn_ex.TabIndex = 114;
            this.btn_ex.Text = "EXCEL导出";
            this.btn_ex.UseVisualStyleBackColor = true;
            this.btn_ex.Click += new System.EventHandler(this.btn_ex_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Select.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Select.Location = new System.Drawing.Point(717, 49);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(75, 32);
            this.btn_Select.TabIndex = 113;
            this.btn_Select.Text = "搜索";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // txt_PROD_NO
            // 
            this.txt_PROD_NO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_PROD_NO.Location = new System.Drawing.Point(642, 10);
            this.txt_PROD_NO.Name = "txt_PROD_NO";
            this.txt_PROD_NO.Size = new System.Drawing.Size(151, 33);
            this.txt_PROD_NO.TabIndex = 112;
            // 
            // txt_PO_ORDER
            // 
            this.txt_PO_ORDER.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_PO_ORDER.Location = new System.Drawing.Point(398, 11);
            this.txt_PO_ORDER.Name = "txt_PO_ORDER";
            this.txt_PO_ORDER.Size = new System.Drawing.Size(151, 33);
            this.txt_PO_ORDER.TabIndex = 111;
            // 
            // txt_OUTSOURCING_INSPECTION_NO
            // 
            this.txt_OUTSOURCING_INSPECTION_NO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_OUTSOURCING_INSPECTION_NO.Location = new System.Drawing.Point(146, 10);
            this.txt_OUTSOURCING_INSPECTION_NO.Name = "txt_OUTSOURCING_INSPECTION_NO";
            this.txt_OUTSOURCING_INSPECTION_NO.Size = new System.Drawing.Size(151, 33);
            this.txt_OUTSOURCING_INSPECTION_NO.TabIndex = 110;
            // 
            // lab_Art
            // 
            this.lab_Art.AutoSize = true;
            this.lab_Art.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Art.Location = new System.Drawing.Point(572, 13);
            this.lab_Art.Name = "lab_Art";
            this.lab_Art.Size = new System.Drawing.Size(67, 25);
            this.lab_Art.TabIndex = 121;
            this.lab_Art.Text = "ART：";
            // 
            // lab_order
            // 
            this.lab_order.AutoSize = true;
            this.lab_order.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_order.Location = new System.Drawing.Point(308, 15);
            this.lab_order.Name = "lab_order";
            this.lab_order.Size = new System.Drawing.Size(88, 25);
            this.lab_order.TabIndex = 120;
            this.lab_order.Text = "制令号：";
            // 
            // lab_detection_no
            // 
            this.lab_detection_no.AutoSize = true;
            this.lab_detection_no.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_detection_no.Location = new System.Drawing.Point(38, 15);
            this.lab_detection_no.Name = "lab_detection_no";
            this.lab_detection_no.Size = new System.Drawing.Size(107, 25);
            this.lab_detection_no.TabIndex = 119;
            this.lab_detection_no.Text = "检验编号：";
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
            this.splitContainer2.Size = new System.Drawing.Size(1121, 498);
            this.splitContainer2.SplitterDistance = 433;
            this.splitContainer2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.ID,
            this.GUID,
            this.OUTSOURCING_INSPECTION_NO,
            this.SUPPLIERS_CODE,
            this.SUPPLIERS_NAME,
            this.SUPPLIERS_TYPE,
            this.PO_ORDER,
            this.PROD_NO,
            this.WH_QTY,
            this.SPOT_CHECK_QTY,
            this.BAD_QTY,
            this.BAD_RATE,
            this.NOT_ACCEPT_QTY,
            this.SHOE_NO,
            this.ACCEPT_QTY,
            this.GENERAL_TESTTYPE_NO,
            this.CATEGORY_NO});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1121, 433);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(392, 6);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem4.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem4.Name = "DETAIL";
            dataGridViewOperationItem4.Text = "DETAIL";
            dataGridViewOperationItem5.Image = global::SJeMES_QCM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem5.Name = "UPDATE";
            dataGridViewOperationItem5.Text = "UPDATE";
            dataGridViewOperationItem6.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem6.Name = "DELETE";
            dataGridViewOperationItem6.Text = "DELETE";
            this.operation.Items.Add(dataGridViewOperationItem4);
            this.operation.Items.Add(dataGridViewOperationItem5);
            this.operation.Items.Add(dataGridViewOperationItem6);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // GUID
            // 
            this.GUID.HeaderText = "关联键";
            this.GUID.Name = "GUID";
            this.GUID.Visible = false;
            // 
            // OUTSOURCING_INSPECTION_NO
            // 
            this.OUTSOURCING_INSPECTION_NO.HeaderText = "检验编号";
            this.OUTSOURCING_INSPECTION_NO.Name = "OUTSOURCING_INSPECTION_NO";
            // 
            // SUPPLIERS_CODE
            // 
            this.SUPPLIERS_CODE.HeaderText = "厂商代号";
            this.SUPPLIERS_CODE.Name = "SUPPLIERS_CODE";
            // 
            // SUPPLIERS_NAME
            // 
            this.SUPPLIERS_NAME.HeaderText = "厂商名称";
            this.SUPPLIERS_NAME.Name = "SUPPLIERS_NAME";
            // 
            // SUPPLIERS_TYPE
            // 
            this.SUPPLIERS_TYPE.HeaderText = "厂商类型";
            this.SUPPLIERS_TYPE.Name = "SUPPLIERS_TYPE";
            // 
            // PO_ORDER
            // 
            this.PO_ORDER.HeaderText = "制令号/模号";
            this.PO_ORDER.Name = "PO_ORDER";
            // 
            // PROD_NO
            // 
            this.PROD_NO.HeaderText = "ART";
            this.PROD_NO.Name = "PROD_NO";
            // 
            // WH_QTY
            // 
            this.WH_QTY.HeaderText = "进仓数";
            this.WH_QTY.Name = "WH_QTY";
            // 
            // SPOT_CHECK_QTY
            // 
            this.SPOT_CHECK_QTY.HeaderText = "抽检数量";
            this.SPOT_CHECK_QTY.Name = "SPOT_CHECK_QTY";
            // 
            // BAD_QTY
            // 
            this.BAD_QTY.HeaderText = "不良数";
            this.BAD_QTY.Name = "BAD_QTY";
            // 
            // BAD_RATE
            // 
            this.BAD_RATE.HeaderText = "不良率";
            this.BAD_RATE.Name = "BAD_RATE";
            // 
            // NOT_ACCEPT_QTY
            // 
            this.NOT_ACCEPT_QTY.HeaderText = "不接受数量";
            this.NOT_ACCEPT_QTY.Name = "NOT_ACCEPT_QTY";
            // 
            // SHOE_NO
            // 
            this.SHOE_NO.HeaderText = "鞋型";
            this.SHOE_NO.Name = "SHOE_NO";
            // 
            // ACCEPT_QTY
            // 
            this.ACCEPT_QTY.HeaderText = "接受数量";
            this.ACCEPT_QTY.Name = "ACCEPT_QTY";
            // 
            // GENERAL_TESTTYPE_NO
            // 
            this.GENERAL_TESTTYPE_NO.HeaderText = "通用检验类型代号";
            this.GENERAL_TESTTYPE_NO.Name = "GENERAL_TESTTYPE_NO";
            // 
            // CATEGORY_NO
            // 
            this.CATEGORY_NO.HeaderText = "检验类别";
            this.CATEGORY_NO.Name = "CATEGORY_NO";
            // 
            // F_QCM_Reinspectionreport_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 666);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Reinspectionreport_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重检报告";
            this.Load += new System.EventHandler(this.F_QCM_Reinspectionreport_Main_Load);
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
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.Button btn_check;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_exadd;
        private System.Windows.Forms.Button btn_ex;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.TextBox txt_PROD_NO;
        private System.Windows.Forms.TextBox txt_PO_ORDER;
        private System.Windows.Forms.TextBox txt_OUTSOURCING_INSPECTION_NO;
        private System.Windows.Forms.Label lab_Art;
        private System.Windows.Forms.Label lab_order;
        private System.Windows.Forms.Label lab_detection_no;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_modo;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OUTSOURCING_INSPECTION_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLIERS_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLIERS_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLIERS_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PO_ORDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROD_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn WH_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPOT_CHECK_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAD_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn BAD_RATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOT_ACCEPT_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHOE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACCEPT_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn GENERAL_TESTTYPE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CATEGORY_NO;
    }
}