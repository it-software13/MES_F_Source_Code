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

namespace SJeMES_BDM.UControl
{
    public partial class UcBarCode : UserControl
    {
        string Name = string.Empty;// 名称
        string qr_code = string.Empty;// 二维码
        public UcBarCode(string code,string name)
        {
            InitializeComponent();
            qr_code = code;
            Name = name;
        }

        private void UcBarCode_Load(object sender, EventArgs e)
        {
            string code = qr_code;
            if (!string.IsNullOrEmpty(code))
                this.pictureBox1.Image = QRCode.CreateQRCode(code);//  QRCode.CreateQRCode(code);
            if (!string.IsNullOrEmpty(Name))
                this.txt_Name.Text = Name;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
