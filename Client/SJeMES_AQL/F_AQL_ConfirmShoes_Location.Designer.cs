
namespace SJeMES_AQL
{
    partial class F_AQL_ConfirmShoes_Location
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_AQL_ConfirmShoes_Location));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem5 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_search = new System.Windows.Forms.Button();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.cbo_BarCode = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cz = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouse_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(-1, 68);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_search);
            this.splitContainer1.Panel1.Controls.Add(this.tb_search);
            this.splitContainer1.Panel1.Controls.Add(this.cbo_BarCode);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(939, 608);
            this.splitContainer1.SplitterDistance = 46;
            this.splitContainer1.TabIndex = 1;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(800, 7);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(101, 33);
            this.btn_search.TabIndex = 5;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // tb_search
            // 
            this.tb_search.Location = new System.Drawing.Point(619, 9);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(154, 29);
            this.tb_search.TabIndex = 4;
            // 
            // cbo_BarCode
            // 
            this.cbo_BarCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_BarCode.FormattingEnabled = true;
            this.cbo_BarCode.Location = new System.Drawing.Point(461, 9);
            this.cbo_BarCode.Name = "cbo_BarCode";
            this.cbo_BarCode.Size = new System.Drawing.Size(152, 29);
            this.cbo_BarCode.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(193, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 33);
            this.button3.TabIndex = 2;
            this.button3.Text = "导入库位";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "下载模板";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(331, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "新增库位";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.splitContainer2.Size = new System.Drawing.Size(939, 558);
            this.splitContainer2.SplitterDistance = 500;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cz,
            this.id,
            this.xh,
            this.stock_code,
            this.warehouse_name,
            this.stock_name,
            this.REMARK});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(939, 500);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // cz
            // 
            this.cz.Description = null;
            this.cz.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_AQL.Properties.Resources.ic_print_24;
            dataGridViewOperationItem1.Name = "print";
            dataGridViewOperationItem1.Text = "打印";
            dataGridViewOperationItem2.Image = global::SJeMES_AQL.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem2.Name = "DELETE";
            dataGridViewOperationItem2.Text = "删除";
            dataGridViewOperationItem3.Image = global::SJeMES_AQL.Properties.Resources.ic_update_24;
            dataGridViewOperationItem3.Name = "EDIT";
            dataGridViewOperationItem3.Text = "编辑";
            this.cz.Items.Add(dataGridViewOperationItem1);
            this.cz.Items.Add(dataGridViewOperationItem2);
            this.cz.Items.Add(dataGridViewOperationItem3);
            this.cz.ItemSize = new System.Drawing.Size(24, 24);
            this.cz.Name = "cz";
            this.cz.OverflowImage = ((System.Drawing.Image)(resources.GetObject("cz.OverflowImage")));
            this.cz.ReadOnly = true;
            this.cz.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // xh
            // 
            this.xh.HeaderText = "序号";
            this.xh.Name = "xh";
            this.xh.ReadOnly = true;
            // 
            // stock_code
            // 
            this.stock_code.HeaderText = "库位代号";
            this.stock_code.Name = "stock_code";
            this.stock_code.ReadOnly = true;
            // 
            // warehouse_name
            // 
            this.warehouse_name.HeaderText = "仓库名称";
            this.warehouse_name.Name = "warehouse_name";
            this.warehouse_name.ReadOnly = true;
            // 
            // stock_name
            // 
            this.stock_name.HeaderText = "库位";
            this.stock_name.Name = "stock_name";
            this.stock_name.ReadOnly = true;
            // 
            // REMARK
            // 
            this.REMARK.HeaderText = "备注";
            this.REMARK.Name = "REMARK";
            this.REMARK.ReadOnly = true;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(331, 0);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(607, 53);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem4.Image = global::SJeMES_AQL.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem4.Name = "DELETE";
            dataGridViewOperationItem4.Text = "删除";
            dataGridViewOperationItem5.Image = global::SJeMES_AQL.Properties.Resources.ic_update_24;
            dataGridViewOperationItem5.Name = "EDIT";
            dataGridViewOperationItem5.Text = "编辑";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem5);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            this.dataGridViewOperationColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOperationColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewOperationColumn1.Width = 149;
            // 
            // F_AQL_ConfirmShoes_Location
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 675);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_AQL_ConfirmShoes_Location";
            this.Text = "库位维护";
            this.Load += new System.EventHandler(this.F_AQL_ConfirmShoes_Location_Load);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.ComboBox cbo_BarCode;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn cz;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn xh;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouse_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARK;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox tb_search;
    }
}