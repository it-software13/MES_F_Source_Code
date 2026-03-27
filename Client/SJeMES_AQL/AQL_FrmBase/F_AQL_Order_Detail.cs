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
    public partial class F_AQL_Order_Detail : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        List<inspection_state> insplist = new List<inspection_state>();
        public F_AQL_Order_Detail()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            InitDateTimePicker(starttime);
            InitDateTimePicker(endtime);
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

            #endregion
        }


        //验货状态
        public class inspection_state
        {
            public string code { get; set; }
            public string value { get; set; }
        }

        private void F_AQL_Order_Detail_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            pageControl1.BindPageEvent += GetOrder_Detail_Main;
            LoadPage();
            this.dataGridViewEx1.ClearSelection();
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
        /// 查询-订单明细
        /// </summary>
        /// <param name="a">为了防止dataGridViewEx1里添加的按钮重复 </param>
        public void GetOrder_Detail_Main(int pageSize, int pageIndex, out int totalCount)
        {
            this.Enabled = false;
            totalCount = 0;
            try
            {
                string start_date = string.Empty;
                string end_date = string.Empty;
                //string size_no = dataGridViewEx1.CurrentRow.Cells["size_no"].Value.ToString();
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("SE_ID", txt_sales.Text.Trim());//销售订单
                p.Add("mer_po", txt_po.Text.Trim());//PO号
                p.Add("prod_no", txt_art.Text.Trim());//art
                p.Add("name_t", txt_shoes.Text.Trim());//鞋型
                p.Add("size_no", size_no);//鞋型

                p.Add("CUSTORDER", txt_khddh.Text.Trim());//客户订单号
                p.Add("SE_CUSTID", txt_khno.Text.Trim());//客户编号
                p.Add("SHIPCOUNTRY_NAME", txt_country.Text.Trim());//出货国家
                p.Add("WORKORDER_NO", txt_zl.Text.Trim());//制令
                p.Add("type", this.checkBox1.Checked.ToString().ToLower());//size明细

                //进仓日期
                if (!string.IsNullOrWhiteSpace(this.starttime.Text))
                {
                    start_date = Convert.ToDateTime(this.starttime.Value).ToString("yyyy-MM-dd");
                    p.Add("start_date", start_date);
                }
                if (!string.IsNullOrWhiteSpace(this.endtime.Text))
                {
                    end_date = Convert.ToDateTime(this.endtime.Value).ToString("yyyy-MM-dd");
                    p.Add("end_date", end_date);
                }

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                Application.DoEvents();//转让控制权
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Order_Detail",//类名
                                            "GetOrder_Detail_Main",//方法名
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
                        //dgvr.Cells["客户号"].Value = dr["SE_CUSTID"].ToString();
                        dgvr.Cells["客户订单号"].Value = dr["CUSTORDER"].ToString();
                        dgvr.Cells["ART"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["销售订单号"].Value = dr["se_id"].ToString();
                        dgvr.Cells["WORKORDER_NO"].Value = dr["WORKORDER_NO"].ToString();
                        dgvr.Cells["PO号"].Value = dr["mer_po"].ToString();
                        dgvr.Cells["鞋型"].Value = dr["shoe_name"].ToString();
                        dgvr.Cells["模号"].Value = dr["mold_no"].ToString();
                        dgvr.Cells["size_no"].Value = dr["size_no"].ToString();
                        dgvr.Cells["订单数量"].Value = dr["ddse_qty"].ToString();
                        dgvr.Cells["订单有效数量"].Value = dr["ddyxse_qty"].ToString();
                        dgvr.Cells["出货国家"].Value = dr["SHIPCOUNTRY_NAME"].ToString();
                        dgvr.Cells["Gender"].Value = dr["Gender"].ToString();
                        dgvr.Cells["鞋型系列"].Value = dr["code_no"].ToString();
                        //dgvr.Cells["客户编号"].Value = dr["ACC_CUSTID"].ToString();
                        dgvr.Cells["客户编号"].Value = dr["SE_CUSTID"].ToString();
                        dgvr.Cells["VAS"].Value = dr["column1"].ToString();
                        dgvr.Cells["PSDD"].Value = dr["nst"].ToString();
                        dgvr.Cells["PODD"].Value = dr["nlt"].ToString();
                        dgvr.Cells["LPD"].Value = dr["lpd"].ToString();
                        dgvr.Cells["CRD"].Value = dr["cr_reqdate"].ToString();
                        dgvr.Cells["开发季节"].Value = dr["DEVELOP_season"].ToString();
                        dgvr.Cells["开发类型"].Value = dr["DEVELOP_TYPE"].ToString();
                        dgvr.Cells["鞋款配色"].Value = dr["color_way"].ToString();
                        dgvr.Cells["开发鞋型负责人"].Value = dr["USER_IN_SHOECHARGE"].ToString();

                        dgvr.Cells["最终确认交期日期"].Value = dr["FINALCOMFIRM_DATE"].ToString();
                        dgvr.Cells["订单确认状态"].Value = dr["ORDER_STATUS"].ToString();
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
    }
}
