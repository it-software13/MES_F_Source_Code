using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMESSystemTools
{
   public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;

                Frm_SJeMESSystemTools frm = new Frm_SJeMESSystemTools();
                FormCollection collection = Application.OpenForms;
                frm.Owner = collection["frmMain"];

                //frm.StartPosition = FormStartPosition.CenterParent;
                //frm.TopMost = true;
                frm.Show();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
