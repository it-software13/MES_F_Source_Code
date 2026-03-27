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
    public partial class F_AQL_ConfirmShoes_Store_plbf : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        DataTable confirm = new DataTable();
        bool isscan = false;
        public F_AQL_ConfirmShoes_Store_plbf(DataTable _confirm)
        {
            InitializeComponent();
            confirm = _confirm;
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
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text)|| string.IsNullOrEmpty(textBox2.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please enter the job number!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("staff_no", textBox1.Text.Trim());
                data.Add("remark", richTextBox1.Text.Trim());
                data.Add("confirm", confirm);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "DeleteConfirmShoes_Store_plbf", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Edited Successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               GetConfirmShoes_Store_jc_staff_name();


            }
               
        }
        public void GetConfirmShoes_Store_jc_staff_name()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("STAFF_NO", this.textBox1.Text.Trim());
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
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("查无此工号!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
