
namespace SJeMES_BDM
{
    partial class F_BDM_QualityStandard_Item_List
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
            this.txt_code = new System.Windows.Forms.TextBox();
            this.txt_type_code = new System.Windows.Forms.TextBox();
            this.lab_jcxbh = new System.Windows.Forms.Label();
            this.lab_jcxlx = new System.Windows.Forms.Label();
            this.btn_select = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.btn_return = new System.Windows.Forms.Button();
            this.dgvDetection = new System.Windows.Forms.DataGridView();
            this.ckbNo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lab_jcxmc = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetection)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_code
            // 
            this.txt_code.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txt_code.Location = new System.Drawing.Point(431, 76);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(110, 26);
            this.txt_code.TabIndex = 9;
            // 
            // txt_type_code
            // 
            this.txt_type_code.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txt_type_code.Location = new System.Drawing.Point(145, 76);
            this.txt_type_code.Name = "txt_type_code";
            this.txt_type_code.Size = new System.Drawing.Size(110, 26);
            this.txt_type_code.TabIndex = 10;
            // 
            // lab_jcxbh
            // 
            this.lab_jcxbh.AutoSize = true;
            this.lab_jcxbh.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lab_jcxbh.Location = new System.Drawing.Point(315, 81);
            this.lab_jcxbh.Name = "lab_jcxbh";
            this.lab_jcxbh.Size = new System.Drawing.Size(110, 16);
            this.lab_jcxbh.TabIndex = 7;
            this.lab_jcxbh.Text = "检测项编号：";
            // 
            // lab_jcxlx
            // 
            this.lab_jcxlx.AutoSize = true;
            this.lab_jcxlx.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lab_jcxlx.Location = new System.Drawing.Point(29, 81);
            this.lab_jcxlx.Name = "lab_jcxlx";
            this.lab_jcxlx.Size = new System.Drawing.Size(110, 16);
            this.lab_jcxlx.TabIndex = 8;
            this.lab_jcxlx.Text = "检测项类型：";
            // 
            // btn_select
            // 
            this.btn_select.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_select.Location = new System.Drawing.Point(837, 77);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(56, 25);
            this.btn_select.TabIndex = 3;
            this.btn_select.Text = "搜索";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_confirm.Location = new System.Drawing.Point(77, 26);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(56, 25);
            this.btn_confirm.TabIndex = 4;
            this.btn_confirm.Text = "确认";
            this.btn_confirm.UseVisualStyleBackColor = true;
            // 
            // btn_return
            // 
            this.btn_return.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_return.Location = new System.Drawing.Point(15, 26);
            this.btn_return.Name = "btn_return";
            this.btn_return.Size = new System.Drawing.Size(56, 25);
            this.btn_return.TabIndex = 5;
            this.btn_return.Text = "返回";
            this.btn_return.UseVisualStyleBackColor = true;
            this.btn_return.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvDetection
            // 
            this.dgvDetection.AllowUserToAddRows = false;
            this.dgvDetection.AllowUserToDeleteRows = false;
            this.dgvDetection.AllowUserToOrderColumns = true;
            this.dgvDetection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ckbNo});
            this.dgvDetection.Location = new System.Drawing.Point(12, 133);
            this.dgvDetection.Name = "dgvDetection";
            this.dgvDetection.ReadOnly = true;
            this.dgvDetection.RowTemplate.Height = 23;
            this.dgvDetection.Size = new System.Drawing.Size(881, 286);
            this.dgvDetection.TabIndex = 6;
            // 
            // ckbNo
            // 
            this.ckbNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ckbNo.HeaderText = "操作";
            this.ckbNo.Name = "ckbNo";
            this.ckbNo.ReadOnly = true;
            this.ckbNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // lab_jcxmc
            // 
            this.lab_jcxmc.AutoSize = true;
            this.lab_jcxmc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lab_jcxmc.Location = new System.Drawing.Point(583, 81);
            this.lab_jcxmc.Name = "lab_jcxmc";
            this.lab_jcxmc.Size = new System.Drawing.Size(110, 16);
            this.lab_jcxmc.TabIndex = 7;
            this.lab_jcxmc.Text = "检测项名称：";
            // 
            // txt_name
            // 
            this.txt_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txt_name.Location = new System.Drawing.Point(699, 76);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(110, 26);
            this.txt_name.TabIndex = 9;
            // 
            // F_BDM_QualityStandard_Item_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 464);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.txt_code);
            this.Controls.Add(this.txt_type_code);
            this.Controls.Add(this.lab_jcxmc);
            this.Controls.Add(this.lab_jcxbh);
            this.Controls.Add(this.lab_jcxlx);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.btn_return);
            this.Controls.Add(this.dgvDetection);
            this.Name = "F_BDM_QualityStandard_Item_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_BDM_QualityStandard_Item_List";
            this.Load += new System.EventHandler(this.F_BDM_QualityStandard_Item_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.TextBox txt_type_code;
        private System.Windows.Forms.Label lab_jcxbh;
        private System.Windows.Forms.Label lab_jcxlx;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Button btn_return;
        private System.Windows.Forms.DataGridView dgvDetection;
        private System.Windows.Forms.Label lab_jcxmc;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ckbNo;
    }
}