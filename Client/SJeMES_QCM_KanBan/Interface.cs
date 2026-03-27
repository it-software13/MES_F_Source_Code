using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM_KanBan
{
   public class Interface
    {
        public static void RunApp(Object obj)
        {
            try
            {
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;

                Form frm;
                FormCollection collection;
                string KanbanUrl = string.Empty;
                switch (FormName)
                {
                    case "ANQS品质看板":
                        KanbanUrl = Program.Client.PicUrl + @"/万邦看板/anqs品质看板.html";
                        frm = new F_QCM_Kanban_ProductionParameter(FormName, KanbanUrl);
                        collection = Application.OpenForms;
                        frm.Owner = collection["frmMain"];
                        frm.StartPosition = FormStartPosition.CenterParent;
                        //frm.TopMost = true;
                        frm.ShowDialog();
                        break;
                    case "生产参数看板":
                        KanbanUrl = Program.Client.PicUrl + @"/万邦看板/生产参数看板.html";
                        frm = new F_QCM_Kanban_ProductionParameter(FormName , KanbanUrl);
                        collection = Application.OpenForms;
                        frm.Owner = collection["frmMain"];
                        frm.StartPosition = FormStartPosition.CenterParent;
                        //frm.TopMost = true;
                        frm.ShowDialog();
                        break;
                    case "化学品看板":
                        KanbanUrl = Program.Client.PicUrl + @"/万邦看板/化学品看板.html";
                        frm = new F_QCM_Kanban_ProductionParameter(FormName , KanbanUrl);
                        collection = Application.OpenForms;
                        frm.Owner = collection["frmMain"];
                        frm.StartPosition = FormStartPosition.CenterParent;
                        //frm.TopMost = true;
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
