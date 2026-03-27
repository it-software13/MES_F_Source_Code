
namespace SJeMES_IQC
{
    partial class ColorNotice_Upload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorNotice_Upload));
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem7 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem8 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Search = new System.Windows.Forms.Button();
            this.txt_mcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.mcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 66);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackgroundImage = global::SJeMES_IQC.Properties.Resources.istockphoto_1042172390_612x612;
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel1.Controls.Add(this.btn_Search);
            this.splitContainer1.Panel1.Controls.Add(this.txt_mcode);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pageControl1);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1052, 512);
            this.splitContainer1.SplitterDistance = 78;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.Transparent;
            this.btn_Search.BackgroundImage = global::SJeMES_IQC.Properties.Resources.SEA;
            this.btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Search.Location = new System.Drawing.Point(440, 29);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(120, 30);
            this.btn_Search.TabIndex = 3;
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // txt_mcode
            // 
            this.txt_mcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mcode.Location = new System.Drawing.Point(250, 35);
            this.txt_mcode.Name = "txt_mcode";
            this.txt_mcode.Size = new System.Drawing.Size(135, 24);
            this.txt_mcode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(82, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "MaterialCode";
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Font = new System.Drawing.Font("Microsoft YaHei", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageControl1.Location = new System.Drawing.Point(209, 368);
            this.pageControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(616, 53);
            this.pageControl1.TabIndex = 1;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.mcode,
            this.mname});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1046, 356);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.operation.Description = null;
            this.operation.FillWeight = 50F;
            this.operation.HeaderText = "Operation";
            dataGridViewOperationItem5.Image = global::SJeMES_IQC.Properties.Resources.ic_update_24;
            dataGridViewOperationItem5.Name = "upload";
            dataGridViewOperationItem5.Text = "uplod";
            dataGridViewOperationItem6.Image = global::SJeMES_IQC.Properties.Resources.ic_select_24;
            dataGridViewOperationItem6.Name = "view";
            dataGridViewOperationItem6.Text = "view";
            this.operation.Items.Add(dataGridViewOperationItem5);
            this.operation.Items.Add(dataGridViewOperationItem6);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.operation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // mcode
            // 
            this.mcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mcode.HeaderText = "Material Code";
            this.mcode.Name = "mcode";
            // 
            // mname
            // 
            this.mname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mname.FillWeight = 200F;
            this.mname.HeaderText = "Material Name";
            this.mname.Name = "mname";
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.FillWeight = 50F;
            this.dataGridViewOperationColumn1.HeaderText = "Operation";
            dataGridViewOperationItem7.Image = global::SJeMES_IQC.Properties.Resources.ic_update_24;
            dataGridViewOperationItem7.Name = "upload";
            dataGridViewOperationItem7.Text = "uplod";
            dataGridViewOperationItem8.Image = global::SJeMES_IQC.Properties.Resources.ic_select_24;
            dataGridViewOperationItem8.Name = "view";
            dataGridViewOperationItem8.Text = "view";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem7);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem8);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            this.dataGridViewOperationColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOperationColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ColorNotice_Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 582);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ColorNotice_Upload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ColorNotice_Upload";
            this.Load += new System.EventHandler(this.ColorNotice_Upload_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox txt_mcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn mcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn mname;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
    }
}