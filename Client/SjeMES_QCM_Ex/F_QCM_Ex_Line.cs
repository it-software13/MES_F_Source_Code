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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_Line : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_Line()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(string Name); //调用win api将指定名称的打印机设置为默认打印机
        private void F_QCM_Ex_Line_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDataList;
            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                cbo_BarCode.Items.Add(fPrinterName);
            }
            #endregion
            SJeMES_Framework.Common.UIHelper.AdjustComboBoxDropDownListWidth(cbo_BarCode);
            FormLoad();

        }
        public void FormLoad()
        {
            pageControl1.PageSize = 15;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
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
                                            "GetExLineList",//方法名
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
                        //dataGridViewEx1.Rows[i].Cells["id"].Value = dr["ID"].ToString(); ;
                        dataGridViewEx1.Rows[i].Cells["产线编号"].Value = dr["PRODUCTION_LINE_CODE"].ToString(); ;
                        dataGridViewEx1.Rows[i].Cells["国家"].Value = dr["COUNTRY"].ToString();
                        dataGridViewEx1.Rows[i].Cells["地区"].Value = dr["REGION"].ToString();
                        dataGridViewEx1.Rows[i].Cells["厂区"].Value = dr["PLANT_AREA"].ToString();
                        dataGridViewEx1.Rows[i].Cells["部门"].Value = dr["DEPARTMENT"].ToString();
                        dataGridViewEx1.Rows[i].Cells["产线"].Value = dr["PRODUCTION_LINE_NAME"].ToString();
                        dataGridViewEx1.Rows[i].Cells["备注"].Value = dr["REMARKS"].ToString();

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

        private void btn_search_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < dataGridViewEx1.Rows.Count; i++)
            {
                var check = dataGridViewEx1.Rows[i].Cells["check"].Value == null ? "false" : dataGridViewEx1.Rows[i].Cells["check"].Value;

                if (check.ToString().ToLower() == "true")
                {
                    list.Add(dataGridViewEx1.Rows[i].Cells["产线编号"].Value.ToString());
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
            p.Add("line", list);

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetPrintLineList",//方法名
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
                    PrintHelper.WriteTxt(dt, "Production line code printing", Application.StartupPath + "/Printer/BarCodeModel/产线条码打印.txt", 1);
                    if (string.IsNullOrEmpty(print))
                    {
                        print = "Microsoft Print to PDF";

                    }
                    SetDefaultPrinter(print);
                    Thread.Sleep(1000);
                    #region 启动答应程序
                    Process p1 = new Process();
                    p1.StartInfo.FileName = "产线条码打印_print.bat";
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
    }
}
