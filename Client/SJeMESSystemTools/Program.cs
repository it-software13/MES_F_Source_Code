using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMESSystemTools
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");

            Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);

            Client.Language = string.Empty;
            Client.CompanyCode = "HTEMS";
            Client.CompanyName = "HTEMS";
            Client.APIURL = Pconfig["api"];
            Client.UserCode = "ADMIN";
            Client.UserToken = "fd0cef16-5f79-460d-9304-a9eaf967d8a4";
            Client.UserName = "ADMIN";




            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_SJeMESSystemTools());
        }

        public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        public static Dictionary<string,SJeMES_Framework.Web.JSONMenu> MenusInfo;
        public static string configstring;


        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();

    }
}
