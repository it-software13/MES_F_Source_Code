using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QA
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_QA";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "QA鞋型管理":
                      frm = new F_QA_ShoeShape_Main(); 
                      break;
                    case "DQA鞋型管理":
                        frm = new F_DQA_ShoeShape_Main(); 
                        break;
                    case "MQA鞋型管理":
                        frm = new F_MQA_ShoeShape_Main(); 
                        break;
                    case "获取client":
                        return;

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
