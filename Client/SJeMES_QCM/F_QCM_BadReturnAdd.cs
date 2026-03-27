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
    public partial class F_QCM_BadReturnAdd : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_BadReturnAdd()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dtp1);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_BadReturnAdd_Load(object sender, EventArgs e)
        {
            this.dtp1.Format = DateTimePickerFormat.Custom;
            this.dtp1.CustomFormat = " ";
        }

        private void btn_affirm_Click(object sender, EventArgs e)
        {
            try
            {
                string start_date1 = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dtp1.Text))
                {
                    start_date1 = Convert.ToDateTime(this.dtp1.Value).ToString("yyyy-MM-dd");
                }
                if (string.IsNullOrEmpty(start_date1.ToString()) ||
                      string.IsNullOrEmpty(txt_PLANT_AREA.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_ORDER_QTY.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_TURNOVER_QTY.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_RETURN_FREQUENCY.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_B_QTY.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_SHOE_NO.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_PROD_NO.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_AFFECT_HOURS.Text.Trim()) ||
                      string.IsNullOrEmpty(txt_PO.Text.Trim()))
                {
                    throw new Exception("必填项不能为空，请检查！");
                }


                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("RETURN_DATE", start_date1.ToString());
                p.Add("PLANT_AREA", txt_PLANT_AREA.Text);
                p.Add("ORDER_QTY", txt_ORDER_QTY.Text);
                p.Add("TURNOVER_QTY", txt_TURNOVER_QTY.Text);
                p.Add("RETURN_FREQUENCY", txt_RETURN_FREQUENCY.Text);
                p.Add("B_QTY", txt_B_QTY.Text);
                p.Add("SHOE_NO", txt_SHOE_NO.Text);
                p.Add("PROD_NO", txt_PROD_NO.Text);
                p.Add("AFFECT_HOURS", txt_AFFECT_HOURS.Text);
                p.Add("PO", txt_PO.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BadReturnBase",//类名
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
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_QCM_BadReturnAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (F_QCM_BadReturnMain main = new F_QCM_BadReturnMain())
            {
                main.Show();
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

        private void txt_PO_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "	select MER_PO PO单号,SHOE_NO 鞋型,PROD_NO ART from bdm_se_order_master a join bdm_se_order_item b on a.SE_ID=b.SE_ID";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_PO.Text = frmData.RetData.Rows[0]["PO单号"].ToString();
                txt_PROD_NO.Text = frmData.RetData.Rows[0]["ART"].ToString();
                txt_SHOE_NO.Text = frmData.RetData.Rows[0]["鞋型"].ToString();
            }
        }

        #region 输入框只能输入decimal类型
        private void txt_ORDER_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_ORDER_QTY.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_ORDER_QTY.Text, out oldf);
                    b2 = float.TryParse(txt_ORDER_QTY.Text + e.KeyChar.ToString(), out f);
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

        private void txt_TURNOVER_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_TURNOVER_QTY.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_TURNOVER_QTY.Text, out oldf);
                    b2 = float.TryParse(txt_TURNOVER_QTY.Text + e.KeyChar.ToString(), out f);
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

        private void txt_B_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_B_QTY.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_B_QTY.Text, out oldf);
                    b2 = float.TryParse(txt_B_QTY.Text + e.KeyChar.ToString(), out f);
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

        private void txt_RETURN_FREQUENCY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_RETURN_FREQUENCY.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_RETURN_FREQUENCY.Text, out oldf);
                    b2 = float.TryParse(txt_RETURN_FREQUENCY.Text + e.KeyChar.ToString(), out f);
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

        private void txt_AFFECT_HOURS_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_AFFECT_HOURS.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_AFFECT_HOURS.Text, out oldf);
                    b2 = float.TryParse(txt_AFFECT_HOURS.Text + e.KeyChar.ToString(), out f);
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
    }
}
