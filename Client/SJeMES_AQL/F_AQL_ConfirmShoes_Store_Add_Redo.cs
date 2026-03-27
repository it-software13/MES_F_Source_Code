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
    public partial class F_AQL_ConfirmShoes_Store_Add_Redo : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string prod_nos = string.Empty;
        string STOCK_CODE = string.Empty;
        string MODULE_TYPE = string.Empty;
        public F_AQL_ConfirmShoes_Store_Add_Redo(string _prod_no,string _STOCK_CODE,string _MODULE_TYPE)
        {
            InitializeComponent();
            prod_nos = _prod_no;
            STOCK_CODE = _STOCK_CODE;
            MODULE_TYPE = _MODULE_TYPE;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("确认人不能为空!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            AddConfirmShoes_Store_Redo();
        }

        /// <summary>
        /// 保存-确认鞋-存放管理-aql
        /// </summary>
        public void AddConfirmShoes_Store_Redo()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_nos", prod_nos);//鞋子二维码
                data.Add("STOCK_CODE", STOCK_CODE);//库位编号
                data.Add("MODULE_TYPE", MODULE_TYPE);//模板类别
                data.Add("confirm_by", textBox1.Text.Trim());//确认人
                data.Add("redo_reason", richTextBox1.Text.Trim());//重做原因
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "AddConfirmShoes_Store_Redo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
                else
                {
                    throw new Exception(j["ErrMsg"].ToString());
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
