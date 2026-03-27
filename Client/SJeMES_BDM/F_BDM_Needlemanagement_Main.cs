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

namespace SJeMES_BDM
{
    public partial class F_BDM_Needlemanagement_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_Needlemanagement_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        #region 参数
        /// <summary>
        /// id
        /// </summary>
        private string _id = string.Empty;
        /// <summary>
        /// 厂区代号
        /// </summary>
        private string org_codes = string.Empty;
        /// <summary>
        /// 产线编号
        /// </summary>
        private string production_line_codes = string.Empty;
        /// <summary>
        /// 车针类别代号
        /// </summary>
        private string needle_category_nos = string.Empty;
        #endregion
        #region 日期控件初始为空值处理

        /// <summary>
        /// 初始化日期时间控件
        /// </summary>
        /// <param name="dtp"></param>
        public static void InitDateTimePicker(DateTimePicker dtp)
        {
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = " ";  //必须设置成" "
            dtp.ValueChanged -= DateTimePicker_ValueChanged;
            dtp.ValueChanged += DateTimePicker_ValueChanged;
            dtp.KeyPress -= DateTimePicker_KeyPress;
            dtp.KeyPress += DateTimePicker_KeyPress;
        }

        public static void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd"; //null;
            dtp.Checked = false;// 解决BUG ：防止日期控件不能选择相同日期的 --- 要放置在设置格式之后
        }

        public static void DateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)  // backspace左删除键
            {
                DateTimePicker dtp = (DateTimePicker)sender;
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
            }
        }
        #endregion
        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
      
        private void F_BDM_Needlemanagement_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

           /* this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";

            this.dateTimeP_end_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_end_date.CustomFormat = " ";*/
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
            this.dataGridView1.ClearSelection();
            //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {

            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("org_name", txt_org.Text);
                p.Add("production_line_name", txt_production_line.Text);
                p.Add("needle_category_name", cbo_status.Text);
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                            "BDM_Needlemanagement_View",//方法名
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
                        dgvr.Cells["org_code"].Value = dr["org_code"].ToString();
                        dgvr.Cells["org_name"].Value = dr["org_name"].ToString();
                        dgvr.Cells["production_line_code"].Value = dr["production_line_code"].ToString();
                        dgvr.Cells["production_line_name"].Value = dr["production_line_name"].ToString();
                        dgvr.Cells["needle_category_no"].Value = dr["needle_category_no"].ToString();
                        dgvr.Cells["needle_category_name"].Value = dr["needle_category_name"].ToString();
                        //dgvr.Cells["remarks"].Value = dr["remarks"].ToString();
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();


                        dgvr.Cells["ly_qty"].Value = dr["ly_qty"].ToString();//领用数量
                        dgvr.Cells["fz_qty"].Value = dr["fz_qty"].ToString();//发针数量
                        dgvr.Cells["zx_qty"].Value = dr["zx_qty"].ToString();//在线使用数量
                        dgvr.Cells["dz_qty"].Value = dr["dz_qty"].ToString();//断针数量
                        dgvr.Cells["sy_qty"].Value = dr["sy_qty"].ToString();//剩余数量


                        i++;
                    }
                }
                //GenClass.AutoSizeColumn(dataGridView1);
                totalCount = int.Parse(dic["rowCount"].ToString());
                //加载下拉框
                DataTable dt_n = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Datan"].ToString());
                if (dt_n.Rows.Count > 0)
                {
                    cbo_status.DataSource = dt_n;
                    cbo_status.ValueMember = "NEEDLE_CATEGORY_NO";
                    cbo_status.DisplayMember = "NEEDLE_CATEGORY_NAME";
                    cbo_status.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_select_Click(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txt_org.Text)||
                string.IsNullOrWhiteSpace(txt_production_line.Text)||
                string.IsNullOrWhiteSpace(cbo_status.Text)
                )
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("When adding, required fields cannot be empty！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("org_code", org_codes);
                p.Add("org_name", txt_org.Text);
                p.Add("production_line_code", production_line_codes);
                p.Add("production_line_name", txt_production_line.Text);
                p.Add("needle_category_no", cbo_status.SelectedValue);
                p.Add("needle_category_name", cbo_status.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                            "BDM_Needlemanagement_add",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("added successfully！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    txt_org.Text = "";
                    org_codes = "";
                    txt_production_line.Text = "";
                    production_line_codes = "";
                    FormLoad();
                    
                }
            }
           
           
        }
        private void txt_org_DoubleClick(object sender, EventArgs e)
        {
            //string sql = $@"select org_code 厂区代号,org_name 厂区名称 from base001m";

            string sql = $@"select org_code Factory_Code,org_name Factory_Name from base001m";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_org.Text = frmData.RetData.Rows[0]["Factory_Name"].ToString();
                org_codes = frmData.RetData.Rows[0]["Factory_Code"].ToString();
            }
        }

        private void txt_org_Click(object sender, EventArgs e)
        {
            //string sql = $@"select org_code 厂区代号,org_name 厂区名称 from base001m";

            string sql = $@"select org_code Factory_Code,org_name Factory_Name from base001m";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_org.Text = frmData.RetData.Rows[0]["Factory_Name"].ToString();
                org_codes = frmData.RetData.Rows[0]["Factory_Code"].ToString();
            }
        }

        private void txt_production_line_Click(object sender, EventArgs e)
        {
            //            string sql = $@"
            //SELECT
            //	PRODUCTION_LINE_CODE 产线代号,
            //	PRODUCTION_LINE_NAME 产线名称,
            //	REMARKS 备注
            //FROM
            //	BDM_PRODUCTION_LINE_M
            //UNION 
            //SELECT
            //	DEPARTMENT_CODE 产线代号,
            //	DEPARTMENT_NAME 产线名称,
            //	'' 备注
            //FROM
            //	base005m";

            string sql = $@"
SELECT
	PRODUCTION_LINE_CODE Production_Line_Code,
	PRODUCTION_LINE_NAME Production_Line_Name,
	REMARKS Remark
FROM
	BDM_PRODUCTION_LINE_M
UNION 
SELECT
	DEPARTMENT_CODE Production_Line_Code,
	DEPARTMENT_NAME Production_Line_Name,
	'' Remark
FROM
	base005m";

            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                txt_production_line.Text = frmData.RetData.Rows[0]["Production_Line_Name"].ToString();
                production_line_codes = frmData.RetData.Rows[0]["Production_Line_Code"].ToString();

            }
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
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    switch (dataGridView1.Columns[e.ColumnIndex].Name)
                    {
                        case "delete":
                            string id = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                            delete_top(id);
                            FormLoad();
                            break;
                        case "btn_fz":
                            dic = new Dictionary<string, object>();
                            dic.Add("needle_category_no", dataGridView1.CurrentRow.Cells["needle_category_no"].Value.ToString());
                            dic.Add("needle_category_name", dataGridView1.CurrentRow.Cells["needle_category_name"].Value.ToString());
                            dic.Add("id", dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
                            using (F_BDM_NSendneedlerecords add=new F_BDM_NSendneedlerecords(dic))
                            {
                                add.ShowDialog();
                                FormLoad();
                            }
                                break;
                        case "btn_dz":

                            dic = new Dictionary<string, object>();
                            dic.Add("needle_category_no", dataGridView1.CurrentRow.Cells["needle_category_no"].Value.ToString());
                            dic.Add("needle_category_name", dataGridView1.CurrentRow.Cells["needle_category_name"].Value.ToString());
                            dic.Add("id", dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
                            using (F_BDM_NWiththemanagement add = new F_BDM_NWiththemanagement(dic))
                            {
                                add.ShowDialog();
                                FormLoad();
                            }
                            break;
                        case "btn_ly":

                            dic = new Dictionary<string, object>();
                            dic.Add("org_name", dataGridView1.CurrentRow.Cells["org_name"].Value.ToString());
                            dic.Add("production_line_name", dataGridView1.CurrentRow.Cells["production_line_name"].Value.ToString());
                            dic.Add("needle_category_name", dataGridView1.CurrentRow.Cells["needle_category_name"].Value.ToString());
                            dic.Add("id", dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
                            using (F_BDM_Needlemanagement_Mainmin add=new F_BDM_Needlemanagement_Mainmin(dic))
                            {
                                add.ShowDialog();
                                FormLoad();
                            }
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void delete_top(string id)
        {
            if (MessageBox.Show("confirm deletion? ", "This delete cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("id", id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Needlemanagement",//类名
                                            "BDM_Needlemanagement_delete",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    MessageBox.Show(ret.ErrMsg);
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    FormLoad();
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_org.Text = "";
            org_codes = "";
            txt_production_line.Text = "";
            production_line_codes = "";
            FormLoad();
        }
    }
}
