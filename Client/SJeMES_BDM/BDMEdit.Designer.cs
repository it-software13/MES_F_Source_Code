
namespace SJeMES_BDM
{
    partial class BDMEdit
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
            this.lab_systematic_name = new System.Windows.Forms.Label();
            this.lab_remarks = new System.Windows.Forms.Label();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.rtxt_remarks = new System.Windows.Forms.RichTextBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_affirm = new System.Windows.Forms.Button();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.lab_classification_designation = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lab_systematic_name
            // 
            this.lab_systematic_name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_systematic_name.AutoSize = true;
            this.lab_systematic_name.Location = new System.Drawing.Point(91, 103);
            this.lab_systematic_name.Name = "lab_systematic_name";
            this.lab_systematic_name.Size = new System.Drawing.Size(74, 21);
            this.lab_systematic_name.TabIndex = 0;
            this.lab_systematic_name.Text = "分类名称";
            // 
            // lab_remarks
            // 
            this.lab_remarks.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_remarks.AutoSize = true;
            this.lab_remarks.Location = new System.Drawing.Point(99, 135);
            this.lab_remarks.Name = "lab_remarks";
            this.lab_remarks.Size = new System.Drawing.Size(42, 21);
            this.lab_remarks.TabIndex = 1;
            this.lab_remarks.Text = "备注";
            // 
            // txt1
            // 
            this.txt1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt1.Location = new System.Drawing.Point(167, 100);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(159, 29);
            this.txt1.TabIndex = 2;
            // 
            // rtxt_remarks
            // 
            this.rtxt_remarks.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rtxt_remarks.Location = new System.Drawing.Point(101, 160);
            this.rtxt_remarks.Name = "rtxt_remarks";
            this.rtxt_remarks.Size = new System.Drawing.Size(319, 102);
            this.rtxt_remarks.TabIndex = 3;
            this.rtxt_remarks.Text = "";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_cancel.Location = new System.Drawing.Point(167, 288);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 37);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn_affirm
            // 
            this.btn_affirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_affirm.Location = new System.Drawing.Point(282, 288);
            this.btn_affirm.Name = "btn_affirm";
            this.btn_affirm.Size = new System.Drawing.Size(75, 37);
            this.btn_affirm.TabIndex = 5;
            this.btn_affirm.Text = "确认";
            this.btn_affirm.UseVisualStyleBackColor = true;
            this.btn_affirm.Click += new System.EventHandler(this.btn2_Click);
            // 
            // txt2
            // 
            this.txt2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt2.Location = new System.Drawing.Point(167, 67);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(159, 29);
            this.txt2.TabIndex = 7;
            // 
            // lab_classification_designation
            // 
            this.lab_classification_designation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_classification_designation.AutoSize = true;
            this.lab_classification_designation.Location = new System.Drawing.Point(91, 70);
            this.lab_classification_designation.Name = "lab_classification_designation";
            this.lab_classification_designation.Size = new System.Drawing.Size(74, 21);
            this.lab_classification_designation.TabIndex = 6;
            this.lab_classification_designation.Text = "分类代号";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.lab_systematic_name);
            this.panel1.Controls.Add(this.txt2);
            this.panel1.Controls.Add(this.lab_remarks);
            this.panel1.Controls.Add(this.lab_classification_designation);
            this.panel1.Controls.Add(this.txt1);
            this.panel1.Controls.Add(this.btn_affirm);
            this.panel1.Controls.Add(this.rtxt_remarks);
            this.panel1.Controls.Add(this.btn_cancel);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(1, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 393);
            this.panel1.TabIndex = 8;
            // 
            // BDMEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 459);
            this.Controls.Add(this.panel1);
            this.Name = "BDMEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建分类";
            this.Load += new System.EventHandler(this.BDMEdit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_systematic_name;
        private System.Windows.Forms.Label lab_remarks;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.RichTextBox rtxt_remarks;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_affirm;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.Label lab_classification_designation;
        private System.Windows.Forms.Panel panel1;
    }
}