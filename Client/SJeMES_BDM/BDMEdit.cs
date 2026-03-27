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

namespace SJeMES_BDM
{
    public partial class BDMEdit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public BDMEdit()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BDMEdit_Load(object sender, EventArgs e)
        {

        }

        string Typeno = string.Empty;
        public BDMEdit(String id,string GENERAL_TESTTYPE_NO)
        {
            InitializeComponent();
            Typeno = GENERAL_TESTTYPE_NO;
            if (!string.IsNullOrEmpty(id))
            {
                TypeUpdata(id); 
            }
        }

        string bdmid=string.Empty;
        //类型修改赋值
        public void TypeUpdata(string id)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("id", id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "TypeUpdata", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        foreach (DataRow item in dt.Rows)
                        {
                            bdmid = id;
                            this.txt1.Text = item["quality_category_name"].ToString();
                            this.txt2.Text = item["quality_category_no"].ToString();
                            this.rtxt_remarks.Text = item["remarks"].ToString();
                            this.txt2.ReadOnly = true;
                        }
                    }
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

        //类型修改,新增
        private void btn2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt1.Text.Trim() != "" && this.txt2.Text.Trim() != "")
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("id", bdmid);
                    data.Add("GENERAL_TESTTYPE_NO", Typeno);
                    data.Add("quality_category_no", this.txt2.Text.Trim());
                    data.Add("quality_category_name", this.txt1.Text.Trim());
                    data.Add("remarks", this.rtxt_remarks.Text.Trim());
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "TypeEdit", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                        this.Close();
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
                else
                    throw new Exception("代号和名称不能为空!");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
