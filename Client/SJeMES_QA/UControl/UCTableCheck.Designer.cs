
namespace SJeMES_QA.UControl
{
    partial class UCTableCheck
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
            DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem dataGridViewOperationItem1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTableCheck));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewOperationColumn1 = new DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.shoes_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xz = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Itemnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workshop_section_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workshop_section_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choice_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choice_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qa_risk_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qa_risk_category_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qa_risk_category_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.image_guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_dqa_mqa_band = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.shoes_code,
            this.itemid,
            this.xz,
            this.Itemnumber,
            this.workshop_section_no,
            this.workshop_section_name,
            this.choice_no,
            this.choice_name,
            this.qa_risk_desc,
            this.qa_risk_category_code,
            this.qa_risk_category_name,
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
            this.image_guid,
            this.is_dqa_mqa_band});
            this.dataGridView1.Location = new System.Drawing.Point(0, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
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
            this.dataGridView1.Size = new System.Drawing.Size(1064, 176);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridViewOperationColumn1
            // 
            this.dataGridViewOperationColumn1.Description = null;
            this.dataGridViewOperationColumn1.Frozen = true;
            this.dataGridViewOperationColumn1.HeaderText = "操作";
            dataGridViewOperationItem1.Image = null;
            dataGridViewOperationItem1.Name = "DETAIL";
            dataGridViewOperationItem1.Text = "查看";
            this.dataGridViewOperationColumn1.Items.Add(dataGridViewOperationItem1);
            this.dataGridViewOperationColumn1.ItemSize = new System.Drawing.Size(24, 24);
            this.dataGridViewOperationColumn1.Name = "dataGridViewOperationColumn1";
            this.dataGridViewOperationColumn1.OverflowImage = ((System.Drawing.Image)(resources.GetObject("dataGridViewOperationColumn1.OverflowImage")));
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(52, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // shoes_code
            // 
            this.shoes_code.HeaderText = "鞋型";
            this.shoes_code.Name = "shoes_code";
            this.shoes_code.ReadOnly = true;
            this.shoes_code.Visible = false;
            // 
            // itemid
            // 
            this.itemid.HeaderText = "itemid";
            this.itemid.Name = "itemid";
            this.itemid.ReadOnly = true;
            this.itemid.Visible = false;
            // 
            // xz
            // 
            this.xz.HeaderText = "";
            this.xz.Name = "xz";
            this.xz.ReadOnly = true;
            // 
            // Itemnumber
            // 
            this.Itemnumber.HeaderText = "项次";
            this.Itemnumber.Name = "Itemnumber";
            this.Itemnumber.ReadOnly = true;
            // 
            // workshop_section_no
            // 
            this.workshop_section_no.HeaderText = "工段代号";
            this.workshop_section_no.Name = "workshop_section_no";
            this.workshop_section_no.ReadOnly = true;
            this.workshop_section_no.Visible = false;
            // 
            // workshop_section_name
            // 
            this.workshop_section_name.HeaderText = "工段";
            this.workshop_section_name.Name = "workshop_section_name";
            this.workshop_section_name.ReadOnly = true;
            // 
            // choice_no
            // 
            this.choice_no.HeaderText = "材料编号";
            this.choice_no.Name = "choice_no";
            this.choice_no.ReadOnly = true;
            // 
            // choice_name
            // 
            this.choice_name.HeaderText = "材料名称";
            this.choice_name.Name = "choice_name";
            this.choice_name.ReadOnly = true;
            // 
            // qa_risk_desc
            // 
            this.qa_risk_desc.HeaderText = "品质风险描述";
            this.qa_risk_desc.Name = "qa_risk_desc";
            this.qa_risk_desc.ReadOnly = true;
            // 
            // qa_risk_category_code
            // 
            this.qa_risk_category_code.HeaderText = "品质风险类别code";
            this.qa_risk_category_code.Name = "qa_risk_category_code";
            this.qa_risk_category_code.ReadOnly = true;
            this.qa_risk_category_code.Visible = false;
            // 
            // qa_risk_category_name
            // 
            this.qa_risk_category_name.HeaderText = "品质风险类别";
            this.qa_risk_category_name.Name = "qa_risk_category_name";
            this.qa_risk_category_name.ReadOnly = true;
            // 
            // qa_risk_details_desc
            // 
            this.qa_risk_details_desc.HeaderText = "品质风险";
            this.qa_risk_details_desc.Name = "qa_risk_details_desc";
            this.qa_risk_details_desc.ReadOnly = true;
            // 
            // art_codes
            // 
            this.art_codes.HeaderText = "相关art";
            this.art_codes.Name = "art_codes";
            this.art_codes.ReadOnly = true;
            // 
            // phase_date
            // 
            this.phase_date.HeaderText = "日期";
            this.phase_date.Name = "phase_date";
            this.phase_date.ReadOnly = true;
            // 
            // phase_creation_no
            // 
            this.phase_creation_no.HeaderText = "阶段代号";
            this.phase_creation_no.Name = "phase_creation_no";
            this.phase_creation_no.ReadOnly = true;
            this.phase_creation_no.Visible = false;
            // 
            // phase_creation_name
            // 
            this.phase_creation_name.HeaderText = "阶段名称";
            this.phase_creation_name.Name = "phase_creation_name";
            this.phase_creation_name.ReadOnly = true;
            // 
            // total_production
            // 
            this.total_production.HeaderText = "生产总数(双)";
            this.total_production.Name = "total_production";
            this.total_production.ReadOnly = true;
            // 
            // bad_qty
            // 
            this.bad_qty.HeaderText = "不良数(双)";
            this.bad_qty.Name = "bad_qty";
            this.bad_qty.ReadOnly = true;
            // 
            // bad_rate
            // 
            this.bad_rate.HeaderText = "不良率";
            this.bad_rate.Name = "bad_rate";
            this.bad_rate.ReadOnly = true;
            // 
            // measures
            // 
            this.measures.HeaderText = "改善措施&行动方案";
            this.measures.Name = "measures";
            this.measures.ReadOnly = true;
            // 
            // measures_res
            // 
            this.measures_res.HeaderText = "改善结果";
            this.measures_res.Name = "measures_res";
            this.measures_res.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.HeaderText = "负责人";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // image_guid
            // 
            this.image_guid.HeaderText = "图片guid";
            this.image_guid.Name = "image_guid";
            this.image_guid.ReadOnly = true;
            this.image_guid.Visible = false;
            // 
            // is_dqa_mqa_band
            // 
            this.is_dqa_mqa_band.HeaderText = "DQA,MQA需求绑定";
            this.is_dqa_mqa_band.Name = "is_dqa_mqa_band";
            this.is_dqa_mqa_band.ReadOnly = true;
            // 
            // UCTableCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "UCTableCheck";
            this.Size = new System.Drawing.Size(1067, 202);
            this.Load += new System.EventHandler(this.UCTableCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGrid.DataGridViewCustomColumn.DataGridViewOperationColumn dataGridViewOperationColumn1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn shoes_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn xz;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn workshop_section_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn workshop_section_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn choice_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn choice_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn qa_risk_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn qa_risk_category_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn qa_risk_category_name;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn image_guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_dqa_mqa_band;
    }
}
