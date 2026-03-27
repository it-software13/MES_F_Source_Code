
namespace F_TailorRounds
{
    partial class ProductionWorkOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvCheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvProductionWorkOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSaleOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSure = new System.Windows.Forms.Button();
            this.txtProductWorKOrder = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbProductionWorkOrder = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAllCheckOrNo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCheckBox,
            this.dgvProductionWorkOrder,
            this.dgvSaleOrder});
            this.dataGridView1.Location = new System.Drawing.Point(5, 65);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(278, 463);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dgvCheckBox
            // 
            this.dgvCheckBox.FillWeight = 60.9137F;
            this.dgvCheckBox.HeaderText = "选择";
            this.dgvCheckBox.Name = "dgvCheckBox";
            // 
            // dgvProductionWorkOrder
            // 
            this.dgvProductionWorkOrder.DataPropertyName = "UDF01";
            this.dgvProductionWorkOrder.HeaderText = "生产工单";
            this.dgvProductionWorkOrder.Name = "dgvProductionWorkOrder";
            // 
            // dgvSaleOrder
            // 
            this.dgvSaleOrder.DataPropertyName = "SALES_ORDER";
            this.dgvSaleOrder.HeaderText = "销售订单";
            this.dgvSaleOrder.Name = "dgvSaleOrder";
            // 
            // btnSure
            // 
            this.btnSure.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSure.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSure.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSure.Location = new System.Drawing.Point(289, 534);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(101, 35);
            this.btnSure.TabIndex = 2;
            this.btnSure.Text = "确认";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // txtProductWorKOrder
            // 
            this.txtProductWorKOrder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtProductWorKOrder.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProductWorKOrder.Location = new System.Drawing.Point(116, 534);
            this.txtProductWorKOrder.Multiline = true;
            this.txtProductWorKOrder.Name = "txtProductWorKOrder";
            this.txtProductWorKOrder.Size = new System.Drawing.Size(167, 35);
            this.txtProductWorKOrder.TabIndex = 3;
            this.txtProductWorKOrder.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.lbProductionWorkOrder);
            this.groupBox1.Location = new System.Drawing.Point(289, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 463);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // lbProductionWorkOrder
            // 
            this.lbProductionWorkOrder.AutoSize = true;
            this.lbProductionWorkOrder.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbProductionWorkOrder.Location = new System.Drawing.Point(6, 5);
            this.lbProductionWorkOrder.Name = "lbProductionWorkOrder";
            this.lbProductionWorkOrder.Size = new System.Drawing.Size(52, 21);
            this.lbProductionWorkOrder.TabIndex = 1;
            this.lbProductionWorkOrder.Text = "已选";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView2.Location = new System.Drawing.Point(6, 30);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(225, 427);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            this.dataGridView2.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView2_CellPainting);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "生产工单";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "操作";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDel.Location = new System.Drawing.Point(411, 534);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(109, 35);
            this.btnDel.TabIndex = 2;
            this.btnDel.Text = "一键删除";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAllCheckOrNo
            // 
            this.btnAllCheckOrNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAllCheckOrNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllCheckOrNo.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAllCheckOrNo.Location = new System.Drawing.Point(5, 534);
            this.btnAllCheckOrNo.Name = "btnAllCheckOrNo";
            this.btnAllCheckOrNo.Size = new System.Drawing.Size(105, 35);
            this.btnAllCheckOrNo.TabIndex = 4;
            this.btnAllCheckOrNo.Text = "全选";
            this.btnAllCheckOrNo.UseVisualStyleBackColor = true;
            this.btnAllCheckOrNo.Click += new System.EventHandler(this.btnAllCheckOrNo_Click);
            // 
            // ProductionWorkOrder
            // 
            this.AcceptButton = this.btnSure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 587);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAllCheckOrNo);
            this.Controls.Add(this.txtProductWorKOrder);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "ProductionWorkOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生产工单查询";
            this.Load += new System.EventHandler(this.ProductionWorkOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.TextBox txtProductWorKOrder;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProductionWorkOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSaleOrder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbProductionWorkOrder;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAllCheckOrNo;
    }
}