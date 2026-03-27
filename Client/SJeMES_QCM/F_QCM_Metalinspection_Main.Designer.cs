
namespace SJeMES_QCM
{
    partial class F_QCM_Metalinspection_Main
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Metalinspection_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lab_RL = new System.Windows.Forms.Label();
            this.txt_left_or_right = new System.Windows.Forms.TextBox();
            this.lab_ART = new System.Windows.Forms.Label();
            this.txt_prod_no = new System.Windows.Forms.TextBox();
            this.btn_Select = new System.Windows.Forms.Button();
            this.lab_PO = new System.Windows.Forms.Label();
            this.txt_po_order = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.inspect_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.po_order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shoe_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.left_or_right = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productionline_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productionline_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handle_way = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handle_result = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_left_or_right);
            this.splitContainer1.Panel1.Controls.Add(this.txt_prod_no);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Select);
            this.splitContainer1.Panel1.Controls.Add(this.txt_po_order);
            this.splitContainer1.Panel1.Controls.Add(this.lab_RL);
            this.splitContainer1.Panel1.Controls.Add(this.lab_ART);
            this.splitContainer1.Panel1.Controls.Add(this.lab_PO);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(945, 434);
            this.splitContainer1.SplitterDistance = 84;
            this.splitContainer1.TabIndex = 0;
            // 
            // lab_RL
            // 
            this.lab_RL.AutoSize = true;
            this.lab_RL.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_RL.Location = new System.Drawing.Point(598, 27);
            this.lab_RL.Name = "lab_RL";
            this.lab_RL.Size = new System.Drawing.Size(61, 25);
            this.lab_RL.TabIndex = 121;
            this.lab_RL.Text = "R/L：";
            // 
            // txt_left_or_right
            // 
            this.txt_left_or_right.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_left_or_right.Location = new System.Drawing.Point(662, 23);
            this.txt_left_or_right.Name = "txt_left_or_right";
            this.txt_left_or_right.Size = new System.Drawing.Size(141, 33);
            this.txt_left_or_right.TabIndex = 3;
            // 
            // lab_ART
            // 
            this.lab_ART.AutoSize = true;
            this.lab_ART.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ART.Location = new System.Drawing.Point(333, 27);
            this.lab_ART.Name = "lab_ART";
            this.lab_ART.Size = new System.Drawing.Size(67, 25);
            this.lab_ART.TabIndex = 119;
            this.lab_ART.Text = "ART：";
            // 
            // txt_prod_no
            // 
            this.txt_prod_no.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_prod_no.Location = new System.Drawing.Point(403, 23);
            this.txt_prod_no.Name = "txt_prod_no";
            this.txt_prod_no.Size = new System.Drawing.Size(141, 33);
            this.txt_prod_no.TabIndex = 2;
            // 
            // btn_Select
            // 
            this.btn_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Select.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Select.Location = new System.Drawing.Point(808, 23);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(85, 33);
            this.btn_Select.TabIndex = 4;
            this.btn_Select.Text = "搜索";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // lab_PO
            // 
            this.lab_PO.AutoSize = true;
            this.lab_PO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_PO.Location = new System.Drawing.Point(33, 27);
            this.lab_PO.Name = "lab_PO";
            this.lab_PO.Size = new System.Drawing.Size(96, 25);
            this.lab_PO.TabIndex = 115;
            this.lab_PO.Text = "PO单号：";
            // 
            // txt_po_order
            // 
            this.txt_po_order.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_po_order.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_po_order.Location = new System.Drawing.Point(131, 23);
            this.txt_po_order.Name = "txt_po_order";
            this.txt_po_order.Size = new System.Drawing.Size(141, 33);
            this.txt_po_order.TabIndex = 1;
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
            this.splitContainer2.Size = new System.Drawing.Size(945, 346);
            this.splitContainer2.SplitterDistance = 286;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.inspect_no,
            this.po_order,
            this.prod_no,
            this.shoe_no,
            this.code_number,
            this.left_or_right,
            this.productionline_no,
            this.productionline_name,
            this.handle_way,
            this.handle_result});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(945, 286);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(221, 3);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.Frozen = true;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem3.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem3.Name = "DETAIL";
            dataGridViewOperationItem3.Text = "DETAIL";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem3);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
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
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem2.Name = "DELETE";
            dataGridViewOperationItem2.Text = "DELETE";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // inspect_no
            // 
            this.inspect_no.HeaderText = "检验单号";
            this.inspect_no.Name = "inspect_no";
            this.inspect_no.Visible = false;
            this.inspect_no.Width = 99;
            // 
            // po_order
            // 
            this.po_order.HeaderText = "PO单号";
            this.po_order.Name = "po_order";
            this.po_order.Width = 90;
            // 
            // prod_no
            // 
            this.prod_no.HeaderText = "ART";
            this.prod_no.Name = "prod_no";
            this.prod_no.Width = 65;
            // 
            // shoe_no
            // 
            this.shoe_no.HeaderText = "鞋型";
            this.shoe_no.Name = "shoe_no";
            this.shoe_no.Width = 67;
            // 
            // code_number
            // 
            this.code_number.HeaderText = "码数";
            this.code_number.Name = "code_number";
            this.code_number.Width = 67;
            // 
            // left_or_right
            // 
            this.left_or_right.HeaderText = "R/L";
            this.left_or_right.Name = "left_or_right";
            this.left_or_right.Width = 60;
            // 
            // productionline_no
            // 
            this.productionline_no.HeaderText = "产线代号";
            this.productionline_no.Name = "productionline_no";
            this.productionline_no.Visible = false;
            this.productionline_no.Width = 99;
            // 
            // productionline_name
            // 
            this.productionline_name.HeaderText = "产线名称";
            this.productionline_name.Name = "productionline_name";
            this.productionline_name.Width = 99;
            // 
            // handle_way
            // 
            this.handle_way.HeaderText = "处理方法";
            this.handle_way.Name = "handle_way";
            this.handle_way.Width = 99;
            // 
            // handle_result
            // 
            this.handle_result.HeaderText = "处理结果";
            this.handle_result.Name = "handle_result";
            this.handle_result.Width = 99;
            // 
            // F_QCM_Metalinspection_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 497);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Metalinspection_Main";
            this.Text = "金属检验";
            this.Load += new System.EventHandler(this.F_QCM_Metalinspection_Main_Load);
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
        private System.Windows.Forms.Label lab_RL;
        private System.Windows.Forms.TextBox txt_left_or_right;
        private System.Windows.Forms.Label lab_ART;
        private System.Windows.Forms.TextBox txt_prod_no;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Label lab_PO;
        private System.Windows.Forms.TextBox txt_po_order;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspect_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn po_order;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn shoe_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn code_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn left_or_right;
        private System.Windows.Forms.DataGridViewTextBoxColumn productionline_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn productionline_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn handle_way;
        private System.Windows.Forms.DataGridViewTextBoxColumn handle_result;
    }
}