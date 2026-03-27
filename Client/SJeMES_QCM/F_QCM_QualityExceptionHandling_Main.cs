using DataGrid.DataGridViewCustomColumn;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_QualityExceptionHandling_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_QualityExceptionHandling_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();

            InitDateTimePicker(dtp1);
            InitDateTimePicker(dtp2);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void F_QCM_QualityExceptionHandling_Main_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.dtp1.Format = DateTimePickerFormat.Custom;
            this.dtp1.CustomFormat = " ";
            this.dtp2.Format = DateTimePickerFormat.Custom;
            this.dtp2.CustomFormat = " ";
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
            GetDataOrg();
            GetDataDepartment();
            GetDataDepartment2();
            GetProblemLevel();
        }

        /// <summary>
        /// 视图展示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetProblemLevel()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.AbnormalReport",//类名
                                            "SearchProblemLevel",//方法名
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
                if (dt.Rows.Count > 0)
                {
                    cbo_QUALITY_PROBLEM_LEVEL.DataSource = dt;
                    cbo_QUALITY_PROBLEM_LEVEL.ValueMember = "ENUM_CODE";
                    cbo_QUALITY_PROBLEM_LEVEL.DisplayMember = "ENUM_VALUE";
                    cbo_QUALITY_PROBLEM_LEVEL.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        /// <summary>
        /// 视图展示
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string start_date1 = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dtp1.Text))
                {
                    start_date1 = Convert.ToDateTime(this.dtp1.Value).ToString("yyyy-MM-dd");
                }
                string start_date2 = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dtp2.Text))
                {
                    start_date2 = Convert.ToDateTime(this.dtp2.Value).ToString("yyyy-MM-dd");
                }
                //请求api的数据展示
                SearchAbnormalReportCSReqDto lis1 = new SearchAbnormalReportCSReqDto();
                lis1.PROBLEM_DES = txt_PROBLEM_DES.Text.Trim();
                lis1.PROD_NO = txt_PROD_NO.Text.Trim();
                lis1.PRODUCTION_MONTH_START = start_date1.ToString();
                lis1.PRODUCTION_MONTH_END = start_date2.ToString();
                switch (cbo_QUALITY_PROBLEM_LEVEL.Text)
                {
                    case "普通品质问题":
                        cbo_QUALITY_PROBLEM_LEVEL.Text = "0";
                        break;
                    case "严重品质问题":
                        cbo_QUALITY_PROBLEM_LEVEL.Text = "1";
                        break;
                    case "批量/重大品质问题":
                        cbo_QUALITY_PROBLEM_LEVEL.Text = "2";
                        break;
                }
                lis1.QUALITY_PROBLEM_LEVEL = cbo_QUALITY_PROBLEM_LEVEL.Text;
                if (cbo_ORG_CODE.SelectedIndex.ToString() != "-1")
                {
                    lis1.ORG_CODE = cbo_ORG_CODE.SelectedValue.ToString();
                }

                if (cbo_PRO_DEPARTMENT_NAME.SelectedIndex.ToString() != "-1")
                {
                    lis1.PRO_DEPARTMENT_NO = cbo_PRO_DEPARTMENT_NAME.SelectedValue.ToString();
                }
                if (cbo_RESPONSIBLE_DEPARTMENT_NAME.SelectedIndex.ToString() != "-1")
                {
                    lis1.RESPONSIBLE_DEPARTMENT_NO = cbo_RESPONSIBLE_DEPARTMENT_NAME.SelectedValue.ToString();
                }
                if (cbo_QUALITY_PROBLEM_LEVEL.SelectedIndex.ToString() != "-1")
                {
                    lis1.QUALITY_PROBLEM_LEVEL = cbo_QUALITY_PROBLEM_LEVEL.SelectedValue.ToString();
                }


                lis1.pageSize = pageSize.ToString();
                lis1.pageIndex = pageIndex.ToString();

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.AbnormalReport",//类名
                                            "SearchAbnormalReportCS",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(lis1));
                Dictionary<string, object> ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (!Convert.ToBoolean(ret["IsSuccess"]))
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData1"].ToString());
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                        dgvr.Cells["PRO_DEPARTMENT_NAME"].Value = dr["PRO_DEPARTMENT_NAME"].ToString();
                        dgvr.Cells["RESPONSIBLE_DEPARTMENT_NAME"].Value = dr["RESPONSIBLE_DEPARTMENT_NAME"].ToString();
                        switch (dr["QUALITY_PROBLEM_LEVEL"])
                        {
                            case "0":
                                dr["QUALITY_PROBLEM_LEVEL"] = "普通品质问题";
                                break;
                            case "1":
                                dr["QUALITY_PROBLEM_LEVEL"] = "严重品质问题";
                                break;
                            case "2":
                                dr["QUALITY_PROBLEM_LEVEL"] = "批量/重大品质问题";
                                break;
                            default:
                                break;
                        }
                        dgvr.Cells["QUALITY_PROBLEM_LEVEL"].Value = dr["QUALITY_PROBLEM_LEVEL"].ToString();
                        dgvr.Cells["PRODUCTION_MONTH"].Value = dr["PRODUCTION_MONTH"].ToString();
                        dgvr.Cells["PROBLEM_DES"].Value = dr["PROBLEM_DES"].ToString();
                        switch (dr["STATUS"])
                        {
                            case "0":
                                dr["STATUS"] = "未结案";
                                break;
                            case "1":
                                dr["STATUS"] = "结案";
                                break;
                            case "2":
                                dr["STATUS"] = "公开";
                                break;
                            default:
                                break;
                        }
                        dgvr.Cells["STATUS"].Value = dr["STATUS"].ToString();
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["PROBLEM_GUID_IMG"].Value = dr["PROBLEM_GUID_IMG"].ToString();
                        dgvr.Cells["SOLVE_GUID_IMG"].Value = dr["SOLVE_GUID_IMG"].ToString();

                        //cbo_QUALITY_PROBLEM_LEVEL.Items.Add(dr["QUALITY_PROBLEM_LEVEL"]);
                        i++;
                    }
                }
                totalCount = int.Parse(dic["total"].ToString());
                GenClass.AutoSizeColumn(dataGridView1);

                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void GetDataOrg()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BASE",//类名
                                            "GetOrg",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (string.IsNullOrEmpty(ret["IsSuccess"].ToString()))
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }

                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData1"].ToString());
                if (dt.Rows.Count > 0)
                {
                    cbo_ORG_CODE.DataSource = dt;
                    cbo_ORG_CODE.ValueMember = "ORG_CODE";
                    cbo_ORG_CODE.DisplayMember = "ORG_NAME";
                    cbo_ORG_CODE.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void GetDataDepartment()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BASE",//类名
                                            "GetDepartment",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (string.IsNullOrEmpty(ret["IsSuccess"].ToString()))
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }

                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData1"].ToString());
                if (dt.Rows.Count > 0)
                {
                    cbo_PRO_DEPARTMENT_NAME.DataSource = dt;
                    cbo_PRO_DEPARTMENT_NAME.ValueMember = "DEPARTMENT_NO";
                    cbo_PRO_DEPARTMENT_NAME.DisplayMember = "DEPARTMENT_NAME";
                    cbo_PRO_DEPARTMENT_NAME.SelectedIndex = -1;


                    cbo_RESPONSIBLE_DEPARTMENT_NAME.DataSource = dt;
                    cbo_RESPONSIBLE_DEPARTMENT_NAME.ValueMember = "DEPARTMENT_NO";
                    cbo_RESPONSIBLE_DEPARTMENT_NAME.DisplayMember = "DEPARTMENT_NAME";
                    cbo_RESPONSIBLE_DEPARTMENT_NAME.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDataDepartment2()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BASE",//类名
                                            "GetDepartment",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (string.IsNullOrEmpty(ret["IsSuccess"].ToString()))
                {
                    throw new Exception(ret["ErrMsg"].ToString());
                }

                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData1"].ToString());
                if (dt.Rows.Count > 0)
                {
                    cbo_RESPONSIBLE_DEPARTMENT_NAME.DataSource = dt;
                    cbo_RESPONSIBLE_DEPARTMENT_NAME.ValueMember = "DEPARTMENT_NO";
                    cbo_RESPONSIBLE_DEPARTMENT_NAME.DisplayMember = "DEPARTMENT_NAME";
                    cbo_RESPONSIBLE_DEPARTMENT_NAME.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
                        if (cell.CurrentItem.Equals("DETAIL"))
                        {
                            string ID = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                            string PROBLEM_GUID_IMG = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                            string SOLVE_GUID_IMG = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();


                            DataTable dt = new DataTable();
                            dt.Columns.Add("ID", typeof(string));
                            dt.Columns.Add("PROBLEM_GUID_IMG", typeof(string));
                            dt.Columns.Add("SOLVE_GUID_IMG", typeof(string));

                            DataRow dr = dt.NewRow();

                            dr["ID"] = ID;
                            dr["PROBLEM_GUID_IMG"] = PROBLEM_GUID_IMG;
                            dr["SOLVE_GUID_IMG"] = SOLVE_GUID_IMG;



                            dt.Rows.Add(dr);
                            F_QCM_QualityExceptionHandling_Detail add = new F_QCM_QualityExceptionHandling_Detail(dt);
                            add.ShowDialog();
                            FormLoad();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public class SearchAbnormalReportCSReqDto : SearchAbnormalReportReqDto
        {
            /// <summary>
            /// 发生部门（生产工段）
            /// </summary>
            public string PRO_DEPARTMENT_NO { get; set; }
        }
        public class SearchAbnormalReportReqDto
        {
            /// <summary>
            /// 页码
            /// </summary>
            public string pageIndex { get; set; }
            /// <summary>
            /// 每页行数
            /// </summary>
            public string pageSize { get; set; }

            /// <summary>
            /// 问题描述
            /// </summary>
            public string PROBLEM_DES { get; set; }
            /// <summary>
            /// ART
            /// </summary>
            public string PROD_NO { get; set; }
            /// <summary>
            /// 日期范围 开始
            /// </summary>
            public string PRODUCTION_MONTH_START { get; set; }
            /// <summary>
            /// 日期范围 结束
            /// </summary>
            public string PRODUCTION_MONTH_END { get; set; }
            /// <summary>
            /// 厂区
            /// </summary>
            public string ORG_CODE { get; set; }
            /// <summary>
            /// 问题级别
            /// </summary>
            public string QUALITY_PROBLEM_LEVEL { get; set; }
            /// <summary>
            /// 责任部门
            /// </summary>
            public string RESPONSIBLE_DEPARTMENT_NO { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLoad();
            cbo_QUALITY_PROBLEM_LEVEL.SelectedIndex = -1;
            cbo_ORG_CODE.SelectedIndex = -1;
            cbo_PRO_DEPARTMENT_NAME.SelectedIndex = -1;
            cbo_RESPONSIBLE_DEPARTMENT_NAME.SelectedIndex = -1;
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
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
