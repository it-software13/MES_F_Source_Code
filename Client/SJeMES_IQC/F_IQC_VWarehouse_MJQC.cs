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

namespace SJeMES_IQC
{
    public partial class F_IQC_VWarehouse_MJQC : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string,object> dics;

        public F_IQC_VWarehouse_MJQC(Dictionary<string,object> dic)
        {
            InitializeComponent();
            dics=dic;
            InitDateTimePicker(dateTimeP_putin_date);
            InitDateTimePicker(dateTimeP_end_date);
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
     Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void F_IQC_VWarehouse_MJQC_Load(object sender, EventArgs e)
        {
            txt_sccs.Text = dics["SUPPLIERS_NAME"].ToString();//生产厂商
            txt_ck.Text = dics["WAREHOUSE_NAME"].ToString();//仓库
            txt_gc.Text = dics["ORG_NAME"].ToString();//工厂
            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";
            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";
           // GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pageControl1.BindPageEvent += GetDataList;
            //FormLoad();
            dataGridView1.ClearSelection();

            if (!string.IsNullOrEmpty(dics["putin_date"].ToString()))
            {
                dateTimeP_putin_date.Value = Convert.ToDateTime(dics["putin_date"].ToString());
            }
            if (!string.IsNullOrEmpty(dics["end_date"].ToString()))
            {
                dateTimeP_end_date.Value = Convert.ToDateTime(dics["end_date"].ToString());
            }

            cbo_jyzt.SelectedIndex = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

            //pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            FormLoad();
          
        }

        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
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
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                p.Add("VEND_NO", txt_cgcs.Text);//采购厂商
                p.Add("VEND_NO2", dics["SUPPLIERS_CODE"]);//生产厂商
                p.Add("STOC_NO", dics["STOC_NO"]);//仓库代号
                p.Add("ORG_ID", dics["ORG_ID"]);//工厂代号
                p.Add("putin_date",putin_date);
                p.Add("end_date",end_date);
                p.Add("jyzt", cbo_jyzt.SelectedIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.VMaterialinventory",//类名
                                            "CheckResultMJQCView2",//方法名
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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["JYZT"].Value = dr["JYZT"].ToString();
                        dgvr.Cells["RCPT_DATE"].Value = dr["RCPT_DATE"].ToString();
                        dgvr.Cells["SUPPLIERS_CODE"].Value = dr["SUPPLIERS_CODE"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                        dgvr.Cells["CHK_NO"].Value = dr["CHK_NO"].ToString();
                        dgvr.Cells["ITEM_NO"].Value = dr["ITEM_NO"].ToString();
                        dgvr.Cells["ITEM_NAME"].Value = dr["NAME_T"].ToString();
                        dgvr.Cells["SUPPLIERS_CODE2"].Value = dr["SUPPLIERS_CODE2"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME2"].Value = dr["SUPPLIERS_NAME2"].ToString();
                        dgvr.Cells["CREATEDATE"].Value = dr["INSPECTIONDATE"].ToString();//外观检验日期
                        dgvr.Cells["SHDW"].Value = dr["SHDW"].ToString();
                        dgvr.Cells["ORD_QTY"].Value = dr["ORD_QTY"].ToString();
                        dgvr.Cells["ORDER_NO"].Value = dr["ORDER_NO"].ToString();
                        if (dr["DETERMINE"].ToString() == "0")
                        {
                            dgvr.Cells["DETERMINE"].Value = "Qualified";
                        }
                       if(dr["DETERMINE"].ToString() == "1")
                        {
                            dgvr.Cells["DETERMINE"].Value = "UnQualified";
                        }
                        dgvr.Cells["CSQYZK"].Value = dr["SAMPLING_STATUS"].ToString();
                        dgvr.Cells["CSJG"].Value = dr["CSJG"].ToString();
                        dgvr.Cells["CREATEBY"].Value = dr["STAFF_NO"].ToString();//检验员编号
                        dgvr.Cells["STAFF_NAME"].Value = dr["STAFF_NAME"].ToString();//检验员名称

                        dgvr.Cells["IV_QTY"].Value = dr["IV_QTY"].ToString();
                        dgvr.Cells["PASS_QTY"].Value = dr["PASS_QTY"].ToString();
                        dgvr.Cells["YTS"].Value = dr["YTS"].ToString();
                        dgvr.Cells["BS"].Value = dr["BS"].ToString();
                        dgvr.Cells["STOC_NO"].Value = dr["STOC_NO"].ToString();//仓库代号
                        dgvr.Cells["WAREHOUSE_NAME"].Value = dr["WAREHOUSE_NAME"].ToString();//仓库名称
                        dgvr.Cells["CHK_SEQ"].Value = dr["CHK_SEQ"].ToString();
                        dgvr.Cells["ORG_ID"].Value = dr["ORG_ID"].ToString();//工厂代号
                        dgvr.Cells["ORG_NAME"].Value = dr["ORG_NAME"].ToString();//工厂名称
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);

                }
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
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    List<Dictionary<string, object>> chk = new List<Dictionary<string, object>>();//未检验的数据
                    List<Dictionary<string, object>> chk_yjy = new List<Dictionary<string, object>>();//已检验的数据
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                     
                        //检测项目
                        string cc = dgr.Cells["Column1"].EditedFormattedValue.ToString();
                        string curr_jyzt = dgr.Cells["JYZT"].Value.ToString();
                        if (cc=="True")
                        {
                            if(curr_jyzt== "Not_Tested")
                            {
                                dic.Add("CHK_NO", dgr.Cells["CHK_NO"].Value.ToString());
                                dic.Add("ITEM_NO", dgr.Cells["ITEM_NO"].Value.ToString());
                                dic.Add("CHK_SEQ", dgr.Cells["CHK_SEQ"].Value.ToString());
                                dic.Add("RCPT_QTY", dgr.Cells["IV_QTY"].Value.ToString());
                                chk.Add(dic);
                            }
                            else
                            {
                                dic.Add("CHK_NO", dgr.Cells["CHK_NO"].Value.ToString());
                                dic.Add("ITEM_NO", dgr.Cells["ITEM_NO"].Value.ToString());
                                dic.Add("CHK_SEQ", dgr.Cells["CHK_SEQ"].Value.ToString());
                                dic.Add("RCPT_QTY", dgr.Cells["IV_QTY"].Value.ToString());
                                chk_yjy.Add(dic);
                            }
                        }
                       
                    }
                    if (chk.Count > 0)
                    {
                        int totalCount = chk.Count;
                        int batchCount = 20;
                        int pgCount = 0;//进度数
                        int successCount = 0;
                        int failCount = 0;
                        string errMsg = "";
                        SjeMES_QCM_Ex.ProgressBar progressBar = new SjeMES_QCM_Ex.ProgressBar(0, totalCount);
                        progressBar.Show();
                        while (true)
                        {
                            int canTakeCount = 0;
                            if ((pgCount + batchCount) > totalCount)
                            {
                                canTakeCount = batchCount - ((pgCount + batchCount) - totalCount);
                            }
                            else
                                canTakeCount = batchCount;

                            var curr_chk = chk.Skip(pgCount).Take(canTakeCount).ToList();
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJeMES_IQC",//类库名
                                                    "SJeMES_IQC.VMaterialinventory",//类名
                                                    "CheckResultMJQUpdate",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(curr_chk));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                            pgCount += canTakeCount;
                            //启动进度条
                            System.Threading.Thread.Sleep(100);
                            progressBar.StartProgressBar(pgCount);

                            if (!ret.IsSuccess)
                            {
                                failCount += canTakeCount;
                                errMsg += ret.ErrMsg;
                            }
                            else
                            {
                                successCount += canTakeCount;
                            }
                            if (pgCount == totalCount)
                                break;
                        }
                        //string msg = SJeMES_Framework.Common.UIHelper.UImsg($@"操作免检成功！成功{successCount}条，失败{failCount}条，已检验数据跳过{chk_yjy.Count}条。", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg($@"The operation was exempted from inspection successfully! There are {successCount} items of success, {failCount} items of failure, and {chk_yjy.Count} items of verified data are skipped.", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        FormLoad();
                        progressBar.Close();
                    }
                    else
                    {
                        if (chk_yjy.Count > 0)
                        {
                            //string msg = SJeMES_Framework.Common.UIHelper.UImsg("勾选数据都为已检验数据，无需免检！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("The checked data is all inspected data and does not need to be exempted from inspection!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        }
                        else
                        {
                            //string msg = SJeMES_Framework.Common.UIHelper.UImsg("请先选择，再做免检确认！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select first and then confirm the exemption!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        }
                    }
                    
                }
                else
                {

                   // string msg = SJeMES_Framework.Common.UIHelper.UImsg("请先选择，再做免检确认！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select first and then confirm the exemption!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_gx_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    dgr.Cells["Column1"].Value = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                {
                    dgr.Cells["Column1"].Value = false;
                }
            }
        }
    }
}
