namespace RPT_WMS_Stoc_Matching
{
    partial class F_WMS_Multiple_ArtSelect
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.btn_select = new System.Windows.Forms.Button();
            this.btn_qyery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.text_art_name = new System.Windows.Forms.TextBox();
            this.text_art_no = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.col_art = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_clear_all = new System.Windows.Forms.Button();
            this.select_add = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.product_line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productLineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._depart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 61);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(755, 543);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_confirm);
            this.panel1.Controls.Add(this.btn_select);
            this.panel1.Controls.Add(this.btn_qyery);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.text_art_name);
            this.panel1.Controls.Add(this.text_art_no);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(749, 94);
            this.panel1.TabIndex = 0;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(343, 54);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(87, 23);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(252, 54);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(84, 23);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "重置搜索";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirm.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.btn_confirm.ForeColor = System.Drawing.Color.Black;
            this.btn_confirm.Location = new System.Drawing.Point(557, 14);
            this.btn_confirm.Margin = new System.Windows.Forms.Padding(0);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(78, 26);
            this.btn_confirm.TabIndex = 5;
            this.btn_confirm.Text = "确认";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(136, 54);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(101, 23);
            this.btn_select.TabIndex = 5;
            this.btn_select.Text = "全选/取消全选";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // btn_qyery
            // 
            this.btn_qyery.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_qyery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_qyery.ForeColor = System.Drawing.Color.White;
            this.btn_qyery.Location = new System.Drawing.Point(48, 54);
            this.btn_qyery.Name = "btn_qyery";
            this.btn_qyery.Size = new System.Drawing.Size(81, 23);
            this.btn_qyery.TabIndex = 4;
            this.btn_qyery.Text = "查询";
            this.btn_qyery.UseVisualStyleBackColor = false;
            this.btn_qyery.Click += new System.EventHandler(this.btn_qyery_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(210, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "部门";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "产线";
            // 
            // text_art_name
            // 
            this.text_art_name.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.text_art_name.Location = new System.Drawing.Point(252, 12);
            this.text_art_name.Name = "text_art_name";
            this.text_art_name.Size = new System.Drawing.Size(178, 23);
            this.text_art_name.TabIndex = 2;
            this.text_art_name.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.text_art_name_PreviewKeyDown);
            // 
            // text_art_no
            // 
            this.text_art_no.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.text_art_no.Location = new System.Drawing.Point(48, 11);
            this.text_art_no.Name = "text_art_no";
            this.text_art_no.Size = new System.Drawing.Size(120, 23);
            this.text_art_no.TabIndex = 2;
            this.text_art_no.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.text_art_no_PreviewKeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select_add,
            this.product_line,
            this.productLineName,
            this._depart,
            this.Column3});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(499, 437);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("黑体", 13F);
            this.tabControl1.Location = new System.Drawing.Point(508, 103);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(244, 437);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.OldLace;
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Font = new System.Drawing.Font("宋体", 9F);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(236, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "已选组别";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.Color.Bisque;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_art,
            this.col_delete});
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(230, 368);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // col_art
            // 
            this.col_art.HeaderText = "产线";
            this.col_art.Name = "col_art";
            this.col_art.ReadOnly = true;
            // 
            // col_delete
            // 
            this.col_delete.HeaderText = "删除";
            this.col_delete.Name = "col_delete";
            this.col_delete.ReadOnly = true;
            this.col_delete.Text = "删除";
            this.col_delete.UseColumnTextForButtonValue = true;
            this.col_delete.Width = 65;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btn_clear_all);
            this.panel2.Location = new System.Drawing.Point(3, 375);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(230, 26);
            this.panel2.TabIndex = 1;
            // 
            // btn_clear_all
            // 
            this.btn_clear_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_clear_all.Location = new System.Drawing.Point(0, 0);
            this.btn_clear_all.Name = "btn_clear_all";
            this.btn_clear_all.Size = new System.Drawing.Size(230, 26);
            this.btn_clear_all.TabIndex = 0;
            this.btn_clear_all.Text = "全部清空";
            this.btn_clear_all.UseVisualStyleBackColor = true;
            this.btn_clear_all.Click += new System.EventHandler(this.btn_clear_all_Click);
            // 
            // select_add
            // 
            this.select_add.FalseValue = "0";
            this.select_add.HeaderText = "选择";
            this.select_add.Name = "select_add";
            this.select_add.TrueValue = "1";
            this.select_add.Width = 55;
            // 
            // product_line
            // 
            this.product_line.DataPropertyName = "productLine";
            this.product_line.HeaderText = "产线";
            this.product_line.Name = "product_line";
            this.product_line.ReadOnly = true;
            this.product_line.Width = 75;
            // 
            // productLineName
            // 
            this.productLineName.DataPropertyName = "productLineName";
            this.productLineName.HeaderText = "产线名称";
            this.productLineName.Name = "productLineName";
            // 
            // _depart
            // 
            this._depart.DataPropertyName = "depart";
            this._depart.HeaderText = "部门";
            this._depart.Name = "_depart";
            this._depart.ReadOnly = true;
            this._depart.Width = 260;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "MOLD_NO";
            this.Column3.HeaderText = "模号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // F_WMS_Multiple_ArtSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 605);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "F_WMS_Multiple_ArtSelect";
            this.Text = "产线选择";
            this.Load += new System.EventHandler(this.F_TPM_RD_Item_Art_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_qyery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_art_no;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_art_name;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_clear_all;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_art;
        private System.Windows.Forms.DataGridViewButtonColumn col_delete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select_add;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_line;
        private System.Windows.Forms.DataGridViewTextBoxColumn productLineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _depart;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}