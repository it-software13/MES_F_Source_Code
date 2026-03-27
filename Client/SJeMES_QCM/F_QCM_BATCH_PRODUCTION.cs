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
    public partial class F_QCM_BATCH_PRODUCTION : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_BATCH_PRODUCTION()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
 Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_daoru_Click(object sender, EventArgs e)
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
                if (dt.Columns.Count!=14||dt.Columns[dt.Columns.Count-1].ColumnName!= "执行部门")
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
                        p.Add("import_type", 1);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.BASE",//类名
                                                    "ImportData",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if(ret.IsSuccess)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("量试编号");
                dt.Columns.Add("开发季度");
                dt.Columns.Add("类别");
                dt.Columns.Add("ART");
                dt.Columns.Add("量试日期");
                dt.Columns.Add("生产日期");
                dt.Columns.Add("鞋型名称");
                dt.Columns.Add("大底模号");
                dt.Columns.Add("试作SIZE、双数");
                dt.Columns.Add("配色");
                dt.Columns.Add("楦头");
                dt.Columns.Add("工艺");
                dt.Columns.Add("组长会签");
                dt.Columns.Add("执行部门");
                FolderBrowserDialog ofd = new FolderBrowserDialog();
                ofd.ShowDialog();
                string path = ofd.SelectedPath;
                SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"量产试作导入模板{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx");
                MessageBox.Show("下载成功");
            }
            catch
            {
                MessageBox.Show("下载失败");
            }


        }

        public void BindData(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {


                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                p.Add("is_pda", "0");
                p.Add("BATCH_DATE", string.IsNullOrWhiteSpace(dtp_batch_date.Text)?"":dtp_batch_date.Value.ToString("yyyy-MM-dd"));
                p.Add("ART", txt_art.Text.Trim());
                p.Add("SHOE_NAME", txt_shoe_name.Text.Trim());
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.BatchProduction",//类名
                                            "GetBatchProductionList",//方法名
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
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["量试编号"].Value = dr["BATCH_CODE"].ToString();
                        dgvr.Cells["开发季度"].Value = dr["DEVELOP_QUARTER"].ToString();
                        dgvr.Cells["类别"].Value = dr["TYPE"].ToString();
                        dgvr.Cells["ART"].Value = dr["ART"].ToString();
                        dgvr.Cells["量试日期"].Value = dr["BATCH_DATE"].ToString();
                        dgvr.Cells["生产日期"].Value = dr["PRODUCTION_DATE"].ToString();
                        dgvr.Cells["鞋型名称"].Value = dr["SHOE_NAME"].ToString();
                        dgvr.Cells["大底模号"].Value = dr["BIG_MOLD_NO"].ToString();
                        dgvr.Cells["试作SIZE_双数"].Value = dr["SIZE_DOUBLE"].ToString();
                        dgvr.Cells["配色"].Value = dr["COLOR"].ToString();
                        dgvr.Cells["楦头"].Value = dr["SHOE_LAST"].ToString();
                        dgvr.Cells["工艺"].Value = dr["PROCEDURE"].ToString();
                        dgvr.Cells["组长会签"].Value = dr["LEADER_AUTOGRAPH"].ToString();
                        dgvr.Cells["执行部门"].Value = dr["DEPARTMENT"].ToString();
                        dgvr.Cells["状态"].Value = dr["STATUS"].ToString()=="0"?"未完成": dr["STATUS"].ToString() == "1"?"已完成":"";
                        dgvr.Cells["STATUS"].Value = dr["STATUS"].ToString();
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        i++;
                    }
                }
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation1"].DefaultCellStyle.SelectionBackColor = Color.White;
                totalCount = int.Parse(dic["rowCount"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void F_QCM_BATCH_PRODUCTION_Load(object sender, EventArgs e)
        {
            this.dtp_batch_date.Format = DateTimePickerFormat.Custom;
            this.dtp_batch_date.CustomFormat = " ";
            pageControl1.BindPageEvent += BindData;
            FormLoad();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F_QCM_BATCH_PRODUCTION_Edit frm = new F_QCM_BATCH_PRODUCTION_Edit(this, null);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation1")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation1"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("info"))//查看
                    {
                        //F_QCM_BATCH_PRODUCTION_Detail MessageBox.Show("正在开发");
                        
                         var dr = dataGridView1.Rows[e.RowIndex];
                        F_QCM_BATCH_PRODUCTION_Detail frm = new F_QCM_BATCH_PRODUCTION_Detail(dr);
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();

                    }
                    else if (cell.CurrentItem.Equals("edit"))//编辑
                    {
                        //  MessageBox.Show("正在开发");
                        var dr = dataGridView1.Rows[e.RowIndex];
                        F_QCM_BATCH_PRODUCTION_Edit frm = new F_QCM_BATCH_PRODUCTION_Edit(this, dr);
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.ShowDialog();

                    }
                    else if (cell.CurrentItem.Equals("delete"))//删除
                    {
                        //string ID = this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString().Trim();
                        string BATCH_CODE = this.dataGridView1.Rows[e.RowIndex].Cells["量试编号"].Value.ToString().Trim();
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("BATCH_CODE", BATCH_CODE);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                    "SJ_QCMAPI",//类库名
                                                    "SJ_QCMAPI.BatchProduction",//类名
                                                    "Delete",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (ret.IsSuccess)
                        {
                            MessageBox.Show("删除成功");
                            FormLoad();
                        }
                    }
                }

            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("pageSize", "99999");
            p.Add("pageIndex", "1");
            p.Add("is_pda", "0");
            p.Add("BATCH_DATE", string.IsNullOrWhiteSpace(dtp_batch_date.Text) ? "" : dtp_batch_date.Value.ToString("yyyy-MM-dd"));
            p.Add("ART", txt_art.Text.Trim());
            p.Add("SHOE_NAME", txt_shoe_name.Text.Trim());
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.BatchProduction",//类名
                                        "GetBatchProductionList",//方法名
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
            Execldic.Add("BATCH_CODE", "量试编号");
            Execldic.Add("DEVELOP_QUARTER", "开发季度");
            Execldic.Add("TYPE", "类别");
            Execldic.Add("ART", "ART");
            Execldic.Add("BATCH_DATE", "量试日期");
            Execldic.Add("PRODUCTION_DATE", "生产日期");
            Execldic.Add("SHOE_NAME", "鞋型名称");
            Execldic.Add("BIG_MOLD_NO", "大底模号");
            Execldic.Add("SIZE_DOUBLE", "试作SIZE_双数");
            Execldic.Add("COLOR", "配色");
            Execldic.Add("SHOE_LAST", "楦头");
            Execldic.Add("PROCEDURE", "工艺");
            Execldic.Add("LEADER_AUTOGRAPH", "组长会签");
            Execldic.Add("DEPARTMENT", "执行部门");
            Execldic.Add("STATUS", "状态");
            
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.ShowDialog();
            string path = ofd.SelectedPath;
            SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"量产试作{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx", Execldic);
            MessageBox.Show("导出成功");
        }

        private void dtp_batch_date_ValueChanged(object sender, EventArgs e)
        {
            dtp_batch_date.CustomFormat = "yyyy年MM月dd日";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.dtp_batch_date.CustomFormat = " ";
        }
    }
}
