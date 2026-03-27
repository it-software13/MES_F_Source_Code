namespace SjeMES_QCM_Ex
{
    partial class F_QCM_Ex_Item_Set_New
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv = new SJeMES_Control_Library.DataGridViewEx();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_task_no = new System.Windows.Forms.TextBox();
            this.panel17 = new System.Windows.Forms.Panel();
            this.txt_staff_name = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.txt_staff_department = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_test_type = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.panel31 = new System.Windows.Forms.Panel();
            this.panel33 = new System.Windows.Forms.Panel();
            this.panel32 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_staff_no = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.panel30 = new System.Windows.Forms.Panel();
            this.panel34 = new System.Windows.Forms.Panel();
            this.xh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_type_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choice_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.judgment_criteria = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.judge_type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.standard_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sample_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tygs = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.zdygs = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inspection_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.choice_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewButtonColumn();
            this.d_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel17.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel31.SuspendLayout();
            this.panel32.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel30.SuspendLayout();
            this.panel34.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgv);
            this.panel2.Location = new System.Drawing.Point(12, 387);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1328, 302);
            this.panel2.TabIndex = 1;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xh,
            this.type,
            this.inspection_type_name,
            this.choice_name,
            this.inspection_code,
            this.inspection_name,
            this.judgment_criteria,
            this.judge_type,
            this.standard_value,
            this.unit,
            this.sample_qty,
            this.tygs,
            this.zdygs,
            this.remarks,
            this.inspection_type,
            this.choice_no,
            this.action,
            this.d_id});
            this.dgv.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgv.Location = new System.Drawing.Point(7, 3);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1320, 283);
            this.dgv.TabIndex = 1;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(161, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "新增检验项";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(458, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 25);
            this.button2.TabIndex = 3;
            this.button2.Text = "打印检验项二维码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(260, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 25);
            this.button3.TabIndex = 4;
            this.button3.Text = "保存";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(359, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 25);
            this.button4.TabIndex = 5;
            this.button4.Text = "取消";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务编号：";
            // 
            // txt_task_no
            // 
            this.txt_task_no.Enabled = false;
            this.txt_task_no.Location = new System.Drawing.Point(83, 78);
            this.txt_task_no.Name = "txt_task_no";
            this.txt_task_no.Size = new System.Drawing.Size(145, 20);
            this.txt_task_no.TabIndex = 1;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.txt_staff_name);
            this.panel17.Controls.Add(this.label16);
            this.panel17.Location = new System.Drawing.Point(6, 61);
            this.panel17.Margin = new System.Windows.Forms.Padding(10, 11, 0, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(233, 32);
            this.panel17.TabIndex = 15;
            // 
            // txt_staff_name
            // 
            this.txt_staff_name.Enabled = false;
            this.txt_staff_name.Location = new System.Drawing.Point(82, 6);
            this.txt_staff_name.Name = "txt_staff_name";
            this.txt_staff_name.Size = new System.Drawing.Size(145, 20);
            this.txt_staff_name.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "员工姓名：";
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.txt_staff_department);
            this.panel18.Controls.Add(this.label17);
            this.panel18.Location = new System.Drawing.Point(6, 104);
            this.panel18.Margin = new System.Windows.Forms.Padding(10, 11, 0, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(233, 38);
            this.panel18.TabIndex = 16;
            // 
            // txt_staff_department
            // 
            this.txt_staff_department.Enabled = false;
            this.txt_staff_department.Location = new System.Drawing.Point(82, 6);
            this.txt_staff_department.Name = "txt_staff_department";
            this.txt_staff_department.Size = new System.Drawing.Size(145, 20);
            this.txt_staff_department.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "员工部门：";
            // 
            // txt_test_type
            // 
            this.txt_test_type.Enabled = false;
            this.txt_test_type.Location = new System.Drawing.Point(309, 78);
            this.txt_test_type.Name = "txt_test_type";
            this.txt_test_type.Size = new System.Drawing.Size(145, 20);
            this.txt_test_type.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(238, 81);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(58, 13);
            this.label29.TabIndex = 0;
            this.label29.Text = "测试类型：";
            // 
            // panel31
            // 
            this.panel31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel31.BackColor = System.Drawing.Color.White;
            this.panel31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel31.Controls.Add(this.panel33);
            this.panel31.Controls.Add(this.panel32);
            this.panel31.Location = new System.Drawing.Point(12, 107);
            this.panel31.Name = "panel31";
            this.panel31.Size = new System.Drawing.Size(1328, 274);
            this.panel31.TabIndex = 29;
            // 
            // panel33
            // 
            this.panel33.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel33.Location = new System.Drawing.Point(14, 13);
            this.panel33.Name = "panel33";
            this.panel33.Size = new System.Drawing.Size(1059, 249);
            this.panel33.TabIndex = 1;
            // 
            // panel32
            // 
            this.panel32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel32.Controls.Add(this.panel1);
            this.panel32.Controls.Add(this.panel17);
            this.panel32.Controls.Add(this.panel18);
            this.panel32.Location = new System.Drawing.Point(1079, 13);
            this.panel32.Name = "panel32";
            this.panel32.Size = new System.Drawing.Size(244, 249);
            this.panel32.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_staff_no);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Location = new System.Drawing.Point(6, 17);
            this.panel1.Margin = new System.Windows.Forms.Padding(10, 11, 0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 33);
            this.panel1.TabIndex = 17;
            // 
            // txt_staff_no
            // 
            this.txt_staff_no.Enabled = false;
            this.txt_staff_no.Location = new System.Drawing.Point(82, 6);
            this.txt_staff_no.Name = "txt_staff_no";
            this.txt_staff_no.Size = new System.Drawing.Size(145, 20);
            this.txt_staff_no.TabIndex = 1;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(9, 9);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(61, 13);
            this.label30.TabIndex = 0;
            this.label30.Text = "员工编号：";
            // 
            // panel30
            // 
            this.panel30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel30.BackColor = System.Drawing.Color.White;
            this.panel30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel30.Controls.Add(this.panel34);
            this.panel30.Location = new System.Drawing.Point(6, 695);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(1320, 40);
            this.panel30.TabIndex = 30;
            // 
            // panel34
            // 
            this.panel34.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel34.Controls.Add(this.button1);
            this.panel34.Controls.Add(this.button2);
            this.panel34.Controls.Add(this.button4);
            this.panel34.Controls.Add(this.button3);
            this.panel34.Location = new System.Drawing.Point(726, 1);
            this.panel34.Name = "panel34";
            this.panel34.Size = new System.Drawing.Size(590, 36);
            this.panel34.TabIndex = 6;
            // 
            // xh
            // 
            this.xh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.xh.HeaderText = "序号";
            this.xh.Name = "xh";
            this.xh.ReadOnly = true;
            this.xh.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.xh.Width = 60;
            // 
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.type.HeaderText = "常规/DQA测试任务";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.type.Width = 150;
            // 
            // inspection_type_name
            // 
            this.inspection_type_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.inspection_type_name.HeaderText = "检测类型";
            this.inspection_type_name.Name = "inspection_type_name";
            this.inspection_type_name.ReadOnly = true;
            this.inspection_type_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // choice_name
            // 
            this.choice_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.choice_name.HeaderText = "检测材料";
            this.choice_name.Name = "choice_name";
            this.choice_name.ReadOnly = true;
            this.choice_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // inspection_code
            // 
            this.inspection_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.inspection_code.HeaderText = "检测项目编号";
            this.inspection_code.Name = "inspection_code";
            this.inspection_code.ReadOnly = true;
            this.inspection_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.inspection_code.Width = 120;
            // 
            // inspection_name
            // 
            this.inspection_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.inspection_name.HeaderText = "检测项目名称";
            this.inspection_name.Name = "inspection_name";
            this.inspection_name.ReadOnly = true;
            this.inspection_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.inspection_name.Width = 120;
            // 
            // judgment_criteria
            // 
            this.judgment_criteria.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.judgment_criteria.HeaderText = "判断标准";
            this.judgment_criteria.Name = "judgment_criteria";
            this.judgment_criteria.ReadOnly = true;
            this.judgment_criteria.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // judge_type
            // 
            this.judge_type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.judge_type.HeaderText = "判断类型";
            this.judge_type.Name = "judge_type";
            this.judge_type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.judge_type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // standard_value
            // 
            this.standard_value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.standard_value.HeaderText = "测量标准";
            this.standard_value.Name = "standard_value";
            this.standard_value.ReadOnly = true;
            this.standard_value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // unit
            // 
            this.unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.unit.Width = 80;
            // 
            // sample_qty
            // 
            this.sample_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sample_qty.HeaderText = "试样数量";
            this.sample_qty.Name = "sample_qty";
            this.sample_qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tygs
            // 
            this.tygs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tygs.HeaderText = "通用公式类型";
            this.tygs.Items.AddRange(new object[] {
            "平均值",
            "极差",
            "无"});
            this.tygs.Name = "tygs";
            this.tygs.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // zdygs
            // 
            this.zdygs.HeaderText = "自定义公式类型";
            this.zdygs.Name = "zdygs";
            // 
            // remarks
            // 
            this.remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.remarks.HeaderText = "ART定制备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // inspection_type
            // 
            this.inspection_type.HeaderText = "检测项目类型";
            this.inspection_type.Name = "inspection_type";
            this.inspection_type.Visible = false;
            // 
            // choice_no
            // 
            this.choice_no.HeaderText = "检验材料编号";
            this.choice_no.Name = "choice_no";
            this.choice_no.Visible = false;
            // 
            // action
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "Delete";
            this.action.DefaultCellStyle = dataGridViewCellStyle1;
            this.action.HeaderText = "操作";
            this.action.Name = "action";
            // 
            // d_id
            // 
            this.d_id.HeaderText = "d_id";
            this.d_id.Name = "d_id";
            this.d_id.Visible = false;
            // 
            // F_QCM_Ex_Item_Set_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 747);
            this.Controls.Add(this.panel30);
            this.Controls.Add(this.txt_test_type);
            this.Controls.Add(this.txt_task_no);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.panel31);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_QCM_Ex_Item_Set_New";
            this.Text = "检验项设置";
            this.Load += new System.EventHandler(this.F_QCM_Ex_Item_Set_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel31.ResumeLayout(false);
            this.panel32.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel30.ResumeLayout(false);
            this.panel34.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txt_task_no;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.TextBox txt_staff_name;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.TextBox txt_staff_department;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_test_type;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Panel panel31;
        private System.Windows.Forms.Panel panel33;
        private System.Windows.Forms.Panel panel32;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_staff_no;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Panel panel34;
        private SJeMES_Control_Library.DataGridViewEx dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn xh;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_type_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn choice_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn judgment_criteria;
        private System.Windows.Forms.DataGridViewComboBoxColumn judge_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn standard_value;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn sample_qty;
        private System.Windows.Forms.DataGridViewComboBoxColumn tygs;
        private System.Windows.Forms.DataGridViewComboBoxColumn zdygs;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn inspection_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn choice_no;
        private System.Windows.Forms.DataGridViewButtonColumn action;
        private System.Windows.Forms.DataGridViewTextBoxColumn d_id;
    }
}