
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM.Forms
{
    public partial class FrmShowFiles : Form
    {
        string file_url = string.Empty;
        public FrmShowFiles(string file_url)
        {
            this.file_url = file_url;
            InitializeComponent();
        }

        private void FrmShowFiles_Load(object sender, EventArgs e)
        {
            try
            {
                panel1.Controls.Clear();
                if (!string.IsNullOrEmpty(this.file_url))
                {
                    //ChromiumWebBrowser webview = new ChromiumWebBrowser(this.file_url);
                    //webview.Dock = DockStyle.Fill;
                    //panel1.Controls.Add(webview);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }
    }
}
