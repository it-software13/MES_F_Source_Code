
namespace SJeMES_QCM
{
    partial class F_QCM_ChemicalPrint
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
            this.btn_clearAll = new System.Windows.Forms.Button();
            this.lab_select_clqd = new System.Windows.Forms.Label();
            this.printbtn = new System.Windows.Forms.Button();
            this.txt_BarCode = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_clearAll
            // 
            this.btn_clearAll.Location = new System.Drawing.Point(419, 32);
            this.btn_clearAll.Name = "btn_clearAll";
            this.btn_clearAll.Size = new System.Drawing.Size(79, 28);
            this.btn_clearAll.TabIndex = 7;
            this.btn_clearAll.Text = "清空界面";
            this.btn_clearAll.UseVisualStyleBackColor = true;
            this.btn_clearAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // lab_select_clqd
            // 
            this.lab_select_clqd.AutoSize = true;
            this.lab_select_clqd.BackColor = System.Drawing.Color.White;
            this.lab_select_clqd.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_select_clqd.Location = new System.Drawing.Point(43, 39);
            this.lab_select_clqd.Name = "lab_select_clqd";
            this.lab_select_clqd.Size = new System.Drawing.Size(136, 16);
            this.lab_select_clqd.TabIndex = 4;
            this.lab_select_clqd.Text = "请选择材料清单：";
            // 
            // printbtn
            // 
            this.printbtn.Location = new System.Drawing.Point(334, 32);
            this.printbtn.Name = "printbtn";
            this.printbtn.Size = new System.Drawing.Size(79, 28);
            this.printbtn.TabIndex = 6;
            this.printbtn.Text = "打印";
            this.printbtn.UseVisualStyleBackColor = true;
            this.printbtn.Click += new System.EventHandler(this.printbtn_Click);
            // 
            // txt_BarCode
            // 
            this.txt_BarCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_BarCode.Location = new System.Drawing.Point(185, 35);
            this.txt_BarCode.Name = "txt_BarCode";
            this.txt_BarCode.ReadOnly = true;
            this.txt_BarCode.Size = new System.Drawing.Size(121, 21);
            this.txt_BarCode.TabIndex = 5;
            this.txt_BarCode.Click += new System.EventHandler(this.txt_BarCode_Click);
            this.txt_BarCode.TextChanged += new System.EventHandler(this.txt_BarCode_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(-2, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_clearAll);
            this.splitContainer1.Panel1.Controls.Add(this.txt_BarCode);
            this.splitContainer1.Panel1.Controls.Add(this.printbtn);
            this.splitContainer1.Panel1.Controls.Add(this.lab_select_clqd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AllowDrop = true;
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(804, 385);
            this.splitContainer1.SplitterDistance = 86;
            this.splitContainer1.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(804, 295);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // F_QCM_ChemicalPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_ChemicalPrint";
            this.Text = "化学品打印条码功能";
            this.Load += new System.EventHandler(this.F_QCM_ChemicalPrint_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_clearAll;
        private System.Windows.Forms.Label lab_select_clqd;
        private System.Windows.Forms.Button printbtn;
        private System.Windows.Forms.TextBox txt_BarCode;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}