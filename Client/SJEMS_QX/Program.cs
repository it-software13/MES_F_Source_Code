using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections;
using Newtonsoft.Json;
using MaterialSkin;

namespace SJEMS_QX
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            WebServiceUrl = @"http://192.168.0.122:80/SJ-WebService.asmx";

            string sqlconn = ConfigurationSettings.AppSettings["ConStr"].ToString();
            string[] t = new string[1];
            t[0] = ";";
            string[] a = sqlconn.Split(t, StringSplitOptions.RemoveEmptyEntries);

            //configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");

            //Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);

            //Client.Language = Pconfig["language"];
            //Client.CompanyCode = Pconfig["org"];
            //Client.APIURL = Pconfig["api"];
            //Client.UserCode = Pconfig["usercode"];
            //Client.WebServiceUrl = Pconfig["webservice"];
            //WebService.Url = Client.WebServiceUrl;

            Org = new GDSJ_Framework.Class.OrgClass();
            Org.Org = "gdsj";
            Org.OrgName = "广东商基网络";
            Org.DBServer = a[3].ToString().Substring(12);
            Org.DBType = "SqlServer";
            Org.DBName = a[5].ToString().Substring(16);
            Org.DBUser = a[2].ToString().Substring(8);
            Org.DBPassword = a[4].ToString().Substring(9);

            DB = new GDSJ_Framework.DBHelper.DataBase();
            DB = new GDSJ_Framework.DBHelper.DataBase("SqlServer", Program.Org.DBServer, Program.Org.DBName, Program.Org.DBUser, Program.Org.DBPassword, string.Empty);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_PermissionSettings());
        }
        public static string User = "ADMIN";
        public static string WebServiceUrl;
        public static GDSJ_Framework.Class.OrgClass Org;
        public static GDSJ_Framework.DBHelper.DataBase DB;
        public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();
        public static string Language;
        public static string configstring;
    }
}
