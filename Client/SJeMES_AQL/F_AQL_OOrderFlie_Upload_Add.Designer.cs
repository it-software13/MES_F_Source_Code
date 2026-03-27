namespace SJeMES_AQL
{
    partial class F_AQL_OOrderFlie_Upload_Add
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
            this.label2 = new System.Windows.Forms.Label();
            this.btn_file_upload = new System.Windows.Forms.Button();
            this.btn_commit = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbl_file_name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "选择文件";
            // 
            // btn_file_upload
            // 
            this.btn_file_upload.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_file_upload.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_file_upload.Location = new System.Drawing.Point(157, 127);
            this.btn_file_upload.Name = "btn_file_upload";
            this.btn_file_upload.Size = new System.Drawing.Size(202, 33);
            this.btn_file_upload.TabIndex = 7;
            this.btn_file_upload.Text = "+";
            this.btn_file_upload.UseVisualStyleBackColor = false;
            this.btn_file_upload.Click += new System.EventHandler(this.btn_file_upload_Click);
            // 
            // btn_commit
            // 
            this.btn_commit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_commit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_commit.Location = new System.Drawing.Point(269, 237);
            this.btn_commit.Name = "btn_commit";
            this.btn_commit.Size = new System.Drawing.Size(101, 33);
            this.btn_commit.TabIndex = 12;
            this.btn_commit.Text = "确认";
            this.btn_commit.UseVisualStyleBackColor = false;
            this.btn_commit.Click += new System.EventHandler(this.btn_commit_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.Location = new System.Drawing.Point(122, 237);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(101, 33);
            this.btn_cancel.TabIndex = 13;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbl_file_name
            // 
            this.lbl_file_name.AutoSize = true;
            this.lbl_file_name.Location = new System.Drawing.Point(158, 177);
            this.lbl_file_name.Name = "lbl_file_name";
            this.lbl_file_name.Size = new System.Drawing.Size(0, 20);
            this.lbl_file_name.TabIndex = 14;
            // 
            // F_AQL_SpcPkgFile_Upload_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 383);
            this.Controls.Add(this.lbl_file_name);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_commit);
            this.Controls.Add(this.btn_file_upload);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "F_AQL_SpcPkgFile_Upload_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上传文件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_file_upload;
        private System.Windows.Forms.Button btn_commit;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label lbl_file_name;
    }
}