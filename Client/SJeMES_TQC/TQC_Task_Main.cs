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

namespace SJeMES_TQC
{
    public partial class TQC_Task_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        // public string task_state = "0,1";//0 in progress, 2 ended, 1 stopped
        public string task_state = "0,1,3,4";//Default display: 0 in progress, 2 ended, 1 stopped, 3 rummaging in progress, 4 rummaging stopped
        public TQC_Task_Main()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimePicker1);
            InitDateTimePicker(dateTimePicker2);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "   ";

            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "   ";

          

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

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// tqc主页查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetTQC_Task_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    data.Add("datestart", dateTimePicker1.Value.ToString("yyyy-MM-dd"));//条件 日期开始
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    data.Add("dateend", dateTimePicker2.Value.ToString("yyyy-MM-dd"));//条件 日期结束
                }
                data.Add("task_state", task_state);//任务状态
                data.Add("shoe_no", textBox1.Text);//鞋型
                data.Add("prod_no", textBox4.Text);//art
                //data.Add("workshop_section", textBox5.Text);//工段
                data.Add("department", textBox6.Text);//部门
                data.Add("production_line", textBox3.Text);//art
                data.Add("mer_po", textBox2.Text);//po
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",//类库名
                                            "SJ_TQCAPI.TQC_Task",//类名
                                            "GetTQC_Task_Main",//方法名
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
                        dgvr.Cells["序号"].Value = i + 1;
                        dgvr.Cells["aqlresult"].Value = dr["aqlresult"].ToString();
                        dgvr.Cells["task_id"].Value = dr["ID"].ToString();
                        dgvr.Cells["PO"].Value = dr["mer_po"].ToString();
                        dgvr.Cells["任务编号"].Value = dr["task_no"].ToString();
                        dgvr.Cells["日期"].Value = dr["createdate"].ToString();
                        dgvr.Cells["部门"].Value = dr["department"].ToString();
                        dgvr.Cells["组别"].Value = dr["production_line_name"].ToString();
                        dgvr.Cells["鞋型"].Value = dr["shoe_no"].ToString();
                        dgvr.Cells["name_tt"].Value = dr["name_tt"].ToString();
                        dgvr.Cells["ART"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["检验总数"].Value = dr["total"].ToString();
                        dgvr.Cells["首次合格总数"].Value = dr["FirstQualifiedNum"].ToString();
                        dgvr.Cells["合格总数"].Value = dr["qualified"].ToString();
                        dgvr.Cells["B品数量"].Value = dr["bnum"].ToString();
                        dgvr.Cells["产线总合格率"].Value = Math.Round((Convert.ToDecimal(dr["totalpass"]) * 100),2).ToString() + "%";
                        dgvr.Cells["RFT首次合格率"].Value = Math.Round((Convert.ToDecimal(dr["rftpass"]) * 100),2).ToString() + "%";
                        dgvr.Cells["状态"].Value = dr["task_state"].ToString();
                        //dgvr.Cells["INSPECTION_NAME_1"].Value = dr["INSPECTION_NAME_1"].ToString();
                        //dgvr.Cells["Fail_Quantity_1"].Value = dr["Fail_Quantity_1"].ToString();
                        //dgvr.Cells["INSPECTION_NAME_2"].Value = dr["INSPECTION_NAME_2"].ToString();
                        //dgvr.Cells["Fail_Quantity_2"].Value = dr["Fail_Quantity_2"].ToString();
                        //dgvr.Cells["INSPECTION_NAME_3"].Value = dr["INSPECTION_NAME_3"].ToString();
                        //dgvr.Cells["Fail_Quantity_3"].Value = dr["Fail_Quantity_3"].ToString();

                        if (dr["task_state"].ToString() == "Over")
                        {
                            this.dataGridView1.Rows[i].Cells["继续录入"] = new DataGridViewOperationCell();
                        }
                        else
                        {
                            this.dataGridView1.Rows[i].Cells["查看"] = new DataGridViewOperationCell();
                        }
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["删除"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["继续录入"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["查看"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void TQC_Task_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //task_state = "0,1";
            btn_jxz.FlatStyle = FlatStyle.Flat;
            btn_jxz.BackColor = Color.SkyBlue;
            btn_jxz.FlatAppearance.BorderColor = btn_jxz.BackColor;


            btn_yjs.FlatStyle = FlatStyle.Flat;
            btn_yjs.BackColor = Color.Gray;
            btn_yjs.FlatAppearance.BorderColor = btn_yjs.BackColor;
            pageControl1.BindPageEvent += GetTQC_Task_Main;
            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["删除"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView1.Columns["继续录入"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView1.Columns["查看"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (TQC_Task_Edit t = new TQC_Task_Edit())
            {
                t.ShowDialog();
            }
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "继续录入")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["继续录入"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("edit"))
                    { 
                        string task_id = dataGridView1.Rows[e.RowIndex].Cells["task_id"].Value.ToString();
                        List<bool> tipsRes = new List<bool>();
                        tipsRes.Add(false);
                        TQC_Task_Main_Opra_Confirm tQC_Task_Main_Opra_Confirm = new TQC_Task_Main_Opra_Confirm(task_id, tipsRes);
                        tQC_Task_Main_Opra_Confirm.ShowDialog();
                        if (tipsRes[0])
                        {
                            string task_no = dataGridView1.Rows[e.RowIndex].Cells["任务编号"].Value.ToString();
                            using (TQC_Task_Edit t = new TQC_Task_Edit(task_no))
                            {
                                t.ShowDialog();
                            }
                            LoadPage();
                        }
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "删除")
                { 
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["删除"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("delete"))
                    {
                        string task_id = dataGridView1.Rows[e.RowIndex].Cells["task_id"].Value.ToString();
                        List<bool> tipsRes = new List<bool>();
                        tipsRes.Add(false);
                        TQC_Task_Main_Opra_Confirm tQC_Task_Main_Opra_Confirm = new TQC_Task_Main_Opra_Confirm(task_id, tipsRes);
                        tQC_Task_Main_Opra_Confirm.ShowDialog();
                        if (tipsRes[0])
                        {
                            string task_no = dataGridView1.Rows[e.RowIndex].Cells["任务编号"].Value.ToString();
                            Delete_TQC_Task_Main(task_no);

                        }
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "查看")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["查看"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("select"))
                    {
                        string task_no = dataGridView1.Rows[e.RowIndex].Cells["任务编号"].Value.ToString();
                        using (TQC_Task_Edit t = new TQC_Task_Edit(task_no,"true"))
                        {
                            t.ShowDialog();
                        }
                        LoadPage();
                    }
                }
            }
        }



        /// <summary>
        /// tqc主页删除
        /// </summary>
        public void Delete_TQC_Task_Main(string task_no)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("task_no", task_no);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_TQCAPI", "SJ_TQCAPI.TQC_Task", "Delete_TQC_Task_Main", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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

        private void btn_jxz_Click(object sender, EventArgs e)
        {
            task_state = "0,1";
            btn_jxz.FlatStyle = FlatStyle.Flat;
            btn_jxz.BackColor = Color.SkyBlue;
            btn_jxz.FlatAppearance.BorderColor = btn_jxz.BackColor;

            btn_yjs.FlatStyle = FlatStyle.Flat;
            btn_yjs.BackColor = Color.Gray;
            btn_yjs.FlatAppearance.BorderColor = btn_yjs.BackColor;
            LoadPage();
        }

        private void btn_yjs_Click(object sender, EventArgs e)
        {
            task_state = "2";
            btn_yjs.FlatStyle = FlatStyle.Flat;
            btn_yjs.BackColor = Color.SkyBlue;
            btn_yjs.FlatAppearance.BorderColor = btn_jxz.BackColor;

            btn_jxz.FlatStyle = FlatStyle.Flat;
            btn_jxz.BackColor = Color.Gray;
            btn_jxz.FlatAppearance.BorderColor = btn_yjs.BackColor;
            LoadPage();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cell = row.Cells[3];
                string result = cell.Value.ToString();
                switch (result)
                {
                    case "Accepted":
                        cell.Style.BackColor = Color.Green;
                        break;
                    case "Rejected":
                        cell.Style.BackColor = Color.Red;
                        break;
                    case "Not_Inspected":
                        cell.Style.BackColor = Color.Yellow;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
