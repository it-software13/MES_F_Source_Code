using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SizeWisePlanningSchedule;
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
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "61fc4de1-6a84-4084-9a7c-9dfbe7278e2f";        
            client.Language = "en";            
            Application.Run(new Welcome());   
        } 
        public static ClientClass client;  
    } 
}
