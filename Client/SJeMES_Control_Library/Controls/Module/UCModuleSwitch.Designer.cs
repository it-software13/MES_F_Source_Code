namespace SJeMES_Control_Library.Controls
{
    partial class UCModuleSwitch
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
            this.lab_Err = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lab_Title = new System.Windows.Forms.Label();
            this.ucSwitch1 = new SJeMES_Control_Library.Controls.UCSwitch();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
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
            this.panel3.Controls.Add(this.ucSwitch1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(175, 35);
            this.panel3.TabIndex = 0;
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
            // ucSwitch1
            // 
            this.ucSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.ucSwitch1.Checked = false;
            this.ucSwitch1.FalseColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.ucSwitch1.FalseTextColr = System.Drawing.Color.White;
            this.ucSwitch1.Location = new System.Drawing.Point(6, 3);
            this.ucSwitch1.Name = "ucSwitch1";
            this.ucSwitch1.Size = new System.Drawing.Size(83, 26);
            this.ucSwitch1.SwitchType = SJeMES_Control_Library.Controls.SwitchType.Ellipse;
            this.ucSwitch1.TabIndex = 0;
            this.ucSwitch1.Texts = null;
            this.ucSwitch1.TrueColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucSwitch1.TrueTextColr = System.Drawing.Color.White;
            this.ucSwitch1.CheckedChanged += new System.EventHandler(this.ucSwitch1_CheckedChanged);
            // 
            // UCModuleSwitch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lab_Err);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "UCModuleSwitch";
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
        private System.Windows.Forms.Label lab_Title;
        private System.Windows.Forms.Label lab_Err;
        private UCSwitch ucSwitch1;
    }
}
