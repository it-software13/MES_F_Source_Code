
namespace SJeMES_QCM
{
    partial class F_QCM_Vampschedule_Main
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
            this.btn_entering = new System.Windows.Forms.Button();
            this.btn_excel = new System.Windows.Forms.Button();
            this.btn_select = new System.Windows.Forms.Button();
            this.btn_out = new System.Windows.Forms.Button();
            this.txt_SE_ID = new System.Windows.Forms.TextBox();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.txt_SHOE_NO = new System.Windows.Forms.TextBox();
            this.lab_date = new System.Windows.Forms.Label();
            this.lab_shoes = new System.Windows.Forms.Label();
            this.lab_sale_order = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.WEEK_TIMES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PUTINTO_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WORK_HOURS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_DELIVERY_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LEAD_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRIP_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VAMP_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHOE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MODULE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(1, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.btn_entering);
            this.splitContainer1.Panel1.Controls.Add(this.btn_excel);
            this.splitContainer1.Panel1.Controls.Add(this.btn_select);
            this.splitContainer1.Panel1.Controls.Add(this.btn_out);
            this.splitContainer1.Panel1.Controls.Add(this.txt_SE_ID);
            this.splitContainer1.Panel1.Controls.Add(this.dtp);
            this.splitContainer1.Panel1.Controls.Add(this.txt_SHOE_NO);
            this.splitContainer1.Panel1.Controls.Add(this.lab_date);
            this.splitContainer1.Panel1.Controls.Add(this.lab_shoes);
            this.splitContainer1.Panel1.Controls.Add(this.lab_sale_order);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1213, 675);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_entering
            // 
            this.btn_entering.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_entering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_entering.Location = new System.Drawing.Point(128, 75);
            this.btn_entering.Name = "btn_entering";
            this.btn_entering.Size = new System.Drawing.Size(75, 28);
            this.btn_entering.TabIndex = 0;
            this.btn_entering.Text = "录入";
            this.btn_entering.UseVisualStyleBackColor = true;
            this.btn_entering.Click += new System.EventHandler(this.btn_entering_Click);
            // 
            // btn_excel
            // 
            this.btn_excel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_excel.Location = new System.Drawing.Point(36, 75);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(75, 28);
            this.btn_excel.TabIndex = 0;
            this.btn_excel.Text = "EXCEL";
            this.btn_excel.UseVisualStyleBackColor = true;
            // 
            // btn_select
            // 
            this.btn_select.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_select.Location = new System.Drawing.Point(931, 37);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(100, 30);
            this.btn_select.TabIndex = 0;
            this.btn_select.Text = "搜索";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // btn_out
            // 
            this.btn_out.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_out.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_out.Location = new System.Drawing.Point(11, 8);
            this.btn_out.Name = "btn_out";
            this.btn_out.Size = new System.Drawing.Size(75, 23);
            this.btn_out.TabIndex = 0;
            this.btn_out.Text = "返回";
            this.btn_out.UseVisualStyleBackColor = true;
            this.btn_out.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_SE_ID
            // 
            this.txt_SE_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SE_ID.Location = new System.Drawing.Point(664, 37);
            this.txt_SE_ID.Name = "txt_SE_ID";
            this.txt_SE_ID.Size = new System.Drawing.Size(187, 30);
            this.txt_SE_ID.TabIndex = 2;
            // 
            // dtp
            // 
            this.dtp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtp.Location = new System.Drawing.Point(97, 40);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(167, 29);
            this.dtp.TabIndex = 1;
            this.dtp.Value = new System.DateTime(2021, 11, 11, 13, 55, 20, 0);
            // 
            // txt_SHOE_NO
            // 
            this.txt_SHOE_NO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SHOE_NO.Location = new System.Drawing.Point(357, 38);
            this.txt_SHOE_NO.Name = "txt_SHOE_NO";
            this.txt_SHOE_NO.Size = new System.Drawing.Size(187, 30);
            this.txt_SHOE_NO.TabIndex = 2;
            // 
            // lab_date
            // 
            this.lab_date.AutoSize = true;
            this.lab_date.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_date.Location = new System.Drawing.Point(51, 47);
            this.lab_date.Name = "lab_date";
            this.lab_date.Size = new System.Drawing.Size(40, 16);
            this.lab_date.TabIndex = 0;
            this.lab_date.Text = "日期";
            // 
            // lab_shoes
            // 
            this.lab_shoes.AutoSize = true;
            this.lab_shoes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_shoes.Location = new System.Drawing.Point(311, 47);
            this.lab_shoes.Name = "lab_shoes";
            this.lab_shoes.Size = new System.Drawing.Size(40, 16);
            this.lab_shoes.TabIndex = 0;
            this.lab_shoes.Text = "鞋型";
            // 
            // lab_sale_order
            // 
            this.lab_sale_order.AutoSize = true;
            this.lab_sale_order.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_sale_order.Location = new System.Drawing.Point(586, 46);
            this.lab_sale_order.Name = "lab_sale_order";
            this.lab_sale_order.Size = new System.Drawing.Size(72, 16);
            this.lab_sale_order.TabIndex = 0;
            this.lab_sale_order.Text = "销售订单";
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
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer2.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer2.Size = new System.Drawing.Size(1213, 544);
            this.splitContainer2.SplitterDistance = 488;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WEEK_TIMES,
            this.PUTINTO_DATE,
            this.WORK_HOURS,
            this.ORDER_DELIVERY_DATE,
            this.LEAD_TIME,
            this.LAST_NUMBER,
            this.TRIP_QTY,
            this.VAMP_TYPE,
            this.SHOE_NO,
            this.MODULE_NO,
            this.SE_ID,
            this.ITEM_NO,
            this.QTY});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1213, 488);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // WEEK_TIMES
            // 
            this.WEEK_TIMES.HeaderText = "周次";
            this.WEEK_TIMES.Name = "WEEK_TIMES";
            this.WEEK_TIMES.Width = 60;
            // 
            // PUTINTO_DATE
            // 
            this.PUTINTO_DATE.HeaderText = "投产日期";
            this.PUTINTO_DATE.Name = "PUTINTO_DATE";
            // 
            // WORK_HOURS
            // 
            this.WORK_HOURS.HeaderText = "每周计划工作时数";
            this.WORK_HOURS.Name = "WORK_HOURS";
            this.WORK_HOURS.Width = 150;
            // 
            // ORDER_DELIVERY_DATE
            // 
            this.ORDER_DELIVERY_DATE.HeaderText = "订单交期";
            this.ORDER_DELIVERY_DATE.Name = "ORDER_DELIVERY_DATE";
            // 
            // LEAD_TIME
            // 
            this.LEAD_TIME.HeaderText = "Lead Time";
            this.LEAD_TIME.Name = "LEAD_TIME";
            // 
            // LAST_NUMBER
            // 
            this.LAST_NUMBER.HeaderText = "楦头号";
            this.LAST_NUMBER.Name = "LAST_NUMBER";
            // 
            // TRIP_QTY
            // 
            this.TRIP_QTY.HeaderText = "趟数";
            this.TRIP_QTY.Name = "TRIP_QTY";
            // 
            // VAMP_TYPE
            // 
            this.VAMP_TYPE.HeaderText = "类型";
            this.VAMP_TYPE.Name = "VAMP_TYPE";
            // 
            // SHOE_NO
            // 
            this.SHOE_NO.HeaderText = "鞋型";
            this.SHOE_NO.Name = "SHOE_NO";
            // 
            // MODULE_NO
            // 
            this.MODULE_NO.HeaderText = "模号";
            this.MODULE_NO.Name = "MODULE_NO";
            // 
            // SE_ID
            // 
            this.SE_ID.HeaderText = "销售订单";
            this.SE_ID.Name = "SE_ID";
            // 
            // ITEM_NO
            // 
            this.ITEM_NO.HeaderText = "成品物料号";
            this.ITEM_NO.Name = "ITEM_NO";
            // 
            // QTY
            // 
            this.QTY.HeaderText = "数量";
            this.QTY.Name = "QTY";
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.BackColor = System.Drawing.Color.White;
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(582, 0);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(618, 49);
            this.pageControl1.TabIndex = 2;
            this.pageControl1.TotalCount = 0;
            // 
            // F_QCM_Vampschedule_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 740);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Vampschedule_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "鞋面进度表（针车）";
            this.Load += new System.EventHandler(this.F_QCM_Vampschedule_Main_Load);
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
        private System.Windows.Forms.Button btn_out;
        private System.Windows.Forms.Label lab_date;
        private System.Windows.Forms.TextBox txt_SE_ID;
        private System.Windows.Forms.TextBox txt_SHOE_NO;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Label lab_sale_order;
        private System.Windows.Forms.Label lab_shoes;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_entering;
        private System.Windows.Forms.Button btn_excel;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEEK_TIMES;
        private System.Windows.Forms.DataGridViewTextBoxColumn PUTINTO_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn WORK_HOURS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_DELIVERY_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LEAD_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRIP_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAMP_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHOE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MODULE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
    }
}