using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMESClient
{
    public partial class frmUserSetting : Form
    {
        string UserCode = string.Empty;
        public frmUserSetting(string UserCode)
        {
            this.UserCode = UserCode;
            InitializeComponent(); 
            textBox1.Text = UserCode; 
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ChangePWD_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_PWD.Text.Trim()) || string.IsNullOrEmpty(txt_PWD2.Text.Trim()))
                    throw new Exception("请输入密码信息！");  
                if (txt_PWD.Text.Trim() != txt_PWD2.Text.Trim())
                    throw new Exception("两次输入的密码不一致！");

                string msg1 = "提示";
                string msg2 = "确定要修改密码吗？";
                List<string> lstKeys = new List<string>();
                lstKeys.Add(msg1);
                lstKeys.Add(msg2);
                Dictionary<string, object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                if (dic.Count > 0)
                {
                    msg1 = dic[msg1].ToString();
                    msg2 = dic[msg2].ToString();
                } 
                DialogResult dr = MessageBox.Show(msg2, msg1, MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    string pwd = SJeMES_Framework.Common.Security.MD5(txt_PWD.Text.Trim().ToLower());
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("user_code", this.UserCode);
                    data.Add("pwd", pwd); 
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_SYSAPI", "SJ_SYSAPI.User", "UpdatePassword", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("修改成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                        this.Close();
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
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
            try
            {
                string msg1 = "提示";
                string msg2 = "确定要重置密码吗？";
                List<string> lstKeys = new List<string>();
                lstKeys.Add(msg1);
                lstKeys.Add(msg2);
                Dictionary<string, object> dic = SJeMES_Framework.Common.UIHelper.UIListMsg(lstKeys, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                if (dic.Count > 0)
                {
                    msg1 = dic[msg1].ToString();
                    msg2 = dic[msg2].ToString();
                }
                DialogResult dr = MessageBox.Show(msg2, msg1, MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    string pwd = SJeMES_Framework.Common.Security.MD5(textBox1.Text.Trim().ToLower());
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("user_code", this.UserCode);
                    data.Add("pwd", pwd);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_SYSAPI", "SJ_SYSAPI.User", "UpdatePassword", string.Empty, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("修改成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                        this.Close();
                    }
                    else
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
