using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Control_Library
{
    public class MessageHelper
    {
        public static void ShowErr(System.Windows.Forms.Form f ,string ErrMsg)
        {
            if (!string.IsNullOrEmpty(ErrMsg))
            {
                if (ErrMsg.Contains("用户已在另一个地方登陆"))
                {
                    string Apppath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
                    Apppath = Path.GetDirectoryName(Apppath);
                    System.Diagnostics.Process.Start(Apppath + @"\SJeMESClient.exe", "");
                    System.Environment.Exit(0);
                }
                else
                {
                    SJeMES_Control_Library.Forms.FrmTips.ShowTips(f, ErrMsg, 5000, true, ContentAlignment.MiddleCenter, null, SJeMES_Control_Library.Forms.TipsSizeMode.Medium,
                            new Size(300, 80), SJeMES_Control_Library.Forms.TipsState.Error);
                }
            }
        }

        public static void ShowSuccess(System.Windows.Forms.Form f, string Msg)
        {
            SJeMES_Control_Library.Forms.FrmTips.ShowTips(f, Msg, 2000, true, ContentAlignment.MiddleCenter, null, SJeMES_Control_Library.Forms.TipsSizeMode.Medium,
                    new Size(300, 50), SJeMES_Control_Library.Forms.TipsState.Success);
        }


        public static System.Windows.Forms.DialogResult ShowWarning(System.Windows.Forms.Form f, string WarningMsg)
        {
            SJeMES_Control_Library.Forms.FrmWithOKCancel1 tmp = new Forms.FrmWithOKCancel1();
            //tmp.Title = "警告";
            tmp.Title = "Warning";
            tmp.Msg = WarningMsg;
            return tmp.ShowDialog();
        }
        public static System.Windows.Forms.DialogResult ShowOK(System.Windows.Forms.Form f, string Msg)
        {
            SJeMES_Control_Library.Forms.FrmWithOKCancel1 tmp = new Forms.FrmWithOKCancel1();
            tmp.Title = "OK";
            tmp.Msg = Msg;
            return tmp.ShowDialog();
        }
    }
}
