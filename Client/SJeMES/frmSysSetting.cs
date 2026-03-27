using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SJeMES_Framework;

namespace SJeMES
{
    public partial class frmSysSetting : Form
    {
        public frmSysSetting()
        {
            InitializeComponent();
        }

        private void frmSysSetting_Load(object sender, EventArgs e)
        {
            try
            {
                txtServer.Text = SJeMES_Framework.Common.ConfigHelper.getSetting("Server");
                txtPort.Text = SJeMES_Framework.Common.ConfigHelper.getSetting("Port");
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            SJeMES_Framework.Common.ConfigHelper.updateSeeting("Server", txtServer.Text.Trim());
            SJeMES_Framework.Common.ConfigHelper.updateSeeting("Port", txtPort.Text.Trim());

            Program.SetWebServiceUrl();

            MessageBox.Show("保存成功");
        }
    }
}
