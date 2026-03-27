
namespace SJeMES_QCM
{
    partial class F_QCM_Broken_Needle_Main
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_Broken_Needle_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comProduction_line = new System.Windows.Forms.ComboBox();
            this.txtplant = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnselect = new System.Windows.Forms.Button();
            this.lab_ProdLine = new System.Windows.Forms.Label();
            this.lab_vend = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Production_line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recipients_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inuse_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surplus_needlenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Broken_needle_num = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comProduction_line);
            this.splitContainer1.Panel1.Controls.Add(this.txtplant);
            this.splitContainer1.Panel1.Controls.Add(this.btnEdit);
            this.splitContainer1.Panel1.Controls.Add(this.btnselect);
            this.splitContainer1.Panel1.Controls.Add(this.lab_ProdLine);
            this.splitContainer1.Panel1.Controls.Add(this.lab_vend);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1190, 623);
            this.splitContainer1.SplitterDistance = 61;
            this.splitContainer1.TabIndex = 0;
            // 
            // comProduction_line
            // 
            this.comProduction_line.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comProduction_line.FormattingEnabled = true;
            this.comProduction_line.Location = new System.Drawing.Point(496, 18);
            this.comProduction_line.Name = "comProduction_line";
            this.comProduction_line.Size = new System.Drawing.Size(177, 29);
            this.comProduction_line.TabIndex = 5;
            // 
            // txtplant
            // 
            this.txtplant.Location = new System.Drawing.Point(91, 20);
            this.txtplant.Name = "txtplant";
            this.txtplant.Size = new System.Drawing.Size(177, 29);
            this.txtplant.TabIndex = 4;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(950, 18);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(109, 30);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "新增";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnselect
            // 
            this.btnselect.Location = new System.Drawing.Point(789, 18);
            this.btnselect.Name = "btnselect";
            this.btnselect.Size = new System.Drawing.Size(109, 30);
            this.btnselect.TabIndex = 2;
            this.btnselect.Text = "搜索";
            this.btnselect.UseVisualStyleBackColor = true;
            this.btnselect.Click += new System.EventHandler(this.btnselect_Click);
            // 
            // lab_ProdLine
            // 
            this.lab_ProdLine.AutoSize = true;
            this.lab_ProdLine.Location = new System.Drawing.Point(435, 23);
            this.lab_ProdLine.Name = "lab_ProdLine";
            this.lab_ProdLine.Size = new System.Drawing.Size(42, 21);
            this.lab_ProdLine.TabIndex = 1;
            this.lab_ProdLine.Text = "产线";
            // 
            // lab_vend
            // 
            this.lab_vend.AutoSize = true;
            this.lab_vend.Location = new System.Drawing.Point(30, 23);
            this.lab_vend.Name = "lab_vend";
            this.lab_vend.Size = new System.Drawing.Size(42, 21);
            this.lab_vend.TabIndex = 0;
            this.lab_vend.Text = "厂区";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer2.Size = new System.Drawing.Size(1190, 558);
            this.splitContainer2.SplitterDistance = 497;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.number,
            this.plant,
            this.Production_line,
            this.recipients_num,
            this.inuse_num,
            this.surplus_needlenum,
            this.Broken_needle_num});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1190, 497);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(469, 3);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem2.Name = "SELECT";
            dataGridViewOperationItem2.Text = "查看";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem2);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            this.dataGridViewOperationColumn1.ReadOnly = true;
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "SELECT";
            dataGridViewOperationItem1.Text = "查看";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // number
            // 
            this.number.HeaderText = "序号";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 67;
            // 
            // plant
            // 
            this.plant.HeaderText = "产线";
            this.plant.Name = "plant";
            this.plant.ReadOnly = true;
            this.plant.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.plant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.plant.Width = 48;
            // 
            // Production_line
            // 
            this.Production_line.HeaderText = "厂区";
            this.Production_line.Name = "Production_line";
            this.Production_line.ReadOnly = true;
            this.Production_line.Width = 67;
            // 
            // recipients_num
            // 
            this.recipients_num.HeaderText = "领用数量";
            this.recipients_num.Name = "recipients_num";
            this.recipients_num.ReadOnly = true;
            this.recipients_num.Width = 99;
            // 
            // inuse_num
            // 
            this.inuse_num.HeaderText = "在用数量";
            this.inuse_num.Name = "inuse_num";
            this.inuse_num.ReadOnly = true;
            this.inuse_num.Width = 99;
            // 
            // surplus_needlenum
            // 
            this.surplus_needlenum.HeaderText = "剩余针数";
            this.surplus_needlenum.Name = "surplus_needlenum";
            this.surplus_needlenum.ReadOnly = true;
            this.surplus_needlenum.Width = 99;
            // 
            // Broken_needle_num
            // 
            this.Broken_needle_num.HeaderText = "断针数量";
            this.Broken_needle_num.Name = "Broken_needle_num";
            this.Broken_needle_num.ReadOnly = true;
            this.Broken_needle_num.Width = 99;
            // 
            // F_QCM_Broken_Needle_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 685);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Broken_Needle_Main";
            this.Text = "断针管控";
            this.Load += new System.EventHandler(this.F_QCM_Broken_Needle_Main_Load);
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
        private System.Windows.Forms.ComboBox comProduction_line;
        private System.Windows.Forms.TextBox txtplant;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnselect;
        private System.Windows.Forms.Label lab_ProdLine;
        private System.Windows.Forms.Label lab_vend;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn plant;
        private System.Windows.Forms.DataGridViewTextBoxColumn Production_line;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipients_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn inuse_num;
        private System.Windows.Forms.DataGridViewTextBoxColumn surplus_needlenum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Broken_needle_num;
    }
}