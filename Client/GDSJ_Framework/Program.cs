using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GDSJ_Framework
{
    public static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        { 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new GDSJ_Framework.Printer.frmBarCodePrinter("http://127.0.0.1:80/SJ-WebService.asmx","SELECT packing_barcode from CODE003M(NOLOCK)"));
            //Application.Run(new GDSJ_Framework.WinForm.FormDesign.FormDesignManager());

            WebServiceUrl = @"http://127.0.0.1:8080/SJ-WebService.asmx";

            DataTable dt = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(
                GDSJ_Framework.Common.WebServiceHelper.RunService(Program.WebServiceUrl, "SJEMS_API", "SJEMS_API.SYS", "GetOrg", new Dictionary<string, string>()));

            Org = new GDSJ_Framework.Class.OrgClass();

            if (dt.Rows.Count > 0)
            {
                Org.Org = dt.Rows[0]["org"].ToString();
                Org.OrgName = dt.Rows[0]["orgname"].ToString();
                Org.DBType = dt.Rows[0]["dbtype"].ToString();
                Org.DBServer = dt.Rows[0]["dbserver"].ToString();
                Org.DBName = dt.Rows[0]["dbname"].ToString();
                Org.DBUser = dt.Rows[0]["dbuser"].ToString();
                Org.DBPassword = dt.Rows[0]["dbpassword"].ToString();
            } 
            try
            {
                string XML = GDSJ_Framework.Common.WebServiceHelper.GetAppXML(Program.WebServiceUrl, "PC_BASE020");

                Application.Run(new GDSJ_Framework.WinForm.FormXML.Forms.FormHB(XML,WebServiceUrl,Org));
            }
            catch (Exception EX) { MessageBox.Show(EX.Message); }
        }

        public static Class.OrgClass Org;
        public static string WebServiceUrl; 
        public static string LoadLastRow;
    }
}
