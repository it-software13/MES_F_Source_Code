namespace SJeMES_AQL
{
    partial class F_AQL_OutData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_AQL_OutData));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ucBtnImg4 = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHIPCOUNTRY_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.se_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.存放位置 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.组别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.PO,
            this.SHIPCOUNTRY_name,
            this.se_qty,
            this.存放位置,
            this.组别});
            this.dataGridView1.Location = new System.Drawing.Point(6, 153);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(660, 361);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ucBtnImg4
            // 
            this.ucBtnImg4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBtnImg4.BackColor = System.Drawing.Color.White;
            this.ucBtnImg4.BtnBackColor = System.Drawing.Color.White;
            this.ucBtnImg4.BtnFont = new System.Drawing.Font("微软雅黑", 17F);
            this.ucBtnImg4.BtnForeColor = System.Drawing.Color.Gray;
            this.ucBtnImg4.BtnText = "";
            this.ucBtnImg4.ConerRadius = 5;
            this.ucBtnImg4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg4.EnabledMouseEffect = true;
            this.ucBtnImg4.FillColor = System.Drawing.Color.Green;
            this.ucBtnImg4.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg4.Image = ((System.Drawing.Image)(resources.GetObject("ucBtnImg4.Image")));
            this.ucBtnImg4.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg4.ImageFontIcons = null;
            this.ucBtnImg4.IsRadius = true;
            this.ucBtnImg4.IsShowRect = true;
            this.ucBtnImg4.IsShowTips = false;
            this.ucBtnImg4.Location = new System.Drawing.Point(596, 70);
            this.ucBtnImg4.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg4.Name = "ucBtnImg4";
            this.ucBtnImg4.RectColor = System.Drawing.Color.Green;
            this.ucBtnImg4.RectWidth = 1;
            this.ucBtnImg4.Size = new System.Drawing.Size(69, 27);
            this.ucBtnImg4.TabIndex = 6;
            this.ucBtnImg4.TabStop = false;
            this.ucBtnImg4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg4.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg4.TipsText = "";
            this.ucBtnImg4.BtnClick += new System.EventHandler(this.ucBtnImg4_BtnClick);
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.BackColor = System.Drawing.Color.White;
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(125, 519);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(542, 44);
            this.pageControl1.TabIndex = 7;
            this.pageControl1.TotalCount = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(29, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "模糊查询";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(88, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(161, 26);
            this.textBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(6, 102);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(659, 45);
            this.textBox2.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(331, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 26);
            this.button2.TabIndex = 12;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(255, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 26);
            this.button1.TabIndex = 13;
            this.button1.Text = "搜索";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // check
            // 
            this.check.HeaderText = "选择";
            this.check.Name = "check";
            this.check.ReadOnly = true;
            this.check.Width = 35;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "PO";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 42;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ART";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 48;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "鞋型";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 54;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "制令号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 66;
            // 
            // PO
            // 
            this.PO.HeaderText = "PO";
            this.PO.Name = "PO";
            this.PO.ReadOnly = true;
            this.PO.Visible = false;
            this.PO.Width = 42;
            // 
            // SHIPCOUNTRY_name
            // 
            this.SHIPCOUNTRY_name.HeaderText = "国家";
            this.SHIPCOUNTRY_name.Name = "SHIPCOUNTRY_name";
            this.SHIPCOUNTRY_name.ReadOnly = true;
            this.SHIPCOUNTRY_name.Width = 54;
            // 
            // se_qty
            // 
            this.se_qty.HeaderText = "数量";
            this.se_qty.Name = "se_qty";
            this.se_qty.ReadOnly = true;
            this.se_qty.Width = 54;
            // 
            // 存放位置
            // 
            this.存放位置.HeaderText = "存放位置";
            this.存放位置.Name = "存放位置";
            this.存放位置.ReadOnly = true;
            this.存放位置.Width = 78;
            // 
            // 组别
            // 
            this.组别.HeaderText = "组别";
            this.组别.Name = "组别";
            this.组别.ReadOnly = true;
            this.组别.Width = 54;
            // 
            // F_AQL_OutData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 569);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageControl1);
            this.Controls.Add(this.ucBtnImg4);
            this.Controls.Add(this.dataGridView1);
            this.Name = "F_AQL_OutData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择数据";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_AQL_OutData_FormClosing);
            this.Load += new System.EventHandler(this.F_AQL_OutData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.UCBtnImg ucBtnImg4;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn PO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIPCOUNTRY_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn se_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn 存放位置;
        private System.Windows.Forms.DataGridViewTextBoxColumn 组别;
    }
}