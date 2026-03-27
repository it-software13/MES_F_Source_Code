using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
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
    public partial class F_QCM_Ex_LookResult_New : MaterialForm
    {
        public string _task_no = "";
        public string _task_no_type = "";
        public DataTable D_JUDGMENT_CRITERIA = new DataTable();
        private readonly MaterialSkinManager materialSkinManager;
        DataTable ITEM_LIST;
        UserControl currItem;

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
        public F_QCM_Ex_LookResult_New(string task_no)
        {
            InitializeComponent();
            _task_no = task_no;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

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

        public F_QCM_Ex_LookResult_New(string task_no, SJeMES_Framework.Class.ClientClass client)
        {
            Program.Client = client;
            InitializeComponent();
            _task_no = task_no;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            GetALLDDLData();
            list_fgt_data = GetFGTInfo();
            list_size_data = GetSizeInfo();
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

        public void getdate()
        {
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
            ITEM_LIST = itemlist;
            _task_no_type = info["TEST_TYPE"].ToString();

            panel1.Controls.Clear();
            if (info["TEST_TYPE"].ToString() == "0")
            {
               // txt_test_type.Text = "成品鞋";
                txt_test_type.Text = "Processing-finishedshoes";
                UC_F_QCM_Ex_Item_Set_New_CPX ucItem = new UC_F_QCM_Ex_Item_Set_New_CPX(true);
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

                panel1.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;
            }
            if (info["TEST_TYPE"].ToString() == "1")
            {
                //txt_test_type.Text = "部件";
                txt_test_type.Text = "part";
                UC_F_QCM_Ex_Item_Set_New_BJ ucItem = new UC_F_QCM_Ex_Item_Set_New_BJ(true);
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

                panel1.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;
            }
            if (info["TEST_TYPE"].ToString() == "2")
            {
                //txt_test_type.Text = "工艺";
                txt_test_type.Text = "craft";
                UC_F_QCM_Ex_Item_Set_New_GY ucItem = new UC_F_QCM_Ex_Item_Set_New_GY(true);
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

                panel1.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;
            }
            if (info["TEST_TYPE"].ToString() == "3")
            {
                //txt_test_type.Text = "材料";
                txt_test_type.Text = "Material";
                UC_F_QCM_Ex_Item_Set_New_CL ucItem = new UC_F_QCM_Ex_Item_Set_New_CL(true);
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

                panel1.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;
            }
            if (info["TEST_TYPE"].ToString() == "4")
            {
                txt_test_type.Text = "ProductionRally";
                UC_F_QCM_Ex_Item_Set_New_LCLI ucItem = new UC_F_QCM_Ex_Item_Set_New_LCLI(true);
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

                panel1.Controls.Add(ucItem);
                ucItem.Dock = DockStyle.Fill;

                currItem = ucItem;

            }

            //txt_task_no.Text = info["TASK_NO"].ToString();

            //string test_type = "";

            //if (info["TEST_TYPE"].ToString() == "0")
            //{
            //    test_type = "成品鞋";
            //}
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
            // txt_test_type.Text = test_type;
            // txt_art.Text = info["ART_NO"].ToString();
            // txt_po_order.Text = info["ORDER_PO"].ToString();
            // txt_material_id.Text = info["MATERIAL_WAY"].ToString();
            //// txt_line.Text = info["LINE_NAME"].ToString();
            // txt_shose.Text = info["SHOE_NO"].ToString();
            // txt_po_qty.Text = info["ORDER_PO_QTY"].ToString();
            // //txt_bjmc.Text = info["PARTS_NAME"].ToString();
            // txt_cs.Text = info["MANUFACTURER_NAME"].ToString();
            // txt_category.Text = info["CATEGORY_NAME"].ToString();
            // txt_jianduan.Text = info["PHASE_CREATION_NAME"].ToString();
            // txt_bwmc.Text = info["POSITION_NAME"].ToString();
            // //txt_fgt.Text = info["FGT_NAME"].ToString();
            // txt_cpjb.Text = info["PRODUCT_LEVEL_VALUE"].ToString();
            // txt_scsl.Text = info["SEND_TEST_QTY"].ToString();
            //// txt_gymc.Text = info["MANUFACTURER_NAME"].ToString();
            // txt_clid.Text = info["MAKINGS_ID"].ToString();
            // txt_jd.Text = info["SEASON"].ToString();
            // //txt_size.Text = info["SIZES"].ToString();
            // txt_clzl.Text = info["MAKINGS_TYPE_NAME"].ToString();
            // txt_wlmc.Text = info["MATERIAL_NAME"].ToString();
            // //txt_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();
            // //txt_xb.Text = info["GENDER"].ToString();
            // txt_ys.Text = info["COLORS"].ToString();
            // //txt_reason.Text = info["TEST_REASON"].ToString();
            //// txt_jsxx.Text = info["GLUE"].ToString();
            // //txt_staff_no.Text = info["STAFF_NO"].ToString();
            // txt_staff_name.Text = info["STAFF_NAME"].ToString();
            // txt_department.Text = info["STAFF_DEPARTMENT"].ToString();
            // tb_test_id.Text = info["TEST_ID"].ToString();

            dgv.Rows.Clear();
            foreach (DataRow item in itemlist.Rows)
            {
                int i = dgv.Rows.Add();
                dgv.Rows[i].Cells["xh"].Value = (i + 1).ToString();
              
                dgv.Rows[i].Cells["inspection_code"].Value = item["INSPECTION_CODE"].ToString();
                dgv.Rows[i].Cells["inspection_name"].Value = item["INSPECTION_NAME"].ToString();
                dgv.Rows[i].Cells["standard_value"].Value = item["STANDARD_VALUE"].ToString();
                dgv.Rows[i].Cells["scjg"].Value = item["ITEM_TEST_VAL"].ToString();
                dgv.Rows[i].Cells["pdjg"].Value = item["ITEM_TEST_RESULT"].ToString();
                dgv.Rows[i].Cells["remark"].Value = item["REMARK"].ToString();
                dgv.Rows[i].Cells["id"].Value = item["ID"].ToString();


            }
            lab_result.Text = info["TEST_RESULT"].ToString();
            if(info["TEST_RESULT"].ToString()=="PASS")
            {
                lab_result.ForeColor = Color.Green;
            }
            else
            {
                lab_result.ForeColor = Color.Red;
            }
        }

        private void F_QCM_Ex_LookResult_Load(object sender, EventArgs e)
        {
            //D_JUDGMENT_CRITERIA = Get_JUDGMENT_CRITERIA();
            getdate();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> HeadDic = new Dictionary<string, string>();

                switch (_task_no_type)
                {
                    case "0"://成品鞋
                        UC_F_QCM_Ex_Item_Set_New_CPX curr_cpx_ucItem = (UC_F_QCM_Ex_Item_Set_New_CPX)currItem;
                        //HeadDic.Add("是否重测：", curr_cpx_ucItem.ckb_cpx_sfcc.Checked ? "是" : "否");
                        //HeadDic.Add("重测实验室编号：", curr_cpx_ucItem.txt_cpx_cc_task_no.Text);
                        //HeadDic.Add("扫描ART条码：", curr_cpx_ucItem.txt_cpx_qrcode.Text);
                        //HeadDic.Add("鞋型名称：", curr_cpx_ucItem.txt_cpx_shose.Text);
                        //HeadDic.Add("ART：", curr_cpx_ucItem.txt_cpx_art.Text);
                        //HeadDic.Add("Model No：", curr_cpx_ucItem.txt_cpx_model_no.Text);
                        //HeadDic.Add("Category：", curr_cpx_ucItem.tb_cpx_category.Text);
                        //HeadDic.Add("尺码标编号：", curr_cpx_ucItem.txt_cpx_cmbbh.Text);
                        //HeadDic.Add("产品级别：", curr_cpx_ucItem.tb_cpx_cpjb.Text);
                        //HeadDic.Add("季度：", curr_cpx_ucItem.txt_cpx_jidu.Text);
                        //HeadDic.Add("新旧级别：", curr_cpx_ucItem.tb_cpx_xjjb.Text);
                        //HeadDic.Add("年龄性别：", curr_cpx_ucItem.cmb_cpx_xb.Text);
                        //HeadDic.Add("成品种类：", curr_cpx_ucItem.cmb_cpx_cpzl.Text);
                        //HeadDic.Add("Test ID：", curr_cpx_ucItem.txt_cpx_test_id.Text);
                        //HeadDic.Add("阶段：", curr_cpx_ucItem.cmb_cpx_jd.Text);
                        //HeadDic.Add("送测数量：", curr_cpx_ucItem.txt_cpx_scsl.Text + "只");
                        //HeadDic.Add("Size：", curr_cpx_ucItem.cmb_cpx_size.Text);
                        //HeadDic.Add("订单PO：", curr_cpx_ucItem.txt_cpx_ddpo.Text);
                        //HeadDic.Add("PO数量：", curr_cpx_ucItem.txt_cpx_posl.Text);
                        //HeadDic.Add("实验室编号：", curr_cpx_ucItem.txt_cpx_task_no.Text);
                        //HeadDic.Add("FGT送测类型：", curr_cpx_ucItem.cmb_cpx_fgt.Text);
                        //HeadDic.Add("送测原因：", curr_cpx_ucItem.txt_cpx_reason.Text);
                        HeadDic.Add("Whether_to_retest：", curr_cpx_ucItem.ckb_cpx_sfcc.Checked ? "Yes" : "No");
                        HeadDic.Add("Retest_Lab_No.：", curr_cpx_ucItem.txt_cpx_cc_task_no.Text);
                        HeadDic.Add("Scan_ART_barcode：", curr_cpx_ucItem.txt_cpx_qrcode.Text);
                        HeadDic.Add("shoe_type_name：", curr_cpx_ucItem.txt_cpx_shose.Text);
                        HeadDic.Add("ART：", curr_cpx_ucItem.txt_cpx_art.Text);
                        HeadDic.Add("Model_No：", curr_cpx_ucItem.txt_cpx_model_no.Text);
                        HeadDic.Add("Category：", curr_cpx_ucItem.tb_cpx_category.Text);
                        HeadDic.Add("Size_label_number：", curr_cpx_ucItem.txt_cpx_cmbbh.Text);
                        HeadDic.Add("product_level：", curr_cpx_ucItem.tb_cpx_cpjb.Text);
                        HeadDic.Add("the_quarter：", curr_cpx_ucItem.txt_cpx_jidu.Text);
                        HeadDic.Add("old_and_new_levels：", curr_cpx_ucItem.tb_cpx_xjjb.Text);
                        HeadDic.Add("age_gender：", curr_cpx_ucItem.cmb_cpx_xb.Text);
                        HeadDic.Add("Finished_product_type：", curr_cpx_ucItem.cmb_cpx_cpzl.Text);
                        HeadDic.Add("Test_ID：", curr_cpx_ucItem.txt_cpx_test_id.Text);
                        HeadDic.Add("stage：", curr_cpx_ucItem.cmb_cpx_jd.Text);
                        HeadDic.Add("Quantity_to_be_tested：", curr_cpx_ucItem.txt_cpx_scsl.Text + "Only");
                        HeadDic.Add("Size：", curr_cpx_ucItem.cmb_cpx_size.Text);
                        HeadDic.Add("Order_PO：", curr_cpx_ucItem.txt_cpx_ddpo.Text);
                        HeadDic.Add("Number_of_POs：", curr_cpx_ucItem.txt_cpx_posl.Text);
                        HeadDic.Add("laboratory_Number：", curr_cpx_ucItem.txt_cpx_task_no.Text);
                        HeadDic.Add("FGT_test_Type：", curr_cpx_ucItem.cmb_cpx_fgt.Text);
                        HeadDic.Add("Reason_for_Testing：", curr_cpx_ucItem.txt_cpx_reason.Text);
                        break;
                    case "1"://部件
                        UC_F_QCM_Ex_Item_Set_New_BJ curr_bj_ucItem = (UC_F_QCM_Ex_Item_Set_New_BJ)currItem;
                        HeadDic.Add("Whether_to_retest：", curr_bj_ucItem.ckb_bj_sfcc.Checked ? "Yes" : "No");
                        HeadDic.Add("Retest_Lab_No：", curr_bj_ucItem.txt_bj_cc_task_no.Text);
                        HeadDic.Add("ART：", curr_bj_ucItem.txt_bj_art.Text);
                        HeadDic.Add("shoe_type_name：", curr_bj_ucItem.txt_bj_shose.Text);
                        HeadDic.Add("Part_Name：", curr_bj_ucItem.cmb_bj_bwmc.Text);
                        HeadDic.Add("Model_No：", curr_bj_ucItem.txt_bj_model_no.Text);
                        HeadDic.Add("Category：", curr_bj_ucItem.tb_bj_kfxl.Text);
                        HeadDic.Add("product_level：", curr_bj_ucItem.tb_bj_cpjb.Text);
                        HeadDic.Add("the_quarter：", curr_bj_ucItem.txt_bj_jidu.Text);
                        HeadDic.Add("old_and_new_levels：", curr_bj_ucItem.txt_bj_xjjb.Text);
                        HeadDic.Add("age_gender：", curr_bj_ucItem.txt_bj_xb.Text);
                        HeadDic.Add("stage：", curr_bj_ucItem.cmb_bj_jieduan.Text);
                        HeadDic.Add("Quantity_to_be_tested：", curr_bj_ucItem.txt_bj_scsl.Text + "Only");
                        HeadDic.Add("Size：", curr_bj_ucItem.cmb_bj_size.Text);
                        HeadDic.Add("Order_PO：", curr_bj_ucItem.txt_bj_po_order.Text);
                        HeadDic.Add("Number_of_POs：", curr_bj_ucItem.txt_bj_po_qty.Text);
                        HeadDic.Add("laboratory_Number：", curr_bj_ucItem.txt_bj_task_no.Text);
                        HeadDic.Add("FGT_test_Type：", curr_bj_ucItem.cmb_bj_fgt.Text);
                        HeadDic.Add("Manufacturers：", curr_bj_ucItem.txt_bj_cs.Text);
                        HeadDic.Add("Reason_for_Testing：", curr_bj_ucItem.txt_bj_reasaon.Text);
                        HeadDic.Add("Test Id：", curr_bj_ucItem.tb_bj_test_id.Text);
                        break;
                    case "2"://工艺
                        UC_F_QCM_Ex_Item_Set_New_GY curr_gy_ucItem = (UC_F_QCM_Ex_Item_Set_New_GY)currItem;
                        HeadDic.Add("Whether_to_retest：", curr_gy_ucItem.ckb_gy_sfcc.Checked ? "Yes" : "No");
                        HeadDic.Add("Retest_Lab_No：", curr_gy_ucItem.txt_gy_cc_task_no.Text);
                        HeadDic.Add("ART：", curr_gy_ucItem.txt_gy_art.Text);
                        HeadDic.Add("shoe_type_name：", curr_gy_ucItem.txt_gy_shose.Text);
                        HeadDic.Add("Process_name：", curr_gy_ucItem.cmb_gy_gymc.Text);
                        HeadDic.Add("part name：", curr_gy_ucItem.cmb_gy_bwmc.Text);
                        HeadDic.Add("Category：", curr_gy_ucItem.tb_gy_kfxl.Text);
                        HeadDic.Add("product_level：", curr_gy_ucItem.txt_gy_cpjb.Text);
                        HeadDic.Add("the_quarter：", curr_gy_ucItem.txt_gy_jidu.Text);
                        HeadDic.Add("stage：", curr_gy_ucItem.cmb_gy_jieduan.Text);
                        HeadDic.Add("Quantity_to_be_tested：", curr_gy_ucItem.txt_gy_scsl.Text + "Piece/Piece");
                        HeadDic.Add("laboratory_Number：", curr_gy_ucItem.txt_gy_task_no.Text);
                        HeadDic.Add("FGT_test_Type：", curr_gy_ucItem.cmb_gy_fgt.Text);
                        HeadDic.Add("Manufacturers：", curr_gy_ucItem.txt_gy_cs.Text);
                        HeadDic.Add("Reason_for_Testing：", curr_gy_ucItem.txt_gy_reason.Text);
                        break;
                    case "3"://材料
                        UC_F_QCM_Ex_Item_Set_New_CL curr_cl_ucItem = (UC_F_QCM_Ex_Item_Set_New_CL)currItem;
                        HeadDic.Add("Whether_to_retest：", curr_cl_ucItem.ckb_cl_sfcc.Checked ? "Yes" : "No");
                        HeadDic.Add("Retest_Lab_No：", curr_cl_ucItem.txt_cl_cc_task_no.Text);
                        //HeadDic.Add("料号：", curr_cl_ucItem.tb_cl_lh.Text);
                        HeadDic.Add("Part_No：", curr_cl_ucItem.tb_cl_lh.Text);
                        //HeadDic.Add("材料名称：", curr_cl_ucItem.tb_cl_clmc.Text);
                        HeadDic.Add("Material_Name：", curr_cl_ucItem.tb_cl_clmc.Text);
                       // HeadDic.Add("所用部位名称：", curr_cl_ucItem.tb_cl_suoyongbuweimingcheng.Text);
                        HeadDic.Add("Part_name_used：", curr_cl_ucItem.tb_cl_suoyongbuweimingcheng.Text);
                        HeadDic.Add("Manufacturers：", curr_cl_ucItem.txt_cl_cs.Text);
                        //HeadDic.Add("材料送测类型：", curr_cl_ucItem.cmb_cl_fgt.Text);
                        HeadDic.Add("Material_delivery_type：", curr_cl_ucItem.cmb_cl_fgt.Text);
                        HeadDic.Add("TEST ID：", curr_cl_ucItem.txt_cl_test_id.Text);
                        //HeadDic.Add("鞋型/ART：", curr_cl_ucItem.tb_artandshoe.Text);
                        HeadDic.Add("Shoe_type/ART：", curr_cl_ucItem.tb_artandshoe.Text);
                        HeadDic.Add("Order_Number/Quantity：", curr_cl_ucItem.tb_order_number.Text);
                        HeadDic.Add("Reason_for_Testing：", curr_cl_ucItem.txt_cl_reason.Text);
                        HeadDic.Add("laboratory_Number：", curr_cl_ucItem.txt_cl_task_no.Text);
                        break;
                    case "4"://量产拉力
                        UC_F_QCM_Ex_Item_Set_New_LCLI curr_lcll_ucItem = (UC_F_QCM_Ex_Item_Set_New_LCLI)currItem;
                        HeadDic.Add("Whether_to_retest：", curr_lcll_ucItem.ckb_lcll_sfcc.Checked ? "Yes" : "No");
                        HeadDic.Add("Retest_Lab_No：", curr_lcll_ucItem.txt_lcll_cc_task_no.Text);
                        HeadDic.Add("ART：", curr_lcll_ucItem.txt_lcll_art.Text);
                        HeadDic.Add("shoe_type_name：", curr_lcll_ucItem.txt_lcll_shose.Text);
                        HeadDic.Add("Category：", curr_lcll_ucItem.txt_lcll_category.Text);
                        //HeadDic.Add("产线：", curr_lcll_ucItem.cmb_lcll_line.Text);
                        HeadDic.Add("Production_line：", curr_lcll_ucItem.cmb_lcll_line.Text);
                        HeadDic.Add("Size_label_number：", curr_lcll_ucItem.txt_lcll_cmbbh.Text);
                        HeadDic.Add("the_quarter：", curr_lcll_ucItem.txt_lcll_jd.Text);
                        HeadDic.Add("stage：", curr_lcll_ucItem.cmb_lcll_jieduan.Text);
                        HeadDic.Add("Quantity_to_be_tested：", curr_lcll_ucItem.txt_lcll_scsl.Text + "Piece/Piece");
                        HeadDic.Add("Size：", curr_lcll_ucItem.cmb_lcll_size.Text);
                        HeadDic.Add("shoe_testing_time：", curr_lcll_ucItem.txt_lcll_test_time.Text);
                        //HeadDic.Add("鞋子抽测时间：", curr_lcll_ucItem.txt_lcll_test_time.Text);
                        HeadDic.Add("Order_PO：", curr_lcll_ucItem.txt_lcll_po_order.Text);
                        HeadDic.Add("Number_of_POs：", curr_lcll_ucItem.txt_lcll_po_qty.Text);
                        HeadDic.Add("laboratory_Number：", curr_lcll_ucItem.txt_lcll_task_no.Text);
                        HeadDic.Add("Glue/Treatment_Information：", curr_lcll_ucItem.txt_lcll_jsxx.Text);
                        HeadDic.Add("Reason_for_Testing：", curr_lcll_ucItem.txt_lcll_reason.Text);
                        break;
                    default:
                        break;
                }

                //HeadDic.Add("ART：", txt_art.Text);
                //HeadDic.Add("PO数量：", txt_po_qty.Text);
                //HeadDic.Add("Material NO：", txt_material_no.Text);
                //HeadDic.Add("鞋型名称：", txt_shose.Text);
                //HeadDic.Add("物料名称：", txt_wlmc.Text);
                //HeadDic.Add("送测人姓名：", txt_staff_name.Text);
                //HeadDic.Add("category：", txt_category.Text);
                //HeadDic.Add("材料ID：", txt_clid.Text);
                //HeadDic.Add("部门：", txt_department.Text);
                //HeadDic.Add("产品级别：", txt_cpjb.Text);
                //HeadDic.Add("使用部位：", txt_bwmc.Text);
                //HeadDic.Add("检验项目：", txt_test_type.Text);
                //HeadDic.Add("订单PO：", txt_po_order.Text);
                //HeadDic.Add("厂商：", txt_cs.Text);
                //HeadDic.Add("任务编号：", txt_task_no.Text);
                //HeadDic.Add("季度：", txt_jd.Text);
                //HeadDic.Add("阶段：", txt_jianduan.Text);
                //HeadDic.Add("TEST ID：", tb_test_id.Text);
                //HeadDic.Add("送测数量：", txt_scsl.Text);
                //HeadDic.Add("材料种类：", txt_clzl.Text);
                //HeadDic.Add("颜色：", txt_ys.Text);
                //HeadDic.Add("Material ID：", txt_material_id.Text);
                HeadDic.Add("Test_Result：", lab_result.Text);

                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("xh", "Serial_Number");
                Execldic.Add("INSPECTION_CODE".ToLower(), "INSPECTION_CODE");
                Execldic.Add("INSPECTION_NAME".ToLower(), "INSPECTION_NAME");
                Execldic.Add("STANDARD_VALUE".ToLower(), "STANDARD_VALUE");
                Execldic.Add("ITEM_TEST_VAL".ToLower(), "ITEM_TEST_VAL");
                Execldic.Add("ITEM_TEST_RESULT".ToLower(), "ITEM_TEST_RESULT");
                Execldic.Add("REMARK".ToLower(), "REMARK");

                if (!ITEM_LIST.Columns.Contains("xh"))
                    ITEM_LIST.Columns.Add("xh", typeof(string));

                List<string> removeCol = new List<string>();
                foreach (DataColumn item in ITEM_LIST.Columns)
                {
                    if (!Execldic.Keys.Contains(item.ColumnName.ToLower()))
                        removeCol.Add(item.ColumnName);
                }

                foreach (var item in removeCol)
                {
                    ITEM_LIST.Columns.Remove(item);
                }

                int i = 1;
                foreach (DataRow item in ITEM_LIST.Rows)
                {
                    item["xh"] = i.ToString();
                    i++;
                }

                ITEM_LIST.Columns["xh"].SetOrdinal(0);
                ITEM_LIST.Columns["INSPECTION_NAME"].SetOrdinal(1);
                ITEM_LIST.Columns["INSPECTION_CODE"].SetOrdinal(2);
                ITEM_LIST.Columns["STANDARD_VALUE"].SetOrdinal(3);
                ITEM_LIST.Columns["ITEM_TEST_VAL"].SetOrdinal(4);
                ITEM_LIST.Columns["ITEM_TEST_RESULT"].SetOrdinal(5);
                ITEM_LIST.Columns["REMARK"].SetOrdinal(6);

                ExeclHelper.ExportToTrueExcelEx(ITEM_LIST, HeadDic, Execldic, "Laboratory_Test_Report");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgv.Columns[e.ColumnIndex].Name == "search_img")
                {
                    var frm = new F_QCM_Ex_LookResult_Item_Img(dgv.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
            }
        }
    }
}
