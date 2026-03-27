using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_AQL.AQL_FrmBase;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using SJeMES_Shared_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SJeMES_IQC.F_IQC_VWarehouse_Main;

namespace SJeMES_AQL
{
    public partial class F_AQL_CmaTask_Inspection_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        List<inspection_state> insplist = new List<inspection_state>();
        public F_AQL_CmaTask_Inspection_Main()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimePicker1);
            InitDateTimePicker(dateTimePicker2);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "   ";

            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "   ";

            GetAutographSetting();

            #region 验货状态
            inspection_state i3 = new inspection_state();
            i3.code = "";
            i3.value = "";
            insplist.Add(i3);
            inspection_state i1 = new inspection_state();
            i1.code = "0";
            i1.value = "Not_Inspected";//未验货
            insplist.Add(i1);
            inspection_state i2 = new inspection_state();
            i2.code = "1";
            i2.value = "Checked";//已验货
            insplist.Add(i2);
            comboBox3.DataSource = insplist;
            comboBox3.DisplayMember = "value";
            comboBox3.ValueMember = "code";
            #endregion

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

        /// <summary>
        /// 查询-验货室订单查询
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetCmaTask_Inspection_Main(int pageSize, int pageIndex, out int totalCount)
        {
            this.Enabled = false;
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("sccq", textBox1.Text.Trim());
                p.Add("po", textBox2.Text.Trim());
                p.Add("shoe_name", textBox4.Text.Trim());
                p.Add("art_name", textBox3.Text.Trim());
                p.Add("zhubie", textBox5.Text.Trim());
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    p.Add("f_inspection_timeS", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    p.Add("f_inspection_timeE", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                }
                p.Add("guojia", textBox6.Text.Trim());
                p.Add("chzt", cb_chzt.Text.Trim());
                p.Add("inspection_state", comboBox3.SelectedValue.ToString());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                Application.DoEvents();//转让控制权
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CmaTask_Inspection",//类名
                                            "GetCmaTask_Inspection_Main",//方法名
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
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                var aql_clear_autograph = dic["aql_clear_autograph"].ToString();
                if (aql_clear_autograph == "1")
                {
                    dataGridViewEx1.Columns["清除数据"].Visible = true;
                }
                else
                {
                    dataGridViewEx1.Columns["清除数据"].Visible = false;
                }
                dataGridViewEx1.Rows.Clear();
                Dictionary<string, Dictionary<string, string>> p_chzt = new Dictionary<string, Dictionary<string, string>>();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
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
                        dataGridViewEx1.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                        dgvr.Cells["SE_ID"].Value = dr["SE_ID"].ToString();
                        dgvr.Cells["column2"].Value = dr["column2"].ToString();
                        dgvr.Cells["se_qty"].Value = dr["se_qty"].ToString();
                        dgvr.Cells["aid"].Value = dr["id"].ToString();
                        dgvr.Cells["序号"].Value = i + 1;
                        dgvr.Cells["任务编号"].Value = dr["task_no"].ToString();
                        dgvr.Cells["生产厂区"].Value = dr["sccq"].ToString();
                        dgvr.Cells["PO"].Value = dr["po"].ToString();
                        dgvr.Cells["ART"].Value = dr["art_no"].ToString();
                        dgvr.Cells["鞋型"].Value = dr["shoe_name"].ToString();
                        dgvr.Cells["标签"].Value = dr["bq"].ToString();
                        dgvr.Cells["客户"].Value = dr["kh"].ToString();
                        dgvr.Cells["国家"].Value = dr["guojia"].ToString();
                        dgvr.Cells["PO数量"].Value = dr["po_num"].ToString();
                        dgvr.Cells["分批数量"].Value = dr["lot_num"].ToString();
                        dgvr.Cells["鞋型编号"].Value = dr["SHOE_NO"].ToString();

                        //----制令级别
                        if (dr["order_level"].ToString() == "0")
                            dgvr.Cells["制令级别"].Value = "Order";//大制令
                        else if (dr["order_level"].ToString() == "1")
                            dgvr.Cells["制令级别"].Value = "Small orders";//小制令
                        else
                            dgvr.Cells["制令级别"].Value = "";
                        
                        //----满箱状态
                        //if (dr["full_state"].ToString() == "0")
                        //    dgvr.Cells["满箱状态"].Value = "已满箱";
                        //else if (dr["full_state"].ToString() == "1")
                        //    dgvr.Cells["满箱状态"].Value = "满3200箱";
                        //else
                        //    dgvr.Cells["满箱状态"].Value = "";

                        dgvr.Cells["满箱状态"].Value = dr["full_state"].ToString();

                        //----验货状态
                        if (dr["inspection_state"].ToString() == "0")
                            dgvr.Cells["验货状态"].Value = "Not_Inspected";//未验货
                        else if (dr["inspection_state"].ToString() == "1")
                            dgvr.Cells["验货状态"].Value = "Checked";//已验货
                        else
                            dgvr.Cells["验货状态"].Value = "";

                        dgvr.Cells["inspection_results"].Value = "";
                        //已验货才赋值
                        if (dr["inspection_state"].ToString() == "1")
                        {
                            dgvr.Cells["inspection_results"].Value = dr["inspection_results"].ToString();

                            dgvr.Cells["首次验货日期"].Value = dr["INSPECTION_DATE"].ToString();
                            //dgvr.Cells["首次验货日期"].Value = dr["f_inspection_time"].ToString();
                        }

                        //0：已出货，1：未出货，2：订单取消，3：订单替换，4：订单减少，5：分批出货
                        dgvr.Cells["出货状态"].Value = dr["chzt"].ToString();
                        //switch (dr["chzt"].ToString())
                        //{
                        //    case "0":
                        //        dgvr.Cells["出货状态"].Value = "已出货";
                        //        break;
                        //    case "1":
                        //        dgvr.Cells["出货状态"].Value = "未出货";
                        //        break;
                        //    case "2":
                        //        dgvr.Cells["出货状态"].Value = "订单取消";
                        //        break;
                        //    case "3":
                        //        dgvr.Cells["出货状态"].Value = "订单替换";
                        //        break;
                        //    case "4":
                        //        dgvr.Cells["出货状态"].Value = "订单减少";
                        //        break;
                        //    case "5":
                        //        dgvr.Cells["出货状态"].Value = "分批出货";
                        //        break;
                        //    default:
                        //        dgvr.Cells["出货状态"].Value = "未出货";
                        //        break;
                        //}
                        if (dr["IS_INSPECTION"].ToString() == "1")
                            dgvr.Cells["首次验货日期"].Value = dr["INSPECTION_DATE"].ToString();
                        dgvr.Cells["工厂代表签名"].Value = dr["factory_autograph"].ToString();
                        dgvr.Cells["客户签名"].Value = dr["customer_autograph"].ToString();
                        dgvr.Cells["factory_autograph_date"].Value = dr["factory_autograph_date"].ToString();
                        dgvr.Cells["customer_autograph_date"].Value = dr["customer_autograph_date"].ToString();
                        if (dr["task_type"].ToString() == "0")
                        {
                            dgvr.Cells["任务类型"].Value = "Automatic_Generated";//自动生成
                        }
                        else if (dr["task_type"].ToString() == "1")
                        {
                            dgvr.Cells["任务类型"].Value = "Manually_Created";//手动创建
                        }
                        dgvr.Cells["点箱完成状态"].Value = dr["pb_state"].ToString();
                        //dgvr.Cells["inspection_results"].Value = dr["inspection_results"].ToString();
                        //检验类型 0：最终 1：翻箱 2：再次 3：再次翻箱
                        string inspection_type_str = "";
                        switch (dr["inspection_type"].ToString())
                        {
                            case "0":
                                inspection_type_str = "Finally";//最终
                                break;
                            case "1":
                                inspection_type_str = "Rummage";//翻箱
                                break;
                            case "2":
                                inspection_type_str = "Again";//再次
                                break;
                            case "3":
                                inspection_type_str = "Rummage_Again";//再次翻箱
                                break;
                            default:
                                break;
                        }
                        dgvr.Cells["inspection_type"].Value = inspection_type_str;
                        //生效状态  0：生效 1：失效
                        string effective_status_str = "";
                        switch (dr["effective_status"].ToString())
                        {
                            case "0":
                                effective_status_str = "Take_Effect";//生效
                                break;
                            case "1":
                                effective_status_str = "Fail";//失效
                                break;
                            default:
                                break;
                        }
                        dgvr.Cells["effective_status"].Value = effective_status_str;
                        dgvr.Cells["warning"].Value = dr["warning"].ToString();

                        dgvr.Cells["vas"].Value = dr["vas"].ToString();
                        dgvr.Cells["BA_EDIT_STATE"].Value = dr["BA_EDIT_STATE"].ToString();
                        dgvr.Cells["H_EDIT_STATE"].Value = dr["H_EDIT_STATE"].ToString();
                        dgvr.Cells["AQL_EDIT_STATE"].Value = dr["AQL_EDIT_STATE"].ToString();
                        dgvr.Cells["PH_EDIT_STATE"].Value = dr["PH_EDIT_STATE"].ToString();

                        dgvr.Cells["rule_no"].Value = dr["rule_no"].ToString();
                        dgvr.Cells["from_line"].Value = dr["from_line"].ToString();
                        dgvr.Cells["CHECKER"].Value = dr["CHECKER"].ToString();
                        i++;
                        Application.DoEvents();//转让控制权
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridViewEx1.ClearSelection();

                #region 动态加载出货状态
                if (p_chzt.Count > 0)
                {
                    //string loadingInfo = SJeMES_Framework.Common.UIHelper.UImsg("开始加载出货状态", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    //SJeMES_Control_Library.MessageHelper.ShowSuccess(this, loadingInfo);

                    int totalCountCHZT = p_chzt.Count;
                    int batchCount = 50;
                    int pgCount = 0;//进度数
                    int successCount = 0;
                    int failCount = 0;
                    string errMsg = "";

                    SetProgressBarCount(0, p_chzt.Count);
                    this.pb_loading.Visible = true;
                    //SjeMES_QCM_Ex.ProgressBar chztProgressBar = new SjeMES_QCM_Ex.ProgressBar(0, p_chzt.Count);
                    //chztProgressBar.ShowDialog();

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

                    foreach (DataGridViewRow item in dataGridViewEx1.Rows)
                    {
                        string se_id = item.Cells["SE_ID"].Value.ToString();
                        if (chzt_dic_total.ContainsKey(se_id))
                        {
                            item.Cells["出货状态"].Value = chzt_dic_total[se_id];
                        }
                        else
                            item.Cells["出货状态"].Value = "Not_Shipped";//未出货
                    }
                    pb_loading.Visible = false;
                    //chztProgressBar.Close();
                }

                //出货状态不为空，过滤
                if (!string.IsNullOrEmpty(cb_chzt.Text))
                {
                    var fRows = dataGridViewEx1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["出货状态"].Value.ToString().Equals(cb_chzt.Text)).ToArray();
                    dataGridViewEx1.Rows.Clear();
                    if (fRows != null && fRows.Length > 0)
                    {
                        foreach (var add_row in fRows)
                        {
                            dataGridViewEx1.Rows.Add(add_row);
                        }
                    }
                }
                #endregion

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

        public void GetAutographSetting()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CmaTask_Inspection",//类名
                                            "GetAutographSetting",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    if (row["FACTORY_AUTOGRAPH"].ToString() == "1")
                    {
                        button1.Visible = true;
                    }
                    if (row["CUSTOMER_AUTOGRAPH"].ToString() == "1")
                    {
                        button2.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
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

        private void F_AQL_CmaTask_Inspection_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            pageControl1.BindPageEvent += GetCmaTask_Inspection_Main;
            LoadPage();
            this.dataGridViewEx1.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewEx1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "录入报告")
                {

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("task_no", dataGridViewEx1.Rows[e.RowIndex].Cells["任务编号"].Value);
                    dic.Add("po", dataGridViewEx1.Rows[e.RowIndex].Cells["PO"].Value);
                    dic.Add("shoe_no", dataGridViewEx1.Rows[e.RowIndex].Cells["鞋型编号"].Value);
                    dic.Add("shoe_name", dataGridViewEx1.Rows[e.RowIndex].Cells["鞋型"].Value);
                    dic.Add("kehu", dataGridViewEx1.Rows[e.RowIndex].Cells["客户"].Value);
                    dic.Add("guojia", dataGridViewEx1.Rows[e.RowIndex].Cells["国家"].Value);
                    dic.Add("queren", "");
                    dic.Add("art", dataGridViewEx1.Rows[e.RowIndex].Cells["ART"].Value);
                    dic.Add("num", dataGridViewEx1.Rows[e.RowIndex].Cells["分批数量"].Value);
                    dic.Add("vas", dataGridViewEx1.Rows[e.RowIndex].Cells["vas"].Value);
                    dic.Add("task_type", dataGridViewEx1.Rows[e.RowIndex].Cells["任务类型"].Value);
                    dic.Add("pb_state", dataGridViewEx1.Rows[e.RowIndex].Cells["点箱完成状态"].Value);
                    dic.Add("effective_status", dataGridViewEx1.Rows[e.RowIndex].Cells["effective_status"].Value);
                    dic.Add("BA_EDIT_STATE", dataGridViewEx1.Rows[e.RowIndex].Cells["BA_EDIT_STATE"].Value);
                    dic.Add("H_EDIT_STATE", dataGridViewEx1.Rows[e.RowIndex].Cells["H_EDIT_STATE"].Value);
                    dic.Add("AQL_EDIT_STATE", dataGridViewEx1.Rows[e.RowIndex].Cells["AQL_EDIT_STATE"].Value);
                    dic.Add("PH_EDIT_STATE", dataGridViewEx1.Rows[e.RowIndex].Cells["PH_EDIT_STATE"].Value);
                    dic.Add("rule_no", dataGridViewEx1.Rows[e.RowIndex].Cells["rule_no"].Value);
                    dic.Add("from_line", dataGridViewEx1.Rows[e.RowIndex].Cells["from_line"].Value);
                    dic.Add("CHECKER", dataGridViewEx1.Rows[e.RowIndex].Cells["CHECKER"].Value);
                    dic.Add("factory_autograph", dataGridViewEx1.Rows[e.RowIndex].Cells["工厂代表签名"].Value);
                    dic.Add("factory_autograph_date", dataGridViewEx1.Rows[e.RowIndex].Cells["factory_autograph_date"].Value);
                    dic.Add("customer_autograph", dataGridViewEx1.Rows[e.RowIndex].Cells["客户签名"].Value);
                    dic.Add("customer_autograph_date", dataGridViewEx1.Rows[e.RowIndex].Cells["customer_autograph_date"].Value);

                    string frmName = $@"F_AQL_CheckthedataMAX_{dataGridViewEx1.Rows[e.RowIndex].Cells["任务编号"].Value}";
                    var findFrm = Application.OpenForms[frmName];
                    if (findFrm == null)
                    {
                        F_AQL_CheckthedataMAX a = new F_AQL_CheckthedataMAX(0, dic, 1);
                        a.Name = frmName;
                        a.Show();
                    }
                    else
                    {
                        findFrm.Activate();
                    }
                    //using (F_AQL_CheckthedataMAX a = new F_AQL_CheckthedataMAX(0, dic))
                    //{
                    //    a.ShowDialog();
                    //}
                    //this.F_AQL_CmaTask_Inspection_Main_Load(null, null);
                }
                if(dataGridViewEx1.Columns[e.ColumnIndex].Name == "查看报告")
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("task_no", dataGridViewEx1.Rows[e.RowIndex].Cells["任务编号"].Value.ToString());
                    dic.Add("po", dataGridViewEx1.Rows[e.RowIndex].Cells["PO"].Value.ToString());
                    dic.Add("num", dataGridViewEx1.Rows[e.RowIndex].Cells["PO数量"].Value.ToString());
                    dic.Add("fpnum", dataGridViewEx1.Rows[e.RowIndex].Cells["分批数量"].Value.ToString());
                    dic.Add("yhstatus", dataGridViewEx1.Rows[e.RowIndex].Cells["验货状态"].Value.ToString());

                    string frmName = $@"F_AQL_Aqlreport_New_{dataGridViewEx1.Rows[e.RowIndex].Cells["任务编号"].Value}";
                    var findFrm = Application.OpenForms[frmName];
                    if (findFrm == null)
                    {
                        F_AQL_Aqlreport_New a = new F_AQL_Aqlreport_New(dic, Program.Client);
                        a.Name = frmName;
                        a.Show();
                    }
                    else
                    {
                        findFrm.Activate();
                    }
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "成品仓信息")
                {
                    string frmName = $@"F_AQL_FinishedProduct_Information_I_{dataGridViewEx1.Rows[e.RowIndex].Cells["任务编号"].Value}";
                    var findFrm = Application.OpenForms[frmName];
                    if (findFrm == null)
                    {
                        F_AQL_FinishedProduct_Information_I a = new F_AQL_FinishedProduct_Information_I();
                        a.Name = frmName;
                        a.Show();
                    }
                    else
                    {
                        findFrm.Activate();
                    }
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "订单明细")
                {

                    string frmName = $@"F_AQL_Order_Detail_{dataGridViewEx1.Rows[e.RowIndex].Cells["任务编号"].Value}";
                    var findFrm = Application.OpenForms[frmName];
                    if (findFrm == null)
                    {
                        F_AQL_Order_Detail a = new F_AQL_Order_Detail();
                        a.Name = frmName;
                        a.Show();
                    }
                    else
                    {
                        findFrm.Activate();
                    }
                    dataGridViewEx1.ClearSelection();
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "清除数据")
                {
                    DialogResult dr = MessageBox.Show("确认要清除数据吗!", "清除AQL任务清单", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (dr == DialogResult.OK)
                    {
                        string task_no = dataGridViewEx1.Rows[e.RowIndex].Cells["任务编号"].Value.ToString();
                        string PO = dataGridViewEx1.Rows[e.RowIndex].Cells["PO"].Value.ToString();
                        DelelteCmaTask_TaskList_All(task_no, PO);
                    }
                }
            }
        }

        /// <summary>
        /// 删除-AQL验货任务-清除数据
        /// </summary>
        public void DelelteCmaTask_TaskList_All(string task_no, string po)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", task_no);
                data.Add("po", po);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_CmaTask_TaskList", "DelelteCmaTask_TaskList_All", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    MessageBox.Show("清除成功!");
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// dgv控件转datatable
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        //工厂代表签名
        private void button1_Click(object sender, EventArgs e)
        {
            bool b = false;
            foreach (DataRow item in GetDgvToTable(dataGridViewEx1).Rows)
            {
                if (item["xz"].ToString() == "True")
                {
                    b = true;
                }
            }
            if (b)
            {
                using (F_AQL_CmaTask_Inspection_Signature a = new F_AQL_CmaTask_Inspection_Signature(GetDgvToTable(dataGridViewEx1), "0"))
                {
                    a.Text = "Signature_of_factory_representative";//工厂代表签名
                    a.ShowDialog();
                }
                LoadPage();
            }
            else
            {
                MessageBox.Show("Please check first!");
                return;
            }
        }

        //客户签名
        private void button2_Click(object sender, EventArgs e)
        {
            bool b = false;
            foreach (DataRow item in GetDgvToTable(dataGridViewEx1).Rows)
            {
                if (item["xz"].ToString() == "True")
                {
                    b = true;
                }
            }
            if (b)
            {
                using (F_AQL_CmaTask_Inspection_Signature a = new F_AQL_CmaTask_Inspection_Signature(GetDgvToTable(dataGridViewEx1), "1"))
                {
                    a.Text = "customer signature";
                    a.ShowDialog();
                }
                LoadPage();
            }
            else
            {
                MessageBox.Show("Please check first!");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            F_AQL_CmaTask_Inspection_dxPrint ff = new F_AQL_CmaTask_Inspection_dxPrint("");
            ff.Show();
        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
