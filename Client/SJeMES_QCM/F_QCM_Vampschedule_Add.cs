using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class F_QCM_Vampschedule_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Vampschedule_Add()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dtp1);//日期选择器
            InitDateTimePicker(dtp2);//日期选择器
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Vampschedule_Add_Load(object sender, EventArgs e)
        {
            #region 日期选择器初始为空
            this.dtp1.Format = DateTimePickerFormat.Custom;
            this.dtp1.CustomFormat = " ";
            this.dtp2.Format = DateTimePickerFormat.Custom;
            this.dtp2.CustomFormat = " ";
            #endregion
            GetDataList();
        }

        public void GetDataList()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.VampscheduleBase",//类名
                                            "GetOrderMasterList",//方法名
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
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        cb_SE_ID.Items.Add(dr["SE_ID"].ToString());
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            try
            {
                #region 获取日期控件的值
                string start_date1 = string.Empty;
                string start_date2 = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dtp1.Text))
                {
                    start_date1 = Convert.ToDateTime(this.dtp1.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.dtp2.Text))
                {
                    start_date2 = Convert.ToDateTime(this.dtp2.Value).ToString("yyyy-MM-dd");
                }
                #endregion
                if (string.IsNullOrEmpty(txt_WEEK_TIMES.Text.Trim()) ||
                      string.IsNullOrEmpty(dtp1.ToString().Trim()) ||
                      string.IsNullOrEmpty(txt_WORK_HOURS.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_LEAD_TIME.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_TRIP_QTY.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_SHOE_NO.Text.Trim()) ||
                      string.IsNullOrEmpty(cb_SE_ID.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_QTY.Text.Trim()) ||
                      string.IsNullOrEmpty(dtp2.ToString().Trim()) ||
                      string.IsNullOrEmpty(txt_LAST_NUMBER.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_VAMP_TYPE.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_MODULE_NO.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_ITEM_NO.Text.Trim()))
                {
                    throw new Exception("必填项不能为空，请检查！");
                }


                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("WEEK_TIMES", txt_WEEK_TIMES.Text);
                p.Add("WORK_HOURS", txt_WORK_HOURS.Text);
                p.Add("LEAD_TIME", txt_LEAD_TIME.Text);
                p.Add("TRIP_QTY", txt_TRIP_QTY.Text);
                p.Add("SHOE_NO", txt_SHOE_NO.Text);
                p.Add("SE_ID", cb_SE_ID.Text);
                p.Add("QTY", txt_QTY.Text);
                p.Add("LAST_NUMBER", txt_LAST_NUMBER.Text);
                p.Add("VAMP_TYPE", txt_VAMP_TYPE.Text);
                p.Add("MODULE_NO", txt_MODULE_NO.Text);
                p.Add("ITEM_NO", txt_ITEM_NO.Text);
                p.Add("PUTINTO_DATE", start_date1.ToString());
                p.Add("ORDER_DELIVERY_DATE", start_date2.ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.VampscheduleBase",//类名
                                            "AddList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                else
                {
                    MessageBox.Show("保存成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        #region 输入框只能输入decimal类型
        private void txt_WORK_HOURS_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_WORK_HOURS.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_WORK_HOURS.Text, out oldf);
                    b2 = float.TryParse(txt_WORK_HOURS.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void txt_TRIP_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_TRIP_QTY.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_TRIP_QTY.Text, out oldf);
                    b2 = float.TryParse(txt_TRIP_QTY.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void txt_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_QTY.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_QTY.Text, out oldf);
                    b2 = float.TryParse(txt_QTY.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
