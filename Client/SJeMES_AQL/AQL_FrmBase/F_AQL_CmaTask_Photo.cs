using SJeMES_Control_Library.VideoCapture;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_AQL.AQL_FrmBase
{
    public partial class F_AQL_CmaTask_Photo : Form
    {
        Dictionary<string, object> dics = new Dictionary<string, object>();

        string product_imgGuid1 = string.Empty;//产品照片guid1
        string product_imgGuid2 = string.Empty;//产品照片guid2
        string product_imgGuid3 = string.Empty;//产品照片guid3
        string product_imgGuid4 = string.Empty;//产品照片guid4
        string product_imgGuid5 = string.Empty;//产品照片guid5
        string product_imgGuid6 = string.Empty;//产品照片guid6

        string measure_imgGuid1 = string.Empty;//测量照片guid1
        string measure_imgGuid2 = string.Empty;//测量照片guid2
        string measure_imgGuid3 = string.Empty;//测量照片guid3
        public F_AQL_CmaTask_Photo(Dictionary<string,object> _dics)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dics = _dics;

            DisabledEdit();
        }

        public void DisabledEdit()
        {
            if (dics["effective_status"].ToString() == "失效" || dics["PH_EDIT_STATE"].ToString() == "1")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                button16.Enabled = false;
                button17.Enabled = false;
                button18.Enabled = false;
                btn_commit.Enabled = false;
                button19.Enabled = false;
                button20.Enabled = false;
                button27.Enabled = false;
                button23.Enabled = false;
                button21.Enabled = false;
                button26.Enabled = false;
                button24.Enabled = false;
                button22.Enabled = false;
                button25.Enabled = false;
            }
        }

        private void F_AQL_CmaTask_Photo_Load(object sender, EventArgs e)
        {

            this.splitContainer1.Panel1.Controls.Clear();
            F_AQL_Inspection_GeneralInformation uc = new F_AQL_Inspection_GeneralInformation("Photo", dics);//照片
            //uc.TopLevel = false;

            //使用DockStyle进行填充
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            //将需要填充窗体的容器设置为窗体的父容器
            // uc.Parent = this.splitContainer1.Panel1;
            //使用内置函数ADD()进行窗体的添加
            this.splitContainer1.Panel1.Controls.Add(uc);

            this.FormBorderStyle = FormBorderStyle.None;
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            GetInspection_GeneralInformationImg();


        }

        /// <summary>
        /// 查询-AQL验货-照片-图片
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetInspection_GeneralInformationImg()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"].ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CmaTask_Photo",//类名
                                            "GetInspection_GeneralInformationImg",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["image_type"].ToString()=="0")
                        {
                            switch (dt.Rows[i]["image_index"].ToString())
                            {
                                case "1":
                                    var webC = new System.Net.WebClient();
                                    string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    Image image = new Bitmap(webC.OpenRead(url));
                                    pictureBox1.Image = image;
                                    label1.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                                case "2":
                                    webC = new System.Net.WebClient();
                                    url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    image = new Bitmap(webC.OpenRead(url));
                                    pictureBox2.Image = image;
                                    label2.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                                case "3":
                                    webC = new System.Net.WebClient();
                                    url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    image = new Bitmap(webC.OpenRead(url));
                                    pictureBox3.Image = image;
                                    label3.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                                case "4":
                                    webC = new System.Net.WebClient();
                                    url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    image = new Bitmap(webC.OpenRead(url));
                                    pictureBox4.Image = image;
                                    label4.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                                case "5":
                                    webC = new System.Net.WebClient();
                                    url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    image = new Bitmap(webC.OpenRead(url));
                                    pictureBox5.Image = image;
                                    label5.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                                case "6":
                                    webC = new System.Net.WebClient();
                                    url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    image = new Bitmap(webC.OpenRead(url));
                                    pictureBox6.Image = image;
                                    label6.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (dt.Rows[i]["image_type"].ToString() == "1")
                        {
                            switch (dt.Rows[i]["image_index"].ToString())
                            {
                                case "1":
                                    var webC = new System.Net.WebClient();
                                    string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    Image image = new Bitmap(webC.OpenRead(url));
                                    pictureBox7.Image = image;
                                    label7.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                                case "2":
                                    webC = new System.Net.WebClient();
                                    url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    image = new Bitmap(webC.OpenRead(url));
                                    pictureBox8.Image = image;
                                    label8.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                                case "3":
                                    webC = new System.Net.WebClient();
                                    url = Program.Client.PicUrl + Convert.ToString(dt.Rows[i]["file_url"].ToString());
                                    image = new Bitmap(webC.OpenRead(url));
                                    pictureBox9.Image = image;
                                    label9.Text = dt.Rows[i]["file_name"].ToString();
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;

        /// <summary>
        /// 上传-AQL验货-照片-上传
        /// </summary>
        public void UploadInspection_GeneralInformationImg(string image_type,string image_index,string file_guid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString()) ;
                data.Add("image_type", image_type);
                data.Add("image_index", image_index);
                data.Add("file_guid", file_guid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_CmaTask_Photo", "UploadInspection_GeneralInformationImg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Upload successful!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    MessageBox.Show(msg);
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //产品上传1
        private void button2_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label1.Text = ofd.SafeFileName;
                    product_imgGuid1 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox1.Image = image;
                    UploadInspection_GeneralInformationImg("0","1", product_imgGuid1);
                }
            }
        }

        //产品上传2
        private void button3_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label2.Text = ofd.SafeFileName;
                    product_imgGuid2 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox2.Image = image;
                    UploadInspection_GeneralInformationImg("0", "2", product_imgGuid2);
                }
            }
        }

        //产品上传3
        private void button5_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label3.Text = ofd.SafeFileName;
                    product_imgGuid3 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox3.Image = image;
                    UploadInspection_GeneralInformationImg("0", "3", product_imgGuid3);
                }
            }
        }

        //产品上传4
        private void button7_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label4.Text = ofd.SafeFileName;
                    product_imgGuid4 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox4.Image = image;
                    UploadInspection_GeneralInformationImg("0", "4", product_imgGuid4);
                }
            }
        }

        //产品上传5
        private void button9_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label5.Text = ofd.SafeFileName;
                    product_imgGuid5 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox5.Image = image;
                    UploadInspection_GeneralInformationImg("0", "5", product_imgGuid5);
                }
            }
        }

        //产品上传6
        private void button11_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label6.Text = ofd.SafeFileName;
                    product_imgGuid6 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox6.Image = image;
                    UploadInspection_GeneralInformationImg("0", "6", product_imgGuid6);
                }
            }
        }

        //测量上传1
        private void button13_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label7.Text = ofd.SafeFileName;
                    measure_imgGuid1 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox7.Image = image;
                    UploadInspection_GeneralInformationImg("1", "1", measure_imgGuid1);
                }
            }
        }

        //测量上传2
        private void button15_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label8.Text = ofd.SafeFileName;
                    measure_imgGuid2 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox8.Image = image;
                    UploadInspection_GeneralInformationImg("1", "2", measure_imgGuid2);
                }
            }
        }

        //测量上传3
        private void button17_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件夹";
            ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                filePath = ofd.FileName;
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label9.Text = ofd.SafeFileName;
                    measure_imgGuid3 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox9.Image = image;
                    UploadInspection_GeneralInformationImg("1", "3", measure_imgGuid3);
                }
            }
        }


        /// <summary>
        /// 删除-AQL验货-照片
        /// </summary>
        public void DeleteInspection_GeneralInformationImg(string image_type, string image_index)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("task_no", dics["task_no"].ToString());
                data.Add("image_type", image_type);
                data.Add("image_index", image_index);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_CmaTask_Photo", "DeleteInspection_GeneralInformationImg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Successfully Deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    MessageBox.Show(msg);
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //产品删除1
        private void button1_Click(object sender, EventArgs e)
        {
            product_imgGuid1 = string.Empty;
            label1.Text = "Photo_1";//照片1
            pictureBox1.Image = null;
            DeleteInspection_GeneralInformationImg("0","1");
        }

        //产品删除2
        private void button4_Click(object sender, EventArgs e)
        {
            product_imgGuid2 = string.Empty;
            label2.Text = "Photo_2";//照片2
            pictureBox2.Image = null;
            DeleteInspection_GeneralInformationImg("0", "2");
        }

        //产品删除3
        private void button6_Click(object sender, EventArgs e)
        {
            product_imgGuid3 = string.Empty;
            label3.Text = "Photo_3";//照片3
            pictureBox3.Image = null;
            DeleteInspection_GeneralInformationImg("0", "3");
        }

        //产品删除4
        private void button8_Click(object sender, EventArgs e)
        {
            product_imgGuid4 = string.Empty;
            label4.Text = "Photo_4";//照片4
            pictureBox4.Image = null;
            DeleteInspection_GeneralInformationImg("0", "4");
        }

        //产品删除5
        private void button10_Click(object sender, EventArgs e)
        {
            product_imgGuid5 = string.Empty;
            label5.Text = "Photo_5";//照片5
            pictureBox5.Image = null;
            DeleteInspection_GeneralInformationImg("0", "5");
        }

        //产品删除6
        private void button12_Click(object sender, EventArgs e)
        {
            product_imgGuid6 = string.Empty;
            label6.Text = "Photo_6";//照片6
            pictureBox6.Image = null;
            DeleteInspection_GeneralInformationImg("0", "6");
        }

        //测量删除1
        private void button14_Click(object sender, EventArgs e)
        {
            measure_imgGuid1 = string.Empty;
            label7.Text = "Photo_1";//照片1
            pictureBox7.Image = null;
            DeleteInspection_GeneralInformationImg("1", "1");
        }

        //测量删除2
        private void button16_Click(object sender, EventArgs e)
        {
            measure_imgGuid2 = string.Empty;
            label8.Text = "Photo_2";//照片2
            pictureBox8.Image = null;
            DeleteInspection_GeneralInformationImg("1", "2");
        }

        //测量删除3
        private void button18_Click(object sender, EventArgs e)
        {
            measure_imgGuid3 = string.Empty;
            label9.Text = "Photo_3";//照片3
            pictureBox9.Image = null;
            DeleteInspection_GeneralInformationImg("1", "3");
        }

        #region 点击图片放大
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image!=null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox1.Image;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox2.Image;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image != null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox3.Image;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (pictureBox4.Image != null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox4.Image;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (pictureBox5.Image != null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox5.Image;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (pictureBox6.Image != null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox6.Image;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (pictureBox7.Image != null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox7.Image;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (pictureBox8.Image != null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox8.Image;
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (pictureBox9.Image != null)
            {
                pictureBox10.Visible = true;
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = pictureBox9.Image;
            }
        }
        #endregion

        //点击后放大的图片隐藏
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pictureBox10.Dock = DockStyle.None;
            pictureBox10.Visible = false;
        }

        public void TakePh(Label label,PictureBox pictureBox, string image_type, string image_index)
        {
            var phRes = new FrmPhotographResult();
            FrmPhotograph frmTakePh = new FrmPhotograph(phRes);
            frmTakePh.ShowDialog();
            if (phRes.IsSuccess)
            {
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, phRes.SaveImgPath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    label.Text = phRes.SaveImgName;
                    product_imgGuid1 = resultDIC["guid"].ToString();
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox.Image = image;
                    UploadInspection_GeneralInformationImg(image_type, image_index, product_imgGuid1);

                    System.IO.File.Delete(phRes.SaveImgPath);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(phRes.ErrorMsg))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(phRes.ErrorMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    MessageBox.Show(phRes.ErrorMsg);
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            TakePh(label1, pictureBox1, "0", "1");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            TakePh(label2, pictureBox2, "0", "2");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            TakePh(label3, pictureBox3, "0", "3");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            TakePh(label4, pictureBox4, "0", "4");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            TakePh(label5, pictureBox5, "0", "5");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            TakePh(label6, pictureBox6, "0", "6");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            TakePh(label7, pictureBox7, "1", "1");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            TakePh(label8, pictureBox8, "1", "2");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            TakePh(label9, pictureBox9, "1", "3");
        }

        private void btn_commit_Click(object sender, EventArgs e)
        {
            if (AreAllPictureBoxesFilled())
            {
                DialogResult dr = MessageBox.Show("Are you sure to submit?!", "Submit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dr == DialogResult.OK)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("task_no", dics["task_no"].ToString());
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_AQLAPI", "SJ_AQLAPI.AQL_CmaTask_Photo", "EditPHState", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                    if (ret.IsSuccess)
                    {
                        dics["PH_EDIT_STATE"] = "1";
                        DisabledEdit();
                    }
                    else
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Upload All Images");
            }
            
        }

        private bool AreAllPictureBoxesFilled()
        {
            PictureBox[] pictureBoxes =
            {
        pictureBox1, pictureBox2, 
        pictureBox7
    };

            foreach (PictureBox pb in pictureBoxes)
            {
                if (pb.Image == null)
                    return false;
            }

            return true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
