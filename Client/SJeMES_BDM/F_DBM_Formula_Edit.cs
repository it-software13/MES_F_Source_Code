using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls;
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
    public partial class F_DBM_Formula_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string formula_code;
        public F_DBM_Formula_Edit(string ID)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            formula_code = ID;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void Frm_bdm_formulaEdit_Load(object sender, EventArgs e)
        {
            //设置文本框禁止输入
            cbo_type.Enabled = false;//禁止输入，默认自定义
            
            Enum();

            if (!string.IsNullOrEmpty(formula_code))
            {
                try
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    //键值对传值
                    p.Add("formula_code", formula_code);

                    #region 找接口
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.Formula",//类名
                                                       "GetByIdFormulaList",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    #endregion

                    //int Uid = 0;

                    DataTable table = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    //Uid = (int)table.Rows[0]["序号"] ;
                    txt_code.Text = table.Rows[0]["formula_code"].ToString();
                    txt_name.Text = table.Rows[0]["formula_name"].ToString();

                    cbo_type.Text = table.Rows[0]["formula_type"].ToString() == "1" ? Formula_Type_enum.Type_enum_1: "";

                    txt_review.Text = table.Rows[0]["formula_content"].ToString();
                    rtb_results.Text = table.Rows[0]["formula_content"].ToString();
                    rtb_remarks.Text = table.Rows[0]["remarks"].ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                txt_code.Enabled = false;//禁止修改公式编号
            }

        }

        /// <summary>
        /// 获取枚举方法
        /// </summary>
        public void Enum()
        {
            try
            {
                List<string> lst_enum_type = new List<string>();
                lst_enum_type.Add("enum_formula_type");
                lst_enum_type.Add("enum_general_formula");

                //查询枚举
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.BASE",//类名
                                           "GetSYS001MDataListS",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());

                //公式类型
                cbo_type.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_formula_type"].ToString());
                cbo_type.DisplayMember = "enum_value";
                cbo_type.ValueMember = "enum_code";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 关闭当前窗体 取消按钮
        /// </summary>
        private void button23_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        private void button24_Click(object sender, EventArgs e)
        {
            //有id就修改
            if (!string.IsNullOrEmpty(formula_code))
            {
                try
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    if (string.IsNullOrEmpty(txt_name.Text.Trim()) ||
                       string.IsNullOrEmpty(cbo_type.Text.Trim())||
                        string.IsNullOrEmpty(rtb_results.Text.Trim())
                        )
                    {
                        throw new Exception("必填项不能为空，请检查！");
                    }
                    //键值对传值
                    p.Add("formula_code", formula_code);
                    p.Add("txt_name", this.txt_name.Text.Trim());
                    p.Add("cbo_type", this.cbo_type.Text.Trim());
                    p.Add("txt_content", this.rtb_results.Text.Trim());
                    p.Add("rtb_remarks", this.rtb_remarks.Text.Trim());

                    #region 找接口
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.Formula",//类名
                                                       "UpdateByIdFormulaList",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    #endregion
                    MessageBox.Show("修改成功！");
                    this.Hide();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //没有就添加
            else
            {
                try
                {
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();

                    if ( string.IsNullOrEmpty(txt_code.Text.Trim()) || 
                        string.IsNullOrEmpty(txt_name.Text.Trim()) || 
                        string.IsNullOrEmpty(cbo_type.Text.Trim())||
                        string.IsNullOrEmpty(rtb_results.Text.Trim())
                        )
                    {
                        throw new Exception("必填项不能为空，请检查！");
                    }
                    if (cbo_type.Text.Trim() == Formula_Type_enum.Type_enum_1)
                    {
                        cbo_type.Text = "1";
                    }
                    #region 参数

                    //键值对传值
                    p.Add("txt_code", this.txt_code.Text.Trim());
                    p.Add("txt_name", this.txt_name.Text.Trim());
                    p.Add("cbo_type", this.cbo_type.Text.Trim());
                    p.Add("txt_content", this.rtb_results.Text.Trim());
                    p.Add("rtb_remarks", this.rtb_remarks.Text.Trim());

                    #endregion

                    #region 找接口
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.Formula",//类名
                                                       "InsFormulaList",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    MessageBox.Show("添加成功！");
                    this.Hide();
                    #endregion

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string tt = ((Button)sender).Tag.ToString();
               
                //退格
                    if (tt == Formula_Type_enum.Type_enum_2)
                    {
                        if(txt_review.Text.Length>0 && rtb_results.Text.Length > 0)
                        {
                             txt_review.Text = txt_review.Text.Substring(0, txt_review.Text.Length - 1);
                             rtb_results.Text = rtb_results.Text.Substring(0, rtb_results.Text.Length - 1);
                             tt = string.Empty;
                         }
                  
                    }
                    //清空
                    if (tt == Formula_Type_enum.Type_enum_3)
                    {
                        txt_review.Text = "";
                        rtb_results.Text = string.Empty;
                        tt = string.Empty;
                   
                     }
                    //输入值N
                    if (tt == Formula_Type_enum.Type_enum_4)
                    {
                        txt_review.Text += "N";
                        rtb_results.Text += "N";
                    }
                    else
                    {
                    //退格
                         if (tt != Formula_Type_enum.Type_enum_2)
                         {
                             txt_review.Text += tt;
                             rtb_results.Text += tt;
                         }
                       
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作错误，原因是:{ex}，请重新输入");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void btn_backspace_Click(object sender, EventArgs e)
        {

        }
    }
}
