using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_IQC
{
    public partial class F_IQC_ConfirmShoes_Location_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public class ref_standardEx
        {
            public string code { get; set; }
            public string value { get; set; }
        }
        List<ref_standardEx> lisrse = new List<ref_standardEx>();
        string sid = string.Empty;
        public F_IQC_ConfirmShoes_Location_Add(string _sid)
        {
            InitializeComponent();
            sid = _sid;
            textBox1.Enabled = false;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public F_IQC_ConfirmShoes_Location_Add()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void F_IQC_ConfirmShoes_Location_Add_Load(object sender, EventArgs e)
        {
            lisrse.Add(new ref_standardEx()
            {
                code = "0",
                value = "入库时间"
            });
            lisrse.Add(new ref_standardEx()
            {
                code = "1",
                value = "量产时间"
            });
            comboBox2.DataSource = lisrse;
            comboBox2.DisplayMember = "value";
            comboBox2.ValueMember = "code";

            GetConfirmShoesLocation_Edit_ck();

            GetConfirmShoesLocation_Edit();
        }

        /// <summary>
        /// 查询-确认鞋-库位维护-编辑-仓库
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoesLocation_Edit_ck()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("sid", sid);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_ConfirmShoes",//类名
                                            "GetConfirmShoesLocation_Edit_ck",//方法名
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
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "value";
                    comboBox1.ValueMember = "code";
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询-确认鞋-库位维护-编辑
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoesLocation_Edit()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("sid", sid);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_ConfirmShoes",//类名
                                            "GetConfirmShoesLocation_Edit",//方法名
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
                    textBox1.Text = dt.Rows[0]["STOCK_CODE"].ToString();
                    comboBox1.SelectedValue = dt.Rows[0]["WAREHOUSE_CODE"].ToString();
                    textBox2.Text = dt.Rows[0]["STOCK_NAME"].ToString();
                    textBox3.Text = dt.Rows[0]["remark"].ToString();
                    comboBox2.SelectedValue = dt.Rows[0]["ref_standard"].ToString();
                    textBox4.Text = dt.Rows[0]["expire_day"].ToString();
                    textBox5.Text = dt.Rows[0]["remind_day"].ToString();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()) || string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("库位代号和库位不能为空!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text.Trim()) || string.IsNullOrWhiteSpace(textBox5.Text.Trim()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("到期时长和提醒时间不能为空!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("仓库不能为空!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            if (string.IsNullOrWhiteSpace(comboBox1.SelectedValue.ToString()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("仓库不能为空!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            EditConfirmShoesLocation();
        }

        /// <summary>
        /// 编辑-确认鞋-库位维护
        /// </summary>
        public void EditConfirmShoesLocation()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("sid", sid);
                data.Add("STOCK_CODE", textBox1.Text.Trim());
                data.Add("STOCK_NAME", textBox2.Text.Trim());
                data.Add("WAREHOUSE_CODE", comboBox1.SelectedValue.ToString());
                data.Add("WAREHOUSE_NAME", comboBox1.Text);
                data.Add("remark", textBox3.Text.Trim());
                data.Add("ref_standard", comboBox2.SelectedValue.ToString());
                data.Add("expire_day", textBox4.Text.Trim());
                data.Add("remind_day", textBox5.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_ConfirmShoes", "EditConfirmShoesLocation", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
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
    }
}
