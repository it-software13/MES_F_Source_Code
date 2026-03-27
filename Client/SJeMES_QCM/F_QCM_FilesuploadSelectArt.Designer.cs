
namespace SJeMES_QCM
{
    partial class F_QCM_FilesuploadSelectArt
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
            this.btn_search = new System.Windows.Forms.Button();
            this.tb_search = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewEx2 = new SJeMES_Control_Library.DataGridViewEx();
            this.鞋型R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARTR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewEx1 = new SJeMES_Control_Library.DataGridViewEx();
            this.Column1 = new SJeMES_Control_Library.DataGridViewCheckBoxColumnEx();
            this.行号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.鞋型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(491, 73);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 25);
            this.btn_search.TabIndex = 10;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // tb_search
            // 
            this.tb_search.Location = new System.Drawing.Point(353, 75);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(132, 20);
            this.tb_search.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(602, 532);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 8;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(437, 532);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 7;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewEx2
            // 
            this.dataGridViewEx2.AllowUserToAddRows = false;
            this.dataGridViewEx2.AllowUserToResizeColumns = false;
            this.dataGridViewEx2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEx2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.鞋型R,
            this.ARTR});
            this.dataGridViewEx2.Location = new System.Drawing.Point(572, 104);
            this.dataGridViewEx2.Name = "dataGridViewEx2";
            this.dataGridViewEx2.RowHeadersVisible = false;
            this.dataGridViewEx2.RowTemplate.Height = 23;
            this.dataGridViewEx2.Size = new System.Drawing.Size(565, 397);
            this.dataGridViewEx2.TabIndex = 11;
            // 
            // 鞋型R
            // 
            this.鞋型R.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.鞋型R.HeaderText = "鞋型";
            this.鞋型R.Name = "鞋型R";
            // 
            // ARTR
            // 
            this.ARTR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ARTR.HeaderText = "ART";
            this.ARTR.Name = "ARTR";
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.AllowUserToResizeColumns = false;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.行号,
            this.鞋型,
            this.ART});
            this.dataGridViewEx1.Location = new System.Drawing.Point(1, 104);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowHeadersVisible = false;
            this.dataGridViewEx1.RowTemplate.Height = 23;
            this.dataGridViewEx1.Size = new System.Drawing.Size(565, 397);
            this.dataGridViewEx1.TabIndex = 6;
            this.dataGridViewEx1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellContentClick);
            this.dataGridViewEx1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEx1_ColumnHeaderMouseClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 30;
            // 
            // 行号
            // 
            this.行号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.行号.HeaderText = "行号";
            this.行号.Name = "行号";
            // 
            // 鞋型
            // 
            this.鞋型.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.鞋型.HeaderText = "鞋型";
            this.鞋型.Name = "鞋型";
            this.鞋型.Width = 150;
            // 
            // ART
            // 
            this.ART.HeaderText = "ART";
            this.ART.Name = "ART";
            this.ART.Width = 150;
            // 
            // F_QCM_FilesuploadSelectArt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 582);
            this.Controls.Add(this.dataGridViewEx2);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.tb_search);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewEx1);
            this.Name = "F_QCM_FilesuploadSelectArt";
            this.Text = "选择数据";
            this.Load += new System.EventHandler(this.F_QCM_FilesuploadSelectArt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SJeMES_Control_Library.DataGridViewEx dataGridViewEx2;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox tb_search;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private SJeMES_Control_Library.DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 鞋型R;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARTR;
        private SJeMES_Control_Library.DataGridViewCheckBoxColumnEx Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 行号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 鞋型;
        private System.Windows.Forms.DataGridViewTextBoxColumn ART;
    }
}