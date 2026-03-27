
namespace SJeMES_QCM
{
    partial class F_QCM_ExternalColorCard_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_ExternalColorCard_Main));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.insertbtn = new System.Windows.Forms.Button();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.importbtn = new System.Windows.Forms.Button();
            this.Modelbtn = new System.Windows.Forms.Button();
            this.searchbtn = new System.Windows.Forms.Button();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_vend_name = new System.Windows.Forms.TextBox();
            this.txt_prod_no = new System.Windows.Forms.TextBox();
            this.lab_vend_name = new System.Windows.Forms.Label();
            this.lab_prod_no = new System.Windows.Forms.Label();
            this.lab_date = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.CARD_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VEND_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VEND_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIRSTARTICLE_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHOE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROD_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IS_QCCONFIRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEST_RESULT = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(-2, 62);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.insertbtn);
            this.splitContainer1.Panel1.Controls.Add(this.start_date);
            this.splitContainer1.Panel1.Controls.Add(this.importbtn);
            this.splitContainer1.Panel1.Controls.Add(this.Modelbtn);
            this.splitContainer1.Panel1.Controls.Add(this.searchbtn);
            this.splitContainer1.Panel1.Controls.Add(this.end_date);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txt_vend_name);
            this.splitContainer1.Panel1.Controls.Add(this.txt_prod_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_vend_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_prod_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_date);
            this.splitContainer1.Panel1.Click += new System.EventHandler(this.splitContainer1_Panel1_Click);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(934, 475);
            this.splitContainer1.SplitterDistance = 116;
            this.splitContainer1.TabIndex = 0;
            // 
            // insertbtn
            // 
            this.insertbtn.Location = new System.Drawing.Point(218, 79);
            this.insertbtn.Name = "insertbtn";
            this.insertbtn.Size = new System.Drawing.Size(75, 23);
            this.insertbtn.TabIndex = 13;
            this.insertbtn.Text = "录入";
            this.insertbtn.UseVisualStyleBackColor = true;
            this.insertbtn.Click += new System.EventHandler(this.insertbtn_Click);
            // 
            // start_date
            // 
            this.start_date.Location = new System.Drawing.Point(90, 31);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(120, 21);
            this.start_date.TabIndex = 12;
            // 
            // importbtn
            // 
            this.importbtn.Location = new System.Drawing.Point(122, 79);
            this.importbtn.Name = "importbtn";
            this.importbtn.Size = new System.Drawing.Size(75, 23);
            this.importbtn.TabIndex = 10;
            this.importbtn.Text = "导入";
            this.importbtn.UseVisualStyleBackColor = true;
            this.importbtn.Click += new System.EventHandler(this.importbtn_Click);
            // 
            // Modelbtn
            // 
            this.Modelbtn.Location = new System.Drawing.Point(32, 79);
            this.Modelbtn.Name = "Modelbtn";
            this.Modelbtn.Size = new System.Drawing.Size(75, 23);
            this.Modelbtn.TabIndex = 9;
            this.Modelbtn.Text = "导入模板";
            this.Modelbtn.UseVisualStyleBackColor = true;
            this.Modelbtn.Click += new System.EventHandler(this.Modelbtn_Click);
            // 
            // searchbtn
            // 
            this.searchbtn.Location = new System.Drawing.Point(721, 32);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(75, 23);
            this.searchbtn.TabIndex = 8;
            this.searchbtn.Text = "搜索";
            this.searchbtn.UseVisualStyleBackColor = true;
            this.searchbtn.Click += new System.EventHandler(this.searchbtn_Click);
            // 
            // end_date
            // 
            this.end_date.Location = new System.Drawing.Point(235, 32);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(120, 21);
            this.end_date.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(215, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "-";
            // 
            // txt_vend_name
            // 
            this.txt_vend_name.Location = new System.Drawing.Point(437, 33);
            this.txt_vend_name.Name = "txt_vend_name";
            this.txt_vend_name.Size = new System.Drawing.Size(100, 21);
            this.txt_vend_name.TabIndex = 5;
            // 
            // txt_prod_no
            // 
            this.txt_prod_no.Location = new System.Drawing.Point(599, 32);
            this.txt_prod_no.Name = "txt_prod_no";
            this.txt_prod_no.Size = new System.Drawing.Size(100, 21);
            this.txt_prod_no.TabIndex = 4;
            // 
            // lab_vend_name
            // 
            this.lab_vend_name.AutoSize = true;
            this.lab_vend_name.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_vend_name.Location = new System.Drawing.Point(373, 36);
            this.lab_vend_name.Name = "lab_vend_name";
            this.lab_vend_name.Size = new System.Drawing.Size(49, 14);
            this.lab_vend_name.TabIndex = 3;
            this.lab_vend_name.Text = "厂商：";
            // 
            // lab_prod_no
            // 
            this.lab_prod_no.AutoSize = true;
            this.lab_prod_no.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_prod_no.Location = new System.Drawing.Point(550, 36);
            this.lab_prod_no.Name = "lab_prod_no";
            this.lab_prod_no.Size = new System.Drawing.Size(42, 14);
            this.lab_prod_no.TabIndex = 2;
            this.lab_prod_no.Text = "ART：";
            // 
            // lab_date
            // 
            this.lab_date.AutoSize = true;
            this.lab_date.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_date.Location = new System.Drawing.Point(33, 34);
            this.lab_date.Name = "lab_date";
            this.lab_date.Size = new System.Drawing.Size(49, 14);
            this.lab_date.TabIndex = 1;
            this.lab_date.Text = "日期：";
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
            this.splitContainer2.Size = new System.Drawing.Size(934, 355);
            this.splitContainer2.SplitterDistance = 282;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.CARD_DATE,
            this.VEND_NO,
            this.VEND_NAME,
            this.FIRSTARTICLE_TYPE,
            this.SHOE_NO,
            this.PROD_NO,
            this.IS_QCCONFIRM,
            this.TEST_RESULT});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(934, 282);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // operation
            // 
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem1.Name = "edit";
            dataGridViewOperationItem1.Text = "编辑";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.operation.Width = 67;
            // 
            // CARD_DATE
            // 
            this.CARD_DATE.HeaderText = "日期";
            this.CARD_DATE.Name = "CARD_DATE";
            this.CARD_DATE.Width = 67;
            // 
            // VEND_NO
            // 
            this.VEND_NO.HeaderText = "厂商代号";
            this.VEND_NO.Name = "VEND_NO";
            this.VEND_NO.Visible = false;
            this.VEND_NO.Width = 99;
            // 
            // VEND_NAME
            // 
            this.VEND_NAME.HeaderText = "厂商";
            this.VEND_NAME.Name = "VEND_NAME";
            this.VEND_NAME.Width = 67;
            // 
            // FIRSTARTICLE_TYPE
            // 
            this.FIRSTARTICLE_TYPE.HeaderText = "首件确认种类";
            this.FIRSTARTICLE_TYPE.Name = "FIRSTARTICLE_TYPE";
            this.FIRSTARTICLE_TYPE.Width = 131;
            // 
            // SHOE_NO
            // 
            this.SHOE_NO.HeaderText = "鞋型";
            this.SHOE_NO.Name = "SHOE_NO";
            this.SHOE_NO.Width = 67;
            // 
            // PROD_NO
            // 
            this.PROD_NO.HeaderText = "ART";
            this.PROD_NO.Name = "PROD_NO";
            this.PROD_NO.Width = 65;
            // 
            // IS_QCCONFIRM
            // 
            this.IS_QCCONFIRM.HeaderText = "QC确认";
            this.IS_QCCONFIRM.Name = "IS_QCCONFIRM";
            this.IS_QCCONFIRM.Visible = false;
            this.IS_QCCONFIRM.Width = 91;
            // 
            // TEST_RESULT
            // 
            this.TEST_RESULT.HeaderText = "检测状况";
            this.TEST_RESULT.Name = "TEST_RESULT";
            this.TEST_RESULT.Width = 99;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(210, 7);
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
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem2.Name = "edit";
            dataGridViewOperationItem2.Text = "编辑";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem2);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            this.dataGridViewOperationColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // F_QCM_ExternalColorCard_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 536);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_ExternalColorCard_Main";
            this.Text = "发外厂商色卡";
            this.Load += new System.EventHandler(this.F_QCM_ExternalColorCard_Main_Load);
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
        private System.Windows.Forms.Button importbtn;
        private System.Windows.Forms.Button Modelbtn;
        private System.Windows.Forms.Button searchbtn;
        private System.Windows.Forms.DateTimePicker end_date;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_vend_name;
        private System.Windows.Forms.TextBox txt_prod_no;
        private System.Windows.Forms.Label lab_vend_name;
        private System.Windows.Forms.Label lab_prod_no;
        private System.Windows.Forms.Label lab_date;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DateTimePicker start_date;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.Button insertbtn;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARD_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VEND_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VEND_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIRSTARTICLE_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHOE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROD_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IS_QCCONFIRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEST_RESULT;
    }
}