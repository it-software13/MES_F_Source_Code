
namespace SJeMES_BDM
{
    partial class F_BDM_QualityStandard_List
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_BDM_QualityStandard_List));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem5 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem6 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labdqfl = new System.Windows.Forms.Label();
            this.lab3 = new System.Windows.Forms.Label();
            this.btnnewfl = new System.Windows.Forms.Button();
            this.lab_flmc = new System.Windows.Forms.Label();
            this.btnexport = new System.Windows.Forms.Button();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.btnupload = new System.Windows.Forms.Button();
            this.btnselect = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.did = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.分类代号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.分类名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.二级分类代号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.二级分类名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
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
            this.splitContainer1.Location = new System.Drawing.Point(2, 66);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labdqfl);
            this.splitContainer1.Panel1.Controls.Add(this.lab3);
            this.splitContainer1.Panel1.Controls.Add(this.btnnewfl);
            this.splitContainer1.Panel1.Controls.Add(this.lab_flmc);
            this.splitContainer1.Panel1.Controls.Add(this.btnexport);
            this.splitContainer1.Panel1.Controls.Add(this.txt1);
            this.splitContainer1.Panel1.Controls.Add(this.btnupload);
            this.splitContainer1.Panel1.Controls.Add(this.btnselect);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 549);
            this.splitContainer1.SplitterDistance = 122;
            this.splitContainer1.TabIndex = 20;
            // 
            // labdqfl
            // 
            this.labdqfl.AutoSize = true;
            this.labdqfl.Location = new System.Drawing.Point(54, 36);
            this.labdqfl.Name = "labdqfl";
            this.labdqfl.Size = new System.Drawing.Size(78, 21);
            this.labdqfl.TabIndex = 19;
            this.labdqfl.Text = "当前分类:";
            // 
            // lab3
            // 
            this.lab3.AutoSize = true;
            this.lab3.Location = new System.Drawing.Point(141, 36);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(26, 21);
            this.lab3.TabIndex = 20;
            this.lab3.Text = "空";
            // 
            // btnnewfl
            // 
            this.btnnewfl.Location = new System.Drawing.Point(383, 69);
            this.btnnewfl.Name = "btnnewfl";
            this.btnnewfl.Size = new System.Drawing.Size(94, 33);
            this.btnnewfl.TabIndex = 26;
            this.btnnewfl.Text = "新建分类";
            this.btnnewfl.UseVisualStyleBackColor = true;
            this.btnnewfl.Click += new System.EventHandler(this.btn5_Click);
            // 
            // lab_flmc
            // 
            this.lab_flmc.AutoSize = true;
            this.lab_flmc.Location = new System.Drawing.Point(54, 75);
            this.lab_flmc.Name = "lab_flmc";
            this.lab_flmc.Size = new System.Drawing.Size(78, 21);
            this.lab_flmc.TabIndex = 21;
            this.lab_flmc.Text = "分类名称:";
            // 
            // btnexport
            // 
            this.btnexport.Location = new System.Drawing.Point(622, 68);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(94, 33);
            this.btnexport.TabIndex = 25;
            this.btnexport.Text = "导入数据";
            this.btnexport.UseVisualStyleBackColor = true;
            this.btnexport.Visible = false;
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(134, 72);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(116, 29);
            this.txt1.TabIndex = 22;
            // 
            // btnupload
            // 
            this.btnupload.Location = new System.Drawing.Point(516, 68);
            this.btnupload.Name = "btnupload";
            this.btnupload.Size = new System.Drawing.Size(94, 33);
            this.btnupload.TabIndex = 24;
            this.btnupload.Text = "下载模板";
            this.btnupload.UseVisualStyleBackColor = true;
            this.btnupload.Visible = false;
            // 
            // btnselect
            // 
            this.btnselect.Location = new System.Drawing.Point(260, 68);
            this.btnselect.Name = "btnselect";
            this.btnselect.Size = new System.Drawing.Size(94, 33);
            this.btnselect.TabIndex = 23;
            this.btnselect.Text = "搜索";
            this.btnselect.UseVisualStyleBackColor = true;
            this.btnselect.Click += new System.EventHandler(this.btn2_Click);
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
            this.splitContainer2.Size = new System.Drawing.Size(1050, 423);
            this.splitContainer2.SplitterDistance = 364;
            this.splitContainer2.TabIndex = 21;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.序号,
            this.did,
            this.分类代号,
            this.分类名称,
            this.二级分类代号,
            this.二级分类名称,
            this.备注});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1050, 364);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem1.Name = "UPDATE";
            dataGridViewOperationItem1.Text = "修改";
            dataGridViewOperationItem2.Image = global::SJeMES_BDM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem2.Name = "DETAIL";
            dataGridViewOperationItem2.Text = "编辑";
            dataGridViewOperationItem3.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem3.Name = "DELETE";
            dataGridViewOperationItem3.Text = "删除";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.Items.Add(dataGridViewOperationItem3);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.ReadOnly = true;
            // 
            // 序号
            // 
            this.序号.DataPropertyName = "xh";
            this.序号.HeaderText = "序号";
            this.序号.Name = "序号";
            this.序号.ReadOnly = true;
            this.序号.Width = 67;
            // 
            // did
            // 
            this.did.DataPropertyName = "id";
            this.did.HeaderText = "did";
            this.did.Name = "did";
            this.did.ReadOnly = true;
            this.did.Visible = false;
            // 
            // 分类代号
            // 
            this.分类代号.DataPropertyName = "quality_category_no";
            this.分类代号.HeaderText = "分类代号";
            this.分类代号.Name = "分类代号";
            this.分类代号.ReadOnly = true;
            this.分类代号.Width = 99;
            // 
            // 分类名称
            // 
            this.分类名称.DataPropertyName = "quality_category_name";
            this.分类名称.HeaderText = "分类名称";
            this.分类名称.Name = "分类名称";
            this.分类名称.ReadOnly = true;
            this.分类名称.Width = 99;
            // 
            // 二级分类代号
            // 
            this.二级分类代号.DataPropertyName = "secondary_category_no";
            this.二级分类代号.HeaderText = "二级分类代号";
            this.二级分类代号.Name = "二级分类代号";
            this.二级分类代号.ReadOnly = true;
            this.二级分类代号.Width = 131;
            // 
            // 二级分类名称
            // 
            this.二级分类名称.DataPropertyName = "secondary_category_name";
            this.二级分类名称.HeaderText = "二级分类名称";
            this.二级分类名称.Name = "二级分类名称";
            this.二级分类名称.ReadOnly = true;
            this.二级分类名称.Width = 131;
            // 
            // 备注
            // 
            this.备注.DataPropertyName = "REMARKS";
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            this.备注.ReadOnly = true;
            this.备注.Width = 67;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(53, -2);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(995, 55);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem4.Image = global::SJeMES_BDM.Properties.Resources.ic_update_24;
            dataGridViewOperationItem4.Name = "Update";
            dataGridViewOperationItem4.Text = "修改";
            dataGridViewOperationItem5.Image = global::SJeMES_BDM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem5.Name = "Detail";
            dataGridViewOperationItem5.Text = "编辑";
            dataGridViewOperationItem6.Image = global::SJeMES_BDM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem6.Name = "Delete";
            dataGridViewOperationItem6.Text = "删除";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem5);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem6);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // F_BDM_QualityStandard_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 615);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_BDM_QualityStandard_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通用品质二级分类";
            this.Load += new System.EventHandler(this.F_BDM_QualityStandard_List_Load);
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
        private System.Windows.Forms.Label labdqfl;
        private System.Windows.Forms.Label lab3;
        private System.Windows.Forms.Button btnnewfl;
        private System.Windows.Forms.Label lab_flmc;
        private System.Windows.Forms.Button btnexport;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Button btnupload;
        private System.Windows.Forms.Button btnselect;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn did;
        private System.Windows.Forms.DataGridViewTextBoxColumn 分类代号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 分类名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 二级分类代号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 二级分类名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
    }
}