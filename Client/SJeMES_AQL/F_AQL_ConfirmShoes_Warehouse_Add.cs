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
    public partial class F_AQL_ConfirmShoes_Warehouse_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string MODULE_TYPE = string.Empty;//模板类别
        public F_AQL_ConfirmShoes_Warehouse_Add(string _MODULE_TYPE)
        {
            InitializeComponent();
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
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim())|| string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Warehouse code and warehouse name cannot be empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            AddConfirmShoesWarehouse();
        }

        /// <summary>
        /// 保存-确认鞋-仓库维护-aql
        /// </summary>
        public void AddConfirmShoesWarehouse()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("MODULE_TYPE", MODULE_TYPE);
                data.Add("WAREHOUSE_CODE", textBox1.Text.Trim());
                data.Add("WAREHOUSE_NAME", textBox2.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "AddConfirmShoesWarehouse", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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
