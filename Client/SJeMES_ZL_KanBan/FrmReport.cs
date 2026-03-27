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
    public partial class FrmReport : Form
    {
        private string _url = string.Empty;
        public FrmReport(string url)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _url = url;
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            //string url1 = $@"https://www.baidu.com/{idd}";
            ChromiumWebBrowser webview1 = new ChromiumWebBrowser(_url);
            this.WindowState = FormWindowState.Maximized;
            webview1.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(webview1);
        }
    }
}
