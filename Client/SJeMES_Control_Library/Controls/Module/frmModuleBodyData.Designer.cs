namespace SJeMES_Control_Library.Controls.Module
{
    partial class frmModuleBodyData
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
            this.panel_Controls = new System.Windows.Forms.Panel();
            this.ucPanelQuote2 = new SJeMES_Control_Library.Controls.UCPanelQuote();
            this.btn_Back = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Save = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.label2 = new System.Windows.Forms.Label();
            this.ucPanelQuote2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Controls
            // 
            this.panel_Controls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Controls.BackColor = System.Drawing.Color.White;
            this.panel_Controls.Location = new System.Drawing.Point(15, 134);
            this.panel_Controls.Name = "panel_Controls";
            this.panel_Controls.Size = new System.Drawing.Size(1073, 69);
            this.panel_Controls.TabIndex = 6;
            this.panel_Controls.UseWaitCursor = true;
            // 
            // ucPanelQuote2
            // 
            this.ucPanelQuote2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPanelQuote2.BackColor = System.Drawing.Color.White;
            this.ucPanelQuote2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.ucPanelQuote2.Controls.Add(this.btn_Back);
            this.ucPanelQuote2.Controls.Add(this.flowLayoutPanel1);
            this.ucPanelQuote2.Controls.Add(this.label2);
            this.ucPanelQuote2.LeftColor = System.Drawing.Color.DarkSlateGray;
            this.ucPanelQuote2.Location = new System.Drawing.Point(15, 79);
            this.ucPanelQuote2.Name = "ucPanelQuote2";
            this.ucPanelQuote2.Padding = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.ucPanelQuote2.Size = new System.Drawing.Size(1073, 49);
            this.ucPanelQuote2.TabIndex = 5;
            this.ucPanelQuote2.UseWaitCursor = true;
            // 
            // btn_Back
            // 
            this.btn_Back.BackColor = System.Drawing.Color.White;
            this.btn_Back.BtnBackColor = System.Drawing.Color.White;
            this.btn_Back.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_Back.BtnForeColor = System.Drawing.Color.Gray;
            this.btn_Back.BtnText = "";
            this.btn_Back.ConerRadius = 5;
            this.btn_Back.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.btn_Back.EnabledMouseEffect = true;
            this.btn_Back.FillColor = System.Drawing.Color.White;
            this.btn_Back.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Back.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Back.Image = global::SJeMES_Control_Library.Properties.Resources.icon_back_24;
            this.btn_Back.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Back.ImageFontIcons = null;
            this.btn_Back.IsRadius = true;
            this.btn_Back.IsShowRect = true;
            this.btn_Back.IsShowTips = false;
            this.btn_Back.Location = new System.Drawing.Point(86, 8);
            this.btn_Back.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.RectColor = System.Drawing.Color.Gray;
            this.btn_Back.RectWidth = 1;
            this.btn_Back.Size = new System.Drawing.Size(49, 33);
            this.btn_Back.TabIndex = 6;
            this.btn_Back.TabStop = false;
            this.btn_Back.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Back.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Back.TipsText = "";
            this.btn_Back.UseWaitCursor = true;
            this.btn_Back.BtnClick += new System.EventHandler(this.btn_Back_BtnClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_Save);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(154, 8);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(586, 33);
            this.flowLayoutPanel1.TabIndex = 5;
            this.flowLayoutPanel1.UseWaitCursor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.White;
            this.btn_Save.BtnBackColor = System.Drawing.Color.White;
            this.btn_Save.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_Save.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Save.BtnText = "";
            this.btn_Save.ConerRadius = 5;
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.btn_Save.EnabledMouseEffect = true;
            this.btn_Save.FillColor = System.Drawing.Color.Green;
            this.btn_Save.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Image = global::SJeMES_Control_Library.Properties.Resources.icon_save_24;
            this.btn_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Save.ImageFontIcons = null;
            this.btn_Save.IsRadius = true;
            this.btn_Save.IsShowRect = true;
            this.btn_Save.IsShowTips = false;
            this.btn_Save.Location = new System.Drawing.Point(20, 0);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Save.RectWidth = 1;
            this.btn_Save.Size = new System.Drawing.Size(60, 33);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.TabStop = false;
            this.btn_Save.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Save.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Save.TipsText = "";
            this.btn_Save.UseWaitCursor = true;
            this.btn_Save.BtnClick += new System.EventHandler(this.btn_Save_BtnClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.Location = new System.Drawing.Point(16, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "操      作";
            this.label2.UseWaitCursor = true;
            // 
            // frmModuleBodyData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 217);
            this.Controls.Add(this.panel_Controls);
            this.Controls.Add(this.ucPanelQuote2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1100, 217);
            this.Name = "frmModuleBodyData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Table body data";
            this.UseWaitCursor = true;
            this.ucPanelQuote2.ResumeLayout(false);
            this.ucPanelQuote2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCPanelQuote ucPanelQuote2;
        private UCBtnImg btn_Back;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private UCBtnImg btn_Save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_Controls;
    }
}