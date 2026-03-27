using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_Chemical_information_create_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Chemical_information_create_Edit()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        DataTable h = new DataTable();
        public DataTable ha
        {
            get { return h; }
            set { h = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            h.Columns.Add("chemicals_no", typeof(object));
            h.Columns.Add("chemicals_name", typeof(object));
            h.Columns.Add("validtime", typeof(object));
            h.Rows.Add();
            h.Rows[0]["chemicals_no"] = txtchemicals_no.Text;
            h.Rows[0]["chemicals_name"] = txtchemicals_name.Text;
            h.Rows[0]["validtime"] = datevalidtime.Value.ToString("HH");
            this.Close();
        }
    }
}
