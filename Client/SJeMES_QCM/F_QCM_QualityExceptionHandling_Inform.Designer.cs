
namespace SJeMES_QCM
{
    partial class F_QCM_QualityExceptionHandling_Inform
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_affirm = new System.Windows.Forms.Button();
            this.vbtn_cancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lab_mass = new System.Windows.Forms.Label();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.STAFF_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STAFF_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STAFF_DEPARTMENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.btn_affirm);
            this.splitContainer1.Panel1.Controls.Add(this.vbtn_cancel);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel1.Controls.Add(this.lab_mass);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(910, 569);
            this.splitContainer1.SplitterDistance = 116;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_affirm
            // 
            this.btn_affirm.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_affirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_affirm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_affirm.Location = new System.Drawing.Point(819, 25);
            this.btn_affirm.Name = "btn_affirm";
            this.btn_affirm.Size = new System.Drawing.Size(75, 29);
            this.btn_affirm.TabIndex = 3;
            this.btn_affirm.Text = "确定";
            this.btn_affirm.UseVisualStyleBackColor = true;
            this.btn_affirm.Click += new System.EventHandler(this.button2_Click);
            // 
            // vbtn_cancel
            // 
            this.vbtn_cancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.vbtn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbtn_cancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vbtn_cancel.Location = new System.Drawing.Point(819, 72);
            this.vbtn_cancel.Name = "vbtn_cancel";
            this.vbtn_cancel.Size = new System.Drawing.Size(75, 29);
            this.vbtn_cancel.TabIndex = 2;
            this.vbtn_cancel.Text = "取消";
            this.vbtn_cancel.UseVisualStyleBackColor = true;
            this.vbtn_cancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(779, 76);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // lab_mass
            // 
            this.lab_mass.AutoSize = true;
            this.lab_mass.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_mass.Location = new System.Drawing.Point(12, 2);
            this.lab_mass.Name = "lab_mass";
            this.lab_mass.Size = new System.Drawing.Size(65, 20);
            this.lab_mass.TabIndex = 0;
            this.lab_mass.Text = "批量通知";
            // 
            // pageControl1
            // 
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(258, 406);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 40);
            this.pageControl1.TabIndex = 1;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.STAFF_NO,
            this.STAFF_NAME,
            this.STAFF_DEPARTMENT});
            this.dataGridView1.Location = new System.Drawing.Point(14, 3);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 33;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(883, 397);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // select
            // 
            this.select.HeaderText = "勾选";
            this.select.Name = "select";
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // STAFF_NO
            // 
            this.STAFF_NO.HeaderText = "工号";
            this.STAFF_NO.Name = "STAFF_NO";
            // 
            // STAFF_NAME
            // 
            this.STAFF_NAME.HeaderText = "姓名";
            this.STAFF_NAME.Name = "STAFF_NAME";
            // 
            // STAFF_DEPARTMENT
            // 
            this.STAFF_DEPARTMENT.HeaderText = "职位";
            this.STAFF_DEPARTMENT.Name = "STAFF_DEPARTMENT";
            // 
            // F_QCM_QualityExceptionHandling_Inform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 631);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_QualityExceptionHandling_Inform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "批量通知";
            this.Load += new System.EventHandler(this.F_QCM_QualityExceptionHandling_Inform_Load);
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
        private System.Windows.Forms.Label lab_mass;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAFF_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAFF_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn STAFF_DEPARTMENT;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button vbtn_cancel;
        private System.Windows.Forms.Button btn_affirm;
    }
}