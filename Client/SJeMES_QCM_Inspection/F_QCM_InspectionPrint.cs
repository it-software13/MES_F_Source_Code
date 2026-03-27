using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_QCM_Inspection
{
    public partial class F_QCM_InspectionPrint : MaterialForm
    {

        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 检验单
        /// </summary>
        public string INSPECTION_NO { get; set; }
        
        public F_QCM_InspectionPrint(string INSPECTION_NO_order)
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            INSPECTION_NO = INSPECTION_NO_order;
            //INSPECTION_NO = "SYD202110141143";
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
        //初始化界面
        private void InspectionPrint_Load(object sender, EventArgs e)
        { 
            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                cbo_BarCode.Items.Add(fPrinterName);
            }
            #endregion

            this.DesktopBounds = Screen.GetWorkingArea(this); // 在桌面区域全屏显示。
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("INSPECTION_NO", INSPECTION_NO);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.InspectionPrint",//类名
                                                       "GetPrintDetail",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            var datasource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(ret["RetData"].ToString());
            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                #region 打印单头
                string order = datasource[0]["INSPECTION_NO"].ToString(); // 检测单号
                UcInspectionPrintHead UcPrintHead = new UcInspectionPrintHead(order);

                UcPrintHead.No.Text = order; // 单号

                if (datasource[0]["GENERAL_TESTTYPE_NAME"] != null)// 送检类型
                    UcPrintHead.SJType1.Text = datasource[0]["GENERAL_TESTTYPE_NAME"].ToString(); 
                else
                    UcPrintHead.SJType1.Text = "";

                if (datasource[0]["INSPECTION_DATE"] != null)// 提交日期
                    UcPrintHead.JCsubmit.Text = datasource[0]["INSPECTION_DATE"].ToString(); 
                else
                    UcPrintHead.JCsubmit.Text = "";

                if (datasource[0]["CATEGORY_NAME"] != null)
                    UcPrintHead.SYKind1.Text = datasource[0]["CATEGORY_NAME"].ToString(); // 试样种类
                else
                    UcPrintHead.SYKind1.Text = "";

                //if (datasource[0]["GENERAL_TESTTYPE_NAME"] != null)
                //    UcPrintHead.TYPEJC.Text = datasource[0]["GENERAL_TESTTYPE_NAME"].ToString(); // 检测类型
                //else
                //    UcPrintHead.TYPEJC.Text = "";

                if (datasource[0]["ART_CODE"] != null)
                    UcPrintHead.ARTTest.Text = datasource[0]["ART_CODE"].ToString(); // ART
                else
                    UcPrintHead.ARTTest.Text = "";



                if (datasource[0]["DEPARTMENT_NAME"] != null)
                    UcPrintHead.JDText.Text = datasource[0]["DEPARTMENT_NAME"].ToString(); // 阶段
                else
                    UcPrintHead.JDText.Text = "";




                if (datasource[0]["PLANTAREA_NAME"] != null)
                    UcPrintHead.AreaText.Text = datasource[0]["PLANTAREA_NAME"].ToString(); // 厂区
                else
                    UcPrintHead.AreaText.Text = "";

                UcPrintHead.Dock = DockStyle.Fill;

                this.panel1.Controls.Add(UcPrintHead);

                #endregion

                #region 打印单身

                Dictionary<string, object> dataDetail = new Dictionary<string, object>();
                dataDetail.Add("INSPECTION_NO", INSPECTION_NO);
                string retdataDetail = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                           Program.Client.APIURL,
                                                           "SJ_QCMAPI",//类库名
                                                           "SJ_QCMAPI.InspectionPrint",//类名
                                                           "GetPrintDetailList",//方法名
                                                           Program.Client.UserToken,//token
                                                           Newtonsoft.Json.JsonConvert.SerializeObject(dataDetail));

                var retDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdataDetail);
                var datasourceDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(retDetail["RetData"].ToString());

                //计算试样数量
                int sample_num_count = 0;
                for (int i = 0; i < datasourceDetail.Count; i++)
                {
                    if (datasourceDetail[i]["SAMPLE_NUM"] == null)
                        break;
                    else
                        sample_num_count += Convert.ToInt32(datasourceDetail[i]["SAMPLE_NUM"].ToString());
                }

                int id = 0; //序号
                int index = 0; //二维码序号
                string TESTITEM_CODE = string.Empty; //检测编号
                string testitem_name = string.Empty; // 检测名称
                string sample_num = string.Empty; // 试样数量
                string qr_code = string.Empty;// 二维码

    
                //循环add打印明细
                for (int i = 0; i < datasourceDetail.Count; i++)
                {
                    index = 0;

                    TESTITEM_CODE = datasourceDetail[i]["TESTITEM_CODE"].ToString();
                    //test_no = "FT" + "-" + test_no + "-" + id;
                    testitem_name = datasourceDetail[i]["TESTITEM_NAME"].ToString();

                    //如果试样数量为null则不打印该明细
                    if (datasourceDetail[i]["SAMPLE_NUM"] == null)
                        break;
                    else
                        sample_num = datasourceDetail[i]["SAMPLE_NUM"].ToString();

                    for (int j = 0; j < Convert.ToInt32(datasourceDetail[i]["SAMPLE_NUM"].ToString()); j++)
                    {
                        qr_code = datasourceDetail[i]["INSPECTION_NO"].ToString() + "@" + datasourceDetail[i]["TESTITEM_CODE"].ToString() + "@" + (index + 1);//二维码
                        id = (id + 1);
                        index = (j + 1);
                        string test_no = "";
                        test_no = /*"FT" + "-" +*/ TESTITEM_CODE + "-" + (index);
                        Item item = new Item(id, order, test_no, testitem_name, sample_num, qr_code);
                        this.flowLayoutPanel1.Controls.Add(item);

                    }

                }
                #endregion

            }
             
        }

        private void Back_btn(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打印全部二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qr_code_print(object sender, EventArgs e)
        {
            string Print = this.cbo_BarCode.Text;
            if (string.IsNullOrEmpty(Print))
            {
                MessageBox.Show("请选择打印机！");
                return;
            }
            Dictionary<string, object> dataDetail2 = new Dictionary<string, object>();
            dataDetail2.Add("INSPECTION_NO", INSPECTION_NO);
            string retdataDetail2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.InspectionPrint",//类名
                                                       "GetPrintDetailPrint",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(dataDetail2));

            var retDetail2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdataDetail2);
            var datasourceDetail2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retDetail2["RetData"].ToString());
            if (datasourceDetail2.Rows.Count > 0)
            {
                WriteTxt(datasourceDetail2, "送检单条码(实验室)", Application.StartupPath + "/Printer/BarCodeModel/送检单条码(实验室).txt", 1);
                Program.DefaultPrinter = Print;
                SetDefaultPrinter(Program.DefaultPrinter);
            }

            Thread.Sleep(1000);

            #region 启动答应程序
            //Process p = new Process();

            //p.StartInfo.FileName = Application.StartupPath + $@"\送检单条码(实验室).bat";

            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardError = true;
            //p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.UseShellExecute = false;
            //p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            //p.Start();
            //p.BeginOutputReadLine();
            //p.BeginErrorReadLine();
            //p.WaitForExit(1 * 1000);//等待上述进程执行完毕
            //if (p.HasExited == false)
            //{
            //    p.Kill();
            //}
            #endregion


            #region 启动答应程序
            Process p = new Process();
            p.StartInfo.FileName = "送检单条码(实验室)_print.bat";
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

            MessageBox.Show("打印成功！");
 
        }

        public static bool WriteTxt(DataTable dt, string ModelName, string Path, int printQty)
        {
            try
            {
                string Data = string.Empty;

                string FilePath = Path ;

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

      
    }

}
