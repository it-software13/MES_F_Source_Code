
namespace SJeMES_QCM
{
    partial class F_QCM_ExternalColorCard_Edit
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.updatebtn = new System.Windows.Forms.Button();
            this.txt_test_result = new System.Windows.Forms.TextBox();
            this.lab_jczkuang = new System.Windows.Forms.Label();
            this.txt_prod_no = new System.Windows.Forms.TextBox();
            this.lab_art = new System.Windows.Forms.Label();
            this.txt_shoe_no = new System.Windows.Forms.TextBox();
            this.lab_xxing = new System.Windows.Forms.Label();
            this.txt_firstarticle_type = new System.Windows.Forms.TextBox();
            this.lab_sjqrzl = new System.Windows.Forms.Label();
            this.txt_vend_name = new System.Windows.Forms.TextBox();
            this.lab_cs = new System.Windows.Forms.Label();
            this.txt_date = new System.Windows.Forms.TextBox();
            this.lab_rq = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CARD_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VEND_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHOE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROD_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APTESTITEM_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEST_STANDARD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAMP_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AQL_LEVEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHECK_RESULT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REMARKS = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(-3, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.updatebtn);
            this.splitContainer1.Panel1.Controls.Add(this.txt_test_result);
            this.splitContainer1.Panel1.Controls.Add(this.lab_jczkuang);
            this.splitContainer1.Panel1.Controls.Add(this.txt_prod_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_art);
            this.splitContainer1.Panel1.Controls.Add(this.txt_shoe_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_xxing);
            this.splitContainer1.Panel1.Controls.Add(this.txt_firstarticle_type);
            this.splitContainer1.Panel1.Controls.Add(this.lab_sjqrzl);
            this.splitContainer1.Panel1.Controls.Add(this.txt_vend_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_cs);
            this.splitContainer1.Panel1.Controls.Add(this.txt_date);
            this.splitContainer1.Panel1.Controls.Add(this.lab_rq);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(849, 440);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 0;
            // 
            // updatebtn
            // 
            this.updatebtn.Location = new System.Drawing.Point(734, 96);
            this.updatebtn.Name = "updatebtn";
            this.updatebtn.Size = new System.Drawing.Size(75, 23);
            this.updatebtn.TabIndex = 14;
            this.updatebtn.Text = "确认";
            this.updatebtn.UseVisualStyleBackColor = true;
            this.updatebtn.Click += new System.EventHandler(this.updatebtn_Click);
            // 
            // txt_test_result
            // 
            this.txt_test_result.Location = new System.Drawing.Point(291, 60);
            this.txt_test_result.Name = "txt_test_result";
            this.txt_test_result.ReadOnly = true;
            this.txt_test_result.Size = new System.Drawing.Size(100, 21);
            this.txt_test_result.TabIndex = 13;
            // 
            // lab_jczkuang
            // 
            this.lab_jczkuang.AutoSize = true;
            this.lab_jczkuang.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_jczkuang.Location = new System.Drawing.Point(211, 62);
            this.lab_jczkuang.Name = "lab_jczkuang";
            this.lab_jczkuang.Size = new System.Drawing.Size(77, 14);
            this.lab_jczkuang.TabIndex = 12;
            this.lab_jczkuang.Text = "检测状况：";
            // 
            // txt_prod_no
            // 
            this.txt_prod_no.Location = new System.Drawing.Point(89, 55);
            this.txt_prod_no.Name = "txt_prod_no";
            this.txt_prod_no.ReadOnly = true;
            this.txt_prod_no.Size = new System.Drawing.Size(100, 21);
            this.txt_prod_no.TabIndex = 9;
            // 
            // lab_art
            // 
            this.lab_art.AutoSize = true;
            this.lab_art.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_art.Location = new System.Drawing.Point(34, 57);
            this.lab_art.Name = "lab_art";
            this.lab_art.Size = new System.Drawing.Size(42, 14);
            this.lab_art.TabIndex = 8;
            this.lab_art.Text = "ART：";
            // 
            // txt_shoe_no
            // 
            this.txt_shoe_no.Location = new System.Drawing.Point(709, 20);
            this.txt_shoe_no.Name = "txt_shoe_no";
            this.txt_shoe_no.ReadOnly = true;
            this.txt_shoe_no.Size = new System.Drawing.Size(100, 21);
            this.txt_shoe_no.TabIndex = 7;
            // 
            // lab_xxing
            // 
            this.lab_xxing.AutoSize = true;
            this.lab_xxing.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_xxing.Location = new System.Drawing.Point(654, 22);
            this.lab_xxing.Name = "lab_xxing";
            this.lab_xxing.Size = new System.Drawing.Size(49, 14);
            this.lab_xxing.TabIndex = 6;
            this.lab_xxing.Text = "鞋型：";
            // 
            // txt_firstarticle_type
            // 
            this.txt_firstarticle_type.Location = new System.Drawing.Point(535, 20);
            this.txt_firstarticle_type.Name = "txt_firstarticle_type";
            this.txt_firstarticle_type.ReadOnly = true;
            this.txt_firstarticle_type.Size = new System.Drawing.Size(100, 21);
            this.txt_firstarticle_type.TabIndex = 5;
            // 
            // lab_sjqrzl
            // 
            this.lab_sjqrzl.AutoSize = true;
            this.lab_sjqrzl.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_sjqrzl.Location = new System.Drawing.Point(426, 22);
            this.lab_sjqrzl.Name = "lab_sjqrzl";
            this.lab_sjqrzl.Size = new System.Drawing.Size(105, 14);
            this.lab_sjqrzl.TabIndex = 4;
            this.lab_sjqrzl.Text = "首件确认种类：";
            // 
            // txt_vend_name
            // 
            this.txt_vend_name.Location = new System.Drawing.Point(294, 20);
            this.txt_vend_name.Name = "txt_vend_name";
            this.txt_vend_name.ReadOnly = true;
            this.txt_vend_name.Size = new System.Drawing.Size(100, 21);
            this.txt_vend_name.TabIndex = 3;
            // 
            // lab_cs
            // 
            this.lab_cs.AutoSize = true;
            this.lab_cs.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_cs.Location = new System.Drawing.Point(239, 22);
            this.lab_cs.Name = "lab_cs";
            this.lab_cs.Size = new System.Drawing.Size(49, 14);
            this.lab_cs.TabIndex = 2;
            this.lab_cs.Text = "厂商：";
            // 
            // txt_date
            // 
            this.txt_date.Location = new System.Drawing.Point(89, 20);
            this.txt_date.Name = "txt_date";
            this.txt_date.ReadOnly = true;
            this.txt_date.Size = new System.Drawing.Size(100, 21);
            this.txt_date.TabIndex = 1;
            // 
            // lab_rq
            // 
            this.lab_rq.AutoSize = true;
            this.lab_rq.Font = new System.Drawing.Font("宋体", 10F);
            this.lab_rq.Location = new System.Drawing.Point(34, 22);
            this.lab_rq.Name = "lab_rq";
            this.lab_rq.Size = new System.Drawing.Size(49, 14);
            this.lab_rq.TabIndex = 0;
            this.lab_rq.Text = "日期：";
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
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer2.Size = new System.Drawing.Size(849, 304);
            this.splitContainer2.SplitterDistance = 228;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CARD_DATE,
            this.VEND_NO,
            this.SHOE_NO,
            this.PROD_NO,
            this.APTESTITEM_NAME,
            this.TEST_STANDARD,
            this.SAMP_QTY,
            this.AQL_LEVEL,
            this.AC,
            this.RE,
            this.CHECK_RESULT,
            this.REMARKS});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(849, 228);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // CARD_DATE
            // 
            this.CARD_DATE.HeaderText = "日期";
            this.CARD_DATE.Name = "CARD_DATE";
            this.CARD_DATE.ReadOnly = true;
            // 
            // VEND_NO
            // 
            this.VEND_NO.HeaderText = "厂商编号";
            this.VEND_NO.Name = "VEND_NO";
            this.VEND_NO.ReadOnly = true;
            // 
            // SHOE_NO
            // 
            this.SHOE_NO.HeaderText = "鞋型";
            this.SHOE_NO.Name = "SHOE_NO";
            this.SHOE_NO.ReadOnly = true;
            // 
            // PROD_NO
            // 
            this.PROD_NO.HeaderText = "ART";
            this.PROD_NO.Name = "PROD_NO";
            this.PROD_NO.ReadOnly = true;
            // 
            // APTESTITEM_NAME
            // 
            this.APTESTITEM_NAME.HeaderText = "检测项名称";
            this.APTESTITEM_NAME.Name = "APTESTITEM_NAME";
            this.APTESTITEM_NAME.ReadOnly = true;
            // 
            // TEST_STANDARD
            // 
            this.TEST_STANDARD.HeaderText = "检验标准";
            this.TEST_STANDARD.Name = "TEST_STANDARD";
            this.TEST_STANDARD.ReadOnly = true;
            // 
            // SAMP_QTY
            // 
            this.SAMP_QTY.HeaderText = "抽样数量";
            this.SAMP_QTY.Name = "SAMP_QTY";
            this.SAMP_QTY.ReadOnly = true;
            // 
            // AQL_LEVEL
            // 
            this.AQL_LEVEL.HeaderText = "AQL级别";
            this.AQL_LEVEL.Name = "AQL_LEVEL";
            this.AQL_LEVEL.ReadOnly = true;
            // 
            // AC
            // 
            this.AC.HeaderText = "AC";
            this.AC.Name = "AC";
            this.AC.ReadOnly = true;
            // 
            // RE
            // 
            this.RE.HeaderText = "RE";
            this.RE.Name = "RE";
            this.RE.ReadOnly = true;
            // 
            // CHECK_RESULT
            // 
            this.CHECK_RESULT.HeaderText = "检验结果";
            this.CHECK_RESULT.Name = "CHECK_RESULT";
            this.CHECK_RESULT.ReadOnly = true;
            // 
            // REMARKS
            // 
            this.REMARKS.HeaderText = "备注";
            this.REMARKS.Name = "REMARKS";
            this.REMARKS.ReadOnly = true;
            // 
            // pageControl1
            // 
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(125, 11);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 49);
            this.pageControl1.TabIndex = 1;
            this.pageControl1.TotalCount = 0;
            // 
            // F_QCM_ExternalColorCard_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 504);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_ExternalColorCard_Edit";
            this.Text = "编辑";
            this.Load += new System.EventHandler(this.F_QCM_ExternalColorCard_Add_Load);
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
        private System.Windows.Forms.Label lab_rq;
        private System.Windows.Forms.TextBox txt_test_result;
        private System.Windows.Forms.Label lab_jczkuang;
        private System.Windows.Forms.TextBox txt_prod_no;
        private System.Windows.Forms.Label lab_art;
        private System.Windows.Forms.TextBox txt_shoe_no;
        private System.Windows.Forms.Label lab_xxing;
        private System.Windows.Forms.TextBox txt_firstarticle_type;
        private System.Windows.Forms.Label lab_sjqrzl;
        private System.Windows.Forms.TextBox txt_vend_name;
        private System.Windows.Forms.Label lab_cs;
        private System.Windows.Forms.TextBox txt_date;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button updatebtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARD_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VEND_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHOE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROD_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn APTESTITEM_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEST_STANDARD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAMP_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn AQL_LEVEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn AC;
        private System.Windows.Forms.DataGridViewTextBoxColumn RE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHECK_RESULT;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARKS;
    }
}