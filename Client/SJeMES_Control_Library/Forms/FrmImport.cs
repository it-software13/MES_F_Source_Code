using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Forms
{
    public partial class FrmImport : Form
    {
        public bool is_sure = false;
        DataTable _dt = null;
        public FrmImport(DataTable dt)
        {
            InitializeComponent();
            _dt = dt;
        }

        private void FrmImport_Load(object sender, EventArgs e)
        {
            if(_dt!=null)
            {
                dataGridView1.DataSource = _dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            is_sure = true;
            this.Close();
        }
    }
}
