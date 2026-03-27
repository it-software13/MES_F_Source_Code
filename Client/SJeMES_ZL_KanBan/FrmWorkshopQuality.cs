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
    public partial class FrmWorkshopQuality : Form
    {
        public static string BaseUrl = string.Empty;
        public FrmWorkshopQuality()
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
                case "RQC抽验":
                    string Url1 = BaseUrl + "/rqcExtractionTest?en";
                    ChromiumWebBrowser ChromB1 = new ChromiumWebBrowser(Url1);
                    ChromB1.Dock = DockStyle.Fill;
                    pal_list.Controls.Add(ChromB1);
                    break;
                case "TQC抽验":
                    string Url2 = BaseUrl + "/tqcExtractionTest?en";
                    ChromiumWebBrowser ChromB2 = new ChromiumWebBrowser(Url2);
                    ChromB2.Dock = DockStyle.Fill;
                    pal_wgTest.Controls.Add(ChromB2);
                    break; 
                case "温湿度看板":
                    string Url3 = BaseUrl + "/temperatureHumidity?en";
                    ChromiumWebBrowser ChromB3 = new ChromiumWebBrowser(Url3);
                    ChromB3.Dock = DockStyle.Fill;
                    pal_temp_hum.Controls.Add(ChromB3);
                    break;
                case "金属管控":
                    string Url4 = BaseUrl + "/metalManagement?en";
                    ChromiumWebBrowser ChromB4 = new ChromiumWebBrowser(Url4);
                    ChromB4.Dock = DockStyle.Fill;
                    jspanel.Controls.Add(ChromB4);
                    break;
                case "设备参数":
                    string Url5 = BaseUrl + "/equipmentParameters?en";
                    ChromiumWebBrowser ChromB5 = new ChromiumWebBrowser(Url5);
                    ChromB5.Dock = DockStyle.Fill;
                    panel1.Controls.Add(ChromB5);
                    break;

            }
        }

        private void FrmWorkshopQuality_Load(object sender, EventArgs e)
        {
            TabSelected("RQC抽验");
        }
    }
}
