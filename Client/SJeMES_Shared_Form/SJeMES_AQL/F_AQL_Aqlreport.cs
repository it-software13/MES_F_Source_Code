using Newtonsoft.Json;
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

namespace SJeMES_Shared_Form
{
    public partial class F_AQL_Aqlreport : Form
    {
        private Dictionary<string, object> dics = new Dictionary<string, object>();
        public F_AQL_Aqlreport(Dictionary<string,object> _dic, SJeMES_Framework.Class.ClientClass _Client)
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

            //buttom1,2
            this.rowMergeView1.RowHeadersVisible = false;//隐藏第一列
            this.rowMergeView2.RowHeadersVisible = false;//隐藏第一列



            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;//隐藏表头hread
            this.dataGridView1.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
            this.dataGridView1.AllowUserToResizeColumns = false;
            GetListView();//加载中间长盒子内容
            topviewinput();//加载头部内

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
                            lefttest = "Sample lot抽检比例"; righttest = vals;
                        }

                        else
                        {
                            lefttest = "Sample size样本"; righttest = level;
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
                            lefttest = "Qty / Carton 每箱数量"; righttest = xnum;
                        }
                        else
                        {
                            lefttest = "Pairs双数"; righttest = snum;
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
                            lefttest = "Max.defect(s)accpet最大可接受不良数";
                            dgvr.Cells["Columncenterc1"].Value = lefttest;
                            dgvr.Cells["Columncenterc2"].Value = ac13;
                            dgvr.Cells["Columncenterc3"].Value = ac12;
                            dgvr.Cells["Columncenterc4"].Value = "0";
                        }
                        else {
                            lefttest = "No.of defct(s)to reject拒绝的不良数";
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
            string star = string.Empty;//最终结果
            string brand = string.Empty;
            string star2 = string.Empty;//再次检验
            if (dt_top.Rows.Count > 0)
            {
                art_no = dt_top.Rows[0]["art_no"].ToString();
                shoe_name = dt_top.Rows[0]["shoe_name"].ToString();
                po = dt_top.Rows[0]["po"].ToString();
                po_num = dt_top.Rows[0]["po_num"].ToString();
                yhrq = dt_top.Rows[0]["f_inspection_time"].ToString();
                Customer = "";
                factoy = "APACHE";
                brand = "adidas";
                star = "";
                star2 = "✔";
                label9.Text = dt_top.Rows[0]["createdate"].ToString();
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
            dr["Column1"] = "Factoy工厂";
            dr["Column2"] = factoy;
            dr["Column3"] = "Brand品牌";
            dr["Column4"] = "P.O.#订单";
            dr["Column5"] = po;
            dr["Column6"] = "Inspection date验货日期";
            dr["Column7"] = yhrq;
          
           

            
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Column1"] = "Article配色";
            dr["Column2"] = art_no;
            dr["Column3"] = brand;
            dr["Column4"] = "Total order qty订单总数";
            dr["Column5"] = po_num;
            dr["Column6"] = "Final Random inspection最终抽检";
            dr["Column7"] = star;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Column1"] = "ModelName鞋型";
            dr["Column2"] = shoe_name;
            dr["Column3"] = "*";
            dr["Column4"] = "Customer客户";
            dr["Column5"] = Customer;
            dr["Column6"] = "Re-inspection再次检验";
            dr["Column7"] = star2;
            dt.Rows.Add(dr);
            return dt;
        }
       
        #endregion

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
                    vals =dic["vals"].ToString();
                    level=dic["level"].ToString();
                    xnum =dic["xnum"].ToString();
                    snum =dic["snum"].ToString();
                    ac = dic["ac"].ToString();
                    ac12 = dic["ac12"].ToString();
                    ac13 = dic["ac13"].ToString();

                    rowMergeView1.Rows.Clear();
                    rowMergeView2.Rows.Clear();
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        int j = 0;
                        int hegt = 0;
                        int minheight = dt.Rows.Count / 2;
                        foreach (DataRow dr in dt.Rows)
                        {

                            if (i > minheight)
                            {
                                rowMergeView2.Rows.Add();
                                DataGridViewRow dgvr = rowMergeView2.Rows[j];
                                dgvr.Cells["Columnrowb1"].Value = dr["bad_classify_name"].ToString();
                                dgvr.Cells["Columnrowb2"].Value = dr["bad_item_name"].ToString();
                                dgvr.Cells["Columnrowb3"].Value = dr["bad_qty"].ToString();
                                j++;

                            }
                            else
                            {
                                rowMergeView1.Rows.Add();
                                DataGridViewRow dgvr = rowMergeView1.Rows[i];
                                dgvr.Cells["Columnrowa1"].Value = dr["bad_classify_name"].ToString();
                                dgvr.Cells["Columnrowa2"].Value = dr["bad_item_name"].ToString();
                                dgvr.Cells["Columnrowa3"].Value = dr["bad_qty"].ToString();
                            }
                            i++;

                        }
                        hegt = (this.rowMergeView1.GetCellDisplayRectangle(this.rowMergeView1.CurrentCell.ColumnIndex, this.rowMergeView1.CurrentCell.RowIndex, true).Height) * ((dt.Rows.Count + 2) / 2);
                        this.rowMergeView1.Height = hegt + 20;
                        this.rowMergeView2.Height = hegt + 20;

                        this.rowMergeView1.MergeColumnNames.Add($"Columnrowa1");
                        this.rowMergeView2.MergeColumnNames.Add($"Columnrowb1");
                    }
                    rowMergeView2.ClearSelection();
                    rowMergeView1.ClearSelection();
                    #endregion

