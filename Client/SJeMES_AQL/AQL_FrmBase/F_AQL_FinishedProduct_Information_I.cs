using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
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

namespace SJeMES_AQL.AQL_FrmBase
{
    public partial class F_AQL_FinishedProduct_Information_I : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        List<inspection_state> insplist = new List<inspection_state>();
        List<inspection_state> xsddlist = new List<inspection_state>();
        List<inspection_state> chztlist = new List<inspection_state>();
        public bool _enable;
        public F_AQL_FinishedProduct_Information_I()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            InitDateTimePicker(dateTimePicker1);
            InitDateTimePicker(dateTimePicker2);
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "   ";
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "   ";

            DateTime currDate = DateTime.Now;
            

            #region 验货状态
            inspection_state i3 = new inspection_state();
            i3.code = "";
            i3.value = "";
            insplist.Add(i3);
            inspection_state i1 = new inspection_state();
            i1.code = "0";
            i1.value = "未验货";
            insplist.Add(i1);
            inspection_state i2 = new inspection_state();
            i2.code = "1";
            i2.value = "已验货";
            insplist.Add(i2);
            comboBox3.DataSource = insplist;
            comboBox3.DisplayMember = "value";
            comboBox3.ValueMember = "code";
            #endregion

            #region 销售订单状态
            inspection_state xsdd1 = new inspection_state();
            xsdd1.code = "";
            xsdd1.value = "All";//所有
            xsddlist.Add(xsdd1);
            inspection_state xsdd2 = new inspection_state();
            xsdd2.code = "0";
            xsdd2.value = "新接订单";
            xsddlist.Add(xsdd2);
            inspection_state xsdd3 = new inspection_state();
            xsdd3.code = "7";
            xsdd3.value = "有效订单";
            xsddlist.Add(xsdd3);
            inspection_state xsdd4 = new inspection_state();
            xsdd4.code = "99";
            xsdd4.value = "取消订单";
            xsddlist.Add(xsdd4);
            cb_xsddzt.DataSource = xsddlist;
            cb_xsddzt.DisplayMember = "value";
            cb_xsddzt.ValueMember = "code";
            #endregion

