using DataGrid.DataGridViewCustomColumn;
using GDSJ_Framework.WinForm.CommonForm;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class F_BDM_QualityStandard_Item : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        int a = 1;

        string qid = string.Empty;//一级菜单编号
        string yq = string.Empty;//通用类型编号
        string did = string.Empty;

        //当前分类(自定义类型)
        public F_BDM_QualityStandard_Item(string id, string YQ, string _did)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            qid = id;
            yq = YQ;
            did = _did;
            GetTitle(id, YQ);
            pageControl1.BindPageEvent += GetSelect;
            LoadPage();
            pageControl2.BindPageEvent += GetSelect2;
            LoadPage2();
            pageControl3.BindPageEvent += GetSelect3;
            LoadPage3();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        //当前分类(通用类型)
        public F_BDM_QualityStandard_Item(string gmdName, string _ids)
        {
            InitializeComponent();
            this.label4.Text = gmdName;
            string[] ids = _ids.Split(',');
            yq = ids[0];
            qid = ids[1];
            did = ids[2];
            pageControl1.BindPageEvent += GetSelect;
            LoadPage();
            pageControl2.BindPageEvent += GetSelect2;
            LoadPage2();
            pageControl3.BindPageEvent += GetSelect3;
            LoadPage3();
        }

        //获取当前分类(自定义类型)
        public void GetTitle(string qid, string yq)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("qid", qid);
                data.Add("yq", yq);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "GetTitle", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    List<string> dt = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(j["RetData"].ToString());
                    this.label4.Text = dt[0] + "—" + dt[1];
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //委托测试项数据
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        //委托外观检测项数据
        public void LoadPage2()
        {
            pageControl2.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl2.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl2.SetPage();
        }

        //委托试穿检测项数据
        public void LoadPage3()
        {
            pageControl3.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl3.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl3.SetPage();
        }

        //返回
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //确认
        private void button2_Click(object sender, EventArgs e)
        {
            InsertBDM_QUALITYTEST_ITEM();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabTesting")
            {
                a = 1;
            }
            if (tabControl1.SelectedTab.Name == "tabAppearance")
            {
                a = 2;
            }
            if (tabControl1.SelectedTab.Name == "tabTryOn")
            {
                a = 3;
            }
        }

        /// <summary>
        /// 勾选外观按钮事件
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            if (a == 1)
                BDM_TESTITEM_M();
            else if (a == 2)
                BDM_APTESTITEM_M();
            else if (a == 3)
                BDM_TNTESTITEM_M();
        }

        /// <summary>
        /// 添加测试项数据
        /// </summary>
        public void BDM_TESTITEM_M()
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            string sql = @" SELECT
	testtype_no AS 检测项类型,
	testitem_code AS 检测项编号,
	testitem_name AS 检测项名称,
	testtype_name AS 检测项类型名称,
	'' AS 判断标准， '' AS 测量标准,
	AQL_LEVEL as AQL级别,
	unit AS 单位,
	sample_num AS 试样数量,
	reference_level AS 引用级别,
	currency_formula AS 通用公式代号,
	( SELECT ENUM_VALUE FROM SYS001M WHERE ENUM_TYPE = 'enum_general_formula' AND ENUM_CODE = currency_formula ) AS 通用公式名称， 
    custom_formula AS 自定义公式代号,
	( SELECT formula_name FROM bdm_formula_m WHERE formula_code = custom_formula ) AS 自定义公式名称,
	TYPE AS 类型,
CASE
	TYPE 
		WHEN '1' THEN
		'固定项' 
		WHEN '2' THEN
		'上下限' 
		WHEN '3' THEN
		'极差值' ELSE '' 
	END AS 类型名称，
	remarks AS 备注 
