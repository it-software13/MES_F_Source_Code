namespace SJeMES_Control_Library.Controls
{
    partial class UCModuleBaseList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCModuleBaseList));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lab_DataTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ucCombox1 = new SJeMES_Control_Library.Controls.UCCombox();
            this.ucPagerControl21 = new SJeMES_Control_Library.Controls.UCPagerControl2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucSelectTool1 = new SJeMES_Control_Library.Controls.UCSelectTool();
            this.ucPanelQuote2 = new SJeMES_Control_Library.Controls.UCPanelQuote();
            this.label3 = new System.Windows.Forms.Label();
            this.ucSwitch1 = new SJeMES_Control_Library.Controls.UCSwitch();
            this.ucBtnImg5 = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.ucBtnImg3 = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.ucBtnImg4 = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ucPanelQuote2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 111);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(971, 430);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.ucPagerControl21);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 550);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 50);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lab_DataTotal);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.ucCombox1);
            this.panel3.Location = new System.Drawing.Point(679, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(306, 41);
            this.panel3.TabIndex = 1;
            // 
            // lab_DataTotal
            // 
            this.lab_DataTotal.AutoSize = true;
            this.lab_DataTotal.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lab_DataTotal.ForeColor = System.Drawing.Color.Gray;
            this.lab_DataTotal.Location = new System.Drawing.Point(170, 10);
            this.lab_DataTotal.Name = "lab_DataTotal";
            this.lab_DataTotal.Size = new System.Drawing.Size(54, 20);
            this.lab_DataTotal.TabIndex = 6;
            this.lab_DataTotal.Text = "总行数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(9, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "页行数";
            // 
            // ucCombox1
            // 
            this.ucCombox1.BackColor = System.Drawing.Color.Transparent;
            this.ucCombox1.BackColorExt = System.Drawing.Color.White;
            this.ucCombox1.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ucCombox1.ConerRadius = 5;
            this.ucCombox1.DropPanelHeight = -1;
            this.ucCombox1.FillColor = System.Drawing.Color.White;
            this.ucCombox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ucCombox1.IsRadius = true;
            this.ucCombox1.IsShowRect = true;
            this.ucCombox1.ItemWidth = 70;
            this.ucCombox1.Location = new System.Drawing.Point(64, 5);
            this.ucCombox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCombox1.Name = "ucCombox1";
            this.ucCombox1.RectColor = System.Drawing.Color.White;
            this.ucCombox1.RectWidth = 1;
            this.ucCombox1.SelectedIndex = -1;
            this.ucCombox1.SelectedValue = "";
            this.ucCombox1.Size = new System.Drawing.Size(99, 32);
            this.ucCombox1.Source = null;
            this.ucCombox1.TabIndex = 4;
            this.ucCombox1.TextValue = null;
            this.ucCombox1.TriangleColor = System.Drawing.Color.DarkSlateGray;
            this.ucCombox1.SelectedChangedEvent += new System.EventHandler(this.ucCombox1_SelectedChangedEvent);
            // 
            // ucPagerControl21
            // 
            this.ucPagerControl21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPagerControl21.BackColor = System.Drawing.Color.White;
            this.ucPagerControl21.DataSource = ((System.Collections.Generic.List<object>)(resources.GetObject("ucPagerControl21.DataSource")));
            this.ucPagerControl21.Location = new System.Drawing.Point(14, 0);
            this.ucPagerControl21.Name = "ucPagerControl21";
            this.ucPagerControl21.PageCount = 0;
            this.ucPagerControl21.PageIndex = 1;
            this.ucPagerControl21.PageModel = SJeMES_Control_Library.Controls.PageModel.PageCount;
            this.ucPagerControl21.PageSize = 10;
            this.ucPagerControl21.Size = new System.Drawing.Size(668, 41);
            this.ucPagerControl21.StartIndex = 0;
            this.ucPagerControl21.TabIndex = 0;
            this.ucPagerControl21.ShowSourceChanged += new SJeMES_Control_Library.Controls.PageControlEventHandler(this.ucPagerControl21_ShowSourceChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucSelectTool1);
            this.panel1.Controls.Add(this.ucPanelQuote2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 108);
            this.panel1.TabIndex = 0;
            // 
            // ucSelectTool1
            // 
            this.ucSelectTool1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucSelectTool1.ConerRadius = 24;
            this.ucSelectTool1.FillColor = System.Drawing.Color.Transparent;
            this.ucSelectTool1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucSelectTool1.IsRadius = false;
            this.ucSelectTool1.IsSelectMore = false;
            this.ucSelectTool1.IsShowRect = false;
            this.ucSelectTool1.Keys = null;
            this.ucSelectTool1.Location = new System.Drawing.Point(14, 9);
            this.ucSelectTool1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucSelectTool1.Name = "ucSelectTool1";
            this.ucSelectTool1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucSelectTool1.RectWidth = 1;
            this.ucSelectTool1.Size = new System.Drawing.Size(971, 45);
            this.ucSelectTool1.TabIndex = 5;
            this.ucSelectTool1.SelectData += new SJeMES_Control_Library.Controls.UCSelectTool.SelectDataHandle(this.ucSelectTool1_SelectData);
            // 
            // ucPanelQuote2
            // 
            this.ucPanelQuote2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPanelQuote2.BackColor = System.Drawing.Color.White;
            this.ucPanelQuote2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.ucPanelQuote2.Controls.Add(this.label3);
            this.ucPanelQuote2.Controls.Add(this.ucSwitch1);
            this.ucPanelQuote2.Controls.Add(this.ucBtnImg5);
            this.ucPanelQuote2.Controls.Add(this.ucBtnImg3);
            this.ucPanelQuote2.Controls.Add(this.ucBtnImg4);
            this.ucPanelQuote2.Controls.Add(this.label2);
            this.ucPanelQuote2.LeftColor = System.Drawing.Color.DarkSlateGray;
            this.ucPanelQuote2.Location = new System.Drawing.Point(14, 60);
            this.ucPanelQuote2.Name = "ucPanelQuote2";
            this.ucPanelQuote2.Padding = new System.Windows.Forms.Padding(5, 1, 1, 1);
            this.ucPanelQuote2.Size = new System.Drawing.Size(971, 45);
            this.ucPanelQuote2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label3.Location = new System.Drawing.Point(408, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "显示系统字段";
            // 
            // ucSwitch1
            // 
            this.ucSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.ucSwitch1.Checked = false;
            this.ucSwitch1.FalseColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.ucSwitch1.FalseTextColr = System.Drawing.Color.WhiteSmoke;
            this.ucSwitch1.Location = new System.Drawing.Point(362, 12);
            this.ucSwitch1.Name = "ucSwitch1";
            this.ucSwitch1.Size = new System.Drawing.Size(40, 20);
            this.ucSwitch1.SwitchType = SJeMES_Control_Library.Controls.SwitchType.Ellipse;
            this.ucSwitch1.TabIndex = 1;
            this.ucSwitch1.Texts = new string[0];
            this.ucSwitch1.TrueColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucSwitch1.TrueTextColr = System.Drawing.Color.White;
            this.ucSwitch1.CheckedChanged += new System.EventHandler(this.ucSwitch1_CheckedChanged);
            // 
            // ucBtnImg5
            // 
            this.ucBtnImg5.BackColor = System.Drawing.Color.White;
            this.ucBtnImg5.BtnBackColor = System.Drawing.Color.White;
            this.ucBtnImg5.BtnFont = new System.Drawing.Font("微软雅黑", 17F);
            this.ucBtnImg5.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg5.BtnText = "";
            this.ucBtnImg5.ConerRadius = 5;
            this.ucBtnImg5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg5.EnabledMouseEffect = true;
            this.ucBtnImg5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ucBtnImg5.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg5.Image = global::SJeMES_Control_Library.Properties.Resources.icon_edit_24_b;
            this.ucBtnImg5.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg5.ImageFontIcons = null;
            this.ucBtnImg5.IsRadius = true;
            this.ucBtnImg5.IsShowRect = true;
            this.ucBtnImg5.IsShowTips = false;
            this.ucBtnImg5.Location = new System.Drawing.Point(250, 8);
            this.ucBtnImg5.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg5.Name = "ucBtnImg5";
            this.ucBtnImg5.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ucBtnImg5.RectWidth = 1;
            this.ucBtnImg5.Size = new System.Drawing.Size(60, 30);
            this.ucBtnImg5.TabIndex = 3;
            this.ucBtnImg5.TabStop = false;
            this.ucBtnImg5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg5.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg5.TipsText = "";
            this.ucBtnImg5.BtnClick += new System.EventHandler(this.ucBtnImg5_BtnClick);
            // 
            // ucBtnImg3
            // 
            this.ucBtnImg3.BackColor = System.Drawing.Color.White;
            this.ucBtnImg3.BtnBackColor = System.Drawing.Color.White;
            this.ucBtnImg3.BtnFont = new System.Drawing.Font("微软雅黑", 17F);
            this.ucBtnImg3.BtnForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg3.BtnText = "";
            this.ucBtnImg3.ConerRadius = 5;
            this.ucBtnImg3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg3.EnabledMouseEffect = true;
            this.ucBtnImg3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ucBtnImg3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg3.Image = global::SJeMES_Control_Library.Properties.Resources.icon_delete_24_r;
            this.ucBtnImg3.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg3.ImageFontIcons = null;
            this.ucBtnImg3.IsRadius = true;
            this.ucBtnImg3.IsShowRect = true;
            this.ucBtnImg3.IsShowTips = false;
            this.ucBtnImg3.Location = new System.Drawing.Point(167, 8);
            this.ucBtnImg3.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg3.Name = "ucBtnImg3";
            this.ucBtnImg3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ucBtnImg3.RectWidth = 1;
            this.ucBtnImg3.Size = new System.Drawing.Size(60, 30);
            this.ucBtnImg3.TabIndex = 2;
            this.ucBtnImg3.TabStop = false;
            this.ucBtnImg3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg3.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg3.TipsText = "";
            this.ucBtnImg3.BtnClick += new System.EventHandler(this.ucBtnImg3_BtnClick);
            // 
            // ucBtnImg4
            // 
            this.ucBtnImg4.BackColor = System.Drawing.Color.White;
            this.ucBtnImg4.BtnBackColor = System.Drawing.Color.White;
            this.ucBtnImg4.BtnFont = new System.Drawing.Font("微软雅黑", 17F);
            this.ucBtnImg4.BtnForeColor = System.Drawing.Color.Gray;
            this.ucBtnImg4.BtnText = "";
            this.ucBtnImg4.ConerRadius = 5;
            this.ucBtnImg4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg4.EnabledMouseEffect = true;
            this.ucBtnImg4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ucBtnImg4.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg4.Image = global::SJeMES_Control_Library.Properties.Resources.icon_add_24_g;
            this.ucBtnImg4.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg4.ImageFontIcons = null;
            this.ucBtnImg4.IsRadius = true;
            this.ucBtnImg4.IsShowRect = true;
            this.ucBtnImg4.IsShowTips = false;
            this.ucBtnImg4.Location = new System.Drawing.Point(86, 8);
            this.ucBtnImg4.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg4.Name = "ucBtnImg4";
            this.ucBtnImg4.RectColor = System.Drawing.Color.Green;
            this.ucBtnImg4.RectWidth = 1;
            this.ucBtnImg4.Size = new System.Drawing.Size(60, 30);
            this.ucBtnImg4.TabIndex = 1;
            this.ucBtnImg4.TabStop = false;
            this.ucBtnImg4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg4.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg4.TipsText = "";
            this.ucBtnImg4.BtnClick += new System.EventHandler(this.ucBtnImg4_BtnClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label2.Location = new System.Drawing.Point(16, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "操      作";
            // 
            // UCModuleBaseList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UCModuleBaseList";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.UCModuleBaseList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ucPanelQuote2.ResumeLayout(false);
            this.ucPanelQuote2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private UCPanelQuote ucPanelQuote2;
        private UCBtnImg ucBtnImg5;
        private UCBtnImg ucBtnImg3;
        private UCBtnImg ucBtnImg4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private UCSwitch ucSwitch1;
        private System.Windows.Forms.Panel panel2;
        private UCPagerControl2 ucPagerControl21;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lab_DataTotal;
        private System.Windows.Forms.Label label4;
        private UCCombox ucCombox1;
        private UCSelectTool ucSelectTool1;
    }
}
