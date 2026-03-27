using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_AQL.Common;
using SJeMES_Control_Library;
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
using PrintHelper = SJeMES_AQL.Common.PrintHelper;

namespace SJeMES_AQL
{
    public partial class F_AQL_TheinspectionplanEdit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private List<string> list=new List<string>();
        private string id = string.Empty;
        private string plan_date = string.Empty;
        private string level_type = string.Empty;
        private DataTable aql_dt = new DataTable();
        private int i = 0;
        private bool flag = false;
        public F_AQL_TheinspectionplanEdit(string _id,string _plan_date,string _level_type)
        {
            InitializeComponent();
            id = _id;//表头Id
            plan_date = _plan_date;//计划日期
            level_type = _level_type;//AQL级别
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public F_AQL_TheinspectionplanEdit()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void F_AQL_TheinspectionplanEdit_Load(object sender, EventArgs e)
        {
            cbl_view();//加载下拉框
            pageControl1.BindPageEvent += GetDataList;
            LoadPage();
            if (!string.IsNullOrWhiteSpace(plan_date))
            {
                dateTimePicker1.Text = plan_date;
            }
          

        }
        public void LoadPage()
        {
            pageControl1.PageSize = 10;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        /// <summary>
        /// 加载下拉框
        /// </summary>
        private void cbl_view()
        {

            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.VMaterialinventory",//类名
                                            "GetAQLEnum",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["AQL_Level"].ToString());
                if (dt.Rows.Count > 0)
                {
                    comboBox1.DataSource = dt;//加载检验水平下拉框
                    comboBox1.ValueMember = "ENUM_CODE";
                    comboBox1.DisplayMember = "ENUM_VALUE";
                    comboBox1.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return;
                }
                comboBox1.SelectedValue = level_type;//表头绑定上自己的aql级别
                aql_getlist();
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("id", id);//表头id
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Theinspectionplan",//类名
                                            "Get_MainMin",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["d_id"].Value = dr["id"].ToString();//id
                        dgvr.Cells["group"].Value = dr["groupo"].ToString();//组别
                        dgvr.Cells["prod_name"].Value = dr["name_t"].ToString();//鞋型
                        dgvr.Cells["se_id"].Value = dr["se_id"].ToString();//制令号
                        dgvr.Cells["po"].Value = dr["po"].ToString();//PO
                        dgvr.Cells["art"].Value = dr["prod_no"].ToString();//ART
                        dgvr.Cells["state"].Value = dr["国家"].ToString();//国家
                        //dgvr.Cells["qty"].Value = dr["数量"].ToString();//数量
                        dgvr.Cells["qty"].Value =dr["qty"].ToString();//数量
                        dgvr.Cells["aql_qty"].Value = dr["AQL数量"].ToString();//aql数量
                        dgvr.Cells["sfbz"].Value = dr["是否外观标准"].ToString();//是否外观标准
                        dgvr.Cells["is_disclaimer"].Value = dr["is_disclaimer"].ToString();//是否负责说明
                        dgvr.Cells["storage_area"].Value = dr["storage_area"].ToString();//存放区域
                        
                        list.Add(dr["po"].ToString());
                        i++;
                    }
                    // GenClass.AutoSizeColumn(dataGridView1);

                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
       
        private void textBox1_Click(object sender, EventArgs e)
        {
            i = dataGridView1.RowCount;
            //F_AQL_OutData frm = new F_AQL_OutData();
            F_AQL_OutData_New frm = new F_AQL_OutData_New("");
            frm.ShowDialog();
            List<Dictionary<string, object>> dic = frm.selectdata;

            if (dic != null)
            {
                foreach (Dictionary<string, object> item in dic)
                {
                    list.Add(item["PO"].ToString());
                    if (list.Distinct().Count() != list.Count())
                    {
                        list = list.Distinct().ToList();
                        continue;
                    }
                    dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    /*

                     PO", da
                     ART", d
                     鞋型", 
                     指令号"
                     国家", 
                     数量", 
                     存放位置
                     组别", 
                     */
                    dgvr.Cells["po"].Value = item["PO"].ToString();//PO
                    dgvr.Cells["art"].Value = item["ART"].ToString();//art
                    dgvr.Cells["prod_name"].Value = item["鞋型"].ToString();//鞋型
                    dgvr.Cells["se_id"].Value = item["制令号"].ToString();//制令
                    dgvr.Cells["d_id"].Value = string.Empty;
                    dgvr.Cells["state"].Value = item["国家"];
                    dgvr.Cells["qty"].Value = item["数量"];

                    dgvr.Cells["storage_area"].Value = item["存放位置"];
                    dgvr.Cells["group"].Value = item["组别"];
                    foreach (DataRow keyqty in aql_dt.Rows)
                    {
                        flag = compare(Convert.ToDecimal(keyqty["START_QTY"]), 100, Convert.ToDecimal(keyqty["END_QTY"]));
                        if (flag)
                        {
                            dgvr.Cells["aql_qty"].Value = keyqty["VALS"];
                            break;
                        }
                    }
                    i++;
                }
            }
          
            dataGridView1.ClearSelection();

        }
        /// <summary>
        /// 计算区间样品量
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="number3"></param>
        /// <returns></returns>
        public  bool compare(decimal number1,decimal number2,decimal number3)
        {
            if (number1 < number2 && number2< number3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("delete"))//删除
                        {
                            bool flag = false;
                            string MessText = string.Empty;
                            string id = dataGridView1.CurrentRow.Cells["d_id"].Value.ToString();
                            string po = dataGridView1.CurrentRow.Cells["po"].Value.ToString();
                            if (string.IsNullOrWhiteSpace(id))
                            {
                                dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                                list = list.Where(a => a != po).ToList();
                            }
                            else
                            {
                                if (MessageBox.Show("confirm deletion？", "This deletion cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    try
                                    {

                                        Dictionary<string, object> p = new Dictionary<string, object>();
                                        p.Add("id", id);
                                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                             "SJ_AQLAPI", "SJ_AQLAPI.AQL_Theinspectionplan", "Main_Delete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                        if (ret.IsSuccess)
                                        {
                                            flag = true;
                                        }
                                        else
                                        {
                                            MessText = ret.ErrMsg;
                                        }
                                        if (flag)
                                        {
                                            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                                            dataGridView1.Rows.Remove(row);
                                            list = list.Where(a => a != po).ToList();
                                            MessageHelper.ShowSuccess(this, "successfully deleted");
                                        }
                                        else
                                        {
                                            MessageHelper.ShowErr(this, "failed to delete:" + MessText);
                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                    }
                                }
                            }
                          

                        }
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDgvToTable(dataGridView1);
            dt.Columns.Remove("aql_qty");
            dt.Columns.Remove("sfbz");
            dt.Columns.Remove("is_disclaimer");
            dt.Columns["group"].ColumnName = "组别";
            dt.Columns["prod_name"].ColumnName = "鞋型";
            dt.Columns["se_id"].ColumnName = "销售订单号";
            dt.Columns["po"].ColumnName = "PO";
            dt.Columns["art"].ColumnName = "ART";
            dt.Columns["state"].ColumnName = "国家";
            dt.Columns["qty"].ColumnName = "数量(双)";
            //dt.Columns["aql_qty"].ColumnName = "AQL数量(双)";
            //dt.Columns["sfbz"].ColumnName = "是否有外观标准";
            //dt.Columns["is_disclaimer"].ColumnName = "是否有免责声明";
            dt.Columns["storage_area"].ColumnName = "存放位置";
            string[] keyhread = { "operation", "d_id" };
            for (int i = 0; i < keyhread.Length; i++)
            {
                if (dt.Columns.Contains(keyhread[i]))
                {
                    dt.Columns.Remove(keyhread[i]);
                }
            }
            #region 方案一
            //new PrintHelper().Print(dt, plan_date + "验货计划");
            Common.PrintHelper print = new Common.PrintHelper();
            print.PrintPriview(dt, plan_date + "验货计划");
            /* System.Drawing.Printing.PrintDocument printDoc = print.CreatePrintDocument(dt, plan_date + "验货计划");
             PrintPreviewDialog ppvw = new PrintPreviewDialog();
             ppvw.PrintPreviewControl.Zoom = 1.0; //显示比例为100%
             PrintDialog MyDlg = new PrintDialog();
             MyDlg.Document = printDoc;
             printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1000);
             printDoc.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(60, 60, 60, 60); //设置边距
             ((Form)ppvw).WindowState = FormWindowState.Maximized; //最大化       */

            #endregion


            #region 方案二
            /* ToPrint bll = new ToPrint();
             System.Drawing.Printing.PrintDocument printDoc = bll.CreatePrintDocument(dt, plan_date + "验货计划");
             //bll.Print(dt, plan_date + "验货计划");
             PrintPreviewDialog ppvw = new PrintPreviewDialog();
             ppvw.PrintPreviewControl.Zoom = 1.0; //显示比例为100%
             PrintDialog MyDlg = new PrintDialog();
             MyDlg.Document = printDoc;
             printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 850, 1000);
             printDoc.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(60, 60, 60, 60); //设置边距
             ppvw.Document = printDoc;   //设置要打印的文档 
             ((Form)ppvw).WindowState = FormWindowState.Maximized; //最大化  
                                                                   //printDoc.EndPrint += new System.Drawing.Printing.PrintEventHandler(printDoc_EndPrint);
                                                                   //ppvw.Document.DefaultPageSettings.Landscape = true;    // 设置打印为横向               
             ppvw.Document = printDoc;   //设置要打印的文档 
             ((Form)ppvw).WindowState = FormWindowState.Maximized; //最大化        
             ppvw.ShowDialog(); //打开预览 */
            #endregion
        }
      
        /// <summary>
        /// 下拉框内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            aql_getlist();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // aql_getlist();
        }
        public void aql_getlist()
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("level_type", comboBox1.SelectedValue);//表头id
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_Theinspectionplan",//类名
                                            "GetAQL_Level",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                aql_dt = dt;
                  
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetDgvToTable(dataGridView1);
                if (dt.Rows.Count > 0)
                {
                    string plan_date = string.Empty;

                    if (!string.IsNullOrWhiteSpace(this.dateTimePicker1.Text))
                    {
                        plan_date = Convert.ToDateTime(this.dateTimePicker1.Value).ToString("yyyy-MM-dd");
                    }
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("plan_date", plan_date);
                    p.Add("id", id);//表头id
                    p.Add("data", dt);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                             "SJ_AQLAPI", "SJ_AQLAPI.AQL_Theinspectionplan", "Commit_data", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
               
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public static DataTable GetDgvToTable(DataGridView dgv)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
