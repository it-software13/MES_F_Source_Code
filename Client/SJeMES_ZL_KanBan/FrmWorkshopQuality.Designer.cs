
namespace SJeMES_ZL_KanBan
{
    partial class FrmWorkshopQuality
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
            this.pal_list = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.RQC抽验 = new System.Windows.Forms.TabPage();
            this.TQC抽验 = new System.Windows.Forms.TabPage();
            this.pal_wgTest = new System.Windows.Forms.Panel();
            this.金属管控 = new System.Windows.Forms.TabPage();
            this.温湿度看板 = new System.Windows.Forms.TabPage();
            this.pal_temp_hum = new System.Windows.Forms.Panel();
            this.设备参数 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jspanel = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.RQC抽验.SuspendLayout();
            this.TQC抽验.SuspendLayout();
            this.金属管控.SuspendLayout();
            this.温湿度看板.SuspendLayout();
            this.设备参数.SuspendLayout();
            this.SuspendLayout();
            // 
            // pal_list
            // 
            this.pal_list.BackColor = System.Drawing.Color.Transparent;
            this.pal_list.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pal_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_list.Location = new System.Drawing.Point(3, 3);
            this.pal_list.Name = "pal_list";
            this.pal_list.Size = new System.Drawing.Size(1240, 724);
            this.pal_list.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.RQC抽验);
            this.tabControl1.Controls.Add(this.TQC抽验);
            this.tabControl1.Controls.Add(this.金属管控);
            this.tabControl1.Controls.Add(this.温湿度看板);
            this.tabControl1.Controls.Add(this.设备参数);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1254, 770);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // RQC抽验
            // 
            this.RQC抽验.Controls.Add(this.pal_list);
            this.RQC抽验.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RQC抽验.Location = new System.Drawing.Point(4, 36);
            this.RQC抽验.Name = "RQC抽验";
            this.RQC抽验.Padding = new System.Windows.Forms.Padding(3);
            this.RQC抽验.Size = new System.Drawing.Size(1246, 730);
            this.RQC抽验.TabIndex = 0;
            this.RQC抽验.Text = "RQC抽验";
            this.RQC抽验.UseVisualStyleBackColor = true;
            // 
            // TQC抽验
            // 
            this.TQC抽验.Controls.Add(this.pal_wgTest);
            this.TQC抽验.Location = new System.Drawing.Point(4, 36);
            this.TQC抽验.Name = "TQC抽验";
            this.TQC抽验.Padding = new System.Windows.Forms.Padding(3);
            this.TQC抽验.Size = new System.Drawing.Size(1246, 730);
            this.TQC抽验.TabIndex = 1;
            this.TQC抽验.Text = "TQC抽验";
            this.TQC抽验.UseVisualStyleBackColor = true;
            // 
            // pal_wgTest
            // 
            this.pal_wgTest.BackColor = System.Drawing.Color.Transparent;
            this.pal_wgTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_wgTest.Location = new System.Drawing.Point(3, 3);
            this.pal_wgTest.Name = "pal_wgTest";
            this.pal_wgTest.Size = new System.Drawing.Size(1240, 724);
            this.pal_wgTest.TabIndex = 0;
            // 
            // 金属管控
            // 
            this.金属管控.Controls.Add(this.jspanel);
            this.金属管控.Location = new System.Drawing.Point(4, 36);
            this.金属管控.Name = "金属管控";
            this.金属管控.Size = new System.Drawing.Size(1246, 730);
            this.金属管控.TabIndex = 3;
            this.金属管控.Text = "金属管控";
            this.金属管控.UseVisualStyleBackColor = true;
            // 
            // 温湿度看板
            // 
            this.温湿度看板.Controls.Add(this.pal_temp_hum);
            this.温湿度看板.Location = new System.Drawing.Point(4, 36);
            this.温湿度看板.Name = "温湿度看板";
            this.温湿度看板.Size = new System.Drawing.Size(1246, 730);
            this.温湿度看板.TabIndex = 2;
            this.温湿度看板.Text = "温湿度看板";
            this.温湿度看板.UseVisualStyleBackColor = true;
            // 
            // pal_temp_hum
            // 
            this.pal_temp_hum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pal_temp_hum.Location = new System.Drawing.Point(0, 0);
            this.pal_temp_hum.Name = "pal_temp_hum";
            this.pal_temp_hum.Size = new System.Drawing.Size(1246, 730);
            this.pal_temp_hum.TabIndex = 0;
            // 
            // 设备参数
            // 
            this.设备参数.Controls.Add(this.panel1);
            this.设备参数.Location = new System.Drawing.Point(4, 36);
            this.设备参数.Name = "设备参数";
            this.设备参数.Size = new System.Drawing.Size(1246, 730);
            this.设备参数.TabIndex = 4;
            this.设备参数.Text = "设备参数";
            this.设备参数.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1246, 730);
            this.panel1.TabIndex = 0;
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
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "订单数量";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 184;
            // 
            // jspanel
            // 
            this.jspanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jspanel.Location = new System.Drawing.Point(0, 0);
            this.jspanel.Name = "jspanel";
            this.jspanel.Size = new System.Drawing.Size(1246, 730);
            this.jspanel.TabIndex = 0;
            // 
            // FrmWorkshopQuality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 770);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmWorkshopQuality";
            this.Text = "车间Q品质看板";
            this.Load += new System.EventHandler(this.FrmWorkshopQuality_Load);
            this.tabControl1.ResumeLayout(false);
            this.RQC抽验.ResumeLayout(false);
            this.TQC抽验.ResumeLayout(false);
            this.金属管控.ResumeLayout(false);
            this.温湿度看板.ResumeLayout(false);
            this.设备参数.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pal_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage RQC抽验;
        private System.Windows.Forms.TabPage TQC抽验;
        private System.Windows.Forms.Panel pal_wgTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.TabPage 温湿度看板;
        private System.Windows.Forms.Panel pal_temp_hum;
        private System.Windows.Forms.TabPage 金属管控;
        private System.Windows.Forms.TabPage 设备参数;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel jspanel;
    }
}