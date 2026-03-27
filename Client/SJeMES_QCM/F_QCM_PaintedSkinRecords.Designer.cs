
namespace SJeMES_QCM
{
    partial class F_QCM_PaintedSkinRecords
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_PaintedSkinRecords));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_select = new System.Windows.Forms.Button();
            this.lab_wliaoxinxi = new System.Windows.Forms.Label();
            this.txt_item_name = new System.Windows.Forms.TextBox();
            this.txt_vend_name = new System.Windows.Forms.TextBox();
            this.txt_item_no = new System.Windows.Forms.TextBox();
            this.lab_vendor = new System.Windows.Forms.Label();
            this.lab_material_no = new System.Windows.Forms.Label();
            this.lab_material_name = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.paint_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vend_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PAINT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.btn_select);
            this.splitContainer1.Panel1.Controls.Add(this.lab_wliaoxinxi);
            this.splitContainer1.Panel1.Controls.Add(this.txt_item_name);
            this.splitContainer1.Panel1.Controls.Add(this.txt_vend_name);
            this.splitContainer1.Panel1.Controls.Add(this.txt_item_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_vendor);
            this.splitContainer1.Panel1.Controls.Add(this.lab_material_no);
            this.splitContainer1.Panel1.Controls.Add(this.lab_material_name);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1224, 591);
            this.splitContainer1.SplitterDistance = 79;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_select
            // 
            this.btn_select.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_select.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_select.Location = new System.Drawing.Point(868, 38);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(89, 36);
            this.btn_select.TabIndex = 2;
            this.btn_select.Text = "搜索";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // lab_wliaoxinxi
            // 
            this.lab_wliaoxinxi.AutoSize = true;
            this.lab_wliaoxinxi.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_wliaoxinxi.Location = new System.Drawing.Point(19, 8);
            this.lab_wliaoxinxi.Name = "lab_wliaoxinxi";
            this.lab_wliaoxinxi.Size = new System.Drawing.Size(74, 21);
            this.lab_wliaoxinxi.TabIndex = 0;
            this.lab_wliaoxinxi.Text = "物料信息";
            // 
            // txt_item_name
            // 
            this.txt_item_name.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_item_name.Location = new System.Drawing.Point(674, 44);
            this.txt_item_name.Name = "txt_item_name";
            this.txt_item_name.Size = new System.Drawing.Size(175, 26);
            this.txt_item_name.TabIndex = 1;
            // 
            // txt_vend_name
            // 
            this.txt_vend_name.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_vend_name.Location = new System.Drawing.Point(112, 44);
            this.txt_vend_name.Name = "txt_vend_name";
            this.txt_vend_name.Size = new System.Drawing.Size(175, 26);
            this.txt_vend_name.TabIndex = 1;
            // 
            // txt_item_no
            // 
            this.txt_item_no.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_item_no.Location = new System.Drawing.Point(375, 44);
            this.txt_item_no.Name = "txt_item_no";
            this.txt_item_no.Size = new System.Drawing.Size(175, 26);
            this.txt_item_no.TabIndex = 1;
            // 
            // lab_vendor
            // 
            this.lab_vendor.AutoSize = true;
            this.lab_vendor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_vendor.Location = new System.Drawing.Point(20, 47);
            this.lab_vendor.Name = "lab_vendor";
            this.lab_vendor.Size = new System.Drawing.Size(90, 21);
            this.lab_vendor.TabIndex = 0;
            this.lab_vendor.Text = "生产厂商：";
            // 
            // lab_material_no
            // 
            this.lab_material_no.AutoSize = true;
            this.lab_material_no.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_material_no.Location = new System.Drawing.Point(315, 46);
            this.lab_material_no.Name = "lab_material_no";
            this.lab_material_no.Size = new System.Drawing.Size(58, 21);
            this.lab_material_no.TabIndex = 0;
            this.lab_material_no.Text = "料号：";
            // 
            // lab_material_name
            // 
            this.lab_material_name.AutoSize = true;
            this.lab_material_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_material_name.Location = new System.Drawing.Point(582, 46);
            this.lab_material_name.Name = "lab_material_name";
            this.lab_material_name.Size = new System.Drawing.Size(90, 21);
            this.lab_material_name.TabIndex = 0;
            this.lab_material_name.Text = "材料名称：";
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
            this.splitContainer2.Size = new System.Drawing.Size(1224, 508);
            this.splitContainer2.SplitterDistance = 445;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.operation,
            this.paint_no,
            this.ITEM_NO,
            this.vend_name,
            this.ITEM_NAME,
            this.QTY,
            this.PAINT_DATE});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1224, 445);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(599, 7);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(613, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "DETAIL";
            dataGridViewOperationItem1.Text = "DETAIL";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.MinimumWidth = 80;
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.operation.Width = 90;
            // 
            // paint_no
            // 
            this.paint_no.HeaderText = "任务编号";
            this.paint_no.Name = "paint_no";
            this.paint_no.ReadOnly = true;
            this.paint_no.Width = 120;
            // 
            // ITEM_NO
            // 
            this.ITEM_NO.HeaderText = "料号";
            this.ITEM_NO.Name = "ITEM_NO";
            this.ITEM_NO.ReadOnly = true;
            this.ITEM_NO.Width = 120;
            // 
            // vend_name
            // 
            this.vend_name.HeaderText = "生产厂商";
            this.vend_name.Name = "vend_name";
            this.vend_name.ReadOnly = true;
            this.vend_name.Width = 120;
            // 
            // ITEM_NAME
            // 
            this.ITEM_NAME.HeaderText = "物料名称";
            this.ITEM_NAME.Name = "ITEM_NAME";
            this.ITEM_NAME.ReadOnly = true;
            this.ITEM_NAME.Width = 120;
            // 
            // QTY
            // 
            this.QTY.HeaderText = "进仓数量";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 120;
            // 
            // PAINT_DATE
            // 
            this.PAINT_DATE.HeaderText = "创建时间";
            this.PAINT_DATE.Name = "PAINT_DATE";
            this.PAINT_DATE.ReadOnly = true;
            this.PAINT_DATE.Width = 120;
            // 
            // F_QCM_PaintedSkinRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 654);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_PaintedSkinRecords";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "画皮记录列表";
            this.Load += new System.EventHandler(this.F_QCM_PaintedSkinRecords_Load);
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
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lab_wliaoxinxi;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.TextBox txt_item_name;
        private System.Windows.Forms.TextBox txt_item_no;
        private System.Windows.Forms.TextBox txt_vend_name;
        private System.Windows.Forms.Label lab_material_name;
        private System.Windows.Forms.Label lab_material_no;
        private System.Windows.Forms.Label lab_vendor;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn paint_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn vend_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn PAINT_DATE;
    }
}