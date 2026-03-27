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

namespace SJeMES_QCM
{
    public partial class F_QCM_CUSTOMER_COMPLAINT_Detail : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _COMPLAINT_NO { get; set; }
        public F_QCM_CUSTOMER_COMPLAINT_Detail(string COMPLAINT_NO)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(COMPLAINT_DATE);
            InitDateTimePicker(PRODUCT_MONTH);
            _COMPLAINT_NO = COMPLAINT_NO;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

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

        //初始化
        private void F_QCM_CUSTOMER_COMPLAINT_Detail_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetData;
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        //获取数据
        public void GetData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                #region 表头
                string SPOTCHECK_DATE_START = string.Empty;
                string SPOTCHECK_DATE_END = string.Empty;

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("COMPLAINT_NO", _COMPLAINT_NO);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.CustomerComplaint", "GetCustomerComplaintDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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

                            txt_COMPLAINT_NO.Text = dr["COMPLAINT_NO"].ToString();
                            COMPLAINT_DATE.Text = dr["COMPLAINT_DATE"].ToString();
                            txt_COUNTRY_REGION.Text = dr["COUNTRY_REGION"].ToString();

                            txt_PO_ORDER.Text = dr["PO_ORDER"].ToString();
                            txt_NG_QTY.Text = dr["NG_QTY"].ToString();
                            txt_COMPLAINT_MONEY.Text = dr["COMPLAINT_MONEY"].ToString();
                            DEFECT_CONTENT.Text = dr["DEFECT_CONTENT"].ToString();

                            txt_DEVELOP_SEASON.Text = dr["DEVELOP_SEASON"].ToString();
                            txt_CATEGORY.Text = dr["CATEGORY"].ToString();
                            txt_DEVELOPMENT_COURSE.Text = dr["DEVELOPMENT_COURSE"].ToString();

                            PRODUCT_MONTH.Text = dr["PRODUCT_MONTH"].ToString();
                            txt_PROD_NO.Text = dr["PROD_NO"].ToString();
                            txt_SHOE_NO.Text = dr["SHOE_NO"].ToString();

                            txt_MATERIAL_WAY.Text = dr["MATERIAL_WAY"].ToString();
                            txt_PRODUCTIONLINE_NAME.Text = dr["PRODUCTIONLINE_NAME"].ToString();
                            i++;
                        }
                    }
                    //totalCount = int.Parse(dic["rowCount"].ToString());
                    
                }
                #endregion

                #region 列表

                Dictionary<string, object> data2 = new Dictionary<string, object>();
                data2.Add("COMPLAINT_NO", _COMPLAINT_NO);
                data2.Add("pageSize", pageSize);
                data2.Add("pageIndex", pageIndex);

                string retdata2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.CustomerComplaint", "GetCustomerComplaintDetailList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data2));
                ResultObject ret2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata2);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {

                    Dictionary<string, object> dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2.RetData);

                    dataGridView1.Rows.Clear();
                    var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic2["Data"].ToString());
                    if (dt2.Rows.Count > 0)
                    {
                        int i = 0;
                        int z = 1;
                        foreach (DataRow dr in dt2.Rows)
                        {

                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr2 = dataGridView1.Rows[i];

                            //dgvr.Cells["SPOTCHECK_NO"].Value

                            dgvr2.Cells["ID"].Value = z;
                            dgvr2.Cells["CAUSE_ANALYSIS"].Value = dr["CAUSE_ANALYSIS"].ToString();

                            dgvr2.Cells["RESPONSIBILITY_JUDGMENT"].Value = dr["RESPONSIBILITY_JUDGMENT"].ToString();
                            dgvr2.Cells["IMPROVEMENT_ACTION"].Value = dr["IMPROVEMENT_ACTION"].ToString();
                            dgvr2.Cells["CONCLUSION"].Value = dr["CONCLUSION"].ToString();
                            i++;
                            z++;
                        }
                    }
                    totalCount = int.Parse(dic2["rowCount"].ToString());

                }

                #endregion
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
    }
}
