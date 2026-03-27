
namespace SJeMES_QCM
{
    partial class F_QCM_Chemical_information_create
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnselect = new System.Windows.Forms.Button();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.datevalidtime = new System.Windows.Forms.DateTimePicker();
            this.txtchemicals_name = new System.Windows.Forms.TextBox();
            this.lab_Enddate = new System.Windows.Forms.Label();
            this.lab_ChemicalName = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chemicals_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chemicals_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.validtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnselect);
            this.splitContainer1.Panel1.Controls.Add(this.btnsubmit);
            this.splitContainer1.Panel1.Controls.Add(this.datevalidtime);
            this.splitContainer1.Panel1.Controls.Add(this.txtchemicals_name);
            this.splitContainer1.Panel1.Controls.Add(this.lab_Enddate);
            this.splitContainer1.Panel1.Controls.Add(this.lab_ChemicalName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1111, 510);
            this.splitContainer1.SplitterDistance = 73;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnselect
            // 
            this.btnselect.Location = new System.Drawing.Point(691, 22);
            this.btnselect.Name = "btnselect";
            this.btnselect.Size = new System.Drawing.Size(89, 29);
            this.btnselect.TabIndex = 5;
            this.btnselect.Text = "搜索";
            this.btnselect.UseVisualStyleBackColor = true;
            this.btnselect.Click += new System.EventHandler(this.btnselect_Click);
            // 
            // btnsubmit
            // 
            this.btnsubmit.Location = new System.Drawing.Point(824, 23);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(89, 29);
            this.btnsubmit.TabIndex = 4;
            this.btnsubmit.Text = "录入";
            this.btnsubmit.UseVisualStyleBackColor = true;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // datevalidtime
            // 
            this.datevalidtime.CustomFormat = "HH";
            this.datevalidtime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datevalidtime.Location = new System.Drawing.Point(459, 21);
            this.datevalidtime.Name = "datevalidtime";
            this.datevalidtime.ShowUpDown = true;
            this.datevalidtime.Size = new System.Drawing.Size(200, 29);
            this.datevalidtime.TabIndex = 3;
            // 
            // txtchemicals_name
            // 
            this.txtchemicals_name.Location = new System.Drawing.Point(109, 23);
            this.txtchemicals_name.Name = "txtchemicals_name";
            this.txtchemicals_name.Size = new System.Drawing.Size(200, 29);
            this.txtchemicals_name.TabIndex = 2;
            // 
            // lab_Enddate
            // 
            this.lab_Enddate.AutoSize = true;
            this.lab_Enddate.Location = new System.Drawing.Point(379, 26);
            this.lab_Enddate.Name = "lab_Enddate";
            this.lab_Enddate.Size = new System.Drawing.Size(74, 21);
            this.lab_Enddate.TabIndex = 1;
            this.lab_Enddate.Text = "有效时间";
            // 
            // lab_ChemicalName
            // 
            this.lab_ChemicalName.AutoSize = true;
            this.lab_ChemicalName.Location = new System.Drawing.Point(18, 26);
            this.lab_ChemicalName.Name = "lab_ChemicalName";
            this.lab_ChemicalName.Size = new System.Drawing.Size(90, 21);
            this.lab_ChemicalName.TabIndex = 0;
            this.lab_ChemicalName.Text = "化学品名称";
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
            this.splitContainer2.Size = new System.Drawing.Size(1111, 433);
            this.splitContainer2.SplitterDistance = 381;
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
            this.number,
            this.chemicals_no,
            this.chemicals_name,
            this.validtime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1111, 381);
            this.dataGridView1.TabIndex = 0;
            // 
            // number
            // 
            this.number.HeaderText = "序号";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 67;
            // 
            // chemicals_no
            // 
            this.chemicals_no.HeaderText = "化学品代号";
            this.chemicals_no.Name = "chemicals_no";
            this.chemicals_no.ReadOnly = true;
            this.chemicals_no.Width = 115;
            // 
            // chemicals_name
            // 
            this.chemicals_name.HeaderText = "化学品名称";
            this.chemicals_name.Name = "chemicals_name";
            this.chemicals_name.ReadOnly = true;
            this.chemicals_name.Width = 115;
            // 
            // validtime
            // 
            this.validtime.HeaderText = "有效时间(H)";
            this.validtime.Name = "validtime";
            this.validtime.ReadOnly = true;
            this.validtime.Width = 121;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(410, -1);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(701, 46);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // F_QCM_Chemical_information_create
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 573);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_Chemical_information_create";
            this.Text = "化学品信息创建";
            this.Load += new System.EventHandler(this.F_QCM_Chemical_information_create_Load);
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
        private System.Windows.Forms.Label lab_ChemicalName;
        private System.Windows.Forms.DateTimePicker datevalidtime;
        private System.Windows.Forms.TextBox txtchemicals_name;
        private System.Windows.Forms.Label lab_Enddate;
        private System.Windows.Forms.Button btnselect;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn chemicals_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn chemicals_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn validtime;
    }
}