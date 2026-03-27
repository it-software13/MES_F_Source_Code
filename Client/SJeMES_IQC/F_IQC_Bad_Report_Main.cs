using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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

namespace SJeMES_IQC
{
    public partial class F_IQC_Bad_Report_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private string CHK_NOS;
        private string ORG_ID = string.Empty;
        private string WAREHOUSE_CODE = string.Empty;
        public F_IQC_Bad_Report_Main(string CHK_NO)
        {
            InitializeComponent();
            CHK_NOS = CHK_NO;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_IQC_Bad_Report_Main()
        {
            InitializeComponent();
            InitDateTimePicker(dateTimeP_putin_date);
            InitDateTimePicker(dateTimeP_end_date);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
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
        /// 初始化日期时间控件
        /// </summary>
        /// <param name="dtp"></param>
        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = 25;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void btn_select_Click(object sender, EventArgs e)
        {
            LoadPage();
            
            //cbo_wgjg.SelectedIndex = -1;
            //cbo_status.SelectedIndex = -1;
            //cbo_csjg.SelectedIndex = -1;
            //cbo_qyzk.SelectedIndex = -1;
        }

        private void btn_outexcel_Click(object sender, EventArgs e)
        {
            try
            {
                int pageSize = int.MaxValue;
                int pageIndex = 1;
                var ret = CallMainApi(pageSize, pageIndex);
                if (ret == null)
                    return;
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    var dts = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                    if (dts.Rows.Count > 0)
                    {

                        Dictionary<string, string> Execldic = new Dictionary<string, string>();
                        //Execldic.Add("RCPT_DATE", "收货日期");
                        //Execldic.Add("REPORT_TYPE", "报告类型");
                        //Execldic.Add("SUPPLIERS_CODE", "生产厂商编号");
                        //Execldic.Add("SUPPLIERS_NAME", "生产厂商");
                        //Execldic.Add("CHK_NO", "收料单号");
                        //Execldic.Add("ITEM_NO", "料号");
                        ////Execldic.Add("ITEM_TYPE_NO", "物料类型");
                        //Execldic.Add("SUPPLIERS_CODE2", "采购厂商编号");
                        //Execldic.Add("SUPPLIERS_NAME2", "采购厂商");
                        //Execldic.Add("CREATEDATE", "外观检验日期");
                        //Execldic.Add("PR_UNIT", "收货单位");
                        //Execldic.Add("ORD_QTY", "采购数量");
                        //Execldic.Add("ORDER_NO", "采购单号");
                        //Execldic.Add("DETERMINE", "外观检验结果");
                        //Execldic.Add("SAMPLING_STATUS", "测试取样状况");
                        //Execldic.Add("CSJG", "测试结果");
                        //Execldic.Add("STAFF_NAME", "检验员");
                        //Execldic.Add("IV_QTY", "检验数");
                        //Execldic.Add("PASS_QTY", "合格数");
                        //Execldic.Add("YTS", "验退数");
                        //Execldic.Add("BS", "补送");
                        //Execldic.Add("WAREHOUSE_NAME", "仓库");
                        //Execldic.Add("CLOSING_STATUS", "结案状况");

                        Execldic.Add("RCPT_DATE", "Delivery_Date");
                        Execldic.Add("REPORT_TYPE", "Report_Type");
                        Execldic.Add("SUPPLIERS_CODE", "Manufacturer_Number");
                        Execldic.Add("SUPPLIERS_NAME", "Manufacturer");
                        Execldic.Add("CHK_NO", "Receipt_No.");
                        Execldic.Add("ITEM_NO", "ITEM_NO");
                        //Execldic.Add("ITEM_TYPE_NO", "物料类型");
                        Execldic.Add("SUPPLIERS_CODE2", "Purchasing_Manufacturer_Number");
                        Execldic.Add("SUPPLIERS_NAME2", "Purchaser");
                        Execldic.Add("CREATEDATE", "Appearance_Inspection_Date");
                        Execldic.Add("PR_UNIT", "Receiving_Unit");
                        Execldic.Add("ORD_QTY", "Purchase_Quantity");
                        Execldic.Add("ORDER_NO", "Purchase_Order_No");
                        Execldic.Add("DETERMINE", "Appearance_Inspection_Results");
                        Execldic.Add("SAMPLING_STATUS", "Test_Sampling_Status");
                        Execldic.Add("CSJG", "Test_Results");
                        Execldic.Add("STAFF_NAME", "Inspectors");
                        Execldic.Add("IV_QTY", "Inspection_Number");
                        Execldic.Add("PASS_QTY", "PASS_QTY");
                        Execldic.Add("YTS", "Number_Of_Check-outs");
                        Execldic.Add("BS", "Refill");
                        Execldic.Add("WAREHOUSE_NAME", "Storehouse");
                        Execldic.Add("CLOSING_STATUS", "Closing_Status");

                        dts.Columns.Remove("ITEM_TYPE_NO");
                        foreach (DataRow item in dts.Rows)
                        {
                            if (item["DETERMINE"].ToString() == "0")
                            {
                               // item["DETERMINE"] = "合格";//测试结果
                                item["DETERMINE"] = "qualified";//测试结果
                            }
                            if (item["DETERMINE"].ToString() == "1")
                            {
                               // item["DETERMINE"] = "不合格";//测试结果
                                item["DETERMINE"] = "unqualified";//测试结果
                            }
                            if (item["CLOSING_STATUS"].ToString() == "0")
                            {
                                //item["CLOSING_STATUS"] = "结案";
                                item["CLOSING_STATUS"] = "close the case";
                            }
                            else
                            {
                                item["CLOSING_STATUS"] = "opencase";//未结案
                            }
                        }

                        List<string> removeCol = new List<string>();
                        foreach (DataColumn item in dts.Columns)
                        {
                            if (!Execldic.Keys.Contains(item.ColumnName))
                                removeCol.Add(item.ColumnName);
                        }

                        foreach (var item in removeCol)
                        {
                            dts.Columns.Remove(item);
                        }

                        ExeclHelper.ExportToTrueExcel(dts, Execldic, "Bad report list");

                    }
                }
                else
                {
                    throw new Exception(ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }


        /// <summary>
        /// IQC不良报告主页面查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetBad_Report_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                
                CHK_NOS = txt_sldh.Text.Trim();

                var ret = CallMainApi(pageSize, pageIndex);
                if (ret == null)
                    return;
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["RCPT_DATE"].Value = dr["RCPT_DATE"].ToString();
                        dgvr.Cells["REPORT_TYPE"].Value = dr["REPORT_TYPE"].ToString();
                        dgvr.Cells["SUPPLIERS_CODE"].Value = dr["SUPPLIERS_CODE"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                        dgvr.Cells["CHK_NO"].Value = dr["CHK_NO"].ToString();
                        dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();
                        dgvr.Cells["ITEM_TYPE_NO"].Value = dr["ITEM_TYPE_NO"].ToString();//物料类型
                        dgvr.Cells["CHK_SEQ"].Value = dr["CHK_SEQ"].ToString();//料号序号
                        dgvr.Cells["SUPPLIERS_CODE2"].Value = dr["SUPPLIERS_CODE2"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME2"].Value = dr["SUPPLIERS_NAME2"].ToString();
                        dgvr.Cells["CREATEDATE"].Value = dr["CREATEDATE"].ToString();
                        dgvr.Cells["PR_UNIT"].Value = dr["PR_UNIT"].ToString();//收货单位
                        dgvr.Cells["STAFF_NAME"].Value = dr["STAFF_NAME"].ToString();//检验员名称
                        dgvr.Cells["CREATEBY"].Value = dr["STAFF_NO"].ToString();//检验员编号
                        dgvr.Cells["ORDER_NO"].Value = dr["ORDER_NO"].ToString();
                        dgvr.Cells["STOC_NO1"].Value = dr["STOC_NO"].ToString();//仓库代号
                        dgvr.Cells["WAREHOUSE_NAME"].Value = dr["WAREHOUSE_NAME"].ToString();//仓库名称


                        dgvr.Cells["CSQYZK"].Value = dr["SAMPLING_STATUS"].ToString();//取样状况
                        if (dr["DETERMINE"].ToString() == "0")
                        {
                            dgvr.Cells["DETERMINE"].Value = "qualified";//测试结果//合格 i chenged this
                        }
                        else if (dr["DETERMINE"].ToString() == "1")
                        { 
                            dgvr.Cells["DETERMINE"].Value = "unqualified";//测试结果//不合格 i chenged this
                        }


                        dgvr.Cells["IV_QTY"].Value = dr["IV_QTY"].ToString();
                        dgvr.Cells["cjsl"].Value = dr["SAMPLE_QTY"].ToString();
                        dgvr.Cells["PASS_QTY"].Value = dr["PASS_QTY"].ToString();
                        dgvr.Cells["ORD_QTY"].Value = dr["ORD_QTY"].ToString();
                        dgvr.Cells["RETURN_QTY"].Value = dr["RETURN_QTY"].ToString();//验退数
                        dgvr.Cells["BS1"].Value = dr["BS"].ToString();//?

                      
                        dgvr.Cells["CLOSING_STATUS"].Value = dr["CLOSING_STATUS"].ToString();

                        if (dr["CLOSING_STATUS"].ToString() == "0")
                        {
                            dgvr.Cells["CLOSING_STATUS"].Style.ForeColor = Color.Red;
                            dgvr.Cells["CLOSING_STATUS"].Value = "Closed";//已结案
                        }
                        if (dr["CLOSING_STATUS"].ToString() == "1")
                        {
                            dgvr.Cells["CLOSING_STATUS"].Style.ForeColor = Color.Green;
                            dgvr.Cells["CLOSING_STATUS"].Value = "opencase";//未结案
                        }
                        if (string.IsNullOrWhiteSpace(dr["CLOSING_STATUS"].ToString()))
                        {
                            dgvr.Cells["CLOSING_STATUS"].Value = "无";//none
                        }
                        i++;

                    }
                    GenClass.AutoSizeColumn(dataGridView1, 1);
                }

                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public ResultObject CallMainApi(int pageSize, int pageIndex)
        {
            string putin_date = string.Empty;
            string end_date = string.Empty;
            if (string.IsNullOrWhiteSpace(txt_ck.Text))
            {
                WAREHOUSE_CODE = string.Empty;
            }
            if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
            {
                putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
            {
                end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrWhiteSpace(txt_ck.Text) ||
                string.IsNullOrWhiteSpace(putin_date) ||
                string.IsNullOrWhiteSpace(end_date))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the necessary conditions and then execute the query, prompt: warehouse, receipt date！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                return null;
            }
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("CHK_NO", CHK_NOS);//收料单号
            data.Add("SUPPLIERS_NAME2", txt_cgcs.Text);//采购厂商
            data.Add("ITEM_NO", txt_wpbh.Text);//料品编码
            data.Add("DETERMINE", cbo_wgjg.Text);//外观结果
            data.Add("CSJG", cbo_csjg.Text);//测试结果
            data.Add("SUPPLIERS_NAME", txt_sccs.Text);//生产厂商
            data.Add("rcpt_dateS", putin_date);//收货日期开始
            data.Add("rcpt_dateE", end_date);//收货日期结束
            data.Add("ORG_ID", ORG_ID);//工厂
            data.Add("STOC_NO", WAREHOUSE_CODE);//仓库
            data.Add("CSQYZK", cbo_qyzk.Text);//取样状况 
            data.Add("closing_status", cbo_status.Text);//状态 
            data.Add("pageSize", pageSize);
            data.Add("pageIndex", pageIndex);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Bad_Report",//类名
                                        "GetBad_Report_Main2",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }

            return ret;
        }

        private void F_IQC_Bad_Report_Main_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";

            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";
            pageControl1.BindPageEvent += GetBad_Report_Main;

            //LoadPage();
            this.dataGridView1.ClearSelection();
            // this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
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
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("SELECT"))//查询
                        {

                            dic.Add("CHK_NO", dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString());//收料单号
                            dic.Add("ITEM_NO", dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString());//料号
                            dic.Add("CHK_SEQ", dataGridView1.CurrentRow.Cells["CHK_SEQ"].Value.ToString());
                            if (!string.IsNullOrWhiteSpace(dataGridView1.CurrentRow.Cells["ITEM_TYPE_NO"].Value.ToString()))
                            {
                                string ITEM_TYPE_NO = dataGridView1.CurrentRow.Cells["ITEM_TYPE_NO"].Value.ToString().Substring(0, 3);//物料类型
                                if (ITEM_TYPE_NO.Contains("401"))
                                {
                                    using (F_IQC_Bad_Report_Leather frm = new F_IQC_Bad_Report_Leather(dic,"0"))
                                    {
                                        //frm.Text = "皮料不良报告";//皮料不良报告
                                        frm.Text = "Bad leather report";//皮料不良报告
                                        frm.ShowDialog();
                                        LoadPage();
                                    }
                                }
                                else
                                {
                                    using (F_IQC_Bad_Report_NoLeather frm = new F_IQC_Bad_Report_NoLeather(dic, "1"))
                                    {
                                        //frm.Text = "非皮料不良报告";//非皮料不良报告
                                        frm.Text = "Non-leather bad report";//非皮料不良报告
                                        frm.ShowDialog();
                                        LoadPage();
                                    }
                                    //using (F_IQC_Bad_Report_Leather frm = new F_IQC_Bad_Report_Leather(dic,"1"))
                                    //{
                                    //    //frm.Text = "非皮料不良报告";//非皮料不良报告
                                    //    frm.Text = "Non-leather bad report";//非皮料不良报告
                                    //    frm.ShowDialog();
                                    //    LoadPage();
                                    //}
                                }

                            }
                            else
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Material type data is missing, please check！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            }


                        }
                        else if (cell.CurrentItem.Equals("DELETE"))//删除
                        {

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

        private void txt_ck_DoubleClick(object sender, EventArgs e)
        {
//            string sql = $@" 
//SELECT
//	WH.ORG_ID 工厂代号,
//	o.ORG_NAME 工厂名称,
// 	WH.WAREHOUSE_CODE 仓库代号,
// 	WH.WAREHOUSE_NAME 仓库名称
//FROM
//	MMS_WAREHOUSE_MANAGE wh 
//INNER JOIN BASE001M o ON o.ORG_CODE=WH.ORG_ID 
//";

            string sql = $@" 
SELECT
	WH.ORG_ID Factory_Code,
	o.ORG_NAME Factory_Name,
 	WH.WAREHOUSE_CODE Warehouse_Code,
 	WH.WAREHOUSE_NAME Warehouse_Name
FROM
	MMS_WAREHOUSE_MANAGE wh 
INNER JOIN BASE001M o ON o.ORG_CODE=WH.ORG_ID 
";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                ORG_ID = frmData.RetData.Rows[0]["Factory_Code"].ToString();
                WAREHOUSE_CODE = frmData.RetData.Rows[0]["Warehouse_Code"].ToString();
                txt_ck.Text = frmData.RetData.Rows[0]["Warehouse_Name"].ToString();
            }
        }

        
    }
}
