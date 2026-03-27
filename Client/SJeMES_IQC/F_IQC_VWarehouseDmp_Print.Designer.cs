using FastReport.Preview;

namespace SJeMES_IQC
{
    partial class F_IQC_VWarehouseDmp_Print
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cscode = new System.Windows.Forms.TextBox();
            this.txt_stoc_no = new System.Windows.Forms.TextBox();
            this.txt_item_no = new System.Windows.Forms.TextBox();
            this.dateTimeP_end_date = new System.Windows.Forms.DateTimePicker();
            this.dateTimeP_putin_date = new System.Windows.Forms.DateTimePicker();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_pljybg = new System.Windows.Forms.Button();
            this.btn_ycl = new System.Windows.Forms.Button();
            this.btn_out = new System.Windows.Forms.Button();
            this.textBoxfailremark = new SJeMES_Control_Library.Controls.TextBoxEx();
            this.label26 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(254, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "不合格说明";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(41, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "厂商代码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(73, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "仓库：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(41, 349);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "材料编号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(40, 399);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "收料日期：";
            // 
            // txt_cscode
            // 
            this.txt_cscode.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_cscode.Location = new System.Drawing.Point(258, 249);
            this.txt_cscode.Name = "txt_cscode";
            this.txt_cscode.Size = new System.Drawing.Size(451, 29);
            this.txt_cscode.TabIndex = 6;
            // 
            // txt_stoc_no
            // 
            this.txt_stoc_no.BackColor = System.Drawing.SystemColors.Info;
            this.txt_stoc_no.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_stoc_no.Location = new System.Drawing.Point(258, 297);
            this.txt_stoc_no.Name = "txt_stoc_no";
            this.txt_stoc_no.ReadOnly = true;
            this.txt_stoc_no.Size = new System.Drawing.Size(259, 29);
            this.txt_stoc_no.TabIndex = 7;
            this.txt_stoc_no.Click += new System.EventHandler(this.txt_stoc_no_Click);
            // 
            // txt_item_no
            // 
            this.txt_item_no.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_item_no.Location = new System.Drawing.Point(258, 345);
            this.txt_item_no.Name = "txt_item_no";
            this.txt_item_no.Size = new System.Drawing.Size(438, 29);
            this.txt_item_no.TabIndex = 8;
            // 
            // dateTimeP_end_date
            // 
            this.dateTimeP_end_date.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_end_date.Location = new System.Drawing.Point(505, 391);
            this.dateTimeP_end_date.Name = "dateTimeP_end_date";
            this.dateTimeP_end_date.Size = new System.Drawing.Size(191, 29);
            this.dateTimeP_end_date.TabIndex = 132;
            // 
            // dateTimeP_putin_date
            // 
            this.dateTimeP_putin_date.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_putin_date.Location = new System.Drawing.Point(258, 392);
            this.dateTimeP_putin_date.Name = "dateTimeP_putin_date";
            this.dateTimeP_putin_date.Size = new System.Drawing.Size(186, 29);
            this.dateTimeP_putin_date.TabIndex = 131;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox4.Location = new System.Drawing.Point(450, 392);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(45, 34);
            this.textBox4.TabIndex = 133;
            this.textBox4.Text = "～";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(535, 300);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 21);
            this.label6.TabIndex = 134;
            this.label6.Text = "格式为：仓库代号|工厂";
            // 
            // btn_pljybg
            // 
            this.btn_pljybg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pljybg.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_pljybg.Location = new System.Drawing.Point(23, 445);
            this.btn_pljybg.Name = "btn_pljybg";
            this.btn_pljybg.Size = new System.Drawing.Size(128, 35);
            this.btn_pljybg.TabIndex = 137;
            this.btn_pljybg.Text = "皮料检验报告";
            this.btn_pljybg.UseVisualStyleBackColor = true;
            this.btn_pljybg.Click += new System.EventHandler(this.btn_pljybg_Click);
            // 
            // btn_ycl
            // 
            this.btn_ycl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ycl.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ycl.Location = new System.Drawing.Point(207, 445);
            this.btn_ycl.Name = "btn_ycl";
            this.btn_ycl.Size = new System.Drawing.Size(128, 35);
            this.btn_ycl.TabIndex = 138;
            this.btn_ycl.Text = "原材料报告";
            this.btn_ycl.UseVisualStyleBackColor = true;
            this.btn_ycl.Click += new System.EventHandler(this.btn_ycl_Click);
            // 
            // btn_out
            // 
            this.btn_out.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_out.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_out.Location = new System.Drawing.Point(568, 445);
            this.btn_out.Name = "btn_out";
            this.btn_out.Size = new System.Drawing.Size(128, 35);
            this.btn_out.TabIndex = 139;
            this.btn_out.Text = "返回";
            this.btn_out.UseVisualStyleBackColor = true;
            this.btn_out.Click += new System.EventHandler(this.btn_out_Click);
            // 
            // textBoxfailremark
            // 
            this.textBoxfailremark.DecLength = 2;
            this.textBoxfailremark.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxfailremark.InputType = SJeMES_Control_Library.TextInputType.NotControl;
            this.textBoxfailremark.Location = new System.Drawing.Point(23, 107);
            this.textBoxfailremark.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.textBoxfailremark.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.textBoxfailremark.Multiline = true;
            this.textBoxfailremark.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.textBoxfailremark.Name = "textBoxfailremark";
            this.textBoxfailremark.OldText = null;
            this.textBoxfailremark.PromptColor = System.Drawing.Color.Gray;
            this.textBoxfailremark.PromptFont = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxfailremark.PromptText = "";
            this.textBoxfailremark.RegexPattern = "";
            this.textBoxfailremark.Size = new System.Drawing.Size(686, 132);
            this.textBoxfailremark.TabIndex = 1;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(702, 349);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(28, 29);
            this.label26.TabIndex = 141;
            this.label26.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(702, 391);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 29);
            this.label7.TabIndex = 142;
            this.label7.Text = "*";
            // 
            // F_IQC_VWarehouseDmp_Print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 505);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.btn_out);
            this.Controls.Add(this.btn_ycl);
            this.Controls.Add(this.btn_pljybg);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.dateTimeP_end_date);
            this.Controls.Add(this.dateTimeP_putin_date);
            this.Controls.Add(this.txt_item_no);
            this.Controls.Add(this.txt_stoc_no);
            this.Controls.Add(this.txt_cscode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxfailremark);
            this.Controls.Add(this.label1);
            this.Name = "F_IQC_VWarehouseDmp_Print";
            this.Padding = new System.Windows.Forms.Padding(20, 22, 20, 22);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检验报告打印";
            this.Load += new System.EventHandler(this.F_IQC_VWarehouseDmp_Print_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private SJeMES_Control_Library.Controls.TextBoxEx textBoxfailremark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_cscode;
        private System.Windows.Forms.TextBox txt_stoc_no;
        private System.Windows.Forms.TextBox txt_item_no;
        private System.Windows.Forms.DateTimePicker dateTimeP_end_date;
        private System.Windows.Forms.DateTimePicker dateTimeP_putin_date;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_pljybg;
        private System.Windows.Forms.Button btn_ycl;
        private System.Windows.Forms.Button btn_out;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label7;
    }
}