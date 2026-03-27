using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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
    public partial class F_DQA_ShoeShape_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string shoe_no = string.Empty;
        string user_fdd = string.Empty;
        private F_DQA_ShoeShape_Main _f_DQA_ShoeShape_Main;
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
        public F_DQA_ShoeShape_Edit(string _shoe_no, F_DQA_ShoeShape_Main f_DQA_ShoeShape_Main,string _user_fdd)
        {
            shoe_no = _shoe_no;
            user_fdd = _user_fdd;
            InitializeComponent();
            aa();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _f_DQA_ShoeShape_Main = f_DQA_ShoeShape_Main;
            textBox4.Visible = true;
        }

        public F_DQA_ShoeShape_Edit()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_DQA_ShoeShape_Edit_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetShoeShape_EditTab();
            GetShoeShape_Edit(shoe_no);

            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView1.Columns["accessory"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView1.Columns["MQAFiles"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView1.Columns["tp"].DefaultCellStyle.NullValue = null;
        }

        /// <summary>
        /// 跳转DQA管理时查询信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetShoeShape_Edit(string shoe_no)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("SHOE_NO", shoe_no);//名称
                data.Add("workshop_section_no", tabid);//名称
                data.Add("id", tabid2);//id
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
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
                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                if (dt.Rows.Count > 0)
                {
                    //string[] prod_no = dt.Rows[0]["PROD_NO"].ToString().Split(',');
                    richTextBox1.Text = dt.Rows[0]["PROD_NO"].ToString();
                    label12.Text = dt.Rows[0]["name_t"].ToString();
                    //label14.Text = dt.Rows[0]["user_section"].ToString();
                    //label16.Text = dt.Rows[0]["user_technical"].ToString();
                    label13.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();
                    label15.Text = dt.Rows[0]["rule_no"].ToString();
                    label17.Text = dt.Rows[0]["TEST_LEVEL"].ToString();
                    label19.Text = dt.Rows[0]["develop_type"].ToString();
                    label14.Text = dt.Rows[0]["user_section"].ToString();
                    label21.Text = dt.Rows[0]["COL1"].ToString();
                    label16.Text = user_fdd;
                    label18.Text = dt.Rows[0]["user_technical"].ToString();

                    textBox4.Text = dt.Rows[0]["qa_principal"].ToString();

                    //label27.Text = dt.Rows[0]["bom_date"].ToString();
                    //label28.Text = dt.Rows[0]["cwa_date"].ToString();
                    //label29.Text = dt.Rows[0]["user_fdd"].ToString();
                    //label24.Text = dt.Rows[0]["develop_type"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["file_url"].ToString()))
                    {
                        try
                        {
                            var webC = new System.Net.WebClient();
                            string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["file_url"].ToString());
                            Image image = new Bitmap(webC.OpenRead(url));
                            pictureBox1.Image = image;
                            this.label_upload_pic.Visible = false;
                            pic_guid.Text = dt.Rows[0]["image_guid"].ToString();
                        }
                        catch{ }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }

                }
                dataGridView1.Rows.Clear();
                //表身数据
                if (dt2.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt2.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        //dgvr.Cells["tp"].Value = dr["PROD_NO"].ToString();//图片
                        //if (!string.IsNullOrEmpty(dr["FILE_URL"].ToString()))
                        //{
                        //    try
                        //    {
                        //        var webC = new System.Net.WebClient();
                        //        string url = Program.Client.PicUrl + Convert.ToString(dr["FILE_URL"].ToString());
                        //        Image image = new Bitmap(webC.OpenRead(url));
                        //        dgvr.Cells["tp"].Value = image;
                        //    }
                        //    catch
                        //    {
                        //    }
                        //}
                        //else
                        //{
                        //    dgvr.Cells["tp"].Value = null;
                        //}
                        dgvr.Cells["did"].Value = dr["id"].ToString();//隐藏列id
                        dgvr.Cells["art_code"].Value = dr["art_code"].ToString();//ART
                        dgvr.Cells["image_guid"].Value = dr["IMAGE_GUID"].ToString();//imgguid
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

                        dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();//材料编号
                        dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();//材料编号
                        dgvr.Cells["qa_risk_desc"].Value = dr["qa_risk_desc"].ToString();//品质风险描述
                        dgvr.Cells["inspection_code"].Value = dr["inspection_code"].ToString();//检验项目代号
                        dgvr.Cells["inspection_name"].Value = dr["inspection_name"].ToString();//检验项目名称
                        dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString();//检验项目类型
                        dgvr.Cells["judge_mode"].Value = dr["judge_mode"].ToString();//判断方式
                        dgvr.Cells["judge_modeNo"].Value = dr["JUDGMENT_CRITERIA"].ToString();//判断方式
                        //dgvr.Cells["judge_mode"].Value = dr["judge_mode"].ToString();//判断方式
                        dgvr.Cells["standard_value"].Value = dr["standard_value"].ToString();//标准值
                        dgvr.Cells["judge_type"].Value = dr["judge_type"].ToString();//判断标准
                        dgvr.Cells["unit"].Value = dr["unit"].ToString();//单位
                        dgvr.Cells["other_measures"].Value = dr["other_measures"].ToString();//其他措施
                        dgvr.Cells["remark"].Value = dr["remark"].ToString();//备注
                        dgvr.Cells["processing_record"].Value = dr["processing_record"].ToString();//mqa备注
                        dgvr.Cells["mfjguid"].Value = dr["mffile_id"].ToString();//

                        dgvr.Cells["f_insp_dep"].Value = dr["f_insp_dep"].ToString();
                        dgvr.Cells["f_insp_date"].Value = dr["f_insp_date"].ToString();
                        dgvr.Cells["f_insp_res"].Value = dr["f_insp_res"].ToString();

                        dgvr.Cells["fjguid"].Value = dr["file_id"].ToString();//附件id隐藏列
                        dgvr.Cells["qa_risk_details_desc"].Value = dr["QA_RISK_DETAILS_DESC"].ToString();//品质风险细项
                        dgvr.Cells["qa_risk_category_code"].Value = dr["qa_risk_category_code"].ToString();
                        dgvr.Cells["qa_risk_category_name"].Value = dr["qa_risk_category_name"].ToString();

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void button4_Click(object sender, EventArgs e)
        {
            /*
            #region 上传图片按钮
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
                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.APIURL, filePath, Program.Client.UserToken);
                if (res.IsSuccess)
                {
                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                    UploadShoeShape_EditImg(resultDIC["guid"].ToString());
                }
            }
            #endregion
            */
        }

        /// <summary>
        /// 上传DQA管理页面图片
        /// </summary>
        public void UploadShoeShape_EditImg(Dictionary<string, object> dic)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("shoes_code", shoe_no);
                data.Add("image_guid", dic["guid"].ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "UploadShoeShape_EditImg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    var webC = new System.Net.WebClient();
                    string url = Program.Client.PicUrl + dic["url"].ToString();
                    Image image = new Bitmap(webC.OpenRead(url));
                    pictureBox1.Image = image;
                    this.label_upload_pic.Visible = false;
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Uploaded successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    _f_DQA_ShoeShape_Main.LoadPage();
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

        //页签查询
        public string tabid = string.Empty;//页签id
        public string tabid2 = string.Empty;//页签id
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
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
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

                    foreach (DataRow item in dt.Rows)
                    {
                        TabPage tabPage = new TabPage();
                        tabPage.Name = "tabPage" + i;
                        this.tab_type_standard.TabPages.Add(tabPage);
                        tabPage.Text = item["workshop_section_name"].ToString();
                        //tabPage.Tag = item["id"].ToString();

                        taid taid = new taid();
                        taid.taid1 = item["workshop_section_no"].ToString();//dt.Rows[0]["workshop_section_no"].ToString();
                        taid.taid2 = item["id"].ToString(); ///dt.Rows[0]["id"].ToString();
                        tabPage.Tag = taid;//item["workshop_section_no"].ToString();
                        tabid = dt.Rows[0]["workshop_section_no"].ToString();//dt.Rows[0]["workshop_section_no"].ToString();
                        tabid2 = dt.Rows[0]["id"].ToString(); ///dt.Rows[0]["id"].ToString();
                        i++;
                    }
                    int index = this.tab_type_standard.SelectedIndex;
                    var taid2 = (taid)this.tab_type_standard.TabPages[index].Tag;
                    tabid = taid2.taid1;
                    tabid2 = taid2.taid2;
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            F_DQA_ShoeShape_Edit_workshop update = new F_DQA_ShoeShape_Edit_workshop(shoe_no);
            update.ShowDialog();
            if (update.Tag != null)
            {
                GetShoeShape_EditTab();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Delete section", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                Deletedqa_mag_m();
            }
        }

        /// <summary>
        /// DQA管理页面删除页签
        /// </summary>
        public void Deletedqa_mag_m()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("mid", tabid2);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "Deletedqa_mag_m", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    GetShoeShape_EditTab();
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

        private void tab_type_standard_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            int index = this.tab_type_standard.SelectedIndex;
             
            var taid = (taid)this.tab_type_standard.TabPages[index].Tag;
            tabid = taid.taid1;
            tabid2 = taid.taid2;

            GetShoeShape_Edit(shoe_no);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tab_type_standard.TabPages.Count > 0)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["id"].Value =index;
                this.dataGridView1.Rows[index].Cells["image_guid"].Value = "";
                this.dataGridView1.Rows[index].Cells["choice_no"].Value = "";
                this.dataGridView1.Rows[index].Cells["choice_name"].Value = "";
                this.dataGridView1.Rows[index].Cells["qa_risk_desc"].Value = "";
                this.dataGridView1.Rows[index].Cells["inspection_code"].Value = "";
                this.dataGridView1.Rows[index].Cells["judge_mode"].Value = "";
                this.dataGridView1.Rows[index].Cells["standard_value"].Value = "";
                this.dataGridView1.Rows[index].Cells["unit"].Value = "";
                this.dataGridView1.Rows[index].Cells["other_measures"].Value = "";
                this.dataGridView1.Rows[index].Cells["remark"].Value = "";
                this.dataGridView1.Rows[index].Cells["art_code"].Value = "";
                this.dataGridView1.Rows[index].Cells["fjguid"].Value = "";
                this.dataGridView1.Rows[index].Cells["mfjguid"].Value = "";
                this.dataGridView1.Rows[index].Cells["processing_record"].Value = "";
                this.dataGridView1.Rows[index].Cells["inspection_type"].Value = "";

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

        /// <summary>
        /// DQA管理页面查询判断方式
        /// </summary>
        public DataTable Getjudge_mode()
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("SHOE_NO", shoe_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
                                            "Getjudge_mode",//方法名
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
                //var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                //if (dt.Rows.Count > 0)
                //{
                //    comboBox1.DataSource = dt;
                //    comboBox1.DisplayMember = "";
                //    comboBox1.ValueMember = "";
                //}
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
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
        string _inspection_name = string.Empty;//检测项名称
        string _inspection_type = string.Empty;//检测项类型
        string _judgment_criteria = string.Empty;//判断标准
        string _judge_type = string.Empty;//判断类型
        string _judgment_criteriaName = string.Empty;//判断标准名称
        public void Edit_inspection(string inspection_code = "", string inspection_name = "", string inspection_type = "", string judgment_criteria = "", string judge_type = "", string judgment_criteriaName = "")
        {
            _inspection_code = inspection_code;
            _inspection_name = inspection_name;
            _inspection_type = inspection_type;
            _judgment_criteria = judgment_criteria;
            _judge_type = judge_type;
            _judgment_criteriaName = judgment_criteriaName;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "art_code") // 
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox5.Visible = false;
                    textBox3.Visible = false;
                     
                    textBox6.Visible = false;
                    F_DQA_ShoeShape_trait_Insert_ART update = new F_DQA_ShoeShape_trait_Insert_ART(this.shoe_no, dataGridView1.Rows[e.RowIndex].Cells["art_code"].Value.ToString(),false);
                    update.ShowDialog();
                    if (update.Tag != null)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["art_code"].Value = update.Tag.ToString();
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "choice_no") // 
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox3.Visible = false;
                    textBox5.Visible = false;
                     
                    textBox6.Visible = false;

                    string art_no_list = dataGridView1.Rows[e.RowIndex].Cells["art_code"].Value.ToString();
                    F_DQA_ShoeShape_trait_Insert_material update = new F_DQA_ShoeShape_trait_Insert_material(tabid2, art_no_list);
                    update.ShowDialog();
                    if (update.Tag != null)
                    {
                        string[] choice = update.Tag.ToString().Split(',');
                        dataGridView1.Rows[e.RowIndex].Cells["choice_no"].Value = choice[0];
                        dataGridView1.Rows[e.RowIndex].Cells["choice_name"].Value = choice[1];
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_details_desc") // 
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox3.Visible = false;
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
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "inspection_code") // 
                {
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                     
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    F_DQA_ShoeShape_Edit_inspection update = new F_DQA_ShoeShape_Edit_inspection(tabid2, this);
                    update.ShowDialog();
                    dataGridView1.Rows[e.RowIndex].Cells["inspection_code"].Value = _inspection_code;
                    dataGridView1.Rows[e.RowIndex].Cells["inspection_name"].Value = _inspection_name;
                    dataGridView1.Rows[e.RowIndex].Cells["inspection_type"].Value = _inspection_type;
                    dataGridView1.Rows[e.RowIndex].Cells["judge_mode"].Value = _judgment_criteriaName;
                    dataGridView1.Rows[e.RowIndex].Cells["judge_modeNo"].Value = _judgment_criteria;
                    dataGridView1.Rows[e.RowIndex].Cells["judge_type"].Value = _judge_type;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "standard_value") // 
                {
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                     
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["standard_value"].Value is null ? "" : dataGridView1.CurrentRow.Cells["standard_value"].Value.ToString();
                    string standard_value = aa == "" ? "" : aa;
                    textBox1.Text = standard_value; //判断值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "other_measures") // 
                {
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox3.Visible = false;
                     
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["other_measures"].Value is null ? "" : dataGridView1.CurrentRow.Cells["other_measures"].Value.ToString();
                    string standard_value = aa == "" ? "" : aa;
                    textBox2.Text = standard_value; //其他措施

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox2.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "remark") // 
                {
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox2.Visible = false;
                     
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["remark"].Value is null ? "" : dataGridView1.CurrentRow.Cells["remark"].Value.ToString();
                    string remark = aa == "" ? "" : aa;
                    textBox3.Text = remark; //备注

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox3.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox3.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "unit") // 
                {
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                     
                    textBox6.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["unit"].Value is null ? "" : dataGridView1.CurrentRow.Cells["unit"].Value.ToString();
                    string unit = aa == "" ? "" : aa;
                    textBox5.Text = unit; //单位

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox5.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox5.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_desc") // 
                {
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                     
                    string aa = dataGridView1.CurrentRow.Cells["qa_risk_desc"].Value is null ? "" : dataGridView1.CurrentRow.Cells["qa_risk_desc"].Value.ToString();
                    string qa_risk_desc = aa == "" ? "" : aa;
                    textBox6.Text = qa_risk_desc; //品质风险描述

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox6.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox6.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_category_name") // combobox显示条件 
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox3.Visible = false;
                    textBox5.Visible = false;

                    textBox6.Visible = false;

                    DataTable dt_tval = Getrisk_category();
                    comboBox1.DataSource = dt_tval;
                    if (dt_tval != null && dt_tval.Rows.Count > 0)
                    {
                        comboBox1.DisplayMember = "qa_risk_category_name";
                        comboBox1.ValueMember = "qa_risk_category_code";
                    }
                    string qa_risk_category_name = dataGridView1.CurrentRow.Cells["qa_risk_category_name"].Value == null ? "" : dataGridView1.CurrentRow.Cells["qa_risk_category_name"].Value.ToString(); //对combobox赋值
                    comboBox1.Text = qa_risk_category_name;

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    comboBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    comboBox1.Visible = true;
                }
                else
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox3.Visible = false;
                     
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox3.Visible = false;
                     
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("DELETE"))
                    {
                        //dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = "";
                        //Image image = null;
                        //dataGridView1.Rows[e.RowIndex].Cells["tp"].Value = image;
                        //dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex]);//删除行
                        DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Delete MQA", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        string did = string.Empty;
                        if (dataGridView1.Rows[e.RowIndex].Cells["did"].Value == null)
                        {
                            if (dr == DialogResult.OK)
                            {
                                dataGridView1.Rows.RemoveAt(e.RowIndex);
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                            }
                        }
                        else
                        {
                            did = dataGridView1.Rows[e.RowIndex].Cells["did"].Value.ToString();

                            if (dr == DialogResult.OK)
                            {
                                Deletemqa_mag_d(did);
                            }
                        }

                    }
                    else if (cell.CurrentItem.Equals("UPLOAD"))
                    {
                        //创建文件弹出选择窗口（包括文件名）对象
                        OpenFileDialog ofd = new OpenFileDialog();
                        //判断选择的路径
                        string path = string.Empty;
                        ofd.Title = "Please select a folder";
                        ofd.Filter = "Image file (.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
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
                                MessageBox.Show("uploaded successfully");
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

                if (dataGridView1.Columns[e.ColumnIndex].Name == "accessory")
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox3.Visible = false;
                     
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["accessory"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("UPLOAD"))
                    {
                        // string res = UpLoad("3", file_type);
                        string guid = Guid.NewGuid().ToString("N");
                        // 创建文件弹出选择窗口（包括文件名）对象
                        OpenFileDialog ofd = new OpenFileDialog();
                        //判断选择的路径
                        string path = string.Empty;
                        ofd.Title = "Please select a file";
                        ofd.Filter = "All files|*.*";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                            filePath = ofd.FileName;


                            UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                            if (res.IsSuccess)
                            {
                                var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                if (dataGridView1.Rows[e.RowIndex].Cells["fjguid"].Value.ToString() == "")
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["fjguid"].Value = resultDIC["guid"].ToString();
                                }
                                else
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["fjguid"].Value = dataGridView1.Rows[e.RowIndex].Cells["fjguid"].Value + "," + resultDIC["guid"].ToString();
                                }
                                MessageBox.Show("uploaded successfully");
                            }
                            else
                            {

                                MessageBox.Show("Failed to upload file！");
                            }
                        }
                    }
                    else if (cell.CurrentItem.Equals("accessorylist"))
                    {
                        string[] fjguid = dataGridView1.Rows[e.RowIndex].Cells["fjguid"].Value.ToString().Split(',');

                        var currRowFileDt = GetShoeShape_EditFile(fjguid);
                        FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", false);
                        add.ShowDialog();
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
                        dataGridView1.Rows[e.RowIndex].Cells["fjguid"].Value = fjguids;
                    }
                }

                //MQA附件
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "MQAFiles")
                { 
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["MQAFiles"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("SelectMQAFile"))
                    {  
                        string[] mfjguid = dataGridView1.Rows[e.RowIndex].Cells["mfjguid"].Value.ToString().Split(',');

                        var currRowFileDt = GetShoeShape_EditFile(mfjguid);
                        FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", false, false);
                        add.ShowDialog();
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
                        dataGridView1.Rows[e.RowIndex].Cells["mfjguid"].Value = fjguids;

                    }
                }

            }
            else
            {
                textBox2.Visible = false;
                textBox1.Visible = false;
                comboBox1.Visible = false;
                textBox3.Visible = false;
                 
                textBox5.Visible = false;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox1.Text.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox2.Text.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox3.Text.ToString();
        }

        /// <summary>
        /// DQA管理页面添加
        /// </summary>
        public void Editdqa_mag_d()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("shoes_code", shoe_no);
                data.Add("qa_principal", textBox4.Text.Trim());
                data.Add("m_id", tabid2);
                data.Add("dqa_mag_d", GetDgvToTable(dataGridView1));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "Editdqa_mag_d", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved Successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
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
        /// DQA管理页面删除
        /// </summary>
        public void Deletemqa_mag_d(string did)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("did", did);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "Deletedqa_mag_d", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    GetShoeShape_Edit(shoe_no);
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

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //材料编号/工序代码
                string choice_no = dataGridView1.Rows[i].Cells["choice_no"].Value == null ? "" : dataGridView1.Rows[i].Cells["choice_no"].Value.ToString();
                //品质风险类别
                string qa_risk_category_code = dataGridView1.Rows[i].Cells["qa_risk_desc"].Value == null ? "" : dataGridView1.Rows[i].Cells["qa_risk_desc"].Value.ToString();
                //检验项编号
                string inspection_code = dataGridView1.Rows[i].Cells["inspection_code"].Value == null ? "" : dataGridView1.Rows[i].Cells["inspection_code"].Value.ToString();
                //判断方式
                string judge_mode = dataGridView1.Rows[i].Cells["judge_mode"].Value == null ? "" : dataGridView1.Rows[i].Cells["judge_mode"].Value.ToString();
                //标准值
                string standard_value = dataGridView1.Rows[i].Cells["standard_value"].Value == null ? "" : dataGridView1.Rows[i].Cells["standard_value"].Value.ToString();
                //单位
                string unit = dataGridView1.Rows[i].Cells["unit"].Value == null ? "" : dataGridView1.Rows[i].Cells["unit"].Value.ToString();
                if ( string.IsNullOrEmpty(qa_risk_category_code))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Quality risk description cannot be empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
            }

            Editdqa_mag_d();
            
            GetShoeShape_Edit(shoe_no);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox5.Text.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox6.Text.ToString();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            
           
        }

        private void button4_Click_1(object sender, EventArgs e)
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
        /// <summary>
        /// 右键删除/左键上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            try
            {
                if (e.Button == MouseButtons.Right && e.Clicks == 1)
                {
                    if (MessageBox.Show("It cannot be restored after deletion, whether it is really deleted! ", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        //请求api的数据展示
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("guid", pic_guid.Text);

                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_BDMAPI",//类库名
                                                    "SJ_BDMAPI.DQA_ShoeShape",//类名
                                                    "DeletePicbyGuid",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(data));

                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            throw new Exception(ret.ErrMsg);
                        }
                        else
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        }
                        pictureBox1.Image = null;
                        label_upload_pic.Visible = true;
                    }

                }
                else
                {
                    //创建文件弹出选择窗口（包括文件名）对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //判断选择的路径
                    string path = string.Empty;
                    ofd.Title = "Please select a folder";
                    ofd.Filter = "image file(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                        filePath = ofd.FileName;
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            UploadShoeShape_EditImg(resultDIC);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = comboBox1.Text;
            dataGridView1.CurrentRow.Cells["qa_risk_category_code"].Value = comboBox1.SelectedValue.ToString();
        }
    }

    public class taid
    {
         public string taid1;
        public string taid2;
    }

}
