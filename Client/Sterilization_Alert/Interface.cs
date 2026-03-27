//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Sterilization_Alert
//{
//    public class Interface
//    {
//        public static void RunApp(Object obj)
//        {
//            try
//            {
//                Program.Client = obj as SJeMES_Framework.Class.ClientClass;

//                Sterilization_Alert_System frm = new Sterilization_Alert_System();
//                FormCollection collection = Application.OpenForms;
//                frm.Owner = collection["frmMain"];

//                //frm.StartPosition = FormStartPosition.CenterParent;
//                //frm.TopMost = true;
//                frm.Show();
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sterilization_Alert
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "Sterilization_Entry";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "Sterilization_Entry":
                        frm = new Sterilization_Alert_System();
                        break;
                    case "Email_Alert_Days_Submit":
                        frm = new Email_Alert_Days_Submit();
                        break;
                }


                var findFrm = collection[interfaceName + FormName];
                if (findFrm == null)
                {
                    frm.Owner = collection["frmMain"];
                    frm.Name = interfaceName + FormName;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.Show();
                }
                else
                {
                    findFrm.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}