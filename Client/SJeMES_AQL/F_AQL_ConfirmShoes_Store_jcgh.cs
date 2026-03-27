using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_ConfirmShoes_Store_jcgh : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string aid = string.Empty;
        string ref_standard = string.Empty;
        string _MODULE_TYPE = string.Empty;
        bool isscan = false;
        Dictionary<string, object> _dic;
        public F_AQL_ConfirmShoes_Store_jcgh(string _aid, string _ref_standard,Dictionary<string,object> dic,string MODULE_TYPE)
        {
            InitializeComponent();
            aid = _aid;
            ref_standard = _ref_standard;
            _MODULE_TYPE = MODULE_TYPE;
            _dic = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //录入获取名称
                EditConfirmShoes_Store_jc_staff_name();



            }
        }

        /// <summary>
        /// 编辑-确认鞋-存放管理_借出/归还-aql
        /// </summary>
        public void EditConfirmShoes_Store_jc_gh()
        {
            try
            {
                
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("aid", aid);
                data.Add("ref_standard", ref_standard);
                data.Add("opra_by", textBox1.Text.Trim());

                data.Add("STOCK_CODE", _dic["STOCK_CODE"]);
                data.Add("STOCK_NAME", _dic["STOCK_NAME"]);
                data.Add("WAREHOUSE_CODE", _dic["WAREHOUSE_CODE"]);
                data.Add("WAREHOUSE_NAME", _dic["WAREHOUSE_NAME"]);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "EditConfirmShoes_Store_jc_gh", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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
                return;
            }
        }

        public void EditConfirmShoes_Store_jc_staff_name()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            //if (string.IsNullOrEmpty(this.textBox1.Text.Trim())|| string.IsNullOrEmpty(this.textBox2.Text.Trim()))
            //{
            //    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请录入工号!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
            //    SJeMES_Control_Library.MessageHelper.ShowWarning(this, msg);
            //}

            data.Add("STAFF_NO", this.textBox1.Text.Trim());
            data.Add("MODULE_TYPE", _MODULE_TYPE);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "GetConfirmShoes_Store_staff_name", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(j["RetData"].ToString());
            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                if (dt.Rows.Count > 0)
                {
                    this.textBox1.Text = dt.Rows[0]["STAFF_NO"].ToString();
                    this.textBox2.Text = dt.Rows[0]["STAFF_NAME"].ToString();

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Search succeeded!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);

                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("No job number found!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
            }
            else 
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("No job number found!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please enter the job number!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            EditConfirmShoes_Store_jc_gh();
        }
    }
}
