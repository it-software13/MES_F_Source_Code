using MaterialSkin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_Material_Issue
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Client = new SJeMES_Framework.Class.ClientClass();
            //Client.APIURL = "http://localhost:60626/api/CommonCall";
            ////client.APIURL = "http://10.3.0.24:8082/api/CommonCall";
            //// client.UserToken = "dac7074c-2e56-4606-b916-f77f30789f7e";//
            //Client.UserToken = "99560c54-9b3e-41f5-a735-3ba1cb376b0c";//
            //// client.UserToken = "3b1565ae-0e41-4bec-ba20-319f42e7b629";//
            //Client.Language = "en";
            ////Application.Run(new Production_Material_Request_View());
            //Application.Run(new Production_Material_Issue());

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
        }
        public static string DefaultPrinter;
        public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        public static string configstring;
        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();
        public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
        //public static SJeMES_Framework.Class.ClientClass Client;
       // public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
    }
}
