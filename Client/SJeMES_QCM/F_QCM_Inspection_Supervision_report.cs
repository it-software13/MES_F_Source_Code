using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_Inspection_Supervision_report : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Inspection_Supervision_report()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitDateTimePicker(SPOTCHECK_DATE_START);
            InitDateTimePicker(SPOTCHECK_DATE_END);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                string SPOTCHECK_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["SPOTCHECK_NO"].Value);
                string VEND_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["VEND_NO"].Value);
                string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                        return;
                    if (cell.CurrentItem.Equals("selectbtn"))
                    {

                        F_QCM_Inspection_Supervision_report_Detail F_QCM_Inspection_Supervision_report_Detail = new F_QCM_Inspection_Supervision_report_Detail(SPOTCHECK_NO, VEND_NO);
                        F_QCM_Inspection_Supervision_report_Detail.ShowDialog();
                    }
                    
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void F_QCM_Inspection_Supervision_report_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += SearchData;
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        //查询方法
        public void SearchData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                string start_date = string.Empty;
                string end_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.SPOTCHECK_DATE_START.Text))
                {
                    start_date = Convert.ToDateTime(this.SPOTCHECK_DATE_START.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(this.SPOTCHECK_DATE_END.Text))
                {
                    end_date = Convert.ToDateTime(this.SPOTCHECK_DATE_END.Value).ToString("yyyy-MM-dd");
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("SPOTCHECK_DATE_START", start_date);
                data.Add("SPOTCHECK_DATE_END", end_date);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);


                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.SpotCheck", "GetSpotCheckList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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
                            dgvr.Cells["SPOTCHECK_NO"].Value = dr["SPOTCHECK_NO"].ToString();
                            dgvr.Cells["INSPECT_METHOD"].Value = dr["INSPECT_METHOD"].ToString();
                            dgvr.Cells["VEND_NO"].Value = dr["VEND_NO"].ToString();
                            dgvr.Cells["VEND_NAME"].Value = dr["VEND_NAME"].ToString();

                            dgvr.Cells["PART_NO"].Value = dr["PART_NO"].ToString();
                            dgvr.Cells["SHOE_NOS"].Value = dr["SHOE_NOS"].ToString();
                            dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                            dgvr.Cells["PO_ORDER"].Value = dr["PO_ORDER"].ToString();
                            dgvr.Cells["CODE_NUMBER"].Value = dr["CODE_NUMBER"].ToString();
                            dgvr.Cells["SPOTCHECK_DATE"].Value = dr["SPOTCHECK_DATE"].ToString();
                            dgvr.Cells["PO_QTY"].Value = dr["PO_QTY"].ToString();
                            dgvr.Cells["PLANSAMP_QTY"].Value = dr["PLANSAMP_QTY"].ToString();
                            dgvr.Cells["PROCESS_TYPE"].Value = dr["PROCESS_TYPE"].ToString();
                            dgvr.Cells["NG_QTY"].Value = dr["NG_QTY"].ToString();
                            dgvr.Cells["STATUS"].Value = dr["STATUS"].ToString();
                            i++;
                        }
                        totalCount = int.Parse(dic["rowCount"].ToString());
                        this.dataGridView1.ClearSelection();
                        this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
                        GenClass.AutoSizeColumn(dataGridView1);
                    }
                    

                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            F_QCM_Inspection_Supervision_report_Add f_QCM_Inspection_Supervision_Report_Add = new F_QCM_Inspection_Supervision_report_Add(this);
            f_QCM_Inspection_Supervision_Report_Add.ShowDialog();
        }

        private void Importbtn_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            OpenFileDialog ofd = new OpenFileDialog();
            //判断选择的路径
            string path = string.Empty;
            ofd.Title = "请选择文件";
            ofd.Filter = "EXECL|*.xlsx;*.xls";
            string SafeFileName = "";
            string filePath = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SafeFileName = Path.GetExtension(ofd.FileName);
                filePath = ofd.FileName;
            }
            if (!string.IsNullOrEmpty(filePath))
            {
                if (SafeFileName != ".xlsx" && SafeFileName != ".xls")
                {
                    MessageBox.Show("文件类型错误,请选择(.xlsx,.xls)类型文件");
                    return;
                }
                DataTable dt = SJeMES_Framework.Common.NPOIHelper.ExcelToTable(filePath);
                if (dt.Columns.Count != 15 || dt.Columns[dt.Columns.Count - 1].ColumnName != "状态")
                {
                    MessageBox.Show("导入模板错误,请查阅");
                    return;
                }
                if (dt != null)
                {
                    SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                    bool is_sure = frm.is_sure;
                    if (is_sure)
                    {
                        //请求api的数据展示
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("SOURCE", dt);
                        p.Add("import_type", 6);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.BASE",//类名
                                                    "ImportData",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (ret.IsSuccess)
                        {
                            MessageBox.Show("导入成功");
                            this.F_QCM_Inspection_Supervision_report_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show(ret.ErrMsg);
                        }
                    }
                }
            }
        }

        public static DataTable GetExcelTableName(string p_ExcelFile)
        {
            try
            {
                if (System.IO.File.Exists(p_ExcelFile))
                {
                    OleDbConnection _ExcelConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + p_ExcelFile);
                    _ExcelConn.Open();
                    DataTable _Table = _ExcelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    _ExcelConn.Close();
                    return _Table;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private void Modelbtn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("检验单号");
                dt.Columns.Add("检验方式");
                dt.Columns.Add("厂商代号");
                dt.Columns.Add("厂商");
                dt.Columns.Add("部件");
                dt.Columns.Add("鞋型名称");
                dt.Columns.Add("Article");
                dt.Columns.Add("PO");
                dt.Columns.Add("码数");
                dt.Columns.Add("检验日期");
                dt.Columns.Add("生产数量(双)");
                dt.Columns.Add("抽检数(双)");
                dt.Columns.Add("工艺类型");
                dt.Columns.Add("总不良数(件)");
                dt.Columns.Add("状态");

                FolderBrowserDialog ofd = new FolderBrowserDialog();
                ofd.ShowDialog();
                string path = ofd.SelectedPath;
                SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"抽检品质监督导入模板{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx");
                MessageBox.Show("下载成功");
            }
            catch
            {
                MessageBox.Show("下载失败");
            }
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
