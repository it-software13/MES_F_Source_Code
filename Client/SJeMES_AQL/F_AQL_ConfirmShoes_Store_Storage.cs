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
    public partial class F_AQL_ConfirmShoes_Store_Storage : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string aid = string.Empty;//AQL确认管理id
        string MODULE_TYPE = string.Empty;//模板类别
        Dictionary<string, object> dic;
        public F_AQL_ConfirmShoes_Store_Storage(string _aid,string _MODULE_TYPE,Dictionary<string,object>_dic)
        {
            InitializeComponent();
            aid = _aid;
            dic = _dic;
            MODULE_TYPE = _MODULE_TYPE;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("储位编号不能为空!");
                return;
            }
            EditConfirmShoes_Store_rk();
        }

        /// <summary>
        /// 编辑-确认鞋-存放管理-入库-aql
        /// </summary>
        public void EditConfirmShoes_Store_rk()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("aid", aid);
                data.Add("MODULE_TYPE", MODULE_TYPE);
                data.Add("STOCK_CODE", textBox1.Text.Trim());

                //data.Add("STOCK_NAME", dic["STOCK_NAME"]);
                //data.Add("WAREHOUSE_CODE", dic["WAREHOUSE_CODE"]);
                //data.Add("WAREHOUSE_NAME", dic["WAREHOUSE_NAME"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "EditConfirmShoes_Store_rk", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("入库成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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
