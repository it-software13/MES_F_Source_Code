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
    public partial class F_AQL_ConfirmShoes_crk : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string MODULE_TYPE;
        public string aid;
        public string STOCK_CODE;
        public string ART;
        public string crk;
        public F_AQL_ConfirmShoes_Store frm;
        public Dictionary<string, object> _dic;
        public F_AQL_ConfirmShoes_crk(string _aid,string _MODULE_TYPE,string _STOCK_CODE, string _ART,string _crk, F_AQL_ConfirmShoes_Store _frm,Dictionary<string,object> dic)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            aid = _aid;
             _dic = dic ;
            MODULE_TYPE = _MODULE_TYPE;
            ART = _ART;
            STOCK_CODE = _STOCK_CODE;
            crk = _crk;
            frm = _frm;
            this.button1.Text = crk;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("Whether to confirm the delivery?", "hint", messButton);
            if (dr==DialogResult.Cancel)
            {
                return;
            }

            DeleteConfirmShoes_Store_ck(aid, textBox1.Text);
            frm.F_AQL_ConfirmShoes_Store_Load(null, null);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否确认退开发?", "提示", messButton);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("id", aid);
            data.Add("MODULE_TYPE", MODULE_TYPE);
            data.Add("reason", textBox1.Text.Trim());

            data.Add("ART", ART);
            data.Add("STOCK_CODE", STOCK_CODE);

            data.Add("STOCK_NAME", _dic["STOCK_NAME"]);
            data.Add("WAREHOUSE_NAME", _dic["WAREHOUSE_NAME"]);
            data.Add("WAREHOUSE_CODE", _dic["WAREHOUSE_CODE"]);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "AddConfirmShoes_Store_tkf", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                frm.F_AQL_ConfirmShoes_Store_Load(null, null);
                this.Close();
            }
        }
        /// <summary>
        /// 编辑-确认鞋-存放管理-出库-aql
        /// </summary>
        public void DeleteConfirmShoes_Store_ck(string aid,string reason)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("aid", aid);
                data.Add("MODULE_TYPE", MODULE_TYPE);
                data.Add("reason", reason);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "DeleteConfirmShoes_Store_ck", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Out of stock successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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
