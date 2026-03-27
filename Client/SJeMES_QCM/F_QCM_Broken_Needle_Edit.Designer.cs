
namespace SJeMES_QCM
{
    partial class F_QCM_Broken_Needle_Edit
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
            this.lab_vend = new System.Windows.Forms.Label();
            this.lab_ProdLine = new System.Windows.Forms.Label();
            this.lab_remarks = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnimg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lab_vend
            // 
            this.lab_vend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_vend.AutoSize = true;
            this.lab_vend.BackColor = System.Drawing.Color.White;
            this.lab_vend.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_vend.Location = new System.Drawing.Point(87, 125);
            this.lab_vend.Name = "lab_vend";
            this.lab_vend.Size = new System.Drawing.Size(42, 21);
            this.lab_vend.TabIndex = 0;
            this.lab_vend.Text = "厂区";
            // 
            // lab_ProdLine
            // 
            this.lab_ProdLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_ProdLine.AutoSize = true;
            this.lab_ProdLine.BackColor = System.Drawing.Color.White;
            this.lab_ProdLine.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ProdLine.Location = new System.Drawing.Point(456, 125);
            this.lab_ProdLine.Name = "lab_ProdLine";
            this.lab_ProdLine.Size = new System.Drawing.Size(42, 21);
            this.lab_ProdLine.TabIndex = 1;
            this.lab_ProdLine.Text = "产线";
            // 
            // lab_remarks
            // 
            this.lab_remarks.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_remarks.AutoSize = true;
            this.lab_remarks.BackColor = System.Drawing.Color.White;
            this.lab_remarks.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_remarks.Location = new System.Drawing.Point(78, 316);
            this.lab_remarks.Name = "lab_remarks";
            this.lab_remarks.Size = new System.Drawing.Size(42, 21);
            this.lab_remarks.TabIndex = 2;
            this.lab_remarks.Text = "备注";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(148, 122);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(146, 29);
            this.textBox1.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(530, 122);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 29);
            this.comboBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(82, 354);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(594, 145);
            this.textBox2.TabIndex = 5;
            // 
            // btnclose
            // 
            this.btnclose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnclose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnclose.Location = new System.Drawing.Point(240, 531);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(105, 32);
            this.btnclose.TabIndex = 6;
            this.btnclose.Text = "取消";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnsubmit
            // 
            this.btnsubmit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnsubmit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnsubmit.Location = new System.Drawing.Point(427, 531);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(105, 32);
            this.btnsubmit.TabIndex = 7;
            this.btnsubmit.Text = "保存";
            this.btnsubmit.UseVisualStyleBackColor = true;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Location = new System.Drawing.Point(91, 174);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(203, 134);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // btnimg
            // 
            this.btnimg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnimg.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnimg.Location = new System.Drawing.Point(571, 174);
            this.btnimg.Name = "btnimg";
            this.btnimg.Size = new System.Drawing.Size(105, 32);
            this.btnimg.TabIndex = 9;
            this.btnimg.Text = "拍照上传";
            this.btnimg.UseVisualStyleBackColor = true;
            this.btnimg.Click += new System.EventHandler(this.btnimg_Click);
            // 
            // F_QCM_Broken_Needle_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 600);
            this.Controls.Add(this.btnimg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnsubmit);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lab_remarks);
            this.Controls.Add(this.lab_ProdLine);
            this.Controls.Add(this.lab_vend);
            this.Name = "F_QCM_Broken_Needle_Edit";
            this.Text = "断针编辑";
            this.Load += new System.EventHandler(this.F_QCM_Broken_Needle_Edit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_vend;
        private System.Windows.Forms.Label lab_ProdLine;
        private System.Windows.Forms.Label lab_remarks;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnimg;
    }
}