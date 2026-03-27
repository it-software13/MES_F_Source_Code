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
    public partial class F_QCM_Ex_Shose : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

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

        public F_QCM_Ex_Shose()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 获取art信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="qrcode"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetArtInfo(string type, string qrcode)
        {

            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("qrcode", qrcode);
            data.Add("type", type);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetArtInfo",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());


        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F_QCM_Ex_shose_Load(object sender, EventArgs e)
        {
            GetALLDDLData();
            list_fgt_data = GetFGTInfo();
            list_size_data = GetSizeInfo();
        }

        /// <summary>
        /// 获取所有下拉数据源
        /// </summary>
        /// <returns></returns>
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
        /// 获取员工信息
        /// </summary>
        /// <param name="staff_code"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetStaffInfo(string staff_code)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("qrcode", staff_code);
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetStaffInfo",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());

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
            if (changed)
            {
                switch (type)
                {
                    case "cpx":
                        cmb.SelectionChangeCommitted -= Get_cpx_checkItem;
                        cmb.SelectionChangeCommitted += Get_cpx_checkItem;
                        break;
                    case "bj":
                        cmb.SelectionChangeCommitted -= Get_bj_checkItem;
                        cmb.SelectionChangeCommitted += Get_bj_checkItem;
                        break;
                    case "gy":
                        cmb.SelectionChangeCommitted -= Get_gy_checkItem;
                        cmb.SelectionChangeCommitted += Get_gy_checkItem;
                        break;
                    case "cl":
                        cmb.SelectionChangeCommitted -= Get_cl_checkItem;
                        cmb.SelectionChangeCommitted += Get_cl_checkItem;
                        break;
                }

            }

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

        ComboBox CB_G_formula_type;
        ComboBox CB_D_formula_type;
        TextBox TXT_Unit;
        TextBox TXT_Sample_qt;
        TextBox TXT_Remarks;

        #region 成品鞋
        /// <summary>
        /// 绑定员工
        /// </summary>
        /// <param name="result"></param>
        private void Bind_cpx_staff(Dictionary<string, object> result)
        {
            txt_cpx_staff_no.Text = result["STAFF_NO"].ToString();
            txt_cpx_staff_name.Text = result["STAFF_NAME"].ToString();
            txt_cpx_staff_department.Text = result["DEPARTMENT_NAME"].ToString();
            lab_cpx_staff_department_code.Text = result["DEPARTMENT_CODE"].ToString();
        }

        /// <summary>
        /// 绑定成品鞋信息
        /// </summary>
        /// <param name="result"></param>
        public void Bind_cpx_info(Dictionary<string, object> result)
        {
            Dictionary<string, object> info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result["info"].ToString());
            cpx_po_dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["po_info"].ToString());
            DataTable check_item = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["check_item"].ToString());

            //txt_cpx_shose.Text = info["SHOE_NO"].ToString();
            txt_cpx_art.Text = info["PROD_NO"].ToString();
            //txt_cpx_model_no.Text = info["MODEL_NO"].ToString();
            txt_cpx_jidu.Text = info["DEVELOP_SEASON"].ToString();
            txt_cpx_shose.Text = info["SHOE_NAME"].ToString();
            txt_cpx_model_no.Text = info["SHOE_NO"].ToString();



            BindDDL(cmb_cpx_jd, list_jd_data, "cpx", true);
            BindDDL(cmb_cpx_fgt, list_fgt_data, "cpx", true, true);
            //BindDDL(cmb_cpx_category, list_category_data, "cpx", true, true);
            tb_cpx_category.Text = info["CATEGROY_ID"].ToString();
            //BindDDL(cmb_cpx_xjjb, list_xjjb_data, "cpx", true, true);
            tb_cpx_xjjb.Text = info["DEVELOP_TYPE"].ToString();
            BindDDL(cmb_cpx_xb, list_agesex_data, "cpx", true, true);
            BindDDL(cmb_cpx_cpzl, list_cptype_data, "cpx", true, true);
            //BindDDL(cmb_cpx_cpjb, list_productlevel_data, "cpx", true, true);
            tb_cpx_cpjb.Text = info["PRODUCT_LEVEL"].ToString();
            Bind_size(cmb_cpx_size, list_size_data, true);

            dgv_cpx.Rows.Clear();

            foreach (DataRow dr in check_item.Rows)
            {
                int i = dgv_cpx.Rows.Add();
                dgv_cpx.Rows[i].ReadOnly = true;
                dgv_cpx.Rows[i].Cells["cpx_xh"].Value = (i + 1).ToString();
                dgv_cpx.Rows[i].Cells["cpx_check"].Value = true;
                dgv_cpx.Rows[i].Cells["cpx_type"].Value = dr["type"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_choice_name"].Value = dr["choice_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_judge_type"].Value = dr["judge_type"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_standard_value"].Value = dr["standard_value"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_unit"].Value = dr["unit"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_sample_qty"].Value = "";
                dgv_cpx.Rows[i].Cells["cpx_remarks"].Value = dr["remarks"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_choice_no"].Value = dr["choice_no"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }


        }

        #region 表格编辑代码快
        private void dgv_cpx_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CB_G_formula_type != null)
            {
                CB_G_formula_type.Visible = false;
            }
            if (CB_D_formula_type != null)
            {
                CB_D_formula_type.Visible = false;
            }
            if (TXT_Unit != null)
            {
                TXT_Unit.Visible = false;
                TXT_Unit.Dispose();
            }
            if (TXT_Sample_qt != null)
            {
                TXT_Sample_qt.Visible = false;
                TXT_Sample_qt.Dispose();
            }
            if (TXT_Remarks != null)
            {
                TXT_Remarks.Visible = false;
                TXT_Remarks.Dispose();
            }
            if (e.RowIndex > -1 && !ckb_cpx_sfcc.Checked)
            {
                if (dgv_cpx.Columns[e.ColumnIndex].Name == "cpx_tygs")
                {

                    CB_G_formula_type = new ComboBox();
                    CB_G_formula_type.Enabled = true;
                    CB_G_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_G_formula_type.DataSource = list_tygs_data;
                    CB_G_formula_type.DisplayMember = "NAME";
                    CB_G_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_cpx.GetCellDisplayRectangle(dgv_cpx.CurrentCell.ColumnIndex, dgv_cpx.CurrentCell.RowIndex, false);
                    CB_G_formula_type.Left = rect.Left;
                    CB_G_formula_type.Top = rect.Top;
                    CB_G_formula_type.Width = rect.Width;
                    CB_G_formula_type.Height = rect.Height;
                    CB_G_formula_type.Visible = true;
                    dgv_cpx.Controls.Add(CB_G_formula_type);
                    if (dgv_cpx.Rows[e.RowIndex].Cells["cpx_tygs_code"].Value != null && !string.IsNullOrEmpty(dgv_cpx.Rows[e.RowIndex].Cells["cpx_tygs_code"].Value.ToString()))
                    {
                        CB_G_formula_type.SelectedValue = dgv_cpx.Rows[e.RowIndex].Cells["cpx_tygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_G_formula_type.SelectedIndex = 0;
                    }
                    CB_G_formula_type.Focus();
                    CB_G_formula_type.SelectedIndexChanged += CB_G_formula_type_SelectedIndexChanged;
                }
                if (dgv_cpx.Columns[e.ColumnIndex].Name == "cpx_zdygs")
                {

                    CB_D_formula_type = new ComboBox();
                    CB_D_formula_type.Enabled = true;
                    CB_D_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_D_formula_type.DataSource = list_zdygs_data;
                    CB_D_formula_type.DisplayMember = "NAME";
                    CB_D_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_cpx.GetCellDisplayRectangle(dgv_cpx.CurrentCell.ColumnIndex, dgv_cpx.CurrentCell.RowIndex, false);
                    CB_D_formula_type.Left = rect.Left;
                    CB_D_formula_type.Top = rect.Top;
                    CB_D_formula_type.Width = rect.Width;
                    CB_D_formula_type.Height = rect.Height;
                    CB_D_formula_type.Visible = true;
                    dgv_cpx.Controls.Add(CB_D_formula_type);
                    if (dgv_cpx.Rows[e.RowIndex].Cells["cpx_zdygs_code"].Value != null && !string.IsNullOrEmpty(dgv_cpx.Rows[e.RowIndex].Cells["cpx_zdygs_code"].Value.ToString()))
                    {
                        CB_D_formula_type.SelectedValue = dgv_cpx.Rows[e.RowIndex].Cells["cpx_zdygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_D_formula_type.SelectedIndex = 0;
                    }
                    CB_D_formula_type.Focus();
                    CB_D_formula_type.SelectedIndexChanged += CB_D_formula_type_SelectedIndexChanged;
                }
                if (dgv_cpx.Columns[e.ColumnIndex].Name == "cpx_unit")
                {

                    TXT_Unit = new TextBox();
                    TXT_Unit.Enabled = true;

                    Rectangle rect = dgv_cpx.GetCellDisplayRectangle(dgv_cpx.CurrentCell.ColumnIndex, dgv_cpx.CurrentCell.RowIndex, false);
                    TXT_Unit.Left = rect.Left;
                    TXT_Unit.Top = rect.Top;
                    TXT_Unit.Width = rect.Width;
                    TXT_Unit.Height = rect.Height;
                    TXT_Unit.Visible = true;
                    dgv_cpx.Controls.Add(TXT_Unit);
                    if (dgv_cpx.Rows[e.RowIndex].Cells["cpx_unit"].Value != null && !string.IsNullOrEmpty(dgv_cpx.Rows[e.RowIndex].Cells["cpx_unit"].Value.ToString()))
                    {
                        TXT_Unit.Text = dgv_cpx.Rows[e.RowIndex].Cells["cpx_unit"].Value.ToString();
                    }
                    TXT_Unit.Focus();
                    TXT_Unit.SelectionStart = TXT_Unit.Text.Length;
                    TXT_Unit.TextChanged += TXT_Unit_TextChanged;
                }
                if (dgv_cpx.Columns[e.ColumnIndex].Name == "cpx_sample_qty")
                {

                    TXT_Sample_qt = new TextBox();
                    TXT_Sample_qt.Enabled = true;

                    Rectangle rect = dgv_cpx.GetCellDisplayRectangle(dgv_cpx.CurrentCell.ColumnIndex, dgv_cpx.CurrentCell.RowIndex, false);
                    TXT_Sample_qt.Left = rect.Left;
                    TXT_Sample_qt.Top = rect.Top;
                    TXT_Sample_qt.Width = rect.Width;
                    TXT_Sample_qt.Height = rect.Height;
                    TXT_Sample_qt.Visible = true;
                    dgv_cpx.Controls.Add(TXT_Sample_qt);
                    if (dgv_cpx.Rows[e.RowIndex].Cells["cpx_sample_qty"].Value != null && !string.IsNullOrEmpty(dgv_cpx.Rows[e.RowIndex].Cells["cpx_sample_qty"].Value.ToString()))
                    {
                        TXT_Sample_qt.Text = dgv_cpx.Rows[e.RowIndex].Cells["cpx_sample_qty"].Value.ToString();
                    }
                    TXT_Sample_qt.Focus();
                    TXT_Sample_qt.SelectionStart = TXT_Sample_qt.Text.Length;
                    TXT_Sample_qt.TextChanged += TXT_Sample_qt_TextChanged;
                }
                if (dgv_cpx.Columns[e.ColumnIndex].Name == "cpx_remarks")
                {

                    TXT_Remarks = new TextBox();
                    TXT_Remarks.Enabled = true;

                    Rectangle rect = dgv_cpx.GetCellDisplayRectangle(dgv_cpx.CurrentCell.ColumnIndex, dgv_cpx.CurrentCell.RowIndex, false);
                    TXT_Remarks.Left = rect.Left;
                    TXT_Remarks.Top = rect.Top;
                    TXT_Remarks.Width = rect.Width;
                    TXT_Remarks.Height = rect.Height;
                    TXT_Remarks.Visible = true;
                    dgv_cpx.Controls.Add(TXT_Remarks);
                    if (dgv_cpx.Rows[e.RowIndex].Cells["cpx_remarks"].Value != null && !string.IsNullOrEmpty(dgv_cpx.Rows[e.RowIndex].Cells["cpx_remarks"].Value.ToString()))
                    {
                        TXT_Remarks.Text = dgv_cpx.Rows[e.RowIndex].Cells["cpx_remarks"].Value.ToString();
                    }
                    TXT_Remarks.Focus();
                    TXT_Remarks.SelectionStart = TXT_Remarks.Text.Length;
                    TXT_Remarks.TextChanged += TXT_Remarks_TextChanged;
                }
            }
        }
        private void CB_G_formula_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv_cpx.CurrentCell.Value = CB_G_formula_type.Text;
            dgv_cpx.Rows[dgv_cpx.CurrentCell.RowIndex].Cells["cpx_tygs_code"].Value = CB_G_formula_type.SelectedValue;
            CB_G_formula_type.Visible = false;
            CB_G_formula_type.Dispose();
        }
        private void CB_D_formula_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv_cpx.CurrentCell.Value = CB_D_formula_type.Text;
            dgv_cpx.Rows[dgv_cpx.CurrentCell.RowIndex].Cells["cpx_zdygs_code"].Value = CB_D_formula_type.SelectedValue;
            CB_D_formula_type.Visible = false;
            CB_D_formula_type.Dispose();
        }
        private void TXT_Unit_TextChanged(object sender, EventArgs e)
        {
            dgv_cpx.CurrentCell.Value = TXT_Unit.Text;
        }
        private void TXT_Sample_qt_TextChanged(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(TXT_Sample_qt.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(TXT_Sample_qt.Text))
            {
                MessageBox.Show("Please enter a positive integer");
                TXT_Sample_qt.Text = "";
                return;
            }
            dgv_cpx.CurrentCell.Value = TXT_Sample_qt.Text;
        }
        private void TXT_Remarks_TextChanged(object sender, EventArgs e)
        {
            dgv_cpx.CurrentCell.Value = TXT_Remarks.Text;
        }
        #endregion

        /// <summary>
        /// 扫描事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_cpx_qrcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetArtInfo("cpx", txt_cpx_qrcode.Text.Trim());
                if (result != null)
                {
                    Bind_cpx_info(result);
                }
            }
        }

        /// <summary>
        /// 双击弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_cpx_qrcode_DoubleClick(object sender, EventArgs e)
        {
            F_QCM_Ex_SelectART frm = new F_QCM_Ex_SelectART();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(frm.select_art))
            {
                txt_cpx_qrcode.Text = frm.select_art;
                var result = GetArtInfo("cpx", frm.select_art);
                if (result != null)
                {
                    Bind_cpx_info(result);
                }
            }
        }

        public DataTable cpx_po_dt = new DataTable();

        /// <summary>
        /// 扫描员工编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_cpx_staff_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetStaffInfo(txt_cpx_staff_code.Text.Trim());
                if (result != null)
                {
                    Bind_cpx_staff(result);
                }
            }

        }

        public string cpx_task_no = "";

        /// <summary>
        /// 确认生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cpx_sure_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txt_cpx_staff_no.Text))
            {
                MessageBox.Show("Please scan employee number");
                txt_cpx_staff_code.Focus();
                return;
            }
            else
            {
                //if (string.IsNullOrEmpty(lab_cpx_staff_department_code.Text.Trim()))
                //{
                //    MessageBox.Show("该员工未绑定部门,无法提交");
                //    txt_cpx_staff_code.Focus();
                //    return;
                //}
            }

            if (ckb_cpx_sfcc.Checked)
            {
                if (string.IsNullOrEmpty(txt_cpx_art.Text.Trim()))
                {
                    MessageBox.Show("Please scan the retest lab number");
                    txt_cpx_cc_task_no.Focus();
                    return;
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("cc_task_no", txt_cpx_cc_task_no.Text.Trim());
                data.Add("test_type", "0");
                data.Add("staff_no", txt_cpx_staff_no.Text.Trim());
                data.Add("staff_name", txt_cpx_staff_name.Text.Trim());
                data.Add("staff_department", txt_cpx_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_cpx_staff_department_code.Text.Trim());
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                cpx_task_no = ret.RetData;
                rahs_cpx_taskno_print();
            }
            else
            {
                if (string.IsNullOrEmpty(txt_cpx_art.Text))
                {
                    MessageBox.Show("Please scan or enter the ART number");
                    txt_cpx_art.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(cmb_cpx_category.Text))
                //{
                //    MessageBox.Show("请选择CateGory(开发系列)");
                //    cmb_cpx_category.Focus();
                //    return;
                //}

                #region 【【实验室送检】量产拉力部分选项从必填改为非必填】
                //if (string.IsNullOrEmpty(txt_cpx_cmbbh.Text))
                //{
                //    MessageBox.Show("请输入尺码标编号");
                //    txt_cpx_cmbbh.Focus();
                //    return;
                //}
                #endregion

                //if (string.IsNullOrEmpty(cmb_cpx_cpjb.Text))
                //{
                //    MessageBox.Show("请选择产品级别");
                //    cmb_cpx_cpjb.Focus();
                //    return;
                //}

                //if (string.IsNullOrEmpty(cmb_cpx_xjjb.Text))
                //{
                //    MessageBox.Show("请选择新旧级别");
                //    cmb_cpx_xjjb.Focus();
                //    return;
                //}


                //if (string.IsNullOrEmpty(cmb_cpx_xb.Text))
                //{
                //    MessageBox.Show("请选择年龄性别");
                //    cmb_cpx_xb.Focus();
                //    return;
                //}

                //if (string.IsNullOrEmpty(cmb_cpx_cpzl.Text))
                //{
                //    MessageBox.Show("请选择成品种类");
                //    cmb_cpx_cpzl.Focus();
                //    return;
                //}

                //if (string.IsNullOrEmpty(txt_cpx_test_id.Text))
                //{
                //    MessageBox.Show("请输入TEST ID");
                //    txt_cpx_test_id.Focus();
                //    return;
                //}

                if (string.IsNullOrEmpty(cmb_cpx_jd.Text))
                {
                    MessageBox.Show("Please select a stage");
                    cmb_cpx_jd.Focus();
                    return;
                }
                int scsl = 0;
                int.TryParse(txt_cpx_scsl.Text.Trim(), out scsl);

                if (scsl < 1)
                {
                    MessageBox.Show("Please enter a positive integer >=1 to send the test quantity");
                    txt_cpx_scsl.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmb_cpx_size.Text))
                {
                    MessageBox.Show("Please select size");
                    cmb_cpx_size.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmb_cpx_fgt.Text))
                {
                    MessageBox.Show("Please select the FGT test type");
                    cmb_cpx_fgt.Focus();
                    return;
                }


                List<Dictionary<string, object>> itemlist = new List<Dictionary<string, object>>();
                foreach (DataGridViewRow dr in dgv_cpx.Rows)
                {
                    if (Convert.ToBoolean(dr.Cells["cpx_check"].Value))
                    {
                        int sysl = 0;
                        if (dr.Cells["cpx_sample_qty"].Value == null)
                        {
                            MessageBox.Show($"Sample quantity cannot be empty, serial number{dr.Cells["cpx_xh"].Value}");
                            return;
                        }
                        string qty = dr.Cells["cpx_sample_qty"].Value.ToString();
                        if (string.IsNullOrWhiteSpace(qty))
                        {
                            MessageBox.Show($"Sample quantity cannot be empty, serial number{dr.Cells["cpx_xh"].Value}");
                            return;
                        }
                        int.TryParse(qty, out sysl);
                        if (sysl <= 0)
                        {
                            MessageBox.Show($"Please enter the positive integer sample quantity >= 1, serial number{dr.Cells["cpx_xh"].Value.ToString()}");
                            dr.Cells["cpx_sample_qty"].Selected = true;
                            return;
                        }
                        Dictionary<string, object> item = new Dictionary<string, object>();
                        item.Add("source", dr.Cells["cpx_type"].Value.ToString() == "DQA测试任务" ? "0" : "1");
                        item.Add("inspection_code", dr.Cells["cpx_inspection_code"].Value.ToString());
                        item.Add("inspection_name", dr.Cells["cpx_inspection_name"].Value.ToString());
                        item.Add("judgment_criteria", dr.Cells["cpx_judgment_criteria"].Value.ToString());
                        item.Add("standard_value", dr.Cells["cpx_standard_value"].Value.ToString());
                        item.Add("unit", dr.Cells["cpx_unit"].Value.ToString());
                        item.Add("sample_qty", dr.Cells["cpx_sample_qty"].Value.ToString());
                        item.Add("g_formula_type", dr.Cells["cpx_tygs_code"].Value == null ? "" : dr.Cells["cpx_tygs_code"].Value.ToString());
                        item.Add("d_formula_type", dr.Cells["cpx_zdygs_code"].Value == null ? "" : dr.Cells["cpx_zdygs_code"].Value.ToString());
                        item.Add("art_d_remark", dr.Cells["cpx_remarks"].Value.ToString());
                        item.Add("inspection_type", dr.Cells["cpx_inspection_type"].Value.ToString());
                        item.Add("choice_name", dr.Cells["cpx_choice_name"].Value.ToString());
                        item.Add("choice_no", dr.Cells["cpx_choice_no"].Value.ToString());
                        item.Add("judge_type", dr.Cells["cpx_judge_type"].Value.ToString());
                        itemlist.Add(item);
                    }
                }

                if (itemlist.Count <= 0)
                {
                    MessageBox.Show("No inspection items, please check");
                    return;
                }

                Dictionary<string, object> data1 = new Dictionary<string, object>();
                data1.Add("art_no", txt_cpx_art.Text.Trim());
                data1.Add("test_type", "0");
                //键值对传值
                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "IsSubmit",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data1));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1.ToString());

                if (!ret1.IsSuccess)
                {
                    MessageBox.Show(ret1.ErrMsg);
                    return;
                }
                if (Convert.ToInt32(ret1.RetData) > 0)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("This ART has already submitted finished shoes for testing and registration, do you want to continue?", "Prompt", messButton);

                    if (dr == DialogResult.Cancel)//如果点击“取消”按钮
                    {
                        return;
                    }
                }


                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("art_no", txt_cpx_art.Text.Trim());
                data.Add("shoe_no", txt_cpx_shose.Text.Trim());
                //data.Add("shoe_name", "");
                data.Add("model_no", txt_cpx_model_no.Text.Trim());

                //data.Add("category_code", cmb_cpx_category.SelectedValue == null ? "" : cmb_cpx_category.SelectedValue);
                //data.Add("category_name", cmb_cpx_category.Text);
                data.Add("category_code", tb_cpx_category.Text);
                data.Add("category_name", tb_cpx_category.Text);

                data.Add("product_level_code", tb_cpx_cpjb.Text);
                data.Add("product_level_value", tb_cpx_cpjb.Text);

                data.Add("season", txt_cpx_jidu.Text.Trim());

                //data.Add("pb_type_code", cmb_cpx_xjjb.SelectedValue == null ? "" : cmb_cpx_xjjb.SelectedValue);
                //data.Add("pb_type_level", cmb_cpx_xjjb.Text);
                data.Add("pb_type_code", tb_cpx_xjjb.Text);
                data.Add("pb_type_level", tb_cpx_xjjb.Text);

                data.Add("gender", cmb_cpx_xb.SelectedValue == null ? "" : cmb_cpx_xb.SelectedValue);
                data.Add("gender_name", cmb_cpx_xb.Text);

                data.Add("phase_creation_no", cmb_cpx_jd.SelectedValue == null ? "" : cmb_cpx_jd.SelectedValue);
                data.Add("phase_creation_name", cmb_cpx_jd.Text);

                data.Add("send_test_qty", scsl);
                data.Add("size", cmb_cpx_size.Text);
                data.Add("order_po", txt_cpx_ddpo.Text.Trim());
                data.Add("order_po_qty", txt_cpx_posl.Text.Trim());

                data.Add("fgt_no", cmb_cpx_fgt.SelectedValue == null ? "" : cmb_cpx_fgt.SelectedValue);
                data.Add("fgt_name", cmb_cpx_fgt.Text);

                data.Add("cp_type_code", cmb_cpx_cpzl.SelectedValue == null ? "" : cmb_cpx_cpzl.SelectedValue);
                data.Add("cp_type_name", cmb_cpx_cpzl.Text);

                data.Add("test_reason", txt_cpx_reason.Text.Trim());
                data.Add("staff_no", txt_cpx_staff_no.Text.Trim());
                data.Add("staff_name", txt_cpx_staff_name.Text.Trim());
                data.Add("staff_department", txt_cpx_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_cpx_staff_department_code.Text.Trim());
                data.Add("task_state", 0);
                data.Add("test_type", 0);
                data.Add("cmbbh", txt_cpx_cmbbh.Text.Trim());
                data.Add("test_id", txt_cpx_test_id.Text.Trim());
                data.Add("itemlist", itemlist);



                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                cpx_task_no = ret.RetData;
                rahs_cpx_taskno_print();
            }
        }

        /// <summary>
        /// 刷新任务编号
        /// </summary>
        public void rahs_cpx_taskno_print()
        {
            if (string.IsNullOrEmpty(cpx_task_no))
            {
                txt_cpx_task_no.Text = "";
                btn_cpx_print.Enabled = false;
            }
            else
            {
                txt_cpx_task_no.Text = cpx_task_no;
                btn_cpx_print.Enabled = true;
            }
        }

        /// <summary>
        /// po订单双击弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_cpx_ddpo_DoubleClick(object sender, EventArgs e)
        {
            //F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(cpx_po_dt);
            F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(txt_cpx_art.Text, txt_cpx_ddpo.Text);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.selectlist.Count > 0)
            {
                string poorder = "";
                int total_qty = 0;
                foreach (var item in frm.selectlist)
                {
                    poorder += item["poorder"].ToString() + ",";
                    int qty = 0;
                    int.TryParse(item["qty"].ToString(), out qty);
                    total_qty += qty;
                }
                txt_cpx_ddpo.Text = poorder.Trim(',');
                txt_cpx_posl.Text = total_qty.ToString();
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cpx_print_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_cpx_task_no.Text.Trim()))
            {
                F_QCM_TaskNo_Print frm = new F_QCM_TaskNo_Print(txt_cpx_task_no.Text.Trim());
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }

        }

        public void Get_cpx_checkItem(object sender, EventArgs e)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //data.Add("AGE_GENDER_CODE", cmb_cpx_xb.SelectedIndex > 0 ? cmb_cpx_xb.SelectedValue : "");
            //data.Add("CATEGORY_CODE", cmb_cpx_category.SelectedIndex > 0 ? cmb_cpx_category.SelectedValue : "");
            data.Add("FGT_CODE", cmb_cpx_fgt.SelectedIndex > 0 ? cmb_cpx_fgt.SelectedValue : "");
            //data.Add("FINISHED_PRODUCT_CODE", cmb_cpx_cpzl.SelectedIndex > 0 ? cmb_cpx_cpzl.SelectedValue : "");
            //data.Add("PB_TYPE_CODE", cmb_cpx_xjjb.SelectedIndex > 0 ? cmb_cpx_xjjb.SelectedValue : "");
            //data.Add("PRODUCT_LEVEL_CODE", cmb_cpx_cpjb.SelectedIndex > 0 ? cmb_cpx_cpjb.SelectedValue : "");
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Get_cpx_checkItem",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return;
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());

            for (int i = dgv_cpx.Rows.Count - 1; i >= 0; i--)
            {
                if (dgv_cpx.Rows[i].Cells["cpx_type"].Value.ToString() == "conventional")//常规
                {
                    dgv_cpx.Rows.Remove(dgv_cpx.Rows[i]);
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
                int i = dgv_cpx.Rows.Add();
                dgv_cpx.Rows[i].ReadOnly = true;
                dgv_cpx.Rows[i].Cells["cpx_xh"].Value = (i + 1).ToString();
                dgv_cpx.Rows[i].Cells["cpx_check"].Value = true;
                dgv_cpx.Rows[i].Cells["cpx_type"].Value = dr["type"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_choice_name"].Value = dr["choice_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_judge_type"].Value = dr["judge_type"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_standard_value"].Value = dr["standard_value"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_unit"].Value = dr["unit"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_sample_qty"].Value = "";
                dgv_cpx.Rows[i].Cells["cpx_remarks"].Value = dr["remarks"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_choice_no"].Value = dr["choice_no"].ToString();
                dgv_cpx.Rows[i].Cells["cpx_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }

        }

        private void txt_cpx_scsl_TextChanged(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(txt_cpx_scsl.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(txt_cpx_scsl.Text))
            {
                MessageBox.Show("Please enter a positive integer");
                txt_cpx_scsl.Text = "";
                return;
            }
        }

        private void ckb_cpx_sfcc_CheckedChanged(object sender, EventArgs e)
        {
            dgv_cpx.Rows.Clear();
            if (ckb_cpx_sfcc.Checked)
            {
                txt_cpx_cc_task_no.Enabled = true;
                txt_cpx_cc_task_no.Text = "";
                txt_cpx_cc_task_no.Focus();
                txt_cpx_task_no.Text = "";
                txt_cpx_qrcode.Enabled = false;
                txt_cpx_qrcode.Text = "";
                txt_cpx_shose.Text = "";
                txt_cpx_art.Text = "";
                txt_cpx_model_no.Text = "";
                //cmb_cpx_category.DataSource = null;
                //cmb_cpx_category.Enabled = false;
                //cmb_cpx_category.Text = "";
                tb_cpx_category.Text = "";
                txt_cpx_cmbbh.Text = "";
                txt_cpx_cmbbh.Enabled = false;
                //cmb_cpx_cpjb.DataSource = null;
                //cmb_cpx_cpjb.Enabled = false;
                //cmb_cpx_cpjb.Text = "";
                tb_cpx_cpjb.Text = "";
                txt_cpx_jidu.Text = "";
                //cmb_cpx_xjjb.DataSource = null;
                //cmb_cpx_xjjb.Enabled = false;
                //cmb_cpx_xjjb.Text = "";
                tb_cpx_xjjb.Text = "";
                cmb_cpx_xb.DataSource = null;
                cmb_cpx_xb.Enabled = false;
                cmb_cpx_xb.Text = "";
                cmb_cpx_cpzl.DataSource = null;
                cmb_cpx_cpzl.Enabled = false;
                cmb_cpx_cpzl.Text = "";
                txt_cpx_test_id.Text = "";
                txt_cpx_test_id.Enabled = false;
                cmb_cpx_jd.DataSource = null;
                cmb_cpx_jd.Enabled = false;
                cmb_cpx_jd.Text = "";
                txt_cpx_scsl.Text = "";
                txt_cpx_scsl.Enabled = false;
                cmb_cpx_size.DataSource = null;
                cmb_cpx_size.Enabled = false;
                cmb_cpx_size.Text = "";
                txt_cpx_posl.Text = "";
                txt_cpx_ddpo.Text = "";
                txt_cpx_ddpo.Enabled = false;
                cmb_cpx_fgt.DataSource = null;
                cmb_cpx_fgt.Enabled = false;
                cmb_cpx_fgt.Text = "";
                txt_cpx_reason.Text = "";
                txt_cpx_reason.Enabled = false;
            }
            else
            {
                txt_cpx_cc_task_no.Enabled = false;
                txt_cpx_cc_task_no.Text = "";
                txt_cpx_task_no.Text = "";
                txt_cpx_qrcode.Enabled = true;
                txt_cpx_qrcode.Text = "";
                txt_cpx_shose.Text = "";
                txt_cpx_art.Text = "";
                txt_cpx_model_no.Text = "";
                //cmb_cpx_category.DataSource = null;
                //cmb_cpx_category.Enabled = true;
                //cmb_cpx_category.Text = "";
                tb_cpx_category.Text = "";
                txt_cpx_cmbbh.Text = "";
                txt_cpx_cmbbh.Enabled = true;
                //cmb_cpx_cpjb.DataSource = null;
                //cmb_cpx_cpjb.Enabled = true;
                //cmb_cpx_cpjb.Text = "";
                tb_cpx_cpjb.Text = "";
                txt_cpx_jidu.Text = "";
                //cmb_cpx_xjjb.DataSource = null;
                //cmb_cpx_xjjb.Enabled = true;
                //cmb_cpx_xjjb.Text = "";
                tb_cpx_xjjb.Text = "";
                cmb_cpx_xb.DataSource = null;
                cmb_cpx_xb.Enabled = true;
                cmb_cpx_xb.Text = "";
                cmb_cpx_cpzl.DataSource = null;
                cmb_cpx_cpzl.Enabled = true;
                cmb_cpx_cpzl.Text = "";
                txt_cpx_test_id.Text = "";
                txt_cpx_test_id.Enabled = true;
                cmb_cpx_jd.DataSource = null;
                cmb_cpx_jd.Enabled = true;
                cmb_cpx_jd.Text = "";
                txt_cpx_scsl.Text = "";
                txt_cpx_scsl.Enabled = true;
                cmb_cpx_size.DataSource = null;
                cmb_cpx_size.Enabled = true;
                cmb_cpx_size.Text = "";
                txt_cpx_posl.Text = "";
                txt_cpx_ddpo.Text = "";
                txt_cpx_ddpo.Enabled = true;
                cmb_cpx_fgt.DataSource = null;
                cmb_cpx_fgt.Enabled = true;
                cmb_cpx_fgt.Text = "";
                txt_cpx_reason.Text = "";
                txt_cpx_reason.Enabled = true;
            }
        }

        private void txt_cpx_cc_task_no_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", txt_cpx_cc_task_no.Text.Trim());
                p.Add("test_type", "0");
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
                    MessageBox.Show(ret.ErrMsg);
                    txt_cpx_task_no.Text = "";
                    txt_cpx_task_no.Focus();
                    return;
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["info"].ToString());
                DataTable itemlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["itemlist"].ToString());

                txt_cpx_qrcode.Text = info["ART_NO"].ToString();
                txt_cpx_shose.Text = info["SHOE_NO"].ToString();
                txt_cpx_art.Text = info["ART_NO"].ToString();
                txt_cpx_model_no.Text = info["MODEL_NO"].ToString();
                tb_cpx_category.Text = info["CATEGORY_CODE"].ToString();
                txt_cpx_cmbbh.Text = info["CMBBH"].ToString();
                tb_cpx_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();
                txt_cpx_jidu.Text = info["SEASON"].ToString();
                tb_cpx_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();
                cmb_cpx_xb.Text = info["GENDER_NAME"].ToString();
                cmb_cpx_cpzl.Text = info["CP_TYPE_NAME"].ToString();
                txt_cpx_test_id.Text = info["TEST_ID"].ToString();
                cmb_cpx_jd.Text = info["PHASE_CREATION_NAME"].ToString();
                txt_cpx_scsl.Text = info["SEND_TEST_QTY"].ToString();
                cmb_cpx_size.Text = info["SIZES"].ToString();
                txt_cpx_posl.Text = info["ORDER_PO"].ToString();
                txt_cpx_ddpo.Text = info["ORDER_PO_QTY"].ToString();
                cmb_cpx_fgt.Text = info["FGT_NAME"].ToString();
                txt_cpx_reason.Text = info["TEST_REASON"].ToString();

                dgv_cpx.Rows.Clear();
                foreach (DataRow dr in itemlist.Rows)
                {
                    int i = dgv_cpx.Rows.Add();
                    dgv_cpx.Rows[i].ReadOnly = true;
                    dgv_cpx.Rows[i].Cells["cpx_xh"].Value = (i + 1).ToString();
                    dgv_cpx.Rows[i].Cells["cpx_check"].Value = true;
                    dgv_cpx.Rows[i].Cells["cpx_type"].Value = dr["SOURCES"].ToString() == "0" ? "DQA测试任务" : "conventional";
                    dgv_cpx.Rows[i].Cells["cpx_inspection_type_name"].Value = dr["INSPECTION_TYPE_NAME"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_choice_name"].Value = dr["CHOICE_NAME"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_inspection_code"].Value = dr["INSPECTION_CODE"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_inspection_name"].Value = dr["INSPECTION_NAME"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_judgment_criteria_name"].Value = dr["JUDGMENT_CRITERIA_NAME"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_judge_type"].Value = dr["JUDGE_TYPE"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_judge_type_name"].Value = dr["JUDGE_TYPE_NAME"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_standard_value"].Value = dr["STANDARD_VALUE"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_unit"].Value = dr["UNIT"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_sample_qty"].Value = dr["SAMPLE_QTY"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_remarks"].Value = dr["ART_D_REMARK"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_inspection_type"].Value = dr["INSPECTION_TYPE"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_choice_no"].Value = dr["CHOICE_NO"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_judgment_criteria"].Value = dr["JUDGMENT_CRITERIA"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_tygs_code"].Value = dr["G_FORMULA_CODE"].ToString();
                    dgv_cpx.Rows[i].Cells["cpx_zdygs_code"].Value = dr["D_FORMULA_CODE"].ToString();

                    var tygs = list_tygs_data.FirstOrDefault(x => x.CODE == dr["G_FORMULA_CODE"].ToString());
                    if (tygs != null)
                        dgv_cpx.Rows[i].Cells["cpx_tygs"].Value = tygs.NAME;
                    var zdygs = list_zdygs_data.FirstOrDefault(x => x.CODE == dr["D_FORMULA_CODE"].ToString());
                    if (zdygs != null)
                        dgv_cpx.Rows[i].Cells["cpx_zdygs"].Value = zdygs.NAME;
                }

            }
        }

        #endregion

        #region 部件

        private void Bind_bj_staff(Dictionary<string, object> result)
        {
            txt_bj_staff_no.Text = result["STAFF_NO"].ToString();
            txt_bj_staff_name.Text = result["STAFF_NAME"].ToString();
            txt_bj_staff_department.Text = result["DEPARTMENT_NAME"].ToString();
            lab_bj_staff_department_code.Text = result["DEPARTMENT_CODE"].ToString();
        }

        public void Bind_bj_info(Dictionary<string, object> result)
        {
            Dictionary<string, object> info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result["info"].ToString());
            bj_po_dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["po_info"].ToString());
            DataTable check_item = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["check_item"].ToString());

            txt_bj_shose.Text = info["SHOE_NAME"].ToString();
            txt_bj_model_no.Text = info["SHOE_NO"].ToString();
            //txt_bj_shose.Text = info["SHOE_NO"].ToString();
            //txt_bj_model_no.Text = info["MODEL_NO"].ToString();
            // txt_bj_category.Text = info["CATEGROY"].ToString();
           // txt_bj_cpjb.Text = info["PRODUCTION_LEVEL"].ToString();
            txt_bj_jidu.Text = info["DEVELOP_SEASON"].ToString();
            txt_bj_xb.Text = info["SEG_NO"].ToString(); ;
            txt_bj_xjjb.Text = info["DEVELOP_TYPE"].ToString();

            BindDDL(cmb_bj_jieduan, list_jd_data, "bj", true);
            BindDDL(cmb_bj_fgt, list_fgt_data, "bj", true,true);
            //BindDDL(cmb_bj_bjmc, list_parts_data, "bj", true, true);
            BindDDL(cmb_bj_bwmc, list_position_data, "bj", true, true);
            //BindDDL(cmb_bj_category, list_category_data, "bj", true, true);
            tb_bj_kfxl.Text = info["CATEGROY_ID"].ToString();
            //BindDDL(cmb_bj_cpjb, list_productlevel_data, "bj", true, true); 
            tb_bj_cpjb.Text = info["PRODUCT_LEVEL"].ToString();
            Bind_size(cmb_bj_size, list_size_data);

            dgv_bj.Rows.Clear();
            foreach (DataRow dr in check_item.Rows)
            {
                int i = dgv_bj.Rows.Add();
                dgv_bj.Rows[i].Cells["bj_xh"].Value = (i + 1).ToString();
                dgv_bj.Rows[i].Cells["bj_check"].Value = true;
                dgv_bj.Rows[i].Cells["bj_type"].Value = dr["type"].ToString();
                dgv_bj.Rows[i].Cells["bj_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_choice_name"].Value = dr["choice_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_bj.Rows[i].Cells["bj_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_judge_type"].Value = dr["judge_type"].ToString();
                dgv_bj.Rows[i].Cells["bj_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_standard_value"].Value = dr["standard_value"].ToString();
                dgv_bj.Rows[i].Cells["bj_unit"].Value = dr["unit"].ToString();
                dgv_bj.Rows[i].Cells["bj_sample_qty"].Value = "";
                dgv_bj.Rows[i].Cells["bj_remarks"].Value = dr["remarks"].ToString();
                dgv_bj.Rows[i].Cells["bj_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_bj.Rows[i].Cells["bj_choice_no"].Value = dr["choice_no"].ToString();
                dgv_bj.Rows[i].Cells["bj_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }
        }

        #region 表格编辑代码快
        private void dgv_bj_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CB_G_formula_type != null)
            {
                CB_G_formula_type.Visible = false;
            }
            if (CB_D_formula_type != null)
            {
                CB_D_formula_type.Visible = false;
            }
            if (TXT_Unit != null)
            {
                TXT_Unit.Visible = false;
                TXT_Unit.Dispose();
            }
            if (TXT_Sample_qt != null)
            {
                TXT_Sample_qt.Visible = false;
                TXT_Sample_qt.Dispose();
            }
            if (TXT_Remarks != null)
            {
                TXT_Remarks.Visible = false;
                TXT_Remarks.Dispose();
            }
            if (e.RowIndex > -1)
            {
                if (dgv_bj.Columns[e.ColumnIndex].Name == "bj_tygs")
                {

                    CB_G_formula_type = new ComboBox();
                    CB_G_formula_type.Enabled = true;
                    CB_G_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_G_formula_type.DataSource = list_tygs_data;
                    CB_G_formula_type.DisplayMember = "NAME";
                    CB_G_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_bj.GetCellDisplayRectangle(dgv_bj.CurrentCell.ColumnIndex, dgv_bj.CurrentCell.RowIndex, false);
                    CB_G_formula_type.Left = rect.Left;
                    CB_G_formula_type.Top = rect.Top;
                    CB_G_formula_type.Width = rect.Width;
                    CB_G_formula_type.Height = rect.Height;
                    CB_G_formula_type.Visible = true;
                    dgv_bj.Controls.Add(CB_G_formula_type);
                    if (dgv_bj.Rows[e.RowIndex].Cells["bj_tygs_code"].Value != null && !string.IsNullOrEmpty(dgv_bj.Rows[e.RowIndex].Cells["bj_tygs_code"].Value.ToString()))
                    {
                        CB_G_formula_type.SelectedValue = dgv_bj.Rows[e.RowIndex].Cells["bj_tygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_G_formula_type.SelectedIndex = 0;
                    }
                    CB_G_formula_type.Focus();
                    CB_G_formula_type.SelectedIndexChanged += CB_G_formula_type_SelectedIndexChanged1;
                }
                if (dgv_bj.Columns[e.ColumnIndex].Name == "bj_zdygs")
                {

                    CB_D_formula_type = new ComboBox();
                    CB_D_formula_type.Enabled = true;
                    CB_D_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_D_formula_type.DataSource = list_zdygs_data;
                    CB_D_formula_type.DisplayMember = "NAME";
                    CB_D_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_bj.GetCellDisplayRectangle(dgv_bj.CurrentCell.ColumnIndex, dgv_bj.CurrentCell.RowIndex, false);
                    CB_D_formula_type.Left = rect.Left;
                    CB_D_formula_type.Top = rect.Top;
                    CB_D_formula_type.Width = rect.Width;
                    CB_D_formula_type.Height = rect.Height;
                    CB_D_formula_type.Visible = true;
                    dgv_bj.Controls.Add(CB_D_formula_type);
                    if (dgv_bj.Rows[e.RowIndex].Cells["bj_zdygs_code"].Value != null && !string.IsNullOrEmpty(dgv_bj.Rows[e.RowIndex].Cells["bj_zdygs_code"].Value.ToString()))
                    {
                        CB_D_formula_type.SelectedValue = dgv_bj.Rows[e.RowIndex].Cells["bj_zdygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_D_formula_type.SelectedIndex = 0;
                    }
                    CB_D_formula_type.Focus();
                    CB_D_formula_type.SelectedIndexChanged += CB_D_formula_type_SelectedIndexChanged1;
                }
                if (dgv_bj.Columns[e.ColumnIndex].Name == "bj_unit")
                {

                    TXT_Unit = new TextBox();
                    TXT_Unit.Enabled = true;

                    Rectangle rect = dgv_bj.GetCellDisplayRectangle(dgv_bj.CurrentCell.ColumnIndex, dgv_bj.CurrentCell.RowIndex, false);
                    TXT_Unit.Left = rect.Left;
                    TXT_Unit.Top = rect.Top;
                    TXT_Unit.Width = rect.Width;
                    TXT_Unit.Height = rect.Height;
                    TXT_Unit.Visible = true;
                    dgv_bj.Controls.Add(TXT_Unit);
                    if (dgv_bj.Rows[e.RowIndex].Cells["bj_unit"].Value != null && !string.IsNullOrEmpty(dgv_bj.Rows[e.RowIndex].Cells["bj_unit"].Value.ToString()))
                    {
                        TXT_Unit.Text = dgv_bj.Rows[e.RowIndex].Cells["bj_unit"].Value.ToString();
                    }
                    TXT_Unit.Focus();
                    TXT_Unit.SelectionStart = TXT_Unit.Text.Length;
                    TXT_Unit.TextChanged += TXT_Unit_TextChanged1;
                }
                if (dgv_bj.Columns[e.ColumnIndex].Name == "bj_sample_qty")
                {

                    TXT_Sample_qt = new TextBox();
                    TXT_Sample_qt.Enabled = true;

                    Rectangle rect = dgv_bj.GetCellDisplayRectangle(dgv_bj.CurrentCell.ColumnIndex, dgv_bj.CurrentCell.RowIndex, false);
                    TXT_Sample_qt.Left = rect.Left;
                    TXT_Sample_qt.Top = rect.Top;
                    TXT_Sample_qt.Width = rect.Width;
                    TXT_Sample_qt.Height = rect.Height;
                    TXT_Sample_qt.Visible = true;
                    dgv_bj.Controls.Add(TXT_Sample_qt);
                    if (dgv_bj.Rows[e.RowIndex].Cells["bj_sample_qty"].Value != null && !string.IsNullOrEmpty(dgv_bj.Rows[e.RowIndex].Cells["bj_sample_qty"].Value.ToString()))
                    {
                        TXT_Sample_qt.Text = dgv_bj.Rows[e.RowIndex].Cells["bj_sample_qty"].Value.ToString();
                    }
                    TXT_Sample_qt.Focus();
                    TXT_Sample_qt.SelectionStart = TXT_Sample_qt.Text.Length;
                    TXT_Sample_qt.TextChanged += TXT_Sample_qt_TextChanged1;
                }
                if (dgv_bj.Columns[e.ColumnIndex].Name == "bj_remarks")
                {

                    TXT_Remarks = new TextBox();
                    TXT_Remarks.Enabled = true;

                    Rectangle rect = dgv_bj.GetCellDisplayRectangle(dgv_bj.CurrentCell.ColumnIndex, dgv_bj.CurrentCell.RowIndex, false);
                    TXT_Remarks.Left = rect.Left;
                    TXT_Remarks.Top = rect.Top;
                    TXT_Remarks.Width = rect.Width;
                    TXT_Remarks.Height = rect.Height;
                    TXT_Remarks.Visible = true;
                    dgv_bj.Controls.Add(TXT_Remarks);
                    if (dgv_bj.Rows[e.RowIndex].Cells["bj_remarks"].Value != null && !string.IsNullOrEmpty(dgv_bj.Rows[e.RowIndex].Cells["bj_remarks"].Value.ToString()))
                    {
                        TXT_Remarks.Text = dgv_bj.Rows[e.RowIndex].Cells["bj_remarks"].Value.ToString();
                    }
                    TXT_Remarks.Focus();
                    TXT_Remarks.SelectionStart = TXT_Remarks.Text.Length;
                    TXT_Remarks.TextChanged += TXT_Remarks_TextChanged1;
                }
            }
        }
        private void CB_G_formula_type_SelectedIndexChanged1(object sender, EventArgs e)
        {
            dgv_bj.CurrentCell.Value = CB_G_formula_type.Text;
            dgv_bj.Rows[dgv_bj.CurrentCell.RowIndex].Cells["bj_tygs_code"].Value = CB_G_formula_type.SelectedValue;
            CB_G_formula_type.Visible = false;
            CB_G_formula_type.Dispose();
        }
        private void CB_D_formula_type_SelectedIndexChanged1(object sender, EventArgs e)
        {
            dgv_bj.CurrentCell.Value = CB_D_formula_type.Text;
            dgv_bj.Rows[dgv_bj.CurrentCell.RowIndex].Cells["bj_zdygs_code"].Value = CB_D_formula_type.SelectedValue;
            CB_D_formula_type.Visible = false;
            CB_D_formula_type.Dispose();
        }
        private void TXT_Unit_TextChanged1(object sender, EventArgs e)
        {
            dgv_bj.CurrentCell.Value = TXT_Unit.Text;
        }
        private void TXT_Sample_qt_TextChanged1(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(TXT_Sample_qt.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(TXT_Sample_qt.Text))
            {
                MessageBox.Show("请输入正整数");
                TXT_Sample_qt.Text = "";
                return;
            }
            dgv_bj.CurrentCell.Value = TXT_Sample_qt.Text;
        }
        private void TXT_Remarks_TextChanged1(object sender, EventArgs e)
        {
            dgv_bj.CurrentCell.Value = TXT_Remarks.Text;
        }
        #endregion

        private void txt_bj_art_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetArtInfo("bj", txt_bj_art.Text.Trim());
                if (result != null)
                {
                    Bind_bj_info(result);
                }
            }

        }

        private void txt_bj_art_DoubleClick(object sender, EventArgs e)
        {
            F_QCM_Ex_SelectART frm = new F_QCM_Ex_SelectART();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(frm.select_art))
            {
                txt_bj_art.Text = frm.select_art;
                var result = GetArtInfo("bj", frm.select_art);
                if (result != null)
                {
                    Bind_bj_info(result);
                }
            }
        }

        private void txt_bj_staff_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetStaffInfo(txt_bj_staff_code.Text.Trim());
                if (result != null)
                {
                    Bind_bj_staff(result);
                }
            }
        }

        public string bj_task_no = "";

        public DataTable bj_po_dt = new DataTable();

        public void rahs_bj_taskno_print()
        {
            if (string.IsNullOrEmpty(bj_task_no))
            {
                txt_bj_task_no.Text = "";
                btn_bj_print.Enabled = false;
            }
            else
            {
                txt_bj_task_no.Text = bj_task_no;
                btn_bj_print.Enabled = true;
            }
        }

        private void btn_bj_sure_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txt_bj_staff_no.Text))
            {
                MessageBox.Show("Please scan employee number");
                txt_bj_staff_no.Focus();
                return;
            }
            else
            {
                //if (string.IsNullOrEmpty(lab_bj_staff_department_code.Text.Trim()))
                //{
                //    MessageBox.Show("该员工未绑定部门,无法提交");
                //    txt_bj_staff_code.Focus();
                //    return;
                //}
            }
            if (ckb_bj_sfcc.Checked)
            {
                if (string.IsNullOrEmpty(txt_bj_art.Text.Trim()))
                {
                    MessageBox.Show("Please scan the retest lab number");
                    txt_bj_cc_task_no.Focus();
                    return;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("cc_task_no", txt_bj_cc_task_no.Text.Trim());
                data.Add("test_type", "1");
                data.Add("staff_no", txt_bj_staff_no.Text.Trim());
                data.Add("staff_name", txt_bj_staff_name.Text.Trim());
                data.Add("staff_department", txt_bj_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_bj_staff_department_code.Text.Trim());
                data.Add("manufacturer_jc", lab_bj_cs_jc.Text.Trim());
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                bj_task_no = ret.RetData;
                rahs_bj_taskno_print();
            }
            else
            {
                if (string.IsNullOrEmpty(txt_bj_art.Text))
                {
                    MessageBox.Show("Please scan or enter ART");
                    txt_bj_art.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(cmb_bj_cpjb.Text))
                //{
                //    MessageBox.Show("请选择产品级别");
                //    cmb_bj_cpjb.Focus();
                //    return;
                //}
                if (string.IsNullOrEmpty(cmb_bj_bwmc.Text))
                {
                    MessageBox.Show("Please select a part");
                    cmb_bj_bwmc.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(cmb_bj_category.Text))
                //{
                //    MessageBox.Show("请选择CateGory(开发系列)");
                //    cmb_bj_category.Focus();
                //    return;
                //}
                if (string.IsNullOrEmpty(txt_bj_reasaon.Text))
                {
                    MessageBox.Show("Please enter the reason for the test");
                    txt_bj_reasaon.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmb_bj_jieduan.Text))
                {
                    MessageBox.Show("Please select a stage");
                    cmb_bj_jieduan.Focus();
                    return;
                }
                int scsl = 0;
                int.TryParse(txt_bj_scsl.Text.Trim(), out scsl);

                if (scsl < 1)
                {
                    MessageBox.Show("Please enter a positive integer >=1 to send the test quantity");
                    txt_bj_scsl.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmb_bj_size.Text))
                {
                    MessageBox.Show("Please select size");
                    cmb_bj_size.Focus();
                    return;
                }


                List<Dictionary<string, object>> itemlist = new List<Dictionary<string, object>>();
                foreach (DataGridViewRow dr in dgv_bj.Rows)
                {
                    if (Convert.ToBoolean(dr.Cells["bj_check"].Value))
                    {
                        int sysl = 0;
                        string qty = dr.Cells["bj_sample_qty"].Value.ToString();
                        if (string.IsNullOrWhiteSpace(qty))
                        {
                            MessageBox.Show($"Sample quantity cannot be empty, serial number{dr.Cells["bj_xh"].Value.ToString()}");
                            return;
                        }
                        int.TryParse(qty, out sysl);
                        if (sysl <= 0)
                        {
                            MessageBox.Show($"Please enter the positive integer sample quantity >= 1, serial number{dr.Cells["bj_xh"].Value.ToString()}");
                            dr.Cells["bj_sample_qty"].Selected = true;
                            return;
                        }
                        Dictionary<string, object> item = new Dictionary<string, object>();
                        item.Add("source", dr.Cells["bj_type"].Value.ToString() == "DQA测试任务" ? "0" : "1");
                        item.Add("inspection_code", dr.Cells["bj_inspection_code"].Value.ToString());
                        item.Add("inspection_name", dr.Cells["bj_inspection_name"].Value.ToString());
                        item.Add("judgment_criteria", dr.Cells["bj_judgment_criteria"].Value.ToString());
                        item.Add("standard_value", dr.Cells["bj_standard_value"].Value.ToString());
                        item.Add("unit", dr.Cells["bj_unit"].Value.ToString());
                        item.Add("sample_qty", dr.Cells["bj_sample_qty"].Value.ToString());
                        item.Add("g_formula_type", dr.Cells["bj_tygs_code"].Value == null ? "" : dr.Cells["bj_tygs_code"].Value.ToString());
                        item.Add("d_formula_type", dr.Cells["bj_zdygs_code"].Value == null ? "" : dr.Cells["bj_zdygs_code"].Value.ToString());
                        item.Add("art_d_remark", dr.Cells["bj_remarks"].Value.ToString());
                        item.Add("inspection_type", dr.Cells["bj_inspection_type"].Value.ToString());
                        item.Add("choice_name", dr.Cells["bj_choice_name"].Value.ToString());
                        item.Add("choice_no", dr.Cells["bj_choice_no"].Value.ToString());
                        item.Add("judge_type", dr.Cells["bj_judge_type"].Value.ToString());
                        itemlist.Add(item);
                    }
                }
                if (itemlist.Count <= 0)
                {
                    MessageBox.Show("No inspection items, please check");
                    return;
                }


                Dictionary<string, object> data1 = new Dictionary<string, object>();
                data1.Add("art_no", txt_bj_art.Text.Trim());
                data1.Add("test_type", "1");
                //键值对传值
                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "IsSubmit",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data1));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1.ToString());

                if (!ret1.IsSuccess)
                {
                    MessageBox.Show(ret1.ErrMsg);
                    return;
                }
                if (Convert.ToInt32(ret1.RetData) > 0)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("This ART has already been submitted for testing and registration, do you want to continue?", "Prompt", messButton);

                    if (dr == DialogResult.Cancel)//如果点击“取消”按钮
                    {
                        return;
                    }
                }


                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("bj_code", txt_bj_code.Text.Trim());
                data.Add("art_no", txt_bj_art.Text.Trim());
                data.Add("shoe_no", txt_bj_shose.Text.Trim());
                //data.Add("shoe_name", "");
                data.Add("model_no", txt_bj_model_no.Text.Trim());
                //data.Add("category_code", cmb_bj_category.SelectedValue == null ? "" : cmb_bj_category.SelectedValue);
                //data.Add("category_name", cmb_bj_category.Text);
                data.Add("category_code", tb_bj_kfxl.Text);
                data.Add("category_name", tb_bj_kfxl.Text);
                data.Add("product_level_code", tb_bj_cpjb.Text);
                data.Add("product_level_value", tb_bj_cpjb.Text);
                // data.Add("product_level_value", "");
                data.Add("season", txt_bj_jidu.Text.Trim());
                data.Add("pb_type_level", txt_bj_xjjb.Text.Trim());
                // data.Add("pb_type_level", "");
                data.Add("gender_name", txt_bj_xb.Text.Trim());
                data.Add("phase_creation_no", cmb_bj_jieduan.SelectedValue == null ? "" : cmb_bj_jieduan.SelectedValue);
                data.Add("phase_creation_name", cmb_bj_jieduan.Text);
                data.Add("send_test_qty", scsl);
                data.Add("size", cmb_bj_size.Text);
                data.Add("order_po", txt_bj_po_order.Text.Trim());
                data.Add("order_po_qty", txt_bj_po_qty.Text.Trim());
                data.Add("fgt_no", cmb_bj_fgt.SelectedValue == null ? "" : cmb_bj_fgt.SelectedValue);
                data.Add("fgt_name", cmb_bj_fgt.Text);
                data.Add("test_reason", txt_bj_reasaon.Text.Trim());
                data.Add("staff_no", txt_bj_staff_no.Text.Trim());
                data.Add("staff_name", txt_bj_staff_name.Text.Trim());
                data.Add("staff_department", txt_bj_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_bj_staff_department_code.Text.Trim());
                data.Add("task_state", 0);
                data.Add("test_type", 1);
                //data.Add("parts_code", cmb_bj_cpjb.SelectedValue == null ? "" : cmb_bj_fgt.SelectedValue);
                //data.Add("parts_name", cmb_bj_cpjb.Text);
                data.Add("parts_code", tb_bj_cpjb.Text);
                data.Add("parts_name", tb_bj_cpjb.Text);
                data.Add("position_code", cmb_bj_bwmc.SelectedValue == null ? "" : cmb_bj_bwmc.SelectedValue);
                data.Add("position_name", cmb_bj_bwmc.Text);
                data.Add("manufacturer_code", lab_bj_cs_code.Text.Trim());
                data.Add("manufacturer_name", txt_bj_cs.Text);
                data.Add("manufacturer_jc", lab_bj_cs_jc.Text.Trim());
                data.Add("test_id", tb_bj_test_id.Text);
                data.Add("itemlist", itemlist);



                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                bj_task_no = ret.RetData;
                rahs_bj_taskno_print();
            }

        }

        private void txt_bj_po_order_DoubleClick(object sender, EventArgs e)
        {

            //F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(bj_po_dt);
            F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(txt_bj_art.Text, txt_bj_po_order.Text);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.selectlist.Count > 0)
            {
                string poorder = "";
                int total_qty = 0;
                foreach (var item in frm.selectlist)
                {
                    poorder += item["poorder"].ToString() + ",";
                    int qty = 0;
                    int.TryParse(item["qty"].ToString(), out qty);
                    total_qty += qty;
                }
                txt_bj_po_order.Text = poorder.Trim(',');
                txt_bj_po_qty.Text = total_qty.ToString();
            }
        }

        private void btn_bj_print_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_bj_task_no.Text.Trim()))
            {
                F_QCM_TaskNo_Print frm = new F_QCM_TaskNo_Print(txt_bj_task_no.Text.Trim());
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        private void txt_bj_scsl_TextChanged(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(txt_bj_scsl.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(txt_bj_scsl.Text))
            {
                MessageBox.Show("Please enter a positive integer");
                txt_bj_scsl.Text = "";
                return;
            }
        }

        public void Get_bj_checkItem(object sender, EventArgs e)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //data.Add("CATEGORY_CODE", cmb_bj_category.SelectedIndex > 0 ? cmb_bj_category.SelectedValue : "");
            //data.Add("PARTS_CODE", cmb_bj_cpjb.SelectedIndex > 0 ? cmb_bj_cpjb.SelectedValue : "");
            //data.Add("POSITION_CODE", cmb_bj_bwmc.SelectedIndex > 0 ? cmb_bj_bwmc.SelectedValue : "");
            data.Add("fgt_code", cmb_bj_fgt.SelectedIndex > 0 ? cmb_bj_fgt.SelectedValue : "");
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Get_bj_checkItem",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return;
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());

            for (int i = dgv_bj.Rows.Count - 1; i >= 0; i--)
            {
                if (dgv_bj.Rows[i].Cells["bj_type"].Value.ToString() == "conventional")
                {
                    dgv_bj.Rows.Remove(dgv_bj.Rows[i]);
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                int i = dgv_bj.Rows.Add();
                dgv_bj.Rows[i].Cells["bj_xh"].Value = (i + 1).ToString();
                dgv_bj.Rows[i].Cells["bj_check"].Value = true;
                dgv_bj.Rows[i].Cells["bj_type"].Value = dr["type"].ToString();
                dgv_bj.Rows[i].Cells["bj_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_choice_name"].Value = dr["choice_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_bj.Rows[i].Cells["bj_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_judge_type"].Value = dr["judge_type"].ToString();
                dgv_bj.Rows[i].Cells["bj_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_bj.Rows[i].Cells["bj_standard_value"].Value = dr["standard_value"].ToString();
                dgv_bj.Rows[i].Cells["bj_unit"].Value = dr["unit"].ToString();
                dgv_bj.Rows[i].Cells["bj_sample_qty"].Value = "";
                dgv_bj.Rows[i].Cells["bj_remarks"].Value = dr["remarks"].ToString();
                dgv_bj.Rows[i].Cells["bj_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_bj.Rows[i].Cells["bj_choice_no"].Value = dr["choice_no"].ToString();
                dgv_bj.Rows[i].Cells["bj_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }
        }

        private void txt_bj_cs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_bj_cs.Text.Trim() != "")
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("code", txt_bj_cs.Text.Trim());

                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetCSDataByCode",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    txt_bj_cs.Text = "";
                    txt_bj_cs.Focus();
                }
                else
                {
                    var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                    txt_bj_cs.Text = dic["SUPPLIERS_NAME"].ToString();
                    lab_bj_cs_code.Text = dic["SUPPLIERS_CODE"].ToString();
                    lab_bj_cs_jc.Text = dic["JC"].ToString();
                }
            }
        }

        private void txt_bj_cs_DoubleClick(object sender, EventArgs e)
        {
            F_QCM_SelectCS frm = new F_QCM_SelectCS();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm.selectdic.Count > 0)
            {
                txt_bj_cs.Text = frm.selectdic["SUPPLIERS_NAME"].ToString();
                lab_bj_cs_code.Text = frm.selectdic["SUPPLIERS_CODE"].ToString();
                lab_bj_cs_jc.Text = frm.selectdic["JC"].ToString();
            }

        }

        private void ckb_bj_CheckedChanged(object sender, EventArgs e)
        {
            dgv_bj.Rows.Clear();
            if (ckb_bj_sfcc.Checked)
            {
                txt_bj_cc_task_no.Enabled = true;
                txt_bj_cc_task_no.Text = "";
                txt_bj_cc_task_no.Focus();
                txt_bj_task_no.Text = "";
                txt_bj_code.Enabled = false;
                txt_bj_code.Text = "";
                txt_bj_art.Text = "";
                txt_bj_art.Enabled = false;
                txt_bj_shose.Text = "";
                txt_cpx_model_no.Text = "";
                //cmb_bj_bjmc.DataSource = null;
                //cmb_bj_bjmc.Enabled = false;
                //cmb_bj_bjmc.Text = "";
                cmb_bj_bwmc.DataSource = null;
                cmb_bj_bwmc.Enabled = false;
                cmb_bj_bwmc.Text = "";
                txt_bj_model_no.Text = "";
                //cmb_bj_category.DataSource = null;
                //cmb_bj_category.Enabled = false;
                //cmb_bj_category.Text = "";
                tb_bj_kfxl.Text = "";
                //cmb_bj_cpjb.DataSource = null;
                tb_bj_cpjb.Text = "";
                txt_bj_jidu.Text = "";
                txt_bj_xjjb.Text = "";
                txt_bj_xb.Text = "";
                cmb_bj_jieduan.DataSource = null;
                cmb_bj_jieduan.Enabled = false;
                cmb_bj_jieduan.Text = "";
                txt_bj_scsl.Text = "";
                txt_bj_scsl.Enabled = false;
                cmb_bj_size.DataSource = null;
                cmb_bj_size.Enabled = false;
                cmb_bj_size.Text = "";
                txt_bj_po_order.Text = "";
                txt_bj_po_order.Enabled = false;
                txt_bj_po_qty.Text = "";
                cmb_bj_fgt.DataSource = null;
                cmb_bj_fgt.Enabled = false;
                cmb_bj_fgt.Text = "";
                txt_bj_cs.Text = "";
                txt_bj_cs.Enabled = false;
                lab_bj_cs_jc.Text = "";
                lab_bj_cs_code.Text = "";
                txt_bj_reasaon.Text = "";
                txt_bj_reasaon.Enabled = false;
            }
            else
            {
                txt_bj_cc_task_no.Enabled = false;
                txt_bj_cc_task_no.Text = "";
                txt_bj_task_no.Text = "";
                txt_bj_code.Enabled = true;
                txt_bj_code.Text = "";
                txt_bj_art.Text = "";
                txt_bj_art.Enabled = true;
                txt_bj_shose.Text = "";
                //cmb_bj_bjmc.DataSource = null;
                //cmb_bj_bjmc.Enabled = true;
                //cmb_bj_bjmc.Text = "";
                cmb_bj_bwmc.DataSource = null;
                cmb_bj_bwmc.Enabled = true;
                cmb_bj_bwmc.Text = "";
                txt_bj_model_no.Text = "";
                //cmb_bj_category.DataSource = null;
                //cmb_bj_category.Enabled = true;
                //cmb_bj_category.Text = "";
                //cmb_bj_category.Enabled = false;
                tb_bj_kfxl.Text = "";
                txt_bj_jidu.Text = "";
                txt_bj_xjjb.Text = "";
                txt_bj_xb.Text = "";
                cmb_bj_jieduan.DataSource = null;
                cmb_bj_jieduan.Enabled = true;
                cmb_bj_jieduan.Text = "";
                txt_bj_scsl.Text = "";
                txt_bj_scsl.Enabled = true;
                cmb_bj_size.DataSource = null;
                cmb_bj_size.Enabled = true;
                cmb_bj_size.Text = "";
                txt_bj_po_order.Text = "";
                txt_bj_po_order.Enabled = true;
                txt_bj_po_qty.Text = "";
                cmb_bj_fgt.DataSource = null;
                cmb_bj_fgt.Enabled = true;
                cmb_bj_fgt.Text = "";
                txt_bj_cs.Text = "";
                lab_bj_cs_jc.Text = "";
                lab_bj_cs_code.Text = "";
                txt_bj_cs.Enabled = true;
                txt_bj_reasaon.Text = "";
                txt_bj_reasaon.Enabled = true;
            }
        }

        private void txt_bj_cc_task_no_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", txt_bj_cc_task_no.Text.Trim());
                p.Add("test_type", "1");
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
                    MessageBox.Show(ret.ErrMsg);
                    txt_bj_task_no.Text = "";
                    txt_bj_task_no.Focus();
                    return;
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["info"].ToString());
                DataTable itemlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["itemlist"].ToString());

                txt_bj_shose.Text = info["SHOE_NO"].ToString();
                txt_bj_art.Text = info["ART_NO"].ToString();
                //cmb_bj_bjmc.Text = info["PARTS_NAME"].ToString();
                cmb_bj_bwmc.Text = info["POSITION_NAME"].ToString();

                txt_bj_model_no.Text = info["MODEL_NO"].ToString();
                //cmb_bj_category.Text = info["CATEGORY_NAME"].ToString();
                tb_bj_kfxl.Text = info["CATEGORY_CODE"].ToString();
                //cmb_bj_cpjb.Text = info["PRODUCT_LEVEL_VALUE"].ToString();
                tb_bj_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();
                txt_bj_jidu.Text = info["SEASON"].ToString();
                txt_bj_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();
                txt_bj_xb.Text = info["GENDER_NAME"].ToString();
                cmb_bj_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();
                txt_bj_scsl.Text = info["SEND_TEST_QTY"].ToString();
                cmb_bj_size.Text = info["SIZES"].ToString();
                txt_bj_po_order.Text = info["ORDER_PO"].ToString();
                txt_bj_po_qty.Text = info["ORDER_PO_QTY"].ToString();
                cmb_bj_fgt.Text = info["FGT_NAME"].ToString();
                txt_bj_cs.Text = info["MANUFACTURER_NAME"].ToString();
                lab_bj_cs_code.Text = info["MANUFACTURER_CODE"].ToString();
                lab_bj_cs_jc.Text = info["MANUFACTURER_JC"].ToString();
                txt_bj_reasaon.Text = info["TEST_REASON"].ToString();
                dgv_bj.Rows.Clear();
                foreach (DataRow dr in itemlist.Rows)
                {
                    int i = dgv_bj.Rows.Add();
                    dgv_bj.Rows[i].ReadOnly = true;
                    dgv_bj.Rows[i].Cells["bj_xh"].Value = (i + 1).ToString();
                    dgv_bj.Rows[i].Cells["bj_check"].Value = true;
                    dgv_bj.Rows[i].Cells["bj_type"].Value = dr["SOURCES"].ToString() == "0" ? "DQA测试任务" : "conventional";
                    dgv_bj.Rows[i].Cells["bj_inspection_type_name"].Value = dr["INSPECTION_TYPE_NAME"].ToString();
                    dgv_bj.Rows[i].Cells["bj_choice_name"].Value = dr["CHOICE_NAME"].ToString();
                    dgv_bj.Rows[i].Cells["bj_inspection_code"].Value = dr["INSPECTION_CODE"].ToString();
                    dgv_bj.Rows[i].Cells["bj_inspection_name"].Value = dr["INSPECTION_NAME"].ToString();
                    dgv_bj.Rows[i].Cells["bj_judgment_criteria_name"].Value = dr["JUDGMENT_CRITERIA_NAME"].ToString();
                    dgv_bj.Rows[i].Cells["bj_judge_type"].Value = dr["JUDGE_TYPE"].ToString();
                    dgv_bj.Rows[i].Cells["bj_judge_type_name"].Value = dr["JUDGE_TYPE_NAME"].ToString();
                    dgv_bj.Rows[i].Cells["bj_standard_value"].Value = dr["STANDARD_VALUE"].ToString();
                    dgv_bj.Rows[i].Cells["bj_unit"].Value = dr["UNIT"].ToString();
                    dgv_bj.Rows[i].Cells["bj_sample_qty"].Value = dr["SAMPLE_QTY"].ToString();
                    dgv_bj.Rows[i].Cells["bj_remarks"].Value = dr["ART_D_REMARK"].ToString();
                    dgv_bj.Rows[i].Cells["bj_inspection_type"].Value = dr["INSPECTION_TYPE"].ToString();
                    dgv_bj.Rows[i].Cells["bj_choice_no"].Value = dr["CHOICE_NO"].ToString();
                    dgv_bj.Rows[i].Cells["bj_judgment_criteria"].Value = dr["JUDGMENT_CRITERIA"].ToString();
                    dgv_bj.Rows[i].Cells["bj_tygs_code"].Value = dr["G_FORMULA_CODE"].ToString();
                    dgv_bj.Rows[i].Cells["bj_zdygs_code"].Value = dr["D_FORMULA_CODE"].ToString();

                    var tygs = list_tygs_data.FirstOrDefault(x => x.CODE == dr["G_FORMULA_CODE"].ToString());
                    if (tygs != null)
                        dgv_bj.Rows[i].Cells["bj_tygs"].Value = tygs.NAME;
                    var zdygs = list_zdygs_data.FirstOrDefault(x => x.CODE == dr["D_FORMULA_CODE"].ToString());
                    if (zdygs != null)
                        dgv_bj.Rows[i].Cells["bj_zdygs"].Value = zdygs.NAME;
                }

            }
        }

        #endregion

        #region 工艺
        private void Bind_gy_staff(Dictionary<string, object> result)
        {
            txt_gy_staff_no.Text = result["STAFF_NO"].ToString();
            txt_gy_staff_name.Text = result["STAFF_NAME"].ToString();
            txt_gy_staff_department.Text = result["DEPARTMENT_NAME"].ToString();
            lab_gy_staff_department_code.Text = result["DEPARTMENT_CODE"].ToString();
        }

        public void Bind_gy_info(Dictionary<string, object> result)
        {
            Dictionary<string, object> info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result["info"].ToString());
            gy_po_dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["po_info"].ToString());
            DataTable check_item = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["check_item"].ToString());

            txt_gy_shose.Text = info["SHOE_NAME"].ToString();
            //txt_gy_shose.Text = info["SHOE_NO"].ToString();
            //txt_gy_material_way_id.Text = info["MATERIAL_WAY"].ToString();
            txt_gy_cpjb.Text = info["PRODUCT_LEVEL"].ToString();
            txt_gy_jidu.Text = info["DEVELOP_SEASON"].ToString();
            //txt_gy_xb.Text = info["SEG_NO"].ToString(); ;
            //txt_gy_xjjb.Text = info["NEW_OLD_LEVEL"].ToString();

            BindDDL(cmb_gy_jieduan, list_jd_data, "gy", true);
            //Bind_size(cmb_gy_size, list_size_data, true);
            BindDDL(cmb_gy_fgt, list_fgt_data, "gy", true,true);
            BindDDL(cmb_gy_bwmc, list_position_data, "gy", true, true);
            BindDDL(cmb_gy_gymc, list_workmanship_data, "gy", true, true);
            //BindDDL(cmb_gy_category, list_category_data, "gy", true, true);
            tb_gy_kfxl.Text = info["CATEGROY_ID"].ToString();

            dgv_gy.Rows.Clear();
            foreach (DataRow dr in check_item.Rows)
            {
                int i = dgv_gy.Rows.Add();
                dgv_gy.Rows[i].Cells["gy_xh"].Value = (i + 1).ToString();
                dgv_gy.Rows[i].Cells["gy_check"].Value = true;
                dgv_gy.Rows[i].Cells["gy_type"].Value = dr["type"].ToString();
                dgv_gy.Rows[i].Cells["gy_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_choice_name"].Value = dr["choice_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_gy.Rows[i].Cells["gy_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_judge_type"].Value = dr["judge_type"].ToString();
                dgv_gy.Rows[i].Cells["gy_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_standard_value"].Value = dr["standard_value"].ToString();
                dgv_gy.Rows[i].Cells["gy_unit"].Value = dr["unit"].ToString();
                dgv_gy.Rows[i].Cells["gy_sample_qty"].Value = "";
                dgv_gy.Rows[i].Cells["gy_remarks"].Value = dr["remarks"].ToString();
                dgv_gy.Rows[i].Cells["gy_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_gy.Rows[i].Cells["gy_choice_no"].Value = dr["choice_no"].ToString();
                dgv_gy.Rows[i].Cells["gy_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }
        }

        private void txt_gy_art_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetArtInfo("gy", txt_gy_art.Text.Trim());
                if (result != null)
                {
                    Bind_gy_info(result);
                }
            }
        }

        private void txt_gy_art_DoubleClick(object sender, EventArgs e)
        {
            F_QCM_Ex_SelectART frm = new F_QCM_Ex_SelectART();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (!string.IsNullOrEmpty(frm.select_art))
            {
                txt_gy_art.Text = frm.select_art;
                var result = GetArtInfo("gy", frm.select_art);
                if (result != null)
                {
                    Bind_gy_info(result);
                }
            }
        }

        private void txt_gy_staff_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetStaffInfo(txt_gy_staff_code.Text.Trim());
                if (result != null)
                {
                    Bind_gy_staff(result);
                }
            }
        }

        public string gy_task_no = "";

        public DataTable gy_po_dt = new DataTable();

        private void btn_gy_sure_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txt_gy_staff_no.Text))
            {
                MessageBox.Show("Please scan employee number");
                txt_gy_staff_code.Focus();
                return;
            }

            if (ckb_gy_sfcc.Checked)
            {
                if (string.IsNullOrEmpty(txt_gy_art.Text.Trim()))
                {
                    MessageBox.Show("Please scan the retest lab number");
                    txt_gy_cc_task_no.Focus();
                    return;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("cc_task_no", txt_gy_cc_task_no.Text.Trim());
                data.Add("test_type", "2");
                data.Add("staff_no", txt_gy_staff_no.Text.Trim());
                data.Add("staff_name", txt_gy_staff_name.Text.Trim());
                data.Add("staff_department", txt_gy_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_gy_staff_department_code.Text.Trim());
                data.Add("manufacturer_jc", lab_gy_cs_jc.Text.Trim());
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                gy_task_no = ret.RetData;
                rahs_gy_taskno_print();
            }
            else
            {
                if (string.IsNullOrEmpty(txt_gy_art.Text))
                {
                    MessageBox.Show("Please scan or enter ART");
                    txt_gy_art.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmb_gy_gymc.Text))
                {
                    MessageBox.Show("Please select process");
                    cmb_gy_gymc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmb_gy_bwmc.Text))
                {
                    MessageBox.Show("Please select a part");
                    cmb_gy_bwmc.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(cmb_gy_category.Text))
                //{
                //    MessageBox.Show("请选择category(开发系列)");
                //    cmb_gy_category.Focus();
                //    return;
                //}


                int scsl = 0;
                int.TryParse(txt_gy_scsl.Text.Trim(), out scsl);

                if (scsl < 1)
                {
                    MessageBox.Show("Please enter a positive integer >=1 to send the test quantity");
                    txt_gy_scsl.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(lab_gy_cs_code.Text.Trim()))
                {
                    MessageBox.Show("Please enter or select a manufacturer");
                    txt_gy_cs.Focus();
                    return;
                }
                else
                {
                    //if (string.IsNullOrEmpty(lab_gy_cs_jc.Text.Trim()))
                    //{
                    //    MessageBox.Show("该厂商缩写为空,无法提交");
                    //    txt_gy_cs.Focus();
                    //    return;
                    //}
                }

                List<Dictionary<string, object>> itemlist = new List<Dictionary<string, object>>();
                foreach (DataGridViewRow dr in dgv_gy.Rows)
                {
                    if (Convert.ToBoolean(dr.Cells["gy_check"].Value))
                    {
                        int sysl = 0;
                        string qty = dr.Cells["gy_sample_qty"].Value.ToString();
                        if (string.IsNullOrWhiteSpace(qty))
                        {
                            MessageBox.Show($"Sample quantity cannot be empty, serial number{dr.Cells["gy_xh"].Value.ToString()}");
                            return;
                        }
                        int.TryParse(qty, out sysl);
                        if (sysl <= 0)
                        {
                            MessageBox.Show($"Please enter the positive integer sample quantity >= 1, serial number{dr.Cells["gy_xh"].Value.ToString()}");
                            dr.Cells["gy_sample_qty"].Selected = true;
                            return;
                        }
                        Dictionary<string, object> item = new Dictionary<string, object>();
                        item.Add("source", dr.Cells["gy_type"].Value.ToString() == "DQA测试任务" ? "0" : "1");
                        item.Add("inspection_code", dr.Cells["gy_inspection_code"].Value.ToString());
                        item.Add("inspection_name", dr.Cells["gy_inspection_name"].Value.ToString());
                        item.Add("judgment_criteria", dr.Cells["gy_judgment_criteria"].Value.ToString());
                        item.Add("standard_value", dr.Cells["gy_standard_value"].Value.ToString());
                        item.Add("unit", dr.Cells["gy_unit"].Value.ToString());
                        item.Add("sample_qty", dr.Cells["gy_sample_qty"].Value.ToString());
                        item.Add("g_formula_type", dr.Cells["gy_tygs_code"].Value == null ? "" : dr.Cells["gy_tygs_code"].Value.ToString());
                        item.Add("d_formula_type", dr.Cells["gy_zdygs_code"].Value == null ? "" : dr.Cells["gy_zdygs_code"].Value.ToString());
                        item.Add("art_d_remark", dr.Cells["gy_remarks"].Value.ToString());
                        item.Add("inspection_type", dr.Cells["gy_inspection_type"].Value.ToString());
                        item.Add("choice_name", dr.Cells["gy_choice_name"].Value.ToString());
                        item.Add("choice_no", dr.Cells["gy_choice_no"].Value.ToString());
                        item.Add("judge_type", dr.Cells["gy_judge_type"].Value.ToString());
                        itemlist.Add(item);
                    }
                }
                if (itemlist.Count <= 0)
                {
                    MessageBox.Show("No inspection items, please check");
                    return;
                }

                Dictionary<string, object> data1 = new Dictionary<string, object>();
                data1.Add("art_no", txt_gy_art.Text.Trim());
                data1.Add("test_type", "2");
                //键值对传值
                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "IsSubmit",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data1));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1.ToString());

                if (!ret1.IsSuccess)
                {
                    MessageBox.Show(ret1.ErrMsg);
                    return;
                }
                if (Convert.ToInt32(ret1.RetData) > 0)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("This ART has already been submitted for process registration, do you want to continue?", "Prompt", messButton);

                    if (dr == DialogResult.Cancel)//如果点击“取消”按钮
                    {
                        return;
                    }
                }


                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("gy_code", txt_gy_code.Text.Trim());
                data.Add("art_no", txt_gy_art.Text.Trim());
                data.Add("shoe_no", txt_gy_shose.Text.Trim());
                data.Add("shoe_name", "");
                data.Add("material_way", "");
                //data.Add("category_code", cmb_gy_category.SelectedValue == null ? "" : cmb_gy_category.SelectedValue);
                //data.Add("category_name", cmb_gy_category.Text);
                data.Add("category_code", tb_gy_kfxl.Text);
                data.Add("category_name", tb_gy_kfxl.Text);
                data.Add("product_level_code", txt_gy_cpjb.Text.Trim());
                data.Add("product_level_value", txt_gy_cpjb.Text.Trim());
                // data.Add("product_level_value", "");
                data.Add("season", txt_gy_jidu.Text.Trim());
                data.Add("pb_type_level", "");
                // data.Add("pb_type_level", "");
                data.Add("gender_name", "");
                data.Add("phase_creation_no", cmb_gy_jieduan.SelectedValue);
                data.Add("phase_creation_name", cmb_gy_jieduan.Text);
                data.Add("send_test_qty", scsl);
                data.Add("size", "");
                data.Add("order_po", "");
                data.Add("order_po_qty", "");
                data.Add("fgt_no", cmb_gy_fgt.SelectedValue);
                data.Add("fgt_name", cmb_gy_fgt.Text);
                data.Add("test_reason", txt_gy_reason.Text.Trim());
                data.Add("staff_no", txt_gy_staff_no.Text.Trim());
                data.Add("staff_name", txt_gy_staff_name.Text.Trim());
                data.Add("staff_department", txt_gy_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_gy_staff_department_code.Text.Trim());
                data.Add("task_state", 0);
                data.Add("test_type", 2);
                data.Add("position_code", cmb_gy_bwmc.SelectedValue);
                data.Add("position_name", cmb_gy_bwmc.Text);
                data.Add("workmanship_code", cmb_gy_gymc.SelectedValue);
                data.Add("workmanship_name", cmb_gy_gymc.Text);
                data.Add("manufacturer_code", lab_gy_cs_code.Text.Trim());
                data.Add("manufacturer_name", txt_gy_cs.Text);
                data.Add("manufacturer_jc", lab_gy_cs_jc.Text.Trim());
                // data.Add("test_id", txt_cl_test_id.Text.Trim());
                data.Add("itemlist", itemlist);



                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                gy_task_no = ret.RetData;
                rahs_gy_taskno_print();
            }

        }

        public void rahs_gy_taskno_print()
        {
            if (string.IsNullOrEmpty(gy_task_no))
            {
                txt_gy_task_no.Text = "";
                btn_gy_print.Enabled = false;
            }
            else
            {
                txt_gy_task_no.Text = gy_task_no;
                btn_gy_print.Enabled = true;
            }
        }

        private void txt_gy_po_order_DoubleClick(object sender, EventArgs e)
        {
            F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(gy_po_dt);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.selectlist.Count > 0)
            {
                string poorder = "";
                int total_qty = 0;
                foreach (var item in frm.selectlist)
                {
                    poorder += item["poorder"].ToString() + ",";
                    int qty = 0;
                    int.TryParse(item["qty"].ToString(), out qty);
                    total_qty += qty;
                }
                //txt_gy_po_order.Text = poorder.Trim(',');
                //txt_gy_po_qty.Text = total_qty.ToString();
            }
        }

        private void btn_gy_print_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_gy_task_no.Text.Trim()))
            {
                F_QCM_TaskNo_Print frm = new F_QCM_TaskNo_Print(txt_gy_task_no.Text.Trim());
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        public void Get_gy_checkItem(object sender, EventArgs e)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            //data.Add("CATEGORY_CODE", cmb_gy_category.SelectedIndex > 0 ? cmb_gy_category.SelectedValue : "");
            //data.Add("WORKMANSHIP_CODE", cmb_gy_gymc.SelectedIndex > 0 ? cmb_gy_gymc.SelectedValue : "");
            //data.Add("POSITION_CODE", cmb_gy_bwmc.SelectedIndex > 0 ? cmb_gy_bwmc.SelectedValue : "");
            data.Add("fgt_code", cmb_gy_fgt.SelectedIndex > 0 ? cmb_gy_fgt.SelectedValue : "");

            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Get_gy_checkItem",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return;
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());

            for (int i = dgv_gy.Rows.Count - 1; i >= 0; i--)
            {
                if (dgv_gy.Rows[i].Cells["gy_type"].Value.ToString() == "conventional")
                {
                    dgv_gy.Rows.Remove(dgv_gy.Rows[i]);
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                int i = dgv_gy.Rows.Add();
                dgv_gy.Rows[i].Cells["gy_xh"].Value = (i + 1).ToString();
                dgv_gy.Rows[i].Cells["gy_check"].Value = true;
                dgv_gy.Rows[i].Cells["gy_type"].Value = dr["type"].ToString();
                dgv_gy.Rows[i].Cells["gy_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_choice_name"].Value = dr["choice_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_gy.Rows[i].Cells["gy_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_judge_type"].Value = dr["judge_type"].ToString();
                dgv_gy.Rows[i].Cells["gy_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_gy.Rows[i].Cells["gy_standard_value"].Value = dr["standard_value"].ToString();
                dgv_gy.Rows[i].Cells["gy_unit"].Value = dr["unit"].ToString();
                dgv_gy.Rows[i].Cells["gy_sample_qty"].Value = "";
                dgv_gy.Rows[i].Cells["gy_remarks"].Value = dr["remarks"].ToString();
                dgv_gy.Rows[i].Cells["gy_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_gy.Rows[i].Cells["gy_choice_no"].Value = dr["choice_no"].ToString();
                dgv_gy.Rows[i].Cells["gy_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }
        }


        #region 表格编辑代码快
        private void dgv_gy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CB_G_formula_type != null)
            {
                CB_G_formula_type.Visible = false;
            }
            if (CB_D_formula_type != null)
            {
                CB_D_formula_type.Visible = false;
            }
            if (TXT_Unit != null)
            {
                TXT_Unit.Visible = false;
                TXT_Unit.Dispose();
            }
            if (TXT_Sample_qt != null)
            {
                TXT_Sample_qt.Visible = false;
                TXT_Sample_qt.Dispose();
            }
            if (TXT_Remarks != null)
            {
                TXT_Remarks.Visible = false;
                TXT_Remarks.Dispose();
            }
            if (e.RowIndex > -1)
            {
                if (dgv_gy.Columns[e.ColumnIndex].Name == "gy_tygs")
                {

                    CB_G_formula_type = new ComboBox();
                    CB_G_formula_type.Enabled = true;
                    CB_G_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_G_formula_type.DataSource = list_tygs_data;
                    CB_G_formula_type.DisplayMember = "NAME";
                    CB_G_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_gy.GetCellDisplayRectangle(dgv_gy.CurrentCell.ColumnIndex, dgv_gy.CurrentCell.RowIndex, false);
                    CB_G_formula_type.Left = rect.Left;
                    CB_G_formula_type.Top = rect.Top;
                    CB_G_formula_type.Width = rect.Width;
                    CB_G_formula_type.Height = rect.Height;
                    CB_G_formula_type.Visible = true;
                    dgv_gy.Controls.Add(CB_G_formula_type);
                    if (dgv_gy.Rows[e.RowIndex].Cells["gy_tygs_code"].Value != null && !string.IsNullOrEmpty(dgv_gy.Rows[e.RowIndex].Cells["gy_tygs_code"].Value.ToString()))
                    {
                        CB_G_formula_type.SelectedValue = dgv_gy.Rows[e.RowIndex].Cells["gy_tygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_G_formula_type.SelectedIndex = 0;
                    }
                    CB_G_formula_type.Focus();
                    CB_G_formula_type.SelectedIndexChanged += CB_G_formula_type_SelectedIndexChanged2;
                }
                if (dgv_gy.Columns[e.ColumnIndex].Name == "gy_zdygs")
                {

                    CB_D_formula_type = new ComboBox();
                    CB_D_formula_type.Enabled = true;
                    CB_D_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_D_formula_type.DataSource = list_zdygs_data;
                    CB_D_formula_type.DisplayMember = "NAME";
                    CB_D_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_gy.GetCellDisplayRectangle(dgv_gy.CurrentCell.ColumnIndex, dgv_gy.CurrentCell.RowIndex, false);
                    CB_D_formula_type.Left = rect.Left;
                    CB_D_formula_type.Top = rect.Top;
                    CB_D_formula_type.Width = rect.Width;
                    CB_D_formula_type.Height = rect.Height;
                    CB_D_formula_type.Visible = true;
                    dgv_gy.Controls.Add(CB_D_formula_type);
                    if (dgv_gy.Rows[e.RowIndex].Cells["gy_zdygs_code"].Value != null && !string.IsNullOrEmpty(dgv_gy.Rows[e.RowIndex].Cells["gy_zdygs_code"].Value.ToString()))
                    {
                        CB_D_formula_type.SelectedValue = dgv_gy.Rows[e.RowIndex].Cells["gy_zdygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_D_formula_type.SelectedIndex = 0;
                    }
                    CB_D_formula_type.Focus();
                    CB_D_formula_type.SelectedIndexChanged += CB_D_formula_type_SelectedIndexChanged2;
                }
                if (dgv_gy.Columns[e.ColumnIndex].Name == "gy_unit")
                {

                    TXT_Unit = new TextBox();
                    TXT_Unit.Enabled = true;

                    Rectangle rect = dgv_gy.GetCellDisplayRectangle(dgv_gy.CurrentCell.ColumnIndex, dgv_gy.CurrentCell.RowIndex, false);
                    TXT_Unit.Left = rect.Left;
                    TXT_Unit.Top = rect.Top;
                    TXT_Unit.Width = rect.Width;
                    TXT_Unit.Height = rect.Height;
                    TXT_Unit.Visible = true;
                    dgv_gy.Controls.Add(TXT_Unit);
                    if (dgv_gy.Rows[e.RowIndex].Cells["gy_unit"].Value != null && !string.IsNullOrEmpty(dgv_gy.Rows[e.RowIndex].Cells["gy_unit"].Value.ToString()))
                    {
                        TXT_Unit.Text = dgv_gy.Rows[e.RowIndex].Cells["gy_unit"].Value.ToString();
                    }
                    TXT_Unit.Focus();
                    TXT_Unit.SelectionStart = TXT_Unit.Text.Length;
                    TXT_Unit.TextChanged += TXT_Unit_TextChanged2;
                }
                if (dgv_gy.Columns[e.ColumnIndex].Name == "gy_sample_qty")
                {

                    TXT_Sample_qt = new TextBox();
                    TXT_Sample_qt.Enabled = true;

                    Rectangle rect = dgv_gy.GetCellDisplayRectangle(dgv_gy.CurrentCell.ColumnIndex, dgv_gy.CurrentCell.RowIndex, false);
                    TXT_Sample_qt.Left = rect.Left;
                    TXT_Sample_qt.Top = rect.Top;
                    TXT_Sample_qt.Width = rect.Width;
                    TXT_Sample_qt.Height = rect.Height;
                    TXT_Sample_qt.Visible = true;
                    dgv_gy.Controls.Add(TXT_Sample_qt);
                    if (dgv_gy.Rows[e.RowIndex].Cells["gy_sample_qty"].Value != null && !string.IsNullOrEmpty(dgv_gy.Rows[e.RowIndex].Cells["gy_sample_qty"].Value.ToString()))
                    {
                        TXT_Sample_qt.Text = dgv_gy.Rows[e.RowIndex].Cells["gy_sample_qty"].Value.ToString();
                    }
                    TXT_Sample_qt.Focus();
                    TXT_Sample_qt.SelectionStart = TXT_Sample_qt.Text.Length;
                    TXT_Sample_qt.TextChanged += TXT_Sample_qt_TextChanged2;
                }
                if (dgv_gy.Columns[e.ColumnIndex].Name == "gy_remarks")
                {

                    TXT_Remarks = new TextBox();
                    TXT_Remarks.Enabled = true;

                    Rectangle rect = dgv_gy.GetCellDisplayRectangle(dgv_gy.CurrentCell.ColumnIndex, dgv_gy.CurrentCell.RowIndex, false);
                    TXT_Remarks.Left = rect.Left;
                    TXT_Remarks.Top = rect.Top;
                    TXT_Remarks.Width = rect.Width;
                    TXT_Remarks.Height = rect.Height;
                    TXT_Remarks.Visible = true;
                    dgv_gy.Controls.Add(TXT_Remarks);
                    if (dgv_gy.Rows[e.RowIndex].Cells["gy_remarks"].Value != null && !string.IsNullOrEmpty(dgv_gy.Rows[e.RowIndex].Cells["gy_remarks"].Value.ToString()))
                    {
                        TXT_Remarks.Text = dgv_gy.Rows[e.RowIndex].Cells["gy_remarks"].Value.ToString();
                    }
                    TXT_Remarks.Focus();
                    TXT_Remarks.SelectionStart = TXT_Remarks.Text.Length;
                    TXT_Remarks.TextChanged += TXT_Remarks_TextChanged2;
                }
            }
        }
        private void CB_G_formula_type_SelectedIndexChanged2(object sender, EventArgs e)
        {
            dgv_gy.CurrentCell.Value = CB_G_formula_type.Text;
            dgv_gy.Rows[dgv_gy.CurrentCell.RowIndex].Cells["gy_tygs_code"].Value = CB_G_formula_type.SelectedValue;
            CB_G_formula_type.Visible = false;
            CB_G_formula_type.Dispose();
        }
        private void CB_D_formula_type_SelectedIndexChanged2(object sender, EventArgs e)
        {
            dgv_gy.CurrentCell.Value = CB_D_formula_type.Text;
            dgv_gy.Rows[dgv_gy.CurrentCell.RowIndex].Cells["gy_zdygs_code"].Value = CB_D_formula_type.SelectedValue;
            CB_D_formula_type.Visible = false;
            CB_D_formula_type.Dispose();
        }
        private void TXT_Unit_TextChanged2(object sender, EventArgs e)
        {
            dgv_gy.CurrentCell.Value = TXT_Unit.Text;
        }
        private void TXT_Sample_qt_TextChanged2(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(TXT_Sample_qt.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(TXT_Sample_qt.Text))
            {
                MessageBox.Show("Please enter a positive integer");
                TXT_Sample_qt.Text = "";
                return;
            }
            dgv_gy.CurrentCell.Value = TXT_Sample_qt.Text;
        }
        private void TXT_Remarks_TextChanged2(object sender, EventArgs e)
        {
            dgv_gy.CurrentCell.Value = TXT_Remarks.Text;
        }
        #endregion


        private void txt_gy_scsl_TextChanged(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(txt_gy_scsl.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(txt_gy_scsl.Text))
            {
                MessageBox.Show("Please enter a positive integer");
                txt_gy_scsl.Text = "";
                return;
            }
        }

        private void txt_gy_cs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_gy_cs.Text.Trim() != "")
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("code", txt_gy_cs.Text.Trim());

                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetCSDataByCode",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    txt_gy_cs.Text = "";
                    txt_gy_cs.Focus();
                }
                else
                {
                    var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                    txt_gy_cs.Text = dic["SUPPLIERS_NAME"].ToString();
                    lab_gy_cs_code.Text = dic["SUPPLIERS_CODE"].ToString();
                    lab_gy_cs_jc.Text = dic["JC"].ToString();
                }
            }
        }

        private void txt_gy_cs_DoubleClick(object sender, EventArgs e)
        {
            F_QCM_SelectCS frm = new F_QCM_SelectCS();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm.selectdic.Count > 0)
            {
                txt_gy_cs.Text = frm.selectdic["SUPPLIERS_NAME"].ToString();
                lab_gy_cs_code.Text = frm.selectdic["SUPPLIERS_CODE"].ToString();
                lab_gy_cs_jc.Text = frm.selectdic["JC"].ToString();
            }
        }

        #endregion

        #region 材料

        private void Bind_cl_staff(Dictionary<string, object> result)
        {
            txt_cl_staff_no.Text = result["STAFF_NO"].ToString();
            txt_cl_staff_name.Text = result["STAFF_NAME"].ToString();
            txt_cl_staff_department.Text = result["DEPARTMENT_NAME"].ToString();
            lab_cl_staff_department_code.Text = result["DEPARTMENT_CODE"].ToString();
        }

        public void Bind_cl_info(Dictionary<string, object> result)
        {
            Dictionary<string, object> info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result["info"].ToString());
            cl_po_dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["po_info"].ToString());
            DataTable check_item = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["check_item"].ToString());

            txt_cl_shose.Text = info["SHOE_NAME"].ToString();
            //txt_cl_shose.Text = info["SHOE_NO"].ToString();
            txt_cl_cpjb.Text = info["PRODUCTION_LEVEL"].ToString();
            txt_cl_jd.Text = info["DEVELOP_SEASON"].ToString();
            txt_cl_xjjb.Text = info["NEW_OLD_LEVEL"].ToString();

            BindDDL(cmb_cl_jieduan, list_jd_data, "cl", true);
            BindDDL(cmb_cl_fgt, list_fgt_data, "cl", true);
            BindDDL(cmb_cl_bwmc, list_position_data, "cl", true, true);
            BindDDL(cmb_cl_clzl, list_materialtype_data, "cl", true, true);
            BindDDL(cmb_cl_category, list_category_data, "cl", true, true);
            Bind_size(cmb_cl_size, list_size_data, true);

            dgv_cl.Rows.Clear();
            foreach (DataRow dr in check_item.Rows)
            {
                int i = dgv_cl.Rows.Add();
                dgv_cl.Rows[i].Cells["cl_xh"].Value = (i + 1).ToString();
                dgv_cl.Rows[i].Cells["cl_check"].Value = true;
                dgv_cl.Rows[i].Cells["cl_type"].Value = dr["type"].ToString();
                dgv_cl.Rows[i].Cells["cl_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_choice_name"].Value = dr["choice_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_cl.Rows[i].Cells["cl_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_judge_type"].Value = dr["judge_type"].ToString();
                dgv_cl.Rows[i].Cells["cl_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_standard_value"].Value = dr["standard_value"].ToString();
                dgv_cl.Rows[i].Cells["cl_unit"].Value = dr["unit"].ToString();
                dgv_cl.Rows[i].Cells["cl_sample_qty"].Value = "";

                dgv_cl.Rows[i].Cells["cl_remarks"].Value = dr["remarks"].ToString();
                dgv_cl.Rows[i].Cells["cl_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_cl.Rows[i].Cells["cl_choice_no"].Value = dr["choice_no"].ToString();
                dgv_cl.Rows[i].Cells["cl_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }
        }

        private void txt_cl_staff_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetStaffInfo(txt_cl_staff_code.Text.Trim());
                if (result != null)
                {
                    Bind_cl_staff(result);
                }
            }
        }

        public string cl_task_no = "";

        public DataTable cl_po_dt = new DataTable();

        private void txt_cl_po_order_DoubleClick(object sender, EventArgs e)
        {
            //F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(cl_po_dt);
            F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(txt_cl_art.Text, txt_cl_po_order.Text);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.selectlist.Count > 0)
            {
                string poorder = "";
                int total_qty = 0;
                foreach (var item in frm.selectlist)
                {
                    poorder += item["poorder"].ToString() + ",";
                    int qty = 0;
                    int.TryParse(item["qty"].ToString(), out qty);
                    total_qty += qty;
                }
                txt_cl_po_order.Text = poorder.Trim(',');
                txt_cl_po_qty.Text = total_qty.ToString();
            }
        }

        public void rahs_cl_taskno_print()
        {
            if (string.IsNullOrEmpty(cl_task_no))
            {
                txt_cl_task_no.Text = "";
                btn_cl_print.Enabled = false;
            }
            else
            {
                txt_cl_task_no.Text = cl_task_no;
                btn_cl_print.Enabled = true;
            }
        }

        private void btn_cl_sure_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(txt_cl_staff_no.Text))
            {
                MessageBox.Show("Please scan employee number");
                txt_cl_staff_code.Focus();
                return;
            }

            if (ckb_cl_sfcc.Checked)
            {
                if (string.IsNullOrEmpty(txt_cl_art.Text.Trim()))
                {
                    MessageBox.Show("Please scan the retest lab number");
                    txt_cl_cc_task_no.Focus();
                    return;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("makings_id", txt_cl_clid.Text.Trim());
                data.Add("material_code", txt_cl_clid.Text.Trim());
                data.Add("cc_task_no", txt_cl_cc_task_no.Text.Trim());
                data.Add("test_type", "3");
                data.Add("staff_no", txt_cl_staff_no.Text.Trim());
                data.Add("staff_name", txt_cl_staff_name.Text.Trim());
                data.Add("staff_department", txt_cl_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_cl_staff_department_code.Text.Trim());
                data.Add("manufacturer_jc", lab_cl_cs_jc.Text.Trim());
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                cl_task_no = ret.RetData;
                rahs_cl_taskno_print();
            }
            else
            {
                if (string.IsNullOrEmpty(txt_cl_art.Text))
                {
                    MessageBox.Show("Please scan or enter ART");
                    txt_cl_art.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmb_cl_clzl.Text))
                {
                    MessageBox.Show("Please select material type");
                    cmb_cl_clzl.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmb_cl_bwmc.Text))
                {
                    MessageBox.Show("Please select a part");
                    cmb_cl_bwmc.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(cmb_cl_category.Text))
                {
                    MessageBox.Show("Please select category (development series)");
                    cmb_cl_category.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txt_cl_po_order.Text.Trim()))
                {
                    MessageBox.Show("Please select data or select PO order");
                    txt_cl_po_order.Focus();
                    return;
                }


                int scsl = 0;
                int.TryParse(txt_cl_scsl.Text.Trim(), out scsl);

                if (scsl < 1)
                {
                    MessageBox.Show("Please enter a positive integer >=1 to send the test quantity");
                    txt_cl_scsl.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lab_cl_cs_code.Text.Trim()))
                {
                    MessageBox.Show("Please enter or select a manufacturer");
                    txt_cl_cs.Focus();
                    return;
                }
                else
                {
                    //if (string.IsNullOrEmpty(lab_cl_cs_jc.Text.Trim()))
                    //{
                    //    MessageBox.Show("该厂商缩写为空,无法提交");
                    //    txt_cl_cs.Focus();
                    //    return;
                    //}
                }


                List<Dictionary<string, object>> itemlist = new List<Dictionary<string, object>>();
                foreach (DataGridViewRow dr in dgv_cl.Rows)
                {
                    if (Convert.ToBoolean(dr.Cells["cl_check"].Value))
                    {
                        int sysl = 0;
                        string qty = dr.Cells["cl_sample_qty"].Value.ToString();
                        if (string.IsNullOrWhiteSpace(qty))
                        {
                            MessageBox.Show($"Sample quantity cannot be empty, serial number{dr.Cells["cl_xh"].Value.ToString()}");
                            return;
                        }
                        int.TryParse(qty, out sysl);
                        if (sysl <= 0)
                        {
                            MessageBox.Show($"Please enter the positive integer sample quantity >= 1, serial number{dr.Cells["cl_xh"].Value.ToString()}");
                            dr.Cells["cl_sample_qty"].Selected = true;
                            return;
                        }

                        Dictionary<string, object> item = new Dictionary<string, object>();
                        item.Add("source", dr.Cells["cl_type"].Value.ToString() == "DQA测试任务" ? "0" : "1");
                        item.Add("inspection_code", dr.Cells["cl_inspection_code"].Value.ToString());
                        item.Add("inspection_name", dr.Cells["cl_inspection_name"].Value.ToString());
                        item.Add("judgment_criteria", dr.Cells["cl_judgment_criteria"].Value.ToString());
                        item.Add("standard_value", dr.Cells["cl_standard_value"].Value.ToString());
                        item.Add("unit", dr.Cells["cl_unit"].Value.ToString());
                        item.Add("sample_qty", dr.Cells["cl_sample_qty"].Value.ToString());
                        item.Add("g_formula_type", dr.Cells["cl_tygs_code"].Value == null ? "" : dr.Cells["cl_tygs_code"].Value.ToString());
                        item.Add("d_formula_type", dr.Cells["cl_zdygs_code"].Value == null ? "" : dr.Cells["cl_zdygs_code"].Value.ToString());
                        item.Add("art_d_remark", dr.Cells["cl_remarks"].Value.ToString());
                        item.Add("inspection_type", dr.Cells["cl_inspection_type"].Value.ToString());
                        item.Add("choice_name", dr.Cells["cl_choice_name"].Value.ToString());
                        item.Add("choice_no", dr.Cells["cl_choice_no"].Value.ToString());
                        item.Add("judge_type", dr.Cells["cl_judge_type"].Value.ToString());
                        itemlist.Add(item);
                    }
                }

                Dictionary<string, object> data1 = new Dictionary<string, object>();
                data1.Add("art_no", txt_cl_art.Text.Trim());
                data1.Add("test_type", "3");
                //键值对传值
                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "IsSubmit",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data1));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1.ToString());

                if (!ret1.IsSuccess)
                {
                    MessageBox.Show(ret1.ErrMsg);
                    return;
                }
                if (Convert.ToInt32(ret1.RetData) > 0)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("This ART has already submitted materials for testing and registration, do you want to continue?", "Prompt", messButton);

                    if (dr == DialogResult.Cancel)//如果点击“取消”按钮
                    {
                        return;
                    }
                }


                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("cl_code", txt_cl_qrcode.Text.Trim());
                data.Add("art_no", txt_cl_art.Text.Trim());
                data.Add("shoe_no", txt_cl_shose.Text.Trim());
                //data.Add("shoe_name", "");
                //  data.Add("material_way", txt_cl_material_way_id.Text.Trim());
                data.Add("category_code", cmb_cl_category.SelectedValue == null ? "" : cmb_cl_category.SelectedValue);
                data.Add("category_name", cmb_cl_category.Text);
                data.Add("product_level_code", txt_cl_cpjb.Text.Trim());
                data.Add("product_level_value", txt_cl_cpjb.Text.Trim());
                data.Add("season", txt_cl_jd.Text.Trim());
                data.Add("pb_type_code", txt_cl_xjjb.Text.Trim());
                // data.Add("pb_type_level", "");
                //data.Add("gender", txt_cl_xb.Text.Trim());
                data.Add("phase_creation_no", cmb_cl_jieduan.SelectedValue == null ? "" : cmb_cl_jieduan.SelectedValue);
                data.Add("phase_creation_name", cmb_cl_jieduan.Text);
                data.Add("send_test_qty", scsl);
                data.Add("size", cmb_cl_size.Text);
                data.Add("order_po", txt_cl_po_order.Text.Trim());
                data.Add("order_po_qty", txt_cl_po_qty.Text.Trim());
                data.Add("fgt_no", cmb_cl_fgt.SelectedValue == null ? "" : cmb_cl_fgt.SelectedValue);
                data.Add("fgt_name", cmb_cl_fgt.Text);
                data.Add("makings_type_code", cmb_cl_clzl.SelectedValue == null ? "" : cmb_cl_clzl.SelectedValue);
                data.Add("makings_type_name", cmb_cl_clzl.Text);
                data.Add("test_reason", txt_cl_reason.Text.Trim());
                data.Add("staff_no", txt_cl_staff_no.Text.Trim());
                data.Add("staff_name", txt_cl_staff_name.Text.Trim());
                data.Add("staff_department", txt_cl_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_cl_staff_department_code.Text.Trim());
                data.Add("task_state", 0);
                data.Add("test_type", 3);
                data.Add("position_code", cmb_cl_bwmc.SelectedValue == null ? "" : cmb_cl_bwmc.SelectedValue);
                data.Add("position_name", cmb_cl_bwmc.Text);
                // data.Add("manufacturer_code", cmb_gy_gymc.SelectedValue);
                // data.Add("manufacturer_name", cmb_gy_gymc.Text);
                data.Add("manufacturer_code", lab_cl_cs_code.Text.Trim());
                data.Add("manufacturer_name", txt_cl_cs.Text);
                data.Add("manufacturer_jc", lab_cl_cs_jc.Text.Trim());
                data.Add("test_id", txt_cl_test_id.Text.Trim());
                data.Add("itemlist", itemlist);
                data.Add("sldh", txt_cl_qrcode.Text.Trim());
                data.Add("makings_id", txt_cl_clid.Text.Trim());//料号
                data.Add("material_code", txt_cl_clid.Text.Trim());//料号



                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                cl_task_no = ret.RetData;
                rahs_cl_taskno_print();
            }


        }

        private void txt_cl_art_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetArtInfo("cl", txt_cl_art.Text.Trim());
                if (result != null)
                {
                    Bind_cl_info(result);
                }
            }
        }

        private void btn_cl_print_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_cl_task_no.Text.Trim()))
            {
                F_QCM_TaskNo_Print frm = new F_QCM_TaskNo_Print(txt_cl_task_no.Text.Trim());
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        #region 表格编辑代码快
        private void dgv_cl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CB_G_formula_type != null)
            {
                CB_G_formula_type.Visible = false;
            }
            if (CB_D_formula_type != null)
            {
                CB_D_formula_type.Visible = false;
            }
            if (TXT_Unit != null)
            {
                TXT_Unit.Visible = false;
                TXT_Unit.Dispose();
            }
            if (TXT_Sample_qt != null)
            {
                TXT_Sample_qt.Visible = false;
                TXT_Sample_qt.Dispose();
            }
            if (TXT_Remarks != null)
            {
                TXT_Remarks.Visible = false;
                TXT_Remarks.Dispose();
            }
            if (e.RowIndex > -1)
            {
                if (dgv_cl.Columns[e.ColumnIndex].Name == "cl_tygs")
                {

                    CB_G_formula_type = new ComboBox();
                    CB_G_formula_type.Enabled = true;
                    CB_G_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_G_formula_type.DataSource = list_tygs_data;
                    CB_G_formula_type.DisplayMember = "NAME";
                    CB_G_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_cl.GetCellDisplayRectangle(dgv_cl.CurrentCell.ColumnIndex, dgv_cl.CurrentCell.RowIndex, false);
                    CB_G_formula_type.Left = rect.Left;
                    CB_G_formula_type.Top = rect.Top;
                    CB_G_formula_type.Width = rect.Width;
                    CB_G_formula_type.Height = rect.Height;
                    CB_G_formula_type.Visible = true;
                    dgv_cl.Controls.Add(CB_G_formula_type);
                    if (dgv_cl.Rows[e.RowIndex].Cells["cl_tygs_code"].Value != null && !string.IsNullOrEmpty(dgv_cl.Rows[e.RowIndex].Cells["cl_tygs_code"].Value.ToString()))
                    {
                        CB_G_formula_type.SelectedValue = dgv_cl.Rows[e.RowIndex].Cells["cl_tygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_G_formula_type.SelectedIndex = 0;
                    }
                    CB_G_formula_type.Focus();
                    CB_G_formula_type.SelectedIndexChanged += CB_G_formula_type_SelectedIndexChanged3;
                }
                if (dgv_cl.Columns[e.ColumnIndex].Name == "cl_zdygs")
                {

                    CB_D_formula_type = new ComboBox();
                    CB_D_formula_type.Enabled = true;
                    CB_D_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_D_formula_type.DataSource = list_zdygs_data;
                    CB_D_formula_type.DisplayMember = "NAME";
                    CB_D_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_cl.GetCellDisplayRectangle(dgv_cl.CurrentCell.ColumnIndex, dgv_cl.CurrentCell.RowIndex, false);
                    CB_D_formula_type.Left = rect.Left;
                    CB_D_formula_type.Top = rect.Top;
                    CB_D_formula_type.Width = rect.Width;
                    CB_D_formula_type.Height = rect.Height;
                    CB_D_formula_type.Visible = true;
                    dgv_cl.Controls.Add(CB_D_formula_type);
                    if (dgv_cl.Rows[e.RowIndex].Cells["cl_zdygs_code"].Value != null && !string.IsNullOrEmpty(dgv_cl.Rows[e.RowIndex].Cells["cl_zdygs_code"].Value.ToString()))
                    {
                        CB_D_formula_type.SelectedValue = dgv_cl.Rows[e.RowIndex].Cells["cl_zdygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_D_formula_type.SelectedIndex = 0;
                    }
                    CB_D_formula_type.Focus();
                    CB_D_formula_type.SelectedIndexChanged += CB_D_formula_type_SelectedIndexChanged3;
                }
                if (dgv_cl.Columns[e.ColumnIndex].Name == "cl_unit")
                {

                    TXT_Unit = new TextBox();
                    TXT_Unit.Enabled = true;

                    Rectangle rect = dgv_cl.GetCellDisplayRectangle(dgv_cl.CurrentCell.ColumnIndex, dgv_cl.CurrentCell.RowIndex, false);
                    TXT_Unit.Left = rect.Left;
                    TXT_Unit.Top = rect.Top;
                    TXT_Unit.Width = rect.Width;
                    TXT_Unit.Height = rect.Height;
                    TXT_Unit.Visible = true;
                    dgv_cl.Controls.Add(TXT_Unit);
                    if (dgv_cl.Rows[e.RowIndex].Cells["cl_unit"].Value != null && !string.IsNullOrEmpty(dgv_cl.Rows[e.RowIndex].Cells["cl_unit"].Value.ToString()))
                    {
                        TXT_Unit.Text = dgv_cl.Rows[e.RowIndex].Cells["cl_unit"].Value.ToString();
                    }
                    TXT_Unit.Focus();
                    TXT_Unit.SelectionStart = TXT_Unit.Text.Length;
                    TXT_Unit.TextChanged += TXT_Unit_TextChanged3;
                }
                if (dgv_cl.Columns[e.ColumnIndex].Name == "cl_sample_qty")
                {

                    TXT_Sample_qt = new TextBox();
                    TXT_Sample_qt.Enabled = true;

                    Rectangle rect = dgv_cl.GetCellDisplayRectangle(dgv_cl.CurrentCell.ColumnIndex, dgv_cl.CurrentCell.RowIndex, false);
                    TXT_Sample_qt.Left = rect.Left;
                    TXT_Sample_qt.Top = rect.Top;
                    TXT_Sample_qt.Width = rect.Width;
                    TXT_Sample_qt.Height = rect.Height;
                    TXT_Sample_qt.Visible = true;
                    dgv_cl.Controls.Add(TXT_Sample_qt);
                    if (dgv_cl.Rows[e.RowIndex].Cells["cl_sample_qty"].Value != null && !string.IsNullOrEmpty(dgv_cl.Rows[e.RowIndex].Cells["cl_sample_qty"].Value.ToString()))
                    {
                        TXT_Sample_qt.Text = dgv_cl.Rows[e.RowIndex].Cells["cl_sample_qty"].Value.ToString();
                    }
                    TXT_Sample_qt.Focus();
                    TXT_Sample_qt.SelectionStart = TXT_Sample_qt.Text.Length;
                    TXT_Sample_qt.TextChanged += TXT_Sample_qt_TextChanged3;
                }
                if (dgv_cl.Columns[e.ColumnIndex].Name == "cl_remarks")
                {

                    TXT_Remarks = new TextBox();
                    TXT_Remarks.Enabled = true;

                    Rectangle rect = dgv_cl.GetCellDisplayRectangle(dgv_cl.CurrentCell.ColumnIndex, dgv_cl.CurrentCell.RowIndex, false);
                    TXT_Remarks.Left = rect.Left;
                    TXT_Remarks.Top = rect.Top;
                    TXT_Remarks.Width = rect.Width;
                    TXT_Remarks.Height = rect.Height;
                    TXT_Remarks.Visible = true;
                    dgv_cl.Controls.Add(TXT_Remarks);
                    if (dgv_cl.Rows[e.RowIndex].Cells["cl_remarks"].Value != null && !string.IsNullOrEmpty(dgv_cl.Rows[e.RowIndex].Cells["cl_remarks"].Value.ToString()))
                    {
                        TXT_Remarks.Text = dgv_cl.Rows[e.RowIndex].Cells["cl_remarks"].Value.ToString();
                    }
                    TXT_Remarks.Focus();
                    TXT_Remarks.SelectionStart = TXT_Remarks.Text.Length;
                    TXT_Remarks.TextChanged += TXT_Remarks_TextChanged3;
                }
            }
        }
        private void CB_G_formula_type_SelectedIndexChanged3(object sender, EventArgs e)
        {
            dgv_cl.CurrentCell.Value = CB_G_formula_type.Text;
            dgv_cl.Rows[dgv_cl.CurrentCell.RowIndex].Cells["cl_tygs_code"].Value = CB_G_formula_type.SelectedValue;
            CB_G_formula_type.Visible = false;
            CB_G_formula_type.Dispose();
        }
        private void CB_D_formula_type_SelectedIndexChanged3(object sender, EventArgs e)
        {
            dgv_cl.CurrentCell.Value = CB_D_formula_type.Text;
            dgv_cl.Rows[dgv_cl.CurrentCell.RowIndex].Cells["cl_zdygs_code"].Value = CB_D_formula_type.SelectedValue;
            CB_D_formula_type.Visible = false;
            CB_D_formula_type.Dispose();
        }
        private void TXT_Unit_TextChanged3(object sender, EventArgs e)
        {
            dgv_cl.CurrentCell.Value = TXT_Unit.Text;
        }
        private void TXT_Sample_qt_TextChanged3(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(TXT_Sample_qt.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(TXT_Sample_qt.Text))
            {
                MessageBox.Show("请输入正整数");
                TXT_Sample_qt.Text = "";
                return;
            }
            dgv_cl.CurrentCell.Value = TXT_Sample_qt.Text;
        }
        private void TXT_Remarks_TextChanged3(object sender, EventArgs e)
        {
            dgv_cl.CurrentCell.Value = TXT_Remarks.Text;
        }
        #endregion


        public void Get_cl_checkItem(object sender, EventArgs e)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("CATEGORY_CODE", cmb_cl_category.SelectedIndex > 0 ? cmb_cl_category.SelectedValue : "");
            data.Add("MATERIAL_TYPE_CODE", cmb_cl_clzl.SelectedIndex > 0 ? cmb_cl_clzl.SelectedValue : "");
            data.Add("POSITION_CODE", cmb_cl_bwmc.SelectedIndex > 0 ? cmb_cl_bwmc.SelectedValue : "");
            //键值对传值
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Get_cl_checkItem",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
                return;
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());

            for (int i = dgv_cl.Rows.Count - 1; i >= 0; i--)
            {
                if (dgv_cl.Rows[i].Cells["cl_type"].Value.ToString() == "conventional")
                {
                    dgv_cl.Rows.Remove(dgv_cl.Rows[i]);
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                int i = dgv_cl.Rows.Add();
                dgv_cl.Rows[i].Cells["cl_xh"].Value = (i + 1).ToString();
                dgv_cl.Rows[i].Cells["cl_check"].Value = true;
                dgv_cl.Rows[i].Cells["cl_type"].Value = dr["type"].ToString();
                dgv_cl.Rows[i].Cells["cl_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_choice_name"].Value = dr["choice_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_cl.Rows[i].Cells["cl_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_judge_type"].Value = dr["judge_type"].ToString();
                dgv_cl.Rows[i].Cells["cl_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_cl.Rows[i].Cells["cl_standard_value"].Value = dr["standard_value"].ToString();
                dgv_cl.Rows[i].Cells["cl_unit"].Value = dr["unit"].ToString();
                dgv_cl.Rows[i].Cells["cl_sample_qty"].Value = "";

                dgv_cl.Rows[i].Cells["cl_remarks"].Value = dr["remarks"].ToString();
                dgv_cl.Rows[i].Cells["cl_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_cl.Rows[i].Cells["cl_choice_no"].Value = dr["choice_no"].ToString();
                dgv_cl.Rows[i].Cells["cl_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }
        }

        private void txt_cl_scsl_TextChanged(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(txt_cl_scsl.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(txt_cl_scsl.Text))
            {
                MessageBox.Show("Please enter a positive integer");
                txt_cl_scsl.Text = "";
                return;
            }
        }

        private void txt_cl_cs_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_cl_cs.Text.Trim() != "")
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("code", txt_cl_cs.Text.Trim());

                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetCSDataByCode",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    txt_cl_cs.Text = "";
                    txt_cl_cs.Focus();
                }
                else
                {
                    var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                    txt_cl_cs.Text = dic["SUPPLIERS_NAME"].ToString();
                    lab_cl_cs_code.Text = dic["SUPPLIERS_CODE"].ToString();
                    lab_cl_cs_jc.Text = dic["JC"].ToString();
                }
            }
        }

        private void txt_cl_cs_DoubleClick(object sender, EventArgs e)
        {
            F_QCM_SelectCS frm = new F_QCM_SelectCS();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm.selectdic.Count > 0)
            {
                txt_cl_cs.Text = frm.selectdic["SUPPLIERS_NAME"].ToString();
                lab_cl_cs_code.Text = frm.selectdic["SUPPLIERS_CODE"].ToString();
                lab_cl_cs_jc.Text = frm.selectdic["JC"].ToString();
            }
        }


        #endregion

        #region 量产拉力

        private void Bind_lcll_staff(Dictionary<string, object> result)
        {
            txt_lcll_staff_no.Text = result["STAFF_NO"].ToString();
            txt_lcll_staff_name.Text = result["STAFF_NAME"].ToString();
            txt_lcll_staff_department.Text = result["DEPARTMENT_NAME"].ToString();
            lab_lcll_staff_department_code.Text = result["DEPARTMENT_CODE"].ToString();
        }

        public string lcll_task_no = "";

        public DataTable lcll_po_dt = new DataTable();

        public void Bind_lcll_info(Dictionary<string, object> result)
        {
            Dictionary<string, object> info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(result["info"].ToString());
            lcll_po_dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["po_info"].ToString());
            DataTable check_item = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(result["check_item"].ToString());

            txt_lcll_shose.Text = info["SHOE_NAME"].ToString();
            //txt_lcll_shose.Text = info["SHOE_NO"].ToString();
            //txt_lcll_category.Text = info["CATEGROY"].ToString();
            txt_lcll_category.Text = info["CATEGROY_ID"].ToString();
            txt_lcll_cpjb.Text = info["PRODUCT_LEVEL"].ToString();
            txt_lcll_jd.Text = info["DEVELOP_SEASON"].ToString();
            txt_lcll_art.Text = info["PROD_NO"].ToString();


            BindDDL(cmb_lcll_jieduan, list_jd_data, "lcll", true);
            Bind_size(cmb_lcll_size, list_size_data, true);
            //BindDDL(cmb_lcll_fgt, list_fgt_data, "lcll", true);
            BindDDL(cmb_lcll_line, list_line_data, "lcll", true);

            dgv_lcll.Rows.Clear();
            foreach (DataRow dr in check_item.Rows)
            {
                int i = dgv_lcll.Rows.Add();
                dgv_lcll.Rows[i].Cells["lcll_xh"].Value = (i + 1).ToString();
                dgv_lcll.Rows[i].Cells["lcll_check"].Value = true;
                dgv_lcll.Rows[i].Cells["lcll_type"].Value = dr["type"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_inspection_type_name"].Value = dr["inspection_type_name"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_choice_name"].Value = dr["choice_name"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_inspection_code"].Value = dr["inspection_code"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_inspection_name"].Value = dr["inspection_name"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_judgment_criteria_name"].Value = dr["judgment_criteria_name"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_judge_type"].Value = dr["judge_type"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_judge_type_name"].Value = dr["judge_type_name"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_standard_value"].Value = dr["standard_value"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_unit"].Value = dr["unit"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_sample_qty"].Value = "";
                dgv_lcll.Rows[i].Cells["lcll_remarks"].Value = dr["remarks"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_inspection_type"].Value = dr["inspection_type"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_choice_no"].Value = dr["choice_no"].ToString();
                dgv_lcll.Rows[i].Cells["lcll_judgment_criteria"].Value = dr["judgment_criteria"].ToString();
            }
        }

        private void txt_lcll_staff_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetStaffInfo(txt_lcll_staff_code.Text.Trim());
                if (result != null)
                {
                    Bind_lcll_staff(result);
                }
            }
        }

        private void txt_lcll_po_order_DoubleClick(object sender, EventArgs e)
        {
            //F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(lcll_po_dt);
            F_QCM_Ex_PoOrder frm = new F_QCM_Ex_PoOrder(txt_lcll_art.Text, txt_lcll_po_order.Text);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            if (frm.selectlist.Count > 0)
            {
                string poorder = "";
                int total_qty = 0;
                foreach (var item in frm.selectlist)
                {
                    poorder += item["poorder"].ToString() + ",";
                    int qty = 0;
                    int.TryParse(item["qty"].ToString(), out qty);
                    total_qty += qty;
                }
                txt_lcll_po_order.Text = poorder.Trim(',');
                txt_lcll_po_qty.Text = total_qty.ToString();
            }
        }

        public void rahs_lcll_taskno_print()
        {
            if (string.IsNullOrEmpty(lcll_task_no))
            {
                txt_lcll_task_no.Text = "";
                btn_lcll_print.Enabled = false;
            }
            else
            {
                txt_lcll_task_no.Text = lcll_task_no;
                btn_lcll_print.Enabled = true;
            }
        }

        private void btn_lcll_sure_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_lcll_staff_no.Text))
            {
                MessageBox.Show("Please scan employee number");
                txt_lcll_staff_code.Focus();
                return;
            }
            else
            {
                //if (string.IsNullOrEmpty(lab_lcll_staff_department_code.Text.Trim()))
                //{
                //    MessageBox.Show("该员工未绑定部门,无法提交");
                //    txt_lcll_staff_code.Focus();
                //    return;
                //}
            }

            if (ckb_lcll_sfcc.Checked)
            {
                if (string.IsNullOrEmpty(txt_lcll_art.Text.Trim()))
                {
                    MessageBox.Show("Please scan the retest lab number");
                    txt_lcll_cc_task_no.Focus();
                    return;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("cc_task_no", txt_lcll_cc_task_no.Text.Trim());
                data.Add("test_type", "4");
                data.Add("staff_no", txt_lcll_staff_no.Text.Trim());
                data.Add("staff_name", txt_lcll_staff_name.Text.Trim());
                data.Add("staff_department", txt_lcll_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_lcll_staff_department_code.Text.Trim());
                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                lcll_task_no = ret.RetData;
                rahs_lcll_taskno_print();
            }
            else
            {
                if (string.IsNullOrEmpty(txt_lcll_art.Text))
                {
                    MessageBox.Show("Please scan or enter ART");
                    txt_lcll_art.Focus();
                    return;
                }
                #region 【实验室送检】量产拉力部分选项从必填改为非必填
                //if (string.IsNullOrEmpty(txt_lcll_cmbbh.Text))
                //{
                //    MessageBox.Show("请输入尺码标编号");
                //    txt_lcll_cmbbh.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(txt_lcll_test_time.Text))
                //{
                //    MessageBox.Show("请输入鞋子抽测时间");
                //    txt_lcll_test_time.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(txt_lcll_test_part.Text))
                //{
                //    MessageBox.Show("请输入试样部位名称");
                //    txt_lcll_test_part.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(cmb_lcll_jieduan.Text))
                //{
                //    MessageBox.Show("请选择阶段");
                //    cmb_lcll_jieduan.Focus();
                //    return;
                //}
                #endregion
                if (string.IsNullOrEmpty(cmb_lcll_line.Text))
                {
                    MessageBox.Show("Please select a production line");
                    cmb_lcll_line.Focus();
                    return;
                }
                var lineFind = list_line_data.FirstOrDefault(x => x.NAME == cmb_lcll_line.Text);
                if (lineFind == null)
                {
                    MessageBox.Show("Production line does not exist");
                    cmb_lcll_line.Focus();
                    return;
                }
                int scsl = 0;
                int.TryParse(txt_lcll_scsl.Text.Trim(), out scsl);

                if (scsl < 1)
                {
                    MessageBox.Show("Please enter a positive integer >=1 to send the test quantity");
                    txt_lcll_scsl.Focus();
                    return;
                }
                //if (string.IsNullOrEmpty(cmb_lcll_size.Text))
                //{
                //    MessageBox.Show("请选择size");
                //    cmb_lcll_size.Focus();
                //    return;
                //}



                List<Dictionary<string, object>> itemlist = new List<Dictionary<string, object>>();
                foreach (DataGridViewRow dr in dgv_lcll.Rows)
                {
                    if (Convert.ToBoolean(dr.Cells["lcll_check"].Value))
                    {
                        int sysl = 0;
                        string qty = dr.Cells["lcll_sample_qty"].Value.ToString();
                        if (string.IsNullOrWhiteSpace(qty))
                        {
                            MessageBox.Show($"Sample quantity cannot be empty, serial number{dr.Cells["lcll_xh"].Value.ToString()}");
                            return;
                        }
                        int.TryParse(qty, out sysl);
                        if (sysl <= 0)
                        {
                            MessageBox.Show($"Please enter the positive integer sample quantity >= 1, serial number{dr.Cells["lcll_xh"].Value.ToString()}");
                            dr.Cells["lcll_sample_qty"].Selected = true;
                            return;
                        }
                        Dictionary<string, object> item = new Dictionary<string, object>();
                        item.Add("source", dr.Cells["lcll_type"].Value.ToString() == "DQA测试任务" ? "0" : "1");
                        item.Add("inspection_code", dr.Cells["lcll_inspection_code"].Value.ToString());
                        item.Add("inspection_name", dr.Cells["lcll_inspection_name"].Value.ToString());
                        item.Add("judgment_criteria", dr.Cells["lcll_judgment_criteria"].Value.ToString());
                        item.Add("standard_value", dr.Cells["lcll_standard_value"].Value.ToString());
                        item.Add("unit", dr.Cells["lcll_unit"].Value.ToString());
                        item.Add("sample_qty", dr.Cells["lcll_sample_qty"].Value.ToString());
                        item.Add("g_formula_type", dr.Cells["lcll_tygs_code"].Value == null ? "" : dr.Cells["lcll_tygs_code"].Value.ToString());
                        item.Add("d_formula_type", dr.Cells["lcll_zdygs_code"].Value == null ? "" : dr.Cells["lcll_zdygs_code"].Value.ToString());
                        item.Add("art_d_remark", dr.Cells["lcll_remarks"].Value.ToString());
                        item.Add("inspection_type", dr.Cells["lcll_inspection_type"].Value.ToString());
                        item.Add("choice_name", dr.Cells["lcll_choice_name"].Value.ToString());
                        item.Add("choice_no", dr.Cells["lcll_choice_no"].Value.ToString());
                        item.Add("judge_type", dr.Cells["lcll_judge_type"].Value.ToString());
                        itemlist.Add(item);
                    }
                }

                Dictionary<string, object> data1 = new Dictionary<string, object>();
                data1.Add("art_no", txt_lcll_art.Text.Trim());
                data1.Add("test_type", "4");
                //键值对传值
                string retdata1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "IsSubmit",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data1));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1.ToString());

                if (!ret1.IsSuccess)
                {
                    MessageBox.Show(ret1.ErrMsg);
                    return;
                }
                if (Convert.ToInt32(ret1.RetData) > 0)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("This ART has been submitted for overproduction pull test registration, do you want to continue?", "Prompt", messButton);

                    if (dr == DialogResult.Cancel)//如果点击“取消”按钮
                    {
                        return;
                    }
                }


                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("lcll_code", txt_lcll_qrcode.Text.Trim());
                data.Add("art_no", txt_lcll_art.Text.Trim());
                data.Add("shoe_no", txt_lcll_shose.Text.Trim());
                //data.Add("shoe_name", "");
                //  data.Add("material_way", txt_cl_material_way_id.Text.Trim());
                data.Add("category_code", txt_lcll_category.Text.Trim());
                // data.Add("category_name", "");
                data.Add("product_level_code", txt_lcll_cpjb.Text.Trim());
                data.Add("product_level_value", txt_lcll_cpjb.Text.Trim());
                data.Add("season", txt_lcll_jd.Text.Trim());
                //data.Add("pb_type_code", txt_lcll_xjjb.Text.Trim());
                // data.Add("pb_type_level", "");
                //data.Add("gender", txt_cl_xb.Text.Trim());
                data.Add("phase_creation_no", cmb_lcll_jieduan.SelectedValue == null ? "" : cmb_lcll_jieduan.SelectedValue);
                data.Add("phase_creation_name", cmb_lcll_jieduan.Text);
                data.Add("send_test_qty", scsl);
                data.Add("size", cmb_lcll_size.Text);
                data.Add("order_po", txt_lcll_po_order.Text.Trim());
                data.Add("order_po_qty", txt_lcll_po_qty.Text.Trim());
                //data.Add("fgt_no", cmb_lcll_fgt.SelectedValue == null ? "" : cmb_lcll_fgt.SelectedValue);
                //data.Add("fgt_name", cmb_lcll_fgt.Text);
                data.Add("fgt_no", "");
                data.Add("fgt_name", "");
                data.Add("test_reason", txt_lcll_reason.Text.Trim());
                data.Add("staff_no", txt_lcll_staff_no.Text.Trim());
                data.Add("staff_name", txt_lcll_staff_name.Text.Trim());
                data.Add("staff_department", txt_lcll_staff_department.Text.Trim());
                data.Add("staff_department_code", lab_lcll_staff_department_code.Text.Trim());
                data.Add("task_state", 0);
                data.Add("test_type", 4);
                data.Add("line_code", cmb_lcll_line.SelectedValue == null ? "" : cmb_lcll_line.SelectedValue);
                data.Add("line_name", cmb_lcll_line.Text);
                //data.Add("test_part", txt_lcll_test_part.Text);
                data.Add("test_part", "");
                data.Add("test_time", txt_lcll_test_time.Text);
                data.Add("cmbbh", txt_lcll_cmbbh.Text);
                data.Add("itemlist", itemlist);
                data.Add("glue", txt_lcll_jsxx.Text);
                data.Add("ccsj_date", txt_lcll_test_time.Text);



                //键值对传值
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "SaveExTask",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                    return;
                }
                MessageBox.Show("Saved successfully");
                lcll_task_no = ret.RetData;
                rahs_lcll_taskno_print();
            }

        }

        private void txt_lcll_art_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btn_lcll_print_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_lcll_task_no.Text.Trim()))
            {
                F_QCM_TaskNo_Print frm = new F_QCM_TaskNo_Print(txt_lcll_task_no.Text.Trim());
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        private void txt_lcll_qrcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var result = GetArtInfo("lcll", txt_lcll_qrcode.Text.Trim());
                if (result != null)
                {
                    Bind_lcll_info(result);
                }
            }
        }

        #region 表格编辑代码快
        private void dgv_lcll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CB_G_formula_type != null)
            {
                CB_G_formula_type.Visible = false;
            }
            if (CB_D_formula_type != null)
            {
                CB_D_formula_type.Visible = false;
            }
            if (TXT_Unit != null)
            {
                TXT_Unit.Visible = false;
                TXT_Unit.Dispose();
            }
            if (TXT_Sample_qt != null)
            {
                TXT_Sample_qt.Visible = false;
                TXT_Sample_qt.Dispose();
            }
            if (TXT_Remarks != null)
            {
                TXT_Remarks.Visible = false;
                TXT_Remarks.Dispose();
            }
            if (e.RowIndex > -1)
            {
                if (dgv_lcll.Columns[e.ColumnIndex].Name == "lcll_tygs")
                {

                    CB_G_formula_type = new ComboBox();
                    CB_G_formula_type.Enabled = true;
                    CB_G_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_G_formula_type.DataSource = list_tygs_data;
                    CB_G_formula_type.DisplayMember = "NAME";
                    CB_G_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_lcll.GetCellDisplayRectangle(dgv_lcll.CurrentCell.ColumnIndex, dgv_lcll.CurrentCell.RowIndex, false);
                    CB_G_formula_type.Left = rect.Left;
                    CB_G_formula_type.Top = rect.Top;
                    CB_G_formula_type.Width = rect.Width;
                    CB_G_formula_type.Height = rect.Height;
                    CB_G_formula_type.Visible = true;
                    dgv_lcll.Controls.Add(CB_G_formula_type);
                    if (dgv_lcll.Rows[e.RowIndex].Cells["lcll_tygs_code"].Value != null && !string.IsNullOrEmpty(dgv_lcll.Rows[e.RowIndex].Cells["lcll_tygs_code"].Value.ToString()))
                    {
                        CB_G_formula_type.SelectedValue = dgv_lcll.Rows[e.RowIndex].Cells["lcll_tygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_G_formula_type.SelectedIndex = 0;
                    }
                    CB_G_formula_type.Focus();
                    CB_G_formula_type.SelectedIndexChanged += CB_G_formula_type_SelectedIndexChanged4;
                }
                if (dgv_lcll.Columns[e.ColumnIndex].Name == "lcll_zdygs")
                {

                    CB_D_formula_type = new ComboBox();
                    CB_D_formula_type.Enabled = true;
                    CB_D_formula_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    CB_D_formula_type.DataSource = list_zdygs_data;
                    CB_D_formula_type.DisplayMember = "NAME";
                    CB_D_formula_type.ValueMember = "CODE";

                    Rectangle rect = dgv_lcll.GetCellDisplayRectangle(dgv_lcll.CurrentCell.ColumnIndex, dgv_lcll.CurrentCell.RowIndex, false);
                    CB_D_formula_type.Left = rect.Left;
                    CB_D_formula_type.Top = rect.Top;
                    CB_D_formula_type.Width = rect.Width;
                    CB_D_formula_type.Height = rect.Height;
                    CB_D_formula_type.Visible = true;
                    dgv_lcll.Controls.Add(CB_D_formula_type);
                    if (dgv_lcll.Rows[e.RowIndex].Cells["lcll_zdygs_code"].Value != null && !string.IsNullOrEmpty(dgv_lcll.Rows[e.RowIndex].Cells["lcll_zdygs_code"].Value.ToString()))
                    {
                        CB_D_formula_type.SelectedValue = dgv_lcll.Rows[e.RowIndex].Cells["lcll_zdygs_code"].Value.ToString();
                    }
                    else
                    {
                        CB_D_formula_type.SelectedIndex = 0;
                    }
                    CB_D_formula_type.Focus();
                    CB_D_formula_type.SelectedIndexChanged += CB_D_formula_type_SelectedIndexChanged4;
                }
                if (dgv_lcll.Columns[e.ColumnIndex].Name == "lcll_unit")
                {

                    TXT_Unit = new TextBox();
                    TXT_Unit.Enabled = true;

                    Rectangle rect = dgv_lcll.GetCellDisplayRectangle(dgv_lcll.CurrentCell.ColumnIndex, dgv_lcll.CurrentCell.RowIndex, false);
                    TXT_Unit.Left = rect.Left;
                    TXT_Unit.Top = rect.Top;
                    TXT_Unit.Width = rect.Width;
                    TXT_Unit.Height = rect.Height;
                    TXT_Unit.Visible = true;
                    dgv_lcll.Controls.Add(TXT_Unit);
                    if (dgv_lcll.Rows[e.RowIndex].Cells["lcll_unit"].Value != null && !string.IsNullOrEmpty(dgv_lcll.Rows[e.RowIndex].Cells["lcll_unit"].Value.ToString()))
                    {
                        TXT_Unit.Text = dgv_lcll.Rows[e.RowIndex].Cells["lcll_unit"].Value.ToString();
                    }
                    TXT_Unit.Focus();
                    TXT_Unit.SelectionStart = TXT_Unit.Text.Length;
                    TXT_Unit.TextChanged += TXT_Unit_TextChanged4;
                }
                if (dgv_lcll.Columns[e.ColumnIndex].Name == "lcll_sample_qty")
                {

                    TXT_Sample_qt = new TextBox();
                    TXT_Sample_qt.Enabled = true;

                    Rectangle rect = dgv_lcll.GetCellDisplayRectangle(dgv_lcll.CurrentCell.ColumnIndex, dgv_lcll.CurrentCell.RowIndex, false);
                    TXT_Sample_qt.Left = rect.Left;
                    TXT_Sample_qt.Top = rect.Top;
                    TXT_Sample_qt.Width = rect.Width;
                    TXT_Sample_qt.Height = rect.Height;
                    TXT_Sample_qt.Visible = true;
                    dgv_lcll.Controls.Add(TXT_Sample_qt);
                    if (dgv_lcll.Rows[e.RowIndex].Cells["lcll_sample_qty"].Value != null && !string.IsNullOrEmpty(dgv_lcll.Rows[e.RowIndex].Cells["lcll_sample_qty"].Value.ToString()))
                    {
                        TXT_Sample_qt.Text = dgv_lcll.Rows[e.RowIndex].Cells["lcll_sample_qty"].Value.ToString();
                    }
                    TXT_Sample_qt.Focus();
                    TXT_Sample_qt.SelectionStart = TXT_Sample_qt.Text.Length;
                    TXT_Sample_qt.TextChanged += TXT_Sample_qt_TextChanged4;
                }
                if (dgv_lcll.Columns[e.ColumnIndex].Name == "lcll_remarks")
                {

                    TXT_Remarks = new TextBox();
                    TXT_Remarks.Enabled = true;

                    Rectangle rect = dgv_lcll.GetCellDisplayRectangle(dgv_lcll.CurrentCell.ColumnIndex, dgv_lcll.CurrentCell.RowIndex, false);
                    TXT_Remarks.Left = rect.Left;
                    TXT_Remarks.Top = rect.Top;
                    TXT_Remarks.Width = rect.Width;
                    TXT_Remarks.Height = rect.Height;
                    TXT_Remarks.Visible = true;
                    dgv_lcll.Controls.Add(TXT_Remarks);
                    if (dgv_lcll.Rows[e.RowIndex].Cells["lcll_remarks"].Value != null && !string.IsNullOrEmpty(dgv_lcll.Rows[e.RowIndex].Cells["lcll_remarks"].Value.ToString()))
                    {
                        TXT_Remarks.Text = dgv_lcll.Rows[e.RowIndex].Cells["lcll_remarks"].Value.ToString();
                    }
                    TXT_Remarks.Focus();
                    TXT_Remarks.SelectionStart = TXT_Remarks.Text.Length;
                    TXT_Remarks.TextChanged += TXT_Remarks_TextChanged4;
                }
            }
        }
        private void CB_G_formula_type_SelectedIndexChanged4(object sender, EventArgs e)
        {
            dgv_lcll.CurrentCell.Value = CB_G_formula_type.Text;
            dgv_lcll.Rows[dgv_lcll.CurrentCell.RowIndex].Cells["lcll_tygs_code"].Value = CB_G_formula_type.SelectedValue;
            CB_G_formula_type.Visible = false;
            CB_G_formula_type.Dispose();
        }
        private void CB_D_formula_type_SelectedIndexChanged4(object sender, EventArgs e)
        {
            dgv_lcll.CurrentCell.Value = CB_D_formula_type.Text;
            dgv_lcll.Rows[dgv_lcll.CurrentCell.RowIndex].Cells["lcll_zdygs_code"].Value = CB_D_formula_type.SelectedValue;
            CB_D_formula_type.Visible = false;
            CB_D_formula_type.Dispose();
        }
        private void TXT_Unit_TextChanged4(object sender, EventArgs e)
        {
            dgv_lcll.CurrentCell.Value = TXT_Unit.Text;
        }
        private void TXT_Sample_qt_TextChanged4(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(TXT_Sample_qt.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(TXT_Sample_qt.Text))
            {
                MessageBox.Show("请输入正整数");
                TXT_Sample_qt.Text = "";
                return;
            }
            dgv_lcll.CurrentCell.Value = TXT_Sample_qt.Text;
        }
        private void TXT_Remarks_TextChanged4(object sender, EventArgs e)
        {
            dgv_lcll.CurrentCell.Value = TXT_Remarks.Text;
        }
        #endregion
        private void txt_lcll_scsl_TextChanged(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(txt_lcll_scsl.Text, out qty);
            if (qty <= 0 && !string.IsNullOrEmpty(txt_lcll_scsl.Text))
            {
                MessageBox.Show("Please enter a positive integer");
                txt_lcll_scsl.Text = "";
                return;
            }
        }


        #endregion

        private void ckb_gy_sfcc_CheckedChanged(object sender, EventArgs e)
        {
            dgv_gy.Rows.Clear();
            if (ckb_gy_sfcc.Checked)
            {
                txt_gy_cc_task_no.Enabled = true;
                txt_gy_cc_task_no.Text = "";
                txt_gy_cc_task_no.Focus();
                txt_gy_task_no.Text = "";
                txt_gy_code.Enabled = false;
                txt_gy_code.Text = "";
                txt_gy_art.Text = "";
                txt_gy_art.Enabled = false;
                txt_gy_shose.Text = "";
                cmb_gy_gymc.DataSource = null;
                cmb_gy_gymc.Enabled = false;
                cmb_gy_gymc.Text = "";
                cmb_gy_bwmc.DataSource = null;
                cmb_gy_bwmc.Enabled = false;
                cmb_gy_bwmc.Text = "";
                //txt_gy_material_way_id.Text = "";
                //cmb_gy_category.DataSource = null;
                //cmb_gy_category.Enabled = false;
                //cmb_gy_category.Text = "";
                tb_gy_kfxl.Text = "";
                txt_gy_cpjb.Text = "";
                txt_gy_jidu.Text = "";
                //txt_gy_xjjb.Text = "";
                //txt_gy_xb.Text = "";
                cmb_gy_jieduan.DataSource = null;
                cmb_gy_jieduan.Enabled = false;
                cmb_gy_jieduan.Text = "";
                txt_gy_scsl.Text = "";
                txt_gy_scsl.Enabled = false;
                //cmb_gy_size.DataSource = null;
                //cmb_gy_size.Enabled = false;
                //cmb_gy_size.Text = "";
                //txt_gy_po_order.Text = "";
                //txt_gy_po_order.Enabled = false;
                //txt_gy_po_qty.Text = "";
                cmb_gy_fgt.DataSource = null;
                cmb_gy_fgt.Enabled = false;
                cmb_gy_fgt.Text = "";
                txt_gy_cs.Text = "";
                txt_gy_cs.Enabled = false;
                lab_gy_cs_jc.Text = "";
                lab_gy_cs_code.Text = "";
                txt_gy_reason.Text = "";
                txt_gy_reason.Enabled = false;
            }
            else
            {
                txt_gy_cc_task_no.Enabled = false;
                txt_gy_cc_task_no.Text = "";
                txt_gy_task_no.Text = "";
                txt_gy_code.Enabled = true;
                txt_gy_code.Text = "";
                txt_gy_art.Text = "";
                txt_gy_art.Focus();
                txt_gy_art.Enabled = true;
                txt_gy_shose.Text = "";
                cmb_gy_gymc.DataSource = null;
                cmb_gy_gymc.Enabled = true;
                cmb_gy_gymc.Text = "";
                cmb_gy_bwmc.DataSource = null;
                cmb_gy_bwmc.Enabled = true;
                cmb_gy_bwmc.Text = "";
                //txt_gy_material_way_id.Text = "";
                //cmb_gy_category.DataSource = null;
                //cmb_gy_category.Enabled = true;
                //cmb_gy_category.Text = "";
                tb_gy_kfxl.Text = "";
                txt_gy_cpjb.Text = "";
                txt_gy_jidu.Text = "";
                //txt_gy_xjjb.Text = "";
                //txt_gy_xb.Text = "";
                cmb_gy_jieduan.DataSource = null;
                cmb_gy_jieduan.Enabled = true;
                cmb_gy_jieduan.Text = "";
                txt_gy_scsl.Text = "";
                txt_gy_scsl.Enabled = true;
                //cmb_gy_size.DataSource = null;
                //cmb_gy_size.Enabled = true;
                //cmb_gy_size.Text = "";
                //txt_gy_po_order.Text = "";
                //txt_gy_po_order.Enabled = true;
                //txt_gy_po_qty.Text = "";
                cmb_gy_fgt.DataSource = null;
                cmb_gy_fgt.Enabled = true;
                cmb_gy_fgt.Text = "";
                txt_gy_cs.Text = "";
                txt_gy_cs.Enabled = true;
                lab_gy_cs_jc.Text = "";
                lab_gy_cs_code.Text = "";
                txt_gy_reason.Text = "";
                txt_gy_reason.Enabled = true;
            }
        }

        private void txt_gy_cc_task_no_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", txt_gy_cc_task_no.Text.Trim());
                p.Add("test_type", "2");
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
                    MessageBox.Show(ret.ErrMsg);
                    txt_gy_task_no.Text = "";
                    txt_gy_task_no.Focus();
                    return;
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["info"].ToString());
                DataTable itemlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["itemlist"].ToString());

                txt_gy_shose.Text = info["SHOE_NO"].ToString();
                txt_gy_art.Text = info["ART_NO"].ToString();
                cmb_gy_gymc.Text = info["WORKMANSHIP_NAME"].ToString();
                cmb_gy_bwmc.Text = info["POSITION_NAME"].ToString();

                //txt_gy_material_way_id.Text = info["MODEL_NO"].ToString();
                //cmb_gy_category.Text = info["CATEGORY_NAME"].ToString();
                tb_gy_kfxl.Text = info["CATEGORY_CODE"].ToString();
                txt_gy_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();
                txt_gy_jidu.Text = info["SEASON"].ToString();
                //txt_gy_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();
                //txt_gy_xb.Text = info["GENDER_NAME"].ToString();
                cmb_gy_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();
                txt_gy_scsl.Text = info["SEND_TEST_QTY"].ToString();
                //cmb_gy_size.Text = info["SIZES"].ToString();
                //txt_gy_po_order.Text = info["ORDER_PO"].ToString();
                //txt_gy_po_qty.Text = info["ORDER_PO_QTY"].ToString();
                cmb_gy_fgt.Text = info["FGT_NAME"].ToString();
                txt_gy_cs.Text = info["MANUFACTURER_NAME"].ToString();
                lab_gy_cs_code.Text = info["MANUFACTURER_CODE"].ToString();
                lab_gy_cs_jc.Text = info["MANUFACTURER_JC"].ToString();
                txt_gy_reason.Text = info["TEST_REASON"].ToString();
                dgv_gy.Rows.Clear();
                foreach (DataRow dr in itemlist.Rows)
                {
                    int i = dgv_gy.Rows.Add();
                    dgv_gy.Rows[i].ReadOnly = true;
                    dgv_gy.Rows[i].Cells["gy_xh"].Value = (i + 1).ToString();
                    dgv_gy.Rows[i].Cells["gy_check"].Value = true;
                    dgv_gy.Rows[i].Cells["gy_type"].Value = dr["SOURCES"].ToString() == "0" ? "DQA测试任务" : "conventional";
                    dgv_gy.Rows[i].Cells["gy_inspection_type_name"].Value = dr["INSPECTION_TYPE_NAME"].ToString();
                    dgv_gy.Rows[i].Cells["gy_choice_name"].Value = dr["CHOICE_NAME"].ToString();
                    dgv_gy.Rows[i].Cells["gy_inspection_code"].Value = dr["INSPECTION_CODE"].ToString();
                    dgv_gy.Rows[i].Cells["gy_inspection_name"].Value = dr["INSPECTION_NAME"].ToString();
                    dgv_gy.Rows[i].Cells["gy_judgment_criteria_name"].Value = dr["JUDGMENT_CRITERIA_NAME"].ToString();
                    dgv_gy.Rows[i].Cells["gy_judge_type"].Value = dr["JUDGE_TYPE"].ToString();
                    dgv_gy.Rows[i].Cells["gy_judge_type_name"].Value = dr["JUDGE_TYPE_NAME"].ToString();
                    dgv_gy.Rows[i].Cells["gy_standard_value"].Value = dr["STANDARD_VALUE"].ToString();
                    dgv_gy.Rows[i].Cells["gy_unit"].Value = dr["UNIT"].ToString();
                    dgv_gy.Rows[i].Cells["gy_sample_qty"].Value = dr["SAMPLE_QTY"].ToString();
                    dgv_gy.Rows[i].Cells["gy_remarks"].Value = dr["ART_D_REMARK"].ToString();
                    dgv_gy.Rows[i].Cells["gy_inspection_type"].Value = dr["INSPECTION_TYPE"].ToString();
                    dgv_gy.Rows[i].Cells["gy_choice_no"].Value = dr["CHOICE_NO"].ToString();
                    dgv_gy.Rows[i].Cells["gy_judgment_criteria"].Value = dr["JUDGMENT_CRITERIA"].ToString();
                    dgv_gy.Rows[i].Cells["gy_tygs_code"].Value = dr["G_FORMULA_CODE"].ToString();
                    dgv_gy.Rows[i].Cells["gy_zdygs_code"].Value = dr["D_FORMULA_CODE"].ToString();

                    var tygs = list_tygs_data.FirstOrDefault(x => x.CODE == dr["G_FORMULA_CODE"].ToString());
                    if (tygs != null)
                        dgv_gy.Rows[i].Cells["gy_tygs"].Value = tygs.NAME;
                    var zdygs = list_zdygs_data.FirstOrDefault(x => x.CODE == dr["D_FORMULA_CODE"].ToString());
                    if (zdygs != null)
                        dgv_gy.Rows[i].Cells["gy_zdygs"].Value = zdygs.NAME;
                }
            }
        }

        private void ckb_cl_sfcc_CheckedChanged(object sender, EventArgs e)
        {
            dgv_cl.Rows.Clear();
            if (ckb_cl_sfcc.Checked)
            {
                txt_cl_cc_task_no.Enabled = true;
                txt_cl_cc_task_no.Text = "";
                txt_cl_cc_task_no.Focus();
                txt_cl_task_no.Text = "";
                txt_cl_qrcode.Enabled = false;
                txt_cl_qrcode.Text = "";
                txt_cl_art.Text = "";
                txt_cl_art.Enabled = false;
                txt_cl_shose.Text = "";
                cmb_cl_category.DataSource = null;
                cmb_cl_category.Enabled = false;
                cmb_cl_category.Text = "";
                cmb_cl_clzl.DataSource = null;
                cmb_cl_clzl.Enabled = false;
                cmb_cl_clzl.Text = "";
                cmb_cl_bwmc.DataSource = null;
                cmb_cl_bwmc.Enabled = false;
                cmb_cl_bwmc.Text = "";
                txt_cl_cpjb.Text = "";
                txt_cl_jd.Text = "";
                txt_cl_xjjb.Text = "";
                txt_cl_color.Text = "";
                txt_cl_clid.Text = "";
                txt_cl_test_id.Text = "";
                txt_cl_test_id.Enabled = false;
                cmb_cl_jieduan.DataSource = null;
                cmb_cl_jieduan.Enabled = false;
                cmb_cl_jieduan.Text = "";
                txt_cl_scsl.Text = "";
                txt_cl_scsl.Enabled = false;
                cmb_cl_size.DataSource = null;
                cmb_cl_size.Enabled = false;
                cmb_cl_size.Text = "";
                txt_cl_po_order.Text = "";
                txt_cl_po_order.Enabled = false;
                txt_cl_po_qty.Text = "";
                cmb_cl_fgt.DataSource = null;
                cmb_cl_fgt.Enabled = false;
                cmb_cl_fgt.Text = "";
                txt_cl_cs.Text = "";
                txt_cl_cs.Enabled = false;
                lab_cl_cs_jc.Text = "";
                lab_cl_cs_code.Text = "";
                txt_cl_reason.Text = "";
                txt_cl_reason.Enabled = false;
            }
            else
            {
                txt_cl_cc_task_no.Enabled = false;
                txt_cl_cc_task_no.Text = "";

                txt_cl_task_no.Text = "";
                txt_cl_qrcode.Text = "";
                txt_cl_art.Text = "";
                txt_cl_art.Enabled = true;
                txt_cl_art.Focus();
                txt_cl_shose.Text = "";
                cmb_cl_category.DataSource = null;
                cmb_cl_category.Enabled = true;
                cmb_cl_category.Text = "";
                cmb_cl_clzl.DataSource = null;
                cmb_cl_clzl.Enabled = true;
                cmb_cl_clzl.Text = "";
                cmb_cl_bwmc.DataSource = null;
                cmb_cl_bwmc.Enabled = true;
                cmb_cl_bwmc.Text = "";
                txt_cl_cpjb.Text = "";
                txt_cl_jd.Text = "";
                txt_cl_xjjb.Text = "";
                txt_cl_color.Text = "";
                txt_cl_clid.Text = "";
                txt_cl_test_id.Text = "";
                txt_cl_test_id.Enabled = true;
                cmb_cl_jieduan.DataSource = null;
                cmb_cl_jieduan.Enabled = true;
                cmb_cl_jieduan.Text = "";
                txt_cl_scsl.Text = "";
                txt_cl_scsl.Enabled = true;
                cmb_cl_size.DataSource = null;
                cmb_cl_size.Enabled = true;
                cmb_cl_size.Text = "";
                txt_cl_po_order.Text = "";
                txt_cl_po_order.Enabled = true;
                txt_cl_po_qty.Text = "";
                cmb_cl_fgt.DataSource = null;
                cmb_cl_fgt.Enabled = true;
                cmb_cl_fgt.Text = "";
                txt_cl_cs.Text = "";
                txt_cl_cs.Enabled = true;
                lab_cl_cs_jc.Text = "";
                lab_cl_cs_code.Text = "";
                txt_cl_reason.Text = "";
                txt_cl_reason.Enabled = true;
            }
        }

        private void txt_cl_cc_task_no_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", txt_cl_cc_task_no.Text.Trim());
                p.Add("test_type", "3");
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
                    MessageBox.Show(ret.ErrMsg);
                    txt_cl_task_no.Text = "";
                    txt_cl_task_no.Focus();
                    return;
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["info"].ToString());
                DataTable itemlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["itemlist"].ToString());

                txt_cl_qrcode.Text = info["ART_NO"].ToString();
                txt_cl_shose.Text = info["SHOE_NO"].ToString();
                txt_cl_art.Text = info["ART_NO"].ToString();
                cmb_cl_clzl.Text = info["MAKINGS_TYPE_NAME"].ToString();
                cmb_cl_bwmc.Text = info["POSITION_NAME"].ToString();
                cmb_cl_category.Text = info["CATEGORY_CODE"].ToString();
                txt_cl_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();
                txt_cl_jd.Text = info["SEASON"].ToString();
                txt_cl_xjjb.Text = info["PB_TYPE_LEVEL"].ToString();
                txt_cl_color.Text = info["COLORS"].ToString();
                txt_cl_clid.Text = info["MAKINGS_ID"].ToString();
                txt_cl_test_id.Text = info["TEST_ID"].ToString();
                cmb_cl_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();
                txt_cl_scsl.Text = info["SEND_TEST_QTY"].ToString();
                cmb_cl_size.Text = info["SIZES"].ToString();
                txt_cl_po_order.Text = info["ORDER_PO"].ToString();
                txt_cl_po_qty.Text = info["ORDER_PO_QTY"].ToString();
                cmb_cl_fgt.Text = info["FGT_NAME"].ToString();
                txt_cl_cs.Text = info["MANUFACTURER_NAME"].ToString();
                lab_cl_cs_code.Text = info["MANUFACTURER_CODE"].ToString();
                lab_cl_cs_jc.Text = info["MANUFACTURER_JC"].ToString();
                txt_cl_reason.Text = info["TEST_REASON"].ToString();
                dgv_cl.Rows.Clear();
                foreach (DataRow dr in itemlist.Rows)
                {
                    int i = dgv_cl.Rows.Add();
                    dgv_cl.Rows[i].ReadOnly = true;
                    dgv_cl.Rows[i].Cells["cl_xh"].Value = (i + 1).ToString();
                    dgv_cl.Rows[i].Cells["cl_check"].Value = true;
                    dgv_cl.Rows[i].Cells["cl_type"].Value = dr["SOURCES"].ToString() == "0" ? "DQA测试任务" : "conventional";
                    dgv_cl.Rows[i].Cells["cl_inspection_type_name"].Value = dr["INSPECTION_TYPE_NAME"].ToString();
                    dgv_cl.Rows[i].Cells["cl_choice_name"].Value = dr["CHOICE_NAME"].ToString();
                    dgv_cl.Rows[i].Cells["cl_inspection_code"].Value = dr["INSPECTION_CODE"].ToString();
                    dgv_cl.Rows[i].Cells["cl_inspection_name"].Value = dr["INSPECTION_NAME"].ToString();
                    dgv_cl.Rows[i].Cells["cl_judgment_criteria_name"].Value = dr["JUDGMENT_CRITERIA_NAME"].ToString();
                    dgv_cl.Rows[i].Cells["cl_judge_type"].Value = dr["JUDGE_TYPE"].ToString();
                    dgv_cl.Rows[i].Cells["cl_judge_type_name"].Value = dr["JUDGE_TYPE_NAME"].ToString();
                    dgv_cl.Rows[i].Cells["cl_standard_value"].Value = dr["STANDARD_VALUE"].ToString();
                    dgv_cl.Rows[i].Cells["cl_unit"].Value = dr["UNIT"].ToString();
                    dgv_cl.Rows[i].Cells["cl_sample_qty"].Value = dr["SAMPLE_QTY"].ToString();
                    dgv_cl.Rows[i].Cells["cl_remarks"].Value = dr["ART_D_REMARK"].ToString();
                    dgv_cl.Rows[i].Cells["cl_inspection_type"].Value = dr["INSPECTION_TYPE"].ToString();
                    dgv_cl.Rows[i].Cells["cl_choice_no"].Value = dr["CHOICE_NO"].ToString();
                    dgv_cl.Rows[i].Cells["cl_judgment_criteria"].Value = dr["JUDGMENT_CRITERIA"].ToString();
                    dgv_cl.Rows[i].Cells["cl_tygs_code"].Value = dr["G_FORMULA_CODE"].ToString();
                    dgv_cl.Rows[i].Cells["cl_zdygs_code"].Value = dr["D_FORMULA_CODE"].ToString();

                    var tygs = list_tygs_data.FirstOrDefault(x => x.CODE == dr["G_FORMULA_CODE"].ToString());
                    if (tygs != null)
                        dgv_cl.Rows[i].Cells["cl_tygs"].Value = tygs.NAME;
                    var zdygs = list_zdygs_data.FirstOrDefault(x => x.CODE == dr["D_FORMULA_CODE"].ToString());
                    if (zdygs != null)
                        dgv_cl.Rows[i].Cells["cl_zdygs"].Value = zdygs.NAME;
                }
            }
        }

        private void ckb_lcll_sfcc_CheckedChanged(object sender, EventArgs e)
        {
            dgv_lcll.Rows.Clear();
            if (ckb_lcll_sfcc.Checked)
            {
                txt_lcll_cc_task_no.Enabled = true;
                txt_lcll_cc_task_no.Text = "";
                txt_lcll_cc_task_no.Focus();
                txt_lcll_task_no.Text = "";
                txt_lcll_qrcode.Enabled = false;
                txt_lcll_qrcode.Text = "";
                txt_lcll_art.Text = "";
                txt_lcll_shose.Text = "";
                txt_lcll_category.Text = "";
                cmb_lcll_line.DataSource = null;
                cmb_lcll_line.Enabled = false;
                cmb_lcll_line.Text = "";
                txt_lcll_cmbbh.Text = "";
                txt_lcll_cmbbh.Enabled = false;
                txt_lcll_cpjb.Text = "";
                txt_lcll_jd.Text = "";
                cmb_lcll_jieduan.DataSource = null;
                cmb_lcll_jieduan.Enabled = false;
                cmb_lcll_jieduan.Text = "";
                txt_lcll_scsl.Text = "";
                txt_lcll_scsl.Enabled = false;
                cmb_lcll_size.DataSource = null;
                cmb_lcll_size.Enabled = false;
                cmb_lcll_size.Text = "";
                txt_lcll_po_order.Text = "";
                txt_lcll_po_order.Enabled = false;
                txt_lcll_po_qty.Text = "";
                //cmb_lcll_fgt.DataSource = null;
                //cmb_lcll_fgt.Enabled = false;
                //cmb_lcll_fgt.Text = "";
                txt_lcll_test_time.Text = "";
                txt_lcll_test_time.Enabled = false;
                //txt_lcll_test_part.Text = "";
                //txt_lcll_test_part.Enabled = false;
                txt_lcll_jsxx.Text = "";
                txt_lcll_jsxx.Enabled = false;
                txt_lcll_reason.Text = "";
                txt_lcll_reason.Enabled = false;


            }
            else
            {
                txt_lcll_cc_task_no.Enabled = false;
                txt_lcll_cc_task_no.Text = "";
                txt_lcll_task_no.Text = "";
                txt_lcll_qrcode.Enabled = true;
                txt_lcll_qrcode.Text = "";
                txt_lcll_qrcode.Focus();
                txt_lcll_art.Text = "";
                txt_lcll_shose.Text = "";
                txt_lcll_category.Text = "";
                cmb_lcll_line.DataSource = null;
                cmb_lcll_line.Enabled = true;
                cmb_lcll_line.Text = "";
                txt_lcll_cmbbh.Text = "";
                txt_lcll_cmbbh.Enabled = true;
                txt_lcll_cpjb.Text = "";
                txt_lcll_jd.Text = "";
                cmb_lcll_jieduan.DataSource = null;
                cmb_lcll_jieduan.Enabled = true;
                cmb_lcll_jieduan.Text = "";
                txt_lcll_scsl.Text = "";
                txt_lcll_scsl.Enabled = true;
                cmb_lcll_size.DataSource = null;
                cmb_lcll_size.Enabled = true;
                cmb_lcll_size.Text = "";
                txt_lcll_po_order.Text = "";
                txt_lcll_po_order.Enabled = true;
                txt_lcll_po_qty.Text = "";
                //cmb_lcll_fgt.DataSource = null;
                //cmb_lcll_fgt.Enabled = true;
                //cmb_lcll_fgt.Text = "";
                txt_lcll_test_time.Text = "";
                txt_lcll_test_time.Enabled = true;
                //txt_lcll_test_part.Text = "";
                //txt_lcll_test_part.Enabled = true;
                txt_lcll_jsxx.Text = "";
                txt_lcll_jsxx.Enabled = true;
                txt_lcll_reason.Text = "";
                txt_lcll_reason.Enabled = true;
            }
        }

        private void txt_lcll_cc_task_no_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("task_no", txt_lcll_cc_task_no.Text.Trim());
                p.Add("test_type", "4");
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
                    MessageBox.Show(ret.ErrMsg);
                    txt_lcll_task_no.Text = "";
                    txt_lcll_task_no.Focus();
                    return;
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["info"].ToString());
                DataTable itemlist = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["itemlist"].ToString());

                txt_lcll_qrcode.Text = info["ART_NO"].ToString();
                txt_lcll_shose.Text = info["SHOE_NO"].ToString();
                txt_lcll_art.Text = info["ART_NO"].ToString();
                txt_lcll_category.Text = info["CATEGORY_CODE"].ToString();
                cmb_lcll_line.Text = info["LINE_NAME"].ToString();
                txt_lcll_cmbbh.Text = info["CMBBH"].ToString();
                txt_lcll_cpjb.Text = info["PRODUCT_LEVEL_CODE"].ToString();
                txt_lcll_jd.Text = info["SEASON"].ToString();
                cmb_lcll_jieduan.Text = info["PHASE_CREATION_NAME"].ToString();
                txt_lcll_scsl.Text = info["SEND_TEST_QTY"].ToString();
                cmb_lcll_size.Text = info["SIZES"].ToString();
                txt_lcll_po_order.Text = info["ORDER_PO"].ToString();
                txt_lcll_po_qty.Text = info["ORDER_PO_QTY"].ToString();
                txt_lcll_test_time.Text = info["TEST_TIME"].ToString();
                //txt_lcll_test_part.Text = info["TEST_PARTS"].ToString();
                txt_lcll_jsxx.Text = info["GLUE"].ToString();
                txt_lcll_reason.Text = info["TEST_REASON"].ToString();
                dgv_lcll.Rows.Clear();
                foreach (DataRow dr in itemlist.Rows)
                {
                    int i = dgv_lcll.Rows.Add();
                    dgv_lcll.Rows[i].ReadOnly = true;
                    dgv_lcll.Rows[i].Cells["lcll_xh"].Value = (i + 1).ToString();
                    dgv_lcll.Rows[i].Cells["lcll_check"].Value = true;
                    dgv_lcll.Rows[i].Cells["lcll_type"].Value = dr["SOURCES"].ToString() == "0" ? "DQA测试任务" : "conventional";
                    dgv_lcll.Rows[i].Cells["lcll_inspection_type_name"].Value = dr["INSPECTION_TYPE_NAME"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_choice_name"].Value = dr["CHOICE_NAME"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_inspection_code"].Value = dr["INSPECTION_CODE"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_inspection_name"].Value = dr["INSPECTION_NAME"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_judgment_criteria_name"].Value = dr["JUDGMENT_CRITERIA_NAME"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_judge_type"].Value = dr["JUDGE_TYPE"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_judge_type_name"].Value = dr["JUDGE_TYPE_NAME"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_standard_value"].Value = dr["STANDARD_VALUE"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_unit"].Value = dr["UNIT"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_sample_qty"].Value = dr["SAMPLE_QTY"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_remarks"].Value = dr["ART_D_REMARK"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_inspection_type"].Value = dr["INSPECTION_TYPE"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_choice_no"].Value = dr["CHOICE_NO"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_judgment_criteria"].Value = dr["JUDGMENT_CRITERIA"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_tygs_code"].Value = dr["G_FORMULA_CODE"].ToString();
                    dgv_lcll.Rows[i].Cells["lcll_zdygs_code"].Value = dr["D_FORMULA_CODE"].ToString();

                    var tygs = list_tygs_data.FirstOrDefault(x => x.CODE == dr["G_FORMULA_CODE"].ToString());
                    if (tygs != null)
                        dgv_lcll.Rows[i].Cells["lcll_tygs"].Value = tygs.NAME;
                    var zdygs = list_zdygs_data.FirstOrDefault(x => x.CODE == dr["D_FORMULA_CODE"].ToString());
                    if (zdygs != null)
                        dgv_lcll.Rows[i].Cells["lcll_zdygs"].Value = zdygs.NAME;
                }
            }
        }

        private void txt_cl_qrcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Dictionary<string,object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(txt_cl_qrcode.Text.Trim());
                    if (dic.ContainsKey("rcpt_date") && dic.ContainsKey("item_no"))
                    {
                        txt_chk_no(dic["rcpt_date"].ToString(),dic["item_no"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Invalid QR code");
                        txt_cl_qrcode.Text = "";
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid QR code");
                    txt_cl_qrcode.Text = "";
                }
            }
        }
        private void txt_chk_no(string rcpt_date,string item_no)
        {

            //请求api的数据展示
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("rcpt_date", rcpt_date);
            p.Add("item_no", item_no);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "Getchk_list",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (!ret.IsSuccess)
            {
                MessageBox.Show(ret.ErrMsg);
              
                return;
            }
            else
            {
                txt_cl_qrcode.Text = ret.RetData;
                txt_cl_clid.Text =item_no;
            }
        }

        private void cmb_lcll_line_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_lcll_art.Text) && list_line_data != null && list_line_data.Count > 0)
                {
                    this.cmb_lcll_line.Items.Clear();
                    //查询符合条件的数据
                    var whereList = list_line_data.Where(x => x.NAME.Contains(this.cmb_lcll_line.Text)).ToList();
                    if(whereList.FirstOrDefault(x=>x.NAME== cmb_lcll_line.Text) == null)
                    {
                        whereList.Insert(0, new code_name_obj() { CODE = cmb_lcll_line.Text, NAME = "" });
                    }
                    //combobox添加已经查到的关键词
                    this.cmb_lcll_line.Items.AddRange(whereList.ToArray());
                    //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
                    this.cmb_lcll_line.SelectionStart = this.cmb_lcll_line.Text.Length;
                    //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
                    Cursor = Cursors.Default;
                    //自动弹出下拉框
                    this.cmb_lcll_line.DroppedDown = true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void txt_cpx_ddpo_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.txt_cpx_ddpo;
            if (!string.IsNullOrEmpty(currC.Text))
            {
                // 创建the ToolTip 
                ToolTip toolTip1 = new ToolTip();

                // 设置显示样式
                toolTip1.AutoPopDelay = 25000;
                toolTip1.InitialDelay = 500;//事件触发多久后出现提示
                toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                toolTip1.ShowAlways = true;//是否显示提示框

                //  设置伴随的对象.
                toolTip1.SetToolTip(currC, currC.Text);//设置提示按钮和提示内容
            }
        }

        private void txt_bj_po_order_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.txt_bj_po_order;
            if (!string.IsNullOrEmpty(currC.Text))
            {
                // 创建the ToolTip 
                ToolTip toolTip1 = new ToolTip();

                // 设置显示样式
                toolTip1.AutoPopDelay = 25000;
                toolTip1.InitialDelay = 500;//事件触发多久后出现提示
                toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                toolTip1.ShowAlways = true;//是否显示提示框

                //  设置伴随的对象.
                toolTip1.SetToolTip(currC, currC.Text);//设置提示按钮和提示内容
            }
        }

        private void txt_cl_po_order_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.txt_cl_po_order;
            if (!string.IsNullOrEmpty(currC.Text))
            {
                // 创建the ToolTip 
                ToolTip toolTip1 = new ToolTip();

                // 设置显示样式
                toolTip1.AutoPopDelay = 25000;
                toolTip1.InitialDelay = 500;//事件触发多久后出现提示
                toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                toolTip1.ShowAlways = true;//是否显示提示框

                //  设置伴随的对象.
                toolTip1.SetToolTip(currC, currC.Text);//设置提示按钮和提示内容
            }
        }

        private void txt_lcll_po_order_MouseHover(object sender, EventArgs e)
        {
            Control currC = this.txt_lcll_po_order;
            if (!string.IsNullOrEmpty(currC.Text))
            {
                // 创建the ToolTip 
                ToolTip toolTip1 = new ToolTip();

                // 设置显示样式
                toolTip1.AutoPopDelay = 25000;
                toolTip1.InitialDelay = 500;//事件触发多久后出现提示
                toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
                toolTip1.ShowAlways = true;//是否显示提示框

                //  设置伴随的对象.
                toolTip1.SetToolTip(currC, currC.Text);//设置提示按钮和提示内容
            }
        }
    }
}
