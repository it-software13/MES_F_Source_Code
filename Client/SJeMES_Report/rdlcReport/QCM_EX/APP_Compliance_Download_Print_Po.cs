using Microsoft.Reporting.WinForms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Report.QCM_EX
{
    public partial class APP_Compliance_Download_Print_Po : Form
    {
        Dictionary<string, object> rdlcParam = new Dictionary<string, object>();
        DataTable dtbody = new DataTable();
        string prod_no = string.Empty;
        string prod_name = string.Empty;
        string shoe_name = string.Empty;
        string _apiurl = string.Empty;
        string _UserToken = string.Empty;
        public APP_Compliance_Download_Print_Po(Dictionary<string, object> _rdlcParam, DataTable _dtbody, string _prod_no, string _prod_name, string _shoe_name,string apiurl,string UserToken)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            rdlcParam = _rdlcParam;
            dtbody = _dtbody;
            prod_no = _prod_no;
            prod_name = _prod_name;
            shoe_name = _shoe_name;
            _apiurl = apiurl;
            _UserToken = UserToken;

            InitialReport(rdlcParam);
           // SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public void InitialReport(Dictionary<string, object> rdlcParam)
        {
            var plHeight = flowLayoutPanelTable.Height;
            var plWidth = flowLayoutPanelTable.Width;
            for (int i = 0; i < dtbody.Rows.Count; i++)
            {
                List<string> po_list = dtbody.Rows[i]["mer_po"].ToString().Split(',').ToList();

                foreach (var curr_po in po_list)
                {
                    ReportViewer r = new ReportViewer();
                    r.Name = $@"{prod_no}&{curr_po}&{DateTime.Now:yyyyMMddHHmmssfff}";
                    string space_str_1 = rdlcParam["space_str_1"].ToString();//空白区1
                    string space_str_2 = rdlcParam["space_str_2"].ToString();//空白区2
                    string space_str_3 = rdlcParam["space_str_3"].ToString();//空白区3
                    string space_str_4 = rdlcParam["space_str_4"].ToString();//空白区4
                    string space_str_5 = rdlcParam["space_str_5"].ToString();//空白区5
                    string space_str_6 = rdlcParam["space_str_6"].ToString();//空白区6
                    string signature = rdlcParam["signature"].ToString();//签名图片路径
                    string date = dtbody.Rows[i]["NST"].ToString();//日期
                    string po = curr_po;//po
                    string month = string.Empty;
                    try
                    {
                        month = Convert.ToDateTime(date).Month.ToString();
                    }
                    catch (Exception ex)
                    {

                    }
                    //请求api的数据展示
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("prod_no", po);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                _apiurl,
                                                "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.IQC_APP_Compliance",//类名
                                                "GetAPP_Compliance_Customer",//方法名
                                                _UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(data));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    List<Microsoft.Reporting.WinForms.ReportParameter> PS = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("space_str_1", space_str_1));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("space_str_2", space_str_2));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("space_str_3", space_str_3));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("space_str_4", space_str_4));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("space_str_5", space_str_5));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("space_str_6", space_str_6));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("signature", signature));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("date", date));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("prod_no", prod_no));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("prod_name", prod_name));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("shoe_name", shoe_name));
                    //PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("po", po));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("month", month));

                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("custorder", dic["custorder"].ToString()));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("customer_shipcountry", dic["shipcountry_en"].ToString()));

                    List<string> poList = new List<string>();
                    if (!string.IsNullOrEmpty(po))
                    {
                        poList = po.Split(',').ToList();
                    }

                    List<rdlcReport.QCM_EX.APP_Compliance_Report_Po> class1s_list = new List<rdlcReport.QCM_EX.APP_Compliance_Report_Po>();
                    int col_index = 1;
                    foreach (var item in poList)
                    {
                        if (col_index > 5)
                            col_index = 1;

                        switch (col_index)
                        {
                            case 1:
                                class1s_list.Add(new rdlcReport.QCM_EX.APP_Compliance_Report_Po() { col1 = item });
                                break;
                            case 2:
                                class1s_list.Last().col2 = item;
                                break;
                            case 3:
                                class1s_list.Last().col3 = item;
                                break;
                            case 4:
                                class1s_list.Last().col4 = item;
                                break;
                            case 5:
                                class1s_list.Last().col5 = item;
                                break;
                            default:
                                break;
                        }

                        col_index++;

                    }

                    r.LocalReport.EnableExternalImages = true;
                    r.LocalReport.ReportPath = Application.StartupPath + "\\rdlcReport\\QCM_EX\\APP_Compliance_Report.rdlc";
                    r.LocalReport.DataSources.Clear();
                    r.LocalReport.DataSources.Add(new ReportDataSource("tb111_ds", class1s_list));//指定数据源
                    r.LocalReport.SetParameters(PS);
                    r.RefreshReport();
                    r.Width = plWidth;
                    r.Height = plHeight;
                    this.flowLayoutPanelTable.Controls.Add(r);
                    r.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                }

                
            }
        }

        private void APP_Compliance_Download_Print_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string filePath = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            FolderBrowserDialog dilog = new FolderBrowserDialog();

            dilog.Description = "请选择文件夹";

            if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
            {

                filePath = dilog.SelectedPath;
                string time = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                //string subPath = Path.Combine(filePath, "app2报告" + time);//存放PDF文件 //检查是否存在文件夹
                string subPath = Path.Combine(filePath, "app2 report" + time);//存放PDF文件 //检查是否存在文件夹
                if (!string.IsNullOrWhiteSpace(subPath))   //创建文件夹
                {
                    Directory.CreateDirectory(subPath);
                }
                //string savePath = $@"C:\Users\admin\Desktop\app2报告";
                int index = 1;
                foreach (var rdlcItem in this.flowLayoutPanelTable.Controls)
                {
                    ReportViewer reportViewer = (ReportViewer)rdlcItem;
                   // RDLCExport.CreateFile(reportViewer, FileType.PDF, $@"app2报告_{index}.pdf", subPath);
                    RDLCExport.CreateFile(reportViewer, FileType.PDF, $@"app2 report_{index}.pdf", subPath);
                    index++;
                }

                //合并多个文件
                SJeMES_Control_Library.PdfHelper.PdfTool.MergePdf(subPath, "app2 report");

            }
        }
    }
}
