

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

    public partial class Item : UserControl
    {
        string id = string.Empty; //序号
        string order = string.Empty; //检测单号
        string test_no = string.Empty; //检测编号
        string testitem_name = string.Empty; // 检测名称
        string sample_num = string.Empty; // 试样数量
        string qr_code = string.Empty;// 二维码
        public Item()
        {
            InitializeComponent();
        }
        public Item(int id, string order, string test_no, string testitem_name, string sample_num, string qr_code)
        {
            this.id = id.ToString();
            this.order = order;
            this.test_no = test_no;
            this.testitem_name = testitem_name;
            this.sample_num = sample_num;
            this.qr_code = qr_code;


            InitializeComponent();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void JYName_Click(object sender, EventArgs e)
        {

        }

        private void Item_Load(object sender, EventArgs e)
        {
            this.lab_seq.Text = this.id;
            this.JYDNo.Text = this.order;
            this.JYXNo.Text = this.test_no; //生成编号
            this.JYName.Text = this.testitem_name;
            this.SY_qty.Text = this.sample_num;
            string code = qr_code;
           if (!string.IsNullOrEmpty(code))
                this.pictureBox1.Image = QRCode.CreateQRCode(code);//  QRCode.CreateQRCode(code);

        }
    }
}
