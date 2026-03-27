namespace SJeMES_ZL_KanBan
{
    partial class FrmAppearance
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
            this.实验室测试 = new System.Windows.Forms.TabPage();
            this.pal_test = new System.Windows.Forms.Panel();
            this.外观检验 = new System.Windows.Forms.TabPage();
            this.pal_wgTest = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实验室测试.SuspendLayout();
            this.外观检验.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 实验室测试
            // 
            this.实验室测试.Controls.Add(this.pal_test);
            this.实验室测试.Location = new System.Drawing.Point(4, 36);
            this.实验室测试.Name = "实验室测试";
            this.实验室测试.Padding = new System.Windows.Forms.Padding(3);
            this.实验室测试.Size = new System.Drawing.Size(1276, 815);
            this.实验室测试.TabIndex = 2;
            this.实验室测试.Text = "实验室测试";
            this.实验室测试.UseVisualStyleBackColor = true;
            // 
            // pal_test
            // 
            this.pal_test.BackColor = System.Drawing.Color.Transparent;
            this.pal_test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_test.Location = new System.Drawing.Point(3, 3);
            this.pal_test.Name = "pal_test";
            this.pal_test.Size = new System.Drawing.Size(1270, 809);
            this.pal_test.TabIndex = 0;
            // 
            // 外观检验
            // 
            this.外观检验.Controls.Add(this.pal_wgTest);
            this.外观检验.Location = new System.Drawing.Point(4, 36);
            this.外观检验.Name = "外观检验";
            this.外观检验.Padding = new System.Windows.Forms.Padding(3);
            this.外观检验.Size = new System.Drawing.Size(1276, 815);
            this.外观检验.TabIndex = 1;
            this.外观检验.Text = "外观检验";
            this.外观检验.UseVisualStyleBackColor = true;
            // 
            // pal_wgTest
            // 
            this.pal_wgTest.BackColor = System.Drawing.Color.Transparent;
            this.pal_wgTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_wgTest.Location = new System.Drawing.Point(3, 3);
            this.pal_wgTest.Name = "pal_wgTest";
            this.pal_wgTest.Size = new System.Drawing.Size(1270, 809);
            this.pal_wgTest.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.外观检验);
            this.tabControl1.Controls.Add(this.实验室测试);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 855);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "厂商";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 185;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "鞋型";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 184;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "ART";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 185;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "部件名称/材料名称";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 185;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "工序名称";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 185;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "订单数量";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 184;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "已收数量";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 185;
            // 
            // FrmAppearance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 855);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmAppearance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "前段Q品质看板";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmAppearance_Load);
            this.实验室测试.ResumeLayout(false);
            this.外观检验.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.TabPage 实验室测试;
        private System.Windows.Forms.TabPage 外观检验;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel pal_test;
        private System.Windows.Forms.Panel pal_wgTest;
    }
}