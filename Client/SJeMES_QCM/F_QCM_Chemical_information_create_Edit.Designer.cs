
namespace SJeMES_QCM
{
    partial class F_QCM_Chemical_information_create_Edit
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
            this.lab_ChemicalNo = new System.Windows.Forms.Label();
            this.lab_chemicalName = new System.Windows.Forms.Label();
            this.lab_EndDate = new System.Windows.Forms.Label();
            this.txtchemicals_no = new System.Windows.Forms.TextBox();
            this.txtchemicals_name = new System.Windows.Forms.TextBox();
            this.datevalidtime = new System.Windows.Forms.DateTimePicker();
            this.btn_Out = new System.Windows.Forms.Button();
            this.btn_affirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lab_ChemicalNo
            // 
            this.lab_ChemicalNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_ChemicalNo.AutoSize = true;
            this.lab_ChemicalNo.BackColor = System.Drawing.Color.White;
            this.lab_ChemicalNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ChemicalNo.Location = new System.Drawing.Point(133, 125);
            this.lab_ChemicalNo.Name = "lab_ChemicalNo";
            this.lab_ChemicalNo.Size = new System.Drawing.Size(90, 21);
            this.lab_ChemicalNo.TabIndex = 0;
            this.lab_ChemicalNo.Text = "化学品代号";
            // 
            // lab_chemicalName
            // 
            this.lab_chemicalName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_chemicalName.AutoSize = true;
            this.lab_chemicalName.BackColor = System.Drawing.Color.White;
            this.lab_chemicalName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_chemicalName.Location = new System.Drawing.Point(133, 183);
            this.lab_chemicalName.Name = "lab_chemicalName";
            this.lab_chemicalName.Size = new System.Drawing.Size(90, 21);
            this.lab_chemicalName.TabIndex = 1;
            this.lab_chemicalName.Text = "化学品名称";
            // 
            // lab_EndDate
            // 
            this.lab_EndDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lab_EndDate.AutoSize = true;
            this.lab_EndDate.BackColor = System.Drawing.Color.White;
            this.lab_EndDate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_EndDate.Location = new System.Drawing.Point(127, 305);
            this.lab_EndDate.Name = "lab_EndDate";
            this.lab_EndDate.Size = new System.Drawing.Size(96, 21);
            this.lab_EndDate.TabIndex = 2;
            this.lab_EndDate.Text = "有效时间(H)";
            // 
            // txtchemicals_no
            // 
            this.txtchemicals_no.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtchemicals_no.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtchemicals_no.Location = new System.Drawing.Point(251, 122);
            this.txtchemicals_no.Name = "txtchemicals_no";
            this.txtchemicals_no.Size = new System.Drawing.Size(200, 29);
            this.txtchemicals_no.TabIndex = 3;
            // 
            // txtchemicals_name
            // 
            this.txtchemicals_name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtchemicals_name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtchemicals_name.Location = new System.Drawing.Point(251, 180);
            this.txtchemicals_name.Name = "txtchemicals_name";
            this.txtchemicals_name.Size = new System.Drawing.Size(200, 29);
            this.txtchemicals_name.TabIndex = 4;
            // 
            // datevalidtime
            // 
            this.datevalidtime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datevalidtime.CustomFormat = "HH";
            this.datevalidtime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.datevalidtime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datevalidtime.Location = new System.Drawing.Point(251, 299);
            this.datevalidtime.Name = "datevalidtime";
            this.datevalidtime.ShowUpDown = true;
            this.datevalidtime.Size = new System.Drawing.Size(200, 29);
            this.datevalidtime.TabIndex = 5;
            // 
            // btn_Out
            // 
            this.btn_Out.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Out.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Out.Location = new System.Drawing.Point(202, 388);
            this.btn_Out.Name = "btn_Out";
            this.btn_Out.Size = new System.Drawing.Size(85, 32);
            this.btn_Out.TabIndex = 6;
            this.btn_Out.Text = "取消";
            this.btn_Out.UseVisualStyleBackColor = true;
            this.btn_Out.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_affirm
            // 
            this.btn_affirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_affirm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_affirm.Location = new System.Drawing.Point(345, 388);
            this.btn_affirm.Name = "btn_affirm";
            this.btn_affirm.Size = new System.Drawing.Size(85, 32);
            this.btn_affirm.TabIndex = 7;
            this.btn_affirm.Text = "确认";
            this.btn_affirm.UseVisualStyleBackColor = true;
            this.btn_affirm.Click += new System.EventHandler(this.button2_Click);
            // 
            // F_QCM_Chemical_information_create_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 496);
            this.Controls.Add(this.btn_affirm);
            this.Controls.Add(this.btn_Out);
            this.Controls.Add(this.datevalidtime);
            this.Controls.Add(this.txtchemicals_name);
            this.Controls.Add(this.txtchemicals_no);
            this.Controls.Add(this.lab_EndDate);
            this.Controls.Add(this.lab_chemicalName);
            this.Controls.Add(this.lab_ChemicalNo);
            this.Name = "F_QCM_Chemical_information_create_Edit";
            this.Text = "新增化学品明细";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_ChemicalNo;
        private System.Windows.Forms.Label lab_chemicalName;
        private System.Windows.Forms.Label lab_EndDate;
        private System.Windows.Forms.TextBox txtchemicals_no;
        private System.Windows.Forms.TextBox txtchemicals_name;
        private System.Windows.Forms.DateTimePicker datevalidtime;
        private System.Windows.Forms.Button btn_Out;
        private System.Windows.Forms.Button btn_affirm;
    }
}