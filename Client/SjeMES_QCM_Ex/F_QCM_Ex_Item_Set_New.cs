using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.WebAPI;
using SjeMES_QCM_Ex.F_QCM_Ex_Item_Set_UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_Item_Set_New : MaterialForm
    {
        public string _task_no = "";
        private readonly MaterialSkinManager materialSkinManager;
        public DataTable G_formula_type = new DataTable();
        public DataTable D_formula_type = new DataTable();
        public DataTable D_JUDGMENT_CRITERIA = new DataTable();
        public DataTable D_JUDGE_TYPE = new DataTable();

        public List<code_name_obj> list_tygs_data = new List<code_name_obj>();
        public List<code_name_obj> list_zdygs_data = new List<code_name_obj>();
        public List<code_name_obj> list_category_data = new List<code_name_obj>();
        public List<code_name_obj> list_xjjb_data = new List<code_name_obj>();
        public List<code_name_obj> list_agesex_data = new List<code_name_obj>();
        public List<code_name_obj> list_cptype_data = new List<code_name_obj>();
        public List<code_name_obj> list_jd_data = new List<code_name_obj>();
        public List<code_name_obj> list_fgt_data = new List<code_name_obj>();
        public List<code_name_obj> list_parts_data = new List<code_name_obj>();
        public List<code_name_obj> list_position_data = new List<code_name_obj>();
        public List<code_name_obj> list_line_data = new List<code_name_obj>();
        public List<code_name_obj> list_materialtype_data = new List<code_name_obj>();
        public List<code_name_obj> list_workmanship_data = new List<code_name_obj>();
        public List<code_name_obj> list_productlevel_data = new List<code_name_obj>();
        public List<string> list_size_data = new List<string>();
        public DataTable cpx_po_dt = new DataTable();
        public F_QCM_Ex_Item_Set_New(string task_no)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _task_no = task_no;

            GetALLDDLData();
            list_fgt_data = GetFGTInfo();
            list_size_data = GetSizeInfo();
        }

        /// <summary>
        /// 获取FGT
        /// </summary>
        /// <returns></returns>
        public List<code_name_obj> GetFGTInfo()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetFGTInfo",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<code_name_obj>>(ret.RetData.ToString());



        }

        /// <summary>
        /// 获取size
        /// </summary>
        /// <returns></returns>
        public List<string> GetSizeInfo()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetSizeInfo",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(ret.RetData.ToString());



        }

        public void GetALLDDLData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetALLDDLData",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return;
            }
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<code_name_obj>>>(ret.RetData.ToString());

            list_tygs_data = result["list_tygs_data"];
            list_zdygs_data = result["list_zdygs_data"];
            list_category_data = result["list_category_data"];
            list_xjjb_data = result["list_xjjb_data"];
            list_agesex_data = result["list_agesex_data"];
            list_cptype_data = result["list_cptype_data"];
            list_jd_data = result["list_jd_data"];
            list_parts_data = result["list_parts_data"];
            list_position_data = result["list_position_data"];
            list_line_data = result["list_line_data"];
            list_materialtype_data = result["list_materialtype_data"];
            list_workmanship_data = result["list_workmanship_data"];
            list_productlevel_data = result["list_productlevel_data"];

        }

        /// <summary>
        /// 绑定下拉数据
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="data"></param>
        public void BindDDL(ComboBox cmb, List<code_name_obj> data, string type = "", bool withEmpty = false, bool changed = false)
        {

            if (withEmpty && !data.Any(x => x.CODE == ""))
            {
                data.Add(new code_name_obj
                {
                    CODE = "",
                    NAME = ""
                });
            }
            if (cmb.Name == "cmb_lcll_line")
                cmb.Items.AddRange(data.OrderBy(x => x.CODE).ToList().ToArray());
            else
                cmb.DataSource = data.OrderBy(x => x.CODE).ToList();
            cmb.DisplayMember = "NAME";
            cmb.ValueMember = "CODE";
            cmb.SelectedIndex = 0;
            //if (changed)
            //{
            //    switch (type)
            //    {
            //        case "cpx":
            //            cmb.SelectionChangeCommitted -= Get_cpx_checkItem;
            //            cmb.SelectionChangeCommitted += Get_cpx_checkItem;
            //            break;
            //        case "bj":
            //            cmb.SelectionChangeCommitted -= Get_bj_checkItem;
            //            cmb.SelectionChangeCommitted += Get_bj_checkItem;
            //            break;
            //        case "gy":
            //            cmb.SelectionChangeCommitted -= Get_gy_checkItem;
            //            cmb.SelectionChangeCommitted += Get_gy_checkItem;
            //            break;
            //        case "cl":
            //            cmb.SelectionChangeCommitted -= Get_cl_checkItem;
            //            cmb.SelectionChangeCommitted += Get_cl_checkItem;
            //            break;
            //    }

            //}

        }

        /// <summary>
        /// 绑定size
        /// </summary>
        /// <param name="result"></param>
        private void Bind_size(ComboBox cmb, List<string> result, bool withEmpty = false)
        {
            cmb.Items.Clear();
            if (withEmpty)
            {
                cmb.Items.Add("");
            }
            foreach (var item in result)
            {
                cmb.Items.Add(item);
            }
        }

        public DataTable Get_G_formula_type()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Get_G_formula_type",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());



        }

        public DataTable Get_D_formula_type()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Get_D_formula_type",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());



        }

        public DataTable Get_JUDGMENT_CRITERIA()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Get_JUDGMENT_CRITERIA",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());



        }

        public DataTable Get_JUDGE_TYPE()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Get_JUDGE_TYPE",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());



        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }


        public string test_type = "";
        public string test_type_no = "";
        UserControl currItem;
        List<string> deleteIds = new List<string>();

        public void getdate()
        {
            deleteIds = new List<string>();
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("task_no", _task_no);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetTaskInfo",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                MessageBox.Show("Failed to get data");
                return;
            }
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["info"].ToString());
            DataTable itemlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["itemlist"].ToString());
            txt_task_no.Text = info["TASK_NO"].ToString();

            panel33.Controls.Clear();
            if (info["TEST_TYPE"].ToString() == "0")
            {
                test_type = "FinishedShoes-Testing";//成品鞋//FinishedShoes-Testing
                UC_F_QCM_Ex_Item_Set_New_CPX ucItem = new UC_F_QCM_Ex_Item_Set_New_CPX();
                BindDDL(ucItem.cmb_cpx_xb, list_agesex_data, "cpx", true, true);//成品鞋--年龄性别
                BindDDL(ucItem.cmb_cpx_cpzl, list_cptype_data, "cpx", true, true);//成品鞋--成品种类
                BindDDL(ucItem.cmb_cpx_jd, list_jd_data, "cpx", true);//成品鞋--阶段
                Bind_size(ucItem.cmb_cpx_size, list_size_data, true);//成品鞋--Size
                BindDDL(ucItem.cmb_cpx_fgt, list_fgt_data, "cpx", true, true);//成品写--fgt送测类型

                ucItem.ckb_cpx_sfcc.Checked = string.IsNullOrEmpty(info["RETEST_TASK_NO"].ToString()) ? false : true;//成品鞋--是否重测
                ucItem.txt_cpx_cc_task_no.Text = info["RETEST_TASK_NO"].ToString();//成品鞋--重测实验室编号
                ucItem.txt_cpx_qrcode.Text = info["ART_NO"].ToString();//成品鞋--扫描条码
                ucItem.txt_cpx_art.Text = info["ART_NO"].ToString();//成品鞋--ART
                ucItem.txt_cpx_shose.Text = info["SHOE_NO"].ToString();//成品鞋--鞋型名称
                ucItem.txt_cpx_model_no.Text = info["MODEL_NO"].ToString();//成品鞋--Model No
                ucItem.tb_cpx_category.Text = info["CATEGORY_CODE"].ToString();//成品鞋--Category
                ucItem.txt_cpx_cmbbh.Text = info["CMBBH"].ToString();//成品鞋--尺码标编号
                ucItem.tb_cpx_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();//成品鞋--产品级别
                ucItem.txt_cpx_jidu.Text = info["SEASON"].ToString();//成品鞋--季度
                ucItem.tb_cpx_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();//成品鞋--新旧级别
                ucItem.cmb_cpx_xb.Text = info["GENDER_NAME"].ToString();//成品鞋--年龄性别
                ucItem.cmb_cpx_cpzl.Text = info["CP_TYPE_NAME"].ToString();//成品鞋--成品种类
                ucItem.txt_cpx_test_id.Text = info["TEST_ID"].ToString();//成品鞋--Test ID
                ucItem.cmb_cpx_jd.Text = info["PHASE_CREATION_NAME"].ToString();//成品鞋--阶段
                ucItem.txt_cpx_scsl.Text = info["SEND_TEST_QTY"].ToString();//成品鞋--送测数量
                ucItem.cmb_cpx_size.Text = info["SIZES"].ToString();//成品鞋--size
                ucItem.txt_cpx_ddpo.Text = info["ORDER_PO"].ToString();//成品鞋--订单po
                ucItem.txt_cpx_posl.Text = info["ORDER_PO_QTY"].ToString();//成品鞋--po数量
                ucItem.txt_cpx_task_no.Text = info["TASK_NO"].ToString();//成品鞋--实验室编号
                ucItem.cmb_cpx_fgt.Text = info["FGT_NAME"].ToString();//成品鞋--fgt类型
                ucItem.txt_cpx_reason.Text = info["TEST_REASON"].ToString();//成品鞋--送测原因

                panel33.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;
            }
            if (info["TEST_TYPE"].ToString() == "1")
            {
                test_type = "part";////部件
                UC_F_QCM_Ex_Item_Set_New_BJ ucItem = new UC_F_QCM_Ex_Item_Set_New_BJ();
                BindDDL(ucItem.cmb_bj_bwmc, list_position_data, "bj", true, true);//部件--部位名称
                BindDDL(ucItem.cmb_bj_jieduan, list_jd_data, "bj", true);//部件--阶段
                Bind_size(ucItem.cmb_bj_size, list_size_data);//部件--Size
                BindDDL(ucItem.cmb_bj_fgt, list_fgt_data, "bj", true, true);//部件--fgt送测类型

                ucItem.ckb_bj_sfcc.Checked = string.IsNullOrEmpty(info["RETEST_TASK_NO"].ToString()) ? false : true;//部件--是否重测
                ucItem.txt_bj_cc_task_no.Text = info["RETEST_TASK_NO"].ToString();//部件--重测实验室编号
                ucItem.txt_bj_art.Text = info["ART_NO"].ToString();//部件--ART
                ucItem.txt_bj_shose.Text = info["SHOE_NO"].ToString();//部件--鞋型
                ucItem.cmb_bj_bwmc.Text = info["POSITION_NAME"].ToString();//部件--部位名称
                ucItem.txt_bj_model_no.Text = info["MODEL_NO"].ToString();//部件--MODEL_NO
                ucItem.tb_bj_kfxl.Text = info["CATEGORY_CODE"].ToString();//部件--Category
                ucItem.tb_bj_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();//部件--产品级别
                ucItem.txt_bj_jidu.Text = info["SEASON"].ToString();//部件--季度
                ucItem.txt_bj_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();//部件--新旧级别
                ucItem.txt_bj_xb.Text = info["GENDER_NAME"].ToString();//部件--年龄性别
                ucItem.cmb_bj_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();//部件--阶段
                ucItem.txt_bj_scsl.Text = info["SEND_TEST_QTY"].ToString();//部件--送测数量
                ucItem.cmb_bj_size.Text = info["SIZES"].ToString();//部件--size
                ucItem.txt_bj_po_order.Text = info["ORDER_PO"].ToString();//部件--订单po
                ucItem.txt_bj_po_qty.Text = info["ORDER_PO_QTY"].ToString();//部件--po数量
                ucItem.txt_bj_task_no.Text = info["TASK_NO"].ToString();//部件--实验室编号
                ucItem.cmb_bj_fgt.Text = info["FGT_NAME"].ToString();//部件--fgt类型
                ucItem.lab_bj_cs_code.Text = info["MANUFACTURER_CODE"].ToString();//部件--厂商 code
                ucItem.txt_bj_cs.Text = info["MANUFACTURER_NAME"].ToString();//部件--厂商 名称
                ucItem.lab_bj_cs_jc.Text = info["MANUFACTURER_JC"].ToString();//部件--厂商 简称
                ucItem.txt_bj_reasaon.Text = info["TEST_REASON"].ToString();//部件--送测原因
                ucItem.tb_bj_test_id.Text = info["TEST_ID"].ToString();//部件--Test Id

                panel33.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;
            }
            if (info["TEST_TYPE"].ToString() == "2")
            {
                test_type = "craft";////工艺
                UC_F_QCM_Ex_Item_Set_New_GY ucItem = new UC_F_QCM_Ex_Item_Set_New_GY();
                BindDDL(ucItem.cmb_gy_gymc, list_workmanship_data, "gy", true, true);//工艺--工艺名称
                BindDDL(ucItem.cmb_gy_bwmc, list_position_data, "gy", true, true);//工艺--部位名称
                BindDDL(ucItem.cmb_gy_jieduan, list_jd_data, "gy", true);//工艺--阶段
                BindDDL(ucItem.cmb_gy_fgt, list_fgt_data, "gy", true, true);//工艺--fgt送测类型

                ucItem.ckb_gy_sfcc.Checked = string.IsNullOrEmpty(info["RETEST_TASK_NO"].ToString()) ? false : true;//工艺--是否重测
                ucItem.txt_gy_cc_task_no.Text = info["RETEST_TASK_NO"].ToString();//工艺--重测实验室编号
                ucItem.txt_gy_art.Text = info["ART_NO"].ToString();//工艺--ART
                ucItem.txt_gy_shose.Text = info["SHOE_NO"].ToString();//工艺--鞋型
                ucItem.cmb_gy_gymc.Text = info["WORKMANSHIP_NAME"].ToString();//工艺--工艺名称
                ucItem.cmb_gy_bwmc.Text = info["POSITION_NAME"].ToString();//工艺--部位名称
                ucItem.tb_gy_kfxl.Text = info["CATEGORY_CODE"].ToString();//工艺--Category
                ucItem.txt_gy_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();//工艺--产品级别
                ucItem.txt_gy_jidu.Text = info["SEASON"].ToString();//工艺--季度
                ucItem.cmb_gy_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();//工艺--阶段
                ucItem.txt_gy_scsl.Text = info["SEND_TEST_QTY"].ToString();//工艺--送测数量
                ucItem.txt_gy_task_no.Text = info["TASK_NO"].ToString();//工艺--实验室编号
                ucItem.cmb_gy_fgt.Text = info["FGT_NAME"].ToString();//工艺--fgt类型
                ucItem.lab_gy_cs_code.Text = info["MANUFACTURER_CODE"].ToString();//工艺--厂商 code
                ucItem.txt_gy_cs.Text = info["MANUFACTURER_NAME"].ToString();//工艺--厂商 名称
                ucItem.lab_gy_cs_jc.Text = info["MANUFACTURER_JC"].ToString();//工艺--厂商 简称
                ucItem.txt_gy_reason.Text = info["TEST_REASON"].ToString();//工艺--送测原因

                panel33.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;
            }
            if (info["TEST_TYPE"].ToString() == "3")
            {
                test_type = "Material";//Material//材料
                UC_F_QCM_Ex_Item_Set_New_CL ucItem = new UC_F_QCM_Ex_Item_Set_New_CL();
                BindDDL(ucItem.cmb_cl_fgt, list_fgt_data, "cl", true, true);//材料--fgt送测类型

                ucItem.CL_QRCODE_JSON = info["CL_QRCODE_JSON"].ToString();

                ucItem.ckb_cl_sfcc.Checked = string.IsNullOrEmpty(info["RETEST_TASK_NO"].ToString()) ? false : true;//材料--是否重测
                ucItem.txt_cl_cc_task_no.Text = info["RETEST_TASK_NO"].ToString();//材料--重测实验室编号
                ucItem.cmb_cl_fgt.Text = info["FGT_NAME"].ToString();//材料--材料送测类型
                ucItem.txt_cl_test_id.Text = info["TEST_ID"].ToString();//材料--Test ID
                ucItem.lab_cl_cs_code.Text = info["MANUFACTURER_CODE"].ToString();//材料--厂商 code
                ucItem.txt_cl_cs.Text = info["MANUFACTURER_NAME"].ToString();//材料--厂商 名称
                ucItem.lab_cl_cs_jc.Text = info["MANUFACTURER_JC"].ToString();//材料--厂商 简称
                ucItem.txt_cl_task_no.Text = info["TASK_NO"].ToString();//材料--实验室编号
                ucItem.txt_cl_reason.Text = info["TEST_REASON"].ToString();//材料--送测原因

                panel33.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;
            }
            if (info["TEST_TYPE"].ToString() == "4")
            {
                test_type = "ProductionRally";//ProductionRally//量产拉力
                UC_F_QCM_Ex_Item_Set_New_LCLI ucItem = new UC_F_QCM_Ex_Item_Set_New_LCLI();
                BindDDL(ucItem.cmb_lcll_line, list_line_data, "lcll", true);//量产拉力--产线
                BindDDL(ucItem.cmb_lcll_jieduan, list_jd_data, "lcll", true);//量产拉力--阶段
                Bind_size(ucItem.cmb_lcll_size, list_size_data, true);//量产拉力--size

                ucItem.ckb_lcll_sfcc.Checked = string.IsNullOrEmpty(info["RETEST_TASK_NO"].ToString()) ? false : true;//量产拉力--是否重测
                ucItem.txt_lcll_cc_task_no.Text = info["RETEST_TASK_NO"].ToString();//量产拉力--重测实验室编号
                ucItem.txt_lcll_qrcode.Text = info["ART_NO"].ToString();//量产拉力--ART条码
                ucItem.txt_lcll_art.Text = info["ART_NO"].ToString();//量产拉力--ART
                ucItem.txt_lcll_shose.Text = info["SHOE_NO"].ToString();//量产拉力--鞋型
                ucItem.txt_lcll_category.Text = info["CATEGORY_CODE"].ToString();//量产拉力--Category
                ucItem.cmb_lcll_line.Text = info["LINE_NAME"].ToString();//量产拉力--产线
                ucItem.txt_lcll_cmbbh.Text = info["CMBBH"].ToString();//量产拉力--尺码标编号
                ucItem.txt_lcll_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();//量产拉力--产品级别
                ucItem.txt_lcll_jd.Text = info["SEASON"].ToString();//量产拉力--季度
                ucItem.cmb_lcll_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();//量产拉力--阶段
                ucItem.txt_lcll_scsl.Text = info["SEND_TEST_QTY"].ToString();//量产拉力--送测数量
                ucItem.cmb_lcll_size.Text = info["SIZES"].ToString();//量产拉力--size
                ucItem.txt_lcll_test_time.Text = info["TEST_TIME"].ToString();//量产拉力--鞋子抽测时间
                ucItem.txt_lcll_po_order.Text = info["ORDER_PO"].ToString();//量产拉力--订单po
                ucItem.txt_lcll_po_qty.Text = info["ORDER_PO_QTY"].ToString();//量产拉力--po数量
                ucItem.txt_lcll_task_no.Text = info["TASK_NO"].ToString();//量产拉力--实验室编号
                ucItem.txt_lcll_jsxx.Text = info["GLUE"].ToString();//量产拉力--胶水处理信息
                ucItem.txt_lcll_reason.Text = info["TEST_REASON"].ToString();//量产拉力--送测原因

                panel33.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;

            }
            test_type_no = info["TEST_TYPE"].ToString();
            txt_test_type.Text = test_type;

            //txt_art.Text = info["ART_NO"].ToString();
            //txt_po_order.Text = info["ORDER_PO"].ToString();
            //txt_material_way.Text = info["MATERIAL_WAY"].ToString();
            //txt_line.Text = info["LINE_NAME"].ToString();
            //txt_shose.Text = info["SHOE_NO"].ToString();
            //txt_po_qty.Text = info["ORDER_PO_QTY"].ToString();
            //txt_bjmc.Text = info["PARTS_NAME"].ToString();
            //txt_cs.Text = info["MANUFACTURER_NAME"].ToString();
            //txt_category.Text = info["CATEGORY_NAME"].ToString();
            //txt_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();
            //txt_bwmc.Text = info["POSITION_NAME"].ToString();
            //txt_fgt.Text = info["FGT_NAME"].ToString();
            //txt_cpjb.Text = info["PRODUCT_LEVEL_VALUE"].ToString();
            //txt_scsl.Text = info["SEND_TEST_QTY"].ToString();
            //txt_gymc.Text = info["WORKMANSHIP_NAME"].ToString();
            //txt_clid.Text = info["MAKINGS_ID"].ToString();
            //txt_jd.Text = info["SEASON"].ToString();
            //txt_size.Text = info["SIZES"].ToString();
            //txt_clzl.Text = info["MAKINGS_TYPE_NAME"].ToString();
            //txt_wlmc.Text = info["MATERIAL_NAME"].ToString();
            //txt_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();
            //txt_xb.Text = info["GENDER"].ToString();
            //txt_ys.Text = info["COLORS"].ToString();
            //txt_reason.Text = info["TEST_REASON"].ToString();
            //txt_jsxx.Text = info["GLUE"].ToString();

            txt_staff_no.Text = info["STAFF_NO"].ToString();
            txt_staff_name.Text = info["STAFF_NAME"].ToString();
            txt_staff_department.Text = info["STAFF_DEPARTMENT"].ToString();

            dgv.Rows.Clear();
            foreach (DataRow item in itemlist.Rows)
            {
                int i = dgv.Rows.Add();
                dgv.Rows[i].Cells["xh"].Value = (i + 1).ToString();
                dgv.Rows[i].Cells["xh"].Style = cl_readonly;
                string SOURCES = "";
                if (item["SOURCES"].ToString() == "0")
                {
                   // SOURCES = "DQA测试任务";
                    SOURCES = "DQA测试任务";
                }
                if (item["SOURCES"].ToString() == "1")
                {
                    //SOURCES = "常规";
                    SOURCES = "conventional";//
                }
                
                dgv.Rows[i].Cells["type"].Value = SOURCES;
                dgv.Rows[i].Cells["type"].ReadOnly = true;
                dgv.Rows[i].Cells["type"].Style = cl_readonly;

                dgv.Rows[i].Cells["inspection_type_name"].Value = string.IsNullOrEmpty(item["inspection_type_name"].ToString())? "customize" : item["inspection_type_name"].ToString();
                dgv.Rows[i].Cells["inspection_type_name"].ReadOnly = true;
                dgv.Rows[i].Cells["inspection_type_name"].Style = cl_readonly;
                dgv.Rows[i].Cells["inspection_type"].Value = item["INSPECTION_TYPE"].ToString();
                dgv.Rows[i].Cells["inspection_type"].ReadOnly = true;
                dgv.Rows[i].Cells["inspection_type"].Style = cl_readonly;
                dgv.Rows[i].Cells["choice_name"].Value = item["CHOICE_NAME"].ToString();
                dgv.Rows[i].Cells["choice_name"].ReadOnly = true;
                dgv.Rows[i].Cells["choice_name"].Style = cl_readonly;
                dgv.Rows[i].Cells["choice_no"].Value = item["CHOICE_NO"].ToString();
                dgv.Rows[i].Cells["choice_no"].ReadOnly = true;
                dgv.Rows[i].Cells["choice_no"].Style = cl_readonly;
                dgv.Rows[i].Cells["inspection_code"].Value = item["INSPECTION_CODE"].ToString();
                dgv.Rows[i].Cells["inspection_code"].ReadOnly = true;
                dgv.Rows[i].Cells["inspection_code"].Style = cl_readonly;
                dgv.Rows[i].Cells["inspection_name"].Value = item["INSPECTION_NAME"].ToString();
                dgv.Rows[i].Cells["inspection_name"].ReadOnly = true;
                dgv.Rows[i].Cells["inspection_name"].Style = cl_readonly;
                dgv.Rows[i].Cells["standard_value"].Value = item["STANDARD_VALUE"].ToString();
                //dgv.Rows[i].Cells["standard_value"].ReadOnly = true;
                dgv.Rows[i].Cells["standard_value"].ReadOnly = false;
                //dgv.Rows[i].Cells["standard_value"].Style = cl_readonly;
                dgv.Rows[i].Cells["unit"].Value = item["UNIT"].ToString();
                dgv.Rows[i].Cells["unit"].ReadOnly = true;
                dgv.Rows[i].Cells["unit"].Style = cl_readonly;
                dgv.Rows[i].Cells["sample_qty"].Value = item["SAMPLE_QTY"].ToString();

                comboxLoad(dgv, i);

                //DataGridViewComboBoxColumn cmb3 = (DataGridViewComboBoxColumn)dgv.Rows[i].Cells["judgment_criteria"].OwningColumn;
                //cmb3.DataSource = D_JUDGMENT_CRITERIA;
                //cmb3.DisplayMember = "NAME";
                //cmb3.ValueMember = "CODE";
                if (D_JUDGMENT_CRITERIA.Select($"CODE='{item["JUDGMENT_CRITERIA"].ToString()}'").Length > 0)
                {
                    dgv.Rows[i].Cells["judgment_criteria"].Value = item["JUDGMENT_CRITERIA"].ToString();
                }
                dgv.Rows[i].Cells["judgment_criteria"].ReadOnly = true;

                //DataGridViewComboBoxColumn cmb4 = (DataGridViewComboBoxColumn)dgv.Rows[i].Cells["judge_type"].OwningColumn;
                //cmb4.DataSource = D_JUDGE_TYPE;
                //cmb4.DisplayMember = "NAME";
                //cmb4.ValueMember = "CODE";
                if (D_JUDGE_TYPE.Select($"CODE='{item["judge_type"].ToString()}'").Length > 0)
                {
                    dgv.Rows[i].Cells["judge_type"].Value = item["judge_type"].ToString();
                }
                dgv.Rows[i].Cells["judge_type"].ReadOnly = true;

                //DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv.Rows[i].Cells["tygs"].OwningColumn;
                //cmb.DataSource = G_formula_type;
                //cmb.DisplayMember = "NAME";
                //cmb.ValueMember = "CODE";
                if (G_formula_type.Select($"CODE='{item["G_FORMULA_CODE"].ToString()}'").Length > 0)
                {
                    dgv.Rows[i].Cells["tygs"].Value = item["G_FORMULA_CODE"].ToString();
                }

                //DataGridViewComboBoxColumn cmb1 = (DataGridViewComboBoxColumn)dgv.Rows[i].Cells["zdygs"].OwningColumn;
                //cmb1.DataSource = D_formula_type;
                //cmb1.DisplayMember = "NAME";
                //cmb1.ValueMember = "CODE";
                if (D_formula_type.Select($"CODE='{item["D_FORMULA_CODE"].ToString()}'").Length > 0)
                {
                    dgv.Rows[i].Cells["zdygs"].Value = item["D_FORMULA_CODE"].ToString();
                }
                dgv.Rows[i].Cells["remarks"].Value = item["ART_D_REMARK"].ToString();
                dgv.Rows[i].Cells["d_id"].Value = item["id"].ToString();
               
            }
        }

        private void F_QCM_Ex_Item_Set_Load(object sender, EventArgs e)
        {
            cl_readonly.SelectionBackColor = System.Drawing.SystemColors.MenuBar;
            cl_readonly.BackColor = System.Drawing.SystemColors.MenuBar;
            cl_readonly.SelectionForeColor = Color.Black;

            G_formula_type = Get_G_formula_type();
            D_formula_type = Get_D_formula_type();
            if (D_formula_type.Columns.Count == 0)
            {
                D_formula_type.Columns.Add("CODE", typeof(string));
                D_formula_type.Columns.Add("NAME", typeof(string));
            }
            D_JUDGMENT_CRITERIA = Get_JUDGMENT_CRITERIA();
            D_JUDGE_TYPE = Get_JUDGE_TYPE();
            getdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int newindex = dgv.Rows.Add();
            dgv.Rows[newindex].Cells["xh"].Value = (newindex + 1);
            dgv.Rows[newindex].Cells["xh"].Style = cl_readonly;
            dgv.Rows[newindex].Cells["type"].Value = "conventional";
            dgv.Rows[newindex].Cells["type"].Style = cl_readonly;
            dgv.Rows[newindex].Cells["d_id"].Value = "";
            dgv.Rows[newindex].Cells["inspection_type_name"].Value = "customize";
            dgv.Rows[newindex].Cells["inspection_type_name"].Style = cl_readonly;
            dgv.Rows[newindex].Cells["inspection_type"].Value = "-1";
            dgv.Rows[newindex].Cells["choice_name"].Value = "";
            dgv.Rows[newindex].Cells["choice_name"].ReadOnly = true;
            dgv.Rows[newindex].Cells["choice_name"].Style = cl_readonly;
            dgv.Rows[newindex].Cells["choice_no"].Value = "";
            dgv.Rows[newindex].Cells["inspection_code"].ReadOnly = false;
            dgv.Rows[newindex].Cells["inspection_name"].ReadOnly = false;
            dgv.Rows[newindex].Cells["standard_value"].ReadOnly = false;
            dgv.Rows[newindex].Cells["unit"].ReadOnly = false;
            dgv.Rows[newindex].Cells["sample_qty"].ReadOnly = false;
            //初始化下拉框
            comboxLoad(dgv, newindex);
            dgv.Rows[newindex].Cells["judgment_criteria"].ReadOnly = false;
            dgv.Rows[newindex].Cells["judge_type"].ReadOnly = false;
            dgv.Rows[newindex].Cells["remarks"].ReadOnly = false;

        }

        private void SC_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            int index = Convert.ToInt32(but.Name.Replace("delete_", ""));
            dgv.Rows.Remove(dgv.Rows[index]);
            dgv.Controls.Remove(but);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //初始化下拉框
        public void comboxLoad(DataGridView dgv,int newindex)
        {
            //判断标准
            DataGridViewComboBoxColumn cmb3 = (DataGridViewComboBoxColumn)dgv.Rows[newindex].Cells["judgment_criteria"].OwningColumn;
            cmb3.DataSource = D_JUDGMENT_CRITERIA;
            cmb3.DisplayMember = "NAME";
            cmb3.ValueMember = "CODE";

            //判断类型
            DataGridViewComboBoxColumn cmb4 = (DataGridViewComboBoxColumn)dgv.Rows[newindex].Cells["judge_type"].OwningColumn;
            cmb4.DataSource = D_JUDGE_TYPE;
            cmb4.DisplayMember = "NAME";
            cmb4.ValueMember = "CODE";

            //通用公式类型
            DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv.Rows[newindex].Cells["tygs"].OwningColumn;
            cmb.DataSource = G_formula_type;
            cmb.DisplayMember = "NAME";
            cmb.ValueMember = "CODE";

            //自定义公式类型
            DataGridViewComboBoxColumn cmb1 = (DataGridViewComboBoxColumn)dgv.Rows[newindex].Cells["zdygs"].OwningColumn;
            cmb1.DataSource = D_formula_type;
            cmb1.DisplayMember = "NAME";
            cmb1.ValueMember = "CODE";

        }

        public DataGridViewCellStyle cl_readonly = new DataGridViewCellStyle();

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Detection item cannot be empty");
                return;
            }
            Dictionary<string, object> update_head_info = new Dictionary<string, object>();
            //if (info["TEST_TYPE"].ToString() == "1")
            //{
            //    test_type = "部件";
            //}
            //if (info["TEST_TYPE"].ToString() == "2")
            //{
            //    test_type = "工艺";
            //}
            //if (info["TEST_TYPE"].ToString() == "3")
            //{
            //    test_type = "材料";
            //}
            //if (info["TEST_TYPE"].ToString() == "4")
            //{
            //    test_type = "量产拉力";
            //}
            //test_type_no = info["TEST_TYPE"].ToString();
            switch (test_type_no)
            {
                case "0"://成品鞋
                    UC_F_QCM_Ex_Item_Set_New_CPX curr_cpx_ucItem = (UC_F_QCM_Ex_Item_Set_New_CPX)currItem;
                    //尺码标编号
                    update_head_info.Add("CMBBH", curr_cpx_ucItem.txt_cpx_cmbbh.Text);
                    //年龄性别
                    update_head_info.Add("GENDER", curr_cpx_ucItem.cmb_cpx_xb.SelectedValue == null ? "" : curr_cpx_ucItem.cmb_cpx_xb.SelectedValue);
                    update_head_info.Add("GENDER_NAME", curr_cpx_ucItem.cmb_cpx_xb.Text);
                    //成品种类
                    update_head_info.Add("CP_TYPE_CODE", curr_cpx_ucItem.cmb_cpx_cpzl.SelectedValue == null ? "" : curr_cpx_ucItem.cmb_cpx_cpzl.SelectedValue);
                    update_head_info.Add("CP_TYPE_NAME", curr_cpx_ucItem.cmb_cpx_cpzl.Text);
                    //Test ID
                    update_head_info.Add("TEST_ID", curr_cpx_ucItem.txt_cpx_test_id.Text);
                    //阶段
                    update_head_info.Add("PHASE_CREATION_NO", curr_cpx_ucItem.cmb_cpx_jd.SelectedValue == null ? "" : curr_cpx_ucItem.cmb_cpx_jd.SelectedValue);
                    update_head_info.Add("PHASE_CREATION_NAME", curr_cpx_ucItem.cmb_cpx_jd.Text);
                    //送测数量 
                    update_head_info.Add("SEND_TEST_QTY", curr_cpx_ucItem.txt_cpx_scsl.Text);
                    //Size
                    update_head_info.Add("SIZES", curr_cpx_ucItem.cmb_cpx_size.Text);
                    //订单 Po
                    update_head_info.Add("ORDER_PO", curr_cpx_ucItem.txt_cpx_ddpo.Text);
                    //送测原因
                    update_head_info.Add("TEST_REASON", curr_cpx_ucItem.txt_cpx_reason.Text);
                     
                    break;
                case "1"://部件
                    UC_F_QCM_Ex_Item_Set_New_BJ curr_bj_ucItem = (UC_F_QCM_Ex_Item_Set_New_BJ)currItem;
                    //部位名称
                    update_head_info.Add("POSITION_CODE", curr_bj_ucItem.cmb_bj_bwmc.SelectedValue == null ? "" : curr_bj_ucItem.cmb_bj_bwmc.SelectedValue);
                    update_head_info.Add("POSITION_NAME", curr_bj_ucItem.cmb_bj_bwmc.Text);
                    //阶段
                    update_head_info.Add("PHASE_CREATION_NO", curr_bj_ucItem.cmb_bj_jieduan.SelectedValue == null ? "" : curr_bj_ucItem.cmb_bj_jieduan.SelectedValue);
                    update_head_info.Add("PHASE_CREATION_NAME", curr_bj_ucItem.cmb_bj_jieduan.Text);
                    //送测数量 
                    update_head_info.Add("SEND_TEST_QTY", curr_bj_ucItem.txt_bj_scsl.Text);
                    //Size
                    update_head_info.Add("SIZES", curr_bj_ucItem.cmb_bj_size.Text);
                    //订单 Po
                    update_head_info.Add("ORDER_PO", curr_bj_ucItem.txt_bj_po_order.Text);
                    //厂商
                    update_head_info.Add("MANUFACTURER_CODE", curr_bj_ucItem.lab_bj_cs_code.Text);
                    update_head_info.Add("MANUFACTURER_NAME", curr_bj_ucItem.txt_bj_cs.Text);
                    update_head_info.Add("MANUFACTURER_JC", curr_bj_ucItem.lab_bj_cs_jc.Text);
                    //送测原因
                    update_head_info.Add("TEST_REASON", curr_bj_ucItem.txt_bj_reasaon.Text);
                    //Test Id
                    update_head_info.Add("TEST_ID", curr_bj_ucItem.tb_bj_test_id.Text);
                    break;
                case "2"://工艺
                    UC_F_QCM_Ex_Item_Set_New_GY curr_gy_ucItem = (UC_F_QCM_Ex_Item_Set_New_GY)currItem;
                    //工艺名称
                    update_head_info.Add("WORKMANSHIP_CODE", curr_gy_ucItem.cmb_gy_gymc.SelectedValue == null ? "" : curr_gy_ucItem.cmb_gy_gymc.SelectedValue);
                    update_head_info.Add("WORKMANSHIP_NAME", curr_gy_ucItem.cmb_gy_gymc.Text);
                    //部位名称
                    update_head_info.Add("POSITION_CODE", curr_gy_ucItem.cmb_gy_bwmc.SelectedValue == null ? "" : curr_gy_ucItem.cmb_gy_bwmc.SelectedValue);
                    update_head_info.Add("POSITION_NAME", curr_gy_ucItem.cmb_gy_bwmc.Text);
                    //阶段
                    update_head_info.Add("PHASE_CREATION_NO", curr_gy_ucItem.cmb_gy_jieduan.SelectedValue == null ? "" : curr_gy_ucItem.cmb_gy_jieduan.SelectedValue);
                    update_head_info.Add("PHASE_CREATION_NAME", curr_gy_ucItem.cmb_gy_jieduan.Text);
                    //送测数量 
                    update_head_info.Add("SEND_TEST_QTY", curr_gy_ucItem.txt_gy_scsl.Text);
                    //送测原因
                    update_head_info.Add("TEST_REASON", curr_gy_ucItem.txt_gy_reason.Text);
                    break;
                case "3"://材料
                    UC_F_QCM_Ex_Item_Set_New_CL curr_cl_ucItem = (UC_F_QCM_Ex_Item_Set_New_CL)currItem;
                    update_head_info.Add("TEST_ID", curr_cl_ucItem.txt_cl_test_id.Text);
                    update_head_info.Add("TEST_REASON", curr_cl_ucItem.txt_cl_reason.Text);
                    //ucItem.ckb_cl_sfcc.Checked = string.IsNullOrEmpty(info["RETEST_TASK_NO"].ToString()) ? false : true;//材料--是否重测
                    //ucItem.txt_cl_cc_task_no.Text = info["RETEST_TASK_NO"].ToString();//材料--重测实验室编号
                    //ucItem.cmb_cl_fgt.Text = info["FGT_NAME"].ToString();//材料--材料送测类型
                    //ucItem.txt_cl_test_id.Text = info["TEST_ID"].ToString();//材料--Test ID
                    //ucItem.lab_cl_cs_code.Text = info["MANUFACTURER_CODE"].ToString();//材料--厂商 code
                    //ucItem.txt_cl_cs.Text = info["MANUFACTURER_NAME"].ToString();//材料--厂商 名称
                    //ucItem.lab_cl_cs_jc.Text = info["MANUFACTURER_JC"].ToString();//材料--厂商 简称
                    //ucItem.txt_cl_task_no.Text = info["TASK_NO"].ToString();//材料--实验室编号
                    //ucItem.txt_cl_reason.Text = info["TEST_REASON"].ToString();//材料--送测原因
                    break;
                case "4"://量产拉力
                    UC_F_QCM_Ex_Item_Set_New_LCLI curr_lcll_ucItem = (UC_F_QCM_Ex_Item_Set_New_LCLI)currItem;
                    //产线
                    update_head_info.Add("LINE_CODE", curr_lcll_ucItem.cmb_lcll_line.SelectedValue == null ? "" : curr_lcll_ucItem.cmb_lcll_line.SelectedValue);
                    update_head_info.Add("LINE_NAME", curr_lcll_ucItem.cmb_lcll_line.Text);
                    //尺码标编号
                    update_head_info.Add("CMBBH", curr_lcll_ucItem.txt_lcll_cmbbh.Text);
                    //阶段
                    update_head_info.Add("PHASE_CREATION_NO", curr_lcll_ucItem.cmb_lcll_jieduan.SelectedValue == null ? "" : curr_lcll_ucItem.cmb_lcll_jieduan.SelectedValue);
                    update_head_info.Add("PHASE_CREATION_NAME", curr_lcll_ucItem.cmb_lcll_jieduan.Text);
                    //送测数量 
                    update_head_info.Add("SEND_TEST_QTY", curr_lcll_ucItem.txt_lcll_scsl.Text);
                    //Size
                    update_head_info.Add("SIZES", curr_lcll_ucItem.cmb_lcll_size.Text);
                    //鞋子抽测时间
                    update_head_info.Add("TEST_TIME", curr_lcll_ucItem.txt_lcll_test_time.Text);
                    //订单 Po
                    update_head_info.Add("ORDER_PO", curr_lcll_ucItem.txt_lcll_po_order.Text);
                    //胶水信息
                    update_head_info.Add("GLUE", curr_lcll_ucItem.txt_lcll_jsxx.Text);
                    //送测原因
                    update_head_info.Add("TEST_REASON", curr_lcll_ucItem.txt_lcll_reason.Text);
                    break;
                default:
                    break;
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataGridViewRow item in dgv.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("d_id", item.Cells["d_id"].Value == null ? "" : item.Cells["d_id"].Value.ToString());
                dic.Add("task_no", txt_task_no.Text.Trim());
                dic.Add("source", item.Cells["type"].Value.ToString() == "DQA测试任务" ? "0" : "1");
                dic.Add("inspection_code", item.Cells["inspection_code"].Value == null ? "" : item.Cells["inspection_code"].Value.ToString());//检测项目编号
                dic.Add("inspection_name", item.Cells["inspection_name"].Value == null ? "" : item.Cells["inspection_name"].Value.ToString());//检测项目
                dic.Add("inspection_type", item.Cells["inspection_type"].Value == null ? "" : item.Cells["inspection_type"].Value.ToString());
                dic.Add("judgment_criteria", item.Cells["judgment_criteria"].Value == null ? "" : item.Cells["judgment_criteria"].Value.ToString());
                dic.Add("judge_type", item.Cells["judge_type"].Value == null ? "" : item.Cells["judge_type"].Value.ToString());
                dic.Add("standard_value", item.Cells["standard_value"].Value == null ? "" : item.Cells["standard_value"].Value.ToString());
                dic.Add("unit", item.Cells["unit"].Value == null ? "" : item.Cells["unit"].Value.ToString());
                dic.Add("sample_qty", item.Cells["sample_qty"].Value == null ? "" : item.Cells["sample_qty"].Value.ToString());
                dic.Add("g_formula_code", item.Cells["tygs"].Value == null ? "" : item.Cells["tygs"].Value.ToString());
                dic.Add("d_formula_code", item.Cells["zdygs"].Value == null ? "" : item.Cells["zdygs"].Value.ToString());
                dic.Add("art_d_remark", item.Cells["remarks"].Value == null ? "" : item.Cells["remarks"].Value.ToString());
                dic.Add("choice_no", item.Cells["choice_no"].Value == null ? "" : item.Cells["choice_no"].Value.ToString());
                dic.Add("choice_name", item.Cells["choice_name"].Value == null ? "" : item.Cells["choice_name"].Value.ToString());

                if (string.IsNullOrEmpty(dic["inspection_code"].ToString()))
                {
                    MessageBox.Show("Please enter the test item number");
                    item.Cells["inspection_code"].Selected = true;
                    return;
                }
                if (string.IsNullOrEmpty(dic["inspection_name"].ToString())&&string.IsNullOrEmpty(dic["d_id"].ToString()))
                {
                    MessageBox.Show("Please enter the detection item name");
                    item.Cells["inspection_name"].Selected = true;
                    return;
                }
                if (string.IsNullOrEmpty(dic["judgment_criteria"].ToString()))
                {
                    MessageBox.Show("Please select the judgment standard");
                    item.Cells["judgment_criteria"].Selected = true;
                    return;
                }
                if (string.IsNullOrEmpty(dic["judge_type"].ToString()))
                {
                    MessageBox.Show("Please select a judgment type");
                    item.Cells["judge_type"].Selected = true;
                    return;
                }

                if (string.IsNullOrEmpty(dic["standard_value"].ToString()))
                {
                    MessageBox.Show("Please enter the measurement standard");
                    item.Cells["standard_value"].Selected = true;
                    return;
                }
                else
                {
                    if (dic["judge_type"].ToString() == "1")
                    {
                        decimal standard_value = 0;
                        bool isDeci = decimal.TryParse(dic["standard_value"].ToString(), out standard_value);
                        if (!isDeci)
                        {
                            MessageBox.Show("Please enter the measurement standard in the correct number format");
                            item.Cells["standard_value"].Selected = true;
                            return;
                        }
                    }
                    else
                    {
                        if (!dic["standard_value"].ToString().Contains('~'))
                        {
                            MessageBox.Show("Please enter the measurement standard in the correct format (format such as: upper and lower limits (10~20), error value（100~0.1）)");
                            item.Cells["standard_value"].Selected = true;
                            return;
                        }
                        else
                        {
                            var list_value = dic["standard_value"].ToString().Split('~').ToList();
                            decimal standard_value1 = 0;
                            decimal standard_value2 = 0;
                            bool isDeci1 = decimal.TryParse(list_value[0], out standard_value1);
                            bool isDeci2 = decimal.TryParse(list_value[1], out standard_value2);
                            if (!isDeci1 || !isDeci2)
                            {
                                MessageBox.Show("Please enter the measurement standard in the correct format (format such as: upper and lower limits (10~20), error value（100~0.1）)");
                                item.Cells["standard_value"].Selected = true;
                                return;
                            }
                        }
                    }
                }

                int qty = 0;
                int.TryParse(dic["sample_qty"].ToString(), out qty);
                if (qty < 1)
                {
                    MessageBox.Show("Please enter an integer sample quantity >=1");
                    item.Cells["sample_qty"].Selected = true;
                    return;
                }

                if (string.IsNullOrEmpty(dic["g_formula_code"].ToString()))
                {
                    MessageBox.Show("Please select a general formula type");
                    item.Cells["tygs"].Selected = true;
                    return;
                }

                if (string.IsNullOrEmpty(dic["g_formula_code"].ToString()))
                {
                    MessageBox.Show("Please select a custom formula type");
                    item.Cells["zdygs"].Selected = true;
                    return;
                }


                list.Add(dic);
            }
            if (update_head_info.Count() > 0 || list.Count > 0)
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", _task_no);
                p.Add("head", update_head_info);
                p.Add("list", list);
                p.Add("delete_list", deleteIds);
                p.Add("staff_name", txt_staff_name.Text.Trim());
                p.Add("staff_no", txt_staff_no.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveItemCheck",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show("Failed to get data");
                    return;
                }
                getdate();
                MessageBox.Show("Saved successfully");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            F_QCM_Ex_Task_Print frm = new F_QCM_Ex_Task_Print(txt_task_no.Text.Trim());
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void dgv_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name == "action")
                {
                    string d_id = dgv.Rows[e.RowIndex].Cells["d_id"].Value.ToString();
                    if (!string.IsNullOrEmpty(d_id))
                        deleteIds.Add(d_id);
                    dgv.Rows.Remove(dgv.Rows[e.RowIndex]);
                }
            }
        }
    }
}
