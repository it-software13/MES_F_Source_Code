using GDSJ_Framework;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_BDM.UControl;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class F_BDM_PrintBarCode_Main : MaterialForm
    {
        string Type = string.Empty;
        public string RecordStr { get; set; }
        public string MachineCode { get; set; }//设备条码
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_PrintBarCode_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
        private void txt_BarCode_Click(object sender, EventArgs e)
        {
            try
            {
                Type = this.cbo_BarcodeTypeSelection.Text;
                if (Type == "设备条码")
                {
                    //this.flowLayoutPanel1.Controls.Clear();
                    F_BDM_Mashine_Print f_BDM_Mashine_Print = new F_BDM_Mashine_Print(this);
                    f_BDM_Mashine_Print.ShowDialog();
                    if (!string.IsNullOrEmpty(MachineCode))
                    {
                        UcBarCode Uc = new UcBarCode(MachineCode, MachineCode);
                        this.flowLayoutPanel1.Controls.Add(Uc);
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(Type))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择条码类型！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }
                    string sql = GetSql(Type);
                    FrmSelectData frmData = new FrmSelectData(sql, false, Program.Client);
                    frmData.ShowDialog();
                    if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
                    {
                        for (int i = 0; i < frmData.RetData.Rows.Count; i++)
                        {
                            RecordStr += frmData.RetData.Rows[i][1].ToString() + ",";
                            UcBarCode Uc = new UcBarCode(frmData.RetData.Rows[i][1].ToString(), frmData.RetData.Rows[i][2].ToString());
                            this.flowLayoutPanel1.Controls.Add(Uc);
                        }

                        //this.txt_BarCode.Text = frmData.RetData.Rows[0][0].ToString();
                        //plantarea_name = frmData.RetData.Rows[0]["厂区厂商名称"].To
                        //String();
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            

        }

        private void F_BDM_PrintBarCode_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            #region 请求数据

            Dictionary<string, object> data = new Dictionary<string, object>();

            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                 "SJ_QCMAPI", "SJ_QCMAPI.PrintBarCode", "GetBarCodeEnum",
                 Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
            
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
            #endregion
            #region 下拉框数据

            foreach (DataRow item in dt.Rows)
            {
                cbo_BarcodeTypeSelection.Items.Add(item["ENUM_CODE"].ToString());
            }

            #endregion
        }

        public static string GetSql(string Type)
        {
            string sql = string.Empty;
            switch (Type)
            {
                case "材料条码":
                    sql = "SELECT ITEM_NO,NAME_S FROM BDM_RD_ITEM ";//料品信息表
                    break;
                case "容器条码":
                    sql = "SELECT CONTAINER_NO,CONTAINER_NAME FROM BDM_CONTAINERINFORMATION_M";//化学品容器管理
                    break;
                case "人员条码":
                    sql = "SELECT STAFF_NO,STAFF_NAME FROM HR001M";//HR001M
                    break;
                case "检验工具条码":
                    sql = "SELECT INSPECT_TOOL_CODE,INSPECT_TOOL_NAME FROM BDM_INSPECT_TOOL_M ";//检验工具检验单主表
                    break;
                case "设备条码":
                    sql = "";
                    break;
                case "产线条码":
                    sql = "SELECT PRODUCTIONLINE_NO,PRODUCTIONLINE_NAME FROM BDM_QUALITY_DEPARTMENT_D";//部门产线
                    break;
                case "库位条码":
                    sql = "SELECT LOCATION_NO,LOCATION_NAME FROM BDM_LABORATORYSAMPLE_LOCATION "; //实验室样品库位
                    break;
                case "样品条码":
                    sql = "SELECT ITEM_NO,ITEM_NAME FROM QCM_LABORATORYSAMPLE_STORAGE_M";//实验室样品存放管理
                    break;
                default:
                    break;
            }
            return sql;
        }

        private void printbtn_Click(object sender, EventArgs e)
        {
            if (Type == "设备条码")
            {
                if (string.IsNullOrEmpty(MachineCode))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择需要打印内容！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(RecordStr))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择需要打印内容！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                if (string.IsNullOrEmpty(Type))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择条码打印类型！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
            }


            string printType = "Microsoft Print to PDF";

            Dictionary<string, object> dataDetail = new Dictionary<string, object>();
            dataDetail.Add("Type", Type);
            if (Type == "设备条码")
            {

                dataDetail.Add("RecordStr", MachineCode.TrimEnd(','));
            }
            else
            {
                dataDetail.Add("RecordStr", RecordStr.TrimEnd(','));
            }
               
            string retdataDetail = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.PrintBarCode",//类名
                                                       "GetPrintDetailPrint",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(dataDetail));

            var retDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdataDetail);
            var datasourceDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retDetail["RetData"].ToString());
            if (datasourceDetail.Rows.Count > 0)
            {
                WriteTxt(datasourceDetail, "条码打印", Application.StartupPath + "/Printer/BarCodeModel/条码打印.txt", 1);
                Program.DefaultPrinter = printType;
                SetDefaultPrinter(Program.DefaultPrinter);
            }

            Thread.Sleep(1000);
            
            #region 启动答应程序
            Process p = new Process();
            p.StartInfo.FileName = "条码打印_print.bat";
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            //p.Start();//启动 
            if (p.Start())
            {
                MessageBox.Show("打印成功！");
            }
            else
            {
                MessageBox.Show("打印失败！");
            }

            p.WaitForExit(5 * 1000);//等待上述进程执行完毕
            //p.WaitForExit();//这个会一直等待
            if (p.HasExited == false)
            {
                p.Kill();
            }
            #endregion
            

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

        private void txt_BarCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_BarCode_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Controls.Clear();
        }
    }
}
