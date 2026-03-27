
namespace SJeMES_BDM
{
    partial class F_BDM_DeviceType_Edit
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
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            this.txt_correction = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.order = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txt_Remark = new System.Windows.Forms.TextBox();
            this.combox_eq_type = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.White;
            label7.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            label7.Location = new System.Drawing.Point(22, 127);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(58, 21);
            label7.TabIndex = 27;
            label7.Text = "编号：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.White;
            label2.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            label2.Location = new System.Drawing.Point(629, 122);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(90, 21);
            label2.TabIndex = 25;
            label2.Text = "校正频率：";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.White;
            label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            label1.Location = new System.Drawing.Point(347, 127);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(58, 21);
            label1.TabIndex = 23;
            label1.Text = "名称：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.White;
            label3.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            label3.Location = new System.Drawing.Point(22, 189);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(58, 21);
            label3.TabIndex = 29;
            label3.Text = "备注：";
            // 
            // txt_correction
            // 
            this.txt_correction.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_correction.Location = new System.Drawing.Point(839, 114);
            this.txt_correction.Name = "txt_correction";
            this.txt_correction.Size = new System.Drawing.Size(156, 29);
            this.txt_correction.TabIndex = 26;
            this.txt_correction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_correction_KeyPress);
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name.Location = new System.Drawing.Point(452, 119);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(109, 29);
            this.name.TabIndex = 24;
            // 
            // order
            // 
            this.order.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.order.Location = new System.Drawing.Point(179, 124);
            this.order.Name = "order";
            this.order.ReadOnly = true;
            this.order.Size = new System.Drawing.Size(126, 29);
            this.order.TabIndex = 22;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(839, 274);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(115, 33);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "确认";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txt_Remark
            // 
            this.txt_Remark.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Remark.Location = new System.Drawing.Point(179, 181);
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(126, 29);
            this.txt_Remark.TabIndex = 30;
            // 
            // combox_eq_type
            // 
            this.combox_eq_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_eq_type.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combox_eq_type.FormattingEnabled = true;
            this.combox_eq_type.Location = new System.Drawing.Point(839, 189);
            this.combox_eq_type.Name = "combox_eq_type";
            this.combox_eq_type.Size = new System.Drawing.Size(156, 29);
            this.combox_eq_type.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(601, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 21);
            this.label4.TabIndex = 32;
            this.label4.Text = "管控类型(设备)：";
            // 
            // F_BDM_DeviceType_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 369);
            this.Controls.Add(this.combox_eq_type);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Remark);
            this.Controls.Add(label3);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(label7);
            this.Controls.Add(this.txt_correction);
            this.Controls.Add(label2);
            this.Controls.Add(this.name);
            this.Controls.Add(label1);
            this.Controls.Add(this.order);
            this.Name = "F_BDM_DeviceType_Edit";
            this.Text = "编辑";
            this.Load += new System.EventHandler(this.F_BDM_DeviceType_Edit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_correction;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox order;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txt_Remark;
        private System.Windows.Forms.ComboBox combox_eq_type;
        private System.Windows.Forms.Label label4;
    }
}