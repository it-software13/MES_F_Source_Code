using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using SJeMES_IQC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SJeMES_IQC.F_IQC_VWarehouse_Main;

namespace SJeMES_AQL
{
    public partial class F_AQL_ConfirmShoes_Store : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string MODULE_TYPE = string.Empty;
        public F_AQL_ConfirmShoes_Store(string _MODULE_TYPE)
        {
            InitializeComponent();
            MODULE_TYPE = _MODULE_TYPE;
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
        public string status = "0";//0头部 1-身
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
        /// 查询-确认鞋-存放管理-主页-aql
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoes_Store_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("shoe_name", textBox1.Text.Trim());
                p.Add("prod_no", textBox2.Text.Trim());
                p.Add("stock_name", textBox4.Text.Trim());
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    p.Add("wh_dateS", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    p.Add("wh_dateE", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                }
                List<string> ref_standard = new List<string>();
                foreach (System.Data.DataRowView item in this.checkedListBox1.CheckedItems)
                {
                    ref_standard.Add(item.Row["code"].ToString());
                }
                p.Add("ref_standard", ref_standard);
                p.Add("confirm_by", textBox3.Text.Trim());
                p.Add("WAREHOUSE_NAME", txt_warehouse.Text.Trim());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                p.Add("MODULE_TYPE", MODULE_TYPE);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                            "GetConfirmShoes_Store_Main",//方法名
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
                dataGridViewEx1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridViewEx1.Rows.Add();
                        DataGridViewRow dgvr = dataGridViewEx1.Rows[i];
                        dgvr.Cells["aid"].Value = dr["aid"].ToString();
                        dgvr.Cells["序号"].Value = i + 1;//序号
                        dgvr.Cells["鞋型"].Value = dr["shoe_name"].ToString();//鞋型
                        dgvr.Cells["ART"].Value = dr["prod_no"].ToString();
                        dgvr.Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString();
                        dgvr.Cells["STOCK_NAME"].Value = dr["stock_name"].ToString();
                        dgvr.Cells["Confirmor"].Value = dr["confirm_by"].ToString();
                        dgvr.Cells["state"].Value = dr["state"].ToString();//dgvr状态
                        dgvr.Cells["quantity"].Value = dr["count"].ToString();//dgvr数量
                        dgvr.Cells["unit"].Value = dr["unit"].ToString();//dgvr单位
                        dgvr.Cells["入库日期"].Value = dr["wh_date"].ToString();//dgvr入库日期
                        dgvr.Cells["接收日期"].Value = dr["received_time"].ToString();
                        dgvr.Cells["最近一次确认日期"].Value = dr["confirmation_time"].ToString();
                        dgvr.Cells["foot"].Value = dr["FOOT"].ToString();
                        dgvr.Cells["WAREHOUSE_CODE"].Value = dr["WAREHOUSE_CODE"].ToString();
                        dgvr.Cells["WAREHOUSE_NAME"].Value = dr["WAREHOUSE_NAME"].ToString(); 
                        dgvr.Cells["scrap_life"].Value = dr["scrap_life"].ToString();
                        dgvr.Cells["reminder_duration"].Value = dr["reminder_duration"].ToString();

                        //if (!string.IsNullOrWhiteSpace(dr["confirmation_time"].ToString()))
                        //{
                        //    //待报废提醒日期
                        //    string dbf = (Convert.ToDateTime(dr["confirmation_time"].ToString()).AddYears(Convert.ToInt32(dr["scrap_life"].ToString())).ToString());
                        //    dgvr.Cells["报废到期日期"].Value = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                        //}
                        //else if (!string.IsNullOrWhiteSpace(dr["received_time"].ToString()) &&
                        //    string.IsNullOrWhiteSpace(dr["confirmation_time"].ToString()))
                        //{
                        //    //待报废提醒日期
                        //    string dbf = (Convert.ToDateTime(dr["received_time"].ToString()).AddYears(Convert.ToInt32(dr["scrap_life"].ToString())).ToString());
                        //    dgvr.Cells["报废到期日期"].Value = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                        //}
                        //else if (!string.IsNullOrWhiteSpace(dr["wh_date"].ToString()) && string.IsNullOrWhiteSpace(dr["received_time"].ToString()) &&
                        //    string.IsNullOrWhiteSpace(dr["confirmation_time"].ToString()))
                        //{
                        //    //待报废提醒日期
                        //    string dbf = (Convert.ToDateTime(dr["wh_date"].ToString()).AddYears(Convert.ToInt32(dr["scrap_life"].ToString())).ToString());
                        //    dgvr.Cells["报废到期日期"].Value = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                        //}                                                                                       //ORIGINAL CODE COMMENTED BY ASHOK

                        if (!string.IsNullOrWhiteSpace(dr["confirmation_time"].ToString()))
                        {
                            //待报废提醒日期
                            string dbf = (Convert.ToDateTime(dr["confirmation_time"].ToString()).AddDays(Convert.ToInt32(dr["reminder_duration"].ToString())).ToString());
                            dgvr.Cells["待报废提醒日期"].Value = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                            TimeSpan ts = Convert.ToDateTime(dbf) - Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            if (ts.Days <= 0)
                            {
                                for (int a = 0; a < dataGridViewEx1.ColumnCount; a++)
                                {
                                    dataGridViewEx1.Rows[i].Cells[a].Style.ForeColor = Color.Red;
                                }
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(dr["received_time"].ToString()) &&
                            string.IsNullOrWhiteSpace(dr["confirmation_time"].ToString()))
                        {
                            //待报废提醒日期
                            string dbf = (Convert.ToDateTime(dr["received_time"].ToString()).AddDays(Convert.ToInt32(dr["reminder_duration"].ToString())).ToString());
                            dgvr.Cells["待报废提醒日期"].Value = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                            TimeSpan ts = Convert.ToDateTime(dbf) - Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            if (ts.Days <= 0)
                            {
                                for (int a = 0; a < dataGridViewEx1.ColumnCount; a++)
                                {
                                    dataGridViewEx1.Rows[i].Cells[a].Style.ForeColor = Color.Red;
                                }
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(dr["wh_date"].ToString()) && string.IsNullOrWhiteSpace(dr["received_time"].ToString()) &&
                            string.IsNullOrWhiteSpace(dr["confirmation_time"].ToString()))
                        {
                            //待报废提醒日期
                            string dbf = (Convert.ToDateTime(dr["wh_date"].ToString()).AddDays(Convert.ToInt32(dr["reminder_duration"].ToString())).ToString());
                            dgvr.Cells["待报废提醒日期"].Value = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                            TimeSpan ts = Convert.ToDateTime(dbf) - Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            if (ts.Days <= 0)
                            {
                                for (int a = 0; a < dataGridViewEx1.ColumnCount; a++)
                                {
                                    dataGridViewEx1.Rows[i].Cells[a].Style.ForeColor = Color.Red;
                                }
                            }
                        }     //THIS CODE IS CHANGED BY ASHOK


                        


                        //if (!string.IsNullOrWhiteSpace(dgvr.Cells["报废到期日期"].Value.ToString()))
                        //{
                        //    //报废到期日期
                        //    //string bf = Convert.ToDateTime(dgvr.Cells["报废到期日期"].Value).AddDays(-Convert.ToInt32(dr["reminder_duration"].ToString())).ToString();
                        //    string bf = Convert.ToDateTime(Convert.ToDateTime(dgvr.Cells["入库日期"].Value)).AddDays(Convert.ToInt32(dr["reminder_duration"].ToString())).ToString();
                        //    dgvr.Cells["待报废提醒日期"].Value = Convert.ToDateTime(bf).ToString("yyyy-MM-dd");
                        //    TimeSpan ts = Convert.ToDateTime(bf) - Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        //    if (ts.Days <= 0)
                        //    {
                        //        for (int a = 0; a < dataGridViewEx1.ColumnCount; a++)
                        //        {
                        //            dataGridViewEx1.Rows[i].Cells[a].Style.ForeColor = Color.Red;
                        //        }
                        //    }
                        //}                                                                                  //THIS CODE ALSO COMMENTED BY ASHOK

                        dgvr.Cells["重做原因"].Value = dr["redo_reason"].ToString();

                        //TimeSpan ts2 = Convert.ToDateTime(dgvr.Cells["待报废提醒日期"].Value) - Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        //if(ts2.Days<=0)
                        //{
                        //    string Article = dgvr.Cells["ART"].Value.ToString();
                        //    string location = dgvr.Cells["STOCK_CODE"].Value.ToString();
                        //    string msg = "Please review the article " + Article + " located in location " + location + "";
                        //    MessageBox.Show(msg);
                        //}


                        dataGridViewEx1.Rows[i].Cells["Out_of_Warehouse"].Value = "out of warehouse";//out of warehouse//出库
                        switch (dr["state"].ToString())
                        {
                            //case "在库":
                            case "In_Warehouse":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Return"]).Enabled = false;
                                break;
                            //case "报废":
                            case "scrap":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Return"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Lend"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Out_of_Warehouse"]).Enabled = false; 
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["reconfirm"]).Enabled = false;
                                break;
                            //case "借出":
                            case "lend":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Lend"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Out_of_Warehouse"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["reconfirm"]).Enabled = false;
                                break;
                            //case "出库":
                            case "Out_of_Warehouse":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Lend"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Return"]).Enabled = false;
                                //((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["出库"]).Enabled = false;
                                dataGridViewEx1.Rows[i].Cells["Out_of_Warehouse"].Value = "Storage";
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["reconfirm"]).Enabled = false;
                                break;
                            case "reconfirm":
                            //case "再确认":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Return"]).Enabled = false;
                                break;
                            //case "待确认":
                            case "rework":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Return"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Lend"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Out_of_Warehouse"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["reconfirm"]).Enabled = false;
                                break;
                            //case "退开发":
                            case "Return_to_development":
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Return"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["Lend"]).Enabled = false;
                                //((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["出库"]).Enabled = false;
                                ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[i].Cells["reconfirm"]).Enabled = false;
                                //dataGridViewEx1.Rows[i].Cells["出库"].Value = "入库";
                                dataGridViewEx1.Rows[i].Cells["Out_of_Warehouse"].Value = "Storage";
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridViewEx1.ClearSelection();

                SJeMES_Framework.Common.UIHelper.LoadDgv(dataGridViewEx1);
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询-确认鞋-存放管理-主页-状态
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoes_Store_Main_zt()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                            "GetConfirmShoes_Store_Main_zt",//方法名
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
                checkedListBox1.DataSource = dt;
                checkedListBox1.DisplayMember = "value";
                checkedListBox1.ValueMember = "code";
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public string CurrDefaultPrinter = "";
        public void F_AQL_ConfirmShoes_Store_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GetConfirmShoes_Store_Main_zt();

            pageControl1.BindPageEvent += GetConfirmShoes_Store_Main;
            LoadPage();
            this.dataGridViewEx1.ClearSelection();
            comboBox1.Items.Clear();
            PrintDocument printDocument = new PrintDocument();
            CurrDefaultPrinter = printDocument.PrinterSettings.PrinterName;
            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(fPrinterName);
            }
            #endregion
            if (!string.IsNullOrEmpty(CurrDefaultPrinter))
                comboBox1.SelectedItem = CurrDefaultPrinter;

            SJeMES_Framework.Common.UIHelper.AdjustComboBoxDropDownListWidth(comboBox1);
        }

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机

        /// <summary>
        /// 打印全部二维码-aql
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qr_code_print(string aid,string MODULE_TYPE)
        {

            string Print = this.comboBox1.Text;
            if (string.IsNullOrEmpty(Print))
            {
                MessageBox.Show("Please select a printer！");
                return;
            }
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("aid", aid);
            data.Add("MODULE_TYPE", MODULE_TYPE);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                        "GetConfirmShoes_Store_Print",//方法名
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
            var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            string Review_Date = string.Empty;
            foreach (DataRow dr in dt2.Rows)
            {
                Review_Date = (Convert.ToDateTime(dr["wh_date"].ToString()).AddDays(Convert.ToInt32(dr["reminder_duration"].ToString())).ToString());
            }
               
            //dgvr.Cells["待报废提醒日期"].Value = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");


            if (dt2.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PROD_NO");
                dt.Columns.Add("SHOE_NAME");
                dt.Columns.Add("ATTRIBUTION");
                dt.Columns.Add("qr_code");
                dt.Columns.Add("FOOT");
                dt.Columns.Add("ISSUE_DATE");
                dt.Columns.Add("REVIEW_DATE");
                dt.Columns.Add("MCS_LOCATION");
                dt.Columns.Add("SEASON");
                dt.Columns.Add("INSPECTOR");
                dt.Rows.Add();
                dt.Rows[0]["PROD_NO"] = dt2.Rows[0]["PROD_NO"];
                dt.Rows[0]["SHOE_NAME"] = dt2.Rows[0]["SHOE_NAME"];
                dt.Rows[0]["FOOT"] = dt2.Rows[0]["FOOT"];
                if (dt2.Rows[0]["ATTRIBUTION"].ToString() == "0")
                    dt.Rows[0]["ATTRIBUTION"] = "inspection room";//验货室
                else if (dt2.Rows[0]["ATTRIBUTION"].ToString() == "1")
                    dt.Rows[0]["ATTRIBUTION"] = "Raw material inspection unit";//原材料检验股

                string foot = dt2.Rows[0]["FOOT"].ToString() == "left foot" ? "0" : "1";//左脚
                string qrcode = dt2.Rows[0]["PROD_NO"] + ";" + dt2.Rows[0]["scrap_life"] + ";" + dt2.Rows[0]["reminder_duration"] + ";" + dt2.Rows[0]["ATTRIBUTION"] +";"+ foot;
                dt.Rows[0]["qr_code"] = qrcode;
                dt.Rows[0]["ISSUE_DATE"] = dt2.Rows[0]["WH_DATE"];
                dt.Rows[0]["REVIEW_DATE"] = Review_Date;
                dt.Rows[0]["MCS_LOCATION"] = dt2.Rows[0]["STOCK_NAME"];
                dt.Rows[0]["SEASON"] = dt2.Rows[0]["DEVELOP_SEASON"];
                dt.Rows[0]["INSPECTOR"] = dt2.Rows[0]["STAFF_NAME"];
                //WriteTxt(dt, "Shipping Confirmation Shoes Storage Management Printing", Application.StartupPath + "/Printer/BarCodeModel/Shipping Confirmation Shoes Storage Management Printing.txt", 1);//出货确认鞋存放管理打印
               // WriteTxt(dt, "ComfirmShoes", Application.StartupPath + "/Printer/BarCodeModel/ComfirmShoes.txt", 1);//出货确认鞋存放管理打印
                WriteTxt(dt, "出货确认鞋条码打印", Application.StartupPath + "/Printer/BarCodeModel/出货确认鞋条码打印.txt", 1);
                SetDefaultPrinter(comboBox1.Text);
                if (string.IsNullOrEmpty(Program.DefaultPrinter))
                {
                    Program.DefaultPrinter = comboBox1.Text;
                    SetDefaultPrinter(Program.DefaultPrinter);
                }
            }

            Thread.Sleep(1000);


            #region 启动答应程序
            Process p = new Process();
            p.StartInfo.FileName = "ComfirmShoes.bat";//出货确认鞋存放管理打印
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();//启动 
            p.WaitForExit(5 * 1000);//等待上述进程执行完毕
            //p.WaitForExit();//这个会一直等待
            if (p.HasExited == false)
            {
                p.Kill();
            }
            #endregion

            MessageBox.Show("Printed successfully！");

            if (!string.IsNullOrEmpty(CurrDefaultPrinter))
                SetDefaultPrinter(CurrDefaultPrinter);
        }

        public static bool WriteTxt(DataTable dt, string ModelName, string Path, int printQty)
        {
            try
            {
                string Data = string.Empty;

                string FilePath = Path;

                foreach (DataColumn dc in dt.Columns)
                {
                    Data += dc.ColumnName + ",";
                }

                Data = Data.Remove(Data.Length - 1) + "\r\n";

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < printQty; i++)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Data += dr[dc].ToString() + ",";
                        }

                        Data = Data.Remove(Data.Length - 1) + "\r\n";
                    }

                }
                WriteText(Data, FilePath);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        private static void WriteText(string str, string FilePath)
        {
            string fileName = FilePath;

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            System.IO.File.AppendAllText(fileName, str, Encoding.UTF8);
        }

        public static void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            Console.WriteLine(e.Data);

        }

        public static void p_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里得到的是错误信息
            Console.WriteLine(e.Data);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (F_AQL_ConfirmShoes_Store_Add c = new F_AQL_ConfirmShoes_Store_Add(MODULE_TYPE))
            {
                c.ShowDialog();
            }
            LoadPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void dataGridViewEx1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Lend" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["Lend"]).Enabled)//借出
                {
                    string aid = dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value.ToString();
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("STOCK_CODE", dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString());
                    dic.Add("STOCK_NAME", dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString());
                    dic.Add("WAREHOUSE_CODE", dataGridViewEx1.Rows[e.RowIndex].Cells["WAREHOUSE_CODE"].Value.ToString());
                    dic.Add("WAREHOUSE_NAME", dataGridViewEx1.Rows[e.RowIndex].Cells["WAREHOUSE_NAME"].Value.ToString());
                    using (F_AQL_ConfirmShoes_Store_jcgh c = new F_AQL_ConfirmShoes_Store_jcgh(aid, "2", dic, MODULE_TYPE))
                    {
                        c.Text = "lender";
                        c.ShowDialog();
                    }
                    LoadPage();
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Return" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["Return"]).Enabled)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("STOCK_CODE", dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString());
                    dic.Add("STOCK_NAME", dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString());
                    dic.Add("WAREHOUSE_CODE", dataGridViewEx1.Rows[e.RowIndex].Cells["WAREHOUSE_CODE"].Value.ToString());
                    dic.Add("WAREHOUSE_NAME", dataGridViewEx1.Rows[e.RowIndex].Cells["WAREHOUSE_NAME"].Value.ToString());
                    string aid = dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value.ToString();
                    using (F_AQL_ConfirmShoes_Store_jcgh c = new F_AQL_ConfirmShoes_Store_jcgh(aid, "0", dic, MODULE_TYPE))
                    {
                        c.Text = "return personnel";
                        c.ShowDialog();
                    }
                    LoadPage();
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "reconfirm" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["reconfirm"]).Enabled)
                {
                    string aid = dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value.ToString();
                    string ART = dataGridViewEx1.Rows[e.RowIndex].Cells["ART"].Value.ToString();
                    string STOCK_CODE = dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString();
                    using (F_AQL_ConfirmShoes_Store_qryxq c = new F_AQL_ConfirmShoes_Store_qryxq(aid, ART,STOCK_CODE,this, MODULE_TYPE,"0"))
                    {
                        c.ShowDialog();
                    }
                    LoadPage();
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "delete" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["delete"]).Enabled)
                {
                    var res = MessageBox.Show("Are you sure to delete", "hint", MessageBoxButtons.YesNo);
                    if(res== DialogResult.Yes)
                    {
                        string aid = dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value.ToString();
                        DeleteConfirmShoes_Store(aid);
                    }
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Out_of_Warehouse" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["Out_of_Warehouse"]).Enabled&&dataGridViewEx1.Rows[e.RowIndex].Cells["Out_of_Warehouse"].Value.ToString()== "Out_of_Warehouse")
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("STOCK_CODE", dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString());
                    dic.Add("STOCK_NAME", dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString());
                    dic.Add("WAREHOUSE_CODE", dataGridViewEx1.Rows[e.RowIndex].Cells["WAREHOUSE_CODE"].Value.ToString());
                    dic.Add("WAREHOUSE_NAME", dataGridViewEx1.Rows[e.RowIndex].Cells["WAREHOUSE_NAME"].Value.ToString());
                    string aid = dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value.ToString();
                    //DeleteConfirmShoes_Store_ck(aid);
                    string STOCK_CODE = dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString();
                    string ART = dataGridViewEx1.Rows[e.RowIndex].Cells["ART"].Value.ToString();
                    string crk = dataGridViewEx1.Rows[e.RowIndex].Cells["Out_of_Warehouse"].Value.ToString();//出库
                    using (F_AQL_ConfirmShoes_crk ff = new F_AQL_ConfirmShoes_crk(aid, MODULE_TYPE, STOCK_CODE, ART, crk,this, dic))
                    {
                        ff.ShowDialog();
                    }
                }
                else if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Out_of_Warehouse" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["Out_of_Warehouse"]).Enabled && dataGridViewEx1.Rows[e.RowIndex].Cells["Out_of_Warehouse"].Value.ToString() == "Storage")//出库//入库
                {
                    string aid = dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value.ToString();
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("STOCK_CODE", dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString());
                    dic.Add("STOCK_NAME", dataGridViewEx1.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString());
                    dic.Add("WAREHOUSE_CODE", dataGridViewEx1.Rows[e.RowIndex].Cells["WAREHOUSE_CODE"].Value.ToString());
                    dic.Add("WAREHOUSE_NAME", dataGridViewEx1.Rows[e.RowIndex].Cells["WAREHOUSE_NAME"].Value.ToString());
                    using (F_AQL_ConfirmShoes_Store_Storage f = new F_AQL_ConfirmShoes_Store_Storage(aid, MODULE_TYPE, dic))
                    {
                        f.ShowDialog();
                    }
                    LoadPage();
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Print_QR_Code" && ((DataGridViewDisableButtonCell)dataGridViewEx1.Rows[e.RowIndex].Cells["Print_QR_Code"]).Enabled)//打印二维码
                {
                    string aid = dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value.ToString();
                    qr_code_print(aid, MODULE_TYPE);
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Operation_Record")
                {
                    string aid = dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value.ToString();
                    //string frmName = $@"F_AQL_ConfirmShoes_Store_State_{dataGridViewEx1.Rows[e.RowIndex].Cells["任务编号"].Value}";
                    string frmName = $@"F_AQL_ConfirmShoes_Store_State_{dataGridViewEx1.Rows[e.RowIndex].Cells["aid"].Value}";
                    var findFrm = Application.OpenForms[frmName];
                    if (findFrm == null)
                    {
                        F_AQL_ConfirmShoes_Store_State c = new F_AQL_ConfirmShoes_Store_State(aid);
                        c.Name = frmName;
                        c.Show();
                    }
                    else
                    {
                        findFrm.Activate();
                    }
                }
                if (dataGridViewEx1.Columns[e.ColumnIndex].Name == "Received_Date") // 接收日期
                {
                    string aa = dataGridViewEx1.CurrentRow.Cells["Received_Date"].Value is null ? "" : dataGridViewEx1.CurrentRow.Cells["Received_Date"].Value.ToString();
                    string js = aa == "" ? DateTime.Now.ToString("yyyy-MM-dd") : aa;
                    dateTimePicker3.Text = js; //接收日期
                    dateTimePicker3.Value = Convert.ToDateTime(js); //接收日期
                    Rectangle R = dataGridViewEx1.GetCellDisplayRectangle(dataGridViewEx1.CurrentCell.ColumnIndex, dataGridViewEx1.CurrentCell.RowIndex, false); //获取单元格位置 
                    dateTimePicker3.SetBounds(R.X + dataGridViewEx1.Location.X, R.Y + dataGridViewEx1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                    dateTimePicker3.Visible = true;
                    dateTimePicker3.Focus();
                }
                else
                    dateTimePicker3.Visible = false;
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

        //批量报废
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewEx1.Rows.Count; i++)
            {
                if (dataGridViewEx1.Rows[i].Cells["xz"].Value!=null)
                {
                    if (dataGridViewEx1.Rows[i].Cells["xz"].Value.ToString() == "True")
                    {
                        if (dataGridViewEx1.Rows[i].Cells["state"].Value.ToString() == "out of warehouse" || dataGridViewEx1.Rows[i].Cells["state"].Value.ToString() == "lend")
                        {
                            MessageBox.Show("Out of stock or loan cannot be scrapped!");
                            return;
                        }
                    }
                }
            }

            using (F_AQL_ConfirmShoes_Store_plbf a = new F_AQL_ConfirmShoes_Store_plbf(GetDgvToTable(dataGridViewEx1)))
            {
                a.ShowDialog();
            }
            LoadPage();
        }

        //确认有效期
        private void button4_Click(object sender, EventArgs e)
        {
            using (F_AQL_ConfirmShoes_Store_qryxq a = new F_AQL_ConfirmShoes_Store_qryxq(GetDgvToTable(dataGridViewEx1),MODULE_TYPE,"1"))
            {
                a.ShowDialog();
            }
            LoadPage();
        }

        /// <summary>
        /// 编辑-确认鞋-存放管理-删除-aql
        /// </summary>
        public void DeleteConfirmShoes_Store(string aid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("aid", aid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "DeleteConfirmShoes_Store", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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

        /// <summary>
        /// 编辑-确认鞋-存放管理-出库-aql
        /// </summary>
        public void DeleteConfirmShoes_Store_ck(string aid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("aid", aid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "DeleteConfirmShoes_Store_ck", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Out_of_Stock_Successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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

        /// <summary>
        /// 查询-确认鞋-存放管理-主页-导出-aql
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public DataTable GetConfirmShoes_Store_Main_Excel()
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            //键值对传值
            p.Add("shoe_name", textBox1.Text.Trim());
            p.Add("prod_no", textBox2.Text.Trim());
            p.Add("stock_name", textBox4.Text.Trim());
            if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
            {
                p.Add("wh_dateS", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
            {
                p.Add("wh_dateE", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            }
            List<string> ref_standard = new List<string>();
            foreach (System.Data.DataRowView item in this.checkedListBox1.CheckedItems)
            {
                ref_standard.Add(item.Row["code"].ToString());
            }
            p.Add("ref_standard", ref_standard);
            p.Add("confirm_by", textBox3.Text.Trim());
            p.Add("MODULE_TYPE", MODULE_TYPE);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                        "GetConfirmShoes_Store_Main_Excel",//方法名
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
            return dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //视图数据显示
                DataTable dts = GetConfirmShoes_Store_Main_Excel();
                if (dts.Rows.Count < 1)
                {
                    MessageBox.Show("No data export yet, please check whether the operation is correct");//暂无数据导出，请检查是否操作正确
                    return;
                }
                dts.Columns.Add("DBFDATE");
                dts.Columns.Add("DDBFDATE");
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(dts.Rows[i]["confirmation_time"].ToString()))
                    {
                        //待报废提醒日期
                        string dbf = (Convert.ToDateTime(dts.Rows[i]["confirmation_time"].ToString()).AddYears(Convert.ToInt32(dts.Rows[i]["scrap_life"].ToString())).ToString());
                        dts.Rows[i]["DBFDATE"] = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                    }
                    else if (!string.IsNullOrWhiteSpace(dts.Rows[i]["received_time"].ToString()) &&
                        string.IsNullOrWhiteSpace(dts.Rows[i]["confirmation_time"].ToString()))
                    {
                        //待报废提醒日期
                        string dbf = (Convert.ToDateTime(dts.Rows[i]["received_time"].ToString()).AddYears(Convert.ToInt32(dts.Rows[i]["scrap_life"].ToString())).ToString());
                        dts.Rows[i]["DBFDATE"] = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                    }
                    else if (!string.IsNullOrWhiteSpace(dts.Rows[i]["wh_date"].ToString()) && string.IsNullOrWhiteSpace(dts.Rows[i]["received_time"].ToString()) &&
                        string.IsNullOrWhiteSpace(dts.Rows[i]["confirmation_time"].ToString()))
                    {
                        //待报废提醒日期
                        string dbf = (Convert.ToDateTime(dts.Rows[i]["wh_date"].ToString()).AddYears(Convert.ToInt32(dts.Rows[i]["scrap_life"].ToString())).ToString());
                        dts.Rows[i]["DBFDATE"] = Convert.ToDateTime(dbf).ToString("yyyy-MM-dd");
                    }

                    if (!string.IsNullOrWhiteSpace(dts.Rows[i]["DBFDATE"].ToString()))
                    {
                        //报废到期日期
                        string bf = Convert.ToDateTime(dts.Rows[i]["DBFDATE"].ToString()).AddDays(-Convert.ToInt32(dts.Rows[i]["reminder_duration"].ToString())).ToString();
                        dts.Rows[i]["DDBFDATE"] = Convert.ToDateTime(bf).ToString("yyyy-MM-dd");
                    }
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
                dts.Columns.Remove("scrap_life");
                dts.Columns.Remove("reminder_duration");
                dts.Columns.Remove("aid");
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("SHOE_NAME", "鞋型");
                Execldic.Add("PROD_NO", "ART代号");
                Execldic.Add("STOCK_NAME", "存放位置");
                Execldic.Add("CONFIRM_BY", "确认人");
                Execldic.Add("STATE", "状态");
                Execldic.Add("COUNT", "数量");
                Execldic.Add("UNIT", "单位");
                Execldic.Add("WH_DATE", "入库日期");
                Execldic.Add("RECEIVED_TIME", "接收日期");
                Execldic.Add("CONFIRMATION_TIME", "最近一次确认日期");
                Execldic.Add("DDBFDATE", "待报废提醒日期");
                Execldic.Add("DBFDATE", "报废到期日期");
                Execldic.Add("REDO_REASON", "重做原因");

                ExeclHelper.ExportToTrueExcel(dts, Execldic, "出货确认鞋");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 编辑-确认鞋-存放管理-更新接收日期-aql
        /// </summary>
        public void EditConfirmShoes_Store_jsrq(string aid, string received_time)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("aid", aid);
                data.Add("received_time", received_time);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "EditConfirmShoes_Store_jsrq", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Edited successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
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

        private void dateTimePicker3_CloseUp(object sender, EventArgs e)
        {
            dataGridViewEx1.CurrentCell.Value = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            string aid = dataGridViewEx1.CurrentRow.Cells["aid"].Value.ToString();
            if (!string.IsNullOrWhiteSpace(dataGridViewEx1.CurrentRow.Cells["接收日期"].Value.ToString()))
            {
                EditConfirmShoes_Store_jsrq(aid, dataGridViewEx1.CurrentRow.Cells["接收日期"].Value.ToString());
            }
            dateTimePicker3.Visible = false;
        }

        /// <summary>
        /// 查询-确认鞋-存放管理-主页-aql
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoes_Store_Print(string aid)
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("aid", aid);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                            "GetConfirmShoes_Store_Print",//方法名
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
                if (dt.Rows.Count > 0)
                {

                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Print = this.comboBox1.Text;
            Program.DefaultPrinter = Print;
            if (!SetDefaultPrinter(Program.DefaultPrinter))
            {
                MessageBox.Show("Setting up the printer failed");//设置打印机失败
            }
        }

        /// <summary>
        /// 新增重做
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            F_AQL_ConfirmShoes_remain f_AQL_ConfirmShoes_Remain = new F_AQL_ConfirmShoes_remain(this);

            f_AQL_ConfirmShoes_Remain.Show();
        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
