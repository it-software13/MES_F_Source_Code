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

namespace SJeMES_AQL
{
    public partial class F_AQL_ConfirmShoes_Store_redo : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string id = string.Empty;
        public string ART = string.Empty;
        public string STOCK_CODE = string.Empty;
        public string MODULE_TYPE = string.Empty;
        public string user_code = string.Empty;
        public string status = "0";//0-批量 1-单元格处理
        public DataTable confirm ;
        public F_AQL_ConfirmShoes_Store_redo(DataTable _confirm, string _id,string _MODULE_TYPE,string _user_code,string _status)
        {
            InitializeComponent();
            id = _id;
            confirm = _confirm;
            MODULE_TYPE = _MODULE_TYPE;
            user_code = _user_code;
            status = _status;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(user_code))
            //{
            //    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请扫描工号!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            //    return;
            //}


           if(status == "0")
            {
                //单元格行处理
                update();
            }
            else
            {
                //批量处理

                Allupdate();
            }

        }


        public void update()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("reason", txt_text.Text);
            data.Add("id", id);
            data.Add("MODULE_TYPE", MODULE_TYPE);
            data.Add("confirm_by", user_code);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "UpdateConfirmShoes_Store_rk", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("提交失败!" + ret.ErrMsg.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Submitted successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                this.Close();
            }
        }

        public void Allupdate()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("reason", txt_text.Text);
            data.Add("confirm", confirm);
            data.Add("MODULE_TYPE", MODULE_TYPE);
            data.Add("confirm_by", user_code);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "UpdateConfirmShoes_Store_rk2", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("提交失败!" + ret.ErrMsg.ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("提交成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                this.Close();
            }
        }
    }
}
