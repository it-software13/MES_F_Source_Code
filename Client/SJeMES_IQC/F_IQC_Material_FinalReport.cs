using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using SJeMES_IQC.F_QCM_Ex_Item_Set_UC;
using SjeMES_QCM_Ex;
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
    public partial class F_IQC_Material_FinalReport : MaterialForm
    {
        

        public string _task_no = "";
        public string _task_no_type = "";
        public DataTable D_JUDGMENT_CRITERIA = new DataTable();
        DataTable ITEM_LIST;
        UserControl currItem;
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics;
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
        public F_IQC_Material_FinalReport(Dictionary<string, object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_IQC_Material_FinalReport(string ITEM_NO)
        {
            InitializeComponent();

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public F_IQC_Material_FinalReport(Dictionary<string, object> dic, SJeMES_Framework.Class.ClientClass client)
        {
            InitializeComponent();
            dics = dic;
            Program.Client = client;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_IQC_Material_FinalReport_Load(object sender, EventArgs e)
        {
            panel1.Width = 1400;

            if (dics != null && dics.Count > 0)
            {
                VisualInspectionReport();
                LabTestReport();
            }
        }

        public void VisualInspectionReport()
        {
            lab_sldh.Text = dics["CHK_NO"].ToString();//收料单号
            lab_sccs.Text = dics["SUPPLIERS_NAME"].ToString();//生产厂商
            lab_clmc.Text = dics["ITEM_NAME"].ToString();//材料名称
            LTooltip(lab_clmc, 40, lab_clmc.Text);
            lab_jcrq.Text = dics["RCPT_DATE"].ToString();//进仓日期
            lab_xx.Text = dics["SHOE_NO"].ToString();//鞋型
            lab_bw.Text = dics["PART"].ToString();//部位
            lab_sfpl.Text = dics["ORDER_NO"].ToString();//采购单号
            lab_lh.Text = dics["ITEM_NO"].ToString();//料号
            lab_jcqty.Text = dics["RCPT_QTY"].ToString();//进仓数量
            lab_art.Text = dics["PROD_NO"].ToString();//ART
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CHK_NO", lab_sldh.Text);//收料单号
                p.Add("ITEM_NO", dics["ITEM_NO"].ToString());//物料代号
                p.Add("CHK_SEQ", dics["CHK_SEQ"].ToString());//物料序号
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJeMES_IQC",//类库名
                                            "SJeMES_IQC.VMaterialinventory",//类名
                                            "CheckResultJYView",//方法名
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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["test_item_name"].Value = dr["test_item_name"].ToString();//检验项名称
                        dgvr.Cells["test_standard"].Value = dr["test_standard"].ToString();//检验标准

                        if (dr["determine"].ToString() == "0")
                        {
                            dgvr.Cells["determine"].Value = "PASS";
                        }
                        else
                        {
                            dgvr.Cells["determine"].Value = "FAIL";
                            dgvr.Cells["determine"].Style.ForeColor = Color.Red;
                        }
                        dgvr.Cells["sample_qty"].Value = dr["sample_qty"].ToString();//抽样数量
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);

                }
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public void LabTestReport()
        {
            string task_no = GetTaskNo();
            if (!string.IsNullOrWhiteSpace(task_no))
            {
                GetALLDDLData();
                list_fgt_data = GetFGTInfo();
                list_size_data = GetSizeInfo();
                getdate(task_no);
            }
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
        public string GetTaskNo()
        {
            string task_no = string.Empty;

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("item_no", dics["ITEM_NO"].ToString());//料号
            p.Add("rcpt_date", dics["RCPT_DATE"].ToString());//收料日期
            p.Add("chk_no", dics["CHK_NO"].ToString());//收料日期
            p.Add("task_no", dics["TASK_NO"].ToString());//pemika-2025/12/05
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJeMES_IQC",//类库名
                                        "SJeMES_IQC.VMaterialinventory",//类名
                                        "CheckResultMainDmp_Chk_nolist",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
               // throw new Exception(ret.ErrMsg);
                MessageBox.Show(ret.ErrMsg);
                this.Close();
            }
            else
            {
                Dictionary<string, object> dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                task_no = dic2["task_no"].ToString();
            }
          
            return task_no;
        }


        private static void LTooltip(System.Windows.Forms.Label label, int length, string value)
        {
            label.Text = value;
            if (value.Length > length)
            {
                label.Text = label.Text.Substring(0, length) + "...";
            }
            var tip = new ToolTip();
            tip.IsBalloon = false;
            tip.ShowAlways = true;
            tip.SetToolTip(label, value);
        }

        public void getdate(string task_no)
        {
            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("task_no", task_no);
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
            if (info["TEST_RESULT"].ToString() == "PASS")
            {
                lab_result.ForeColor = Color.Green;
            }
            else
            {
                lab_result.ForeColor = Color.Red;
            }
        }

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
        }

        private void Btn_print_Click(object sender, EventArgs e)
        {

        }
    }
}
