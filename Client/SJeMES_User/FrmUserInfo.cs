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

namespace SJeMES_User
{
    public partial class FrmUserInfo : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public FrmUserInfo()
        {
            InitializeComponent(); 
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
               Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
        }

        private void FrmUserInfo_Load(object sender, EventArgs e)
        {
            try
            {
                //加载用户信息
                GetUserInfoData("");


            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetUserInfoData(textBox1.Text.Trim());
        }

        /// <summary>
        /// 获取系统用户信息
        /// </summary>
        public void GetUserInfoData(string strWhere)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("strWhere", strWhere);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.User", "GetUserListData", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(j["RetData"].ToString());
                    if(dt!=null && dt.Rows.Count>0)
                    {
                        dataGridView1.DataSource = dt;
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

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    string UserCode = dataGridView1.Rows[index].Cells["用户代号"].Value.ToString();
                    if(!string.IsNullOrEmpty(UserCode))
                    {
                        frmUserSetting frmUser = new frmUserSetting(UserCode);
                        frmUser.ShowDialog();
                    }
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
