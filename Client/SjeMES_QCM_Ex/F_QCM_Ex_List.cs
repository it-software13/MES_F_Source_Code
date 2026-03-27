using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_List()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        public void FormLoad()
        {

            pageControl1.PageSize = 25;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 部门产线视图展示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                ResultObject ret = getdata(pageSize, pageIndex);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int i = dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].ReadOnly = true;
                        dataGridView1.Rows[i].Cells["task_no"].Value = dr["TASK_NO"].ToString();
                        //枚举 0：成品鞋；1：部件；2：工艺；3：材料；4：量产拉力；
                        string test_type = "";
                        if (dr["TEST_TYPE"].ToString() == "0")
                        {
                            test_type = "FinishedShoes_Test";////Processing - finished shoes
                        }
                        if (dr["TEST_TYPE"].ToString() == "1")
                        {
                            //test_type = "part";//部件
                            test_type = "Component_Test";//部件
                        }
                        if (dr["TEST_TYPE"].ToString() == "2")
                        {
                            //test_type = "craft";//craft//工艺
                            test_type = "Treatment";//craft//工艺
                        }
                        if (dr["TEST_TYPE"].ToString() == "3")
                        {
                            test_type = "Material_Test";//材料
                        }
                        if (dr["TEST_TYPE"].ToString() == "4")
                        {
                           // test_type = "ProductionRally";//量产拉力
                            test_type = "Bonding_Test";//量产拉力
                        }
                        dataGridView1.Rows[i].Cells["Type"].Value = dr["TEST_TYPE"].ToString();
                        dataGridView1.Rows[i].Cells["report_upload_status"].Value = dr["Report_Upload_Status"].ToString();
                        dataGridView1.Rows[i].Cells["sclx"].Value = test_type;
                        dataGridView1.Rows[i].Cells["scbm"].Value = dr["STAFF_DEPARTMENT"].ToString();
                        dataGridView1.Rows[i].Cells["scr"].Value = dr["STAFF_NAME"].ToString();
                        dataGridView1.Rows[i].Cells["shose_name"].Value = dr["SHOE_NO"].ToString();
                        dataGridView1.Rows[i].Cells["model_no"].Value = dr["MODEL_NO"].ToString();
                        dataGridView1.Rows[i].Cells["scsj"].Value = dr["CREATEDATE"].ToString() + " " + dr["CREATETIME"].ToString();
                        dataGridView1.Rows[i].Cells["scsl"].Value = dr["SEND_TEST_QTY"].ToString();
                        dataGridView1.Rows[i].Cells["art"].Value = dr["ART_NO"].ToString();
                        dataGridView1.Rows[i].Cells["jd"].Value = dr["PHASE_CREATION_NAME"].ToString();
                        dataGridView1.Rows[i].Cells["cc_task_no"].Value = dr["CC_TASK_NO"].ToString();
                        dataGridView1.Rows[i].Cells["cc_test_result"].Value = dr["CC_TEST_RESULT"].ToString();
                        dataGridView1.Rows[i].Cells["source_task_no"].Value = dr["SOURCE_TASK_NO"].ToString();
                        dataGridView1.Rows[i].Cells["location_code"].Value = dr["LOCATION_CODE"].ToString();

                        //枚举 0：登记；1：已签收；2：检测中；3：存档；4：出库中；5：出库完成；6：已取走；
                        string task_state = "";
                        if (dr["TASK_STATE"].ToString() == "0")
                        {
                            task_state = "to register";//登记
                        }
                        if (dr["TASK_STATE"].ToString() == "1")
                        {
                            task_state = "Have been received";//已签收
                        }
                        if (dr["TASK_STATE"].ToString() == "2")
                        {
                            task_state = "checking";//检测中
                        }
                        if (dr["TASK_STATE"].ToString() == "3")
                        {
                            task_state = "archive";//存档
                        }
                        if (dr["TASK_STATE"].ToString() == "4")
                        {
                            task_state = "out of warehouse";//出库中
                        }
                        if (dr["TASK_STATE"].ToString() == "5")
                        {
                            task_state = "Outbound completed";//出库完成
                        }
                        if (dr["TASK_STATE"].ToString() == "6")
                        {
                            task_state = "taken away";//已取走
                        }
                        dataGridView1.Rows[i].Cells["zt"].Value = task_state;
                        dataGridView1.Rows[i].Cells["csjg"].Value = dr["TEST_RESULT"].ToString();
                        dataGridView1.Rows[i].Cells["scbz"].Value = dr["TEST_REASON"].ToString();
                        dataGridView1.Rows[i].Cells["TESTID"].Value = dr["TEST_ID"].ToString();
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private ResultObject getdata(int pageSize, int pageIndex, string test_type = "", string show_detail = "")
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                p.Add("task_no", txt_task_no.Text.Trim());
                string test_type_code = "";
                //switch (cmb_test_type.Text.Trim())
                //{
                //    case "FinishedShoes-Testing"://成品鞋-测试
                //        test_type_code = "0";
                //        break;
                //    case "part"://部件
                //        test_type_code = "1";
                //        break;
                //    case "craft"://工艺
                //        test_type_code = "2";
                //        break;
                //    case "Material"://材料
                //        test_type_code = "3";
                //        break;
                //    case "ProductionRally"://量产拉力
                //        test_type_code = "4";
                //        break;
                //}

                switch (cmb_test_type.Text.Trim())
                {
                    case "FinishedShoes_Test"://成品鞋-测试
                        test_type_code = "0";
                        break;
                    case "Component_Test"://部件
                        test_type_code = "1";
                        break;
                    case "Treatment"://工艺
                        test_type_code = "2";
                        break;
                    case "Material_Test"://材料
                        test_type_code = "3";
                        break;
                    case "Bonding_Test"://量产拉力
                        test_type_code = "4";
                        break;
                }
                if (!string.IsNullOrEmpty(test_type))
                {
                    test_type_code = test_type;
                }
                if (!string.IsNullOrEmpty(show_detail))
                {
                    p.Add("show_detail", show_detail);
                }
                p.Add("test_type", test_type_code);
                p.Add("source_task_no", txt_source_task_no.Text.Trim());
                p.Add("art", txt_art.Text.Trim());
                p.Add("model_no", txt_model_no.Text.Trim());
                p.Add("test_id", txt_test_id.Text.Trim());
                p.Add("sjr", txt_sjr.Text.Trim());
                p.Add("sjbm", txt_sjbm.Text.Trim());
                p.Add("xxmc", txt_xxmc.Text.Trim());
                p.Add("jieduan", txt_jieduan.Text.Trim());
                p.Add("test_result", cmb_result.Text.Trim());
                if (ckb_date.Checked)
                {
                    p.Add("start_time", t1.Value.ToString("yyyy-MM-dd"));
                    p.Add("end_time", t2.Value.ToString("yyyy-MM-dd"));
                }
                else
                {

                    throw new Exception("The test date is required, please fill in the check box！");
                }
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetTaskList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                return ret;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                //SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                ResultObject ret1 = new ResultObject();
                ret1.IsSuccess = false;
                ret1.ErrMsg = msg;
                return ret1;
            }
        }

        private void F_QCM_Ex_List_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDataList;
            //FormLoad();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "print")
                {

                    F_QCM_TaskNo_Print frm = new F_QCM_TaskNo_Print(dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString());
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "set")
                {
                    F_QCM_Ex_Item_Set_New frm = new F_QCM_Ex_Item_Set_New(dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString());
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "look")
                {
                    //F_QCM_Ex_LookResult frm = new F_QCM_Ex_LookResult(dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString());
                    F_QCM_Ex_LookResult_New frm = new F_QCM_Ex_LookResult_New(dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString());
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();

                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "cundang")
                {
                    using (F_QCM_Ex_file_Edit f = new F_QCM_Ex_file_Edit(dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString()))
                    {
                        f.ShowDialog();
                    }
                }
                //upload
                if (dataGridView1.Columns[e.ColumnIndex].Name == "upload")
                {
                    string task_no = dataGridView1.Rows[e.RowIndex].Cells["task_no"].Value.ToString();
                    string sclx    = dataGridView1.Rows[e.RowIndex].Cells["Type"].Value.ToString();//送测类型代号
                    string art     = dataGridView1.Rows[e.RowIndex].Cells["art"].Value.ToString();//art
                    using (F_QCM_Ex_List_add f = new F_QCM_Ex_List_add(task_no,sclx,art))
                    {
                        f.ShowDialog();
                    }
                }
                //select
                if (dataGridView1.Columns[e.ColumnIndex].Name == "select")
                {
                    string sclx = dataGridView1.Rows[e.RowIndex].Cells["Type"].Value.ToString();//送测类型代号
                    string art = dataGridView1.Rows[e.RowIndex].Cells["art"].Value.ToString();//art 
                    string TASK_NO = dataGridView1.Rows[e.RowIndex].Cells["TASK_NO"].Value.ToString();//art

                    DataTable dt = File_list(art, sclx, TASK_NO);
                    F_QCM_Ex_List_view add = new F_QCM_Ex_List_view(dt);
                    add.ShowDialog();

                }
            }
        }

        public DataTable File_list(string art,string sclx,string TASK_NO)
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("TASK_NO", TASK_NO);
                data.Add("art", art);
                data.Add("sclx", sclx);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "Main_ListFileSc",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        item["FILE_URL"] = Program.Client.PicUrl + item["FILE_URL"]; 
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

            return dt;
        }
        private void btn_cpx_Click(object sender, EventArgs e)
        {
            try
            {
                ResultObject ret = getdata(9999, 1, "0", "1");
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
              
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                Dictionary<string, string> headdic = new Dictionary<string, string>();
                headdic.Add("TASK_NO", "laboratory_Number");
                headdic.Add("ART_NO", "ART");
                headdic.Add("SHOE_NO", "shoe_type_name");
                headdic.Add("MODEL_NO", "MODEL NO");
                headdic.Add("CATEGORY_NAME", "Development_series");
                headdic.Add("CMBBH", "Size_label_number");
                headdic.Add("PRODUCT_LEVEL_VALUE", "product_level");
                headdic.Add("SEASON", "the_quarter");//the_quarter
                headdic.Add("PB_TYPE_LEVEL", "old_and_new_levels");
                headdic.Add("GENDER_NAME", "age_gender");
                headdic.Add("CP_TYPE_NAME", "Finished_product_type");
                headdic.Add("TEST_ID", "TEST ID");
                headdic.Add("PHASE_CREATION_NAME", "stage");
                headdic.Add("SEND_TEST_QTY", "Quantity_to_be_tested");
                headdic.Add("SIZES", "SIZE");
                headdic.Add("ORDER_PO", "Order_PO");
                headdic.Add("ORDER_PO_QTY", "Number_of_POs");
                headdic.Add("FGT_NAME", "FGT_test_Type");
                headdic.Add("TEST_REASON", "Reason_for_Testing");
                headdic.Add("STAFF_NAME", "Person_send_for_Test");
                headdic.Add("STAFF_DEPARTMENT", "Testing_department");

                for (int i = dt.Columns.Count - 1; i >= 0; i--)
                {
                    if (!headdic.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns.Remove(dt.Columns[i].ColumnName);
                    }
                }

                DataTable dt_detail = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data_Detail"].ToString());
                List<string> add_inspection = new List<string>();

                var detail = dt_detail.AsEnumerable().Select(y => new
                {
                    INSPECTION_CODE = y.Field<string>("INSPECTION_CODE"),
                    TASK_NO = y.Field<string>("TASK_NO"),
                    ITEM_TEST_VAL = y.Field<string>("ITEM_TEST_VAL"),
                    ITEM_TEST_RESULT = y.Field<string>("ITEM_TEST_RESULT"),
                    REMARK = y.Field<string>("REMARK")
                }).ToList();

                foreach (DataRow dr in dt_detail.Rows)
                {
                    if (!add_inspection.Contains(dr["INSPECTION_CODE"].ToString()))
                    {

                        add_inspection.Add(dr["INSPECTION_CODE"].ToString());
                    }
                }
                foreach (var item in add_inspection)
                {
                    dt.Columns.Add(item + "结果");//result
                    dt.Columns.Add(item + "判定");//judgement
                    dt.Columns.Add(item + "备注");//Remark
                }
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (var cl in add_inspection)
                    {
                        var cl_val = detail.Where(x => x.TASK_NO == dr["TASK_NO"].ToString() && x.INSPECTION_CODE == cl).FirstOrDefault();
                        if (cl_val != null)
                        {
                            dr[cl + "结果"] = cl_val.ITEM_TEST_VAL == null ? "" : cl_val.ITEM_TEST_VAL;
                            dr[cl + "判定"] = cl_val.ITEM_TEST_RESULT == null ? "" : cl_val.ITEM_TEST_RESULT;
                            dr[cl + "备注"] = cl_val.REMARK == null ? "" : cl_val.REMARK;
                        }
                    }
                }
                ExeclHelper.ExportToTrueExcel(dt, headdic, "Laboratory test_FinishedShoes_Test");//实验室送测-成品鞋-测试
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_gy_Click(object sender, EventArgs e)
        {
            try
            {
                ResultObject ret = getdata(9999, 1, "2", "1");
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                Dictionary<string, string> headdic = new Dictionary<string, string>();
                headdic.Add("TASK_NO", "laboratory_Number");
                headdic.Add("ART_NO", "ART");
                headdic.Add("SHOE_NO", "shoe_type_name");
                //headdic.Add("WORKMANSHIP_NAME", "工艺名称");//
                headdic.Add("WORKMANSHIP_NAME", "Process_Name");
                headdic.Add("POSITION_NAME", "Part_name_used");
                headdic.Add("MATERIAL_WAY", "MATERIAL WAY ID");
                headdic.Add("CATEGORY_NAME", "Development_series");
                headdic.Add("PRODUCT_LEVEL_VALUE", "product_level");
                headdic.Add("SEASON", "the_quarter");
                headdic.Add("PB_TYPE_LEVEL", "old_and_new_levels");//old_and_new_levels
                headdic.Add("GENDER_NAME", "age_gender");
                headdic.Add("PHASE_CREATION_NAME", "stage");
                headdic.Add("SEND_TEST_QTY", "Quantity_to_be_tested");
                headdic.Add("SIZES", "SIZE");
                headdic.Add("ORDER_PO", "Order_PO");
                headdic.Add("ORDER_PO_QTY", "Number_of_POs");
                //headdic.Add("FGT_NAME", "工艺送测类型");
                headdic.Add("FGT_NAME", "Process_test_type");
                headdic.Add("MANUFACTURER_NAME", "厂商");
                headdic.Add("TEST_REASON", "Reason_for_Testing");
                headdic.Add("STAFF_NAME", "Tester");
                headdic.Add("STAFF_DEPARTMENT", "Testing_department");

                for (int i = dt.Columns.Count - 1; i >= 0; i--)
                {
                    if (!headdic.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns.Remove(dt.Columns[i].ColumnName);
                    }
                }

                DataTable dt_detail = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data_Detail"].ToString());
                List<string> add_inspection = new List<string>();

                var detail = dt_detail.AsEnumerable().Select(y => new
                {
                    INSPECTION_CODE = y.Field<string>("INSPECTION_CODE"),
                    TASK_NO = y.Field<string>("TASK_NO"),
                    ITEM_TEST_VAL = y.Field<string>("ITEM_TEST_VAL"),
                    ITEM_TEST_RESULT = y.Field<string>("ITEM_TEST_RESULT"),
                    REMARK = y.Field<string>("REMARK")
                }).ToList();

                foreach (DataRow dr in dt_detail.Rows)
                {
                    if (!add_inspection.Contains(dr["INSPECTION_CODE"].ToString()))
                    {

                        add_inspection.Add(dr["INSPECTION_CODE"].ToString());
                    }
                }
                foreach (var item in add_inspection)
                {
                    //dt.Columns.Add(item + "结果");
                    //dt.Columns.Add(item + "判定");
                    //dt.Columns.Add(item + "备注");
                    dt.Columns.Add(item + "结果");
                    dt.Columns.Add(item + "判定");
                    dt.Columns.Add(item + "备注");
                }
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (var cl in add_inspection)
                    {
                        var cl_val = detail.Where(x => x.TASK_NO == dr["TASK_NO"].ToString() && x.INSPECTION_CODE == cl).FirstOrDefault();
                        if (cl_val != null)
                        {
                            dr[cl + "结果"] = cl_val.ITEM_TEST_VAL == null ? "" : cl_val.ITEM_TEST_VAL;
                            dr[cl + "判定"] = cl_val.ITEM_TEST_RESULT == null ? "" : cl_val.ITEM_TEST_RESULT;
                            dr[cl + "备注"] = cl_val.REMARK == null ? "" : cl_val.REMARK;
                        }
                    }
                }
                ExeclHelper.ExportToTrueExcel(dt, headdic, "Laboratory_Test_Treatment");//实验室送测-工艺
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_cl_Click(object sender, EventArgs e)
        {
            try
            {
                ResultObject ret = getdata(9999, 1, "3", "1");
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                Dictionary<string, string> headdic = new Dictionary<string, string>();
                headdic.Add("TASK_NO", "laboratory_Number");
                headdic.Add("ART_NO", "ART");
                headdic.Add("SHOE_NO", "shoe_type_name");
                headdic.Add("CATEGORY_NAME", "Development_series");

                headdic.Add("MATERIAL_NAME", "Material_type");
                headdic.Add("POSITION_NAME", "Part_name_used");//Part_name_used
                headdic.Add("PRODUCT_LEVEL_VALUE", "product_level");
                headdic.Add("SEASON", "the_quarter");
                headdic.Add("PB_TYPE_LEVEL", "old_and_new_levels");
                //headdic.Add("COLORS", "颜色");
                headdic.Add("COLORS", "color");
                headdic.Add("MAKINGS_ID", "材料ID");
                headdic.Add("TEST_ID", "TEST ID");
                headdic.Add("PHASE_CREATION_NAME", "stage");
                headdic.Add("SEND_TEST_QTY", "Quantity_to_be_tested");
                headdic.Add("SIZES", "SIZE");
                headdic.Add("ORDER_PO", "Order_PO");
                headdic.Add("ORDER_PO_QTY", "Number_of_POs");
                //headdic.Add("FGT_NAME", "材料送测类型");//Material_delivery_type
                headdic.Add("FGT_NAME", "Material_delivery_type");//
                headdic.Add("MANUFACTURER_NAME", "Manufacturers");
                headdic.Add("TEST_REASON", "Reason_for_Testing");
                headdic.Add("STAFF_NAME", "Tester");
                headdic.Add("STAFF_DEPARTMENT", "Testing_department");

                for (int i = dt.Columns.Count - 1; i >= 0; i--)
                {
                    if (!headdic.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns.Remove(dt.Columns[i].ColumnName);
                    }
                }

                DataTable dt_detail = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data_Detail"].ToString());
                DataTable dt_remarks = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["dt_remarks"].ToString());
                List<string> add_inspection = new List<string>();

                var detail = dt_detail.AsEnumerable().Select(y => new
                {
                    INSPECTION_CODE = y.Field<string>("INSPECTION_CODE"),
                    TASK_NO = y.Field<string>("TASK_NO"),
                    ITEM_TEST_VAL = y.Field<string>("ITEM_TEST_VAL"),
                    ITEM_TEST_RESULT = y.Field<string>("ITEM_TEST_RESULT"),
                    REMARK = y.Field<string>("REMARK")
                }).ToList();

                var remarks = dt_remarks.AsEnumerable().Select(y => new
                {
                    TASK_NO = y.Field<string>("TASK_NO"),
                    INSPECTION_CODE = y.Field<string>("INSPECTION_CODE"),
                    REMARKS = y.Field<string>("REMARKS")
                }).ToList();

                foreach (DataRow dr in dt_detail.Rows)
                {
                    if (!add_inspection.Contains(dr["INSPECTION_CODE"].ToString()))
                    {

                        add_inspection.Add(dr["INSPECTION_CODE"].ToString());
                    }
                }
                foreach (var item in add_inspection)
                {
                    dt.Columns.Add(item + "结果");
                    dt.Columns.Add(item + "判定");
                    dt.Columns.Add(item + "备注");
                }
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (var cl in add_inspection)
                    {
                        var cl_val = detail.Where(x => x.TASK_NO == dr["TASK_NO"].ToString() && x.INSPECTION_CODE == cl).FirstOrDefault();
                        var remarklist = remarks.Where(x => x.TASK_NO == dr["TASK_NO"].ToString() && x.INSPECTION_CODE == cl).Select(y => y.REMARKS).ToList();
                        if (cl_val != null)
                        {
                            dr[cl + "结果"] = cl_val.ITEM_TEST_VAL == null ? "" : cl_val.ITEM_TEST_VAL;
                            dr[cl + "判定"] = cl_val.ITEM_TEST_RESULT == null ? "" : cl_val.ITEM_TEST_RESULT;
                            dr[cl + "备注"] = cl_val.REMARK == null ? "" : cl_val.REMARK;
                            //dr[cl + "备注"] = string.Join("|", remarklist);
                        }
                    }
                }
                ExeclHelper.ExportToTrueExcel(dt, headdic, "Laboratory_Test_Material_Test");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_lcll_Click(object sender, EventArgs e)
        {
            try
            {
                ResultObject ret = getdata(9999, 1, "4", "1");
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                Dictionary<string, string> headdic = new Dictionary<string, string>();
                headdic.Add("TASK_NO", "laboratory_Number");
                headdic.Add("ART_NO", "ART");
                headdic.Add("SHOE_NO", "shoe_type_name");
                headdic.Add("CATEGORY_NAME", "Development_series");
                headdic.Add("LINE_NAME", "Production_line");
                headdic.Add("CMBBH", "Size_label_number");
                headdic.Add("PRODUCT_LEVEL_VALUE", "product_level");
                headdic.Add("SEASON", "the_quarter");
                headdic.Add("PHASE_CREATION_NAME", "stage");
                headdic.Add("SEND_TEST_QTY", "Quantity_to_be_tested");
                headdic.Add("SIZES", "SIZE");
                headdic.Add("ORDER_PO", "Order_PO");
                headdic.Add("ORDER_PO_QTY", "Number_of_POs");
                headdic.Add("FGT_NAME", "FGT_test_Type");
                headdic.Add("GLUE", "Glue/Treatment_Information");
                headdic.Add("TEST_REASON", "Reason_for_Testing");
                headdic.Add("STAFF_NAME", "Tester");
                headdic.Add("STAFF_DEPARTMENT", "Testing_department");
                headdic.Add("TEST_TIME", "shoe_testing_time");
                headdic.Add("SY_PART_NAME", "Sample_site_name");

                for (int i = dt.Columns.Count - 1; i >= 0; i--)
                {
                    if (!headdic.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        dt.Columns.Remove(dt.Columns[i].ColumnName);
                    }
                }

                DataTable dt_detail = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data_Detail"].ToString());
                List<string> add_inspection = new List<string>();

                var detail = dt_detail.AsEnumerable().Select(y => new
                {
                    INSPECTION_CODE = y.Field<string>("INSPECTION_CODE"),
                    TASK_NO = y.Field<string>("TASK_NO"),
                    ITEM_TEST_VAL = y.Field<string>("ITEM_TEST_VAL"),
                    ITEM_TEST_RESULT = y.Field<string>("ITEM_TEST_RESULT"),
                    REMARK = y.Field<string>("REMARK")
                }).ToList();

                foreach (DataRow dr in dt_detail.Rows)
                {
                    if (!add_inspection.Contains(dr["INSPECTION_CODE"].ToString()))
                    {

                        add_inspection.Add(dr["INSPECTION_CODE"].ToString());
                    }
                }
                foreach (var item in add_inspection)
                {
                    dt.Columns.Add(item + "结果");
                    dt.Columns.Add(item + "判定");
                    dt.Columns.Add(item + "备注");
                }
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (var cl in add_inspection)
                    {
                        var cl_val = detail.Where(x => x.TASK_NO == dr["TASK_NO"].ToString() && x.INSPECTION_CODE == cl).FirstOrDefault();
                        if (cl_val != null)
                        {
                            dr[cl + "结果"] = cl_val.ITEM_TEST_VAL == null ? "" : cl_val.ITEM_TEST_VAL;
                            dr[cl + "判定"] = cl_val.ITEM_TEST_RESULT == null ? "" : cl_val.ITEM_TEST_RESULT;
                            dr[cl + "备注"] = cl_val.REMARK == null ? "" : cl_val.REMARK;
                        }
                    }
                }
                ExeclHelper.ExportToTrueExcel(dt, headdic, "Laboratory_Test_Bonding_Test");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
    }
}
