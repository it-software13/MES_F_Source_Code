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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_IQC
{
    public partial class F_IQC_Customer_Complaint_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        List<lisstu> vc = new List<lisstu>();
        List<lisretstu> retvc = new List<lisretstu>();
        public F_IQC_Customer_Complaint_Main()
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

        /// <summary>
        /// 初始化分页
        /// </summary>
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 客户投诉主页查询
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetCustomer_Complaint_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    p.Add("datestart", dateTimePicker1.Value.ToString("yyyy/MM/dd 00:00:00"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    p.Add("dateend", dateTimePicker2.Value.ToString("yyyy/MM/dd 23:59:59"));
                }
                p.Add("shoe_name", textBox2.Text.Trim());
                p.Add("prod_no", textBox1.Text.Trim());
                p.Add("PO_ORDER", textBox3.Text.Trim());
                p.Add("COUNTRY_REGION", textBox4.Text.Trim());
                p.Add("DEFECT_CONTENT", textBox5.Text.Trim());
                p.Add("DEVELOP_SEASON", textBox6.Text.Trim());
                p.Add("Category", textBox7.Text.Trim());
                p.Add("STATUS", comboBox1.SelectedValue.ToString());
                p.Add("processing_results_status", comboBox2.SelectedValue.ToString());
                p.Add("COMPLAINT_NO", textBox8.Text.Trim());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                            "GetCustomer_Complaint_Main",//方法名
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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["mid"].Value = dr["mid"].ToString();
                        dgvr.Cells["投诉编号"].Value = dr["COMPLAINT_NO"].ToString();
                        dgvr.Cells["序号"].Value = i + 1;
                        dgvr.Cells["投诉日期"].Value = dr["COMPLAINT_DATE"].ToString();
                        dgvr.Cells["国家区域"].Value = dr["COUNTRY_REGION"].ToString();
                        dgvr.Cells["投诉PO号"].Value = dr["PO_ORDER"].ToString();
                        dgvr.Cells["投诉PO数量"].Value = dr["ts_posl"].ToString();
                        dgvr.Cells["问题点"].Value = dr["DEFECT_CONTENT"].ToString();
                        dgvr.Cells["不良数量"].Value = dr["NG_QTY"].ToString();
                        dgvr.Cells["投诉金额"].Value = dr["COMPLAINT_MONEY"].ToString();
                        if (dr["STATUS"].ToString() == "0")
                           // dgvr.Cells["状态"].Value = "未结案";
                            dgvr.Cells["状态"].Value = "open case";
                        else if (dr["STATUS"].ToString() == "1")
                           // dgvr.Cells["状态"].Value = "结案";
                            dgvr.Cells["状态"].Value = "Closed";
                        if (dr["processing_results_status"].ToString() == "0")
                            //dgvr.Cells["处理结果"].Value = "接收投诉";
                            dgvr.Cells["处理结果"].Value = "receive complaints";
                        else if (dr["processing_results_status"].ToString() == "1")
                            //dgvr.Cells["处理结果"].Value = "客户撤销投诉";
                            dgvr.Cells["处理结果"].Value = "Customer withdraws complaint";
                        else if (dr["processing_results_status"].ToString() == "2")
                            //dgvr.Cells["处理结果"].Value = "退货处理";
                            dgvr.Cells["处理结果"].Value = "Return processing";
                        else
                            dgvr.Cells["处理结果"].Value = "";
                        dgvr.Cells["开发季度"].Value = dr["DEVELOP_SEASON"].ToString();
                        dgvr.Cells["Category"].Value = dr["Category"].ToString();
                        dgvr.Cells["开发课"].Value = dr["user_section"].ToString();
                        dgvr.Cells["量产月份"].Value = dr["PRODUCT_MONTH"].ToString();
                        dgvr.Cells["ART"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["鞋型"].Value = dr["shoe_name"].ToString();
                        dgvr.Cells["Material_Way"].Value = dr["Material_Way"].ToString();
                        dgvr.Cells["FOB"].Value = dr["FOB"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_IQC_Customer_Complaint_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            #region 状态
            vc.Add(new lisstu()
            {
                code = "",
                value = ""
            });
            vc.Add(new lisstu()
            {
                code = "0",
               // value = "未结案"
                value = "open case"
            });
            vc.Add(new lisstu()
            {
                code = "1",
                //value = "已结案"
                value = "Closed"
            });
            comboBox1.DataSource = vc;
            comboBox1.DisplayMember = "value";
            comboBox1.ValueMember = "code";
            #endregion

            #region 处理结果状态
            retvc.Add(new lisretstu()
            {
                code = "",
                value = ""
            });
            retvc.Add(new lisretstu()
            {
                code = "0",
               // value = "接收投诉"
                value = "receive complaints"
            });
            retvc.Add(new lisretstu()
            {
                code = "1",
                //value = "客户撤销投诉"
                value = "Customer withdraws complaint"
            });
            retvc.Add(new lisretstu()
            {
                code = "2",
               // value = "退货处理"
                value = "Return processing"
            });
            comboBox2.DataSource = retvc;
            comboBox2.DisplayMember = "value";
            comboBox2.ValueMember = "code";
            #endregion

            pageControl1.BindPageEvent += GetCustomer_Complaint_Main;
            LoadPage();
            this.dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        public class lisstu
        {
            public string code { get; set; }
            public string value { get; set; }
        }
        public class lisretstu
        {
            public string code { get; set; }
            public string value { get; set; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (F_IQC_Customer_Complaint_Edit i=new F_IQC_Customer_Complaint_Edit())
            {
                i.ShowDialog();
            }
            LoadPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "操作")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["操作"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("编辑"))//编辑
                    {
                        string mid = dataGridView1.Rows[e.RowIndex].Cells["mid"].Value.ToString();
                        using (F_IQC_Customer_Complaint_Edit update = new F_IQC_Customer_Complaint_Edit(mid))
                        {
                            update.ShowDialog();
                        }
                        LoadPage();
                    }
                    else if (cell.CurrentItem.Equals("删除"))//删除
                    {
                        string mid = dataGridView1.Rows[e.RowIndex].Cells["mid"].Value.ToString();
                        DeleteCustomer_Complaint_Main(mid);
                    }
                    else if (cell.CurrentItem.Equals("处理"))//处理
                    {
                        string COMPLAINT_NO = dataGridView1.Rows[e.RowIndex].Cells["投诉编号"].Value.ToString();
                        string state = dataGridView1.Rows[e.RowIndex].Cells["状态"].Value.ToString();
                        string ART = dataGridView1.Rows[e.RowIndex].Cells["ART"].Value.ToString();
                        string PO = dataGridView1.Rows[e.RowIndex].Cells["投诉PO号"].Value.ToString();
                        using (F_IQC_Customer_Complaint_Dispose i =new F_IQC_Customer_Complaint_Dispose(COMPLAINT_NO, state, ART, PO))
                        {
                            i.ShowDialog();
                        }
                        LoadPage();
                    }
                }
            }
        }

        /// <summary>
        /// 客户投诉删除
        /// </summary>
        public void DeleteCustomer_Complaint_Main(string mid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("mid", mid);//条件 主表id
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_Customer_Complaint", "DeleteCustomer_Complaint_Main", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                if (ret.IsSuccess)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(ret.ErrMsg.ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //视图数据显示
                DataTable dts = GetCustomer_Complaint_Main_dc();
                if (dts.Rows.Count < 1)
                {
                    MessageBox.Show("No data export yet, please check whether the operation is correct");
                    return;
                }
                /* if (DT_EXCEL.Rows.Count < 1)
                 {
                     MessageBox.Show("数据为空，先搜索再做导出操作");
                     return;
                 }*/
                foreach (DataRow item in dts.Rows)
                {
                    if (item["STATUS"].ToString()=="0")
                        //item["STATUS"] = "未结案";
                        item["STATUS"] = "open case";
                    else if(item["STATUS"].ToString() == "1")
                       // item["STATUS"] = "结案";
                        item["STATUS"] = "close the case";

                    if (item["PROCESSING_RESULTS_STATUS"].ToString() == "0")
                       // item["PROCESSING_RESULTS_STATUS"] = "接收投诉";
                        item["PROCESSING_RESULTS_STATUS"] = "receive complaints";
                    else if (item["PROCESSING_RESULTS_STATUS"].ToString() == "1")
                        //item["PROCESSING_RESULTS_STATUS"] = "客户撤销投诉";
                        item["PROCESSING_RESULTS_STATUS"] = "Customer withdraws complaint";
                    else if (item["PROCESSING_RESULTS_STATUS"].ToString() == "2")
                        //item["PROCESSING_RESULTS_STATUS"] = "退货处理";
                        item["PROCESSING_RESULTS_STATUS"] = "Return processing";
                }
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("COMPLAINT_NO", "Complaint No");
                Execldic.Add("COMPLAINT_DATE", "Complaint date");
                Execldic.Add("COUNTRY_REGION", "country/area");
                Execldic.Add("PO_ORDER", "Complaint PO number");
                Execldic.Add("TS_POSL", "Number of Complaint POs");
                Execldic.Add("DEFECT_CONTENT", "Problems");
                Execldic.Add("NG_QTY", "a poor amount");
                Execldic.Add("COMPLAINT_MONEY", "Complaint amount");
                Execldic.Add("STATUS", "状态");
                Execldic.Add("PROCESSING_RESULTS_STATUS", "process result");
                Execldic.Add("DEVELOP_SEASON", "development quarter");
                Execldic.Add("Category", "Category");
                Execldic.Add("USER_SECTION", "Development class");
                Execldic.Add("PRODUCT_MONTH", "Mass production month");
                Execldic.Add("PROD_NO", "ART");
                Execldic.Add("SHOE_NAME", "Shoe type");
                Execldic.Add("Material_Way", "Material_Way");
                Execldic.Add("FOB", "FOB");

                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Customer complaints");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 客户投诉主页导出
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public DataTable GetCustomer_Complaint_Main_dc()
        {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                   // p.Add("datestart", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    p.Add("datestart", dateTimePicker1.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    //p.Add("dateend", dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    p.Add("dateend", dateTimePicker2.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                }
                p.Add("shoe_name", textBox2.Text.Trim());
                p.Add("prod_no", textBox1.Text.Trim());
                p.Add("PO_ORDER", textBox3.Text.Trim());
                p.Add("COUNTRY_REGION", textBox4.Text.Trim());
                p.Add("DEFECT_CONTENT", textBox5.Text.Trim());
                p.Add("DEVELOP_SEASON", textBox6.Text.Trim());
                p.Add("Category", textBox7.Text.Trim());
                p.Add("STATUS", comboBox1.SelectedValue.ToString());
                p.Add("processing_results_status", comboBox2.SelectedValue.ToString());
                p.Add("COMPLAINT_NO", textBox8.Text.Trim());

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Customer_Complaint",//类名
                                            "GetCustomer_Complaint_Main_dc",//方法名
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
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            return dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //视图数据显示
                DataTable dts = GetCustomer_Complaint_Main_dc();
                if (dts.Rows.Count < 1)
                {
                    MessageBox.Show("No data export yet, please check whether the operation is correct");
                    return;
                }
                /* if (DT_EXCEL.Rows.Count < 1)
                 {
                     MessageBox.Show("数据为空，先搜索再做导出操作");
                     return;
                 }*/
                //for (int i = 0; i < dts.Rows.Count; i++)
                //{
                //    dts.Rows.RemoveAt(i);
                //}
                dts.Rows.Clear();
                dts.Columns.Remove("ts_posl");
                dts.Columns.Remove("STATUS");
                dts.Columns.Remove("processing_results_status");
                dts.Columns.Remove("DEVELOP_SEASON");
                dts.Columns.Remove("Category");
                dts.Columns.Remove("user_section");
                dts.Columns.Remove("PRODUCT_MONTH");
                dts.Columns.Remove("prod_no");
                dts.Columns.Remove("shoe_name");
                dts.Columns.Remove("Material_Way");
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                //Execldic.Add("COMPLAINT_NO", "投诉编号");
                //Execldic.Add("COMPLAINT_DATE", "投诉日期");
                //Execldic.Add("COUNTRY_REGION", "国家区域");
                //Execldic.Add("PO_ORDER", "投诉PO号");
                //Execldic.Add("DEFECT_CONTENT", "问题点");
                //Execldic.Add("NG_QTY", "不良数量");
                //Execldic.Add("COMPLAINT_MONEY", "投诉金额");
                //Execldic.Add("FOB", "FOB");
                Execldic.Add("COMPLAINT_NO", "Complaint No");
                Execldic.Add("COMPLAINT_DATE", "Complaint date");
                Execldic.Add("COUNTRY_REGION", "country/area");
                Execldic.Add("PO_ORDER", "Complaint PO number");
                Execldic.Add("DEFECT_CONTENT", "Problems");
                Execldic.Add("NG_QTY", "a poor amount");
                Execldic.Add("COMPLAINT_MONEY", "Complaint amount");
                Execldic.Add("FOB", "FOB");

                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Customer Complaint Import Template");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Before importing, ensure the bad quantity, the complaint amount is a number type, and the complaint date is a date type, otherwise the import will fail", "Operation prompt！！！", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                        if (dt.Columns.Count !=8)
                        {
                            MessageBox.Show("Import template error, please refer to");
                            return;
                        }

                        //不能为空
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //if (dt.Rows[i]["投诉编号"]==null || string.IsNullOrWhiteSpace(dt.Rows[i]["投诉编号"].ToString()))
                            if (dt.Rows[i]["Complaint No"] ==null || string.IsNullOrWhiteSpace(dt.Rows[i]["Complaint No"].ToString()))
                            {
                                MessageBox.Show($@"The complaint number cannot be empty! No.{i+1}行!");
                                return;
                            }
                            //if (dt.Rows[i]["不良数量"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["不良数量"].ToString()))
                            if (dt.Rows[i]["a poor amount"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["a poor amount"].ToString()))
                            {
                                MessageBox.Show($@"The bad quantity cannot be empty! No.{i + 1}行!");
                                return;
                            }
                            //if (dt.Rows[i]["投诉金额"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["投诉金额"].ToString()))
                            if (dt.Rows[i]["Complaint amount"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["Complaint amount"].ToString()))
                            {
                                MessageBox.Show($@"Complaint amount cannot be empty! No.{i + 1}行!");
                                return;
                            }
                            //if (dt.Rows[i]["投诉PO号"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["投诉PO号"].ToString()))
                            //{
                            //    MessageBox.Show($@"投诉PO号不能为空!第{i + 1}行!");
                            //    return;
                            //}
                            if (dt.Rows[i]["FOB"] != null && !string.IsNullOrWhiteSpace(dt.Rows[i]["FOB"].ToString()))
                            {
                                decimal isNum = 0;
                                if(!decimal.TryParse(dt.Rows[i]["FOB"].ToString(),out isNum))
                                {
                                    MessageBox.Show($@"FOB must be a number! No.{i + 1}行!");
                                    return;
                                }
                            }
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
                                p.Add("import_type", 2);//客户投诉导入
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
                                    LoadPage();
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
    }
}
