using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AEQS_P88_Tool
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;//
                //FrmToolMain frm = new FrmToolMain();
                frmMainToolP88 frm = new frmMainToolP88();
                FormCollection collection = Application.OpenForms;
                frm.Owner = collection["frmMain"];
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
