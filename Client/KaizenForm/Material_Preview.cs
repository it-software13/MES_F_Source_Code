using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenForm
{
    public partial class Material_Preview : Form
    {
        public Material_Preview(DataTable dt, string path)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FastReportHelper.LoadFastReportMaterial(panel1, path, dic, dt, "Table");
        }
    }
}
