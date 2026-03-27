
namespace SJeMES_Control_Library.Controls
{
    partial class PageControl
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
            this.linkFirst = new System.Windows.Forms.LinkLabel();
            this.linkPrev = new System.Windows.Forms.LinkLabel();
            this.linkNext = new System.Windows.Forms.LinkLabel();
            this.linkLast = new System.Windows.Forms.LinkLabel();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.linkGo = new System.Windows.Forms.LinkLabel();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.cb_size = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // linkFirst
            // 
            this.linkFirst.AutoSize = true;
            this.linkFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkFirst.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkFirst.LinkColor = System.Drawing.Color.Black;
            this.linkFirst.Location = new System.Drawing.Point(6, 17);
            this.linkFirst.Name = "linkFirst";
            this.linkFirst.Size = new System.Drawing.Size(42, 21);
            this.linkFirst.TabIndex = 0;
            this.linkFirst.TabStop = true;
            this.linkFirst.Text = "首页";
            this.linkFirst.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFirst_LinkClicked);
            this.linkFirst.MouseLeave += new System.EventHandler(this.linkFirst_MouseLeave);
            this.linkFirst.MouseMove += new System.Windows.Forms.MouseEventHandler(this.linkFirst_MouseMove);
            // 
            // linkPrev
            // 
            this.linkPrev.AutoSize = true;
            this.linkPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkPrev.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkPrev.LinkColor = System.Drawing.Color.Black;
            this.linkPrev.Location = new System.Drawing.Point(54, 17);
            this.linkPrev.Name = "linkPrev";
            this.linkPrev.Size = new System.Drawing.Size(58, 21);
            this.linkPrev.TabIndex = 1;
            this.linkPrev.TabStop = true;
            this.linkPrev.Text = "上一页";
            this.linkPrev.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPrev_LinkClicked);
            this.linkPrev.MouseLeave += new System.EventHandler(this.linkPrev_MouseLeave);
            this.linkPrev.MouseMove += new System.Windows.Forms.MouseEventHandler(this.linkPrev_MouseMove);
            // 
            // linkNext
            // 
            this.linkNext.AutoSize = true;
            this.linkNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkNext.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkNext.LinkColor = System.Drawing.Color.Black;
            this.linkNext.Location = new System.Drawing.Point(118, 17);
            this.linkNext.Name = "linkNext";
            this.linkNext.Size = new System.Drawing.Size(58, 21);
            this.linkNext.TabIndex = 2;
            this.linkNext.TabStop = true;
            this.linkNext.Text = "下一页";
            this.linkNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkNext_LinkClicked);
            this.linkNext.MouseLeave += new System.EventHandler(this.linkNext_MouseLeave);
            this.linkNext.MouseMove += new System.Windows.Forms.MouseEventHandler(this.linkNext_MouseMove);
            // 
            // linkLast
            // 
            this.linkLast.AutoSize = true;
            this.linkLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLast.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLast.LinkColor = System.Drawing.Color.Black;
            this.linkLast.Location = new System.Drawing.Point(195, 17);
            this.linkLast.Name = "linkLast";
            this.linkLast.Size = new System.Drawing.Size(42, 21);
            this.linkLast.TabIndex = 3;
            this.linkLast.TabStop = true;
            this.linkLast.Text = "末页";
            this.linkLast.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLast_LinkClicked);
            this.linkLast.MouseLeave += new System.EventHandler(this.linkLast_MouseLeave);
            this.linkLast.MouseMove += new System.Windows.Forms.MouseEventHandler(this.linkLast_MouseMove);
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrentPage.Location = new System.Drawing.Point(388, 13);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(72, 29);
            this.txtCurrentPage.TabIndex = 4;
            this.txtCurrentPage.Text = "1";
            this.txtCurrentPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentPage_KeyPress);
            // 
            // linkGo
            // 
            this.linkGo.AutoSize = true;
            this.linkGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkGo.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkGo.LinkColor = System.Drawing.Color.Black;
            this.linkGo.Location = new System.Drawing.Point(475, 17);
            this.linkGo.Name = "linkGo";
            this.linkGo.Size = new System.Drawing.Size(42, 21);
            this.linkGo.TabIndex = 5;
            this.linkGo.TabStop = true;
            this.linkGo.Text = "转到";
            this.linkGo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGo_LinkClicked);
            this.linkGo.MouseLeave += new System.EventHandler(this.linkGo_MouseLeave);
            this.linkGo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.linkGo_MouseMove);
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalPage.Location = new System.Drawing.Point(523, 17);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(61, 21);
            this.lblTotalPage.TabIndex = 6;
            this.lblTotalPage.Text = "共 1 页";
            // 
            // cb_size
            // 
            this.cb_size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_size.FormattingEnabled = true;
            this.cb_size.Items.AddRange(new object[] {
            "15",
            "30",
            "50",
            "100",
            "500",
            "1000",
            "3000",
            "5000",
            "10000"});
            this.cb_size.Location = new System.Drawing.Point(244, 17);
            this.cb_size.Name = "cb_size";
            this.cb_size.Size = new System.Drawing.Size(121, 21);
            this.cb_size.TabIndex = 7;
            this.cb_size.SelectedIndexChanged += new System.EventHandler(this.cb_size_SelectedIndexChanged);
            // 
            // PageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_size);
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.linkGo);
            this.Controls.Add(this.txtCurrentPage);
            this.Controls.Add(this.linkLast);
            this.Controls.Add(this.linkNext);
            this.Controls.Add(this.linkPrev);
            this.Controls.Add(this.linkFirst);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "PageControl";
            this.Size = new System.Drawing.Size(715, 56);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkFirst;
        private System.Windows.Forms.LinkLabel linkPrev;
        private System.Windows.Forms.LinkLabel linkNext;
        private System.Windows.Forms.LinkLabel linkLast;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.LinkLabel linkGo;
        private System.Windows.Forms.Label lblTotalPage;
        public System.Windows.Forms.ComboBox cb_size;
    }
}