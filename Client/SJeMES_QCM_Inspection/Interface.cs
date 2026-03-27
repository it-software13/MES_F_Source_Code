using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM_Inspection
{
   public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_QCM_Inspection";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "实验室送检登记":
                        frm = new InspectionTest(); 
                        break;
                    case "送检结果查看":
                        frm = new F_QCM_InspectionResult(); 
                        break;
                    case "进仓材料检验清单":
                        frm = new F_QCM_IncominglnspectionList(); 
                        break;
                    case "CLIMA清单处理":
                        frm = new F_QCM_ClimaList(); 
                        break;
                    case "试穿送检登记":
                        frm = new F_QCM_Fitting_inspection_Main(); 
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
