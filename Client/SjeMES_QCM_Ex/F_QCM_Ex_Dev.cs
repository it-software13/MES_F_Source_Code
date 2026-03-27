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

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_Dev : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_Dev()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(string Name); //调用win api将指定名称的打印机设置为默认打印机
        private void F_QCM_Ex_Dev_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDataList;
            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                cbo_BarCode.Items.Add(fPrinterName);
            }
            #endregion
            FormLoad();
            SJeMES_Framework.Common.UIHelper.AdjustComboBoxDropDownListWidth(cbo_BarCode);
        }

        public void FormLoad()
        {
            pageControl1.PageSize = 15;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("keyword", txt_keyword.Text.Trim());
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetExDevList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridViewEx1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int i = dataGridViewEx1.Rows.Add();
                        dataGridViewEx1.Rows[i].Cells["id"].Value = dr["ID"].ToString(); ;
                        dataGridViewEx1.Rows[i].Cells["编号"].Value = dr["编号"].ToString(); ;
                        dataGridViewEx1.Rows[i].Cells["设备名称"].Value = dr["设备名称"].ToString();
                        dataGridViewEx1.Rows[i].Cells["部门编号"].Value = dr["部门编号"].ToString();
                        dataGridViewEx1.Rows[i].Cells["部门"].Value = dr["部门"].ToString();
                        dataGridViewEx1.Rows[i].Cells["工段编号"].Value = dr["工段编号"].ToString();
                        dataGridViewEx1.Rows[i].Cells["工段"].Value = dr["工段"].ToString();
                        dataGridViewEx1.Rows[i].Cells["设备类型编号"].Value = dr["设备类型编号"].ToString();
                        dataGridViewEx1.Rows[i].Cells["设备类型"].Value = dr["设备类型"].ToString();
                        //dataGridViewEx1.Rows[i].Cells["管控类型设备"].Value = dr["管控类型设备"].ToString();
                        
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridViewEx1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("location_no",typeof(string));
            //dt.Columns.Add("location_name",typeof(string));
            //dt.Columns.Add("warehouse_name",typeof(string));


            List<string> list = new List<string>();
            for (int i = 0; i < dataGridViewEx1.Rows.Count; i++)
            {
                var check = dataGridViewEx1.Rows[i].Cells["check"].Value == null ? "false" : dataGridViewEx1.Rows[i].Cells["check"].Value;

                if (check.ToString().ToLower() == "true")
                {
                    list.Add(dataGridViewEx1.Rows[i].Cells["id"].Value.ToString());
                }

            }

            string Print = this.cbo_BarCode.Text;
            if (list.Count == 0)
            {
                MessageBox.Show("Please tick the required print data！");
                return;
            }
            if (string.IsNullOrEmpty(Print))
            {
                MessageBox.Show("Please select a printer！");
                return;
            }

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("id", list);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetPrintDevList",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (!ret.IsSuccess)
            {
                MessageBox.Show(dic["ErrMsg"].ToString());
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    string print = cbo_BarCode.Text;
                    WriteTxt(dt, "设备信息条码打印", Application.StartupPath + "/Printer/BarCodeModel/设备信息条码打印.txt", 1);
                    if (string.IsNullOrEmpty(print))
                    {
                        print = "Microsoft Print to PDF";
                        
                    }
                    SetDefaultPrinter(print);
                    Thread.Sleep(1000);
                    #region 启动答应程序
                    Process p1 = new Process();
                    p1.StartInfo.FileName = "设备信息条码打印_print.bat";
                    p1.StartInfo.RedirectStandardInput = true;
                    p1.StartInfo.RedirectStandardOutput = true;
                    p1.StartInfo.RedirectStandardError = true;
                    p1.StartInfo.CreateNoWindow = true;
                    p1.StartInfo.UseShellExecute = false;
                    p1.Start();//启动 
                    p1.WaitForExit(5 * 1000);//等待上述进程执行完毕
                                             //p.WaitForExit();//这个会一直等待
                    if (p1.HasExited == false)
                    {
                        p1.Kill();
                    }
                    #endregion

                    MessageBox.Show("Printed successfully");
                }
                else
                {
                    MessageBox.Show(dic["ErrMsg"].ToString());
                }
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
    }
}
