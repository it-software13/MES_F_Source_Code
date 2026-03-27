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
    public partial class F_QCM_VLaboratorytestresults_Vews : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_VLaboratorytestresults_Vews(Dictionary<string,object> dic)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
