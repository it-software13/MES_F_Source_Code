using DataGrid.DataGridViewCustomColumn;
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

namespace SJeMES_TQC.UControl
{
    public partial class UC_DQLMQL_Check : UserControl
    {
        DataTable dt = new DataTable();
        private TQC_Task_Check tqc;
        public List<code_value_pass_fail> pf = new List<code_value_pass_fail>();
        string id = string.Empty;//dqa&mqa的id
        string task_no = string.Empty;
        string ck = string.Empty;
        public UC_DQLMQL_Check()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public UC_DQLMQL_Check(DataTable _dt, TQC_Task_Check _tqc, string _ck)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dt = _dt;
            tqc = _tqc;
            ck = _ck;
        }

        private void UC_DQLMQL_Check_Load(object sender, EventArgs e)
        {
            if (ck == "true")
            {
                button1.Visible = false;
            }
            pf.Add(new code_value_pass_fail() { CODE = "0", VALUE = "PASS" });
            pf.Add(new code_value_pass_fail() { CODE = "1", VALUE = "FAIL" });
            comboBox1.DataSource = pf;
            comboBox1.DisplayMember = "VALUE";
            comboBox1.ValueMember = "CODE";

            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0]["id"].ToString();
                task_no = dt.Rows[0]["task_no"].ToString();
                label8.Text = dt.Rows[0]["choice_name"].ToString();
                label9.Text = dt.Rows[0]["inspection_name"].ToString();
                label10.Text = dt.Rows[0]["enum_value"].ToString();
                label11.Text = dt.Rows[0]["standard_value"].ToString();
                label12.Text = dt.Rows[0]["unit"].ToString();
                label13.Text = dt.Rows[0]["remark"].ToString();
                label14.Text = dt.Rows[0]["other_measures"].ToString();
                label15.Text = dt.Rows[0]["source"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["file_url"].ToString()))
                {
                    try
                    {
                        var webC = new System.Net.WebClient();
                        string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["file_url"].ToString());
                        Image image = new Bitmap(webC.OpenRead(url));
                        pictureBox1.Image = image;
                        //image.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch { }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }

            int index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells["检验总数"].Value = "";
            this.dataGridView1.Rows[index].Cells["合格数量"].Value = "";
            this.dataGridView1.Rows[index].Cells["不良问题描述"].Value = "";
            this.dataGridView1.Rows[index].Cells["检验结果"].Value = "";
            this.dataGridView1.Rows[index].Cells["图片集合"].Value = "";
            this.dataGridView1.Rows[index].Cells["检验结果代号"].Value = "";

            GetTQC_Task_Edit_ART();
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "检验总数") // 检验总数 
                {
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    comboBox1.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["检验总数"].Value is null ? "" : dataGridView1.CurrentRow.Cells["检验总数"].Value.ToString();
                    string jyzs = aa == "" ? "" : aa;
                    textBox1.Text = jyzs; //检验总数

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox1.Visible = true;
                    textBox1.Focus();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "合格数量") // 合格数量 
                {
                    textBox1.Visible = false;
                    textBox3.Visible = false;
                    comboBox1.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["合格数量"].Value is null ? "" : dataGridView1.CurrentRow.Cells["合格数量"].Value.ToString();
                    string hgsl = aa == "" ? "" : aa;
                    textBox2.Text = hgsl; //合格数量

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox2.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox2.Visible = true;
                    textBox2.Focus();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "不良问题描述") // 不良问题描述 
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    comboBox1.Visible = false;
                    string aa = dataGridView1.CurrentRow.Cells["不良问题描述"].Value is null ? "" : dataGridView1.CurrentRow.Cells["不良问题描述"].Value.ToString();
                    string blwt = aa == "" ? "" : aa;
                    textBox3.Text = blwt; //不良问题描述

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    textBox3.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    textBox3.Visible = true;
                    textBox3.Focus();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "检验结果") // 检验结果 
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    string jyjg = dataGridView1.CurrentRow.Cells["检验结果代号"].Value.ToString(); //对combobox赋值
                    comboBox1.SelectedValue = jyjg;

                    Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                    comboBox1.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    comboBox1.Visible = true;
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "图片上传")
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    textBox3.Visible = false;
                    comboBox1.Visible = false;
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["图片上传"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("upload"))
                    {
                        //创建文件弹出选择窗口（包括文件名）对象
                        OpenFileDialog ofd = new OpenFileDialog();
                        //判断选择的路径
                        string path = string.Empty;
                        ofd.Title = "请选择文件夹";
                        ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
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
                                    if (dataGridView1.Rows[e.RowIndex].Cells["图片集合"].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["图片集合"].Value.ToString()))
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells["图片集合"].Value = dataGridView1.Rows[e.RowIndex].Cells["图片集合"].Value + "," + resultDIC["guid"].ToString();
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[e.RowIndex].Cells["图片集合"].Value = resultDIC["guid"].ToString();
                                    }
                                    //string union_id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                                    //TQC_Task_Edit_Upload(union_id, resultDIC["guid"].ToString());
                                }
                            }

                        }
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "查看历史")
                {
                    textBox2.Visible = false;
                    textBox1.Visible = false;
                    textBox3.Visible = false;
                    comboBox1.Visible = false;
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["查看历史"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("select"))
                    {
                        using (DQAMQA_LIST d = new DQAMQA_LIST(id, label15.Text, task_no, ck))
                        {
                            d.ShowDialog();
                        }
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = textBox3.Text.ToString();
        }

        public class code_value_pass_fail
        {
            public string CODE { get; set; }
            public string VALUE { get; set; }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = comboBox1.Text;
            dataGridView1.CurrentRow.Cells["检验结果代号"].Value = comboBox1.SelectedValue.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditDQAMQA();
        }

        /// <summary>
        /// dqa&mqa核对页面编辑
        /// </summary>
        public void EditDQAMQA()
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                data.Add("id", id);
                data.Add("source_type", label15.Text);
                data.Add("tqc_task_check_t_f", GetDgvToTable(dataGridView1));
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "EditDQAMQA", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(tqc, msg);
                    GetTQC_Task_Edit_ART();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(tqc, msg);
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
        /// DQA管理页面添加页签查询工段
        /// </summary>
        /// <param name="OBJ"></param>
        /// <returns></returns>
        public void GetTQC_Task_Edit_ART()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("keycode", "");
                data.Add("id", id);
                data.Add("task_no", task_no);
                data.Add("source_type", label15.Text);
                data.Add("pageSize", "15");
                data.Add("pageIndex", "1");
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetDQAMQA_history",//方法名
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
                    dataGridView1.Rows[0].Cells["检验总数"].Value = dt.Rows[0]["qty"].ToString();
                    dataGridView1.Rows[0].Cells["合格数量"].Value = dt.Rows[0]["q_qty"].ToString();
                    dataGridView1.Rows[0].Cells["不良问题描述"].Value = dt.Rows[0]["bad_desc"].ToString();
                    dataGridView1.Rows[0].Cells["检验结果代号"].Value = dt.Rows[0]["check_res"].ToString();
                    dataGridView1.Rows[0].Cells["检验结果"].Value = dt.Rows[0]["check_res"].ToString();
                    dataGridView1.Rows[0].Cells["图片集合"].Value = "";
                }
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(tqc, msg);
            }
        }
    }
}
