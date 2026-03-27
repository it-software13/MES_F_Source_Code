using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cutting_LabelPrint
{
    class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                Cutting_LabelPrint frm = new Cutting_LabelPrint();
                FormCollection collection = Application.OpenForms;
                frm.Owner = collection["frmMain"];
                frm.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
