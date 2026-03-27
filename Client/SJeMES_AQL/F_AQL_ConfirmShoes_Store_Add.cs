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
    public partial class F_AQL_ConfirmShoes_Store_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string MODULE_TYPE = string.Empty;
        public F_AQL_ConfirmShoes_Store_Add(string _MODULE_TYPE)
        {
            InitializeComponent();
            MODULE_TYPE = _MODULE_TYPE;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()) || string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
                {
                    MessageBox.Show("Information cannot be empty!");
                }
                else
                {
                    AddConfirmShoes_Store();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()) || string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
                {
                    MessageBox.Show("Information cannot be empty!");
                }
                else
                {
                    AddConfirmShoes_Store();
                }
            }
        }

        /// <summary>
        /// 保存-确认鞋-存放管理-aql
        /// </summary>
        public void AddConfirmShoes_Store()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("STOCK_CODE", textBox1.Text.Trim());
                data.Add("MODULE_TYPE", MODULE_TYPE);
                data.Add("prod_nos", textBox2.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "AddConfirmShoes_Store", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(j["ErrMsg"].ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    if (j["ErrMsg"].ToString() == "Shoes QR code information cannot be added repeatedly!")
                    {
                        //string prods = textBox2.Text.Trim();
                        //string STOCK_CODE = textBox1.Text.Trim();
                        //using (F_AQL_ConfirmShoes_Store_Add_Redo a = new F_AQL_ConfirmShoes_Store_Add_Redo(prods, STOCK_CODE, MODULE_TYPE))
                        //{
                        //    a.ShowDialog();
                        //}
                        //this.Close();
                        return;
                    }
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
            this.Close();
        }
    }
}
