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
    public partial class F_MQA_ShoeShape_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_MQA_ShoeShape_List()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        string shoe_no = string.Empty;
        string user_fdd = string.Empty;
        public F_MQA_ShoeShape_List(string _shoe_no,string _user_fdd)
        {
            shoe_no = _shoe_no;
            user_fdd = _user_fdd;
            InitializeComponent();
            aa();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void aa()
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
        /// 跳转MQA查看页面时查询表头
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetShoeShape_ListTH(string shoe_no)
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
                                            "GetShoeShape_ListTH",//方法名
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
                    label12.Text = dt.Rows[0]["name_t"].ToString();
                    label15.Text = dt.Rows[0]["rule_no"].ToString();
                    label17.Text = dt.Rows[0]["TEST_LEVEL"].ToString();
                    label13.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();
                    label19.Text = dt.Rows[0]["develop_type"].ToString();
                    label14.Text = dt.Rows[0]["user_section"].ToString();
                    label21.Text = dt.Rows[0]["COL1"].ToString();
                    label16.Text = user_fdd;
                    label18.Text = dt.Rows[0]["user_technical"].ToString();

                    textBox7.Text = dt.Rows[0]["qa_principal"].ToString();

                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["FILE_URL"].ToString());
                    try
                    {
                        Image image = new Bitmap(webC.OpenRead(url));
                        pictureBox1.Image = image;
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
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

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// DQA查看页面查询数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetShoeShape_List(int pageSize, int pageIndex, out int totalCount)
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
                                            "GetShoeShape_List",//方法名
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
                        dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                        dgvr.Cells["problemsources"].Value = dr["problemsources"].ToString();
                        dgvr.Cells["art_code"].Value = dr["art_code"].ToString();
                        dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();
                        dgvr.Cells["qa_risk_desc"].Value = dr["qa_risk_desc"].ToString();
                        dgvr.Cells["inspection_code"].Value = dr["inspection_code"].ToString();
                        dgvr.Cells["inspection_name"].Value = dr["inspection_name"].ToString();
                        dgvr.Cells["imageguids"].Value = dr["imageguids"].ToString();
                        //var webC = new System.Net.WebClient();
                        //string url = Program.Client.PicUrl + Convert.ToString(dr["file_url"].ToString());
                        
                        //try
                        //{
                        //    Image image = new Bitmap(webC.OpenRead(url));
                        //    dgvr.Cells["tp"].Value = image;
                        //}
                        //catch (Exception)
                        //{ }

                        if (!string.IsNullOrEmpty(dr["file_url"].ToString()))
                        {
                            try
                            {
                                List<string> imgsList = dr["file_url"].ToString().Split(',').ToList();
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

                        dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString();
                        dgvr.Cells["judge_mode"].Value = dr["judge_mode"].ToString();
                        dgvr.Cells["standard_value"].Value = dr["standard_value"].ToString();
                        dgvr.Cells["unit"].Value = dr["unit"].ToString();
                        dgvr.Cells["remark"].Value = dr["remark"].ToString();
                        dgvr.Cells["other_measures"].Value = dr["other_measures"].ToString();
                        dgvr.Cells["filelistguid"].Value = dr["filelistguid"].ToString();
                        dgvr.Cells["qa_risk_details_desc"].Value = dr["QA_RISK_DETAILS_DESC"].ToString();
                        dgvr.Cells["qa_risk_category_code"].Value = dr["qa_risk_category_code"].ToString();
                        dgvr.Cells["qa_risk_category_name"].Value = dr["qa_risk_category_name"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["filelist"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_MQA_ShoeShape_List_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetShoeShape_ListTH(shoe_no);
            GetShoeShape_EditTab();

            pageControl1.BindPageEvent += GetShoeShape_List;
            LoadPage();
            this.dataGridView1.ClearSelection();
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
        /// MQA查看页面查询文件
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public DataTable GetShoeShape_ListFile(string filelistguid)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("filelistguid", filelistguid);//guid
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "GetShoeShape_ListFile",//方法名
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "filelist")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["filelist"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("SELECT"))
                    {
                        string filelistguid = dataGridView1.Rows[e.RowIndex].Cells["filelistguid"].Value.ToString();
                        var currRowFileDt = GetShoeShape_ListFile(filelistguid);
                        FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", true, false);
                        add.Show();
                    }
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {

                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("img"))
                    {
                        List<string> image_guid_res = new List<string>();
                        image_guid_res.Add(dataGridView1.Rows[e.RowIndex].Cells["imageguids"].Value.ToString());
                        //string image_guid_res = dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString();
                        FrmQaImgSetting frmQaImgSetting = new FrmQaImgSetting(image_guid_res, false);
                        frmQaImgSetting.Show();
                        dataGridView1.Rows[e.RowIndex].Cells["imageguids"].Value = image_guid_res[0];

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// MQA查看页面查询所有文件
        /// </summary>
        /// <returns></returns>
        public DataTable GetShoeShape_ListFileALL()
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("shoes_code", shoe_no);
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "GetShoeShape_ListFileALL",//方法名
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

        private void button3_Click(object sender, EventArgs e)
        {
            var currRowFileDt = GetShoeShape_ListFileALL();
            FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", true, false);
            add.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> HeadDic = new Dictionary<string, string>();
                HeadDic.Add("预计订单", label21.Text);
                HeadDic.Add("季度", label13.Text);
                HeadDic.Add("开发课", label14.Text);
                HeadDic.Add("Category", label15.Text);
                HeadDic.Add("鞋型负责人", label16.Text);
                HeadDic.Add("测试级别", label17.Text);
                HeadDic.Add("开发技术负责人", label18.Text);
                HeadDic.Add("PB_Type", label19.Text);
                HeadDic.Add("QA负责人", textBox7.Text);
                string art = string.Empty;
                foreach (System.String item in this.checkedListBox1.CheckedItems)
                {
                    art += item + ",";
                }
                HeadDic.Add("ART", art.TrimEnd(','));
                HeadDic.Add("鞋型名称", label12.Text);

                //视图数据显示
                DataTable dts = GetShoeShape_List_Export();
                if (dts.Rows.Count < 1)
                {
                    MessageBox.Show("No data export yet，Please check if it is done correctly");
                    return;
                }
                /* if (DT_EXCEL.Rows.Count < 1)
                 {
                     MessageBox.Show("数据为空，先搜索再做导出操作");
                     return;
                 }*/
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("workshop_section_name", "Section");//工段
                Execldic.Add("problemsources", "Risk_Identification_Department");//风险识别部门
                Execldic.Add("art_code", "ART");
                Execldic.Add("choice_name", "Material/Part_Name");//材料/部件名称
                Execldic.Add("qa_risk_desc", "Quality_Risk_Description");//品质风险描述
                Execldic.Add("qa_risk_category_name", "Quality_Risk_Category_Name");//品质风险类别名称
                Execldic.Add("qa_risk_details_desc", "Quality_Risk");//品质风险
                Execldic.Add("inspection_name", "Test_Items");//检验项目
                Execldic.Add("judge_mode", "Judgment_Criteria");//判断标准
                Execldic.Add("standard_value", "Measurement_Standard");//测量标准
                Execldic.Add("unit", "Unit");//单位
                Execldic.Add("remark", "Remark");//备注
                Execldic.Add("other_measures", "Other_Measures");//其他措施

                dts.Columns.Remove("inspection_code");
                dts.Columns.Remove("inspection_typecode");

                ExeclHelper.ExportToTrueExcelEx(dts, HeadDic, Execldic, "MQA_View_List");//MQA查看列表
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// DQA查看页面查询数据导出
        /// </summary>
        /// <returns></returns>
        public DataTable GetShoeShape_List_Export()
        {
            DataTable dt = new DataTable();
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
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "GetShoeShape_List_Export",//方法名
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
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmFileList add = new FrmFileList(FileView(), Program.Client.UploadUrl, Program.Client.UserToken);
            add.Show();
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
    }
}
