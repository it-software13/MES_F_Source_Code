
namespace SJeMES_BDM
{
    partial class F_BDM_FittingsampleLocation_Add
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
            this.txt_fittingsamplelocation = new System.Windows.Forms.TextBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Out = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.lab_fittingsamplelocation = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lab_art = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_SUPPLIERS_NAME = new System.Windows.Forms.TextBox();
            this.lab_supplier = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_NAME_S = new System.Windows.Forms.TextBox();
            this.lab_item_name = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_ITEM_NO = new System.Windows.Forms.TextBox();
            this.lab_item_no = new System.Windows.Forms.Label();
            this.lab_bringout = new System.Windows.Forms.Label();
            this.cbo_PARENT_ITEM_NO = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txt_fittingsamplelocation
            // 
            this.txt_fittingsamplelocation.BackColor = System.Drawing.SystemColors.Info;
            this.txt_fittingsamplelocation.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fittingsamplelocation.Location = new System.Drawing.Point(128, 231);
            this.txt_fittingsamplelocation.Name = "txt_fittingsamplelocation";
            this.txt_fittingsamplelocation.Size = new System.Drawing.Size(158, 34);
            this.txt_fittingsamplelocation.TabIndex = 54;
            this.txt_fittingsamplelocation.Click += new System.EventHandler(this.txt_fittingsamplelocation_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(342, 298);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(85, 30);
            this.btn_Add.TabIndex = 56;
            this.btn_Add.Text = "确认";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Out
            // 
            this.btn_Out.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Out.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Out.Location = new System.Drawing.Point(208, 298);
            this.btn_Out.Name = "btn_Out";
            this.btn_Out.Size = new System.Drawing.Size(85, 30);
            this.btn_Out.TabIndex = 55;
            this.btn_Out.Text = "取消";
            this.btn_Out.UseVisualStyleBackColor = true;
            this.btn_Out.Click += new System.EventHandler(this.btn_Out_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(289, 234);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(28, 29);
            this.label26.TabIndex = 66;
            this.label26.Text = "*";
            // 
            // lab_fittingsamplelocation
            // 
            this.lab_fittingsamplelocation.AutoSize = true;
            this.lab_fittingsamplelocation.BackColor = System.Drawing.Color.White;
            this.lab_fittingsamplelocation.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_fittingsamplelocation.Location = new System.Drawing.Point(18, 236);
            this.lab_fittingsamplelocation.Name = "lab_fittingsamplelocation";
            this.lab_fittingsamplelocation.Size = new System.Drawing.Size(107, 25);
            this.lab_fittingsamplelocation.TabIndex = 65;
            this.lab_fittingsamplelocation.Text = "存放位置：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(544, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 29);
            this.label5.TabIndex = 64;
            this.label5.Text = "*";
            // 
            // lab_art
            // 
            this.lab_art.AutoSize = true;
            this.lab_art.BackColor = System.Drawing.Color.White;
            this.lab_art.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_art.Location = new System.Drawing.Point(318, 175);
            this.lab_art.Name = "lab_art";
            this.lab_art.Size = new System.Drawing.Size(67, 25);
            this.lab_art.TabIndex = 63;
            this.lab_art.Text = "ART：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(290, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 29);
            this.label3.TabIndex = 62;
            this.label3.Text = "*";
            // 
            // txt_SUPPLIERS_NAME
            // 
            this.txt_SUPPLIERS_NAME.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SUPPLIERS_NAME.Location = new System.Drawing.Point(131, 170);
            this.txt_SUPPLIERS_NAME.Name = "txt_SUPPLIERS_NAME";
            this.txt_SUPPLIERS_NAME.ReadOnly = true;
            this.txt_SUPPLIERS_NAME.Size = new System.Drawing.Size(156, 34);
            this.txt_SUPPLIERS_NAME.TabIndex = 52;
            // 
            // lab_supplier
            // 
            this.lab_supplier.AutoSize = true;
            this.lab_supplier.BackColor = System.Drawing.Color.White;
            this.lab_supplier.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_supplier.Location = new System.Drawing.Point(40, 175);
            this.lab_supplier.Name = "lab_supplier";
            this.lab_supplier.Size = new System.Drawing.Size(88, 25);
            this.lab_supplier.TabIndex = 61;
            this.lab_supplier.Text = "供应商：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(546, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 29);
            this.label1.TabIndex = 60;
            this.label1.Text = "*";
            // 
            // txt_NAME_S
            // 
            this.txt_NAME_S.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NAME_S.Location = new System.Drawing.Point(387, 106);
            this.txt_NAME_S.Name = "txt_NAME_S";
            this.txt_NAME_S.ReadOnly = true;
            this.txt_NAME_S.Size = new System.Drawing.Size(156, 34);
            this.txt_NAME_S.TabIndex = 51;
            // 
            // lab_item_name
            // 
            this.lab_item_name.AutoSize = true;
            this.lab_item_name.BackColor = System.Drawing.Color.White;
            this.lab_item_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_item_name.Location = new System.Drawing.Point(316, 111);
            this.lab_item_name.Name = "lab_item_name";
            this.lab_item_name.Size = new System.Drawing.Size(69, 25);
            this.lab_item_name.TabIndex = 59;
            this.lab_item_name.Text = "品名：";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(290, 109);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(28, 29);
            this.label30.TabIndex = 58;
            this.label30.Text = "*";
            // 
            // txt_ITEM_NO
            // 
            this.txt_ITEM_NO.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ITEM_NO.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ITEM_NO.Location = new System.Drawing.Point(131, 106);
            this.txt_ITEM_NO.Name = "txt_ITEM_NO";
            this.txt_ITEM_NO.Size = new System.Drawing.Size(156, 34);
            this.txt_ITEM_NO.TabIndex = 50;
            this.txt_ITEM_NO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ITEM_NO_KeyDown);
            // 
            // lab_item_no
            // 
            this.lab_item_no.AutoSize = true;
            this.lab_item_no.BackColor = System.Drawing.Color.White;
            this.lab_item_no.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_item_no.Location = new System.Drawing.Point(59, 111);
            this.lab_item_no.Name = "lab_item_no";
            this.lab_item_no.Size = new System.Drawing.Size(69, 25);
            this.lab_item_no.TabIndex = 57;
            this.lab_item_no.Text = "品号：";
            // 
            // lab_bringout
            // 
            this.lab_bringout.AutoSize = true;
            this.lab_bringout.BackColor = System.Drawing.Color.Transparent;
            this.lab_bringout.ForeColor = System.Drawing.Color.Red;
            this.lab_bringout.Location = new System.Drawing.Point(131, 144);
            this.lab_bringout.Name = "lab_bringout";
            this.lab_bringout.Size = new System.Drawing.Size(77, 12);
            this.lab_bringout.TabIndex = 67;
            this.lab_bringout.Text = "输入回车带出";
            // 
            // cbo_PARENT_ITEM_NO
            // 
            this.cbo_PARENT_ITEM_NO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_PARENT_ITEM_NO.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.cbo_PARENT_ITEM_NO.FormattingEnabled = true;
            this.cbo_PARENT_ITEM_NO.Location = new System.Drawing.Point(387, 169);
            this.cbo_PARENT_ITEM_NO.Name = "cbo_PARENT_ITEM_NO";
            this.cbo_PARENT_ITEM_NO.Size = new System.Drawing.Size(156, 36);
            this.cbo_PARENT_ITEM_NO.TabIndex = 73;
            // 
            // F_BDM_FittingsampleLocation_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 370);
            this.Controls.Add(this.cbo_PARENT_ITEM_NO);
            this.Controls.Add(this.lab_bringout);
            this.Controls.Add(this.txt_fittingsamplelocation);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.btn_Out);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lab_fittingsamplelocation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lab_art);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_SUPPLIERS_NAME);
            this.Controls.Add(this.lab_supplier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_NAME_S);
            this.Controls.Add(this.lab_item_name);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.txt_ITEM_NO);
            this.Controls.Add(this.lab_item_no);
            this.MaximizeBox = false;
            this.Name = "F_BDM_FittingsampleLocation_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "样品库位新增/修改";
            this.Load += new System.EventHandler(this.F_BDM_FittingsampleLocation_Add_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_fittingsamplelocation;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Out;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lab_fittingsamplelocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_art;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_SUPPLIERS_NAME;
        private System.Windows.Forms.Label lab_supplier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_NAME_S;
        private System.Windows.Forms.Label lab_item_name;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_ITEM_NO;
        private System.Windows.Forms.Label lab_item_no;
        private System.Windows.Forms.Label lab_bringout;
        private System.Windows.Forms.ComboBox cbo_PARENT_ITEM_NO;
    }
}