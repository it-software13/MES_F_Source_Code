namespace SJeMES_AQL.AQL_FrmBase
{
    partial class F_AQL_ShowFrm1
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_pass = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_passflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_fail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_failflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.na = new System.Windows.Forms.DataGridViewButtonColumn();
            this.naflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Azure;
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(765, 141);
            this.splitContainer1.SplitterDistance = 87;
            this.splitContainer1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.SeaShell;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ctype,
            this.btn_pass,
            this.btn_passflag,
            this.btn_fail,
            this.btn_failflag,
            this.na,
            this.naflag});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(765, 87);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "2.金属探测要求";
            this.Column1.Name = "Column1";
            // 
            // ctype
            // 
            this.ctype.HeaderText = "类型";
            this.ctype.Name = "ctype";
            this.ctype.Visible = false;
            // 
            // btn_pass
            // 
            this.btn_pass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "Yes";
            this.btn_pass.DefaultCellStyle = dataGridViewCellStyle2;
            this.btn_pass.FillWeight = 50F;
            this.btn_pass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pass.HeaderText = "";
            this.btn_pass.Name = "btn_pass";
            this.btn_pass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btn_pass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btn_pass.Text = "";
            this.btn_pass.Width = 50;
            // 
            // btn_passflag
            // 
            this.btn_passflag.HeaderText = "Yes";
            this.btn_passflag.Name = "btn_passflag";
            this.btn_passflag.Visible = false;
            // 
            // btn_fail
            // 
            this.btn_fail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "No";
            this.btn_fail.DefaultCellStyle = dataGridViewCellStyle3;
            this.btn_fail.FillWeight = 50F;
            this.btn_fail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fail.HeaderText = "";
            this.btn_fail.Name = "btn_fail";
            this.btn_fail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btn_fail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btn_fail.Text = "";
            this.btn_fail.Width = 50;
            // 
            // btn_failflag
            // 
            this.btn_failflag.HeaderText = "No";
            this.btn_failflag.Name = "btn_failflag";
            this.btn_failflag.Visible = false;
            // 
            // na
            // 
            this.na.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = "N/A";
            this.na.DefaultCellStyle = dataGridViewCellStyle4;
            this.na.FillWeight = 50F;
            this.na.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.na.HeaderText = "";
            this.na.Name = "na";
            this.na.Text = "";
            this.na.Width = 50;
            // 
            // naflag
            // 
            this.naflag.HeaderText = "naflag";
            this.naflag.Name = "naflag";
            this.naflag.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(55, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(707, 26);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "注释";
            // 
            // F_AQL_ShowFrm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_AQL_ShowFrm1";
            this.Size = new System.Drawing.Size(765, 141);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctype;
        private System.Windows.Forms.DataGridViewButtonColumn btn_pass;
        private System.Windows.Forms.DataGridViewTextBoxColumn btn_passflag;
        private System.Windows.Forms.DataGridViewButtonColumn btn_fail;
        private System.Windows.Forms.DataGridViewTextBoxColumn btn_failflag;
        private System.Windows.Forms.DataGridViewButtonColumn na;
        private System.Windows.Forms.DataGridViewTextBoxColumn naflag;
    }
}
