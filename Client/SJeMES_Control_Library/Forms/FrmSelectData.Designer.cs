namespace SJeMES_Control_Library.Forms
{
    partial class FrmSelectData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectData));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ucPagerControl21 = new SJeMES_Control_Library.Controls.UCPagerControl2();
            this.ucSelectTool1 = new SJeMES_Control_Library.Controls.UCSelectTool();
            this.ucBtnImg4 = new SJeMES_Control_Library.Controls.UCBtnImg();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            this.dataGridView1.Location = new System.Drawing.Point(7, 131);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(660, 431);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // ucPagerControl21
            // 
            this.ucPagerControl21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPagerControl21.BackColor = System.Drawing.Color.White;
            this.ucPagerControl21.Client = null;
            this.ucPagerControl21.DataSource = ((System.Collections.Generic.List<object>)(resources.GetObject("ucPagerControl21.DataSource")));
            this.ucPagerControl21.Location = new System.Drawing.Point(7, 569);
            this.ucPagerControl21.Name = "ucPagerControl21";
            this.ucPagerControl21.PageCount = 0;
            this.ucPagerControl21.PageIndex = 1;
            this.ucPagerControl21.PageModel = SJeMES_Control_Library.Controls.PageModel.PageCount;
            this.ucPagerControl21.PageSize = 10;
            this.ucPagerControl21.Size = new System.Drawing.Size(662, 44);
            this.ucPagerControl21.StartIndex = 0;
            this.ucPagerControl21.TabIndex = 5;
            this.ucPagerControl21.ShowSourceChanged += new SJeMES_Control_Library.Controls.PageControlEventHandler(this.ucPagerControl21_ShowSourceChanged);
            // 
            // ucSelectTool1
            // 
            this.ucSelectTool1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucSelectTool1.Client = null;
            this.ucSelectTool1.ConerRadius = 24;
            this.ucSelectTool1.FillColor = System.Drawing.Color.Transparent;
            this.ucSelectTool1.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucSelectTool1.IsRadius = false;
            this.ucSelectTool1.IsSelectMore = false;
            this.ucSelectTool1.IsShowRect = false;
            this.ucSelectTool1.Keys = null;
            this.ucSelectTool1.Location = new System.Drawing.Point(7, 71);
            this.ucSelectTool1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucSelectTool1.Name = "ucSelectTool1";
            this.ucSelectTool1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucSelectTool1.RectWidth = 1;
            this.ucSelectTool1.Size = new System.Drawing.Size(581, 52);
            this.ucSelectTool1.TabIndex = 0;
            this.ucSelectTool1.SelectData += new SJeMES_Control_Library.Controls.UCSelectTool.SelectDataHandle(this.ucSelectTool1_SelectData);
            this.ucSelectTool1.Load += new System.EventHandler(this.ucSelectTool1_Load);
            // 
            // ucBtnImg4
            // 
            this.ucBtnImg4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucBtnImg4.BackColor = System.Drawing.Color.White;
            this.ucBtnImg4.BtnBackColor = System.Drawing.Color.White;
            this.ucBtnImg4.BtnFont = new System.Drawing.Font("Microsoft YaHei", 17F);
            this.ucBtnImg4.BtnForeColor = System.Drawing.Color.Gray;
            this.ucBtnImg4.BtnText = "";
            this.ucBtnImg4.ConerRadius = 5;
            this.ucBtnImg4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnImg4.EnabledMouseEffect = true;
            this.ucBtnImg4.FillColor = System.Drawing.Color.Green;
            this.ucBtnImg4.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnImg4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.ucBtnImg4.Image = global::SJeMES_Control_Library.Properties.Resources.icon_select_24;
            this.ucBtnImg4.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg4.ImageFontIcons = null;
            this.ucBtnImg4.IsRadius = true;
            this.ucBtnImg4.IsShowRect = true;
            this.ucBtnImg4.IsShowTips = false;
            this.ucBtnImg4.Location = new System.Drawing.Point(592, 81);
            this.ucBtnImg4.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnImg4.Name = "ucBtnImg4";
            this.ucBtnImg4.RectColor = System.Drawing.Color.Green;
            this.ucBtnImg4.RectWidth = 1;
            this.ucBtnImg4.Size = new System.Drawing.Size(69, 29);
            this.ucBtnImg4.TabIndex = 2;
            this.ucBtnImg4.TabStop = false;
            this.ucBtnImg4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucBtnImg4.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnImg4.TipsText = "";
            this.ucBtnImg4.BtnClick += new System.EventHandler(this.ucBtnImg4_BtnClick);
            // 
            // check
            // 
            this.check.HeaderText = "choose";
            this.check.Name = "check";
            this.check.ReadOnly = true;
            this.check.Width = 48;
            // 
            // FrmSelectData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(677, 616);
            this.Controls.Add(this.ucBtnImg4);
            this.Controls.Add(this.ucPagerControl21);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ucSelectTool1);
            this.MinimumSize = new System.Drawing.Size(677, 616);
            this.Name = "FrmSelectData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "select data";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.UCSelectTool ucSelectTool1;
        private Controls.UCBtnImg ucBtnImg4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Controls.UCPagerControl2 ucPagerControl21;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
    }
}