            #region 出货状态
            //已出货
            //未出货
            //订单取消
            //订单替换
            //订单减少
            //分批出货
            inspection_state chzt1 = new inspection_state();
            chzt1.code = "";
            chzt1.value = "";
            chztlist.Add(chzt1);
            inspection_state chzt2 = new inspection_state();
            chzt2.code = "已出货";
            chzt2.value = "已出货";
            chztlist.Add(chzt2);
            inspection_state chzt3 = new inspection_state();
            chzt3.code = "未出货";
            chzt3.value = "未出货";
            chztlist.Add(chzt3);
            inspection_state chzt4 = new inspection_state();
            chzt4.code = "订单取消";
            chzt4.value = "订单取消";
            chztlist.Add(chzt4);
            inspection_state chzt5 = new inspection_state();
            chzt5.code = "订单替换";
            chzt5.value = "订单替换";
            chztlist.Add(chzt5);
            inspection_state chzt6 = new inspection_state();
            chzt6.code = "订单减少";
            chzt6.value = "订单减少";
            chztlist.Add(chzt6);
            inspection_state chzt7 = new inspection_state();
            chzt7.code = "分批出货";
            chzt7.value = "分批出货";
            chztlist.Add(chzt7);
            cb_chzt.DataSource = chztlist;
            cb_chzt.DisplayMember = "value";
            cb_chzt.ValueMember = "code";
            #endregion

        }

        #region 日期控件初始为空值处理

        /// <summary>
        /// 初始化日期时间控件
        /// </summary>
        /// <param name="dtp"></param>
        public static void InitDateTimePicker(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = " ";  //必须设置成" "
            dtp.ValueChanged -= DateTimePicker_ValueChanged;
            dtp.ValueChanged += DateTimePicker_ValueChanged;
            dtp.KeyPress -= DateTimePicker_KeyPress;
            dtp.KeyPress += DateTimePicker_KeyPress;
        }

        public static void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd"; //null;
            dtp.Checked = false;// 解决BUG ：防止日期控件不能选择相同日期的 --- 要放置在设置格式之后
        }

        public static void DateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)  // backspace左删除键
            {
                DateTimePicker dtp = (DateTimePicker)sender;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
            }
        }
        #endregion

        //验货状态
        public class inspection_state
        {
            public string code { get; set; }
            public string value { get; set; }
        }

        private void F_AQL_FinishedProduct_Information_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            //if (_enable)
            //{
            pageControl1.BindPageEvent += GetFinishedProduct_Information_Main;
            //LoadPage();
            this.dataGridViewEx1.ClearSelection();
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now.AddMonths(-3);
            //}
        }

        /// <summary>
        /// 初始化分页
        /// </summary>
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        Dictionary<string, object> p_temp = new Dictionary<string, object>();
        DataTable dt_temp = null;

        /// <summary>
        /// 查询-出货通知
        /// </summary>
        /// <param name="a">为了防止dataGridViewEx1里添加的按钮重复 </param>
        public void GetFinishedProduct_Information_Main(int pageSize, int pageIndex, out int totalCount)
        {
            this.Enabled = false;
            totalCount = 0;
            try
            {
                bool noFilter = true;//是否无过滤条件

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                string sccq = textBox1.Text.Trim();
                p.Add("sccq", sccq);
                if (!string.IsNullOrEmpty(sccq))
                    noFilter = false;

                string country = textBox6.Text.Trim();
                p.Add("country", country);
                if (!string.IsNullOrEmpty(country))
                    noFilter = false;

                string mer_po = textBox2.Text.Trim();
                p.Add("mer_po", mer_po);
                if (!string.IsNullOrEmpty(mer_po))
                    noFilter = false;

                string prod_no = _enable == false ? textBox3.Text.Trim() : "无数据";
                p.Add("prod_no", prod_no);
                if (!string.IsNullOrEmpty(prod_no))
                    noFilter = false;

                string shoe_name = textBox4.Text.Trim();
                p.Add("shoe_name", shoe_name);
                if (!string.IsNullOrEmpty(shoe_name))
                    noFilter = false;

                string starttime = dateTimePicker1.Text.Trim();
                string endtime = dateTimePicker2.Text.Trim();
                //进仓日期【开始时间若为空，则结束时间向前取三个月】，反之一样逻辑
                if (string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                {
                    p.Add("endtime", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    noFilter = false;
                }
                else if (!string.IsNullOrEmpty(starttime) && string.IsNullOrEmpty(endtime))
                {
                    p.Add("starttime", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    noFilter = false;
                }
                else if(string.IsNullOrEmpty(starttime) && string.IsNullOrEmpty(endtime))
                {
                    //p.Add("starttime", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    //p.Add("endtime", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    //this.dateTimePicker1.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                    //this.dateTimePicker2.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    p.Add("starttime", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    p.Add("endtime", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    noFilter = false;
                }

                string radio = radioButton1.Checked.ToString().ToLower();
                p.Add("radio", radio);
                if (radio.ToLower() == "true")
                    noFilter = false;

                string xsddzt = cb_xsddzt.SelectedValue.ToString();
                p.Add("xsddzt", xsddzt);
                if (!string.IsNullOrEmpty(xsddzt))
                    noFilter = false;

                string zb = textBox5.Text;
                p.Add("zb", zb);
                if (!string.IsNullOrEmpty(zb))
                    noFilter = false;

                string inspection_state = comboBox3.SelectedValue.ToString().Trim();//验货状态
                if (!string.IsNullOrEmpty(inspection_state))
                {
                    noFilter = false;
                    p.Add("inspection_state", inspection_state);
                }
                //if (Convert.ToInt32(pageControl1.cb_size.Text) > 500)
                //    pageControl1.cb_size.SelectedIndex = 4;

                p.Add("noFilter", noFilter);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string curr_noFilter = p["noFilter"].ToString();
                int curr_pageSize = Convert.ToInt32(p["pageSize"].ToString());
                int curr_pageIndex = Convert.ToInt32(p["pageIndex"].ToString());

                if (noFilter)
                {//无过滤条件走 旧逻辑
                    p_temp = new Dictionary<string, object>();
                    Application.DoEvents();//转让控制权
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_AQLAPI",//类库名
                                                "SJ_AQLAPI.AQL_FinishedProduct_Information",//类名
                                                "GetFinishedProduct_Information_Main_I",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }

                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    //视图数据显示

                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    Dictionary<string, Dictionary<string, string>> p_chzt = new Dictionary<string, Dictionary<string, string>>();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(dr["SE_ID"].ToString()))
                            {
                                if (!p_chzt.ContainsKey(dr["SE_ID"].ToString()))
                                {
                                    Dictionary<string, string> useValue = new Dictionary<string, string>();
                                    useValue.Add("se_qty", dr["se_qty"].ToString());
                                    useValue.Add("column2", dr["column2"].ToString());
                                    p_chzt.Add(dr["SE_ID"].ToString(), useValue);
                                }
                            }
                        }
                    }
                    totalCount = int.Parse(dic["rowCount"].ToString());

                    #region 动态加载出货状态
                    if (p_chzt.Count > 0)
                    {
                        //string loadingInfo = SJeMES_Framework.Common.UIHelper.UImsg("开始加载出货状态", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        //SJeMES_Control_Library.MessageHelper.ShowSuccess(this, loadingInfo);

                        int totalCountCHZT = p_chzt.Count;
                        int batchCount = 500;
                        int pgCount = 0;//进度数
                        int successCount = 0;
                        int failCount = 0;
                        string errMsg = "";

                        SetProgressBarCount(0, p_chzt.Count);
                        this.pb_loading.Visible = true;
                        //SjeMES_QCM_Ex.ProgressBar chztProgressBar = new SjeMES_QCM_Ex.ProgressBar(0, p_chzt.Count);
                        //chztProgressBar.Show();

                        Dictionary<string, string> chzt_dic_total = new Dictionary<string, string>();
                        while (true)
                        {
                            int canTakeCount = 0;
                            if ((pgCount + batchCount) > totalCountCHZT)
                            {
                                canTakeCount = batchCount - ((pgCount + batchCount) - totalCountCHZT);
                            }
                            else
                                canTakeCount = batchCount;

                            var curr_p_chzt = p_chzt.Skip(pgCount).Take(canTakeCount).ToDictionary(x => x.Key, y => y.Value);
                            string retdata_chzt = WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_AQLAPI",//类库名
                                                    "SJ_AQLAPI.AQL_CmaTask_Inspection",//类名
                                                    "GetCmaTask_TaskList_Main_CHZT_I",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(curr_p_chzt));
                            ResultObject ret_chzt = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata_chzt);

                            if (!ret_chzt.IsSuccess)
                            {
                                failCount += canTakeCount;
                                errMsg += ret_chzt.ErrMsg;
                            }
                            else
                            {
                                Dictionary<string, string> chzt_dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(ret_chzt.RetData);

                                foreach (var item in chzt_dic)
                                {
                                    if (!chzt_dic_total.ContainsKey(item.Key))
                                    {
                                        chzt_dic_total.Add(item.Key, item.Value);
                                    }
                                }
                                successCount += canTakeCount;
                            }

                            pgCount += canTakeCount;
                            //启动进度条
                            System.Threading.Thread.Sleep(100);
                            StartProgressBar(pgCount);
                            //chztProgressBar.StartProgressBar(pgCount);

                            if (pgCount == totalCountCHZT)
                                break;
                        }

                        foreach (DataRow item in dt.Rows)
                        {
                            string se_id = item["SE_ID"].ToString();
                            if (chzt_dic_total.ContainsKey(se_id))
                            {
                                item["chzt"] = chzt_dic_total[se_id];
                            }
                            else
                                item["chzt"] = "未出货";
                        }
                        pb_loading.Visible = false;
                        //chztProgressBar.Close();
                    }
                    #endregion
                    totalCount = dt.Rows.Count;


                    //出货状态不为空，过滤
                    string filter_dt_str = $@"1=1 ";
                    if (!string.IsNullOrEmpty(cb_chzt.Text))
                    {
                        filter_dt_str += $@" and chzt = '{cb_chzt.Text}' ";
                    }

                    var filter_dt = dt.Select(filter_dt_str).Skip((curr_pageIndex - 1) * curr_pageSize).Take(curr_pageSize).ToList();
                    dataGridViewEx1.Rows.Clear();
                    if (filter_dt != null && filter_dt.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in filter_dt)
                        {
                            dataGridViewEx1.Rows.Add();
                            DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                            dgvr.Cells["序号"].Value = i + 1;

                            dgvr.Cells["组织"].Value = dr["org_id"].ToString();
                            dgvr.Cells["统计日期"].Value = dr["SYSDATE"].ToString();
                            dgvr.Cells["鞋型名称"].Value = dr["shoe_name"].ToString();
                            dgvr.Cells["ART"].Value = dr["PROD_NO"].ToString();
                            dgvr.Cells["销售订单号"].Value = dr["SE_ID"].ToString();
                            dgvr.Cells["PO号"].Value = dr["mer_PO"].ToString();
                            dgvr.Cells["订单总量"].Value = dr["se_QTY"].ToString();
                            dgvr.Cells["订单总箱数"].Value = dr["owe_ctn_qty"].ToString();
                            dgvr.Cells["库存双数"].Value = dr["stoc_pairs"].ToString();
                            dgvr.Cells["库存箱数"].Value = dr["ctn_qty"].ToString();
                            dgvr.Cells["满箱欠双数"].Value = dr["owe_pairs"].ToString();
                            dgvr.Cells["所欠箱数"].Value = dr["se_ctn_qty"].ToString();
                            dgvr.Cells["订单满箱率"].Value = dr["full_rate"].ToString();
                            dgvr.Cells["各组别生产双数"].Value = dr["fromline_qty"].ToString();
                            dgvr.Cells["仓库存放位置"].Value = dr["location_qty"].ToString();
                            dgvr.Cells["计划出货日期"].Value = dr["nst"].ToString();
                            dgvr.Cells["实际出货日期"].Value = dr["POSTING_DATE"].ToString();
                            dgvr.Cells["CRD"].Value = dr["CR_REQDATE"].ToString();
                            dgvr.Cells["最早扫描时间"].Value = dr["insert_date"].ToString();
                            dgvr.Cells["最后扫描时间"].Value = dr["last_date"].ToString();
                            dgvr.Cells["满箱率"].Value = dr["full_rate"].ToString();
                            dgvr.Cells["测钉"].Value = dr["nail_pz"].ToString();
                            if (dr["inspection_state"].ToString() == "0")
                                dgvr.Cells["验货日期"].Value = "";
                            else if (dr["inspection_state"].ToString() == "1")
                                dgvr.Cells["验货日期"].Value = dr["inspection_date"].ToString();
                            else
                                dgvr.Cells["验货日期"].Value = "";
                            dgvr.Cells["验货日期"].Value = dr["inspection_date"].ToString();
                            dgvr.Cells["满箱日期"].Value = dr["a_date"].ToString();
                            dgvr.Cells["重新验货"].Value = dr["recheck_pz"].ToString();
                            dgvr.Cells["出货国家"].Value = dr["SHIPCOUNTRY_NAME"].ToString();
                            dgvr.Cells["出货状态"].Value = dr["chzt"].ToString();


                            if (dr["full_rate"].ToString().StartsWith("100"))
                            {
                                dgvr.Cells["订单满箱率"].Style.BackColor = Color.Blue;
                                dgvr.Cells["满箱日期"].Style.BackColor = Color.Blue;
                            }
                            else
                            {
                                dgvr.Cells["订单满箱率"].Style.BackColor = Color.Yellow;
                            }
                            if (!string.IsNullOrEmpty(dgvr.Cells["验货日期"].Value.ToString()))
                            {
                                dgvr.Cells["验货日期"].Style.BackColor = Color.Green;
                            }
                            if (dr["recheck_pz"].ToString().ToLower() == "y")
                            {
                                dgvr.Cells["重新验货"].Style.BackColor = Color.Red;
                            }

                            i++;
                            Application.DoEvents();//转让控制权
                        }
                        dataGridViewEx1.ClearSelection();
                    }
                }
                else
                {//有过滤条件 走新逻辑
                    bool reSearch = true;//是否重查

                    p.Remove("noFilter");
                    p.Remove("pageSize");
                    p.Remove("pageIndex");
                    if (p_temp.Count() == 0)
                    {
                        p_temp = new Dictionary<string, object>(p);
                    }
                    else
                    {
                        if (p.Count != p_temp.Count)
                        {
                            p_temp = new Dictionary<string, object>(p);
                        }
                        else
                        {
                            string old_p_json = Newtonsoft.Json.JsonConvert.SerializeObject(p_temp);
                            string new_p_json = Newtonsoft.Json.JsonConvert.SerializeObject(p);
                            if (new_p_json != old_p_json)
                            {
                                p_temp = new Dictionary<string, object>(p);
                            }
                            else
                            {
                                reSearch = false;
                            }
                        }
                    }

                    if (reSearch)
                    {//搜索条件变化，需调接口，重查缓存的dt
                        p.Add("noFilter", noFilter);
                        p.Add("pageSize", pageSize);
                        p.Add("pageIndex", pageIndex);
                        Application.DoEvents();//转让控制权
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_AQLAPI",//类库名
                                                    "SJ_AQLAPI.AQL_FinishedProduct_Information",//类名
                                                    "GetFinishedProduct_Information_Main_I",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));

                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                        if (!ret.IsSuccess)
                        {
                            throw new Exception(ret.ErrMsg);
                        }

                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        //视图数据显示

                        var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                        Dictionary<string, Dictionary<string, string>> p_chzt = new Dictionary<string, Dictionary<string, string>>();
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!string.IsNullOrEmpty(dr["SE_ID"].ToString()))
                                {
                                    if (!p_chzt.ContainsKey(dr["SE_ID"].ToString()))
                                    {
                                        Dictionary<string, string> useValue = new Dictionary<string, string>();
                                        useValue.Add("se_qty", dr["se_qty"].ToString());
                                        useValue.Add("column2", dr["column2"].ToString());
                                        p_chzt.Add(dr["SE_ID"].ToString(), useValue);
                                    }
                                }
                            }
                        }

                        #region 加载此dt的出货状态
                        if (p_chzt.Count > 0)
                        {

                            int totalCountCHZT = p_chzt.Count;
                            int batchCount = 500;
                            int pgCount = 0;//进度数
                            int successCount = 0;
                            int failCount = 0;
                            string errMsg = "";

                            SetProgressBarCount(0, p_chzt.Count);
                            this.pb_loading.Visible = true;
                            //SjeMES_QCM_Ex.ProgressBar chztProgressBar = new SjeMES_QCM_Ex.ProgressBar(0, p_chzt.Count);
                            //chztProgressBar.Show();

                            Dictionary<string, string> chzt_dic_total = new Dictionary<string, string>();
                            while (true)
                            {
                                int canTakeCount = 0;
                                if ((pgCount + batchCount) > totalCountCHZT)
                                {
                                    canTakeCount = batchCount - ((pgCount + batchCount) - totalCountCHZT);
                                }
                                else
                                    canTakeCount = batchCount;

                                var curr_p_chzt = p_chzt.Skip(pgCount).Take(canTakeCount).ToDictionary(x => x.Key, y => y.Value);
                                string retdata_chzt = WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJ_AQLAPI",//类库名
                                                        "SJ_AQLAPI.AQL_CmaTask_Inspection",//类名
                                                        "GetCmaTask_TaskList_Main_CHZT_I",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(curr_p_chzt));
                                ResultObject ret_chzt = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata_chzt);

                                if (!ret_chzt.IsSuccess)
                                {
                                    failCount += canTakeCount;
                                    errMsg += ret_chzt.ErrMsg;
                                }
                                else
                                {
                                    Dictionary<string, string> chzt_dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(ret_chzt.RetData);

                                    foreach (var item in chzt_dic)
                                    {
                                        if (!chzt_dic_total.ContainsKey(item.Key))
                                        {
                                            chzt_dic_total.Add(item.Key, item.Value);
                                        }
                                    }
                                    successCount += canTakeCount;
                                }

                                pgCount += canTakeCount;
                                //启动进度条
                                System.Threading.Thread.Sleep(100);
                                StartProgressBar(pgCount);
                                //chztProgressBar.StartProgressBar(pgCount);

                                if (pgCount == totalCountCHZT)
                                    break;
                            }

                            foreach (DataRow item in dt.Rows)
                            {
                                string se_id = item["SE_ID"].ToString();
                                if (chzt_dic_total.ContainsKey(se_id))
                                {
                                    item["chzt"] = chzt_dic_total[se_id];
                                }
                                else
                                    item["chzt"] = "未出货";
                            }
                            pb_loading.Visible = false;
                            //chztProgressBar.Close();
                        }
                        #endregion

                        dt_temp = dt;
                    }

                    string filter_dt_str = $@"1=1 ";
                    if (!string.IsNullOrEmpty(cb_chzt.Text))
                    {
                        filter_dt_str += $@" and chzt = '{cb_chzt.Text}' ";
                    }

                    if (dt_temp != null && dt_temp.Rows.Count > 0)
                    {
                        totalCount = dt_temp.Rows.Count;

                        var filter_dt = dt_temp.Select(filter_dt_str).ToList();
                        dataGridViewEx1.Rows.Clear();
                        if (filter_dt != null && filter_dt.Count() > 0)
                        {
                            totalCount = filter_dt.Count();
                            int i = 0;
                            foreach (var dr in filter_dt.Skip((curr_pageIndex - 1) * curr_pageSize).Take(curr_pageSize).ToList())
                            {
                                dataGridViewEx1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                                dgvr.Cells["序号"].Value = i + 1;

                                dgvr.Cells["组织"].Value = dr["org_id"].ToString();
                                dgvr.Cells["统计日期"].Value = dr["SYSDATE"].ToString();
                                dgvr.Cells["鞋型名称"].Value = dr["shoe_name"].ToString();
                                dgvr.Cells["ART"].Value = dr["PROD_NO"].ToString();
                                dgvr.Cells["销售订单号"].Value = dr["SE_ID"].ToString();
                                dgvr.Cells["PO号"].Value = dr["mer_PO"].ToString();
                                dgvr.Cells["订单总量"].Value = dr["se_QTY"].ToString();
                                dgvr.Cells["订单总箱数"].Value = dr["owe_ctn_qty"].ToString();
                                dgvr.Cells["库存双数"].Value = dr["stoc_pairs"].ToString();
                                dgvr.Cells["库存箱数"].Value = dr["ctn_qty"].ToString();
                                dgvr.Cells["满箱欠双数"].Value = dr["owe_pairs"].ToString();
                                dgvr.Cells["所欠箱数"].Value = dr["se_ctn_qty"].ToString();
                                dgvr.Cells["订单满箱率"].Value = dr["full_rate"].ToString();
                                dgvr.Cells["各组别生产双数"].Value = dr["fromline_qty"].ToString();
                                dgvr.Cells["仓库存放位置"].Value = dr["location_qty"].ToString();
                                dgvr.Cells["计划出货日期"].Value = dr["nst"].ToString();
                                dgvr.Cells["实际出货日期"].Value = dr["POSTING_DATE"].ToString();
                                dgvr.Cells["CRD"].Value = dr["CR_REQDATE"].ToString();
                                dgvr.Cells["最早扫描时间"].Value = dr["insert_date"].ToString();
                                dgvr.Cells["最后扫描时间"].Value = dr["last_date"].ToString();
                                dgvr.Cells["满箱率"].Value = dr["full_rate"].ToString();
                                dgvr.Cells["测钉"].Value = dr["nail_pz"].ToString(); 
                                if (dr["inspection_state"].ToString() == "0")
                                    dgvr.Cells["验货日期"].Value = "";
                                else if (dr["inspection_state"].ToString() == "1")
                                    dgvr.Cells["验货日期"].Value = dr["inspection_date"].ToString();
                                else
                                    dgvr.Cells["验货日期"].Value = "";
                                dgvr.Cells["满箱日期"].Value = dr["a_date"].ToString();
                                dgvr.Cells["重新验货"].Value = dr["recheck_pz"].ToString();
                                dgvr.Cells["出货国家"].Value = dr["SHIPCOUNTRY_NAME"].ToString();
                                dgvr.Cells["出货状态"].Value = dr["chzt"].ToString();


                                if (dr["full_rate"].ToString().StartsWith("100"))
                                {
                                    dgvr.Cells["订单满箱率"].Style.BackColor = Color.Blue;
                                    dgvr.Cells["满箱日期"].Style.BackColor = Color.Blue;
                                }
                                else
                                {
                                    dgvr.Cells["订单满箱率"].Style.BackColor = Color.Yellow;
                                }
                                if (!string.IsNullOrEmpty(dgvr.Cells["验货日期"].Value.ToString()))
                                {
                                    dgvr.Cells["验货日期"].Style.BackColor = Color.Green;
                                }
                                if (dr["recheck_pz"].ToString().ToLower() == "y")
                                {
                                    dgvr.Cells["重新验货"].Style.BackColor = Color.Red;
                                }

                                i++;
                                Application.DoEvents();//转让控制权
                            }
                            dataGridViewEx1.ClearSelection();
                        }
                        else
                        {
                            totalCount = 0;
                        }
                    }
                    else
                    {
                        dataGridViewEx1.Rows.Clear();
                        totalCount = 0;
                    }
                }

                

                this.Enabled = true;
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void SetProgressBarCount(int minValue, int maxValue)
        {
            pb_loading.Minimum = minValue;
            pb_loading.Maximum = maxValue;
        }

        public void StartProgressBar(int value)
        {
            if (pb_loading == null) return;
            Application.DoEvents();
            pb_loading.Value = value;
            //decimal tmp = Math.Round(Convert.ToDecimal(value) / Convert.ToDecimal(pb_loading.Maximum), 2) * 100;
            ////txt_num.Text = tmp + "%__" + value + "/" + progressBar1.Maximum;
            //txt_num.Text = tmp + "%";
            //valueres = value;
            //txt_num.Refresh();
            pb_loading.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_DoubleClick(object sender, EventArgs e)
        {
            F_AQL_FromLine frm = new F_AQL_FromLine(textBox5.Text);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.selectlist.Count > 0)
            {
                string fromline = "";
                foreach (var item in frm.selectlist)
                {
                    fromline += item["fromline"].ToString() + ",";
                }
                textBox5.Text = fromline.Trim(',');
            }
        }
    }
}
