using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM.RDLC
{
    public partial class F_A01_Report : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        string art_no = string.Empty;
        string art_name = string.Empty;
        string codenumber = string.Empty;

        public F_A01_Report(string art_no,string art_name,string codenumber)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            this.art_no = art_no;
            this.art_name = art_name;
            this.codenumber = codenumber;
            InitializeComponent();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F_A01_Report_Load(object sender, EventArgs e)
        {
            try
            {  
                this.panel1.Controls.Add(this.reportViewer1);

                //this.reportViewer1.Width = this.Width;
                //this.reportViewer1.Height = this.Height;
                this.reportViewer1.Dock = DockStyle.Fill;
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "SJeMES_QCM.RDLC.A-01.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataTable dt = new DataTable();
                dt.Columns.Add("No");
                dt.Columns.Add("Name");
                dt.Columns.Add("CodeNumber");
                dt.Columns.Add("Date");

                DataRow dr = dt.NewRow();
                dr["No"] = this.art_no;
                dr["Name"] = this.art_name;
                dr["CodeNumber"] = this.codenumber;
                dr["Date"] = DateTime.Now.ToString("yyyy-MM-dd"); 

                this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt)); 
                 
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
