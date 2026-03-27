using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QA.UControl
{
    public partial class UCimg : UserControl
    {
        private string IMG_URl;
        public UCimg(string lujin)
        {
            InitializeComponent();
            IMG_URl = lujin;
        }

        private void UCimg_Load(object sender, EventArgs e)
        {
            try
            {
                var webC = new System.Net.WebClient();
                string url = Program.Client.PicUrl + IMG_URl;
                Image image = new Bitmap(webC.OpenRead(url));
                this.pictureBox1.Image = image;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
