using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_AQL
{
    public partial class F_AQL_ConfirmShoes_Location : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string MODULE_TYPE = string.Empty;
        public F_AQL_ConfirmShoes_Location(string _MODULE_TYPE)
        {
            InitializeComponent();
            MODULE_TYPE = _MODULE_TYPE;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 初始化分页
        /// </summary>
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 查询-确认鞋-库位维护-主页
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoesLocation_Main(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                p.Add("MODULE_TYPE", MODULE_TYPE);
                p.Add("search_str", tb_search.Text.Trim());

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                            "GetConfirmShoesLocation_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["xh"].Value = i + 1;
                        dgvr.Cells["id"].Value = dr["id"].ToString();
                        dgvr.Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString();
                        dgvr.Cells["warehouse_name"].Value = dr["WAREHOUSE_NAME"].ToString();
                        dgvr.Cells["STOCK_NAME"].Value = dr["STOCK_NAME"].ToString();
                        dgvr.Cells["REMARK"].Value = dr["REMARK"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
        private void F_AQL_ConfirmShoes_Location_Load(object sender, EventArgs e)
        {
            //只要加载一次委托 
            pageControl1.BindPageEvent += GetConfirmShoesLocation_Main;
            LoadPage();
            this.dataGridView1.ClearSelection();

            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                cbo_BarCode.Items.Add(fPrinterName);
            }
            SJeMES_Framework.Common.UIHelper.AdjustComboBoxDropDownListWidth(cbo_BarCode);
            #endregion
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "cz")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["cz"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }

                    if (cell.CurrentItem.Equals("DELETE"))
                    {
                        //DialogResult dr = MessageBox.Show("确认要删除吗!", "删除鞋型品质状况", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Delete location", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (dr == DialogResult.OK)
                        {
                            string sid = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();//库位id
                            DeleteConfirmShoesLocation(sid);
                        }
                    }
                    else if (cell.CurrentItem.Equals("EDIT"))
                    {
                        string sid = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();//库位id
                        using (F_AQL_ConfirmShoes_Location_Add c = new F_AQL_ConfirmShoes_Location_Add(sid, MODULE_TYPE))
                        {
                            c.ShowDialog();
                        }
                        LoadPage();
                    }
                    else if (cell.CurrentItem.Equals("print"))
                    {
                        string Print = this.cbo_BarCode.Text;
                        if (string.IsNullOrEmpty(Print))
                        {
                            MessageBox.Show("Please select a printer！");
                            return;
                        }
                        DataTable dt = new DataTable();
                        dt.Columns.Add("库位代号");
                        dt.Columns.Add("库位名称");
                        dt.Columns.Add("仓库代号");
                        dt.Columns.Add("仓库名称");

                        DataRow newRow;
                        newRow = dt.NewRow();
                        newRow["库位代号"] = dataGridView1.Rows[e.RowIndex].Cells["stock_code"].Value.ToString();
                        newRow["库位名称"] = dataGridView1.Rows[e.RowIndex].Cells["stock_name"].Value.ToString();
                        newRow["仓库代号"] = "";
                        newRow["仓库名称"] = dataGridView1.Rows[e.RowIndex].Cells["warehouse_name"].Value.ToString();
                        dt.Rows.Add(newRow);

                        if (dt.Rows.Count > 0)
                        {
                            WriteTxt(dt, "库位条码打印", Application.StartupPath + "/Printer/BarCodeModel/库位条码打印.txt", 1);
                            Program.DefaultPrinter = Print;
                            SetDefaultPrinter(Program.DefaultPrinter);
                        }

                        Thread.Sleep(1000);


                        #region 启动答应程序
                        Process p = new Process();
                        p.StartInfo.FileName = "库位条码打印_print.bat";
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = false;
                        p.Start();//启动 
                        p.WaitForExit(5 * 1000);//等待上述进程执行完毕
                                                //p.WaitForExit();//这个会一直等待
                        if (p.HasExited == false)
                        {
                            p.Kill();
                        }
                        #endregion

                        MessageBox.Show("Printed successfully！");
                    }
                }
            }
        }

        /// <summary>
        /// 删除-确认鞋-仓库维护-aql
        /// </summary>
        public void DeleteConfirmShoesLocation(string sid)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("id", sid);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_ConfirmShoes", "DeleteConfirmShoesLocation", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (F_AQL_ConfirmShoes_Location_Add r = new F_AQL_ConfirmShoes_Location_Add(MODULE_TYPE))
            {
                r.ShowDialog();
            }
            LoadPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //视图数据显示
                DataTable dts = new DataTable();
                //if (dts.Rows.Count < 1)
                //{
                //    MessageBox.Show("暂无数据导出，请检查是否操作正确");
                //    return;
                //}
                /* if (DT_EXCEL.Rows.Count < 1)
                 {
                     MessageBox.Show("数据为空，先搜索再做导出操作");
                     return;
                 }*/
                //for (int i = 0; i < dts.Rows.Count; i++)
                //{
                //    dts.Rows.RemoveAt(i);
                //}
                dts.Columns.Add("STOCK_CODE");
                dts.Columns.Add("STOCK_NAME");
                dts.Columns.Add("WAREHOUSE_CODE");
                dts.Columns.Add("REMARK");
                Dictionary<string, string> Execldic = new Dictionary<string, string>();
                Execldic.Add("STOCK_CODE", "Location_Code");//库位代号
                Execldic.Add("STOCK_NAME", "Location_Name");//库位名称
                Execldic.Add("WAREHOUSE_CODE", "Warehouse_Code");//仓库代号
                Execldic.Add("REMARK", "Remark");//备注

                ExeclHelper.ExportToTrueExcel(dts, Execldic, "Location_Maintenance_Import_Template");
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Make sure the location code before importing、The location name is not empty, and the warehouse code has been maintained, otherwise the import will fail", "Operation prompt！！！", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //创建文件弹出选择窗口（包括文件名）对象
                    OpenFileDialog ofd = new OpenFileDialog();
                    //判断选择的路径
                    string path = string.Empty;
                    ofd.Title = "Please select a file";
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
                            MessageBox.Show("Wrong file type, please select (.xlsx,.xls) type file");
                            return;
                        }
                        DataTable dt = SJeMES_Framework.Common.NPOIHelper.ExcelToTable(filePath);
                        //|| dt.Columns[dt.Columns.Count - 1].ColumnName != "机台"
                        if (dt.Columns.Count != 4)
                        {
                            MessageBox.Show("Import template error, please refer to");
                            return;
                        }
                        dt.Columns.Add("MODULE_TYPE");
                        //不能为空
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["Location_Code"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["Location_Code"].ToString()))
                            {
                                MessageBox.Show($@"The location code cannot be empty! No.{i + 1}行!");
                                return;
                            }
                            if (dt.Rows[i]["Location_Name"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["Location_Name"].ToString()))//库位名称
                            {
                                MessageBox.Show($@"The location name cannot be empty! No.{i + 1}行!");
                                return;
                            }
                            if (dt.Rows[i]["Warehouse_Code"] == null || string.IsNullOrWhiteSpace(dt.Rows[i]["Warehouse_Code"].ToString()))
                            {
                                MessageBox.Show($@"The warehouse code cannot be empty! No.{i + 1}行!");
                                return;
                            }
                            dt.Rows[i]["MODULE_TYPE"] = MODULE_TYPE;
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
                                p.Add("import_type", 12);//出货库位维护导入
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
                                    MessageBox.Show("Imported successfully");
                                    LoadPage();
                                }
                                else
                                {
                                    MessageBox.Show(ret.ErrMsg);
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

        public static bool WriteTxt(DataTable dt, string ModelName, string Path, int printQty)
        {
            try
            {
                string Data = string.Empty;

                string FilePath = Path;

                foreach (DataColumn dc in dt.Columns)
                {
                    Data += dc.ColumnName + "￥";
                }

                Data = Data.Remove(Data.Length - 1) + "\r\n";

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < printQty; i++)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Data += dr[dc].ToString() + "￥";
                        }

                        Data = Data.Remove(Data.Length - 1) + "\r\n";
                    }

                }
                WriteText(Data, FilePath);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        private static void WriteText(string str, string FilePath)
        {
            string fileName = FilePath;

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            System.IO.File.AppendAllText(fileName, str, Encoding.UTF8);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            LoadPage();
        }
    }
}
