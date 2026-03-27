
namespace SJeMES_QA.UControl
{
    partial class UCImgSet
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
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_select = new System.Windows.Forms.Button();
            this.pb_img = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_img)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(687, 15);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(92, 35);
            this.btn_del.TabIndex = 5;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(687, 61);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(92, 35);
            this.btn_select.TabIndex = 6;
            this.btn_select.Text = "选做封面";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // pb_img
            // 
            this.pb_img.Location = new System.Drawing.Point(16, 15);
            this.pb_img.Name = "pb_img";
            this.pb_img.Size = new System.Drawing.Size(650, 470);
            this.pb_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_img.TabIndex = 4;
            this.pb_img.TabStop = false;
            // 
            // UCImgSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.pb_img);
            this.Name = "UCImgSet";
            this.Size = new System.Drawing.Size(800, 500);
            ((System.ComponentModel.ISupportInitialize)(this.pb_img)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pb_img;
        public System.Windows.Forms.Button btn_del;
        public System.Windows.Forms.Button btn_select;
    }
}
