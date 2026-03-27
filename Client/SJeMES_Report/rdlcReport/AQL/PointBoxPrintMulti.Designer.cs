
namespace SJeMES_Report.AQL
{
    partial class PointBoxPrintMulti
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
            this.flp_rv_list = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // flp_rv_list
            // 
            this.flp_rv_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flp_rv_list.AutoScroll = true;
            this.flp_rv_list.BackColor = System.Drawing.Color.White;
            this.flp_rv_list.Location = new System.Drawing.Point(0, 0);
            this.flp_rv_list.Name = "flp_rv_list";
            this.flp_rv_list.Size = new System.Drawing.Size(1250, 771);
            this.flp_rv_list.TabIndex = 3;
            // 
            // btn_close
            // 
            this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_close.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_close.Location = new System.Drawing.Point(808, 781);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(135, 35);
            this.btn_close.TabIndex = 6;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_download
            // 
            this.btn_download.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_download.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_download.Location = new System.Drawing.Point(620, 781);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(135, 35);
            this.btn_download.TabIndex = 5;
            this.btn_download.Text = "下载";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // btn_print
            // 
            this.btn_print.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_print.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_print.Location = new System.Drawing.Point(430, 781);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(135, 35);
            this.btn_print.TabIndex = 7;
            this.btn_print.Text = "打印";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(257, 788);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(135, 20);
            this.comboBox1.TabIndex = 8;
            // 
            // PointBoxPrintMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 828);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.flp_rv_list);
            this.Name = "PointBoxPrintMulti";
            this.Text = "PointBoxPrint";
            this.Load += new System.EventHandler(this.PointBoxPrintMulti_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flp_rv_list;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}