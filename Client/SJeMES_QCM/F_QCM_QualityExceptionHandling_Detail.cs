using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_QualityExceptionHandling_Detail : MaterialForm
    {
        public DataTable _dt { get; set; }
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_QualityExceptionHandling_Detail(DataTable dt)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _dt = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_QualityExceptionHandling_Detail_Load(object sender, EventArgs e)
        {
            GetDataList();
        }

        /// <summary>
        /// 视图展示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                GetAbnormalReportByIdResDto lis = new GetAbnormalReportByIdResDto();
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dt.Rows)
                    {
                        p.Add("ID", dr["ID"].ToString());
                    }
                }

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.AbnormalReport",//类名
                                            "GetAbnormalReportById",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                Dictionary<string, object> ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (!Convert.ToBoolean(ret["IsSuccess"]))
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }
                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAbnormalReportByIdResDto>(ret["RetData1"].ToString());

                switch (request.QUALITY_PROBLEM_LEVEL)
                {
                    case "0":
                        request.QUALITY_PROBLEM_LEVEL = "普通品质问题";
                        break;
                    case "1":
                        request.QUALITY_PROBLEM_LEVEL = "严重品质问题";
                        break;
                    case "2":
                        request.QUALITY_PROBLEM_LEVEL = "批量/重大品质问题";
                        break;
                    default:
                        break;
                }
                switch (request.STATUS)
                {
                    case "0":
                        request.STATUS = "未结案";
                        break;
                    case "1":
                        request.STATUS = "结案";
                        break;
                    case "2":
                        request.STATUS = "公开";
                        break;
                    default:
                        break;
                }

                #region 带出的参数
                txt_PROD_NO.Text = request.PROD_NO;
                txt_PRODUCTION_MONTH.Text = request.PRODUCTION_MONTH;
                txt_PRODUCTIONLINE_NAME.Text = request.PRODUCTIONLINE_NAME;
                txt_FW.Text = request.FW;
                txt_PRO_DEPARTMENT_NAME.Text = request.PRO_DEPARTMENT_NAME;
                txt_QUALITY_PROBLEM_LEVEL.Text = request.QUALITY_PROBLEM_LEVEL;
                txt_SHOE_NO.Text = request.SHOE_NO;
                txt_ORG_NAME.Text = request.ORG_NAME;
                txt_STATUS.Text = request.STATUS;
                txt_Details_problem.Text = request.PROBLEM_DETAIL;
                txt_Emergencymeasures.Text = request.EMERGENCY_MEASURES;
                txt_reson.Text = request.PROBLEM_REASON_STR;
                txt_pde.Text = request.PROBLEM_DES;
                cbo_rde.Text = request.RESPONSIBLE_DEPARTMENT_NAME;

                var webC = new System.Net.WebClient();

                try
                {
                    string art_imgurl =Program.Client.PicUrl + request.ART_IMG_URL;
                    Image image1 = new Bitmap(webC.OpenRead(art_imgurl));
                    pic_ART.Image = image1;
                    pic_ART.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch
                {
                }

                foreach (string url in request.PROBLEM_IMG_LIST)
                {
                    try
                    {
                        string img_url = Program.Client.PicUrl + url;
                        PictureBox pb = new PictureBox();
                        pb.Size = new Size(flowLayoutPanelimg.Height + 20, flowLayoutPanelimg.Height);
                        Image image = new Bitmap(webC.OpenRead(img_url));
                        pb.Image = image;
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        flowLayoutPanelimg.Controls.Add(pb);
                    }
                    catch
                    {
                    }
                }

                foreach (string url in request.SOLVE_IMG_LIST)
                {
                    try
                    {
                        string img_url = Program.Client.PicUrl + url;
                        PictureBox pb = new PictureBox();
                        pb.Size = new Size(flowLayoutPanelimg.Height + 20, flowLayoutPanelimg.Height);
                        Image image = new Bitmap(webC.OpenRead(img_url));
                        pb.Image = image;
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        flowLayoutImg1.Controls.Add(pb);
                    }
                    catch
                    {
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F_QCM_QualityExceptionHandling_Inform inform = new F_QCM_QualityExceptionHandling_Inform();
            inform.ShowDialog();
        }

        private void btn_settle_Click(object sender, EventArgs e)
        {
            try
            {

                if (_dt.Rows.Count > 0)
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        p.Add("ID", dr["ID"].ToString());

                    }
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.AbnormalReport",//类名
                                                "ChangeAbnormalReportSTATUS",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    else
                    {
                        MessageBox.Show("结案成功");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateAbnormalReportByIdReqDto update = new UpdateAbnormalReportByIdReqDto();

                update.MEASURES = txt_cause2.Text;

                foreach (DataRow dr in _dt.Rows)
                {
                    update.ID = dr["ID"].ToString();
                }

                update.MEASURES = txt_cause2.Text;

                List<string> vs = new List<string>();
                foreach (var item in lst_pic)
                {
                    string aa = item["filePath"].ToString();
                    vs.Add(aa);
                }
                update.SOLVE_IMG_LIST = vs;


                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.AbnormalReport",//类名
                                            "UpdateAbnormalReportById",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(update));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                else
                {
                    MessageBox.Show("保存成功");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {

        }

        public string SafeFileName { get; set; }
        public string filePath { get; set; }
        List<Dictionary<string, string>> lst_pic = new List<Dictionary<string, string>>();
        private void btn_uploadImg_Click(object sender, EventArgs e)
        {

            string guid = Guid.NewGuid().ToString("N");
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
                var webC = new System.Net.WebClient();
                try
                {
                    Task.Run(async () =>
                    {
                        string loadPath = System.Environment.CurrentDirectory + @"\openFile";

                        string filename = loadPath + @"\" + filePath.Substring(filePath.Replace(@"/", @"\").LastIndexOf(@"\") + 1);
                        System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                        System.Net.WebClient webclient = new System.Net.WebClient();
                        webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        webclient.DownloadFile(filePath, filename);
                        webclient.Dispose();
                        Image image = new Bitmap(webC.OpenRead(filename));
                        //Image image = new Bitmap(filename);
                        PictureBox pic = new PictureBox();
                        pic.Name = filename;
                        pic.Size = new System.Drawing.Size(120, 80);
                        pic.SizeMode = PictureBoxSizeMode.Zoom;
                        pic.Image = image;
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.flowLayoutImg1.Controls.Add(pic);
                        }));
                    });
                }
                catch (Exception ex)
                {
                }


                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoad(Program.Client.APIURL, filePath, 14, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    filePath = resultDIC["url"].ToString();
                    SafeFileName = resultDIC["filename"].ToString();
                    MessageBox.Show("上传图片成功！");
                    

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("filePath", filePath);
                    //dic.Add("SafeFileName", SafeFileName);
                    lst_pic.Add(dic);
                }
                else
                {
                    MessageBox.Show("上传图片失败！");
                }
            }

        }


        public class UpdateAbnormalReportByIdReqDto
        {
            public string ID { get; set; }
            /// <summary>
            /// 解决后图片
            /// </summary>
            public List<string> SOLVE_IMG_LIST { get; set; }
            /// <summary>
            /// 措施
            /// </summary>
            public string MEASURES { get; set; }
        }

        public class GetAbnormalReportByIdResDto : CreateAbnormalReportReqDto
        {
            /// <summary>
            /// 解决后图片
            /// </summary>
            public List<string> SOLVE_IMG_LIST { get; set; }
            /// <summary>
            /// 措施
            /// </summary>
            public string MEASURES { get; set; }
            /// <summary>
            /// 问题图片关联键
            /// </summary>
            public string PROBLEM_GUID_IMG { get; set; }
            /// <summary>
            /// 解决后图片关联键
            /// </summary>
            public string SOLVE_GUID_IMG { get; set; }
            /// <summary>
            /// 状态
            /// </summary>
            public string STATUS { get; set; }
            /// <summary>
            /// 状态转义
            /// </summary>
            public string STATUS_STR { get; set; }
            /// <summary>
            /// 品质问题级别转义 0普通品质问题1严重品质问题2批量/重大品质问题
            /// </summary>
            public string QUALITY_PROBLEM_LEVEL_STR { get; set; }
        }

        public class CreateAbnormalReportReqDto
        {
            /// <summary>
            /// ART
            /// </summary>
            public string PROD_NO { get; set; }
            /// <summary>
            /// ART图片路径
            /// </summary>
            public string ART_IMG_URL { get; set; }
            /// <summary>
            /// FW21
            /// </summary>
            public string FW { get; set; }
            /// <summary>
            /// 鞋型
            /// </summary>
            public string SHOE_NO { get; set; }
            /// <summary>
            /// 生产月份
            /// </summary>
            public string PRODUCTION_MONTH { get; set; }
            /// <summary>
            /// 厂区
            /// </summary>
            public string ORG_CODE { get; set; }
            /// <summary>
            /// 厂区名称
            /// </summary>
            public string ORG_NAME { get; set; }
            /// <summary>
            /// 生产工段
            /// </summary>
            public string PRO_DEPARTMENT_NO { get; set; }
            /// <summary>
            /// 生产工段名称
            /// </summary>
            public string PRO_DEPARTMENT_NAME { get; set; }
            /// <summary>
            /// 生产线代号
            /// </summary>
            public string PRODUCTIONLINE_NO { get; set; }
            /// <summary>
            /// 生产线名称
            /// </summary>
            public string PRODUCTIONLINE_NAME { get; set; }
            /// <summary>
            /// 品质问题级别 0普通品质问题1严重品质问题2批量/重大品质问题
            /// </summary>
            public string QUALITY_PROBLEM_LEVEL { get; set; }
            /// <summary>
            /// 问题描述
            /// </summary>
            public string PROBLEM_DES { get; set; }
            /// <summary>
            /// 责任部门代号
            /// </summary>
            public string RESPONSIBLE_DEPARTMENT_NO { get; set; }
            /// <summary>
            /// 责任部门名称
            /// </summary>
            public string RESPONSIBLE_DEPARTMENT_NAME { get; set; }
            /// <summary>
            /// 问题详情
            /// </summary>
            public string PROBLEM_DETAIL { get; set; }
            /// <summary>
            /// 紧急处理措施
            /// </summary>
            public string EMERGENCY_MEASURES { get; set; }
            /// <summary>
            /// 问题图片集合
            /// </summary>
            public List<string> PROBLEM_IMG_LIST { get; set; }
            /// <summary>
            /// 问题原因
            /// </summary>
            public string PROBLEM_REASON_STR { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
