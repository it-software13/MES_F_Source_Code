using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using SJeMES_QA.FileSForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QA
{
    public partial class F_MQA_ShoeShape_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string shoe_no = string.Empty;
        string user_fdd = string.Empty;

        public bool isEdit = false;

        List<Dictionary<string, object>> dic = new List<Dictionary<string, object>>();
        Dictionary<string, object> workshop_sectionDic = new Dictionary<string, object>();
        List<string> list = new List<string>();
        public F_MQA_ShoeShape_Edit()
        {
            InitializeComponent();
            ClearTest();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_MQA_ShoeShape_Edit(string _shoe_no,string _user_fdd)
        {
            shoe_no = _shoe_no;
            user_fdd = _user_fdd;
            InitializeComponent();
            ClearTest();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void ClearTest()
        {
            label12.Text = "";
            label17.Text = "";
            label21.Text = "";
            label13.Text = "";
            label19.Text = "";
            label16.Text = "";
            label15.Text = "";
            label14.Text = "";
            label18.Text = "";
        }


        /// <summary>
        /// 点击 添加问题点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (tab_type_standard.TabPages.Count > 0)
            {
                isEdit = true;
                button2.Visible = true;
                btn_cencle.Visible = true;

                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["did"].Value = "";
                this.dataGridView1.Rows[index].Cells["problemsources"].Value = "MQA";
                this.dataGridView1.Rows[index].Cells["workshop_section_no"].Value = "";
                this.dataGridView1.Rows[index].Cells["workshop_section_name"].Value = "";
                //this.dataGridView1.Rows[index].Cells["data_source"].Value = "";
                this.dataGridView1.Rows[index].Cells["image_guid"].Value = "";
                this.dataGridView1.Rows[index].Cells["art_code"].Value = "";
                this.dataGridView1.Rows[index].Cells["choice_no"].Value = "";
                this.dataGridView1.Rows[index].Cells["choice_name"].Value = "";
                this.dataGridView1.Rows[index].Cells["qa_risk_desc"].Value = "";
                this.dataGridView1.Rows[index].Cells["inspection_code"].Value = "";
                this.dataGridView1.Rows[index].Cells["inspection_name"].Value = ""; 
                this.dataGridView1.Rows[index].Cells["inspection_type"].Value = "";
                this.dataGridView1.Rows[index].Cells["judge_mode"].Value = "";
                this.dataGridView1.Rows[index].Cells["JUDGMENT_CRITERIA"].Value = "";
                this.dataGridView1.Rows[index].Cells["standard_value"].Value = "";
                this.dataGridView1.Rows[index].Cells["unit"].Value = "";
                this.dataGridView1.Rows[index].Cells["other_measures"].Value = "";
                this.dataGridView1.Rows[index].Cells["remark"].Value = "";
                this.dataGridView1.Rows[index].Cells["DQAfilelist"].Value = "";
                this.dataGridView1.Rows[index].Cells["dep_attr"].Value = "";
                this.dataGridView1.Rows[index].Cells["f_insp_dep"].Value = "";
                this.dataGridView1.Rows[index].Cells["f_insp_date"].Value = "";
                this.dataGridView1.Rows[index].Cells["f_insp_res"].Value = "";
                this.dataGridView1.Rows[index].Cells["processing_record"].Value = "";
                this.dataGridView1.Rows[index].Cells["MQAfilelist"].Value = "";
                this.dataGridView1.Rows[index].Cells["DQAfile"] = new DataGridViewOperationCell();

                if (dataGridView1.Rows.Count > 0)
                {
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Index;
                }
            }
            else
            {
                MessageBox.Show("No section!!!");
            }
        }

        private void F_MQA_ShoeShape_Edit_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetShoeShape_EditTH(shoe_no);
            GetShoeShape_EditTab();

            pageControl1.BindPageEvent += GetShoeShape_Edit;
            LoadPage();
            this.dataGridView1.ClearSelection();
        }

        /// <summary>
        /// 跳转MQA管理时查询表头
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetShoeShape_EditTH(string shoe_no)
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
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "GetShoeShape_EditTH",//方法名
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
                    //checkedListBox1.Text = dt.Rows[0]["PROD_NO"].ToString();
                    label12.Text = dt.Rows[0]["name_t"].ToString();
                    label13.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();


                    label15.Text = dt.Rows[0]["rule_no"].ToString();
                    label17.Text = dt.Rows[0]["TEST_LEVEL"].ToString();
                    label19.Text = dt.Rows[0]["develop_type"].ToString();
                    label14.Text = dt.Rows[0]["user_section"].ToString();
                    label21.Text = dt.Rows[0]["COL1"].ToString();
                    label16.Text = user_fdd;
                    label18.Text = dt.Rows[0]["user_technical"].ToString();

                    textBox7.Text = dt.Rows[0]["qa_principal"].ToString();

                    if (!string.IsNullOrEmpty(dt.Rows[0]["file_url"].ToString()))
                    {
                        try
                        {
                            var webC = new System.Net.WebClient();
                            string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["file_url"].ToString());
                            Image image = new Bitmap(webC.OpenRead(url));
                            pictureBox1.Image = image;
                            //image.SizeMode = PictureBoxSizeMode.StretchImage;
                        }catch{}
                    }
                    else
                    {
                        pictureBox1.Image = null;
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
        /// MQA鞋型管理编辑页面查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetShoeShape_Edit(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
               
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("shoe_no", shoe_no);//鞋型
                data.Add("workshop_section_no", tabid);//工段
                List<string> art = new List<string>();
                foreach (System.String item in this.checkedListBox1.CheckedItems)
                {
                    art.Add(item);
                }
                data.Add("arts", art);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "GetShoeShape_Edit",//方法名
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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["did"].Value = dr["did"].ToString();

                       
                        dgvr.Cells["problemsources"].Value = dr["problemsources"].ToString();
                        dgvr.Cells["workshop_section_no"].Value = dr["workshop_section_no"].ToString();
                        dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                        dgvr.Cells["image_guid"].Value = dr["image_guid"].ToString();

                        if (!string.IsNullOrEmpty(dr["image_guid"].ToString()))
                        {
                            try
                            {
                                List<string> imgsList = dr["image_guid"].ToString().Split(',').ToList();
                                foreach (var imgInfo in imgsList)
                                {
                                    List<string> imgInfoArr = imgInfo.Split(':').ToList();
                                    if (imgInfoArr[1] == "1")
                                    {//是主图

                                        var webC = new System.Net.WebClient();
                                        string url = Program.Client.PicUrl + imgInfoArr[2];
                                        Image image = new Bitmap(webC.OpenRead(url));
                                        dgvr.Cells["tp"].Value = image;
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            dgvr.Cells["tp"].Value = null;
                        }

                        dgvr.Cells["art_code"].Value = dr["art_code"].ToString();
                        //var webC = new System.Net.WebClient();
                        //string url = Program.Client.PicUrl + Convert.ToString(dr["FILE_URL"].ToString());
                        //try
                        //{
                        //    Image image = new Bitmap(webC.OpenRead(url));
                        //    dgvr.Cells["tp"].Value = image;
                        //}
                        //catch (Exception)
                        //{  }
                         
                        dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();
                        dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();
                        dgvr.Cells["qa_risk_desc"].Value = dr["qa_risk_desc"].ToString();
                        dgvr.Cells["inspection_code"].Value = dr["inspection_code"].ToString();
                        dgvr.Cells["inspection_name"].Value = dr["inspection_name"].ToString();
                        dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString(); 
                        dgvr.Cells["judge_mode"].Value = dr["judge_mode"].ToString();
                        dgvr.Cells["JUDGMENT_CRITERIA"].Value= dr["JUDGMENT_CRITERIA"].ToString();
                        dgvr.Cells["standard_value"].Value = dr["standard_value"].ToString();
                        dgvr.Cells["unit"].Value = dr["unit"].ToString();
                        dgvr.Cells["other_measures"].Value = dr["other_measures"].ToString();
                        dgvr.Cells["remark"].Value = dr["remark"].ToString();
                        dgvr.Cells["DQAfilelist"].Value = dr["DQAfilelist"].ToString(); 
                        dgvr.Cells["processing_record"].Value = dr["processing_record"].ToString();

                        dgvr.Cells["dep_attr"].Value = dr["dep_attr"].ToString();
                        dgvr.Cells["dep_attr_name"].Value = dr["dep_attr_name"].ToString();
                        dgvr.Cells["f_insp_dep"].Value = dr["f_insp_dep"].ToString();
                        dgvr.Cells["f_insp_date"].Value = dr["f_insp_date"].ToString();
                        dgvr.Cells["f_insp_res"].Value = dr["f_insp_res"].ToString();

                        dgvr.Cells["MQAfilelist"].Value = dr["MQAfilelist"].ToString();
                        dgvr.Cells["qa_risk_details_desc"].Value = dr["QA_RISK_DETAILS_DESC"].ToString();
                        dgvr.Cells["qa_risk_category_code"].Value = dr["qa_risk_category_code"].ToString();
                        dgvr.Cells["qa_risk_category_name"].Value = dr["qa_risk_category_name"].ToString();


                        if (dr["problemsources"].ToString() != "MQA")
                        {
                            var currOp = (DataGridViewOperationItems)dataGridView1.Rows[i].Cells["operation"].Value;
                            currOp.RemoveAt(0);
                            currOp.RemoveAt(0);

                        }
                        else
                        {
                            this.dataGridView1.Rows[i].Cells["DQAfile"] = new DataGridViewOperationCell();
                        }
                        //this.dataGridView1.Rows[i].Cells["MQAfile"] = new DataGridViewOperationCell();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["DQAfile"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["MQAfile"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //委托查询
        public void LoadPage()
        { 
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        //页签查询
        public string tabid = string.Empty;//页签id
        public void GetShoeShape_EditTab()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("SHOE_NO", shoe_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "GetShoeShape_EditTab",//方法名
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
                //workshop_sectiondt = new DataTable();
                //workshop_sectiondt.Columns.Add("workshop_section_no", typeof(string));
                //workshop_sectiondt.Columns.Add("workshop_section_name", typeof(string));

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                tab_type_standard.TabPages.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    TabPage tabPage11 = new TabPage();
                    tabPage11.Name = "QX";
                    this.tab_type_standard.TabPages.Add(tabPage11);
                    tabPage11.Text = "All";//全选
                    tabPage11.Tag = "qx";
                    tabid = "qx";
                    foreach (DataRow item in dt.Rows)
                    {
                        TabPage tabPage = new TabPage();
                        tabPage.Name = "tabPage" + i;
                        this.tab_type_standard.TabPages.Add(tabPage);
                        tabPage.Text = item["workshop_section_name"].ToString();
                        tabPage.Tag = item["workshop_section_no"].ToString();
                        list.Add(item["workshop_section_no"].ToString());
                        this.dic.Add(workshop_sectionDic);
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

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void tab_type_standard_Click(object sender, EventArgs e)
        {
            int index = this.tab_type_standard.SelectedIndex;
            tabid = this.tab_type_standard.TabPages[index].Tag.ToString();
            LoadPage();
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
                data.Add("DQAfilelistguid", fjguid);//guid
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                MessageBox.Show("It is being edited, please save the process first!");
                return;
            }
            this.Close();
        }

        /// <summary>
        /// MQA编辑页面查询工段
        /// </summary>
        /// <returns></returns>
        public DataTable Getworkshop_section()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("data", list);//界面页签工段
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.MQA_ShoeShape",//类名
                                        "Getworkshop_section",//方法名
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
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
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

        /// <summary>
        /// 获取检测项数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string _inspection_code = string.Empty;//检测项编号
        string _inspection_name = string.Empty;
        string _judgment_criteria = string.Empty;//判断标准 枚举
        string _judgment_criteria_name = string.Empty;
        string _inspection_type = string.Empty;//检测项目类型
        public void Edit_inspection(string inspection_code = "",string inspection_name="", string judgment_criteria = "",string inspection_type = "",string judgment_criteria_name="")
        {
            _inspection_code = inspection_code;
            _inspection_name = inspection_name;
            _judgment_criteria = judgment_criteria;
            _judgment_criteria_name = judgment_criteria_name;
            _inspection_type = inspection_type;
        }

        /// <summary>
        /// MQA编辑页面查询材料工序数据源
        /// </summary>
        /// <returns></returns>
        public string Getdqa_mag_mid(string workshop_section_no, string workshop_section_name)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("workshop_section_no", workshop_section_no);
            data.Add("workshop_section_name", workshop_section_name);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.MQA_ShoeShape",//类名
                                        "Getdqa_mag_mid",//方法名
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
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            return dt.Rows[0]["id"].ToString();
        }

        /// <summary>
        /// MQA管理页面删除
        /// </summary>
        public void Deletemqa_mag_d(string did)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("did", did);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.MQA_ShoeShape", "Deletemqa_mag_d", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Successfully Deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
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

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        int RowIndex = -1;
        int ColumnIndex = -1;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                string problemsources = dataGridView1.Rows[e.RowIndex].Cells["problemsources"].Value.ToString();

                //判断是否是编辑状态
                if (isEdit)
                { 
                    #region MQA

                    if (problemsources == "MQA")
                    {
                        List<string> filterColList = new List<string>()
                        {
                            "standard_value",
                            "unit",
                            "other_measures",
                            "qa_risk_desc",
                            "processing_record",
                            "remark"
                        };
                        if (string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString()))
                        {
                            filterColList.Add("workshop_section_name");
                        }
                        if (filterColList.Contains(dataGridView1.Columns[e.ColumnIndex].Name))
                        {
                            RowIndex = e.RowIndex;
                            ColumnIndex = e.ColumnIndex;
                        }
                        //工段
                        if (string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString()) && dataGridView1.Columns[e.ColumnIndex].Name == "workshop_section_name")
                        {
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            comboBox2.Visible = false;
                            DataTable dt_tval = Getworkshop_section();


                            comboBox1.DataSource = dt_tval;
                            if (dt_tval != null && dt_tval.Rows.Count > 0)
                            {
                                comboBox1.DisplayMember = "workshop_section_name";
                                comboBox1.ValueMember = "workshop_section_no";
                            }
                            string workshop_section_name = dataGridView1.CurrentRow.Cells["workshop_section_name"].Value.ToString(); //对combobox赋值
                            comboBox1.Text = workshop_section_name;

                            Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                            comboBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            comboBox1.Visible = true;
                        }
                        //操作
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                            if (cell.CurrentItem == null)
                            {
                                return;
                            }
                            if (cell.CurrentItem.Equals("DELETE"))
                            {
                                string did = dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString();
                                if (!string.IsNullOrEmpty(did))
                                {
                                    DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Delete MQA", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (dr == DialogResult.OK)
                                    {
                                        Deletemqa_mag_d(did);
                                    }
                                }
                                else
                                {
                                    DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Delete MQA", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (dr == DialogResult.OK)
                                    {
                                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Successfully Deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                                    }
                                }
                            }
                            else if (cell.CurrentItem.Equals("UPLOAD"))
                            {
                                //string did = dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString();
                                //if (!string.IsNullOrEmpty(did))
                                //{
                                //    MessageBox.Show("不能上传!");
                                //    return;
                                //}
                                //创建文件弹出选择窗口（包括文件名）对象
                                OpenFileDialog ofd = new OpenFileDialog();
                                //判断选择的路径
                                string path = string.Empty;
                                ofd.Title = "Please select a folder";
                                ofd.Filter = "Image File(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
                                if (ofd.ShowDialog() == DialogResult.OK)
                                {
                                    SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                                    filePath = ofd.FileName;
                                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                                    if (res.IsSuccess)
                                    {
                                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                        List<string> image_guid_list = dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString().Split(',').ToList().Where(x => !string.IsNullOrEmpty(x)).ToList();


                                        if (image_guid_list.Count() == 0)
                                        {
                                            try
                                            {
                                                image_guid_list.Add($@"{resultDIC["guid"]}:1:{resultDIC["url"]}");
                                                dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = string.Join(",", image_guid_list);
                                                var webC = new System.Net.WebClient();
                                                string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                                                Image image = new Bitmap(webC.OpenRead(url));
                                                dataGridView1.Rows[e.RowIndex].Cells["tp"].Value = image;
                                            }
                                            catch
                                            {
                                            }
                                        }
                                        else
                                        {
                                            image_guid_list.Add($@"{resultDIC["guid"]}:0:{resultDIC["url"]}");
                                            dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = string.Join(",", image_guid_list);
                                        }

                                        //var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                        //dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = resultDIC["guid"].ToString();
                                        //var webC = new System.Net.WebClient();
                                        //string url = Program.Client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                                        //Image image = new Bitmap(webC.OpenRead(url));
                                        //dataGridView1.Rows[e.RowIndex].Cells["tp"].Value = image;
                                        MessageBox.Show("Uploaded Successfully");
                                    }
                                    else
                                    {

                                        MessageBox.Show("Upload Failed！");
                                    }
                                }
                            }
                            else if (cell.CurrentItem.Equals("EDITIMG"))
                            {
                                List<string> image_guid_res = new List<string>();
                                image_guid_res.Add(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString());
                                //string image_guid_res = dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString();
                                FrmQaImgSetting frmQaImgSetting = new FrmQaImgSetting(image_guid_res);
                                frmQaImgSetting.ShowDialog();
                                dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = image_guid_res[0];

                                if (!string.IsNullOrEmpty(image_guid_res[0]))
                                {
                                    foreach (var item in image_guid_res[0].Split(','))
                                    {
                                        var info_arr = item.Split(':');
                                        if (info_arr[1].ToString() == "1")
                                        {
                                            try
                                            {
                                                var webC = new System.Net.WebClient();
                                                string url = Program.Client.PicUrl + info_arr[2].ToString();
                                                Image image = new Bitmap(webC.OpenRead(url));
                                                dataGridView1.Rows[e.RowIndex].Cells["tp"].Value = image;
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["tp"].Value = null;
                                }

                            }
                        }
                        //ART
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "art_code")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            F_DQA_ShoeShape_trait_Insert_ART update = new F_DQA_ShoeShape_trait_Insert_ART(this.shoe_no, dataGridView1.Rows[e.RowIndex].Cells["art_code"].Value.ToString(),false);
                            update.ShowDialog();
                            if (update.Tag != null)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells["art_code"].Value = update.Tag.ToString();
                            }
                        }
                        //材料料号/工序代码
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "choice_no")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            string workshop_section_no = dataGridView1.Rows[e.RowIndex].Cells["workshop_section_no"].Value.ToString();
                            string workshop_section_name = dataGridView1.Rows[e.RowIndex].Cells["workshop_section_name"].Value.ToString();
                            if (string.IsNullOrEmpty(workshop_section_no))
                            {
                                MessageBox.Show("Please select Process!");
                                return;
                            }

                            string art_no_list = dataGridView1.Rows[e.RowIndex].Cells["art_code"].Value.ToString();
                            F_DQA_ShoeShape_trait_Insert_material update = new F_DQA_ShoeShape_trait_Insert_material(Getdqa_mag_mid(workshop_section_no, workshop_section_name), art_no_list);
                            update.ShowDialog();
                            if (update.Tag != null)
                            {
                                string[] choice = update.Tag.ToString().Split(',');
                                dataGridView1.Rows[e.RowIndex].Cells["choice_no"].Value = choice[0];
                                dataGridView1.Rows[e.RowIndex].Cells["choice_name"].Value = choice[1];
                            }
                        }
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_details_desc")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;

                            string qa_risk_details_desc = dataGridView1.Rows[e.RowIndex].Cells["qa_risk_details_desc"].Value == null ? "" : dataGridView1.Rows[e.RowIndex].Cells["qa_risk_details_desc"].Value.ToString();
                            QA_RISK_DETAILS frm = new QA_RISK_DETAILS(qa_risk_details_desc);
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.ShowDialog();
                            if (frm.selectlist.Count > 0)
                            {
                                string poorder = "";
                                foreach (var item in frm.selectlist)
                                {
                                    poorder += item["poorder"].ToString() + ",";
                                }
                                dataGridView1.Rows[e.RowIndex].Cells["qa_risk_details_desc"].Value = poorder.Trim(',');
                            }
                        }

                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_category_name") // combobox显示条件 
                        {
                            textBox2.Visible = false;
                            textBox1.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox5.Visible = false;

                            textBox6.Visible = false;

                            DataTable dt_tval = Getrisk_category();
                            comboBox2.DataSource = dt_tval;
                            if (dt_tval != null && dt_tval.Rows.Count > 0)
                            {
                                comboBox2.DisplayMember = "qa_risk_category_name";
                                comboBox2.ValueMember = "qa_risk_category_code";
                            }
                            string qa_risk_category_name = dataGridView1.CurrentRow.Cells["qa_risk_category_name"].Value == null ? "" : dataGridView1.CurrentRow.Cells["qa_risk_category_name"].Value.ToString(); //对combobox赋值
                            comboBox2.Text = qa_risk_category_name;

                            Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                            comboBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            comboBox2.Visible = true;
                        }
                        //品质风险描述
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_desc")
                        {
                            comboBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            string aa = dataGridView1.CurrentRow.Cells["qa_risk_desc"].Value is null ? "" : dataGridView1.CurrentRow.Cells["qa_risk_desc"].Value.ToString();
                            string qa_risk_desc = aa == "" ? "" : aa;
                            textBox1.Text = qa_risk_desc; //判断值

                            Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                            textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            textBox1.Visible = true;
                        }
                        //检验项目/品质风险
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "inspection_name")
                        {
                            string workshop_section_no = dataGridView1.Rows[e.RowIndex].Cells["workshop_section_no"].Value.ToString();
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            F_MQA_ShoeShape_Edit_inspection update = new F_MQA_ShoeShape_Edit_inspection(tabid, this, workshop_section_no);
                            update.ShowDialog();
                            dataGridView1.Rows[e.RowIndex].Cells["inspection_code"].Value = _inspection_code;
                            dataGridView1.Rows[e.RowIndex].Cells["inspection_name"].Value = _inspection_name;
                            dataGridView1.Rows[e.RowIndex].Cells["judge_mode"].Value = _judgment_criteria_name;
                            dataGridView1.Rows[e.RowIndex].Cells["JUDGMENT_CRITERIA"].Value = _judgment_criteria;
                            dataGridView1.Rows[e.RowIndex].Cells["inspection_type"].Value = _inspection_type;
                        }
                        //标准值
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "standard_value")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            string aa = dataGridView1.CurrentRow.Cells["standard_value"].Value is null ? "" : dataGridView1.CurrentRow.Cells["standard_value"].Value.ToString();
                            string standard_value = aa == "" ? "" : aa;
                            textBox2.Text = standard_value; //判断值

                            Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                            textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            textBox2.Visible = true;
                        }
                        //单位
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "unit")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            string aa = dataGridView1.CurrentRow.Cells["unit"].Value is null ? "" : dataGridView1.CurrentRow.Cells["unit"].Value.ToString();
                            string unit = aa == "" ? "" : aa;
                            textBox3.Text = unit; //判断值

                            Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                            textBox3.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            textBox3.Visible = true;
                        }
                        //其他措施
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "other_measures")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            string aa = dataGridView1.CurrentRow.Cells["other_measures"].Value is null ? "" : dataGridView1.CurrentRow.Cells["other_measures"].Value.ToString();
                            string other_measures = aa == "" ? "" : aa;
                            textBox4.Text = other_measures; //判断值

                            Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                            textBox4.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            textBox4.Visible = true;
                        }
                        //备注
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "remark")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox6.Visible = false;
                            string aa = dataGridView1.CurrentRow.Cells["remark"].Value is null ? "" : dataGridView1.CurrentRow.Cells["remark"].Value.ToString();
                            string remark = aa == "" ? "" : aa;
                            textBox5.Text = remark; //判断值

                            Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                            textBox5.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                            textBox5.Visible = true;
                        }
                        //部门属性
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "dep_attr_name")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            F_MQA_ShoeShape_Edit_dep_attr update = new F_MQA_ShoeShape_Edit_dep_attr();
                            update.ShowDialog();
                            if (update.Tag != null)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells["dep_attr"].Value = update.Tag.ToString();
                                dataGridView1.Rows[e.RowIndex].Cells["dep_attr_name"].Value = update.Text.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                        {
                            comboBox1.Visible = false;
                            textBox1.Visible = false;
                            textBox2.Visible = false;
                            textBox3.Visible = false;
                            comboBox2.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                            if (cell.CurrentItem == null)
                            {
                                return;
                            }
                            if (cell.CurrentItem.Equals("EDITIMG"))
                            {
                                List<string> image_guid_res = new List<string>();
                                image_guid_res.Add(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString());
                                //string image_guid_res = dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString();
                                FrmQaImgSetting frmQaImgSetting = new FrmQaImgSetting(image_guid_res, false);
                                frmQaImgSetting.ShowDialog();
                                dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = image_guid_res[0];

                                if (!string.IsNullOrEmpty(image_guid_res[0]))
                                {
                                    foreach (var item in image_guid_res[0].Split(','))
                                    {
                                        var info_arr = item.Split(':');
                                        if (info_arr[1].ToString() == "1")
                                        {
                                            try
                                            {
                                                var webC = new System.Net.WebClient();
                                                string url = Program.Client.PicUrl + info_arr[2].ToString();
                                                Image image = new Bitmap(webC.OpenRead(url));
                                                dataGridView1.Rows[e.RowIndex].Cells["tp"].Value = image;
                                            }
                                            catch
                                            {
                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["tp"].Value = null;
                                }

                            }
                        }
                    }
                    #endregion



                    //MQA处理记录
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "processing_record")
                    {
                        comboBox1.Visible = false;
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        textBox3.Visible = false;
                        comboBox2.Visible = false;
                        textBox4.Visible = false;
                        textBox5.Visible = false;
                        string aa = dataGridView1.CurrentRow.Cells["processing_record"].Value is null ? "" : dataGridView1.CurrentRow.Cells["processing_record"].Value.ToString();
                        string processing_record = aa == "" ? "" : aa;
                        textBox6.Text = processing_record; //判断值

                        Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                        textBox6.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        textBox6.Visible = true;
                    } 
                    //MQA附件
                    else if (dataGridView1.Columns[e.ColumnIndex].Name == "MQAfile")
                    {
                        comboBox1.Visible = false;
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        textBox3.Visible = false;
                        comboBox2.Visible = false;
                        textBox4.Visible = false;
                        textBox5.Visible = false;
                        textBox6.Visible = false;
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["MQAfile"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("UPLOAD"))
                        {
                            //string did = dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString();
                            //if (!string.IsNullOrEmpty(did))
                            //{
                            //    MessageBox.Show("不能上传!");
                            //    return;
                            //}

                            // string res = UpLoad("3", file_type);
                            string guid = Guid.NewGuid().ToString("N");
                            // 创建文件弹出选择窗口（包括文件名）对象
                            OpenFileDialog ofd = new OpenFileDialog();
                            //判断选择的路径
                            string path = string.Empty;
                            ofd.Title = "Please select a file";//请选择文件
                            ofd.Filter = "All files|*.*";//所有文件
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                                filePath = ofd.FileName;


                                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                                if (res.IsSuccess)
                                {
                                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                    if (dataGridView1.Rows[e.RowIndex].Cells["MQAfilelist"].Value.ToString() == "")
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells["MQAfilelist"].Value = resultDIC["guid"].ToString();
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells["MQAfilelist"].Value = dataGridView1.Rows[e.RowIndex].Cells["MQAfilelist"].Value + "," + resultDIC["guid"].ToString();
                                    }
                                    MessageBox.Show("Uploaded Successfully");
                                }
                                else
                                { 
                                    MessageBox.Show("Failed to upload file！");
                                }
                            }
                        }
                    }
                }


                #region 查看文件 
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
                    comboBox1.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox2.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    textBox6.Visible = false;
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
                            FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", true, true, "QCM_MQA_MAG_D_F&FILE_ID");
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
                #endregion




            }
        }



        /// <summary>
        /// 各阶段样品记录添加页面查询品质风险类别
        /// </summary>
        /// <returns></returns>
        public DataTable Getrisk_category()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.DQA_ShoeShape",//类名
                                        "Getrisk_category",//方法名
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
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            return dt;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = comboBox1.Text;
            dataGridView1.Rows[RowIndex].Cells["workshop_section_no"].Value = comboBox1.SelectedValue.ToString();
            comboBox1.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = textBox1.Text.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = textBox2.Text.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = textBox3.Text.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = textBox4.Text.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = textBox6.Text.ToString();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[RowIndex].Cells[ColumnIndex].Value = textBox5.Text.ToString();
        }

        /// <summary>
        /// MQA管理页面添加
        /// </summary>
        public void Editmqa_mag_d()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("shoes_code", shoe_no);
                data.Add("qa_principal", textBox7.Text.Trim());
                data.Add("mqa_mag_d", GetDgvToTable(dataGridView1));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.MQA_ShoeShape", "Editmqa_mag_d", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved Successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    btn_cencle.Visible = false;
                    button2.Visible = false;
                    isEdit = false; 
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox2.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    textBox6.Visible = false; 

                    LoadPage();//刷新数据

                    //this.Close();
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

        /// <summary>
        /// dgv控件转datatable
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        { 
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["problemsources"].Value.ToString() == "MQA")
                {
                    //工段
                    string workshop_section_no = dataGridView1.Rows[i].Cells["workshop_section_no"].Value == null ? "" : dataGridView1.Rows[i].Cells["workshop_section_no"].Value.ToString();
                    ////材料编号/工序代码
                    //string choice_no = dataGridView1.Rows[i].Cells["choice_no"].Value == null ? "" : dataGridView1.Rows[i].Cells["choice_no"].Value.ToString();
                    ////品质风险类别
                    //string qa_risk_category_code = dataGridView1.Rows[i].Cells["qa_risk_desc"].Value == null ? "" : dataGridView1.Rows[i].Cells["qa_risk_desc"].Value.ToString();
                    ////检验项编号
                    //string inspection_code = dataGridView1.Rows[i].Cells["inspection_code"].Value == null ? "" : dataGridView1.Rows[i].Cells["inspection_code"].Value.ToString();
                    ////判断方式
                    //string judge_mode = dataGridView1.Rows[i].Cells["judge_mode"].Value == null ? "" : dataGridView1.Rows[i].Cells["judge_mode"].Value.ToString();
                    ////标准值
                    //string standard_value = dataGridView1.Rows[i].Cells["standard_value"].Value == null ? "" : dataGridView1.Rows[i].Cells["standard_value"].Value.ToString();
                    ////单位
                    //string unit = dataGridView1.Rows[i].Cells["unit"].Value == null ? "" : dataGridView1.Rows[i].Cells["unit"].Value.ToString();
                    ////部门属性
                    //string dep_attr = dataGridView1.Rows[i].Cells["dep_attr"].Value == null ? "" : dataGridView1.Rows[i].Cells["dep_attr"].Value.ToString();
                    if (string.IsNullOrEmpty(workshop_section_no))
                        //string.IsNullOrEmpty(dep_attr) || 
                        //string.IsNullOrEmpty(choice_no) || string.IsNullOrEmpty(qa_risk_category_code) ||
                        //string.IsNullOrEmpty(inspection_code) || string.IsNullOrEmpty(judge_mode) ||
                        //string.IsNullOrEmpty(standard_value) || string.IsNullOrEmpty(unit)
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Section data is required, please check！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                }
            }

            Editmqa_mag_d();
        }

        /// <summary>
        /// 取消点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cencle_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            comboBox2.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            button2.Visible = false;
            btn_cencle.Visible = false;
            isEdit = false;
            LoadPage();//刷新数据
        }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_edit_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            btn_cencle.Visible = true;
            isEdit = true;
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = comboBox2.Text;
            dataGridView1.CurrentRow.Cells["qa_risk_category_code"].Value = comboBox2.SelectedValue.ToString();
        }
    }
}
