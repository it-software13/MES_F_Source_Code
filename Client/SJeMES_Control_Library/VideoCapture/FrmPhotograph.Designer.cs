namespace SJeMES_Control_Library.VideoCapture
{
    partial class FrmPhotograph
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cboVideo = new System.Windows.Forms.ComboBox();
            this.cboResolution = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnPic = new System.Windows.Forms.Button();
            this.vispShoot = new AForge.Controls.VideoSourcePlayer();
            this.picbPreview = new AForge.Controls.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_commit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // cboVideo
            // 
            this.cboVideo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVideo.FormattingEnabled = true;
            this.cboVideo.Location = new System.Drawing.Point(70, 17);
            this.cboVideo.Name = "cboVideo";
            this.cboVideo.Size = new System.Drawing.Size(130, 21);
            this.cboVideo.TabIndex = 0;
            this.cboVideo.SelectedIndexChanged += new System.EventHandler(this.cboVideo_SelectedIndexChanged);
            // 
            // cboResolution
            // 
            this.cboResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResolution.FormattingEnabled = true;
            this.cboResolution.Location = new System.Drawing.Point(249, 17);
            this.cboResolution.Name = "cboResolution";
            this.cboResolution.Size = new System.Drawing.Size(119, 21);
            this.cboResolution.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(374, 14);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(58, 25);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCut
            // 
            this.btnCut.Enabled = false;
            this.btnCut.Location = new System.Drawing.Point(438, 13);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(71, 25);
            this.btnCut.TabIndex = 2;
            this.btnCut.Text = "disconnect";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnPic
            // 
            this.btnPic.Enabled = false;
            this.btnPic.Location = new System.Drawing.Point(518, 13);
            this.btnPic.Name = "btnPic";
            this.btnPic.Size = new System.Drawing.Size(69, 25);
            this.btnPic.TabIndex = 2;
            this.btnPic.Text = "Photograph";
            this.btnPic.UseVisualStyleBackColor = true;
            this.btnPic.Click += new System.EventHandler(this.btnPic_Click);
            // 
            // vispShoot
            // 
            this.vispShoot.Location = new System.Drawing.Point(12, 44);
            this.vispShoot.Name = "vispShoot";
            this.vispShoot.Size = new System.Drawing.Size(605, 359);
            this.vispShoot.TabIndex = 3;
            this.vispShoot.Text = "videoSourcePlayer1";
            this.vispShoot.VideoSource = null;
            // 
            // picbPreview
            // 
            this.picbPreview.Image = null;
            this.picbPreview.Location = new System.Drawing.Point(475, 410);
            this.picbPreview.Name = "picbPreview";
            this.picbPreview.Size = new System.Drawing.Size(142, 82);
            this.picbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbPreview.TabIndex = 4;
            this.picbPreview.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Camera:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Pixels:";
            // 
            // btn_commit
            // 
            this.btn_commit.Location = new System.Drawing.Point(593, 12);
            this.btn_commit.Name = "btn_commit";
            this.btn_commit.Size = new System.Drawing.Size(45, 26);
            this.btn_commit.TabIndex = 6;
            this.btn_commit.Text = "submit";
            this.btn_commit.UseVisualStyleBackColor = true;
            this.btn_commit.Click += new System.EventHandler(this.btn_commit_Click);
            // 
            // FrmPhotograph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 493);
            this.Controls.Add(this.btn_commit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picbPreview);
            this.Controls.Add(this.vispShoot);
            this.Controls.Add(this.btnPic);
            this.Controls.Add(this.btnCut);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cboResolution);
            this.Controls.Add(this.cboVideo);
            this.Name = "FrmPhotograph";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Camera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboVideo;
        private System.Windows.Forms.ComboBox cboResolution;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.Button btnPic;
        private AForge.Controls.VideoSourcePlayer vispShoot;
        private AForge.Controls.PictureBox picbPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_commit;
    }
}

