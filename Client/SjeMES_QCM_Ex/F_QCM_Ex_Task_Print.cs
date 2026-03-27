using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class F_QCM_Ex_Task_Print : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string task_no = string.Empty;//实验任务编号
        public F_QCM_Ex_Task_Print(string taskno)
        {
            task_no = taskno;

            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 检测条码打印查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetEx_Task_Print_Main()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("task_no", task_no);//名称
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_PRINTAPI",//类库名
                                            "SJ_PRINTAPI.QCM_Ex_Task_Print",//类名
                                            "GetEx_Task_Print_Main",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                if (dt1.Rows.Count > 0)
                {
                    label16.Text = dt1.Rows[0]["art_no"].ToString();
                    label17.Text = dt1.Rows[0]["order_po_qty"].ToString();
                    label18.Text = dt1.Rows[0]["shoe_name"].ToString();
                    label19.Text = dt1.Rows[0]["MATERIAL_NAME"].ToString();
                    label20.Text = dt1.Rows[0]["category_name"].ToString();
                    label21.Text = dt1.Rows[0]["makings_id"].ToString();
                    label22.Text = dt1.Rows[0]["product_level_value"].ToString();
                    label23.Text = dt1.Rows[0]["position_name"].ToString();
                    label24.Text = dt1.Rows[0]["order_po"].ToString();
                    label25.Text = dt1.Rows[0]["MANUFACTURER_NAME"].ToString();
                    label26.Text = dt1.Rows[0]["season"].ToString();
                    label27.Text = dt1.Rows[0]["phase_creation_name"].ToString();
                    //label28.Text = dt1.Rows[0]["SHOE_NO"].ToString();//测试种类编号
                    label29.Text = dt1.Rows[0]["makings_type_name"].ToString();
                }


                int id = 0; //序号
                int index = 0; //二维码序号
                string inspection_code = string.Empty;
                string inspection_name = string.Empty; // 检测名称
                string sample_qty = string.Empty; // 试样数量
                string qr_code = string.Empty;// 二维码
                string art_no = string.Empty;// 二维码

                //循环add打印明细
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    index = 0;
                    inspection_code = dt2.Rows[i]["INSPECTION_CODE"].ToString();
                    //test_no = "FT" + "-" + test_no + "-" + id;
                    inspection_name = dt2.Rows[i]["INSPECTION_NAME"].ToString();

                    //如果试样数量为null则不打印该明细
                    if (dt2.Rows[i]["sample_qty"] == null)
                        break;
                    else
                        sample_qty = dt2.Rows[i]["SAMPLE_QTY"].ToString();

                    qr_code = dt2.Rows[i]["TASK_NO"].ToString() + "@" + dt2.Rows[i]["INSPECTION_TYPE"].ToString() + "@" + dt2.Rows[i]["INSPECTION_CODE"].ToString() + "@" + dt2.Rows[i]["SEQ"].ToString();//二维码
                    art_no = dt2.Rows[i]["art_no"].ToString();//二维码
                    id = (id + 1);
                    ItemEX item = new ItemEX(this, id, dt2.Rows[i]["TASK_NO"].ToString(), dt2.Rows[i]["INSPECTION_CODE"].ToString(), dt2.Rows[i]["INSPECTION_NAME"].ToString(), dt2.Rows[i]["INSPECTION_TYPE"].ToString(), dt2.Rows[i]["SEQ"].ToString(), dt2.Rows[i]["SAMPLE_QTY"].ToString(), dt2.Rows[i]["art_no"].ToString());
                    this.flowLayoutPanel1.Controls.Add(item);

                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机

        /// <summary>
        /// 打印全部二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qr_code_print(object sender, EventArgs e)
        {
            string Print = this.comboBox1.Text;
            if (string.IsNullOrEmpty(Print))
            {
                MessageBox.Show("Please select a printer！");
                return;
            }
            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("task_no", task_no);//名称
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_PRINTAPI",//类库名
                                        "SJ_PRINTAPI.QCM_Ex_Task_Print",//类名
                                        "GetEx_Task_Print_MainALL",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (!ret.IsSuccess)
            {
                throw new Exception(ret.ErrMsg);
            }

            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            //视图数据显示

            //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
            var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());


            if (dt2.Rows.Count > 0)
            {
                WriteTxt(dt2, "检测项条码打印(实验室)", Application.StartupPath + "/Printer/BarCodeModel/检测项条码打印(实验室).txt", 1);

                if (string.IsNullOrEmpty(Program.DefaultPrinter))
                {
                    Program.DefaultPrinter = comboBox1.Text;
                    SetDefaultPrinter(Program.DefaultPrinter);
                }
            }

            Thread.Sleep(1000);


            #region 启动答应程序
            Process p = new Process();
            p.StartInfo.FileName = "检测项条码打印(实验室)_print.bat";
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

        private void F_QCM_Ex_Task_Print_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(fPrinterName);
            }
            #endregion

            GetEx_Task_Print_Main();

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

        public static void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里是正常的输出
            Console.WriteLine(e.Data);

        }

        public static void p_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            //这里得到的是错误信息
            Console.WriteLine(e.Data);

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
