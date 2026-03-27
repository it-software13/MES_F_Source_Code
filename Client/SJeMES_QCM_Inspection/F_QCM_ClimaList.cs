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

namespace SJeMES_QCM_Inspection
{
    public partial class F_QCM_ClimaList : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_ClimaList()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(start_date);
            InitDateTimePicker(end_date);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }


        string Type = "1"; // 0-测试中 1-已完成
        //记录当前页签
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                Type = "1";
            }
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                Type = "2";
            }

        }

        //查询按钮
        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (Type == "1")//检测中查询
            {
                pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
                pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
                pageControl1.SetPage();
            }
            else // 已完成
            {
                pageControl2.PageSize = int.Parse(enum_page.enum_PageSize);
                pageControl2.PageIndex = int.Parse(enum_page.PageIndex);
                pageControl2.SetPage();
            }
        }

        //获取待处理数据
        public void GetDetail(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("TYPE", "1");
                data.Add("CHK_NO", this.txt_order.Text); //收料单号
                data.Add("PRO_VEND_NAME", this.txt_production_manufacturer.Text); //生产厂商
                data.Add("PUR_VEND_NAME", this.txt_purchase_manufacturer.Text); //采购厂商
                //data.Add("STATUS", this.txt_Status2.Text); //状态
                data.Add("ITEM_NO", this.txt_ITEM_NO.Text); //料品编码
                data.Add("start_date", start_date); //开始日期
                data.Add("end_date", end_date); //结束日期
                data.Add("TSCHECK_RESULT", this.txt_Result.Text); //物性结果
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.MaterialInspection", "GetMaterialInspectionList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    dataGridView1.Rows.Clear();
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["RCPT_DATE"].Value = dr["RCPT_DATE"].ToString();//生产厂商
                            dgvr.Cells["PRO_VEND_NO"].Value = dr["PRO_VEND_NO"].ToString();//生产厂商名称
                            dgvr.Cells["PRO_VEND_NAME"].Value = dr["PRO_VEND_NAME"].ToString();//收料单号
                            dgvr.Cells["CHK_NO"].Value = dr["CHK_NO"].ToString();//收料单号
                            dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();//收料单号
                            dgvr.Cells["PUR_VEND_NO"].Value = dr["PUR_VEND_NO"].ToString();//收料单号

                            dgvr.Cells["PUR_VEND_NAME"].Value = dr["PUR_VEND_NAME"].ToString();//料号
                            dgvr.Cells["APCHECK_DATE"].Value = dr["APCHECK_DATE"].ToString();//采购厂商编号
                            dgvr.Cells["TSCHECK_DATE"].Value = dr["TSCHECK_DATE"].ToString();//采购厂商名称
                            dgvr.Cells["PR_UNIT"].Value = dr["PR_UNIT"].ToString();//采购单号
                            dgvr.Cells["ORD_QTY"].Value = dr["ORD_QTY"].ToString();//检验人员
                            dgvr.Cells["PURCHASE_NO"].Value = dr["PURCHASE_NO"].ToString();//实验室送检日期
                            dgvr.Cells["APCHECK_RESULT"].Value = dr["APCHECK_RESULT"].ToString();//实验室送检日期
                            dgvr.Cells["SAMP_CONDITION"].Value = dr["SAMP_CONDITION"].ToString();//实验室送检日期
                            dgvr.Cells["TSCHECK_RESULT"].Value = dr["TSCHECK_RESULT"].ToString();//实验室送检日期
                            dgvr.Cells["INSPECTOR"].Value = dr["INSPECTOR"].ToString();//实验室送检日期
                            dgvr.Cells["INSPECT_QTY"].Value = dr["INSPECT_QTY"].ToString();//实验室送检日期
                            dgvr.Cells["OK_QTY"].Value = dr["OK_QTY"].ToString();//实验室送检日期
                            dgvr.Cells["NG_QTY"].Value = dr["NG_QTY"].ToString();//实验室送检日期
                            dgvr.Cells["REPAIR_QTY"].Value = dr["REPAIR_QTY"].ToString();//实验室送检日期
                            dgvr.Cells["WAREHOUSE_QTY"].Value = dr["WAREHOUSE_QTY"].ToString();//实验室送检日期
                            i++;
                        }
                        totalCount = int.Parse(dic["rowCount"].ToString());

                        dataGridView1.ClearSelection();
                        this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //获取已完成数据
        public void GetFinishDetail(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("TYPE", "2");
                data.Add("CHK_NO", this.txt_order.Text); //收料单号
                data.Add("PRO_VEND_NAME", this.txt_production_manufacturer.Text); //生产厂商
                data.Add("PUR_VEND_NAME", this.txt_purchase_manufacturer.Text); //采购厂商
                data.Add("ITEM_NO", this.txt_ITEM_NO.Text); //物料编码
                data.Add("start_date", start_date); //开始日期
                data.Add("end_date", end_date); //结束日期
                data.Add("TSCHECK_RESULT", this.txt_Result.Text); //物性结果
                data.Add("WAREHOUSE_QTY", this.txt_warehouse.Text); //仓库
                data.Add("SAMP_CONDITION", this.txt_status.Text); //取样状态

                //data.Add("STATUS", this.txt_Status2.Text); //状态
                data.Add("APCHECK_RESULT", this.txt_WG_Result.Text); //外观结果
                
                
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.MaterialInspection", "GetMaterialInspectionList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    dataGridView2.Rows.Clear();
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView2.Rows.Add();
                            DataGridViewRow dgvr = dataGridView2.Rows[i];
                            dgvr.Cells["RCPT_DATE2"].Value = dr["RCPT_DATE"].ToString();//生产厂商
                            dgvr.Cells["PRO_VEND_NO2"].Value = dr["PRO_VEND_NO"].ToString();//生产厂商名称
                            dgvr.Cells["PRO_VEND_NAME2"].Value = dr["PRO_VEND_NAME"].ToString();//收料单号
                            dgvr.Cells["CHK_NO2"].Value = dr["CHK_NO"].ToString();//收料单号
                            dgvr.Cells["ITEM_NO2"].Value = dr["ITEM_NO"].ToString();//收料单号
                            dgvr.Cells["PUR_VEND_NO2"].Value = dr["PUR_VEND_NO"].ToString();//收料单号

                            dgvr.Cells["PUR_VEND_NAME2"].Value = dr["PUR_VEND_NAME"].ToString();//料号
                            dgvr.Cells["APCHECK_DATE2"].Value = dr["APCHECK_DATE"].ToString();//采购厂商编号
                            dgvr.Cells["TSCHECK_DATE2"].Value = dr["TSCHECK_DATE"].ToString();//采购厂商名称
                            dgvr.Cells["PR_UNIT2"].Value = dr["PR_UNIT"].ToString();//采购单号
                            dgvr.Cells["ORD_QTY2"].Value = dr["ORD_QTY"].ToString();//检验人员
                            dgvr.Cells["PURCHASE_NO2"].Value = dr["PURCHASE_NO"].ToString();//实验室送检日期
                            dgvr.Cells["APCHECK_RESULT2"].Value = dr["APCHECK_RESULT"].ToString();//实验室送检日期
                            dgvr.Cells["SAMP_CONDITION2"].Value = dr["SAMP_CONDITION"].ToString();//实验室送检日期
                            dgvr.Cells["TSCHECK_RESULT2"].Value = dr["TSCHECK_RESULT"].ToString();//实验室送检日期
                            dgvr.Cells["INSPECTOR2"].Value = dr["INSPECTOR"].ToString();//实验室送检日期
                            dgvr.Cells["INSPECT_QTY2"].Value = dr["INSPECT_QTY"].ToString();//实验室送检日期
                            dgvr.Cells["OK_QTY2"].Value = dr["OK_QTY"].ToString();//实验室送检日期
                            dgvr.Cells["NG_QTY2"].Value = dr["NG_QTY"].ToString();//实验室送检日期
                            dgvr.Cells["REPAIR_QTY2"].Value = dr["REPAIR_QTY"].ToString();//实验室送检日期
                            dgvr.Cells["WAREHOUSE_QTY2"].Value = dr["WAREHOUSE_QTY"].ToString();//实验室送检日期
                            i++;
                        }
                        totalCount = int.Parse(dic["rowCount"].ToString());
                        dataGridView2.ClearSelection();
                        this.dataGridView2.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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

        private void F_QCM_ClimaList_Load(object sender, EventArgs e)
        {
            this.start_date.Format = DateTimePickerFormat.Custom;
            this.start_date.CustomFormat = " ";

            this.end_date.Format = DateTimePickerFormat.Custom;
            this.end_date.CustomFormat = " ";
            //只要加载一次委托 
            pageControl1.BindPageEvent += GetDetail;

            pageControl2.BindPageEvent += GetFinishDetail;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //待处理
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();

            //已完成
            pageControl2.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl2.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl2.SetPage();
        }

        //待处理按钮
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                //string INSPECTION_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["INSPECTION_NO"].Value);
                string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                        return;
                    if (cell.CurrentItem.Equals("selectbtn"))
                    {

                        //F_QCM_ReportPrint reportPrint = new F_QCM_ReportPrint(INSPECTION_NO);
                        //reportPrint.ShowDialog();
                    }
                    
                }
            }

        }
        //已完成按钮
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                string INSPECTION_NO = Convert.ToString(dataGridView2.CurrentRow.Cells["INSPECTION_NO2"].Value);
                string name = this.dataGridView2.Columns[e.ColumnIndex].Name;
                if (name == "operation2")
                {
                    DataGridViewOperationCell cell = this.dataGridView2.Rows[this.dataGridView2.CurrentRow.Index].Cells["operation2"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                        return;
                    if (cell.CurrentItem.Equals("selectbtn2"))
                    {

                        //F_QCM_ReportPrint reportPrint = new F_QCM_ReportPrint(INSPECTION_NO);
                        //reportPrint.ShowDialog();
                    }

                }
            }
        }
    }
}
