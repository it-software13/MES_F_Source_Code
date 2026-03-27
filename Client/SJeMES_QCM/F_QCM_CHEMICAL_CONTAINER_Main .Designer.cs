
namespace SJeMES_QCM
{
    partial class F_QCM_ComplianceMangement_Main
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Select = new System.Windows.Forms.Button();
            this.txt_CONTAINER_NO = new System.Windows.Forms.TextBox();
            this.txt_CHEMICAL_NAME = new System.Windows.Forms.TextBox();
            this.lab_CONTAINER_NO = new System.Windows.Forms.Label();
            this.lab_CHEMICAL_NAME = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CONTAINER_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHEMICAL_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GLUE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EFFECTIVE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXPIRATION_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_Select);
            this.splitContainer1.Panel1.Controls.Add(this.txt_CONTAINER_NO);
            this.splitContainer1.Panel1.Controls.Add(this.txt_CHEMICAL_NAME);
            this.splitContainer1.Panel1.Controls.Add(this.lab_CONTAINER_NO);
            this.splitContainer1.Panel1.Controls.Add(this.lab_CHEMICAL_NAME);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1072, 586);
            this.splitContainer1.SplitterDistance = 93;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_Select
            // 
            this.btn_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Select.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Select.Location = new System.Drawing.Point(732, 32);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(85, 35);
            this.btn_Select.TabIndex = 106;
            this.btn_Select.Text = "搜索";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // txt_CONTAINER_NO
            // 
            this.txt_CONTAINER_NO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_CONTAINER_NO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_CONTAINER_NO.Location = new System.Drawing.Point(174, 33);
            this.txt_CONTAINER_NO.Name = "txt_CONTAINER_NO";
            this.txt_CONTAINER_NO.Size = new System.Drawing.Size(141, 33);
            this.txt_CONTAINER_NO.TabIndex = 1;
            // 
            // txt_CHEMICAL_NAME
            // 
            this.txt_CHEMICAL_NAME.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_CHEMICAL_NAME.Location = new System.Drawing.Point(474, 33);
            this.txt_CHEMICAL_NAME.Name = "txt_CHEMICAL_NAME";
            this.txt_CHEMICAL_NAME.Size = new System.Drawing.Size(141, 33);
            this.txt_CHEMICAL_NAME.TabIndex = 2;
            // 
            // lab_CONTAINER_NO
            // 
            this.lab_CONTAINER_NO.AutoSize = true;
            this.lab_CONTAINER_NO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_CONTAINER_NO.Location = new System.Drawing.Point(64, 37);
            this.lab_CONTAINER_NO.Name = "lab_CONTAINER_NO";
            this.lab_CONTAINER_NO.Size = new System.Drawing.Size(107, 25);
            this.lab_CONTAINER_NO.TabIndex = 102;
            this.lab_CONTAINER_NO.Text = "容器编号：";
            // 
            // lab_CHEMICAL_NAME
            // 
            this.lab_CHEMICAL_NAME.AutoSize = true;
            this.lab_CHEMICAL_NAME.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_CHEMICAL_NAME.Location = new System.Drawing.Point(365, 37);
            this.lab_CHEMICAL_NAME.Name = "lab_CHEMICAL_NAME";
            this.lab_CHEMICAL_NAME.Size = new System.Drawing.Size(107, 25);
            this.lab_CHEMICAL_NAME.TabIndex = 104;
            this.lab_CHEMICAL_NAME.Text = "材料清单：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.CONTAINER_NO,
            this.CHEMICAL_NAME,
            this.GLUE_TIME,
            this.EFFECTIVE_TIME,
            this.EXPIRATION_TIME});
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
            this.dataGridView1.Size = new System.Drawing.Size(1072, 431);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // CONTAINER_NO
            // 
            this.CONTAINER_NO.HeaderText = "容器编号";
            this.CONTAINER_NO.Name = "CONTAINER_NO";
            // 
            // CHEMICAL_NAME
            // 
            this.CHEMICAL_NAME.HeaderText = "材料清单";
            this.CHEMICAL_NAME.Name = "CHEMICAL_NAME";
            // 
            // GLUE_TIME
            // 
            this.GLUE_TIME.HeaderText = "调胶时间";
            this.GLUE_TIME.Name = "GLUE_TIME";
            // 
            // EFFECTIVE_TIME
            // 
            this.EFFECTIVE_TIME.HeaderText = "有效时间（h）";
            this.EFFECTIVE_TIME.Name = "EFFECTIVE_TIME";
            // 
            // EXPIRATION_TIME
            // 
            this.EXPIRATION_TIME.HeaderText = "到期时间";
            this.EXPIRATION_TIME.Name = "EXPIRATION_TIME";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pageControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 431);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1072, 58);
            this.panel1.TabIndex = 0;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(349, 3);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // F_QCM_ComplianceMangement_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 650);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_ComplianceMangement_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "化学品容器管理看板";
            this.Load += new System.EventHandler(this.F_QCM_CHEMICAL_CONTAINER_Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lab_CONTAINER_NO;
        private System.Windows.Forms.TextBox txt_CONTAINER_NO;
        private System.Windows.Forms.Label lab_CHEMICAL_NAME;
        private System.Windows.Forms.TextBox txt_CHEMICAL_NAME;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONTAINER_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHEMICAL_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn GLUE_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn EFFECTIVE_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXPIRATION_TIME;
        private System.Windows.Forms.Timer timer1;
    }
}