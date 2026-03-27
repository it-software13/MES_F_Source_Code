namespace SJeMES_Control_Library.Controls
{
    partial class UCModuleBase
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ucPanelQuote2 = new SJeMES_Control_Library.Controls.UCPanelQuote();
            this.ucCombox1 = new SJeMES_Control_Library.Controls.UCCombox();
            this.btn_Back = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Add = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.btn_Del = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.btn_Edit = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.btn_DoSure = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.btn_Aduit = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.btn_Save = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_Head = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tab_Body = new System.Windows.Forms.TabControl();
            this.ucPanelQuote2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucPanelQuote2
            // 
            this.ucPanelQuote2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPanelQuote2.BackColor = System.Drawing.Color.White;
            this.ucPanelQuote2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.ucPanelQuote2.Controls.Add(this.ucCombox1);
            this.ucPanelQuote2.Controls.Add(this.btn_Back);
            this.ucPanelQuote2.Controls.Add(this.flowLayoutPanel1);
            this.ucPanelQuote2.Controls.Add(this.label2);
            this.ucPanelQuote2.LeftColor = System.Drawing.Color.DarkSlateGray;
            this.ucPanelQuote2.Location = new System.Drawing.Point(19, 13);
            this.ucPanelQuote2.Name = "ucPanelQuote2";
            this.ucPanelQuote2.Padding = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.ucPanelQuote2.Size = new System.Drawing.Size(952, 45);
            this.ucPanelQuote2.TabIndex = 4;
            // 
            // ucCombox1
            // 
            this.ucCombox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucCombox1.BackColor = System.Drawing.Color.Transparent;
            this.ucCombox1.BackColorExt = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ucCombox1.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ucCombox1.ConerRadius = 5;
            this.ucCombox1.DropPanelHeight = -1;
            this.ucCombox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ucCombox1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.ucCombox1.IsRadius = true;
            this.ucCombox1.IsShowRect = true;
            this.ucCombox1.ItemWidth = 70;
            this.ucCombox1.Location = new System.Drawing.Point(768, 6);
            this.ucCombox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCombox1.Name = "ucCombox1";
            this.ucCombox1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ucCombox1.RectWidth = 1;
            this.ucCombox1.SelectedIndex = -1;
            this.ucCombox1.SelectedValue = "";
            this.ucCombox1.Size = new System.Drawing.Size(173, 32);
            this.ucCombox1.Source = null;
            this.ucCombox1.TabIndex = 7;
            this.ucCombox1.TextValue = null;
            this.ucCombox1.TriangleColor = System.Drawing.Color.DarkSlateGray;
            this.ucCombox1.Visible = false;
            this.ucCombox1.SelectedChangedEvent += new System.EventHandler(this.ucCombox1_SelectedChangedEvent);
            // 
            // btn_Back
            // 
            this.btn_Back.BackColor = System.Drawing.Color.White;
            this.btn_Back.BtnBackColor = System.Drawing.Color.White;
            this.btn_Back.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_Back.BtnForeColor = System.Drawing.Color.Gray;
            this.btn_Back.BtnText = "";
            this.btn_Back.ConerRadius = 5;
            this.btn_Back.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.btn_Back.Location = new System.Drawing.Point(75, 6);
            this.btn_Back.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.RectColor = System.Drawing.Color.Gray;
            this.btn_Back.RectWidth = 1;
            this.btn_Back.Size = new System.Drawing.Size(60, 31);
            this.btn_Back.TabIndex = 6;
            this.btn_Back.TabStop = false;
            this.btn_Back.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Back.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Back.TipsText = "";
            this.btn_Back.BtnClick += new System.EventHandler(this.btn_Back_BtnClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_Add);
            this.flowLayoutPanel1.Controls.Add(this.btn_Del);
            this.flowLayoutPanel1.Controls.Add(this.btn_Edit);
            this.flowLayoutPanel1.Controls.Add(this.btn_DoSure);
            this.flowLayoutPanel1.Controls.Add(this.btn_Aduit);
            this.flowLayoutPanel1.Controls.Add(this.btn_Save);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(154, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(586, 30);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.White;
            this.btn_Add.BtnBackColor = System.Drawing.Color.White;
            this.btn_Add.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_Add.BtnForeColor = System.Drawing.Color.Gray;
            this.btn_Add.BtnText = "";
            this.btn_Add.ConerRadius = 5;
            this.btn_Add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Add.EnabledMouseEffect = true;
            this.btn_Add.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_Add.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Add.Image = global::SJeMES_Control_Library.Properties.Resources.icon_add_24_g;
            this.btn_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Add.ImageFontIcons = null;
            this.btn_Add.IsRadius = true;
            this.btn_Add.IsShowRect = true;
            this.btn_Add.IsShowTips = false;
            this.btn_Add.Location = new System.Drawing.Point(0, 0);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.RectColor = System.Drawing.Color.Green;
            this.btn_Add.RectWidth = 1;
            this.btn_Add.Size = new System.Drawing.Size(60, 30);
            this.btn_Add.TabIndex = 1;
            this.btn_Add.TabStop = false;
            this.btn_Add.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Add.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Add.TipsText = "";
            this.btn_Add.BtnClick += new System.EventHandler(this.btn_Add_BtnClick);
            // 
            // btn_Del
            // 
            this.btn_Del.BackColor = System.Drawing.Color.White;
            this.btn_Del.BtnBackColor = System.Drawing.Color.White;
            this.btn_Del.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_Del.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Del.BtnText = "";
            this.btn_Del.ConerRadius = 5;
            this.btn_Del.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Del.EnabledMouseEffect = true;
            this.btn_Del.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_Del.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Del.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Del.Image = global::SJeMES_Control_Library.Properties.Resources.icon_delete_24_r;
            this.btn_Del.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Del.ImageFontIcons = null;
            this.btn_Del.IsRadius = true;
            this.btn_Del.IsShowRect = true;
            this.btn_Del.IsShowTips = false;
            this.btn_Del.Location = new System.Drawing.Point(80, 0);
            this.btn_Del.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Del.RectWidth = 1;
            this.btn_Del.Size = new System.Drawing.Size(60, 30);
            this.btn_Del.TabIndex = 2;
            this.btn_Del.TabStop = false;
            this.btn_Del.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Del.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Del.TipsText = "";
            this.btn_Del.BtnClick += new System.EventHandler(this.btn_Del_BtnClick);
            // 
            // btn_Edit
            // 
            this.btn_Edit.BackColor = System.Drawing.Color.White;
            this.btn_Edit.BtnBackColor = System.Drawing.Color.White;
            this.btn_Edit.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_Edit.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Edit.BtnText = "";
            this.btn_Edit.ConerRadius = 5;
            this.btn_Edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Edit.EnabledMouseEffect = true;
            this.btn_Edit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Edit.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Edit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Edit.Image = global::SJeMES_Control_Library.Properties.Resources.icon_edit_24_b;
            this.btn_Edit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Edit.ImageFontIcons = null;
            this.btn_Edit.IsRadius = true;
            this.btn_Edit.IsShowRect = true;
            this.btn_Edit.IsShowTips = false;
            this.btn_Edit.Location = new System.Drawing.Point(160, 0);
            this.btn_Edit.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_Edit.RectWidth = 1;
            this.btn_Edit.Size = new System.Drawing.Size(60, 30);
            this.btn_Edit.TabIndex = 3;
            this.btn_Edit.TabStop = false;
            this.btn_Edit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Edit.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Edit.TipsText = "";
            this.btn_Edit.BtnClick += new System.EventHandler(this.btn_Edit_BtnClick);
            // 
            // btn_DoSure
            // 
            this.btn_DoSure.BackColor = System.Drawing.Color.White;
            this.btn_DoSure.BtnBackColor = System.Drawing.Color.White;
            this.btn_DoSure.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_DoSure.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_DoSure.BtnText = "";
            this.btn_DoSure.ConerRadius = 5;
            this.btn_DoSure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DoSure.EnabledMouseEffect = true;
            this.btn_DoSure.FillColor = System.Drawing.Color.Gray;
            this.btn_DoSure.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_DoSure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_DoSure.Image = global::SJeMES_Control_Library.Properties.Resources.icon_dosure_24;
            this.btn_DoSure.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_DoSure.ImageFontIcons = null;
            this.btn_DoSure.IsRadius = true;
            this.btn_DoSure.IsShowRect = true;
            this.btn_DoSure.IsShowTips = false;
            this.btn_DoSure.Location = new System.Drawing.Point(240, 0);
            this.btn_DoSure.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_DoSure.Name = "btn_DoSure";
            this.btn_DoSure.RectColor = System.Drawing.Color.Gray;
            this.btn_DoSure.RectWidth = 1;
            this.btn_DoSure.Size = new System.Drawing.Size(60, 30);
            this.btn_DoSure.TabIndex = 4;
            this.btn_DoSure.TabStop = false;
            this.btn_DoSure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_DoSure.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_DoSure.TipsText = "";
            this.btn_DoSure.BtnClick += new System.EventHandler(this.btn_DoSure_BtnClick);
            // 
            // btn_Aduit
            // 
            this.btn_Aduit.BackColor = System.Drawing.Color.White;
            this.btn_Aduit.BtnBackColor = System.Drawing.Color.White;
            this.btn_Aduit.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_Aduit.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Aduit.BtnText = "";
            this.btn_Aduit.ConerRadius = 5;
            this.btn_Aduit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Aduit.EnabledMouseEffect = true;
            this.btn_Aduit.FillColor = System.Drawing.Color.Gray;
            this.btn_Aduit.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Aduit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Aduit.Image = global::SJeMES_Control_Library.Properties.Resources.icon_aduit_24;
            this.btn_Aduit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Aduit.ImageFontIcons = null;
            this.btn_Aduit.IsRadius = true;
            this.btn_Aduit.IsShowRect = true;
            this.btn_Aduit.IsShowTips = false;
            this.btn_Aduit.Location = new System.Drawing.Point(320, 0);
            this.btn_Aduit.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_Aduit.Name = "btn_Aduit";
            this.btn_Aduit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Aduit.RectWidth = 1;
            this.btn_Aduit.Size = new System.Drawing.Size(60, 30);
            this.btn_Aduit.TabIndex = 5;
            this.btn_Aduit.TabStop = false;
            this.btn_Aduit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Aduit.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Aduit.TipsText = "";
            this.btn_Aduit.BtnClick += new System.EventHandler(this.btn_Aduit_BtnClick);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.White;
            this.btn_Save.BtnBackColor = System.Drawing.Color.White;
            this.btn_Save.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.btn_Save.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btn_Save.BtnText = "";
            this.btn_Save.ConerRadius = 5;
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
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
            this.btn_Save.Location = new System.Drawing.Point(400, 0);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Save.RectWidth = 1;
            this.btn_Save.Size = new System.Drawing.Size(60, 30);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.TabStop = false;
            this.btn_Save.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_Save.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.btn_Save.TipsText = "";
            this.btn_Save.BtnClick += new System.EventHandler(this.btn_Save_BtnClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.Location = new System.Drawing.Point(16, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "操      作";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_Head);
            this.panel1.Controls.Add(this.ucPanelQuote2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 291);
            this.panel1.TabIndex = 5;
            // 
            // panel_Head
            // 
            this.panel_Head.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Head.AutoScroll = true;
            this.panel_Head.BackColor = System.Drawing.Color.White;
            this.panel_Head.Location = new System.Drawing.Point(19, 64);
            this.panel_Head.Name = "panel_Head";
            this.panel_Head.Size = new System.Drawing.Size(952, 224);
            this.panel_Head.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tab_Body);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 291);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 309);
            this.panel2.TabIndex = 5;
            // 
            // tab_Body
            // 
            this.tab_Body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab_Body.Location = new System.Drawing.Point(19, 3);
            this.tab_Body.Name = "tab_Body";
            this.tab_Body.SelectedIndex = 0;
            this.tab_Body.Size = new System.Drawing.Size(952, 303);
            this.tab_Body.TabIndex = 0;
            // 
            // UCModuleBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UCModuleBase";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UCModuleBase_Load);
            this.SizeChanged += new System.EventHandler(this.UCModuleBase_SizeChanged);
            this.ucPanelQuote2.ResumeLayout(false);
            this.ucPanelQuote2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCPanelQuote ucPanelQuote2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private UCBtnImg btn_Add;
        private UCBtnImg btn_Del;
        private UCBtnImg btn_Edit;
        private UCBtnImg btn_DoSure;
        private UCBtnImg btn_Aduit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private UCBtnImg btn_Back;
        private UCBtnImg btn_Save;
        private System.Windows.Forms.Panel panel_Head;
        private System.Windows.Forms.TabControl tab_Body;
        private UCCombox ucCombox1;
    }
}
