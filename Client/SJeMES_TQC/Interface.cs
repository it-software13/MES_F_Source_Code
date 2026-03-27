using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_TQC
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_TQC";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "TQC任务":
                        frm = new TQC_Task_Main(); 
                        break;
                    case "TQCTop3Defects":
                        frm = new TQCHourlyTop3Issues();
                        break;
                    case "TQC_Line_stop_Record":
                        frm = new TQC_Line_Stop_Record();
                        break;
                    case "Manual_RFT":
                        frm = new Manual_RFT();
                        break;
                    case "TQC_Bgrade_Report":
                        frm = new TQC_Bgrade_Report();
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
