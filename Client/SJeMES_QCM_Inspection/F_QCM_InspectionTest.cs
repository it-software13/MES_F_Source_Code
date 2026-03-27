using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
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
    public partial class InspectionTest : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 送检单号
        /// </summary>
        string inspection_no =string.Empty;
        /// <summary>
        /// 厂商名称
        /// </summary>
        private string plantarea_name=string.Empty;
        /// <summary>
        /// 产线名称
        /// </summary>
        private string productionline_name = string.Empty;
        /// <summary>
        /// 联动下拉框第三级要用的两条件之一
        /// </summary>
        private string Prod_NO=string.Empty;
        /// <summary>
        /// 物料条码
        /// </summary>
        private string material_noS;
        public InspectionTest()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        private void InspectionTest_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
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
        //实验室送检测试视图
        public void Table()
        {
            try
            {
                List<string> lst_enum_type = new List<string>();
                //请求api的数据展示

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("data", cbo_general_testtype_no.SelectedValue);
                p.Add("data1", cbo_category_no.SelectedValue);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.InspectionTableView",//类名
                                            "GetQCM_INSPECTION_LABORATORY_D_List",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["testtype_name"].Value = dr["testtype_name"].ToString();
                        dgvr.Cells["testitem_code"].Value = dr["testitem_code"].ToString();
                        dgvr.Cells["testitem_name"].Value = dr["testitem_name"].ToString();
                        dgvr.Cells["check_item_1"].Value = dr["check_item_1"].ToString();
                        dgvr.Cells["check_value_1"].Value = dr["check_value_1"].ToString();
                        dgvr.Cells["check_item_2"].Value = dr["check_item_2"].ToString();
                        dgvr.Cells["check_value_2"].Value = dr["check_value_2"].ToString();

                        dgvr.Cells["unit"].Value = dr["unit"].ToString();
                        dgvr.Cells["check_type"].Value = dr["check_type"].ToString();

                        dgvr.Cells["enum_value_1"].Value = dr["enum_value_1"].ToString();
                        dgvr.Cells["enum_value_2"].Value = dr["enum_value_2"].ToString();
                        dgvr.Cells["sample_num"].Value = dr["sample_num"].ToString();

                        dgvr.Cells["currency_formula"].Value = dr["currency_formula"].ToString();
                        dgvr.Cells["formula_name_1"].Value = dr["formula_name_1"].ToString();


                        dgvr.Cells["custom_formula"].Value = dr["custom_formula"].ToString();
                        dgvr.Cells["formula_name_2"].Value = dr["formula_name_2"].ToString();//ART备注
                        //dgvr.Cells["remarks"].Value = dr["remarks"].ToString();
                        dgvr.Cells["remark"].Value = dr["remarks"].ToString();
                        i++;
                    }
                    dataGridView1.Columns["currency_formula"].Visible = false;
                    dataGridView1.Columns["custom_formula"].Visible = false;
                    GenClass.AutoSizeColumn(dataGridView1,4);

                }
                /*else
                {
                    DataTable dt1 = (DataTable)dataGridView1.DataSource;
                    dt1.Rows.Clear();
                    dataGridView1.DataSource = dt1;
                }*/
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 回车事件触发按钮查询有无物料条码,员工二维码信息
        private void txt_material_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Select_Click(sender, e);
            }
        }
        //扫描条码
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

        #endregion

        #region 确认添加表头表身数据
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //添加表头，表身接口
            try
            {
                if (NotNull.Trues(txt_material_no.Text,
                    material_noS,
                    txt_material_name.Text,
                    cbo_category_no.Text,
                    cbo_general_testtype_no.Text,
                    txt_department_nos.Text,
                    txt_Id.Text,
                    cbo_category_no.Text,
                    txt_Name.Text,
                    txt_Branch.Text,
                    txt_plantarea_no.Text
                    ))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("必填项不能为空！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
                else
                {

                    List<BdnListClass> list1 = new List<BdnListClass>();
                    BdnListClass bdm_Testitem_M_List = new BdnListClass();
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        bdm_Testitem_M_List.testtype_name = dgr.Cells["testtype_name"].Value.ToString();
                        bdm_Testitem_M_List.testitem_code = dgr.Cells["testitem_code"].Value.ToString();
                        bdm_Testitem_M_List.testitem_name = dgr.Cells["testitem_name"].Value.ToString();
                        bdm_Testitem_M_List.t_check_item = dgr.Cells["check_item_1"].Value.ToString();

                        bdm_Testitem_M_List.reference_level = dgr.Cells["enum_value_1"].Value.ToString();
                       
                        bdm_Testitem_M_List.t_check_value = dgr.Cells["check_value_1"].Value.ToString();
                        bdm_Testitem_M_List.d_check_item = dgr.Cells["check_item_2"].Value.ToString();
                        bdm_Testitem_M_List.d_check_value = dgr.Cells["check_value_2"].Value.ToString();
                        bdm_Testitem_M_List.unit = dgr.Cells["unit"].Value.ToString();

                        bdm_Testitem_M_List.check_type = dgr.Cells["check_type"].Value.ToString();

                        bdm_Testitem_M_List.sample_num = dgr.Cells["sample_num"].Value.ToString();
                        bdm_Testitem_M_List.custom_formula = dgr.Cells["custom_formula"].Value.ToString();
                        bdm_Testitem_M_List.currency_formula = dgr.Cells["currency_formula"].Value.ToString();
                        //bdm_Testitem_M_List.art_remarks = dgr.Cells["remarks"].Value.ToString();
                        bdm_Testitem_M_List.test_remarks = dgr.Cells["remark"].Value.ToString();
                        list1.Add(bdm_Testitem_M_List);
                        //清除原有的实体类值再带入
                        bdm_Testitem_M_List = new BdnListClass();
                    }
                    if (list1.Count > 0)
                    {
                        #region 表头内容

                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("material_no", material_noS);//物料条码
                        p.Add("material_name", txt_material_name.Text);//物料名称
                        p.Add("category_no", cbo_category_no.SelectedValue);//样式种类
                        p.Add("category_name", cbo_category_no.Text);//式样种类名称
                        p.Add("general_testtype_no", cbo_general_testtype_no.SelectedValue);//通用检测标准
                        p.Add("department_no", txt_department_nos.Text);//阶段
                        p.Add("Id", txt_Id.Text);//账号
                        p.Add("Name", txt_Name.Text);//名称
                        p.Add("Branch", txt_Branch.Text);//部门
                        p.Add("art_code", cbo_PARENT_ITEM_NO.Text);//ART选择
                        p.Add("productionline_no", txt_productionline_no.Text);//产线
                        p.Add("productionline_name", productionline_name);//产线名称
                        p.Add("plantarea_no", txt_plantarea_no.Text);//厂区 
                        p.Add("plantarea_name",plantarea_name);//厂称
                        p.Add("cbo_document_type", cbo_document_type.SelectedValue!=null? cbo_document_type.SelectedValue.ToString():"");//来源单据类型
                        p.Add("txt_sjno", txt_sjno.Text.Trim());//单号
                        #endregion
                        p.Add("data11", list1);//表身数据

                   

                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.Inspectionbasic",//类名
                                                    "BDM_RD_ITEM_Select_item_Add",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (ret.IsSuccess)
                        {
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("保存成功！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);

                            inspection_no = ret.RetData;
                            dataGridView1.DataSource = null;
                            F_QCM_InspectionPrint add = new F_QCM_InspectionPrint(inspection_no);
                            add.Show();
                            cbo_general_testtype_no.Text = null;
                            cbo_category_no.DataSource = null;
                        }
                        else
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                    }
                    else
                    {
                        MessageBox.Show("表身无值可添加，请确认验证经验标准值是否正确");
                    }
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
        #endregion
        /// <summary>
        /// 用于接收上一级的ART选择的dt做下一级的准备
        /// </summary>
        public DataTable dts=null;
        #region 点击带出
        //阶段找品管部门表bdm_quality_department_m弹窗						
        private void txt_department_no_Click(object sender, EventArgs e)
        {
         

          /*  //当前窗体名称+"_"+当前方法名称
            string FrmMenthName = this.Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name;

            string sql = @"select department_no as 部门代号,DEPARTMENT_NAME as 部门名称,REMARKS as 备注  from bdm_quality_department_m order by id desc";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_department_no.Text = frmData.RetData.Rows[0]["部门代号"].ToString();
                txt_productionline_no.Text = null;
            }*/
        }
        //产线带出
        private void txt_productionline_no_Click(object sender, EventArgs e)
        {
            try
            {
                /* if (!string.IsNullOrEmpty(txt_department_no.Text))
                 {
                     //当前窗体名称+"_"+当前方法名称
                     string sql = $@"select productionline_no as 产线编号,productionline_name as 产线名称,REMARKS as 备注 from bdm_quality_department_d where department_no='{txt_department_no.Text}'";
                     FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
                     frmData.ShowDialog();
                     if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                     {
                         txt_productionline_no.Text = frmData.RetData.Rows[0]["产线编号"].ToString();
                         productionline_name = frmData.RetData.Rows[0]["产线名称"].ToString();
                     }
                 }
                 else
                 {
                     MessageBox.Show("请先选择阶段");
                     return;
                 }*/
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
        private void txt_plantarea_no_Click(object sender, EventArgs e)
        {
            //当前窗体名称+"_"+当前方法名称
            string sql = "select PLANT_CODE as  厂区厂商编号, PLANT_NAME as  厂区厂商名称 from base001a1 union select SUPPLIERS_CODE  厂区厂商编号,SUPPLIERS_NAME 厂区厂商名称 from base003m";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_plantarea_no.Text = frmData.RetData.Rows[0]["厂区厂商编号"].ToString();
                plantarea_name=frmData.RetData.Rows[0]["厂区厂商名称"].ToString();
            }
        }

        #endregion
        #region 联动下拉框
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
        private void cbo_general_testtype_no_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
               
                if (cbo_general_testtype_no.SelectedIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(dts.Rows[0]["PARENT_ITEM_NO"].ToString()) && cbo_general_testtype_no.SelectedValue!=null)
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
        #endregion

        private void cbo_category_no_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Table();
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
       
        private void cbo_PARENT_ITEM_NO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dts.Rows[0]["PARENT_ITEM_NO"].ToString()))
            {
                TwoXLK();
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

        private void txt_plantarea_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
