using SJeMES_Control_Library.WhiteBoardTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Forms 
{
    public partial class FrmShowFile : Form
    {
        private string file_urls; 
        public string _file_name; 
        public FrmShowFile(string file_url, string file_name = "") 
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            file_urls = file_url;
            _file_name = file_name;
        }

        private void FrmShowFile_Load(object sender, EventArgs e)
        {
            //int pwith = this.flowLayoutPanel1.Width;
            if (!string.IsNullOrEmpty(_file_name))
            {
                this.Text = _file_name;
            }

            //flowLayoutPanel1.Padding = new Padding(100, 50, 100, 10);
            //if (!VitrualFileExist(file_urls))
            //{
            //    MessageBox.Show("文件不存在");
            //    this.Close();
            //    return;
            //}

            string loadPath = System.Environment.CurrentDirectory + @"\openFile";


             
            DateTime currTime = DateTime.Now.AddDays(-7);

            if (Directory.Exists(loadPath))
            {
                foreach (string d in Directory.GetFileSystemEntries(loadPath))
                {
                    DateTime createTime = File.GetCreationTime(d);
                    if (createTime <= currTime)
                        File.Delete(d);
                }
            }
            else
            {
                Directory.CreateDirectory(loadPath);
            }


            string filename = loadPath + @"\" + file_urls.Substring(file_urls.Replace(@"/", @"\").LastIndexOf(@"\") + 1); 
            try
            {
                if (!File.Exists(filename))
                {
                    System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                    System.Net.WebClient webclient = new System.Net.WebClient();
                    webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    webclient.DownloadFile(file_urls, filename);
                    webclient.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("file does not exist");
                this.Close();
                return;
            }

            try
            {
                Process proc = Process.Start(filename);
                if (proc != null)
                {
                    proc.WaitForExit(3000);
                    //if (proc.HasExited) MessageBox.Show(String.Format("结束 {0} 文件预览", _file_name), this.Text,
                    //MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //else
                    //{
                    //    // 如果外部程序没有结束运行则强行终止之。
                    //    proc.Kill();
                    //    MessageBox.Show(String.Format("外部程序 {0} 被强行终止！", _file_name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //}
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }

            //File.Delete(filename);   
            this.Close();

            //string exp = file_urls.Substring(file_urls.LastIndexOf("."));
            //List<string> f = new List<string>();
            //f.Add(".ppt");
            //f.Add(".pdf");
            //f.Add(".xlsx");
            //f.Add(".xls");
            //f.Add(".docx");
            //f.Add(".doc");
            //f.Add(".txt");
            //f.Add(".jpeg");
            //f.Add(".git");
            //f.Add(".png");
            //f.Add(".bmp");
            //f.Add(".jpg");
            //if (!f.Contains(exp.ToLower()))
            //{
            //    MessageBox.Show("该文件类型暂不支持浏览");  
            //    this.Close();
            //    return;
            //}


            ////string loadPath=  this.GetType().Assembly.Location+ @"\openFile";

            //Task.Run(async () =>
            //{
            //    System.Net.ServicePointManager.DefaultConnectionLimit = 100;
            //    ImageConverterFactory imageConverterFactory = new ImageConverterFactory();
            //    imageConverterFactory.CreateImageConverter(Path.GetExtension(file_urls)).ConvertToImage(
            //   filename,//服务器路径
            //   loadPath);//本地路径
            //    this.Invoke(new MethodInvoker(delegate
            //    {
            //        Get_Folder(loadPath);
            //    }));
            //});




        }
        private void Get_Folder(string FilePath)
        {


            if (Directory.Exists(FilePath))
            {
                foreach (string d in Directory.GetFileSystemEntries(FilePath))
                {
                    var exp = d.Substring(d.LastIndexOf(".")).ToLower();
                    if (exp == ".jpeg" || exp == ".git" || exp == ".png" || exp == ".bmp" || exp == ".jpg")
                    {
                        Image img = Image.FromFile(d);
                        if (File.Exists(d) &&
                                img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg) ||
                                img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif) ||
                                img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp) ||
                                img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                        {
                            PictureBox pic = new PictureBox();
                            pic.Image = img;
                            //pic.Width = this.flowLayoutPanel1.Width;

                            pic.SizeMode = PictureBoxSizeMode.AutoSize;
                            //this.flowLayoutPanel1.Controls.Add(pic);
                        }
                    }
                }
            }

        }

        private void FrmShowFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            //foreach (var item in flowLayoutPanel1.Controls)
            //{
            //    ((PictureBox)item).Image.Dispose();
            //}

        }

        private static bool VitrualFileExist(string url)
        {
            try
            {
                System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                //创建根据网络地址的请求对象
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.CreateDefault(new Uri(url));
                httpWebRequest.Method = "HEAD";
                httpWebRequest.Timeout = 1000;
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
