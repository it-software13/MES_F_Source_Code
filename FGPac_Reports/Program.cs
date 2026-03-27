using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGPac_Reports
{
    internal static class Program           
    {
        /// <summary> 
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new SJeMES_Framework.Class.ClientClass();
            client.APIURL = "http://localhost:60626//api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090//api/CommonCall"; 
            client.UserToken = "11835ebb-89db-408c-bab9-09ec3ecc126e";     
            client.Language = "en";
            Application.Run(new FGPac_Reports()); 
        }
        public static SJeMES_Framework.Class.ClientClass client;  
    }
}
