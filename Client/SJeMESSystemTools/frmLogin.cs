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

namespace SJeMESSystemTools
{
    public partial class frmLogin : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public frmLogin()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
                 MaterialSkinManager.Themes.LIGHT, materialSkinManager, this);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                ucTextBoxEx1.InputText = Program.Client.UserCode;

                #region 语言
                List<string> LanguagesType = LoginHelper.GetLanguagesType();

                List<KeyValuePair<string, string>> lstCom = new List<KeyValuePair<string, string>>();
                foreach (string s in LanguagesType)
                {
                    lstCom.Add(new KeyValuePair<string, string>(s, s));

                }

                ucCombox1.Source = lstCom;

                foreach (KeyValuePair<string, string> kv in lstCom)
                {
                    if (kv.Key == Program.Client.Language)
                    {
                        ucCombox1.TextValue = kv.Key;
                    }
                }

                if (string.IsNullOrEmpty(ucCombox1.TextValue) && lstCom.Count > 0)
                {
                    ucCombox1.SelectedIndex = 0;
                } 
                #endregion

                #region 公司
                Dictionary<string, string> Orgs = new Dictionary<string, string>();
                Orgs = LoginHelper.GetOrg();
                List<KeyValuePair<string, string>> lstCom2 = new List<KeyValuePair<string, string>>();
                foreach (string key in Orgs.Keys)
                {
                    lstCom2.Add(new KeyValuePair<string, string>(key, Orgs[key]));


                }

                ucCombox2.Source = lstCom2;

                foreach (KeyValuePair<string, string> kv in lstCom2)
                {
                    if (kv.Key == Program.Client.CompanyCode)
                    {
                        ucCombox2.SelectedValue = kv.Key;
                    }
                }

                if (string.IsNullOrEmpty(ucCombox2.TextValue) && lstCom2.Count > 0)
                {
                    ucCombox2.SelectedIndex = 0;
                } 
                #endregion
            }
            catch(Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void ucBtnImg2_BtnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ucBtnImg3_BtnClick(object sender, EventArgs e)
        {
            SJeMES_Control_Library.Forms.FrmInputs frm = new
                 SJeMES_Control_Library.Forms.FrmInputs("设置API地址",
                 new string[] { "API地址" },
                 new Dictionary<string, SJeMES_Control_Library.TextInputType>(),
                 new Dictionary<string, string>(),
                 new Dictionary<string, SJeMES_Control_Library.Controls.KeyBoardType>(),
                 new List<string>() { "API地址" },
                 new Dictionary<string, string>() { {"API地址",Program.Client.APIURL} });
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(Program.configstring);

                Pconfig["api"] = frm.Values[0];
                Program.Client.APIURL = frm.Values[0];
                Program.configstring = Newtonsoft.Json.JsonConvert.SerializeObject(Pconfig);
                

                //System.IO.File.Delete("Config.json");
                //SJeMES_Framework.Common.TXTHelper.WriteToEnd("Config.json", Program.configstring);
                SJeMES_Framework.Common.TXTHelper.WriteLine("Config.json", Program.configstring);
            }
        }

        private void ucBtnImg1_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(ucTextBoxEx1.InputText))
                {
                    throw new Exception("请输入账号");
                }

                if (string.IsNullOrEmpty(ucTextBoxEx2.InputText))
                {
                    throw new Exception("请输入密码");
                }


                if (LoginHelper.Login(
                    ucCombox2.SelectedValue,ucCombox2.SelectedText,
                    ucTextBoxEx1.InputText.Trim(),
                    ucTextBoxEx2.InputText.Trim()))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "验证成功");
                    this.Close();
                }
            }
            catch(Exception ex) { SJeMES_Control_Library.MessageHelper.ShowErr(this,ex.Message); }
        }
    }
}
