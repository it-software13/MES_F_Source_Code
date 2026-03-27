
namespace SJeMES_ZL_KanBan
{
    partial class FrmTestDepartment
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
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.试穿测试工作量分析 = new System.Windows.Forms.TabPage();
            this.pal_list = new System.Windows.Forms.Panel();
            this.测试异常 = new System.Windows.Forms.TabPage();
            this.pal_wgTest = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.试穿测试工作量分析.SuspendLayout();
            this.测试异常.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "已收数量";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 185;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "工序名称";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 185;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "部件名称/材料名称";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 185;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "ART";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 185;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "鞋型";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 184;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "厂商";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 185;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.试穿测试工作量分析);
            this.tabControl1.Controls.Add(this.测试异常);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 855);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // 试穿测试工作量分析
            // 
            this.试穿测试工作量分析.Controls.Add(this.pal_list);
            this.试穿测试工作量分析.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.试穿测试工作量分析.Location = new System.Drawing.Point(4, 36);
            this.试穿测试工作量分析.Name = "试穿测试工作量分析";
            this.试穿测试工作量分析.Padding = new System.Windows.Forms.Padding(3);
            this.试穿测试工作量分析.Size = new System.Drawing.Size(1276, 815);
            this.试穿测试工作量分析.TabIndex = 0;
            this.试穿测试工作量分析.Text = "测试工作量分析";
            this.试穿测试工作量分析.UseVisualStyleBackColor = true;
            // 
            // pal_list
            // 
            this.pal_list.BackColor = System.Drawing.Color.Transparent;
            this.pal_list.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pal_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_list.Location = new System.Drawing.Point(3, 3);
            this.pal_list.Name = "pal_list";
            this.pal_list.Size = new System.Drawing.Size(1270, 809);
            this.pal_list.TabIndex = 0;
            // 
            // 测试异常
            // 
            this.测试异常.Controls.Add(this.pal_wgTest);
            this.测试异常.Location = new System.Drawing.Point(4, 36);
            this.测试异常.Name = "测试异常";
            this.测试异常.Padding = new System.Windows.Forms.Padding(3);
            this.测试异常.Size = new System.Drawing.Size(1276, 815);
            this.测试异常.TabIndex = 1;
            this.测试异常.Text = "FAIL3次测试异常";
            this.测试异常.UseVisualStyleBackColor = true;
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
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "订单数量";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 184;
            // 
            // FrmTestDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 855);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmTestDepartment";
            this.Text = "测试部品质看板";
            this.Load += new System.EventHandler(this.FrmTestDepartment_Load);
            this.tabControl1.ResumeLayout(false);
            this.试穿测试工作量分析.ResumeLayout(false);
            this.测试异常.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 试穿测试工作量分析;
        private System.Windows.Forms.Panel pal_list;
        private System.Windows.Forms.TabPage 测试异常;
        private System.Windows.Forms.Panel pal_wgTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}