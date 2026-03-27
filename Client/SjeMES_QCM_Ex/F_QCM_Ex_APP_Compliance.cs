using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
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
    public partial class F_QCM_Ex_APP_Compliance : MaterialForm
    {
        private bool CALL_API = false;//是否调用api
        private DataTable DT_HEAD;//表头数据
        private DataTable DT_BODY;//表头数据
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_APP_Compliance()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_Ex_APP_Compliance_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetAPP_ComplianceMain;
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 查询-APP2合规-主页-身体
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetAPP_ComplianceMain(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                if (CALL_API)
                {
                    //请求api的数据展示
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    //键值对传值
                    data.Add("PROD_NO", textBox1.Text);//条件 art
                                                       //data.Add("pageSize", pageSize);
                                                       //data.Add("pageIndex", pageIndex);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJeMES_IQC",//类库名
                                                "SJeMES_IQC.IQC_APP_Compliance",//类名
                                                "GetAPP_ComplianceMain",//方法名
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
                    DT_HEAD = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["DataHead"].ToString());
                    DT_BODY = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                    CALL_API = false;
                }
                dataGridView1.Rows.Clear();
                if (DT_HEAD.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in DT_HEAD.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["ART"].Value = dr["PROD_NO"].ToString();
                        dgvr.Cells["ART名称"].Value = dr["prod_name"].ToString();
                        dgvr.Cells["鞋型"].Value = dr["shoe_name"].ToString();
                        dgvr.Cells["状态"].Value = dr["Astate"].ToString();
                        dgvr.Cells["到期日期"].Value = dr["Adate"].ToString();
                        i++;
                    }
                }

                dataGridView2.Rows.Clear();
                if (DT_BODY != null && DT_BODY.Rows.Count > 0)
                {
                    var dt = GetPageDataTable(DT_BODY, pageIndex, pageSize);
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView2.Rows.Add();
                        DataGridViewRow dgvr = dataGridView2.Rows[i];
                        dgvr.Cells["序号"].Value = i + 1;
                        dgvr.Cells["报告编号"].Value = dr["report_no"].ToString();
                        dgvr.Cells["部位"].Value = dr["POSITION_CN"].ToString() + dr["POSITION_EN"].ToString();
                        dgvr.Cells["料号"].Value = dr["ITEM_NO"].ToString();
                        dgvr.Cells["材料ID"].Value = dr["AD_ITEM_NO"].ToString();
                        dgvr.Cells["材料名称"].Value = dr["NAME_CN"].ToString();
                        //dgvr.Cells["颜色"].Value = dr["COLOR_NAME"].ToString();
                        //dgvr.Cells["颜色代码"].Value = dr["COLOR_NO"].ToString();
                        //dgvr.Cells["生产厂商"].Value = dr["VEND_NAME"].ToString();
                        dgvr.Cells["供应商"].Value = dr["gys"].ToString();
                        dgvr.Cells["状态2"].Value = dr["Astate"].ToString();
                        dgvr.Cells["到期日期2"].Value = dr["Adate"].ToString();
                        if (dr["Astate"].ToString() == "Arrival")
                        {
                            dgvr.Cells["状态2"].Style.ForeColor = Color.Red;
                            dgvr.Cells["到期日期2"].Style.ForeColor = Color.Red;
                        }
                        i++;
                    }
                    totalCount = DT_BODY.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public DataTable GetPageDataTable(DataTable dt, int currentPageIndex, int pageSize)
        {
            if (currentPageIndex == 0)
            {
                return dt;
            }

            DataTable newdt = dt.Clone();

            int rowbegin = (currentPageIndex - 1) * pageSize;//当前页的第一条数据在dt中的位置
            int rowend = currentPageIndex * pageSize;//当前页的最后一条数据在dt中的位置

            if (rowbegin >= dt.Rows.Count)
            {
                return newdt;
            }

            if (rowend > dt.Rows.Count)
            {
                rowend = dt.Rows.Count;
            }

            DataView dv = dt.DefaultView;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                newdt.ImportRow(dv[i].Row);
            }

            return newdt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CALL_API = true;
            LoadPage();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "操作")
                {
                    string report_no = dataGridView2.Rows[e.RowIndex].Cells["报告编号"].Value.ToString();
                    string item_no = dataGridView2.Rows[e.RowIndex].Cells["料号"].Value.ToString();
                    var currRowFileDt = GetAPP_ComplianceMain_bg(report_no,item_no);
                    FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", false, false);
                    add.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 查询-APP2合规-主页-查看报告
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public DataTable GetAPP_ComplianceMain_bg(string report_no,string item_no)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("report_no", report_no);//报告编号
                data.Add("item_no", item_no);//料号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_APP_Compliance",//类名
                                            "GetAPP_ComplianceMain_bg",//方法名
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
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("net_file_url", typeof(string));
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["file_url"].ToString()))
                        {
                            try
                            {
                                dr["net_file_url"] = Program.Client.PicUrl + dr["file_url"].ToString();
                            }
                            catch
                            {
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        /// <summary>
        /// 查询-APP2合规-主页-下载列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetAPP_ComplianceDownloadLists()
        {
            try
            {
                if (DT_HEAD != null && DT_HEAD.Rows.Count > 0)
                {

                    var dthead = DT_HEAD.Copy();
                    var dt = DT_BODY.Copy();

                    Dictionary<string, string> HeadDic = new Dictionary<string, string>();
                    //HeadDic.Add("ART", dthead.Rows[0]["PROD_NO"].ToString());
                    //HeadDic.Add("鞋型", dthead.Rows[0]["shoe_name"].ToString());
                    //HeadDic.Add("A-01状态", dthead.Rows[0]["Astate"].ToString());
                    //HeadDic.Add("A-01到期日期", dthead.Rows[0]["Adate"].ToString());
                    HeadDic.Add("ART", dthead.Rows[0]["PROD_NO"].ToString());
                    HeadDic.Add("Shoe_type", dthead.Rows[0]["shoe_name"].ToString());
                    HeadDic.Add("A-01_status", dthead.Rows[0]["Astate"].ToString());
                    HeadDic.Add("A-01_Expire_date", dthead.Rows[0]["Adate"].ToString());

                    Dictionary<string, string> Execldic = new Dictionary<string, string>();
                    //Execldic.Add("report_no", "报告编号");
                    //Execldic.Add("position", "部位");
                    //Execldic.Add("item_no", "料号");
                    //Execldic.Add("item_id", "材料ID");
                    //Execldic.Add("name_cn", "材料名称");
                    ////Execldic.Add("color_name", "颜色");
                    ////Execldic.Add("color_no", "颜色代码");
                    ////Execldic.Add("vend_name", "生产厂商");
                    //Execldic.Add("gys", "供应商");
                    //Execldic.Add("astate", "A-01状态");
                    //Execldic.Add("adate", "A-01到期日期");

                    Execldic.Add("report_no", "Report_No");
                    Execldic.Add("position", "Parts");
                    Execldic.Add("item_no", "Part_No");
                    Execldic.Add("item_id", "Material_ID");
                    Execldic.Add("name_cn", "Material_Name");
                    //Execldic.Add("color_name", "颜色");
                    //Execldic.Add("color_no", "颜色代码");
                    //Execldic.Add("vend_name", "生产厂商");
                    Execldic.Add("gys", "Supplier");
                    Execldic.Add("astate", "A-01_status");
                    Execldic.Add("adate", "A-01_Expire_date");


                    foreach (DataRow item in dt.Rows)
                    {
                        item["position"] = item["POSITION_CN"].ToString() + item["POSITION_EN"].ToString();
                    }

                    dt.Columns.Remove("POSITION_CN");
                    dt.Columns.Remove("POSITION_EN");
                    dt.Columns.Remove("COLOR_NAME");
                    dt.Columns.Remove("COLOR_NO");
                    dt.Columns.Remove("VEND_NO");
                    dt.Columns.Remove("VEND_NAME");

                  //  ExeclHelper.ExportToTrueExcelEx(dt, HeadDic, Execldic, "APP2下载列表");
                    ExeclHelper.ExportToTrueExcelEx(dt, HeadDic, Execldic, "APP2 download list");
                }
                else
                {
                    MessageBox.Show("No data export yet, please check whether the operation is correct");
                    return;
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetAPP_ComplianceDownloadLists();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetAPP_Compliance_Maintenance();
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

                using (APP_Compliance_Print a=new APP_Compliance_Print(rdlcParam,Program.Client))
                {
                    a.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("ART information cannot be empty!");
                return;
            }
            string prod_no = dataGridView1.Rows[0].Cells["ART"].Value.ToString();
            string prod_name = dataGridView1.Rows[0].Cells["ART名称"].Value.ToString();
            string shoe_name = dataGridView1.Rows[0].Cells["鞋型"].Value.ToString();
            using (F_QCM_Ex_APP_Compliance_Download f = new F_QCM_Ex_APP_Compliance_Download(prod_no, prod_name, shoe_name))
            {
                f.ShowDialog();
            }
        }
    }
}
