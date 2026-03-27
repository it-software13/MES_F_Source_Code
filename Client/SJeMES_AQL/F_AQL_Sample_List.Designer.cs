
namespace SJeMES_AQL
{
    partial class F_AQL_Sample_List
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
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewEx1 = new SJeMES_Control_Library.DataGridViewEx();
            this.ART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POSITION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME_CN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME_EN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.process_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(570, 545);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(607, 49);
            this.pageControl1.TabIndex = 2;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.AllowUserToDeleteRows = false;
            this.dataGridViewEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEx1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEx1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ART,
            this.STAGE,
            this.ITEM_NO,
            this.POSITION,
            this.NAME_CN,
            this.NAME_EN,
            this.process_desc,
            this.remark});
            this.dataGridViewEx1.Location = new System.Drawing.Point(0, 62);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.ReadOnly = true;
            this.dataGridViewEx1.RowTemplate.Height = 23;
            this.dataGridViewEx1.Size = new System.Drawing.Size(1177, 476);
            this.dataGridViewEx1.TabIndex = 1;
            // 
            // ART
            // 
            this.ART.HeaderText = "ART_NO";
            this.ART.Name = "ART";
            this.ART.ReadOnly = true;
            // 
            // STAGE
            // 
            this.STAGE.HeaderText = "阶段";
            this.STAGE.Name = "STAGE";
            this.STAGE.ReadOnly = true;
            // 
            // ITEM_NO
            // 
            this.ITEM_NO.HeaderText = "料号";
            this.ITEM_NO.Name = "ITEM_NO";
            this.ITEM_NO.ReadOnly = true;
            // 
            // POSITION
            // 
            this.POSITION.HeaderText = "部位";
            this.POSITION.Name = "POSITION";
            this.POSITION.ReadOnly = true;
            // 
            // NAME_CN
            // 
            this.NAME_CN.HeaderText = "中文名称";
            this.NAME_CN.Name = "NAME_CN";
            this.NAME_CN.ReadOnly = true;
            // 
            // NAME_EN
            // 
            this.NAME_EN.HeaderText = "英文名称";
            this.NAME_EN.Name = "NAME_EN";
            this.NAME_EN.ReadOnly = true;
            // 
            // process_desc
            // 
            this.process_desc.HeaderText = "工艺描述";
            this.process_desc.Name = "process_desc";
            this.process_desc.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // F_AQL_Sample_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 597);
            this.Controls.Add(this.pageControl1);
            this.Controls.Add(this.dataGridViewEx1);
            this.Name = "F_AQL_Sample_List";
            this.Text = "样品单";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SJeMES_Control_Library.DataGridViewEx dataGridViewEx1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ART;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAGE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn POSITION;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME_CN;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME_EN;
        private System.Windows.Forms.DataGridViewTextBoxColumn process_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
    }
}