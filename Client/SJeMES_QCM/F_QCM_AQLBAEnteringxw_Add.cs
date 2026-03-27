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
    public partial class F_QCM_AQLBAEnteringxw_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_AQLBAEnteringxw_Add()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
     Program.SkinThemes, materialSkinManager, this);
           
            InitializeComponent();
        }

        private void F_QCM_AQLBAEnteringxw_Add_Load(object sender, EventArgs e)
        {
            
           
        }
    }
}
