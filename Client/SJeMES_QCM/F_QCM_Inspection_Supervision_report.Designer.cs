
namespace SJeMES_QCM
{
    partial class F_QCM_Inspection_Supervision_report
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Inspection_Supervision_report));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Model = new System.Windows.Forms.Button();
            this.SPOTCHECK_DATE_END = new System.Windows.Forms.DateTimePicker();
            this.SPOTCHECK_DATE_START = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.lab_survey_date = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.SPOTCHECK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INSPECT_METHOD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VEND_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VEND_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHOE_NOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROD_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PO_ORDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODE_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPOTCHECK_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PO_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLANSAMP_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROCESS_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NG_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(-3, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_Model);
            this.splitContainer1.Panel1.Controls.Add(this.SPOTCHECK_DATE_END);
            this.splitContainer1.Panel1.Controls.Add(this.SPOTCHECK_DATE_START);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btn_add);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Import);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Search);
            this.splitContainer1.Panel1.Controls.Add(this.lab_survey_date);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(937, 522);
            this.splitContainer1.SplitterDistance = 107;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_Model
            // 
            this.btn_Model.Location = new System.Drawing.Point(125, 76);
            this.btn_Model.Name = "btn_Model";
            this.btn_Model.Size = new System.Drawing.Size(94, 28);
            this.btn_Model.TabIndex = 9;
            this.btn_Model.Text = "导入模板";
            this.btn_Model.UseVisualStyleBackColor = true;
            this.btn_Model.Click += new System.EventHandler(this.Modelbtn_Click);
            // 
            // SPOTCHECK_DATE_END
            // 
            this.SPOTCHECK_DATE_END.CustomFormat = "yyyy-MM-dd";
            this.SPOTCHECK_DATE_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SPOTCHECK_DATE_END.Location = new System.Drawing.Point(272, 28);
            this.SPOTCHECK_DATE_END.Name = "SPOTCHECK_DATE_END";
            this.SPOTCHECK_DATE_END.Size = new System.Drawing.Size(113, 21);
            this.SPOTCHECK_DATE_END.TabIndex = 8;
            // 
            // SPOTCHECK_DATE_START
            // 
            this.SPOTCHECK_DATE_START.CustomFormat = "yyyy-MM-dd";
            this.SPOTCHECK_DATE_START.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SPOTCHECK_DATE_START.Location = new System.Drawing.Point(127, 28);
            this.SPOTCHECK_DATE_START.Name = "SPOTCHECK_DATE_START";
            this.SPOTCHECK_DATE_START.Size = new System.Drawing.Size(112, 21);
            this.SPOTCHECK_DATE_START.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "-";
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_add.Location = new System.Drawing.Point(236, 76);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(68, 28);
            this.btn_add.TabIndex = 4;
            this.btn_add.Text = "录入";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_Import.Location = new System.Drawing.Point(44, 76);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(68, 28);
            this.btn_Import.TabIndex = 3;
            this.btn_Import.Text = "导入";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.Importbtn_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Search.Font = new System.Drawing.Font("宋体", 10F);
            this.btn_Search.Location = new System.Drawing.Point(442, 25);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(81, 29);
            this.btn_Search.TabIndex = 2;
            this.btn_Search.Text = "搜索";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.Searchbtn_Click);
            // 
            // lab_survey_date
            // 
            this.lab_survey_date.AutoSize = true;
            this.lab_survey_date.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_survey_date.Location = new System.Drawing.Point(33, 30);
            this.lab_survey_date.Name = "lab_survey_date";
            this.lab_survey_date.Size = new System.Drawing.Size(88, 16);
            this.lab_survey_date.TabIndex = 0;
            this.lab_survey_date.Text = "检验日期：";
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
            this.splitContainer2.Size = new System.Drawing.Size(937, 411);
            this.splitContainer2.SplitterDistance = 328;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.SPOTCHECK_NO,
            this.INSPECT_METHOD,
            this.VEND_NO,
            this.VEND_NAME,
            this.PART_NO,
            this.SHOE_NOS,
            this.PROD_NO,
            this.PO_ORDER,
            this.CODE_NUMBER,
            this.SPOTCHECK_DATE,
            this.PO_QTY,
            this.PLANSAMP_QTY,
            this.PROCESS_TYPE,
            this.NG_QTY,
            this.STATUS});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(937, 328);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(287, 23);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(628, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem2.Name = "selectbtn";
            dataGridViewOperationItem2.Text = "查看";
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.operation.Width = 90;
            // 
            // SPOTCHECK_NO
            // 
            this.SPOTCHECK_NO.HeaderText = "检验单号";
            this.SPOTCHECK_NO.Name = "SPOTCHECK_NO";
            this.SPOTCHECK_NO.ReadOnly = true;
            this.SPOTCHECK_NO.Width = 99;
            // 
            // INSPECT_METHOD
            // 
            this.INSPECT_METHOD.HeaderText = "检验方式";
            this.INSPECT_METHOD.Name = "INSPECT_METHOD";
            this.INSPECT_METHOD.ReadOnly = true;
            this.INSPECT_METHOD.Width = 99;
            // 
            // VEND_NO
            // 
            this.VEND_NO.HeaderText = "厂商代号";
            this.VEND_NO.Name = "VEND_NO";
            this.VEND_NO.ReadOnly = true;
            this.VEND_NO.Visible = false;
            this.VEND_NO.Width = 99;
            // 
            // VEND_NAME
            // 
            this.VEND_NAME.HeaderText = "厂商";
            this.VEND_NAME.Name = "VEND_NAME";
            this.VEND_NAME.ReadOnly = true;
            this.VEND_NAME.Width = 67;
            // 
            // PART_NO
            // 
            this.PART_NO.HeaderText = "部件";
            this.PART_NO.Name = "PART_NO";
            this.PART_NO.ReadOnly = true;
            this.PART_NO.Width = 67;
            // 
            // SHOE_NOS
            // 
            this.SHOE_NOS.HeaderText = "鞋型名称";
            this.SHOE_NOS.Name = "SHOE_NOS";
            this.SHOE_NOS.ReadOnly = true;
            this.SHOE_NOS.Width = 99;
            // 
            // PROD_NO
            // 
            this.PROD_NO.HeaderText = "Article";
            this.PROD_NO.Name = "PROD_NO";
            this.PROD_NO.ReadOnly = true;
            this.PROD_NO.Width = 83;
            // 
            // PO_ORDER
            // 
            this.PO_ORDER.HeaderText = "PO";
            this.PO_ORDER.Name = "PO_ORDER";
            this.PO_ORDER.ReadOnly = true;
            this.PO_ORDER.Width = 58;
            // 
            // CODE_NUMBER
            // 
            this.CODE_NUMBER.HeaderText = "码数";
            this.CODE_NUMBER.Name = "CODE_NUMBER";
            this.CODE_NUMBER.ReadOnly = true;
            this.CODE_NUMBER.Width = 67;
            // 
            // SPOTCHECK_DATE
            // 
            this.SPOTCHECK_DATE.HeaderText = "检验日期";
            this.SPOTCHECK_DATE.Name = "SPOTCHECK_DATE";
            this.SPOTCHECK_DATE.ReadOnly = true;
            this.SPOTCHECK_DATE.Width = 99;
            // 
            // PO_QTY
            // 
            this.PO_QTY.HeaderText = "生产数量(双)";
            this.PO_QTY.Name = "PO_QTY";
            this.PO_QTY.ReadOnly = true;
            this.PO_QTY.Width = 125;
            // 
            // PLANSAMP_QTY
            // 
            this.PLANSAMP_QTY.HeaderText = "抽检数(双)";
            this.PLANSAMP_QTY.Name = "PLANSAMP_QTY";
            this.PLANSAMP_QTY.ReadOnly = true;
            this.PLANSAMP_QTY.Width = 109;
            // 
            // PROCESS_TYPE
            // 
            this.PROCESS_TYPE.HeaderText = "工艺类型";
            this.PROCESS_TYPE.Name = "PROCESS_TYPE";
            this.PROCESS_TYPE.ReadOnly = true;
            this.PROCESS_TYPE.Width = 99;
            // 
            // NG_QTY
            // 
            this.NG_QTY.HeaderText = "总不良数(件)";
            this.NG_QTY.Name = "NG_QTY";
            this.NG_QTY.ReadOnly = true;
            this.NG_QTY.Width = 125;
            // 
            // STATUS
            // 
            this.STATUS.HeaderText = "状态";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Width = 67;
            // 
            // F_QCM_Inspection_Supervision_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 586);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Inspection_Supervision_report";
            this.Text = "抽检品质监督报表";
            this.Load += new System.EventHandler(this.F_QCM_Inspection_Supervision_report_Load);
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
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label lab_survey_date;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.DateTimePicker SPOTCHECK_DATE_END;
        private System.Windows.Forms.DateTimePicker SPOTCHECK_DATE_START;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Model;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPOTCHECK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn INSPECT_METHOD;
        private System.Windows.Forms.DataGridViewTextBoxColumn VEND_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VEND_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHOE_NOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROD_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PO_ORDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODE_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPOTCHECK_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PO_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLANSAMP_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROCESS_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NG_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
    }
}