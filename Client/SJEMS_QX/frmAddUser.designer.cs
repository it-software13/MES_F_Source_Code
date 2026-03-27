namespace SJEMS_QX
{
    partial class Frm_ButtonPermissions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ButtonPermissions));
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.btn_SelectNone = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.txt_UserCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.查看数据 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.添加数据 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.修改数据 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.删除数据 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.确认操作 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.审核操作 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.其他操作 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.打印 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.更多功能 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btn_SelectAll);
            this.panel1.Controls.Add(this.btn_SelectNone);
            this.panel1.Controls.Add(this.btn_Add);
            this.panel1.Controls.Add(this.txt_UserCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(885, 84);
            this.panel1.TabIndex = 0;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(685, 16);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(474, 16);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 6;
            this.textBox2.Click += new System.EventHandler(this.textBox2_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(276, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 4;
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Location = new System.Drawing.Point(23, 45);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(94, 23);
            this.btn_SelectAll.TabIndex = 1;
            this.btn_SelectAll.Text = "选择全部";
            this.btn_SelectAll.UseVisualStyleBackColor = true;
            this.btn_SelectAll.Click += new System.EventHandler(this.btn_SelectAll_Click);
            // 
            // btn_SelectNone
            // 
            this.btn_SelectNone.Location = new System.Drawing.Point(134, 45);
            this.btn_SelectNone.Name = "btn_SelectNone";
            this.btn_SelectNone.Size = new System.Drawing.Size(94, 23);
            this.btn_SelectNone.TabIndex = 2;
            this.btn_SelectNone.Text = "清空全部";
            this.btn_SelectNone.UseVisualStyleBackColor = true;
            this.btn_SelectNone.Click += new System.EventHandler(this.btn_SelectNone_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(248, 45);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(94, 23);
            this.btn_Add.TabIndex = 3;
            this.btn_Add.Text = "保存";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_UserCode
            // 
            this.txt_UserCode.BackColor = System.Drawing.Color.White;
            this.txt_UserCode.Location = new System.Drawing.Point(79, 16);
            this.txt_UserCode.Name = "txt_UserCode";
            this.txt_UserCode.Size = new System.Drawing.Size(100, 21);
            this.txt_UserCode.TabIndex = 1;
            this.txt_UserCode.Click += new System.EventHandler(this.txt_UserCode_Click);
            this.txt_UserCode.TextChanged += new System.EventHandler(this.txt_UserCode_TextChanged);
            this.txt_UserCode.DoubleClick += new System.EventHandler(this.txt_UserCode_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "角色代号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "角色名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(408, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "模块代号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(616, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "模块名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.查看数据,
            this.添加数据,
            this.修改数据,
            this.删除数据,
            this.确认操作,
            this.审核操作,
            this.其他操作,
            this.打印,
            this.更多功能});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 84);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(885, 373);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "模块代号";
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "模块代号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "模块名称";
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "模块名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "全部权限";
            this.Column3.HeaderText = "全部权限";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // 查看数据
            // 
            this.查看数据.DataPropertyName = "查看数据";
            this.查看数据.HeaderText = "查看数据";
            this.查看数据.Name = "查看数据";
            this.查看数据.ReadOnly = true;
            // 
            // 添加数据
            // 
            this.添加数据.DataPropertyName = "添加数据";
            this.添加数据.HeaderText = "添加数据";
            this.添加数据.Name = "添加数据";
            this.添加数据.ReadOnly = true;
            // 
            // 修改数据
            // 
            this.修改数据.DataPropertyName = "修改数据";
            this.修改数据.HeaderText = "修改数据";
            this.修改数据.Name = "修改数据";
            this.修改数据.ReadOnly = true;
            // 
            // 删除数据
            // 
            this.删除数据.DataPropertyName = "删除数据";
            this.删除数据.HeaderText = "删除数据";
            this.删除数据.Name = "删除数据";
            this.删除数据.ReadOnly = true;
            // 
            // 确认操作
            // 
            this.确认操作.DataPropertyName = "确认操作";
            this.确认操作.HeaderText = "确认操作";
            this.确认操作.Name = "确认操作";
            this.确认操作.ReadOnly = true;
            // 
            // 审核操作
            // 
            this.审核操作.DataPropertyName = "审核操作";
            this.审核操作.HeaderText = "审核操作";
            this.审核操作.Name = "审核操作";
            this.审核操作.ReadOnly = true;
            // 
            // 其他操作
            // 
            this.其他操作.DataPropertyName = "其他操作";
            this.其他操作.HeaderText = "其他操作";
            this.其他操作.Name = "其他操作";
            this.其他操作.ReadOnly = true;
            // 
            // 打印
            // 
            this.打印.DataPropertyName = "打印";
            this.打印.HeaderText = "打印";
            this.打印.Name = "打印";
            this.打印.ReadOnly = true;
            // 
            // 更多功能
            // 
            this.更多功能.DataPropertyName = "更多功能";
            this.更多功能.HeaderText = "更多功能";
            this.更多功能.Name = "更多功能";
            this.更多功能.ReadOnly = true;
            // 
            // Frm_ButtonPermissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 457);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_ButtonPermissions";
            this.Text = "按钮权限";
            this.Load += new System.EventHandler(this.frmAddUser_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.Button btn_SelectNone;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox txt_UserCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 查看数据;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 添加数据;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 修改数据;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 删除数据;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 确认操作;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 审核操作;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 其他操作;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 打印;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 更多功能;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
    }
}