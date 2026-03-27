using SJeMES_QCM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_BDM";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {

                    case "计算公式维护":
                        frm = new F_BDM_Formula_List();
                        break;

                    case "测试项目库":
                        frm = new F_QCM_Testltem_List();
                        break;

                    case "通用品质标准":
                        frm = new F_BDM_QualityStandard_Main();
                        break;
                    case "ART定制检测项":
                        frm = new F_BDM_ProdCustomQuality_Main();
                        break;
                    case "条码打印功能":
                        frm = new F_BDM_PrintBarCode_Main();
                        break;
                    case "AQL标准":
                        frm = new F_BDMAQL_Edit();
                        break;
                    case "试穿部样品存放管理":
                        frm = new F_BDM_FittingsampleLocation_Main();
                        break;
                    case "抽检品质监督报表":
                        frm = new F_QCM_Inspection_Supervision_report();
                        break;
                    case "送测频率":
                        frm = new F_BDM_SendTestFrequency_Main();
                        break;
                    case "工段创建":
                        frm = new F_BDM_WORKSHOP_SECTION_Main();
                        break;
                    case "参数项目":
                        frm = new BDM_PARAM_ITEM_M_Main();
                        break;
                    case "画皮":
                        frm = new F_BDM_Painted_Skin_Main();
                        break;
                    case "键帽设置":
                        frm = new F_BDM_KetCap_Main();
                        break;
                    case "设备型号":
                        frm = new F_BDM_DeviceType_Main();
                        break;
                    case "成品鞋-测试":
                        frm = new F_BDM_SHOSE_Test();
                        break;
                    case "材料-测试":
                        frm = new F_BDM_MATERIAL_TESTITEM_Main();
                        break;
                    case "工艺-测试":
                        frm = new F_BDM_WORKMANSHIP_TESTITEM_Main();
                        break;
                    case "部件-测试":
                        frm = new F_BDM_PARTS_TESTITEM_Main();
                        break;
                    case "车针管理":
                        frm = new F_BDM_Needlemanagement_Main();
                        break;
                    case "化学品信息看板":
                        frm = new BDM_Chemicalkanban();
                        break;
                    case "打印化学品条码":
                        frm = new BDM_Chemicalkanban_Print();
                        break;
                    case "移动端下载":
                        frm = new F_BDM_MobileTerminal_QrCode();
                        break;
                    case "设备校正保养":
                        frm = new BDM_Aeqinfom();
                        break;
                    case "品质目标":
                        frm = new BDM_Quality_Documents_Main("0");
                        break;
                    case "组织架构":
                        frm = new BDM_Quality_Documents_Main("1");
                        break;
                    case "WI":
                        frm = new BDM_Quality_Documents_Main("2");
                        break;
                    case "品质制度":
                        frm = new BDM_Quality_Documents_Main("3");
                        break;
                    case "培训文件":
                        frm = new BDM_Quality_Documents_Main("4");
                        break;
                    case "品质报告":
                        frm = new BDM_Quality_Documents_Main("5");
                        break;
                    case "政策":
                        frm = new BDM_Quality_Documents_Main("6");
                        break;
                    case "BPM":
                        frm = new BDM_Quality_Documents_Main("7");
                        break;
                    case "胶水危废处理报表":
                        frm = new BDM_ScrapGlueMag();
                        break;
                    case "移动端多语言设置":
                        frm = new FrmSetLanguageMApp();
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
