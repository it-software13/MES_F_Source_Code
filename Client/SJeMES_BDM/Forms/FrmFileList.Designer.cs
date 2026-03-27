
namespace SJeMES_BDM
{
    partial class FrmFileList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FILE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FILE_URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btncz = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 331);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FILE_NAME,
            this.FILE_URL,
            this.btncz});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(362, 331);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // FILE_NAME
            // 
            this.FILE_NAME.HeaderText = "文件名";
            this.FILE_NAME.Name = "FILE_NAME";
            this.FILE_NAME.ReadOnly = true;
            // 
            // FILE_URL
            // 
            this.FILE_URL.HeaderText = "文件地址";
            this.FILE_URL.Name = "FILE_URL";
            this.FILE_URL.ReadOnly = true;
            this.FILE_URL.Visible = false;
            // 
            // btncz
            // 
            this.btncz.HeaderText = "查看";
            this.btncz.Name = "btncz";
            this.btncz.ReadOnly = true;
            this.btncz.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btncz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.btncz.Text = "查看";
            this.btncz.ToolTipText = "查看";
            this.btncz.UseColumnTextForButtonValue = true;
            // 
            // FrmFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 331);
            this.Controls.Add(this.panel1);
            this.Name = "FrmFileList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择查看文件";
            this.Load += new System.EventHandler(this.F_BDM_ProdCustomQuality_LookFile_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FILE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FILE_URL;
        private System.Windows.Forms.DataGridViewButtonColumn btncz;
    }
}