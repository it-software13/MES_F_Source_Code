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
    public partial class _6s_Previewcs : Form
    {
        public _6s_Previewcs(DataTable dt, string path)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FastReportHelper.LoadFastReport2(panel1, path, dic, dt, "Table");
        }










    }
}
