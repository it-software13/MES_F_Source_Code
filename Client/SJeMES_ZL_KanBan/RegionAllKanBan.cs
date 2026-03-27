using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_ZL_KanBan
{
    public partial class RegionAllKanBan : Form
    {
        //看板默认查询国家
        public static string Country = "中国区";
        //看板查询数据时间区间
        public static string DateStart;//开始时间
        public static string DateEnd;//截止时间
        public static FrmLoad FrmLoad;

        public RegionAllKanBan()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        private void RegionAllKanBan_Load(object sender, EventArgs e)
        {
            UpdateSize(this);
            BandPanelData(this);
        }
        /// <summary>
        ///动态修改容器大小
        /// </summary>
        public static void UpdateSize(RegionAllKanBan region)
        {
            #region 动态修改左中右容器宽度
            region.pnl_right.Width = Convert.ToInt32(region.pnl_content.Width * 0.3);
            region.pnl_left.Width = Convert.ToInt32(region.pnl_content.Width * 0.3);

            //region.pnl_content.Width = Convert.ToInt32(region.pnl_content.Width * 0.4);
            #endregion
            #region 修改左右容器内部容器高度

            int RightHight = Convert.ToInt32(region.pnl_right.Height * 0.33);
            region.pnl_rigth_A.Height = RightHight;
            region.pnl_right_B.Height = RightHight;
            region.pnl_right_C.Height = RightHight;


            region.pnl_Center_First.Height = Convert.ToInt32(region.pnl_content.Height * 0.45);
            region.pnl_center_last.Height = Convert.ToInt32(region.pnl_content.Height * 0.55);



            int LeftHeight = Convert.ToInt32(region.pnl_left.Height * 0.33);
            region.pnl_left_A.Height = LeftHeight;
            region.pnl_left_B.Height = LeftHeight;
            region.pnl_left_C.Height = LeftHeight;

            #endregion
            #region 修改中部容器的内部容器高度


            //内部小容器宽度
            region.pnl_center_FisterA.Width = Convert.ToInt32(region.pnl_Center_First.Width * 0.5);
            region.pnl_center_FisterB.Width = Convert.ToInt32(region.pnl_Center_First.Width * 0.5);
            #endregion
            #region 切换按钮位置
            region.pnl_top_A.Width = Convert.ToInt32(region.pnl_top.Width * 0.4);
            region.pnl_top_B.Width = Convert.ToInt32(region.pnl_top.Width * 0.4);
            #endregion
        }
        /// <summary>
        /// 绑定容器数据
        /// </summary>
        /// <param name="region"></param>
        public static void BandPanelData(RegionAllKanBan region)
        {
            string rightAUrl = $@"http://10.2.171.110:8090/#/mainQualityIndicators";
            ChromiumWebBrowser webview_rigth_A = new ChromiumWebBrowser(rightAUrl);
            webview_rigth_A.Dock = DockStyle.Fill;
            region.pnl_rigth_A.Controls.Add(webview_rigth_A);
            
            string rightBUrl = $@"http://10.2.171.110:8090/#/testPart";
            ChromiumWebBrowser webview_rigth_B = new ChromiumWebBrowser(rightBUrl);
            webview_rigth_B.Dock = DockStyle.Fill;
            region.pnl_right_B.Controls.Add(webview_rigth_B);

            string rightCUrl = $@"http://10.2.171.110:8090/#/marketFeedback";
            ChromiumWebBrowser webview_rigth_C = new ChromiumWebBrowser(rightCUrl);
            webview_rigth_C.Dock = DockStyle.Fill;
            region.pnl_right_C.Controls.Add(webview_rigth_C);


            string firstAUrl = $@"http://10.2.171.110:8090/#/dqa";
            ChromiumWebBrowser webview_first_A = new ChromiumWebBrowser(firstAUrl);
            webview_first_A.Dock = DockStyle.Fill;
            region.pnl_center_FisterA.Controls.Add(webview_first_A);

            string firstBUrl = $@"http://10.2.171.110:8090/#/mqa";
            ChromiumWebBrowser webview_first_B = new ChromiumWebBrowser(firstBUrl);
            webview_first_B.Dock = DockStyle.Fill;
            region.pnl_center_FisterB.Controls.Add(webview_first_B);

            string lastUrl = $@"http://10.2.171.110:8090/#/qualityAbnormal";
            ChromiumWebBrowser webview_last = new ChromiumWebBrowser(lastUrl);
            webview_last.Dock = DockStyle.Fill;
            region.pnl_center_last.Controls.Add(webview_last);


            string leftAUrl = $@"http://10.2.171.110:8090/#/forepartQ";
            ChromiumWebBrowser webview_left_A = new ChromiumWebBrowser(leftAUrl);
            webview_left_A.Dock = DockStyle.Fill;
            region.pnl_left_A.Controls.Add(webview_left_A);

            string leftBUrl = $@"http://10.2.171.110:8090/#/workshopQ";
            ChromiumWebBrowser webview_left_B = new ChromiumWebBrowser(leftBUrl);
            webview_left_B.Dock = DockStyle.Fill;
            region.pnl_left_B.Controls.Add(webview_left_B);

            string leftCUrl = $@"http://10.2.171.110:8090/#/aqLInspection";
            ChromiumWebBrowser webview_left_C = new ChromiumWebBrowser(leftCUrl);
            webview_left_C.Dock = DockStyle.Fill;
            region.pnl_left_C.Controls.Add(webview_left_C);
        }
        private void RegionAllKanBan_SizeChanged(object sender, EventArgs e)
        {
            UpdateSize(this);
        }
        /// <summary>
        /// 区域按钮点击样式切换
        /// </summary>
        /// <param name="Btn"></param>
        /// <param name="Region"></param>
        public static void ChangeStyle(Button Btn, RegionAllKanBan Region)
        {
            List<Control> buttonList = new List<Control>();
            foreach (Control con in Region.Controls)
            {
                //判断控件类型是否为按钮
                if (con is Button)
                {
                    buttonList.Add(con);
                }
            }

            //遍历所有List，设定属性
            for (int i = 0; i < buttonList.Count; i++)
            {
                Button fbtn = buttonList[i] as Button;
                fbtn.BackColor = System.Drawing.Color.SteelBlue;
            }
            Btn.BackColor = System.Drawing.Color.DeepSkyBlue;
        }
        /// <summary>
        /// 中国区点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_china_Click(object sender, EventArgs e)
        {
            Country = "中国区";
            lal_title.Text = "万邦" + Country + "品质看板";
            ChangeStyle(this.btn_china, this);
        }
        /// <summary>
        /// 印度区点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_India_Click(object sender, EventArgs e)
        {
            Country = "印度区";
            lal_title.Text = "万邦" + Country + "品质看板";
            ChangeStyle(this.btn_India, this);
        }
        /// <summary>
        /// 越南区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Vietnam_Click(object sender, EventArgs e)
        {
            {
                Country = "越南区";
                lal_title.Text = "万邦" + Country + "品质看板";
                ChangeStyle(this.btn_Vietnam, this);
            }
        }
    }
}
