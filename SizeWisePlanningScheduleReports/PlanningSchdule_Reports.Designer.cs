namespace PlanningSchedule_Reports
{
    partial class PlanningSchdule_Reports
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            PlanningSchedule = new TabControl();
            label6 = new Label();
            tableLayoutPanel7 = new TableLayoutPanel();
            week = new Label();
            Delete = new Button();
            dateTimePicker1 = new DateTimePicker();
            SaveOrUpdate = new Button();
            Search = new Button();
            dateTimePicker2 = new DateTimePicker();
            dataGridView3 = new DataGridView();
            LINE = new DataGridViewTextBoxColumn();
            WEEKVALUE = new DataGridViewTextBoxColumn();
            Sales_Order = new DataGridViewTextBoxColumn();
            CONO = new DataGridViewTextBoxColumn();
            ART_NO = new DataGridViewTextBoxColumn();
            SHOE_NAME = new DataGridViewTextBoxColumn();
            CRD = new DataGridViewTextBoxColumn();
            LPD = new DataGridViewTextBoxColumn();
            PSDD = new DataGridViewTextBoxColumn();
            LAST_NO = new DataGridViewTextBoxColumn();
            MOLD_NO = new DataGridViewTextBoxColumn();
            QTY = new DataGridViewTextBoxColumn();
            CLASS_CODE = new DataGridViewTextBoxColumn();
            DESTINATION = new DataGridViewTextBoxColumn();
            REMARKS1 = new DataGridViewTextBoxColumn();
            REMARKS2 = new DataGridViewTextBoxColumn();
            UpdateStatus = new DataGridViewTextBoxColumn();
            PlanningScheduleTab = new TabControl();
            Planning_Schedule = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            label8 = new Label();
            comboBox1 = new ComboBox();
            label9 = new Label();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            label5 = new Label();
            label10 = new Label();
            comboBox2 = new ComboBox();
            label1 = new Label();
            dateTimePicker5 = new DateTimePicker();
            dateTimePicker6 = new DateTimePicker();
            label4 = new Label();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button5 = new Button();
            button3 = new Button();
            button1 = new Button();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            PlanningScheduleTab.SuspendLayout();
            Planning_Schedule.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // PlanningSchedule
            // 
            PlanningSchedule.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlanningSchedule.Location = new Point(3, 1);
            PlanningSchedule.Name = "PlanningSchedule";
            PlanningSchedule.SelectedIndex = 0;
            PlanningSchedule.Size = new Size(1813, 824);
            PlanningSchedule.TabIndex = 0;
            PlanningSchedule.SelectedIndexChanged += PlanningSchedule_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.BackColor = Color.RoyalBlue;
            label6.Font = new Font("Lucida Bright", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(-4, 0);
            label6.MaximumSize = new Size(2000, 50);
            label6.MinimumSize = new Size(2000, 50);
            label6.Name = "label6";
            label6.Padding = new Padding(10);
            label6.Size = new Size(2000, 50);
            label6.TabIndex = 3;
            label6.Text = "Planning Maual Schedule Upload";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Click += label6_Click;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel7.BackColor = Color.MediumTurquoise;
            tableLayoutPanel7.ColumnCount = 6;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17F));
            tableLayoutPanel7.Controls.Add(week, 0, 0);
            tableLayoutPanel7.Controls.Add(Delete, 5, 0);
            tableLayoutPanel7.Controls.Add(dateTimePicker1, 1, 0);
            tableLayoutPanel7.Controls.Add(SaveOrUpdate, 4, 0);
            tableLayoutPanel7.Controls.Add(Search, 3, 0);
            tableLayoutPanel7.Controls.Add(dateTimePicker2, 2, 0);
            tableLayoutPanel7.Location = new Point(20, 67);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Size = new Size(1760, 65);
            tableLayoutPanel7.TabIndex = 10;
            tableLayoutPanel7.Paint += tableLayoutPanel7_Paint;
            // 
            // week
            // 
            week.Anchor = AnchorStyles.Right;
            week.BackColor = Color.Orange;
            week.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            week.Location = new Point(178, 17);
            week.MaximumSize = new Size(100, 30);
            week.MinimumSize = new Size(100, 30);
            week.Name = "week";
            week.Size = new Size(100, 30);
            week.TabIndex = 3;
            week.Text = "week";
            week.TextAlign = ContentAlignment.MiddleCenter;
            week.Click += week_Click;
            // 
            // Delete
            // 
            Delete.Anchor = AnchorStyles.Left;
            Delete.BackColor = Color.Gold;
            Delete.Location = new Point(1462, 17);
            Delete.Name = "Delete";
            Delete.Size = new Size(100, 30);
            Delete.TabIndex = 7;
            Delete.Text = "Delete";
            Delete.UseVisualStyleBackColor = false;
            Delete.Click += Delete_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = AnchorStyles.None;
            dateTimePicker1.Location = new Point(321, 17);
            dateTimePicker1.MaximumSize = new Size(200, 30);
            dateTimePicker1.MinimumSize = new Size(200, 30);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 30);
            dateTimePicker1.TabIndex = 1;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // SaveOrUpdate
            // 
            SaveOrUpdate.Anchor = AnchorStyles.None;
            SaveOrUpdate.BackColor = Color.LightCoral;
            SaveOrUpdate.Location = new Point(1209, 17);
            SaveOrUpdate.MaximumSize = new Size(200, 30);
            SaveOrUpdate.MinimumSize = new Size(200, 30);
            SaveOrUpdate.Name = "SaveOrUpdate";
            SaveOrUpdate.Size = new Size(200, 30);
            SaveOrUpdate.TabIndex = 6;
            SaveOrUpdate.Text = "Save/Update";
            SaveOrUpdate.UseVisualStyleBackColor = false;
            SaveOrUpdate.Click += SaveOrUpdate_Click;
            // 
            // Search
            // 
            Search.Anchor = AnchorStyles.None;
            Search.BackColor = Color.ForestGreen;
            Search.Cursor = Cursors.Hand;
            Search.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Search.ForeColor = SystemColors.ButtonHighlight;
            Search.Location = new Point(960, 17);
            Search.Name = "Search";
            Search.Size = new Size(100, 30);
            Search.TabIndex = 5;
            Search.Text = "Search";
            Search.UseMnemonic = false;
            Search.UseVisualStyleBackColor = false;
            Search.Click += Search_Click;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Anchor = AnchorStyles.None;
            dateTimePicker2.Location = new Point(611, 17);
            dateTimePicker2.MaximumSize = new Size(200, 30);
            dateTimePicker2.MinimumSize = new Size(200, 30);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(200, 30);
            dateTimePicker2.TabIndex = 4;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // dataGridView3
            // 
            dataGridView3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView3.BackgroundColor = SystemColors.ActiveCaption;
            dataGridView3.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.PaleGreen;
            dataGridViewCellStyle1.Font = new Font("Lucida Bright", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Columns.AddRange(new DataGridViewColumn[] { LINE, WEEKVALUE, Sales_Order, CONO, ART_NO, SHOE_NAME, CRD, LPD, PSDD, LAST_NO, MOLD_NO, QTY, CLASS_CODE, DESTINATION, REMARKS1, REMARKS2, UpdateStatus });
            dataGridView3.GridColor = Color.SeaGreen;
            dataGridView3.Location = new Point(20, 138);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.Size = new Size(1760, 642);
            dataGridView3.TabIndex = 9;
            dataGridView3.CellContentClick += dataGridView3_CellContentClick;
            // 
            // LINE
            // 
            LINE.HeaderText = "LINE";
            LINE.Name = "LINE";
            // 
            // WEEKVALUE
            // 
            WEEKVALUE.HeaderText = "WEEK";
            WEEKVALUE.Name = "WEEKVALUE";
            WEEKVALUE.Width = 150;
            // 
            // Sales_Order
            // 
            Sales_Order.HeaderText = "Sales Order";
            Sales_Order.Name = "Sales_Order";
            Sales_Order.Width = 120;
            // 
            // CONO
            // 
            CONO.HeaderText = "CONO";
            CONO.Name = "CONO";
            // 
            // ART_NO
            // 
            ART_NO.HeaderText = "ART NO";
            ART_NO.Name = "ART_NO";
            // 
            // SHOE_NAME
            // 
            SHOE_NAME.HeaderText = "SHOE NAME";
            SHOE_NAME.Name = "SHOE_NAME";
            SHOE_NAME.Width = 150;
            // 
            // CRD
            // 
            CRD.HeaderText = "CRD";
            CRD.Name = "CRD";
            // 
            // LPD
            // 
            LPD.HeaderText = "LPD";
            LPD.Name = "LPD";
            // 
            // PSDD
            // 
            PSDD.HeaderText = "PSDD";
            PSDD.Name = "PSDD";
            // 
            // LAST_NO
            // 
            LAST_NO.HeaderText = "LAST NO";
            LAST_NO.Name = "LAST_NO";
            // 
            // MOLD_NO
            // 
            MOLD_NO.HeaderText = "MOLD NO";
            MOLD_NO.Name = "MOLD_NO";
            // 
            // QTY
            // 
            QTY.HeaderText = "QTY";
            QTY.Name = "QTY";
            // 
            // CLASS_CODE
            // 
            CLASS_CODE.HeaderText = "CLASS CODE";
            CLASS_CODE.Name = "CLASS_CODE";
            CLASS_CODE.Width = 120;
            // 
            // DESTINATION
            // 
            DESTINATION.HeaderText = "DESTINATION";
            DESTINATION.Name = "DESTINATION";
            // 
            // REMARKS1
            // 
            REMARKS1.HeaderText = "REMARKS1";
            REMARKS1.Name = "REMARKS1";
            // 
            // REMARKS2
            // 
            REMARKS2.HeaderText = "REMARKS2";
            REMARKS2.Name = "REMARKS2";
            // 
            // UpdateStatus
            // 
            UpdateStatus.HeaderText = "UpdateStatus";
            UpdateStatus.Name = "UpdateStatus";
            // 
            // PlanningScheduleTab
            // 
            PlanningScheduleTab.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlanningScheduleTab.Controls.Add(Planning_Schedule);
            PlanningScheduleTab.Location = new Point(3, -1);
            PlanningScheduleTab.Name = "PlanningScheduleTab";
            PlanningScheduleTab.SelectedIndex = 0;
            PlanningScheduleTab.Size = new Size(1589, 827);
            PlanningScheduleTab.TabIndex = 0;
            PlanningScheduleTab.SelectedIndexChanged += PlanningScheduleTab_SelectedIndexChanged;
            // 
            // Planning_Schedule
            // 
            Planning_Schedule.BackColor = Color.White;
            Planning_Schedule.Controls.Add(tableLayoutPanel1);
            Planning_Schedule.Controls.Add(label2);
            Planning_Schedule.Controls.Add(dataGridView1);
            Planning_Schedule.Location = new Point(4, 24);
            Planning_Schedule.Name = "Planning_Schedule";
            Planning_Schedule.Padding = new Padding(3);
            Planning_Schedule.Size = new Size(1581, 799);
            Planning_Schedule.TabIndex = 0;
            Planning_Schedule.Text = "Planning_Schedule_Reports";
            Planning_Schedule.Click += Planning_Schedule_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.BackColor = SystemColors.ActiveCaption;
            tableLayoutPanel1.ColumnCount = 9;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.11111F));
            tableLayoutPanel1.Controls.Add(label8, 0, 0);
            tableLayoutPanel1.Controls.Add(comboBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(label9, 2, 0);
            tableLayoutPanel1.Controls.Add(comboBox4, 3, 0);
            tableLayoutPanel1.Controls.Add(comboBox3, 1, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 1);
            tableLayoutPanel1.Controls.Add(label10, 2, 1);
            tableLayoutPanel1.Controls.Add(comboBox2, 3, 1);
            tableLayoutPanel1.Controls.Add(label1, 4, 0);
            tableLayoutPanel1.Controls.Add(dateTimePicker5, 5, 0);
            tableLayoutPanel1.Controls.Add(dateTimePicker6, 6, 0);
            tableLayoutPanel1.Controls.Add(label4, 4, 1);
            tableLayoutPanel1.Controls.Add(textBox2, 5, 1);
            tableLayoutPanel1.Controls.Add(textBox1, 3, 2);
            tableLayoutPanel1.Controls.Add(button5, 8, 0);
            tableLayoutPanel1.Controls.Add(button3, 7, 1);
            tableLayoutPanel1.Controls.Add(button1, 8, 1);
            tableLayoutPanel1.Location = new Point(3, 53);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(1575, 205);
            tableLayoutPanel1.TabIndex = 11;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.Location = new Point(37, 10);
            label8.MaximumSize = new Size(100, 30);
            label8.MinimumSize = new Size(100, 30);
            label8.Name = "label8";
            label8.Size = new Size(100, 30);
            label8.TabIndex = 22;
            label8.Text = "Factory";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            label8.Click += label8_Click;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownHeight = 130;
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Location = new Point(178, 14);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(169, 23);
            comboBox1.TabIndex = 24;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.None;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = SystemColors.ActiveCaptionText;
            label9.Location = new Point(387, 10);
            label9.MaximumSize = new Size(100, 30);
            label9.MinimumSize = new Size(100, 30);
            label9.Name = "label9";
            label9.Size = new Size(100, 30);
            label9.TabIndex = 23;
            label9.Text = "Process";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            label9.Click += label9_Click;
            // 
            // comboBox4
            // 
            comboBox4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox4.DropDownHeight = 130;
            comboBox4.FormattingEnabled = true;
            comboBox4.IntegralHeight = false;
            comboBox4.Location = new Point(528, 14);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(169, 23);
            comboBox4.TabIndex = 29;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // comboBox3
            // 
            comboBox3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox3.DropDownHeight = 130;
            comboBox3.FormattingEnabled = true;
            comboBox3.IntegralHeight = false;
            comboBox3.Location = new Point(178, 65);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(169, 23);
            comboBox3.TabIndex = 26;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.Location = new Point(37, 61);
            label5.MaximumSize = new Size(100, 30);
            label5.MinimumSize = new Size(100, 30);
            label5.Name = "label5";
            label5.Size = new Size(100, 30);
            label5.TabIndex = 16;
            label5.Text = "Plant";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            label5.Click += label5_Click;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.None;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = SystemColors.ActiveCaptionText;
            label10.Location = new Point(387, 61);
            label10.MaximumSize = new Size(100, 30);
            label10.MinimumSize = new Size(100, 30);
            label10.Name = "label10";
            label10.Size = new Size(100, 30);
            label10.TabIndex = 28;
            label10.Text = "Line";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            label10.Click += label10_Click;
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox2.DropDownHeight = 130;
            comboBox2.FormattingEnabled = true;
            comboBox2.IntegralHeight = false;
            comboBox2.Location = new Point(528, 65);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(169, 23);
            comboBox2.TabIndex = 25;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(763, 16);
            label1.Name = "label1";
            label1.Size = new Size(48, 19);
            label1.TabIndex = 30;
            label1.Text = "Week";
            label1.Click += label1_Click;
            // 
            // dateTimePicker5
            // 
            dateTimePicker5.Anchor = AnchorStyles.None;
            dateTimePicker5.Location = new Point(886, 10);
            dateTimePicker5.MaximumSize = new Size(200, 30);
            dateTimePicker5.MinimumSize = new Size(100, 30);
            dateTimePicker5.Name = "dateTimePicker5";
            dateTimePicker5.Size = new Size(152, 30);
            dateTimePicker5.TabIndex = 4;
            dateTimePicker5.ValueChanged += dateTimePicker5_ValueChanged;
            // 
            // dateTimePicker6
            // 
            dateTimePicker6.Anchor = AnchorStyles.None;
            dateTimePicker6.Location = new Point(1061, 10);
            dateTimePicker6.MaximumSize = new Size(200, 30);
            dateTimePicker6.MinimumSize = new Size(100, 30);
            dateTimePicker6.Name = "dateTimePicker6";
            dateTimePicker6.Size = new Size(152, 30);
            dateTimePicker6.TabIndex = 1;
            dateTimePicker6.ValueChanged += dateTimePicker6_ValueChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(737, 61);
            label4.MaximumSize = new Size(100, 30);
            label4.MinimumSize = new Size(100, 30);
            label4.Name = "label4";
            label4.Size = new Size(100, 30);
            label4.TabIndex = 13;
            label4.Text = "Bulk SO";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Click += label4_Click;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Location = new Point(878, 61);
            textBox2.MaximumSize = new Size(200, 50);
            textBox2.MinimumSize = new Size(100, 30);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(169, 30);
            textBox2.TabIndex = 15;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(542, 116);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(140, 23);
            textBox1.TabIndex = 31;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.None;
            button5.AutoSize = true;
            button5.BackColor = Color.MediumOrchid;
            button5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.ForeColor = Color.White;
            button5.Location = new Point(1436, 8);
            button5.Name = "button5";
            button5.Size = new Size(103, 34);
            button5.TabIndex = 9;
            button5.Text = "Export Excel";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.None;
            button3.BackColor = Color.ForestGreen;
            button3.Cursor = Cursors.Hand;
            button3.Font = new Font("Verdana", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Location = new Point(1264, 57);
            button3.Name = "button3";
            button3.Size = new Size(97, 38);
            button3.TabIndex = 5;
            button3.Text = "Search";
            button3.UseMnemonic = false;
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Location = new Point(1436, 61);
            button1.Name = "button1";
            button1.Size = new Size(103, 30);
            button1.TabIndex = 32;
            button1.Text = "Clear";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AccessibleDescription = "";
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.BackColor = Color.Purple;
            label2.Font = new Font("Lucida Bright", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(3, 0);
            label2.MaximumSize = new Size(2000, 50);
            label2.MinimumSize = new Size(1000, 50);
            label2.Name = "label2";
            label2.Padding = new Padding(10);
            label2.Size = new Size(1578, 50);
            label2.TabIndex = 10;
            label2.Text = "Size Wise Planning Schedule Reports";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += label2_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.PaleGreen;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = Color.SeaGreen;
            dataGridView1.Location = new Point(3, 266);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Size = new Size(1572, 533);
            dataGridView1.TabIndex = 9;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // PlanningSchdule_Reports
            // 
            ClientSize = new Size(1604, 826);
            Controls.Add(PlanningScheduleTab);
            Name = "PlanningSchdule_Reports";
            tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            PlanningScheduleTab.ResumeLayout(false);
            Planning_Schedule.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl PlanningSchedule;
        private System.Windows.Forms.TabPage Planning_Schdule;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label week;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button SaveOrUpdate;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINE;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEEKVALUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sales_Order;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ART_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHOE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRD;
        private System.Windows.Forms.DataGridViewTextBoxColumn LPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSDD;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MOLD_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLASS_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESTINATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARKS1;
        private System.Windows.Forms.DataGridViewTextBoxColumn REMARKS2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateStatus;
        private System.Windows.Forms.TabControl PlanningScheduleTab;
        private System.Windows.Forms.TabPage Planning_Schedule;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dateTimePicker5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

