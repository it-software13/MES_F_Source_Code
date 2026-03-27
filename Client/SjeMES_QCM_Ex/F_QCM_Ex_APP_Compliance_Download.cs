using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using SJeMES_Report.QCM_EX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_APP_Compliance_Download : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        DataTable Downloaddt = new DataTable();
        string prod_no = string.Empty;//art
        string prod_name = string.Empty;//art名称
        string shoe_name = string.Empty;//鞋型名称
        public F_QCM_Ex_APP_Compliance_Download(string _prod_no,string _prod_name, string _shoe_name)
        {
            InitializeComponent();
            prod_no = _prod_no;
            shoe_name = _shoe_name;
            prod_name = _prod_name;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker2.MinDate =Convert.ToDateTime("1753-1-1 0:00:00");
                dateTimePicker2.MaxDate = GetCurrentMonthLastDay(Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00:00")));
                dateTimePicker2.MinDate = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-" + "01" + " 0:00:00"));
                if (dateTimePicker1.Value> dateTimePicker2.Value)
                {
                    dateTimePicker2.Value = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00:00"));
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// 获取指定月份的最后一天
        /// </summary>
        /// <param name="dateTime">传入时间</param>
        /// <returns></returns>
        public DateTime GetCurrentMonthLastDay(DateTime dateTime)
        {
            DateTime d1 = new DateTime(dateTime.Year, dateTime.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            return d2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAPP_Compliance_Download();
        }

        /// <summary>
        /// 查询-APP2合规-下载APP2报告
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public void GetAPP_Compliance_Download()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("DueDateS", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                data.Add("DueDateE", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                data.Add("prod_no", prod_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_APP_Compliance",//类名
                                            "GetAPP_Compliance_Download",//方法名
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
                Downloaddt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                string mer_pos = string.Empty;
                foreach (DataRow item in Downloaddt.Rows)
                {
                    mer_pos += item["MER_PO"].ToString() + ",";
                }
                richTextBox1.Text = mer_pos.TrimEnd(',');
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询-APP2合规-主页-模板维护
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetAPP_Compliance_Maintenance()
        {
            Dictionary<string, object> rdlcParam = new Dictionary<string, object>();
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
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
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

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return rdlcParam;
        }

        /// <summary>
        /// 查询-APP2合规-下载APP2报告-----DT
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public DataTable GetAPP_Compliance_Download_DT()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("DueDateS", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                data.Add("DueDateE", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                data.Add("prod_no", prod_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_APP_Compliance",//类名
                                            "GetAPP_Compliance_Download",//方法名
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
                Downloaddt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return Downloaddt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (APP_Compliance_Download_Print a = new APP_Compliance_Download_Print(GetAPP_Compliance_Maintenance(), GetAPP_Compliance_Download_DT(), prod_no, prod_name, shoe_name, Program.Client.APIURL, Program.Client.UserToken, Program.Client.UploadUrl))
            {
                a.ShowDialog();
            }
        }
    }
}
