namespace SJeMES_ZL_KanBan
{
    partial class FrmOrder
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PO信息列表 = new System.Windows.Forms.TabPage();
            this.pal_po = new System.Windows.Forms.Panel();
            this.A01信息 = new System.Windows.Forms.TabPage();
            this.pal_a01 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.PO信息列表.SuspendLayout();
            this.A01信息.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 651);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PO信息列表);
            this.tabControl1.Controls.Add(this.A01信息);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1144, 651);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // PO信息列表
            // 
            this.PO信息列表.Controls.Add(this.pal_po);
            this.PO信息列表.Location = new System.Drawing.Point(4, 36);
            this.PO信息列表.Name = "PO信息列表";
            this.PO信息列表.Padding = new System.Windows.Forms.Padding(3);
            this.PO信息列表.Size = new System.Drawing.Size(1136, 611);
            this.PO信息列表.TabIndex = 0;
            this.PO信息列表.Text = "PO信息列表";
            this.PO信息列表.UseVisualStyleBackColor = true;
            // 
            // pal_po
            // 
            this.pal_po.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_po.Location = new System.Drawing.Point(3, 3);
            this.pal_po.Name = "pal_po";
            this.pal_po.Size = new System.Drawing.Size(1130, 605);
            this.pal_po.TabIndex = 0;
            // 
            // A01信息
            // 
            this.A01信息.Controls.Add(this.pal_a01);
            this.A01信息.Location = new System.Drawing.Point(4, 36);
            this.A01信息.Name = "A01信息";
            this.A01信息.Size = new System.Drawing.Size(1136, 611);
            this.A01信息.TabIndex = 2;
            this.A01信息.Text = "A-01信息";
            this.A01信息.UseVisualStyleBackColor = true;
            // 
            // pal_a01
            // 
            this.pal_a01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_a01.Location = new System.Drawing.Point(0, 0);
            this.pal_a01.Name = "pal_a01";
            this.pal_a01.Size = new System.Drawing.Size(1136, 611);
            this.pal_a01.TabIndex = 0;
            // 
            // FrmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 651);
            this.Controls.Add(this.panel1);
            this.Name = "FrmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Order";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOrder_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.PO信息列表.ResumeLayout(false);
            this.A01信息.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PO信息列表;
        private System.Windows.Forms.TabPage A01信息;
        private System.Windows.Forms.Panel pal_po;
        private System.Windows.Forms.Panel pal_a01;
    }
}