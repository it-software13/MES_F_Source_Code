
namespace SJeMES_QCM
{
    partial class F_QCM_SystemFileMaintenance_Add
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
            this.fileName = new System.Windows.Forms.TextBox();
            this.lab_file_name = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lab_file_type = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.link_delete = new System.Windows.Forms.LinkLabel();
            this.link_file_url = new System.Windows.Forms.LinkLabel();
            this.cancel = new System.Windows.Forms.Button();
            this.btn = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileName
            // 
            this.fileName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.fileName.Location = new System.Drawing.Point(239, 153);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(121, 26);
            this.fileName.TabIndex = 7;
            this.fileName.Text = "+";
            this.fileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fileName.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // lab_file_name
            // 
            this.lab_file_name.AutoSize = true;
            this.lab_file_name.BackColor = System.Drawing.Color.White;
            this.lab_file_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_file_name.Location = new System.Drawing.Point(133, 155);
            this.lab_file_name.Name = "lab_file_name";
            this.lab_file_name.Size = new System.Drawing.Size(90, 21);
            this.lab_file_name.TabIndex = 6;
            this.lab_file_name.Text = "文件名称：";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "品质目标",
            "品质流程",
            "组织框架",
            "组值架构",
            "WI",
            "培训文件",
            "Adidas文件",
            "品质制度",
            "品质报告"});
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "品质目标",
            "品质流程",
            "组织架构",
            "WI",
            "培训文件",
            "Adidas文件",
            "品质制度",
            "品质报告"});
            this.comboBox1.Location = new System.Drawing.Point(239, 108);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 5;
            // 
            // lab_file_type
            // 
            this.lab_file_type.AutoSize = true;
            this.lab_file_type.BackColor = System.Drawing.Color.White;
            this.lab_file_type.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_file_type.Location = new System.Drawing.Point(133, 106);
            this.lab_file_type.Name = "lab_file_type";
            this.lab_file_type.Size = new System.Drawing.Size(90, 21);
            this.lab_file_type.TabIndex = 4;
            this.lab_file_type.Text = "文件类型：";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.link_delete);
            this.panel1.Controls.Add(this.link_file_url);
            this.panel1.Location = new System.Drawing.Point(72, 205);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 25);
            this.panel1.TabIndex = 18;
            this.panel1.Visible = false;
            // 
            // link_delete
            // 
            this.link_delete.AutoSize = true;
            this.link_delete.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link_delete.LinkColor = System.Drawing.Color.DimGray;
            this.link_delete.Location = new System.Drawing.Point(332, 7);
            this.link_delete.Name = "link_delete";
            this.link_delete.Size = new System.Drawing.Size(29, 12);
            this.link_delete.TabIndex = 17;
            this.link_delete.TabStop = true;
            this.link_delete.Text = "删除";
            this.link_delete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_delete_LinkClicked);
            // 
            // link_file_url
            // 
            this.link_file_url.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.link_file_url.LinkColor = System.Drawing.Color.DimGray;
            this.link_file_url.Location = new System.Drawing.Point(18, 7);
            this.link_file_url.Name = "link_file_url";
            this.link_file_url.Size = new System.Drawing.Size(251, 12);
            this.link_file_url.TabIndex = 16;
            this.link_file_url.TabStop = true;
            this.link_file_url.Text = "link_file_url";
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(174, 272);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(76, 30);
            this.cancel.TabIndex = 19;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(284, 272);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(76, 30);
            this.btn.TabIndex = 20;
            this.btn.Text = "确认";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(99, 103);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(28, 32);
            this.label25.TabIndex = 28;
            this.label25.Text = "*";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(99, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 32);
            this.label3.TabIndex = 29;
            this.label3.Text = "*";
            // 
            // F_QCM_SystemFileMaintenance_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 314);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.lab_file_name);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lab_file_type);
            this.Name = "F_QCM_SystemFileMaintenance_Add";
            this.Text = "录入";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Label lab_file_name;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lab_file_type;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel link_delete;
        private System.Windows.Forms.LinkLabel link_file_url;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label3;
    }
}