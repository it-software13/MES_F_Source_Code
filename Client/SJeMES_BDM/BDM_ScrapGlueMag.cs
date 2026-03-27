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

namespace SJeMES_BDM
{
    public partial class BDM_ScrapGlueMag : MaterialForm
    {
       
        private readonly MaterialSkinManager materialSkinManager;
        public BDM_ScrapGlueMag()
        {
            InitializeComponent();

            InitDateTimePicker(dtp_start);
            InitDateTimePicker(dtp_end);
            this.dtp_start.Format = DateTimePickerFormat.Custom;
            this.dtp_start.CustomFormat = "   ";
            this.dtp_end.Format = DateTimePickerFormat.Custom;
            this.dtp_end.CustomFormat = "   ";

            InitialComboData();

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
           Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public void LoadPage()
        {
            pageControl1.PageSize = 25;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
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

        private void BDM_ScrapGlueMag_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(uiDataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            pageControl1.BindPageEvent += GetMain_List;
            LoadPage();
        }

        private void InitialComboData()
        {
            #region 生产单位
            Dictionary<string, object> data_dp = new Dictionary<string, object>();
            //键值对传值
            data_dp.Add("is_cs", "");
            data_dp.Add("page", 1);
            data_dp.Add("pageRow", 99999999);
            string retdata_dp = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.SCRAP_GLUE",//类名
                                        "GetDEPARTMENT",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data_dp));

            ResultObject ret_dp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata_dp);
            if (ret_dp.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret_dp.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt.Columns.Add("value", typeof(string));
                    dt.Columns.Add("label", typeof(string));
                }
                DataRow dr = dt.NewRow();
                dr["value"] = "";
                dr["label"] = "All";//全部
                dt.Rows.InsertAt(dr, 0);
                cb_dw.DataSource = dt;
                cb_dw.DisplayMember = "label";
                cb_dw.ValueMember = "value";
            }
            #endregion


            #region 胶水报废原因
            Dictionary<string, object> data_reason = new Dictionary<string, object>();
            //键值对传值
            data_reason.Add("is_cs", "");
            data_reason.Add("page", 1);
            data_reason.Add("pageRow", 99999999);
            string retdata_reason = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.SCRAP_GLUE",//类名
                                        "GetScrapGlueReason",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data_reason));

            ResultObject ret_reason = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata_reason);
            if (ret_reason.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret_reason.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt.Columns.Add("value", typeof(string));
                    dt.Columns.Add("label", typeof(string));
                }
                DataRow dr = dt.NewRow();
                dr["value"] = "";
                dr["label"] = "All";//全部
                dt.Rows.InsertAt(dr, 0);
                cb_reason.DataSource = dt;
                cb_reason.DisplayMember = "label";
                cb_reason.ValueMember = "value";
            }
            #endregion

        }

        public string GetDateListApi(int pageSize, int pageIndex)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            // 日期
            if (!string.IsNullOrWhiteSpace(dtp_start.Text.ToString()))
            {
                data.Add("start_date", dtp_start.Value.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrWhiteSpace(dtp_end.Text.ToString()))
            {
                data.Add("end_date", dtp_end.Value.ToString("yyyy-MM-dd"));
            }
            // 生产单位
            data.Add("dp_code", cb_dw.SelectedValue.ToString());
            // 报废原因
            data.Add("sg_reason", cb_reason.SelectedValue.ToString());
            // 报废单位签名
            data.Add("bf_staff_name", tb_bf.Text.ToString());
            // 环保股回收签名
            data.Add("hb_staff_name", tb_hbg.Text.ToString());
            data.Add("pageSize", pageSize);
            data.Add("pageIndex", pageIndex);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_BDMAPI",//类库名
                                        "SJ_BDMAPI.SCRAP_GLUE",//类名
                                        "SearchScrapGlueMagRecordByCS",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            return retdata;
        }

        public void GetMain_List(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                string retdata = GetDateListApi(pageSize, pageIndex);

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                uiDataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        uiDataGridView1.Rows.Add();
                        DataGridViewRow dgvr = uiDataGridView1.Rows[i];
                        dgvr.Cells["CREATEDATE"].Value = dr["CREATEDATE"].ToString();//日期
                        dgvr.Cells["DEPARTMENT_NAME"].Value = dr["DEPARTMENT_NAME"].ToString();//生产单位
                        dgvr.Cells["SCRAP_GLUE_NAME"].Value = dr["SCRAP_GLUE_NAME"].ToString();//报废胶水名称
                        dgvr.Cells["SCRAP_GLUE_WEIGHT"].Value = dr["SCRAP_GLUE_WEIGHT"].ToString();//报废胶水重量
                        dgvr.Cells["SCRAP_GLUE_REASON"].Value = dr["SCRAP_GLUE_REASON"].ToString();//报废原因
                        dgvr.Cells["BF_AUTOGRAPH_NAME"].Value = dr["BF_AUTOGRAPH_NAME"].ToString();//报废单位签名
                        dgvr.Cells["HB_AUTOGRAPH_NAME"].Value = dr["HB_AUTOGRAPH_NAME"].ToString();//环保股回收签名
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
              
                uiDataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            dtp_start.CustomFormat = "   ";
            dtp_end.CustomFormat = "   ";
            // 生产单位
            cb_dw.SelectedIndex = 0;
            // 报废原因
            cb_reason.SelectedIndex = 0;
            // 报废单位签名
            tb_bf.Text = "";
            // 环保股回收签名
            tb_hbg.Text = "";
        }
    }
}
