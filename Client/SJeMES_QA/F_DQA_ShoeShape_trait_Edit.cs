using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using SJeMES_QA.UControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SJeMES_QA
{
    public partial class F_DQA_ShoeShape_trait_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string shoe_no = string.Empty;
        string section_manager = string.Empty;

        string txt_sc = string.Empty;
        string txt_fgt = string.Empty;
        string txt_cma = string.Empty;
        public F_DQA_ShoeShape_trait_Edit()
        {
            InitializeComponent();
            aa();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void aa()
        {
            label37.Text = "";
            label38.Text = "";
            label19.Text = "";
            label23.Text = "";
            label26.Text = "";
            label35.Text = "";
            label30.Text = "";
            label20.Text = "";
            label24.Text = "";
            label7.Text = "";
            label29.Text = "";
            label21.Text = "";
            label27.Text = "";
            label25.Text = "";
            label36.Text = "";
            label22.Text = "";
            label28.Text = "";
            label34.Text = "";
        }
        public F_DQA_ShoeShape_trait_Edit(string _shoe_no,string _section_manager,string _txt_sc,string _txt_fgt,string _txt_cma)
        {
            txt_sc = _txt_sc;
            txt_fgt = _txt_fgt;
            txt_cma = _txt_cma;

            shoe_no = _shoe_no;
            section_manager = _section_manager;
            InitializeComponent();
            aa();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 跳转各阶段样品记录详情页面查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDQAtraitEdit(string shoe_no)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("SHOE_NO", shoe_no);//名称
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
                                            "GetDQAtraitEdit",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

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
                    string[] prod_no = dt.Rows[0]["PROD_NO"].ToString().Split(',');
                    for (int i = 0; i < prod_no.Length; i++)
                    {
                        checkedListBox1.Items.Add(prod_no[i]);
                    }
                    label19.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();
                    label20.Text = dt.Rows[0]["name_t"].ToString();
                    label30.Text = dt.Rows[0]["PRODUCT_MONTH"].ToString();
                    label37.Text = string.IsNullOrEmpty(dt.Rows[0]["try_on_state"].ToString()) ? "Pending" : dt.Rows[0]["try_on_state"].ToString();
                    label38.Text = string.IsNullOrEmpty(dt.Rows[0]["fgt_state"].ToString()) ? "Pending" : dt.Rows[0]["fgt_state"].ToString();
                    label39.Text = string.IsNullOrEmpty(dt.Rows[0]["cma_state"].ToString()) ? "Pending" : dt.Rows[0]["cma_state"].ToString();
                    label7.Text = dt.Rows[0]["production_plant"].ToString();
                    label25.Text = dt.Rows[0]["process_specialist"].ToString();
                    label36.Text = dt.Rows[0]["section_chief"].ToString();
                    label34.Text = dt.Rows[0]["bottom_formwork_specialist"].ToString();
                    label35.Text = dt.Rows[0]["master_editor"].ToString();

                    label23.Text = dt.Rows[0]["rule_no"].ToString();
                    label27.Text = dt.Rows[0]["BOM_DATE"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["BOM_DATE"]).ToString("yyyy-MM-dd");
                    label28.Text = dt.Rows[0]["cwa_date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["cwa_date"]).ToString("yyyy-MM-dd");
                    label26.Text = dt.Rows[0]["sumcol1"].ToString();
                    label29.Text = dt.Rows[0]["user_fdd"].ToString();

                    label21.Text = dt.Rows[0]["mold_no"].ToString();
                    label22.Text = dt.Rows[0]["SHOE_NO"].ToString();
                    label24.Text = dt.Rows[0]["develop_type"].ToString();

                    //textBox1.Text = dt.Rows[0]["try_on_remark"].ToString();
                    //textBox2.Text = dt.Rows[0]["fgt_remark"].ToString();
                    //textBox3.Text = dt.Rows[0]["cma_remark"].ToString();
                    textBox1.Text = txt_sc;
                    textBox2.Text = txt_fgt;
                    textBox3.Text = txt_cma;
                    label36.Text = section_manager;
                    //label19.Text= dt.Rows[0]["develop_season"].ToString();
                    //label27.Text = dt.Rows[0]["bom_date"].ToString();
                    //label28.Text = dt.Rows[0]["cwa_date"].ToString();
                    //label29.Text = dt.Rows[0]["user_fdd"].ToString();
                    //label24.Text = dt.Rows[0]["develop_type"].ToString();
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 历史各阶段样品品质状况
        /// </summary>
        public void GET_ShoeShapecenterView(string is_dqa_mqa_band="")
        {
            try
            {
                this.flowLayoutPanelTable.Controls.Clear();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("shoes_code", shoe_no);
                data.Add("is_dqa_mqa_band", is_dqa_mqa_band);
                List<string> art = new List<string>();
                foreach (System.String item in this.checkedListBox1.CheckedItems)
                {
                    art.Add(item);
                }
                data.Add("art", art);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                           "SJ_BDMAPI.DQA_ShoeShape",//类名
                                           "GET_ShoeShapecenterViewItem",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable data1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());//鞋型品质记录——品质状况
                DataTable data2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());//鞋型品质记录——品质状况——详情
                if (data1.Rows.Count > 0 && data2.Rows.Count > 0)
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("itemid");
                    dt.Columns.Add("shoes_code");
                    dt.Columns.Add("choice_no");
                    dt.Columns.Add("choice_name");
                    dt.Columns.Add("qa_risk_desc");
                    dt.Columns.Add("qa_risk_category_code");
                    dt.Columns.Add("qa_risk_category_name");
                    dt.Columns.Add("art_codes");
                    dt.Columns.Add("bad_qty");
                    dt.Columns.Add("bad_rate");
                    dt.Columns.Add("measures");
                    // dt.Columns.Add("person_in_charge");
                    dt.Columns.Add("remark");
                    dt.Columns.Add("image_guid");
                    dt.Columns.Add("phase_date");//生产只数
                    dt.Columns.Add("itemnumber");//项次
                    dt.Columns.Add("phase_creation_no");
                    dt.Columns.Add("phase_creation_name");
                    dt.Columns.Add("total_production");
                    dt.Columns.Add("is_dqa_mqa_band"); 
                    dt.Columns.Add("workshop_section_no");
                    dt.Columns.Add("workshop_section_name");
                    dt.Columns.Add("qa_risk_details_desc");
                    dt.Columns.Add("measures_res");
                    foreach (DataRow item1 in data1.Rows)
                    {
                        dt.Rows.Clear();
                        int i = 1;
                        foreach (DataRow item2 in data2.Rows)
                        {
                            //if (item1["shoes_code"].ToString() == item2["shoes_code"].ToString() &&
                            //item1["phase_date"].ToString() == item2["phase_date"].ToString() &&
                            //item1["phase_creation_no"].ToString() == item2["phase_creation_no"].ToString() &&
                            //item1["total_production"].ToString() == item2["total_production"].ToString())
                            if(item1["id"].ToString() == item2["d_id"].ToString())
                            {
                                DataRow drr = dt.NewRow();
                                drr["itemid"] = item2["itemid"];
                                drr["shoes_code"] = item2["shoes_code"];
                                drr["choice_no"] = item2["choice_no"];
                                drr["choice_name"] = item2["choice_name"];
                                drr["qa_risk_desc"] = item2["qa_risk_desc"];
                                drr["qa_risk_category_code"] = item2["qa_risk_category_code"];
                                drr["qa_risk_category_name"] = item2["qa_risk_category_name"];
                                drr["art_codes"] = item2["art_codes"];
                                drr["bad_qty"] = item2["bad_qty"];
                                drr["bad_rate"] = item2["bad_rate"];
                                drr["measures"] = item2["measures"];
                                drr["measures_res"] = item2["MEASURES_RES"];
                                drr["remark"] = item2["remark"];
                                drr["qa_risk_details_desc"] = item2["QA_RISK_DETAILS_DESC"];
                                drr["image_guid"] = item2["image_guid"];
                                drr["phase_date"] = item2["phase_date"];
                                drr["phase_creation_no"] = item2["phase_creation_no"];
                                drr["phase_creation_name"] = item2["phase_creation_name"];
                                drr["total_production"] = item2["total_production"];
                                drr["is_dqa_mqa_band"] = item2["is_dqa_mqa_band"];

                                drr["workshop_section_no"] = item2["workshop_section_no"];
                                drr["workshop_section_name"] = item2["workshop_section_name"];
                                drr["itemnumber"] = i++;
                                dt.Rows.Add(drr);
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            UCTableCheck uc = new UCTableCheck(dt,this);
                            this.flowLayoutPanelTable.Controls.Add(uc);
                        }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void F_DQA_ShoeShape_trait_Edit_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetDQAtraitEdit(shoe_no);
            GET_ShoeShapecenterView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 各阶段样品记录查看详情查看图片
        /// </summary>
        /// <param name="itemid"></param>
        public List<string> _itemid = new List<string>();
        public void Getimage_guidItem(List<string> itemid)
        {

            //加载图片
            this.flowLayoutPanelimg.Controls.Clear();
            if (itemid != null && itemid.Count > 0)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("itemid", itemid);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                           "SJ_BDMAPI.DQA_ShoeShape",//类名
                                           "Getimage_guidItem",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable data3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());//图片

                if (data3.Rows.Count > 0)
                {
                    foreach (DataRow item in data3.Rows)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(item["file_url"].ToString()))
                            {
                                var webC = new System.Net.WebClient();
                                string url = Program.Client.PicUrl + item["file_url"].ToString();
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
        }
        //图片预览
        void pic_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            if (null == pic) return;
            string url = pic.Name; // 取出url
            FrmShowImg add = new FrmShowImg(url, "");
            add.Show();
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            GET_ShoeShapecenterView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmFileList add = new FrmFileList(FileView(), Program.Client.UploadUrl, Program.Client.UserToken);
            add.ShowDialog();
        }

        /// <summary>
        /// 查看文件;
        /// </summary>
        public DataTable FileView()
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("SHOE_NO", shoe_no);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
                                            "GetDQAtraitMainFile",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

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

       
        private void rdo_false_Click(object sender, EventArgs e)
        {
            GET_ShoeShapecenterView("0");
        }

        private void rdo_true_Click(object sender, EventArgs e)
        {
            GET_ShoeShapecenterView("1");
        }

        private void rdo_all_Click(object sender, EventArgs e)
        {
            GET_ShoeShapecenterView();
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.textBox2;
            if (!string.IsNullOrEmpty(currC.Text))
            {
                // 创建the ToolTip 
                ToolTip toolTip1 = new ToolTip();

                // 设置显示样式
                toolTip1.AutoPopDelay = 25000;
                toolTip1.InitialDelay = 500;//事件触发多久后出现提示
                toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                toolTip1.ShowAlways = true;//是否显示提示框

                //  设置伴随的对象.
                toolTip1.SetToolTip(currC, currC.Text);//设置提示按钮和提示内容
            }
        }

        private void textBox3_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.textBox3;
            if (!string.IsNullOrEmpty(currC.Text))
            {
                // 创建the ToolTip 
                ToolTip toolTip1 = new ToolTip();

                // 设置显示样式
                toolTip1.AutoPopDelay = 25000;
                toolTip1.InitialDelay = 500;//事件触发多久后出现提示
                toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                toolTip1.ShowAlways = true;//是否显示提示框

                //  设置伴随的对象.
                toolTip1.SetToolTip(currC, currC.Text);//设置提示按钮和提示内容
            }
        }
    }
}
