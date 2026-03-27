using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_ZL_KanBan
{
    public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                string interfaceName = "SJeMES_ZL_KanBan";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "网页嵌套测试":
                        frm = new Form1();
                        break;
                    case "万邦中国区品质总看板":
                        //frm = new RegionAllKanBan();
                        frm = new RegionKanBan();
                        break;
                    case "产品全程品质记录"://  //   全生命品质报表
                        frm = new FrmWholeLifeMain();
                        break;
                    case "前段Q报表":
                        frm = new FrmAppearance();
                        break;
                    case "AQL订单报表":
                        frm = new FrmOrder();
                        break;
                    case "测试部品质看板":
                        frm = new FrmTestDepartment();
                        break;
                    case "车间Q品质看板":
                        frm = new FrmWorkshopQuality();
                        break;
                    case "市场反馈看板":
                        frm = new FrmReturn();
                        break;
                }
                //FrmTestDepartment
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
