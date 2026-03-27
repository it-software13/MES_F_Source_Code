
namespace SjeMES_QCM_Ex
{
    partial class F_QCM_Ex_Dev
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
            this.编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.部门编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.部门 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工段编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工段 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备类型编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.panel2.Location = new System.Drawing.Point(0, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(940, 38);
            this.panel2.TabIndex = 2;
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
            this.printbtn.Text = "打印设备条码";
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
            this.panel3.Location = new System.Drawing.Point(0, 541);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(940, 55);
            this.panel3.TabIndex = 3;
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
            this.编号,
            this.设备名称,
            this.部门编号,
            this.部门,
            this.工段编号,
            this.工段,
            this.设备类型编号,
            this.设备类型,
            this.id});
            this.dataGridViewEx1.Location = new System.Drawing.Point(0, 112);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowHeadersVisible = false;
            this.dataGridViewEx1.RowTemplate.Height = 23;
            this.dataGridViewEx1.Size = new System.Drawing.Size(934, 423);
            this.dataGridViewEx1.TabIndex = 4;
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.Name = "check";
            // 
            // 编号
            // 
            this.编号.HeaderText = "编号";
            this.编号.Name = "编号";
            this.编号.Width = 200;
            // 
            // 设备名称
            // 
            this.设备名称.HeaderText = "设备名称";
            this.设备名称.Name = "设备名称";
            this.设备名称.Width = 200;
            // 
            // 部门编号
            // 
            this.部门编号.HeaderText = "部门编号";
            this.部门编号.Name = "部门编号";
            // 
            // 部门
            // 
            this.部门.HeaderText = "部门";
            this.部门.Name = "部门";
            // 
            // 工段编号
            // 
            this.工段编号.HeaderText = "工段编号";
            this.工段编号.Name = "工段编号";
            // 
            // 工段
            // 
            this.工段.HeaderText = "工段";
            this.工段.Name = "工段";
            // 
            // 设备类型编号
            // 
            this.设备类型编号.HeaderText = "设备类型编号";
            this.设备类型编号.Name = "设备类型编号";
            // 
            // 设备类型
            // 
            this.设备类型.HeaderText = "设备类型";
            this.设备类型.Name = "设备类型";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // F_QCM_Ex_Dev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 603);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "F_QCM_Ex_Dev";
            this.Text = "设备信息打印";
            this.Load += new System.EventHandler(this.F_QCM_Ex_Dev_Load);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn 编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 部门编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 部门;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工段编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工段;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备类型编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}