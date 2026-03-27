using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using SJeMES_Report.AQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_CmaTask_Inspection_dxPrint : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        List<TestType> ttList = new List<TestType>();
        public DataTable _dt;
        public string _PO;
        public F_AQL_CmaTask_Inspection_dxPrint(string PO)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _PO = PO;
        }

        private void F_AQL_CmaTask_Inspection_dxPrint_Load(object sender, EventArgs e)
        {
            #region  下拉框
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            //键值对传值
            //p.Add("task_no", dics["task_no"]);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.F_AQL_Entry",//类名
                                        "GetAQLEntry_Level",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }

            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示

            //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
            var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
            //var dt3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());

            //样品级别初始化
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "value";
            comboBox1.ValueMember = "code";
            comboBox1.SelectedIndex = 1;//设置该下拉框默认选中
            //
            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "value";
            comboBox2.ValueMember = "code";
            comboBox2.SelectedIndex = 12;//设置该下拉框默认选中
            #endregion

            #region 检验类型
            TestType t1 = new TestType();
            t1.code = "0";
            t1.value = "Finally";//最终
            ttList.Add(t1);
            TestType t2 = new TestType();
            t2.code = "1";
            t2.value = "Rummage";//翻箱
            ttList.Add(t2);
            TestType t3 = new TestType();
            t3.code = "2";
            t3.value = "Again";//再次
            ttList.Add(t3);
            TestType t4 = new TestType();
            t4.code = "3";
            t4.value = "Rummage_Again";
            ttList.Add(t4);
            comboBox3.DataSource = ttList;
            comboBox3.DisplayMember = "value";
            comboBox3.ValueMember = "code";
            #endregion

            Application.DoEvents();//转让控制权
            //初始化数据
            GetDataList();
            Application.DoEvents();//转让控制权
        }
        public class TestType
        {
            public string code { get; set; }
            public string value { get; set; }
        }


        public void GetDataList(int indexRow = -1)
        {

            dataGridViewEx1.Rows.Clear();
            dataGridViewEx2.Rows.Clear();



            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("keyword", tb_search.Text);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                        "GetPOData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            _dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

            //if (_dt.Rows.Count > 0)
            //{
            //    int i = 0;
            //    var list = _PO.Split(',').ToArray();
            //    dataGridViewEx1.Rows.Clear();
            //    dataGridViewEx2.Rows.Clear();

            //    List<string> artist = new List<string>();
            //    if (!string.IsNullOrEmpty(_PO))
            //    {
            //        artist = _PO.Split(',').ToList();
            //    }

            //    if (_dt != null && _dt.Rows.Count > 0)
            //    {
            //        foreach (var art in artist)
            //        {
            //            var search_dt_rows = _dt.Select($@"MER_PO = '{art}'");
            //            if (search_dt_rows.Length > 0)
            //            {
            //                foreach (DataRow item in search_dt_rows)
            //                {
            //                    int x = dataGridViewEx2.Rows.Add();
            //                    dataGridViewEx2.Rows[x].Cells["鞋型2"].Value =   item["name_t"].ToString();
            //                    dataGridViewEx2.Rows[x].Cells["ART2"].Value =    item["PROD_NO"].ToString();
            //                    dataGridViewEx2.Rows[x].Cells["PO2"].Value =     item["MER_PO"].ToString();
            //                    dataGridViewEx2.Rows[x].Cells["POnum2"].Value =  item["SE_QTY"].ToString();
            //                }
            //            }
            //        }
            //    }


            //    if (_dt != null && _dt.Rows.Count > 0)
            //    {
            //        string search = tb_search.Text;
            //        if (!string.IsNullOrEmpty(search))
            //        {
            //            var search_dt_rows = _dt.Select($@"MER_PO like '%{search}%'");
            //            //var search_dt_rows = _dt.Select($@"MER_PO like '%{search}%' or PROD_NO like '%{search}%' or NAME_T like '%{search}%'");
            //            //var search_dt_rows = _dt.Select($@"");
            //            if (search_dt_rows.Length > 0)
            //            {
            //                foreach (DataRow item in search_dt_rows)
            //                {
            //                    int x = dataGridViewEx1.Rows.Add();
            //                    dataGridViewEx1.Rows[x].Cells["Column1"].Value = (artist.FirstOrDefault(c => c == item["MER_PO"].ToString()) == null) ? "False" : "True";

            //                    dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
            //                    dataGridViewEx1.Rows[x].Cells["鞋型"].Value = item["name_t"].ToString();
            //                    dataGridViewEx1.Rows[x].Cells["ART"].Value = item["PROD_NO"].ToString();
            //                    dataGridViewEx1.Rows[x].Cells["PO"].Value = item["MER_PO"].ToString();
            //                    dataGridViewEx1.Rows[x].Cells["POnum"].Value = item["SE_QTY"].ToString();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            foreach (DataRow item in _dt.Rows)
            //            {
            //                int x = dataGridViewEx1.Rows.Add();
            //                dataGridViewEx1.Rows[x].Cells["Column1"].Value = (artist.FirstOrDefault(c => c == item["MER_PO"].ToString()) == null) ? "False" : "True";
            //                dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
            //                dataGridViewEx1.Rows[x].Cells["鞋型"].Value = item["name_t"].ToString();
            //                dataGridViewEx1.Rows[x].Cells["ART"].Value =   item["PROD_NO"].ToString();
            //                dataGridViewEx1.Rows[x].Cells["PO"].Value =    item["MER_PO"].ToString();;
            //                dataGridViewEx1.Rows[x].Cells["POnum"].Value = item["SE_QTY"].ToString();
            //            }
            //        }



            //    }
            //}

            //SJeMES_Framework.Common.UIHelper.LoadDgv(dataGridViewEx1);
        }
        private void updatedata()
        {
            dataGridViewEx2.Rows.Clear();

            if (_dt != null && _dt.Rows.Count > 0)
            {
                var list = _PO.Split(',');
                foreach (var art in list)
                {
                    var search_dt_rows = _dt.Select($@"MER_PO = '{art}'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int x = dataGridViewEx2.Rows.Add();
                            dataGridViewEx2.Rows[x].Cells["鞋型2"].Value = item["name_t"].ToString();
                            dataGridViewEx2.Rows[x].Cells["ART2"].Value =   item["PROD_NO"].ToString();
                            dataGridViewEx2.Rows[x].Cells["PO2"].Value =    item["MER_PO"].ToString();;
                            dataGridViewEx2.Rows[x].Cells["POnum2"].Value = item["SE_QTY"].ToString();
                        }
                    }
                }

            }
            SJeMES_Framework.Common.UIHelper.LoadDgv(dataGridViewEx2);
        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Thread.Sleep(200);
            if (e.RowIndex > -1)
            {

                //_artlist = "";
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Column1")
                {
                    List<string> art_list = new List<string>();
                    if (!string.IsNullOrEmpty(_PO))
                    {
                        art_list = _PO.Split(',').ToList();
                    }
                    var currCheck = dataGridViewEx1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();
                    var art = dataGridViewEx1.Rows[e.RowIndex].Cells["PO"].Value.ToString();
                    if (currCheck.ToLower() == "true")
                    {
                        art_list.Add(art);
                    }
                    else
                    {
                        art_list.Remove(art);
                    }

                    if (art_list.Count > 0)
                    {
                        art_list = art_list.Distinct().ToList();
                        _PO = string.Join(",", art_list);
                    }
                    else
                    {
                        _PO = "";
                    }
                    updatedata();

                }
            }
        }

        private void dataGridViewEx1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Column1")
            {
                List<string> art_list = new List<string>();
                if (!string.IsNullOrEmpty(_PO))
                {
                    art_list = _PO.Split(',').ToList();
                }
                var currCheck = dataGridViewEx1.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
                if (currCheck.ToLower() == "true")
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        art_list.Add(item.Cells["PO"].Value.ToString());
                    }
                }
                else
                {
                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        art_list.Remove(item.Cells["PO"].Value.ToString());
                    }
                }
                if (art_list.Count > 0)
                {
                    art_list = art_list.Distinct().ToList();
                    _PO = string.Join(",", art_list);
                }
                else
                {
                    _PO = "";
                }
                updatedata();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var POlist = _PO.Split(',');
            if (POlist.Length > 0)
            {
                string APIURL = Program.Client.APIURL;
                string token = Program.Client.UserToken;
               var Language = Program.Client.Language;
                //string value = string.Empty;
                switch (Language.ToLower())
                {
                    case "cn":
                        Language = "UI_CN";
                        break;
                    case "yn":
                        Language = "UI_YN";
                        break;
                    case "en":
                        Language = "UI_EN";
                        break;
                    default:
                        break;
                }

                using (PointBoxPrintMulti h = new PointBoxPrintMulti(POlist, APIURL, token, comboBox1.SelectedValue.ToString(), comboBox1.Text.ToString(), comboBox2.SelectedValue.ToString(), comboBox3.SelectedValue.ToString(), Language))
                {
                    h.ShowDialog();
                }

            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择数据！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_search_Click(object sender, EventArgs e)
        {
            //初始化数据
            //GetDataList();
            dataGridViewEx1.Rows.Clear();
            dataGridViewEx2.Rows.Clear();
            List<string> art_list = new List<string>();
            if (!string.IsNullOrEmpty(_PO))
            {
                art_list = _PO.Split(',').ToList();
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                foreach (var art in art_list)
                {
                    var search_dt_rows = _dt.Select($@"MER_PO = '{art}'");
                    if (search_dt_rows.Length > 0)
                    {
                        foreach (DataRow item in search_dt_rows)
                        {
                            int x = dataGridViewEx2.Rows.Add();
                            dataGridViewEx2.Rows[x].Cells["鞋型2"].Value = item["name_t"].ToString();
                            dataGridViewEx2.Rows[x].Cells["ART2"].Value = item["PROD_NO"].ToString();
                            dataGridViewEx2.Rows[x].Cells["PO2"].Value = item["MER_PO"].ToString(); ;
                            dataGridViewEx2.Rows[x].Cells["POnum2"].Value = item["SE_QTY"].ToString();
                        }
                    }
                }
            }

            if (_dt != null && _dt.Rows.Count > 0)
            {
                string search = tb_search.Text;
                if (!string.IsNullOrEmpty(search))
                {
                    List<string> searchList = search.Split(new char[2] { '\r', '\n' }).ToList();
                    searchList = searchList.Where(x => !string.IsNullOrEmpty(x)).ToList();
                    if (searchList.Count > 0)
                    {
                        var search_dt_rows = _dt.Select(string.Join(" or ", searchList.Select(x=>$@"MER_PO = '{x}'")));
                        if (search_dt_rows.Length > 0)
                        {
                            foreach (DataRow item in search_dt_rows)
                            {
                                int x = dataGridViewEx1.Rows.Add();
                                dataGridViewEx1.Rows[x].Cells["Column1"].Value = (art_list.FirstOrDefault(c => c == item["MER_PO"].ToString()) == null) ? "False" : "True";
                                dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
                                dataGridViewEx1.Rows[x].Cells["鞋型"].Value = item["name_t"].ToString();
                                dataGridViewEx1.Rows[x].Cells["ART"].Value = item["PROD_NO"].ToString();
                                dataGridViewEx1.Rows[x].Cells["PO"].Value = item["MER_PO"].ToString(); ;
                                dataGridViewEx1.Rows[x].Cells["POnum"].Value = item["SE_QTY"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow item in _dt.Rows)
                    {
                        int x = dataGridViewEx1.Rows.Add();
                        dataGridViewEx1.Rows[x].Cells["Column1"].Value = (art_list.FirstOrDefault(c => c == item["MER_PO"].ToString()) == null) ? "False" : "True";
                        dataGridViewEx1.Rows[x].Cells["RN"].Value = item["RN"].ToString();
                        dataGridViewEx1.Rows[x].Cells["鞋型"].Value = item["name_t"].ToString();
                        dataGridViewEx1.Rows[x].Cells["ART"].Value = item["PROD_NO"].ToString();
                        dataGridViewEx1.Rows[x].Cells["PO"].Value = item["MER_PO"].ToString(); ;
                        dataGridViewEx1.Rows[x].Cells["POnum"].Value = item["SE_QTY"].ToString();
                    }
                }


            }
        }
    }
}
