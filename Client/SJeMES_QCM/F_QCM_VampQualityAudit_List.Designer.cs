
namespace SJeMES_QCM
{
    partial class F_QCM_VampQualityAudit_List
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_VampQualityAudit_List));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.datatxt = new System.Windows.Forms.DateTimePicker();
            this.btnselect = new System.Windows.Forms.Button();
            this.txtcs = new System.Windows.Forms.TextBox();
            this.txtdata = new System.Windows.Forms.Label();
            this.labcs = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.SUPPLIERS_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operation1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.SUPPLIERS_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUALITY_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOCRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.datatxt);
            this.splitContainer1.Panel1.Controls.Add(this.btnselect);
            this.splitContainer1.Panel1.Controls.Add(this.txtcs);
            this.splitContainer1.Panel1.Controls.Add(this.txtdata);
            this.splitContainer1.Panel1.Controls.Add(this.labcs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(907, 476);
            this.splitContainer1.SplitterDistance = 64;
            this.splitContainer1.TabIndex = 0;
            // 
            // datatxt
            // 
            this.datatxt.Location = new System.Drawing.Point(371, 21);
            this.datatxt.Name = "datatxt";
            this.datatxt.Size = new System.Drawing.Size(146, 29);
            this.datatxt.TabIndex = 5;
            // 
            // btnselect
            // 
            this.btnselect.Location = new System.Drawing.Point(568, 22);
            this.btnselect.Name = "btnselect";
            this.btnselect.Size = new System.Drawing.Size(88, 28);
            this.btnselect.TabIndex = 4;
            this.btnselect.Text = "搜索";
            this.btnselect.UseVisualStyleBackColor = true;
            this.btnselect.Click += new System.EventHandler(this.btnselect_Click);
            // 
            // txtcs
            // 
            this.txtcs.Location = new System.Drawing.Point(69, 24);
            this.txtcs.Name = "txtcs";
            this.txtcs.Size = new System.Drawing.Size(117, 29);
            this.txtcs.TabIndex = 2;
            // 
            // txtdata
            // 
            this.txtdata.AutoSize = true;
            this.txtdata.Location = new System.Drawing.Point(324, 27);
            this.txtdata.Name = "txtdata";
            this.txtdata.Size = new System.Drawing.Size(42, 21);
            this.txtdata.TabIndex = 1;
            this.txtdata.Text = "日期";
            // 
            // labcs
            // 
            this.labcs.AutoSize = true;
            this.labcs.Location = new System.Drawing.Point(22, 27);
            this.labcs.Name = "labcs";
            this.labcs.Size = new System.Drawing.Size(42, 21);
            this.labcs.TabIndex = 0;
            this.labcs.Text = "厂商";
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
            this.SUPPLIERS_CODE,
            this.GUID,
            this.operation1,
            this.SUPPLIERS_NAME,
            this.QUALITY_DATE,
            this.SOCRE});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(907, 408);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "查看";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem2.Name = null;
            dataGridViewOperationItem2.Text = null;
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem2);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // SUPPLIERS_CODE
            // 
            this.SUPPLIERS_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SUPPLIERS_CODE.HeaderText = "厂商代号";
            this.SUPPLIERS_CODE.Name = "SUPPLIERS_CODE";
            this.SUPPLIERS_CODE.ReadOnly = true;
            this.SUPPLIERS_CODE.Visible = false;
            // 
            // GUID
            // 
            this.GUID.HeaderText = "GUID";
            this.GUID.Name = "GUID";
            this.GUID.ReadOnly = true;
            this.GUID.Visible = false;
            this.GUID.Width = 76;
            // 
            // operation1
            // 
            this.operation1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation1.Description = null;
            this.operation1.HeaderText = "查看";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "SELECT";
            dataGridViewOperationItem1.Text = "查看";
            this.operation1.Items.Add(dataGridViewOperationItem1);
            this.operation1.Name = "operation1";
            this.operation1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation1.OverflowImage")));
            this.operation1.ReadOnly = true;
            this.operation1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // SUPPLIERS_NAME
            // 
            this.SUPPLIERS_NAME.HeaderText = "厂商名称";
            this.SUPPLIERS_NAME.Name = "SUPPLIERS_NAME";
            this.SUPPLIERS_NAME.ReadOnly = true;
            this.SUPPLIERS_NAME.Width = 99;
            // 
            // QUALITY_DATE
            // 
            this.QUALITY_DATE.HeaderText = "日期";
            this.QUALITY_DATE.Name = "QUALITY_DATE";
            this.QUALITY_DATE.ReadOnly = true;
            this.QUALITY_DATE.Width = 67;
            // 
            // SOCRE
            // 
            this.SOCRE.HeaderText = "分数";
            this.SOCRE.Name = "SOCRE";
            this.SOCRE.ReadOnly = true;
            this.SOCRE.Width = 67;
            // 
            // F_QCM_VampQualityAudit_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 539);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_VampQualityAudit_List";
            this.Text = "品质审核列表";
            this.Load += new System.EventHandler(this.F_QCM_VampQualityAudit_List_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnselect;
        private System.Windows.Forms.TextBox txtcs;
        private System.Windows.Forms.Label txtdata;
        private System.Windows.Forms.Label labcs;
        private System.Windows.Forms.DateTimePicker datatxt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLIERS_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPPLIERS_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUALITY_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOCRE;
    }
}