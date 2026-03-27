using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N_WMS_MaterialMatchingTrackReport
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
            client.UserToken = "8f6199be-9a45-43dd-ae62-7326ab84ea11";    

            client.Language = "en";
            Application.Run(new N_WMS_MaterialMatchingTrackReport());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
