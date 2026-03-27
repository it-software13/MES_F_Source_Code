using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using RPT_WMS_Stoc_Matching;
using SJeMES_BDM;
using SJeMES_Control_Library.Forms;
using SJeMES_Control_Library.VideoCapture;
using SJeMES_Framework.WebAPI;
using SjeMES_QCM_Ex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SJeMES_IQC.F_IQC_VWarehouse_Main;

namespace SJeMES_TQC
{
    public partial class TQC_Task_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string task_no = string.Empty;
        string ck = string.Empty;//查看
        string task_state = string.Empty;
        public TQC_Task_Edit()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            comboBox2.DataSource = LoadStage();
            comboBox2.DisplayMember = "PHASE_CREATION_NAME";
            comboBox2.ValueMember = "PHASE_CREATION_NAME";
            Control_initialization();
            GetTQC_Task_Edit_Com_List();
        }

        public TQC_Task_Edit(string _task_no)
        {
            InitializeComponent();
            task_no = _task_no;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            comboBox2.DataSource = LoadStage();
            comboBox2.DisplayMember = "PHASE_CREATION_NAME";
            comboBox2.ValueMember = "PHASE_CREATION_NAME";

            GetTQC_Task_Edit_Com_List();
            GetTQC_Task_Main(task_no);
            button8.Visible = false;
            this.KeyPreview = true;
            button7.Visible = false;
            button16.Visible = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            comboBox3.Enabled = false;
            textBox6.Enabled = false;
            button20.Visible = false;
            if (label14.Text == "open")
            {
                button15.Visible = false;
            }
            else
            {
                button14.Visible = false;
            }
            //
            if (GetClaimDetails())
            {
                button21.ForeColor = Color.Red;
            }
        }

        DataTable LoadStage()
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable = Program.Client.GetDT("select PHASE_CREATION_NAME from bdm_phase_creation_m");
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = "";
                dataTable.Rows.InsertAt(dataRow, 0);
            }
            catch (Exception ex)
            {
            }
            return dataTable;
        }
        public TQC_Task_Edit(string _task_no, SJeMES_Framework.Class.ClientClass _Client)
        {
            InitializeComponent();
            Program.Client = _Client;
            task_no = _task_no;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            GetTQC_Task_Edit_Com_List();
            comboBox2.DataSource = LoadStage();
            comboBox2.DisplayMember = "PHASE_CREATION_NAME";
            comboBox2.ValueMember = "PHASE_CREATION_NAME";
            GetTQC_Task_Main(task_no);
            button8.Visible = false;
            this.KeyPreview = true;
            button7.Visible = false;
            button16.Visible = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            comboBox3.Enabled = false;
            textBox6.Enabled = false;
            button20.Visible = false;
        }

        public TQC_Task_Edit(string _task_no, string _ck)
        {
            InitializeComponent();
            task_no = _task_no;
            ck = _ck;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            GetTQC_Task_Edit_Com_List();
            GetTQC_Task_Main(task_no);

            comboBox2.DataSource = LoadStage();
            comboBox2.DisplayMember = "PHASE_CREATION_NAME";
            comboBox2.ValueMember = "PHASE_CREATION_NAME";
            button8.Visible = false;
            this.KeyPreview = true;
            button7.Visible = false;
            button16.Visible = false;
            textBox3.Enabled = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            button15.Visible = false;
            label14.Visible = false;
            button1.Visible = false;
            textBox6.Enabled = false;
            comboBox3.Enabled = false;
            //button6.Visible = false;
            comboBox1.Enabled = false;
            button19.Visible = false;
            //button20.Visible = false;
        }

        /// <summary>
        /// 新增时初始化
        /// </summary>
        public void Control_initialization()
        {
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            label7.Visible = false;
            button17.Visible = false;

            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            button15.Visible = false;

            label32.Text = "";
            label33.Text = "";
            label34.Text = "";
            label35.Text = "";
            label36.Text = "";
            label37.Text = "";
            label39.Text = "";
            label42.Text = "";
            label43.Text = "";
            label44.Text = "";

            label41.Text = "";
            label46.Text = "";
            label52.Text = "";

            label14.Visible = false;

            button19.Visible = false;
            dataGridView1.Visible = false;
        }

        private void TQC_Task_Edit_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //button8.Visible = false;// changed
            //this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            GetShoe_no_jijie();

        }

        /// <summary>
        /// tqc创建页面查询art
        /// </summary>
        /// <returns></returns>
        public void GetTQC_Task_Edit_Com_List()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "GetTQC_Task_Edit_Com_List",//方法名
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
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["eq_info_name"] = "---请选择---";
            if (dt.Rows.Count > 0)
            {
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "eq_info_name";
                comboBox1.ValueMember = "eq_info_no";
            }
            comboBox1.SelectedIndex = dt.Rows.Count - 1;
            DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
            if (dt2.Rows.Count > 0)
            {
                comboBox3.DataSource = dt2;
                comboBox3.DisplayMember = "WORKSHOP_SECTION_NAME";
                comboBox3.ValueMember = "WORKSHOP_SECTION_NO";
            }
        }

        /// <summary>
        /// tqc创建页面根据art查询鞋型和季节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetShoe_no_jijie()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("mer_po", textBox3.Text);
                data.Add("art", textBox10.Text);
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetShoe_no_jijie",//方法名
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
                    textBox1.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();
                    textBox2.Text = dt.Rows[0]["SHOE_NO"].ToString();
                    textBox7.Text = dt.Rows[0]["name_t"].ToString();
                    label8.Text = dt.Rows[0]["user_section"].ToString();
                    label9.Text = dt.Rows[0]["USER_IN_SHOECHARGE"].ToString();
                    label10.Text = dt.Rows[0]["user_technical"].ToString();
                    label11.Text = dt.Rows[0]["qa_principal"].ToString();
                    label12.Text = dt.Rows[0]["style_seq"].ToString();
                    label13.Text = dt.Rows[0]["develop_type"].ToString();
                    textBox10.Text = dt.Rows[0]["PROD_NO"].ToString();
                    textBox8.Text = dt.Rows[0]["MOLD_NO"].ToString();
                    textBox4.Text = dt.Rows[0]["workorder_no"].ToString();
                    textBox5.Text = dt.Rows[0]["se_id"].ToString();
                    //textBox2.Text = dt.Rows[0]["SHOE_NO"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["file_url"].ToString()))
                    {
                        try
                        {
                            var webC = new System.Net.WebClient();
                            string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["file_url"].ToString());
                            Image image = new Bitmap(webC.OpenRead(url));
                            pictureBox1.Image = image;
                        }
                        catch { }
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
        /// tqc创建页面根据art查询鞋型和季节
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetShoe_no_jijieByART()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("prod_no", textBox10.Text);
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetShoe_no_jijiebyART",//方法名
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
                    textBox1.Text = dt.Rows[0]["DEVELOP_SEASON"].ToString();
                    textBox2.Text = dt.Rows[0]["SHOE_NO"].ToString();
                    textBox7.Text = dt.Rows[0]["name_t"].ToString();
                    label8.Text = dt.Rows[0]["user_section"].ToString();
                    label9.Text = dt.Rows[0]["USER_IN_SHOECHARGE"].ToString();
                    label10.Text = dt.Rows[0]["user_technical"].ToString();
                    label11.Text = dt.Rows[0]["qa_principal"].ToString();
                    label12.Text = dt.Rows[0]["style_seq"].ToString();
                    label13.Text = dt.Rows[0]["develop_type"].ToString();
                    textBox10.Text = dt.Rows[0]["PROD_NO"].ToString();
                    textBox8.Text = dt.Rows[0]["MOLD_NO"].ToString();
                    textBox4.Text = dt.Rows[0]["workorder_no"].ToString();
                    textBox5.Text = dt.Rows[0]["se_id"].ToString();
                    //textBox2.Text = dt.Rows[0]["SHOE_NO"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["file_url"].ToString()))
                    {
                        try
                        {
                            var webC = new System.Net.WebClient();
                            string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["file_url"].ToString());
                            Image image = new Bitmap(webC.OpenRead(url));
                            pictureBox1.Image = image;
                        }
                        catch { }
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

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetDepartment();
            }
        }

        /// <summary>
        /// tqc创建页面根据产线查询部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetDepartment()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("department_code", textBox6.Text);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "GetDepartment",//方法名
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
                textBox9.Text = dt.Rows[0]["udf07"].ToString();
            }
            else
            {
                MessageBox.Show("There is no such production line!");
                textBox6.Text = "";
                textBox9.Text = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GetDepartment();
            string po = textBox3.Text;//po
            string cx = textBox6.Text;//产线
            string gd = comboBox3.SelectedValue.ToString();//工段
            if (string.IsNullOrWhiteSpace(po) || string.IsNullOrWhiteSpace(cx) || string.IsNullOrWhiteSpace(gd))
            {
                MessageBox.Show("Basic information cannot be empty!");
                return;
            }
            TQC_Task_Edit_Insert();
            if (GetClaimDetails())
            {
                button21.ForeColor = Color.Red;
            }
            button15.Visible = false;
        }

        public Boolean GetClaimDetails()
        {
            string Art = textBox10.Text;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Art", Art);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "GetClaimData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// tqc创建页面生成任务
        /// </summary>
        public void TQC_Task_Edit_Insert()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("develop_season", textBox1.Text);//条件 季度
                data.Add("shoe_no", textBox2.Text);//条件 鞋型
                data.Add("prod_no", textBox10.Text);//条件 art
                data.Add("workshop_section_no", comboBox3.SelectedValue.ToString());//条件 工段编号
                data.Add("department", textBox9.Text);//条件 部门
                data.Add("production_line_code", textBox6.Text);//条件 产线
                data.Add("eq_info_no", comboBox1.SelectedValue.ToString());//条件 机台
                data.Add("mold_no", textBox8.Text);//条件 模号
                data.Add("mer_po", textBox3.Text);//条件 po
                data.Add("workorderno", textBox4.Text);//条件 制令
                data.Add("se_id", textBox5.Text);//条件 销售订单号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_Task_Edit_Insert", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                task_no = dic["task_no"].ToString();

                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Generated successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    GetTQC_Task_Main(task_no);
                    button9.Visible = true;
                    button10.Visible = true;
                    button11.Visible = true;
                    button12.Visible = true;
                    button13.Visible = true;
                    button14.Visible = true;
                    button15.Visible = true;
                    label14.Visible = true;
                    label14.Text = "open";
                    label14.ForeColor = Color.Green;
                    label7.Visible = true;
                    button17.Visible = true;
                    button8.Visible = false;
                    this.KeyPreview = true;
                    button7.Visible = false;
                    textBox3.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox3.Enabled = false;
                    textBox6.Enabled = false;
                    button19.Visible = true;
                    dataGridView1.Visible = true;
                    button16.Visible = false;
                    button20.Visible = false;
                }
                else
                    throw new Exception(ret.ErrMsg.ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //label14.Text = "关";
            //label14.ForeColor = Color.Red;
            //button9.Visible = false;
            //button10.Visible = false;
            //button11.Visible = false;
            //button12.Visible = false;
            //button13.Visible = false;
            using (TQC_Task_Edit_Reason t = new TQC_Task_Edit_Reason(task_no, "1"))
            {
                t.ShowDialog();
            }
            GetTQC_Task_Main(task_no);
            button14.Visible = false;
            button15.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //label14.Text = "开";
            //label14.ForeColor = Color.Green;
            //button9.Visible = true;
            //button10.Visible = true;
            //button11.Visible = true;
            //button12.Visible = true;
            //button13.Visible = true;
            TQC_Task_Edit_state(task_no, "0");
            button15.Visible = false;
            button14.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string workshop_section_no = comboBox3.SelectedValue.ToString();

            using (F_BDM_KetCap_Main f = new F_BDM_KetCap_Main(workshop_section_no, Program.Client))
            {
                f.ShowDialog();
            }
        }

        /// <summary>
        /// tqc home page query
        /// </summary>
        public void GetTQC_Task_Main(string task_no)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetTask_Edit",//方法名
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
                DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());//当前数据
                if (dt1.Rows.Count > 0)
                {
                    label32.Text = dt1.Rows[0]["total"].ToString();//检验总数
                    label33.Text = dt1.Rows[0]["qualified"].ToString();//合格总数
                    label34.Text = dt1.Rows[0]["bnum"].ToString();//label34
                    label39.Text = dt1.Rows[0]["First_Time_UnQualified"].ToString();//不合格提交次数
                    label35.Text = Math.Round((Convert.ToDecimal(dt1.Rows[0]["totalpass"]) * 100), 2).ToString() + "%";//产线总合格率
                    label36.Text = dt1.Rows[0]["fx"].ToString();//返修总数
                    label37.Text = Math.Round((Convert.ToDecimal(dt1.Rows[0]["rftpass"]) * 100), 2).ToString() + "%";//首次合格率
                    label44.Text = dt1.Rows[0]["fx"].ToString();
                    label43.Text = dt1.Rows[0]["scbhhzs"].ToString();
                    //label43.Text = dt1.Rows[0]["fx"].ToString();
                    label42.Text = Math.Round((Convert.ToDecimal(dt1.Rows[0]["fxrft"]) * 100), 2).ToString() + "%";

                    label52.Text = dt1.Rows[0]["fxhgs"].ToString();//返修合格数
                    label41.Text = dt1.Rows[0]["schgzs"].ToString();//首次合格总数
                    label46.Text = dt1.Rows[0]["scbhhzs"].ToString();//首次不合格总数
                    textBox11.Text = (Convert.ToInt32(dt1.Rows[0]["schgzs"].ToString()) + Convert.ToInt32(dt1.Rows[0]["scbhhzs"].ToString())).ToString();//首次检验总数
                }

                DataTable dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());//基本信息
                if (dt2.Rows.Count > 0)
                {
                    textBox1.Text = dt2.Rows[0]["develop_season"].ToString();
                    textBox2.Text = dt2.Rows[0]["shoe_no"].ToString();
                    textBox7.Text = dt2.Rows[0]["name_t"].ToString();
                    textBox10.Text = dt2.Rows[0]["prod_no"].ToString();
                    comboBox3.SelectedValue = dt2.Rows[0]["workshop_section_no"].ToString();
                    textBox9.Text = dt2.Rows[0]["department"].ToString();
                    textBox6.Text = dt2.Rows[0]["department_name"].ToString();
                    comboBox1.SelectedValue = dt2.Rows[0]["eq_info_no"].ToString();
                    textBox8.Text = dt2.Rows[0]["mold_no"].ToString();

                    textBox3.Text = dt2.Rows[0]["mer_po"].ToString();
                    textBox4.Text = dt2.Rows[0]["workorderno"].ToString();
                    textBox5.Text = dt2.Rows[0]["se_id"].ToString();

                    if (dt2.Rows[0]["task_state"].ToString() == "0")
                    {
                        //button9.Visible = true;
                        //button10.Visible = true;
                        //button11.Visible = true;
                        //button12.Visible = true;
                        //button13.Visible = true;
                        label14.Text = "open";
                        label14.ForeColor = Color.Green;
                    }
                    else if (dt2.Rows[0]["task_state"].ToString() == "1")
                    {
                        //button9.Visible = false;
                        //button10.Visible = false;
                        //button11.Visible = false;
                        //button12.Visible = false;
                        //button13.Visible = false;
                        label14.Text = "close";
                        label14.ForeColor = Color.Red;
                    }
                }

                DataTable dt3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data3"].ToString());//键帽信息
                InitialDgvData(dt3);
                if (dt3.Rows.Count <= 0)
                {
                    MessageBox.Show($@"There is no key information for this section!");
                }


                //The rummaging task displays box data
                task_state = dt2.Rows[0]["task_state"].ToString();
                if (task_state == "3" || task_state == "4" || task_state == "5")
                {
                    label55.Visible = true;
                    textBox12.Visible = true;

                    //AQL rummage quantity
                    label56.Visible = true;
                    textBox13.Visible = true;
                    textBox13.Location = new Point(comboBox2.Location.X, comboBox2.Location.Y + 35);
                    label56.Location = new Point(textBox13.Location.X - label56.Width - 10, textBox13.Location.Y + 6);

                    //Production line
                    label57.Visible = true;
                    label57.Location = new Point(label23.Location.X, label23.Location.Y);
                    textBox14.Visible = true;
                    textBox14.Location = new Point(textBox6.Location.X, textBox6.Location.Y);

                    groupBox4.Visible = true;

                    label23.Visible = false;
                    textBox6.Visible = false;


                    textBox12.Text = dt2.Rows[0]["aql_task_no"].ToString();
                    textBox13.Text = dt2.Rows[0]["aql_rework_quantity"].ToString();
                    textBox14.Text = dt2.Rows[0]["production_line_code"].ToString();

                    DataTable dt4 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data4"].ToString());//点箱数据
                    if (dt4.Rows.Count > 0)
                    {
                        dataGridView2.Rows.Clear();
                        int i = 0;
                        foreach (DataRow dr in dt4.Rows)
                        {
                            dataGridView2.Rows.Add();
                            DataGridViewRow dgvr = dataGridView2.Rows[i];
                            dgvr.Cells["case_no"].Value = dr["case_no"].ToString();
                            dgvr.Cells["size"].Value = dr["cr_size"].ToString();
                            dgvr.Cells["po_qty"].Value = dr["po_qty"].ToString();
                            dgvr.Cells["se_qty"].Value = dr["se_qty"].ToString();
                            i++;
                        }
                    }
                }







                DataTable dtut = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dtut"].ToString());//不常见项目
                if (dtut.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                    int i = 0;
                    foreach (DataRow dr in dtut.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["id"].Value = dr["id"].ToString();
                        dgvr.Cells["inspection_code"].Value = dr["inspection_code"].ToString();
                        dgvr.Cells["inspection_name"].Value = dr["inspection_name"].ToString();
                        dgvr.Cells["shortcut_key"].Value = dr["shortcut_key"].ToString();
                        dgvr.Cells["qc_type"].Value = dr["qc_type"].ToString();
                        dgvr.Cells["judgment_criteria"].Value = dr["judgment_criteria"].ToString();
                        dgvr.Cells["standard_value"].Value = dr["standard_value"].ToString();
                        dgvr.Cells["num"].Value = dr["num"].ToString();
                        dgvr.Cells["ifclick"].Value = "false";
                        dgvr.Cells["imglist"].Value = dr["imglist"].ToString();
                        i++;
                    }
                }

                string check_res = dic["check_res"].ToString();//检验结果
                if (check_res == "FAIL")
                {
                    button17.Text = check_res;
                    button17.ForeColor = Color.Red;
                }
                else if (check_res == "PASS")
                {
                    button17.Text = check_res;
                    button17.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public List<DataGridView> dgv_list = new List<DataGridView>();
        /// <summary>
        /// 动态生成dgv控件
        /// </summary>
        public void InitialDgvData(DataTable dt)
        {
            this.splitContainer4.Panel1.Controls.Clear();
            this.splitContainer4.Panel2.Controls.Clear();

            dgv_list = new List<DataGridView>();
            //1.调用接口

            //2.根据接口返回结果条数，做循环，假设返回15条。
            //一个dgv最多10条，那就需要两个dgv
            int resCount = dt.Rows.Count;
            int dgvRowCount = 10;//dgv最大行数
            int dgvCount = (resCount + dgvRowCount - 1) / dgvRowCount;//计算dgv个数

            for (int i = 0; i < dgvCount; i++)
            {
                #region 建dgv
                DataGridView dataGridView = new DataGridView();
                dataGridView.Name = $@"dgv_{i}";
                dataGridView.Dock = DockStyle.Fill;
                var col1 = new DataGridViewColumn();
                var col2 = new DataGridViewColumn();
                var col3 = new DataGridViewColumn();
                var col4 = new DataGridViewColumn();
                var col5 = new DataGridViewColumn();
                var col6 = new DataGridViewColumn();
                var col7 = new DataGridViewColumn();
                DataGridViewDisableButtonColumn btn1 = new DataGridViewDisableButtonColumn();
                DataGridViewDisableButtonColumn btn2 = new DataGridViewDisableButtonColumn();
                DataGridViewDisableButtonColumn btn3 = new DataGridViewDisableButtonColumn();
                DataGridViewDisableButtonColumn btn4 = new DataGridViewDisableButtonColumn();
                var col8 = new DataGridViewColumn();
                var col9 = new DataGridViewColumn();
                var col10 = new DataGridViewColumn();
                //要插入列的类型
                col1.CellTemplate = new DataGridViewTextBoxCell();
                col1.Name = "id";
                col1.HeaderText = "id";
                col1.Visible = false;
                col1.ReadOnly = true;
                dataGridView.Columns.Insert(0, col1);
                col2.CellTemplate = new DataGridViewTextBoxCell();
                col2.Name = "inspection_code";
                col2.HeaderText = "Test item code";
                col2.Visible = false;
                col2.ReadOnly = true;
                dataGridView.Columns.Insert(1, col2);
                col3.CellTemplate = new DataGridViewTextBoxCell();
                col3.Name = "inspection_name";
                col3.HeaderText = "Test items";
                col3.ReadOnly = true;
                col3.Width = 240;
                col3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView.Columns.Insert(2, col3);
                col4.CellTemplate = new DataGridViewTextBoxCell();
                col4.Name = "shortcut_key";
                col4.HeaderText = "corresponding button";
                col4.Visible = false;
                col4.ReadOnly = true;
                dataGridView.Columns.Insert(3, col4);
                col5.CellTemplate = new DataGridViewTextBoxCell();
                col5.Name = "qc_type";
                col5.HeaderText = "qc category";
                col5.Visible = false;
                col5.ReadOnly = true;
                dataGridView.Columns.Insert(4, col5);
                col6.CellTemplate = new DataGridViewTextBoxCell();
                col6.Name = "judgment_criteria";
                col6.HeaderText = "Judgment criteria";
                col6.Visible = false;
                col6.ReadOnly = true;
                dataGridView.Columns.Insert(5, col6);
                col7.CellTemplate = new DataGridViewTextBoxCell();
                col7.Name = "standard_value";
                col7.HeaderText = "Inspection item standard";
                col7.Visible = false;
                col7.ReadOnly = true;
                dataGridView.Columns.Insert(6, col7);
                col10.CellTemplate = new DataGridViewTextBoxCell();
                col10.Name = "num";
                col10.HeaderText = "quantity";
                col10.Visible = true;
                col10.ReadOnly = true;
                dataGridView.Columns.Insert(7, col10);
                btn1.Name = "Add";    //设置列的名称
                btn1.Text = "+";     //按钮上的文字属性
                btn1.HeaderText = "increase";     //显示的列名
                btn1.UseColumnTextForButtonValue = true;//显示按钮用属性
                dataGridView.Columns.Insert(8, btn1);
                btn2.Name = "minus";    //设置列的名称
                btn2.Text = "-";     //按钮上的文字属性
                btn2.HeaderText = "Reduce";     //显示的列名
                btn2.UseColumnTextForButtonValue = true;//显示按钮用属性
                dataGridView.Columns.Insert(9, btn2);
                btn3.Name = "upload";    //设置列的名称
                btn3.Text = "Upload PIC";     //按钮上的文字属性
                btn3.HeaderText = "Photograph";     //显示的列名
                btn3.UseColumnTextForButtonValue = true;//显示按钮用属性
                dataGridView.Columns.Insert(10, btn3);
                btn4.Name = "selectImg";    //设置列的名称
                btn4.Text = "view Pic";     //按钮上的文字属性
                btn4.HeaderText = "Check";     //显示的列名
                btn4.UseColumnTextForButtonValue = true;//显示按钮用属性
                dataGridView.Columns.Insert(11, btn4);
                col8.CellTemplate = new DataGridViewTextBoxCell();
                col8.Name = "imglist";
                col8.HeaderText = "Image guid collection";
                col8.Visible = false;
                col8.ReadOnly = true;
                dataGridView.Columns.Insert(12, col8);
                col9.CellTemplate = new DataGridViewTextBoxCell();
                col9.Name = "ifclick";
                col9.HeaderText = "Did you click add";
                col9.Visible = false;
                col9.ReadOnly = true;
                dataGridView.Columns.Insert(13, col9);
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView.ColumnHeadersHeight = 50;
                dataGridView.RowTemplate.Height = 30; //改变行的高度;
                int min = Math.Min(dgvRowCount, dt.Rows.Count);
                #endregion
                switch (i)
                {
                    case 0:
                        //分页读取接口返回数据
                        for (int a = 0; a < min; a++)
                        {
                            dataGridView.Rows.Add();
                            DataGridViewRow dgvr = dataGridView.Rows[a];
                            dgvr.Cells["id"].Value = dt.Rows[a]["id"].ToString();
                            dgvr.Cells["inspection_code"].Value = dt.Rows[a]["inspection_code"].ToString();
                            dgvr.Cells["inspection_name"].Value = dt.Rows[a]["inspection_name"].ToString();
                            dgvr.Cells["shortcut_key"].Value = dt.Rows[a]["shortcut_key"].ToString();
                            dgvr.Cells["qc_type"].Value = dt.Rows[a]["qc_type"].ToString();
                            dgvr.Cells["judgment_criteria"].Value = dt.Rows[a]["judgment_criteria"].ToString();
                            dgvr.Cells["standard_value"].Value = dt.Rows[a]["standard_value"].ToString();
                            dgvr.Cells["num"].Value = dt.Rows[a]["num"].ToString();
                            dgvr.Cells["ifclick"].Value = "false";
                            dgvr.Cells["imglist"].Value = dt.Rows[a]["imglist"].ToString();
                        }
                        this.splitContainer4.Panel1.Controls.Add(dataGridView);
                        break;
                    case 1:
                        //分页读取接口返回数据
                        int b = 0;
                        for (int a = dgvRowCount; a < dt.Rows.Count; a++)
                        {
                            dataGridView.Rows.Add();
                            DataGridViewRow dgvr = dataGridView.Rows[b];
                            dgvr.Cells["id"].Value = dt.Rows[a]["id"].ToString();
                            dgvr.Cells["inspection_code"].Value = dt.Rows[a]["inspection_code"].ToString();
                            dgvr.Cells["inspection_name"].Value = dt.Rows[a]["inspection_name"].ToString();
                            dgvr.Cells["shortcut_key"].Value = dt.Rows[a]["shortcut_key"].ToString();
                            dgvr.Cells["qc_type"].Value = dt.Rows[a]["qc_type"].ToString();
                            dgvr.Cells["judgment_criteria"].Value = dt.Rows[a]["judgment_criteria"].ToString();
                            dgvr.Cells["standard_value"].Value = dt.Rows[a]["standard_value"].ToString();
                            dgvr.Cells["num"].Value = dt.Rows[a]["num"].ToString();
                            dgvr.Cells["ifclick"].Value = "false";
                            dgvr.Cells["imglist"].Value = dt.Rows[a]["imglist"].ToString();
                            b++;
                        }
                        this.splitContainer4.Panel2.Controls.Add(dataGridView);
                        break;
                    default:
                        break;
                }
                dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick);
                //dataGridView.KeyPress += new KeyPressEventHandler(DGV_KeyPress);
                //dataGridView.KeyDown += new KeyEventHandler(DGV_KeyDown);
                //dataGridView.KeyUp += new KeyEventHandler(DGV_KeyDown);
                dgv_list.Add(dataGridView);
            }
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        /// <summary>
        /// 动态添加dgv点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                DataGridView dataGridViewEx1 = (DataGridView)sender;
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Add")
                {
                    if (dataGridViewEx1.Rows[e.RowIndex].Cells["ifclick"].Value.ToString() == "false")
                    {
                        dataGridViewEx1.Rows[e.RowIndex].Cells["num"].Value = Convert.ToInt32(dataGridViewEx1.Rows[e.RowIndex].Cells["num"].Value) + 1;
                        ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["Add"]).Enabled = false;
                        dataGridViewEx1.Rows[e.RowIndex].Cells["ifclick"].Value = "true";
                        //for (int i = 0; i < dgv_list.Count; i++)
                        //{
                        //    for (int t = 0; t < dgv_list[i].Rows.Count; t++)
                        //    {
                        //        if (dgv_list[i].Rows[t].Cells["ifclick"].Value.ToString() == "true")
                        //        {
                        //            button10.Text = "UnQualified";//不合格提交
                        //            button10.Tag = "1";
                        //            button13.Visible = false;
                        //            button13.Text = "UnQualified after Rework";//返修不合格提交Submission of unqualified repairs
                        //            button13.Tag = "3";
                        //        }
                        //        else  //else condition Added to Ashok on 20260109 to hide UnQualified after Rework
                        //        {
                        //            button13.Visible = true;
                        //        }
                        //    }
                        //}
                        bool hasClicked = false;
                        for (int i = 0; i < dgv_list.Count; i++)
                        {
                            for (int t = 0; t < dgv_list[i].Rows.Count; t++)
                            {
                                if (dgv_list[i].Rows[t].Cells["ifclick"].Value?.ToString() == "true")
                                {
                                    hasClicked = true;
                                    break; // no need to continue loop
                                }
                            }
                        }
                        // Apply UI logic ONCE
                        if (hasClicked)
                        {
                            button10.Text = "UnQualified";
                            button10.Tag = "1";

                            button13.Visible = false;
                            button13.Text = "UnQualified after Rework";
                            button13.Tag = "3";
                        }
                        else
                        {
                            button13.Visible = true;
                        }
                    }
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "minus")
                {
                    if (dataGridViewEx1.Rows[e.RowIndex].Cells["ifclick"].Value.ToString() == "true") //true//khaleel changed in place of true -- false
                    {
                        dataGridViewEx1.Rows[e.RowIndex].Cells["num"].Value = Convert.ToInt32(dataGridViewEx1.Rows[e.RowIndex].Cells["num"].Value) - 1;
                        ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["Add"]).Enabled = true;
                        dataGridViewEx1.Rows[e.RowIndex].Cells["ifclick"].Value = "false";
                        string btn10 = "Qualified";//合格或不合格//合格提交
                        string btn10tag = "0";//提交类型 合格或不合格
                        string btn13 = "Qualified after Rework";//返修合格或不合格//返修合格提交
                        string btn13tag = "2";//提交类型 返修合格或返修不合格
                        //for (int i = 0; i < dgv_list.Count; i++)
                        //{
                        //    for (int t = 0; t < dgv_list[i].Rows.Count; t++)
                        //    {
                        //        if (dgv_list[i].Rows[t].Cells["ifclick"].Value.ToString() == "true")
                        //        {
                        //            btn10 = "UnQualified";//不合格提交
                        //            btn10tag = "1";
                        //            button13.Visible = false;
                        //            btn13 = "UnQualified after Rework";//返修不合格提交
                        //            btn13tag = "3";
                        //        }
                        //        else  //else condition Added to Ashok on 20260109 to hide UnQualified after Rework
                        //        {
                        //            button13.Visible = true;
                        //        }
                        //    }
                        //}
                         bool hasClicked = false;
                        for (int i = 0; i < dgv_list.Count; i++)
                        {
                            for (int t = 0; t < dgv_list[i].Rows.Count; t++)
                            {
                                if (dgv_list[i].Rows[t].Cells["ifclick"].Value?.ToString() == "true")
                                {
                                    hasClicked = true;
                                    break; // no need to continue loop
                                }
                            }
                        }
                        // Apply UI logic ONCE
                        if (hasClicked)
                        {
                            button10.Text = "UnQualified";
                            button10.Tag = "1";

                            button13.Visible = false;
                            button13.Text = "UnQualified after Rework";
                            button13.Tag = "3";
                        }
                        else
                        {
                            button13.Visible = true;
                        }
                        //for (int t = 0; t < dataGridView1.Rows.Count; t++)
                        //{
                        //    if (dataGridView1.Rows[t].Cells["ifclick"].Value.ToString() == "true")
                        //    {
                        //        btn10 = "UnQualified";//不合格提交
                        //        btn10tag = "1";
                        //        button13.Visible = false;
                        //        btn13 = "UnQualified after Rework";//返修不合格提交
                        //        btn13tag = "3";
                        //    }
                        //    else  //else condition Added to Ashok on 20260109 to hide UnQualified after Rework
                        //    {
                        //        button13.Visible = true;
                        //    }
                        //}
                        bool hasClicked2 = false;
                        //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        //{
                        //    for (int t = 0; t < dgv_list[i].Rows.Count; t++)
                        //    {
                        //        if (dgv_list[i].Rows[t].Cells["ifclick"].Value?.ToString() == "true")
                        //        {
                        //            hasClicked2 = true;
                        //            break; // no need to continue loop
                        //        }
                        //    }
                        //}
                        //Added by Ashok by commenting above code
                        for (int i = 0; i < dgv_list.Count; i++)
                        {
                            var grid = dgv_list[i];

                            if (grid == null) continue;

                            for (int t = 0; t < grid.Rows.Count; t++)
                            {
                                if (grid.Rows[t].Cells["ifclick"]?.Value?.ToString() == "true")
                                {
                                    hasClicked2 = true;
                                    break;
                                }
                            }

                            if (hasClicked2) break;
                        }
                        //End Added by Ashok
                        // Apply UI logic ONCE
                        if (hasClicked2)
                        {
                            button10.Text = "UnQualified";
                            button10.Tag = "1";

                            button13.Visible = false;
                            button13.Text = "UnQualified after Rework";
                            button13.Tag = "3";
                        }
                        else
                        {
                            button13.Visible = true;
                        }
                        button10.Text = btn10;
                        button10.Tag = btn10tag;
                        button13.Text = btn13;
                        button13.Tag = btn13tag;
                    }
                    else
                    {
                        MessageBox.Show("The inspection item submitted this time has not been added!");
                        return;
                    }
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "upload")
                {
                    //创建文件弹出选择窗口（包括文件名）对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //判断选择的路径
                    string path = string.Empty;
                    ofd.Title = "Please select a folder";
                    ofd.Filter = "image file(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
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
                                if (dataGridViewEx1.Rows[e.RowIndex].Cells["imglist"].Value != null && !string.IsNullOrEmpty(dataGridViewEx1.Rows[e.RowIndex].Cells["imglist"].Value.ToString()))
                                {
                                    dataGridViewEx1.Rows[e.RowIndex].Cells["imglist"].Value = dataGridViewEx1.Rows[e.RowIndex].Cells["imglist"].Value + "," + resultDIC["guid"].ToString();
                                }
                                else
                                {
                                    dataGridViewEx1.Rows[e.RowIndex].Cells["imglist"].Value = resultDIC["guid"].ToString();
                                }
                                string union_id = dataGridViewEx1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                                //TQC_Task_Edit_Upload(union_id, resultDIC["guid"].ToString());
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Uploaded successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                            }
                        }

                    }
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "selectImg")
                {
                    var currRowFileDt = Getimage_guid(dataGridViewEx1.Rows[e.RowIndex].Cells["imglist"].Value.ToString());
                    FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.APIURL, Program.Client.UserToken, "", false);
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
                    dataGridViewEx1.Rows[e.RowIndex].Cells["imglist"].Value = image_guids;

                    //SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(Getimage_guid(dataGridViewEx1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString()), null, "");
                    //fil.ShowDialog();
                }
            }
        }

        /// <summary>
        /// tqc创建页面键帽设置上传照片
        /// </summary>
        //public void TQC_Task_Edit_Upload(string union_id,string file_guid)
        //{
        //    try
        //    {
        //        Dictionary<string, object> data = new Dictionary<string, object>();
        //        data.Add("union_id", union_id);
        //        data.Add("file_guid", file_guid);
        //        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
        //                 "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_Task_Edit_Upload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
        //        var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

        //        if (Convert.ToBoolean(j["IsSuccess"].ToString()))
        //        {
        //            string msg = SJeMES_Framework.Common.UIHelper.UImsg("上传成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
        //            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
        //            GetTQC_Task_Main(task_no);
        //        }
        //        else
        //            throw new Exception(j["ErrMsg"].ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
        //    }
        //}

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
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
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

        /// <summary>
        /// 各阶段样品记录添加页面查询图片
        /// </summary>
        /// <returns></returns>
        public DataTable Getimage_guidB()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("task_no", task_no);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "Getimage_guidB",//方法名
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

        /// <summary>
        /// tqc创建页面设置停线或者重新开线
        /// </summary>
        public void TQC_Task_Edit_state(string task_no, string task_state)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                data.Add("task_state", task_state);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_Task_Edit_state", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("conversion successful!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    GetTQC_Task_Main(task_no);
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

        private void button11_Click(object sender, EventArgs e)
        {

            int result = B_Bgrade_reason();

            if (result == 0)
            {
                return;
            }
            if (result == 1)
            {
                string file_guid = "";
                EditTestItemB(button11.Tag.ToString(), file_guid); //by this function it will directly submit the bgrade count after entering the Bgrade reason 
                //B_EditTestItem(); Since the Photo capturing the Bgrades now currently they are not using so kept this fuction in comment commented by Srinath N
            }
        }
        public int B_Bgrade_reason()
        {
            int result = 0;
            using (TQC_Bgrade_Reason t = new TQC_Bgrade_Reason(this, textBox10.Text, textBox3.Text, textBox9.Text, task_no))
            {

                t.ShowDialog();
                result = t.ResultValue;

            }
            return result;

        }
        public void B_EditTestItem()
        {
            string file_guid = "";
            var is_take_photo = MessageBox.Show("Do you want to take pictures? ", "Product B record", MessageBoxButtons.YesNo);
            if (is_take_photo == DialogResult.Yes)
            {
                var is_use_machine = MessageBox.Show("Whether to connect the camera", "Upload photos", MessageBoxButtons.YesNo);
                if (is_use_machine == DialogResult.Yes)
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
                            file_guid = resultDIC["guid"].ToString();

                            System.IO.File.Delete(phRes.SaveImgPath);
                        }
                        else
                        {
                            MessageBox.Show("Photo upload failed");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Photo upload failed");
                        return;
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
                                file_guid = resultDIC["guid"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Photo upload failed");
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else if (is_take_photo == DialogResult.No)
            {
            }
            else
            {
                return;
            }
            EditTestItemB(button11.Tag.ToString(), file_guid);

        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (TQC_BGrade_View t = new TQC_BGrade_View(this, task_no))
            {
                t.ShowDialog();
            }
        }

        private void TQC_Task_Edit_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable dtkey = GetTQC_Task_Edit_Stopline_Record();
            if (dtkey == null||dtkey.Rows.Count<=0)
                return;
            DataRow[] dr = dtkey.Select($@"shortcut_key='{e.KeyData.ToString()}'");
            if (dr.Length > 0)
            {
                switch (dr[0]["tqc_key"].ToString())
                {
                    case "1":
                        EditTestItem(button10.Tag.ToString());
                        break;
                    case "2":
                        EditTestItem(button13.Tag.ToString());
                        break;
                    case "3":
                        TQC_Task_Edit_recall();
                        break;
                    case "4":
                        B_EditTestItem();
                        break;
                    default:
                        break;
                }
                return;
            }

            foreach (var dgv in dgv_list)
            {
                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    if (item.Name == "shortcut_key")
                    {
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            if (dgv.Rows[i].Cells["ifclick"].Value.ToString() == "false")
                            {
                                if (dgv.Rows[i].Cells[item.Name].Value.ToString() == e.KeyData.ToString())
                                {
                                    dgv.Rows[i].Cells["num"].Value = Convert.ToDecimal(dgv.Rows[i].Cells["num"].Value) + 1;
                                    ((DataGridViewDisableButtonCell)dgv.Rows[i].Cells["Add"]).Enabled = true;
                                    dgv.Rows[i].Cells["ifclick"].Value = "true";
                                    button10.Text = "UnQualified";//不合格提交
                                    button10.Tag = "1";
                                    button13.Text = "UnQualified after Rework";//返修不合格提交
                                    button13.Tag = "3";
                                }
                            }
                        }
                    }
                }
            }

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                if (item.Name == "shortcut_key")
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["ifclick"].Value.ToString() == "false")
                        {
                            if (dataGridView1.Rows[i].Cells[item.Name].Value.ToString() == e.KeyData.ToString())
                            {
                                dataGridView1.Rows[i].Cells["num"].Value = Convert.ToDecimal(dataGridView1.Rows[i].Cells["num"].Value) + 1;
                                ((DataGridViewDisableButtonCell)dataGridView1.Rows[i].Cells["Add"]).Enabled = true;
                                dataGridView1.Rows[i].Cells["ifclick"].Value = "true";
                                button10.Text = "UnQualified";//不合格提交
                                button10.Tag = "1";
                                button13.Text = "UnQualified after Rework";//返修不合格提交
                                button13.Tag = "3";
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// tqc编辑页面查询提交快捷键
        /// </summary>
        public DataTable GetTQC_Task_Edit_Stopline_Record()
        {
            DataTable dtkey = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetTQC_Task_Edit_shortcut_key",//方法名
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
                    dtkey = dt;
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dtkey;
        }

        /// <summary>
        /// tqc创建页面提交
        /// </summary>
        public void EditTestItem(string commit_type)
        {
            try
            {
                if (dgv_list.Count > 0)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    DataTable TestItem = new DataTable();
                    //TestItem.Columns.Add(new DataColumn() { ColumnName = "id" });
                    //TestItem.Columns.Add(new DataColumn() { ColumnName = "inspection_code" });
                    //TestItem.Columns.Add(new DataColumn() { ColumnName = "inspection_name" });
                    //TestItem.Columns.Add(new DataColumn() { ColumnName = "shortcut_key", DataType = typeof(String) });
                    for (int i = 0; i < dgv_list.Count; i++)
                    {
                        TestItem.Merge(GetDgvToTable(dgv_list[i]));
                    }
                    //foreach (DataRow item in TestItem.Rows)
                    //{
                    //    if (item["shortcut_key"].ToString() == "请输入按键")
                    //    {
                    //        item["shortcut_key"] = stag_key;
                    //    }
                    //}
                    data.Add("commit_type", commit_type);
                    data.Add("task_no", task_no);
                    data.Add("TestItem", TestItem);
                    data.Add("Uncommon_TestItem", GetDgvToTable(dataGridView1));
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_Task_Edit", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        GetTQC_Task_Main(task_no);
                        string btn10 = "Qualified";//合格或不合格//
                        string btn10tag = "0";//提交类型 合格或不合格
                        string btn13 = "Qualified after Rework";//返修合格或不合格
                        string btn13tag = "2";//提交类型 返修合格或返修不合格
                        for (int i = 0; i < dgv_list.Count; i++)
                        {
                            for (int t = 0; t < dgv_list[i].Rows.Count; t++)
                            {
                                if (dgv_list[i].Rows[t].Cells["ifclick"].Value.ToString() == "true")
                                {
                                    btn10 = "UnQualified";
                                    btn10tag = "1";
                                    button13.Visible = false;
                                    btn13 = "UnQualified after Rework ";//返修不合格提交
                                    btn13tag = "3";
                                }
                                else  //else condition Added to Ashok on 20260109 to hide UnQualified after Rework
                                {
                                    button13.Visible = true;
                                }
                            }
                        }
                        button10.Text = btn10;
                        button10.Tag = btn10tag;
                        button13.Text = btn13;
                        button13.Tag = btn13tag;
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
                else
                {
                    MessageBox.Show("No data!!!");
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// tqc创建页面b品提交
        /// </summary>
        public void EditTestItemB(string commit_type, string file_guidB)
        {
            try
            {
                if (dgv_list.Count > 0)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    DataTable TestItem = new DataTable();
                    for (int i = 0; i < dgv_list.Count; i++)
                    {
                        TestItem.Merge(GetDgvToTable(dgv_list[i]));
                    }
                    data.Add("commit_type", commit_type);
                    data.Add("file_guidB", file_guidB);
                    data.Add("task_no", task_no);
                    data.Add("TestItem", TestItem);
                    data.Add("Uncommon_TestItem", GetDgvToTable(dataGridView1));
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "EditTestItemB", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        GetTQC_Task_Main(task_no);
                        string btn10 = "Qualified";//合格或不合格
                        string btn10tag = "0";//提交类型 合格或不合格
                        string btn13 = "Qualified after Rework";//返修合格或不合格
                        string btn13tag = "2";//提交类型 返修合格或返修不合格
                        for (int i = 0; i < dgv_list.Count; i++)
                        {
                            for (int t = 0; t < dgv_list[i].Rows.Count; t++)
                            {
                                if (dgv_list[i].Rows[t].Cells["ifclick"].Value.ToString() == "true")
                                {
                                    btn10 = "UnQualified";
                                    btn10tag = "1";
                                    button13.Visible = false;
                                    btn13 = "UnQualified after Rework";
                                    btn13tag = "3";
                                }
                                else  //else condition Added to Ashok on 20260109 to hide UnQualified after Rework
                                {
                                    button13.Visible = true;
                                }
                            }
                        }
                        button10.Text = btn10;
                        button10.Tag = btn10tag;
                        button13.Text = btn13;
                        button13.Tag = btn13tag;
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
                else
                {
                    MessageBox.Show("No data!!!");
                }
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

        private void button10_Click(object sender, EventArgs e)
        {
            EditTestItem(button10.Tag.ToString());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            EditTestItem(button13.Tag.ToString());
        }

        /// <summary>
        /// tqc创建页面撤回提交
        /// </summary>
        public void TQC_Task_Edit_recall()
        {
            try
            {
                if (dgv_list.Count > 0)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("task_no", task_no);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TQC_Task_Edit_recall", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Withdraw successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        GetTQC_Task_Main(task_no);
                    }
                    else
                        throw new Exception(j["ErrMsg"].ToString());
                }
                else
                {
                    MessageBox.Show("No data!!!");
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TQC_Task_Edit_recall();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            GetDQAMQA();
        }

        /// <summary>
        /// tqc创建页面根据鞋型查dqa&mqa
        /// </summary>
        public void GetDQAMQA()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("shoe_no", textBox2.Text);
                List<string> art = new List<string>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_TQCAPI",//类库名
                                           "SJ_TQCAPI.TQC_Task",//类名
                                           "GetDQAMQA",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable data1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (data1.Rows.Count > 0)
                {
                    using (TQC_Task_Check t = new TQC_Task_Check(data1, task_no, ck))
                    {
                        t.ShowDialog();
                    }
                    GetTQC_Task_Main(task_no);
                }
            }
            catch (Exception)
            {

                throw;
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
            textBox3.Text = _po;
            GetShoe_no_jijie();
        }
        public void art(string _art)
        {
            textBox10.Text = _art;
            textBox3.Text = "";

            //以art带出信息
            GetShoe_no_jijieByART();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            over_inspection();
        }

        /// <summary>
        /// end inspection
        /// </summary>
        public void over_inspection()
        {
            try
            {
                //The rummaging task requires verification


                if (task_state == "3" || task_state == "4")
                {
                    if (string.IsNullOrEmpty(textBox14.Text.Trim()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("The production line cannot be empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                    if (Convert.ToInt32(textBox11.Text.Trim()) < Convert.ToInt32(textBox13.Text.Trim()))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("The total number of first inspections should be >= AQL rummage inspection!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                    if (dataGridView2.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                        {
                            //When the code number is greater than 0, the box number must be filled in.
                            if (!string.IsNullOrEmpty(dgvr.Cells["se_qty"].Value.ToString()) && Convert.ToInt32(dgvr.Cells["se_qty"].Value.ToString()) > 0 && string.IsNullOrEmpty(dgvr.Cells["case_no"].Value.ToString()))
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Box number cannot be empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                return;
                            }
                        }

                    }

                }


                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                data.Add("task_state", task_state);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "over_inspection", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("finished!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);

                    //The rummaging task needs to be transferred to pivot88
                    if (task_state == "3" || task_state == "4")
                    {
                        string retdataRework = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "TranserTQCReworkDataToPivot88", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        var jrework = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                        if (Convert.ToBoolean(jrework["IsSuccess"].ToString()))
                        {
                            string msg_rework = SJeMES_Framework.Common.UIHelper.UImsg("Synchronization of pivot88 successful!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg_rework);
                        }
                        else
                        {
                            string msg_rework = SJeMES_Framework.Common.UIHelper.UImsg("Sync failed：" + jrework["ErrMsg"].ToString(), Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg_rework);
                        }
                    }
                    this.Close();
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

        private void button7_Click(object sender, EventArgs e)
        {
            using (TQC_Task_Edit_PO t = new TQC_Task_Edit_PO(this, textBox10.Text))
            {
                t.ShowDialog();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            GetShoe_no_jijie();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            using (TQC_Task_Edit_ART t = new TQC_Task_Edit_ART(this))
            {
                t.ShowDialog();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            using (TQC_Task_Edit_Stopline_Record t = new TQC_Task_Edit_Stopline_Record(task_no))
            {
                t.ShowDialog();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDgvToTable(dataGridView1);
            using (TQC_Uncommon_TestItem t = new TQC_Uncommon_TestItem(task_no, comboBox3.SelectedValue.ToString(), this, dt))
            {
                t.ShowDialog();
            }
        }

        /// <summary>
        /// 添加不常见项目
        /// </summary>
        public void Uncommon_TestItem_RowsAdd(DataTable bcjdt)
        {
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells["inspection_code"].Value = bcjdt.Rows[0]["inspection_code"];
            dataGridView1.Rows[index].Cells["inspection_name"].Value = bcjdt.Rows[0]["inspection_name"];
            dataGridView1.Rows[index].Cells["qc_type"].Value = bcjdt.Rows[0]["qc_type"];
            dataGridView1.Rows[index].Cells["judgment_criteria"].Value = bcjdt.Rows[0]["judgment_criteria"];
            dataGridView1.Rows[index].Cells["standard_value"].Value = bcjdt.Rows[0]["standard_value"];
            dataGridView1.Rows[index].Cells["shortcut_key"].Value = bcjdt.Rows[0]["shortcut_key"];
            dataGridView1.Rows[index].Cells["num"].Value = "1";
            dataGridView1.Rows[index].Cells["ifclick"].Value = "true";
            dataGridView1.Rows[index].Cells["imglist"].Value = "";
            ((DataGridViewDisableButtonCell)dataGridView1.Rows[index].Cells["Add"]).Enabled = false;
            button10.Text = "UnQualified";//不合格提交
            button10.Tag = "1";
           // button13.Text = "UnQualified after Rework";//返修不合格提交
           // button13.Tag = "3";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Add")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["ifclick"].Value.ToString() == "false")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["num"].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["num"].Value) + 1;
                        ((DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["Add"]).Enabled = false;
                        dataGridView1.Rows[e.RowIndex].Cells["ifclick"].Value = "true";
                        //for (int t = 0; t < dataGridView1.Rows.Count; t++)
                        //{
                        //    if (dataGridView1.Rows[t].Cells["ifclick"].Value.ToString() == "true")
                        //    {
                        //        button10.Text = "UnQualified";
                        //        button10.Tag = "1";
                        //        button13.Visible = false;
                        //        button13.Text = "UnQualified after Rework";
                        //        button13.Tag = "3";
                        //    }
                        //    else  //else condition Added to Ashok on 20260109 to hide UnQualified after Rework
                        //    {
                        //        button13.Visible = true;
                        //    }
                        //}

                        bool hasClicked = false;

                        for (int t = 0; t < dataGridView1.Rows.Count; t++)
                        {
                            if (dataGridView1.Rows[t].Cells["ifclick"]?.Value?.ToString() == "true")
                            {
                                hasClicked = true;
                                break; // no need to check further
                            }
                        }

                        // Apply UI logic ONCE
                        if (hasClicked)
                        {
                            button10.Text = "UnQualified";
                            button10.Tag = "1";

                            button13.Visible = false;
                            button13.Text = "UnQualified after Rework";
                            button13.Tag = "3";
                        }
                        else
                        {
                            button13.Visible = true;
                        }

                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "minus")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["ifclick"].Value.ToString() == "true")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["num"].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["num"].Value) - 1;
                        ((DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells["Add"]).Enabled = true;
                        dataGridView1.Rows[e.RowIndex].Cells["ifclick"].Value = "false";
                        string btn10 = "Qualified ";//合格或不合格
                        string btn10tag = "0";//提交类型 合格或不合格
                        string btn13 = "Qualified after Rework";//返修合格或不合格
                        string btn13tag = "2";//提交类型 返修合格或返修不合格
                        //for (int t = 0; t < dataGridView1.Rows.Count; t++)
                        //{
                        //    if (dataGridView1.Rows[t].Cells["ifclick"].Value.ToString() == "true")
                        //    {
                        //        btn10 = "UnQualified";
                        //        btn10tag = "1";
                        //        button13.Visible = false;
                        //        btn13 = "UnQualified after Rework";
                        //        btn13tag = "3";
                        //    }
                        //    else  //else condition Added to Ashok on 20260109 to hide UnQualified after Rework
                        //    {
                        //        button13.Visible = true;
                        //    }
                        //}
                        //for (int i = 0; i < dgv_list.Count; i++)
                        //{
                        //    for (int t = 0; t < dgv_list[i].Rows.Count; t++)
                        //    {
                        //        if (dgv_list[i].Rows[t].Cells["ifclick"].Value.ToString() == "true")
                        //        {
                        //            btn10 = "UnQualified";
                        //            btn10tag = "1";
                        //            button13.Visible = false;
                        //            btn13 = "UnQualified after Rework";
                        //            btn13tag = "3";
                        //        }
                        //        else  //else condition Added to Ashok on 20260109 to hide UnQualified after Rework
                        //        {
                        //            button13.Visible = true;
                        //        }
                        //    }
                        //}


                        bool hasClicked = false;

                        // Check dataGridView1
                        for (int t = 0; t < dataGridView1.Rows.Count; t++)
                        {
                            if (dataGridView1.Rows[t].Cells["ifclick"]?.Value?.ToString() == "true")
                            {
                                hasClicked = true;
                                break;
                            }
                        }

                        // If not found, check dgv_list
                        if (!hasClicked)
                        {
                            for (int i = 0; i < dgv_list.Count; i++)
                            {
                                var grid = dgv_list[i];
                                if (grid == null) continue;

                                for (int t = 0; t < grid.Rows.Count; t++)
                                {
                                    if (grid.Rows[t].Cells["ifclick"]?.Value?.ToString() == "true")
                                    {
                                        hasClicked = true;
                                        break;
                                    }
                                }

                                if (hasClicked) break;
                            }
                        }

                        if (hasClicked)
                        {
                            btn10 = "UnQualified";
                            btn10tag = "1";

                            btn13 = "UnQualified after Rework";
                            btn13tag = "3";

                            button13.Visible = false;
                        }
                        else
                        {
                            button13.Visible = true;
                        }

                        button10.Text = btn10;
                        button10.Tag = btn10tag;
                        button13.Text = btn13;
                        button13.Tag = btn13tag;
                    }
                    else
                    {
                        MessageBox.Show("The inspection item submitted this time has not been added!");
                        return;
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "upload")
                {
                    //创建文件弹出选择窗口（包括文件名）对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //判断选择的路径
                    string path = string.Empty;
                    ofd.Title = "Please select a folder";
                    ofd.Filter = "image file(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
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
                                if (dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value.ToString()))
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value = dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value + "," + resultDIC["guid"].ToString();
                                }
                                else
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value = resultDIC["guid"].ToString();
                                }
                                //string union_id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                                //TQC_Task_Edit_Upload(union_id, resultDIC["guid"].ToString());
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Uploaded successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                            }
                        }

                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "selectImg")// 選擇圖像
                {
                    var currRowFileDt = Getimage_guid(dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value.ToString());
                    FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.APIURL, Program.Client.UserToken, "", false);
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
                    dataGridView1.Rows[e.RowIndex].Cells["imglist"].Value = image_guids;

                    //SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(Getimage_guid(dataGridView1.Rows[e.RowIndex].Cells["image_guid"].Value.ToString()), null, "");
                    //fil.ShowDialog();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(task_no))
            {
                MessageBox.Show("Please complete the task first!");
                return;
            }
            GetEx_LookResult();
        }

        /// <summary>
        /// tqc编辑页查询测试结果
        /// </summary>
        public void GetEx_LookResult()
         {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("tqc_task_no", task_no);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "GetEx_LookResult",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                MessageBox.Show("Failed to get data");
                return;
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            //var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["task_no"].ToString());
            if (string.IsNullOrWhiteSpace(dic["task_no"].ToString()))
            {
                MessageBox.Show("No test result!");
                return;
            }
            using (F_QCM_Ex_LookResult_New frm = new F_QCM_Ex_LookResult_New(dic["task_no"].ToString(), Program.Client))
            {
                frm.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(task_no))
            {
                MessageBox.Show("Please complete the task first!");
                return;
            }
            FrmFileList add = new FrmFileList(GetCompliance_File(), Program.Client.UploadUrl, Program.Client.UserToken,"",false,false);
            add.ShowDialog();
        }

        /// <summary>
        /// tqc编辑页查询安全合规文件
        /// </summary>
        public DataTable GetCompliance_File()
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("tqc_task_no", task_no);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetCompliance_File",//方法名
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(task_no))
            {
                MessageBox.Show("Please complete the task first!");
                return;
            }
            FrmFileList add = new FrmFileList(GetDQAFile(), Program.Client.UploadUrl, Program.Client.UserToken, "", false, false);
            add.ShowDialog();
        }

        /// <summary>
        /// Query DQA files on the tqc edit page
        /// </summary>
        public DataTable GetDQAFile()
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("tqc_task_no", task_no);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetDQAFile",//方法名
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
        /// 以art生成任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            GetDepartment();

            string art = textBox10.Text;
            //string po = textBox3.Text;//po
            string cx = textBox6.Text;//产线
            string gd = comboBox3.SelectedValue.ToString();//工段
            if (string.IsNullOrWhiteSpace(cx) || string.IsNullOrWhiteSpace(gd) || string.IsNullOrWhiteSpace(art))
            {
                MessageBox.Show("Basic information cannot be empty!");
                return;
            }

            TQC_Task_Edit_Insert();
            if (GetClaimDetails())
            {
                button21.ForeColor = Color.Red;
            }
            button15.Visible = false;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            
            string Art = textBox10.Text;
            //string po = textBox3.Text;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Art", Art);
            //data.Add("po", po);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "GetClaimData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData); 
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            ClaimList CL = new ClaimList(dt);
             CL.ShowDialog();  
        }

        private void button22_Click(object sender, EventArgs e)
        {
            
        }

        private void splitContainer4_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox14_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void DataChangedArt1(object sender, F_WMS_Multiple_ArtSelect.DataTableChangeEventArgs args, int row_index)
        {
            DataTable table = args.dataTable;
            textBox14.Text = string.Join(",", Array.ConvertAll<DataRow, string>(table.Rows.Cast<DataRow>().ToArray(), r => r["productLine"].ToString()));
            if (table.Rows.Count > 0)
            {
                HashSet<string> set = new HashSet<string>();
                foreach (DataRow dr in table.Rows)
                {
                    set.Add(dr["depart"].ToString());
                }
                textBox9.Text = string.Join(",", set);
            }
            //textBox9.Text = string.Join(",", Array.ConvertAll<DataRow, string>(table.Rows.Cast<DataRow>().ToArray(), r => r["depart"].ToString()));
        }

        private void textBox14_DoubleClick_1(object sender, EventArgs e)
        {
            using (F_WMS_Multiple_ArtSelect frm = new F_WMS_Multiple_ArtSelect())
            {
                frm.DataChange += new F_WMS_Multiple_ArtSelect.DataChangeHandler(DataChangedArt1);
                frm.ShowDialog();
            }
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            string art = textBox10.Text;
            using (TQC_DR_View t = new TQC_DR_View(this, art))
            {
                t.ShowDialog();
            }

        }

        //DataGridview to DataTable
        public DataTable DataGridViewToDataTable(DataGridView dataGridView)
        {
            //Add column
            DataTable pointBoxTable = new DataTable();
            foreach (DataGridViewColumn dgvc in dataGridView.Columns)
            {
                DataColumn column = new DataColumn(dgvc.Name.ToString());
                pointBoxTable.Columns.Add(column);
            }

            //Add row
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                DataRow dr = pointBoxTable.NewRow();
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    dr[j] = dataGridView.Rows[i].Cells[j].Value;
                }
                pointBoxTable.Rows.Add(dr);
            }
            return pointBoxTable;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string productline = textBox14.Text.Trim();
            string depart = textBox9.Text.Trim();
            string stage = comboBox2.Text;
            string _task_no = task_no;
            DataTable pointBoxTable = DataGridViewToDataTable(dataGridView2);


            //Request api data display
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("productline", productline);
            data.Add("depart", depart);
            data.Add("stage", stage);
            data.Add("_task_no", _task_no);
            data.Add("pointBoxTable", pointBoxTable);

            //键值对传值
            string retdata = WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TQCAPI",//类库名
                                        "SJ_TQCAPI.TQC_Task",//类名
                                        "SavePointBoxData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (ret.IsSuccess)
            {
                MessageBox.Show("Saved successfully");
            }
        }

        private void splitContainer5_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void Fetch_PO_Qty(string poNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(poNumber))
                {
                    textBox17.Text = "";
                    return;
                }

                Dictionary<string, string> requestData = new Dictionary<string, string>
                {
                    {"CUSTOMER_PO", poNumber }
                };

                string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                            Program.Client.APIURL,
                            "SJ_TQCAPI",
                            "SJ_TQCAPI.TQC_Task",
                            "Fetch_PO_Qty",
                            Program.Client.UserToken,
                            Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
                            );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);

                if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
                {
                    var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);

                    if (dataList != null && dataList.Count > 0)
                    {
                        var rowdata = dataList[0];

                        //string availableKeys = string.Join(", ", rowdata.Keys);
                        //MessageBox.Show("Available Keys: " + availableKeys, "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (rowdata.ContainsKey("PO_QUANTITY"))
                        {
                            textBox17.Text = rowdata["PO_QUANTITY"].ToString();
                        }
                        else
                        {
                            textBox17.Text = "Key Not Found";
                            MessageBox.Show("Error: 'PO_Quantity' key is missing in API response.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        textBox17.Text = "Not Found";
                    }
                }
                else
                {
                    textBox17.Text = "";
                    //MessageBox.Show("Error: " + result.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error Fetching Data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fetch_PO_Country(string poNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(poNumber))
                {
                    textBox16.Text = "";
                    return;
                }

                Dictionary<string, string> requestData = new Dictionary<string, string>
                {
                    {"CUSTOMER_PO", poNumber }
                };

                string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                            Program.Client.APIURL,
                            "SJ_TQCAPI",
                            "SJ_TQCAPI.TQC_Task",
                            "Fetch_PO_Country",
                            Program.Client.UserToken,
                            Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
                            );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);

                if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
                {
                    var dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);

                    if (dataList != null && dataList.Count > 0)
                    {
                        var rowdata = dataList[0];

                        //string availableKeys = string.Join(", ", rowdata.Keys);
                        //MessageBox.Show("Available Keys: " + availableKeys, "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (rowdata.ContainsKey("C_NAME"))
                        {
                            textBox16.Text = rowdata["C_NAME"].ToString();
                        }
                        else
                        {
                            textBox16.Text = "Key Not Found";
                            MessageBox.Show("Error: 'PO_Country' key is missing in API response.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        textBox16.Text = "Not Found";
                    }
                }
                else
                {
                    textBox16.Text = "";
                    //MessageBox.Show("Error: " + result.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Fetching Data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string poNumber = textBox3.Text.Trim();
            Fetch_PO_Qty(textBox3.Text);
            Fetch_PO_Country(textBox3.Text);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
