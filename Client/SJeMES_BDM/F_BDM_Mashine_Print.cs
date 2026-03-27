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

namespace SJeMES_BDM
{
    
    public partial class F_BDM_Mashine_Print : MaterialForm
    {
        public F_BDM_PrintBarCode_Main _MachineCode { get; set; }
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_Mashine_Print(F_BDM_PrintBarCode_Main frm)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            //_MachineCode = frm.MachineCode.ToString(); 
            _MachineCode = frm;

        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_mashine_no.Text))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择设备编号！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            _MachineCode.MachineCode = this.txt_mashine_no.Text;
           // string mashine_no = this.txt_mashine_no.Text;
            this.Close();

        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
