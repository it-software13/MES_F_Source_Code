using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_QCM.UControl;
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

namespace SJeMES_QCM
{
    public partial class F_QCM_ChemicalPrint : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_ChemicalPrint()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }
        public List<string> Record_no { get; set; }//被选中的化学品代号
        DataTable dt = new DataTable();
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
        //清空二维码
        private void button1_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Controls.Clear();
        }

        private void txt_BarCode_TextChanged(object sender, EventArgs e)
        {
           
        }
        //打印
        private void printbtn_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dt.Rows.Count > 0)
                {
                    WriteTxt(dt, "化学品条码打印", Application.StartupPath + "/Printer/BarCodeModel/化学品条码打印.txt", 1);
                    Program.DefaultPrinter = "Microsoft Print to PDF";
                    SetDefaultPrinter(Program.DefaultPrinter);

                    Thread.Sleep(1000);

                    #region 启动答应程序
                    Process p = new Process();
                    p.StartInfo.FileName = "化学品条码打印_print.bat";
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.UseShellExecute = false;
                    p.Start();//启动 

                    MessageBox.Show("打印成功！");
                    

                    p.WaitForExit(5 * 1000);//等待上述进程执行完毕
                                            //p.WaitForExit();//这个会一直等待
                    if (p.HasExited == false)
                    {
                        p.Kill();
                    }
                    #endregion
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("请选择需要打印内容！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            
        }
        //弹窗
        private void txt_BarCode_Click(object sender, EventArgs e)
        {
            string RecordStr = string.Empty;

            var sql = $@"
select CHEMICAL_NO as 化学品代号,CHEMICAL_NAME as 化学品名称,nvl(EFFECTIVE_TIME,0) as 有效时间  from bdm_Chemicalglue_m ";
            
            FrmSelectData frmData = new FrmSelectData(sql, false, Program.Client);
            frmData.ShowDialog();
            if (frmData.RetData != null && frmData.RetData.Rows.Count > 0)
            {
                for (int i = 0; i < frmData.RetData.Rows.Count; i++)
                {
                    RecordStr += frmData.RetData.Rows[i][1].ToString() + ",";
                    UcChemicalsCode Uc = new UcChemicalsCode(frmData.RetData.Rows[i][1].ToString(), frmData.RetData.Rows[i][2].ToString(), frmData.RetData.Rows[i][3].ToString());
                    //Record_no.Add(frmData.RetData.Rows[i][1].ToString());
                    this.flowLayoutPanel1.Controls.Add(Uc);
                    DataRow dr = dt.NewRow();

                    dr["化学品代号"] = Uc.txt_no2;
                    dr["化学品名称"] = Uc.txt_name2;
                    dr["调胶时间"] = Uc.time2;
                    dr["有效期"] = Uc.txt_eff2;

                    dt.Rows.Add(dr);

                   
                }
                string aa = "1";
                //this.txt_BarCode.Text = frmData.RetData.Rows[0][0].ToString();
                //plantarea_name = frmData.RetData.Rows[0]["厂区厂商名称"].To
                //String();
                //}
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

        private void F_QCM_ChemicalPrint_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            dt.Columns.Add("化学品代号", typeof(string));
            dt.Columns.Add("化学品名称", typeof(string));
            dt.Columns.Add("调胶时间", typeof(string));
            dt.Columns.Add("有效期", typeof(string));
        }
    }
}
