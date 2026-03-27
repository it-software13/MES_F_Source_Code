using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SjeMES_QCM_Ex";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "送测登记":
                        //frm = new F_QCM_Ex_Shose();
                        frm = new F_QCM_Ex_Shose_New();
                        break;
                    case "送测登记清单":
                        frm = new F_QCM_Ex_List(); 
                        break;
                    case "测试样品签收/取走":
                        frm = new F_QCM_Ex_OutIn(); 
                        break;
                    case "实验室库位资料":
                        frm = new F_QCM_Ex_Stock(); 
                        break;
                    case "实验室仓库资料":
                        frm = new F_QCM_Ex_Wh(); 
                        break;
                    case "设备信息二维码打印":
                        frm = new F_QCM_Ex_Dev();
                        break;
                    case "实验室存档管理":
                        frm = new F_QCM_Ex_file(); 
                        break;
                    case "APP2查询":
                        frm = new F_QCM_Ex_APP_Compliance(); 
                        break;
                    case "产线二维码打印":
                        frm = new F_QCM_Ex_Line();
                        break;
                    case "APP2报告上传":
                        frm = new F_QCM_Ex_app_t_fileUpload();
                        break;
                    case "FGT_Required_List":
                        frm = new FGT_Required_List();
                        break;
                    case "FGT_Requested_List":
                        frm = new FGT_Requested_List();
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

    public class code_name_obj
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
    }
}
