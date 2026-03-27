using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using SjeMES_QCM_Ex;
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
    public partial class F_IQC_VWarehouse_Main : MaterialForm
    {
        private string ORG_ID = "";
        private string WAREHOUSE_CODE = "";
        private DataTable DT_EXCEL = new DataTable();
        private readonly MaterialSkinManager materialSkinManager;
        public F_IQC_VWarehouse_Main()
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
        private void F_QCM_VWarehouse_Main_Load(object sender, EventArgs e)
        {

            //GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";

            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";
            pageControl1.BindPageEvent += GetDataList;




            //premika article btn hide
            btn_art.Visible = false;
        }
        private int keys = 0;
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string retdata = GetDateListApi(pageSize, pageIndex,keys);

                 ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                   
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                string num1 = dic["num1"].ToString();
                string num2 = dic["num2"].ToString();
                //premika--comment--2025/12/19
                //Task sk = new Task(() =>
                //{
                //    MessageBox.Show($"Checked number: {num1}, unchecked number{num2}");
                //});
                //if (keys == 1)
                //{
                //    sk.Start();
                //}
                //keys= 0;
                DT_EXCEL = dt;
                dataGridView1.Rows.Clear();
                //premika--start2025/12/19
                if (dt.Rows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        Task sk = new Task(() =>
                        {
                            MessageBox.Show($"Checked number: {num1}, unchecked number{num2}");
                        });
                        if (keys == 1)
                        {
                            sk.Start();
                        }
                        keys = 0;
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["RCPT_DATE"].Value = dr["RCPT_DATE"].ToString();
                            dgvr.Cells["SUPPLIERS_CODE"].Value = dr["SUPPLIERS_CODE"].ToString();
                            dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                            dgvr.Cells["SUPPLIERS_CODE2"].Value = dr["SUPPLIERS_CODE2"].ToString();
                            dgvr.Cells["SUPPLIERS_NAME2"].Value = dr["SUPPLIERS_NAME2"].ToString();
                            dgvr.Cells["CHK_NO"].Value = dr["CHK_NO"].ToString();
                            dgvr.Cells["ITEM_TYPE_NO"].Value = dr["ITEM_TYPE_NO"].ToString();
                            dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();
                            dgvr.Cells["ITEM_NAME"].Value = dr["NAME_T"].ToString();
                            dgvr.Cells["ORD_QTY"].Value = dr["ORD_QTY"].ToString();
                            dgvr.Cells["ORDER_NO"].Value = dr["ORDER_NO"].ToString();
                            dgvr.Cells["CREATEDATE"].Value = dr["CREATEDATE"].ToString();
                            dgvr.Cells["CHK_SEQ"].Value = dr["CHK_SEQ"].ToString();//序号
                            dgvr.Cells["TASK_NO"].Value = dr["TASK_NO"].ToString();//premika-28/11/2025
                            dgvr.Cells["CREATEBY"].Value = dr["CREATEBY"].ToString();
                            dgvr.Cells["CSQYZK"].Value = dr["SAMPLING_STATUS"].ToString();
                            dgvr.Cells["SYSCE_DATE"].Value = dr["SYSCE_DATE"].ToString();
                            //dgvr.Cells["ORDER_NO"].Value = dr["ORDER_NO"].ToString();
                            dgvr.Cells["IV_QTY"].Value = dr["IV_QTY"].ToString();
                            dgvr.Cells["PASS_QTY"].Value = dr["PASS_QTY"].ToString();
                            dgvr.Cells["SHOE_NO"].Value = dr["NAME_S2"].ToString();
                            dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                            dgvr.Cells["ORG_NAME"].Value = dr["ORG_NAME"].ToString();
                            dgvr.Cells["ORG_NO"].Value = dr["ORG_ID"].ToString();//工厂编号
                            dgvr.Cells["STOC_NO"].Value = dr["WAREHOUSE_NAME"].ToString();//仓库名称
                            dgvr.Cells["RCPT_QTY"].Value = dr["RCPT_QTY"].ToString();
                            dgvr.Cells["PART_NO"].Value = dr["PART_NO"].ToString();//部位
                            dgvr.Cells["TEST_RESULT"].Value = dr["TEST_RESULT"].ToString();
                            dgvr.Cells["YTS_QTYS"].Value = dr["YTS_QTY"].ToString(); //验退数
                            dgvr.Cells["BS"].Value = dr["BS"].ToString(); //补送
                            dgvr.Cells["SAMPLE_QTY"].Value = dr["SAMPLE_QTY"].ToString(); //抽样数量
                            dgvr.Cells["INSP_REPORT"].Value = dr["SDISDELETE"].ToString(); //抽样数量
                            dgvr.Cells["STAFF_NAME"].Value = dr["STAFF_NAME"].ToString(); //检验员名称
                            dgvr.Cells["SHDW"].Value = dr["SHDW"].ToString(); //收货单位

                            dgvr.Cells["BAD_QTY"].Value = dr["BAD_QTY"].ToString(); //不合格数
                            dgvr.Cells["SPC_MINING"].Value = dr["SPC_MINING"].ToString(); //特采数量
                            if (string.IsNullOrWhiteSpace(dr["CLOSING_STATUS"].ToString()))
                            {
                                dgvr.Cells["CLOSING_STATUS"].Value = "none";//无// I chenged
                            }
                            else if (dr["CLOSING_STATUS"].ToString() == "0")//不良处理状态
                            {
                                dgvr.Cells["CLOSING_STATUS"].Value = "Closed";//已结案
                                dgvr.Cells["CLOSING_STATUS"].Style.ForeColor = Color.Red;
                            }
                            else if (dr["CLOSING_STATUS"].ToString() == "1")
                            {
                                dgvr.Cells["CLOSING_STATUS"].Style.ForeColor = Color.Green;
                                dgvr.Cells["CLOSING_STATUS"].Value = "opencase";//未结案
                            }

                            if (dr["DETERMINE"].ToString() == "0")
                            {
                                ((DataGridViewDisableButtonCell)dgvr.Cells["operation_d"]).Enabled = false;//检验项判断合格 0
                                                                                                           //dgvr.Cells["DETERMINE"].Value = "合格";
                                dgvr.Cells["DETERMINE"].Value = "Qualified";
                            }
                            else if (dr["DETERMINE"].ToString() == "1")
                            {
                                //dgvr.Cells["DETERMINE"].Value = "不合格";
                                dgvr.Cells["DETERMINE"].Value = "Unqualified";
                            }
                            else if (dr["DETERMINE"].ToString() == "2")
                            {
                                ((DataGridViewDisableButtonCell)dgvr.Cells["operation_d"]).Enabled = false;
                            }
                            if (dr["SDISDELETE"].ToString() == "1")
                            {
                                ((DataGridViewDisableButtonCell)dgvr.Cells["operation_d"]).Enabled = false;
                            }
                            i++;
                        }
                        GenClass.AutoSizeColumn(dataGridView1, 5);
                    }
                    else
                    {
                        string lab_id = textBox1.Text.ToString();
                        DataTable dt1 = dt.Clone();
                        DataRow[] filteredRows = dt.Select(
                            $"TASK_NO = '{lab_id.Replace("'", "''")}'"
                        );

                        foreach (DataRow row in filteredRows)
                        {
                            dt1.ImportRow(row);
                        }
                        if (dt1.Rows.Count > 0)
                        {

                            Task sk = new Task(() =>
                            {
                                MessageBox.Show($"Checked number: {dt1.Rows.Count}");
                            });
                            if (keys == 1)
                            {
                                sk.Start();
                            }
                            keys = 0;

                            int i = 0;
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                dataGridView1.Rows.Add();
                                DataGridViewRow dgvr1 = dataGridView1.Rows[i];
                                dgvr1.Cells["RCPT_DATE"].Value = dr1["RCPT_DATE"].ToString();
                                dgvr1.Cells["SUPPLIERS_CODE"].Value = dr1["SUPPLIERS_CODE"].ToString();
                                dgvr1.Cells["SUPPLIERS_NAME"].Value = dr1["SUPPLIERS_NAME"].ToString();
                                dgvr1.Cells["SUPPLIERS_CODE2"].Value = dr1["SUPPLIERS_CODE2"].ToString();
                                dgvr1.Cells["SUPPLIERS_NAME2"].Value = dr1["SUPPLIERS_NAME2"].ToString();
                                dgvr1.Cells["CHK_NO"].Value = dr1["CHK_NO"].ToString();
                                dgvr1.Cells["ITEM_TYPE_NO"].Value = dr1["ITEM_TYPE_NO"].ToString();
                                dgvr1.Cells["ITEM_NO"].Value = dr1["ITEM_NO"].ToString();
                                dgvr1.Cells["ITEM_NAME"].Value = dr1["NAME_T"].ToString();
                                dgvr1.Cells["ORD_QTY"].Value = dr1["ORD_QTY"].ToString();
                                dgvr1.Cells["ORDER_NO"].Value = dr1["ORDER_NO"].ToString();
                                dgvr1.Cells["CREATEDATE"].Value = dr1["CREATEDATE"].ToString();
                                dgvr1.Cells["CHK_SEQ"].Value = dr1["CHK_SEQ"].ToString();
                                dgvr1.Cells["TASK_NO"].Value = dr1["TASK_NO"].ToString();
                                dgvr1.Cells["CREATEBY"].Value = dr1["CREATEBY"].ToString();
                                dgvr1.Cells["CSQYZK"].Value = dr1["SAMPLING_STATUS"].ToString();
                                dgvr1.Cells["SYSCE_DATE"].Value = dr1["SYSCE_DATE"].ToString();
                                dgvr1.Cells["IV_QTY"].Value = dr1["IV_QTY"].ToString();
                                dgvr1.Cells["PASS_QTY"].Value = dr1["PASS_QTY"].ToString();
                                dgvr1.Cells["SHOE_NO"].Value = dr1["NAME_S2"].ToString();
                                dgvr1.Cells["PROD_NO"].Value = dr1["PROD_NO"].ToString();
                                dgvr1.Cells["ORG_NAME"].Value = dr1["ORG_NAME"].ToString();
                                dgvr1.Cells["ORG_NO"].Value = dr1["ORG_ID"].ToString();
                                dgvr1.Cells["STOC_NO"].Value = dr1["WAREHOUSE_NAME"].ToString();
                                dgvr1.Cells["RCPT_QTY"].Value = dr1["RCPT_QTY"].ToString();
                                dgvr1.Cells["PART_NO"].Value = dr1["PART_NO"].ToString();
                                dgvr1.Cells["TEST_RESULT"].Value = dr1["TEST_RESULT"].ToString();
                                dgvr1.Cells["YTS_QTYS"].Value = dr1["YTS_QTY"].ToString(); 
                                dgvr1.Cells["BS"].Value = dr1["BS"].ToString(); //补送
                                dgvr1.Cells["SAMPLE_QTY"].Value = dr1["SAMPLE_QTY"].ToString(); 
                                dgvr1.Cells["INSP_REPORT"].Value = dr1["SDISDELETE"].ToString();
                                dgvr1.Cells["STAFF_NAME"].Value = dr1["STAFF_NAME"].ToString();
                                dgvr1.Cells["SHDW"].Value = dr1["SHDW"].ToString(); 
                                dgvr1.Cells["BAD_QTY"].Value = dr1["BAD_QTY"].ToString();
                                dgvr1.Cells["SPC_MINING"].Value = dr1["SPC_MINING"].ToString();
                                if (string.IsNullOrWhiteSpace(dr1["CLOSING_STATUS"].ToString()))
                                {
                                    dgvr1.Cells["CLOSING_STATUS"].Value = "none";
                                }
                                else if (dr1["CLOSING_STATUS"].ToString() == "0")
                                {
                                    dgvr1.Cells["CLOSING_STATUS"].Value = "Closed";
                                    dgvr1.Cells["CLOSING_STATUS"].Style.ForeColor = Color.Red;
                                }
                                else if (dr1["CLOSING_STATUS"].ToString() == "1")
                                {
                                    dgvr1.Cells["CLOSING_STATUS"].Style.ForeColor = Color.Green;
                                    dgvr1.Cells["CLOSING_STATUS"].Value = "opencase";
                                }

                                if (dr1["DETERMINE"].ToString() == "0")
                                {
                                    ((DataGridViewDisableButtonCell)dgvr1.Cells["operation_d"]).Enabled = false;                                                                        
                                    dgvr1.Cells["DETERMINE"].Value = "Qualified";
                                }
                                else if (dr1["DETERMINE"].ToString() == "1")
                                {
                                    dgvr1.Cells["DETERMINE"].Value = "Unqualified";
                                }
                                else if (dr1["DETERMINE"].ToString() == "2")
                                {
                                    ((DataGridViewDisableButtonCell)dgvr1.Cells["operation_d"]).Enabled = false;
                                }
                                if (dr1["SDISDELETE"].ToString() == "1")
                                {
                                    ((DataGridViewDisableButtonCell)dgvr1.Cells["operation_d"]).Enabled = false;
                                }
                                i++;
                            }
                            GenClass.AutoSizeColumn(dataGridView1, 5);
                        }
                        else
                        {
                            dataGridView1.Rows.Clear();
                        }
                     

                    }


                }
                //premika--end2025/12/19


                //premika--comment--2025/12/09
                //if (dt.Rows.Count > 0)
                //{
                //    int i = 0;
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        dataGridView1.Rows.Add();
                //        DataGridViewRow dgvr = dataGridView1.Rows[i];
                //        dgvr.Cells["RCPT_DATE"].Value = dr["RCPT_DATE"].ToString();
                //        dgvr.Cells["SUPPLIERS_CODE"].Value = dr["SUPPLIERS_CODE"].ToString();
                //        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                //        dgvr.Cells["SUPPLIERS_CODE2"].Value = dr["SUPPLIERS_CODE2"].ToString();
                //        dgvr.Cells["SUPPLIERS_NAME2"].Value = dr["SUPPLIERS_NAME2"].ToString();
                //        dgvr.Cells["CHK_NO"].Value = dr["CHK_NO"].ToString();
                //        dgvr.Cells["ITEM_TYPE_NO"].Value = dr["ITEM_TYPE_NO"].ToString();
                //        dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();
                //        dgvr.Cells["ITEM_NAME"].Value = dr["NAME_T"].ToString(); 
                //        dgvr.Cells["ORD_QTY"].Value = dr["ORD_QTY"].ToString();
                //        dgvr.Cells["ORDER_NO"].Value = dr["ORDER_NO"].ToString();
                //        dgvr.Cells["CREATEDATE"].Value = dr["CREATEDATE"].ToString();
                //        dgvr.Cells["CHK_SEQ"].Value = dr["CHK_SEQ"].ToString();//序号
                //        dgvr.Cells["TASK_NO"].Value = dr["TASK_NO"].ToString();//premika-28/11/2025
                //        dgvr.Cells["CREATEBY"].Value = dr["CREATEBY"].ToString();
                //        dgvr.Cells["CSQYZK"].Value = dr["SAMPLING_STATUS"].ToString();
                //        dgvr.Cells["SYSCE_DATE"].Value = dr["SYSCE_DATE"].ToString();
                //        //dgvr.Cells["ORDER_NO"].Value = dr["ORDER_NO"].ToString();
                //        dgvr.Cells["IV_QTY"].Value = dr["IV_QTY"].ToString();
                //        dgvr.Cells["PASS_QTY"].Value = dr["PASS_QTY"].ToString();
                //        dgvr.Cells["SHOE_NO"].Value = dr["NAME_S2"].ToString();
                //        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                //        dgvr.Cells["ORG_NAME"].Value = dr["ORG_NAME"].ToString();
                //        dgvr.Cells["ORG_NO"].Value = dr["ORG_ID"].ToString();//工厂编号
                //        dgvr.Cells["STOC_NO"].Value = dr["WAREHOUSE_NAME"].ToString();//仓库名称
                //        dgvr.Cells["RCPT_QTY"].Value = dr["RCPT_QTY"].ToString(); 
                //        dgvr.Cells["PART_NO"].Value = dr["PART_NO"].ToString();//部位
                //        dgvr.Cells["TEST_RESULT"].Value = dr["TEST_RESULT"].ToString(); 
                //        dgvr.Cells["YTS_QTYS"].Value = dr["YTS_QTY"].ToString(); //验退数
                //        dgvr.Cells["BS"].Value = dr["BS"].ToString(); //补送
                //        dgvr.Cells["SAMPLE_QTY"].Value = dr["SAMPLE_QTY"].ToString(); //抽样数量
                //        dgvr.Cells["INSP_REPORT"].Value = dr["SDISDELETE"].ToString(); //抽样数量
                //        dgvr.Cells["STAFF_NAME"].Value = dr["STAFF_NAME"].ToString(); //检验员名称
                //        dgvr.Cells["SHDW"].Value = dr["SHDW"].ToString(); //收货单位

                //        dgvr.Cells["BAD_QTY"].Value = dr["BAD_QTY"].ToString(); //不合格数
                //        dgvr.Cells["SPC_MINING"].Value = dr["SPC_MINING"].ToString(); //特采数量
                //        if (string.IsNullOrWhiteSpace(dr["CLOSING_STATUS"].ToString()))
                //        {
                //            dgvr.Cells["CLOSING_STATUS"].Value = "none";//无// I chenged
                //        }
                //        else  if (dr["CLOSING_STATUS"].ToString() == "0")//不良处理状态
                //        {
                //            dgvr.Cells["CLOSING_STATUS"].Value = "Closed";//已结案
                //            dgvr.Cells["CLOSING_STATUS"].Style.ForeColor = Color.Red;
                //        }
                //        else if(dr["CLOSING_STATUS"].ToString() == "1")
                //        {
                //            dgvr.Cells["CLOSING_STATUS"].Style.ForeColor = Color.Green;
                //            dgvr.Cells["CLOSING_STATUS"].Value = "opencase";//未结案
                //        }

                //        if (dr["DETERMINE"].ToString() == "0")
                //        {
                //            ((DataGridViewDisableButtonCell)dgvr.Cells["operation_d"]).Enabled = false;//检验项判断合格 0
                //            //dgvr.Cells["DETERMINE"].Value = "合格";
                //            dgvr.Cells["DETERMINE"].Value = "Qualified";
                //        }
                //        else if (dr["DETERMINE"].ToString() == "1")
                //        {
                //            //dgvr.Cells["DETERMINE"].Value = "不合格";
                //            dgvr.Cells["DETERMINE"].Value = "Unqualified";
                //        }
                //        else if (dr["DETERMINE"].ToString()=="2")
                //        {
                //            ((DataGridViewDisableButtonCell)dgvr.Cells["operation_d"]).Enabled = false;
                //        }
                //        if (dr["SDISDELETE"].ToString() == "1")
                //        {
                //            ((DataGridViewDisableButtonCell)dgvr.Cells["operation_d"]).Enabled = false;
                //        }
                //        i++;
                //    }
                //   GenClass.AutoSizeColumn(dataGridView1,5);

                //}
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();
               
                //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public string GetDateListApi(int pageSize, int pageIndex,int keys)
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
            if (string.IsNullOrWhiteSpace(txt_STOC_NO.Text))
            {
                WAREHOUSE_CODE = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(putin_date) ||
                string.IsNullOrWhiteSpace(end_date) ||
                string.IsNullOrWhiteSpace(txt_ORG_ID.Text))
            {
                throw new Exception("Please fill in the necessary conditions and then execute the query, prompt: receiving time, factory！");
            }
            //premika--start2025/12/19
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (string.IsNullOrWhiteSpace(txt_bianma.Text))
                {
                    txt_bianma.Focus();
                    throw new Exception("Please Enter Material Coding!");
                }
            }
            //premika--end

            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("putin_date", putin_date);//收料日期 
            p.Add("end_date", end_date);//收料日期 
            p.Add("pageSize", pageSize);
            p.Add("pageIndex", pageIndex);
            p.Add("CHK_NO", txt_CHK_NO.Text);//收料单号
            p.Add("PURCHASE_NO", tb_purchase.Text);//采购单号
            p.Add("jieguo", cbo_csjg.Text);//测试结果
            p.Add("quyang", cbo_NY.Text);//取样状况
            p.Add("VEND_NO", txt_VEND_NO.Text);//采购厂商
            p.Add("VEND_NO2", txt_VEND_NO2.Text);//生产厂商
            p.Add("bianma", txt_bianma.Text);//物料编码
            p.Add("STOC_NO", WAREHOUSE_CODE);//仓别
            p.Add("wgjieguo", cbo_wgjy.Text);//外观结果
            p.Add("ORG_ID", ORG_ID);//工厂
            p.Add("DETERMINE", cbo_status.Text);//状态
            p.Add("keys", keys);//记录是否为搜索查询（）
            p.Add("Lab", textBox1.Text);//premika-2025/11/28
           // p.Add("Artno", textBox3.Text);//premika-2025/12/22
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.VMaterialinventory",//类名
                                        "CheckResultMain2",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            return retdata;
        }


        private void btn_Select_Click(object sender, EventArgs e)
        {
            keys = 1;
            FormLoad();
        }
        public void FormLoad()
        { 
            pageControl1.PageSize = 29;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        /// <summary>
        /// T2厂商上传主页查询文件
        /// </summary>
        /// <param name="fjguid"></param>
        /// <returns></returns>
        public DataTable GetVendor_Report_Main_ListFile(string vend_no, string order_no, string item_no)
        {
            DataTable dt = new DataTable();
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("vend_no", vend_no);//生产厂商
                data.Add("order_no", order_no);//采购单号
                data.Add("item_no", item_no);//料号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "VMaterialinventory",//类名
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
        //已完成
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
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                     
                    switch (dataGridView1.Columns[e.ColumnIndex].Name)
                    {
                        case "Column1":  
                            dic.Add("SUPPLIERS_NAME", dataGridView1.CurrentRow.Cells["SUPPLIERS_NAME"].Value.ToString());//生产厂商 
                            dic.Add("ITEM_NAME", dataGridView1.CurrentRow.Cells["ITEM_NAME"].Value.ToString());//材料品名 
                            dic.Add("ITEM_NO", dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString());//料号  
                            using (F_IQC_Previousinspectionresults_view aa = new F_IQC_Previousinspectionresults_view(dic))
                            {
                                //查看检验结果
                                aa.ShowDialog();
                                FormLoad();
                            } 
                            break;
                        case "final_report":
                            dic.Add("RCPT_DATE", dataGridView1.CurrentRow.Cells["RCPT_DATE"].Value.ToString());//进仓日期
                            dic.Add("SUPPLIERS_NAME", dataGridView1.CurrentRow.Cells["SUPPLIERS_NAME"].Value.ToString());//生产厂商
                            dic.Add("RCPT_QTY", dataGridView1.CurrentRow.Cells["RCPT_QTY"].Value.ToString());//收料数量
                            dic.Add("SHOE_NO", dataGridView1.CurrentRow.Cells["SHOE_NO"].Value.ToString());//鞋型
                            dic.Add("PROD_NO", dataGridView1.CurrentRow.Cells["PROD_NO"].Value.ToString());//ART
                            dic.Add("ITEM_NAME", dataGridView1.CurrentRow.Cells["ITEM_NAME"].Value.ToString());//材料品名
                            dic.Add("ITEM_TYPE_NO", dataGridView1.CurrentRow.Cells["ITEM_TYPE_NO"].Value.ToString());//材料类型
                            dic.Add("ORDER_NO", dataGridView1.CurrentRow.Cells["ORDER_NO"].Value.ToString());//采购单号
                            dic.Add("CHK_NO", dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString());//收料单号
                            dic.Add("ITEM_NO", dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString());//料号
                            dic.Add("CHK_SEQ", dataGridView1.CurrentRow.Cells["CHK_SEQ"].Value.ToString());//材料序号
                            dic.Add("PART", dataGridView1.CurrentRow.Cells["PART_NO"].Value.ToString());//部位ITEM_NAME
                            dic.Add("TASK_NO", dataGridView1.CurrentRow.Cells["TASK_NO"].Value.ToString());//lab ID premika--2025/12/05

                            using (F_IQC_Material_FinalReport aa = new F_IQC_Material_FinalReport(dic, Program.Client))
                            {
                                //查看检验结果
                                aa.ShowDialog();
                                FormLoad();
                            }
                            break;
                        case "operation_f": 
                            string vend_no = dataGridView1.Rows[e.RowIndex].Cells["SUPPLIERS_NAME"].Value.ToString();
                            string order_no = dataGridView1.Rows[e.RowIndex].Cells["ORDER_NO"].Value.ToString();
                            string item_no1 = dataGridView1.Rows[e.RowIndex].Cells["ITEM_NO"].Value.ToString();

                            var currRowFileDt = GetVendor_Report_Main_ListFile(vend_no, order_no, item_no1);
                            //打开T2测试报告
                            FrmFileList add1 = new FrmFileList(currRowFileDt, Program.Client.UploadUrl, Program.Client.UserToken, "", true, true);
                            add1.ShowDialog();

                            break;
                        case "operation_a":
                            dic.Add("CHK_NO", dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString());//收料单号
                            dic.Add("ITEM_NO", dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString());//料号
                            dic.Add("CHK_SEQ", dataGridView1.CurrentRow.Cells["CHK_SEQ"].Value.ToString());//材料序号
                            dic.Add("SUPPLIERS_NAME", dataGridView1.CurrentRow.Cells["SUPPLIERS_NAME"].Value.ToString());//生产厂商
                            dic.Add("SUPPLIERS_NAME2", dataGridView1.CurrentRow.Cells["SUPPLIERS_NAME2"].Value.ToString());//采购厂商
                            dic.Add("RCPT_QTY", dataGridView1.CurrentRow.Cells["RCPT_QTY"].Value.ToString());//收料数量
                            dic.Add("SHOE_NO", dataGridView1.CurrentRow.Cells["SHOE_NO"].Value.ToString());//鞋型
                            dic.Add("PROD_NO", dataGridView1.CurrentRow.Cells["PROD_NO"].Value.ToString());//ART
                            dic.Add("ORDER_NO", dataGridView1.CurrentRow.Cells["ORDER_NO"].Value.ToString());//采购单号
                            dic.Add("ITEM_NAME", dataGridView1.CurrentRow.Cells["ITEM_NAME"].Value.ToString());//材料品名
                            dic.Add("RCPT_DATE", dataGridView1.CurrentRow.Cells["RCPT_DATE"].Value.ToString());//进仓日期
                            dic.Add("IV_QTY", dataGridView1.CurrentRow.Cells["IV_QTY"].Value.ToString());//检验数量
                            dic.Add("PASS_QTY", dataGridView1.CurrentRow.Cells["PASS_QTY"].Value.ToString());//合格数量
                            dic.Add("ITEM_TYPE_NO", dataGridView1.CurrentRow.Cells["ITEM_TYPE_NO"].Value.ToString());//物料类型代号
                            dic.Add("PART_NO", dataGridView1.CurrentRow.Cells["PART_NO"].Value.ToString());//部位ITEM_NAME
                            using (F_IQC_VMaterialresults_Add aa = new F_IQC_VMaterialresults_Add(dic))
                            {
                                aa.ShowDialog();
                                FormLoad();
                            }

                            break;
                        case "operation_b":
                            dic.Add("RCPT_DATE", dataGridView1.CurrentRow.Cells["RCPT_DATE"].Value.ToString());//进仓日期
                            dic.Add("SUPPLIERS_NAME", dataGridView1.CurrentRow.Cells["SUPPLIERS_NAME"].Value.ToString());//生产厂商
                            dic.Add("RCPT_QTY", dataGridView1.CurrentRow.Cells["RCPT_QTY"].Value.ToString());//收料数量
                            dic.Add("SHOE_NO", dataGridView1.CurrentRow.Cells["SHOE_NO"].Value.ToString());//鞋型
                            dic.Add("PROD_NO", dataGridView1.CurrentRow.Cells["PROD_NO"].Value.ToString());//ART
                            dic.Add("ITEM_NAME", dataGridView1.CurrentRow.Cells["ITEM_NAME"].Value.ToString());//材料品名
                            dic.Add("ITEM_TYPE_NO", dataGridView1.CurrentRow.Cells["ITEM_TYPE_NO"].Value.ToString());//材料类型
                            dic.Add("ORDER_NO", dataGridView1.CurrentRow.Cells["ORDER_NO"].Value.ToString());//采购单号
                            dic.Add("CHK_NO", dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString());//收料单号
                            dic.Add("ITEM_NO", dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString());//料号
                            dic.Add("CHK_SEQ", dataGridView1.CurrentRow.Cells["CHK_SEQ"].Value.ToString());//材料序号
                            dic.Add("PART", dataGridView1.CurrentRow.Cells["PART_NO"].Value.ToString());//部位ITEM_NAME

                            using (F_IQC_Viewinspectionresults_view aa = new F_IQC_Viewinspectionresults_view(dic))
                            {
                                //查看检验结果
                                aa.ShowDialog();
                                FormLoad();
                            }
                            break;
                        case "operation_c":
                            //实验室
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("item_no", dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString());//料号
                            p.Add("rcpt_date", dataGridView1.CurrentRow.Cells["RCPT_DATE"].Value.ToString());//收料日期
                            p.Add("chk_no", dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString());//收料日期
                            p.Add("task_no", dataGridView1.CurrentRow.Cells["TASK_NO"].Value.ToString());//premika-2025/12/05
                          
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJeMES_IQC",//类库名
                                                        "SJeMES_IQC.VMaterialinventory",//类名
                                                        "CheckResultMainDmp_Chk_nolist",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (!ret.IsSuccess)
                            {
                                throw new Exception(ret.ErrMsg);
                            }
                            Dictionary<string, object> dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                            //视图数据显示
                            string task_no = dic2["task_no"].ToString();
                            if (!string.IsNullOrWhiteSpace(task_no))
                            {
                                using (F_QCM_Ex_LookResult_New aa = new F_QCM_Ex_LookResult_New(task_no, Program.Client))
                                {
                                    //实验室结果(测检报告)
                                    aa.ShowDialog();
                                    FormLoad();
                                }
                            }
                            else
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("No lab assignment number, please check！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            }
                            break;
                        case "operation_d":
                             string ITEM_TYPE_NO = dataGridView1.CurrentRow.Cells["ITEM_TYPE_NO"].Value.ToString();
                            dic.Add("CHK_NO", dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString());//收料单号
                            dic.Add("ITEM_NO", dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString());//料号
                            dic.Add("cysl", dataGridView1.CurrentRow.Cells["SAMPLE_QTY"].Value.ToString());//抽样数量
                            dic.Add("ORDER_NO", dataGridView1.CurrentRow.Cells["ORDER_NO"].Value.ToString());//采购单号
                            dic.Add("CHK_SEQ", dataGridView1.CurrentRow.Cells["CHK_SEQ"].Value.ToString());//材料序号 
                            bool tatus = ((DataGridViewDisableButtonCell)dataGridView1.CurrentRow.Cells["operation_d"]).Enabled;
                            //bool tatus = ((DataGridViewDisableButtonCell)dataGridView1.CurrentRow.Cells["operation_d"]).Enabled;

                            if (tatus)
                            {
                                if (!string.IsNullOrWhiteSpace(ITEM_TYPE_NO))
                                {
                                    string ITEM_TYPE_NOS = ITEM_TYPE_NO.ToString().Substring(0, 3);
                                    if (ITEM_TYPE_NOS.Contains("401"))
                                    {

                                        using (F_IQC_Bad_Report_Leather frm = new F_IQC_Bad_Report_Leather(dic, "0"))//皮料
                                        {
                                            //frm.Text = "皮料不良报告";//Bad leather report
                                            frm.Text = "Bad leather report";//Bad leather report
                                            frm.ShowDialog();
                                            FormLoad();
                                        }
                                    }
                                    else
                                    {
                                        //using (F_IQC_Bad_Report_Leather frm = new F_IQC_Bad_Report_Leather(dic, "1"))//非皮料
                                        //{
                                        //    frm.Text = "非皮料不良报告";//Non-leather bad report
                                        //    frm.Text = "Non-leather bad report";//Non-leather bad report
                                        //    frm.ShowDialog();
                                        //    FormLoad();
                                        //}
                                        using (F_IQC_Bad_Report_NoLeather frm = new F_IQC_Bad_Report_NoLeather(dic, "1"))//非皮料
                                        {
                                            //frm.Text = "非皮料不良报告";//Non-leather bad report
                                            frm.Text = "Non-leather bad report";//Non-leather bad report
                                            frm.ShowDialog();
                                            FormLoad();
                                        }
                                    }
                                }
                                else
                                {
                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("This material has no material type, please check！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                }
                            }
                           
                            break;
                        case "operation_e":
                            string chk_no = dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString();//收料单号
                            string item_no = dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString();//料号
                            string chk_seq = dataGridView1.CurrentRow.Cells["CHK_SEQ"].Value.ToString();//料号序号
                            string rcpt_date = dataGridView1.CurrentRow.Cells["RCPT_DATE"].Value.ToString();//料号序号
                            string org_id = dataGridView1.CurrentRow.Cells["ORG_NO"].Value.ToString();//工厂编号
                            DataTable dt = print_lict(rcpt_date,chk_no, item_no, chk_seq, org_id);
                            if (dt.Rows.Count > 0)
                            {
                                string url= Application.StartupPath + "/newfrx/物料送测标签.frx";
                                List<DataTable> list_dt = new List<DataTable>();
                                List<DataSet> list_ds = new List<DataSet>();
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        DataTable dd = dt.Clone();
                                        DataRow row = dt.Rows[i];
                                        dd.ImportRow(row);
                                        list_dt.Add(dd);
                                    }
                                    for (int i = 0; i < list_dt.Count; i++)
                                    {
                                        list_dt[i].TableName = "Table";
                                        DataSet dsa = new DataSet();
                                        dsa.Tables.Add(list_dt[i].Copy());
                                        list_ds.Add(dsa);
                                    }
                                    using (FrmSelectPrint add = new FrmSelectPrint(url, list_ds))
                                    {
                                        add.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg($"Item number: {item_no} and receipt number: {chk_no} No data found, please check", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
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
        public DataTable print_lict(string rcpt_date, string chk_no, string item_no,string chk_seq,string org_id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("chk_no", chk_no);//收料单号
            p.Add("item_no", item_no);//料号
            p.Add("chk_seq", chk_seq);//料号序号
            p.Add("rcpt_date", rcpt_date);//收料日期
            p.Add("org_id", org_id);//工厂编号
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.VMaterialinventory",//类名
                                        "CheckResultMainDmp_PrintXC2",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            return dt;
        }

        private void btn_input_Click(object sender, EventArgs e)
        {
            using (F_IQC_VWarehouse_MJ aa=new F_IQC_VWarehouse_MJ())
            {
                aa.ShowDialog();
            }
        }

        private void btn_Outexcel_Click(object sender, EventArgs e)
        {
            try
            {
                string retdata = GetDateListApi(10000, 1,0);

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
                /* if (DT_EXCEL.Rows.Count < 1)
                 {
                     MessageBox.Show("数据为空，先搜索再做导出操作");
                     return;
                 }*/
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("RCPT_DATE", "收货日期");
                Execldic.Add("SUPPLIERS_CODE", "生产厂商编号");
                Execldic.Add("SUPPLIERS_NAME", "生产厂商");
                Execldic.Add("SUPPLIERS_CODE2", "采购厂商编号");
                Execldic.Add("SUPPLIERS_NAME2", "采购厂商");
                Execldic.Add("CHK_NO", "收料单号");
                Execldic.Add("ITEM_NO", "ITEM_NO");
                Execldic.Add("NAME_T", "材料名称");
                Execldic.Add("SHDW", "收货单位");
                Execldic.Add("ORD_QTY", "采购量");
                Execldic.Add("ORDER_NO", "采购单号");
                Execldic.Add("CREATEDATE", "外观检验日期");
                Execldic.Add("TASK_NO", "TASK_NO");
                Execldic.Add("DETERMINE", "外观检验结果");
                Execldic.Add("SAMPLING_STATUS", "测试取样状况");
                Execldic.Add("SYSCE_DATE", "实验室测试日期"); 
                Execldic.Add("TEST_RESULT", "测试结果"); 
                Execldic.Add("STAFF_NAME", "检验员");
                Execldic.Add("IV_QTY", "检验数");
                Execldic.Add("PASS_QTY", "合格数");
                Execldic.Add("BAD_QTY", "不合格数");
                Execldic.Add("SPC_MINING", "特采数");
                Execldic.Add("YTS_QTY", "实退数");
                Execldic.Add("BS", "补送");
                Execldic.Add("NAME_S2", "鞋型");
                Execldic.Add("PROD_NO", "ART");
                Execldic.Add("ORG_NAME", "工厂");
                Execldic.Add("WAREHOUSE_NAME", "仓别");
                Execldic.Add("CLOSING_STATUS", "不良处理状态");

                List<string> list = new List<string>();
                string[] keyhread = { "DATAS", "SOURCE_SEQ", "CHK_SEQ", "PART_NO", "SHOE_NO", "RCPT_QTY", "ITEM_TYPE_NO", "SAMPLE_QTY", "CREATEBY", "CORS", "CREATEBY", "SDISDELETE", "EX_ID", "INSERT_DATE", "STOC_NO", "ISDELETE" };
                for (int i = 0; i < keyhread.Length; i++)
                {
                    if (dts.Columns.Contains(keyhread[i]))
                    {
                        dts.Columns.Remove(keyhread[i]);
                    }
                }

                foreach (DataRow item in dts.Rows)
                {
                    if (item["CLOSING_STATUS"].ToString() == "0")//不良处理状态
                    {
                        item["CLOSING_STATUS"] = "Closed";//已结案

                    }
                    else if (item["CLOSING_STATUS"].ToString() == "1")
                    {
                        item["CLOSING_STATUS"] = "opencase";//未结案
                    }
                    if (string.IsNullOrWhiteSpace(item["CLOSING_STATUS"].ToString()))
                    {
                        item["CLOSING_STATUS"] = "none";////无
                    }

                    if (item["DETERMINE"].ToString() == "0")
                    {
                        //item["DETERMINE"] = "合格";
                        item["DETERMINE"] = "Qualified";
                    }

                    else if (item["DETERMINE"].ToString() == "1")
                    {

                       // item["DETERMINE"] = "不合格";
                        item["DETERMINE"] = "Unqualified";
                    }
                    else if (item["DETERMINE"].ToString() == "2")
                    {
                        item["DETERMINE"] = "";
                    }
                }
                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Warehousing inspection list list");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
         
        }

        public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
        {
            public DataGridViewDisableButtonColumn()
            {
                this.CellTemplate = new DataGridViewDisableButtonCell();
            }
        }

        public class DataGridViewDisableButtonCell : DataGridViewButtonCell
        {
            private bool enabledValue;
            public bool Enabled
            {
                get
                {
                    return enabledValue;
                }
                set
                {
                    enabledValue = value;
                }
            }

            public override object Clone()
            {
                DataGridViewDisableButtonCell cell =
                (DataGridViewDisableButtonCell)base.Clone();
                cell.Enabled = this.Enabled;
                return cell;
            }

            public DataGridViewDisableButtonCell()
            {
                this.enabledValue = true;
            }

            protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
            {
                if (!this.enabledValue)
                {
                    if ((paintParts & DataGridViewPaintParts.Background) ==
                    DataGridViewPaintParts.Background)
                    {
                        SolidBrush cellBackground =
                        new SolidBrush(cellStyle.BackColor);
                        graphics.FillRectangle(cellBackground, cellBounds);
                        cellBackground.Dispose();
                    }

                    if ((paintParts & DataGridViewPaintParts.Border) ==
                    DataGridViewPaintParts.Border)
                    {
                        PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                        advancedBorderStyle);
                    }
                    Rectangle buttonArea = cellBounds;
                    Rectangle buttonAdjustment =
                    this.BorderWidths(advancedBorderStyle);
                    buttonArea.X += buttonAdjustment.X;
                    buttonArea.Y += buttonAdjustment.Y;
                    buttonArea.Height -= buttonAdjustment.Height;
                    buttonArea.Width -= buttonAdjustment.Width;
                    ButtonRenderer.DrawButton(graphics, buttonArea,
                    System.Windows.Forms.VisualStyles.PushButtonState.Disabled);

                    if (this.FormattedValue is String)
                    {
                        TextRenderer.DrawText(graphics,
                        (string)this.FormattedValue,
                        this.DataGridView.Font,
                        buttonArea, SystemColors.GrayText);
                    }
                }
                else
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
                }
            }
        }

        private void btn_jybg_Click(object sender, EventArgs e)
        {
            using (F_IQC_VWarehouseDmp_Print addPrint = new F_IQC_VWarehouseDmp_Print())
            {
                addPrint.ShowDialog();
            }
        }
        private void txt_ORG_ID_DoubleClick(object sender, EventArgs e)
        {
            string sql = $@"SELECT
    DISTINCT
	ORG_CODE Factory_Code,
	ORG_NAME Factory_Name
FROM
	BASE001M A LEFT JOIN MMS_WAREHOUSE_MANAGE B ON A.ORG_CODE=B.ORG_ID";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                //txt_ORG_ID.Text = frmData.RetData.Rows[0]["工厂名称"].ToString();
                txt_ORG_ID.Text = frmData.RetData.Rows[0]["Factory_Name"].ToString();
                ORG_ID= frmData.RetData.Rows[0]["Factory_Code"].ToString();
                //ORG_ID= frmData.RetData.Rows[0]["工厂代号"].ToString();
            }
        }

        private void txt_STOC_NO_DoubleClick(object sender, EventArgs e)
        {
            string sql = string.Empty;
            if (string.IsNullOrWhiteSpace(txt_ORG_ID.Text))
            {
                sql= $@"SELECT
    DISTINCT
	WAREHOUSE_CODE Warehouse_Code,
	WAREHOUSE_NAME Warehouse_Name
FROM
	MMS_WAREHOUSE_MANAGE
";
                FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                frmData.ShowDialog();
                if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                {
                   // txt_STOC_NO.Text = frmData.RetData.Rows[0]["仓库名称"].ToString();
                    txt_STOC_NO.Text = frmData.RetData.Rows[0]["Warehouse_Name"].ToString();
                    WAREHOUSE_CODE = frmData.RetData.Rows[0]["Warehouse_Code"].ToString();
                    //WAREHOUSE_CODE = frmData.RetData.Rows[0]["仓库代号"].ToString();
                }
            }
            else
            {
                sql= $@"SELECT
	DISTINCT
	WAREHOUSE_CODE Warehouse_Code,
	WAREHOUSE_NAME Warehouse_Name
FROM
	MMS_WAREHOUSE_MANAGE
    WHERE  ORG_ID = '{ORG_ID}'
";
                FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                frmData.ShowDialog();
                if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                {
                    txt_STOC_NO.Text = frmData.RetData.Rows[0]["Warehouse_Name"].ToString();
                    WAREHOUSE_CODE = frmData.RetData.Rows[0]["Warehouse_Code"].ToString();
                }
            }
            
        }

        private void btn_cal_sampling_status_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.VMaterialinventory",//类名
                                        "CalSamplingStatus",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "update completed");
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = comboBox3.Text;
            string qy = comboBox3.Text;
            string chk_no = dataGridView1.CurrentRow.Cells["CHK_NO"].Value.ToString();//收料单号
            string item_no = dataGridView1.CurrentRow.Cells["ITEM_NO"].Value.ToString();//料号
            string chk_seq = dataGridView1.CurrentRow.Cells["CHK_SEQ"].Value.ToString();//料号序号
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("qy", qy);
            p.Add("chk_no", chk_no);
            p.Add("item_no", item_no);
            p.Add("chk_seq", chk_seq);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.VMaterialinventory",//类名
                                        "CheckResultCSViewupdateqy",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "update completed");
            comboBox3.Visible = false;



        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "CSQYZK") // 检验结果 
                    {
                        Rectangle R = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false); //获取单元格位置 
                        comboBox3.SetBounds(R.X + dataGridView1.Location.X, R.Y + dataGridView1.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                        comboBox3.Visible = true;
                    }
                    else
                    {
                        comboBox3.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        //premika added article button--2025/12/31
        private void Btn_art_Click(object sender, EventArgs e)
        {
            using (Article_Search_Warehouse_Main aa = new Article_Search_Warehouse_Main())
            {
                aa.ShowDialog();
       
            }
        }
    }
}
