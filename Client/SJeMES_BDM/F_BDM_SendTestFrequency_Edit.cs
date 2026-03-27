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

namespace SJeMES_BDM
{
    public partial class F_BDM_SendTestFrequency_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string SJID = string.Empty; 
        public F_BDM_SendTestFrequency_Edit()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_BDM_SendTestFrequency_Edit(string id)
        {
            SJID = id;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 下拉框值
        /// </summary>
        public void GetComValue()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.SendTestFrequency",//类名
                                            "GetComValue",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                comTime.DataSource = dt;
                comTime.DisplayMember = "enum_value";
                comTime.ValueMember = "enum_code";
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 修改赋值
        /// </summary>
        public void GetUpdataValue(string id)
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("id", id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.SendTestFrequency",//类名
                                            "GetUpdataValue",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    SJID = dt.Rows[0]["ID"].ToString() ;
                    comTime.SelectedValue = dt.Rows[0]["INSPECTION_FREQUENCY_TIME_UNIT"].ToString();
                    txtValue.Text = dt.Rows[0]["INSPECTION_FREQUENCY_VALUE"].ToString();
                    txtRemark.Text = dt.Rows[0]["REMARKS"].ToString();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            EditSendTestFrequency();
        }

        /// <summary>
        /// 编辑送检频率
        /// </summary>
        public void EditSendTestFrequency()
        {
            try
            {
                if (this.txtValue.Text.Trim() != "" && this.txtRemark.Text.Trim() != "")
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("ID", SJID);
                    data.Add("INSPECTION_FREQUENCY_VALUE", this.txtValue.Text.Trim());
                    data.Add("INSPECTION_FREQUENCY_TIME_UNIT", this.comTime.SelectedValue.ToString());
                    data.Add("REMARKS", this.txtRemark.Text.Trim());
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.SendTestFrequency", "EditSendTestFrequency", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                        this.Close();
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
                else
                    throw new Exception("Value and comment cannot be empty!");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_BDM_QualityStandard_Edit_Load(object sender, EventArgs e)
        {
            GetComValue();
            GetUpdataValue(SJID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
