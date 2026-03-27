using SJeMES_Control_Library.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Control_Library
{
    public class ShowFileHelper
    {
        public static void ShowFile(string url, string file_name = "")
        {
            string exp = url.Substring(url.LastIndexOf(".")).ToLower();
            if (exp == ".jpeg" || exp == ".git" || exp == ".png" || exp == ".bmp" || exp == ".jpg")
            {
                FrmShowImg add = new FrmShowImg(url, file_name);
                add.ShowDialog();
            }
            else
            {
                FrmShowFile add = new FrmShowFile(url, file_name);
                add.ShowDialog();
            }
        }
    }
}
