
namespace SJeMES_TSM
{
    partial class Signature_Upload
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Login = new System.Windows.Forms.TabPage();
            this.Apprbtn = new System.Windows.Forms.Button();
            this.passtxt = new System.Windows.Forms.TextBox();
            this.Bartxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Sign_Up = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.savebtn = new System.Windows.Forms.Button();
            this.uploadbtn = new System.Windows.Forms.Button();
            this.Pwdtxt = new System.Windows.Forms.TextBox();
            this.Barcodetxt = new System.Windows.Forms.TextBox();
            this.imgibl = new System.Windows.Forms.Label();
            this.pwdlbl = new System.Windows.Forms.Label();
            this.barcodelbl = new System.Windows.Forms.Label();
            this.Change_Password = new System.Windows.Forms.TabPage();
            this.Updatebtn = new System.Windows.Forms.Button();
            this.pdtxt = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbdesignation = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.Login.SuspendLayout();
            this.Sign_Up.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Change_Password.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Login);
            this.tabControl1.Controls.Add(this.Sign_Up);
            this.tabControl1.Controls.Add(this.Change_Password);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(394, 417);
            this.tabControl1.TabIndex = 0;
            // 
            // Login
            // 
            this.Login.BackColor = System.Drawing.Color.LavenderBlush;
            this.Login.Controls.Add(this.Apprbtn);
            this.Login.Controls.Add(this.passtxt);
            this.Login.Controls.Add(this.Bartxt);
            this.Login.Controls.Add(this.label1);
            this.Login.Controls.Add(this.label2);
            this.Login.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Login.Location = new System.Drawing.Point(4, 22);
            this.Login.Name = "Login";
            this.Login.Padding = new System.Windows.Forms.Padding(3);
            this.Login.Size = new System.Drawing.Size(386, 391);
            this.Login.TabIndex = 1;
            this.Login.Text = "Login";
            // 
            // Apprbtn
            // 
            this.Apprbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Apprbtn.Location = new System.Drawing.Point(185, 190);
            this.Apprbtn.Name = "Apprbtn";
            this.Apprbtn.Size = new System.Drawing.Size(75, 31);
            this.Apprbtn.TabIndex = 9;
            this.Apprbtn.Text = "Approval";
            this.Apprbtn.UseVisualStyleBackColor = true;
            this.Apprbtn.Click += new System.EventHandler(this.Apprbtn_Click_1);
            // 
            // passtxt
            // 
            this.passtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passtxt.Location = new System.Drawing.Point(185, 121);
            this.passtxt.Name = "passtxt";
            this.passtxt.Size = new System.Drawing.Size(148, 24);
            this.passtxt.TabIndex = 8;
            // 
            // Bartxt
            // 
            this.Bartxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bartxt.Location = new System.Drawing.Point(185, 56);
            this.Bartxt.Name = "Bartxt";
            this.Bartxt.Size = new System.Drawing.Size(148, 24);
            this.Bartxt.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Eneter Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter  Barcode";
            // 
            // Sign_Up
            // 
            this.Sign_Up.BackColor = System.Drawing.Color.Azure;
            this.Sign_Up.Controls.Add(this.cbdesignation);
            this.Sign_Up.Controls.Add(this.label5);
            this.Sign_Up.Controls.Add(this.pictureBox1);
            this.Sign_Up.Controls.Add(this.savebtn);
            this.Sign_Up.Controls.Add(this.uploadbtn);
            this.Sign_Up.Controls.Add(this.Pwdtxt);
            this.Sign_Up.Controls.Add(this.Barcodetxt);
            this.Sign_Up.Controls.Add(this.imgibl);
            this.Sign_Up.Controls.Add(this.pwdlbl);
            this.Sign_Up.Controls.Add(this.barcodelbl);
            this.Sign_Up.Location = new System.Drawing.Point(4, 22);
            this.Sign_Up.Name = "Sign_Up";
            this.Sign_Up.Padding = new System.Windows.Forms.Padding(3);
            this.Sign_Up.Size = new System.Drawing.Size(386, 391);
            this.Sign_Up.TabIndex = 0;
            this.Sign_Up.Text = "Sign Up";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.MistyRose;
            this.pictureBox1.Location = new System.Drawing.Point(217, 249);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(154, 68);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // savebtn
            // 
            this.savebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savebtn.Location = new System.Drawing.Point(217, 336);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(75, 32);
            this.savebtn.TabIndex = 6;
            this.savebtn.Text = "Save";
            this.savebtn.UseVisualStyleBackColor = true;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click_1);
            // 
            // uploadbtn
            // 
            this.uploadbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadbtn.Location = new System.Drawing.Point(217, 195);
            this.uploadbtn.Name = "uploadbtn";
            this.uploadbtn.Size = new System.Drawing.Size(75, 35);
            this.uploadbtn.TabIndex = 5;
            this.uploadbtn.Text = "Upload";
            this.uploadbtn.UseVisualStyleBackColor = true;
            this.uploadbtn.Click += new System.EventHandler(this.uploadbtn_Click_1);
            // 
            // Pwdtxt
            // 
            this.Pwdtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pwdtxt.Location = new System.Drawing.Point(217, 99);
            this.Pwdtxt.Name = "Pwdtxt";
            this.Pwdtxt.Size = new System.Drawing.Size(144, 24);
            this.Pwdtxt.TabIndex = 4;
            // 
            // Barcodetxt
            // 
            this.Barcodetxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Barcodetxt.Location = new System.Drawing.Point(217, 45);
            this.Barcodetxt.Name = "Barcodetxt";
            this.Barcodetxt.Size = new System.Drawing.Size(144, 24);
            this.Barcodetxt.TabIndex = 3;
            // 
            // imgibl
            // 
            this.imgibl.AutoSize = true;
            this.imgibl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgibl.Location = new System.Drawing.Point(46, 203);
            this.imgibl.Name = "imgibl";
            this.imgibl.Size = new System.Drawing.Size(165, 18);
            this.imgibl.TabIndex = 2;
            this.imgibl.Text = "Upload Signature Image";
            // 
            // pwdlbl
            // 
            this.pwdlbl.AutoSize = true;
            this.pwdlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwdlbl.Location = new System.Drawing.Point(46, 99);
            this.pwdlbl.Name = "pwdlbl";
            this.pwdlbl.Size = new System.Drawing.Size(122, 18);
            this.pwdlbl.TabIndex = 1;
            this.pwdlbl.Text = "Eneter Password";
            // 
            // barcodelbl
            // 
            this.barcodelbl.AutoSize = true;
            this.barcodelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barcodelbl.Location = new System.Drawing.Point(46, 51);
            this.barcodelbl.Name = "barcodelbl";
            this.barcodelbl.Size = new System.Drawing.Size(107, 18);
            this.barcodelbl.TabIndex = 0;
            this.barcodelbl.Text = "Enter  Barcode";
            // 
            // Change_Password
            // 
            this.Change_Password.BackColor = System.Drawing.Color.Thistle;
            this.Change_Password.Controls.Add(this.Updatebtn);
            this.Change_Password.Controls.Add(this.pdtxt);
            this.Change_Password.Controls.Add(this.textBox1);
            this.Change_Password.Controls.Add(this.label4);
            this.Change_Password.Controls.Add(this.label3);
            this.Change_Password.Location = new System.Drawing.Point(4, 22);
            this.Change_Password.Name = "Change_Password";
            this.Change_Password.Size = new System.Drawing.Size(386, 391);
            this.Change_Password.TabIndex = 2;
            this.Change_Password.Text = "Change Password";
            // 
            // Updatebtn
            // 
            this.Updatebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Updatebtn.Location = new System.Drawing.Point(208, 208);
            this.Updatebtn.Name = "Updatebtn";
            this.Updatebtn.Size = new System.Drawing.Size(75, 28);
            this.Updatebtn.TabIndex = 4;
            this.Updatebtn.Text = "Update";
            this.Updatebtn.UseVisualStyleBackColor = true;
            this.Updatebtn.Click += new System.EventHandler(this.Updatebtn_Click_1);
            // 
            // pdtxt
            // 
            this.pdtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pdtxt.Location = new System.Drawing.Point(208, 158);
            this.pdtxt.Name = "pdtxt";
            this.pdtxt.Size = new System.Drawing.Size(117, 22);
            this.pdtxt.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(208, 87);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 22);
            this.textBox1.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(95, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Enter Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(95, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Enter  Barcode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(46, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "Designation";
            // 
            // cbdesignation
            // 
            this.cbdesignation.FormattingEnabled = true;
            this.cbdesignation.Items.AddRange(new object[] {
            "Trainer",
            "Operator",
            "IE Specialist",
            "QIP Incharge",
            "Line Supervisor",
            "Plant Incharge",
            "Assembly Training Supervisor",
            "Senior Supervisor of Training Dept"});
            this.cbdesignation.Location = new System.Drawing.Point(217, 147);
            this.cbdesignation.Name = "cbdesignation";
            this.cbdesignation.Size = new System.Drawing.Size(144, 21);
            this.cbdesignation.TabIndex = 9;
            // 
            // Signature_Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 417);
            this.Controls.Add(this.tabControl1);
            this.Name = "Signature_Upload";
            this.Text = "QCO_Get_Signatures";
            this.tabControl1.ResumeLayout(false);
            this.Login.ResumeLayout(false);
            this.Login.PerformLayout();
            this.Sign_Up.ResumeLayout(false);
            this.Sign_Up.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Change_Password.ResumeLayout(false);
            this.Change_Password.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Sign_Up;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button savebtn;
        private System.Windows.Forms.Button uploadbtn;
        private System.Windows.Forms.TextBox Pwdtxt;
        private System.Windows.Forms.TextBox Barcodetxt;
        private System.Windows.Forms.Label imgibl;
        private System.Windows.Forms.Label pwdlbl;
        private System.Windows.Forms.Label barcodelbl;
        private System.Windows.Forms.TabPage Login;
        private System.Windows.Forms.Button Apprbtn;
        private System.Windows.Forms.TextBox passtxt;
        private System.Windows.Forms.TextBox Bartxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage Change_Password;
        private System.Windows.Forms.Button Updatebtn;
        private System.Windows.Forms.TextBox pdtxt;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbdesignation;
    }
}