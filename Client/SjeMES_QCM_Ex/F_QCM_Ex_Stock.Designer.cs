namespace SjeMES_QCM_Ex
{
    partial class F_QCM_Ex_Stock
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewEx1 = new SJeMES_Control_Library.DataGridViewEx();
            this.check = new SJeMES_Control_Library.DataGridViewCheckBoxColumnEx();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.housecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.present_capacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacity_remaining = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cbo_BarCode = new System.Windows.Forms.ComboBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_keyword = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridViewEx1);
            this.panel1.Location = new System.Drawing.Point(12, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 465);
            this.panel1.TabIndex = 0;
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.code,
            this.name,
            this.housecode,
            this.Column1,
            this.id,
            this.capacity,
            this.present_capacity,
            this.capacity_remaining});
            this.dataGridViewEx1.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowHeadersVisible = false;
            this.dataGridViewEx1.RowTemplate.Height = 23;
            this.dataGridViewEx1.Size = new System.Drawing.Size(1138, 458);
            this.dataGridViewEx1.TabIndex = 0;
            this.dataGridViewEx1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellContentClick);
            this.dataGridViewEx1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEx1_CellMouseDoubleClick);
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.Name = "check";
            // 
            // code
            // 
            this.code.HeaderText = "库位编号";
            this.code.Name = "code";
            this.code.Width = 200;
            // 
            // name
            // 
            this.name.HeaderText = "库位名称";
            this.name.Name = "name";
            this.name.Width = 200;
            // 
            // housecode
            // 
            this.housecode.HeaderText = "仓库代号";
            this.housecode.Name = "housecode";
            // 
            // Column1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "Delete";
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "操作";
            this.Column1.Name = "Column1";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // capacity
            // 
            this.capacity.HeaderText = "Max_Capacity";
            this.capacity.Name = "capacity";
            // 
            // present_capacity
            // 
            this.present_capacity.HeaderText = "Present_Capacity";
            this.present_capacity.Name = "present_capacity";
            // 
            // capacity_remaining
            // 
            this.capacity_remaining.HeaderText = "Remaining_Capacity";
            this.capacity_remaining.Name = "capacity_remaining";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.cbo_BarCode);
            this.panel2.Controls.Add(this.btn_add);
            this.panel2.Controls.Add(this.btn_search);
            this.panel2.Controls.Add(this.txt_keyword);
            this.panel2.Location = new System.Drawing.Point(12, 77);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1128, 41);
            this.panel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(502, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 25);
            this.button1.TabIndex = 9;
            this.button1.Text = "打印库位条码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.printbtn_Click);
            // 
            // cbo_BarCode
            // 
            this.cbo_BarCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_BarCode.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.cbo_BarCode.FormattingEnabled = true;
            this.cbo_BarCode.Location = new System.Drawing.Point(629, 7);
            this.cbo_BarCode.Name = "cbo_BarCode";
            this.cbo_BarCode.Size = new System.Drawing.Size(186, 27);
            this.cbo_BarCode.TabIndex = 8;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(403, 9);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 25);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "新增";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(310, 9);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 25);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_keyword
            // 
            this.txt_keyword.Location = new System.Drawing.Point(73, 9);
            this.txt_keyword.Name = "txt_keyword";
            this.txt_keyword.Size = new System.Drawing.Size(215, 20);
            this.txt_keyword.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.pageControl1);
            this.panel3.Location = new System.Drawing.Point(12, 596);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1144, 60);
            this.panel3.TabIndex = 2;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.BackColor = System.Drawing.Color.White;
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(267, 3);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(680, 42);
            this.pageControl1.TabIndex = 19;
            this.pageControl1.TotalCount = 0;
            // 
            // F_QCM_Ex_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 689);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "F_QCM_Ex_Stock";
            this.Text = "实验室存档库位资料";
            this.Load += new System.EventHandler(this.F_QCM_Ex_Stock_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private SJeMES_Control_Library.DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_keyword;
        private System.Windows.Forms.Panel panel3;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.ComboBox cbo_BarCode;
        private System.Windows.Forms.Button button1;
        private SJeMES_Control_Library.DataGridViewCheckBoxColumnEx check;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn housecode;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn present_capacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacity_remaining;
    }
}