
namespace SJeMES_BDM
{
    partial class F_BDM_WORKMANSHIP_TESTITEM_Main
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.dgv_data = new SJeMES_Control_Library.DataGridViewEx();
            this.select = new SJeMES_Control_Library.DataGridViewCheckBoxColumnEx();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judge_text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judge_type_text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judge_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judge_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_lcll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_judge = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_remark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pageControl1);
            this.panel3.Location = new System.Drawing.Point(12, 617);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1335, 59);
            this.panel3.TabIndex = 5;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(727, 4);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(603, 46);
            this.pageControl1.TabIndex = 1;
            this.pageControl1.TotalCount = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.dgv_data);
            this.panel2.Location = new System.Drawing.Point(12, 132);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1335, 482);
            this.panel2.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btn_update);
            this.panel4.Controls.Add(this.btn_delete);
            this.panel4.Controls.Add(this.btn_add);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1327, 38);
            this.panel4.TabIndex = 2;
            // 
            // btn_update
            // 
            this.btn_update.BackColor = System.Drawing.Color.White;
            this.btn_update.Location = new System.Drawing.Point(93, 2);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(84, 33);
            this.btn_update.TabIndex = 19;
            this.btn_update.Text = "编辑";
            this.btn_update.UseVisualStyleBackColor = false;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.White;
            this.btn_delete.Location = new System.Drawing.Point(183, 2);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(84, 33);
            this.btn_delete.TabIndex = 18;
            this.btn_delete.Text = "删除";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.White;
            this.btn_add.ForeColor = System.Drawing.Color.Black;
            this.btn_add.Location = new System.Drawing.Point(3, 2);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(84, 33);
            this.btn_add.TabIndex = 17;
            this.btn_add.Text = "新增";
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // dgv_data
            // 
            this.dgv_data.AllowUserToAddRows = false;
            this.dgv_data.BackgroundColor = System.Drawing.Color.White;
            this.dgv_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.code,
            this.name,
            this.judge_text,
            this.judge_type_text,
            this.judge_value,
            this.remark,
            this.ID,
            this.judge,
            this.judge_type,
            this.is_lcll});
            this.dgv_data.Location = new System.Drawing.Point(3, 47);
            this.dgv_data.Name = "dgv_data";
            this.dgv_data.RowHeadersVisible = false;
            this.dgv_data.RowTemplate.Height = 23;
            this.dgv_data.Size = new System.Drawing.Size(1327, 430);
            this.dgv_data.TabIndex = 0;
            // 
            // select
            // 
            this.select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.select.HeaderText = "";
            this.select.Name = "select";
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.select.Width = 60;
            // 
            // code
            // 
            this.code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.code.HeaderText = "检验项编号";
            this.code.Name = "code";
            this.code.Width = 150;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.name.HeaderText = "检验项名称";
            this.name.Name = "name";
            this.name.Width = 150;
            // 
            // judge_text
            // 
            this.judge_text.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.judge_text.HeaderText = "判断标准";
            this.judge_text.Name = "judge_text";
            this.judge_text.Width = 120;
            // 
            // judge_type_text
            // 
            this.judge_type_text.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.judge_type_text.HeaderText = "检测项标准类型";
            this.judge_type_text.Name = "judge_type_text";
            this.judge_type_text.Width = 120;
            // 
            // judge_value
            // 
            this.judge_value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.judge_value.HeaderText = "检测项标准";
            this.judge_value.Name = "judge_value";
            this.judge_value.Width = 250;
            // 
            // remark
            // 
            this.remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // judge
            // 
            this.judge.HeaderText = "judge";
            this.judge.Name = "judge";
            this.judge.Visible = false;
            // 
            // judge_type
            // 
            this.judge_type.HeaderText = "judge_type";
            this.judge_type.Name = "judge_type";
            this.judge_type.Visible = false;
            // 
            // is_lcll
            // 
            this.is_lcll.HeaderText = "is_lcll";
            this.is_lcll.Name = "is_lcll";
            this.is_lcll.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmb_judge);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.txt_remark);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_name);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_code);
            this.panel1.Controls.Add(this.label39);
            this.panel1.Location = new System.Drawing.Point(12, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1335, 56);
            this.panel1.TabIndex = 6;
            // 
            // cmb_judge
            // 
            this.cmb_judge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_judge.FormattingEnabled = true;
            this.cmb_judge.Location = new System.Drawing.Point(668, 14);
            this.cmb_judge.Name = "cmb_judge";
            this.cmb_judge.Size = new System.Drawing.Size(121, 20);
            this.cmb_judge.TabIndex = 73;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1192, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "定制检验项";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(1056, 12);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 16;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_remark
            // 
            this.txt_remark.Location = new System.Drawing.Point(899, 14);
            this.txt_remark.Name = "txt_remark";
            this.txt_remark.Size = new System.Drawing.Size(121, 21);
            this.txt_remark.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(814, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "备注：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(566, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "判断标准：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(418, 14);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(121, 21);
            this.txt_name.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(292, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "检验项目名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_code
            // 
            this.txt_code.Location = new System.Drawing.Point(137, 14);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(121, 21);
            this.txt_code.TabIndex = 9;
            // 
            // label39
            // 
            this.label39.Location = new System.Drawing.Point(11, 17);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(120, 12);
            this.label39.TabIndex = 8;
            this.label39.Text = "检验项目编号：";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // F_BDM_WORKMANSHIP_TESTITEM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 680);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "F_BDM_WORKMANSHIP_TESTITEM_Main";
            this.Text = "检测项目-工艺-测试";
            this.Load += new System.EventHandler(this.F_BDM_WORKMANSHIP_TESTITEM_Main_Load);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_add;
        private SJeMES_Control_Library.DataGridViewEx dgv_data;
        private SJeMES_Control_Library.DataGridViewCheckBoxColumnEx select;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn judge_text;
        private System.Windows.Forms.DataGridViewTextBoxColumn judge_type_text;
        private System.Windows.Forms.DataGridViewTextBoxColumn judge_value;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn judge;
        private System.Windows.Forms.DataGridViewTextBoxColumn judge_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_lcll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmb_judge;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_remark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Label label39;
    }
}