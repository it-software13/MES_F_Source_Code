using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_AQL";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = new Form();
                switch (FormName)
                {
                    case "出货仓库维护":
                        frm = new F_AQL_ConfirmShoes_Warehouse("0");
                        break;
                    case "出货库位维护":
                        frm = new F_AQL_ConfirmShoes_Location("0");
                        break; 
                   case "验货计划列表":
                        frm = new F_AQL_Theinspectionplan();
                        break;
                    case "出货确认鞋条码打印":
                        frm = new F_AQL_ConfirmShoes_BarcodePrint("0");
                        break;
                    case "出货确认鞋存放管理":
                        frm = new F_AQL_ConfirmShoes_Store("0");
                        break;
                    case "CMA测试鞋":
                        frm = new F_AQL_CMAThetestshoes();
                        break;
                    case "鞋款材料成分维护":
                        frm = new F_AQL_ShoeMaterial_Composition();
                        break;
                    case "出货通知":
                        frm = new F_AQL_Shipment_Notice();
                        break;
                    case "AQL任务清单查询":
                        frm = new F_AQL_CmaTask_TaskList_Main();
                        break;
                    case "验货室订单查询":
                        frm = new F_AQL_CmaTask_Inspection_Main();
                        break;
                    case "原材料仓库维护":
                        frm = new F_AQL_ConfirmShoes_Warehouse("1");
                        break;
                    case "原材料库位维护":
                        frm = new F_AQL_ConfirmShoes_Location("1");
                        break;
                    case "原材料确认鞋条码打印":
                        frm = new F_AQL_ConfirmShoes_BarcodePrint("1");
                        break;
                    case "原材料确认鞋存放管理":
                        frm = new F_AQL_ConfirmShoes_Store("1");
                        break;
                    case "特殊包装资料文件上传":
                        frm = new F_AQL_SpcPkgFile_Upload();
                        break;
                    case "正式订单文件上传":
                        frm = new F_AQL_O_ORDER_Upload();
                        break;
                    case "BA_Reports":
                        frm = new F_AQL_BA_Reports();
                        break;
                    case "Repacking_Data":
                        frm = new F_AQL_RepackingDataEntry();
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
