
namespace SJeMES_QA.FileSForm
{
    partial class FrmQaImgSetting
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
            this.pl_imgs = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pl_imgs
            // 
            this.pl_imgs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_imgs.AutoScroll = true;
            this.pl_imgs.Location = new System.Drawing.Point(0, 64);
            this.pl_imgs.Name = "pl_imgs";
            this.pl_imgs.Size = new System.Drawing.Size(818, 635);
            this.pl_imgs.TabIndex = 0;
            // 
            // FrmQaImgSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 700);
            this.Controls.Add(this.pl_imgs);
            this.Name = "FrmQaImgSetting";
            this.Text = "主图设置";
            this.Load += new System.EventHandler(this.FrmQaImgSetting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_imgs;
    }
}