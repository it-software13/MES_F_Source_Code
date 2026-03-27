
using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPIINPUT
{
    static class Program
    {
        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "fa4830af-5415-4b44-8b13-81043f5b5a15";
            client.Language = "en";
            Application.Run(new PowerSavings());
        }
        public static ClientClass client;


    }
}
