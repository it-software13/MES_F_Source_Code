namespace SJeMES_QCM
{
    partial class F_QCM_ATR_File3_Edit
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
            this.btn_affirm = new System.Windows.Forms.Button();
            this.btn_out = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.dtp_time = new System.Windows.Forms.DateTimePicker();
            this.ddl_emun_file_type = new System.Windows.Forms.ComboBox();
            this.lab_ZMwenjian = new System.Windows.Forms.Label();
            this.lab_EndDate = new System.Windows.Forms.Label();
            this.lab_ART = new System.Windows.Forms.Label();
            this.txt_art = new System.Windows.Forms.TextBox();
            this.lab_wenjian_type = new System.Windows.Forms.Label();
            this.link_file_url = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.link_delete = new System.Windows.Forms.LinkLabel();
            this.ddl_baogao_type = new System.Windows.Forms.ComboBox();
            this.lab_baogao_type = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_affirm
            // 
            this.btn_affirm.BackColor = System.Drawing.Color.White;
            this.btn_affirm.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.btn_affirm.Location = new System.Drawing.Point(450, 387);
            this.btn_affirm.Name = "btn_affirm";
            this.btn_affirm.Size = new System.Drawing.Size(90, 37);
            this.btn_affirm.TabIndex = 38;
            this.btn_affirm.Text = "确认";
            this.btn_affirm.UseVisualStyleBackColor = false;
            this.btn_affirm.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_out
            // 
            this.btn_out.BackColor = System.Drawing.Color.White;
            this.btn_out.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.btn_out.Location = new System.Drawing.Point(354, 387);
            this.btn_out.Name = "btn_out";
            this.btn_out.Size = new System.Drawing.Size(90, 37);
            this.btn_out.TabIndex = 37;
            this.btn_out.Text = "取消";
            this.btn_out.UseVisualStyleBackColor = false;
            this.btn_out.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.BackColor = System.Drawing.Color.White;
            this.btn_upload.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.btn_upload.Location = new System.Drawing.Point(137, 264);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(364, 29);
            this.btn_upload.TabIndex = 36;
            this.btn_upload.Text = "+";
            this.btn_upload.UseVisualStyleBackColor = false;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // dtp_time
            // 
            this.dtp_time.Font = new System.Drawing.Font("宋体", 14F);
            this.dtp_time.Location = new System.Drawing.Point(137, 215);
            this.dtp_time.Name = "dtp_time";
            this.dtp_time.Size = new System.Drawing.Size(364, 29);
            this.dtp_time.TabIndex = 35;
            // 
            // ddl_emun_file_type
            // 
            this.ddl_emun_file_type.Font = new System.Drawing.Font("宋体", 14F);
            this.ddl_emun_file_type.FormattingEnabled = true;
            this.ddl_emun_file_type.Location = new System.Drawing.Point(137, 121);
            this.ddl_emun_file_type.Name = "ddl_emun_file_type";
            this.ddl_emun_file_type.Size = new System.Drawing.Size(364, 27);
            this.ddl_emun_file_type.TabIndex = 34;
            // 
            // lab_ZMwenjian
            // 
            this.lab_ZMwenjian.AutoSize = true;
            this.lab_ZMwenjian.BackColor = System.Drawing.Color.White;
            this.lab_ZMwenjian.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_ZMwenjian.Location = new System.Drawing.Point(28, 275);
            this.lab_ZMwenjian.Name = "lab_ZMwenjian";
            this.lab_ZMwenjian.Size = new System.Drawing.Size(95, 19);
            this.lab_ZMwenjian.TabIndex = 33;
            this.lab_ZMwenjian.Text = "证明文件:";
            // 
            // lab_EndDate
            // 
            this.lab_EndDate.AutoSize = true;
            this.lab_EndDate.BackColor = System.Drawing.Color.White;
            this.lab_EndDate.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_EndDate.Location = new System.Drawing.Point(28, 228);
            this.lab_EndDate.Name = "lab_EndDate";
            this.lab_EndDate.Size = new System.Drawing.Size(95, 19);
            this.lab_EndDate.TabIndex = 32;
            this.lab_EndDate.Text = "有效时间:";
            // 
            // lab_ART
            // 
            this.lab_ART.AutoSize = true;
            this.lab_ART.BackColor = System.Drawing.Color.White;
            this.lab_ART.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_ART.Location = new System.Drawing.Point(74, 177);
            this.lab_ART.Name = "lab_ART";
            this.lab_ART.Size = new System.Drawing.Size(49, 19);
            this.lab_ART.TabIndex = 31;
            this.lab_ART.Text = "ART:";
            // 
            // txt_art
            // 
            this.txt_art.Font = new System.Drawing.Font("宋体", 14F);
            this.txt_art.Location = new System.Drawing.Point(137, 168);
            this.txt_art.Name = "txt_art";
            this.txt_art.ReadOnly = true;
            this.txt_art.Size = new System.Drawing.Size(364, 29);
            this.txt_art.TabIndex = 30;
            this.txt_art.Click += new System.EventHandler(this.txt_art_Click);
            // 
            // lab_wenjian_type
            // 
            this.lab_wenjian_type.AutoSize = true;
            this.lab_wenjian_type.BackColor = System.Drawing.Color.White;
            this.lab_wenjian_type.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_wenjian_type.Location = new System.Drawing.Point(28, 124);
            this.lab_wenjian_type.Name = "lab_wenjian_type";
            this.lab_wenjian_type.Size = new System.Drawing.Size(95, 19);
            this.lab_wenjian_type.TabIndex = 29;
            this.lab_wenjian_type.Text = "文件类型:";
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
            this.panel1.Location = new System.Drawing.Point(32, 299);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 25);
            this.panel1.TabIndex = 39;
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
            // ddl_baogao_type
            // 
            this.ddl_baogao_type.Font = new System.Drawing.Font("宋体", 14F);
            this.ddl_baogao_type.FormattingEnabled = true;
            this.ddl_baogao_type.Items.AddRange(new object[] {
            "成品检验报告",
            "产品安全报告"});
            this.ddl_baogao_type.Location = new System.Drawing.Point(137, 76);
            this.ddl_baogao_type.Name = "ddl_baogao_type";
            this.ddl_baogao_type.Size = new System.Drawing.Size(364, 27);
            this.ddl_baogao_type.TabIndex = 41;
            this.ddl_baogao_type.SelectedIndexChanged += new System.EventHandler(this.ddl_baogao_type_SelectedIndexChanged);
            // 
            // lab_baogao_type
            // 
            this.lab_baogao_type.AutoSize = true;
            this.lab_baogao_type.BackColor = System.Drawing.Color.White;
            this.lab_baogao_type.Font = new System.Drawing.Font("宋体", 14F);
            this.lab_baogao_type.Location = new System.Drawing.Point(28, 79);
            this.lab_baogao_type.Name = "lab_baogao_type";
            this.lab_baogao_type.Size = new System.Drawing.Size(95, 19);
            this.lab_baogao_type.TabIndex = 40;
            this.lab_baogao_type.Text = "报告类型:";
            // 
            // F_QCM_ATR_File3_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 441);
            this.Controls.Add(this.ddl_baogao_type);
            this.Controls.Add(this.btn_affirm);
            this.Controls.Add(this.btn_out);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.dtp_time);
            this.Controls.Add(this.ddl_emun_file_type);
            this.Controls.Add(this.txt_art);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lab_baogao_type);
            this.Controls.Add(this.lab_ZMwenjian);
            this.Controls.Add(this.lab_EndDate);
            this.Controls.Add(this.lab_ART);
            this.Controls.Add(this.lab_wenjian_type);
            this.Name = "F_QCM_ATR_File3_Edit";
            this.Text = "ART测试文件录入";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_affirm;
        private System.Windows.Forms.Button btn_out;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.DateTimePicker dtp_time;
        private System.Windows.Forms.ComboBox ddl_emun_file_type;
        private System.Windows.Forms.Label lab_ZMwenjian;
        private System.Windows.Forms.Label lab_EndDate;
        private System.Windows.Forms.Label lab_ART;
        private System.Windows.Forms.TextBox txt_art;
        private System.Windows.Forms.Label lab_wenjian_type;
        private System.Windows.Forms.LinkLabel link_file_url;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel link_delete;
        private System.Windows.Forms.ComboBox ddl_baogao_type;
        private System.Windows.Forms.Label lab_baogao_type;
    }
}