using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_User
{
   public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            { 
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                FrmUserInfo frm = new FrmUserInfo();
                FormCollection collection = Application.OpenForms;
                frm.Owner= collection["frmMain"];
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
