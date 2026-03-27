
namespace Production_Material_Issue
{
    partial class Production_Material_Request
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_submit = new System.Windows.Forms.Button();
            this.txtmname = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMpo = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtremarks = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtmcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtdcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.autocompleteMenu2 = new AutocompleteMenuNS.AutocompleteMenu();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Linen;
            this.panel1.Controls.Add(this.btn_cancel);
            this.panel1.Controls.Add(this.btn_submit);
            this.panel1.Controls.Add(this.txtmname);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtMpo);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtremarks);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtqty);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtmcode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtdcode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(4, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1090, 248);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(71, 167);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(83, 31);
            this.btn_cancel.TabIndex = 11;
            this.btn_cancel.Text = "Clear";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.Location = new System.Drawing.Point(892, 167);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(90, 31);
            this.btn_submit.TabIndex = 10;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // txtmname
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtmname, null);
            this.txtmname.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmname.Location = new System.Drawing.Point(140, 88);
            this.txtmname.Name = "txtmname";
            this.txtmname.ReadOnly = true;
            this.txtmname.Size = new System.Drawing.Size(201, 49);
            this.txtmname.TabIndex = 14;
            this.txtmname.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Material_Name";
            // 
            // txtMpo
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtMpo, null);
            this.txtMpo.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMpo.Location = new System.Drawing.Point(521, 13);
            this.txtMpo.Name = "txtMpo";
            this.txtMpo.ReadOnly = true;
            this.txtMpo.Size = new System.Drawing.Size(201, 48);
            this.txtMpo.TabIndex = 11;
            this.txtMpo.Text = "";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Production_Material_Issue.Properties.Resources.search;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(485, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 22);
            this.button1.TabIndex = 10;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtremarks
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtremarks, null);
            this.txtremarks.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtremarks.Location = new System.Drawing.Point(917, 102);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(143, 21);
            this.txtremarks.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(368, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Request_Quantity";
            // 
            // txtqty
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtqty, null);
            this.txtqty.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtqty.Location = new System.Drawing.Point(522, 99);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(143, 21);
            this.txtqty.TabIndex = 7;
            this.txtqty.TextChanged += new System.EventHandler(this.txtqty_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(739, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Production_Remarks";
            // 
            // txtmcode
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtmcode, null);
            this.txtmcode.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmcode.Location = new System.Drawing.Point(895, 15);
            this.txtmcode.Name = "txtmcode";
            this.txtmcode.Size = new System.Drawing.Size(143, 21);
            this.txtmcode.TabIndex = 5;
            this.txtmcode.TextChanged += new System.EventHandler(this.txtmcode_TextChanged);
            this.txtmcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtmcode_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(761, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Material_Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(378, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "M_Prod_Num";
            // 
            // txtdcode
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.txtdcode, null);
            this.txtdcode.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdcode.Location = new System.Drawing.Point(181, 25);
            this.txtdcode.Name = "txtdcode";
            this.txtdcode.Size = new System.Drawing.Size(143, 21);
            this.txtdcode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Request_Dept_Code";
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu1.ImageList = null;
            this.autocompleteMenu1.Items = new string[0];
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // autocompleteMenu2
            // 
            this.autocompleteMenu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu2.ImageList = null;
            this.autocompleteMenu2.Items = new string[0];
            this.autocompleteMenu2.TargetControlWrapper = null;
            // 
            // Production_Material_Request
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 317);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Production_Material_Request";
            this.Text = "Production_Material_Request";
            this.Load += new System.EventHandler(this.Production_Material_Request_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.TextBox txtremarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtqty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtmcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtdcode;
        private System.Windows.Forms.Label label1;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txtMpo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox txtmname;
    }
}