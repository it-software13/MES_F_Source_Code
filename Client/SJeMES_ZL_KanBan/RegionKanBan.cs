using CefSharp.WinForms;
using MaterialSkin;
using SJeMES_QA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SJeMES_ZL_KanBan
{
    public partial class RegionKanBan : Form
    {

        private delegate void CloseDelegate(FrmLoad load);
        private static string BaseUrl = string.Empty;//前端页面地址

        public static FrmLoad GetFrmLoad=null;//遮盖层窗体
        public static Thread GetThread =null;//遮盖层渲染线程

        /// <summary>
        /// 关闭遮盖层
        /// </summary>
        /// <param name="frmload"></param>
        public void Closeds(FrmLoad frmload)
        {
            if (frmload.InvokeRequired)
            {
                frmload.Invoke(new CloseDelegate(Closeds), frmload);
            }
            else
            {
                frmload.Close();
            }
        }

        //加载遮盖层
        public Thread FrmLoading(FrmLoad frm)
        {
            System.Threading.Thread th;
            th = new Thread(new ThreadStart(delegate ()
            {
                frm.ShowDialog();
            }));
            th.Start();
            return th;
        }


        public RegionKanBan()
        {
            //异步Loading层
            FrmLoad Frm = new FrmLoad(this.Width, this.Height);
            GetFrmLoad = Frm;
            Thread th = FrmLoading(Frm);
            GetThread = th;
            //使用反射赋值QA的Progarm.client对象
            //解决点击DQA,MQA跳转报错
            SetProgramClent();

            //获取前端页面站点地址
            BaseUrl = Common.ConfigHelper.GetConfigUrl();
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        /// <summary>
        /// 自动分配当前屏幕各块占比
        /// </summary>
        /// <param name="Frm"></param>
        public void ChangeSize(RegionKanBan Frm)
        {
            int FrmHight = this.Height;
            int FrmWidth = this.Width;
            Frm.pal_top.Height = Convert.ToInt32(FrmHight * 0.1);
            //Frm.pal_content.Height = Convert.ToInt32(FrmHight * 0.88);

            int ContontWidth = Convert.ToInt32(FrmWidth * 0.28);
            Frm.pal_left.Width = ContontWidth;
            Frm.pal_right.Width = ContontWidth;


            int ThreeWidth = Convert.ToInt32(pal_left.Height * 0.33);
            Frm.pal_left_1.Height = ThreeWidth;
            Frm.pal_left_2.Height = ThreeWidth;
            Frm.pal_left_3.Height = ThreeWidth;
            Frm.pal_right_1.Height = ThreeWidth;
            Frm.pal_right_2.Height = ThreeWidth;
            Frm.pal_right_3.Height = ThreeWidth;


            int CenterHight = Convert.ToInt32(this.pal_center.Height * 0.4);
            int CenterWidth = Convert.ToInt32(this.pal_center.Width * 0.5);
            center_a.Width = CenterWidth;
            pal_center_top.Height = CenterHight;
        }

        /// <summary>
        /// 嵌入各模块web网页
        /// </summary>
        /// <param name="region"></param>
        public static void BandPanelData(RegionKanBan region)
        {
            string rightAUrl = $@"{BaseUrl}/mainQualityIndicators?en";
            ChromiumWebBrowser webview_rigth_A = new ChromiumWebBrowser(rightAUrl);
            webview_rigth_A.Dock = DockStyle.Fill;
            region.left_a.Controls.Add(webview_rigth_A);

            string rightBUrl = $@"{BaseUrl}/testPart?en";
            ChromiumWebBrowser webview_rigth_B = new ChromiumWebBrowser(rightBUrl);
            webview_rigth_B.Dock = DockStyle.Fill;
            region.left_b.Controls.Add(webview_rigth_B);

            string rightCUrl = $@"{BaseUrl}/marketFeedback?en";
            ChromiumWebBrowser webview_rigth_C = new ChromiumWebBrowser(rightCUrl);
            webview_rigth_C.Dock = DockStyle.Fill;
            region.left_c.Controls.Add(webview_rigth_C);


            string firstAUrl = $@"{BaseUrl}/dqa?en";
            ChromiumWebBrowser webview_first_A = new ChromiumWebBrowser(firstAUrl);
            webview_first_A.Dock = DockStyle.Fill;
            region.ct_a.Controls.Add(webview_first_A);

            string firstBUrl = $@"{BaseUrl}/mqa?en";
            ChromiumWebBrowser webview_first_B = new ChromiumWebBrowser(firstBUrl);
            webview_first_B.Dock = DockStyle.Fill;
            region.ct_b.Controls.Add(webview_first_B);

            string lastUrl = $@"{BaseUrl}/qualityAbnormal?en";
            ChromiumWebBrowser webview_last = new ChromiumWebBrowser(lastUrl);
            webview_last.Dock = DockStyle.Fill;
            region.cl_a.Controls.Add(webview_last);


            string leftAUrl = $@"{BaseUrl}/forepartQ?en";
            ChromiumWebBrowser webview_left_A = new ChromiumWebBrowser(leftAUrl);
            webview_left_A.Dock = DockStyle.Fill;
            region.right_a.Controls.Add(webview_left_A);

            string leftBUrl = $@"{BaseUrl}/workshopQ?en";
            ChromiumWebBrowser webview_left_B = new ChromiumWebBrowser(leftBUrl);
            webview_left_B.Dock = DockStyle.Fill;
            region.right_b.Controls.Add(webview_left_B);

            string leftCUrl = $@"{BaseUrl}/aqLInspection?en";
            ChromiumWebBrowser webview_left_C = new ChromiumWebBrowser(leftCUrl);
            webview_left_C.Dock = DockStyle.Fill;
            region.rigth_c.Controls.Add(webview_left_C);
        }
        private void RegionKanBan_Load(object sender, EventArgs e)
        {
            ChangeSize(this);
            //绑定数据
            BandPanelData(this);

            //关闭LOADING遮盖层
            Closeds(GetFrmLoad);
            GetThread.Abort();
        }

        //按钮点击事件
        private void btn_Click(object sender, EventArgs e)
        {
            List<Control> buttonList = new List<Control>();
            foreach (Control con in this.pal_top.Controls)
            {
                //判断控件类型是否为按钮
                if (con is Button)
                {
                    Button fbtn = con as Button;
                    fbtn.BackColor = System.Drawing.Color.SteelBlue;
                }
            }
            Button NewBtn = sender as Button;
            NewBtn.BackColor = Color.DeepSkyBlue;
            //lbl_title.Text = $@"万邦{NewBtn.Text}品质指挥中心";
            lbl_title.Text = $@"{NewBtn.Text} Quality Command Center";
        }

        /// <summary>
        /// 看板关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 各模块点击跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LalClick(object sender, EventArgs e)
        {
            Label lbl_title = (Label)sender;
            Form Frm = null;

            switch (lbl_title.Name.ToString())
            {
                //测试部点击
                case "lal_test":
                    Frm = new FrmTestDepartment();
                    break;
                //市场反馈点击
                case "lal_scfk":
                    Frm = new FrmReturn();
                    break;
                //DQA点击
                case "lal_dqa":
                    Frm = new F_DQA_ShoeShape_Main();
                    break;
                //MQA点击
                case "lal_mqa":
                    Frm = new F_MQA_ShoeShape_Main();
                    break;
                //前段Q点击
                case "lal_fort":
                    Frm = new FrmAppearance();
                    break;
                //车间Q点击
                case "lal_work":
                    Frm = new FrmWorkshopQuality();
                    break;
                //AQL点击
                case "lal_aql":
                    Frm = new FrmOrder();
                    break;
            }
            if (Frm != null)
            {
                //this.Close();//先关闭总看板后再跳转至其他看板
                Frm.StartPosition = FormStartPosition.CenterScreen;
                Frm.ShowDialog();
            }
        }

        public void SetProgramClent()
        {
            Assembly assembly = null;
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            if (!File.Exists(path + @"\SJeMES_QA.dll"))
            {
                MessageBox.Show("SJeMES_QA.dll file not found");
                return;
            }
            assembly = Assembly.LoadFrom(path + @"\SJeMES_QA.dll");
            Type type = assembly.GetType("SJeMES_QA.Interface");
            object instance = null;
            instance = Activator.CreateInstance(type);
            MethodInfo mi = type.GetMethod("RunApp");
            object[] args = new object[1];
            Program.Client.FormName = "获取client";
            args[0] = Program.Client;
            object obj = mi.Invoke(instance, args);
        }

        private void lbl_title_Click(object sender, EventArgs e)
        {

        }
    }
}
