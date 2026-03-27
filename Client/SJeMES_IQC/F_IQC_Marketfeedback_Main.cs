using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
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

namespace SJeMES_IQC
{
    public partial class F_IQC_Marketfeedback_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_Marketfeedback_Main()
        {
            InitializeComponent();
            //InitDateTimePicker(dateTimeP_putin_date);
            //InitDateTimePicker(dateTimeP_end_date);
            dateTimeP_putin_date.Text = DateTime.Now.AddMonths(-5).ToString("yyyy-MM");
            dateTimeP_end_date.Text = DateTime.Now.ToString("yyyy-MM");
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
        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void F_IQC_Marketfeedback_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
            dataGridView1.ClearSelection();
        }
        private void btn_select_Click(object sender, EventArgs e)
        {
            FormLoad();
        }
        public string GetDateListApi(int pageSize, int pageIndex)
        {
            string putin_date = string.Empty;
            string end_date = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
            {
                putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
            {
                end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM-dd");
            }
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("art", txt_art.Text);//art
            p.Add("name_t", txt_name.Text);//鞋型
            p.Add("putin_date", putin_date);
            p.Add("end_date", end_date);
            p.Add("pageSize", pageSize);
            p.Add("pageIndex", pageIndex);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.Marketfeedback",//类名
                                        "MianGetList",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            return retdata;
        }
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string retdata = GetDateListApi(pageSize, pageIndex);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["task_no"].Value = dr["task_no"].ToString();//任务编号
                        //dgvr.Cells["times"].Value = dr["times"].ToString();//月份
                        dgvr.Cells["factory"].Value = dr["factory"].ToString();//工厂
                        dgvr.Cells["region_no"].Value = dr["region_no"].ToString();//地区代号
                        dgvr.Cells["region_name"].Value = dr["region_name"].ToString();//地区名称
                        dgvr.Cells["art_id"].Value = dr["prod_no"].ToString();//art
                        dgvr.Cells["po"].Value = dr["po"].ToString();//po
                        dgvr.Cells["category"].Value = dr["style_seq"].ToString();//category
                        dgvr.Cells["name_t"].Value = dr["name_t"].ToString();//鞋型
                        dgvr.Cells["addtime"].Value = dr["production_month"].ToString();//生产日期
                        dgvr.Cells["main_code"].Value = dr["main_code"].ToString();//代码/名称
                        dgvr.Cells["minor_code"].Value = dr["minor_code"].ToString();
                        dgvr.Cells["content_name"].Value = dr["content_cn"].ToString();
                        dgvr.Cells["content_name2"].Value = dr["content_cn2"].ToString();
                        dgvr.Cells["fob_price"].Value = dr["fob_price"].ToString(); //FOB单价($)
                        dgvr.Cells["out_qty"].Value = dr["out_qty"].ToString();//退货数量
                        dgvr.Cells["compensation_amount"].Value = dr["compensation_amount"].ToString();//赔偿金额
                        dgvr.Cells["problem_point_desc"].Value = dr["problem_point_desc"].ToString();//问题点描述
                        dgvr.Cells["codeincode"].Value = dr["codeincode"].ToString();//合并代码
                        dgvr.Cells["RETURN_MONTH"].Value = dr["RETURN_MONTH"].ToString();//退货月份
                        if (dr["status"].ToString() == "0") 
                        {
                            dgvr.Cells["Column1"].Value = "audit";//審計
                        }
                        else
                        {
                            dgvr.Cells["Column1"].Value = "Cancel review";//取消审核
                        }
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

        private void btn_add_Click(object sender, EventArgs e)
        {
            using (F_IQC_Marketfeedback_Edit frm=new F_IQC_Marketfeedback_Edit(""))
            {
                frm.ShowDialog();
                FormLoad();
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
                    switch (dataGridView1.Columns[e.ColumnIndex].Name)
                    {
                        case "Delete":
                            if (MessageBox.Show("confirm deletion? ", "This delete cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("task_no", dataGridView1.CurrentRow.Cells["task_no"].Value.ToString());//任务编号

                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                            "SJeMES_IQC",//类库名
                                                            "SJeMES_IQC.Marketfeedback",//类名
                                                            "Delete_Main",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (!ret.IsSuccess)
                                {
                                    throw new Exception(ret.ErrMsg);
                                }
                                else
                                {
                                    MessageBox.Show("successfully deleted");
                                    FormLoad();
                                }
                            }
                            break;
                        case "Column1":
                            Dictionary<string, object> p1 = new Dictionary<string, object>();
                            p1.Add("task_no", dataGridView1.CurrentRow.Cells["task_no"].Value.ToString());//任务编号
                            string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJeMES_IQC",//类库名
                                                        "SJeMES_IQC.Marketfeedback",//类名
                                                        "Update_Status",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p1));

                            ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);
                            if (!ret1.IsSuccess)
                            {
                                throw new Exception(ret1.ErrMsg);
                            }
                            else
                            {
                                MessageBox.Show(ret1.ErrMsg);
                                FormLoad();
                            }
                            break;
                        case "update":
                            string task_no = dataGridView1.CurrentRow.Cells["task_no"].Value.ToString();
                            using (F_IQC_Marketfeedback_Edit frm = new F_IQC_Marketfeedback_Edit(task_no))
                            {
                                frm.ShowDialog();
                                FormLoad();
                            }
                            break;
                           

                    }


                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //中国区客户退货数据模板下载
        private void btn_rdiou_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                //dt.Columns.Add("PO");
                //dt.Columns.Add("退货月份");
                //dt.Columns.Add("年份/月份");
                //dt.Columns.Add("国家/地区代号");
                //dt.Columns.Add("码数");
                //dt.Columns.Add("新鞋数量");
                //dt.Columns.Add("旧鞋子数量");
                //dt.Columns.Add("主要不良代码");
                //dt.Columns.Add("次要不良代码");
                //dt.Columns.Add("FOB单价($)");
                //dt.Columns.Add("赔偿金额($)");
                //dt.Columns.Add("问题点描述");
                dt.Columns.Add("PO");
                dt.Columns.Add("Return_Month");
                dt.Columns.Add("Year/Month");
                dt.Columns.Add("Country_Code");
                dt.Columns.Add("Yardage");
                dt.Columns.Add("New_Shoes_Quantity");
                dt.Columns.Add("Old_Shoes_Quantity");
                dt.Columns.Add("Main_Bad_Code");
                dt.Columns.Add("Minor_Bad_Code");
                dt.Columns.Add("FOB_Unit_Price($)");
                dt.Columns.Add("Amount_Of_Compensation($)");
                dt.Columns.Add("Problem_Description");



                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                ExeclHelper.ExportToTrueExcel(dt, Execldic, "Import template for customer return data in China");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to download template" + ex.Message);
            }
        }
        //中国区客户退货数据导入
        private void btn_inputex_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Before importing, make sure that the new shoes, old shoes, FOB unit price, and compensation are digital types, otherwise the import will fail", "Operation tips！！！", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //创建文件弹出选择窗口（包括文件名）对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //判断选择的路径
                    string path = string.Empty;
                    ofd.Title = "Please select a file";
                    ofd.Filter = "EXECL|*.xlsx;*.xls";
                    string SafeFileName = "";
                    string filePath = "";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        SafeFileName = Path.GetExtension(ofd.FileName);
                        filePath = ofd.FileName;
                    }
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (SafeFileName != ".xlsx" && SafeFileName != ".xls")
                        {
                            MessageBox.Show("Wrong file type, please select (.xlsx,.xls) type file");
                            return;
                        }
                        DataTable dt = SJeMES_Framework.Common.NPOIHelper.ExcelToTable(filePath);
                        //|| dt.Columns[dt.Columns.Count - 1].ColumnName != "机台"
                        if (dt.Columns.Count != 12)
                        {
                            MessageBox.Show("Import template error, please refer to");
                            return;
                        }
                        if (dt != null)
                        {
                            SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.ShowDialog();
                            bool is_sure = frm.is_sure;
                            if (is_sure)
                            {
                                //请求api的数据展示
                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("SOURCE", dt);
                                p.Add("import_type", 8);//中国区客户退货数据导入代号
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                            "SJ_QCMAPI",//类库名
                                                            "SJ_QCMAPI.BASE",//类名
                                                            "ImportData",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (ret.IsSuccess)
                                {
                                    MessageBox.Show("Imported successfully");
                                    FormLoad();
                                }
                                else
                                {
                                    MessageBox.Show(ret.ErrMsg);
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
        //中国区客户退货数据导出
        private void btn_outex_Click(object sender, EventArgs e)
        {
            try
            {
                string retdata = GetDateListApi(1000000, 1);

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dts = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dts.Rows.Count < 1)
                {
                    MessageBox.Show("No data export yet, please check whether the operation is correct");
                    return;
                }
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                //Execldic.Add("RETURN_MONTH", "退货月份");
                ////Execldic.Add("TIMES", "生产月份");
                //Execldic.Add("FACTORY", "工厂名称");
                //Execldic.Add("REGION_NO", "国家/地区代号");
                //Execldic.Add("REGION_NAME", "国家/地区");
                //Execldic.Add("PROD_NO", "ART ID");
                //Execldic.Add("PO", "PO号");
                //Execldic.Add("STYLE_SEQ", "Category");
                //Execldic.Add("NAME_T", "鞋型名称");
                //Execldic.Add("PRODUCTION_MONTH", "生产日期");
                //Execldic.Add("MAIN_CODE", "主要不良代码");
                //Execldic.Add("MINOR_CODE", "次要不良代码");
                //Execldic.Add("CONTENT_CN", "主要不良原因");
                //Execldic.Add("CONTENT_CN2", "次要不良原因");
                //Execldic.Add("FOB_PRICE", "FOB单价($)");
                //Execldic.Add("OUT_QTY", "退货数量");
                //Execldic.Add("COMPENSATION_AMOUNT", "赔偿金额($)");
                //Execldic.Add("PROBLEM_POINT_DESC", "问题点描述");
                //Execldic.Add("CODEINCODE", "合并代码");
                Execldic.Add("RETURN_MONTH", "Return_Month");
                //Execldic.Add("TIMES", "生产月份");
                Execldic.Add("FACTORY", "Factory_Name");
                Execldic.Add("REGION_NO", "Country_Code");
                Execldic.Add("REGION_NAME", "Country/Region");
                Execldic.Add("PROD_NO", "ART ID");
                Execldic.Add("PO", "PO_Number");
                Execldic.Add("STYLE_SEQ", "Category");
                Execldic.Add("NAME_T", "Shoe_Type_Name");
                Execldic.Add("PRODUCTION_MONTH", "Production_Date");
                Execldic.Add("MAIN_CODE", "Main_Bad_Code");
                Execldic.Add("MINOR_CODE", "Minor_Bad_Code");
                Execldic.Add("CONTENT_CN", "Main_Adverse_Cause");
                Execldic.Add("CONTENT_CN2", "Secondary_Adverse_Cause");
                Execldic.Add("FOB_PRICE", "FOB_Unit_Price($)");
                Execldic.Add("OUT_QTY", "Return_Quantity");
                Execldic.Add("COMPENSATION_AMOUNT", "Amount_Of_Compensation($)");
                Execldic.Add("PROBLEM_POINT_DESC", "Problem_Description");
                Execldic.Add("CODEINCODE", "Merge_Code");
                List<string> list = new List<string>();
                string[] keyhread = { "ID", "TASK_NO", "STATUS"};
                for (int i = 0; i < keyhread.Length; i++)
                {
                    if (dts.Columns.Contains(keyhread[i]))
                    {
                        dts.Columns.Remove(keyhread[i]);
                    }
                }
                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Customer return data list in China");////中国区客户退货数据列表
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
