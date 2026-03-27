using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png)|.jpg;.jpeg;.gif; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txtSoundPath.Text = ofd.FileName.ToString();
                SafeFileName = Path.GetExtension(ofd.FileName);
            }
            filePath = this.txtSoundPath.Text;
            pictureBox1.Image = Image.FromFile(filePath);
        }

        private void btnUpSound_Click(object sender, EventArgs e)
        {
            try
            {
                string res = UpLoad();
                if (res == "ok")
                {
                    MessageBox.Show("上传文件成功！");
                }
                else
                {
                    MessageBox.Show("上传文件失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.GetBaseException());
            }


        }


        public string UpLoad()
        {

            using (HttpClient client = new HttpClient())
            {
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmss") + SafeFileName;

                var content = new MultipartFormDataContent();
                string path = Path.Combine(filePath);

                content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", saveName);

                var requestUri = "http://localhost:60627//api/CommonCall/UploadIMG";

                var result = client.PostAsync(requestUri, content).Result.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());



                if (dic.ContainsKey("isSuccess"))
                {
                    string ss = dic["isSuccess"].ToString();
                    if (dic["isSuccess"].ToString().Trim().ToLower() == "true")
                    {
                        return "ok";
                    }

                }
                return "no";

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;//等于true表示可以选择多个文件
            dlg.Title = "请选择文件";
            dlg.Filter = "所有文件(*xls*)|*.xls*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = new DataTable();
                int i = 0;
                foreach (string file in dlg.FileNames)
                {
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    dgvr.Cells["Column1"].Value = Path.GetFileName(file);
                    dgvr.Cells["Column2"].Value = file;
                    i++;
                }

            }
        }



        private void btnUpSound_Click2(object sender, EventArgs e)
        {
            try
            {
                string res = UpLoad();
                if (res == "ok")
                {
                    MessageBox.Show("上传文件成功！");
                }
                else
                {
                    MessageBox.Show("上传文件失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.GetBaseException());
            }


        }


        public string UpLoad2()
        {

            using (HttpClient client = new HttpClient())
            {
                string saveName = DateTime.Now.ToString("yyyyMMddHHmmss") + SafeFileName;

                var content = new MultipartFormDataContent();
                string path = Path.Combine(filePath);
                content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", saveName);

                var requestUri = "http://localhost:60627//api/CommonCall/UploadIMG";

                var result = client.PostAsync(requestUri, content).Result.Content.ReadAsStringAsync().Result;

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());


                if (dic.ContainsKey("isSuccess"))
                {
                    string ss = dic["isSuccess"].ToString();
                    if (dic["isSuccess"].ToString().Trim().ToLower() == "true")
                    {
                        return "ok";
                    }

                }
                return "no";

            }
        }
    }
}
