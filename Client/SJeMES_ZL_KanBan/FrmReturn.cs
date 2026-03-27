using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_ZL_KanBan
{
    public partial class FrmReturn : Form
    {
        public static string BaseUrl = string.Empty;
        public FrmReturn()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            BaseUrl = Common.ConfigHelper.GetConfigUrl();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TabName = tabControl1.SelectedTab.Name;
            TabSelected(TabName);
        }
        private void TabSelected(string TabName)
        {
            switch (TabName)
            {
                case "中国市场退货分析":
                    string Url1 = BaseUrl + "/chinaReturn?en";
                    ChromiumWebBrowser ChromB1 = new ChromiumWebBrowser(Url1);
                    ChromB1.Dock = DockStyle.Fill;
                    pal_list.Controls.Add(ChromB1);
                    break;
                case "投诉分析":
                    string Url2 = BaseUrl + "/complaintsAnalysis?en";
                    ChromiumWebBrowser ChromB2 = new ChromiumWebBrowser(Url2);
                    ChromB2.Dock = DockStyle.Fill;
                    pal_wgTest.Controls.Add(ChromB2);
                    break;

            }
        }

        private void FrmReturn_Load(object sender, EventArgs e)
        {
            TabSelected("中国市场退货分析");
        }
    }
}
