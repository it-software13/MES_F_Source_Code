using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Controls.Btn;
using SJeMES_Control_Library.Controls.DataGridView;
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

namespace SJeMES_QCM_Inspection
{
    public partial class F_QCM_InspectionResult : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        int page = 0;//记录页数
        public F_QCM_InspectionResult()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            InitDateTimePicker(start_date);
            InitDateTimePicker(end_date);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        string Type = "0"; // 0-检测中 //1-已完成

        //查询按钮
        private void Searchbtn(object sender, EventArgs e)
        { 
            if(Type == "0")//检测中查询
            { 
                pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
                pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
                pageControl1.SetPage(); 
            }
            else // 已完成
            { 
                pageControl2.PageSize = int.Parse(enum_page.enum_PageSize);
                pageControl2.PageIndex = int.Parse(enum_page.PageIndex); 
                pageControl2.SetPage(); 
            } 
        }
        //检测中按钮
        

        // 初始化界面
        private void Result_Load(object sender, EventArgs e)
        {
            #region 加载操作按钮
            //根据界面功能加载
            //List<ActionButton> list = new List<ActionButton>();
            ////list.Add(ActionButtonDefaultConfig.GetUpdateBtnConfig());//修改
            ////list.Add(ActionButtonDefaultConfig.GetDeleteBtnConfig());//删除
            //list.Add(ActionButtonDefaultConfig.GetDetailBtnConfig());//查看明细
            //list.Add(ActionButtonDefaultConfig.GetPrintBtnConfig());//打印
            ////list.Add(ActionButtonDefaultConfig.GetUploadIMGBtnConfig());//上传图片
            ////list.Add(ActionButtonDefaultConfig.GetUploadFileBtnConfig()); //上传文件
            //DataGridViewActionButtonColumn dataGridViewColumn = new DataGridViewActionButtonColumn(list);

            //dataGridViewColumn.Width = 80;
            //dataGridViewColumn.HeaderText = "操作";
            //dataGridViewColumn.Name = "operation";
            //dataGridViewColumn.Resizable = DataGridViewTriState.False;
            //dataGridViewColumn.SortMode= DataGridViewColumnSortMode.NotSortable;
            //this.dataGridView1.Columns.Add(dataGridViewColumn);
            //this.dataGridView1.Columns["operation"].DisplayIndex = 0;//设置列在最左侧
            //this.dataGridView1.Columns["operation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //this.dataGridView1.Columns["operation"].Frozen = true;//设置列冻结
            //this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent; 

            #endregion

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.start_date.Format = DateTimePickerFormat.Custom;
            this.start_date.CustomFormat = " ";

            this.end_date.Format = DateTimePickerFormat.Custom;
            this.end_date.CustomFormat = " ";

            //只要加载一次委托 
            pageControl1.BindPageEvent += BindPage1; 
             
            pageControl2.BindPageEvent += BindPage2; 

        }
        
        /// <summary>
        /// 扫描员工工号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void staff_no_scan(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                
                pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
                pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
                pageControl1.SetPage();

