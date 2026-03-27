using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
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
using System.Net.Http;
using SJeMES_Control_Library;
using DataGrid.DataGridViewCustomColumn;

namespace SJeMES_QA
{
    public partial class F_QA_ShoeShapeAdd : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private string develop_season;//季度
        private string shoe_no;//鞋型
        public F_QA_ShoeShapeAdd(string _develop_season, string _shoe_no)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            develop_season = _develop_season;
            shoe_no = _shoe_no;
            GET_LastShoeshape_Item();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QA_ShoeShapeAdd_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            txt_shoe_no.LostFocus += new EventHandler(SCZS);
            textBox2.LostFocus += new EventHandler(BLS);
            dateTimePicker1.MinDate = DateTime.Now;
            GET_ShoeShape();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        //查询新增中的阶段
        public void GET_ShoeShape()
        {
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
                    cbo_type.DataSource = dt;
                    cbo_type.DisplayMember = "dpstage_name";
                    cbo_type.ValueMember = "dpstage_code";
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //查询重要问题分类
        public DataTable GET_Problemcategory()
        {

            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.QAShoeShapeTable", "GET_Problemcategory", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());

                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        //查询重要问题追踪点
        public DataTable GET_ProblemcategoryD(string problemcategory_no)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("problemcategory_no", problemcategory_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.QAShoeShapeTable", "GET_ProblemcategoryD", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());

                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }

        private void btnAddWT_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_shoe_no.Text))
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells["JY"].Value = "";
                this.dataGridView1.Rows[index].Cells["problemcategory_no"].Value = "";
                this.dataGridView1.Rows[index].Cells["problemcategory_name"].Value = "";
                this.dataGridView1.Rows[index].Cells["problem_no"].Value = "";
                this.dataGridView1.Rows[index].Cells["problem_name"].Value = "";
                this.dataGridView1.Rows[index].Cells["ng_qty"].Value = "";
                this.dataGridView1.Rows[index].Cells["ng_rate"].Value = "";
                this.dataGridView1.Rows[index].Cells["improvement_measures"].Value = "";
                this.dataGridView1.Rows[index].Cells["respon_people"].Value = "";
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the basic data first!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //给表格行赋上控件和数据
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                string problem_no = dataGridView1.Rows[e.RowIndex].Cells["problem_no"].Value.ToString();
                string problemcategory_no = dataGridView1.Rows[e.RowIndex].Cells["problemcategory_no"].Value.ToString();
                string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem==null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("selectImg"))
                    {
                        SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(GET_Qcm_qa_shoeshape_image(problemcategory_no, problem_no),null,"3");
                        fil.ShowDialog();
                    }
                    else if (cell.CurrentItem.Equals("UploadIMG"))
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
                            string res = UpLoad("4", problemcategory_no, problem_no);
                            if (res == "ok")
                            {
                                MessageBox.Show("File uploaded successfully！");
                            }
                            else
                            {
                                MessageBox.Show("Failed to upload file！");
                            }
                        }
                    }
                }
            }
            if (dataGridView1.Rows[e.RowIndex].Cells["JY"].Value.ToString() != "1")
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "problemcategory_name") // combobox显示条件 
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    DataTable dt_tval = GET_Problemcategory();
                    comboBox1.DataSource = dt_tval;
                    if (dt_tval != null && dt_tval.Rows.Count > 0)
                    {
                        comboBox1.DisplayMember = "problemcategory_name";
                        comboBox1.ValueMember = "problemcategory_no";
                    }
                    comboBox1.Text = dataGridView1.CurrentCell.Value.ToString(); //对combobox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    comboBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    comboBox1.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "problem_name")
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    string problemcategory_no = dataGridView1.Rows[e.RowIndex].Cells["problemcategory_no"].Value.ToString();
                    DataTable dt_tval = GET_ProblemcategoryD(problemcategory_no);
                    comboBox1.DataSource = dt_tval;
                    if (dt_tval != null && dt_tval.Rows.Count > 0)
                    {
                        comboBox1.DisplayMember = "problem_name";
                        comboBox1.ValueMember = "problem_no";
                    }
                    comboBox1.Text = dataGridView1.CurrentCell.Value.ToString(); //对combobox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    comboBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    comboBox1.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "respon_people")
                {
                    textBox2.Visible = false;
                    comboBox1.Visible = false;
                    textBox1.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "ng_qty")
                {
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox2.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox2.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "improvement_measures")
                {
                    textBox2.Visible = false;
                    comboBox1.Visible = false;
                    textBox1.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                }
                else
                {
                    comboBox1.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                }
            }
            else
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "respon_people")
                {
                    textBox2.Visible = false;
                    comboBox1.Visible = false;
                    textBox1.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "ng_qty")
                {
                    textBox1.Visible = false;
                    comboBox1.Visible = false;
                    textBox2.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox2.Visible = true;
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "improvement_measures")
                {
                    textBox2.Visible = false;
                    comboBox1.Visible = false;
                    textBox1.Text = dataGridView1.CurrentCell.Value.ToString(); //对textbox赋值

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                }
                else
                {
                    comboBox1.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                }
            }
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = comboBox1.Text;
            if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name== "problemcategory_name")
            {
                dataGridView1.CurrentRow.Cells["problemcategory_no"].Value = comboBox1.SelectedValue.ToString();
            }
            if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == "problem_name")
            {
                dataGridView1.CurrentRow.Cells["problem_no"].Value = comboBox1.SelectedValue.ToString();
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
        private void textBox1_Leave(object sender, EventArgs e)
        {
        }

        private void txt_shoe_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
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
                    //分类代号
                    string problemcategory_no = dataGridView1.Rows[i].Cells["problemcategory_no"].Value == null ? "" : dataGridView1.Rows[i].Cells["problemcategory_no"].Value.ToString();
                    //追踪点代号
                    string problem_no = dataGridView1.Rows[i].Cells["problem_no"].Value == null ? "" : dataGridView1.Rows[i].Cells["problem_no"].Value.ToString();
                    //不良数
                    string ng_qty = dataGridView1.Rows[i].Cells["ng_qty"].Value == null ? "" : dataGridView1.Rows[i].Cells["ng_qty"].Value.ToString();
                    //不良率
                    string ng_rate = dataGridView1.Rows[i].Cells["ng_rate"].Value == null ? "" : dataGridView1.Rows[i].Cells["ng_rate"].Value.ToString();
                    if (string.IsNullOrEmpty(problemcategory_no) || string.IsNullOrEmpty(problem_no) || string.IsNullOrEmpty(ng_qty) || string.IsNullOrEmpty(ng_rate))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Form data cannot be empty!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                }
                InsertShoeshape_Item();
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
          
        }

        /// <summary>
        /// 查询最后一次新增的数据
        /// </summary>
        public void GET_LastShoeshape_Item()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("develop_season", develop_season);
                data.Add("shoe_no", shoe_no);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.QAShoeShapeTable", "GET_LastShoeshape_Item", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
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
                        dgvr.Cells["problemcategory_name"].Value = dr["problemcategory_name"].ToString();
                        dgvr.Cells["problemcategory_no"].Value = dr["problemcategory_no"].ToString();
                        dgvr.Cells["problem_no"].Value = dr["problem_no"].ToString();
                        dgvr.Cells["problem_name"].Value = dr["problem_name"].ToString();
                        dgvr.Cells["ng_qty"].Value = dr["ng_qty"].ToString();
                        dgvr.Cells["ng_rate"].Value = dr["ng_rate"].ToString();
                        dgvr.Cells["improvement_measures"].Value = dr["improvement_measures"].ToString();
                        dgvr.Cells["respon_people"].Value = dr["respon_people"].ToString();
                        dgvr.Cells["JY"].Value = "1";
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
        /// 新增阶段样品品质状况
        /// </summary>
        public void InsertShoeshape_Item()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                #region 参数
                DataTable dtt = new DataTable();
                dtt.Columns.Add("problemcategory_no");
                dtt.Columns.Add("problem_no");
                dtt.Columns.Add("ng_qty");
                dtt.Columns.Add("ng_rate");
                dtt.Columns.Add("improvement_measures");
                dtt.Columns.Add("respon_people");
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["problemcategory_no"] = dgvr.Cells["problemcategory_no"].Value.ToString();
                    dr["problem_no"] = dgvr.Cells["problem_no"].Value.ToString();
                    dr["ng_qty"] = dgvr.Cells["ng_qty"].Value.ToString();
                    dr["ng_rate"] = dgvr.Cells["ng_rate"].Value.ToString();
                    dr["improvement_measures"] = dgvr.Cells["improvement_measures"].Value.ToString();
                    dr["respon_people"] = dgvr.Cells["respon_people"].Value.ToString();

                    dtt.Rows.Add(dr);
                }
                if (string.IsNullOrEmpty(dateTimePicker1.Value.ToString()) || string.IsNullOrEmpty(cbo_type.Text) || string.IsNullOrEmpty(txt_shoe_no.Text))
                {
                    MessageBox.Show("Please fill in the basic data!");
                    return;
                }
                p.Add("qcm_qa_shoeshape_item", dtt);
                p.Add("develop_season", develop_season);//季度
                p.Add("shoe_no", shoe_no);//鞋型
                p.Add("check_date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));//日期
                p.Add("dpstage_code", cbo_type.SelectedValue);//阶段代号
                p.Add("qty", txt_shoe_no.Text);//生产总数
                #endregion

                #region 找接口

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                   Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                   "SJ_QCMAPI.QAShoeShapeTable",//类名
                                                   "InsertShoeshape_Item",//方法名
                                                   Program.Client.UserToken,//token
                                                   Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                this.Close();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //点击上传图片
        public string UpLoad(string type,string problemcategory_no,string problem_no)
        {
            string isload = "no";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmss") + SafeFileName;
                    var content = new MultipartFormDataContent();
                    string path = System.IO.Path.Combine(filePath);

                    content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", saveName);
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("type", type);
                    p.Add("develop_season", develop_season);
                    p.Add("shoe_no", shoe_no);
                    p.Add("check_date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    p.Add("dpstage_code", cbo_type.SelectedValue);
                    p.Add("problemcategory_no", problemcategory_no);
                    p.Add("problem_no", problem_no);
                    p.Add("img_name", SafeFileName);
                    p.Add("img_url", filePath);
                    p.Add("usertoken", Program.Client.UserToken);
                    p.Add("guid", Guid.NewGuid().ToString());
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

        /// <summary>
        /// 操作
        /// </summary>
        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string problem_no = dataGridView1.Rows[e.RowIndex].Cells["problem_no"].Value.ToString();
            string problemcategory_no = dataGridView1.Rows[e.RowIndex].Cells["problemcategory_no"].Value.ToString();
            //if (e.RowIndex >= 0)
            //{
            //    if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation")
            //    {
            //        DataGridViewActionButtonColumn dataGridViewColumn = (DataGridViewActionButtonColumn)this.dataGridView1.Columns[e.ColumnIndex];

            //        List<ActionButton> buttonList = dataGridViewColumn.ButtonList;

            //        foreach (ActionButton act in buttonList)
            //        {
            //            //此时鼠标悬浮在上面
            //            if (act.MouseOnButton)
            //            {
            //                //MessageBox.Show("点击了:" + act.Name);
            //                if (act.Name.Equals("DETAIL"))//查看
            //                {
            //                    SJeMES_Control_Library.Forms.FrmImgList fil = new SJeMES_Control_Library.Forms.FrmImgList(GET_Qcm_qa_shoeshape_image(problemcategory_no, problem_no));
            //                    fil.Show();
            //                }
            //                //上传图片
            //                if (act.Name.Equals("UploadIMG"))
            //                {
            //                    //创建文件弹出选择窗口（包括文件名）对象
            //                    OpenFileDialog ofd = new OpenFileDialog();
            //                    //判断选择的路径
            //                    string path = string.Empty;
            //                    ofd.Title = "请选择文件夹";
            //                    ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
            //                    if (ofd.ShowDialog() == DialogResult.OK)
            //                    {
            //                        SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
            //                        filePath = ofd.FileName;
            //                        string res = UpLoad("4", problemcategory_no, problem_no);
            //                        if (res == "ok")
            //                        {
            //                            MessageBox.Show("上传文件成功！");
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("上传文件失败！");
            //                        }
            //                    }
            //                }
            //            }
            //        }

            //    }
            //}


           
        }

        /// <summary>
        /// 查看上传QA鞋型品质问题点图片
        /// </summary>
        public DataTable GET_Qcm_qa_shoeshape_image(string problemcategory_no,string problem_no)
        {
            DataTable dt=new DataTable();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("develop_season", develop_season);
                data.Add("shoe_no", shoe_no);
                data.Add("dpstage_code", cbo_type.SelectedValue.ToString());
                data.Add("problem_no", problem_no);
                data.Add("problemcategory_no", problemcategory_no);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.QAShoeShapeTable", "GET_Qcm_qa_shoeshape_image", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count>0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["img_url"] = Program.Client.PicUrl+ dr["img_url"];
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

        public void SCZS(object sender, System.EventArgs e)
        {
            int index = dataGridView1.Rows.Count;
            double BL = 0;
            double SCZS = txt_shoe_no.Text == "" ? 0 : Convert.ToDouble(txt_shoe_no.Text);
            for (int i = 0; i < index; i++)
            {
                BL = dataGridView1.Rows[i].Cells["ng_qty"].Value.ToString() == "" ? 0 : Convert.ToDouble(dataGridView1.Rows[i].Cells["ng_qty"].Value.ToString());
                if (BL != 0)
                {
                    if (BL > SCZS)
                    {
                        dataGridView1.Rows[i].Cells["ng_qty"].Value = "";
                        dataGridView1.Rows[i].Cells["ng_rate"].Value = "";
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("The bad quantity cannot be greater than the total production!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["ng_rate"].Value = Math.Round((BL / SCZS * 100), 2).ToString() + "%";
                    }
                }
            }
        }

        public void BLS(object sender, System.EventArgs e)
        {
            int index = dataGridView1.Rows.Count;
            double BL = 0;
            double SCZS = txt_shoe_no.Text == "" ? 0 : Convert.ToDouble(txt_shoe_no.Text);
            for (int i = 0; i < index; i++)
            {
                BL = dataGridView1.Rows[i].Cells["ng_qty"].Value.ToString() == "" ? 0 : Convert.ToDouble(dataGridView1.Rows[i].Cells["ng_qty"].Value.ToString());
                if (BL != 0)
                {
                    if (BL > SCZS)
                    {
                        dataGridView1.Rows[i].Cells["ng_qty"].Value = "";
                        dataGridView1.Rows[i].Cells["ng_rate"].Value = "";
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("The bad quantity cannot be greater than the total production!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["ng_rate"].Value = Math.Round((BL / SCZS * 100), 2).ToString() + "%";
                    }
                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
