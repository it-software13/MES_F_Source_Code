namespace SJeMES_Control_Library.Controls
{
    partial class UCModuleTextBox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucTextBoxEx1 = new SJeMES_Control_Library.Controls.UCTextBoxEx();
            this.lab_Title = new System.Windows.Forms.Label();
            this.lab_Err = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lab_Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 35);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(75, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(175, 35);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ucTextBoxEx1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(175, 35);
            this.panel3.TabIndex = 0;
            // 
            // ucTextBoxEx1
            // 
            this.ucTextBoxEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucTextBoxEx1.BackColor = System.Drawing.Color.Transparent;
            this.ucTextBoxEx1.ConerRadius = 1;
            this.ucTextBoxEx1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucTextBoxEx1.DecLength = 2;
            this.ucTextBoxEx1.FillColor = System.Drawing.Color.Empty;
            this.ucTextBoxEx1.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucTextBoxEx1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxEx1.InputText = "";
            this.ucTextBoxEx1.InputType = SJeMES_Control_Library.TextInputType.NotControl;
            this.ucTextBoxEx1.IsFocusColor = true;
            this.ucTextBoxEx1.IsPassword = false;
            this.ucTextBoxEx1.IsRadius = true;
            this.ucTextBoxEx1.IsShowClearBtn = true;
            this.ucTextBoxEx1.IsShowKeyboard = false;
            this.ucTextBoxEx1.IsShowRect = true;
            this.ucTextBoxEx1.IsShowSearchBtn = false;
            this.ucTextBoxEx1.KeyBoardType = SJeMES_Control_Library.Controls.KeyBoardType.UCKeyBorderAll_EN;
            this.ucTextBoxEx1.Location = new System.Drawing.Point(3, 4);
            this.ucTextBoxEx1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucTextBoxEx1.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ucTextBoxEx1.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.ucTextBoxEx1.Name = "ucTextBoxEx1";
            this.ucTextBoxEx1.Padding = new System.Windows.Forms.Padding(5);
            this.ucTextBoxEx1.PromptColor = System.Drawing.Color.Gray;
            this.ucTextBoxEx1.PromptFont = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucTextBoxEx1.PromptText = "";
            this.ucTextBoxEx1.RectColor = System.Drawing.Color.Black;
            this.ucTextBoxEx1.RectWidth = 1;
            this.ucTextBoxEx1.RegexPattern = "";
            this.ucTextBoxEx1.Size = new System.Drawing.Size(170, 25);
            this.ucTextBoxEx1.TabIndex = 0;
            this.ucTextBoxEx1.SearchClick += new System.EventHandler(this.ucTextBoxEx1_SearchClick);
            this.ucTextBoxEx1.TextChanged += new System.EventHandler(this.ucTextBoxEx1_TextChanged);
            // 
            // lab_Title
            // 
            this.lab_Title.Dock = System.Windows.Forms.DockStyle.Left;
            this.lab_Title.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lab_Title.ForeColor = System.Drawing.Color.Black;
            this.lab_Title.Location = new System.Drawing.Point(0, 0);
            this.lab_Title.Name = "lab_Title";
            this.lab_Title.Size = new System.Drawing.Size(75, 35);
            this.lab_Title.TabIndex = 1;
            this.lab_Title.Text = "一二三四";
            this.lab_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_Err
            // 
            this.lab_Err.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_Err.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lab_Err.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lab_Err.Location = new System.Drawing.Point(0, 35);
            this.lab_Err.Name = "lab_Err";
            this.lab_Err.Size = new System.Drawing.Size(250, 20);
            this.lab_Err.TabIndex = 1;
            this.lab_Err.Text = "验证数据出错信息";
            this.lab_Err.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // UCModuleTextBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lab_Err);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "UCModuleTextBox";
            this.Size = new System.Drawing.Size(250, 55);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private UCTextBoxEx ucTextBoxEx1;
        private System.Windows.Forms.Label lab_Title;
        private System.Windows.Forms.Label lab_Err;
    }
}
