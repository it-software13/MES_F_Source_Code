using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace PlanningSchedule_Reports 
{
    public class Interface
    {
        public static void RunApp(object obj)
        {
            try
            {
                Program.client = obj as ClientClass;
                PlanningSchdule_Reports frm = new PlanningSchdule_Reports();
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