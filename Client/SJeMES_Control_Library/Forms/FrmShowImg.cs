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


namespace SJeMES_Control_Library.Forms
{
    public partial class FrmShowImg : Form
    {
        string Img_url = string.Empty;
        string img_name = string.Empty;
        public FrmShowImg(string img_url,string img_name="")
        {
            InitializeComponent();
            Img_url = img_url;
            this.img_name = img_name;
        }

        private void FrmShowImg_Load(object sender, EventArgs e)
        { 


            System.Net.ServicePointManager.DefaultConnectionLimit = 100;
            var webC = new System.Net.WebClient();
            try
            {
                //if (!VitrualFileExist(Img_url))
                //{
                //    MessageBox.Show("文件不存在");
                //    this.Close();
                //    return;
                //}
                string loadPath = System.Environment.CurrentDirectory + @"\openFile";
                if (Directory.Exists(loadPath))
                {
                    //foreach (string d in Directory.GetFileSystemEntries(loadPath))
                    //{
                    //    File.Delete(d);
                    //}
                }
                else
                {
                    Directory.CreateDirectory(loadPath);
                }
                string filename = loadPath + @"\" + Img_url.Substring(Img_url.Replace(@"/", @"\").LastIndexOf(@"\") + 1);
                try
                {

                    //System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                    //System.Net.WebClient webclient = new System.Net.WebClient();
                    //webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    //webclient.DownloadFile(Img_url, filename);
                    //webclient.Dispose();
                }
                catch
                {
                    MessageBox.Show("文件不存在");
                    this.Close();
                    return;
                }

                if (!string.IsNullOrEmpty(img_name))
                {
                    this.Text = img_name;
                }
                panel1.Controls.Clear();
                if (!string.IsNullOrEmpty(this.Img_url))
                {
                    try
                    {
                        Task.Run(async () =>
                        {
                            //string filename = loadPath + @"\" + Img_url.Substring(Img_url.Replace(@"/",@"\").LastIndexOf(@"\")+1);
                            //System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                            //System.Net.WebClient webclient = new System.Net.WebClient();
                            //webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                            //webclient.DownloadFile(Img_url, filename);
                            //webclient.Dispose();
                            Image image = new Bitmap(webC.OpenRead(Img_url));
                            //Image image = new Bitmap(filename);
                            PictureBox pic = new PictureBox();
                            pic.Image = image; 
                            pic.SizeMode = PictureBoxSizeMode.AutoSize;

                            if (pic.Width>this.panel1.Width && pic.Height>this.panel1.Height)
                            {
                                pic.Width = this.panel1.Width;
                                pic.Height = this.panel1.Height;
                                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                            } 
                            this.Invoke(new MethodInvoker(delegate {
                                this.panel1.Controls.Add(pic);
                            }));
                            
                        });
                       
                    }
                    catch(Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmShowImg_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var item in panel1.Controls)
            {
                ((PictureBox)item).Image.Dispose();
            }
        }

        private static bool VitrualFileExist(string url)
        {
           
            try
            {
                //创建根据网络地址的请求对象
                System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.CreateDefault(new Uri(url));
                httpWebRequest.Method = "HEAD";
                httpWebRequest.Timeout = 5000;
                //返回响应状态是否是成功比较的布尔值
                return (((System.Net.HttpWebResponse)httpWebRequest.GetResponse()).StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }

        }
    }
}
