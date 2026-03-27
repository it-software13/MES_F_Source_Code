
namespace SJeMES_QCM
{
    partial class F_QCM_ProductDelivery
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.lab_date = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lab_result = new System.Windows.Forms.Label();
            this.txt_area = new System.Windows.Forms.TextBox();
            this.lab_vend = new System.Windows.Forms.Label();
            this.btn_export = new System.Windows.Forms.Button();
            this.Modelbtn = new System.Windows.Forms.Button();
            this.Searchbtn = new System.Windows.Forms.Button();
            this.txt_art = new System.Windows.Forms.TextBox();
            this.lab_art_no = new System.Windows.Forms.Label();
            this.txt_order = new System.Windows.Forms.TextBox();
            this.lab_order = new System.Windows.Forms.Label();
            this.txt_zl = new System.Windows.Forms.TextBox();
            this.lab_zhiling = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
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
            this.splitContainer1.Location = new System.Drawing.Point(-1, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.end_date);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.start_date);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.txt_area);
            this.splitContainer1.Panel1.Controls.Add(this.btn_export);
            this.splitContainer1.Panel1.Controls.Add(this.Modelbtn);
            this.splitContainer1.Panel1.Controls.Add(this.Searchbtn);
            this.splitContainer1.Panel1.Controls.Add(this.txt_art);
            this.splitContainer1.Panel1.Controls.Add(this.txt_order);
            this.splitContainer1.Panel1.Controls.Add(this.txt_zl);
            this.splitContainer1.Panel1.Controls.Add(this.lab_date);
            this.splitContainer1.Panel1.Controls.Add(this.lab_zhiling);
            this.splitContainer1.Panel1.Controls.Add(this.lab_order);
            this.splitContainer1.Panel1.Controls.Add(this.lab_art_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_vend);
            this.splitContainer1.Panel1.Controls.Add(this.lab_result);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1132, 492);
            this.splitContainer1.SplitterDistance = 131;
            this.splitContainer1.TabIndex = 0;
            // 
            // end_date
            // 
            this.end_date.Location = new System.Drawing.Point(277, 60);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(127, 21);
            this.end_date.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F);
            this.label7.Location = new System.Drawing.Point(254, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "-";
            // 
            // start_date
            // 
            this.start_date.Location = new System.Drawing.Point(113, 60);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(127, 21);
            this.start_date.TabIndex = 15;
            // 
            // lab_date
            // 
            this.lab_date.AutoSize = true;
            this.lab_date.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_date.Location = new System.Drawing.Point(19, 62);
            this.lab_date.Name = "lab_date";
            this.lab_date.Size = new System.Drawing.Size(88, 16);
            this.lab_date.TabIndex = 14;
            this.lab_date.Text = "验货日期：";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "PASS",
            "FAIL"});
            this.comboBox1.Location = new System.Drawing.Point(898, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 13;
            // 
            // lab_result
            // 
            this.lab_result.AutoSize = true;
            this.lab_result.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_result.Location = new System.Drawing.Point(812, 24);
            this.lab_result.Name = "lab_result";
            this.lab_result.Size = new System.Drawing.Size(88, 16);
            this.lab_result.TabIndex = 11;
            this.lab_result.Text = "验货结果：";
            // 
            // txt_area
            // 
            this.txt_area.Location = new System.Drawing.Point(696, 22);
            this.txt_area.Name = "txt_area";
            this.txt_area.Size = new System.Drawing.Size(100, 21);
            this.txt_area.TabIndex = 10;
            // 
            // lab_vend
            // 
            this.lab_vend.AutoSize = true;
            this.lab_vend.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_vend.Location = new System.Drawing.Point(632, 24);
            this.lab_vend.Name = "lab_vend";
            this.lab_vend.Size = new System.Drawing.Size(56, 16);
            this.lab_vend.TabIndex = 9;
            this.lab_vend.Text = "厂区：";
            // 
            // btn_export
            // 
            this.btn_export.Font = new System.Drawing.Font("宋体", 10F);
            this.btn_export.Location = new System.Drawing.Point(180, 92);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(72, 27);
            this.btn_export.TabIndex = 8;
            this.btn_export.Text = "导入";
            this.btn_export.UseVisualStyleBackColor = true;
            // 
            // Modelbtn
            // 
            this.Modelbtn.Font = new System.Drawing.Font("宋体", 9F);
            this.Modelbtn.Location = new System.Drawing.Point(35, 92);
            this.Modelbtn.Name = "Modelbtn";
            this.Modelbtn.Size = new System.Drawing.Size(124, 27);
            this.Modelbtn.TabIndex = 7;
            this.Modelbtn.Text = "导入模板";
            this.Modelbtn.UseVisualStyleBackColor = true;
            // 
            // Searchbtn
            // 
            this.Searchbtn.Font = new System.Drawing.Font("宋体", 10F);
            this.Searchbtn.Location = new System.Drawing.Point(1047, 20);
            this.Searchbtn.Name = "Searchbtn";
            this.Searchbtn.Size = new System.Drawing.Size(72, 27);
            this.Searchbtn.TabIndex = 6;
            this.Searchbtn.Text = "搜索";
            this.Searchbtn.UseVisualStyleBackColor = true;
            this.Searchbtn.Click += new System.EventHandler(this.Searchbtn_Click);
            // 
            // txt_art
            // 
            this.txt_art.Location = new System.Drawing.Point(498, 22);
            this.txt_art.Name = "txt_art";
            this.txt_art.Size = new System.Drawing.Size(100, 21);
            this.txt_art.TabIndex = 5;
            // 
            // lab_art_no
            // 
            this.lab_art_no.AutoSize = true;
            this.lab_art_no.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_art_no.Location = new System.Drawing.Point(434, 24);
            this.lab_art_no.Name = "lab_art_no";
            this.lab_art_no.Size = new System.Drawing.Size(64, 16);
            this.lab_art_no.TabIndex = 4;
            this.lab_art_no.Text = "ARTNO：";
            // 
            // txt_order
            // 
            this.txt_order.Location = new System.Drawing.Point(304, 22);
            this.txt_order.Name = "txt_order";
            this.txt_order.Size = new System.Drawing.Size(100, 21);
            this.txt_order.TabIndex = 3;
            // 
            // lab_order
            // 
            this.lab_order.AutoSize = true;
            this.lab_order.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_order.Location = new System.Drawing.Point(242, 24);
            this.lab_order.Name = "lab_order";
            this.lab_order.Size = new System.Drawing.Size(56, 16);
            this.lab_order.TabIndex = 2;
            this.lab_order.Text = "订单：";
            // 
            // txt_zl
            // 
            this.txt_zl.Location = new System.Drawing.Point(113, 22);
            this.txt_zl.Name = "txt_zl";
            this.txt_zl.Size = new System.Drawing.Size(100, 21);
            this.txt_zl.TabIndex = 1;
            // 
            // lab_zhiling
            // 
            this.lab_zhiling.AutoSize = true;
            this.lab_zhiling.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_zhiling.Location = new System.Drawing.Point(51, 24);
            this.lab_zhiling.Name = "lab_zhiling";
            this.lab_zhiling.Size = new System.Drawing.Size(56, 16);
            this.lab_zhiling.TabIndex = 0;
            this.lab_zhiling.Text = "制令：";
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
            this.splitContainer2.Size = new System.Drawing.Size(1132, 357);
            this.splitContainer2.SplitterDistance = 288;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1132, 288);
            this.dataGridView1.TabIndex = 0;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Enabled = false;
            this.pageControl1.Location = new System.Drawing.Point(497, 2);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(632, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // F_QCM_ProductDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 553);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_ProductDelivery";
            this.Text = "成品出货看板";
            this.Load += new System.EventHandler(this.F_QCM_ProductDelivery_Load);
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
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Button Modelbtn;
        private System.Windows.Forms.Button Searchbtn;
        private System.Windows.Forms.TextBox txt_art;
        private System.Windows.Forms.Label lab_art_no;
        private System.Windows.Forms.TextBox txt_order;
        private System.Windows.Forms.Label lab_order;
        private System.Windows.Forms.TextBox txt_zl;
        private System.Windows.Forms.Label lab_zhiling;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lab_result;
        private System.Windows.Forms.TextBox txt_area;
        private System.Windows.Forms.Label lab_vend;
        private System.Windows.Forms.DateTimePicker end_date;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker start_date;
        private System.Windows.Forms.Label lab_date;
    }
}