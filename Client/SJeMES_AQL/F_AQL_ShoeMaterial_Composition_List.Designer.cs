
namespace SJeMES_AQL
{
    partial class F_AQL_ShoeMaterial_Composition_List
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.ZJJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAKTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MIIDS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUM_TOTAL_CHECK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.颜色代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.鞋面颜色名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_Change_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZKFFZR_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZBM_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSTATUS_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOL1_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOL2_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOL3_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOL4_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOL5_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOL6_NM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ZJJ,
            this.MAKTX,
            this.MIIDS,
            this.SUM_TOTAL,
            this.SUM_TOTAL_CHECK,
            this.颜色代码,
            this.鞋面颜色名称,
            this.Date_Change_date,
            this.ZKFFZR_NM,
            this.ZBM_X,
            this.ZSTATUS_NM,
            this.ZCOL1_NM,
            this.ZCOL2_NM,
            this.ZCOL3_NM,
            this.ZCOL4_NM,
            this.ZCOL5_NM,
            this.ZCOL6_NM});
            this.dgvData.Location = new System.Drawing.Point(4, 66);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(1415, 690);
            this.dgvData.TabIndex = 1;
            // 
            // ZJJ
            // 
            this.ZJJ.HeaderText = "季节";
            this.ZJJ.Name = "ZJJ";
            this.ZJJ.ReadOnly = true;
            // 
            // MAKTX
            // 
            this.MAKTX.HeaderText = "鞋型";
            this.MAKTX.Name = "MAKTX";
            this.MAKTX.ReadOnly = true;
            // 
            // MIIDS
            // 
            this.MIIDS.HeaderText = "材料信息降序排列";
            this.MIIDS.Name = "MIIDS";
            this.MIIDS.ReadOnly = true;
            // 
            // SUM_TOTAL
            // 
            this.SUM_TOTAL.HeaderText = "占比";
            this.SUM_TOTAL.Name = "SUM_TOTAL";
            this.SUM_TOTAL.ReadOnly = true;
            // 
            // SUM_TOTAL_CHECK
            // 
            this.SUM_TOTAL_CHECK.HeaderText = "100%检查材料信息";
            this.SUM_TOTAL_CHECK.Name = "SUM_TOTAL_CHECK";
            this.SUM_TOTAL_CHECK.ReadOnly = true;
            // 
            // 颜色代码
            // 
            this.颜色代码.HeaderText = "颜色代码";
            this.颜色代码.Name = "颜色代码";
            this.颜色代码.ReadOnly = true;
            // 
            // 鞋面颜色名称
            // 
            this.鞋面颜色名称.HeaderText = "鞋面颜色名称";
            this.鞋面颜色名称.Name = "鞋面颜色名称";
            this.鞋面颜色名称.ReadOnly = true;
            // 
            // Date_Change_date
            // 
            this.Date_Change_date.HeaderText = "变更日期";
            this.Date_Change_date.Name = "Date_Change_date";
            this.Date_Change_date.ReadOnly = true;
            // 
            // ZKFFZR_NM
            // 
            this.ZKFFZR_NM.HeaderText = "开发员";
            this.ZKFFZR_NM.Name = "ZKFFZR_NM";
            this.ZKFFZR_NM.ReadOnly = true;
            // 
            // ZBM_X
            // 
            this.ZBM_X.HeaderText = "部门";
            this.ZBM_X.Name = "ZBM_X";
            this.ZBM_X.ReadOnly = true;
            // 
            // ZSTATUS_NM
            // 
            this.ZSTATUS_NM.HeaderText = "状况";
            this.ZSTATUS_NM.Name = "ZSTATUS_NM";
            this.ZSTATUS_NM.ReadOnly = true;
            // 
            // ZCOL1_NM
            // 
            this.ZCOL1_NM.HeaderText = "脚踝包裹";
            this.ZCOL1_NM.Name = "ZCOL1_NM";
            this.ZCOL1_NM.ReadOnly = true;
            // 
            // ZCOL2_NM
            // 
            this.ZCOL2_NM.HeaderText = "底部材料";
            this.ZCOL2_NM.Name = "ZCOL2_NM";
            this.ZCOL2_NM.ReadOnly = true;
            // 
            // ZCOL3_NM
            // 
            this.ZCOL3_NM.HeaderText = "内里";
            this.ZCOL3_NM.Name = "ZCOL3_NM";
            this.ZCOL3_NM.ReadOnly = true;
            // 
            // ZCOL4_NM
            // 
            this.ZCOL4_NM.HeaderText = "鞋舌尺码标位置";
            this.ZCOL4_NM.Name = "ZCOL4_NM";
            this.ZCOL4_NM.ReadOnly = true;
            // 
            // ZCOL5_NM
            // 
            this.ZCOL5_NM.HeaderText = "是否被硫化";
            this.ZCOL5_NM.Name = "ZCOL5_NM";
            this.ZCOL5_NM.ReadOnly = true;
            // 
            // ZCOL6_NM
            // 
            this.ZCOL6_NM.HeaderText = "鞋舌尺码标位置";
            this.ZCOL6_NM.Name = "ZCOL6_NM";
            this.ZCOL6_NM.ReadOnly = true;
            // 
            // F_AQL_ShoeMaterial_Composition_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1420, 761);
            this.Controls.Add(this.dgvData);
            this.Name = "F_AQL_ShoeMaterial_Composition_List";
            this.Text = "查看鞋材成分数据";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZJJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAKTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn MIIDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUM_TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUM_TOTAL_CHECK;
        private System.Windows.Forms.DataGridViewTextBoxColumn 颜色代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 鞋面颜色名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_Change_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZKFFZR_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZBM_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSTATUS_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOL1_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOL2_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOL3_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOL4_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOL5_NM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOL6_NM;
    }
}