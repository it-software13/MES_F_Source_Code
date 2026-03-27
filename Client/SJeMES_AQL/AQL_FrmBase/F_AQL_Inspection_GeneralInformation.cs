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
    public partial class F_AQL_Inspection_GeneralInformation : UserControl
    {
        List<TestType> ttList = new List<TestType>();
        List<NewOldshoe> noList = new List<NewOldshoe>();
        string function_type = string.Empty;
        string task_no = string.Empty;
        Dictionary<string, object> dics = new Dictionary<string, object>();
        public F_AQL_Inspection_GeneralInformation(string _function_type, Dictionary<string, object> _dics)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitializeControls();
            function_type = _function_type;
            task_no = _dics["task_no"].ToString();
            dics = _dics;
            if (function_type=="点箱")//点箱
            {
                button1.Visible = true;
            }

            //InitDateTimePicker(dateTimePicker1);
        }

        //初始化控件
        public void InitializeControls()
        {
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
        }

        private void F_AQL_Inspection_GeneralInformation_Load(object sender, EventArgs e)
        {


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
            t4.value = "Rummage_Again";//再次翻箱
            ttList.Add(t4);
            comboBox1.DataSource = ttList;
            comboBox1.DisplayMember = "value";
            comboBox1.ValueMember = "code";
            #endregion

            #region 新旧鞋型
            NewOldshoe n2 = new NewOldshoe();
            n2.code = "0";
            n2.value = "Old_Shoes";//旧鞋型
            noList.Add(n2);
            NewOldshoe n1 = new NewOldshoe();
            n1.code = "1";
            n1.value = "New_Shoe_Type";//新鞋型
            noList.Add(n1);
            comboBox2.DataSource = noList;
            comboBox2.DisplayMember = "value";
            comboBox2.ValueMember = "code";
            #endregion

            GetPointBox_title();
        }

        //检验类型
        public class TestType
        {
            public string code { get; set; }
            public string value { get; set; }
        }

        //新旧鞋型
        public class NewOldshoe
        {
            public string code { get; set; }
            public string value { get; set; }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetInspection_GeneralInformationPo();
            }
        }

        /// <summary>
        /// 查询-验货室订单查询
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetInspection_GeneralInformationPo()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("po", textBox1.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CmaTask_Photo",//类名
                                            "GetInspection_GeneralInformationPo",//方法名
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
                if (dt.Rows.Count > 0)
                {
                    label15.Text = dt.Rows[0]["PROD_NO"].ToString();
                    label16.Text = dt.Rows[0]["SE_QTY"].ToString();
                    label18.Text = dt.Rows[0]["shoe_name"].ToString();
                }
                else
                {
                    MessageBox.Show("无此PO!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditPointBox_title();
        }

        /// <summary>
        /// 保存-点箱-头
        /// </summary>
        public void EditPointBox_title()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", task_no);
                data.Add("po", textBox1.Text.Trim());
                data.Add("inspection_type", comboBox1.SelectedValue.ToString());
                data.Add("inspection_date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                data.Add("shoe_type", comboBox2.SelectedValue.ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_PointBox", "EditPointBox_title", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    MessageBox.Show("保存成功!");
                }
                else
                    MessageBox.Show(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 查询-点箱-头
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetPointBox_title()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_PointBox",//类名
                                            "GetPointBox_title",//方法名
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

                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0]["MER_PO"].ToString();
                    label15.Text= dt.Rows[0]["PROD_NO"].ToString();
                    label19.Text= dt.Rows[0]["lot_num"].ToString();
                    label16.Text = dt.Rows[0]["SE_QTY"].ToString();
                    label18.Text = dt.Rows[0]["shoe_name"].ToString();
                    comboBox1.SelectedValue= dt.Rows[0]["inspection_type"].ToString();
                    comboBox2.SelectedValue = dt.Rows[0]["shoe_type"].ToString();

                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["inspection_date"].ToString()))
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["inspection_date"].ToString());
                    }
                    else
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                    string is_inspection = dt.Rows[0]["is_inspection"].ToString();
                    if (is_inspection == "1")
                        dateTimePicker1.Enabled = false;
                    else
                        dateTimePicker1.Enabled = true;
                }
                label13.Text = dics["guojia"].ToString();
                label14.Text = dics["rule_no"].ToString();
                label17.Text = dics["CHECKER"].ToString();
                label20.Text = dics["from_line"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
    }
}
