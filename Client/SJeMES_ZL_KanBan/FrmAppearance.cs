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
    public partial class FrmAppearance : Form
    {

        public static string BaseUrl = string.Empty;
        public FrmAppearance()
        {
            InitializeComponent();
            BaseUrl = Common.ConfigHelper.GetConfigUrl();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
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
                case "外观检验":
                    string Url2 = BaseUrl+"/visualInspection?en";
                    ChromiumWebBrowser ChromB2 = new ChromiumWebBrowser(Url2);
                    ChromB2.Dock = DockStyle.Fill;
                    pal_wgTest.Controls.Add(ChromB2);
                    break;
                case "实验室测试":
                    string Url3 = BaseUrl+"/laboratoryTests?en";
                    ChromiumWebBrowser ChromB3 = new ChromiumWebBrowser(Url3);
                    ChromB3.Dock = DockStyle.Fill;
                    pal_test.Controls.Add(ChromB3);
                    break;
            }
        }

        private void FrmAppearance_Load(object sender, EventArgs e)
        {
            TabSelected("外观检验");
        }
    }
}
