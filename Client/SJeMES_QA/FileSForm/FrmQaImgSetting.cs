using MaterialSkin.Controls;
using SJeMES_QA.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QA.FileSForm
{
    public partial class FrmQaImgSetting : MaterialForm
    {
        public List<string> image_guid_res;
        public bool btn_visible;
        public FrmQaImgSetting(List<string> _image_guid_res, bool _btn_visible = true)
        {
            InitializeComponent();
            image_guid_res = _image_guid_res;
            btn_visible = _btn_visible;
        }

        private void FrmQaImgSetting_Load(object sender, EventArgs e)
        {
            InitialImgControl();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public void InitialImgControl()
        {
            pl_imgs.Controls.Clear();
            int y_location = 0;
            var image_guid_res_list = image_guid_res[0].Split(',').ToList().Where(x => !string.IsNullOrEmpty(x)).ToList();
            foreach (var img_info in image_guid_res_list)
            {
                var img_info_arr = img_info.Split(':').ToList();
                UCImgSet ucImgSet = new UCImgSet(this, img_info_arr[0].ToString());
                ucImgSet.Name = $@"uc_{img_info[0]}";
                ucImgSet.btn_del.Visible = btn_visible;
                ucImgSet.btn_select.Visible = btn_visible;
                try
                {
                    string url = Program.Client.PicUrl + img_info_arr[2].ToString();
                    var webC = new System.Net.WebClient();
                    Image image = new Bitmap(webC.OpenRead(url));
                    ucImgSet.pb_img.Image = image;
                }
                catch 
                {
                }
                if (img_info_arr[1].ToString() == "1")
                    ucImgSet.BorderStyle = BorderStyle.FixedSingle;
                pl_imgs.Controls.Add(ucImgSet);
                ucImgSet.Location = new Point(1, y_location);
                y_location += ucImgSet.Height + 5;
            }
        }

    }
}
