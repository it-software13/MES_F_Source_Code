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
    public partial class F_AQL_ConfirmShoes_Store_qryxq : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string aid = string.Empty;
        string ART = string.Empty;
        string STOCK_CODE = string.Empty;
        string MODULE_TYPE = string.Empty;
        F_AQL_ConfirmShoes_Store _ff;
        public string status = "0";//单元格 1-批量
        DataTable confirm = new DataTable();
        bool isscan = false;
        public F_AQL_ConfirmShoes_Store_qryxq(string _aid,string _ART,string _STOCK_CODE, F_AQL_ConfirmShoes_Store ff,string _MODULE_TYPE,string _status)
        {
            InitializeComponent();
            aid = _aid;
            ART = _ART;
            _ff = ff;
            status = _status;
            MODULE_TYPE = _MODULE_TYPE;
            STOCK_CODE = _STOCK_CODE;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_AQL_ConfirmShoes_Store_qryxq(DataTable _confirm,string _MODULE_TYPE,string _status)
        {
            InitializeComponent();
            confirm = _confirm;
            status = _status;
            MODULE_TYPE = _MODULE_TYPE;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //确认有效期
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(status == "0")
                {
                    //单元格
                    update();
                }
                else
                {
                    //批量
                    Allupdate();
                }
              


                

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        public void update()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("请录入工号!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("aid", aid);
            data.Add("MODULE_TYPE", MODULE_TYPE);
            data.Add("confirm_by", textBox1.Text.Trim());
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "DeleteConfirmShoes_Store_qryxq2", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("编辑成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                //_ff.F_AQL_ConfirmShoes_Store_Load(null,null);
                this.Close();
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Edit failed！" + j["ErrMsg"].ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //批量更新
        public void Allupdate()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please enter the job number!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("confirm", confirm);
            data.Add("aid", aid);
            data.Add("MODULE_TYPE", MODULE_TYPE);
            data.Add("confirm_by", textBox1.Text.Trim());
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "DeleteConfirmShoes_Store_qryxq", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(j["IsSuccess"].ToString()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("编辑成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                //_ff.F_AQL_ConfirmShoes_Store_Load(null,null);
                this.Close();
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("编辑失败！" + j["ErrMsg"].ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please enter the job number!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            using (F_AQL_ConfirmShoes_Store_redo redo = new F_AQL_ConfirmShoes_Store_redo(confirm,aid, MODULE_TYPE, textBox1.Text, status))
            {
                redo.ShowDialog();
            }
            this.Close();
        }

        public void EditConfirmShoes_Store_jc_staff_name()
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
                    //this.Enabled = true;

                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("No job number found!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    //this.Enabled = false;
                    return;
                }
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("No job number found!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                //this.Enabled = false;
                return;

            }

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditConfirmShoes_Store_jc_staff_name();
                
            }
            }
    }
}
