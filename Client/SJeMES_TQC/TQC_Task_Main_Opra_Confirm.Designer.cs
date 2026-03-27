
namespace SJeMES_TQC
{
    partial class TQC_Task_Main_Opra_Confirm
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
            this.btn_commit = new System.Windows.Forms.Button();
            this.tb_user_code = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_cancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_commit
            // 
            this.btn_commit.Location = new System.Drawing.Point(67, 221);
            this.btn_commit.Name = "btn_commit";
            this.btn_commit.Size = new System.Drawing.Size(102, 38);
            this.btn_commit.TabIndex = 20;
            this.btn_commit.Text = "确认";
            this.btn_commit.UseVisualStyleBackColor = true;
            this.btn_commit.Click += new System.EventHandler(this.btn_commit_Click);
            // 
            // tb_user_code
            // 
            this.tb_user_code.Location = new System.Drawing.Point(155, 124);
            this.tb_user_code.Name = "tb_user_code";
            this.tb_user_code.Size = new System.Drawing.Size(163, 20);
            this.tb_user_code.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(78, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "账号:";
            // 
            // btn_cancle
            // 
            this.btn_cancle.Location = new System.Drawing.Point(216, 221);
            this.btn_cancle.Name = "btn_cancle";
            this.btn_cancle.Size = new System.Drawing.Size(102, 38);
            this.btn_cancle.TabIndex = 21;
            this.btn_cancle.Text = "取消";
            this.btn_cancle.UseVisualStyleBackColor = true;
            this.btn_cancle.Click += new System.EventHandler(this.btn_cancle_Click);
            // 
            // TQC_Task_Main_Opra_Confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 336);
            this.Controls.Add(this.btn_cancle);
            this.Controls.Add(this.btn_commit);
            this.Controls.Add(this.tb_user_code);
            this.Controls.Add(this.label8);
            this.Name = "TQC_Task_Main_Opra_Confirm";
            this.Text = "请输入创建人账号";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_commit;
        private System.Windows.Forms.TextBox tb_user_code;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_cancle;
    }
}