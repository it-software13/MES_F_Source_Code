using Microsoft.Reporting.WinForms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Report.AQL
{
    public partial class frmInspectionCertificate : Form
    {
        public string _APIURL;
        public string _token;
        public frmInspectionCertificate(Dictionary<string, string> rdlcParam)
        {
            InitializeComponent();
            InitialReport(rdlcParam);
        }
        public void InitialReport(Dictionary<string,string> rdlcParam)
        {
            try
            {
                string ICNo = rdlcParam["ICNo"];
                string ModelNo = rdlcParam["ModelNo"];
                string QuantilyNum = rdlcParam["QuantilyNum"];
                string ArticleNo = rdlcParam["ArticleNo"];
                string CustomeNo = rdlcParam["CustomeNo"];
                string Destination = rdlcParam["Destination"];
                string PoNo = rdlcParam["PoNo"];
                string qc = rdlcParam["qc"];
                string dateString = rdlcParam["dateString"];
                string manager = rdlcParam["manager"];
                string dateString2 = rdlcParam["dateString2"];
                string result = rdlcParam["result"];
                string result2 = rdlcParam["result2"];
                List<Microsoft.Reporting.WinForms.ReportParameter> PS = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ICNo", ICNo));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ModelNo", ModelNo));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("QuantilyNum", QuantilyNum));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ArticleNo", ArticleNo));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("CustomeNo", CustomeNo));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("Destination", Destination));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("PoNo", PoNo));

                //20180920新增
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("qc", qc));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("qctime", dateString));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("manager", manager));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("managertime", dateString2));

                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("result", result));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("result2", result2));

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\rdlcReport\\AQL\\InspectionCertificateReport.rdlc";
                this.reportViewer1.LocalReport.SetParameters(PS);
                this.reportViewer1.RefreshReport();
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
