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

namespace SJeMES_QCM.UControl
{
    public partial class UcChemicalsCode : UserControl
    {
        public string txt_no2 { get; set; }
        public string txt_name2 { get; set; }
        public string time2 { get; set; }
        public string txt_eff2 { get; set; }
        string _no = string.Empty;// 代号
        string _name = string.Empty;// 名称
        double _time = 0;
        public UcChemicalsCode(string no, string name,string time)
        {
            InitializeComponent();
            _no = no;
            _name = name;
            _time = Convert.ToDouble(time);
        }


        private void UcChemicalsCode_Load(object sender, EventArgs e)
        {
            string code = _no;
            if (!string.IsNullOrEmpty(code))
                this.pictureBox1.Image = QRCode.CreateQRCode(code);//  QRCode.CreateQRCode(code);

            if (!string.IsNullOrEmpty(_no))
                this.txt_no.Text = _no;
            if (!string.IsNullOrEmpty(_name))
                this.txt_name.Text = _name;

            this.txt_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            this.txt_eff.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss");


            txt_no2 = _no;
            txt_name2 = _name;
            time2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txt_eff2 = DateTime.Now.AddHours(_time).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
