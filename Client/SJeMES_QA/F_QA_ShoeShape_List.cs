using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using SJeMES_QA.UControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QA
{
    public partial class F_QA_ShoeShape_List : MaterialForm
    {
        /// <summary>
        /// 季度
        /// </summary>
        private string develop_season;
        /// <summary>
        /// 鞋型
        /// </summary>
        private string shoe_no;
        private readonly MaterialSkinManager materialSkinManager;
        public F_QA_ShoeShape_List(string DEVELOP_SEASON, string SHOE_NO)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            develop_season = DEVELOP_SEASON;
            shoe_no = SHOE_NO;
            GET_ShoeShapeHeader();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        /// <summary>
        /// 表身数据源
        /// </summary>
        public void Table()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.QAShoeShapeTable", "GET_ShoeShapeProblem_List", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    //string a = "量产时间:";
                    string a = "Mass production time:";
                    string b = "ART:";
                    foreach (DataRow item in dt.Rows)
                    {
                        //季度
                        //lab_quarter.Text = "季度:" + item["DEVELOP_SEASON"].ToString();
                        lab_quarter.Text = "Quarter:" + item["DEVELOP_SEASON"].ToString();
                        //鞋型
                        //lab_shoes.Text = "季度:" + item["SHOE_NO"].ToString();
                        lab_shoes.Text = "Quarter:" + item["SHOE_NO"].ToString();
                        //量产时间
                        a += item["PRODUCT_MONTH"].ToString() + "、";
                        //ART
                        b += item["PROD_NO"].ToString() + "、";
                    }
                    lab_Time.Text = a.TrimEnd('、');
                    lab_Art.Text = b.TrimEnd('、');
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 查询试穿枚举
        /// </summary>
        /// <returns></returns>
        public DataTable GetDGVComboBox()
        {
            #region 查询枚举
            List<string> lst_enum_type = new List<string>();
            lst_enum_type.Add("enum_test_result");
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJ_QCMAPI",//类库名
                                       "SJ_QCMAPI.BASE",//类名
                                       "GetSYS001MDataListS",//方法名
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
            #endregion

            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_test_result"].ToString());


            return dt;
        }
        /// <summary>
        /// 查询FGT枚举
        /// </summary>
        /// <returns></returns>
        public DataTable GetDGVComboBox2()
        {
            #region 查询枚举
            List<string> lst_enum_type = new List<string>();
            lst_enum_type.Add("enum_test_result");
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJ_QCMAPI",//类库名
                                       "SJ_QCMAPI.BASE",//类名
                                       "GetSYS001MDataListS",//方法名
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
            #endregion

            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_test_result"].ToString());


            return dt;
        }
        /// <summary>
        /// 查询CMA枚举
        /// </summary>
        /// <returns></returns>
        public DataTable GetDGVComboBox3()
        {
            #region 查询枚举
            List<string> lst_enum_type = new List<string>();
            lst_enum_type.Add("enum_test_result");
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJ_QCMAPI",//类库名
                                       "SJ_QCMAPI.BASE",//类名
                                       "GetSYS001MDataListS",//方法名
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
            #endregion

            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_test_result"].ToString());


            return dt;
        }

        private void F_QA_ShoeShape_List_Load(object sender, EventArgs e)
        {
            TableView();
            #region 赋试穿枚举值
            DataTable dt_tval = GetDGVComboBox();
            com1.DataSource = dt_tval;
            if (dt_tval != null && dt_tval.Rows.Count > 0)
            {
                com1.DisplayMember = "enum_code";
                com1.ValueMember = "enum_value";
            }
            #endregion
            #region 赋FGT枚举值
            DataTable dt_tval2 = GetDGVComboBox2();
            com2.DataSource = dt_tval2;
            if (dt_tval2 != null && dt_tval2.Rows.Count > 0)
            {
                com2.DisplayMember = "enum_code";
                com2.ValueMember = "enum_value";
            }
            #endregion
            #region 赋CMA枚举值
            DataTable dt_tval3 = GetDGVComboBox3();
            com3.DataSource = dt_tval3;
            if (dt_tval3 != null && dt_tval3.Rows.Count > 0)
            {
                com3.DisplayMember = "enum_code";
                com3.ValueMember = "enum_value";
            }
            #endregion
        }
        private int p;
        /// <summary>
        /// 新增阶段品质状况
        /// </summary>
        /// <returns></returns>
        public void TableView()
        {
            try
            {
                this.flowLayoutPanelTable.Controls.Clear();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("develop_season", develop_season);
                data.Add("shoe_no", shoe_no);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.QAShoeShapeTable",//类名
                                           "GET_ShoeShapecenterView",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable data1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());//QA鞋型品质信息
                DataTable data2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());//QA鞋型品质问题点
                DataTable data3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());//QA鞋型品质问题点图片
                if (data1.Rows.Count > 0 && data2.Rows.Count > 0)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("develop_season");
                    dt.Columns.Add("shoe_no");
                    dt.Columns.Add("check_date");
                    dt.Columns.Add("dpstage_code");
                    dt.Columns.Add("dpstage_name");
                    dt.Columns.Add("problemcategory_no");
                    dt.Columns.Add("problemcategory_name");
                    dt.Columns.Add("problem_no");
                    dt.Columns.Add("problem_name");
                    dt.Columns.Add("ng_qty");
                    dt.Columns.Add("qty");//生产只数
                    dt.Columns.Add("itemnumber");//项次
                    dt.Columns.Add("ng_rate");
                    dt.Columns.Add("respon_people");
                    dt.Columns.Add("improvement_measures");
                    dt.Columns.Add("createby");
                    dt.Columns.Add("createdate");
                    dt.Columns.Add("createtime");
                    dt.Columns.Add("modifyby");
                    dt.Columns.Add("modifydate");
                    dt.Columns.Add("modifytime");
                    foreach (DataRow item1 in data1.Rows)
                    {
                        dt.Rows.Clear();
                        int i = 1;
                        foreach (DataRow item2 in data2.Rows)
                        {
                            if (item1["develop_season"].ToString() == item2["develop_season"].ToString() &&
                            item1["shoe_no"].ToString() == item2["shoe_no"].ToString() &&
                            item1["check_date"].ToString() == item2["check_date"].ToString() &&
                            item1["dpstage_code"].ToString() == item2["dpstage_code"].ToString())
                            {
                                DataRow drr = dt.NewRow();
                                drr["develop_season"] = item2["develop_season"];
                                drr["shoe_no"] = item2["shoe_no"];
                                drr["check_date"] = item2["check_date"];
                                drr["dpstage_code"] = item2["dpstage_code"];
                                drr["problemcategory_no"] = item2["problemcategory_no"];
                                drr["problemcategory_name"] = item2["problemcategory_name"];
                                drr["problem_no"] = item2["problem_no"];
                                drr["problem_name"] = item2["problem_name"];
                                drr["ng_qty"] = item2["ng_qty"];
                                drr["ng_rate"] = item2["ng_rate"];
                                drr["respon_people"] = item2["respon_people"];
                                drr["improvement_measures"] = item2["improvement_measures"];
                                drr["createby"] = item2["createby"];
                                drr["createdate"] = item2["createdate"];
                                drr["createtime"] = item2["createtime"];
                                drr["modifyby"] = item2["modifyby"];
                                drr["modifydate"] = item2["modifydate"];
                                drr["modifytime"] = item2["modifytime"];

                                drr["dpstage_name"] = item1["dpstage_name"];
                                drr["qty"] = item1["qty"];
                                drr["itemnumber"] = i++;
                                dt.Rows.Add(drr);
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            UCTable uc = new UCTable(dt, null, this);
                            this.flowLayoutPanelTable.Controls.Add(uc);
                        }
                    }

                }
                //加载图片
                this.flowLayoutPanelimg.Controls.Clear();

                if (data3.Rows.Count > 0)
                {
                    foreach (DataRow item in data3.Rows)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(item["img_url"].ToString()))
                            {
                                var webC = new System.Net.WebClient();
                                string url = Program.Client.PicUrl + item["img_url"].ToString();
                                Image image = new Bitmap(webC.OpenRead(url));
                                PictureBox pic = new PictureBox();
                                pic.Image = image;
                                pic.Width = 240;
                                pic.Height = 120;
                                pic.SizeMode = PictureBoxSizeMode.StretchImage;

                                //添加点击事件（预览图片）
                                pic.Name = url;
                                pic.Parent = Parent;
                                pic.Click += new EventHandler(pic_Click);

                                this.Invoke(new MethodInvoker(delegate
                                {
                                    this.flowLayoutPanelimg.Controls.Add(pic);
                                }));
                            }
                        }
                        catch (Exception)
                        {

                           
                        }
                    }
                    #region MyRegion
                    /*  flowLayoutPanelimg.Controls.Clear();
                      System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                      var webC = new System.Net.WebClient();
                      string loadPath = System.Environment.CurrentDirectory + @"\openFile";
                      if (Directory.Exists(loadPath))
                      {
                          foreach (string d in Directory.GetFileSystemEntries(loadPath))
                          {
                              File.Delete(d);
                          }
                      }
                      else
                      {
                          Directory.CreateDirectory(loadPath);
                      }
                      foreach (DataRow item in data3.Rows)
                      {
                          item["img_url"] = Program.Client.PicUrl + item["img_url"].ToString();
                          try
                          {

                              if (!string.IsNullOrEmpty(item["img_url"].ToString()))
                              {
                                  try
                                  {
                                      Task.Run(async () =>
                                      {
                                          string filename = loadPath + @"\" + item["img_url"].ToString().Substring(item["img_url"].ToString().Replace(@"/", @"\").LastIndexOf(@"\") + 1);
                                          System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                                          System.Net.WebClient webclient = new System.Net.WebClient();
                                          webclient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                                          webclient.DownloadFile(item["img_url"].ToString(), filename);
                                          webclient.Dispose();
                                          // Image image = new Bitmap(webC.OpenRead(filename));
                                          Image image = new Bitmap(filename);
                                          PictureBox pic = new PictureBox();
                                          //ShowFileHelper.ShowFile(item["img_url"].ToString(), item["img_name"].ToString());
                                          pic.Image = image;
                                          pic.Width = 240;
                                          pic.Height = 120;
                                          pic.SizeMode = PictureBoxSizeMode.StretchImage;

                                          //添加点击事件（预览图片）
                                          pic.Name = filename;
                                          pic.Parent = Parent;
                                          pic.Click += new EventHandler(pic_Click);

                                          this.Invoke(new MethodInvoker(delegate
                                          {
                                              this.flowLayoutPanelimg.Controls.Add(pic);
                                          }));

                                      });

                                  }
                                  catch (Exception ex)
                                  {
                                  }
                              }
                          }
                          catch (Exception ex)
                          {
                              MessageBox.Show(ex.Message);
                          }


                      } */
                    #endregion

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        //判读新增中的阶段有无数据
        public int GET_ShoeShape()
        {
            int a = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("develop_season", develop_season);
                data.Add("shoe_no", shoe_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.QAShoeShapeTable", "GET_ShoeShape", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                }
                else
                {
                    a = 1;
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return a;
        }

        private void F_QA_ShoeShape_List_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var item in flowLayoutPanelimg.Controls)
            {
                ((PictureBox)item).Image.Dispose();
            }

        }
        //鞋图预览
        void pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (null == pic) return;
            string url = pic.Name; // 取出url
            FrmShowImg add = new FrmShowImg(url, "");
            add.Show();
        }
        //获取根据鞋型和季度查询到的数据赋值
        public void GET_ShoeShapeHeader()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("develop_season", develop_season);
                data.Add("shoe_no", shoe_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.QAShoeShapeTable", "GET_ShoeShapeHeader", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    string a = string.Empty;
                    string b = string.Empty;
                    foreach (DataRow item in dt.Rows)
                    {
                        //季度
                        labDEVELOP_SEASON.Text = item["DEVELOP_SEASON"].ToString();
                        //鞋型
                        labSHOE_NO.Text = item["SHOE_NO"].ToString();
                        //量产时间
                        a += item["PRODUCT_MONTH"].ToString() + "、";
                        //ART
                        b += item["PROD_NO"].ToString() + "、";
                    }
                    labPRODUCT_MONTH.Text = a.TrimEnd('、');
                    labPROD_NO.Text = b.TrimEnd('、');
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// Limited release上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void btnLR_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Disclimer上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDL_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Visual Standard上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVS_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 其他文件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQT_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 新增QA鞋型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuality_Click(object sender, EventArgs e)
        {
            try
            {
                int i = GET_ShoeShape();
                if ( i== 0)
                {
                    using (F_QA_ShoeShapeAdd add = new F_QA_ShoeShapeAdd(develop_season, shoe_no))
                    {
                        add.ShowDialog();
                        TableView();
                    }
                }
                else
                {
                    MessageBox.Show("stage no data!");
                }
            }
            catch (Exception ex)
            {

                
            }
        }
        /// <summary>
        /// 文件上传的dt视图;
        /// </summary>
        public DataTable FileView()
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("tablename", "QCM_QA_SHOESHAPE_FILE");

                Dictionary<string, object> fileddic = new Dictionary<string, object>();
                fileddic.Add("file_url", "file_url");
                fileddic.Add("file_name", "file_name");
                p.Add("fileds", fileddic);

                Dictionary<string, object> parmsdic = new Dictionary<string, object>();
                parmsdic.Add("develop_season", develop_season);
                parmsdic.Add("shoe_no", shoe_no);
                p.Add("parms", parmsdic);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BASE",//类名
                                            "GetFileView",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("net_file_url", typeof(string));
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["file_url"].ToString()))
                        {
                            try
                            {
                                dr["net_file_url"] = Program.Client.PicUrl + dr["file_url"].ToString();
                            }
                            catch
                            {
                            }
                        }
                        i++;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        //五个按钮的文件上传事件
        private void btnLR_Click_1(object sender, EventArgs e)
        {
            string Name = ((Button)sender).Name;
            switch (Name)
            {
                case "btnLR":
                    UploadAll(enum_qa_file_type.enum_qa_file_type_0);
                    break;
                case "btnDL":
                    UploadAll(enum_qa_file_type.enum_qa_file_type_1);
                    break;
                case "btnVS":
                    UploadAll(enum_qa_file_type.enum_qa_file_type_2);
                    break;
                case "btnQT":
                    UploadAll(enum_qa_file_type.enum_qa_file_type_3);
                    break;
                case "btnFile":
                    FrmFileList add = new FrmFileList(FileView(), Program.Client.UploadUrl, Program.Client.UserToken);
                    add.ShowDialog();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 点击上传文件
        /// </summary>
        private void UploadAll(string file_type)
        {

            try
            {
                // string res = UpLoad("3", file_type);
                string guid = Guid.NewGuid().ToString("N");
                // 创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                //判断选择的路径
                string path = string.Empty;
                //ofd.Title = "请选择文件";
                ofd.Title = "Please select a file";
                //ofd.Filter = "所有文件|*.*";
                ofd.Filter = "All files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                    filePath = ofd.FileName;


                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoad(Program.Client.APIURL, filePath, 5, Program.Client.UserToken);
                    if (res.IsSuccess)
                    {
                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());


                        //保存文件信息 
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("FILE_NAME", resultDIC["filename"].ToString());//图片名称
                        data.Add("FILE_URL", resultDIC["url"].ToString());//文件路径
                        data.Add("GUID", guid);//guid
                        data.Add("TYPE", file_type);//文件类型

                        data.Add("DEVELOP_SEASON", develop_season);
                        data.Add("SHOE_NO", shoe_no);


                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_QCMAPI", "SJ_QCMAPI.QAShoeShapeTable", "SaveFileImg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (!ret.IsSuccess)
                            throw new Exception(ret.ErrMsg);
                        else
                        {
                            MessageBox.Show("File uploaded successfully！");
                        }

                    }
                    else
                    {

                        MessageBox.Show("Failed to upload file！");
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public string UpLoad(string type, string file_type)
        {
            string isload = "no";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmss") + SafeFileName;
                    var content = new MultipartFormDataContent();
                    string path = Path.Combine(filePath);

                    content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", saveName);
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("usertoken", Program.Client.UserToken);
                    p.Add("type", type);
                    p.Add("file_type", file_type);
                    p.Add("develop_season", develop_season);
                    p.Add("shoe_no", shoe_no);
                    p.Add("ImgName", SafeFileName);
                    content.Add(new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(p)), "p");
                    var requestUri = Program.Client.APIURL + "/UploadIMG";
                    var result = client.PostAsync(requestUri, content).Result.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrEmpty(result))
                    {
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());
                        Dictionary<string, object> ImgName = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["returnObj"].ToString());
                        string url = Program.Client.PicUrl + ImgName["url"].ToString();
                        if (dic.ContainsKey("isSuccess"))
                        {
                            string ss = dic["isSuccess"].ToString();
                            if (dic["isSuccess"].ToString().Trim().ToLower() == "true")
                            {
                                isload = "ok";
                            }
                            else
                            {
                                throw new Exception("upload failed");
                            }

                        }
                    }
                    else
                    {
                        throw new Exception("upload failed");
                    }


                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return isload;
        }

        //修改QA鞋型管理
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                #region 参数
                p.Add("develop_season", develop_season);//季度
                p.Add("shoe_no", shoe_no);//鞋型
                p.Add("tryon_result", com1.SelectedValue);//试穿结果
                p.Add("fgt_result", com2.SelectedValue);//FGT结果
                p.Add("cma_result", com3.SelectedValue);//CMA结果
                #endregion

                #region 找接口

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                   Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                   "SJ_QCMAPI.QAShoeShapeTable",//类名
                                                   "UpdateQcm_qa_shoeshape_m",//方法名
                                                   Program.Client.UserToken,//token
                                                   Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
