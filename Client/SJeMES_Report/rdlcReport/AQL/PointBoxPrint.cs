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
    public partial class PointBoxPrint : Form
    {
        public string _Language;
        public string _APIURL;
        public string _token;
        public int SourcePage = 0;//0:AQL;1:验货室
        public PointBoxPrint(Dictionary<string, object> rdlcParam, string APIURL, string token, string Language, int _SourcePage)
        {
            InitializeComponent();
            _Language = Language;
            _APIURL = APIURL;
            _token = token;
            SourcePage = _SourcePage;
            try
            {
                InitialReport(rdlcParam);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InitialReport(Dictionary<string, object> rdlcParam)
        {
            string xiangshu = "";
            if (rdlcParam.ContainsKey("xiangshu"))
                xiangshu = rdlcParam["xiangshu"].ToString();
            string art = rdlcParam["art"].ToString();//art
            string shoe_name = rdlcParam["shoe_name"].ToString();//鞋型
            string num = rdlcParam["num"].ToString();//订单总数
            string num_total = rdlcParam["num_total"].ToString();//订单双数
            string po = rdlcParam["po"].ToString();//po订单号
            string guojia = rdlcParam["guojia"].ToString();//Customer 客户
            string level = rdlcParam["level"].ToString();//Sample size样本

            string sample_proportion = rdlcParam["sample_proportion"].ToString();//抽样比例
            string VALS = rdlcParam["VALS"].ToString();//双数

            string act = rdlcParam["act"].ToString();//act
            string ac1 = rdlcParam["ac1"].ToString();//ac1
            string ac2 = rdlcParam["ac2"].ToString();//ac2
            string ac3 = rdlcParam["ac3"].ToString();//ac3

            string ret = rdlcParam["ret"].ToString();//ret
            string re1 = rdlcParam["re1"].ToString();//re1
            string re2 = rdlcParam["re2"].ToString();//re2
            string re3 = rdlcParam["re3"].ToString();//re3
            string boxtype = rdlcParam["boxtype"].ToString();//re3

            //类型 
            IDictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ENUM_CODE", boxtype);
            p.Add("ENUM_TYPE", "boxtype");
            p.Add("LANGUAGE", _Language);
            string type_retdata1 = WebAPIHelper.Post(
                                      _APIURL,
                                      "SJ_AQLAPI",//类库名
                                      "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                      "GetEnum",//方法名
                                      _token,//token
                                      Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject type_ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(type_retdata1);

            if (!type_ret.IsSuccess)
            {
                throw new Exception(type_ret.ErrMsg);
            }
            DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(type_ret.RetData);

            if (dt1.Rows.Count > 0)
            {
                boxtype = dt1.Rows[0]["ENUM_VALUE"].ToString();
            }


            DataTable PointBoxdt = (DataTable)rdlcParam["PointBoxdt"];//点箱记录

            List<Microsoft.Reporting.WinForms.ReportParameter> PS = new List<Microsoft.Reporting.WinForms.ReportParameter>();
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("art", art));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("shoe_name", shoe_name));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("num", num));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("po", po));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("guojia", guojia));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("level", level));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("sample_proportion", sample_proportion));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("VALS", VALS));

            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("act", act));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ac1", ac1));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ac2", ac2));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ac3", ac3));

            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ret", ret));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("re1", re1));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("re2", re2));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("re3", re3));

            //点箱记录
            for (int i = 0; i < PointBoxdt.Rows.Count; i++)
            {
                if (SourcePage == 0)
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"case_no{i + 1}", PointBoxdt.Rows[i]["case_no"].ToString()));
                else
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"case_no{i + 1}", ""));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"cr_size{i + 1}", PointBoxdt.Rows[i]["cr_size"].ToString()));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"se_qty{i + 1}", PointBoxdt.Rows[i]["se_qty"].ToString()));
            }
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("curr_date", DateTime.Now.ToString("yyyyMMdd")));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("num_total", num_total));
            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("boxtype", boxtype));

            PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("xiangshu", xiangshu));

            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\rdlcReport\\AQL\\PointBoxReport.rdlc";
            this.reportViewer1.LocalReport.SetParameters(PS);
            this.reportViewer1.RefreshReport();
        }
    }
}
