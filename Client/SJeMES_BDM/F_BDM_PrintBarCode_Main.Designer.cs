
namespace SJeMES_BDM
{
    partial class F_BDM_PrintBarCode_Main
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbo_BarcodeTypeSelection = new System.Windows.Forms.ComboBox();
            this.lab_BarcodeTypeSelection = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btn_empty = new System.Windows.Forms.Button();
            this.lab_Rejectedmaterial_list = new System.Windows.Forms.Label();
            this.btn_print = new System.Windows.Forms.Button();
            this.txt_BarCode = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(-1, 63);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbo_BarcodeTypeSelection);
            this.splitContainer1.Panel1.Controls.Add(this.lab_BarcodeTypeSelection);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(905, 467);
            this.splitContainer1.SplitterDistance = 67;
            this.splitContainer1.TabIndex = 0;
            // 
            // cbo_BarcodeTypeSelection
            // 
            this.cbo_BarcodeTypeSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_BarcodeTypeSelection.FormattingEnabled = true;
            this.cbo_BarcodeTypeSelection.Location = new System.Drawing.Point(160, 25);
            this.cbo_BarcodeTypeSelection.Name = "cbo_BarcodeTypeSelection";
            this.cbo_BarcodeTypeSelection.Size = new System.Drawing.Size(121, 20);
            this.cbo_BarcodeTypeSelection.TabIndex = 1;
            // 
            // lab_BarcodeTypeSelection
            // 
            this.lab_BarcodeTypeSelection.AutoSize = true;
            this.lab_BarcodeTypeSelection.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_BarcodeTypeSelection.Location = new System.Drawing.Point(39, 26);
            this.lab_BarcodeTypeSelection.Name = "lab_BarcodeTypeSelection";
            this.lab_BarcodeTypeSelection.Size = new System.Drawing.Size(104, 16);
            this.lab_BarcodeTypeSelection.TabIndex = 0;
            this.lab_BarcodeTypeSelection.Text = "条码类型选择";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btn_empty);
            this.splitContainer2.Panel1.Controls.Add(this.btn_print);
            this.splitContainer2.Panel1.Controls.Add(this.txt_BarCode);
            this.splitContainer2.Panel1.Controls.Add(this.lab_Rejectedmaterial_list);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(905, 396);
            this.splitContainer2.SplitterDistance = 71;
            this.splitContainer2.TabIndex = 3;
            // 
            // btn_empty
            // 
            this.btn_empty.Location = new System.Drawing.Point(403, 24);
            this.btn_empty.Name = "btn_empty";
            this.btn_empty.Size = new System.Drawing.Size(79, 28);
            this.btn_empty.TabIndex = 3;
            this.btn_empty.Text = "清空界面";
            this.btn_empty.UseVisualStyleBackColor = true;
            this.btn_empty.Click += new System.EventHandler(this.button1_Click);
            // 
            // lab_Rejectedmaterial_list
            // 
            this.lab_Rejectedmaterial_list.AutoSize = true;
            this.lab_Rejectedmaterial_list.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_Rejectedmaterial_list.Location = new System.Drawing.Point(32, 28);
            this.lab_Rejectedmaterial_list.Name = "lab_Rejectedmaterial_list";
            this.lab_Rejectedmaterial_list.Size = new System.Drawing.Size(120, 16);
            this.lab_Rejectedmaterial_list.TabIndex = 0;
            this.lab_Rejectedmaterial_list.Text = "请选择材料清单";
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(318, 24);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(79, 28);
            this.btn_print.TabIndex = 2;
            this.btn_print.Text = "打印";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.printbtn_Click);
            // 
            // txt_BarCode
            // 
            this.txt_BarCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_BarCode.Location = new System.Drawing.Point(169, 28);
            this.txt_BarCode.Name = "txt_BarCode";
            this.txt_BarCode.Size = new System.Drawing.Size(121, 21);
            this.txt_BarCode.TabIndex = 1;
            this.txt_BarCode.Click += new System.EventHandler(this.txt_BarCode_Click);
            this.txt_BarCode.TextChanged += new System.EventHandler(this.txt_BarCode_TextChanged);
            this.txt_BarCode.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.txt_BarCode_ChangeUICues);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(905, 321);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // F_BDM_PrintBarCode_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 528);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_BDM_PrintBarCode_Main";
            this.Text = "打印条码";
            this.Load += new System.EventHandler(this.F_BDM_PrintBarCode_Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cbo_BarcodeTypeSelection;
        private System.Windows.Forms.Label lab_BarcodeTypeSelection;
        private System.Windows.Forms.TextBox txt_BarCode;
        private System.Windows.Forms.Label lab_Rejectedmaterial_list;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_empty;
    }
}