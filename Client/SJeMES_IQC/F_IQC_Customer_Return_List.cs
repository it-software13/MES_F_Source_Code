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
    public partial class F_IQC_Customer_Return_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_Customer_Return_List()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitDateTimePicker(starttime);
            InitDateTimePicker(endtime);
            dateTimeP_putin_date.Text = DateTime.Now.AddMonths(-5).ToString("yyyy-MM");
            dateTimeP_end_date.Text = DateTime.Now.ToString("yyyy-MM");
        }


        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = 25;//int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        public void GetDataMain(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

               
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("REGION", txt_region.Text);//
                data.Add("FACTORY_NO", txt_factory.Text);//
                data.Add("FACTORY_NAME", txt_factoryname.Text);//
                data.Add("SALESORGAN_NO", txt_SALESORGAN_NO.Text);//
                data.Add("SALESORGAN_NAME", txt_SALESORGAN_NAME.Text);//

                data.Add("ARTICLE", txt_ARTICLE.Text);//
                data.Add("SHOES_NAME", txt_SHOES_NAME.Text);//

                data.Add("MASTERCODE", txt_MASTERCODE.Text);//
                data.Add("MASTERNAME", txt_MASTERNAME.Text);//

                data.Add("SECONDCODE", txt_SECONDCODE.Text);//
                data.Add("SECONDNAME", txt_SECONDNAME.Text);//

                if (!string.IsNullOrWhiteSpace(starttime.Text))
                    data.Add("starttime", starttime.Value.ToString("yyyy-MM-dd"));

                if (!string.IsNullOrWhiteSpace(endtime.Text))
                    data.Add("endtime", endtime.Value.ToString("yyyy-MM-dd"));

                string putin_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM");
                }
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.dateTimeP_end_date.Value).ToString("yyyy-MM");
                }
                data.Add("putin_date", putin_date);
                data.Add("end_date", end_date);

                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Bad_Report",//类名
                                            "GetReturnDatalist",//方法名
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
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["REGION"].Value = dr["REGION"].ToString();
                        dgvr.Cells["FACTORY_NO"].Value = dr["FACTORY_NO"].ToString();
                        dgvr.Cells["FACTORY_NAME"].Value = dr["FACTORY_NAME"].ToString();

                        dgvr.Cells["SALESORGAN_NO"].Value = dr["SALESORGAN_NO"].ToString();
                        dgvr.Cells["SALESORGAN_NAME"].Value = dr["SALESORGAN_NAME"].ToString();

                        dgvr.Cells["ARTICLE"].Value = dr["ARTICLE"].ToString();
                        dgvr.Cells["SHOES_NAME"].Value = dr["SHOES_NAME"].ToString();

                        dgvr.Cells["PRODUCTION_DATE"].Value = dr["PRODUCTION_DATE"].ToString();

                        dgvr.Cells["MASTERCODE"].Value = dr["MASTERCODE"].ToString();
                        dgvr.Cells["MASTERNAME"].Value = dr["MASTERNAME"].ToString();

                        dgvr.Cells["SECONDCODE"].Value = dr["SECONDCODE"].ToString();
                        dgvr.Cells["SECONDNAME"].Value = dr["SECONDNAME"].ToString();

                        dgvr.Cells["FOB"].Value = dr["FOB"].ToString();
                        dgvr.Cells["QTY"].Value = dr["QTY"].ToString();
                        dgvr.Cells["MONEY"].Value = dr["MONEY"].ToString();
                        dgvr.Cells["PRICE"].Value = dr["PRICE"].ToString();
                        dgvr.Cells["RETURN_MONTH"].Value = dr["RETURN_MONTH"].ToString();

                       
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                //this.dataGridView1.Columns["QA文件管理"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
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
            dtp.CustomFormat = "MM-yyyy"; //null;
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

        private void F_IQC_Customer_Return_List_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetDataMain;
            //LoadPage();
            this.dataGridView1.ClearSelection();
            LoadPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //视图数据显示
                DataTable dts = GetCustomer_Return_Main_dc();
                //if (dts.Rows.Count < 1)
                //{
                //    MessageBox.Show("暂无数据导出，请检查是否操作正确");
                //    return;
                //}
                if(dts.Rows.Count <1)
                {
                    dts.Columns.Add("RETURN_MONTH", typeof(string));
                    dts.Columns.Add("REGION", typeof(string));
                    dts.Columns.Add("FACTORY_NO", typeof(string));
                    dts.Columns.Add("FACTORY_NAME", typeof(string));
                    dts.Columns.Add("SALESORGAN_NO", typeof(string));
                    dts.Columns.Add("SALESORGAN_NAME", typeof(string));  
                    dts.Columns.Add("ARTICLE", typeof(string));
                    dts.Columns.Add("SHOES_NAME", typeof(string));
                    dts.Columns.Add("PRODUCTION_DATE", typeof(string));
                    dts.Columns.Add("MASTERCODE", typeof(string));
                    dts.Columns.Add("SECONDCODE", typeof(string));

                    dts.Columns.Add("MASTERNAME", typeof(string));
                    dts.Columns.Add("SECONDNAME", typeof(string));
                    dts.Columns.Add("FOB", typeof(string));
                    dts.Columns.Add("QTY", typeof(string));
                    dts.Columns.Add("MONEY", typeof(string));
                    dts.Columns.Add("PRICE", typeof(string));

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


                //dts.Columns.Add("REGION");
                //dts.Columns.Add("FACTORY_NO");
                //dts.Columns.Add("FACTORY_NAME");

                //dts.Columns.Add("SALESORGAN_NO");
                //dts.Columns.Add("SALESORGAN_NAME");

                //dts.Columns.Add("ARTICLE");
                //dts.Columns.Add("SHOES_NAME");

                //dts.Columns.Add("PRODUCTION_DATE");
                //dts.Columns.Add("MASTERCODE");
                //dts.Columns.Add("MASTERNAME");

                //dts.Columns.Add("SECONDCODE");
                //dts.Columns.Add("SECONDNAME");

                //dts.Columns.Add("FOB");
                //dts.Columns.Add("QTY");
                //dts.Columns.Add("MONEY");
                //dts.Columns.Add("PRICE");
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("RETURN_MONTH", "Return_Month");
                Execldic.Add("REGION", "Area");
                Execldic.Add("FACTORY_NO", "Factory_Code");
                Execldic.Add("FACTORY_NAME", "Factory_Name");
                Execldic.Add("SALESORGAN_NO", "Sales_Org_Code");
                Execldic.Add("SALESORGAN_NAME", "Sales_Org_Name");

                Execldic.Add("ARTICLE", "ARTICLE");
                Execldic.Add("SHOES_NAME", "Shoe_Type_Name");

                Execldic.Add("PRODUCTION_DATE", "Production_Date(MM-yyyy)");
                Execldic.Add("MASTERCODE", "Main_Code");
                Execldic.Add("SECONDCODE", "Minor_Code");

                Execldic.Add("MASTERNAME", "Main_Code_Name");
                Execldic.Add("SECONDNAME", "Minor_Code_Name");

                Execldic.Add("FOB", "FOB");
                Execldic.Add("QTY", "Quantity");
                Execldic.Add("MONEY", "Amount");
                Execldic.Add("PRICE", "Additional_Fees");

                //ExeclHelper.ExportToTrueExcel(dts, Execldic, "客户退货导入模板");
                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Customer Returns Import Template");
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
        public DataTable GetCustomer_Return_Main_dc()
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("REGION", txt_region.Text);//
            data.Add("FACTORY_NO", txt_factory.Text);//
            data.Add("FACTORY_NAME", txt_factoryname.Text);//
            data.Add("SALESORGAN_NO", txt_SALESORGAN_NO.Text);//
            data.Add("SALESORGAN_NAME", txt_SALESORGAN_NAME.Text);//

            data.Add("ARTICLE", txt_ARTICLE.Text);//
            data.Add("SHOES_NAME", txt_SHOES_NAME.Text);//

            data.Add("MASTERCODE", txt_MASTERCODE.Text);//
            data.Add("MASTERNAME", txt_MASTERNAME.Text);//

            data.Add("SECONDCODE", txt_SECONDCODE.Text);//
            data.Add("SECONDNAME", txt_SECONDNAME.Text);//

            if (!string.IsNullOrWhiteSpace(starttime.Text))
                data.Add("starttime", starttime.Value.ToString("yyyy-MM-dd"));

            if (!string.IsNullOrWhiteSpace(endtime.Text))
                data.Add("endtime", endtime.Value.ToString("yyyy-MM-dd"));

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Bad_Report",//类名
                                            "GetReturnDatalist_dc",//方法名
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
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());





            return dt;
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Before importing, make sure that the FOB quantity, amount, quantity, and additional expenses are of numeric type, and the production date is of date type, otherwise the import will fail", "Operation prompt！！！", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                        // From this ----------
                        //if (dt.Columns.Count != 17)
                        //{
                        //    MessageBox.Show("Import template error, please refer to");
                        //    return;
                        //}
                        // the Above block i made comment------
                        //不能为空
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    if (dt.Rows[i]["投诉编号"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["投诉编号"].ToString()))
                        //    {
                        //        MessageBox.Show($@"投诉编号不能为空!第{i + 1}行!");
                        //        return;
                        //    }
                        //    if (dt.Rows[i]["不良数量"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["不良数量"].ToString()))
                        //    {
                        //        MessageBox.Show($@"不良数量不能为空!第{i + 1}行!");
                        //        return;
                        //    }
                        //    if (dt.Rows[i]["投诉金额"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["投诉金额"].ToString()))
                        //    {
                        //        MessageBox.Show($@"投诉金额不能为空!第{i + 1}行!");
                        //        return;
                        //    }
                        //    if (dt.Rows[i]["投诉PO号"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["投诉PO号"].ToString()))
                        //    {
                        //        MessageBox.Show($@"投诉PO号不能为空!第{i + 1}行!");
                        //        return;
                        //    }
                        //}

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
                                p.Add("import_type", 14);//客户投诉导入
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        //DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                        //if (cell.CurrentItem == null)
                        //    return;
                        //if (cell.CurrentItem.Equals("delete"))
                        //{
                            if (MessageBox.Show("confirm deletion? ", "This delete cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("ID", ID);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJeMES_IQC", "SJeMES_IQC.IQC_Bad_Report", "Main_Delete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Operation deleted successfully");
                                        LoadPage();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                }
                            }
                        //}

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
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
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
                DataTable dts = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dts.Rows.Count < 1)
                {
                    MessageBox.Show("No data export yet, please check whether the operation is correct");
                    return;
                }
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("RETURN_MONTH", "Return_Month");
                Execldic.Add("REGION", "Area");
                Execldic.Add("FACTORY_NO", "Factory_Code");
                Execldic.Add("FACTORY_NAME", "Factory_Name");
                Execldic.Add("SALESORGAN_NO", "Sales_Org_Code");
                Execldic.Add("SALESORGAN_NAME", "Sales_Org_Name");

                Execldic.Add("ARTICLE", "ARTICLE");
                Execldic.Add("SHOES_NAME", "Shoe_Type_Name");

                Execldic.Add("PRODUCTION_DATE", "Production_Date(MM-yyyy)");
                Execldic.Add("MASTERCODE", "Main_Code");
                Execldic.Add("MASTERNAME", "Main_Code_Name");

                Execldic.Add("SECONDCODE", "Minor_Code");
                Execldic.Add("SECONDNAME", "Minor_Code_Name");

                Execldic.Add("FOB", "FOB");
                Execldic.Add("QTY", "Quantity");
                Execldic.Add("MONEY", "Amount");
                Execldic.Add("PRICE", "Additional_Fees");

                List<string> list = new List<string>();
                string[] keyhread = { "ID" };
                for (int i = 0; i < keyhread.Length; i++)
                {
                    if (dts.Columns.Contains(keyhread[i]))
                    {
                        dts.Columns.Remove(keyhread[i]);
                    }
                }
                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Customer return data list");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public string GetDateListApi(int pageSize, int pageIndex)
        {
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("REGION", txt_region.Text);//
            data.Add("FACTORY_NO", txt_factory.Text);//
            data.Add("FACTORY_NAME", txt_factoryname.Text);//
            data.Add("SALESORGAN_NO", txt_SALESORGAN_NO.Text);//
            data.Add("SALESORGAN_NAME", txt_SALESORGAN_NAME.Text);//

            data.Add("ARTICLE", txt_ARTICLE.Text);//
            data.Add("SHOES_NAME", txt_SHOES_NAME.Text);//

            data.Add("MASTERCODE", txt_MASTERCODE.Text);//
            data.Add("MASTERNAME", txt_MASTERNAME.Text);//

            data.Add("SECONDCODE", txt_SECONDCODE.Text);//
            data.Add("SECONDNAME", txt_SECONDNAME.Text);//

            

            if (!string.IsNullOrWhiteSpace(starttime.Text))
                data.Add("starttime", starttime.Value.ToString("yyyy-MM-dd"));

            if (!string.IsNullOrWhiteSpace(endtime.Text))
                data.Add("endtime", endtime.Value.ToString("yyyy-MM-dd"));

            //if (!string.IsNullOrWhiteSpace(tui_starttime.Text))
            //    data.Add("tui_starttime", tui_starttime.Value.ToString("yyyy-MM-dd"));

            //if (!string.IsNullOrWhiteSpace(tui_endtime.Text))
            //    data.Add("tui_endtime", tui_starttime.Value.ToString("yyyy-MM-dd"));

            data.Add("pageSize", pageSize);
            data.Add("pageIndex", pageIndex);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.IQC_Bad_Report",//类名
                                        "GetReturnDatalist",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);


            return retdata;
        }
    }
}
