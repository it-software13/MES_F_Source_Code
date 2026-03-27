using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_ConfirmShoes_Add : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 确认鞋ID
        /// </summary>
        private string IDS;
        /// <summary>
        /// 0:原材料确认鞋管理；1：成品确认鞋管理；
        /// </summary>
        private string types = string.Empty;
        public F_QCM_ConfirmShoes_Add(string ID,string type)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
 Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            types = type;
            IDS = ID;
            if (!string.IsNullOrEmpty(IDS))
            {
                btn_Add.Text = "再次确认";
                btn_OutDay.Visible = true;
                btn_Drop.Visible = true;
            }
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        /// <summary>
        /// 修改还有视图表头展示
        /// </summary>
        public void stringtypeList()
        {
            DataTable dt = DataList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    txt_PROD_NO.Text = item["PROD_NO"].ToString();
                    txt_SHOE_NO.Text = item["SHOE_NO"].ToString();
                    txt_DEVELOP_SEASON.Text = item["DEVELOP_SEASON"].ToString();
                    txt_QTY.Text = item["QTY"].ToString();
                    txt_RECEIVE_DATE.Text = item["RECEIVE_DATE"].ToString();
                    txt_RECEIVE_PEOPLE.Text = item["RECEIVE_PEOPLE"].ToString();

                    txt_CONFIRM_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txt_CONFIRM_PEOPLE.Text = item["CONFIRM_PEOPLE"].ToString();
                    txt_REDO_REASON.Text = item["REDO_REASON"].ToString();
                    txt_REMARKS.Text = item["REMARKS"].ToString();
                    XLK();
                    cbo_CONFIRM_RESULT.SelectedValue = item["CONFIRM_RESULT"].ToString();
                    if (item["STATUS"] != null)
                    {
                        string status = item["STATUS"].ToString();
                        switch (status)
                        {
                            case enum_confirm_status.enum_confirm_status_0:
                                txt_STATUS.Text = enum_confirm_status.enum_confirm_status_string_0;
                                break;
                            case enum_confirm_status.enum_confirm_status_1:
                                txt_STATUS.Text = enum_confirm_status.enum_confirm_status_string_1;
                                break;
                            case enum_confirm_status.enum_confirm_status_2:
                                txt_STATUS.Text = enum_confirm_status.enum_confirm_status_string_2;
                                break;
                            case enum_confirm_status.enum_confirm_status_3:
                                txt_STATUS.Text = enum_confirm_status.enum_confirm_status_string_3;
                                break;
                        }
                    }
                }
            }
        }

        private void F_QCM_ConfirmShoes_Add_Load(object sender, EventArgs e)
        {
            txt_RECEIVE_DATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_CONFIRM_DATE.Text= DateTime.Now.ToString("yyyy-MM-dd");
            XLK();
            if (!string.IsNullOrEmpty(IDS))
            {
                stringtypeList();
            }
        }
        public void XLK()
        {
            //下拉框
            try
            {
                List<string> lst_enum_type = new List<string>();
                lst_enum_type.Add("enum_test_result");
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
                //结果引用级别
                cbo_CONFIRM_RESULT.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_test_result"].ToString());
                cbo_CONFIRM_RESULT.DisplayMember = "enum_value";
                cbo_CONFIRM_RESULT.ValueMember = "enum_code";
                cbo_CONFIRM_RESULT.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 抽检明细查看或者修改数据展示
        /// </summary>
        public DataTable DataList()
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ID", IDS);//关联键
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ConfirmShoesBase",//类名
                                        "ConfirmShoesBaseViewByid",//方法名
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
            return dt;
        }
        private string status = enum_confirm_status.enum_confirm_status_0;
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (NotNull.Trues(
                    txt_PROD_NO.Text,
                    txt_SHOE_NO.Text,
                    txt_DEVELOP_SEASON.Text,
                    txt_QTY.Text,
                    txt_RECEIVE_DATE.Text,
                    txt_CONFIRM_DATE.Text,
                    txt_RECEIVE_PEOPLE.Text,
                    txt_CONFIRM_PEOPLE.Text,
                    cbo_CONFIRM_RESULT.Text
                    ))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("必填项不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {
                    //点击添加
                    if (string.IsNullOrEmpty(IDS))
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("PROD_NO", txt_PROD_NO.Text);
                        p.Add("SHOE_NO", txt_SHOE_NO.Text);
                        p.Add("DEVELOP_SEASON", txt_DEVELOP_SEASON.Text);
                        p.Add("QTY", txt_QTY.Text);
                        p.Add("RECEIVE_DATE", txt_RECEIVE_DATE.Text);
                        p.Add("RECEIVE_PEOPLE", txt_RECEIVE_PEOPLE.Text);
                        p.Add("CONFIRM_DATE", txt_CONFIRM_DATE.Text);//再次确认日期
                        p.Add("CONFIRM_PEOPLE", txt_CONFIRM_PEOPLE.Text);
                        p.Add("CONFIRM_RESULT", cbo_CONFIRM_RESULT.SelectedValue);
                        p.Add("REDO_REASON", txt_REDO_REASON.Text);
                        p.Add("REMARKS", txt_REMARKS.Text);
                        p.Add("STATUS", txt_STATUS.Text);

                        p.Add("confirm_type", types);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.ConfirmShoesBase",//类名
                                                    "ConfirmShoesBaseAdd",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("新建数据成功");
                            Thread.Sleep(1000);//当前线程睡一下
                            this.Close();
                        }
                    }
                    //点击修改
                    else if (!string.IsNullOrEmpty(IDS))
                    {
                        ResultObject ret = Update(enum_confirm_status.enum_confirm_status_0);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("修改数据成功");
                            Thread.Sleep(1000);//当前线程睡一下
                            this.Close();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        #region 接收人，确认人弹框选择,ART,鞋型,开发季带出
        private void txt_PROD_NO_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "select PROD_NO ART编号,SHOE_NO 鞋型,DEVELOP_SEASON 开发季 from bdm_rd_prod";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_PROD_NO.Text = frmData.RetData.Rows[0]["ART编号"].ToString();
                txt_SHOE_NO.Text = frmData.RetData.Rows[0]["鞋型"].ToString();
                txt_DEVELOP_SEASON.Text = frmData.RetData.Rows[0]["开发季"].ToString();
            }

        }
        private void txt_RECEIVE_PEOPLE_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "select STAFF_NO 接收人编号,STAFF_NAME 接收人名称 from HR001M";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_RECEIVE_PEOPLE.Text = frmData.RetData.Rows[0]["接收人名称"].ToString();
            }
        }

        private void txt_CONFIRM_PEOPLE_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "select STAFF_NO 确认人编号,STAFF_NAME 确认人名称 from HR001M";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_CONFIRM_PEOPLE.Text = frmData.RetData.Rows[0]["确认人名称"].ToString();
            }
        }
        #endregion
        /// <summary>
        /// 判断正整数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
        public ResultObject Update(string status)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();

            p.Add("ID", IDS);
            p.Add("PROD_NO", txt_PROD_NO.Text.Trim());
            p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim());
            p.Add("DEVELOP_SEASON", txt_DEVELOP_SEASON.Text.Trim());
            p.Add("QTY", txt_QTY.Text.Trim());
            p.Add("RECEIVE_DATE", txt_RECEIVE_DATE.Text.Trim());
            p.Add("RECEIVE_PEOPLE", txt_RECEIVE_PEOPLE.Text.Trim());
            p.Add("CONFIRM_DATE", txt_CONFIRM_DATE.Text.Trim());
            p.Add("CONFIRM_PEOPLE", txt_CONFIRM_PEOPLE.Text.Trim());
            p.Add("CONFIRM_RESULT", cbo_CONFIRM_RESULT.SelectedValue);
            p.Add("REDO_REASON", txt_REDO_REASON.Text);
            p.Add("REMARKS", txt_REMARKS.Text);
            p.Add("STATUS", status);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                       "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ConfirmShoesBase",//类名
                                        "ConfirmShoesBaseUpdate",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            return ret;
        }
        //忽略日期
        private void btn_OutDay_Click(object sender, EventArgs e)
        {
            //0:在期内；1：已忽略；2：过期；3报废
            try
            {
                if (!string.IsNullOrEmpty(IDS))
                {
                    if (MessageBox.Show("确认忽略？", "此操作不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ResultObject ret = Update(enum_confirm_status.enum_confirm_status_1);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("忽略日期成功");
                            Thread.Sleep(1000);//当前线程睡一下
                            this.Close();
                        }
                    }

                }
               
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
           
        }
        //报废
        private void btn_Drop_Click(object sender, EventArgs e)
        {
            //0:在期内；1：已忽略；2：过期；3报废
            try
            {
                if (!string.IsNullOrEmpty(IDS))
                {
                    if (MessageBox.Show("确认报废？", "此操作不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ResultObject ret = Update(enum_confirm_status.enum_confirm_status_3);
                        if (!ret.IsSuccess)
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                        else
                        {
                            MessageBox.Show("报废成功");
                            Thread.Sleep(1000);//当前线程睡一下
                            this.Close();
                        }
                    }

                }
              
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
           
        }
    }
}
