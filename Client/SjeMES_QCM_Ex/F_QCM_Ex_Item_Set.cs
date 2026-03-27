using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class F_QCM_Ex_Item_Set : MaterialForm
    {
        public string _task_no = "";
        private readonly MaterialSkinManager materialSkinManager;
        public DataTable G_formula_type = new DataTable();
        public DataTable D_formula_type = new DataTable();
        public DataTable D_JUDGMENT_CRITERIA = new DataTable();
        public DataTable D_JUDGE_TYPE = new DataTable();
        public F_QCM_Ex_Item_Set(string task_no)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            _task_no = task_no;
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
            txt_task_no.Text = info["TASK_NO"].ToString();
            
            if (info["TEST_TYPE"].ToString() == "0")
            {
                test_type = "FinishedShoes-Testing";////成品鞋
            }
            if (info["TEST_TYPE"].ToString() == "1")
            {
                test_type = "part";////部件
            }
            if (info["TEST_TYPE"].ToString() == "2")
            {
                test_type = "craft";////工艺
            }
            if (info["TEST_TYPE"].ToString() == "3")
            {
                test_type = "Material";////材料
            }
            if (info["TEST_TYPE"].ToString() == "4")
            {
                test_type = "ProductionRally"; ////量产拉力
            }
            test_type_no = info["TEST_TYPE"].ToString();
            txt_test_type.Text = test_type;

            txt_art.Text = info["ART_NO"].ToString();
            txt_po_order.Text = info["ORDER_PO"].ToString();
            txt_material_way.Text = info["MATERIAL_WAY"].ToString();
            txt_line.Text = info["LINE_NAME"].ToString();
            txt_shose.Text = info["SHOE_NO"].ToString();
            txt_po_qty.Text = info["ORDER_PO_QTY"].ToString();
            txt_bjmc.Text = info["PARTS_NAME"].ToString();
            txt_cs.Text = info["MANUFACTURER_NAME"].ToString();
            txt_category.Text = info["CATEGORY_NAME"].ToString();
            txt_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();
            txt_bwmc.Text = info["POSITION_NAME"].ToString();
            txt_fgt.Text = info["FGT_NAME"].ToString();
            txt_cpjb.Text = info["PRODUCT_LEVEL_VALUE"].ToString();
            txt_scsl.Text = info["SEND_TEST_QTY"].ToString();
            txt_gymc.Text = info["WORKMANSHIP_NAME"].ToString();
            txt_clid.Text = info["MAKINGS_ID"].ToString();
            txt_jd.Text = info["SEASON"].ToString();
            txt_size.Text = info["SIZES"].ToString();
            txt_clzl.Text = info["MAKINGS_TYPE_NAME"].ToString();
            txt_wlmc.Text = info["MATERIAL_NAME"].ToString();
            txt_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();
            txt_xb.Text = info["GENDER"].ToString();
            txt_ys.Text = info["COLORS"].ToString();
            txt_reason.Text = info["TEST_REASON"].ToString();
            txt_jsxx.Text = info["GLUE"].ToString();
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
                    //SOURCES = "DQA测试任务";
                    SOURCES = "DQA测试任务";
                }
                if (item["SOURCES"].ToString() == "1")
                {
                    //SOURCES = "常规";
                    SOURCES = "conventional";
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
                dgv.Rows[i].Cells["standard_value"].ReadOnly = true;
                dgv.Rows[i].Cells["standard_value"].Style = cl_readonly;
                dgv.Rows[i].Cells["unit"].Value = item["UNIT"].ToString();
                dgv.Rows[i].Cells["unit"].ReadOnly = true;
                dgv.Rows[i].Cells["unit"].Style = cl_readonly;
                dgv.Rows[i].Cells["sample_qty"].Value = item["SAMPLE_QTY"].ToString();
                DataGridViewComboBoxColumn cmb3 = (DataGridViewComboBoxColumn)dgv.Rows[i].Cells["judgment_criteria"].OwningColumn;
                cmb3.DataSource = D_JUDGMENT_CRITERIA;
                cmb3.DisplayMember = "NAME";
                cmb3.ValueMember = "CODE";
                if (D_JUDGMENT_CRITERIA.Select($"CODE='{item["JUDGMENT_CRITERIA"].ToString()}'").Length > 0)
                {
                    dgv.Rows[i].Cells["judgment_criteria"].Value = item["JUDGMENT_CRITERIA"].ToString();
                }
                dgv.Rows[i].Cells["judgment_criteria"].ReadOnly = true;

                DataGridViewComboBoxColumn cmb4 = (DataGridViewComboBoxColumn)dgv.Rows[i].Cells["judge_type"].OwningColumn;
                cmb4.DataSource = D_JUDGE_TYPE;
                cmb4.DisplayMember = "NAME";
                cmb4.ValueMember = "CODE";
                if (D_JUDGE_TYPE.Select($"CODE='{item["judge_type"].ToString()}'").Length > 0)
                {
                    dgv.Rows[i].Cells["judge_type"].Value = item["judge_type"].ToString();
                }
                dgv.Rows[i].Cells["judge_type"].ReadOnly = true;

                DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv.Rows[i].Cells["tygs"].OwningColumn;
                cmb.DataSource = G_formula_type;
                cmb.DisplayMember = "NAME";
                cmb.ValueMember = "CODE";
                if (G_formula_type.Select($"CODE='{item["G_FORMULA_CODE"].ToString()}'").Length > 0)
                {
                    dgv.Rows[i].Cells["tygs"].Value = item["G_FORMULA_CODE"].ToString();
                }

                DataGridViewComboBoxColumn cmb1 = (DataGridViewComboBoxColumn)dgv.Rows[i].Cells["zdygs"].OwningColumn;
                cmb1.DataSource = D_formula_type;
                cmb1.DisplayMember = "NAME";
                cmb1.ValueMember = "CODE";
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
            dgv.Rows[newindex].Cells["type"].Value = "conventional";//conventional//常规
            dgv.Rows[newindex].Cells["type"].Style = cl_readonly;
            dgv.Rows[newindex].Cells["d_id"].Value = "";
            dgv.Rows[newindex].Cells["inspection_type_name"].Value = "customize";//
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


        public DataGridViewCellStyle cl_readonly = new DataGridViewCellStyle();
        

        private void button3_Click(object sender, EventArgs e)
        {
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
                    if(dic["judge_type"].ToString()=="1")
                    {
                        decimal standard_value = 0;
                        decimal.TryParse(dic["standard_value"].ToString(), out standard_value);
                        if(standard_value<=0)
                        {
                            MessageBox.Show("Please enter the measurement standard in the correct number format");
                            item.Cells["standard_value"].Selected = true;
                            return;
                        }
                    }
                    else
                    {
                        if(!dic["standard_value"].ToString().Contains('~'))
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
                            decimal.TryParse(list_value[0], out standard_value1);
                            decimal.TryParse(list_value[1], out standard_value2);
                            if(standard_value1<=0|| standard_value2<=0)
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

            if (list.Count > 0)
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", _task_no);
                p.Add("list", list);
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
                if (dgv.Columns[e.ColumnIndex].Name=="action" && dgv.Rows[e.RowIndex].Cells["d_id"].Value.ToString() == "")
                {
                    dgv.Rows.Remove(dgv.Rows[e.RowIndex]);
                }

                //if ((e.ColumnIndex == 4 || e.ColumnIndex == 5) && dgv.Rows[e.RowIndex].Cells["d_id"].Value.ToString() == "")
                //{
                //    F_QCM_Select_CheckItem frm = new F_QCM_Select_CheckItem(dgv.Rows[e.RowIndex].Cells["inspection_type"].Value.ToString());
                //    frm.StartPosition = FormStartPosition.CenterScreen;
                //    frm.ShowDialog();

                //    if (frm.selectdic.Count > 0)
                //    {
                //        dgv.Rows[e.RowIndex].Cells[4].Value = frm.selectdic["code"].ToString();
                //        dgv.Rows[e.RowIndex].Cells[5].Value = frm.selectdic["name"].ToString();
                //        dgv.Rows[e.RowIndex].Cells[6].Value = frm.selectdic["pdbz"].ToString();
                //        dgv.Rows[e.RowIndex].Cells[7].Value = frm.selectdic["jybz"].ToString();
                //    }
                //}
            }
        }
    }
}
