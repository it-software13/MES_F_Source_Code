using PlanningSchedule_Reports;
using SJeMES_Framework.Class;

namespace SizeWisePlanningScheduleReports
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 
            client = new ClientClass(); 
            client.APIURL = "http://localhost:60626/api/CommonCall"; 
            client.UserToken = "945acd9f-2fc7-4a5d-8dfa-7ff1941c7a19"; 
            client.Language = "en"; 
            Application.Run(new SizeReports());   
        }
        public static ClientClass client;   
    }
}