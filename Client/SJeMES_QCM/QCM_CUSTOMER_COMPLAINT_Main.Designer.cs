
namespace SJeMES_QCM
{
    partial class QCM_CUSTOMER_COMPLAINT_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QCM_CUSTOMER_COMPLAINT_Main));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.SPOTCHECK_DATE_END = new System.Windows.Forms.DateTimePicker();
            this.SPOTCHECK_DATE_START = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.addbtn = new System.Windows.Forms.Button();
            this.Importbtn = new System.Windows.Forms.Button();
            this.Searchbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txt_Po = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Excelbtn = new System.Windows.Forms.Button();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.SPOTCHECK_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INSPECT_METHOD = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer2.Size = new System.Drawing.Size(937, 439);
            this.splitContainer2.SplitterDistance = 368;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.SPOTCHECK_NO,
            this.INSPECT_METHOD,
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
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(937, 368);
            this.dataGridView1.TabIndex = 0;
            // 
            // pageControl1
            // 
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(306, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(628, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // SPOTCHECK_DATE_END
            // 
            this.SPOTCHECK_DATE_END.CustomFormat = "yyyy-MM-dd";
            this.SPOTCHECK_DATE_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SPOTCHECK_DATE_END.Location = new System.Drawing.Point(212, 28);
            this.SPOTCHECK_DATE_END.Name = "SPOTCHECK_DATE_END";
            this.SPOTCHECK_DATE_END.Size = new System.Drawing.Size(113, 21);
            this.SPOTCHECK_DATE_END.TabIndex = 8;
            // 
            // SPOTCHECK_DATE_START
            // 
            this.SPOTCHECK_DATE_START.CustomFormat = "yyyy-MM-dd";
            this.SPOTCHECK_DATE_START.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SPOTCHECK_DATE_START.Location = new System.Drawing.Point(72, 28);
            this.SPOTCHECK_DATE_START.Name = "SPOTCHECK_DATE_START";
            this.SPOTCHECK_DATE_START.Size = new System.Drawing.Size(112, 21);
            this.SPOTCHECK_DATE_START.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "-";
            // 
            // addbtn
            // 
            this.addbtn.Font = new System.Drawing.Font("宋体", 9F);
            this.addbtn.Location = new System.Drawing.Point(183, 76);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(68, 28);
            this.addbtn.TabIndex = 4;
            this.addbtn.Text = "录入";
            this.addbtn.UseVisualStyleBackColor = true;
            // 
            // Importbtn
            // 
            this.Importbtn.Font = new System.Drawing.Font("宋体", 9F);
            this.Importbtn.Location = new System.Drawing.Point(102, 76);
            this.Importbtn.Name = "Importbtn";
            this.Importbtn.Size = new System.Drawing.Size(68, 28);
            this.Importbtn.TabIndex = 3;
            this.Importbtn.Text = "导入";
            this.Importbtn.UseVisualStyleBackColor = true;
            // 
            // Searchbtn
            // 
            this.Searchbtn.Font = new System.Drawing.Font("宋体", 10F);
            this.Searchbtn.Location = new System.Drawing.Point(717, 25);
            this.Searchbtn.Name = "Searchbtn";
            this.Searchbtn.Size = new System.Drawing.Size(81, 29);
            this.Searchbtn.TabIndex = 2;
            this.Searchbtn.Text = "搜索";
            this.Searchbtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "日期：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(-2, 32);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_Po);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.Excelbtn);
            this.splitContainer1.Panel1.Controls.Add(this.SPOTCHECK_DATE_END);
            this.splitContainer1.Panel1.Controls.Add(this.SPOTCHECK_DATE_START);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.addbtn);
            this.splitContainer1.Panel1.Controls.Add(this.Importbtn);
            this.splitContainer1.Panel1.Controls.Add(this.Searchbtn);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(937, 556);
            this.splitContainer1.SplitterDistance = 113;
            this.splitContainer1.TabIndex = 1;
            // 
            // txt_Po
            // 
            this.txt_Po.Location = new System.Drawing.Point(396, 29);
            this.txt_Po.Name = "txt_Po";
            this.txt_Po.Size = new System.Drawing.Size(100, 21);
            this.txt_Po.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(352, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "PO：";
            // 
            // Excelbtn
            // 
            this.Excelbtn.Font = new System.Drawing.Font("宋体", 9F);
            this.Excelbtn.Location = new System.Drawing.Point(25, 76);
            this.Excelbtn.Name = "Excelbtn";
            this.Excelbtn.Size = new System.Drawing.Size(68, 28);
            this.Excelbtn.TabIndex = 9;
            this.Excelbtn.Text = "EXCEL";
            this.Excelbtn.UseVisualStyleBackColor = true;
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "selectbtn";
            dataGridViewOperationItem1.Text = "查看";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.operation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.operation.Width = 60;
            // 
            // SPOTCHECK_NO
            // 
            this.SPOTCHECK_NO.HeaderText = "检验单号";
            this.SPOTCHECK_NO.Name = "SPOTCHECK_NO";
            // 
            // INSPECT_METHOD
            // 
            this.INSPECT_METHOD.HeaderText = "检验方式";
            this.INSPECT_METHOD.Name = "INSPECT_METHOD";
            // 
            // VEND_NAME
            // 
            this.VEND_NAME.HeaderText = "厂商";
            this.VEND_NAME.Name = "VEND_NAME";
            // 
            // PART_NO
            // 
            this.PART_NO.HeaderText = "部件";
            this.PART_NO.Name = "PART_NO";
            // 
            // SHOE_NOS
            // 
            this.SHOE_NOS.HeaderText = "鞋型名称";
            this.SHOE_NOS.Name = "SHOE_NOS";
            // 
            // PROD_NO
            // 
            this.PROD_NO.HeaderText = "Article";
            this.PROD_NO.Name = "PROD_NO";
            // 
            // PO_ORDER
            // 
            this.PO_ORDER.HeaderText = "PO";
            this.PO_ORDER.Name = "PO_ORDER";
            // 
            // CODE_NUMBER
            // 
            this.CODE_NUMBER.HeaderText = "码数";
            this.CODE_NUMBER.Name = "CODE_NUMBER";
            // 
            // SPOTCHECK_DATE
            // 
            this.SPOTCHECK_DATE.HeaderText = "检验日期";
            this.SPOTCHECK_DATE.Name = "SPOTCHECK_DATE";
            // 
            // PO_QTY
            // 
            this.PO_QTY.HeaderText = "生产数量(双)";
            this.PO_QTY.Name = "PO_QTY";
            // 
            // PLANSAMP_QTY
            // 
            this.PLANSAMP_QTY.HeaderText = "抽检数(双)";
            this.PLANSAMP_QTY.Name = "PLANSAMP_QTY";
            // 
            // PROCESS_TYPE
            // 
            this.PROCESS_TYPE.HeaderText = "工艺类型";
            this.PROCESS_TYPE.Name = "PROCESS_TYPE";
            // 
            // NG_QTY
            // 
            this.NG_QTY.HeaderText = "总不良数(件)";
            this.NG_QTY.Name = "NG_QTY";
            // 
            // STATUS
            // 
            this.STATUS.HeaderText = "状态";
            this.STATUS.Name = "STATUS";
            // 
            // QCM_CUSTOMER_COMPLAINT_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 586);
            this.Controls.Add(this.splitContainer1);
            this.Name = "QCM_CUSTOMER_COMPLAINT_Main";
            this.Text = "客户投诉";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker SPOTCHECK_DATE_END;
        private System.Windows.Forms.DateTimePicker SPOTCHECK_DATE_START;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.Button Importbtn;
        private System.Windows.Forms.Button Searchbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txt_Po;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Excelbtn;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPOTCHECK_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn INSPECT_METHOD;
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