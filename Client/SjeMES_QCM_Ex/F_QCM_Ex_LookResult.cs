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
    public partial class F_QCM_Ex_LookResult : MaterialForm
    {
        public string _task_no = "";
        public DataTable D_JUDGMENT_CRITERIA = new DataTable();
        private readonly MaterialSkinManager materialSkinManager;
        DataTable ITEM_LIST;
        public F_QCM_Ex_LookResult(string task_no)
        {
            InitializeComponent();
            _task_no = task_no;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public F_QCM_Ex_LookResult(string task_no, SJeMES_Framework.Class.ClientClass client)
        {
            Program.Client = client;
            InitializeComponent();
            _task_no = task_no;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
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
            txt_task_no.Text = info["TASK_NO"].ToString();

            string test_type = "";

            if (info["TEST_TYPE"].ToString() == "0")
            {
                test_type = "Finished_Shoes_Test";
            }
            if (info["TEST_TYPE"].ToString() == "1")
            {
                test_type = "Component_Test";
            }
            if (info["TEST_TYPE"].ToString() == "2")
            {
                test_type = "Treatment";
            }
            if (info["TEST_TYPE"].ToString() == "3")
            {
                test_type = "Material_Test";
            }
            if (info["TEST_TYPE"].ToString() == "4")
            {
                test_type = "Bonding_Test";
            }
            txt_test_type.Text = test_type;
            txt_art.Text = info["ART_NO"].ToString();
            txt_po_order.Text = info["ORDER_PO"].ToString();
            txt_material_id.Text = info["MATERIAL_WAY"].ToString();
           // txt_line.Text = info["LINE_NAME"].ToString();
            txt_shose.Text = info["SHOE_NO"].ToString();
            txt_po_qty.Text = info["ORDER_PO_QTY"].ToString();
            //txt_bjmc.Text = info["PARTS_NAME"].ToString();
            txt_cs.Text = info["MANUFACTURER_NAME"].ToString();
            txt_category.Text = info["CATEGORY_NAME"].ToString();
            txt_jianduan.Text = info["PHASE_CREATION_NAME"].ToString();
            txt_bwmc.Text = info["POSITION_NAME"].ToString();
            //txt_fgt.Text = info["FGT_NAME"].ToString();
            txt_cpjb.Text = info["PRODUCT_LEVEL_VALUE"].ToString();
            txt_scsl.Text = info["SEND_TEST_QTY"].ToString();
           // txt_gymc.Text = info["MANUFACTURER_NAME"].ToString();
            txt_clid.Text = info["MAKINGS_ID"].ToString();
            txt_jd.Text = info["SEASON"].ToString();
            //txt_size.Text = info["SIZES"].ToString();
            txt_clzl.Text = info["MAKINGS_TYPE_NAME"].ToString();
            txt_wlmc.Text = info["MATERIAL_NAME"].ToString();
            //txt_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();
            //txt_xb.Text = info["GENDER"].ToString();
            txt_ys.Text = info["COLORS"].ToString();
            //txt_reason.Text = info["TEST_REASON"].ToString();
           // txt_jsxx.Text = info["GLUE"].ToString();
            //txt_staff_no.Text = info["STAFF_NO"].ToString();
            txt_staff_name.Text = info["STAFF_NAME"].ToString();
            txt_department.Text = info["STAFF_DEPARTMENT"].ToString();
            tb_test_id.Text = info["TEST_ID"].ToString();

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
                HeadDic.Add("ART：", txt_art.Text);
                HeadDic.Add("PO数量：", txt_po_qty.Text);
                HeadDic.Add("Material NO：", txt_material_no.Text);
                HeadDic.Add("鞋型名称：", txt_shose.Text);
                HeadDic.Add("物料名称：", txt_wlmc.Text);
                HeadDic.Add("送测人姓名：", txt_staff_name.Text);
                HeadDic.Add("category：", txt_category.Text);
                HeadDic.Add("材料ID：", txt_clid.Text);
                HeadDic.Add("部门：", txt_department.Text);
                HeadDic.Add("产品级别：", txt_cpjb.Text);
                HeadDic.Add("使用部位：", txt_bwmc.Text);
                HeadDic.Add("检验项目：", txt_test_type.Text);
                HeadDic.Add("订单PO：", txt_po_order.Text);
                HeadDic.Add("厂商：", txt_cs.Text);
                HeadDic.Add("任务编号：", txt_task_no.Text);
                HeadDic.Add("季度：", txt_jd.Text);
                HeadDic.Add("阶段：", txt_jianduan.Text);
                HeadDic.Add("TEST ID：", tb_test_id.Text);
                HeadDic.Add("送测数量：", txt_scsl.Text);
                HeadDic.Add("材料种类：", txt_clzl.Text);
                HeadDic.Add("颜色：", txt_ys.Text);
                HeadDic.Add("Material ID：", txt_material_id.Text);
                HeadDic.Add("检验结果：", lab_result.Text);

                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("xh", "Serial_No");
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
    }
}
