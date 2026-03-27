
namespace SJeMES_ZL_KanBan
{
    partial class FrmReturn
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.中国市场退货分析 = new System.Windows.Forms.TabPage();
            this.pal_list = new System.Windows.Forms.Panel();
            this.投诉分析 = new System.Windows.Forms.TabPage();
            this.pal_wgTest = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.中国市场退货分析.SuspendLayout();
            this.投诉分析.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.中国市场退货分析);
            this.tabControl1.Controls.Add(this.投诉分析);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 855);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // 中国市场退货分析
            // 
            this.中国市场退货分析.Controls.Add(this.pal_list);
            this.中国市场退货分析.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.中国市场退货分析.Location = new System.Drawing.Point(4, 36);
            this.中国市场退货分析.Name = "中国市场退货分析";
            this.中国市场退货分析.Padding = new System.Windows.Forms.Padding(3);
            this.中国市场退货分析.Size = new System.Drawing.Size(1276, 815);
            this.中国市场退货分析.TabIndex = 0;
            this.中国市场退货分析.Text = "中国市场退货分析";
            this.中国市场退货分析.UseVisualStyleBackColor = true;
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
            // 投诉分析
            // 
            this.投诉分析.Controls.Add(this.pal_wgTest);
            this.投诉分析.Location = new System.Drawing.Point(4, 36);
            this.投诉分析.Name = "投诉分析";
            this.投诉分析.Padding = new System.Windows.Forms.Padding(3);
            this.投诉分析.Size = new System.Drawing.Size(1276, 815);
            this.投诉分析.TabIndex = 1;
            this.投诉分析.Text = "投诉分析";
            this.投诉分析.UseVisualStyleBackColor = true;
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
            // FrmReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 855);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmReturn";
            this.Text = "市场反馈看板";
            this.Load += new System.EventHandler(this.FrmReturn_Load);
            this.tabControl1.ResumeLayout(false);
            this.中国市场退货分析.ResumeLayout(false);
            this.投诉分析.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 中国市场退货分析;
        private System.Windows.Forms.Panel pal_list;
        private System.Windows.Forms.TabPage 投诉分析;
        private System.Windows.Forms.Panel pal_wgTest;
    }
}