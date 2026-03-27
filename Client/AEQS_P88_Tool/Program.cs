using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AEQS_P88_Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
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
            

            /*Client.APIURL = "http://localhost:60627/api/CommonCall";
            Client.UserToken = "44b3db7e-335d-41c0-8ebf-5800f3059a07";
            Client.Language = "cn";*/
            Application.Run(new frmMainToolP88());
            //Application.Run(new frmMainToolP88());
        }

        public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        public static string configstring;
        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();
        public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
    }
}
