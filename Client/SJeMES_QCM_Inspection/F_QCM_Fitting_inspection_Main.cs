using DataGrid.DataGridViewCustomColumn;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM_Inspection
{
    public partial class F_QCM_Fitting_inspection_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 物料条码
        /// </summary>
        private string material_noS;
        /// <summary>
        /// 联动下拉框第三级要用的两条件之一
        /// </summary>
        private string Prod_NO = string.Empty;
        /// <summary>
        /// 用于接收上一级的ART选择的dt做下一级的准备
        /// </summary>
        public DataTable dts = null;
        /// <summary>
        /// 厂商名称
        /// </summary>
        private string plantarea_name = string.Empty;
        /// <summary>
        /// 产线名称
        /// </summary>
        private string productionline_name = string.Empty;

        public class department
        {
            public string department_no { get; set; }
            public string department_name { get; set; }

        }

        public F_QCM_Fitting_inspection_Main()
        {
            InitializeComponent();
            GenClass.AutoSizeColumn(dataGridView1);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }


        private void btn_Select_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_material_no.Text))
                {
                    //带入物料条码
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("data", txt_material_no.Text);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                               "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.InspectionTableView",//类名
                                                "BDM_RD_ITEM_Select_item_no",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    dts = dt;
                    if (dt.Rows.Count > 0)
                    {
                        txt_material_name.Text = dt.Rows[0]["NAME_S"].ToString();
                        txt_material_no.Text = dt.Rows[0]["ITEM_NO"].ToString();
                        material_noS = dt.Rows[0]["ITEM_NO"].ToString();
                        cbo_PARENT_ITEM_NO.DataSource = dt;
                        cbo_PARENT_ITEM_NO.ValueMember = "PARENT_ITEM_NO";
                        cbo_PARENT_ITEM_NO.DisplayMember = "PARENT_ITEM_NO";
                        cbo_category_no.DataSource = null;

                        if (!string.IsNullOrEmpty(dt.Rows[0]["PARENT_ITEM_NO"].ToString()))
                        {
                            dataGridView1.Rows.Clear();
                            TwoXLK();
                        }
                        else
                        {
                            dataGridView1.Rows.Clear();
                            TYXLK();
                        }
                    }
                    else
                    {
                        MessageBox.Show("物料条码不存在");
                        dataGridView1.Rows.Clear();
                        material_noS = null;
                        txt_material_name.Text = null;
                        txt_material_no.Text = null;
                        cbo_category_no.DataSource = null;
                        cbo_PARENT_ITEM_NO.DataSource = null;
                        cbo_general_testtype_no.DataSource = null;
                    }
                }
                else
                {
                    MessageBox.Show("物料条码不能为空");
                    dataGridView1.Rows.Clear();
                    material_noS = null;
                    txt_material_name.Text = null;
                    txt_material_no.Text = null;
                    cbo_category_no.DataSource = null;
                    cbo_PARENT_ITEM_NO.DataSource = null;
                    cbo_general_testtype_no.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public void TYXLK()
        {
            //通用检测标准下拉框
            try
            {
                List<string> lst_enum_type = new List<string>();
                lst_enum_type.Add("enum_testitem_type");
                lst_enum_type.Add("enum_general_formula");
                lst_enum_type.Add("enum_ref_level");
                lst_enum_type.Add("enum_formula_type");

                //查询枚举
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.InspectionEnum",//类名
                                           "Getbdm_general_testtype_m_Meun",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                //选择通用公式
                cbo_general_testtype_no.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data1"].ToString());
                cbo_general_testtype_no.ValueMember = "general_testtype_no";
                cbo_general_testtype_no.DisplayMember = "general_testtype_name";
                cbo_general_testtype_no.SelectedIndex = -1;

                //取通用检测下拉框的值(一级)
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 二级下拉框内容
        /// </summary>
        public void TwoXLK()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PARENT_ITEM_NO", cbo_PARENT_ITEM_NO.SelectedValue);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.InspectionEnum",//类名
                                            "Getbdm_general_testtype_m_Nos",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data1"].ToString());
                if (dt.Rows.Count > 0)
                {
                    cbo_general_testtype_no.DataSource = dt;
                    cbo_general_testtype_no.ValueMember = "GENERAL_TESTTYPE_NO";
                    cbo_general_testtype_no.DisplayMember = "GENERAL_TESTTYPE_NAME";
                    Prod_NO = dt.Rows[0]["Prod_NO"].ToString();
                    cbo_general_testtype_no.SelectedIndex = -1;
                }
                else
                {
                    cbo_general_testtype_no.DataSource = null;
                }
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 三级下拉框内容
        /// </summary>
        public void ThreeXLK()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("prod_no", cbo_PARENT_ITEM_NO.SelectedValue);
                p.Add("general_testtype_no", cbo_general_testtype_no.SelectedValue);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.InspectionEnum",//类名
                                            "Getbdm_general_testtype_m_Nosd",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data1"].ToString());
                if (dt.Rows.Count > 0)
                {
                    cbo_category_no.DataSource = dt;
                    cbo_category_no.ValueMember = "CATEGORY_NO";
                    cbo_category_no.DisplayMember = "CATEGORY_NAME";
                    cbo_category_no.SelectedIndex = -1;
                }
                else
                {
                    cbo_category_no.DataSource = null;
                }
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void txt_material_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Select_Click(sender, e);
            }
        }

        private void txt_plantarea_no_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "select PLANT_CODE as  厂区厂商编号, PLANT_NAME as  厂区厂商名称 from base001a1 union select SUPPLIERS_CODE  厂区厂商编号,SUPPLIERS_NAME 厂区厂商名称 from base003m";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_plantarea_no.Text = frmData.RetData.Rows[0]["厂区厂商编号"].ToString();
                plantarea_name = frmData.RetData.Rows[0]["厂区厂商名称"].ToString();
            }
        }

        //产线带出
        private void txt_productionline_no_Click(object sender, EventArgs e)
        {
            try
            {
                    //当前窗体名称+"_"+当前方法名称
                    string sql = $@"select productionline_no as 产线编号,productionline_name as 产线名称,REMARKS as 备注 from bdm_quality_department_d";
                    FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                    frmData.ShowDialog();
                    if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                    {
                        txt_productionline_no.Text = frmData.RetData.Rows[0]["产线编号"].ToString();
                        productionline_name = frmData.RetData.Rows[0]["产线名称"].ToString();
                    }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //扫描员工二维码信息
        private void txt_User_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txt_User.Text))
                    {
                        //带入物料条码
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("data", txt_User.Text);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.InspectionTableView",//类名
                                                    "BDM_RD_ITEM_Select_Hr001m",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                        DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            txt_Id.Text = dt.Rows[0]["账号"].ToString();
                            txt_Name.Text = dt.Rows[0]["名称"].ToString();
                            txt_Branch.Text = dt.Rows[0]["部门"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("员工二维码不存在");
                            txt_Id.Text = null;
                            txt_Name.Text = null;
                            txt_Branch.Text = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("员工二维码不能为空");
                        txt_Id.Text = null;
                        txt_Name.Text = null;
                        txt_Branch.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
        }

        private void F_QCM_Fitting_inspection_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            #region 下拉框数据绑定
            IList<department> infoList = new List<department>();
            department info1 = new department() { department_no = "QA", department_name = "CR1" };
            department info2 = new department() { department_no = "WBBM04", department_name = "CR2" };
            department info3 = new department() { department_no = "BMDH01", department_name = "CS1" };
            department info4 = new department() { department_no = "BMDH02", department_name = "CS2" };
            department info5 = new department() { department_no = "WBBM03", department_name = "量试" };
            department info6 = new department() { department_no = "WBBM03", department_name = "量产" };
            department info7 = new department() { department_no = "1", department_name = "其他" };
            infoList.Add(info1);
            infoList.Add(info2);
            infoList.Add(info3);
            infoList.Add(info4);
            infoList.Add(info5);
            infoList.Add(info6);
            infoList.Add(info7);
            com_department_no.DataSource = infoList;
            com_department_no.ValueMember = "department_no";
            com_department_no.DisplayMember = "department_name";
            #endregion
            try
            {
                #region 查询枚举
                List<string> lst_enum_type = new List<string>();
                lst_enum_type.Add("enum_document_type");

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.InspectionTableView",//类名
                                           "GetSYS001MDataList",//方法名
                                           Program.Client.UserToken,//token
                                           Newtonsoft.Json.JsonConvert.SerializeObject(lst_enum_type));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                //来源单据下拉框
                cbo_document_type.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_document_type"].ToString());
                cbo_document_type.DisplayMember = "enum_value";
                cbo_document_type.ValueMember = "enum_code";
                cbo_document_type.SelectedIndex = -1;
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cbo_PARENT_ITEM_NO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dts.Rows[0]["PARENT_ITEM_NO"].ToString()))
            {
                TwoXLK();
            }
        }

        private void cbo_general_testtype_no_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                if (cbo_general_testtype_no.SelectedIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(dts.Rows[0]["PARENT_ITEM_NO"].ToString()) && cbo_general_testtype_no.SelectedValue != null)
                    {
                        dataGridView1.Rows.Clear();
                        ThreeXLK();
                    }
                    else
                    {
                        dataGridView1.Rows.Clear();
                        XLK();

                    }

                }
            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public void XLK()
        {
            // 新增测试项数据
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("data", cbo_general_testtype_no.SelectedValue.ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.InspectionEnum",//类名
                                            "Getbdm_general_testtype_m_No",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data1"].ToString());
                //选择通用公式
                if (dt.Rows.Count > 0)
                {
                    cbo_category_no.DataSource = dt;
                    cbo_category_no.ValueMember = "aa";
                    cbo_category_no.DisplayMember = "bb";
                    cbo_category_no.SelectedIndex = -1;
                    //取第二下拉框的值（二级）
                    //Table();
                }
                else
                {
                    cbo_category_no.DataSource = null;
                }
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

            }
            catch (Exception ex)
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("保存成功!");
        }



        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        public static DataTable InitializeData()
        {
            #region 初始化数据
            DataTable dt = new DataTable();
            dt.Columns.Add("number");
            dt.Columns.Add("testtype_name");
            dt.Columns.Add("testitem_code");
            dt.Columns.Add("testitem_name");
            dt.Columns.Add("t_check_item");
            dt.Columns.Add("t_check_value");
            dt.Columns.Add("reference_level");
            dt.Columns.Add("sample_num");
            dt.Rows.Add();
            dt.Rows[0]["number"] = "1";
            dt.Rows[0]["testtype_name"] = "试穿测试";
            dt.Rows[0]["testitem_code"] = "FT-01";
            dt.Rows[0]["testitem_name"] = "鞋头位高度";
            dt.Rows[0]["t_check_item"] = "太低--太高";
            dt.Rows[0]["t_check_value"] = "不能有断面";
            dt.Rows[0]["reference_level"] = "ART";
            dt.Rows[0]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[1]["number"] = "2";
            dt.Rows[1]["testtype_name"] = "试穿测试";
            dt.Rows[1]["testitem_code"] = "FT-02";
            dt.Rows[1]["testitem_name"] = "鞋头位宽度";
            dt.Rows[1]["t_check_item"] = "太低--太高";
            dt.Rows[1]["t_check_value"] = "不能有";
            dt.Rows[1]["reference_level"] = "ART";
            dt.Rows[1]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[2]["number"] = "3";
            dt.Rows[2]["testtype_name"] = "试穿测试";
            dt.Rows[2]["testitem_code"] = "FT-03";
            dt.Rows[2]["testitem_name"] = "前掌位容积";
            dt.Rows[2]["t_check_item"] = "太低--太高";
            dt.Rows[2]["t_check_value"] = "不能有突横";
            dt.Rows[2]["reference_level"] = "";
            dt.Rows[2]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[3]["number"] = "4";
            dt.Rows[3]["testtype_name"] = "试穿测试";
            dt.Rows[3]["testitem_code"] = "FT-04";
            dt.Rows[3]["testitem_name"] = "鞋口位容积";
            dt.Rows[3]["t_check_item"] = "太低--太高";
            dt.Rows[3]["t_check_value"] = "不能有断面";
            dt.Rows[3]["reference_level"] = "";
            dt.Rows[3]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[4]["number"] = "5";
            dt.Rows[4]["testtype_name"] = "试穿测试";
            dt.Rows[4]["testitem_code"] = "FT-05";
            dt.Rows[4]["testitem_name"] = "鞋垫、腰海绵";
            dt.Rows[4]["t_check_item"] = "太低--太高";
            dt.Rows[4]["t_check_value"] = "不能有";
            dt.Rows[4]["reference_level"] = "ART";
            dt.Rows[4]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[5]["number"] = "6";
            dt.Rows[5]["testtype_name"] = "试穿测试";
            dt.Rows[5]["testitem_code"] = "FT-06";
            dt.Rows[5]["testitem_name"] = "鞋舌位开口";
            dt.Rows[5]["t_check_item"] = "太低--太高";
            dt.Rows[5]["t_check_value"] = "不能有突横";
            dt.Rows[5]["reference_level"] = "";
            dt.Rows[5]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[6]["number"] = "7";
            dt.Rows[6]["testtype_name"] = "试穿测试";
            dt.Rows[6]["testitem_code"] = "FT-07";
            dt.Rows[6]["testitem_name"] = "鞋带长度";
            dt.Rows[6]["t_check_item"] = "太低--太高";
            dt.Rows[6]["t_check_value"] = "不能有";
            dt.Rows[6]["reference_level"] = "ART";
            dt.Rows[6]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[7]["number"] = "8";
            dt.Rows[7]["testtype_name"] = "试穿测试";
            dt.Rows[7]["testitem_code"] = "FT-08";
            dt.Rows[7]["testitem_name"] = "领口高度";
            dt.Rows[7]["t_check_item"] = "太低--太高";
            dt.Rows[7]["t_check_value"] = "不能有突横";
            dt.Rows[7]["reference_level"] = "ART";
            dt.Rows[7]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[8]["number"] = "9";
            dt.Rows[8]["testtype_name"] = "试穿测试";
            dt.Rows[8]["testitem_code"] = "FT-09";
            dt.Rows[8]["testitem_name"] = "后跟宽度";
            dt.Rows[8]["t_check_item"] = "太低--太高";
            dt.Rows[8]["t_check_value"] = "不能有突横";
            dt.Rows[8]["reference_level"] = "MW";
            dt.Rows[0]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[9]["number"] = "10";
            dt.Rows[9]["testtype_name"] = "试穿测试";
            dt.Rows[9]["testitem_code"] = "FT_10";
            dt.Rows[9]["testitem_name"] = "后跟打滑";
            dt.Rows[9]["t_check_item"] = "太低--太高";
            dt.Rows[9]["t_check_value"] = "";
            dt.Rows[9]["reference_level"] = "MW";
            dt.Rows[9]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[10]["number"] = "11";
            dt.Rows[10]["testtype_name"] = "试穿测试";
            dt.Rows[10]["testitem_code"] = "FT-11";
            dt.Rows[10]["testitem_name"] = "底部有响音";
            dt.Rows[10]["t_check_item"] = "";
            dt.Rows[10]["t_check_value"] = "";
            dt.Rows[10]["reference_level"] = "";
            dt.Rows[10]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[11]["number"] = "12";
            dt.Rows[11]["testtype_name"] = "试穿测试";
            dt.Rows[11]["testitem_code"] = "FT-12";
            dt.Rows[11]["testitem_name"] = "鞋子长度";
            dt.Rows[11]["t_check_item"] = "";
            dt.Rows[11]["t_check_value"] = "";
            dt.Rows[11]["reference_level"] = "";
            dt.Rows[11]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[12]["number"] = "13";
            dt.Rows[12]["testtype_name"] = "试穿测试";
            dt.Rows[12]["testitem_code"] = "FT-13";
            dt.Rows[12]["testitem_name"] = "裤子长度";
            dt.Rows[12]["t_check_item"] = "太低--太高";
            dt.Rows[12]["t_check_value"] = "不能有";
            dt.Rows[12]["reference_level"] = "ART";
            dt.Rows[12]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[13]["number"] = "14";
            dt.Rows[13]["testtype_name"] = "试穿测试";
            dt.Rows[13]["testitem_code"] = "FT-14";
            dt.Rows[13]["testitem_name"] = "帽子宽度";
            dt.Rows[13]["t_check_item"] = "太低--太高";
            dt.Rows[13]["t_check_value"] = "不能有";
            dt.Rows[13]["reference_level"] = "MW";
            dt.Rows[13]["sample_num"] = "6";
            dt.Rows.Add();
            dt.Rows[14]["number"] = "15";
            dt.Rows[14]["testtype_name"] = "试穿测试";
            dt.Rows[14]["testitem_code"] = "FT-15";
            dt.Rows[14]["testitem_name"] = "袜子厚度";
            dt.Rows[14]["t_check_item"] = "太低-太高";
            dt.Rows[14]["t_check_value"] = "不能有";
            dt.Rows[14]["reference_level"] = "ART";
            dt.Rows[14]["sample_num"] = "6";
            #endregion

            return dt;
        }

        public void BindingData()
        {
            int i = 0;
            foreach (DataRow dr in InitializeData().Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[i];
                dgvr.Cells["number"].Value = dr["number"].ToString();
                dgvr.Cells["testtype_name"].Value = dr["testtype_name"].ToString();
                dgvr.Cells["testitem_code"].Value = dr["testitem_code"].ToString();
                dgvr.Cells["testitem_name"].Value = dr["testitem_name"].ToString();
                dgvr.Cells["t_check_item"].Value = dr["t_check_item"].ToString();
                dgvr.Cells["t_check_value"].Value = dr["t_check_value"].ToString();
                dgvr.Cells["reference_level"].Value = dr["reference_level"].ToString();
                dgvr.Cells["sample_num"].Value = dr["sample_num"].ToString();
                i++;
            }

            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        public void BindingData2(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 1;
        }

        private void com_department_no_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>=0)
            {
                dataGridView1.Rows.Clear();
            }
            BindingData();
            pageControl1.BindPageEvent += BindingData2;
            FormLoad();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("DELETE"))
                        {
                            dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("category_no", cbo_category_no.Text);
            dic.Add("general_testtype_no", cbo_general_testtype_no.Text);
            dic.Add("PARENT_ITEM_NO", cbo_PARENT_ITEM_NO.Text);
            dic.Add("department_no", com_department_no.Text);
            dic.Add("plantarea_no", txt_plantarea_no.Text);
            F_QCM_Fitting_inspectionPrint ff = new F_QCM_Fitting_inspectionPrint(dic);
            ff.ShowDialog();
        }
    }
}
