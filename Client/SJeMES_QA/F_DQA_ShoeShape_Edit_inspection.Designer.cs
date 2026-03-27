
namespace SJeMES_QA
{
    partial class F_DQA_ShoeShape_Edit_inspection
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.xz = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.enum_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.standard_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qc_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judgment_criteria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judge_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 62);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(763, 512);
            this.splitContainer1.SplitterDistance = 55;
            this.splitContainer1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(629, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 29);
            this.button2.TabIndex = 2;
            this.button2.Text = "确认";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "查找";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(138, 29);
            this.textBox1.TabIndex = 0;
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
            this.splitContainer2.Size = new System.Drawing.Size(763, 453);
            this.splitContainer2.SplitterDistance = 394;
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
            this.xz,
            this.enum_value,
            this.inspection_code,
            this.inspection_name,
            this.standard_value,
            this.qc_type,
            this.judgment_criteria,
            this.judge_type,
            this.inspection_type});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(763, 394);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(157, 6);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(606, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // xz
            // 
            this.xz.HeaderText = "选择";
            this.xz.Name = "xz";
            this.xz.ReadOnly = true;
            // 
            // enum_value
            // 
            this.enum_value.HeaderText = "判断标准名称";
            this.enum_value.Name = "enum_value";
            this.enum_value.ReadOnly = true;
            this.enum_value.Visible = false;
            // 
            // inspection_code
            // 
            this.inspection_code.HeaderText = "检验项编号";
            this.inspection_code.Name = "inspection_code";
            this.inspection_code.ReadOnly = true;
            // 
            // inspection_name
            // 
            this.inspection_name.HeaderText = "检验项名称";
            this.inspection_name.Name = "inspection_name";
            this.inspection_name.ReadOnly = true;
            // 
            // standard_value
            // 
            this.standard_value.HeaderText = "检测项标准";
            this.standard_value.Name = "standard_value";
            this.standard_value.ReadOnly = true;
            // 
            // qc_type
            // 
            this.qc_type.HeaderText = "QC类别";
            this.qc_type.Name = "qc_type";
            this.qc_type.ReadOnly = true;
            // 
            // judgment_criteria
            // 
            this.judgment_criteria.HeaderText = "判断标准";
            this.judgment_criteria.Name = "judgment_criteria";
            this.judgment_criteria.ReadOnly = true;
            this.judgment_criteria.Visible = false;
            // 
            // judge_type
            // 
            this.judge_type.HeaderText = "判断类型";
            this.judge_type.Name = "judge_type";
            this.judge_type.ReadOnly = true;
            this.judge_type.Visible = false;
            // 
            // inspection_type
            // 
            this.inspection_type.HeaderText = "检测项目类型";
            this.inspection_type.Name = "inspection_type";
            this.inspection_type.ReadOnly = true;
            this.inspection_type.Visible = false;
            // 
            // F_DQA_ShoeShape_Edit_inspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 574);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_DQA_ShoeShape_Edit_inspection";
            this.Text = "检验项目";
            this.Load += new System.EventHandler(this.F_DQA_ShoeShape_Edit_inspection_Load);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn xz;
        private System.Windows.Forms.DataGridViewTextBoxColumn enum_value;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn standard_value;
        private System.Windows.Forms.DataGridViewTextBoxColumn qc_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn judgment_criteria;
        private System.Windows.Forms.DataGridViewTextBoxColumn judge_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_type;
    }
}