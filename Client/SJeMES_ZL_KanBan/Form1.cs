using CefSharp;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string url1 = $@"https://www.baidu.com/";
            ChromiumWebBrowser webview1 = new ChromiumWebBrowser(url1);
            webview1.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(webview1);

            string url2 = $@"https://www.baidu.com/";
            ChromiumWebBrowser webview2 = new ChromiumWebBrowser(url2);
            webview2.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(webview2);
        }

    }
}
