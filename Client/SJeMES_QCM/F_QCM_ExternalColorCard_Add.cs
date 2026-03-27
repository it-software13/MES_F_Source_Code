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
    public partial class F_QCM_ExternalColorCard_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_ExternalColorCard_Main _F_QCM_ExternalColorCard_Main { get; set; }
        public F_QCM_ExternalColorCard_Add(F_QCM_ExternalColorCard_Main f_qcm_externalcolorcard_main)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
 Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(time);
            _F_QCM_ExternalColorCard_Main = f_qcm_externalcolorcard_main;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        //添加
        private void actbtn_Click(object sender, EventArgs e)
        {
            try
            {
                #region 验证
                if (string.IsNullOrEmpty(this.txt_vend_no.Text) ||
                    string.IsNullOrEmpty(this.txt_vend_name.Text) ||
                    string.IsNullOrEmpty(this.time.Text) ||
                    string.IsNullOrEmpty(this.txt_firstarticle_type.Text) ||
                    string.IsNullOrEmpty(this.txt_shoes.Text) ||
                    string.IsNullOrEmpty(this.txt_prod_no.Text) ||
                    string.IsNullOrEmpty(this.txt_part_no.Text) ||
                    string.IsNullOrEmpty(this.txt_qc.Text) 
                    )
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("所有字段为必填项，请检查！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }

                #endregion
                string timeDate = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.time.Text))
                {
                    timeDate = Convert.ToDateTime(this.time.Value).ToString("yyyy-MM-dd");
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("time", timeDate);
                data.Add("VEND_NO", this.txt_vend_no.Text);
                data.Add("VEND_NAME", this.txt_vend_name.Text);
                data.Add("FIRSTARTICLE_TYPE", this.txt_firstarticle_type.Text);
                data.Add("SHOE_NO", this.txt_shoes.Text);
                data.Add("PROD_NO", this.txt_prod_no.Text);
                data.Add("PART_NO", this.txt_part_no.Text);
                data.Add("IS_QCCONFIRM", this.txt_qc.Text);



                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.ExternalColorCard", "AddColorCard", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    MessageBox.Show(ret.ErrMsg);
                    this.Close();
                    _F_QCM_ExternalColorCard_Main.F_QCM_ExternalColorCard_Main_Load(null, null);
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //厂商代号点击事件
        private void txt_vend_no_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            string sql = @"select SUPPLIERS_CODE as 厂商代号,SUPPLIERS_NAME as 厂商名称 from BASE003M order by id desc";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                this.txt_vend_no.Text = frmData.RetData.Rows[0]["厂商代号"].ToString();
                this.txt_vend_name.Text = frmData.RetData.Rows[0]["厂商名称"].ToString();
               // txt_productionline_no.Text = null;
            }
        }

        private void F_QCM_ExternalColorCard_Add_Load(object sender, EventArgs e)
        {
            this.time.Format = DateTimePickerFormat.Custom;
            this.time.CustomFormat = " ";
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

        private void txt_vend_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_prod_no_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            string sql = @"SELECT  PROD_NO AS ART,SHOE_NO AS 鞋型 FROM BDM_RD_PROD ";
            FrmSelectData frmData = new FrmSelectData(sql, false, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                this.txt_prod_no.Text = frmData.RetData.Rows[0]["ART"].ToString();
                this.txt_shoes.Text = frmData.RetData.Rows[0]["鞋型"].ToString();
                // txt_productionline_no.Text = null;
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
