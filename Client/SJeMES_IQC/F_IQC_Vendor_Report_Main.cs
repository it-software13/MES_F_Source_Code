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
    public partial class F_IQC_Vendor_Report_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_Vendor_Report_Main()
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

        private class cs
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        private class A01
        {
            public string name { get; set; }
            public string value { get; set; }
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
        /// 新增画皮查询材料
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetVendor_Report_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //if (string.IsNullOrWhiteSpace(textBox4.Text))
                //{
                //    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the necessary conditions and then execute the query, prompt: material name！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                //    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                //    return;
                //}
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("SOURCE_NO", textBox1.Text);//条件 采购单号
                data.Add("CGSUPPLIERS_NAME", textBox2.Text);//条件 采购厂商
                data.Add("ITEM_NO", textBox3.Text);//条件 料号
                data.Add("ITEM_NAME", textBox4.Text);//条件 材料名称
                data.Add("SCSUPPLIERS_NAME", textBox6.Text);//条件 生产厂商
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    data.Add("RCPT_DATES", dateTimePicker1.Value.ToString("yyyy-MM-dd"));//条件 收料日期开始
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the necessary conditions and then execute the query, prompt: the receipt date starts！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    data.Add("RCPT_DATEE", dateTimePicker2.Value.ToString("yyyy-MM-dd"));//条件 收料日期结束
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the necessary conditions and then execute the query, prompt: the date of receipt ends！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                data.Add("cssc", comboBox1.SelectedValue.ToString());//条件 测试报告上传状态
                data.Add("bgsc", comboBox2.SelectedValue.ToString());//条件 A-01报告上传状态
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Vendor_Report",//类名
                                            "GetVendor_Report_Main",//方法名
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
                        dgvr.Cells["SCVEND_NO"].Value = dr["SCVEND_NO"].ToString();
                        dgvr.Cells["SCSUPPLIERS_NAME"].Value = dr["SCSUPPLIERS_NAME"].ToString();
                        dgvr.Cells["SOURCE_NO"].Value = dr["SOURCE_NO"].ToString();
                        dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();
                        dgvr.Cells["ITEM_NAME"].Value = dr["ITEM_NAME"].ToString();
                        dgvr.Cells["CGVEND_NO"].Value = dr["CGVEND_NO"].ToString();
                        dgvr.Cells["CGSUPPLIERS_NAME"].Value = dr["CGSUPPLIERS_NAME"].ToString();
                        dgvr.Cells["ORD_QTY"].Value = dr["ORD_QTY"].ToString();
                        dgvr.Cells["RCPT_DATE"].Value = dr["RCPT_DATE"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["T2SC"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["T2A01SC"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                this.dataGridView1.Columns["T2CK"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_IQC_Vendor_Report_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            List<cs> cc = new List<cs>();
            cc.Add(new cs { name = "", value = "" });
            cc.Add(new cs { name = "uploaded", value = "uploaded" });//已上传
            cc.Add(new cs { name = "Not uploaded", value = "Not uploaded" });//未上传
            comboBox1.DataSource = cc;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "value";

            List<A01> aa = new List<A01>();
            aa.Add(new A01 { name = "", value = "" });
            aa.Add(new A01 { name = "uploaded", value = "uploaded" });
            aa.Add(new A01 { name = "Not uploaded", value = "Not uploaded" });
            comboBox2.DataSource = aa;
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "value";

            pageControl1.BindPageEvent += GetVendor_Report_Main;
            //LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["T2SC"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView1.Columns["T2A01SC"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView1.Columns["T2CK"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage(); 
        }

        /// <summary>
        /// T2厂商上传主页上传操作T2测试报告
        /// </summary>
        public void Vendor_Report_Main_EditT2(string vend_no,string order_no,string item_no,string file_id)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("vend_no", vend_no);
                data.Add("order_no", order_no);
                data.Add("item_no", item_no);
                data.Add("report_type", "0");
                data.Add("file_id", file_id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJeMES_IQC", "SJeMES_IQC.IQC_Vendor_Report", "Vendor_Report_Main_EditT2", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
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

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "T2SC")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["T2SC"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("UPLOAD"))//上传
                    {
                        // string res = UpLoad("3", file_type);
                        string guid = Guid.NewGuid().ToString("N");
                        // 创建文件弹出选择窗口（包括文件名）对象
                        OpenFileDialog ofd = new OpenFileDialog();
                        //判断选择的路径
                        string path = string.Empty;
                        ofd.Title = "Please select a file";
                        ofd.Filter = "所有文件|*.*";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                            filePath = ofd.FileName;


                            UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.Client.UploadUrl, filePath, Program.Client.UserToken);
                            if (res.IsSuccess)
                            {
                                var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                string vend_no = dataGridView1.Rows[e.RowIndex].Cells["SCVEND_NO"].Value.ToString();
                                string order_no = dataGridView1.Rows[e.RowIndex].Cells["SOURCE_NO"].Value.ToString();
                                string item_no = dataGridView1.Rows[e.RowIndex].Cells["ITEM_NO"].Value.ToString();

                                Vendor_Report_Main_EditT2(vend_no, order_no, item_no,resultDIC["guid"].ToString());
                            }
                            else
                            {

                                MessageBox.Show("Failed to upload file！");
                            }
                        }
                    }
                }
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "T2A01SC")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["T2A01SC"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("UPLOAD"))//上传
                    {
                        string vend_no = dataGridView1.Rows[e.RowIndex].Cells["SCVEND_NO"].Value.ToString();
                        string order_no = dataGridView1.Rows[e.RowIndex].Cells["SOURCE_NO"].Value.ToString();
                        string item_no = dataGridView1.Rows[e.RowIndex].Cells["ITEM_NO"].Value.ToString();
                        using (F_IQC_Vendor_Report_A_01Upload f = new F_IQC_Vendor_Report_A_01Upload(vend_no, order_no, item_no))
                        {
                            f.ShowDialog();
                        }
                        LoadPage();
                    }
                }
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "T2CK")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["T2CK"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("SELECT"))//查看
                    {
                        string vend_no = dataGridView1.Rows[e.RowIndex].Cells["SCVEND_NO"].Value.ToString();
                        string order_no = dataGridView1.Rows[e.RowIndex].Cells["SOURCE_NO"].Value.ToString();
                        string item_no = dataGridView1.Rows[e.RowIndex].Cells["ITEM_NO"].Value.ToString();
                        var currRowFileDt = GetVendor_Report_Main_ListFile(vend_no, order_no, item_no);
                        FrmFileList add = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", true, true);
                        add.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// T2厂商上传主页查询文件
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public DataTable GetVendor_Report_Main_ListFile(string vend_no,string order_no,string item_no)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //Key-value pair passing value
                data.Add("vend_no", vend_no);//生产厂商
                data.Add("order_no", order_no);//采购单号
                data.Add("item_no", item_no);//料号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.IQC_Vendor_Report",//类名
                                            "GetVendor_Report_Main_ListFile",//方法名
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
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("net_file_url", typeof(string));
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["file_url"].ToString()))
                        {
                            try
                            {
                                dr["net_file_url"] = Program.Client.PicUrl + dr["file_url"].ToString();
                            }
                            catch
                            {

                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            return dt;
        }
    }
}
