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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_BadReturnMain : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_BadReturnMain()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            InitDateTimePicker(start_date);


        }

        public void FormLoad()
        {

            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void F_QCM_BadReturn_Main_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.start_date.Format = DateTimePickerFormat.Custom;
            this.start_date.CustomFormat = " ";
            pageControl1.BindPageEvent += GetDataList;
            FormLoad();
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
                string start_date1 = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.start_date.Text))
                {
                    start_date1 = Convert.ToDateTime(this.start_date.Value).ToString("yyyy-MM-dd");
                }
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();

                p.Add("RETURN_DATE", start_date1.ToString());
                p.Add("PROD_NO", txt_art.Text.Trim().ToString());
                p.Add("SHOE_NO", txt_shoe_nos.Text.Trim().ToString());

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BadReturnBase",//类名
                                            "GetBadReturnList",//方法名
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
                datagridview1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        datagridview1.Rows.Add();
                        DataGridViewRow dgvr = datagridview1.Rows[i];
                        dgvr.Cells["RETURN_NO"].Value = dr["RETURN_NO"].ToString();
                        dgvr.Cells["RETURN_DATE"].Value = dr["RETURN_DATE"].ToString();
                        dgvr.Cells["PLANT_AREA"].Value = dr["PLANT_AREA"].ToString();
                        dgvr.Cells["ORDER_QTY"].Value = dr["ORDER_QTY"].ToString();

                        dgvr.Cells["TURNOVER_QTY"].Value = dr["TURNOVER_QTY"].ToString();
                        dgvr.Cells["B_QTY"].Value = dr["B_QTY"].ToString();
                        dgvr.Cells["RETURN_FREQUENCY"].Value = dr["RETURN_FREQUENCY"].ToString();
                        dgvr.Cells["AFFECT_HOURS"].Value = dr["AFFECT_HOURS"].ToString();
                        dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString();
                        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.datagridview1.ClearSelection();

                GenClass.AutoSizeColumn(datagridview1);

                this.datagridview1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            F_QCM_BadReturnAdd add = new F_QCM_BadReturnAdd();
            add.ShowDialog();
            FormLoad();
        }

        private void Dgv_BadReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    string name = this.datagridview1.Columns[e.ColumnIndex].Name;
                    if (name == "operation")
                    {
                        DataGridViewOperationCell cell = this.datagridview1.Rows[this.datagridview1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("DETAIL"))
                        {
                            string RETURN_NO = datagridview1.Rows[e.RowIndex].Cells[1].Value.ToString();
                            string RETURN_DATE = datagridview1.Rows[e.RowIndex].Cells[2].Value.ToString();
                            string PLANT_AREA = datagridview1.Rows[e.RowIndex].Cells[3].Value.ToString();
                            string ORDER_QTY = datagridview1.Rows[e.RowIndex].Cells[4].Value.ToString();
                            string TURNOVER_QTY = datagridview1.Rows[e.RowIndex].Cells[5].Value.ToString();
                            string B_QTY = datagridview1.Rows[e.RowIndex].Cells[6].Value.ToString();
                            string RETURN_FREQUENCY = datagridview1.Rows[e.RowIndex].Cells[7].Value.ToString();
                            string SHOE_NO = datagridview1.Rows[e.RowIndex].Cells[9].Value.ToString();

                            DataTable dt = new DataTable();
                            dt.Columns.Add("RETURN_NO", typeof(string));
                            dt.Columns.Add("RETURN_DATE", typeof(string));
                            dt.Columns.Add("PLANT_AREA", typeof(string));
                            dt.Columns.Add("ORDER_QTY", typeof(string));
                            dt.Columns.Add("TURNOVER_QTY", typeof(string));
                            dt.Columns.Add("B_QTY", typeof(string));
                            dt.Columns.Add("RETURN_FREQUENCY", typeof(string));
                            dt.Columns.Add("SHOE_NO", typeof(string));

                            DataRow dr = dt.NewRow();

                            dr["RETURN_NO"] = RETURN_NO;
                            dr["RETURN_DATE"] = RETURN_DATE;
                            dr["PLANT_AREA"] = PLANT_AREA;
                            dr["ORDER_QTY"] = ORDER_QTY;
                            dr["TURNOVER_QTY"] = TURNOVER_QTY;
                            dr["B_QTY"] = B_QTY;
                            dr["RETURN_FREQUENCY"] = RETURN_FREQUENCY;
                            dr["SHOE_NO"] = SHOE_NO;

                            dt.Rows.Add(dr);
                            F_QCM_BadReTurn_Detail add = new F_QCM_BadReTurn_Detail(dt);
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

        private void btn_select_Click(object sender, EventArgs e)
        {
            FormLoad();
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

        private void btn_return_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void datagridview1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void btn_inport_Click(object sender, EventArgs e)
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
                if (dt.Columns.Count != 10 || dt.Columns[dt.Columns.Count - 1].ColumnName != "ART")
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
                        try
                        {
                            //请求api的数据展示
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SOURCE", dt);
                            p.Add("import_type", 7);
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
                            }
                            else
                            {
                                MessageBox.Show(ret.ErrMsg);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void btnsc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("上传成功!");
        }

        //dgv转datatable
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void btn_inportmode_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDgvToTable(datagridview1);

            Dictionary<string, string> Execldic = new Dictionary<string, string>();
            Execldic.Add("RETURN_NO", "退货单号");
            Execldic.Add("RETURN_DATE", "退货日期");
            Execldic.Add("PLANT_AREA", "厂区");
            Execldic.Add("ORDER_QTY", "订单数");
            Execldic.Add("TURNOVER_QTY", "翻箱数（双）");
            Execldic.Add("B_QTY", "B品（只）");
            Execldic.Add("RETURN_FREQUENCY", "退库（次）");
            Execldic.Add("AFFECT_HOURS", "品质影响后段工时");
            Execldic.Add("SHOE_NO", "鞋型");
            Execldic.Add("PROD_NO", "ART");


            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.ShowDialog();
            string path = ofd.SelectedPath;
            SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"不良退货导入{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx", Execldic);
            MessageBox.Show("导出模板成功!");
        }
    }
}
