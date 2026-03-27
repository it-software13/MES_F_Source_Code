
namespace SJeMES_BDM.UControl
{
    partial class UCTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTable));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.Itemnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choice_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choice_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qa_risk_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qa_risk_category_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.art_codes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phase_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phase_creation_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_production = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bad_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bad_rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.measures = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.person_in_charge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.image_guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.Itemnumber,
            this.choice_no,
            this.choice_name,
            this.qa_risk_desc,
            this.qa_risk_category_code,
            this.art_codes,
            this.phase_date,
            this.phase_creation_no,
            this.total_production,
            this.bad_qty,
            this.bad_rate,
            this.measures,
            this.person_in_charge,
            this.image_guid});
            this.dataGridView1.Location = new System.Drawing.Point(0, 26);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 33;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(950, 176);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.Frozen = true;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem2.Image = null;
            dataGridViewOperationItem2.Name = "DETAIL";
            dataGridViewOperationItem2.Text = "查看";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem2);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // operation
            // 
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_BDM.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "DETAIL";
            dataGridViewOperationItem1.Text = "查看";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            // 
            // Itemnumber
            // 
            this.Itemnumber.HeaderText = "项次";
            this.Itemnumber.Name = "Itemnumber";
            this.Itemnumber.Visible = false;
            // 
            // choice_no
            // 
            this.choice_no.HeaderText = "材料编号/工序代码";
            this.choice_no.Name = "choice_no";
            // 
            // choice_name
            // 
            this.choice_name.HeaderText = "材料名称/工序名称";
            this.choice_name.Name = "choice_name";
            // 
            // qa_risk_desc
            // 
            this.qa_risk_desc.HeaderText = "品质风险描述";
            this.qa_risk_desc.Name = "qa_risk_desc";
            // 
            // qa_risk_category_code
            // 
            this.qa_risk_category_code.HeaderText = "品质风险类别";
            this.qa_risk_category_code.Name = "qa_risk_category_code";
            // 
            // art_codes
            // 
            this.art_codes.HeaderText = "相关art";
            this.art_codes.Name = "art_codes";
            // 
            // phase_date
            // 
            this.phase_date.HeaderText = "日期";
            this.phase_date.Name = "phase_date";
            // 
            // phase_creation_no
            // 
            this.phase_creation_no.HeaderText = "阶段";
            this.phase_creation_no.Name = "phase_creation_no";
            // 
            // total_production
            // 
            this.total_production.HeaderText = "生产总数";
            this.total_production.Name = "total_production";
            // 
            // bad_qty
            // 
            this.bad_qty.HeaderText = "不良数";
            this.bad_qty.Name = "bad_qty";
            // 
            // bad_rate
            // 
            this.bad_rate.HeaderText = "不良率";
            this.bad_rate.Name = "bad_rate";
            // 
            // measures
            // 
            this.measures.HeaderText = "改善措施&行动方案";
            this.measures.Name = "measures";
            // 
            // person_in_charge
            // 
            this.person_in_charge.HeaderText = "负责人";
            this.person_in_charge.Name = "person_in_charge";
            // 
            // image_guid
            // 
            this.image_guid.HeaderText = "图片guid";
            this.image_guid.Name = "image_guid";
            this.image_guid.ReadOnly = true;
            this.image_guid.Visible = false;
            // 
            // UCTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "UCTable";
            this.Size = new System.Drawing.Size(950, 202);
            this.Load += new System.EventHandler(this.UCTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn choice_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn choice_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn qa_risk_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn qa_risk_category_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn art_codes;
        private System.Windows.Forms.DataGridViewTextBoxColumn phase_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn phase_creation_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_production;
        private System.Windows.Forms.DataGridViewTextBoxColumn bad_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn bad_rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn measures;
        private System.Windows.Forms.DataGridViewTextBoxColumn person_in_charge;
        private System.Windows.Forms.DataGridViewTextBoxColumn image_guid;
    }
}