                    dt_top = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dt_top"].ToString());

                    #region 点箱内容
                    DataTable dtlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dts"].ToString());
                    if (dtlist.Rows.Count > 0)
                    {
                        dataGridViewa1.Rows.Clear();
                        if (dtlist.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dtlist.Rows)
                            {
                                dataGridViewa1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewa1.Rows[i];
                                dgvr.Cells["Columna1"].Value = dr["case_no"].ToString();//箱号
                                dgvr.Cells["Columna2"].Value = dr["se_qty"].ToString();//箱数
                                dgvr.Cells["Columna3"].Value = dr["cr_size"].ToString();//size码数
                                dgvr.Cells["Columna4"].Value = dr["qty"].ToString();//检验数
                                i++;
                            }


                        }
                        this.dataGridViewa1.ClearSelection();
                    }
                    dtlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dts2"].ToString());
                    if (dtlist.Rows.Count > 0)
                    {
                        dataGridViewb1.Rows.Clear();
                        if (dtlist.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dtlist.Rows)
                            {
                                dataGridViewb1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewb1.Rows[i];
                                dgvr.Cells["Columnb1"].Value = dr["case_no"].ToString();//箱号
                                dgvr.Cells["Columnb2"].Value = dr["se_qty"].ToString();//箱数
                                dgvr.Cells["Columnb3"].Value = dr["cr_size"].ToString();//size码数
                                dgvr.Cells["Columnb4"].Value = dr["qty"].ToString();//检验数
                                i++;
                            }


                        }
                        this.dataGridViewb1.ClearSelection();
                    }
                    dtlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dts3"].ToString());
                    if (dtlist.Rows.Count > 0)
                    {
                        dataGridViewc1.Rows.Clear();
                        if (dtlist.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dtlist.Rows)
                            {
                                dataGridViewc1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewc1.Rows[i];
                                dgvr.Cells["Columnc1"].Value = dr["case_no"].ToString();//箱号
                                dgvr.Cells["Columnc2"].Value = dr["se_qty"].ToString();//箱数
                                dgvr.Cells["Columnc3"].Value = dr["cr_size"].ToString();//size码数
                                dgvr.Cells["Columnc4"].Value = dr["qty"].ToString();//检验数
                                i++;
                            }


                        }
                        this.dataGridViewc1.ClearSelection();
                    }
                    dtlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dts4"].ToString());
                    if (dtlist.Rows.Count > 0)
                    {
                        dataGridViewd1.Rows.Clear();
                        if (dtlist.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in dtlist.Rows)
                            {
                                dataGridViewd1.Rows.Add();
                                DataGridViewRow dgvr = dataGridViewd1.Rows[i];
                                dgvr.Cells["Columnd1"].Value = dr["case_no"].ToString();//箱号
                                dgvr.Cells["Columnd2"].Value = dr["se_qty"].ToString();//箱数
                                dgvr.Cells["Columnd3"].Value = dr["cr_size"].ToString();//size码数
                                dgvr.Cells["Columna4"].Value = dr["qty"].ToString();//检验数
                                i++;
                            }


                        }
                        this.dataGridViewd1.ClearSelection();
                    }

                    #endregion
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

    }
}
