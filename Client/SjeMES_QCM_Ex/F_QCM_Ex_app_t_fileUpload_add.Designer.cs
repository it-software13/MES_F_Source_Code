
namespace SjeMES_QCM_Ex
{
    partial class F_QCM_Ex_app_t_fileUpload_add
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_commit = new System.Windows.Forms.Button();
            this.btn_file_upload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.lbl_file_name = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.DateTimePicker();
            this.starttime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.Location = new System.Drawing.Point(80, 219);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(101, 33);
            this.btn_cancel.TabIndex = 18;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_commit
            // 
            this.btn_commit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_commit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_commit.Location = new System.Drawing.Point(227, 219);
            this.btn_commit.Name = "btn_commit";
            this.btn_commit.Size = new System.Drawing.Size(101, 33);
            this.btn_commit.TabIndex = 17;
            this.btn_commit.Text = "确认";
            this.btn_commit.UseVisualStyleBackColor = false;
            this.btn_commit.Click += new System.EventHandler(this.btn_commit_Click);
            // 
            // btn_file_upload
            // 
            this.btn_file_upload.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_file_upload.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_file_upload.Location = new System.Drawing.Point(115, 95);
            this.btn_file_upload.Name = "btn_file_upload";
            this.btn_file_upload.Size = new System.Drawing.Size(202, 33);
            this.btn_file_upload.TabIndex = 16;
            this.btn_file_upload.Text = "+";
            this.btn_file_upload.UseVisualStyleBackColor = false;
            this.btn_file_upload.Click += new System.EventHandler(this.btn_file_upload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "选择文件";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(115, 141);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "选择文件上传";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(227, 141);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(107, 16);
            this.radioButton1.TabIndex = 23;
            this.radioButton1.Text = "文件夹批量上传";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // lbl_file_name
            // 
            this.lbl_file_name.AutoSize = true;
            this.lbl_file_name.Location = new System.Drawing.Point(120, 166);
            this.lbl_file_name.Name = "lbl_file_name";
            this.lbl_file_name.Size = new System.Drawing.Size(0, 12);
            this.lbl_file_name.TabIndex = 22;
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(115, 184);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(143, 21);
            this.time.TabIndex = 47;
            // 
            // starttime
            // 
            this.starttime.AutoSize = true;
            this.starttime.Location = new System.Drawing.Point(30, 187);
            this.starttime.Name = "starttime";
            this.starttime.Size = new System.Drawing.Size(53, 12);
            this.starttime.TabIndex = 48;
            this.starttime.Text = "开始时间";
            // 
            // F_QCM_Ex_app_t_fileUpload_add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 319);
            this.Controls.Add(this.starttime);
            this.Controls.Add(this.time);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.lbl_file_name);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_commit);
            this.Controls.Add(this.btn_file_upload);
            this.Controls.Add(this.label2);
            this.Name = "F_QCM_Ex_app_t_fileUpload_add";
            this.Text = "上传文件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_commit;
        private System.Windows.Forms.Button btn_file_upload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label lbl_file_name;
        private System.Windows.Forms.DateTimePicker time;
        private System.Windows.Forms.Label starttime;
    }
}