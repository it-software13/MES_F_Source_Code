using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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

namespace SJeMES_QA
{
    public partial class F_DQA_ShoeShape_trait_Insert : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string shoe_no = string.Empty;
        string did = string.Empty;
        DataTable dtt;
        public F_DQA_ShoeShape_trait_Insert()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitDateTimePicker(dateTimePicker1);
        }

        public F_DQA_ShoeShape_trait_Insert(string _shoe_no)
        {
            shoe_no = _shoe_no;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitDateTimePicker(dateTimePicker1);
            dateTimePicker1.Value = DateTime.Now;
        }

        public F_DQA_ShoeShape_trait_Insert(string _did, string _shoe_no)
        {
            shoe_no = _shoe_no;
            did = _did;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitDateTimePicker(dateTimePicker1);
            dateTimePicker1.Value = DateTime.Now;
        }

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddWT_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_shoe_no.Text))
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["choice_no"].Value = "";
                this.dataGridView1.Rows[index].Cells["choice_name"].Value = "";
                this.dataGridView1.Rows[index].Cells["qa_risk_desc"].Value = "";
                this.dataGridView1.Rows[index].Cells["qa_risk_category_code"].Value = "";
                this.dataGridView1.Rows[index].Cells["qa_risk_category_name"].Value = "";
                this.dataGridView1.Rows[index].Cells["art_codes"].Value = "";
                this.dataGridView1.Rows[index].Cells["bad_qty"].Value = "";
                this.dataGridView1.Rows[index].Cells["bad_rate"].Value = "";
                this.dataGridView1.Rows[index].Cells["measures"].Value = "";
                this.dataGridView1.Rows[index].Cells["person_in_charge"].Value = "";
                this.dataGridView1.Rows[index].Cells["image_guid"].Value = "";
                this.dataGridView1.Rows[index].Cells["workshop_section_name"].Value = "";
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the basic data first!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询各阶段样品记录添加页面的数据
        /// </summary>
        public void GetLastrecord_item()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("shoe_no", shoe_no);
                data.Add("did", did);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "GetLastrecord_item", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                if (dataGridView1.Rows.Count >= 0)
                {
                    dataGridView1.Rows.Clear();
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        if (string.IsNullOrWhiteSpace(did))
                        {
                            dgvr.Cells["itemid"].Value = dr["itemid"].ToString();
                            dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();
                            dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();
                            dgvr.Cells["qa_risk_desc"].Value = dr["qa_risk_desc"].ToString();
                            dgvr.Cells["qa_risk_category_code"].Value = dr["qa_risk_category_code"].ToString();
                            dgvr.Cells["qa_risk_category_name"].Value = dr["qa_risk_category_name"].ToString();
                            dgvr.Cells["art_codes"].Value = dr["art_codes"].ToString();
                            dgvr.Cells["image_guid"].Value = dr["image_guid"].ToString();
                            dgvr.Cells["measures"].Value = dr["measures"].ToString();
                            dgvr.Cells["measures_res"].Value = dr["MEASURES_RES"].ToString();
                            dgvr.Cells["person_in_charge"].Value = dr["person_in_charge"].ToString();
                            dgvr.Cells["is_dqa_mqa_band_val"].Value = dr["is_dqa_mqa_band"].ToString();
                            dgvr.Cells["workshop_section_no"].Value = dr["workshop_section_no"].ToString();
                            dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                            if (dr["is_dqa_mqa_band"].ToString() == "1")
                            {
                                dgvr.Cells["is_dqa_mqa_band"].Value = true;
                            }
                            else
                            {
                                dgvr.Cells["is_dqa_mqa_band"].Value = false;

                            }

                            dgvr.Cells["remark"].Value = dr["remark"].ToString();
                            dgvr.Cells["qa_risk_details_desc"].Value = dr["QA_RISK_DETAILS_DESC"].ToString();
                        }
                        else
                        {
                            dgvr.Cells["itemid"].Value = dr["itemid"].ToString();
                            dgvr.Cells["choice_no"].Value = dr["choice_no"].ToString();
                            dgvr.Cells["choice_name"].Value = dr["choice_name"].ToString();
                            dgvr.Cells["qa_risk_desc"].Value = dr["qa_risk_desc"].ToString();
                            dgvr.Cells["qa_risk_category_code"].Value = dr["qa_risk_category_code"].ToString();
                            dgvr.Cells["qa_risk_category_name"].Value = dr["qa_risk_category_name"].ToString();
                            dgvr.Cells["art_codes"].Value = dr["art_codes"].ToString();
                            dgvr.Cells["image_guid"].Value = dr["image_guid"].ToString();
                            dgvr.Cells["bad_qty"].Value = dr["bad_qty"].ToString();
                            dgvr.Cells["bad_rate"].Value = dr["bad_rate"].ToString();
                            dgvr.Cells["measures"].Value = dr["measures"].ToString();
                            dgvr.Cells["measures_res"].Value = dr["MEASURES_RES"].ToString();
                            dgvr.Cells["person_in_charge"].Value = dr["person_in_charge"].ToString();
                            dgvr.Cells["is_dqa_mqa_band_val"].Value = dr["is_dqa_mqa_band"].ToString();
                            dgvr.Cells["workshop_section_no"].Value = dr["workshop_section_no"].ToString();
                            dgvr.Cells["workshop_section_name"].Value = dr["workshop_section_name"].ToString();
                            if (dr["is_dqa_mqa_band"].ToString() == "1")
                            {
                                dgvr.Cells["is_dqa_mqa_band"].Value = true;
                            }
                            else
                            {
                                dgvr.Cells["is_dqa_mqa_band"].Value = false;

                            }
                            dgvr.Cells["remark"].Value = dr["remark"].ToString();
                            dgvr.Cells["qa_risk_details_desc"].Value = dr["QA_RISK_DETAILS_DESC"].ToString();
                        }
                        i++;
                    }
                }

                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                if (!string.IsNullOrWhiteSpace(did))
                {
                    if (dt2.Rows.Count > 0)
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(dt2.Rows[0]["phase_date"]);
                        cbo_type.SelectedValue = dt2.Rows[0]["phase_creation_no"];
                        txt_shoe_no.Text = dt2.Rows[0]["total_production"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_DQA_ShoeShape_trait_Insert_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            #region comboBox2下拉框
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("shoes_no", shoe_no);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.DQA_ShoeShape",//类名
                                        "GetGd",//方法名
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

            dtt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

            comboBox2.DataSource = dtt;
            if (dtt != null && dtt.Rows.Count > 0)
            {
                comboBox2.DisplayMember = "value";
                comboBox2.ValueMember = "code";
            }
            comboBox2.Visible = false;

            #endregion

            Getphase_creation();

            textBox2.LostFocus += new EventHandler(BLS);
            txt_shoe_no.LostFocus += new EventHandler(SCZS);
            GetLastrecord_item();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;

            if (!string.IsNullOrWhiteSpace(did))
            {
                btnAddWT.Visible = false;
            }


        }

        /// <summary>
        /// 各阶段样品记录添加页面查询阶段
        /// </summary>
        /// <returns></returns>
        public void Getphase_creation()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.DQA_ShoeShape",//类名
                                            "Getphase_creation",//方法名
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
                    cbo_type.DataSource = dt;
                    cbo_type.DisplayMember = "phase_creation_name";
                    cbo_type.ValueMember = "phase_creation_no";
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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

        /// <summary>
        /// 各阶段样品记录添加页面查询负责人
        /// </summary>
        /// <returns></returns>
        public string Getperson()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("STAFF_NO", textBox4.Text.Trim());
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.DQA_ShoeShape",//类名
                                        "Getperson",//方法名
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
            string a = string.Empty;
            if (dt.Rows.Count > 0)
                a = dt.Rows[0]["STAFF_NO"].ToString();
            else
                a = "No such person!";

            return a;

        }

        /// <summary>
        /// 各阶段样品记录添加页面查询图片
        /// </summary>
        /// <returns></returns>
        public DataTable Getimage_guid(string image_guid)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("image_guid", image_guid);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.DQA_ShoeShape",//类名
                                        "Getimage_guid",//方法名
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
            return dt;
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "is_dqa_mqa_band") // 
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["is_dqa_mqa_band"].Value))
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["is_dqa_mqa_band"].Value = false;
                        dataGridView1.Rows[e.RowIndex].Cells["is_dqa_mqa_band_val"].Value = "0";
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["is_dqa_mqa_band"].Value = true;
                        dataGridView1.Rows[e.RowIndex].Cells["is_dqa_mqa_band_val"].Value = "1";
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "art_codes") // 
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    F_DQA_ShoeShape_trait_Insert_ART update = new F_DQA_ShoeShape_trait_Insert_ART(shoe_no, dataGridView1.Rows[e.RowIndex].Cells["art_codes"].Value.ToString(), true);
                    update.ShowDialog();
                    if (update.Tag != null)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["art_codes"].Value = update.Tag.ToString();
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "choice_no") // 
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    string atr_no_list = dataGridView1.Rows[e.RowIndex].Cells["art_codes"].Value.ToString();
                    F_DQA_ShoeShape_trait_Insert_material update = new F_DQA_ShoeShape_trait_Insert_material(atr_no_list);
                    update.ShowDialog();
                    if (update.Tag != null)
                    {
                        string[] choice = update.Tag.ToString().Split(',');
                        dataGridView1.Rows[e.RowIndex].Cells["choice_no"].Value = choice[0];
                        dataGridView1.Rows[e.RowIndex].Cells["choice_name"].Value = choice[1];
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_details_desc") // 品质风险细项描述
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
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
                if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_desc") // 品质风险描述 
                {
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["qa_risk_desc"].Value is null ? "" : dataGridView1.CurrentRow.Cells["qa_risk_desc"].Value.ToString();
                    string qa_risk_desc = aa == "" ? "" : aa;
                    textBox1.Text = qa_risk_desc; //品质风险描述

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                    textBox1.Focus();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "bad_qty") // 不良数
                {
                    textBox1.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["bad_qty"].Value is null ? "" : dataGridView1.CurrentRow.Cells["bad_qty"].Value.ToString();
                    string bad_qty = aa == "" ? "" : aa;
                    textBox2.Text = bad_qty; //不良数

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox2.Visible = true;
                    textBox2.Focus();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "measures") // 改善措施&行动方案
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["measures"].Value is null ? "" : dataGridView1.CurrentRow.Cells["measures"].Value.ToString();
                    string measures = aa == "" ? "" : aa;
                    textBox3.Text = measures; //改善措施&行动方案

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox3.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox3.Visible = true;
                    textBox3.Focus();

                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "measures_res") // 改善措施结果
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["measures_res"].Value is null ? "" : dataGridView1.CurrentRow.Cells["measures_res"].Value.ToString();
                    string measures_res = aa == "" ? "" : aa;
                    DataTable dt_tval = new DataTable();
                    dt_tval.Columns.Add("measures_res_code");
                    dt_tval.Columns.Add("measures_res_name");
                    DataRow drr1 = dt_tval.NewRow();
                    drr1["measures_res_code"] = "";
                    drr1["measures_res_name"] = "";
                    dt_tval.Rows.Add(drr1);
                    DataRow drr2 = dt_tval.NewRow();
                    drr2["measures_res_code"] = "Improved";//已改善
                    drr2["measures_res_name"] = "Improved";//已改善
                    dt_tval.Rows.Add(drr2);
                    DataRow drr3 = dt_tval.NewRow();
                    drr3["measures_res_code"] = "To be Improved";//待改善
                    drr3["measures_res_name"] = "To be Improved";//待改善
                    dt_tval.Rows.Add(drr3);
                    comboBox3.DataSource = dt_tval;
                    if (dt_tval != null && dt_tval.Rows.Count > 0)
                    {
                        comboBox3.DisplayMember = "measures_res_name";
                        comboBox3.ValueMember = "measures_res_code";
                    }
                    comboBox3.Text = measures_res;

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    comboBox3.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    comboBox3.Visible = true;

                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "qa_risk_category_name") // combobox显示条件 
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox2.Visible = false;
                    DataTable dt_tval = Getrisk_category();
                    comboBox1.DataSource = dt_tval;
                    if (dt_tval != null && dt_tval.Rows.Count > 0)
                    {
                        comboBox1.DisplayMember = "qa_risk_category_name";
                        comboBox1.ValueMember = "qa_risk_category_code";
                    }
                    string qa_risk_category_name = dataGridView1.CurrentRow.Cells["qa_risk_category_name"].Value.ToString(); //对combobox赋值
                    comboBox1.Text = qa_risk_category_name;

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //Get cell position
                    comboBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //Reposition the combobox. There is coordinate position conversion in the middle. 
                    comboBox1.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "remark") // 备注
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["remark"].Value is null ? "" : dataGridView1.CurrentRow.Cells["remark"].Value.ToString();
                    string measures = aa == "" ? "" : aa;
                    textBox4.Text = measures; //备注

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox4.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox4.Visible = true;
                    textBox4.Focus();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "workshop_section_name") // 工段
                {
                    string workshop_section_name = dataGridView1.CurrentRow.Cells["workshop_section_name"].Value.ToString();
                    //if (dataGridView1.CurrentRow.Cells["workshop_section_name"].Value == )
                    //    workshop_section_name = ""; //对combobox赋值
                    //else
                    //    workshop_section_name = dataGridView1.CurrentRow.Cells["workshop_section_name"].Value.ToString();

                    comboBox2.Text = workshop_section_name;

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    comboBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    comboBox2.Visible = true;
                    comboBox2.Focus();
                }
                else
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    comboBox2.Visible = false;
                }

               
                //else
                //{
                //    //textBox1.Visible = false;
                //    //textBox2.Visible = false;
                //    //textBox3.Visible = false;
                //    //textBox4.Visible = false;
                //    //comboBox1.Visible = false;
                //    comboBox2.Visible = false;
                //}

                if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox3.Visible = false;
                    textBox4.Visible = false;
                    comboBox1.Visible = false;
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("selectImg"))
                    {
                        var currRowFileDt = Getimage_guid(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString());
                        FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", false);
                        add.ShowDialog();
                        int i = 0;
                        string image_guids = string.Empty;
                        foreach (DataRow item in currRowFileDt.Rows)
                        {
                            image_guids += item["guid"];
                            if (i < currRowFileDt.Rows.Count - 1)
                            {
                                image_guids += ",";
                            }
                            i++;
                        }
                        dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = image_guids;

                        //SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(Getimage_guid(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString()), null, "");
                        //fil.ShowDialog();
                    }
                    else if (cell.CurrentItem.Equals("UploadIMG"))
                    {
                        //创建文件弹出选择窗口（包括文件名）对象
                        OpenFileDialog ofd = new OpenFileDialog();
                        //判断选择的路径
                        string path = string.Empty;
                        ofd.Title = "Please select a folder";
                        ofd.Filter = "Image file(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
                        ofd.Multiselect = true;
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var item in ofd.FileNames)
                            {
                                SafeFileName = System.IO.Path.GetFileName(item);
                                filePath = item;
                                UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                                if (res.IsSuccess)
                                {
                                    var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                    if (dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString()))
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value + "," + resultDIC["guid"].ToString();
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value = resultDIC["guid"].ToString();
                                    }

                                }
                            }

                            MessageBox.Show("上传成功");

                        }
                    }
                    else if (cell.CurrentItem.Equals("delete"))
                    {
                        DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Delete shoe type quality status", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (string.IsNullOrWhiteSpace(did))
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
                            if (dr == DialogResult.OK)
                            {
                                string itemid = dataGridView1.Rows[e.RowIndex].Cells["itemid"].Value.ToString();
                                Deleterecord_item(itemid);
                            }
                        }
                    }
                }
            }
            else
            {
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                comboBox3.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
            }
            
        }

        /// <summary>
        /// 编辑鞋型品质管理页面删除
        /// </summary>
        public void Deleterecord_item(string itemid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("did", did);
                data.Add("itemid", itemid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "Deleterecord_item", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    GetLastrecord_item();
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

        public void BLS(object sender, System.EventArgs e)
        {
            try
            {
                int index = dataGridView1.Rows.Count;
                double BL = 0;
                double SCZS = txt_shoe_no.Text == "" ? 0 : Convert.ToDouble(txt_shoe_no.Text);
                //for (int i = 0; i < index; i++)
                //{

                if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value == null)
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value = "";
                }
                if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value.ToString() != "" &&
                        !IsNumberic(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value.ToString())

                    )
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value = "";
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_rate"].Value = "";
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please enter a numeric type！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);

                    //break;
                }
                string aa = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value is null ? "0" : dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value.ToString();
                BL = aa == "" ? 0 : Convert.ToDouble(aa);
                //if (BL != 0)
                //{
                if (SCZS == 0)
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value = "";
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_rate"].Value = "";
                    txt_shoe_no.Focus();
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("The production total cannot be 0 or empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);

                }
                else if (BL > SCZS)
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_qty"].Value = "";
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_rate"].Value = "";
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("The bad quantity cannot be greater than the total production!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["bad_rate"].Value = Math.Round((BL / SCZS * 100), 2).ToString() + "%";
                }
                //}
                //}
            }
            catch (Exception)
            { }

        }

        public void SCZS(object sender, System.EventArgs e)
        {
            int index = dataGridView1.Rows.Count;
            double BL = 0;
            double SCZS = txt_shoe_no.Text == "" ? 0 : Convert.ToDouble(txt_shoe_no.Text);
            for (int i = 0; i < index; i++)
            {
                string aa = dataGridView1.Rows[i].Cells["bad_qty"].Value is null ? "0" : dataGridView1.Rows[i].Cells["bad_qty"].Value.ToString();
                BL = aa == "" ? 0 : Convert.ToDouble(aa);
                if (BL != 0)
                {
                    if (BL > SCZS)
                    {
                        dataGridView1.Rows[i].Cells["bad_qty"].Value = "";
                        dataGridView1.Rows[i].Cells["bad_rate"].Value = "";
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("The bad quantity cannot be greater than the total production!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["bad_rate"].Value = Math.Round((BL / SCZS * 100), 2).ToString() + "%";
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox1.Text.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox2.Text.ToString();
        }

        private void txt_shoe_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('-'))
            {
                if ((sender as TextBox).Text != "")
                {
                    e.Handled = true;
                }
            }
            //第1位是负号时候、第2位小数点不可
            if (((TextBox)sender).Text == "-" && e.KeyChar == (char)('.'))
            {
                e.Handled = true;
            }
            //负号只能1次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            //小数点（最大到2位）   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //光标在小数点右侧时候判断  
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox3.Text.ToString();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = comboBox1.Text;
            dataGridView1.CurrentRow.Cells["qa_risk_category_code"].Value = comboBox1.SelectedValue.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox4.Text.ToString();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (Getperson() != "查无此人!")
            //    {
            //        dataGridView1.CurrentCell.Value = Getperson();
            //        textBox4.Visible = false;
            //    }
            //    else
            //        MessageBox.Show("查无此人!");
            //}
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存各阶段样品记录
        /// </summary>
        public void Editqa_record()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("did", did);
                data.Add("shoes_code", shoe_no);
                data.Add("phase_date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                data.Add("phase_creation_no", cbo_type.SelectedValue.ToString());
                data.Add("total_production", txt_shoe_no.Text.Trim());

                data.Add("qa_record_item", GetDgvToTable(dataGridView1));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.DQA_ShoeShape", "Editqa_record", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    //this.Close();
                }
                else
                    throw new Exception("save failed！" + j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("save failed！" + ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) || string.IsNullOrEmpty(cbo_type.SelectedValue.ToString()) || string.IsNullOrEmpty(txt_shoe_no.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Basic data cannot be empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //材料/工序
                    string choice_no = dataGridView1.Rows[i].Cells["choice_no"].Value == null ? "" : dataGridView1.Rows[i].Cells["choice_no"].Value.ToString();
                    //品质风险类别
                    string qa_risk_category_code = dataGridView1.Rows[i].Cells["qa_risk_category_code"].Value == null ? "" : dataGridView1.Rows[i].Cells["qa_risk_category_code"].Value.ToString();
                    //相关art
                    string art_codes = dataGridView1.Rows[i].Cells["art_codes"].Value == null ? "" : dataGridView1.Rows[i].Cells["art_codes"].Value.ToString();
                    //不良数
                    string bad_qty = dataGridView1.Rows[i].Cells["bad_qty"].Value == null ? "" : dataGridView1.Rows[i].Cells["bad_qty"].Value.ToString();
                    //不良率
                    string bad_rate = dataGridView1.Rows[i].Cells["bad_rate"].Value == null ? "" : dataGridView1.Rows[i].Cells["bad_rate"].Value.ToString();
                    //负责人
                    //string person_in_charge = dataGridView1.Rows[i].Cells["person_in_charge"].Value == null ? "" : dataGridView1.Rows[i].Cells["person_in_charge"].Value.ToString();
                    //{
                    //    string msg = SJeMES_Framework.Common.UIHelper.UImsg("表格数据不能为空!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    //    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    //    return;
                    //}
                }
                if (dataGridView1.Rows.Count <= 0)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Question cannot be empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                Editqa_record();
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "save failed！" + msg);
            }
        }
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
        /// 是否为数字类型
        /// </summary>
        /// <param name="oText"></param>
        /// <returns></returns>
        private bool IsNumberic(string oText)
        {
            try
            {
                decimal var1 = Convert.ToDecimal(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('-'))
            {
                if ((sender as TextBox).Text != "")
                {
                    e.Handled = true;
                }
            }
            //第1位是负号时候、第2位小数点不可
            if (((TextBox)sender).Text == "-" && e.KeyChar == (char)('.'))
            {
                e.Handled = true;
            }
            //负号只能1次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            //小数点（最大到2位）   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //光标在小数点右侧时候判断  
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
            {
                dataGridView1.CurrentCell.Value = comboBox1.Text;
                dataGridView1.CurrentRow.Cells["workshop_section_no"].Value = comboBox2.SelectedValue.ToString();
                dataGridView1.CurrentRow.Cells["workshop_section_name"].Value = comboBox2.Text.ToString();
            }
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = comboBox3.Text;
            dataGridView1.CurrentRow.Cells["measures_res"].Value = comboBox3.SelectedValue.ToString();
        }
    }
}
