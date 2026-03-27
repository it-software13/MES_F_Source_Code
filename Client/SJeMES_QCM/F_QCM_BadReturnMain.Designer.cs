
namespace SJeMES_QCM
{
    partial class F_QCM_BadReturnMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_BadReturnMain));
            this.btn_return = new System.Windows.Forms.Button();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.btn_select = new System.Windows.Forms.Button();
            this.txt_shoe_nos = new System.Windows.Forms.TextBox();
            this.lab_shoes = new System.Windows.Forms.Label();
            this.txt_art = new System.Windows.Forms.TextBox();
            this.lab_art = new System.Windows.Forms.Label();
            this.lab_date = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btn_enter = new System.Windows.Forms.Button();
            this.btn_inportmode = new System.Windows.Forms.Button();
            this.btn_inport = new System.Windows.Forms.Button();
            this.btn_excel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.datagridview1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.RETURN_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RETURN_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLANT_AREA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TURNOVER_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RETURN_FREQUENCY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AFFECT_HOURS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHOE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROD_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_return
            // 
            this.btn_return.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_return.Location = new System.Drawing.Point(24, 3);
            this.btn_return.Name = "btn_return";
            this.btn_return.Size = new System.Drawing.Size(75, 30);
            this.btn_return.TabIndex = 0;
            this.btn_return.Text = "返回";
            this.btn_return.UseVisualStyleBackColor = true;
            this.btn_return.Click += new System.EventHandler(this.btn_return_Click);
            // 
            // start_date
            // 
            this.start_date.CustomFormat = "yyyy-MM-dd";
            this.start_date.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.start_date.Location = new System.Drawing.Point(179, 41);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(165, 26);
            this.start_date.TabIndex = 30;
            this.start_date.Value = new System.DateTime(2021, 11, 9, 0, 0, 0, 0);
            // 
            // btn_select
            // 
            this.btn_select.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_select.Location = new System.Drawing.Point(860, 36);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(100, 35);
            this.btn_select.TabIndex = 4;
            this.btn_select.Text = "搜索";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // txt_shoe_nos
            // 
            this.txt_shoe_nos.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_shoe_nos.Location = new System.Drawing.Point(657, 41);
            this.txt_shoe_nos.Name = "txt_shoe_nos";
            this.txt_shoe_nos.Size = new System.Drawing.Size(159, 26);
            this.txt_shoe_nos.TabIndex = 3;
            // 
            // lab_shoes
            // 
            this.lab_shoes.AutoSize = true;
            this.lab_shoes.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_shoes.Location = new System.Drawing.Point(612, 42);
            this.lab_shoes.Name = "lab_shoes";
            this.lab_shoes.Size = new System.Drawing.Size(42, 21);
            this.lab_shoes.TabIndex = 0;
            this.lab_shoes.Text = "鞋型";
            // 
            // txt_art
            // 
            this.txt_art.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_art.Location = new System.Drawing.Point(421, 42);
            this.txt_art.Name = "txt_art";
            this.txt_art.Size = new System.Drawing.Size(159, 26);
            this.txt_art.TabIndex = 2;
            // 
            // lab_art
            // 
            this.lab_art.AutoSize = true;
            this.lab_art.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_art.Location = new System.Drawing.Point(378, 44);
            this.lab_art.Name = "lab_art";
            this.lab_art.Size = new System.Drawing.Size(40, 21);
            this.lab_art.TabIndex = 0;
            this.lab_art.Text = "ART";
            // 
            // lab_date
            // 
            this.lab_date.AutoSize = true;
            this.lab_date.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_date.Location = new System.Drawing.Point(134, 43);
            this.lab_date.Name = "lab_date";
            this.lab_date.Size = new System.Drawing.Size(42, 21);
            this.lab_date.TabIndex = 0;
            this.lab_date.Text = "日期";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.Color.White;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 64);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btn_enter);
            this.splitContainer2.Panel1.Controls.Add(this.btn_inportmode);
            this.splitContainer2.Panel1.Controls.Add(this.btn_inport);
            this.splitContainer2.Panel1.Controls.Add(this.btn_return);
            this.splitContainer2.Panel1.Controls.Add(this.btn_excel);
            this.splitContainer2.Panel1.Controls.Add(this.start_date);
            this.splitContainer2.Panel1.Controls.Add(this.btn_select);
            this.splitContainer2.Panel1.Controls.Add(this.txt_art);
            this.splitContainer2.Panel1.Controls.Add(this.lab_date);
            this.splitContainer2.Panel1.Controls.Add(this.lab_shoes);
            this.splitContainer2.Panel1.Controls.Add(this.txt_shoe_nos);
            this.splitContainer2.Panel1.Controls.Add(this.lab_art);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(1255, 636);
            this.splitContainer2.SplitterDistance = 116;
            this.splitContainer2.TabIndex = 1;
            // 
            // btn_enter
            // 
            this.btn_enter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_enter.Location = new System.Drawing.Point(255, 82);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(75, 30);
            this.btn_enter.TabIndex = 0;
            this.btn_enter.Text = "录入";
            this.btn_enter.UseVisualStyleBackColor = true;
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
            // 
            // btn_inportmode
            // 
            this.btn_inportmode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_inportmode.Location = new System.Drawing.Point(356, 82);
            this.btn_inportmode.Name = "btn_inportmode";
            this.btn_inportmode.Size = new System.Drawing.Size(89, 30);
            this.btn_inportmode.TabIndex = 0;
            this.btn_inportmode.Text = "导入模板";
            this.btn_inportmode.UseVisualStyleBackColor = true;
            this.btn_inportmode.Click += new System.EventHandler(this.btn_inportmode_Click);
            // 
            // btn_inport
            // 
            this.btn_inport.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_inport.Location = new System.Drawing.Point(151, 82);
            this.btn_inport.Name = "btn_inport";
            this.btn_inport.Size = new System.Drawing.Size(75, 30);
            this.btn_inport.TabIndex = 0;
            this.btn_inport.Text = "导入";
            this.btn_inport.UseVisualStyleBackColor = true;
            this.btn_inport.Click += new System.EventHandler(this.btn_inport_Click);
            // 
            // btn_excel
            // 
            this.btn_excel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_excel.Location = new System.Drawing.Point(51, 82);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(75, 30);
            this.btn_excel.TabIndex = 0;
            this.btn_excel.Text = "Excel";
            this.btn_excel.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.datagridview1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1255, 516);
            this.splitContainer1.SplitterDistance = 454;
            this.splitContainer1.TabIndex = 18;
            // 
            // datagridview1
            // 
            this.datagridview1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.datagridview1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.datagridview1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagridview1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.datagridview1.ColumnHeadersHeight = 33;
            this.datagridview1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.RETURN_NO,
            this.RETURN_DATE,
            this.PLANT_AREA,
            this.ORDER_QTY,
            this.TURNOVER_QTY,
            this.B_QTY,
            this.RETURN_FREQUENCY,
            this.AFFECT_HOURS,
            this.SHOE_NO,
            this.PROD_NO});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.datagridview1.DefaultCellStyle = dataGridViewCellStyle3;
            this.datagridview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagridview1.Location = new System.Drawing.Point(0, 0);
            this.datagridview1.Name = "datagridview1";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.datagridview1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.datagridview1.RowTemplate.Height = 30;
            this.datagridview1.Size = new System.Drawing.Size(1255, 454);
            this.datagridview1.TabIndex = 0;
            this.datagridview1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_BadReturn_CellClick);
            this.datagridview1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.datagridview1_RowPostPaint);
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "查看";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "DETAIL";
            dataGridViewOperationItem1.Text = "DETAIL";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.MinimumWidth = 80;
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.Width = 80;
            // 
            // RETURN_NO
            // 
            this.RETURN_NO.HeaderText = "退货单号";
            this.RETURN_NO.Name = "RETURN_NO";
            this.RETURN_NO.Width = 115;
            // 
            // RETURN_DATE
            // 
            this.RETURN_DATE.HeaderText = "退货日期";
            this.RETURN_DATE.Name = "RETURN_DATE";
            this.RETURN_DATE.Width = 115;
            // 
            // PLANT_AREA
            // 
            this.PLANT_AREA.HeaderText = "厂区";
            this.PLANT_AREA.Name = "PLANT_AREA";
            this.PLANT_AREA.Width = 115;
            // 
            // ORDER_QTY
            // 
            this.ORDER_QTY.HeaderText = "订单数";
            this.ORDER_QTY.Name = "ORDER_QTY";
            this.ORDER_QTY.Width = 115;
            // 
            // TURNOVER_QTY
            // 
            this.TURNOVER_QTY.HeaderText = "翻箱数（双）";
            this.TURNOVER_QTY.Name = "TURNOVER_QTY";
            this.TURNOVER_QTY.Width = 120;
            // 
            // B_QTY
            // 
            this.B_QTY.HeaderText = "B品（只）";
            this.B_QTY.Name = "B_QTY";
            this.B_QTY.Width = 115;
            // 
            // RETURN_FREQUENCY
            // 
            this.RETURN_FREQUENCY.HeaderText = "退库（次）";
            this.RETURN_FREQUENCY.Name = "RETURN_FREQUENCY";
            this.RETURN_FREQUENCY.Width = 115;
            // 
            // AFFECT_HOURS
            // 
            this.AFFECT_HOURS.HeaderText = "品质影响后段工时";
            this.AFFECT_HOURS.Name = "AFFECT_HOURS";
            this.AFFECT_HOURS.Width = 150;
            // 
            // SHOE_NO
            // 
            this.SHOE_NO.HeaderText = "鞋型";
            this.SHOE_NO.Name = "SHOE_NO";
            this.SHOE_NO.Width = 115;
            // 
            // PROD_NO
            // 
            this.PROD_NO.HeaderText = "ART";
            this.PROD_NO.Name = "PROD_NO";
            this.PROD_NO.Width = 115;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.BackColor = System.Drawing.Color.White;
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(547, 8);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(705, 39);
            this.pageControl1.TabIndex = 17;
            this.pageControl1.TotalCount = 0;
            // 
            // F_QCM_BadReturnMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 700);
            this.Controls.Add(this.splitContainer2);
            this.Name = "F_QCM_BadReturnMain";
            this.Text = "不良退货";
            this.Load += new System.EventHandler(this.F_QCM_BadReturn_Main_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridview1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_return;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.TextBox txt_shoe_nos;
        private System.Windows.Forms.Label lab_shoes;
        private System.Windows.Forms.TextBox txt_art;
        private System.Windows.Forms.Label lab_art;
        private System.Windows.Forms.Label lab_date;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btn_enter;
        private System.Windows.Forms.Button btn_inport;
        private System.Windows.Forms.Button btn_excel;
        private System.Windows.Forms.DataGridView datagridview1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DateTimePicker start_date;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn RETURN_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn RETURN_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLANT_AREA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn TURNOVER_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn B_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn RETURN_FREQUENCY;
        private System.Windows.Forms.DataGridViewTextBoxColumn AFFECT_HOURS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHOE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROD_NO;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_inportmode;
    }
}