
namespace SJeMES_QCM
{
    partial class F_QCM_VampQualityAudit_List_D
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_VampQualityAudit_List_D));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labdate = new System.Windows.Forms.Label();
            this.labcs = new System.Windows.Forms.Label();
            this.lab_date = new System.Windows.Forms.Label();
            this.lab_vend = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUALITY_ITEM_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUALITY_ITEM_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUALITY_TYPE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUALITY_TYPE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BASE_SOCRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Panel1.Controls.Add(this.labdate);
            this.splitContainer1.Panel1.Controls.Add(this.labcs);
            this.splitContainer1.Panel1.Controls.Add(this.lab_date);
            this.splitContainer1.Panel1.Controls.Add(this.lab_vend);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 387);
            this.splitContainer1.SplitterDistance = 41;
            this.splitContainer1.TabIndex = 0;
            // 
            // labdate
            // 
            this.labdate.AutoSize = true;
            this.labdate.Location = new System.Drawing.Point(456, 10);
            this.labdate.Name = "labdate";
            this.labdate.Size = new System.Drawing.Size(55, 21);
            this.labdate.TabIndex = 3;
            this.labdate.Text = "label4";
            // 
            // labcs
            // 
            this.labcs.AutoSize = true;
            this.labcs.Location = new System.Drawing.Point(92, 10);
            this.labcs.Name = "labcs";
            this.labcs.Size = new System.Drawing.Size(55, 21);
            this.labcs.TabIndex = 2;
            this.labcs.Text = "label3";
            // 
            // lab_date
            // 
            this.lab_date.AutoSize = true;
            this.lab_date.Location = new System.Drawing.Point(383, 10);
            this.lab_date.Name = "lab_date";
            this.lab_date.Size = new System.Drawing.Size(46, 21);
            this.lab_date.TabIndex = 1;
            this.lab_date.Text = "日期:";
            // 
            // lab_vend
            // 
            this.lab_vend.AutoSize = true;
            this.lab_vend.Location = new System.Drawing.Point(33, 10);
            this.lab_vend.Name = "lab_vend";
            this.lab_vend.Size = new System.Drawing.Size(46, 21);
            this.lab_vend.TabIndex = 0;
            this.lab_vend.Text = "厂商:";
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
            this.operation1,
            this.TYPE,
            this.GUID,
            this.QUALITY_ITEM_CODE,
            this.QUALITY_ITEM_NAME,
            this.QUALITY_TYPE_CODE,
            this.QUALITY_TYPE_NAME,
            this.BASE_SOCRE,
            this.SOCRE});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 33;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(800, 342);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // operation1
            // 
            this.operation1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation1.Description = null;
            this.operation1.HeaderText = "照片记录";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "SELECT";
            dataGridViewOperationItem1.Text = "查看图片";
            this.operation1.Items.Add(dataGridViewOperationItem1);
            this.operation1.Name = "operation1";
            this.operation1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation1.OverflowImage")));
            this.operation1.ReadOnly = true;
            this.operation1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // TYPE
            // 
            this.TYPE.HeaderText = "type";
            this.TYPE.Name = "TYPE";
            this.TYPE.ReadOnly = true;
            this.TYPE.Visible = false;
            this.TYPE.Width = 68;
            // 
            // GUID
            // 
            this.GUID.HeaderText = "guid";
            this.GUID.Name = "GUID";
            this.GUID.ReadOnly = true;
            this.GUID.Visible = false;
            this.GUID.Width = 69;
            // 
            // QUALITY_ITEM_CODE
            // 
            this.QUALITY_ITEM_CODE.HeaderText = "检验项编号";
            this.QUALITY_ITEM_CODE.Name = "QUALITY_ITEM_CODE";
            this.QUALITY_ITEM_CODE.ReadOnly = true;
            this.QUALITY_ITEM_CODE.Visible = false;
            this.QUALITY_ITEM_CODE.Width = 115;
            // 
            // QUALITY_ITEM_NAME
            // 
            this.QUALITY_ITEM_NAME.HeaderText = "检验项名称";
            this.QUALITY_ITEM_NAME.Name = "QUALITY_ITEM_NAME";
            this.QUALITY_ITEM_NAME.ReadOnly = true;
            this.QUALITY_ITEM_NAME.Width = 115;
            // 
            // QUALITY_TYPE_CODE
            // 
            this.QUALITY_TYPE_CODE.HeaderText = "检验分类代号";
            this.QUALITY_TYPE_CODE.Name = "QUALITY_TYPE_CODE";
            this.QUALITY_TYPE_CODE.ReadOnly = true;
            this.QUALITY_TYPE_CODE.Visible = false;
            this.QUALITY_TYPE_CODE.Width = 131;
            // 
            // QUALITY_TYPE_NAME
            // 
            this.QUALITY_TYPE_NAME.HeaderText = "检验分类名称";
            this.QUALITY_TYPE_NAME.Name = "QUALITY_TYPE_NAME";
            this.QUALITY_TYPE_NAME.ReadOnly = true;
            this.QUALITY_TYPE_NAME.Width = 131;
            // 
            // BASE_SOCRE
            // 
            this.BASE_SOCRE.HeaderText = "检验项分数";
            this.BASE_SOCRE.Name = "BASE_SOCRE";
            this.BASE_SOCRE.ReadOnly = true;
            this.BASE_SOCRE.Width = 115;
            // 
            // SOCRE
            // 
            this.SOCRE.HeaderText = "得分";
            this.SOCRE.Name = "SOCRE";
            this.SOCRE.ReadOnly = true;
            this.SOCRE.Width = 67;
            // 
            // F_QCM_VampQualityAudit_List_D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_VampQualityAudit_List_D";
            this.Text = "品质审核历史列表明细";
            this.Load += new System.EventHandler(this.F_QCM_VampQualityAudit_List_D_Load);
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
        private System.Windows.Forms.Label labdate;
        private System.Windows.Forms.Label labcs;
        private System.Windows.Forms.Label lab_date;
        private System.Windows.Forms.Label lab_vend;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUALITY_ITEM_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUALITY_ITEM_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUALITY_TYPE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUALITY_TYPE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn BASE_SOCRE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOCRE;
    }
}