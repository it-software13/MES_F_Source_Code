namespace SJeMES_QCM
{
    partial class F_QCM_ATR_File1_Edit
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
            this.lab_wenjian_type = new System.Windows.Forms.Label();
            this.lab_prod_no = new System.Windows.Forms.Label();
            this.txt_art = new System.Windows.Forms.TextBox();
            this.lab_end_date = new System.Windows.Forms.Label();
            this.lab_zm_wenjian = new System.Windows.Forms.Label();
            this.ddl_emun_file_type = new System.Windows.Forms.ComboBox();
            this.dtp_time = new System.Windows.Forms.DateTimePicker();
            this.btn_upload = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_affirm = new System.Windows.Forms.Button();
            this.link_file_url = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.link_delete = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lab_wenjian_type
            // 
            this.lab_wenjian_type.AutoSize = true;
            this.lab_wenjian_type.BackColor = System.Drawing.Color.White;
            this.lab_wenjian_type.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_wenjian_type.Location = new System.Drawing.Point(50, 87);
            this.lab_wenjian_type.Name = "lab_wenjian_type";
            this.lab_wenjian_type.Size = new System.Drawing.Size(95, 19);
            this.lab_wenjian_type.TabIndex = 1;
            this.lab_wenjian_type.Text = "文件类型:";
            // 
            // lab_prod_no
            // 
            this.lab_prod_no.AutoSize = true;
            this.lab_prod_no.BackColor = System.Drawing.Color.White;
            this.lab_prod_no.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_prod_no.Location = new System.Drawing.Point(96, 133);
            this.lab_prod_no.Name = "lab_prod_no";
            this.lab_prod_no.Size = new System.Drawing.Size(49, 19);
            this.lab_prod_no.TabIndex = 3;
            this.lab_prod_no.Text = "ART:";
            // 
            // txt_art
            // 
            this.txt_art.Font = new System.Drawing.Font("宋体", 14F);
            this.txt_art.Location = new System.Drawing.Point(159, 130);
            this.txt_art.Name = "txt_art";
            this.txt_art.ReadOnly = true;
            this.txt_art.Size = new System.Drawing.Size(364, 29);
            this.txt_art.TabIndex = 2;
            this.txt_art.Click += new System.EventHandler(this.txt_art_Click);
            // 
            // lab_end_date
            // 
            this.lab_end_date.AutoSize = true;
            this.lab_end_date.BackColor = System.Drawing.Color.White;
            this.lab_end_date.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_end_date.Location = new System.Drawing.Point(50, 184);
            this.lab_end_date.Name = "lab_end_date";
            this.lab_end_date.Size = new System.Drawing.Size(95, 19);
            this.lab_end_date.TabIndex = 5;
            this.lab_end_date.Text = "有效时间:";
            // 
            // lab_zm_wenjian
            // 
            this.lab_zm_wenjian.AutoSize = true;
            this.lab_zm_wenjian.BackColor = System.Drawing.Color.White;
            this.lab_zm_wenjian.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_zm_wenjian.Location = new System.Drawing.Point(50, 231);
            this.lab_zm_wenjian.Name = "lab_zm_wenjian";
            this.lab_zm_wenjian.Size = new System.Drawing.Size(95, 19);
            this.lab_zm_wenjian.TabIndex = 7;
            this.lab_zm_wenjian.Text = "证明文件:";
            // 
            // ddl_emun_file_type
            // 
            this.ddl_emun_file_type.Font = new System.Drawing.Font("宋体", 14F);
            this.ddl_emun_file_type.FormattingEnabled = true;
            this.ddl_emun_file_type.Location = new System.Drawing.Point(159, 84);
            this.ddl_emun_file_type.Name = "ddl_emun_file_type";
            this.ddl_emun_file_type.Size = new System.Drawing.Size(364, 27);
            this.ddl_emun_file_type.TabIndex = 8;
            // 
            // dtp_time
            // 
            this.dtp_time.Font = new System.Drawing.Font("宋体", 14F);
            this.dtp_time.Location = new System.Drawing.Point(159, 177);
            this.dtp_time.Name = "dtp_time";
            this.dtp_time.Size = new System.Drawing.Size(364, 29);
            this.dtp_time.TabIndex = 9;
            // 
            // btn_upload
            // 
            this.btn_upload.BackColor = System.Drawing.Color.White;
            this.btn_upload.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.btn_upload.Location = new System.Drawing.Point(159, 226);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(364, 29);
            this.btn_upload.TabIndex = 10;
            this.btn_upload.Text = "+";
            this.btn_upload.UseVisualStyleBackColor = false;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.White;
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.btn_cancel.Location = new System.Drawing.Point(397, 418);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(90, 37);
            this.btn_cancel.TabIndex = 14;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_affirm
            // 
            this.btn_affirm.BackColor = System.Drawing.Color.White;
            this.btn_affirm.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.btn_affirm.Location = new System.Drawing.Point(493, 418);
            this.btn_affirm.Name = "btn_affirm";
            this.btn_affirm.Size = new System.Drawing.Size(90, 37);
            this.btn_affirm.TabIndex = 15;
            this.btn_affirm.Text = "确认";
            this.btn_affirm.UseVisualStyleBackColor = false;
            this.btn_affirm.Click += new System.EventHandler(this.button2_Click);
            // 
            // link_file_url
            // 
            this.link_file_url.Location = new System.Drawing.Point(5, 7);
            this.link_file_url.Name = "link_file_url";
            this.link_file_url.Size = new System.Drawing.Size(464, 12);
            this.link_file_url.TabIndex = 16;
            this.link_file_url.TabStop = true;
            this.link_file_url.Text = "link_file_url";
            this.link_file_url.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_file_url_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.link_delete);
            this.panel1.Controls.Add(this.link_file_url);
            this.panel1.Location = new System.Drawing.Point(54, 261);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 25);
            this.panel1.TabIndex = 17;
            this.panel1.Visible = false;
            // 
            // link_delete
            // 
            this.link_delete.AutoSize = true;
            this.link_delete.Location = new System.Drawing.Point(476, 7);
            this.link_delete.Name = "link_delete";
            this.link_delete.Size = new System.Drawing.Size(29, 12);
            this.link_delete.TabIndex = 17;
            this.link_delete.TabStop = true;
            this.link_delete.Text = "删除";
            this.link_delete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_delete_LinkClicked);
            // 
            // F_QCM_ATR_File1_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 467);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_affirm);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.dtp_time);
            this.Controls.Add(this.ddl_emun_file_type);
            this.Controls.Add(this.txt_art);
            this.Controls.Add(this.lab_zm_wenjian);
            this.Controls.Add(this.lab_end_date);
            this.Controls.Add(this.lab_prod_no);
            this.Controls.Add(this.lab_wenjian_type);
            this.Name = "F_QCM_ATR_File1_Edit";
            this.Text = "ART验货文件录入";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lab_wenjian_type;
        private System.Windows.Forms.Label lab_prod_no;
        private System.Windows.Forms.TextBox txt_art;
        private System.Windows.Forms.Label lab_end_date;
        private System.Windows.Forms.Label lab_zm_wenjian;
        private System.Windows.Forms.ComboBox ddl_emun_file_type;
        private System.Windows.Forms.DateTimePicker dtp_time;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_affirm;
        private System.Windows.Forms.LinkLabel link_file_url;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel link_delete;
    }
}