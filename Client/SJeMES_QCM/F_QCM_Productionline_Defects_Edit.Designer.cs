
namespace SJeMES_QCM
{
    partial class F_QCM_Productionline_Defects_Edit
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
            this.btnNewAdd = new System.Windows.Forms.Button();
            this.txt_defect_name = new System.Windows.Forms.TextBox();
            this.txt_defect_no = new System.Windows.Forms.TextBox();
            this.lab_bad_reson = new System.Windows.Forms.Label();
            this.lab_bad_no = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNewAdd
            // 
            this.btnNewAdd.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNewAdd.Location = new System.Drawing.Point(221, 358);
            this.btnNewAdd.Name = "btnNewAdd";
            this.btnNewAdd.Size = new System.Drawing.Size(326, 49);
            this.btnNewAdd.TabIndex = 8;
            this.btnNewAdd.Text = "确认修改不良问题";
            this.btnNewAdd.UseVisualStyleBackColor = true;
            this.btnNewAdd.Click += new System.EventHandler(this.btnNewAdd_Click);
            // 
            // txt_defect_name
            // 
            this.txt_defect_name.Location = new System.Drawing.Point(221, 192);
            this.txt_defect_name.Multiline = true;
            this.txt_defect_name.Name = "txt_defect_name";
            this.txt_defect_name.Size = new System.Drawing.Size(326, 144);
            this.txt_defect_name.TabIndex = 7;
            // 
            // txt_defect_no
            // 
            this.txt_defect_no.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_defect_no.Location = new System.Drawing.Point(221, 111);
            this.txt_defect_no.Name = "txt_defect_no";
            this.txt_defect_no.ReadOnly = true;
            this.txt_defect_no.Size = new System.Drawing.Size(326, 26);
            this.txt_defect_no.TabIndex = 6;
            // 
            // lab_bad_reson
            // 
            this.lab_bad_reson.AutoSize = true;
            this.lab_bad_reson.BackColor = System.Drawing.Color.White;
            this.lab_bad_reson.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_bad_reson.Location = new System.Drawing.Point(217, 155);
            this.lab_bad_reson.Name = "lab_bad_reson";
            this.lab_bad_reson.Size = new System.Drawing.Size(142, 19);
            this.lab_bad_reson.TabIndex = 4;
            this.lab_bad_reson.Text = "不良问题内容：";
            // 
            // lab_bad_no
            // 
            this.lab_bad_no.AutoSize = true;
            this.lab_bad_no.BackColor = System.Drawing.Color.White;
            this.lab_bad_no.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_bad_no.Location = new System.Drawing.Point(217, 80);
            this.lab_bad_no.Name = "lab_bad_no";
            this.lab_bad_no.Size = new System.Drawing.Size(142, 19);
            this.lab_bad_no.TabIndex = 5;
            this.lab_bad_no.Text = "不良问题代号：";
            // 
            // F_QCM_Productionline_Defects_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnNewAdd);
            this.Controls.Add(this.txt_defect_name);
            this.Controls.Add(this.txt_defect_no);
            this.Controls.Add(this.lab_bad_reson);
            this.Controls.Add(this.lab_bad_no);
            this.Name = "F_QCM_Productionline_Defects_Edit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "不良问题修改";
            this.Load += new System.EventHandler(this.F_QCM_Productionline_Defects_Edit2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewAdd;
        private System.Windows.Forms.TextBox txt_defect_name;
        private System.Windows.Forms.TextBox txt_defect_no;
        private System.Windows.Forms.Label lab_bad_reson;
        private System.Windows.Forms.Label lab_bad_no;
    }
}