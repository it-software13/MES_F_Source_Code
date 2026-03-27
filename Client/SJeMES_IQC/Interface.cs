using SJeMES_IQC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_IQC";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "进仓材料检验清单":
                       frm = new F_IQC_VWarehouse_Main(); 
                     break;
                    case "T2厂商上传报告":
                        frm = new F_IQC_Vendor_Report_Main(); 
                        break;
                    case "IQC-不良报告":
                        frm = new F_IQC_Bad_Report_Main(); 
                        break;
                    //case "原材料仓库维护":
                    //    frm = new F_IQC_ConfirmShoes_Warehouse();
                    //    collection = Application.OpenForms;
                    //    frm.Owner = collection["frmMain"];
                    //    frm.StartPosition = FormStartPosition.CenterParent;
                    //    //frm.TopMost = true;
                    //    frm.ShowDialog();
                    //    break;
                    //case "原材料库位维护":
                    //    frm = new F_IQC_ConfirmShoes_Location();
                    //    collection = Application.OpenForms;
                    //    frm.Owner = collection["frmMain"];
                    //    frm.StartPosition = FormStartPosition.CenterParent;
                    //    //frm.TopMost = true;
                    //    frm.ShowDialog();
                    //    break;
                    //case "原材料确认鞋条码打印":
                    //    frm = new F_IQC_ConfirmShoes_BarcodePrint();
                    //    collection = Application.OpenForms;
                    //    frm.Owner = collection["frmMain"];
                    //    frm.StartPosition = FormStartPosition.CenterParent;
                    //    //frm.TopMost = true;
                    //    frm.ShowDialog();
                    //    break;
                    //case "原材料确认鞋存放管理":
                    //    frm = new F_IQC_ConfirmShoes_Store();
                    //    collection = Application.OpenForms;
                    //    frm.Owner = collection["frmMain"];
                    //    frm.StartPosition = FormStartPosition.CenterParent;
                    //    //frm.TopMost = true;
                    //    frm.ShowDialog();
                    //    break;
                    case "中国区客户退货数据录入":
                        frm = new F_IQC_Marketfeedback_Main(); 
                        break; 
                     //case "客户退货数据录入":
                     //   frm = new F_IQC_Marketfeedback_Main2();
                    case "客户退货数据录入":
                        frm = new F_IQC_Customer_Return_List();
                        break;
                    case "客户投诉":
                        frm = new F_IQC_Customer_Complaint_Main(); 
                        break;
                    case "Color_Notice_Upload":
                        frm = new ColorNotice_Upload();
                        break;
                    case "View_Color_Notice":
                        frm = new View_Color_Notice();
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
