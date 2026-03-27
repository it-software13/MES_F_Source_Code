using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace Cutting_LabelPrint
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
            client = new SJeMES_Framework.Class.ClientClass(); 
            client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            client.UserToken = "123bbad3-8098-4b9d-9843-6026158161a1";// Ss   
            client.Language = "en";

            Application.Run(new Cutting_LabelPrint());
        } 

        public static SJeMES_Framework.Class.ClientClass client;
    } 
} 
