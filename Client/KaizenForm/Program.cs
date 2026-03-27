using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;

namespace KaizenForm
{
    static class Program
    {

        //    /// <summary>
        //    /// The main entry point for the application.
        //    /// </summary>
        //    [STAThread]

        //    static void Main()
        //    {
        //        //Application.EnableVisualStyles();
        //        //Application.SetCompatibleTextRenderingDefault(false);
        //        //client = new SJeMES_Framework.Class.ClientClass();
      //       client.APIURL = "http://localhost:60626/api/CommonCall";
        //        ////client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
        //        //client.UploadUrl = "http://10.3.0.29:8011/api/commoncall";
        //        //client.PicUrl = "http://10.3.0.29:8011";
        //        //client.UserToken = "009f2595-6feb-49e4-9473-4496fd3f125d";//
        //        //client.Language = "en";
        //        //Application.Run(new Kaizan_reports());


        //        try
        //        {
        //            configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");
        //            Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);
        //            client.Language = string.Empty;
        //            client.CompanyCode = string.Empty;
        //            client.APIURL = Pconfig["api"];
        //            client.UserCode = string.Empty;

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }

        //        Application.EnableVisualStyles();
        //        Application.SetCompatibleTextRenderingDefault(false);
        //    }
        //    public static string DefaultPrinter;
        //    public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        //    public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        //    public static string configstring;
        //    public static SJeMES_Framework.Class.ClientClass client = new SJeMES_Framework.Class.ClientClass();
        //    public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
        //}

        //   // public static SJeMES_Framework.Class.ClientClass client;


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

                client.Language = string.Empty;
                client.CompanyCode = string.Empty;
                client.APIURL = Pconfig["api"];
                client.UserCode = string.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
       // public static string DefaultPrinter;
        public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        public static string configstring;
        public static SJeMES_Framework.Class.ClientClass client = new SJeMES_Framework.Class.ClientClass();
        public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
    }
}

