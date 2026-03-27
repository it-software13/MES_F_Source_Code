using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N_WMS_MaterialMatchingTrackReport
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                Program.client = obj as SJeMES_Framework.Class.ClientClass;
                N_WMS_MaterialMatchingTrackReport frm = new N_WMS_MaterialMatchingTrackReport();
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
