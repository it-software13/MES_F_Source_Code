using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
namespace SJeMES_BDM
{
    public partial class FIle_Look : Form
    {
        public FIle_Look()
        {
            InitializeComponent();
            string url = "http://localhost:60627/File/ART_CustomQuality_File/20211022142053112.xlsx";
            ChromiumWebBrowser webview = new ChromiumWebBrowser(url);
            webview.Dock = DockStyle.Fill;
            this.Controls.Add(webview);
        }
    }
}
