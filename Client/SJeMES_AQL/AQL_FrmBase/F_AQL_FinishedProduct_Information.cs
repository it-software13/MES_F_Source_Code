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
    public partial class F_AQL_FinishedProduct_Information : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        List<inspection_state> insplist = new List<inspection_state>();
        public bool _enable;
        public F_AQL_FinishedProduct_Information(string mer_po,bool enable)
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

            this.textBox2.Text = mer_po;
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

                string mer_po = textBox2.Text.Trim();
                p.Add("mer_po", mer_po);
                if (!string.IsNullOrEmpty(mer_po))
                    noFilter = false;

                string sccq = textBox1.Text.Trim();
                p.Add("sccq", sccq);
                if (!string.IsNullOrEmpty(sccq))
                    noFilter = false;

                string country = textBox6.Text.Trim();
                p.Add("country", country);
                if (!string.IsNullOrEmpty(country))
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
                else if(!string.IsNullOrEmpty(starttime) && string.IsNullOrEmpty(endtime))
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

                //if (noFilter)
                //{
                //    if (Convert.ToInt32(pageControl1.cb_size.Text) > 500)
                //        pageControl1.cb_size.SelectedIndex = 4;
                //}

                p.Add("noFilter", noFilter);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                Application.DoEvents();//转让控制权
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_FinishedProduct_Information",//类名
                                            "GetFinishedProduct_Information_Main",//方法名
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
                dataGridViewEx1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
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
                        dgvr.Cells["重新验货"].Value =dr["recheck_pz"].ToString();
                        dgvr.Cells["出货国家"].Value = dr["SHIPCOUNTRY_NAME"].ToString();

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
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridViewEx1.ClearSelection();

                this.Enabled = true;
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
