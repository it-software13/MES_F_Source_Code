using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SJeMES_ZL_KanBan
{
    public partial class FrmOrder : Form
    {

        public static string BaseUrl = string.Empty;
        public FrmOrder()
        {
            InitializeComponent();
            BaseUrl = Common.ConfigHelper.GetConfigUrl();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void TabSelected(string TabName)
        {

            switch (TabName)
            {
                case "PO信息列表":
                    string Url1 = BaseUrl+"/poInformation?en";
                    ChromiumWebBrowser ChromB1 = new ChromiumWebBrowser(Url1);
                    ChromB1.Dock = DockStyle.Fill;
                    pal_po.Controls.Add(ChromB1);
                    break;
                case "AQL验货":
                    //string Url2 = BaseUrl + "/";
                    string Url2 = BaseUrl + "/";//未开发【第二批计划才开始】
                    break;
                case "A01信息":
                    string Url3 = BaseUrl+ "/a01Information?en";
                    ChromiumWebBrowser ChromB3 = new ChromiumWebBrowser(Url3);
                    ChromB3.Dock = DockStyle.Fill;
                    pal_a01.Controls.Add(ChromB3);
                    break;
                case "合规性信息":
                    string Url4 = BaseUrl+"/visualInspection?en";
                    break;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TabName = tabControl1.SelectedTab.Name;
            TabSelected(TabName);
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            TabSelected("PO信息列表");
        }
    }
}
