using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using SJeMES_Shared_Form.SJeMES_AQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Shared_Form
{
    public partial class F_AQL_Aqlreport_New : MaterialForm
    {
        private Dictionary<string, object> dics = new Dictionary<string, object>();
        public string sumnum = "";//实际双数
        public F_AQL_Aqlreport_New(Dictionary<string,object> _dic, SJeMES_Framework.Class.ClientClass _Client)
        {
            InitializeComponent();
            dics = _dic;
            Program.Client = _Client;
        }
        private void F_AQL_Aqlreport_Load(object sender, EventArgs e)
        {

            //top1
            this.dataGridView1.RowHeadersVisible = false;//隐藏第一列

            //center one 1,2,3,4
            this.dataGridViewa1.RowHeadersVisible = false;//隐藏第一列
            this.dataGridViewb1.RowHeadersVisible = false;//隐藏第一列
            this.dataGridViewc1.RowHeadersVisible = false;//隐藏第一列
            this.dataGridViewd1.RowHeadersVisible = false;//隐藏第一列
                      
            //center two 1,2，3
            this.dataGridViewcenter1.RowHeadersVisible = false;//隐藏第一列
            this.dataGridViewcenter2.RowHeadersVisible = false;//隐藏第一列
            this.dataGridViewcenter3.RowHeadersVisible = false;//隐藏第一列
            this.dataGridViewcenter1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;//隐藏表头hread
            this.dataGridViewcenter2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;//隐藏表头hread

            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;//隐藏表头hread
            this.dataGridView1.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
            this.dataGridView1.AllowUserToResizeColumns = false;
            GetListView();//加载中间长盒子内容
            topviewinput();//加载头部内

            //SJeMES_Framework.Common.UIHelper.LoadDgv(dgv_bad_item1);
            //SJeMES_Framework.Common.UIHelper.LoadDgv(dgv_bad_item2);

        }
        #region 头部oneView
        private string vals=string.Empty;
        private string level = string.Empty;
        private string xnum = string.Empty;
        private string snum = string.Empty;
        private string ac = string.Empty;
        private string ac12 = string.Empty;
        private string ac13 = string.Empty;
        private void topviewinput()
        {
            try
            {
                //top1
                if (dataGridView1.Rows.Count < 1)
                {
                    DataTable dt = topviewdata();
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["Column1"].Value = dr["Column1"].ToString();
                        dgvr.Cells["Column2"].Value = dr["Column2"].ToString();
                        dgvr.Cells["Column3"].Value = dr["Column3"].ToString();
                        dgvr.Cells["Column4"].Value = dr["Column4"].ToString();
                        dgvr.Cells["Column5"].Value = dr["Column5"].ToString();
                        dgvr.Cells["Column6"].Value = dr["Column6"].ToString();
                        dgvr.Cells["Column7"].Value = "";
                        //验货状态才赋值验货日期
                        if (dr["Column6"].ToString().Trim() == "Inspection date")
                        {
                            if (dics["yhstatus"].ToString().Trim() == "Inspected")
                                dgvr.Cells["Column7"].Value = dr["Column7"].ToString();
                        }
                        else
                            dgvr.Cells["Column7"].Value = dr["Column7"].ToString();

                        i++;
                    }
                    dataGridView1.Rows[0].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[1].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[2].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[0].Cells[2].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[0].Cells[3].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[1].Cells[3].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[2].Cells[3].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[0].Cells[5].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[1].Cells[5].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridView1.Rows[2].Cells[5].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                }
                int k = 2;
                //center two 1
                string lefttest = string.Empty;
                string righttest = string.Empty;
                if (dataGridViewcenter1.Rows.Count < 1)
                {
                    lefttest = string.Empty;
                    righttest = string.Empty;
                    for (int j   = 0; j < k; j++)
                    {
                        dataGridViewcenter1.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewcenter1.Rows[j];
                        if (j == 0)
                        {
                            lefttest = "Sample lot"; righttest = vals;
                        }

                        else
                        {
                            lefttest = "Sample size"; righttest = level;
                        }
                        dgvr.Cells["Columncentera1"].Value =lefttest;
                        dgvr.Cells["Columncentera2"].Value = righttest;
                        
                    }
                    dataGridViewcenter1.Height=(this.dataGridViewcenter1.GetCellDisplayRectangle(this.dataGridViewcenter1.CurrentCell.ColumnIndex, this.dataGridViewcenter1.CurrentCell.RowIndex, true).Height) * (k+1);
                    dataGridViewcenter1.Rows[0].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridViewcenter1.Rows[1].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                }
                //center two 2
                if (dataGridViewcenter2.Rows.Count < 1)
                {
                    lefttest = string.Empty;
                    righttest = string.Empty;
                    for (int j = 0; j < k; j++)
                    {
                        dataGridViewcenter2.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewcenter2.Rows[j];
                        if (j == 0)
                        {
                            lefttest = "Cartons"; righttest = xnum;
                        }
                        else
                        {
                            lefttest = "Pairs"; righttest = snum;
                        }
                        dgvr.Cells["Columncenterb1"].Value = lefttest;
                        dgvr.Cells["Columncenterb2"].Value = righttest;

                    }
                    dataGridViewcenter2.Height = (this.dataGridViewcenter2.GetCellDisplayRectangle(this.dataGridViewcenter2.CurrentCell.ColumnIndex, this.dataGridViewcenter2.CurrentCell.RowIndex, true).Height) * (k + 1);
                    dataGridViewcenter2.Rows[0].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridViewcenter2.Rows[1].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                }
                //center two 3
                if (dataGridViewcenter3.Rows.Count < 1)
                {
                    lefttest = string.Empty;
                    for (int j = 0; j < k; j++)
                    {
                        dataGridViewcenter3.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewcenter3.Rows[j];
                        if (j == 0)
                        {
                            lefttest = "Max.defect(s)accpet";
                            dgvr.Cells["Columncenterc1"].Value = lefttest;
                            dgvr.Cells["Columncenterc2"].Value = ac13;
                            dgvr.Cells["Columncenterc3"].Value = ac12;
                            dgvr.Cells["Columncenterc4"].Value = "0";
                        }
                        else {
                            lefttest = "No.of defct(s)to reject";
                            dgvr.Cells["Columncenterc1"].Value = lefttest;
                            dgvr.Cells["Columncenterc2"].Value = (Convert.ToInt32(ac13)+1).ToString();
                            dgvr.Cells["Columncenterc3"].Value = (Convert.ToInt32(ac12) + 1).ToString();
                            dgvr.Cells["Columncenterc4"].Value = (Convert.ToInt32("0") + 1).ToString();
                        }
                      
                       


                    }
                    dataGridViewcenter3.Height = (this.dataGridViewcenter3.GetCellDisplayRectangle(this.dataGridViewcenter3.CurrentCell.ColumnIndex, this.dataGridViewcenter3.CurrentCell.RowIndex, true).Height) * (k + 1);
                    dataGridViewcenter3.Rows[0].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                    dataGridViewcenter3.Rows[1].Cells[0].Style.Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);
                }
                GetREJECT_AccEpted();//加载底部ac判断的单选框




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        //top1demo
        private DataTable dt_top = new DataTable();
        private DataTable topviewdata()
        {
            string art_no = string.Empty;//art编号
            string shoe_name = string.Empty;//鞋型名称
            string factoy = string.Empty;//工厂
            string po = string.Empty;//po号码
            string po_num = string.Empty;//po数量
            string yhrq = string.Empty;//验货日期
            string Customer = string.Empty;//客户
            string star = "";//最终结果
            string brand = string.Empty;
            string star2 = "";//再次检验
            if (dt_top.Rows.Count > 0)
            {
                DateTime inspection_date = DateTime.Now;
                bool is_date = DateTime.TryParse(dt_top.Rows[0]["inspection_date"].ToString(), out inspection_date);

                art_no = dt_top.Rows[0]["art_no"].ToString();
                shoe_name = dt_top.Rows[0]["shoe_name"].ToString();
                po = dt_top.Rows[0]["po"].ToString();
                po_num = dt_top.Rows[0]["po_num"].ToString();
                yhrq = is_date ? inspection_date.ToString("yyyyMMdd") : "";
                Customer = dt_top.Rows[0]["guojia"].ToString();
                factoy = "APACHE";
                brand = "adidas";
                star = "";
                if (dt_top.Rows[0]["inspection_type"].ToString() == "0")
                {
                    star = "✔";
                    star2 = "";
                }
                else
                {
                    star = "";
                    star2 = "✔";
                }
                if (dt_top.Rows[0]["INSPECTION_STATE"].ToString() == "1")
                {
                    label9.Text = is_date ? inspection_date.ToString("yyyyMMdd") : "";
                    lbl_cherk_name.Text = dt_top.Rows[0]["checker"].ToString();
                }
                else
                {
                    label9.Text = "";
                    lbl_cherk_name.Text = "";
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Column1", typeof(string));
            dt.Columns.Add("Column2", typeof(string));
            dt.Columns.Add("Column3", typeof(string));
            dt.Columns.Add("Column4", typeof(string));
            dt.Columns.Add("Column5", typeof(string));
            dt.Columns.Add("Column6", typeof(string));
            dt.Columns.Add("Column7", typeof(string));
            DataRow dr = dt.NewRow();
            dr["Column1"] = "Factoy";
            dr["Column2"] = factoy;
            dr["Column3"] = "Brand";
            dr["Column4"] = "P.O.#";
            dr["Column5"] = po;
            dr["Column6"] = "Inspection date";
            dr["Column7"] = yhrq;
          
           

            
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Column1"] = "Article";
            dr["Column2"] = art_no;
            dr["Column3"] = brand;
            dr["Column4"] = "Total order qty";
            dr["Column5"] = po_num;
            dr["Column6"] = "Final Random inspection";
            dr["Column7"] = star;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Column1"] = "ModelName";
            dr["Column2"] = shoe_name;
            dr["Column3"] = "*";
            dr["Column4"] = "Customer";
            dr["Column5"] = Customer;
            dr["Column6"] = "Re-inspection";
            dr["Column7"] = star2;
            dt.Rows.Add(dr);
            return dt;
        }

        #endregion

        /// <summary>
        /// 计算双数 dgv 
        /// </summary>
        /// 
        public void CalculateEvenNumbers()
        {
            decimal sampleSize = 0;//样本量
            bool sampleSize_bool = decimal.TryParse(snum, out sampleSize);//双数
            decimal actualEvenNumber = 0;//实际双数
            bool actualEvenNumber_bool = decimal.TryParse(sumnum, out actualEvenNumber);//实际双数
            
            if (sampleSize_bool && actualEvenNumber_bool)
            {
                Dictionary<string, decimal> evenNumbersDic = new Dictionary<string, decimal>();
                if (dataGridViewa1 != null && dataGridViewa1.Rows.Count > 0)
                {

                    int dataGridView1_index = 0;
                    foreach (DataGridViewRow item in dataGridViewa1.Rows)
                    {
                        if (string.IsNullOrEmpty(item.Cells[$"订单量1"].Value.ToString()))
                            continue;
                        //当前行的双数
                        decimal curr_evenNumber = (Convert.ToDecimal(item.Cells[$"订单量1"].Value.ToString()) / actualEvenNumber) * sampleSize;
                        evenNumbersDic.Add($@"{dataGridViewa1.Name}-{dataGridView1_index}", curr_evenNumber);
                        dataGridView1_index++;
                    }
                }

                if (dataGridViewb1 != null && dataGridViewb1.Rows.Count > 0)
                {

                    int dataGridView1_index = 0;
                    foreach (DataGridViewRow item in dataGridViewb1.Rows)
                    {
                        if (string.IsNullOrEmpty(item.Cells[$"订单量2"].Value.ToString()))
                            continue;
                        //当前行的双数
                        decimal curr_evenNumber = (Convert.ToDecimal(item.Cells[$"订单量2"].Value.ToString()) / actualEvenNumber) * sampleSize;
                        evenNumbersDic.Add($@"{dataGridViewb1.Name}-{dataGridView1_index}", curr_evenNumber);
                        dataGridView1_index++;
                    }
                }

                if (dataGridViewc1 != null && dataGridViewc1.Rows.Count > 0)
                {

                    int dataGridView1_index = 0;
                    foreach (DataGridViewRow item in dataGridViewc1.Rows)
                    {
                        if (string.IsNullOrEmpty(item.Cells[$"订单量3"].Value.ToString()))
                            continue;
                        //当前行的双数
                        decimal curr_evenNumber = (Convert.ToDecimal(item.Cells[$"订单量3"].Value.ToString()) / actualEvenNumber) * sampleSize;
                        evenNumbersDic.Add($@"{dataGridViewc1.Name}-{dataGridView1_index}", curr_evenNumber);
                        dataGridView1_index++;
                    }
                }

                if (dataGridViewd1 != null && dataGridViewd1.Rows.Count > 0)
                {

                    int dataGridView1_index = 0;
                    foreach (DataGridViewRow item in dataGridViewd1.Rows)
                    {
                        if (string.IsNullOrEmpty(item.Cells[$"订单量4"].Value.ToString()))
                            continue;
                        //当前行的双数
                        decimal curr_evenNumber = (Convert.ToDecimal(item.Cells[$"订单量4"].Value.ToString()) / actualEvenNumber) * sampleSize;
                        evenNumbersDic.Add($@"{dataGridViewd1.Name}-{dataGridView1_index}", curr_evenNumber);
                        dataGridView1_index++;
                    }
                }

                //计算余数差值
                int addOne_count = Convert.ToInt32(sampleSize - evenNumbersDic.Sum(x => Math.Floor(x.Value)));

                evenNumbersDic = evenNumbersDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

                string[] keys = evenNumbersDic.Keys.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    if (i < addOne_count)
                    {
                        evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]) + 1;
                    }
                    else
                    {
                        evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]);
                    }
                }

                foreach (var item in evenNumbersDic)
                {
                    string[] key_info = item.Key.Split('-');
                    if (key_info[0] == "dataGridViewa1")
                    {
                        dataGridViewa1.Rows[Convert.ToInt32(key_info[1])].Cells["Columna4"].Value = item.Value.ToString();
                    }
                    else if (key_info[0] == "dataGridViewb1")
                    {
                        dataGridViewb1.Rows[Convert.ToInt32(key_info[1])].Cells["Columnb4"].Value = item.Value.ToString();
                    }
                    else if (key_info[0] == "dataGridViewc1")
                    {
                        dataGridViewc1.Rows[Convert.ToInt32(key_info[1])].Cells["Columnc4"].Value = item.Value.ToString();
                    }
                    else if (key_info[0] == "dataGridViewd1")
                    {
                        dataGridViewd1.Rows[Convert.ToInt32(key_info[1])].Cells["Columnd4"].Value = item.Value.ToString();
                    }
                }

            }
        }

        public void GetListView()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(dics["task_no"].ToString()))
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    //键值对传值
                    data.Add("task_no", dics["task_no"].ToString());//任务编号
                    data.Add("po", dics["po"].ToString());//po
                    data.Add("fpnum", dics["fpnum"].ToString());//PO数量
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_AQLAPI",//类库名
                                                "SJ_AQLAPI.AQL_Aqlreport",//类名
                                                "Get_Main",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(data));

                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    //视图数据显示
                    #region 加载中间两大动态模块视图
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    DataTable dt_head = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data_head"].ToString());
                    vals =dic["vals"].ToString();
                    level=dic["level"].ToString().Replace("一般检验水平", "").Replace("特殊检验水平", "");
                    xnum =dic["xnum"].ToString();
                    snum =dic["snum"].ToString();
                    ac = dic["ac"].ToString();
                    ac12 = dic["ac12"].ToString();
                    ac13 = dic["ac13"].ToString();

                    decimal bad_qty_total_1 = 0;//轻微不良
                    decimal bad_qty_total_2 = 0;//严重不良
                    decimal bad_qty_total_3 = 0;//重大不良
                    if (dt.Rows.Count > 0)
                    {
                        dgv_bad_item1.MaximumSize = new Size(this.dgv_bad_item1.Width, 0);
                        dgv_bad_item1.AutoSize = true;
                        dgv_bad_item2.MaximumSize = new Size(this.dgv_bad_item2.Width, 0);
                        dgv_bad_item2.AutoSize = true;
                        //平均分总行数
                        int totoalItemCount = (dt_head.Rows.Count + dt.Rows.Count) / 2;

                        if (dt_head.Rows.Count > 0)
                        {
                            //行数计数
                            int totoalItemCountCal = 1;
                            int wrap = 0;//0:未换表格；1:换表格；2：不再换表格；

                            var dt_head_sort = dt_head.Select().OrderBy(x => Convert.ToDecimal(x["bad_classify_code"].ToString().Replace(",", ""))).ToList();
                            foreach (DataRow head_item in dt_head_sort)
                            {
                                if (totoalItemCountCal <= totoalItemCount)
                                {//add to dgv_bad_item1
                                    int addHeadRow = dgv_bad_item1.Rows.Add();
                                    dgv_bad_item1.Rows[addHeadRow].Cells["CodeDefect"].Value = head_item["bad_classify_code"].ToString();
                                    dgv_bad_item1.Rows[addHeadRow].Cells["DEFECT_DESCRIPTION"].Value = head_item["bad_classify_name"].ToString();
                                    dgv_bad_item1.Rows[addHeadRow].DefaultCellStyle.BackColor = Color.Gray;
                                    totoalItemCountCal++;

                                    var findItemRows = dt.Select($@"bad_classify_code='{head_item["bad_classify_code"]}'");
                                    if (findItemRows != null && findItemRows.Length > 0)
                                    {
                                        int subItemIndex = 0;
                                        foreach (DataRow item in findItemRows.OrderBy(x => Convert.ToDecimal(x["bad_item_code"].ToString().Replace(",", ""))).ToArray())
                                        {
                                            if (totoalItemCountCal <= totoalItemCount)
                                            {//add to dgv_bad_item1
                                                int addItemRow = dgv_bad_item1.Rows.Add();
                                                dgv_bad_item1.Rows[addItemRow].Cells["CodeDefect"].Value = item["bad_item_code"].ToString();
                                                dgv_bad_item1.Rows[addItemRow].Cells["DEFECT_DESCRIPTION"].Value = item["bad_item_name"].ToString();
                                                dgv_bad_item1.Rows[addItemRow].Cells["BAD_STANDARD"].Value = item["bad_standard"].ToString();

                                                string problem_level = item["problem_level"].ToString();

                                                dgv_bad_item1.Rows[addItemRow].Cells["MINOR_DEFECT"].Value = "";
                                                dgv_bad_item1.Rows[addItemRow].Cells["MAJOR_DEFECT"].Value = "";
                                                dgv_bad_item1.Rows[addItemRow].Cells["CRITICAL_DEFECT"].Value = "";
                                                decimal bad_qty = -1;
                                                string bad_qty_str = " ";
                                                bool bad_qty_convert = decimal.TryParse(item["bad_qty"].ToString(), out bad_qty);
                                                if (bad_qty_convert && bad_qty != 0)
                                                    bad_qty_str = bad_qty.ToString();
                                                switch (problem_level)
                                                {
                                                    case "0":
                                                        dgv_bad_item1.Rows[addItemRow].Cells["MAJOR_DEFECT"].Value = bad_qty_str;
                                                        if (bad_qty_convert)
                                                            bad_qty_total_2 += bad_qty;
                                                        break;
                                                    case "1":
                                                        dgv_bad_item1.Rows[addItemRow].Cells["MINOR_DEFECT"].Value = bad_qty_str;
                                                        if (bad_qty_convert)
                                                            bad_qty_total_1 += bad_qty;
                                                        break;
                                                    case "2":
                                                        dgv_bad_item1.Rows[addItemRow].Cells["CRITICAL_DEFECT"].Value = bad_qty_str;
                                                        if (bad_qty_convert)
                                                            bad_qty_total_3 += bad_qty;
                                                        break;
                                                    default:
                                                        break;
                                                }

                                                totoalItemCountCal++;
                                            }
                                            else
                                            {//add to dgv_bad_item2
                                                if (subItemIndex == 0 && wrap == 0)
                                                {
                                                    int addHeadRowHH = dgv_bad_item2.Rows.Add();
                                                    dgv_bad_item2.Rows[addHeadRowHH].Cells["CodeDefect2"].Value = head_item["bad_classify_code"].ToString();
                                                    dgv_bad_item2.Rows[addHeadRowHH].Cells["DEFECT_DESCRIPTION2"].Value = head_item["bad_classify_name"].ToString();
                                                    dgv_bad_item2.Rows[addHeadRowHH].DefaultCellStyle.BackColor = Color.Gray;

                                                    wrap = 1;
                                                }

                                                int addItemRow = dgv_bad_item2.Rows.Add();
                                                dgv_bad_item2.Rows[addItemRow].Cells["CodeDefect2"].Value = item["bad_item_code"].ToString();
                                                dgv_bad_item2.Rows[addItemRow].Cells["DEFECT_DESCRIPTION2"].Value = item["bad_item_name"].ToString();
                                                dgv_bad_item2.Rows[addItemRow].Cells["BAD_STANDARD2"].Value = item["bad_standard"].ToString();

                                                string problem_level = item["problem_level"].ToString();

                                                dgv_bad_item2.Rows[addItemRow].Cells["MINOR_DEFECT2"].Value = "";
                                                dgv_bad_item2.Rows[addItemRow].Cells["MAJOR_DEFECT2"].Value = "";
                                                dgv_bad_item2.Rows[addItemRow].Cells["CRITICAL_DEFECT2"].Value = "";
                                                decimal bad_qty = -1;
                                                string bad_qty_str = " ";
                                                bool bad_qty_convert = decimal.TryParse(item["bad_qty"].ToString(), out bad_qty);
                                                if (bad_qty_convert && bad_qty != 0)
                                                    bad_qty_str = bad_qty.ToString();
                                                switch (problem_level)
                                                {
                                                    case "0":
                                                        dgv_bad_item2.Rows[addItemRow].Cells["MAJOR_DEFECT2"].Value = bad_qty_str;
                                                        if (bad_qty_convert)
                                                            bad_qty_total_2 += bad_qty;
                                                        break;
                                                    case "1":
                                                        dgv_bad_item2.Rows[addItemRow].Cells["MINOR_DEFECT2"].Value = bad_qty_str;
                                                        if (bad_qty_convert)
                                                            bad_qty_total_1 += bad_qty;
                                                        break;
                                                    case "2":
                                                        dgv_bad_item2.Rows[addItemRow].Cells["CRITICAL_DEFECT2"].Value = bad_qty_str;
                                                        if (bad_qty_convert)
                                                            bad_qty_total_3 += bad_qty;
                                                        break;
                                                    default:
                                                        break;
                                                }

                                                totoalItemCountCal++;
                                            }
                                        }
                                    }

                                }
                                else
                                {//add to dgv_bad_item2
                                    int addHeadRow = dgv_bad_item2.Rows.Add();
                                    dgv_bad_item2.Rows[addHeadRow].Cells["CodeDefect2"].Value = head_item["bad_classify_code"].ToString();
                                    dgv_bad_item2.Rows[addHeadRow].Cells["DEFECT_DESCRIPTION2"].Value = head_item["bad_classify_name"].ToString();
                                    dgv_bad_item2.Rows[addHeadRow].DefaultCellStyle.BackColor = Color.Gray;
                                    totoalItemCountCal++;

                                    var findItemRows = dt.Select($@"bad_classify_code='{head_item["bad_classify_code"]}'");
                                    if (findItemRows != null && findItemRows.Length > 0)
                                    {
                                        foreach (DataRow item in findItemRows.OrderBy(x => Convert.ToDecimal(x["bad_item_code"].ToString().Replace(",", ""))).ToArray())
                                        {
                                            int addItemRow = dgv_bad_item2.Rows.Add();
                                            dgv_bad_item2.Rows[addItemRow].Cells["CodeDefect2"].Value = item["bad_item_code"].ToString();
                                            dgv_bad_item2.Rows[addItemRow].Cells["DEFECT_DESCRIPTION2"].Value = item["bad_item_name"].ToString();
                                            dgv_bad_item2.Rows[addItemRow].Cells["BAD_STANDARD2"].Value = item["bad_standard"].ToString();

                                            string problem_level = item["problem_level"].ToString();

                                            dgv_bad_item2.Rows[addItemRow].Cells["MINOR_DEFECT2"].Value = "";
                                            dgv_bad_item2.Rows[addItemRow].Cells["MAJOR_DEFECT2"].Value = "";
                                            dgv_bad_item2.Rows[addItemRow].Cells["CRITICAL_DEFECT2"].Value = "";
                                            decimal bad_qty = -1;
                                            string bad_qty_str = " ";
                                            bool bad_qty_convert = decimal.TryParse(item["bad_qty"].ToString(), out bad_qty);
                                            if (bad_qty_convert && bad_qty != 0)
                                                bad_qty_str = bad_qty.ToString();
                                            switch (problem_level)
                                            {
                                                case "0":
                                                    dgv_bad_item2.Rows[addItemRow].Cells["MAJOR_DEFECT2"].Value = bad_qty_str;
                                                    if (bad_qty_convert)
                                                        bad_qty_total_2 += bad_qty;
                                                    break;
                                                case "1":
                                                    dgv_bad_item2.Rows[addItemRow].Cells["MINOR_DEFECT2"].Value = bad_qty_str;
                                                    if (bad_qty_convert)
                                                        bad_qty_total_1 += bad_qty;
                                                    break;
                                                case "2":
                                                    dgv_bad_item2.Rows[addItemRow].Cells["CRITICAL_DEFECT2"].Value = bad_qty_str;
                                                    if (bad_qty_convert)
                                                        bad_qty_total_3 += bad_qty;
                                                    break;
                                                default:
                                                    break;
                                            }

                                            totoalItemCountCal++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    #endregion

                    dt_top = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dt_top"].ToString());

                    #region 点箱内容
                    dataGridViewa1.MaximumSize = new Size(this.dataGridViewa1.Width, 0);
                    dataGridViewa1.AutoSize = true;
                    dataGridViewb1.MaximumSize = new Size(this.dataGridViewb1.Width, 0);
                    dataGridViewb1.AutoSize = true;
                    dataGridViewc1.MaximumSize = new Size(this.dataGridViewc1.Width, 0);
                    dataGridViewc1.AutoSize = true;
                    dataGridViewd1.MaximumSize = new Size(this.dataGridViewd1.Width, 0);
                    dataGridViewd1.AutoSize = true;
                    DataTable dts1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dts"].ToString());
                    if (dts1.Rows.Count > 0)
                    {
                        //计算检验数 => 双数
                        //请求api的数据展示
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        //键值对传值
                        p.Add("task_no", dics["task_no"]);
                        string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_AQLAPI",//类库名
                                                    "SJ_AQLAPI.AQL_PointBox",//类名
                                                    "GetPointBox_title",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);

                        if (!ret1.IsSuccess)
                        {
                            throw new Exception(ret1.ErrMsg);
                        }
                        Dictionary<string, object> dic1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1.RetData);
                        var dtt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic1["Data"].ToString());

                        if (dtt.Rows.Count > 0)
                        {
                            sumnum = dtt.Rows[0]["lot_num"].ToString();//实际双数
                        }

                        dataGridViewa1.Rows.Clear();
                        if (dts1.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dts1.Rows)
                            {
                                
                                dataGridViewa1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewa1.Rows[i];
                                dgvr.Cells["Columna1"].Value = dr["case_no"].ToString();//箱号
                                //dgvr.Cells["Columna2"].Value = dr["se_qty"].ToString();//箱数
                                dgvr.Cells["Columna2"].Value = "";//箱数
                                dgvr.Cells["Columna3"].Value = dr["cr_size"].ToString();//size码数
                                //dgvr.Cells["Columna4"].Value = dr["qty"].ToString();//检验数
                                dgvr.Cells["Columna4"].Value = "";//检验数
                                dgvr.Cells["订单量1"].Value = dr["qty"].ToString();//分批订单数量
                                //dgvr.Cells["订单量1"].Value = dr["se_qty"].ToString();//分批订单数量
                                i++;
                            }
                        }
                        
                        this.dataGridViewa1.ClearSelection();
                    }
                    var dts2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dts2"].ToString());
                    if (dts2.Rows.Count > 0)
                    {
                        dataGridViewb1.Rows.Clear();
                        if (dts2.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dts2.Rows)
                            {
                                dataGridViewb1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewb1.Rows[i];
                                dgvr.Cells["Columnb1"].Value = dr["case_no"].ToString();//箱号
                                //dgvr.Cells["Columnb2"].Value = dr["se_qty"].ToString();//箱数
                                dgvr.Cells["Columnb2"].Value = "";//箱数
                                dgvr.Cells["Columnb3"].Value = dr["cr_size"].ToString();//size码数
                                dgvr.Cells["Columnb4"].Value = "";//检验数
                                dgvr.Cells["订单量2"].Value = dr["qty"].ToString();//分批订单数量
                                //dgvr.Cells["订单量2"].Value = dr["se_qty"].ToString();//分批订单数量
                                i++;
                            }
                        }
                        this.dataGridViewb1.ClearSelection();
                    }
                    var dts3 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dts3"].ToString());
                    if (dts3.Rows.Count > 0)
                    {
                        dataGridViewc1.Rows.Clear();
                        if (dts3.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dts3.Rows)
                            {
                                dataGridViewc1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewc1.Rows[i];
                                dgvr.Cells["Columnc1"].Value = dr["case_no"].ToString();//箱号
                                //dgvr.Cells["Columnc2"].Value = dr["se_qty"].ToString();//箱数
                                dgvr.Cells["Columnc2"].Value = "";//箱数
                                dgvr.Cells["Columnc3"].Value = dr["cr_size"].ToString();//size码数
                                dgvr.Cells["Columnc4"].Value = dr["qty"].ToString();//检验数
                                dgvr.Cells["订单量3"].Value = dr["qty"].ToString();//分批订单数量
                                //dgvr.Cells["订单量3"].Value = dr["se_qty"].ToString();//分批订单数量
                                i++;
                            }

                        }
                        this.dataGridViewc1.ClearSelection();
                    }
                    var dts4 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dts4"].ToString());
                    if (dts4.Rows.Count > 0)
                    {
                        dataGridViewd1.Rows.Clear();
                        if (dts4.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dts4.Rows)
                            {
                                dataGridViewd1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewd1.Rows[i];
                                dgvr.Cells["Columnd1"].Value = dr["case_no"].ToString();//箱号
                                //dgvr.Cells["Columnd2"].Value = dr["se_qty"].ToString();//箱数
                                dgvr.Cells["Columnd2"].Value = "";//箱数
                                dgvr.Cells["Columnd3"].Value = dr["cr_size"].ToString();//size码数
                                dgvr.Cells["Columnd4"].Value = dr["qty"].ToString();//检验数
                                dgvr.Cells["订单量4"].Value = dr["qty"].ToString();//分批订单数量
                                //dgvr.Cells["订单量4"].Value = dr["se_qty"].ToString();//分批订单数量
                                i++;
                            }
                        }
                        this.dataGridViewd1.ClearSelection();
                    }

                    CalculateEvenNumbers();//计算双数
                    #endregion

                    //不良统计
                    gdv_bad_total.Rows.Add();
                    gdv_bad_total.Rows[0].Cells["bad_str"].Value = gdv_bad_total.Columns["bad_str"].HeaderText;
                    gdv_bad_total.Rows[0].Cells["bad_total_1"].Value = bad_qty_total_1.ToString();
                    gdv_bad_total.Rows[0].Cells["bad_total_2"].Value = bad_qty_total_2.ToString();
                    gdv_bad_total.Rows[0].Cells["bad_total_3"].Value = bad_qty_total_3.ToString();

                    gdv_bad_total.Columns["bad_str"].Width = dgv_bad_item1.Width + dgv_bad_item2.Width - (dgv_bad_item2.Columns["MINOR_DEFECT2"].Width + dgv_bad_item2.Columns["MAJOR_DEFECT2"].Width + dgv_bad_item2.Columns["CRITICAL_DEFECT2"].Width);
                    gdv_bad_total.Columns["bad_total_1"].Width = dgv_bad_item2.Columns["MINOR_DEFECT2"].Width;
                    gdv_bad_total.Columns["bad_total_2"].Width = dgv_bad_item2.Columns["MAJOR_DEFECT2"].Width;
                    gdv_bad_total.Columns["bad_total_3"].Width = dgv_bad_item2.Columns["CRITICAL_DEFECT2"].Width;

                    int height = 0;
                    if(dgv_bad_item1.Height> dgv_bad_item2.Height)
                    {
                        height = dgv_bad_item1.Height;
                    }
                    else
                    {
                        height = dgv_bad_item2.Height;
                    }
                    gdv_bad_total.Location = new Point(dgv_bad_item1.Location.X, dgv_bad_item1.Location.Y + height);
                    pl_bottom_info.Location = new Point(dgv_bad_item1.Location.X, dgv_bad_item1.Location.Y + height + gdv_bad_total.Height);
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public void GetREJECT_AccEpted()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("task_no", dics["task_no"]);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.F_AQL_Entry",//类名
                                            "GetAQLEntry_Sorting",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

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

                int zy = 0;//主要
                int cy = 0;//次要
                int yz = 0;//严重
                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["problem_level"])
                    {
                        case "0":
                            zy += Convert.ToInt32(dr["bad_qty"].ToString());
                            break;
                        case "1":
                            cy += Convert.ToInt32(dr["bad_qty"].ToString());
                            break;
                        case "2":
                            yz += Convert.ToInt32(dr["bad_qty"].ToString());
                            break;
                        default:
                            break;
                    }
                }

                int hjbl = cy + zy + yz;//合计不良

                if(dt_top.Rows[0]["INSPECTION_STATE"].ToString() == "1")
                {
                    if (hjbl > Convert.ToInt32(ac))
                    {
                        checkBox2.Checked = true;
                        checkBox1.Checked = false;
                    }
                    else
                    {
                        checkBox1.Checked = true;
                        checkBox2.Checked = false;
                    }
                }
                else
                {
                    lbl_cherk_name.Text = "";
                    label9.Text = "";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                }
                
                   
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            try
            {

                //创建文件弹出选择窗口（包括文件名）对象
                FolderBrowserDialog dilog = new FolderBrowserDialog();

                //dilog.Description = "请选择文件夹";
                dilog.Description = "Please select a folder";

                if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
                {

                    string selctDicPath = dilog.SelectedPath;
                    string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string exportPath = selctDicPath;

                    string mubanPath = Application.StartupPath + "\\Printer\\AQL\\AQL_CmaTaskReport.xlsx";

                    Dictionary<string, string> replaceDic = new Dictionary<string, string>();
                    //头部
                    replaceDic.Add("ArtParm", dataGridView1.Rows[1].Cells["Column2"].Value.ToString());//art_no
                    replaceDic.Add("ModelNameParm", dataGridView1.Rows[2].Cells["Column2"].Value.ToString());//鞋型
                    replaceDic.Add("poParm", dataGridView1.Rows[0].Cells["Column5"].Value.ToString());//po
                    replaceDic.Add("orderQtyParm", dataGridView1.Rows[1].Cells["Column5"].Value.ToString());//订单总数
                    replaceDic.Add("actualQtyParm", dataGridView1.Rows[1].Cells["Column5"].Value.ToString());//实际数量
                    replaceDic.Add("customParm", dataGridView1.Rows[2].Cells["Column5"].Value.ToString());//客户
                    replaceDic.Add("InspectionDateParm", dataGridView1.Rows[0].Cells["Column7"].Value.ToString());//检验日期
                    replaceDic.Add("FinalRandomInspectionParm", dataGridView1.Rows[1].Cells["Column7"].Value.ToString());//最终检验
                    replaceDic.Add("ReInspectionParm", dataGridView1.Rows[2].Cells["Column7"].Value.ToString());//翻箱检验

                    //中部
                    replaceDic.Add("SampleLotParm", dataGridViewcenter1.Rows[0].Cells[1].Value.ToString());//抽样比例
                    replaceDic.Add("LevelParm", dataGridViewcenter1.Rows[1].Cells[1].Value.ToString());//样品级别
                    replaceDic.Add("cartonsParm", dataGridViewcenter2.Rows[0].Cells[1].Value.ToString());//箱数
                    replaceDic.Add("pairsParm", dataGridViewcenter2.Rows[1].Cells[1].Value.ToString());//双数
                    replaceDic.Add("aql251", dataGridViewcenter3.Rows[0].Cells[1].Value.ToString());//最大可接受不良数2.5
                    replaceDic.Add("aql151", dataGridViewcenter3.Rows[0].Cells[2].Value.ToString());//最大可接受不良数1.5
                    replaceDic.Add("aql01", dataGridViewcenter3.Rows[0].Cells[3].Value.ToString());//最大可接受不良数0
                    replaceDic.Add("aql252", dataGridViewcenter3.Rows[1].Cells[1].Value.ToString());//拒收不良数2.5
                    replaceDic.Add("aql152", dataGridViewcenter3.Rows[1].Cells[2].Value.ToString());//拒收不良数1.5
                    replaceDic.Add("aql02", dataGridViewcenter3.Rows[1].Cells[3].Value.ToString());//拒收不良数0

                    replaceDic.Add("total1", gdv_bad_total.Rows[0].Cells["bad_total_1"].Value.ToString());
                    replaceDic.Add("total2", gdv_bad_total.Rows[0].Cells["bad_total_2"].Value.ToString());
                    replaceDic.Add("total3", gdv_bad_total.Rows[0].Cells["bad_total_3"].Value.ToString());

                    //底部
                    replaceDic.Add("AcceptedParm", checkBox1.Checked ? "√" : "");//Accepted接受
                    replaceDic.Add("RejectedParm", checkBox2.Checked ? "√" : "");//Rejected拒收
                    replaceDic.Add("NameFactoryInspectorParm", lbl_cherk_name.Text);//工厂检查员姓名
                    replaceDic.Add("SignatureParm", label9.Text);//签名/日期

                    //点箱信息
                    int pb_index = 1;
                    foreach (DataGridViewRow item in dataGridViewa1.Rows)
                    {
                        replaceDic.Add($@"carton{pb_index}", item.Cells["Columna1"].Value.ToString());
                        replaceDic.Add($@"cartonQty{pb_index}", item.Cells["Columna2"].Value.ToString());
                        replaceDic.Add($@"size{pb_index}", item.Cells["Columna3"].Value.ToString());
                        replaceDic.Add($@"insp{pb_index}", item.Cells["Columna4"].Value.ToString());
                        pb_index++;
                    }
                    foreach (DataGridViewRow item in dataGridViewb1.Rows)
                    {
                        replaceDic.Add($@"carton{pb_index}", item.Cells["Columnb1"].Value.ToString());
                        replaceDic.Add($@"cartonQty{pb_index}", item.Cells["Columnb2"].Value.ToString());
                        replaceDic.Add($@"size{pb_index}", item.Cells["Columnb3"].Value.ToString());
                        replaceDic.Add($@"insp{pb_index}", item.Cells["Columnb4"].Value.ToString());
                        pb_index++;
                    }
                    foreach (DataGridViewRow item in dataGridViewc1.Rows)
                    {
                        replaceDic.Add($@"carton{pb_index}", item.Cells["Columnc1"].Value.ToString());
                        replaceDic.Add($@"cartonQty{pb_index}", item.Cells["Columnc2"].Value.ToString());
                        replaceDic.Add($@"size{pb_index}", item.Cells["Columnc3"].Value.ToString());
                        replaceDic.Add($@"insp{pb_index}", item.Cells["Columnc4"].Value.ToString());
                        pb_index++;
                    }
                    foreach (DataGridViewRow item in dataGridViewd1.Rows)
                    {
                        replaceDic.Add($@"carton{pb_index}", item.Cells["Columnd1"].Value.ToString());
                        replaceDic.Add($@"cartonQty{pb_index}", item.Cells["Columnd2"].Value.ToString());
                        replaceDic.Add($@"size{pb_index}", item.Cells["Columnd3"].Value.ToString());
                        replaceDic.Add($@"insp{pb_index}", item.Cells["Columnd4"].Value.ToString());
                        pb_index++;
                    }

                    AqlreportExportHelper.ExportAqlReport(mubanPath, exportPath, $@"{dics["task_no"]}_{time}", replaceDic, dgv_bad_item1, dgv_bad_item2);
                    MessageBox.Show("Export succeeded");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
