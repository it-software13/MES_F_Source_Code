using SJeMES_Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjeMES_QCM_Ex
{
    public partial class ItemEX : UserControl
    {
        F_QCM_Ex_Task_Print p_frm;//父窗体
        string id = string.Empty; //序号
        string task_no = string.Empty; 
        string inspection_code = string.Empty; 
        string inspection_name = string.Empty; 
        string inspection_type = string.Empty;
        string seq = string.Empty;
        string sample_qty = string.Empty; 
        string art_no = string.Empty; //Added on 20240717

        public ItemEX(F_QCM_Ex_Task_Print _p_frm, int id, string task_no,string inspection_code,string inspection_name,string inspection_type,string seq,string sample_qty,string art_no)
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.p_frm = _p_frm;
            this.id = id.ToString();
            this.task_no = task_no;
            this.inspection_code = inspection_code;
            this.inspection_name = inspection_name;
            this.inspection_type = inspection_type;
            this.seq = seq;
            this.sample_qty = sample_qty;
            this.art_no = art_no;//Added on 20240717
        }
        private void ItemEX_Load(object sender, EventArgs e)
        {
            this.lab_seq.Text = this.id;
            this.JYDNo.Text = this.task_no;
            this.JYXNo.Text = this.inspection_code + "-" + this.seq; //生成编号
            this.JYName.Text = this.inspection_name;
            this.SY_qty.Text = this.sample_qty;
            this.article.Text = this.art_no;

            string code = task_no + "@" + inspection_type + "@" + inspection_code + "@" + seq;
            if (!string.IsNullOrEmpty(code))
                this.pictureBox1.Image = QRCode.CreateQRCode(code);//  QRCode.CreateQRCode(code);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Print = this.p_frm.comboBox1.Text;
            if (string.IsNullOrEmpty(Print))
            {
                MessageBox.Show("Please select a printer！");
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("TASK_NO", typeof(string));
            dt.Columns.Add("INSPECTION_CODE", typeof(string));
            dt.Columns.Add("INSPECTION_NAME", typeof(string));
            dt.Columns.Add("SEQ", typeof(string));
            dt.Columns.Add("SAMPLE_QTY", typeof(string));
            dt.Columns.Add("qr_code", typeof(string));
            dt.Columns.Add("art_no", typeof(string));
            DataRow addRow = dt.NewRow();
            addRow["TASK_NO"] = this.task_no;
            addRow["INSPECTION_CODE"] = this.inspection_code;
            addRow["INSPECTION_NAME"] = this.inspection_name;
            addRow["SEQ"] = this.seq;
            addRow["SAMPLE_QTY"] = this.sample_qty;
            addRow["qr_code"] = task_no + "@" + inspection_type + "@" + inspection_code + "@" + seq;
            addRow["art_no"] = this.art_no;
            dt.Rows.Add(addRow);

            F_QCM_Ex_Task_Print.WriteTxt(dt, "检测项条码打印(实验室)", Application.StartupPath + "/Printer/BarCodeModel/检测项条码打印(实验室).txt", 1);
            if (string.IsNullOrEmpty(Program.DefaultPrinter))
            {
                Program.DefaultPrinter = Print;
                F_QCM_Ex_Task_Print.SetDefaultPrinter(Program.DefaultPrinter);
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

            MessageBox.Show("打印成功！");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
