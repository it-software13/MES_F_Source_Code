using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class F_BDM_NRecipientsburs : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics = new Dictionary<string, object>();

        public F_BDM_NRecipientsburs(Dictionary<string, object> dic)
        {
            InitializeComponent();
            dics=dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
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
        private void F_BDM_NRecipientsburs_Load(object sender, EventArgs e)
        {
            txt_org.Text = dics["org_name"].ToString();
            txt_production_line.Text = dics["production_line_name"].ToString();
            txt_needle_category.Text = dics["needle_category_name"].ToString();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string putin_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_date.Value).ToString("yyyy-MM-dd");
                }
                int qty = 0;
                int.TryParse(txt_qty.Text, out qty);
                if (qty < 1)
                {
                    MessageBox.Show("Please enter the quantity >=1");
                   
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("id", dics["id"].ToString());
                p.Add("collar_qty", txt_qty.Text);
                p.Add("collar_date", putin_date);
                p.Add("opa_type", "0");//领用
                p.Add("remarks", txt_remark.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                            "BDM_Needlemanagement_PDAadd",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Saved successfully！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_out_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
