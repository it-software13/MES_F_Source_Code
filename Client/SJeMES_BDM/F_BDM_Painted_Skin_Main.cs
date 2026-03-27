using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
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

namespace SJeMES_BDM
{
    public partial class F_BDM_Painted_Skin_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_Painted_Skin_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "   ";

            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "   ";
        }

        private void F_BDM_Painted_Skin_Main_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            GetPainted_Skin_Main_State();
            pageControl1.BindPageEvent += GetPainted_Skin_Main;
            //LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["ywc"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView1.Columns["csh"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView1.Columns["jxz"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        public void GetPainted_Skin_Main_State()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_Main_State",//方法名
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
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "enum_value";
                    comboBox1.ValueMember = "enum_code";
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 画皮主页查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetPainted_Skin_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("item_no", textBox1.Text);//料号
                data.Add("task_state", comboBox1.SelectedValue.ToString());//状态
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    data.Add("wh_date_start", dateTimePicker1.Value.ToString("yyyy-MM-dd"));//范围开始时间
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    data.Add("wh_date_end", dateTimePicker2.Value.ToString("yyyy-MM-dd"));//范围结束时间
                }
                if (string.IsNullOrWhiteSpace(dateTimePicker1.Text)|| string.IsNullOrWhiteSpace(dateTimePicker2.Text))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the necessary conditions and then execute the query, prompt: time range for warehouse entry！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                data.Add("vend_name", textBox4.Text);//生产厂商
                data.Add("item_name", textBox5.Text);//材料名称
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Painted_Skin",//类名
                                            "GetPainted_Skin_Main",//方法名
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
                        dgvr.Cells["task_no"].Value = dr["task_no"].ToString();
                        dgvr.Cells["item_no"].Value = dr["item_no"].ToString();
                        dgvr.Cells["item_name"].Value = dr["item_name"].ToString();
                        dgvr.Cells["vend_no"].Value = dr["vend_no"].ToString();
                        dgvr.Cells["vend_name"].Value = dr["vend_name"].ToString();
                        dgvr.Cells["wh_date"].Value = dr["wh_date"].ToString();
                        dgvr.Cells["mtl_qty"].Value = dr["mtl_qty"].ToString();
                        dgvr.Cells["yhp_qty"].Value = dr["yhp_qty"].ToString();
                        dgvr.Cells["task_state"].Value = dr["task_state"].ToString();
                        if (dr["task_state"].ToString() == "initialization")//initialization//初始化
                        {
                            dataGridView1.Rows[i].Cells["ywc"] = new DataGridViewOperationCell();
                            dataGridView1.Rows[i].Cells["jxz"] = new DataGridViewOperationCell();
                        }
                        else if (dr["task_state"].ToString() == "inprogress")//In progress//进行中
                        {
                            dataGridView1.Rows[i].Cells["ywc"] = new DataGridViewOperationCell();
                            dataGridView1.Rows[i].Cells["csh"] = new DataGridViewOperationCell();
                        }
                        else if (dr["task_state"].ToString() == "completed")//completed//已完成
                        {
                            dataGridView1.Rows[i].Cells["jxz"] = new DataGridViewOperationCell();
                            dataGridView1.Rows[i].Cells["csh"] = new DataGridViewOperationCell();
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["jxz"] = new DataGridViewOperationCell();
                            dataGridView1.Rows[i].Cells["csh"] = new DataGridViewOperationCell();
                            dataGridView1.Rows[i].Cells["ywc"] = new DataGridViewOperationCell();
                        }
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["ywc"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["csh"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["jxz"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadPage();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (F_BDM_Painted_Skin_Insert f = new F_BDM_Painted_Skin_Insert())
            {
                f.ShowDialog();
            }
        }

        /// <summary>
        /// 画皮删除
        /// </summary>
        public void InsertPainted_Skin_Delete(string task_no)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_BDMAPI", "SJ_BDMAPI.BDM_Painted_Skin", "InsertPainted_Skin_Delete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "csh")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["csh"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("UPDATE"))//操作
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();
                        using (F_BDM_Painted_Skin_Edit f=new F_BDM_Painted_Skin_Edit(task_no))
                        {
                            f.ShowDialog();
                        }
                    }
                    else if (cell.CurrentItem.Equals("DELETE"))//删除
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();//任务编号
                        if (!string.IsNullOrEmpty(task_no))
                        {
                            DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Delete the painting skin", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (dr == DialogResult.OK)
                            {
                                InsertPainted_Skin_Delete(task_no);
                            }
                        }
                    }
                    else if (cell.CurrentItem.Equals("SELECT"))//查看进度
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();//任务编号
                        string task_state = dataGridView1.Rows[e.RowIndex].Cells["task_state"].Value.ToString();//状态
                        using (F_BDM_Painted_Skin_List f=new F_BDM_Painted_Skin_List(task_no, task_state))
                        {
                            f.ShowDialog();
                        }
                    }

                    LoadPage();
                }
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "jxz")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["jxz"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("UPDATE"))//操作
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();
                        using (F_BDM_Painted_Skin_Edit f = new F_BDM_Painted_Skin_Edit(task_no))
                        {
                            f.ShowDialog();
                        }
                    }
                    else if (cell.CurrentItem.Equals("SELECT"))//查看进度
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();//任务编号
                        string task_state = dataGridView1.Rows[e.RowIndex].Cells["task_state"].Value.ToString();//状态
                        using (F_BDM_Painted_Skin_List f = new F_BDM_Painted_Skin_List(task_no,task_state))
                        {
                            f.ShowDialog();
                        }
                    }

                    LoadPage();
                }
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "ywc")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["ywc"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("UPDATE"))//操作
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();
                        string state = dataGridView1.Rows[e.RowIndex].Cells["task_state"].Value.ToString();//状态
                        using (F_BDM_Painted_Skin_Edit f = new F_BDM_Painted_Skin_Edit(task_no,state))
                        {
                            f.ShowDialog();
                        }
                    }
                    else if (cell.CurrentItem.Equals("SELECT"))//查看进度
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();//任务编号
                        string task_state = dataGridView1.Rows[e.RowIndex].Cells["task_state"].Value.ToString();//状态
                        using (F_BDM_Painted_Skin_List f = new F_BDM_Painted_Skin_List(task_no,task_state))
                        {
                            f.ShowDialog();
                        }
                    }
                    else if (cell.CurrentItem.Equals("BAOGAO"))//报告
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();//任务编号
                        using (F_BDM_Painted_Skin_Report f = new F_BDM_Painted_Skin_Report(task_no))
                        {
                            f.ShowDialog();
                        }
                    }
                    LoadPage();
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Long;
            this.dateTimePicker1.CustomFormat = null;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker2.Format = DateTimePickerFormat.Long;
            this.dateTimePicker2.CustomFormat = null;
        }
    }
}
