using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Report
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
                switch (FormName)
                {
                    
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
