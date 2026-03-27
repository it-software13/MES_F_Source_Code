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

namespace SJeMES_BDM
{
    public partial class F_BDM_MATERIAL_TESTITEM_Insert : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_MATERIAL_TESTITEM_Insert()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询-检测项目-材料-测试-定制类型-新增查询
        /// </summary>
        public void GetMATERIAL_TESTITEM_Custom_Insert()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_MATERIAL_TESTITEM",//类名
                                            "GetMATERIAL_TESTITEM_Custom_Insert",//方法名
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
                var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                var dt3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());
                var dt4 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data4"].ToString());
                //if (dt1.Rows.Count > 0)
                //{
                //    comboBox1.DataSource = dt1;
                //    comboBox1.DisplayMember = "value";
                //    comboBox1.ValueMember = "code";
                //}
                //if (dt2.Rows.Count > 0)
                //{
                //    comboBox2.DataSource = dt2;
                //    comboBox2.DisplayMember = "value";
                //    comboBox2.ValueMember = "code";
                //}
                //if (dt3.Rows.Count > 0)
                //{
                //    comboBox3.DataSource = dt3;
                //    comboBox3.DisplayMember = "value";
                //    comboBox3.ValueMember = "code";
                //}

                if (dt4.Rows.Count > 0)
                {
                    comboBox4.DataSource = dt4;
                    comboBox4.DisplayMember = "value";
                    comboBox4.ValueMember = "code";
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_BDM_MATERIAL_TESTITEM_Insert_Load(object sender, EventArgs e)
        {
            GetMATERIAL_TESTITEM_Custom_Insert();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("ID cannot be empty!");
                return;
            }
            SaveMATERIAL_TESTITEM_Custom_Insert();
        }

        /// <summary>
        /// 保存-检测项目-材料-测试-定制类型
        /// </summary>
        public void SaveMATERIAL_TESTITEM_Custom_Insert()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("c_no", textBox1.Text.Trim());
                //data.Add("material_type_code", comboBox1.SelectedValue.ToString());
                //data.Add("material_type_name", comboBox1.Text);
                //data.Add("position_code", comboBox2.SelectedValue.ToString());
                //data.Add("position_name", comboBox2.Text);
                //data.Add("category_code", comboBox3.SelectedValue.ToString());
                //data.Add("category_name", comboBox3.Text); 

                data.Add("fgt_code", comboBox4.SelectedValue.ToString());
                data.Add("fgt_name", comboBox4.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_MATERIAL_TESTITEM", "SaveMATERIAL_TESTITEM_Custom_Insert", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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
