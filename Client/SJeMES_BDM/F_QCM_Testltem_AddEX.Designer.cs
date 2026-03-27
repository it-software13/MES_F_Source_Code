
namespace SJeMES_BDM
{
    partial class F_QCM_Testltem_AddEX
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
            this.txt_min_value = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txt_max_value = new System.Windows.Forms.TextBox();
            this.lab_unit = new System.Windows.Forms.Label();
            this.lab_CM = new System.Windows.Forms.Label();
            this.lab_remarks = new System.Windows.Forms.Label();
            this.richTextBox_remarks = new System.Windows.Forms.RichTextBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.txt_value = new System.Windows.Forms.TextBox();
            this.lab_NumberOnly = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_min_value
            // 
            this.txt_min_value.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_min_value.Location = new System.Drawing.Point(92, 93);
            this.txt_min_value.Multiline = true;
            this.txt_min_value.Name = "txt_min_value";
            this.txt_min_value.Size = new System.Drawing.Size(117, 35);
            this.txt_min_value.TabIndex = 2;
            this.txt_min_value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(215, 93);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(39, 34);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_max_value
            // 
            this.txt_max_value.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_max_value.Location = new System.Drawing.Point(256, 93);
            this.txt_max_value.Multiline = true;
            this.txt_max_value.Name = "txt_max_value";
            this.txt_max_value.Size = new System.Drawing.Size(105, 35);
            this.txt_max_value.TabIndex = 3;
            this.txt_max_value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // lab_unit
            // 
            this.lab_unit.AutoSize = true;
            this.lab_unit.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_unit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_unit.Location = new System.Drawing.Point(365, 69);
            this.lab_unit.Name = "lab_unit";
            this.lab_unit.Size = new System.Drawing.Size(40, 16);
            this.lab_unit.TabIndex = 5;
            this.lab_unit.Text = "单位";
            // 
            // lab_CM
            // 
            this.lab_CM.AutoSize = true;
            this.lab_CM.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_CM.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_CM.Location = new System.Drawing.Point(362, 93);
            this.lab_CM.Name = "lab_CM";
            this.lab_CM.Size = new System.Drawing.Size(47, 33);
            this.lab_CM.TabIndex = 6;
            this.lab_CM.Text = "cm";
            // 
            // lab_remarks
            // 
            this.lab_remarks.AutoSize = true;
            this.lab_remarks.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_remarks.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_remarks.Location = new System.Drawing.Point(77, 141);
            this.lab_remarks.Name = "lab_remarks";
            this.lab_remarks.Size = new System.Drawing.Size(40, 16);
            this.lab_remarks.TabIndex = 7;
            this.lab_remarks.Text = "备注";
            // 
            // richTextBox_remarks
            // 
            this.richTextBox_remarks.Location = new System.Drawing.Point(75, 171);
            this.richTextBox_remarks.Name = "richTextBox_remarks";
            this.richTextBox_remarks.Size = new System.Drawing.Size(334, 110);
            this.richTextBox_remarks.TabIndex = 4;
            this.richTextBox_remarks.Text = "";
            // 
            // btn_add
            // 
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(322, 291);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(85, 30);
            this.btn_add.TabIndex = 9;
            this.btn_add.Text = "确认添加";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // txt_value
            // 
            this.txt_value.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_value.Location = new System.Drawing.Point(92, 93);
            this.txt_value.Multiline = true;
            this.txt_value.Name = "txt_value";
            this.txt_value.Size = new System.Drawing.Size(269, 35);
            this.txt_value.TabIndex = 1;
            this.txt_value.Visible = false;
            this.txt_value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // lab_NumberOnly
            // 
            this.lab_NumberOnly.AutoSize = true;
            this.lab_NumberOnly.ForeColor = System.Drawing.Color.Red;
            this.lab_NumberOnly.Location = new System.Drawing.Point(259, 139);
            this.lab_NumberOnly.Name = "lab_NumberOnly";
            this.lab_NumberOnly.Size = new System.Drawing.Size(77, 12);
            this.lab_NumberOnly.TabIndex = 11;
            this.lab_NumberOnly.Text = "只能填写数字";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 12;
            // 
            // F_QCM_Testltem_AddEX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 328);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lab_NumberOnly);
            this.Controls.Add(this.txt_value);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.richTextBox_remarks);
            this.Controls.Add(this.lab_remarks);
            this.Controls.Add(this.lab_CM);
            this.Controls.Add(this.lab_unit);
            this.Controls.Add(this.txt_max_value);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txt_min_value);
            this.MaximizeBox = false;
            this.Name = "F_QCM_Testltem_AddEX";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增测试标准";
            this.Load += new System.EventHandler(this.F_QCM_Testltem_AddEX_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_min_value;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txt_max_value;
        private System.Windows.Forms.Label lab_unit;
        private System.Windows.Forms.Label lab_CM;
        private System.Windows.Forms.Label lab_remarks;
        private System.Windows.Forms.RichTextBox richTextBox_remarks;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.TextBox txt_value;
        private System.Windows.Forms.Label lab_NumberOnly;
        private System.Windows.Forms.Label label5;
    }
}