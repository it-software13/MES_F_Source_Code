
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
   public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_QCM";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "发外厂商色卡":
                        frm = new F_QCM_ExternalColorCard_Main(); 
                        break;
                    case "实验室样品管理":
                        frm = new F_QCM_LaboratorySampleStorage_Main(); 
                        break;
                    case "化学品记录":
                        frm = new F_QCM_ComplianceMangement_Main(); 
                        break;
                    case "A-01合规管理":
                        frm = new F_QCM_ComplianceManagement_Main(); 
                        break;
                    case "鞋面厂商品质审核标准":
                        frm = new F_QCM_VampQualityAudit_Main(); 
                        break;
                    case "检验工具保养记录":
                        frm = new F_QCM_ChecktoolMaintenance_Main(); 
                        break;
                    case "鞋面厂商品质审核历史记录":
                        frm = new F_QCM_VampQualityAudit_List(); 
                        break;
                    case "金属检验":
                        frm = new F_QCM_Metalinspection_Main(); 
                        break;
                    case "产线不良问题管理":
                        frm = new F_QCM_Productionline_Defects_Main(); 
                        break;
                    case "SATRA皮料评估报表":
                        frm = new F_QCM_SatraLeatherEvaluationTable(); 
                        break;
                    case "进仓异常材料统计":
                        frm = new F_QCM_InwarehouseAnomalyStat(); 
                        break;
                    case "ART验货文件绑定":
                        frm = new F_QCM_ATR_File1(); 
                        break;
                    case "ARTFDVS文件绑定":
                        frm = new F_QCM_ATR_File2(); 
                        break;
                    case "ART测试文件绑定":
                        frm = new F_QCM_ATR_File3(); 
                        break;
                    case "AQL文件核对":
                        frm = new F_QCM_ART_File_Detail(); 
                        break;
                    case "重检报告":
                        frm = new F_QCM_Reinspectionreport_Main(); 
                        break;
                    case "品质异常呈报单":
                        frm = new F_QCM_AbnormalReport_Main(); 
                        break;
                    case "首件确认记录表":
                        frm = new F_QCM_Firstarticle_confirm_Main(); 
                        break;
                    case "发外厂商品质体系审核标准":
                        frm = new F_QCM_Bdmoutqualitylistm_Main(); 
                        break;
                    case "品质审核历史记录":
                        frm = new F_QCM_BdmoutqualitylistmSelect(); 
                        break;

                    case "量产试作清单":
                        frm = new F_QCM_BATCH_PRODUCTION(); 
                        break;
                    case "客户投诉":
                        frm = new F_QCM_CUSTOMER_COMPLAINT_Main(); 
                        break;
                    case "鞋面进度表（针车）":
                        frm = new F_QCM_Vampschedule_Main(); 
                        break;
                    case "不良退货":
                        frm = new F_QCM_BadReturnMain(); 
                        break;
                    case "品质异常明细":
                        frm = new F_QCM_QualityExceptionHandling_Main(); 
                        break;
                    case "创建确认鞋(成品)":
                        frm = new F_QCM_ConfirmShoes_Main("1"); 
                        break;
                    case "创建确认鞋(原材料)":
                        frm = new F_QCM_ConfirmShoes_Main("0"); 
                        break;
                    case "成品出货看板":
                        frm = new F_QCM_ProductDelivery(); 
                        frm.ShowDialog();
                        break;
                    case "鞋面厂月度AQL数据汇总":
                        frm = new F_QCM_VampQualityAudit_board(); 
                        break;
                    case "原材料检验看板":
                        frm = new F_QCM_Ravwmaterialinspection_Main(); 
                        break;
                    case "巡线":
                        frm = new F_QCM_RQCPatrol_Main(); 
                        break;
                    case "CWA状态查询":
                        frm = new F_QCM_CWAState_Main(); 
                        break;
                    case "体系文件维护":
                        frm = new F_QCM_SystemFileMaintenance_Main(); 
                        break;
                    case "AQL-BA录入":
                        //frm = new F_QCM_AQLBAEntering_Main();
                        frm = new frmAQLBAInsert(); 
                        break;
                    case "TQC":
                        frm = new F_QCM_TQC_Main(); 
                        break;
                    case "画皮记录列表":
                        frm = new F_QCM_PaintedSkinRecords(); 
                        break;
                    case "ART合规A-01查询":
                        frm = new F_QCM_VampQuality_query(); 
                        break;
					case "化学品信息创建":
                        frm = new F_QCM_Chemical_information_create(); 
                        break;
                    case "测钉机矫正记录":
                        frm = new F_QCM_Measuringthenailcorrection_Main(); 
                        break;
					case "断针记录":
                        frm = new F_QCM_Broken_Needle_Main(); 
                        break;
                    case "鞋面厂月度数据报表":
                        frm = new F_QCM_VampQualityAudit_board(); 
                        break;
                    case "抽检品质监督报表":
                        frm = new F_QCM_Inspection_Supervision_report(); 
                        break;
                    case "AQL任务清单":
                        frm = new F_QCM_TaskList(); 
                        break;
                    case "化学品打印条码功能":
                        frm = new F_QCM_ChemicalPrint(); 
                        break;
                    case "联名产品文件类型管理":
                        frm = new F_QCM_Filesupload(); 
                        break;
                    case "安全合规文件管理":
                        frm = new F_QCM_Filesupload2(); 
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
