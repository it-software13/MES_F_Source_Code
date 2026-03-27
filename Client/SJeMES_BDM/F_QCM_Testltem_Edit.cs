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

namespace SJeMES_BDM
{
    public partial class F_QCM_Testltem_Edit : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        //传过来的编号
        string Codeid;
        //原有的类型代号
        string txt_testtype_name=string.Empty;
        string cbo_typeno_val = string.Empty;
        string unit_val = string.Empty;
        public F_QCM_Testltem_Edit(string id)
        {
            this.Codeid = id;
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 加载表身明细数据
        /// </summary>
        /// <param name="txt_testitem_code"></param>
        public void GetData(string txt_testitem_code)
        {
            if (string.IsNullOrEmpty(txt_testitem_code))
                return;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("data", txt_testitem_code);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.TableView",//类名
                                            "GetBDM_TESTITEMAddList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p)); 
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
              
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
               DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["ID"].Value = dr["id"].ToString();
                        dgvr.Cells["Standard_measurement"].Value = dr["标准测量"].ToString();
                        dgvr.Cells["unit"].Value = dr["单位"].ToString();
                        dgvr.Cells["remarks"].Value = dr["备注"].ToString();
                       
                        i++;
                    }
                }
                GenClass.AutoSizeColumn(dataGridView1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        /// <summary>
        /// Load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void F_QCM_Testltem_Edit_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //绑定数据的下拉框
            try
            { 
                #region 查询枚举
                List<string> lst_enum_type = new List<string>();
                lst_enum_type.Add("enum_testitem_type");
                lst_enum_type.Add("enum_ref_level"); 
                lst_enum_type.Add("enum_general_formula"); 
                lst_enum_type.Add("enum_aql_level"); 
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
                //结果引用级别
                cbo_type.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_testitem_type"].ToString());
                cbo_type.DisplayMember = "enum_value";
                cbo_type.ValueMember = "enum_code";
                cbo_type.SelectedIndex = -1;
                //引用级别
                cbo_reference_level.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_ref_level"].ToString());
                cbo_reference_level.DisplayMember = "enum_value";
                cbo_reference_level.ValueMember = "enum_code";
                cbo_reference_level.SelectedIndex = -1;
                //通用公式
                cbo_currency_formula.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_general_formula"].ToString());
                cbo_currency_formula.DisplayMember = "enum_value";
                cbo_currency_formula.ValueMember = "enum_code";
                cbo_currency_formula.SelectedIndex = -1;
                //AQL级别
                cbo_AQL_LEVEL.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["enum_aql_level"].ToString());
                cbo_AQL_LEVEL.DisplayMember = "enum_value";
                cbo_AQL_LEVEL.ValueMember = "enum_code";
                cbo_AQL_LEVEL.SelectedIndex = -1;
                #endregion

                #region MyRegion
                //两个公式的下拉框   
                retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                           Program.Client.APIURL,
                                           "SJ_QCMAPI",//类库名
                                           "SJ_QCMAPI.TableView",//类名
                                           "GetFormulaData",//方法名
                                           Program.Client.UserToken,//token
                                           string.Empty);
                ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
             
               //选择自定义公式
                cbo_custom_formula.DataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                cbo_custom_formula.ValueMember = "formula_code";
                cbo_custom_formula.DisplayMember = "formula_name";
                cbo_custom_formula.SelectedIndex = -1;
                #endregion

