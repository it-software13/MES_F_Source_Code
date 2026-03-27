
namespace SJeMES_TQC
{
    partial class ClainListView
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem5 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem6 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClainListView));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.file_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operate = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.file_url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.file_name,
            this.guid,
            this.operate,
            this.file_url,
            this.id});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(699, 450);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "Operate";
            dataGridViewOperationItem5.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationItem5.Image")));
            dataGridViewOperationItem5.Name = "view";
            dataGridViewOperationItem5.Text = "view";
            dataGridViewOperationItem6.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationItem6.Image")));
            dataGridViewOperationItem6.Name = "delete";
            dataGridViewOperationItem6.Text = "delete";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem5);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem6);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // file_name
            // 
            this.file_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_name.DefaultCellStyle = dataGridViewCellStyle2;
            this.file_name.HeaderText = "File Name";
            this.file_name.Name = "file_name";
            // 
            // guid
            // 
            this.guid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.guid.HeaderText = "guid";
            this.guid.Name = "guid";
            this.guid.Visible = false;
            // 
            // operate
            // 
            this.operate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.operate.Description = null;
            this.operate.HeaderText = "Operate";
            dataGridViewOperationItem4.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationItem4.Image")));
            dataGridViewOperationItem4.Name = "view";
            dataGridViewOperationItem4.Text = "view";
            this.operate.Items.Add(dataGridViewOperationItem4);
            this.operate.Name = "operate";
            this.operate.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operate.OverflowImage")));
            // 
            // file_url
            // 
            this.file_url.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.file_url.HeaderText = "File_Url";
            this.file_url.Name = "file_url";
            this.file_url.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.file_url.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.file_url.Visible = false;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // ClainListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ClainListView";
            this.Text = "ClainListView";
            this.Load += new System.EventHandler(this.ClainListView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn guid;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operate;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_url;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}