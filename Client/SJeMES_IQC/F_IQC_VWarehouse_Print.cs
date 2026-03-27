using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
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

namespace SJeMES_IQC
{
    public partial class F_IQC_VWarehouse_Print : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private Dictionary<string, object> dics;
        public F_IQC_VWarehouse_Print(Dictionary<string,object> dic)
        {
            InitializeComponent();
            dics = dic;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
         Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
        private void F_IQC_VWarehouse_Print_Load(object sender, EventArgs e)
        {

            string code = dics[""].ToString() ;
            if (!string.IsNullOrEmpty(code))
                this.pictureBox1.Image = QRCode.CreateQRCode(code);

            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                cbo_BarCode.Items.Add(fPrinterName);
            }
            #endregion
        }
        private void Printbtn_Click(object sender, EventArgs e)
        {
            string Print = this.cbo_BarCode.Text;
            if (string.IsNullOrEmpty(Print))
            {
                MessageBox.Show("Please select a printer！");
                return;
            }
            Dictionary<string, object> dataDetail2 = new Dictionary<string, object>();
            //dataDetail2.Add("INSPECTION_NO", INSPECTION_NO);
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
               
            }

            Thread.Sleep(1000);



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

            MessageBox.Show("Printed successfully！");
        }

        
    }
}
