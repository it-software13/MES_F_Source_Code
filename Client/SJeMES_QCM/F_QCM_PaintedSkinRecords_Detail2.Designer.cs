
namespace SJeMES_QCM
{
    partial class F_QCM_PaintedSkinRecords_Detail2
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lab_gjzlxs = new System.Windows.Forms.Label();
            this.lab_gjzlxs2 = new System.Windows.Forms.Label();
            this.level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PANIT_LEVEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACTUAL_AREA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XISHU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.multiple = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coefficient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
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
            this.level,
            this.PANIT_LEVEL,
            this.ACTUAL_AREA,
            this.XISHU,
            this.multiple,
            this.coefficient});
            this.dataGridView1.Location = new System.Drawing.Point(11, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(601, 304);
            this.dataGridView1.TabIndex = 34;
            // 
            // lab_gjzlxs
            // 
            this.lab_gjzlxs.AutoSize = true;
            this.lab_gjzlxs.BackColor = System.Drawing.Color.White;
            this.lab_gjzlxs.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_gjzlxs.Location = new System.Drawing.Point(373, 380);
            this.lab_gjzlxs.Name = "lab_gjzlxs";
            this.lab_gjzlxs.Size = new System.Drawing.Size(122, 21);
            this.lab_gjzlxs.TabIndex = 35;
            this.lab_gjzlxs.Text = "购进质量系数：";
            // 
            // lab_gjzlxs2
            // 
            this.lab_gjzlxs2.AutoSize = true;
            this.lab_gjzlxs2.BackColor = System.Drawing.Color.White;
            this.lab_gjzlxs2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_gjzlxs2.Location = new System.Drawing.Point(501, 380);
            this.lab_gjzlxs2.Name = "lab_gjzlxs2";
            this.lab_gjzlxs2.Size = new System.Drawing.Size(106, 21);
            this.lab_gjzlxs2.TabIndex = 35;
            this.lab_gjzlxs2.Text = "购进质量系数";
            // 
            // level
            // 
            this.level.HeaderText = "level";
            this.level.Name = "level";
            this.level.ReadOnly = true;
            this.level.Visible = false;
            // 
            // PANIT_LEVEL
            // 
            this.PANIT_LEVEL.HeaderText = "等级";
            this.PANIT_LEVEL.Name = "PANIT_LEVEL";
            this.PANIT_LEVEL.ReadOnly = true;
            this.PANIT_LEVEL.Width = 150;
            // 
            // ACTUAL_AREA
            // 
            this.ACTUAL_AREA.HeaderText = "数量（尺）";
            this.ACTUAL_AREA.Name = "ACTUAL_AREA";
            this.ACTUAL_AREA.ReadOnly = true;
            this.ACTUAL_AREA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ACTUAL_AREA.Width = 150;
            // 
            // XISHU
            // 
            this.XISHU.HeaderText = "系数";
            this.XISHU.Name = "XISHU";
            this.XISHU.ReadOnly = true;
            this.XISHU.Width = 150;
            // 
            // multiple
            // 
            this.multiple.HeaderText = "倍数";
            this.multiple.Name = "multiple";
            this.multiple.ReadOnly = true;
            this.multiple.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.multiple.Width = 150;
            // 
            // coefficient
            // 
            this.coefficient.HeaderText = "coefficient";
            this.coefficient.Name = "coefficient";
            this.coefficient.ReadOnly = true;
            this.coefficient.Visible = false;
            // 
            // F_QCM_PaintedSkinRecords_Detail2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 422);
            this.Controls.Add(this.lab_gjzlxs2);
            this.Controls.Add(this.lab_gjzlxs);
            this.Controls.Add(this.dataGridView1);
            this.Name = "F_QCM_PaintedSkinRecords_Detail2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "物料信息";
            this.Load += new System.EventHandler(this.F_QCM_PaintedSkinRecords_Detail2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lab_gjzlxs;
        private System.Windows.Forms.Label lab_gjzlxs2;
        private System.Windows.Forms.DataGridViewTextBoxColumn level;
        private System.Windows.Forms.DataGridViewTextBoxColumn PANIT_LEVEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACTUAL_AREA;
        private System.Windows.Forms.DataGridViewTextBoxColumn XISHU;
        private System.Windows.Forms.DataGridViewTextBoxColumn multiple;
        private System.Windows.Forms.DataGridViewTextBoxColumn coefficient;
    }
}