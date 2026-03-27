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
    public partial class F_QCM_TaskNo_Print : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _task_no;

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机

        public F_QCM_TaskNo_Print(string task_no)
        {
            InitializeComponent();
            _task_no = task_no;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_QCM_TaskNo_Print_Load(object sender, EventArgs e)
        {

            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(fPrinterName);
            }
            #endregion
            label1.Text = _task_no;
            if (!string.IsNullOrEmpty(_task_no))
                this.pictureBox1.Image = QRCode.CreateQRCode(_task_no);//  QRCode.CreateQRCode(code);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Please select the print medium before printing");
                return;
            }
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("task_no", _task_no);//名称
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_QCMAPI",//类库名
                                        "SJ_QCMAPI.ExShose",//类名
                                        "GetInfoByTaskNo",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (ret.IsSuccess)
            {
                DataTable vdt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                DataTable dt = new DataTable();
                //dt.Columns.Add("检验单编号");
                //dt.Columns.Add("试样数量");
                //dt.Columns.Add("测试类型");
                //dt.Columns.Add("ArtNo");
                //dt.Columns.Add("部件名称");
                //dt.Columns.Add("工艺名称");
                //dt.Columns.Add("材料类型");
                //dt.Columns.Add("产线");
                //DataRow dr = dt.NewRow();
                //dr["检验单编号"] = vdt.Rows[0]["TASK_NO"].ToString();
                //dr["测试类型"] = vdt.Rows[0]["FGT_NAME"].ToString();
                //dr["ArtNo"] = vdt.Rows[0]["ART_NO"].ToString();
                //dr["试样数量"] = vdt.Rows[0]["SEND_TEST_QTY"].ToString();
                //dr["部件名称"] = vdt.Rows[0]["PARTS_NAME"].ToString();
                //dr["工艺名称"] = vdt.Rows[0]["WORKMANSHIP_NAME"].ToString();
                //dr["材料类型"] = vdt.Rows[0]["MAKINGS_TYPE_NAME"].ToString();
                //dr["产线"] = vdt.Rows[0]["LINE_NAME"].ToString();
                dt.Columns.Add("Inspection_Order_Number");
                dt.Columns.Add("Sample_Quantity");
                dt.Columns.Add("Test_Type");
                dt.Columns.Add("ArtNo");
                dt.Columns.Add("Part_Name");
                dt.Columns.Add("Process_Name");
                dt.Columns.Add("Material_Type");
                dt.Columns.Add("Production_Line");
                dt.Columns.Add("Order_Po");
                dt.Columns.Add("Art_No");
                dt.Columns.Add("Shoe_No");
                dt.Columns.Add("Sizes");
                dt.Columns.Add("Po_Quantity"); 
                dt.Columns.Add("Model");
                dt.Columns.Add("Received_Date");
                dt.Columns.Add("Received_Time");
                dt.Columns.Add("Category");
                dt.Columns.Add("Product_Level");
                dt.Columns.Add("Color");
                DataRow dr = dt.NewRow();
                dr["Inspection_Order_Number"] = vdt.Rows[0]["TASK_NO"].ToString();
                dr["Test_Type"] = vdt.Rows[0]["FGT_NAME"].ToString();
                dr["ArtNo"] = vdt.Rows[0]["ART_NO"].ToString();
                dr["Sample_Quantity"] = vdt.Rows[0]["SEND_TEST_QTY"].ToString();
                //dr["Part_Name"] = vdt.Rows[0]["PARTS_NAME"].ToString();
                dr["Part_Name"] = vdt.Rows[0]["POSITION_NAME"].ToString();
                dr["Process_Name"] = vdt.Rows[0]["WORKMANSHIP_NAME"].ToString();
                dr["Material_Type"] = vdt.Rows[0]["MAKINGS_TYPE_NAME"].ToString();
                dr["Production_Line"] = vdt.Rows[0]["LINE_NAME"].ToString();
                dr["Order_Po"] = vdt.Rows[0]["ORDER_PO"].ToString(); 
                dr["Shoe_No"] = vdt.Rows[0]["SHOE_NO"].ToString();
                dr["Sizes"] = vdt.Rows[0]["SIZES"].ToString();
                dr["Po_Quantity"] = vdt.Rows[0]["ORDER_PO_QTY"].ToString(); 
                dr["Model"] = vdt.Rows[0]["CATEGORY_NAME"].ToString();
                dr["Received_Date"] = vdt.Rows[0]["CREATEDATE"].ToString();
                dr["Received_Time"] = vdt.Rows[0]["CREATETIME"].ToString();
                dr["Category"] = vdt.Rows[0]["CATEGORY_NAME"].ToString();
                dr["Product_Level"] = vdt.Rows[0]["PRODUCT_LEVEL_VALUE"].ToString();
                dr["Color"] = vdt.Rows[0]["COLORS"].ToString();
                dt.Rows.Add(dr);
                string FileNames = string.Empty;
                switch (vdt.Rows[0]["TEST_TYPE"].ToString())
                {
                    case "0":
                        WriteTxt(dt, "实验室成品鞋", Application.StartupPath + "/Printer/BarCodeModel/实验室成品鞋.txt", 1);
                        FileNames = "实验室成品鞋_print.bat";
                        break;
                    case "1":
                        WriteTxt(dt, "实验室部件", Application.StartupPath + "/Printer/BarCodeModel/实验室部件.txt", 1);
                        FileNames = "实验室部件_print.bat";
                        break;
                    case "2":
                        WriteTxt(dt, "实验室工艺", Application.StartupPath + "/Printer/BarCodeModel/实验室工艺.txt", 1);
                        FileNames = "实验室工艺_print.bat";
                        break;
                    case "3":
                        WriteTxt(dt, "实验室材料", Application.StartupPath + "/Printer/BarCodeModel/实验室材料.txt", 1);
                        FileNames = "实验室材料_print.bat";
                        break;
                    case "4":
                        WriteTxt(dt, "实验室量产拉力", Application.StartupPath + "/Printer/BarCodeModel/实验室量产拉力.txt", 1);
                        FileNames = "实验室量产拉力_print.bat";
                        break;
                }
                if (string.IsNullOrEmpty(Program.DefaultPrinter))
                {
                    Program.DefaultPrinter = comboBox1.Text;
                    SetDefaultPrinter(Program.DefaultPrinter);
                }
                Thread.Sleep(1000);
                #region 启动答应程序
                Process p = new Process();
                p.StartInfo.FileName = FileNames;
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

                MessageBox.Show("Printed successfully");
            }
            else
            {
                MessageBox.Show(ret.ErrMsg);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Print = this.comboBox1.Text;
            Program.DefaultPrinter = Print;
            if (!SetDefaultPrinter(Program.DefaultPrinter))
            {
                MessageBox.Show("Setting up the printer failed");
            }
        }
    }
}
