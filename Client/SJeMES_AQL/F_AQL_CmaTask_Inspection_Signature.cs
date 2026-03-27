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
    public partial class F_AQL_CmaTask_Inspection_Signature : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        DataTable cma_yhs = new DataTable();//验货室数据
        string autograph_state = string.Empty;//签名类型 0:工厂代表签名 1:客户签名
        public F_AQL_CmaTask_Inspection_Signature(DataTable _cma_yhs,string _autograph_state)
        {
            InitializeComponent();
            cma_yhs = _cma_yhs;
            autograph_state = _autograph_state;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()))
            {
                MessageBox.Show("账号不能为空!");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
            {
                MessageBox.Show("密码不能为空!");
                return;
            }

            EidtCmaTask_TaskList_Signature();
        }

        /// <summary>
        /// 新增-新增AQL验货任务
        /// </summary>
        public void EidtCmaTask_TaskList_Signature()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("autograph_state", autograph_state);//签名类型 0:工厂代表签名 1:客户签名
                data.Add("cma_yhs", cma_yhs);//验货室数据
                data.Add("account", textBox1.Text.Trim());//账号
                data.Add("pwd", SJeMES_Framework.Common.Security.MD5(textBox2.Text.Trim()).ToUpper());//密码
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_CmaTask_Inspection", "EidtCmaTask_TaskList_Signature", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("签名成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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
