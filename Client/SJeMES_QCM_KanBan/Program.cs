using MaterialSkin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms; 

namespace SJeMES_QCM_KanBan
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
            string KanbanUrl = @"http://localhost:8013/#id=slo4v3&p=生产参数看板";
            Application.Run(new F_QCM_Kanban_ProductionParameter("生产参数看板", KanbanUrl));
        }

        public static string DefaultPrinter;
        public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        public static string configstring;
        public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();
        public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;
    }
}
