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
    public partial class F_DQA_ShoeShape_trait_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private String comboboxState = "Pending";//当测试选项为空时默认选中
        string shoe_no = string.Empty;
        string develop_season = string.Empty;
        string section_manager = string.Empty;
        public F_DQA_ShoeShape_trait_Main()
        {
            InitializeComponent();
            aa();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_DQA_ShoeShape_trait_Main(string _shoe_no, string _develop_season,string _section_manager)
        {
            shoe_no = _shoe_no;
            develop_season = _develop_season;
            section_manager = _section_manager;
            InitializeComponent();
            aa();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void aa()
        {
            label19.Text = "";
            label23.Text = "";
            label27.Text = "";
            label28.Text = "";
            label30.Text = "";
            label20.Text = "";
            label24.Text = "";
            label29.Text = "";
            label21.Text = "";
            label25.Text = "";
            label22.Text = "";
            label26.Text = "";
        }
        private void F_DQA_ShoeShape_trait_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //#region 赋试穿枚举值
            //DataTable dt_tval = GetDGVComboBox1();
            //comboBox1.DataSource = dt_tval;
            //if (dt_tval != null && dt_tval.Rows.Count > 0)
            //{
            //    comboBox1.DisplayMember = "enum_code";
            //    comboBox1.ValueMember = "enum_value";
            //}
            
            //#endregion
            //#region 赋FGT枚举值
            //DataTable dt_tval2 = GetDGVComboBox2();
            //comboBox2.DataSource = dt_tval2;
            //if (dt_tval2 != null && dt_tval2.Rows.Count > 0)
            //{
            //    comboBox2.DisplayMember = "enum_code";
            //    comboBox2.ValueMember = "enum_value";
            //}
            //#endregion
            //#region 赋CMA枚举值
            //DataTable dt_tval3 = GetDGVComboBox3();
            //comboBox3.DataSource = dt_tval3;
            //if (dt_tval3 != null && dt_tval3.Rows.Count > 0)
            //{
            //    comboBox3.DisplayMember = "enum_code";
            //    comboBox3.ValueMember = "enum_value";
            //}
            //#endregion

            GetDQAtraitMain(shoe_no);

            GET_ShoeShapecenterView();


        }

        /// <summary>
        /// 跳转各阶段样品记录查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDQAtraitMain(string shoe_no)
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
                                            "GetDQAtraitMain",//方法名
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
                    richTextBox1.Text = dt.Rows[0]["PROD_NO"].ToString();
                    label20.Text = dt.Rows[0]["name_t"].ToString();
                    label30.Text = dt.Rows[0]["PRODUCT_MONTH"].ToString();
                    label23.Text = dt.Rows[0]["rule_no"].ToString();
                    label25.Text = dt.Rows[0]["TEST_LEVEL"].ToString();
                    label27.Text = dt.Rows[0]["BOM_DATE"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["BOM_DATE"]).ToString("yyyy-MM-dd");
                    label28.Text = dt.Rows[0]["cwa_date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["cwa_date"]).ToString("yyyy-MM-dd");
                    label29.Text = dt.Rows[0]["user_fdd"].ToString();
                    label26.Text = dt.Rows[0]["sumcol1"].ToString();

                    label22.Text = dt.Rows[0]["SHOE_NO"].ToString();
                    label21.Text = dt.Rows[0]["mold_no"].ToString();
                    label24.Text = dt.Rows[0]["develop_type"].ToString();

                    textBox1.Text = dt.Rows[0]["production_plant"].ToString(); // 生产厂区
                    textBox2.Text = dt.Rows[0]["process_specialist"].ToString();//工艺专员
                    textBox3.Text = dt.Rows[0]["section_chief"].ToString();//课长
                    textBox4.Text = dt.Rows[0]["bottom_formwork_specialist"].ToString();//地膜专员
                    textBox5.Text = dt.Rows[0]["master_editor"].ToString();//主板师

                    textBox6.Text = dt.Rows[0]["try_on_remark"].ToString();//试穿备注
                    textBox7.Text = dt.Rows[0]["fgt_remark"].ToString();//FGT备注
                    textBox8.Text = dt.Rows[0]["cma_remark"].ToString();//CMA备注
                    textBox3.Text = section_manager;
                    label19.Text = develop_season;
                    lbl_try_on.Text = string.IsNullOrEmpty(dt.Rows[0]["TRY_ON_STATE"].ToString()) ? "Pending" : dt.Rows[0]["TRY_ON_STATE"].ToString();
                    lbl_fgt.Text = string.IsNullOrEmpty(dt.Rows[0]["FGT_STATE"].ToString()) ? "Pending" : dt.Rows[0]["FGT_STATE"].ToString();
                    lbl_cma.Text = string.IsNullOrEmpty(dt.Rows[0]["CMA_STATE"].ToString()) ? "Pending" : dt.Rows[0]["CMA_STATE"].ToString();
                    //if (string.IsNullOrEmpty(dt.Rows[0]["TRY_ON_STATE"].ToString()))
                    //{
                    //    this.comboBox1.SelectedValue = "Pending";
                    //}
                    //else
                    //{
                    //    this.comboBox1.SelectedValue = dt.Rows[0]["TRY_ON_STATE"].ToString();
                    //}
                    //if (string.IsNullOrEmpty(dt.Rows[0]["FGT_STATE"].ToString()))
                    //{
                    //    this.comboBox2.SelectedValue = "Pending";
                    //}
                    //else
                    //{
                    //    this.comboBox2.SelectedValue = dt.Rows[0]["FGT_STATE"].ToString();
                    //}
                    //if (string.IsNullOrEmpty(dt.Rows[0]["CMA_STATE"].ToString()))
                    //{
                    //    this.comboBox3.SelectedValue = "Pending";
                    //}
                    //else
                    //{
                    //    this.comboBox3.SelectedValue = dt.Rows[0]["CMA_STATE"].ToString();
                    //}
                    if (!string.IsNullOrEmpty(dt.Rows[0]["file_url"].ToString()))
                    {
                        try
                        {
                            var webC = new System.Net.WebClient();
                            string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["file_url"].ToString());
                            Image image = new Bitmap(webC.OpenRead(url));
                            pictureBox1.Image = image;
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
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
        /// 查询试穿枚举
        /// </summary>
        /// <returns></returns>
        public DataTable GetDGVComboBox1()
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

        private void button3_Click(object sender, EventArgs e)
        {
            Editshoes_qa_record_m();
        }

        /// <summary>
        /// 保存鞋型品质记录
        /// </summary>
        public void Editshoes_qa_record_m()
        {
            try
            {
                //if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null ||
                //    comboBox3.SelectedValue == null)
                //{
                //    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择鞋型测试状态!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                //    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                //    return;
                //}
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("shoes_code", shoe_no);
                data.Add("try_on_state", lbl_try_on.Text);
                data.Add("fgt_state", lbl_fgt.Text);
                data.Add("cma_state", lbl_cma.Text);
                data.Add("production_plant", this.textBox1.Text.Trim());
                data.Add("process_specialist", this.textBox2.Text.Trim());
                data.Add("section_chief", this.textBox3.Text.Trim());
                data.Add("bottom_formwork_specialist", this.textBox4.Text.Trim());
                data.Add("master_editor", this.textBox5.Text.Trim());

                data.Add("try_on_remark", this.textBox6.Text.Trim());
                data.Add("fgt_remark", this.textBox7.Text.Trim());
                data.Add("cma_remark", this.textBox8.Text.Trim());

                data.Add("Production_Factory", this.textBox1.Text.Trim());//生产厂区
                data.Add("Technology_Specialist", this.textBox2.Text.Trim());//工艺专员
                //data.Add("Section_Manager", this.textBox3.Text.Trim());//课长
                //data.Add("Mulch_Commissioner", this.textBox3.Text.Trim());//地膜专员
                //data.Add("Main_Board_Division", this.textBox3.Text.Trim());//主板师
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "Editshoes_qa_record_m", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);//保存成功
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
        /// 点击上传文件
        /// </summary>
        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void UploadAll()
        {

            try
            {
                // string res = UpLoad("3", file_type);
                // 创建文件弹出选择窗口（包括文件名）对象
                OpenFileDialog ofd = new OpenFileDialog();
                //判断选择的路径
                string path = string.Empty;
                ofd.Title = "请选择文件";
                ofd.Filter = "所有文件|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                    filePath = ofd.FileName;


                    UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                    if (res.IsSuccess)
                    {
                        var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());


                        //保存文件信息 
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("shoes_code", shoe_no);//鞋型
                        data.Add("file_id", resultDIC["guid"].ToString());//文件关联id
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "UploadtraitEditFile", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        if (!ret.IsSuccess)
                            throw new Exception(ret.ErrMsg);
                        else
                        {
                            MessageBox.Show("上传文件成功！");
                        }

                    }
                    else
                    {

                        MessageBox.Show("上传文件失败！");
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            F_DQA_ShoeShape_trait_File f_DQA_ShoeShape_Trait_File = new F_DQA_ShoeShape_trait_File(shoe_no);
            f_DQA_ShoeShape_Trait_File.StartPosition = FormStartPosition.CenterParent;
            f_DQA_ShoeShape_Trait_File.ShowDialog();
            //UploadAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F_DQA_ShoeShape_trait_Edit update = new F_DQA_ShoeShape_trait_Edit(shoe_no, textBox3.Text, textBox6.Text, textBox7.Text, textBox8.Text);
            update.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (F_DQA_ShoeShape_trait_Insert update = new F_DQA_ShoeShape_trait_Insert(shoe_no))
            {
                update.ShowDialog();
                GET_ShoeShapecenterView();
            }
        }

        /// <summary>
        /// 历史各阶段样品品质状况
        /// </summary>
        public void GET_ShoeShapecenterView(string type = "")
        {
            try
            {
                this.flowLayoutPanelTable.Controls.Clear();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("shoes_code", shoe_no);
                data.Add("is_dqa_mqa_band", type);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                           "SJ_BDMAPI.DQA_ShoeShape",//类名
                                           "GET_ShoeShapecenterView",//方法名
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
                    dt.Columns.Add("did");
                    dt.Columns.Add("shoe_code");
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
                    dt.Columns.Add("image_guid");
                    dt.Columns.Add("phase_date");//生产只数
                    dt.Columns.Add("itemnumber");//项次
                    dt.Columns.Add("phase_creation_no");
                    dt.Columns.Add("phase_creation_name");
                    dt.Columns.Add("total_production");
                    dt.Columns.Add("remark");
                    dt.Columns.Add("is_dqa_mqa_band");
                    dt.Columns.Add("img_name");
                    dt.Columns.Add("img_url");
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
                            if (item1["shoes_code"].ToString() == item2["shoes_code"].ToString() &&
                            item1["phase_date"].ToString() == item2["phase_date"].ToString() &&
                            item1["phase_creation_no"].ToString() == item2["phase_creation_no"].ToString() &&
                            item1["total_production"].ToString() == item2["total_production"].ToString() &&
                            item1["createdate"].ToString() == item2["createdate"].ToString() &&
                            item1["createtime"].ToString() == item2["createtime"].ToString())
                            {
                                DataRow drr = dt.NewRow();
                                drr["did"] = item2["did"];
                                drr["shoe_code"] = item2["shoe_code"];
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
                                // drr["person_in_charge"] = item2["person_in_charge"];
                                drr["image_guid"] = item2["image_guid"];
                                drr["phase_date"] = item2["phase_date"];
                                drr["phase_creation_no"] = item2["phase_creation_no"];
                                drr["phase_creation_name"] = item2["phase_creation_name"];
                                drr["is_dqa_mqa_band"] = item2["is_dqa_mqa_band"];
                                drr["total_production"] = item2["total_production"];
                                drr["itemnumber"] = i++;
                                drr["img_name"] = item2["img_name"];
                                drr["img_url"] = item2["img_url"];
                                drr["remark"] = item2["remark"];
                                drr["qa_risk_details_desc"] = item2["QA_RISK_DETAILS_DESC"];
                                drr["workshop_section_no"] = item2["workshop_section_no"];
                                drr["workshop_section_name"] = item2["workshop_section_name"];
                                dt.Rows.Add(drr);
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            UCTable uc = new UCTable(dt, this, null, "1");
                            this.flowLayoutPanelTable.Controls.Add(uc);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }

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

        private void button6_Click(object sender, EventArgs e)
        {
            FrmFileList add = new FrmFileList(FileView(), Program.Client.UploadUrl, Program.Client.UserToken);
            add.ShowDialog();
        }

        private void rdo_true_Click(object sender, EventArgs e)
        {
            GET_ShoeShapecenterView("1");
        }

        private void rdo_false_Click(object sender, EventArgs e)
        {
            GET_ShoeShapecenterView("0");
        }

        private void rdo_all_Click(object sender, EventArgs e)
        {
            GET_ShoeShapecenterView();
        }

        private void textBox7_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.textBox7;
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

        private void textBox8_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.textBox8;
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
