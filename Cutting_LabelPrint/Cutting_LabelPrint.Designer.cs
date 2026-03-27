
namespace Cutting_LabelPrint
{
    partial class Cutting_LabelPrint
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
            this.deselectbutton = new System.Windows.Forms.Button();
            this.selectallbutton = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.txt_PartNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_MasterPO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ORDER_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SALES_ORDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIZE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELIVERY_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Register = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.Register);
            this.panel1.Controls.Add(this.deselectbutton);
            this.panel1.Controls.Add(this.selectallbutton);
            this.panel1.Controls.Add(this.btn_delete);
            this.panel1.Controls.Add(this.btn_Search);
            this.panel1.Controls.Add(this.txt_PartNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_MasterPO);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1151, 84);
            this.panel1.TabIndex = 0;
            // 
            // deselectbutton
            // 
            this.deselectbutton.BackColor = System.Drawing.Color.MidnightBlue;
            this.deselectbutton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.deselectbutton.Location = new System.Drawing.Point(714, 25);
            this.deselectbutton.Name = "deselectbutton";
            this.deselectbutton.Size = new System.Drawing.Size(95, 32);
            this.deselectbutton.TabIndex = 8;
            this.deselectbutton.Text = "De_Select All";
            this.deselectbutton.UseVisualStyleBackColor = false;
            this.deselectbutton.Click += new System.EventHandler(this.deselectbutton_Click);
            // 
            // selectallbutton
            // 
            this.selectallbutton.BackColor = System.Drawing.Color.DarkSlateGray;
            this.selectallbutton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.selectallbutton.Location = new System.Drawing.Point(826, 25);
            this.selectallbutton.Name = "selectallbutton";
            this.selectallbutton.Size = new System.Drawing.Size(75, 34);
            this.selectallbutton.TabIndex = 7;
            this.selectallbutton.Text = "Select All";
            this.selectallbutton.UseVisualStyleBackColor = false;
            this.selectallbutton.Click += new System.EventHandler(this.selectallbutton_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete.BackColor = System.Drawing.Color.SaddleBrown;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_delete.Location = new System.Drawing.Point(918, 28);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(79, 27);
            this.btn_delete.TabIndex = 6;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Search.Location = new System.Drawing.Point(508, 28);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(72, 27);
            this.btn_Search.TabIndex = 4;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // txt_PartNo
            // 
            this.txt_PartNo.ForeColor = System.Drawing.Color.Navy;
            this.txt_PartNo.Location = new System.Drawing.Point(388, 30);
            this.txt_PartNo.Multiline = true;
            this.txt_PartNo.Name = "txt_PartNo";
            this.txt_PartNo.Size = new System.Drawing.Size(101, 23);
            this.txt_PartNo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(327, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Part No : ";
            // 
            // txt_MasterPO
            // 
            this.txt_MasterPO.ForeColor = System.Drawing.Color.Navy;
            this.txt_MasterPO.Location = new System.Drawing.Point(162, 30);
            this.txt_MasterPO.Multiline = true;
            this.txt_MasterPO.Name = "txt_MasterPO";
            this.txt_MasterPO.Size = new System.Drawing.Size(152, 23);
            this.txt_MasterPO.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(76, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Master PO : ";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1151, 560);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LavenderBlush;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.ORDER_NO,
            this.SALES_ORDER,
            this.MAPO,
            this.SIZE_NO,
            this.PART_NO,
            this.PART_NAME,
            this.DELIVERY_DATE,
            this.QUANTITY});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1147, 556);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Select
            // 
            this.Select.DataPropertyName = "Select";
            this.Select.Frozen = true;
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ORDER_NO
            // 
            this.ORDER_NO.DataPropertyName = "ORDER_NO";
            this.ORDER_NO.HeaderText = "ORDER NO";
            this.ORDER_NO.Name = "ORDER_NO";
            this.ORDER_NO.ReadOnly = true;
            // 
            // SALES_ORDER
            // 
            this.SALES_ORDER.DataPropertyName = "SALES_ORDER";
            this.SALES_ORDER.HeaderText = "SALES ORDER";
            this.SALES_ORDER.Name = "SALES_ORDER";
            this.SALES_ORDER.ReadOnly = true;
            this.SALES_ORDER.Width = 150;
            // 
            // MAPO
            // 
            this.MAPO.DataPropertyName = "MAPO";
            this.MAPO.HeaderText = "MAPO";
            this.MAPO.Name = "MAPO";
            this.MAPO.ReadOnly = true;
            // 
            // SIZE_NO
            // 
            this.SIZE_NO.DataPropertyName = "SIZE_NO";
            this.SIZE_NO.HeaderText = "SIZE NO";
            this.SIZE_NO.Name = "SIZE_NO";
            this.SIZE_NO.ReadOnly = true;
            // 
            // PART_NO
            // 
            this.PART_NO.DataPropertyName = "PART_NO";
            this.PART_NO.HeaderText = "PART NO";
            this.PART_NO.Name = "PART_NO";
            this.PART_NO.ReadOnly = true;
            // 
            // PART_NAME
            // 
            this.PART_NAME.DataPropertyName = "PART_NAME";
            this.PART_NAME.HeaderText = "PART NAME";
            this.PART_NAME.Name = "PART_NAME";
            this.PART_NAME.ReadOnly = true;
            // 
            // DELIVERY_DATE
            // 
            this.DELIVERY_DATE.DataPropertyName = "DELIVERY_DATE";
            this.DELIVERY_DATE.HeaderText = "DELIVERY DATE";
            this.DELIVERY_DATE.Name = "DELIVERY_DATE";
            this.DELIVERY_DATE.ReadOnly = true;
            this.DELIVERY_DATE.Width = 150;
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            this.QUANTITY.HeaderText = "QUANTITY";
            this.QUANTITY.Name = "QUANTITY";
            this.QUANTITY.ReadOnly = true;
            // 
            // Register
            // 
            this.Register.BackColor = System.Drawing.Color.SteelBlue;
            this.Register.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Register.Location = new System.Drawing.Point(1028, 29);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(75, 28);
            this.Register.TabIndex = 9;
            this.Register.Text = "Register";
            this.Register.UseVisualStyleBackColor = false;
            this.Register.Click += new System.EventHandler(this.Register_Click);
            // 
            // Cutting_LabelPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 644);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Cutting_LabelPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox txt_PartNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_MasterPO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button selectallbutton;
        private System.Windows.Forms.Button deselectbutton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SALES_ORDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIZE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DELIVERY_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.Button Register;
    }
}

