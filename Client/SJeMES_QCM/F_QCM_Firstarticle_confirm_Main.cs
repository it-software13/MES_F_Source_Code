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
    public partial class F_QCM_Firstarticle_confirm_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Firstarticle_confirm_Main()
        {

            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
      Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            InitDateTimePicker(dateTimeP_putin_date);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        public void FormLoad()
        {
            
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
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
        private void F_QCM_Firstarticle_confirm_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";
            pageControl1.BindPageEvent += GetDataList;
            //GetDataList();
            FormLoad();
            dataGridView1.ClearSelection();
            dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;

        }
        /// <summary>
        /// 录入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
        {
            F_QCM_Firstarticle_confirm_Add add = new F_QCM_Firstarticle_confirm_Add("","Add");
            add.ShowDialog();
            FormLoad();
        }
        /// <summary>
        /// 搜索及视图展示(收件确认记录表视图)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string putin_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
                }
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PROD_NO", txt_PROD_NO.Text.Trim().ToString());//ART
                p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim().ToString());//鞋型
                p.Add("MODULE_NO", txt_MODULE_NO.Text.Trim().ToString());//品号
                p.Add("excel_no","");

                p.Add("putin_date",putin_date);//时间

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FirstarticleconfirmmBase",//类名
                                            "FirstarticleconfirmmView",//方法名
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
                        dgvr.Cells["INSPECT_NO"].Value = dr["INSPECT_NO"].ToString();
                        dgvr.Cells["PO_ORDER"].Value = dr["PO_ORDER"].ToString();
                        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                        dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString();
                        dgvr.Cells["MODULE_NO"].Value = dr["MODULE_NO"].ToString();
                        dgvr.Cells["PHYSICAL_NAME"].Value = dr["PHYSICAL_NAME"].ToString();
                        dgvr.Cells["MACHINE"].Value = dr["MACHINE"].ToString();
                        dgvr.Cells["CODE_NUMBER"].Value = dr["CODE_NUMBER"].ToString();
                        dgvr.Cells["DEPARTMENT_NO"].Value = dr["DEPARTMENT_NO"].ToString();
                        dgvr.Cells["DEPARTMENT_NAME"].Value = dr["DEPARTMENT_NAME"].ToString();
                        dgvr.Cells["PRODUCTIONLINE_NO"].Value = dr["PRODUCTIONLINE_NO"].ToString();
                        dgvr.Cells["PRODUCTIONLINE_NAME"].Value = dr["PRODUCTIONLINE_NAME"].ToString();

                        dgvr.Cells["CreateDay"].Value = dr["CREATEDATE"].ToString();

                        if (dr["STATUS"].ToString() == "PASS")
                        {
                            dgvr.Cells["STATUS"].Value ="已完成";

                        }
                        else if(dr["STATUS"].ToString() == "FAIL")
                        {
                            dgvr.Cells["STATUS"].Value ="未完成";

                        }
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            FormLoad();
            this.dateTimeP_putin_date.Format = DateTimePickerFormat.Custom;
            this.dateTimeP_putin_date.CustomFormat = " ";
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
                        if (cell.CurrentItem.Equals("DETAIL"))//查看
                        {
                            string INSPECT_NO = dataGridView1.CurrentRow.Cells["INSPECT_NO"].Value.ToString();//检验单号
                            F_QCM_Firstarticle_confirm_Add add = new F_QCM_Firstarticle_confirm_Add(INSPECT_NO, "DETAIL");
                            add.ShowDialog();
                        }
                        else if (cell.CurrentItem.Equals("UPDATE"))//修改
                        {
                            string INSPECT_NO = dataGridView1.CurrentRow.Cells["INSPECT_NO"].Value.ToString();//检验单号
                            F_QCM_Firstarticle_confirm_Add add = new F_QCM_Firstarticle_confirm_Add(INSPECT_NO, "UPDATE");
                            add.ShowDialog();
                            FormLoad();
                        }
                        else  if (cell.CurrentItem.Equals("DELETE"))//删除
                        {

                            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string INSPECT_NO = dataGridView1.CurrentRow.Cells["INSPECT_NO"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();

                                    p.Add("INSPECT_NO", INSPECT_NO);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.FirstarticleconfirmmBase", "FirstarticleconfirmmDelete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (!ret.IsSuccess)
                                    {
                                        throw new Exception(ret.ErrMsg);
                                    }
                                    else
                                    {
                                        MessageBox.Show("操作删除成功");
                                        FormLoad();
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
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btn_exadd_Click(object sender, EventArgs e)
        {
            try
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
                    //|| dt.Columns[dt.Columns.Count - 1].ColumnName != "机台"
                    if (dt.Columns.Count != 7)
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
                            p.Add("import_type", 3);
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
                                FormLoad();
                            }
                            else
                            {
                                MessageBox.Show(ret.ErrMsg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        /// <summary>
        /// 导入模板下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_modo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PO单号");
                dt.Columns.Add("ART");
                dt.Columns.Add("鞋型");
                dt.Columns.Add("模号");
                dt.Columns.Add("实物名称");
                dt.Columns.Add("机台");
                dt.Columns.Add("码数");
                FolderBrowserDialog ofd = new FolderBrowserDialog();
                ofd.ShowDialog();
                string path = ofd.SelectedPath;
                if (!string.IsNullOrEmpty(path))
                {
                    SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"首件确认单导入模板{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx");
                    MessageBox.Show("下载模板成功");
                }
            }
            catch
            {
                MessageBox.Show("下载模板失败");
            }
        }

        private void btn_ex_Click(object sender, EventArgs e)
        {
            try
            {
                string putin_date = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.dateTimeP_putin_date.Text))
                {
                    putin_date = Convert.ToDateTime(this.dateTimeP_putin_date.Value).ToString("yyyy-MM-dd");
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("pageSize", "99999");
                p.Add("pageIndex", "1");
                p.Add("excel_no", "导出");
                p.Add("PROD_NO", txt_PROD_NO.Text.Trim().ToString());//ART
                p.Add("SHOE_NO", txt_SHOE_NO.Text.Trim().ToString());//鞋型
                p.Add("MODULE_NO", txt_MODULE_NO.Text.Trim().ToString());//品号

                p.Add("putin_date", putin_date);//时间
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.FirstarticleconfirmmBase",//类名
                                            "FirstarticleconfirmmView",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData.ToString());
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("INSPECT_NO", "检验单号");
                Execldic.Add("PO_ORDER", "PO单号");
                Execldic.Add("PROD_NO", "ART");
                Execldic.Add("SHOE_NO", "鞋型");
                Execldic.Add("MODULE_NO", "模号");
                Execldic.Add("PHYSICAL_NAME", "实物名称");
                Execldic.Add("MACHINE", "机台");
                Execldic.Add("CODE_NUMBER", "码数");
                Execldic.Add("DEPARTMENT_NO", "部门代号");
                Execldic.Add("DEPARTMENT_NAME", "部门名称");
                Execldic.Add("PRODUCTIONLINE_NO", "产线代号");
                Execldic.Add("PRODUCTIONLINE_NAME", "产线名称");
                Execldic.Add("STATUS", "状态");


                FolderBrowserDialog ofd = new FolderBrowserDialog();
                ofd.ShowDialog();
                string path = ofd.SelectedPath;
                SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"首件确认单{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx", Execldic);
                MessageBox.Show("导出成功");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
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
