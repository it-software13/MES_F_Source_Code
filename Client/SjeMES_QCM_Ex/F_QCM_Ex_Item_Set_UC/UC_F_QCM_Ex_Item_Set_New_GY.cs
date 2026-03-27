using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex.F_QCM_Ex_Item_Set_UC
{
    public partial class UC_F_QCM_Ex_Item_Set_New_GY : UserControl
    {
        public UC_F_QCM_Ex_Item_Set_New_GY(bool is_readonly = false)
        {
            InitializeComponent();
            if (is_readonly)
                ReadOnlyControl();
        }

        public void ReadOnlyControl()
        {
            foreach (Control item in this.Controls)
            {
                item.Enabled = false;
            }
        }

    }
}
