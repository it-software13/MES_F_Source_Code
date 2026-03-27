using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_TSM
{
    class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_TSM";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "Registration":
                        frm = new Registration();
                        break;
                    case "Production_Adjustment":
                        frm = new Production_Adjustment();
                        break;
                    case "Training_Emp_Attandance":
                        frm = new Training_Emp_Attendance();
                        break;
                    case "Skill Score Evaluation":
                        frm = new Skill_Score_Evaluation();
                        break;
                    case "Skill Matrix":
                        frm = new Skill_Matrix();
                        break;
                    case "ManDayHours":
                        frm = new Manday_Hours();
                        break;
                    case "Multi_Skill_Bonus_Calculation":
                        frm = new Multi_Skill_Bonus_Calculation();
                        break;
                    case "Termination_Emp_List":
                        frm = new Termination_Emp_List();
                        break;
                    case "APC_SUPPLEMENTARY_DATA":
                        frm = new APC_SupplementaryData();
                        break;
                    case "B GRADE REPORTS":
                        frm = new B_Grade_Reports();
                        break;
                    case "Training_Status":
                        frm = new Monthly_Training_Status();
                        break;
                    case "Process_List":
                        frm = new Process_List();
                        break;
                    case "Skill_Matrix_Report":
                        frm = new Skill_Matrix_Report();
                        break;
                    case "Employee_Absent_Entry":
                        frm = new Employee_Absent_Entry();
                        break;
                    case "MPAC_Allocation":
                        frm = new MPAC_Allocation();
                        break;
                    case "Excess_Employee_Entry":
                        frm = new Excess_Employee_Entry();
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
