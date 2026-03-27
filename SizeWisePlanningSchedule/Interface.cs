using System;
using System.Windows.Forms;
using PlanningSchedule;
using SJeMES_Framework.Class;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SizeWisePlanningSchedule
{
    public class Interface 
    { 
        public static void RunApp(object obj)  
        { 
            try 
            { 
                Program.client = obj as ClientClass;
                Welcome frm = new Welcome(); 
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