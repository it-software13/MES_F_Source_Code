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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_EX_Stock_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _id = "";
        public string _code = "";
        public string _name = "";
        public string _housecode = "";
        public bool bl = false;
        public F_QCM_EX_Stock_Edit(string id = "",string code="",string name="",string housecode="")
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _id = id;
            _code = code;
            _name = name;
            _housecode= housecode;
        }

        private void F_QCM_EX_Stock_Edit_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetExWhList",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            txt_code.Text = _code;
            if(!string.IsNullOrEmpty(_id))
            {
                txt_code.Enabled = false;
            }
            txt_name.Text = _name;
            comboBox1.DisplayMember = "WAREHOUSE_NAME";
            comboBox1.ValueMember = "WAREHOUSE_CODE";
            comboBox1.DataSource= dt;
            comboBox1.SelectedValue = _housecode;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txt_code.Text.Trim()))
            {
                MessageBox.Show("Location code cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_name.Text.Trim()))
            {
                MessageBox.Show("Location name cannot be empty");
                return;
            }
            if (string.IsNullOrEmpty(comboBox1.Text.ToString().Trim()))
            {
                MessageBox.Show("Please select a storage warehouse");
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_capacity.Text.Trim()))
            {
                MessageBox.Show("Location Capacity cannot be empty");
                return;
            }

            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id",_id);
            p.Add("code", txt_code.Text.Trim());
            p.Add("name", txt_name.Text.Trim());
            p.Add("capacity", txt_capacity.Text.Trim());
            p.Add("housecode",comboBox1.SelectedValue.ToString().Trim()); 
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "EditExStock",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                MessageBox.Show("Saved successfully");
                if(string.IsNullOrEmpty(_id))
                {
                    bl = true;
                }
                else
                {
                    _code = txt_code.Text.Trim();
                    _name = txt_name.Text.Trim();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(ret.ErrMsg);
            }
        }
    }
}
