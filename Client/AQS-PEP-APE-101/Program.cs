using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AQS_PEP_APE_101
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
            //Application.Run(new F_QCM_TrackIn_Main());
        }
        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();
    }
}
