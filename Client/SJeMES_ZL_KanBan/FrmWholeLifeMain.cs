using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using SJeMES_IQC;
using SjeMES_QCM_Ex;
using SJeMES_Shared_Form;
using SJeMES_TQC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SJeMES_ZL_KanBan
{
    public partial class FrmWholeLifeMain : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        //页签查询
        //public string tabid = string.Empty;//页签id
        Dictionary<string, object> dic = new Dictionary<string, object>();
        public bool status = false;
        //public string tag = string.Empty;
        public string tagName = string.Empty;
        public string configurl = string.Empty;
        //private AsynchronousDataHelper asynchronousDataHelper;
        Dictionary<string, DataTable> dts;
        public FrmWholeLifeMain()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language); 
        }

        private void FrmWholeLifeMain_Load(object sender, EventArgs e)
        {
            //GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            dts = new Dictionary<string, DataTable>();
            InitDateTimePicker(start_date);
            InitDateTimePicker(end_date);
            configurl = Common.ConfigHelper.GetConfigUrl();
            //foreach (Control control in this.Controls)
            //{
            //    //if(control.Container.)
            //}
            SJeMES_Framework.Common.UIHelper.LoadDgv(dgvFailInfoReported);
            //SJeMES_Framework.Common.UIHelper.LoadDgv(DGV_TQC);
            //this.DGV_TQC.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //this.DGV_TQC.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        public static void AutoWidth(DataGridView dgv, DataTable dt)
        {
            int i = 0;
            int w_Count = 0;
            int w = 0;
            int width = dgv.Width;
            int avgWidth = width / dt.Columns.Count;//求出每一列的header宽度
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;//数据溢出换行,根据内容大小自动换行
            for (; i < dgv.Columns.Count; i++)
            {
                dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            }
        }


        //基本信息
        private void GetHeadBaseInfo()
        {
            if (string.IsNullOrEmpty(art_textBox.Text.Trim()) || string.IsNullOrWhiteSpace(this.start_date.Text) || string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Required fields cannot be empty, please check!");
                return;
            }

            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("Art", this.art_textBox.Text);//名称
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetDataHead",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                return;
            }

            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示

            //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.txt_calegory.Text = dt.Rows[0]["rule_no"].ToString();
                this.txt_cslevel.Text = dt.Rows[0]["TEST_LEVEL"].ToString();
                this.txt_shoes.Text = dt.Rows[0]["NAME_T"].ToString();
                this.txt_PDTYPE.Text = dt.Rows[0]["develop_type"].ToString();
                this.txt_date.Text = dt.Rows[0]["PRODUCT_MONTH"].ToString();
                this.txt_season.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();
                this.txt_cwa.Text = dt.Rows[0]["cwa_date"].ToString();


                this.txt_productionlevel.Text = dt.Rows[0]["PRODUCT_LEVEL"].ToString();
                this.txt_cma.Text = dt.Rows[0]["COL1"].ToString();//预定订单

                if (!string.IsNullOrEmpty(dt.Rows[0]["FILE_URL"].ToString()))
                {
                    try
                    {
                        var webC = new System.Net.WebClient();
                        string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["FILE_URL"].ToString());
                        Image image = new Bitmap(webC.OpenRead(url));
                        IMG_Pic.Image = image;
                        IMG_Pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch 
                    {
                    
                    }
                }
                else
                {
                    IMG_Pic.Image = null;
                }
            }
            else
            {
                this.txt_calegory.Text = "";
                this.txt_cslevel.Text = "";
                this.txt_shoes.Text = "";
                this.txt_PDTYPE.Text = "";
                this.txt_date.Text = "";
                this.txt_season.Text = "";
                this.txt_productionlevel.Text = "";
                this.txt_cma.Text = "";
                this.txt_cwa.Text = "";
                //pictureBox1.Image.Dispose();
                IMG_Pic.Image = null;


            }
        }

        public void po(List<string> _listPo)
        {
            string _po = string.Empty;
            for (int i = 0; i < _listPo.Count; i++)
            {
                _po += _listPo[i] + ",";
            }
            _po = _po.TrimEnd(',');
            txt_PO.Text = _po;
            //GetShoe_no_jijie();
        }
        #region old
        //异步加载数据
        /*
        private void LoadTabpageData()
        {
            if (string.IsNullOrEmpty(this.art_textBox.Text.Trim()))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("ART为必填项，请检查！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            dic = new Dictionary<string, object>();
            dic.Add("Art",this.art_textBox.Text);
            dic.Add("PO",this.txt_PO.Text);

            dic.Add("start_date", this.start_date.Text);
            dic.Add("end_date", this.end_date.Text);
            //asynchronousDataHelper = new AsynchronousDataHelper(dic,tabControl1, this);
            //asynchronousDataHelper.GetData();

        }
        
        
        private void GetQAData()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("Art", this.art_textBox.Text);//名称
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetQAData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dic.Add("qadt",dt);
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }


        }
        */
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //this.button1.FillColor = Color.Gainsboro;
            //Thread.Sleep(100);
            //this.button1.FillColor = Color.White;
            //tag = this.tabControl1.SelectedTab.Tag.ToString();
            tagName = this.tabControl1.SelectedTab.Name.ToString();
            //基本信息
            //GetHeadBaseInfo();

            //页签异步获取数据
            //LoadTabpageData();

            //页签数据初始化
            TagDataLoad(tagName, 0);
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tagName = tabControl1.SelectedTab.Name;
            TagDataLoad(tagName, 1);
        }

        //根据页签数据初始化数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="type">0-查询 1-切换页签</param>
        public void TagDataLoad(string tagName, int type)
        {
            if (string.IsNullOrEmpty(art_textBox.Text.Trim()) || string.IsNullOrWhiteSpace(this.start_date.Text) || string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Required fields cannot be empty, please check!");
                return;
            }
            GetHeadBaseInfo();
            switch (tagName)
            {
                case "QA":
                    GetQAData();
                    break;
                case "试穿测试":
                    GetCSData();
                    break;
                case "RQC":
                    GetRQCData();
                    break;
                case "TQC":
                    GetTQCData();
                    break;
                case "实验室":
                    GetSYSData();
                    break;
                case "IQC":
                    GetIQCData();
                    break;
                case "量试":
                    GetLSData();
                    break;
                case "市场反馈":
                    GetCustomerData();
                    break;
                case "金属检测":
                    GetJSJCData();
                    break;
                case "订单信息":
                    GetDDData();
                    break;
                case "合规性":
                    GetComplianceData();
                    //GetJointlyData();
                    //GetA01Data();
                    break;
                case "AQL":
                    GetA01Data();
                    break;
                case "异常呈报":
                    GetFailInfoReported();
                    break;
                default:
                    break;
            }
        }

        //获取QA页签数据
        private void GetQAData()
        {
            try
            {
                DataTable dt = new DataTable();
                //点击查询 或 记录数据字典不存在该页签dt

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值  
                data.Add("Art", art_textBox.Text);//名称

                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetQAData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                //dic_data.Add(tabPage.Name, dt);
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    ////记录dt
                    //dts[tabControl1.SelectedTab.Name] = dt;

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["did"].Value = dr["did"].ToString();


                        dgvr.Cells["problemsources"].Value = dr["problemsources"].ToString();//问题来源
                        dgvr.Cells["workshop_section_no"].Value = dr["workshop_section_no"].ToString();//工段编号
                        dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();//工段名称
                        //dgvr.Cells["image_guid"].Value = dr["image_guid"].ToString();


                        dgvr.Cells["art_code"].Value = dr["art_code"].ToString();//ART

                        dgvr.Cells["qa_risk"].Value = dr["qa_risk"].ToString();//品质风险【暂无数据来源】
                        dgvr.Cells["inspection_code"].Value = dr["inspection_code"].ToString();
                        dgvr.Cells["inspection_name"].Value = dr["inspection_name"].ToString();//检测项目名称
                        dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString();//类型

                        dgvr.Cells["DQAfilelist"].Value = dr["DQAfilelist"].ToString();
                        dgvr.Cells["processing_record"].Value = dr["processing_record"].ToString();

                        dgvr.Cells["f_insp_res"].Value = dr["f_insp_res"].ToString();//检测结果

                        dgvr.Cells["MQAfilelist"].Value = dr["MQAfilelist"].ToString();


                        i++;
                    }


                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);

            }


        }
        //试穿页签
        private void GetCSData()
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                //点击查询 或 记录数据字典不存在该页签dt
                //if (type == 0 && !dts.ContainsKey(tabControl1.SelectedTab.Name))
                //{
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值  
                data.Add("Art", art_textBox.Text);//名称

                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetCSData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["FitData1"].ToString());
                dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["WearData1"].ToString());

                //记录dt
                //dts.Add(tabControl1.SelectedTab.Name + "Fit", dt);
                //dts.Add(tabControl1.SelectedTab.Name + "Wear", dt);
                //}
                //else
                //{
                //    dt = dts[tabControl1.SelectedTab.Name];
                //}


                sc_dgv.Rows.Clear();
                if (dt.Rows.Count > 0)
                {

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sc_dgv.Rows.Add();
                        DataGridViewRow dgvr = sc_dgv.Rows[i];
                        dgvr.Cells["TESTID"].Value = dr["TESTID"].ToString();
                        dgvr.Cells["TESTPHASE"].Value = dr["TESTPHASE"].ToString();
                        dgvr.Cells["REPORTDATE"].Value = dr["REPORTDATE"].ToString();
                        dgvr.Cells["SIZE"].Value = dr["SIZE"].ToString();
                        //dgvr.Cells["试穿员"].Value = dr["USERNAME"].ToString();
                        dgvr.Cells["FITRESULT"].Value = dr["FITRESULT"].ToString();

                        if (!string.IsNullOrEmpty(dr["FITRESULT"].ToString()))
                        {
                            string value = dr["FITRESULT"].ToString().ToUpper() == "PASS" ? "0" : dr["FITRESULT"].ToString().ToUpper() == "FAIL" ? "1" : "";
                            string color = isBool(value);

                            if (color == "red")
                            {
                                dgvr.Cells["FITRESULT"].Style.BackColor = Color.Red;
                                dgvr.Cells["FITRESULT"].Style.ForeColor = Color.White;
                            }
                            else if (color == "green")
                            {
                                dgvr.Cells["FITRESULT"].Style.BackColor = Color.Green;
                                dgvr.Cells["FITRESULT"].Style.ForeColor = Color.White;
                            }
                        }

                        dgvr.Cells["URL"].Value = "查看测试报告";//查看测试报告

                        i++;
                    }


                }

                sc_dgv2.Rows.Clear();
                if (dt2.Rows.Count > 0)
                {
                    //记录dt
                    //dts.Add(tabControl1.SelectedTab.Name + "Wear", dt2);

                    int i = 0;
                    foreach (DataRow dr in dt2.Rows)
                    {
                        sc_dgv2.Rows.Add();
                        DataGridViewRow dgvr = sc_dgv2.Rows[i];
                        dgvr.Cells["TESTID2"].Value = dr["TESTID"].ToString();
                        dgvr.Cells["TESTPHASE2"].Value = dr["TESTPHASE"].ToString();
                        dgvr.Cells["ENDDATE"].Value = dr["ENDDATE"].ToString();
                        dgvr.Cells["HOURS1"].Value = dr["HOURS1"].ToString();
                        dgvr.Cells["HOURS2"].Value = dr["HOURS2"].ToString();
                        dgvr.Cells["SIZE2"].Value = dr["SIZE"].ToString();
                        dgvr.Cells["试穿员2"].Value = dr["USERNAME"].ToString();
                        dgvr.Cells["试穿员代号2"].Value = dr["USERCODE"].ToString();

                        dgvr.Cells["TEST_REPORT2"].Value = "";
                        dgvr.Cells["URL2"].Value = "查看测试报告";// dr["SIZE"].ToString();

                        i++;
                    }


                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);

            }


        }

        //实验室页签
        private void GetSYSData()
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                //点击查询 或 记录数据字典不存在该页签dt
                //if (type == 0 && !dts.ContainsKey(tabControl1.SelectedTab.Name))
                //{
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值  
                data.Add("Art", art_textBox.Text);//名称

                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetSYSData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());


                sysdgv.Rows.Clear();
                if (dt.Rows.Count > 0)
                {

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sysdgv.Rows.Add();
                        DataGridViewRow dgvr = sysdgv.Rows[i];

                        string TEST_TYPE = string.Empty;
                        //枚举 0：成品鞋；1：部件；2：工艺；3：材料；4：量产拉力；
                        switch (dr["TEST_TYPE"].ToString())
                        {
                            case "0":
                                //TEST_TYPE = "成品鞋";
                                TEST_TYPE = "Finished shoes";
                                break;
                            case "1":
                                //TEST_TYPE = "部件";
                                TEST_TYPE = "Part";
                                break;
                            case "2":
                               // TEST_TYPE = "工艺";
                                TEST_TYPE = "Craft";
                                break;
                            case "3":
                                //TEST_TYPE = "材料";
                                TEST_TYPE = "Material";
                                break;
                            case "4":
                                //TEST_TYPE = "量产拉力";
                                TEST_TYPE = "Production Rally";
                                break;
                            default:
                                break;
                        }


                        dgvr.Cells["PHASE_CREATION_NAME"].Value = dr["PHASE_CREATION_NAME"].ToString();
                        dgvr.Cells["TEST_TYPE"].Value = TEST_TYPE;
                        dgvr.Cells["CREATEDATE"].Value = dr["CREATEDATE"].ToString();
                        dgvr.Cells["TEST_ID"].Value = dr["TEST_ID"].ToString();
                        dgvr.Cells["TEST_RESULT"].Value = dr["TEST_RESULT"].ToString();

                        if (!string.IsNullOrEmpty(dr["TEST_RESULT"].ToString()))
                        {
                            string value = dr["TEST_RESULT"].ToString() == "PASS" ? "0" : "1";
                            string color = isBool(value);

                            if (color == "red")
                            {
                                dgvr.Cells["TEST_RESULT"].Style.BackColor = Color.Red;
                                dgvr.Cells["TEST_RESULT"].Style.ForeColor = Color.White;
                            }
                            else if (color == "green")
                            {
                                dgvr.Cells["TEST_RESULT"].Style.BackColor = Color.Green;
                                dgvr.Cells["TEST_RESULT"].Style.ForeColor = Color.White;
                            }
                        }



                        dgvr.Cells["TASK_NO"].Value = dr["TASK_NO"].ToString();


                        dgvr.Cells["STAFF_DEPARTMENT"].Value = dr["STAFF_DEPARTMENT"].ToString();


                        i++;
                    }


                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }


        }

        /// <summary>
        /// 合规性文件/联名/A-01 页签数据
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void GetComplianceData()
        {
            Dictionary<string, Object> data = new Dictionary<string, object>();
            data.Add("PROD_NO", art_textBox.Text.Trim());
            data.Add("PO", txt_PO.Text.Trim());
            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_date.Text))
            {
                start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
            }

            data.Add("start_date", start_date);
            data.Add("end_date", end_date);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetComplianceData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                DataTable dt3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());

                //合规
                DGV_Compliance.Rows.Clear();
                int i = 0;
                foreach (DataRow item in dt.Rows)
                {
                    DGV_Compliance.Rows.Add();
                    DataGridViewRow dr = DGV_Compliance.Rows[i];
                    dr.Cells["PO_HG"].Value = item["MER_PO"].ToString();
                    dr.Cells["PODD_HG"].Value = item["PODD"].ToString();
                    dr.Cells["出货日期"].Value = item["OUT_TIME"].ToString();
                    dr.Cells["文件类型"].Value = item["curr_file_type"].ToString();
                    dr.Cells["开始时间"].Value = item["START_TIME"].ToString();
                    dr.Cells["有效期"].Value = item["CURR_VALID_TIME"].ToString();
                    dr.Cells["上传时间"].Value = item["curr_upload_time"].ToString();
                    dr.Cells["查看文件"].Value = "查看文件";//item["FILE_GUID"].ToString();
                    dr.Cells["查看文件类型代号"].Value = item["file_type"].ToString();
                    i++;
                }

                //联名
                DGV_Jointly.Rows.Clear();
                int x = 0;
                foreach (DataRow item in dt2.Rows)
                {
                    DGV_Jointly.Rows.Add();
                    DataGridViewRow dr = DGV_Jointly.Rows[x];
                    dr.Cells["PO_LM"].Value = item["PO"].ToString();
                    dr.Cells["PODD_LM"].Value = item["PODD"].ToString();
                    dr.Cells["出货日期联名"].Value = item["OUT_TIME"].ToString();
                    dr.Cells["文件类型联名"].Value = item["curr_file_type"].ToString();

                    dr.Cells["开始日期联名"].Value = item["START_TIME"].ToString();
                    dr.Cells["有效期联名"].Value = item["CURR_VALID_TIME"].ToString();
                    dr.Cells["上传时间联名"].Value = item["curr_upload_time"].ToString();
                    dr.Cells["查看文件联名"].Value = "查看文件";//item["FILE_GUID"].ToString();
                    dr.Cells["文件类型代号联名"].Value = item["file_type"].ToString();
                    x++;
                }
                //A-01
                DataTable AQLdt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());
                DGV_A01.Rows.Clear();
                //DGV_AQL.Rows.Clear();
                int z = 0;
                foreach (DataRow item in dt3.Rows)
                {
                    DGV_A01.Rows.Add();
                    DataGridViewRow dr = DGV_A01.Rows[z];
                    dr.Cells["PO_A01"].Value = item["PO"].ToString();
                    dr.Cells["PODD_A01"].Value = item["PODD"].ToString();
                    dr.Cells["验货时间"].Value = item["验货时间"].ToString();
                    dr.Cells["出货时间"].Value = item["出货时间"].ToString();
                    dr.Cells["开始日期"].Value = item["开始日期"].ToString();
                    dr.Cells["有效期A01"].Value = item["有效期"].ToString();
                    dr.Cells["查看文件A01"].Value = "查看文件";//item["GUID"].ToString();
                    dr.Cells["文件信息"].Value = item["文件GUID"].ToString();

                    dr.Cells["文件名称"].Value = item["file_name"].ToString();
                    dr.Cells["文件上传"].Value = item["CURR_UPLOAD_TIME"].ToString();
                    i++;
                }
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
        }

        /// <summary>
        /// 联名性文件页签数据【弃用】
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void GetJointlyData()
        {
            Dictionary<string, Object> data = new Dictionary<string, object>();
            data.Add("PROD_NO", art_textBox.Text.Trim());
            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_date.Text))
            {
                start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
            }

            data.Add("start_date", start_date);
            data.Add("end_date", end_date);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetJointlyData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                DGV_Jointly.Rows.Clear();
                int i = 0;
                foreach (DataRow item in dt.Rows)
                {
                    DGV_Jointly.Rows.Add();
                    DataGridViewRow dr = DGV_Jointly.Rows[i];
                    dr.Cells["PO_LM"].Value = item["PO"].ToString();
                    dr.Cells["PODD_LM"].Value = item["PODD"].ToString();
                    dr.Cells["出货日期联名"].Value = item["OUT_TIME"].ToString();
                    dr.Cells["文件类型联名"].Value = item["curr_file_type"].ToString();
                    dr.Cells["开始日期联名"].Value = item["START_TIME"].ToString();
                    dr.Cells["有效期联名"].Value = item["CURR_VALID_TIME"].ToString();
                    dr.Cells["上传时间联名"].Value = item["curr_upload_time"].ToString();
                    dr.Cells["查看文件联名"].Value = item["FILE_GUID"].ToString();
                    i++;
                }
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
        }

        /// <summary>
        /// IQC页签
        /// </summary>
        private void GetIQCData()
        {
            try
            {
                DataTable dt = new DataTable();
                //点击查询 或 记录数据字典不存在该页签dt
                //if (type == 0 && !dts.ContainsKey(tabControl1.SelectedTab.Name))
                //{
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值  
                data.Add("Art", art_textBox.Text);//名称
                data.Add("PO", txt_PO.Text);//PO

                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetIQCData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());


                IQCdgv.Rows.Clear();
                if (dt.Rows.Count > 0)
                {

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        IQCdgv.Rows.Add();
                        DataGridViewRow dgvr = IQCdgv.Rows[i];

                        dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();//材料编号
                        dgvr.Cells["NAME_T"].Value = dr["NAME_T"].ToString();//材料名称

                        dgvr.Cells["SUPPLIERS_CODE2"].Value = dr["SUPPLIERS_CODE2"].ToString();//采购厂商
                        dgvr.Cells["SUPPLIERS_NAME2"].Value = dr["SUPPLIERS_NAME2"].ToString();


                        dgvr.Cells["RCPT_QTY"].Value = dr["RCPT_QTY"].ToString();//进仓数量/收料数量
                        dgvr.Cells["INSPECTIONDATE"].Value = dr["INSPECTIONDATE"].ToString();//外观检验日期





                        dgvr.Cells["sysresult"].Value = dr["RESULT"].ToString();//实验室结果

                        if (!string.IsNullOrEmpty(dr["RESULT"].ToString()))
                        {

                            string value = dr["RESULT"].ToString() == "PASS" ? "0" : dr["RESULT"].ToString() == "FAIL" ? "1" : "";//外观结果

                            string color = isBool(value);

                            if (color == "red")
                            {
                                dgvr.Cells["sysresult"].Style.BackColor = Color.Red;
                                dgvr.Cells["sysresult"].Style.ForeColor = Color.White;
                            }
                            else if (color == "green")
                            {
                                dgvr.Cells["sysresult"].Style.BackColor = Color.Green;
                                dgvr.Cells["sysresult"].Style.ForeColor = Color.White;
                            }
                        }






                        dgvr.Cells["DETERMINE"].Value = dr["DETERMINE"].ToString() == "0" ? "PASS" : dr["DETERMINE"].ToString() == "1" ? "FAIL" : "";//外观结果
                        if (!string.IsNullOrEmpty(dr["DETERMINE"].ToString()))
                        {
                            string color = isBool(dr["DETERMINE"].ToString());

                            if (color == "red")
                            {
                                dgvr.Cells["DETERMINE"].Style.BackColor = Color.Red;
                                dgvr.Cells["DETERMINE"].Style.ForeColor = Color.White;
                            }
                            else if (color == "green")
                            {
                                dgvr.Cells["DETERMINE"].Style.BackColor = Color.Green;
                                dgvr.Cells["DETERMINE"].Style.ForeColor = Color.White;
                            }
                        }



                        dgvr.Cells["result_order"].Value = "原材料外观报告";//实验室结果
                        //dgvr.Cells[""].Value = dr[""].ToString();
                        dgvr.Cells["taskno"].Value = dr["task_no"].ToString();
                        dgvr.Cells["sysbg"].Value = dr["task_no"].ToString();

                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();//生产厂商
                        dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString();//鞋型
                        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();//ART
                        dgvr.Cells["ITEM_TYPE_NO"].Value = dr["ITEM_TYPE_NO"].ToString();//材料类型
                        dgvr.Cells["ORDER_NO"].Value = dr["ORDER_NO"].ToString();//采购单号
                        dgvr.Cells["CHK_SEQ"].Value = dr["CHK_SEQ"].ToString();//材料序号
                        dgvr.Cells["PART_NO"].Value = dr["PART_NO"].ToString();//材料序号
                        dgvr.Cells["RCPT_DATE"].Value = dr["RCPT_DATE"].ToString();//材料序号
                        dgvr.Cells["CHK_NO"].Value = dr["CHK_NO"].ToString();//收料单号

                        i++;
                    }


                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);

            }


        }

        /// <summary>
        /// 根据结果判断单元格底色
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string isBool(string Value)
        {
            string res = string.Empty;

            if (Value.ToString().Trim() == "0")
            {
                res = "green";
            }
            else if (Value.ToString().Trim() == "1")
            {
                res = "red";

            }
            else
            {
                res = "";
            }
            return res;
        }

        /// <summary>
        ///量试页签
        /// </summary>
        private void GetLSData()
        {
            try
            {
                DataTable dt = new DataTable();
                //点击查询 或 记录数据字典不存在该页签dt
                //if (type == 0 && !dts.ContainsKey(tabControl1.SelectedTab.Name))
                //{
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值  
                data.Add("Art", art_textBox.Text);//名称
                //data.Add("PO", txt_PO.Text);//PO

                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetLSData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());


                LSdgv.Rows.Clear();
                if (dt.Rows.Count > 0)
                {

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        LSdgv.Rows.Add();
                        DataGridViewRow dgvr = LSdgv.Rows[i];

                        dgvr.Cells["日期2"].Value = dr["日期"].ToString();//日期
                        dgvr.Cells["工段2"].Value = dr["工段"].ToString();//
                        dgvr.Cells["问题数量2"].Value = dr["问题数量"].ToString();//
                        dgvr.Cells["通过数量2"].Value = dr["通过数量"].ToString();//
                        dgvr.Cells["不通过数量2"].Value = dr["不通过数量"].ToString();//
                        dgvr.Cells["ART"].Value = dr["prod_no"].ToString();//
                        dgvr.Cells["鞋型"].Value = dr["shoes_code"].ToString();//
                        dgvr.Cells["工段代号"].Value = dr["workshop_section_no"].ToString();//
                        dgvr.Cells["量试报告2"].Value = "查看报告";//



                        i++;
                    }


                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);

            }


        }

        /// <summary>
        /// AQL页签数据查询
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void GetA01Data()
        {
            Dictionary<string, Object> data = new Dictionary<string, object>();
            data.Add("PROD_NO", art_textBox.Text.Trim());
            data.Add("PO", txt_PO.Text.Trim());
            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_date.Text))
            {
                start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
            }

            data.Add("start_date", start_date);
            data.Add("end_date", end_date);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetA01Data",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                DataTable AQLdt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["AQLData"].ToString());

                DGV_AQL.Rows.Clear();
                /*
                 * DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                 * DGV_A01.Rows.Clear();
                int i = 0;
                foreach (DataRow item in dt.Rows)
                {
                    DGV_A01.Rows.Add();
                    DataGridViewRow dr = DGV_A01.Rows[i];
                    dr.Cells["PO_A01"].Value = item["PO"].ToString();
                    dr.Cells["PODD_A01"].Value = item["PODD"].ToString();
                    dr.Cells["验货时间"].Value = item["Inspection_Date"].ToString();
                    dr.Cells["出货时间"].Value = item["PostDate"].ToString();
                    dr.Cells["开始日期"].Value = item["StartDate"].ToString();
                    dr.Cells["有效期A01"].Value = item["ValidDate"].ToString();
                    dr.Cells["查看文件A01"].Value = item["GUID"].ToString();
                    i++;
                }
               */
                int j = 0;
                foreach (DataRow item in AQLdt.Rows)
                {
                    DGV_AQL.Rows.Add();
                    DataGridViewRow dr = DGV_AQL.Rows[j];
                    dr.Cells["PO_AQL"].Value = item["PO"].ToString();
                    dr.Cells["出货国家2"].Value = item["DESCOUNTRY_NAME"].ToString();
                    dr.Cells["PO数量3"].Value = item["po_num"].ToString();
                    dr.Cells["验货日期"].Value = item["f_inspection_time"].ToString();
                    dr.Cells["验货数量"].Value = item["lot_num"].ToString();
                    dr.Cells["抽验双数"].Value = item["Sampling_quantity"].ToString();
                    dr.Cells["验货员"].Value = item["Inspector"].ToString();
                    dr.Cells["验货结果"].Value = item["inspection_results"].ToString();

                    string value = item["inspection_results"].ToString().ToLower() == "pass" ? "0" : item["inspection_results"].ToString().ToLower() == "fail" ? "1" : "";
                    if (!string.IsNullOrEmpty(value))
                    {
                        string color = isBool(value);

                        if (color == "red")
                        {
                            dr.Cells["验货结果"].Style.BackColor = Color.Red;
                            dr.Cells["验货结果"].Style.ForeColor = Color.White;
                        }
                        else if (color == "green")
                        {
                            dr.Cells["验货结果"].Style.BackColor = Color.Green;
                            dr.Cells["验货结果"].Style.ForeColor = Color.White;
                        }
                    }



                    dr.Cells["首次出货日期2"].Value = item["First_ShipmentDate"].ToString();
                    dr.Cells["最后出货日期2"].Value = item["Last_ShipmentDate"].ToString();
                    dr.Cells["验货报告"].Value = item["task_no"].ToString();
                    dr.Cells["验货状态AQL"].Value = item["inspection_state"].ToString();
                    j++;
                }

            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
        }

        #region 异常呈报
        private void GetFailInfoReported()
        {
            try
            {
                Dictionary<string, Object> data = new Dictionary<string, object>();
                data.Add("PROD_NO", art_textBox.Text.Trim());
                data.Add("PO", txt_PO.Text.Trim());
                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "QualityFailReported",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                //dgvFailInfoReported.DataSource = dt;

                int j = 0;
                foreach (DataRow item in dt.Rows)
                {
                    dgvFailInfoReported.Rows.Add();
                    DataGridViewRow dr = dgvFailInfoReported.Rows[j];
                    dr.Cells["fir_task_no"].Value = item["task_no"].ToString();
                    dr.Cells["fir_abnormal_level"].Value = item["abnormal_level"].ToString();
                    dr.Cells["fir_abnormal_level_name"].Value = item["abnormal_level_name"].ToString();
                    dr.Cells["fir_abnormal_category_no"].Value = item["abnormal_category_no"].ToString();

                    dr.Cells["fir_abnormal_category_name"].Value = item["abnormal_category_name"].ToString();

                    dr.Cells["fir_prod_no"].Value = item["prod_no"].ToString();
                    dr.Cells["fir_prod_name"].Value = item["prod_name"].ToString();//鞋型名称

                    dr.Cells["fir_shoe_no"].Value = item["shoe_no"].ToString();
                    dr.Cells["fir_name_t"].Value = item["name_t"].ToString();
                    dr.Cells["fir_develop_season"].Value = item["develop_season"].ToString();
                    dr.Cells["fir_pro_month"].Value = item["pro_month"].ToString();

                    dr.Cells["fir_workshop_section_no"].Value = item["workshop_section_no"].ToString();
                    dr.Cells["fir_workshop_section_name"].Value = item["workshop_section_name"].ToString();

                    dr.Cells["fir_production_line_code"].Value = item["production_line_code"].ToString();
                    dr.Cells["fir_production_line_name"].Value = item["production_line_name"].ToString();
                    dr.Cells["fir_plant_area"].Value = item["plant_area"].ToString();//厂区

                    dr.Cells["fir_po_list"].Value = item["po_list"].ToString();//
                    dr.Cells["fir_fx_qty"].Value = item["fx_qty"].ToString();//
                    dr.Cells["fir_problem_desc"].Value = item["problem_desc"].ToString();//问题描述

                    dr.Cells["fir_department_code"].Value = item["department_code"].ToString();//
                    dr.Cells["fir_department_name"].Value = item["department_name"].ToString();//
                    dr.Cells["fir_department_codecc"].Value = item["department_codecc"].ToString();//
                    dr.Cells["fir_closing_status"].Value = item["closing_status"].ToString();//
                    dr.Cells["详情页"].Value = "查看详情";//
                    j++;


                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
        }
        //品质异常
        private void dgvFailInfoReported_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (dgvFailInfoReported.Columns[e.ColumnIndex].Name == "详情页")
            {
                string fir_task_no = dgvFailInfoReported.Rows[e.RowIndex].Cells["fir_task_no"].Value.ToString();

                //string url = string.Empty;calhost:8081/#/qualityErrorDetail?task_no=R2022-11-2800001
                string url = $"{configurl}/qualityErrorDetail?{fir_task_no}&{Program.Client.CompanyCode}";

                //&{Program.Client.CompanyCode}
                if (!string.IsNullOrEmpty(fir_task_no))
                {
                    FrmReport frmReport = new FrmReport(url);
                    frmReport.Show();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("查无任务编号，打开失败！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }

            }
        }
        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string problemsources = dataGridView1.Rows[e.RowIndex].Cells["problemsources"].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex].Name == "DQAfile")
            {
                DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["DQAfile"] as DataGridViewOperationCell;
                if (cell.CurrentItem == null)
                {
                    return;
                }
                if (cell.CurrentItem.Equals("DQALIST"))
                {
                    string DQAfjguid = dataGridView1.Rows[e.RowIndex].Cells["DQAfilelist"].Value.ToString();
                    FrmFileList add = new FrmFileList(GetDQAFile(DQAfjguid), Program.Client.UploadUrl, Program.Client.UserToken, "", true, false);
                    add.Show();
                }
            }
            //MQA附件
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "MQAfile")
            {

                DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["MQAfile"] as DataGridViewOperationCell;
                if (cell.CurrentItem == null)
                {
                    return;
                }
                if (cell.CurrentItem.Equals("MQALIST"))
                {
                    string did = dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString();
                    if (!string.IsNullOrEmpty(did))
                    {
                        string[] fjguid = dataGridView1.Rows[e.RowIndex].Cells["MQAfilelist"].Value.ToString().Split(',');

                        var currRowFileDt = GetShoeShape_EditFile(fjguid);
                        FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", false, false, "QCM_MQA_MAG_D_F&FILE_ID");
                        add.Show();
                        int i = 0;
                        string fjguids = string.Empty;
                        foreach (DataRow item in currRowFileDt.Rows)
                        {
                            fjguids += item["guid"];
                            if (i < currRowFileDt.Rows.Count - 1)
                            {
                                fjguids += ",";
                            }
                            i++;
                        }
                        dataGridView1.Rows[e.RowIndex].Cells["MQAfilelist"].Value = fjguids;
                    }
                    else
                    {
                        string[] fjguid = dataGridView1.Rows[e.RowIndex].Cells["MQAfilelist"].Value.ToString().Split(',');

                        var currRowFileDt = GetShoeShape_EditFile(fjguid);
                        FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", false);
                        add.Show();
                        int i = 0;
                        string fjguids = string.Empty;
                        foreach (DataRow item in currRowFileDt.Rows)
                        {
                            fjguids += item["guid"];
                            if (i < currRowFileDt.Rows.Count - 1)
                            {
                                fjguids += ",";
                            }
                            i++;
                        }
                        dataGridView1.Rows[e.RowIndex].Cells["MQAfilelist"].Value = fjguids;
                    }
                }
            }
        }
        /// <summary>
        /// MQA编辑页面查询DQA文件
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public DataTable GetDQAFile(string fjguid)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("DQAfilelistguid", fjguid);//art
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "GetDQAFile",//方法名
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
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        /// <summary>
        /// 查看文件集合
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public DataTable GetDQAFileList(string PROD_NO, string PO, string TYPE,string FileType)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("PROD_NO", PROD_NO);//art
                data.Add("PO", PO);//art
                data.Add("TYPE", TYPE);//art
                data.Add("FileType", FileType);//art
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetDQAFilelist",//方法名
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
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        /// <summary>
        /// 查询MQA文件
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public DataTable GetShoeShape_EditFile(string[] fjguid)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("fjguid", fjguid);//guid
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
                                            "GetShoeShape_EditFile",//方法名
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
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }
        //public void SetPanel()
        //{
        //    //dataGridView1.add
        //    this.panel1.Location = dataGridView1.Location;
        //    this.panel1.Height = dataGridView1.Height;
        //    this.panel1.Width = dataGridView1.Width;
        //    this.panel1.Visible = true;
        //}

        #region 日期控件初始为空值处理

        /// <summary>
        /// 初始化日期时间控件
        /// </summary>
        /// <param name="dtp"></param>
        public static void InitDateTimePicker(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = " ";  //必须设置成" "
            dtp.ValueChanged -= DateTimePicker_ValueChanged;
            dtp.ValueChanged += DateTimePicker_ValueChanged;
            dtp.KeyPress -= DateTimePicker_KeyPress;
            dtp.KeyPress += DateTimePicker_KeyPress;
        }

        public static void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd"; //null;
            dtp.Checked = false;// 解决BUG ：防止日期控件不能选择相同日期的 --- 要放置在设置格式之后
        }

        public static void DateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)  // backspace左删除键
            {
                DateTimePicker dtp = (DateTimePicker)sender;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
            }
        }
        #endregion

        /// <summary>
        /// 获取RQC数据
        /// </summary>
        public void GetRQCData()
        {
            Dictionary<string, Object> data = new Dictionary<string, object>();
            data.Add("PROD_NO", art_textBox.Text.Trim());
            data.Add("PO", txt_PO.Text.Trim());

            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_date.Text))
            {
                start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
            }
            data.Add("start_date", start_date);
            data.Add("end_date", end_date);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetRQCData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            int i = 0;
            DGV_RQC.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                DGV_RQC.Rows.Add();
                DataGridViewRow dgvr = DGV_RQC.Rows[i];
                dgvr.Cells["PO1"].Value = dr["mer_po"].ToString();
                dgvr.Cells["日期范围1"].Value = dr["createdate"].ToString();
                dgvr.Cells["工段1"].Value = dr["workshop_section_name"].ToString();
                dgvr.Cells["部门1"].Value = dr["department"].ToString();
                dgvr.Cells["组别1"].Value = dr["production_line_code"].ToString();
                dgvr.Cells["抽检数量1"].Value = dr["Inspection_quantity"].ToString();
                dgvr.Cells["合格数1"].Value = dr["Qualified_quantity"].ToString();
                dgvr.Cells["合格率1"].Value = dr["Pass_rate"].ToString();
                //dgvr.Cells["判定结果1"].Value = dr["critical result"].ToString();

                dgvr.Cells["RQC报告"].Value = dr["TASK_NO"].ToString();
                i++;
            }
        }

        #region TQC页签数据查询

        public void GetTQCData()
        {
            Dictionary<string, Object> data = new Dictionary<string, object>();
            data.Add("PROD_NO", art_textBox.Text.Trim());
            data.Add("PO", txt_PO.Text.Trim());

            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_date.Text))
            {
                start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
            }
            data.Add("start_date", start_date);
            data.Add("end_date", end_date);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetTQCData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            int i = 0;
            DGV_TQC.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                DGV_TQC.Rows.Add();
                DataGridViewRow dgvr = DGV_TQC.Rows[i];
                dgvr.Cells["TASK_NO2"].Value = dr["TASK_NO"].ToString();
                dgvr.Cells["TQC_PO"].Value = dr["PO"].ToString();
                dgvr.Cells["日期范围"].Value = dr["日期范围"].ToString();
                dgvr.Cells["首检合格总数"].Value = dr["首检合格总数"].ToString();
                dgvr.Cells["工段"].Value = dr["工段"].ToString();
                dgvr.Cells["部门"].Value = dr["部门"].ToString();
                dgvr.Cells["组别"].Value = dr["组别"].ToString();
                dgvr.Cells["检验数量"].Value = dr["检验总数"].ToString();
                dgvr.Cells["合格总数"].Value = dr["合格总数"].ToString();
                dgvr.Cells["B品数量"].Value = dr["B品数量"].ToString();
                dgvr.Cells["产线总合格率"].Value = dr["产线合格率"].ToString();
                dgvr.Cells["RFT"].Value = dr["RFT"].ToString();
                dgvr.Cells["TQC报告"].Value = "";//dr["TASK_NO"].ToString();
                i++;
            }
        }
        #endregion

        //市场反馈查询
        public void GetCustomerData()
        {
            try
            {
                //客户投诉
                GetCustomerComplaintData();

                //中国区退货
                GetCHNCustomerComplaintData();

                //客户退货
                GetReturnData();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }

        }
        //客户退货
        public void GetReturnData()
        {
            Dictionary<string, Object> data = new Dictionary<string, object>();
            data.Add("PROD_NO", art_textBox.Text.Trim());
            data.Add("PO_ORDER", txt_PO.Text.Trim());

            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_date.Text))
            {
                start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
            }
            data.Add("start_date", start_date);
            data.Add("end_date", end_date);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetCustomerReturnData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            int i = 0;
            khdgv.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                khdgv.Rows.Add();
                DataGridViewRow dgvr = khdgv.Rows[i];

                dgvr.Cells["退货月份2"].Value = dr["RETURNDATE"].ToString();
                dgvr.Cells["退货数2"].Value = dr["RETURN_QTY"].ToString();
                dgvr.Cells["退货金额2"].Value = dr["COMPENSATION_AMOUNT"].ToString();
                dgvr.Cells["出货数量2"].Value = dr["SHIPPING_QTY"].ToString();
                dgvr.Cells["THQTY1"].Value = dr["QTY1"].ToString();
                dgvr.Cells["THQTY2"].Value = dr["QTY2"].ToString();
                dgvr.Cells["THQTY3"].Value = dr["QTY3"].ToString();

                i++;
            }

            txt_returnmoney2.Text = dic["SUM_MONEY"].ToString();
            txt_returnqty2.Text = dic["SUM_RETURN"].ToString();
            txt_outqty2.Text = dic["SUM_OUTQTY"].ToString();
            txt_rate2.Text = dic["SUM_RATE"].ToString();
        }

        //客户投诉
        public void GetCustomerComplaintData()
        {
            Dictionary<string, Object> data = new Dictionary<string, object>();
            data.Add("PROD_NO", art_textBox.Text.Trim());
            data.Add("PO_ORDER", txt_PO.Text.Trim());

            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_date.Text))
            {
                start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
            }
            data.Add("start_date", start_date);
            data.Add("end_date", end_date);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetCustomerComplaintData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            int i = 0;
            tsdgv.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                tsdgv.Rows.Add();
                DataGridViewRow dgvr = tsdgv.Rows[i];
                dgvr.Cells["投诉编号"].Value = dr["COMPLAINT_NO"].ToString();
                dgvr.Cells["投诉日期"].Value = dr["COMPLAINT_DATE"].ToString();
                dgvr.Cells["投诉的国家区域"].Value = dr["COUNTRY_REGION"].ToString();
                dgvr.Cells["投诉的PO号"].Value = dr["PO_ORDER"].ToString();
                dgvr.Cells["投诉PO数量"].Value = dr["ts_posl"].ToString();
                dgvr.Cells["问题点"].Value = dr["DEFECT_CONTENT"].ToString();
                dgvr.Cells["不良数量"].Value = dr["NG_QTY"].ToString();
                dgvr.Cells["投诉金额"].Value = dr["COMPLAINT_MONEY"].ToString();

                if (dr["STATUS"].ToString() == "0")
                    dgvr.Cells["状态"].Value = "未结案";
                else if (dr["STATUS"].ToString() == "1")
                    dgvr.Cells["状态"].Value = "结案";
                if (dr["processing_results_status"].ToString() == "0")
                    dgvr.Cells["处理结果"].Value = "接收投诉";
                else if (dr["processing_results_status"].ToString() == "1")
                    dgvr.Cells["处理结果"].Value = "客户撤销投诉";
                else
                    dgvr.Cells["处理结果"].Value = "";

                i++;
            }
        }

        //中国区退货
        public void GetCHNCustomerComplaintData()
        {
            Dictionary<string, Object> data = new Dictionary<string, object>();
            data.Add("PROD_NO", art_textBox.Text.Trim());
            data.Add("PO_ORDER", txt_PO.Text.Trim());

            string start_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.start_date.Text))
            {
                start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.end_date.Text))
            {
                end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
            }
            data.Add("start_date", start_date);
            data.Add("end_date", end_date);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_KanBanAPI",//类库名
                                        "SJ_KanBanAPI.WholeLife",//类名
                                        "GetCNCustomerComplaintData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return;
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            int i = 0;
            zgqdgv.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                zgqdgv.Rows.Add();
                DataGridViewRow dgvr = zgqdgv.Rows[i];
                dgvr.Cells["PO号"].Value = dr["PO"].ToString();
                dgvr.Cells["退货月份"].Value = dr["RETURN_MONTH"].ToString();
                dgvr.Cells["退货数"].Value = dr["RETURN_QTY"].ToString();
                dgvr.Cells["退货金额"].Value = dr["COMPENSATION_AMOUNT"].ToString();
                dgvr.Cells["出货数量"].Value = dr["SHIPPING_QTY"].ToString();
                dgvr.Cells["QTY1"].Value = dr["QTY1"].ToString();
                dgvr.Cells["QTY2"].Value = dr["QTY2"].ToString();
                dgvr.Cells["QTY3"].Value = dr["QTY3"].ToString();

                i++;
            }
            txt_returnmoney.Text = dic["SUM_MONEY"].ToString();
            txt_returnqty.Text = dic["SUM_RETURN"].ToString();
            txt_outqty.Text = dic["SUM_OUTQTY"].ToString();
            txt_rate.Text = dic["SUM_RATE"].ToString();

        }

        //金属检测
        private void GetJSJCData()
        {
            try
            {
                DataTable dt = new DataTable();
                //点击查询 或 记录数据字典不存在该页签dt
                //if (type == 0 && !dts.ContainsKey(tabControl1.SelectedTab.Name))
                //{
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值  
                data.Add("PROD_NO", art_textBox.Text);//名称
                data.Add("PO_ORDER", txt_PO.Text);//PO

                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetJSJCtData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());


                jsjcdgv.Rows.Clear();
                if (dt.Rows.Count > 0)
                {

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        jsjcdgv.Rows.Add();
                        DataGridViewRow dgvr = jsjcdgv.Rows[i];

                        dgvr.Cells["PO2"].Value = dr["PO"].ToString();//
                        dgvr.Cells["PO数量"].Value = dr["SE_NUM"].ToString();//PO数量

                        dgvr.Cells["日期"].Value = dr["RIQI"].ToString();//
                        dgvr.Cells["MD检测数量"].Value = dr["QTY1"].ToString();
                        dgvr.Cells["MD通过数量"].Value = dr["QTY2"].ToString();//
                        dgvr.Cells["MD不通过数量"].Value = dr["QTY3"].ToString();//

                        dgvr.Cells["X光机复测数量"].Value = dr["XQTY1"].ToString();//
                        dgvr.Cells["X光机复测通过数量"].Value = dr["XQTY2"].ToString();//
                        dgvr.Cells["X光机复测不通过数量"].Value = dr["XQTY3"].ToString();//


                        i++;
                    }


                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);

            }


        }

        //订单信息
        private void GetDDData()
        {
            try
            {
                DataTable dt = new DataTable();
                //点击查询 或 记录数据字典不存在该页签dt
                //if (type == 0 && !dts.ContainsKey(tabControl1.SelectedTab.Name))
                //{
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值  
                data.Add("PROD_NO", art_textBox.Text);//名称
                data.Add("PO_ORDER", txt_PO.Text);//PO

                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetDDData",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                string firstPO = dic["PO"].ToString();


                dddgv.Rows.Clear();
                if (dt.Rows.Count > 0)
                {

                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dddgv.Rows.Add();
                        DataGridViewRow dgvr = dddgv.Rows[i];

                        dgvr.Cells["PO号2"].Value = dr["PO号"].ToString();//
                        dgvr.Cells["PODD"].Value = dr["PODD"].ToString();//
                        dgvr.Cells["首次出货日期"].Value = dr["首次出货日期"].ToString();//PO数量

                        dgvr.Cells["最后出货日期"].Value = dr["最后出货日期"].ToString();//
                        dgvr.Cells["客户编码"].Value = dr["客户编码"].ToString();
                        dgvr.Cells["出货国家"].Value = dr["出货国家"].ToString();//
                        dgvr.Cells["PO数量2"].Value = dr["PO数量"].ToString();//

                        dgvr.Cells["生产组别"].Value = dr["生产组别"].ToString();//

                        if (dr["PO号"].ToString() == firstPO)
                        {
                            dgvr.Cells["首次上线PO"].Value = "是";
                        }
                        //= dr["首次上线PO"].ToString();//


                        i++;
                    }


                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);

            }


        }

        /// <summary>
        /// 实验室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sysdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            string TASK_NO = sysdgv.Rows[e.RowIndex].Cells["TASK_NO"].Value.ToString();

            if (sysdgv.Columns[e.ColumnIndex].Name == "TASK_NO")
            {
                F_QCM_Ex_LookResult_New frm = new F_QCM_Ex_LookResult_New(TASK_NO, Program.Client);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }

        }


        private void IQCdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                //外观检测清单
                if (IQCdgv.Columns[e.ColumnIndex].Name == "result_order")
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("RCPT_DATE", IQCdgv.CurrentRow.Cells["RCPT_DATE"].Value.ToString());//进仓日期
                    dic.Add("SUPPLIERS_NAME", IQCdgv.CurrentRow.Cells["SUPPLIERS_NAME"].Value.ToString());//生产厂商
                    dic.Add("RCPT_QTY", IQCdgv.CurrentRow.Cells["RCPT_QTY"].Value.ToString());//收料数量
                    dic.Add("SHOE_NO", IQCdgv.CurrentRow.Cells["SHOE_NO"].Value.ToString());//鞋型
                    dic.Add("PROD_NO", IQCdgv.CurrentRow.Cells["PROD_NO"].Value.ToString());//ART
                    dic.Add("ITEM_NAME", IQCdgv.CurrentRow.Cells["NAME_T"].Value.ToString());//材料品名
                    dic.Add("ITEM_TYPE_NO", IQCdgv.CurrentRow.Cells["ITEM_TYPE_NO"].Value.ToString());//材料类型
                    dic.Add("ORDER_NO", IQCdgv.CurrentRow.Cells["ORDER_NO"].Value.ToString());//采购单号
                    dic.Add("CHK_NO", IQCdgv.CurrentRow.Cells["CHK_NO"].Value.ToString());//收料单号
                    dic.Add("ITEM_NO", IQCdgv.CurrentRow.Cells["ITEM_NO"].Value.ToString());//料号
                    dic.Add("CHK_SEQ", IQCdgv.CurrentRow.Cells["CHK_SEQ"].Value.ToString());//材料序号
                    dic.Add("PART", IQCdgv.CurrentRow.Cells["PART_NO"].Value.ToString());//部位ITEM_NAME

                    using (F_IQC_Viewinspectionresults_view aa = new F_IQC_Viewinspectionresults_view(dic, Program.Client))
                    {
                        //查看检验结果
                        aa.ShowDialog();
                        //FormLoad();
                    }


                }
                //实验室测试检测报告
                if (IQCdgv.Columns[e.ColumnIndex].Name == "sysbg")
                {

                    string task_no = IQCdgv.CurrentRow.Cells["sysbg"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(task_no))
                    {
                        using (F_QCM_Ex_LookResult_New aa = new F_QCM_Ex_LookResult_New(task_no, Program.Client))
                        {
                            //实验室结果(测检报告)
                            aa.ShowDialog();
                            //FormLoad();
                        }
                    }
                    else
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("没有实验室任务编号，请检查！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// TQC附件 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGV_TQC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (DGV_TQC.Columns[e.ColumnIndex].Name == "TQC报告")
            {
                string DQAfjguid = DGV_TQC.Rows[e.RowIndex].Cells["TQC报告"].Value.ToString();
                FrmFileList add = new FrmFileList(GetDQAFile(DQAfjguid), Program.Client.UploadUrl, Program.Client.UserToken, "", true, false);
                add.Show();
            }
            else if (DGV_TQC.Columns[e.ColumnIndex].Name == "TASK_NO2")
            {
                string task_no = DGV_TQC.Rows[e.RowIndex].Cells["TASK_NO2"].Value.ToString();
                List<string> list = new List<string>();
                var arr = task_no.Split(',');
                string WhereStr = string.Empty;
                if (arr.Length > 0)
                {
                    string Str = string.Empty;
                    foreach (var item in arr)
                    {
                        Str += "'" + item + "'" + ",";
                    }
                    WhereStr = $"and task_no in({Str.TrimEnd(',')})";
                }

                string sql = $@"SELECT task_no as 任务编号 from TQC_TASK_M where 1=1  {WhereStr} ";

                FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                frmData.ShowDialog();
                if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                {
                    task_no = frmData.RetData.Rows[0]["任务编号"].ToString();
                    TQC_Task_Edit FrmTQC = new TQC_Task_Edit(task_no, Program.Client);
                    FrmTQC.Show();
                }

            }
        }
        /// <summary>
        /// 合规性文件查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGV_Compliance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (DGV_Compliance.Columns[e.ColumnIndex].Name == "查看文件")
            {
                string file_type = DGV_Compliance.Rows[e.RowIndex].Cells["查看文件类型代号"].Value.ToString();
                string PO = DGV_Compliance.Rows[e.RowIndex].Cells["PO_HG"].Value.ToString();
                string PROD_NO = art_textBox.Text;


                //string DQAfjguid = DGV_Compliance.Rows[e.RowIndex].Cells["查看文件"].Value.ToString();
                FrmFileList add = new FrmFileList(GetDQAFileList(PROD_NO, PO, file_type,"0"), Program.Client.UploadUrl, Program.Client.UserToken, "", false, false);
                add.Show();
            }
        }

        private void DGV_Jointly_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (DGV_Jointly.Columns[e.ColumnIndex].Name == "查看文件联名")
            {
                string file_type = DGV_Jointly.Rows[e.RowIndex].Cells["文件类型代号联名"].Value.ToString();
                string PO = DGV_Jointly.Rows[e.RowIndex].Cells["PO_LM"].Value.ToString();
                string PROD_NO = art_textBox.Text;


                //string DQAfjguid = DGV_Compliance.Rows[e.RowIndex].Cells["查看文件"].Value.ToString();
                FrmFileList add = new FrmFileList(GetDQAFileList(PROD_NO, PO, file_type,"1"), Program.Client.UploadUrl, Program.Client.UserToken, "", false, false);
                add.Show();
            }
        }
        private void DGV_AQL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (DGV_AQL.Columns[e.ColumnIndex].Name == "验货报告")
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("task_no", DGV_AQL.Rows[e.RowIndex].Cells["验货报告"].Value.ToString());
                dic.Add("po", DGV_AQL.Rows[e.RowIndex].Cells["PO_AQL"].Value.ToString());
                dic.Add("num", DGV_AQL.Rows[e.RowIndex].Cells["PO数量3"].Value.ToString());
                dic.Add("fpnum", DGV_AQL.Rows[e.RowIndex].Cells["验货数量"].Value.ToString());
                dic.Add("yhstatus", DGV_AQL.Rows[e.RowIndex].Cells["验货状态AQL"].Value.ToString());

                string frmName = $@"F_AQL_Aqlreport_New_{DGV_AQL.Rows[e.RowIndex].Cells["验货报告"].Value}";
                var findFrm = Application.OpenForms[frmName];
                if (findFrm == null)
                {
                    F_AQL_Aqlreport_New a = new F_AQL_Aqlreport_New(dic, Program.Client);
                    a.Name = frmName;
                    a.Show();
                }
                else
                {
                    findFrm.Activate();
                }
                //using (F_AQL_Aqlreport a = new F_AQL_Aqlreport(dic, Program.Client))
                //{
                //    a.ShowDialog();
                //}
            }
        }

        /// <summary>
        /// Fit Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sc_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //URLL2
            if (e.RowIndex < 0)
            {
                return;
            }
            if (sc_dgv.Columns[e.ColumnIndex].Name == "URL")
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string TESTID = sc_dgv.Rows[e.RowIndex].Cells["TESTID"].Value.ToString();

                if (!string.IsNullOrEmpty(TESTID))
                {
                    configurl = Common.ConfigHelper.GetConfigUrl();
                    string url = $@"{configurl}/fitReportSee?{TESTID}";

                    FrmReport frmReport = new FrmReport(url);
                    frmReport.Show();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("查无报告！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }


            }

        }

        /// <summary>
        /// Wear Test 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sc_dgv2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //URLL2

            if (e.RowIndex < 0)
            {
                return;
            }
            if (sc_dgv2.Columns[e.ColumnIndex].Name == "URL2")
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string TESTID = sc_dgv2.Rows[e.RowIndex].Cells["TESTID2"].Value.ToString();
                string USERCODE = sc_dgv2.Rows[e.RowIndex].Cells["试穿员代号2"].Value.ToString();


                data.Add("TESTID", TESTID);
                data.Add("USERCODE", USERCODE);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_KanBanAPI",//类库名
                                            "SJ_KanBanAPI.WholeLife",//类名
                                            "GetWearReportListAPI",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ret.ErrMsg, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                    string dtjson = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

                    configurl = Common.ConfigHelper.GetConfigUrl();

                    string url = $@"{configurl}/wearReportSee?{dtjson}";
                    FrmReport frmReport = new FrmReport(url);
                    frmReport.Show();
                }

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void art_textBox_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;

            sql = $@"SELECT PROD_NO as ART FROM bdm_rd_prod ";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                art_textBox.Text = frmData.RetData.Rows[0]["ART"].ToString();
                txt_PO.Text = "";
            }

        }

        private void txt_PO_Click(object sender, EventArgs e)
        {
            using (FrmSelectPO t = new FrmSelectPO(this, art_textBox.Text))
            {
                t.ShowDialog();
            }
        }


        //A-01报告
        private void DGV_A01_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (DGV_A01.Columns[e.ColumnIndex].Name == "查看文件A01")
            {
                string GUID = DGV_A01.Rows[e.RowIndex].Cells["文件信息"].Value.ToString();
                //string PO = DGV_Jointly.Rows[e.RowIndex].Cells["PO_LM"].Value.ToString();
                //string PROD_NO = art_textBox.Text;


                //string DQAfjguid = DGV_Compliance.Rows[e.RowIndex].Cells["查看文件"].Value.ToString();
                FrmFileList add = new FrmFileList(GetDQAFile(GUID), Program.Client.UploadUrl, Program.Client.UserToken, "", true, false);
                add.Show();
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            //tag = this.tabControl1.SelectedTab.Tag.ToString();
            tagName = this.tabControl1.SelectedTab.Name.ToString();
            //基本信息
            //GetHeadBaseInfo();

            //页签异步获取数据
            //LoadTabpageData();

            //页签数据初始化
            TagDataLoad(tagName, 0);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void khdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //RQC报告
        private void DGV_RQC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //RQC报告
            if (e.RowIndex < 0)
            {
                return;
            }
            if (DGV_RQC.Columns[e.ColumnIndex].Name == "RQC报告")
            {
                string TASKNO = DGV_RQC.Rows[e.RowIndex].Cells["RQC报告"].Value.ToString();

                string url = $@"{configurl}/rQCDetail?{TASKNO}&{Program.Client.CompanyCode}?en";

                if (!string.IsNullOrEmpty(TASKNO))
                {
                    FrmReport frmReport = new FrmReport(url);
                    frmReport.Show();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("No task number found, failed to open！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }

            }
        }

        //量试
        private void LSdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex < 0)
            {
                return;
            }
            if (LSdgv.Columns[e.ColumnIndex].Name == "量试报告2")
            {
                string art = LSdgv.Rows[e.RowIndex].Cells["ART"].Value.ToString();
                string shoes_code = LSdgv.Rows[e.RowIndex].Cells["鞋型"].Value.ToString();
                string workshop_section_no = LSdgv.Rows[e.RowIndex].Cells["工段代号"].Value.ToString();
                var res = new
                {
                    art = art,
                    shoes_code = shoes_code,
                    workshop_section_no = workshop_section_no,
                    CompanyCode = Program.Client.CompanyCode,
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(res);
                string url = $"{configurl}/batchTryDetail?{json}";

                FrmReport frmReport = new FrmReport(url);
                frmReport.Show();
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
