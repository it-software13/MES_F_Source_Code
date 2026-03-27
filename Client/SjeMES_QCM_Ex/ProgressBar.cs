using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class ProgressBar : MaterialForm
    {
        int minValue = 0;
        int maxValue = 0;
        public int valueres = 0;
        private readonly MaterialSkinManager materialSkinManager;
        public ProgressBar(int _minValue, int _maxValue)
        {
            InitializeComponent();
            minValue = _minValue;
            maxValue = _maxValue;

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
        }
        /// <summary>
        /// 初始化进度条
        /// </summary>
        /// <param name="progressBar">进度条组件</param>
        /// <param name="minValue">当前进度值</param>
        /// <param name="maxValue">进度显示标签</param>
        private void InitProgressBar(int minValue, int maxValue)
        {
            if (this.progressBar1 == null || minValue < 0 || maxValue < 0 || minValue >= maxValue) return;
            progressBar1.Minimum = minValue;
            progressBar1.Maximum = maxValue;
        }
        /// <summary>
        /// 启动进度条
        /// </summary>
        /// <param name="progressBar">进度条组件</param>
        /// <param name="value">当前进度值</param>
        /// <param name="lable">进度显示标签</param>
        public void StartProgressBar(int value)
        {
            if (progressBar1 == null || txt_num == null) return;
            Application.DoEvents();
            progressBar1.Value = value;
            decimal tmp =Math.Round(Convert.ToDecimal(value) / Convert.ToDecimal(progressBar1.Maximum),2)*100;
            //txt_num.Text = tmp + "%__" + value + "/" + progressBar1.Maximum;
            txt_num.Text = tmp + "%";
            valueres = value;
            txt_num.Refresh();
            progressBar1.Refresh();
        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            InitProgressBar(minValue,maxValue);
        }
    }

}
