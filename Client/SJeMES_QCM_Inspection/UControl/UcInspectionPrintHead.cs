

using SJeMES_Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM_Inspection
{
    public partial class UcInspectionPrintHead : UserControl
    {
        string order = string.Empty;

        public UcInspectionPrintHead()
        {
            
            InitializeComponent();
        }

        public UcInspectionPrintHead(string order)
        {
            this.order = order;

            InitializeComponent();
        }

        private void SYKind_Click(object sender, EventArgs e)
        {

        }

        private void Head_Load(object sender, EventArgs e)
        {
            string code = order ;
            if (!string.IsNullOrEmpty(code))
                this.pictureBox1.Image =  QRCode.CreateQRCode(code);

        }
    }
}
