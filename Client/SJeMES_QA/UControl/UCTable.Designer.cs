
namespace SJeMES_QA.UControl
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem2 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTable));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.operation = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.did = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shoe_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itemnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workshop_section_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workshop_section_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choice_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choice_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qa_risk_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qa_risk_category_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qa_risk_details_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.art_codes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phase_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phase_creation_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phase_creation_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_production = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bad_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bad_rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.measures = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.measures_res = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_dqa_mqa_band = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.image_guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.img_url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.img_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.operation,
            this.did,
            this.shoe_code,
            this.Itemnumber,
            this.workshop_section_no,
            this.workshop_section_name,
            this.choice_no,
            this.choice_name,
            this.qa_risk_desc,
            this.qa_risk_category_code,
            this.qa_risk_details_desc,
            this.art_codes,
            this.phase_date,
            this.phase_creation_no,
            this.phase_creation_name,
            this.total_production,
            this.bad_qty,
            this.bad_rate,
            this.measures,
            this.measures_res,
            this.remark,
            this.is_dqa_mqa_band,
            this.image_guid,
            this.img_url,
            this.img_name});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 33;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(950, 164);
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(950, 202);
            this.splitContainer1.SplitterDistance = 164;
            this.splitContainer1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(7, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "编辑";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // operation
            // 
            this.operation.Description = null;
            this.operation.HeaderText = "操作";
            dataGridViewOperationItem1.Image = global::SJeMES_QA.Properties.Resources.ic_select_24;
            dataGridViewOperationItem1.Name = "DETAIL";
            dataGridViewOperationItem1.Text = "查看";
            this.operation.Items.Add(dataGridViewOperationItem1);
            this.operation.Name = "operation";
            this.operation.OverflowImage = ((System.Drawing.Image)(resources.GetObject("operation.OverflowImage")));
            // 
            // did
            // 
            this.did.HeaderText = "did";
            this.did.Name = "did";
            this.did.ReadOnly = true;
            this.did.Visible = false;
            // 
            // shoe_code
            // 
            this.shoe_code.HeaderText = "shoe_code";
            this.shoe_code.Name = "shoe_code";
            this.shoe_code.ReadOnly = true;
            this.shoe_code.Visible = false;
            // 
            // Itemnumber
            // 
            this.Itemnumber.HeaderText = "项次";
            this.Itemnumber.Name = "Itemnumber";
            // 
            // workshop_section_no
            // 
            this.workshop_section_no.HeaderText = "工段代号";
            this.workshop_section_no.Name = "workshop_section_no";
            this.workshop_section_no.Visible = false;
            // 
            // workshop_section_name
            // 
            this.workshop_section_name.HeaderText = "工段";
            this.workshop_section_name.Name = "workshop_section_name";
            // 
            // choice_no
            // 
            this.choice_no.HeaderText = "材料编号";
            this.choice_no.Name = "choice_no";
            // 
            // choice_name
            // 
            this.choice_name.HeaderText = "材料名称";
            this.choice_name.Name = "choice_name";
            // 
            // qa_risk_desc
            // 
            this.qa_risk_desc.HeaderText = "品质问题描述";
            this.qa_risk_desc.Name = "qa_risk_desc";
            // 
            // qa_risk_category_code
            // 
            this.qa_risk_category_code.HeaderText = "品质风险类别";
            this.qa_risk_category_code.Name = "qa_risk_category_code";
            // 
            // qa_risk_details_desc
            // 
            this.qa_risk_details_desc.HeaderText = "品质风险";
            this.qa_risk_details_desc.Name = "qa_risk_details_desc";
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
            this.phase_creation_no.HeaderText = "阶段代号";
            this.phase_creation_no.Name = "phase_creation_no";
            this.phase_creation_no.Visible = false;
            // 
            // phase_creation_name
            // 
            this.phase_creation_name.HeaderText = "阶段名称";
            this.phase_creation_name.Name = "phase_creation_name";
            // 
            // total_production
            // 
            this.total_production.HeaderText = "生产总数(双)";
            this.total_production.Name = "total_production";
            // 
            // bad_qty
            // 
            this.bad_qty.HeaderText = "不良数(双)";
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
            // measures_res
            // 
            this.measures_res.HeaderText = "改善结果";
            this.measures_res.Name = "measures_res";
            // 
            // remark
            // 
            this.remark.HeaderText = "负责人";
            this.remark.Name = "remark";
            // 
            // is_dqa_mqa_band
            // 
            this.is_dqa_mqa_band.HeaderText = "DQA,MQA需求绑定";
            this.is_dqa_mqa_band.Name = "is_dqa_mqa_band";
            // 
            // image_guid
            // 
            this.image_guid.HeaderText = "图片guid";
            this.image_guid.Name = "image_guid";
            this.image_guid.ReadOnly = true;
            this.image_guid.Visible = false;
            // 
            // img_url
            // 
            this.img_url.HeaderText = "图片img_url";
            this.img_url.Name = "img_url";
            this.img_url.Visible = false;
            // 
            // img_name
            // 
            this.img_name.HeaderText = "图片img_name";
            this.img_name.Name = "img_name";
            this.img_name.Visible = false;
            // 
            // UCTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UCTable";
            this.Size = new System.Drawing.Size(950, 202);
            this.Load += new System.EventHandler(this.UCTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn operation;
        private System.Windows.Forms.DataGridViewTextBoxColumn did;
        private System.Windows.Forms.DataGridViewTextBoxColumn shoe_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn workshop_section_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn workshop_section_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn choice_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn choice_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn qa_risk_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn qa_risk_category_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn qa_risk_details_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn art_codes;
        private System.Windows.Forms.DataGridViewTextBoxColumn phase_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn phase_creation_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn phase_creation_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_production;
        private System.Windows.Forms.DataGridViewTextBoxColumn bad_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn bad_rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn measures;
        private System.Windows.Forms.DataGridViewTextBoxColumn measures_res;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_dqa_mqa_band;
        private System.Windows.Forms.DataGridViewTextBoxColumn image_guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn img_url;
        private System.Windows.Forms.DataGridViewTextBoxColumn img_name;
    }
}
