using MaterialSkin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace SJeMES_TQC
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

                Client.Language = string.Empty;
                Client.CompanyCode = string.Empty;
                Client.APIURL = Pconfig["api"];
                Client.UserCode = string.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmLogin());
        }
        public static string DefaultPrinter;
        public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        public static string configstring;
        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();
        public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
    }
}
