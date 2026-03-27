using MaterialSkin;
using SJeMES_BDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMESClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("LoginUser.json");
                //if (string.IsNullOrEmpty(configstring))
                //{
                //    configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");
                //}

                configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");
                int sIndex = configstring.IndexOf("{");
                int eIndex = configstring.IndexOf("}");
                if (!string.IsNullOrEmpty(configstring) && sIndex >= 0 && eIndex >= 0)
                {
                    configstring = configstring.Substring(sIndex, eIndex + 1);
                    Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);

                    Client.Language = Pconfig["language"];
                    Client.CompanyCode = Pconfig["org"];
                    Client.APIURL = Pconfig["api"];
                    Client.UserCode = Pconfig["usercode"];
                    Client.WebServiceUrl = Pconfig["webservice"];
                    //Client.UploadUrl = Pconfig["uploadurl"];
                    //Client.PicUrl = Client.UploadUrl.ToLower().Replace("/api/commoncall", "");


                }
                else
                    MessageBox.Show("Config.json The file content is empty, please check！");

                WebService.Url = Client.WebServiceUrl;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmLogin= new frmLogin();
            Application.Run(FrmLogin);
        }

        public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
        public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        public static string configstring;

        //public static string LoginUser;

        public static SJ_WebService.SJ_WebService WebService = new SJeMESClient.SJ_WebService.SJ_WebService();

        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();


        public static frmMain FrmMain;
        public static frmLogin FrmLogin;

        public static bool IsExit = true;

        public static Dictionary<string, SJeMES_Control_Library.Controls.UCModuleBaseList> DicModuleLists=new Dictionary<string, SJeMES_Control_Library.Controls.UCModuleBaseList>();
        public static Dictionary<string, SJeMES_Control_Library.Controls.UCModuleBase> DicModules= new Dictionary<string, SJeMES_Control_Library.Controls.UCModuleBase>();

    }
}