FROM
	BDM_TESTITEM_M m";

            FrmSelectData frmData = new FrmSelectData(sql, false, Program.Client,"R");
            frmData.ShowDialog();

            DataTable dtt = new DataTable();
            dtt.Columns.Add("testtype_no_1");
            dtt.Columns.Add("testitem_code_1");
            dtt.Columns.Add("testitem_name_1");
            dtt.Columns.Add("testtype_name_1");
            dtt.Columns.Add("check_item_1");
            dtt.Columns.Add("check_value_1");
            dtt.Columns.Add("unit_1");
            dtt.Columns.Add("reference_level_1");
            dtt.Columns.Add("sample_num_1");
            dtt.Columns.Add("currency_formula_1");
            dtt.Columns.Add("currency_formula_name_1");
            dtt.Columns.Add("custom_formula_1");
            dtt.Columns.Add("custom_formula_name_1");
            dtt.Columns.Add("remarks_1");
            dtt.Columns.Add("check_type_1");
            //新增的三个（AQL级别，AC值，RE值）
            dtt.Columns.Add("AQL_LEVEL_1");
            Dictionary<string, object> p = new Dictionary<string, object>();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                for (int i = 0; i < frmData.RetData.Rows.Count; i++)
                {

                    object xq = frmData.RetData.Rows[i]["检测项类型"];
                    object xw = frmData.RetData.Rows[i]["检测项编号"];
                    object xe = frmData.RetData.Rows[i]["检测项名称"];
                    object lx = frmData.RetData.Rows[i]["检测项类型名称"];
                    object pd = frmData.RetData.Rows[i]["判断标准"];
                    object cl = frmData.RetData.Rows[i]["测量标准"];
                    object dw = frmData.RetData.Rows[i]["单位"];
                    object xr = frmData.RetData.Rows[i]["试样数量"];
                    object xt = frmData.RetData.Rows[i]["引用级别"];
                    object xy = frmData.RetData.Rows[i]["通用公式代号"];
                    object tygsmc = frmData.RetData.Rows[i]["通用公式名称"];
                    object xu = frmData.RetData.Rows[i]["自定义公式代号"];
                    object zdygsmc = frmData.RetData.Rows[i]["自定义公式名称"];
                    object xi = frmData.RetData.Rows[i]["类型"];
                    object xp = frmData.RetData.Rows[i]["备注"];
                    //新增的三个（AQL级别，AC值，RE值）
                    object aql = frmData.RetData.Rows[i]["AQL级别"];
                    DataTable ddd = new DataTable();
                    for (int count = 0; count < dgvTesting.Columns.Count; count++)
                    {
                        DataColumn dc = new DataColumn(dgvTesting.Columns[count].Name.ToString());
                        ddd.Columns.Add(dc);
                    }
                    for (int count = 0; count < dgvTesting.Rows.Count; count++)
                    {
                        DataRow dr = ddd.NewRow();
                        for (int countsub = 0; countsub < dgvTesting.Columns.Count; countsub++)
                        {
                            dr[countsub] = Convert.ToString(dgvTesting.Rows[count].Cells[countsub].Value);
                        }
                        ddd.Rows.Add(dr);
                    }
                    DataRow[] dcl = ddd.Select($"testitem_code_1='{frmData.RetData.Rows[i]["检测项编号"].ToString()}'");

                    if (dcl.Length == 0)
                    {
                        DataRow dr = dtt.NewRow();
                        dr["testtype_no_1"] = xq;
                        dr["testitem_code_1"] = xw;
                        dr["testitem_name_1"] = xe;
                        dr["testtype_name_1"] = lx;
                        dr["check_item_1"] = pd;
                        dr["check_value_1"] = cl;
                        dr["unit_1"] = dw;
                        dr["reference_level_1"] = xt;
                        dr["sample_num_1"] = xr;
                        dr["currency_formula_1"] = xy;
                        dr["currency_formula_name_1"] = tygsmc;
                        dr["custom_formula_1"] = xu;
                        dr["custom_formula_name_1"] = zdygsmc;
                        dr["remarks_1"] = xp;
                        dr["check_type_1"] = xi;
                        //新增的三个（AQL级别，AC值，RE值）
                        dr["AQL_LEVEL_1"] = aql;
                        dtt.Rows.Add(dr);
                    }
                }
                p.Add("bdm_qualitytest_item", dtt);
                p.Add("general_testtype_no", yq);
                p.Add("qid", qid);
                p.Add("secondary_category_no", did);

                try
                {
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                  Program.Client.APIURL,
                                                  "SJ_QCMAPI",//类库名
                                                  "SJ_QCMAPI.Generalquality",//类名
                                                  "InsertBDM_QUALITYTEST_ITEM",//方法名
                                                  Program.Client.UserToken,//token
                                                  Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                    LoadPage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 返回dgv单元多语言格下拉框
        /// </summary>
        /// <returns></returns>
        public DataGridViewComboBoxColumn GetDGVComboBox()
        {
            #region 查询枚举
            List<string> lst_enum_type = new List<string>();
            lst_enum_type.Add("enum_judge_symbol");
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
            #endregion
            DataGridViewComboBoxColumn com = new DataGridViewComboBoxColumn();
            com.HeaderText = "判断标准";
            com.Name = "判断标准";

            DataTable dt = new DataTable();
            dt.Columns.Add("code");
            dt.Columns.Add("value");
            DataRow dr = dt.NewRow();
            //结果引用级别
            com.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_judge_symbol"].ToString());
            com.DisplayMember = "enum_value";
            com.ValueMember = "enum_code";

            return com;
        }

        /// <summary>
        /// 返回测量标准
        /// </summary>
        /// <returns></returns>
        public DataGridViewComboBoxColumn GetValue(string code)
        {
            #region 查询枚举
            Dictionary<string, object> lst_enum_type = new Dictionary<string, object>();
            lst_enum_type.Add("code", code);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJ_QCMAPI",//类库名
                                       "SJ_QCMAPI.Generalquality",//类名
                                       "GetValue",//方法名
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());

            #endregion
            DataGridViewComboBoxColumn com = new DataGridViewComboBoxColumn();
            com.HeaderText = "测量标准";
            com.Name = "测量标准";

            if (dic.Count == 0)
            {
                return com;
            }
            DataTable dt = new DataTable();
            //dt.Columns.Add("code");
            //dt.Columns.Add("value");
            //DataRow dr = dt.NewRow();
            //结果引用级别
            DataTable tt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["values"].ToString());
            if (tt.Rows.Count <= 0)
            {
                return com;
            }
            com.DataSource = tt;
            com.DisplayMember = "value";
            com.ValueMember = "value";
            return com;
        }

        //测试项测量标准
        public DataTable GetTestValue(string code)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> lst_enum_type = new Dictionary<string, object>();
                lst_enum_type.Add("code", code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.Generalquality",//类名
                                           "GetTestValue",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                if (dic.Count > 0 && !string.IsNullOrEmpty(dic["values"].ToString()))
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["values"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        //外观检查项测量标准
        public DataTable GetAppearanceValue(string code)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> lst_enum_type = new Dictionary<string, object>();
                lst_enum_type.Add("code", code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.Generalquality",//类名
                                           "GetAppearanceValue",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                if (dic.Count > 0 && !string.IsNullOrEmpty(dic["values"].ToString()))
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["values"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        //试穿检测标准
        public DataTable GetTryOnValue(string code)
        {
            DataTable dt = new DataTable();
            try
            {
                Dictionary<string, object> lst_enum_type = new Dictionary<string, object>();
                lst_enum_type.Add("code", code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.Generalquality",//类名
                                           "GetTryOnValue",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                if (dic.Count > 0 && !string.IsNullOrEmpty(dic["values"].ToString()))
                {
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["values"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }


        public DataTable GetDGVComboBox1()
        {
            #region 查询枚举
            List<string> lst_enum_type = new List<string>();
            lst_enum_type.Add("enum_judge_symbol");
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
            #endregion

            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_judge_symbol"].ToString());


            return dt;
        }

        /// <summary>
        /// 添加外观测试项数据
        /// </summary>
        public void BDM_APTESTITEM_M()
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            string sql = @" SELECT
	aptestitem_code AS 检测项编号,
	aptestitem_name AS 检测项名称,
    testtype_no AS 检测项类型,
    testtype_name AS 检测项类型名称,
    AQL_LEVEL as AQL级别,
	'' AS 判断标准,
	'' AS 测量标准,
    sample_num as 试样数量,
	reference_level AS 引用级别,
	remarks AS 备注 
FROM
bdm_aptestitem_m";

            FrmSelectData frmData = new FrmSelectData(sql, false, Program.Client,"R");
            frmData.ShowDialog();

            Dictionary<string, object> p = new Dictionary<string, object>();
            DataTable dyy = new DataTable();
            dyy.Columns.Add("testitem_code_2");
            dyy.Columns.Add("testitem_name_2");
            dyy.Columns.Add("testtype_no_2");
            dyy.Columns.Add("testtype_name_2");
            dyy.Columns.Add("check_item_2");
            dyy.Columns.Add("check_value_2");
            dyy.Columns.Add("reference_level_2");
            dyy.Columns.Add("remarks_2");
            dyy.Columns.Add("sample_num_2");
            //新增的三个（AQL级别，AC值，RE值）
            dyy.Columns.Add("AQL_LEVEL_2");
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                for (int i = 0; i < frmData.RetData.Rows.Count; i++)
                {
                    object xw = frmData.RetData.Rows[i]["检测项编号"];
                    object xe = frmData.RetData.Rows[i]["检测项名称"];
                    object lx = frmData.RetData.Rows[i]["检测项类型"];
                    object lxmc = frmData.RetData.Rows[i]["检测项类型名称"];
                    object pd = frmData.RetData.Rows[i]["判断标准"];
                    object cl = frmData.RetData.Rows[i]["测量标准"];
                    object xt = frmData.RetData.Rows[i]["引用级别"];
                    object xp = frmData.RetData.Rows[i]["备注"];
                    object sl = frmData.RetData.Rows[i]["试样数量"];
                    //新增的三个（AQL级别，AC值，RE值）
                    object qal = frmData.RetData.Rows[i]["AQL级别"];
                    DataTable ddd = new DataTable();
                    for (int count = 0; count < dgvAppearance.Columns.Count; count++)
                    {
                        DataColumn dc = new DataColumn(dgvAppearance.Columns[count].Name.ToString());
                        ddd.Columns.Add(dc);
                    }
                    for (int count = 0; count < dgvAppearance.Rows.Count; count++)
                    {
                        DataRow dr = ddd.NewRow();
                        for (int countsub = 0; countsub < dgvAppearance.Columns.Count; countsub++)
                        {
                            dr[countsub] = Convert.ToString(dgvAppearance.Rows[count].Cells[countsub].Value);
                        }
                        ddd.Rows.Add(dr);
                    }


                    DataRow[] dcl = ddd.Select($"testitem_code_2='{frmData.RetData.Rows[i]["检测项编号"].ToString()}'");
                    if (dcl.Length == 0)
                    {
                        DataRow dr = dyy.NewRow();
                        dr["testitem_code_2"] = xw;
                        dr["testitem_name_2"] = xe;
                        dr["testtype_name_2"] = lxmc;
                        dr["testtype_no_2"] = lx;
                        dr["check_item_2"] = pd;
                        dr["check_value_2"] = cl;
                        dr["reference_level_2"] = xt;
                        dr["remarks_2"] = xp;
                        dr["sample_num_2"] = sl;
                        //新增的三个（AQL级别，AC值，RE值）
                        dr["AQL_LEVEL_2"] = qal;
                        dyy.Rows.Add(dr);
                    }
                }
                p.Add("bdm_qualityaptest_item", dyy);
                p.Add("general_testtype_no", yq);
                p.Add("qid", qid);
                p.Add("secondary_category_no", did);

                try
                {
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                   Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                   "SJ_QCMAPI.Generalquality",//类名
                                                   "InsertBDM_QUALITYAPTEST_ITEM",//方法名
                                                   Program.Client.UserToken,//token
                                                   Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                    LoadPage2();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        /// <summary>
        /// 添加试穿检验项数据
        /// </summary>
        public void BDM_TNTESTITEM_M()
        {
            //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            string sql = @" select tntestitem_code as 检测项编号,tntestitem_name as 检测项名称,sample_num as 试样数量,AQL_LEVEL as AQL级别,testtype_no as 检测项类型,testtype_name as 检测项类型名称,'' AS 判断标准,'' AS 测量标准,reference_level as 引用级别,remarks as 备注 from bdm_tntestitem_m ";

            FrmSelectData frmData = new FrmSelectData(sql, false, Program.Client,"R");
            frmData.ShowDialog();

            Dictionary<string, object> p = new Dictionary<string, object>();
            DataTable duu = new DataTable();
            duu.Columns.Add("testitem_code_3");
            duu.Columns.Add("testitem_name_3");
            duu.Columns.Add("testtype_no_3");
            duu.Columns.Add("testtype_name_3");
            duu.Columns.Add("check_item_3");
            duu.Columns.Add("check_value_3");
            duu.Columns.Add("reference_level_3");
            duu.Columns.Add("remarks_3");
            duu.Columns.Add("sample_num_3");
            //新增的三个（AQL级别，AC值，RE值）
            duu.Columns.Add("AQL_LEVEL_3");

            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                for (int i = 0; i < frmData.RetData.Rows.Count; i++)
                {
                    object xw = frmData.RetData.Rows[i]["检测项编号"];
                    object xe = frmData.RetData.Rows[i]["检测项名称"];
                    object lx = frmData.RetData.Rows[i]["检测项类型"];
                    object lxmc = frmData.RetData.Rows[i]["检测项类型名称"];
                    object pd = frmData.RetData.Rows[i]["判断标准"];
                    object cl = frmData.RetData.Rows[i]["测量标准"];
                    object xt = frmData.RetData.Rows[i]["引用级别"];
                    object xp = frmData.RetData.Rows[i]["备注"];
                    object sl = frmData.RetData.Rows[i]["试样数量"];
                    object aql = frmData.RetData.Rows[i]["AQL级别"];
                    object ac = frmData.RetData.Rows[i]["引用级别"];
                    object re = frmData.RetData.Rows[i]["备注"];
                    //新增的三个（AQL级别，AC值，RE值）

                    DataTable ddd = new DataTable();
                    for (int count = 0; count < dgvTryOn.Columns.Count; count++)
                    {
                        DataColumn dc = new DataColumn(dgvTryOn.Columns[count].Name.ToString());
                        ddd.Columns.Add(dc);
                    }
                    for (int count = 0; count < dgvTryOn.Rows.Count; count++)
                    {
                        DataRow dr = ddd.NewRow();
                        for (int countsub = 0; countsub < dgvTryOn.Columns.Count; countsub++)
                        {
                            dr[countsub] = Convert.ToString(dgvTryOn.Rows[count].Cells[countsub].Value);
                        }
                        ddd.Rows.Add(dr);
                    }

                    DataRow[] dcl = ddd.Select($"testitem_code_3='{frmData.RetData.Rows[i]["检测项编号"].ToString()}'");
                    if (dcl.Length == 0)
                    {
                        DataRow dr = duu.NewRow();
                        dr["testitem_code_3"] = xw;
                        dr["testitem_name_3"] = xe;
                        dr["testtype_no_3"] = lx;
                        dr["testtype_name_3"] = lxmc;
                        dr["check_item_3"] = pd;
                        dr["check_value_3"] = cl;
                        dr["reference_level_3"] = xt;
                        dr["remarks_3"] = xp;
                        dr["sample_num_3"] = sl;
                        //新增的三个（AQL级别，AC值，RE值）
                        dr["AQL_LEVEL_3"] = aql;
                        duu.Rows.Add(dr);
                    }
                }
                p.Add("bdm_qualitytntest_item", duu);
                p.Add("general_testtype_no", yq);
                p.Add("qid", qid);
                p.Add("secondary_category_no", did);
                try
                {
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                   Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                   "SJ_QCMAPI.Generalquality",//类名
                                                   "InsertBDM_QUALITYTNTEST_ITEM",//方法名
                                                   Program.Client.UserToken,//token
                                                   Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        throw new Exception(ret.ErrMsg);
                    }
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                    LoadPage3();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        public void InsertBDM_QUALITYTEST_ITEM()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                #region 参数
                #region 测试项目
                DataTable dtt = new DataTable();
                dtt.Columns.Add("testtype_no_1");
                dtt.Columns.Add("testitem_code_1");
                dtt.Columns.Add("testitem_name_1");
                dtt.Columns.Add("testtype_name_1");
                dtt.Columns.Add("check_item_1");
                dtt.Columns.Add("check_value_1");
                dtt.Columns.Add("unit_1");
                dtt.Columns.Add("reference_level_1");
                dtt.Columns.Add("sample_num_1");
                dtt.Columns.Add("currency_formula_1");
                dtt.Columns.Add("custom_formula_1");
                dtt.Columns.Add("remarks_1");
                foreach (DataGridViewRow dgvr in dgvTesting.Rows)
                {
                    if (string.IsNullOrEmpty(dgvr.Cells["check_item_1"].Value.ToString()) || string.IsNullOrEmpty(dgvr.Cells["check_value_1"].Value.ToString()))
                    {
                        MessageBox.Show("测试项目:判断标准跟测量标准不能为空！");
                        return;
                    }
                    DataRow dr = dtt.NewRow();
                    dr["testtype_no_1"] = dgvr.Cells["testtype_no_1"].Value.ToString();
                    dr["testitem_code_1"] = dgvr.Cells["testitem_code_1"].Value.ToString();
                    dr["testitem_name_1"] = dgvr.Cells["testitem_name_1"].Value.ToString();
                    dr["testtype_name_1"] = dgvr.Cells["testtype_name_1"].Value.ToString();
                    dr["check_item_1"] = dgvr.Cells["check_item_1"].Value.ToString();
                    dr["check_value_1"] = dgvr.Cells["check_value_1"].Value.ToString();
                    dr["unit_1"] = dgvr.Cells["unit_1"].Value.ToString();
                    dr["reference_level_1"] = dgvr.Cells["reference_level_1"].Value.ToString();
                    dr["sample_num_1"] = dgvr.Cells["sample_num_1"].Value.ToString();
                    dr["currency_formula_1"] = dgvr.Cells["currency_formula_1"].Value.ToString();
                    dr["custom_formula_1"] = dgvr.Cells["custom_formula_1"].Value.ToString();
                    dr["remarks_1"] = dgvr.Cells["remarks_1"].Value.ToString();

                    dtt.Rows.Add(dr);
                }
                #endregion
                #region 外观检测项目
                DataTable dyy = new DataTable();
                dyy.Columns.Add("testitem_code_2");
                dyy.Columns.Add("testitem_name_2");
                dyy.Columns.Add("check_item_2");
                dyy.Columns.Add("check_value_2");
                dyy.Columns.Add("reference_level_2");
                dyy.Columns.Add("remarks_2");

                foreach (DataGridViewRow dgvr in dgvAppearance.Rows)
                {
                    if (string.IsNullOrEmpty(dgvr.Cells["check_item_2"].Value.ToString()) || string.IsNullOrEmpty(dgvr.Cells["check_value_2"].Value.ToString()))
                    {
                        MessageBox.Show("外观检测项目:判断标准跟测量标准不能为空！");
                        return;
                    }
                    DataRow dr = dyy.NewRow();
                    dr["testitem_code_2"] = dgvr.Cells["testitem_code_2"].Value.ToString();
                    dr["testitem_name_2"] = dgvr.Cells["testitem_name_2"].Value.ToString();
                    dr["check_item_2"] = dgvr.Cells["check_item_2"].Value.ToString();
                    dr["check_value_2"] = dgvr.Cells["check_value_2"].Value.ToString();
                    dr["reference_level_2"] = dgvr.Cells["reference_level_2"].Value.ToString();
                    dr["remarks_2"] = dgvr.Cells["remarks_2"].Value.ToString();
                    dyy.Rows.Add(dr);
                }
                #endregion
                #region 试穿检测项目
                DataTable duu = new DataTable();
                duu.Columns.Add("testitem_code_3");
                duu.Columns.Add("testitem_name_3");
                duu.Columns.Add("check_item_3");
                duu.Columns.Add("check_value_3");
                duu.Columns.Add("reference_level_3");
                duu.Columns.Add("remarks_3");
                foreach (DataGridViewRow dgvr in dgvTryOn.Rows)
                {
                    if (string.IsNullOrEmpty(dgvr.Cells["check_item_3"].Value.ToString()) || string.IsNullOrEmpty(dgvr.Cells["check_value_3"].Value.ToString()))
                    {
                        MessageBox.Show("试穿检测项目:判断标准跟测量标准不能为空！");
                        return;
                    }
                    DataRow dr = duu.NewRow();
                    dr["testitem_code_3"] = dgvr.Cells["testitem_code_3"].Value.ToString();
                    dr["testitem_name_3"] = dgvr.Cells["testitem_name_3"].Value.ToString();
                    dr["check_item_3"] = dgvr.Cells["check_item_3"].Value.ToString();
                    dr["check_value_3"] = dgvr.Cells["check_value_3"].Value.ToString();
                    dr["reference_level_3"] = dgvr.Cells["reference_level_3"].Value.ToString();
                    dr["remarks_3"] = dgvr.Cells["remarks_3"].Value.ToString();
                    duu.Rows.Add(dr);
                }
                #endregion

                p.Add("bdm_qualitytest_item", dtt);
                p.Add("bdm_qualityaptest_item", dyy);
                p.Add("bdm_qualitytntest_item", duu);
                p.Add("general_testtype_no", yq);
                p.Add("qid", qid);
                p.Add("secondary_category_no", did);


                #endregion

                #region 找接口

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                   Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                   "SJ_QCMAPI.Generalquality",//类名
                                                   "UpdataSJ",//方法名
                                                   Program.Client.UserToken,//token
                                                   Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                LoadPage();
                LoadPage2();
                LoadPage3();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 初始化查询测试项
        /// </summary>
        public void GetSelect(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("TableName", "bdm_qualitytest_item");
                data.Add("did", did);
                data.Add("qid", qid);
                data.Add("yq", yq);
                data.Add("testitem_code", txt_code.Text.Trim());
                data.Add("testitem_name", txt_name.Text.Trim());

                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "GetCheck", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dgvTesting.Rows.Count >= 0)
                {
                    dgvTesting.Rows.Clear();
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dgvTesting.Rows.Add();
                        DataGridViewRow dgvr = dgvTesting.Rows[i];
                        dgvr.Cells["testtype_no_1"].Value = dr["testtype_no"].ToString();
                        dgvr.Cells["testitem_code_1"].Value = dr["testitem_code"].ToString();
                        dgvr.Cells["testitem_name_1"].Value = dr["testitem_name"].ToString();
                        dgvr.Cells["testtype_name_1"].Value = dr["testtype_name"].ToString();
                        dgvr.Cells["check_item_1"].Value = dr["check_item"].ToString();
                        dgvr.Cells["check_value_1"].Value = dr["check_value"].ToString();
                        dgvr.Cells["unit_1"].Value = dr["unit"].ToString();
                        dgvr.Cells["reference_level_1"].Value = dr["reference_level"].ToString();
                        dgvr.Cells["sample_num_1"].Value = dr["sample_num"].ToString();
                        dgvr.Cells["currency_formula_1"].Value = dr["currency_formula"].ToString();
                        dgvr.Cells["currency_formula_name_1"].Value = dr["通用公式名称"].ToString();
                        dgvr.Cells["custom_formula_1"].Value = dr["custom_formula"].ToString();
                        dgvr.Cells["custom_formula_name_1"].Value = dr["自定义公式名称"].ToString();
                        dgvr.Cells["remarks_1"].Value = dr["remarks"].ToString();
                        dgvr.Cells["AQL_LEVEL_1"].Value = dr["AQL_LEVEL"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dgvTesting);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            this.dgvTesting.ClearSelection();
            this.dgvTesting.Columns["operation1"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        /// <summary>
        /// 初始化查询外观检测
        /// </summary>
        public void GetSelect2(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("TableName", "bdm_qualityaptest_item");
                data.Add("did", did);
                data.Add("qid", qid);
                data.Add("yq", yq);
                data.Add("testitem_code", txt_code.Text.Trim());
                data.Add("testitem_name", txt_name.Text.Trim());
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "GetCheck2", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dgvAppearance.Rows.Count >= 0)
                {
                    dgvAppearance.Rows.Clear();
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dgvAppearance.Rows.Add();
                        DataGridViewRow dgvr = dgvAppearance.Rows[i];
                        dgvr.Cells["testitem_code_2"].Value = dr["检测项编号"].ToString();
                        dgvr.Cells["testitem_name_2"].Value = dr["检测项名称"].ToString();
                        dgvr.Cells["testtype_no_2"].Value = dr["检测项类型"].ToString();
                        dgvr.Cells["testtype_name_2"].Value = dr["检测项类型名称"].ToString();
                        dgvr.Cells["check_item_2"].Value = dr["判断标准"].ToString();
                        dgvr.Cells["check_value_2"].Value = dr["测量标准"].ToString();
                        dgvr.Cells["reference_level_2"].Value = dr["项目引用级别"].ToString();
                        dgvr.Cells["sample_num_2"].Value = dr["试样数量"].ToString();
                        dgvr.Cells["remarks_2"].Value = dr["备注"].ToString();
                        dgvr.Cells["AQL_LEVEL_2"].Value = dr["AQL等级"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dgvAppearance);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            this.dgvAppearance.ClearSelection();
            this.dgvAppearance.Columns["operation2"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        /// <summary>
        /// 初始化查询试穿
        /// </summary>
        public void GetSelect3(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("TableName", "bdm_qualitytntest_item");
                data.Add("did", did);
                data.Add("qid", qid);
                data.Add("yq", yq);
                data.Add("testitem_code", txt_code.Text.Trim());
                data.Add("testitem_name", txt_name.Text.Trim());
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.Generalquality", "GetCheck3", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                if (dgvTryOn.Rows.Count >= 0)
                {
                    dgvTryOn.Rows.Clear();
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dgvTryOn.Rows.Add();
                        DataGridViewRow dgvr = dgvTryOn.Rows[i];
                        dgvr.Cells["testitem_code_3"].Value = dr["检测项编号"].ToString();
                        dgvr.Cells["testitem_name_3"].Value = dr["检测项名称"].ToString();
                        dgvr.Cells["testtype_no_3"].Value = dr["检测项类型"].ToString();
                        dgvr.Cells["testtype_name_3"].Value = dr["检测项类型名称"].ToString();
                        dgvr.Cells["check_item_3"].Value = dr["判断标准"].ToString();
                        dgvr.Cells["check_value_3"].Value = dr["测量标准"].ToString();
                        dgvr.Cells["reference_level_3"].Value = dr["项目引用级别"].ToString();
                        dgvr.Cells["sample_num_3"].Value = dr["试样数量"].ToString();
                        dgvr.Cells["remarks_3"].Value = dr["备注"].ToString();
                        dgvr.Cells["AQL_LEVEL_3"].Value = dr["AQL等级"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dgvTryOn);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            this.dgvTryOn.ClearSelection();
            this.dgvTryOn.Columns["operation3"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void dgvTesting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0|| e.ColumnIndex < 0)
                return;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dgvTesting.Columns[e.ColumnIndex].Name == "operation1")
                {
                    DataGridViewOperationCell cell = this.dgvTesting.Rows[this.dgvTesting.CurrentRow.Index].Cells["operation1"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("DELETE"))//删除
                    {
                        try
                        {
                            string testitem_code = dgvTesting.Rows[e.RowIndex].Cells["testitem_code_1"].Value.ToString();//检测项编号
                                                                                                                         //请求api的数据展示
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("general_testtype_no", yq);
                            p.Add("qid", qid);
                            p.Add("secondary_category_no", did);
                            p.Add("testitem_code", testitem_code);
                            p.Add("TableName", "bdm_qualitytest_item");
                            #region 找接口
                            if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "是否删除").ToString().ToLower() == "ok")
                            {
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                           Program.Client.APIURL,
                                                           "SJ_QCMAPI",//类库名
                                                           "SJ_QCMAPI.Generalquality",//类名
                                                           "DeleteSJ",//方法名
                                                           Program.Client.UserToken,//token
                                                           Newtonsoft.Json.JsonConvert.SerializeObject(p));

                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (!ret.IsSuccess)
                                {
                                    throw new Exception(ret.ErrMsg);
                                }
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("删除成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                                LoadPage();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            if (dgvTesting.Columns[e.ColumnIndex].Name == "check_value_1") // combobox显示条件 
            {
                string testitem_code = dgvTesting.CurrentRow.Cells["testitem_code_1"].Value.ToString();
                DataTable dt_tval = GetTestValue(testitem_code);
                comboBox1.DataSource = dt_tval;
                if (dt_tval != null && dt_tval.Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "value";
                    comboBox1.ValueMember = "value";
                }
                comboBox1.Text = dgvTesting.CurrentCell.Value.ToString(); //对combobox赋值

                Rectangle R = dgvTesting.GetCellDisplayRectangle(dgvTesting.CurrentCell.ColumnIndex, dgvTesting.CurrentCell.RowIndex, false); //获取单元格位置 
                comboBox1.SetBounds(R.X + dgvTesting.Location.X, R.Y + dgvTesting.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                comboBox1.Visible = true;
            }
            else if (dgvTesting.Columns[e.ColumnIndex].Name == "check_item_1")
            {
                DataTable dt_tval = GetDGVComboBox1();
                comboBox1.DataSource = dt_tval;
                if (dt_tval != null && dt_tval.Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "enum_code";
                    comboBox1.ValueMember = "enum_value";
                }
                comboBox1.Text = dgvTesting.CurrentCell.Value.ToString(); //对combobox赋值

                Rectangle R = dgvTesting.GetCellDisplayRectangle(dgvTesting.CurrentCell.ColumnIndex, dgvTesting.CurrentCell.RowIndex, false); //获取单元格位置 
                comboBox1.SetBounds(R.X + dgvTesting.Location.X, R.Y + dgvTesting.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                comboBox1.Visible = true;
            }
            else
                comboBox1.Visible = false;
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvTesting.CurrentCell.Value = comboBox1.SelectedValue.ToString();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvAppearance.CurrentCell.Value = comboBox2.SelectedValue.ToString();
        }
        private void dgvAppearance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dgvAppearance.Columns[e.ColumnIndex].Name == "operation2")
                {
                    DataGridViewOperationCell cell = this.dgvAppearance.Rows[this.dgvAppearance.CurrentRow.Index].Cells["operation2"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("DELETE"))//删除
                    {;
                        try
                        {
                            string testitem_code = dgvAppearance.Rows[e.RowIndex].Cells["testitem_code_2"].Value.ToString();//检测项编号
                                                                                                                            //请求api的数据展示
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("general_testtype_no", yq);
                            p.Add("qid", qid);
                            p.Add("secondary_category_no", did);
                            p.Add("testitem_code", testitem_code);
                            p.Add("TableName", "bdm_qualityaptest_item");
                            #region 找接口
                            if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "是否删除").ToString().ToLower() == "ok")
                            {
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                           Program.Client.APIURL,
                                                           "SJ_QCMAPI",//类库名
                                                           "SJ_QCMAPI.Generalquality",//类名
                                                           "DeleteSJ",//方法名
                                                           Program.Client.UserToken,//token
                                                           Newtonsoft.Json.JsonConvert.SerializeObject(p));

                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (!ret.IsSuccess)
                                {
                                    throw new Exception(ret.ErrMsg);
                                }
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("删除成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                                LoadPage2();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }

            }
            if (dgvAppearance.Columns[e.ColumnIndex].Name == "check_value_2") // combobox显示条件 
            {
                string testitem_code = dgvAppearance.CurrentRow.Cells["testitem_code_2"].Value.ToString();
                DataTable dt_tval = GetAppearanceValue(testitem_code);
                comboBox2.DataSource = dt_tval;
                if (dt_tval != null && dt_tval.Rows.Count > 0)
                {
                    comboBox2.DisplayMember = "test_standard";
                    comboBox2.ValueMember = "test_standard";
                }
                comboBox2.Text = dgvAppearance.CurrentCell.Value.ToString(); //对combobox赋值

                Rectangle R = dgvAppearance.GetCellDisplayRectangle(dgvAppearance.CurrentCell.ColumnIndex, dgvAppearance.CurrentCell.RowIndex, false); //获取单元格位置 
                comboBox2.SetBounds(R.X + dgvAppearance.Location.X, R.Y + dgvAppearance.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                comboBox2.Visible = true;
            }
            else if (dgvAppearance.Columns[e.ColumnIndex].Name == "check_item_2")
            {
                DataTable dt_tval = GetDGVComboBox1();
                comboBox2.DataSource = dt_tval;
                if (dt_tval != null && dt_tval.Rows.Count > 0)
                {
                    comboBox2.DisplayMember = "enum_code";
                    comboBox2.ValueMember = "enum_value";
                }
                comboBox2.Text = dgvAppearance.CurrentCell.Value.ToString(); //对combobox赋值

                Rectangle R = dgvAppearance.GetCellDisplayRectangle(dgvAppearance.CurrentCell.ColumnIndex, dgvAppearance.CurrentCell.RowIndex, false); //获取单元格位置 
                comboBox2.SetBounds(R.X + dgvAppearance.Location.X, R.Y + dgvAppearance.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                comboBox2.Visible = true;
            }
            else
                comboBox2.Visible = false;
        }

        private void dgvTryOn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dgvTryOn.Columns[e.ColumnIndex].Name == "operation3")
                {
                    DataGridViewOperationCell cell = this.dgvTryOn.Rows[this.dgvTryOn.CurrentRow.Index].Cells["operation3"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("DELETE"))//删除
                    {
                        try
                        {
                            string testitem_code = dgvTryOn.Rows[e.RowIndex].Cells["testitem_code_3"].Value.ToString();//检测项编号
                                                                                                                       //请求api的数据展示
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("general_testtype_no", yq);
                            p.Add("qid", qid);
                            p.Add("secondary_category_no", did);
                            p.Add("testitem_code", testitem_code);
                            p.Add("TableName", "bdm_qualitytntest_item");
                            #region 找接口
                            if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "是否删除").ToString().ToLower() == "ok")
                            {
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                           Program.Client.APIURL,
                                                           "SJ_QCMAPI",//类库名
                                                           "SJ_QCMAPI.Generalquality",//类名
                                                           "DeleteSJ",//方法名
                                                           Program.Client.UserToken,//token
                                                           Newtonsoft.Json.JsonConvert.SerializeObject(p));

                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (!ret.IsSuccess)
                                {
                                    throw new Exception(ret.ErrMsg);
                                }
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("删除成功!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                                LoadPage3();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
            }
            if (dgvTryOn.Columns[e.ColumnIndex].Name == "check_value_3") // combobox显示条件 
            {
                string testitem_code = dgvTryOn.CurrentRow.Cells["testitem_code_3"].Value.ToString();
                DataTable dt_tval = GetTryOnValue(testitem_code);
                comboBox3.DataSource = dt_tval;
                if (dt_tval != null && dt_tval.Rows.Count > 0)
                {
                    comboBox3.DisplayMember = "test_standard";
                    comboBox3.ValueMember = "test_standard";
                }
                comboBox3.Text = dgvTryOn.CurrentCell.Value.ToString(); //对combobox赋值

                Rectangle R = dgvTryOn.GetCellDisplayRectangle(dgvTryOn.CurrentCell.ColumnIndex, dgvTryOn.CurrentCell.RowIndex, false); //获取单元格位置 
                comboBox3.SetBounds(R.X + dgvTryOn.Location.X, R.Y + dgvTryOn.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                comboBox3.Visible = true;
            }
            else if (dgvTryOn.Columns[e.ColumnIndex].Name == "check_item_3")
            {
                DataTable dt_tval = GetDGVComboBox1();
                comboBox3.DataSource = dt_tval;
                if (dt_tval != null && dt_tval.Rows.Count > 0)
                {
                    comboBox3.DisplayMember = "enum_code";
                    comboBox3.ValueMember = "enum_value";
                }
                comboBox3.Text = dgvTryOn.CurrentCell.Value.ToString(); //对combobox赋值

                Rectangle R = dgvTryOn.GetCellDisplayRectangle(dgvTryOn.CurrentCell.ColumnIndex, dgvTryOn.CurrentCell.RowIndex, false); //获取单元格位置 
                comboBox3.SetBounds(R.X + dgvTryOn.Location.X, R.Y + dgvTryOn.Location.Y, R.Width, R.Height); //重新定位combobox.中间有坐标位置的转换 
                comboBox3.Visible = true;
            }
            else
                comboBox3.Visible = false;
        }
        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dgvTryOn.CurrentCell.Value = comboBox3.SelectedValue.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPage();
            LoadPage2();
            LoadPage3();
        }

        private void F_BDM_QualityStandard_Item_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
        }

        private void dgvTesting_Scroll(object sender, ScrollEventArgs e)
        {
            comboBox1.Visible = false;
        }

        private void dgvAppearance_Scroll(object sender, ScrollEventArgs e)
        {
            comboBox2.Visible = false;
        }

        private void dgvTryOn_Scroll(object sender, ScrollEventArgs e)
        {
            comboBox3.Visible = false;
        }

        private void F_BDM_QualityStandard_Item_Load(object sender, EventArgs e)
        {
        }
    }
}
