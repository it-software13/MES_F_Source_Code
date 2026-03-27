namespace Cutting_LabelPrint
{
    partial class AddUsers
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
            this.Barcode = new System.Windows.Forms.Label();
            this.Department = new System.Windows.Forms.Label();
            this.txt_barcode = new System.Windows.Forms.TextBox();
            this.txt_dpt = new System.Windows.Forms.TextBox();
            this.Add = new System.Windows.Forms.Button();
            this.Register_head = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Barcode
            // 
            this.Barcode.AutoSize = true;
            this.Barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Barcode.Location = new System.Drawing.Point(95, 100);
            this.Barcode.MaximumSize = new System.Drawing.Size(100, 30);
            this.Barcode.MinimumSize = new System.Drawing.Size(100, 20);
            this.Barcode.Name = "Barcode";
            this.Barcode.Size = new System.Drawing.Size(100, 20);
            this.Barcode.TabIndex = 0;
            this.Barcode.Text = "Barcode : ";
            this.Barcode.Click += new System.EventHandler(this.Barcode_Click);
            // 
            // Department
            // 
            this.Department.AutoSize = true;
            this.Department.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Department.Location = new System.Drawing.Point(95, 146);
            this.Department.MaximumSize = new System.Drawing.Size(100, 30);
            this.Department.MinimumSize = new System.Drawing.Size(100, 20);
            this.Department.Name = "Department";
            this.Department.Size = new System.Drawing.Size(100, 20);
            this.Department.TabIndex = 1;
            this.Department.Text = "Department : ";
            // 
            // txt_barcode
            // 
            this.txt_barcode.ForeColor = System.Drawing.Color.Navy;
            this.txt_barcode.Location = new System.Drawing.Point(263, 97);
            this.txt_barcode.Multiline = true;
            this.txt_barcode.Name = "txt_barcode";
            this.txt_barcode.Size = new System.Drawing.Size(167, 23);
            this.txt_barcode.TabIndex = 4;
            // 
            // txt_dpt
            // 
            this.txt_dpt.ForeColor = System.Drawing.Color.Navy;
            this.txt_dpt.Location = new System.Drawing.Point(263, 146);
            this.txt_dpt.Multiline = true;
            this.txt_dpt.Name = "txt_dpt";
            this.txt_dpt.Size = new System.Drawing.Size(167, 23);
            this.txt_dpt.TabIndex = 5;
            this.txt_dpt.TextChanged += new System.EventHandler(this.txt_dpt_TextChanged);
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.SystemColors.Highlight;
            this.Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Add.Location = new System.Drawing.Point(179, 197);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(96, 35);
            this.Add.TabIndex = 7;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Register_head
            // 
            this.Register_head.AutoSize = true;
            this.Register_head.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Register_head.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Register_head.Location = new System.Drawing.Point(173, 23);
            this.Register_head.MaximumSize = new System.Drawing.Size(200, 30);
            this.Register_head.MinimumSize = new System.Drawing.Size(100, 20);
            this.Register_head.Name = "Register_head";
            this.Register_head.Size = new System.Drawing.Size(124, 30);
            this.Register_head.TabIndex = 8;
            this.Register_head.Text = "Register";
            // 
            // AddUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 260);
            this.Controls.Add(this.Register_head);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.txt_dpt);
            this.Controls.Add(this.txt_barcode);
            this.Controls.Add(this.Department);
            this.Controls.Add(this.Barcode);
            this.Name = "AddUsers";
            this.Text = "AddUsers";
            this.Load += new System.EventHandler(this.AddUsers_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Barcode;
        private System.Windows.Forms.Label Department;
        private System.Windows.Forms.TextBox txt_barcode;
        private System.Windows.Forms.TextBox txt_dpt;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Label Register_head;
    }
}