using MaterialSkin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SJeMES_User
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
                configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");

                Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);

                Client.Language = "en";
                Client.CompanyCode = "HTEMS";
                Client.CompanyName = "HTEMS";
                Client.APIURL = "http://localhost:84//api/CommonCall";
                Client.UserCode = "ADMIN";
                Client.UserToken = "fd0cef16-5f79-460d-9304-a9eaf967d8a4";
                Client.UserName = "ADMIN";
                Client.WebServiceUrl = "http://127.0.0.1:80/SJ-WebService.asmx";
                //Client.Org.DBServer = "192.168.0.125";
                //Client.Org.DBType = "SqlServer";
                //Client.Org.DBUser = "sa";
                //Client.Org.DBName = "HTEMS";
                //Client.Org.DBPassword = "123456";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmUserInfo());
        }

        public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        public static string configstring;
        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();
        public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
    }
}
