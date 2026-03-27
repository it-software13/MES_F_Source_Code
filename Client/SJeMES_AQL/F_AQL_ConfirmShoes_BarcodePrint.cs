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
    public partial class F_AQL_ConfirmShoes_BarcodePrint : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        string prod_no = string.Empty;
        string MODULE_TYPE = string.Empty;
        public F_AQL_ConfirmShoes_BarcodePrint(string _MODULE_TYPE)
        {
            InitializeComponent();
            MODULE_TYPE = _MODULE_TYPE;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetConfirmShoes_BarcodePrint();
        }

        /// <summary>
        /// 查询-确认鞋-条码打印-aql
        /// </summary>
        /// <param name="a">为了防止dataGridView1里添加的按钮重复 </param>
        public void GetConfirmShoes_BarcodePrint()
        {
            try
            {

                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                //键值对传值
                p.Add("prod_no", textBox1.Text.Trim());
                //p.Add("MODULE_TYPE", MODULE_TYPE);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                            "GetConfirmShoes_BarcodePrint",//方法名
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
                if (dt.Rows.Count > 0)
                {
                    prod_no = dt.Rows[0]["prod_no"].ToString();
                    label6.Text = dt.Rows[0]["prod_no"].ToString();
                    label7.Text = dt.Rows[0]["shoe_name"].ToString();
                    label8.Text = dt.Rows[0]["develop_season"].ToString();
                    label9.Text = dt.Rows[0]["rule_no"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["file_url"].ToString()))
                    {
                        try
                        {
                            var webC = new System.Net.WebClient();
                            string url = Program.Client.PicUrl + Convert.ToString(dt.Rows[0]["file_url"].ToString());
                            Image image = new Bitmap(webC.OpenRead(url));
                            pictureBox1.Image = image;
                        }
                        catch { }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        public string CurrDefaultPrinter = "";

        private void F_AQL_ConfirmShoes_BarcodePrint_Load(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            CurrDefaultPrinter = printDocument.PrinterSettings.PrinterName;
            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(fPrinterName);
            }
            #endregion

            if (!string.IsNullOrEmpty(CurrDefaultPrinter))
                comboBox1.SelectedItem = CurrDefaultPrinter;

            if (MODULE_TYPE == "0")
            {
                radioButton1.Checked = true;
                radioButton2.Visible = false;//确认鞋隐藏原材料检验股
            }
            else
            {
                radioButton1.Visible = false;//原材料隐藏验货室
                radioButton2.Checked = true;
            }
            SJeMES_Framework.Common.UIHelper.AdjustComboBoxDropDownListWidth(comboBox1);

        }

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机

        /// <summary>
        /// 打印全部二维码-aql
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qr_code_print(object sender, EventArgs e)
        {
            string footType = string.Empty;
            if (this.radioButton4.Checked == true)
                footType = "0";
            if (this.radioButton3.Checked == true)
                footType = "1";
            if (this.radioButton5.Checked == true)
                footType = "2";

            if (!string.IsNullOrWhiteSpace(label6.Text))
            {
                groupBox3.Visible = true;
                label15.Text = label6.Text;
                label16.Text = label7.Text;
                if (radioButton1.Checked)
                    label18.Text = "inspection room";//验货室
                else
                    label18.Text = "Raw material inspection unit";//原材料检验股
                if (!string.IsNullOrEmpty(prod_no))
                {
                    if (string.IsNullOrWhiteSpace(textBox2.Text))
                        textBox2.Text = "3";
                    if (string.IsNullOrWhiteSpace(textBox3.Text))
                        textBox3.Text = "30";
                    string gs = string.Empty;
                    if (label18.Text == "inspection room")
                        gs = "0";
                    else if (label18.Text == "Raw material inspection unit")
                        gs = "1";

                    string qrcode = prod_no + ";" + textBox2.Text + ";" + textBox3.Text + ";" + gs + ";" + footType;
                    this.pictureBox2.Image = QRCode.CreateQRCode(qrcode);
                }
            }

            string Print = this.comboBox1.Text;
            if (string.IsNullOrEmpty(Print))
            {
                MessageBox.Show("Please select a printer！");
                return;
            }

            //请求api的数据展示
            Dictionary<string, object> data = new Dictionary<string, object>();
            //键值对传值
            data.Add("prod_no", prod_no);
            data.Add("MODULE_TYPE", MODULE_TYPE);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_AQLAPI",//类库名
                                        "SJ_AQLAPI.AQL_ConfirmShoes",//类名
                                        "GetConfirmShoes_BarcodePrint",//方法名
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
                string footTypeText = string.Empty;
                if (radioButton4.Checked == true)
                    footTypeText = radioButton4.Text;
                if (radioButton3.Checked == true)
                    footTypeText = radioButton3.Text;
                if (radioButton5.Checked == true)
                    footTypeText = radioButton5.Text;


                DataTable dt = new DataTable();
                dt.Columns.Add("PROD_NO");
                dt.Columns.Add("SHOE_NAME");
                dt.Columns.Add("ATTRIBUTION");
                dt.Columns.Add("qr_code");
                dt.Columns.Add("footType");
                dt.Columns.Add("ISSUE_DATE");
                dt.Columns.Add("REVIEW_DATE");
                dt.Columns.Add("MCS_LOCATION");
                dt.Columns.Add("SEASON");
                dt.Columns.Add("INSPECTOR");
                dt.Rows.Add();
                dt.Rows[0]["PROD_NO"] = dt2.Rows[0]["PROD_NO"];
                dt.Rows[0]["SHOE_NAME"] = dt2.Rows[0]["SHOE_NAME"];
                dt.Rows[0]["footType"] = footTypeText;
                if (radioButton1.Checked)
                    dt.Rows[0]["ATTRIBUTION"] = "inspection room";//验货室
                else
                    dt.Rows[0]["ATTRIBUTION"] = "Raw material inspection unit";
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                    textBox2.Text = "3";
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                    textBox3.Text = "30";
                string gs = string.Empty;
                if (label18.Text == "inspection room")
                    gs = "0";
                else if (label18.Text == "Raw material inspection unit")
                    gs = "1";


                txt_foot.Text = footTypeText;

                string qrcode = dt2.Rows[0]["PROD_NO"] + ";" + textBox2.Text + ";" + textBox3.Text + ";" + gs + ";" + footType;
                dt.Rows[0]["qr_code"] = qrcode;
                //WriteTxt(dt, "ComfirmShoes", Application.StartupPath + "/Printer/BarCodeModel/ComfirmShoes.txt", 1);//出货确认鞋条码打印
                WriteTxt(dt, "出货确认鞋条码打印", Application.StartupPath + "/Printer/BarCodeModel/出货确认鞋条码打印.txt", 1);

                SetDefaultPrinter(comboBox1.Text);
                if (string.IsNullOrEmpty(Program.DefaultPrinter))
                {
                    Program.DefaultPrinter = comboBox1.Text;
                    SetDefaultPrinter(Program.DefaultPrinter);
                }
            }

            Thread.Sleep(1000);


            #region 启动答应程序
            Process p = new Process();
            //p.StartInfo.FileName = "ComfirmShoes.bat";//出货确认鞋条码打印.bat//ComfirmShoes

            p.StartInfo.FileName = "ComfirmShoes.bat";//出货确认鞋条码打印.bat//ComfirmShoes
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

            if (!string.IsNullOrEmpty(CurrDefaultPrinter))
                SetDefaultPrinter(CurrDefaultPrinter);

        }

        public static bool WriteTxt(DataTable dt, string ModelName, string Path, int printQty)
        {
            try
            {
                string Data = string.Empty;

                string FilePath = Path;

                foreach (DataColumn dc in dt.Columns)
                {
                    Data += dc.ColumnName + ",";
                }

                Data = Data.Remove(Data.Length - 1) + "\r\n";

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < printQty; i++)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Data += dr[dc].ToString() + ",";
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
