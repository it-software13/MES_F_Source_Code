namespace SjeMES_QCM_Ex
{
    partial class F_QCM_Ex_LookResult_New
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lab_result = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv = new SJeMES_Control_Library.DataGridViewEx();
            this.xh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.standard_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scjg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pdjg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.search_img = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btn_print = new System.Windows.Forms.Button();
            this.txt_test_type = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(19, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1156, 259);
            this.panel1.TabIndex = 0;
            // 
            // lab_result
            // 
            this.lab_result.Font = new System.Drawing.Font("SimSun", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_result.ForeColor = System.Drawing.Color.Lime;
            this.lab_result.Location = new System.Drawing.Point(1172, 291);
            this.lab_result.Name = "lab_result";
            this.lab_result.Size = new System.Drawing.Size(154, 133);
            this.lab_result.TabIndex = 50;
            this.lab_result.Text = "PASS";
            this.lab_result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgv);
            this.panel2.Location = new System.Drawing.Point(19, 383);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1147, 332);
            this.panel2.TabIndex = 1;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xh,
            this.inspection_name,
            this.inspection_code,
            this.standard_value,
            this.scjg,
            this.pdjg,
            this.remark,
            this.search_img,
            this.id});
            this.dgv.Location = new System.Drawing.Point(3, 4);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1074, 323);
            this.dgv.TabIndex = 7;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // xh
            // 
            this.xh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.xh.HeaderText = "序号";
            this.xh.Name = "xh";
            this.xh.ReadOnly = true;
            this.xh.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.xh.Width = 60;
            // 
            // inspection_name
            // 
            this.inspection_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.inspection_name.HeaderText = "测试项名称";
            this.inspection_name.Name = "inspection_name";
            this.inspection_name.ReadOnly = true;
            this.inspection_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.inspection_name.Width = 180;
            // 
            // inspection_code
            // 
            this.inspection_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.inspection_code.HeaderText = "测试编号";
            this.inspection_code.Name = "inspection_code";
            this.inspection_code.ReadOnly = true;
            this.inspection_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.inspection_code.Width = 180;
            // 
            // standard_value
            // 
            this.standard_value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.standard_value.HeaderText = "测试标准";
            this.standard_value.Name = "standard_value";
            this.standard_value.ReadOnly = true;
            this.standard_value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.standard_value.Width = 180;
            // 
            // scjg
            // 
            this.scjg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.scjg.HeaderText = "实测结果";
            this.scjg.Name = "scjg";
            this.scjg.ReadOnly = true;
            this.scjg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.scjg.Width = 180;
            // 
            // pdjg
            // 
            this.pdjg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.pdjg.HeaderText = "判断结果";
            this.pdjg.Name = "pdjg";
            this.pdjg.ReadOnly = true;
            this.pdjg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pdjg.Width = 277;
            // 
            // remark
            // 
            this.remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            // 
            // search_img
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = "View Sample";
            this.search_img.DefaultCellStyle = dataGridViewCellStyle4;
            this.search_img.HeaderText = "操作";
            this.search_img.Name = "search_img";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(16, 366);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(88, 13);
            this.label23.TabIndex = 17;
            this.label23.Text = "检测项信息";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(16, 78);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(88, 13);
            this.label24.TabIndex = 51;
            this.label24.Text = "基础信息";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(110, 72);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(75, 25);
            this.btn_print.TabIndex = 52;
            this.btn_print.Text = "打印";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // txt_test_type
            // 
            this.txt_test_type.Enabled = false;
            this.txt_test_type.Location = new System.Drawing.Point(342, 75);
            this.txt_test_type.Name = "txt_test_type";
            this.txt_test_type.Size = new System.Drawing.Size(145, 20);
            this.txt_test_type.TabIndex = 54;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(242, 78);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(58, 13);
            this.label29.TabIndex = 53;
            this.label29.Text = "测试类型：";
            // 
            // F_QCM_Ex_LookResult_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 727);
            this.Controls.Add(this.txt_test_type);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.lab_result);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "F_QCM_Ex_LookResult_New";
            this.Text = "查看测试报告";
            this.Load += new System.EventHandler(this.F_QCM_Ex_LookResult_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lab_result;
        private SJeMES_Control_Library.DataGridViewEx dgv;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.TextBox txt_test_type;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.DataGridViewTextBoxColumn xh;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn standard_value;
        private System.Windows.Forms.DataGridViewTextBoxColumn scjg;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdjg;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewButtonColumn search_img;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}