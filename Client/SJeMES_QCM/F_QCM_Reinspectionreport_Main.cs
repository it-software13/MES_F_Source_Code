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
    public partial class F_QCM_Reinspectionreport_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Reinspectionreport_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_Reinspectionreport_Main_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetDataList;
            //GetDataList();
            FormLoad();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }
        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        /// <summary>
        /// 搜索及视图展示（重检报告）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("OUTSOURCING_INSPECTION_NO", txt_OUTSOURCING_INSPECTION_NO.Text.Trim());
                p.Add("PO_ORDER", txt_PO_ORDER.Text.Trim().ToString());
                p.Add("PROD_NO",txt_PROD_NO.Text.Trim().ToString());
                p.Add("excel_no",string.Empty);

                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ReinspectionreportBase",//类名
                                            "ReinspectionreportView",//方法名
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
                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();//条件
                        dgvr.Cells["GUID"].Value = dr["GUID"].ToString();//条件

                        dgvr.Cells["OUTSOURCING_INSPECTION_NO"].Value = dr["OUTSOURCING_INSPECTION_NO"].ToString();
                        dgvr.Cells["SUPPLIERS_CODE"].Value = dr["SUPPLIERS_CODE"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                        dgvr.Cells["SUPPLIERS_TYPE"].Value = dr["SUPPLIERS_TYPE"].ToString();
                        dgvr.Cells["PO_ORDER"].Value = dr["PO_ORDER"].ToString();
                        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                        dgvr.Cells["WH_QTY"].Value = dr["WH_QTY"].ToString();
                        dgvr.Cells["SPOT_CHECK_QTY"].Value = dr["SPOT_CHECK_QTY"].ToString();
                        dgvr.Cells["BAD_QTY"].Value = dr["BAD_QTY"].ToString();
                        dgvr.Cells["BAD_RATE"].Value = dr["BAD_RATE"].ToString();
                        dgvr.Cells["NOT_ACCEPT_QTY"].Value = dr["NOT_ACCEPT_QTY"].ToString();
                        dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString();
                        dgvr.Cells["ACCEPT_QTY"].Value = dr["ACCEPT_QTY"].ToString();
                        dgvr.Cells["GENERAL_TESTTYPE_NO"].Value = dr["GENERAL_TESTTYPE_NO"].ToString();
                        dgvr.Cells["CATEGORY_NO"].Value = dr["CATEGORY_NO"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
               
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            FormLoad();
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
                            string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();//检验单号
                            string GUID= dataGridView1.CurrentRow.Cells["GUID"].Value.ToString();
                            string OUTSOURCING_INSPECTION_NO = dataGridView1.CurrentRow.Cells["OUTSOURCING_INSPECTION_NO"].Value.ToString();//检验单号
                            F_QCM_Reinspectionreport_Add add = new F_QCM_Reinspectionreport_Add(ID,GUID, "DETAIL", OUTSOURCING_INSPECTION_NO);
                            add.ShowDialog();
                        }
                        else if (cell.CurrentItem.Equals("UPDATE"))//修改
                        {
                            string OUTSOURCING_INSPECTION_NO = dataGridView1.CurrentRow.Cells["OUTSOURCING_INSPECTION_NO"].Value.ToString();//检验单号
                            string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();//检验单号
                            string GUID = dataGridView1.CurrentRow.Cells["GUID"].Value.ToString();
                            F_QCM_Reinspectionreport_Add add = new F_QCM_Reinspectionreport_Add(ID,GUID, "UPDATE", OUTSOURCING_INSPECTION_NO);
                            add.ShowDialog();
                            FormLoad();
                        }
                        else if (cell.CurrentItem.Equals("DELETE"))//删除
                        {

                            if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {
                                    string OUTSOURCING_INSPECTION_NO = dataGridView1.CurrentRow.Cells["OUTSOURCING_INSPECTION_NO"].Value.ToString();
                                    string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();//检验单号
                                    string GUID = dataGridView1.CurrentRow.Cells["GUID"].Value.ToString();
                                    Dictionary<string, object> p = new Dictionary<string, object>();

                                    //p.Add("OUTSOURCING_INSPECTION_NO", OUTSOURCING_INSPECTION_NO);
                                    p.Add("ID", ID);
                                    p.Add("GUID", GUID);
                                    p.Add("OUTSOURCING_INSPECTION_NO", OUTSOURCING_INSPECTION_NO);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_QCMAPI", "SJ_QCMAPI.ReinspectionreportBase", "ReinspectionreportDelete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private void btn_add_Click(object sender, EventArgs e)
        {
            F_QCM_Reinspectionreport_Add add = new F_QCM_Reinspectionreport_Add("","", "Add","");
            add.ShowDialog();
            FormLoad();
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
                    //|| dt.Columns[dt.Columns.Count - 1].ColumnName != "执行部门"
                    if (dt.Columns.Count != 12)
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
                            p.Add("import_type", 4);
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
        /// 发外厂商品质体系项目日志(重检报告)EXCEL导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ex_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("pageSize", "99999");
                p.Add("pageIndex", "1");
                p.Add("excel_no", "导出");
                p.Add("OUTSOURCING_INSPECTION_NO", txt_OUTSOURCING_INSPECTION_NO.Text.Trim());
                p.Add("PO_ORDER", txt_PO_ORDER.Text.Trim().ToString());
                p.Add("PROD_NO", txt_PROD_NO.Text.Trim().ToString());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ReinspectionreportBase",//类名
                                            "ReinspectionreportView",//方法名
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
                Execldic.Add("OUTSOURCING_INSPECTION_NO", "外包检验编号");
                Execldic.Add("SUPPLIERS_CODE", "厂商代号");
                Execldic.Add("SUPPLIERS_NAME", "厂商名称");
                Execldic.Add("SUPPLIERS_TYPE", "厂商类型");
                Execldic.Add("PO_ORDER", "制令号（PO）");
                Execldic.Add("PROD_NO", "ART");
                Execldic.Add("WH_QTY", "进仓数");
                Execldic.Add("SPOT_CHECK_QTY", "抽检数");
                Execldic.Add("BAD_QTY", "不良数");
                Execldic.Add("BAD_RATE", "不良率");
                Execldic.Add("NOT_ACCEPT_QTY", "不接受数量");
                Execldic.Add("SHOE_NO", "鞋型");
                Execldic.Add("ACCEPT_QTY", "接受数量");
                Execldic.Add("GENERAL_TESTTYPE_NO", "通用检测类型代号");
                Execldic.Add("CATEGORY_NO", "检测类别");


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
        private void btn_modo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("厂商代号");
                dt.Columns.Add("厂商名称");
                dt.Columns.Add("厂商类型");
                dt.Columns.Add("制令号（PO）");
                dt.Columns.Add("ART");
                dt.Columns.Add("进仓数");
                dt.Columns.Add("抽检数");
                dt.Columns.Add("不良数");
                dt.Columns.Add("不良率");
                dt.Columns.Add("不接受数量");
                dt.Columns.Add("鞋型");
                dt.Columns.Add("接受数量");
                dt.Columns.Add("通用检测类型代号");
                dt.Columns.Add("检测类别");
                FolderBrowserDialog ofd = new FolderBrowserDialog();
                ofd.ShowDialog();
                string path = ofd.SelectedPath;
                if (!string.IsNullOrEmpty(path))
                {
                    SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"发外厂商品质体系项目日志（重检报告）{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx");
                    MessageBox.Show("下载模板成功");
                }
              
            }
            catch
            {
                MessageBox.Show("下载模板失败");
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
