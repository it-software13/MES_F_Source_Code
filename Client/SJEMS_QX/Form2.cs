using SJeMES_Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJEMS_QX
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
             
            Program.Client.APIURL = "http://localhost:60627//api/CommonCall";
            Program.Client.UserToken = "ccc64570-2d4a-4e0a-8eac-9bf88f2d0a44"; 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //打开文件夹操作

            //FolderBrowserDialog fd = new FolderBrowserDialog();

            //if (fd.ShowDialog() == DialogResult.OK)
            //{
            //    label1.Text = fd.SelectedPath;

            //}

            //选择显示图片操作

            OpenFileDialog openFi = new OpenFileDialog();
            openFi.Filter = "图像文件(JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png| JPeg 图像文件(*.jpg;*.jpeg)"
              + "|*.jpg;*.jpeg |GIF 图像文件(*.gif)|*.gif |BMP图像文件(*.bmp)|*.bmp|Tiff图像文件(*.tif;*.tiff)|*.tif;*.tiff|Png图像文件(*.png)"
              + "| *.png |所有文件(*.*)|*.*";
            if (openFi.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFi.FileName;
                PNAME = openFi.SafeFileName;//图片名称
                imgPath = openFi.FileName; //文件路径
            }


        }

        string imgPath = string.Empty;
        string PNAME = string.Empty; 

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            Byte[] imgByte = new Byte[file.Length];//把图片转成 Byte型 二进制流
            file.Read(imgByte, 0, imgByte.Length);//把二进制流读入缓冲区
            file.Close();

            Dictionary<string, object> P = new Dictionary<string, object>();
            P.Add("PHOTO", imgByte);
            P.Add("PNAME", PNAME);
            P.Add("PURL", imgPath);
            
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
             "SJ_SYSAPI", "SJ_SYSAPI.SendMESSAGETest", "SaveImg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(P));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            bool IsSuccess = Convert.ToBoolean(ret["IsSuccess"].ToString());
            if (IsSuccess)
            {
                string reult = ret["RetData"].ToString();
                MessageBox.Show(reult);
            }
            else
            {
                MessageBox.Show(ret["ErrMsg"].ToString());
            }
        }
    }
}
