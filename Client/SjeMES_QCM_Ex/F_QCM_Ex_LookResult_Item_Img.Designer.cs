
namespace SjeMES_QCM_Ex
{
    partial class F_QCM_Ex_LookResult_Item_Img
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
            this.pl_img = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pl_img
            // 
            this.pl_img.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_img.AutoScroll = true;
            this.pl_img.Location = new System.Drawing.Point(1, 63);
            this.pl_img.Name = "pl_img";
            this.pl_img.Size = new System.Drawing.Size(815, 386);
            this.pl_img.TabIndex = 0;
            // 
            // F_QCM_Ex_LookResult_Item_Img
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 450);
            this.Controls.Add(this.pl_img);
            this.Name = "F_QCM_Ex_LookResult_Item_Img";
            this.Text = "查看样本";
            this.Load += new System.EventHandler(this.F_QCM_Ex_LookResult_Item_Img_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pl_img;
    }
}