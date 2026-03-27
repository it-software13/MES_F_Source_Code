namespace SJeMES_TSM
{
    partial class Supplementary_Data
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Supplementary_Data));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.upload_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file_url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.OldLace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.guid,
            this.file_name,
            this.upload_time,
            this.id,
            this.file_url});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(2, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(798, 385);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // operation
            // 
            this.operation.Description = null;
            this.operation.HeaderText = "operation";
            dataGridViewOperationItem1.Image = global::SJeMES_TSM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "DETAIL";
            dataGridViewOperationItem1.Text = null;
            dataGridViewOperationItem2.Image = global::SJeMES_TSM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem2.Name = "Delete";
            dataGridViewOperationItem2.Text = null;
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            // 
            // guid
            // 
            this.guid.HeaderText = "guid";
            this.guid.Name = "guid";
            this.guid.Visible = false;
            // 
            // file_name
            // 
            this.file_name.FillWeight = 200F;
            this.file_name.HeaderText = "filename";
            this.file_name.Name = "file_name";
            this.file_name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.file_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // upload_time
            // 
            this.upload_time.FillWeight = 200F;
            this.upload_time.HeaderText = "upload_time";
            this.upload_time.Name = "upload_time";
            this.upload_time.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.upload_time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // file_url
            // 
            this.file_url.HeaderText = "file_url";
            this.file_url.Name = "file_url";
            this.file_url.Visible = false;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem3.Image = null;
            dataGridViewOperationItem3.Name = "select";
            dataGridViewOperationItem3.Text = "select";
            dataGridViewOperationItem4.Image = null;
            dataGridViewOperationItem4.Name = "delete";
            dataGridViewOperationItem4.Text = "delete";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem3);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // Supplementary_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Supplementary_Data";
            this.Text = "Supplementary_Data";
            this.Load += new System.EventHandler(this.Supplementary_Data_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn upload_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_url;
    }
}