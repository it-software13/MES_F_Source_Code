namespace SJeMES_AQL
{
    partial class F_AQL_CMAThetestshoesMin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_AQL_CMAThetestshoesMin));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.internal_test_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.internal_test_res = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.external_test_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.external_test_res = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.re_delivery_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.import_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RE_TEST_RES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
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
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1079, 639);
            this.splitContainer1.SplitterDistance = 56;
            this.splitContainer1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(28, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.TabIndex = 138;
            this.button2.Text = "返回";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.splitContainer2.Size = new System.Drawing.Size(1079, 579);
            this.splitContainer2.SplitterDistance = 518;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.internal_test_date,
            this.prod_no,
            this.internal_test_res,
            this.external_test_date,
            this.external_test_res,
            this.re_delivery_date,
            this.import_date,
            this.RE_TEST_RES,
            this.id,
            this.operation});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1059, 518);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(434, 1);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(636, 49);
            this.pageControl1.TabIndex = 7;
            this.pageControl1.TotalCount = 0;
            // 
            // internal_test_date
            // 
            this.internal_test_date.HeaderText = "实验室实际内测日期";
            this.internal_test_date.Name = "internal_test_date";
            // 
            // prod_no
            // 
            this.prod_no.HeaderText = "prod_no";
            this.prod_no.Name = "prod_no";
            this.prod_no.Visible = false;
            // 
            // internal_test_res
            // 
            this.internal_test_res.HeaderText = "内测结果勾选";
            this.internal_test_res.Name = "internal_test_res";
            // 
            // external_test_date
            // 
            this.external_test_date.HeaderText = "外部送测日期(有效期三个月)";
            this.external_test_date.Name = "external_test_date";
            // 
            // external_test_res
            // 
            this.external_test_res.HeaderText = "外部测试结果";
            this.external_test_res.Name = "external_test_res";
            // 
            // re_delivery_date
            // 
            this.re_delivery_date.HeaderText = "复测日期";
            this.re_delivery_date.Name = "re_delivery_date";
            // 
            // import_date
            // 
            this.import_date.HeaderText = "导入日期";
            this.import_date.Name = "import_date";
            // 
            // RE_TEST_RES
            // 
            this.RE_TEST_RES.HeaderText = "复测结果";
            this.RE_TEST_RES.Name = "RE_TEST_RES";
            this.RE_TEST_RES.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // operation
            // 
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_AQL.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem1.Name = "delete";
            dataGridViewOperationItem1.Text = "delete";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            // 
            // F_AQL_CMAThetestshoesMin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 704);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_AQL_CMAThetestshoesMin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CMA测试鞋明细";
            this.Load += new System.EventHandler(this.F_AQL_CMAThetestshoesMin_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn internal_test_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn internal_test_res;
        private System.Windows.Forms.DataGridViewTextBoxColumn external_test_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn external_test_res;
        private System.Windows.Forms.DataGridViewTextBoxColumn re_delivery_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn import_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn RE_TEST_RES;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
    }
}