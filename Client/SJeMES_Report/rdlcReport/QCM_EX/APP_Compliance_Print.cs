using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using SJeMES_QCM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Report.QCM_EX
{
    public partial class APP_Compliance_Print : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public APP_Compliance_Print(Dictionary<string, object> rdlcParam, SJeMES_Framework.Class.ClientClass _Program)
        {
            InitializeComponent();
            InitialReport(rdlcParam);
            Program.Client = _Program;
           // SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void InitialReport(Dictionary<string, object> rdlcParam)
        {
            this.reportViewer1.Clear();
            string space_str_1 = rdlcParam["space_str_1"].ToString();//空白区1
            string space_str_2 = rdlcParam["space_str_2"].ToString();//空白区2
            string space_str_3 = rdlcParam["space_str_3"].ToString();//空白区3
            string space_str_4 = rdlcParam["space_str_4"].ToString();//空白区4
            string space_str_5 = rdlcParam["space_str_5"].ToString();//空白区5
            string space_str_6 = rdlcParam["space_str_6"].ToString();//空白区6
            string signature = rdlcParam["signature"].ToString();//签名图片路径
            string date = rdlcParam["date"].ToString();//日期
            string prod_no = rdlcParam["prod_no"].ToString();//art代号
            string prod_name = rdlcParam["prod_name"].ToString();//art名称
            string shoe_name = rdlcParam["shoe_name"].ToString();//鞋型
            string po = rdlcParam["po"].ToString();//po

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

            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\rdlcReport\\QCM_EX\\APP_Compliance_Report.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("tb111_ds", class1s_list));//指定数据源
            this.reportViewer1.LocalReport.SetParameters(PS);
            this.reportViewer1.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (APP_Compliance_Print_Edit a=new APP_Compliance_Print_Edit(Program.Client))
            {
                a.ShowDialog();
                if (a.Tag.ToString()=="成功")
                {
                    GetAPP_Compliance_Maintenance();
                }
            }
        }

        /// <summary>
        /// 查询-APP2合规-主页-模板维护
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public void GetAPP_Compliance_Maintenance()
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_APP_Compliance",//类名
                                            "GetAPP_Compliance_Maintenance",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                Dictionary<string, object> rdlcParam = new Dictionary<string, object>();
                if (dt.Rows.Count > 0)
                {
                    rdlcParam.Add("space_str_1", dt.Rows[0]["space_str_1"].ToString());
                    rdlcParam.Add("space_str_2", dt.Rows[0]["space_str_2"].ToString());
                    rdlcParam.Add("space_str_3", dt.Rows[0]["space_str_3"].ToString());
                    rdlcParam.Add("space_str_4", dt.Rows[0]["space_str_4"].ToString());
                    rdlcParam.Add("space_str_5", dt.Rows[0]["space_str_5"].ToString());
                    rdlcParam.Add("space_str_6", dt.Rows[0]["space_str_6"].ToString());
                    rdlcParam.Add("signature", Program.Client.PicUrl + dt.Rows[0]["FILE_URL"].ToString());
                    rdlcParam.Add("date", "");
                    rdlcParam.Add("prod_no", "");
                    rdlcParam.Add("prod_name", "");
                    rdlcParam.Add("shoe_name", "");
                    rdlcParam.Add("po", "");
                }
                else
                {
                    rdlcParam.Add("space_str_1", "");
                    rdlcParam.Add("space_str_2", "");
                    rdlcParam.Add("space_str_3", "");
                    rdlcParam.Add("space_str_4", "");
                    rdlcParam.Add("space_str_5", "");
                    rdlcParam.Add("space_str_6", "");
                    rdlcParam.Add("signature", "");
                    rdlcParam.Add("date", "");
                    rdlcParam.Add("prod_no", "");
                    rdlcParam.Add("prod_name", "");
                    rdlcParam.Add("shoe_name", "");
                    rdlcParam.Add("po", "");
                }

                InitialReport(rdlcParam);
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
