using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace F_TailorRounds
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
             client.APIURL = "http://localhost:60626/api/CommonCall"; 
            // client.APIURL = "http://10.3.0.29:8082/api/CommonCall"; 
            // client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            //client.UserToken = "1b6e17b6-ea74-482d-8767-2118cfdba8d2";//测试
            client.UserToken = "c7c62394-44d6-48f3-af66-e94ae8e8c721";//正式  
            client.Language = "en"; 
            Application.Run(new F_TailorRounds()); 
        } 


        public static string getFont()
        {
            if (client.Language == "cn")
                return "SimSun";
            if (client.Language == "en")
                return "Gadugi";
            return "Gadugi";
        }

        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls);
        }

        public static ClientClass client;
    }
}