                if (!string.IsNullOrEmpty(Codeid))
                {
                    btn_keep.Text = "保存修改";
                    //检测项编号不给修改
                    txt_testitem_code.ReadOnly = true;
                     
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("data", Codeid);
                    retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.TableView",//类名
                                                "GetBDM_TESTITEMUpdatebyId",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));

                    ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        txt_testitem_name.Text = dt.Rows[0]["检测项名称"].ToString();
                        txt_testtype_no.Text = dt.Rows[0]["测试类型"].ToString();
                        txt_unit.Text = dt.Rows[0]["单位"].ToString();
                        richTextBox_remarks.Text = dt.Rows[0]["备注"].ToString();
                        txt_testitem_code.Text = dt.Rows[0]["检测项编号"].ToString();
                        txt_sample_num.Text = dt.Rows[0]["试样数量"].ToString();

                        cbo_AQL_LEVEL.SelectedValue= dt.Rows[0]["AQL级别"].ToString();
                        
                        cbo_reference_level.SelectedValue = dt.Rows[0]["结果引用级别"].ToString();
                        cbo_custom_formula.SelectedValue = dt.Rows[0]["自定义公式类型"].ToString();

                        cbo_currency_formula.SelectedValue = dt.Rows[0]["通用公式类型"].ToString();
                         
                        cbo_type.SelectedValue = dt.Rows[0]["类型"].ToString();
                        cbo_typeno_val = dt.Rows[0]["类型"].ToString();
                        unit_val = dt.Rows[0]["单位"].ToString();
                    }
                    
                    //加载视图
                    GetData(Codeid);
                     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_keep_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txt_sample_num.Text.Trim())>200)
                {
                    MessageBox.Show("试样数量不能大于200");
                    return;
                }
                if (NotNull.Trues(
                    txt_testitem_name.Text,
                    cbo_reference_level.Text,
                    txt_testtype_no.Text,
                    txt_unit.Text,
                    txt_testitem_code.Text,
                    txt_sample_num.Text,
                    cbo_type.Text,
                    cbo_AQL_LEVEL.Text)
                   )
                {
                    throw new Exception("必填项不能为空，请检查！");
                }

                //添加操作
                if (string.IsNullOrEmpty(Codeid))
                { 
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("testitem_name", txt_testitem_name.Text.Trim());
                    p.Add("reference_level", cbo_reference_level.SelectedValue);
                    p.Add("currency_formula", cbo_currency_formula.SelectedValue);
                    p.Add("testtype_no", txt_testtype_no.Text.Trim());
                    p.Add("unit", txt_unit.Text.Trim());
                    p.Add("custom_formula", cbo_custom_formula.SelectedValue);
                    p.Add("testitem_code", txt_testitem_code.Text.Trim());
                    p.Add("sample_num", txt_sample_num.Text.Trim());
                    p.Add("cbo_type", cbo_type.SelectedValue);
                    p.Add("remarks", richTextBox_remarks.Text.Trim());

                    p.Add("AQL_LEVEL", cbo_AQL_LEVEL.SelectedValue);
                  /*  p.Add("AC", txt_AC.Text.Trim());
                    p.Add("RE", txt_RE.Text.Trim());*/

                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                               "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.BDMBASE",//类名
                                                "GetBDM_TESTITEMAdd",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        MessageBox.Show(ret.ErrMsg);
                    }
                    else
                    {
                        MessageBox.Show("添加数据成功");
                        txt_testitem_code.ReadOnly = true;
                        Codeid = txt_testitem_code.Text.ToString().Trim();
                        cbo_typeno_val = cbo_type.SelectedValue.ToString().Trim();
                        unit_val = txt_unit.Text;
                        GetData(txt_testitem_code.Text.Trim());
                    }
                }
                //修改
                else
                { 
                    if (!cbo_typeno_val.Equals(cbo_type.SelectedValue.ToString()) && this.dataGridView1.Rows.Count>0)
                    {
                        if(MessageBox.Show("修改类型则原有的明细会被清空，是否确认？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    //请求api的数据展示
                    Dictionary<string, object> p = new Dictionary<string, object>(); 
                    p.Add("testitem_name", txt_testitem_name.Text.Trim());
                    p.Add("reference_level", cbo_reference_level.SelectedValue);
                    p.Add("currency_formula", cbo_currency_formula.SelectedValue);
                    p.Add("testtype_no", txt_testtype_no.Text.Trim());
                    p.Add("unit", txt_unit.Text.Trim());
                    p.Add("custom_formula", cbo_custom_formula.SelectedValue);
                    p.Add("testitem_code", txt_testitem_code.Text.Trim());
                    p.Add("sample_num", txt_sample_num.Text.Trim());
                    p.Add("cbo_type", cbo_type.SelectedValue);
                    p.Add("Codeid", Codeid.Trim());
                    p.Add("testtype_name", txt_testtype_name.Trim());
                    p.Add("remarks", richTextBox_remarks.Text.Trim());

                    p.Add("AQL_LEVEL", cbo_AQL_LEVEL.SelectedValue);
                 /*   p.Add("AC", txt_AC.Text.Trim());
                    p.Add("RE", txt_RE.Text.Trim());*/
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_QCMAPI",//类库名
                                                "SJ_QCMAPI.BDMBASE",//类名
                                                "GetBDM_TESTITEMUpdate",//方法名
                                                Program.Client.UserToken,//token
                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (!ret.IsSuccess)
                    {
                        MessageBox.Show(ret.ErrMsg);
                    }
                    else
                    {
                        MessageBox.Show("修改数据成功");
                        cbo_typeno_val = cbo_type.SelectedValue.ToString();
                        unit_val = txt_unit.Text;
                        GetData(txt_testitem_code.Text.Trim());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //公式按钮判断添加
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Codeid))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请先保存检验项目信息再新增！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                 
                using (F_QCM_Testltem_AddEX add = new F_QCM_Testltem_AddEX(cbo_typeno_val, Codeid,unit_val))
                {
                    add.ShowDialog();
                    GetData(Codeid);
                }
                 
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        private void txt_testtype_no_Click(object sender, EventArgs e)
        { 
            string sql = "select TESTTYPE_NO as 检测项类型编号,TESTTYPE_NAME as 检测项类型名称 from BDM_TESTTYPE_M";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_testtype_no.Text = frmData.RetData.Rows[0]["检测项类型编号"].ToString();
                txt_testtype_name = frmData.RetData.Rows[0]["检测项类型名称"].ToString();
            }
        }

        private void txt_sample_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }



        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

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
                      
                        if (cell.CurrentItem.Equals("DELETE"))//删除
                        {
                            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                                string id = Convert.ToString(dataGridView1.CurrentRow.Cells["id"].Value);

                                // 新增测试项数据
                                try
                                {
                                    //请求api的数据展示
                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("data", id);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                                Program.Client.APIURL,
                                                                "SJ_QCMAPI",//类库名
                                                                "SJ_QCMAPI.BDMBASE",//类名
                                                                "GetBDM_TESTITEMCodeDelect",//方法名
                                                                Program.Client.UserToken,//token
                                                                Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("删除数据成功");
                                        GetData(Codeid);
                                    }
                                }
                                catch (Exception ex)
                                {

                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
