using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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
    public partial class F_QCM_CUSTOMER_COMPLAINT_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 产线代号
        /// </summary>
        public string PRODUCTIONLINE_NO { get; set; }
        public F_QCM_CUSTOMER_COMPLAINT_Main _F_QCM_CUSTOMER_COMPLAINT_Main { get; set; }
        public F_QCM_CUSTOMER_COMPLAINT_Add(F_QCM_CUSTOMER_COMPLAINT_Main F_QCM_CUSTOMER_COMPLAINT_Main)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _F_QCM_CUSTOMER_COMPLAINT_Main = F_QCM_CUSTOMER_COMPLAINT_Main;
            InitDateTimePicker(COMPLAINT_DATE);
            //InitDateTimePicker(txt_PRODUCT_MONTH);
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

        private void F_QCM_CUSTOMER_COMPLAINT_Add_Load(object sender, EventArgs e)
        {
            this.COMPLAINT_DATE.Format = DateTimePickerFormat.Custom;
            this.COMPLAINT_DATE.CustomFormat = " ";

            //this.txt_PRODUCT_MONTH.Format = DateTimePickerFormat.Custom;
            //this.txt_PRODUCT_MONTH.CustomFormat = " ";
        }

        

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //确认提交
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                #region 验证

                if (string.IsNullOrEmpty(this.COMPLAINT_DATE.Text) ||
                    string.IsNullOrEmpty(this.txt_PO_ORDER.Text) ||
                    string.IsNullOrEmpty(this.txt_NG_QTY.Text) || 
                    string.IsNullOrEmpty(txt_department_name.Text) ||
                    string.IsNullOrEmpty(this.txt_PRODUCTIONLINE_NAME.Text) )
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("所有字段为必填项，请检查！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }

                #endregion
                string COMPLAINT_DATE = string.Empty;
                string txt_PRODUCT_MONTH = string.Empty;

                if (!string.IsNullOrWhiteSpace(this.COMPLAINT_DATE.Text))
                {
                    COMPLAINT_DATE = Convert.ToDateTime(this.COMPLAINT_DATE.Value).ToString("yyyy-MM-dd");
                }
                //if (!string.IsNullOrWhiteSpace(this.txt_PRODUCT_MONTH.Text))
                //{
                //    txt_PRODUCT_MONTH = Convert.ToDateTime(this.txt_PRODUCT_MONTH.Value).ToString("yyyy-MM-dd");
                //}

                

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("COMPLAINT_DATE", this.COMPLAINT_DATE.Text);
                data.Add("COUNTRY_REGION", this.txt_COUNTRY_REGION.Text);
                data.Add("PO_ORDER", this.txt_PO_ORDER.Text);
                data.Add("NG_QTY", this.txt_NG_QTY.Text);
                data.Add("COMPLAINT_MONEY", this.txt_COMPLAINT_MONEY.Text);
                data.Add("DEVELOP_SEASON", this.txt_DEVELOP_SEASON.Text);
                data.Add("PRODUCT_MONTH", this.txt_PRODUCT_MONTH.Text);

                data.Add("PRODUCTIONLINE_NAME", PRODUCTIONLINE_NO);
                data.Add("PRODUCTIONLINE_NO", this.txt_PRODUCTIONLINE_NAME.Text);
                data.Add("MATERIAL_WAY", this.txt_MATERIAL_WAY.Text);
                data.Add("DEFECT_CONTENT", this.txt_DEFECT_CONTENT.Text);
                data.Add("PROD_NO", this.txt_ART.Text);



                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.CustomerComplaint", "AddCustomerComplaint", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    MessageBox.Show(ret.ErrMsg);
                    this.Close(); 
                    _F_QCM_CUSTOMER_COMPLAINT_Main.F_QCM_CUSTOMER_COMPLAINT_Main_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //PO弹窗
        private void txt_PO_ORDER_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "SELECT MER_PO AS PO号 FROM BDM_SE_ORDER_MASTER  ";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                this.txt_PO_ORDER.Text = frmData.RetData.Rows[0]["PO号"].ToString();

                //请求开发季度，量产月份数据
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("PROD_NO", this.txt_PO_ORDER.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ExternalColorCard", "GetColorCardDataByPO", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if(dt.Rows.Count > 0)
                    {
                        this.txt_DEVELOP_SEASON.Text = dt.Rows[0]["DEVELOP_SEASON"] != null ? dt.Rows[0]["DEVELOP_SEASON"].ToString() : "";
                        this.txt_PRODUCT_MONTH.Text = dt.Rows[0]["PRODUCT_MONTH"] != null ? dt.Rows[0]["PRODUCT_MONTH"].ToString() : "";
                        this.txt_MATERIAL_WAY.Text = dt.Rows[0]["MATERIAL_WAY"] != null ? dt.Rows[0]["MATERIAL_WAY"].ToString() : "";
                        //this.txt_PROD_NO.Text = dt.Rows[0]["PROD_NO"] != null ? dt.Rows[0]["PROD_NO"].ToString() : "";
                        this.txt_ART.Text = dt.Rows[0]["PROD_NO"] != null ? dt.Rows[0]["PROD_NO"].ToString() : "";
                    }
                    else
                    {
                        this.txt_DEVELOP_SEASON.Text = "";
                        this.txt_PRODUCT_MONTH.Text = "";
                        this.txt_PRODUCT_MONTH.Text = "";
                        this.txt_PRODUCT_MONTH.Text = "";
                        this.txt_ART.Text = "";
                    }
                    

                }

            }
        }
        public string department_no { get; set; }
        public string sqlline { get; set; }
        //部门弹窗
        private void txt_department_name_Click(object sender, EventArgs e)
        {
            
                //当前窗体名称+"_"+当前方法名称
                string sql = "SELECT DEPARTMENT_NO AS 部门代号,DEPARTMENT_NAME AS 部门名称 FROM BDM_QUALITY_DEPARTMENT_M ";

                FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                frmData.ShowDialog();
                if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                {

                    this.txt_department_name.Text = frmData.RetData.Rows[0]["部门名称"].ToString();
                    department_no = frmData.RetData.Rows[0]["部门代号"].ToString();

                    if (this.txt_department_name.Text != null || this.txt_department_name.Text != "")
                    {
                        this.txt_PRODUCTIONLINE_NAME.BackColor = Color.FromArgb(255, 255, 192);
                        this.txt_PRODUCTIONLINE_NAME.ReadOnly = false;
                    }

                    sqlline = $@"SELECT PRODUCTIONLINE_NO AS 产线代号,PRODUCTIONLINE_NAME AS 产线名称 FROM BDM_QUALITY_DEPARTMENT_D WHERE DEPARTMENT_NO = '{department_no}'";

                }
            
            
        }
        //产线弹窗
        private void txt_PRODUCTIONLINE_NAME_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            //string sql = "SELECT PRODUCTIONLINE_NO AS 产线代号, PRODUCTIONLINE_NAME AS 产线名称 FROM BDM_QUALITY_DEPARTMENT_D ";

            if (this.txt_department_name.Text == null || this.txt_department_name.Text == "")
            {
                return;
            }
            else
            {
                FrmSelectData frmData = new FrmSelectData(sqlline, true, Program.Client);
                frmData.ShowDialog();
                if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                {
                    this.txt_PRODUCTIONLINE_NAME.Text = frmData.RetData.Rows[0]["产线名称"].ToString();
                    PRODUCTIONLINE_NO = frmData.RetData.Rows[0]["产线代号"].ToString();
                    //PRODUCTIONLINE_NO = frmData.RetData.Rows[0]["产线代号"].ToString();


                }
            }

            
        }
    }
}
