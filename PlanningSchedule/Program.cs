using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace PlanningSchedule
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
            client = new ClientClass(); 
           // client.APIURL = "http://localhost:60626/api/CommonCall"; 
            client.APIURL = "http://10.3.0.24:8092/api/CommonCall";
            client.UserToken = "0a6f859d-80a7-41a2-80d1-e05b85af44d1";                    
            client.Language = "en" ;             
            Application.Run(new PlanningSchdule());   
        }  

        public static ClientClass client;  

    }  
} 
