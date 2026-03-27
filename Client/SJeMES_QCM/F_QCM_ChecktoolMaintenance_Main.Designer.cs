
namespace SJeMES_QCM
{
    partial class F_QCM_ChecktoolMaintenance_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem3 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem4 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_QCM_ChecktoolMaintenance_Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimeP_end_date = new System.Windows.Forms.DateTimePicker();
            this.dateTimeP_putin_date = new System.Windows.Forms.DateTimePicker();
            this.btn_Select = new System.Windows.Forms.Button();
            this.lab_gjmc = new System.Windows.Forms.Label();
            this.txt_INSPECT_ORDER = new System.Windows.Forms.TextBox();
            this.lab_sjsx = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageControl1 = new SJeMES_Control_Library.Controls.PageControl();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.INSPECT_ORDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INSPECT_TOOL_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INSPECT_TOOL_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INSPECT_RESULT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INSPECT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 64);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimeP_end_date);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimeP_putin_date);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Select);
            this.splitContainer1.Panel1.Controls.Add(this.txt_INSPECT_ORDER);
            this.splitContainer1.Panel1.Controls.Add(this.lab_gjmc);
            this.splitContainer1.Panel1.Controls.Add(this.lab_sjsx);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1053, 597);
            this.splitContainer1.SplitterDistance = 89;
            this.splitContainer1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(618, 31);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(39, 34);
            this.textBox2.TabIndex = 114;
            this.textBox2.Text = "～";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTimeP_end_date
            // 
            this.dateTimeP_end_date.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_end_date.Location = new System.Drawing.Point(663, 32);
            this.dateTimeP_end_date.Name = "dateTimeP_end_date";
            this.dateTimeP_end_date.Size = new System.Drawing.Size(185, 33);
            this.dateTimeP_end_date.TabIndex = 3;
            // 
            // dateTimeP_putin_date
            // 
            this.dateTimeP_putin_date.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimeP_putin_date.Location = new System.Drawing.Point(419, 32);
            this.dateTimeP_putin_date.Name = "dateTimeP_putin_date";
            this.dateTimeP_putin_date.Size = new System.Drawing.Size(193, 33);
            this.dateTimeP_putin_date.TabIndex = 2;
            // 
            // btn_Select
            // 
            this.btn_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Select.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Select.Location = new System.Drawing.Point(894, 32);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(85, 33);
            this.btn_Select.TabIndex = 111;
            this.btn_Select.Text = "搜索";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // lab_gjmc
            // 
            this.lab_gjmc.AutoSize = true;
            this.lab_gjmc.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_gjmc.Location = new System.Drawing.Point(20, 36);
            this.lab_gjmc.Name = "lab_gjmc";
            this.lab_gjmc.Size = new System.Drawing.Size(107, 25);
            this.lab_gjmc.TabIndex = 107;
            this.lab_gjmc.Text = "工具名称：";
            // 
            // txt_INSPECT_ORDER
            // 
            this.txt_INSPECT_ORDER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_INSPECT_ORDER.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_INSPECT_ORDER.Location = new System.Drawing.Point(128, 32);
            this.txt_INSPECT_ORDER.Name = "txt_INSPECT_ORDER";
            this.txt_INSPECT_ORDER.Size = new System.Drawing.Size(141, 33);
            this.txt_INSPECT_ORDER.TabIndex = 1;
            // 
            // lab_sjsx
            // 
            this.lab_sjsx.AutoSize = true;
            this.lab_sjsx.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_sjsx.Location = new System.Drawing.Point(306, 36);
            this.lab_sjsx.Name = "lab_sjsx";
            this.lab_sjsx.Size = new System.Drawing.Size(107, 25);
            this.lab_sjsx.TabIndex = 109;
            this.lab_sjsx.Text = "时间筛选：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 33;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.INSPECT_ORDER,
            this.INSPECT_TOOL_CODE,
            this.INSPECT_TOOL_NAME,
            this.INSPECT_RESULT,
            this.INSPECT_DATE});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1053, 446);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pageControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 446);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1053, 58);
            this.panel1.TabIndex = 1;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pageControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageControl1.Location = new System.Drawing.Point(330, 3);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.PageCount = 0;
            this.pageControl1.PageIndex = 0;
            this.pageControl1.PageSize = 15;
            this.pageControl1.Size = new System.Drawing.Size(721, 49);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalCount = 0;
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.Frozen = true;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem3.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem3.Name = "DELETE";
            dataGridViewOperationItem3.Text = "DELETE";
            dataGridViewOperationItem4.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem4.Name = "DETAIL";
            dataGridViewOperationItem4.Text = "DETAIL";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem3);
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem4);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // operation
            // 
            this.operation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.operation.Description = null;
            this.operation.Frozen = true;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QCM.Properties.Resources.ic_delete_24;
            dataGridViewOperationItem1.Name = "DELETE";
            dataGridViewOperationItem1.Text = "DELETE";
            dataGridViewOperationItem2.Image = global::SJeMES_QCM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem2.Name = "DETAIL";
            dataGridViewOperationItem2.Text = "DETAIL";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Items.Add(dataGridViewOperationItem2);
            this.operation.ItemSize = new System.Drawing.Size(24, 24);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            this.operation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // INSPECT_ORDER
            // 
            this.INSPECT_ORDER.HeaderText = "检验单号";
            this.INSPECT_ORDER.Name = "INSPECT_ORDER";
            this.INSPECT_ORDER.Visible = false;
            // 
            // INSPECT_TOOL_CODE
            // 
            this.INSPECT_TOOL_CODE.HeaderText = "检验工具编号";
            this.INSPECT_TOOL_CODE.Name = "INSPECT_TOOL_CODE";
            // 
            // INSPECT_TOOL_NAME
            // 
            this.INSPECT_TOOL_NAME.HeaderText = "检验工具名称";
            this.INSPECT_TOOL_NAME.Name = "INSPECT_TOOL_NAME";
            // 
            // INSPECT_RESULT
            // 
            this.INSPECT_RESULT.HeaderText = "检验结果";
            this.INSPECT_RESULT.Name = "INSPECT_RESULT";
            // 
            // INSPECT_DATE
            // 
            this.INSPECT_DATE.HeaderText = "检验时间";
            this.INSPECT_DATE.Name = "INSPECT_DATE";
            // 
            // F_QCM_ChecktoolMaintenance_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 661);
            this.Controls.Add(this.splitContainer1);
            this.Name = "F_QCM_ChecktoolMaintenance_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检验工具保养维护记录";
            this.Load += new System.EventHandler(this.F_QCM_ChecktoolMaintenance_Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Label lab_gjmc;
        private System.Windows.Forms.TextBox txt_INSPECT_ORDER;
        private System.Windows.Forms.Label lab_sjsx;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private SJeMES_Control_Library.Controls.PageControl pageControl1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker dateTimeP_end_date;
        private System.Windows.Forms.DateTimePicker dateTimeP_putin_date;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn INSPECT_ORDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn INSPECT_TOOL_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn INSPECT_TOOL_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn INSPECT_RESULT;
        private System.Windows.Forms.DataGridViewTextBoxColumn INSPECT_DATE;
    }
}