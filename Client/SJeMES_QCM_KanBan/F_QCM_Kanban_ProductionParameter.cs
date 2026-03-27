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

namespace SJeMES_QCM_KanBan
{
    public partial class F_QCM_Kanban_ProductionParameter : Form
    {
        string KanbanName = string.Empty;
        string KanbanUrl = string.Empty;

        public F_QCM_Kanban_ProductionParameter(string KanbanName,string KanbanUrl)
        {
            this.KanbanName = KanbanName;
            this.KanbanUrl = KanbanUrl;
            InitializeComponent();
        }

        private void F_QCM_Kanban_ProductionParameter_Load(object sender, EventArgs e)
        {
            this.Text = this.KanbanName;
            ChromiumWebBrowser webview = new ChromiumWebBrowser(this.KanbanUrl);
            webview.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(webview);
        }

    }
}
