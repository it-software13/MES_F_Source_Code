
namespace SJeMES_QA.FileSForm
{
    partial class QA_Filemanagement
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.develop_season = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shoe_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.develop_season,
            this.shoe_no,
            this.file_type,
            this.file_name});
            this.dataGridView1.Location = new System.Drawing.Point(1, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.Size = new System.Drawing.Size(593, 311);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // develop_season
            // 
            this.develop_season.HeaderText = "季度";
            this.develop_season.Name = "develop_season";
            // 
            // shoe_no
            // 
            this.shoe_no.HeaderText = "shoe_no鞋型";
            this.shoe_no.Name = "shoe_no";
            // 
            // file_type
            // 
            this.file_type.HeaderText = "文件类型";
            this.file_type.Name = "file_type";
            // 
            // file_name
            // 
            this.file_name.HeaderText = "文件名";
            this.file_name.Name = "file_name";
            // 
            // QA_Filemanagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 373);
            this.Controls.Add(this.dataGridView1);
            this.Name = "QA_Filemanagement";
            this.Text = "QA_鞋品文件管理";
            this.Load += new System.EventHandler(this.QA_Filemanagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn develop_season;
        private System.Windows.Forms.DataGridViewTextBoxColumn shoe_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_name;
    }
}