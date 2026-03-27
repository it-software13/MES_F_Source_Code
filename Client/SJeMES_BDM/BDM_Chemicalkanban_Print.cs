using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
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

namespace SJeMES_BDM
{
    public partial class BDM_Chemicalkanban_Print : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private string chemical_no = string.Empty;
        private string effective_time = string.Empty;
        public BDM_Chemicalkanban_Print()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(string Name); //调用win api将指定名称的打印机设置为默认打印机
        private void BDM_Chemicalkanban_Print_Load(object sender, EventArgs e)
        {
            lab1.Text = "";//胶水名称
            lab2.Text = "";//Drug name
            lab3.Text = "";//Drug ratio
            lab4.Text = "";//对应温度
            lab5.Text = "";//调胶时间
            lab6.Text = "";//有效期
            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                cbo_BarCode.Items.Add(fPrinterName);
            }
            #endregion
        }
        //        private void button1_Click(object sender, EventArgs e)
        //        {
        //            string sql = $@"SELECT
        //CASE
        //	when chemical_category='0' then 'Glue'
        //	when chemical_category='1' then 'Treatment agent'
        //	when chemical_category='2' then 'Other'
        //end as type,
        //   chemical_no,
        //	chemical_name,
        //	medicament_name,
        //    reagent_proportion,
        //	corresponding_humidity,
        //	effective_time 
        //FROM
        //	Bdm_chemical_infomaintenance_m  order by id desc
        //";
        //            string hread = "Chemical Code";
        //            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client, hread);
        //            frmData.ShowDialog();

        //            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
        //            {
        //                lab1.Text = frmData.RetData.Rows[0]["chemical_name"].ToString();//胶水名称
        //                lab2.Text = frmData.RetData.Rows[0]["medicament_name"].ToString();//Drug name
        //                lab3.Text = frmData.RetData.Rows[0]["reagent_proportion"].ToString();//Drug ratio
        //                lab4.Text = frmData.RetData.Rows[0]["corresponding_humidity"].ToString();//对应温度

        //                chemical_no=frmData.RetData.Rows[0]["chemical_no"].ToString();//Chemical Code
        //                effective_time= frmData.RetData.Rows[0]["effective_time"].ToString();//Effective time(H)



        //            }
        //        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $@"SELECT
CASE
	when chemical_category='0' then '胶水类'
	when chemical_category='1' then '处理剂类'
	when chemical_category='2' then '其他类'
end as 类型,
   chemical_no as 化学品代号,
	chemical_name as 化学品名称,
	medicament_name as 药剂名称,
    reagent_proportion as 药剂比例,
	corresponding_humidity as 对应湿度,
	effective_time as 有效时间
FROM
	Bdm_chemical_infomaintenance_m  order by id desc
";
            string hread = "化学品代号";
            FrmSelectData frmData = new FrmSelectData(sql, true, Program.Client, hread);
            frmData.ShowDialog();

            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                lab1.Text = frmData.RetData.Rows[0]["化学品名称"].ToString();//胶水名称
                lab2.Text = frmData.RetData.Rows[0]["药剂名称"].ToString();//药剂名称
                lab3.Text = frmData.RetData.Rows[0]["药剂比例"].ToString();//药剂比例
                lab4.Text = frmData.RetData.Rows[0]["对应湿度"].ToString();//对应温度

                chemical_no = frmData.RetData.Rows[0]["化学品代号"].ToString();//化学品代号
                effective_time = frmData.RetData.Rows[0]["有效时间"].ToString();//有效时间(H)



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image= new Bitmap(this.pictureBox1.Image.Width, this.pictureBox1.Image.Height);
            }
            lab1.Text = "";//胶水名称
            lab2.Text = "";//Drug name
            lab3.Text = "";//Drug ratio
            lab4.Text = "";//对应温度
            lab5.Text = "";//调胶时间
            lab6.Text = "";//有效期
            chemical_no = string.Empty;
            effective_time = string.Empty;
            cbo_BarCode.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Print = this.cbo_BarCode.Text;
            if (string.IsNullOrWhiteSpace(chemical_no) || string.IsNullOrWhiteSpace(effective_time))
            {
                MessageBox.Show("Select the material list first and then print");
                return;
            }
            if (string.IsNullOrEmpty(Print))
            {
                MessageBox.Show("Please select a printer！");
                return;
            }

            else
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("chemical_no", chemical_no);
                p.Add("effective_time", effective_time);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                           "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Chemicalkanban",//类名
                                            "Commit_Printdata",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (!Convert.ToBoolean(dic["IsSuccess"].ToString()))
                {
                    MessageBox.Show(dic["ErrMsg"].ToString());
                }
                else
                {
                    Dictionary<string, object> dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["RetData1"].ToString());
                    lab5.Text = dic2["g_mixing_time"].ToString();//调胶时间
                    lab6.Text = dic2["effective_time"].ToString();//有效期
                    this.pictureBox1.Image = QRCode.CreateQRCode(dic2["id"].ToString());
                    DataTable dt = new DataTable();
                    dt.Columns.Add("chemical_name", typeof(string));
                    dt.Columns.Add("medicament_name", typeof(string));
                    dt.Columns.Add("reagent_proportion", typeof(string));
                    dt.Columns.Add("corresponding_humidity", typeof(string));
                    dt.Columns.Add("id", typeof(string));
                    dt.Columns.Add("g_mixing_time", typeof(string));
                    dt.Columns.Add("effective_time", typeof(string));
                    DataRow dr = dt.NewRow();
                    dr["chemical_name"] = lab1.Text;
                    dr["medicament_name"] = lab2.Text;
                    dr["reagent_proportion"] = lab3.Text;
                    dr["corresponding_humidity"] = lab4.Text;
                    dr["id"] = dic2["id"].ToString();
                    dr["g_mixing_time"] = lab5.Text;
                    dr["effective_time"] = lab6.Text;
                    dt.Rows.Add(dr);

                    if (dt.Rows.Count > 0)
                    {
                        WriteTxt(dt, "调胶化学品打印", Application.StartupPath + "/Printer/BarCodeModel/调胶化学品打印.txt", 1);
                        if (string.IsNullOrEmpty(Program.DefaultPrinter))
                        {
                            Program.DefaultPrinter = cbo_BarCode.Text;
                            SetDefaultPrinter(Program.DefaultPrinter);
                        }
                        Thread.Sleep(1000);
                        #region 启动答应程序
                        Process p1 = new Process();
                        p1.StartInfo.FileName = "调胶化学品打印_print.bat";
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

        private void cbo_BarCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Print = this.cbo_BarCode.Text;
            Program.DefaultPrinter = Print;
            if (!SetDefaultPrinter(Program.DefaultPrinter))
            {
                MessageBox.Show("Setting up the printer failed");
            }
        }
    }
}
