using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_Material_Issue
{
    class Interface
    {
		public static void RunApp(Object obj)
		{
			try
			{
                //Program.client = obj as SJeMES_Framework.Class.ClientClass;
                //Production_Material_Issue frm = new Production_Material_Issue();
                //FormCollection collection = Application.OpenForms;
                //frm.Owner = collection["frmMain"];
                //frm.Show();


                string interfaceName = "Production_Material_Issue";
                Program.Client = obj as SJeMES_Framework.Class.ClientClass;
                string FormName = Program.Client.FormName;
                FormCollection collection;
                collection = Application.OpenForms;

                Form frm = null;
                switch (FormName)
                {
                    case "Prod_Material_Request":
                        frm = new Production_Material_Request_View();
                        break;
                    case "Prod_Material_Issuing":
                        frm = new Production_Material_Submit();
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
				throw ex;
			}
		}
	}
}
