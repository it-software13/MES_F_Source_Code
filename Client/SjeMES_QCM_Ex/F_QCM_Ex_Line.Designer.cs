
namespace SjeMES_QCM_Ex
{
    partial class F_QCM_Ex_Line
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbo_BarCode = new System.Windows.Forms.ComboBox();
            this.printbtn = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_keyword = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewEx1 = new SJeMES_Control_Library.DataGridViewEx();
            this.check = new SJeMES_Control_Library.DataGridViewCheckBoxColumnEx();
            this.产线编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.国家 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.地区 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.厂区 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.部门 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.产线 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.cbo_BarCode);
            this.panel2.Controls.Add(this.printbtn);
            this.panel2.Controls.Add(this.btn_search);
            this.panel2.Controls.Add(this.txt_keyword);
            this.panel2.Location = new System.Drawing.Point(2, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(940, 38);
            this.panel2.TabIndex = 3;
            // 
            // cbo_BarCode
            // 
            this.cbo_BarCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_BarCode.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbo_BarCode.FormattingEnabled = true;
            this.cbo_BarCode.Location = new System.Drawing.Point(439, 5);
            this.cbo_BarCode.Name = "cbo_BarCode";
            this.cbo_BarCode.Size = new System.Drawing.Size(186, 27);
            this.cbo_BarCode.TabIndex = 8;
            // 
            // printbtn
            // 
            this.printbtn.Location = new System.Drawing.Point(342, 8);
            this.printbtn.Name = "printbtn";
            this.printbtn.Size = new System.Drawing.Size(91, 23);
            this.printbtn.TabIndex = 3;
            this.printbtn.Text = "打印产线条码";
            this.printbtn.UseVisualStyleBackColor = true;
            this.printbtn.Click += new System.EventHandler(this.printbtn_Click);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(248, 8);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_keyword
            // 
            this.txt_keyword.Location = new System.Drawing.Point(16, 9);
            this.txt_keyword.Name = "txt_keyword";
            this.txt_keyword.Size = new System.Drawing.Size(215, 21);
            this.txt_keyword.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.pageControl1);
            this.panel3.Location = new System.Drawing.Point(2, 545);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(940, 55);
            this.panel3.TabIndex = 4;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.BackColor = System.Drawing.Color.White;
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(232, 7);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(705, 39);
            this.pageControl1.TabIndex = 19;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.产线编号,
            this.国家,
            this.地区,
            this.厂区,
            this.部门,
            this.产线,
            this.备注});
            this.dataGridViewEx1.Location = new System.Drawing.Point(2, 116);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowHeadersVisible = false;
            this.dataGridViewEx1.RowTemplate.Height = 23;
            this.dataGridViewEx1.Size = new System.Drawing.Size(934, 423);
            this.dataGridViewEx1.TabIndex = 5;
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.Name = "check";
            // 
            // 产线编号
            // 
            this.产线编号.HeaderText = "产线编号";
            this.产线编号.Name = "产线编号";
            this.产线编号.Width = 200;
            // 
            // 国家
            // 
            this.国家.HeaderText = "国家";
            this.国家.Name = "国家";
            this.国家.Width = 200;
            // 
            // 地区
            // 
            this.地区.HeaderText = "地区";
            this.地区.Name = "地区";
            // 
            // 厂区
            // 
            this.厂区.HeaderText = "厂区";
            this.厂区.Name = "厂区";
            // 
            // 部门
            // 
            this.部门.HeaderText = "部门";
            this.部门.Name = "部门";
            // 
            // 产线
            // 
            this.产线.HeaderText = "产线";
            this.产线.Name = "产线";
            // 
            // 备注
            // 
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            // 
            // F_QCM_Ex_Line
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 607);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "F_QCM_Ex_Line";
            this.Text = "产线二维码打印";
            this.Load += new System.EventHandler(this.F_QCM_Ex_Line_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbo_BarCode;
        private System.Windows.Forms.Button printbtn;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_keyword;
        private System.Windows.Forms.Panel panel3;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private SJeMES_Control_Library.DataGridViewEx dataGridViewEx1;
        private SJeMES_Control_Library.DataGridViewCheckBoxColumnEx check;
        private System.Windows.Forms.DataGridViewTextBoxColumn 产线编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 国家;
        private System.Windows.Forms.DataGridViewTextBoxColumn 地区;
        private System.Windows.Forms.DataGridViewTextBoxColumn 厂区;
        private System.Windows.Forms.DataGridViewTextBoxColumn 部门;
        private System.Windows.Forms.DataGridViewTextBoxColumn 产线;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
    }
}