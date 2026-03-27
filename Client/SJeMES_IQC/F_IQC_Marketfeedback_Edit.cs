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

namespace SJeMES_IQC
{
    public partial class F_IQC_Marketfeedback_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private string _task_no;
        public F_IQC_Marketfeedback_Edit(string task_no)
        {
            InitializeComponent();
            _task_no = task_no;
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

        private void F_IQC_Marketfeedback_Edit_Load(object sender, EventArgs e)
        {
            Getlistxlk();//下拉框
            if (!string.IsNullOrWhiteSpace(_task_no))
            {
                Getlist();
            }
        }
        /// <summary>
        /// 限制输入小数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_oldshoes_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            var control = sender as Control;
            if (control.Text.Length > 1)
            {
                if (control.Text.Contains(".") && !control.Text.StartsWith("."))
                {
                    if (e.KeyChar.ToString().EndsWith("."))
                        e.Handled = true;
                    else if (e.KeyChar != 8 && !Char.IsNumber(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
                else if (!control.Text.Contains("."))
                {
                    if (!e.KeyChar.ToString().EndsWith(".") && e.KeyChar != 8 && !Char.IsNumber(e.KeyChar))
                        e.Handled = true;
                }
            }
            else
            {
                if (e.KeyChar != 8 && !Char.IsNumber(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(txt_po.Text))
                //{
                //    MessageBox.Show("po号码不能为空");
                //    return;
                //}
                string thyf_str = "";
                if (!string.IsNullOrWhiteSpace(tb_thyf.Text.Trim()))
                {
                    DateTime thyf_date;
                    if(!DateTime.TryParse(tb_thyf.Text.Trim(),out thyf_date))
                    {
                        MessageBox.Show("Return month format error！");
                        return;
                    }
                    thyf_str = thyf_date.ToString("yyyy-MM");
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", _task_no);//任务编号
                p.Add("po", txt_po.Text);//PO号码
                p.Add("region_no", cbo_region.SelectedValue);//国家代号
                p.Add("size_no", txt_size_no.Text);//码数
                p.Add("newshoes_qty", txt_newshoes_qty.Text);//新鞋qty
                p.Add("oldshoes_qty", txt_oldshoes_qty.Text);//旧鞋qty
                p.Add("main_code", cbo_main_code.SelectedValue);//主要代码
                p.Add("minor_code", cbo_minor_code.SelectedValue);//次要代码
                p.Add("fob_price", txt_fob_price.Text);//FOB
                p.Add("compensation_amount", txt_compensation_amount.Text);//赔偿金额
                p.Add("problem_point_desc", txt_problem_point_desc.Text);//问题描述
                p.Add("datetime", tb_date.Text);//年份/月份
                p.Add("thyf", thyf_str);//退货月份
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.Marketfeedback",//类名
                                            "Commit_Mian",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                }
                else
                {
                    MessageBox.Show(ret.ErrMsg);
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
           
        }
        private void Getlist()
        {

            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", _task_no);//任务编号
              
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.Marketfeedback",//类名
                                            "Commithisorypc",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    txt_po.Text = dt.Rows[0]["po"].ToString();//po
                    cbo_region.SelectedValue = dt.Rows[0]["region_no"].ToString();//国家代号
                    txt_size_no.Text = dt.Rows[0]["size_no"].ToString();//码数
                    txt_newshoes_qty.Text = dt.Rows[0]["newshoes_qty"].ToString();//新鞋数量
                    txt_oldshoes_qty.Text  = dt.Rows[0]["oldshoes_qty"].ToString();//旧鞋数量
                    cbo_main_code.SelectedValue  = dt.Rows[0]["main_code"].ToString();//主要代码
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["main_code"].ToString()))
                    {
                        lisx();
                    }
                    cbo_minor_code.SelectedValue = dt.Rows[0]["minor_code"].ToString();//次要代码
                    txt_fob_price.Text = dt.Rows[0]["fob_price"].ToString();//FOB单价
                    txt_compensation_amount.Text = dt.Rows[0]["compensation_amount"].ToString();//赔偿金额
                    txt_problem_point_desc.Text = dt.Rows[0]["problem_point_desc"].ToString();//问题点描述
                    textBox4.Text = dt.Rows[0]["name_t"].ToString();//鞋型

                    tb_date.Text= dt.Rows[0]["DATETIME"].ToString();//年份月份
                    tb_thyf.Text= dt.Rows[0]["RETURN_MONTH"].ToString();//退货月份
                }
              
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void Getlistxlk()
        {

            try
            {
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.Marketfeedback",//类名
                                            "Getlistxlk",//方法名
                                            Program.Client.UserToken,"");

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                DataTable dt1= Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data1"].ToString());
                if (dt.Rows.Count > 0) 
                {
                    cbo_main_code.DataSource = dt;
                    cbo_main_code.ValueMember = "main_code";
                    cbo_main_code.DisplayMember = "content_cn";//主要问题
                    cbo_main_code.SelectedIndex = -1;
                }
                if (dt1.Rows.Count > 0)
                {
                    cbo_region.DataSource = dt1;
                    cbo_region.ValueMember = "region_no";
                    cbo_region.DisplayMember = "region_name";//国家地区代码
                    cbo_main_code.SelectedIndex = -1;

                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void cbo_main_code_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lisx();
        }
        public void lisx()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("main_code", cbo_main_code.SelectedValue);//任务编号


            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.Marketfeedback",//类名
                                        "GetCode2",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            Dictionary<string, object> ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            if (!Convert.ToBoolean(ret["IsSuccess"]))
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData1"].ToString());
            cbo_minor_code.DataSource = null;
            //视图数据显示
            if (dt.Rows.Count > 0)
            {
                cbo_minor_code.DataSource = dt;
                cbo_minor_code.ValueMember = "minor_code";
                cbo_minor_code.DisplayMember = "content_cn";//次要问题代码
            }
            
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_po_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_po_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btn_po_edit_Click(object sender, EventArgs e)
        {
            string sql = $@"SELECT
	DISTINCT
	M.mer_po as PO号码,--po号
	l.name_t as 鞋型, --鞋型
	r.prod_no as art,--art
	p.name_t as Category -- category
FROM
	BDM_SE_ORDER_MASTER M
LEFT JOIN BDM_SE_ORDER_ITEM E ON M .SE_ID = E.SE_ID
LEFT JOIN bdm_rd_prod r ON E .prod_no = r.PROD_NO
LEFT JOIN BDM_RD_STYLE l ON r.SHOE_NO = l.SHOE_NO
LEFT JOIN bdm_shoe_extend_m s ON r.SHOE_NO = s.SHOE_NO
LEFT JOIN bdm_rd_style y ON s.SHOE_NO = y.SHOE_NO
LEFT JOIN bdm_cd_code p on y.style_seq=p.code_no
";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_po.Text = frmData.RetData.Rows[0]["PO号码"].ToString();
                textBox4.Text = frmData.RetData.Rows[0]["鞋型"].ToString();
            }
        }
    }
}
