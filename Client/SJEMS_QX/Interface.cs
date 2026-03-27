using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJEMS_QX
{
    public class Interface
    {
        public static void RunApp(object OBJ)
        {
            try
            {
                string sql = string.Empty;
                Program.WebServiceUrl = (OBJ as Dictionary<string, object>)["WebServiceUrl"] as string;

                bool IsMaxWindow = false;
                if ((OBJ as Dictionary<string, object>)["Org"] is GDSJ_Framework.Class.OrgClass)
                {
                    Program.Org = (OBJ as Dictionary<string, object>)["Org"] as GDSJ_Framework.Class.OrgClass;
                    Program.User = Program.Org.User.UserCode;
                }
                else
                {


                    Program.Org = new GDSJ_Framework.Class.OrgClass();

                    Program.Org.Org = (OBJ as Dictionary<string, object>)["Org"] as string;
                    Program.Org.OrgName = (OBJ as Dictionary<string, object>)["OrgName"] as string;
                    Program.Org.DBServer = (OBJ as Dictionary<string, object>)["DBServer"] as string;
                    Program.Org.DBType = (OBJ as Dictionary<string, object>)["DBType"] as string;
                    Program.Org.DBName = (OBJ as Dictionary<string, object>)["DBName"] as string;
                    Program.Org.DBUser = (OBJ as Dictionary<string, object>)["DBUser"] as string;
                    Program.Org.DBPassword = (OBJ as Dictionary<string, object>)["DBPassword"] as string;
                    Program.Language = (OBJ as Dictionary<string, object>)["Language"] as string;
                    IsMaxWindow = Convert.ToBoolean((OBJ as Dictionary<string, object>)["IsMaxWindow"]);

                }
                Program.DB= new GDSJ_Framework.DBHelper.DataBase("SqlServer", Program.Org.DBServer, Program.Org.DBName, Program.Org.DBUser, Program.Org.DBPassword, string.Empty);
                string FormName = (OBJ as Dictionary<string, object>)["FormName"] as string;



                Form frm;
                string formCode;
                switch (FormName)
                {
                    case "按钮授权":
                        frm = new Frm_ButtonPermissions();
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();
                        break;
                    case "字段授权":
                        frm = new Frm_FieldAuthorization();
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();
                        break;
                    case "权限设置":
                        frm = new Frm_PermissionSettings();
                        frm.StartPosition = FormStartPosition.CenterParent;
                        FormCollection collection = Application.OpenForms;
                        frm.Owner = collection["frmMain"];
                        frm.Show();
                        break;

                    case "多语言设置":
                        frm = new Frm_FieldMultilingual();
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();
                        break;
                    case "菜单多语言":
                        frm = new Frm_MenuMultilingual();
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();
                        break;
                    case "明细权限":
                        frm = new Frm_DetailedJurisdiction();
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.Show();
                        break;
                    case "测试":
                        frm = new Form2();
                        //frm.Owner = new frm;
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();
                        break;
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