                pageControl2.PageSize = int.Parse(enum_page.enum_PageSize);
                pageControl2.PageIndex = int.Parse(enum_page.PageIndex);
                pageControl2.SetPage();
            }
        }

        //记录当前页签
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "testpage")
            {
                Type = "0";
            }
            if (tabControl1.SelectedTab.Name == "finishpage")
            {
                Type = "1";
            }
            
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 查询检测中的数据
        /// </summary>
        /// <param name="STAFF_NO"></param>
        public void BindPage1(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string STAFF_NO = this.txt_staff_no.Text;
                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Type", "0");
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex); 
                data.Add("STAFF_NO", string.IsNullOrEmpty(STAFF_NO)? txt_people_no.Text.Trim(): STAFF_NO); 
                data.Add("INSPECTION_NO", this.txt_No.Text);
                data.Add("ART", this.txt_Art.Text);
                data.Add("start_date", start_date);
                data.Add("end_date", end_date);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.InspectionResult", "GetCheckResult",
                     Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    dataGridView1.Rows.Clear();
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["INSPECTION_NO"].Value = dr["INSPECTION_NO"].ToString();
                            dgvr.Cells["GENERAL_TESTTYPE_NAME"].Value = dr["GENERAL_TESTTYPE_NAME"].ToString();
                            dgvr.Cells["ART_CODE"].Value = dr["ART_CODE"].ToString();

                            dgvr.Cells["DEPARTMENT_NO"].Value = dr["DEPARTMENT_NO"].ToString();
                            dgvr.Cells["CATEGORY_NAME"].Value = dr["CATEGORY_NAME"].ToString();
                            dgvr.Cells["STAFF_NAME"].Value = dr["STAFF_NAME"].ToString();
                            dgvr.Cells["DEPARTMENT_NAME"].Value = dr["DEPARTMENT_NAME"].ToString();
                            dgvr.Cells["PLANTAREA_NAME"].Value = dr["PLANTAREA_NAME"].ToString();
                            dgvr.Cells["PRODUCTIONLINE_NAME"].Value = dr["PRODUCTIONLINE_NAME"].ToString();
                            dgvr.Cells["CHECK_RESULT"].Value = dr["CHECK_RESULT"].ToString();
                            dgvr.Cells["INSPECTION_DATE"].Value = dr["INSPECTION_DATE"].ToString();
                            i++;
                        }
                    }
                    totalCount = int.Parse(dic["rowCount"].ToString());
                    GenClass.AutoSizeColumn(dataGridView1);
                    this.splitContainer1.Visible = true;
                }

                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        /// <summary>
        /// 查询已完成的数据
        /// </summary>
        public void BindPage2(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string STAFF_NO = this.txt_staff_no.Text;
                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.end_date.Text))
                {
                    end_date = Convert.ToDateTime(this.end_date.Value).ToString("yyyy-MM-dd");
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Type", "1");
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex); 
                data.Add("STAFF_NO", string.IsNullOrEmpty(STAFF_NO) ? txt_people_no.Text.Trim() : STAFF_NO);
                data.Add("INSPECTION_NO", this.txt_No.Text);
                data.Add("ART", this.txt_Art.Text);
                data.Add("start_date", start_date);
                data.Add("end_date", end_date);
                 
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.InspectionResult", "GetCheckResult", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                    throw new Exception(ret.ErrMsg);
                else
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                    dataGridView2.Rows.Clear();
                    var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dataGridView2.Rows.Add();
                            DataGridViewRow dgvr = dataGridView2.Rows[i];
                            dgvr.Cells["INSPECTION_NO2"].Value = dr["INSPECTION_NO"].ToString();
                            dgvr.Cells["GENERAL_TESTTYPE_NAME2"].Value = dr["GENERAL_TESTTYPE_NAME"].ToString();
                            dgvr.Cells["ART_CODE2"].Value = dr["ART_CODE"].ToString();

                            dgvr.Cells["DEPARTMENT_NO2"].Value = dr["DEPARTMENT_NO"].ToString();
                            dgvr.Cells["CATEGORY_NAME2"].Value = dr["CATEGORY_NAME"].ToString();
                            dgvr.Cells["STAFF_NAME2"].Value = dr["STAFF_NAME"].ToString();
                            dgvr.Cells["DEPARTMENT_NAME2"].Value = dr["DEPARTMENT_NAME"].ToString();
                            dgvr.Cells["PLANTAREA_NAME2"].Value = dr["PLANTAREA_NAME"].ToString();
                            dgvr.Cells["INSPECTION_DATE2"].Value = dr["INSPECTION_DATE"].ToString();
                            i++;
                        }
                    }
                    this.splitContainer1.Visible = true;
                    totalCount = int.Parse(dic["rowCount"].ToString());
                }
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                GenClass.AutoSizeColumn(dataGridView2);

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        //点击按钮事件2
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string INSPECTION_NO = Convert.ToString(dataGridView2.CurrentRow.Cells["INSPECTION_NO2"].Value);
            if (dataGridView2.Columns[e.ColumnIndex].Name == "searchbtn2" && e.RowIndex >= 0)
            {
                F_QCM_JcReport add = new F_QCM_JcReport(INSPECTION_NO);
                add.ShowDialog();
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "printbtn2" && e.RowIndex >= 0)
            {

                F_QCM_InspectionPrint add = new F_QCM_InspectionPrint(INSPECTION_NO);
                add.ShowDialog();
            }
        }

        //点击按钮事件1
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
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

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            
        }


        //检测中按钮
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                string INSPECTION_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["INSPECTION_NO"].Value);
                string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                        return;
                    if (cell.CurrentItem.Equals("selectbtn"))
                    {

                        F_QCM_ReportPrint reportPrint = new F_QCM_ReportPrint(INSPECTION_NO);
                        reportPrint.ShowDialog();
                    }
                    else if (cell.CurrentItem.Equals("printbtn"))
                    {
                        F_QCM_InspectionPrint add = new F_QCM_InspectionPrint(INSPECTION_NO);
                        add.ShowDialog();
                    }
                }
            }
            

        }

        //已完成按钮
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                string INSPECTION_NO = Convert.ToString(dataGridView2.CurrentRow.Cells["INSPECTION_NO2"].Value);
                string name = this.dataGridView2.Columns[e.ColumnIndex].Name;
                if (name == "operation2")
                {
                    DataGridViewOperationCell cell = this.dataGridView2.Rows[this.dataGridView2.CurrentRow.Index].Cells["operation2"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                        return;
                    if (cell.CurrentItem.Equals("selectbtn2"))
                    {
                        F_QCM_ReportPrint reportPrint = new F_QCM_ReportPrint(INSPECTION_NO);
                        reportPrint.ShowDialog();
                    }
                    else if (cell.CurrentItem.Equals("printbtn2"))
                    {
                        F_QCM_InspectionPrint add = new F_QCM_InspectionPrint(INSPECTION_NO);
                        add.ShowDialog();
                    }
                }
            }
        }
    }
}
