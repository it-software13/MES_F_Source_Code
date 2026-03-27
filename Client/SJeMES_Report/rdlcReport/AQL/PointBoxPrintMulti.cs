using Microsoft.Reporting.WinForms;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Report.AQL
{
    public partial class PointBoxPrintMulti : Form
    {
        public string[] _POlist;
        public string _APIURL;
        public string _token;
        public string _ybjb;
        public string _aqljb;
        public string _Language;

        public PointBoxPrintMulti(string[] POlist, string APIURL, string token, string ybjb_value, string ybjb, string aqljb,string boxtype,string Language)
        {
            InitializeComponent();
            _APIURL = APIURL;
            _Language = Language;
            _token = token;
            try
            {
                InitialReport_NEW(POlist, ybjb_value, ybjb.Replace("一般检验水平", "").Replace("特殊检验水平", ""), aqljb, boxtype);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #region 获取打印机设备 
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(fPrinterName);
            }
            SJeMES_Framework.Common.UIHelper.AdjustComboBoxDropDownListWidth(comboBox1);
            #endregion

        }

        public void InitialReport_NEW(string[] POlist,string ybjb_value, string ybjb, string aqljb,string boxtype)
        {
            int x = 1;
            var plHeight = flp_rv_list.Height;
            var plWidth = flp_rv_list.Width - 30;
            foreach (var item in POlist)
            {
                //获取dic数据
                string sql = $@"
select
ROWNUM as RN,
a.MER_PO,-- PO
b.SE_QTY, -- 订单数量
b.PROD_NO,--art
c.name_t,
a.DESCOUNTRY_NAME as SHIPCOUNTRY_NAME,-- 国家
s.size_no,
b.SE_QTY -- 订单双数
FROM
BDM_SE_ORDER_MASTER a
	LEFT JOIN BDM_SE_ORDER_ITEM b ON a.SE_ID = b.SE_ID AND a.ORG_ID= b.ORG_ID
	LEFT JOIN BDM_RD_STYLE c ON b.shoe_no = c.shoe_no
LEFT JOIN BDM_SE_ORDER_SIZE s on b.se_id=s.se_id
WHERE
	a.MER_PO = '{item}' ";
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("sql", sql);
                string retdata1 = WebAPIHelper.Post(
                                          _APIURL,
                                          "SJ_AQLAPI",//类库名
                                          "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                          "GetDatalist",//方法名
                                          _token,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }


                DataTable data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);

                string art = string.Empty;
                string shoe_name = string.Empty;
                string num = string.Empty;
                string po = string.Empty;
                string guojia = string.Empty;
                string level = string.Empty;
                string num_total = string.Empty;
                if (data.Rows.Count > 0)
                {
                    art = data.Rows[0]["PROD_NO"].ToString();
                    shoe_name = data.Rows[0]["name_t"].ToString();

                    num = data.Rows[0]["SE_QTY"].ToString();
                    po = data.Rows[0]["MER_PO"].ToString();
                    num_total= data.Rows[0]["SE_QTY"].ToString(); ;
                    guojia = data.Rows[0]["SHIPCOUNTRY_NAME"].ToString();
                    level = ybjb;
                }

                //查询抽样数据
                //请求api的数据展示
                Dictionary<string, object> dic = new Dictionary<string, object>();
                //键值对传值
                dic.Add("ac", aqljb);
                dic.Add("num", num);
                dic.Add("LEVEL_TYPE", ybjb_value);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            _APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_PointBox",//类名
                                            "GetAQLPointBox_SamplingRate",//方法名
                                            _token,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(dic));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret2 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret2.IsSuccess)
                {
                    throw new Exception(ret2.ErrMsg);
                }

                 dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2.RetData);
                //视图数据显示

                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                var dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
                var dt1213 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1213"].ToString());

                sql = $@"
SELECT
	bb.CR_SIZE,
	SUM( bb.SE_QTY ) SE_QTY 
FROM
	(
	SELECT
		s.SIZE_NO as CR_SIZE,
		s.SE_QTY,
			s.SIZE_SEQ
	FROM
		BDM_SE_ORDER_MASTER m
		INNER JOIN BDM_SE_ORDER_SIZE s ON m.ORG_ID = s.ORG_ID 
		AND m.SE_ID = s.SE_ID 
	WHERE
		m.MER_PO = '{item}' 
	) bb 
GROUP BY bb.CR_SIZE 
ORDER BY  MAX(bb.SIZE_SEQ)";
                p = new Dictionary<string, object>();
                p.Add("sql", sql);
                string size_retdata1 = WebAPIHelper.Post(
                                          _APIURL,
                                          "SJ_AQLAPI",//类库名
                                          "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                          "GetDatalist",//方法名
                                          _token,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject size_ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(size_retdata1);

                if (!size_ret.IsSuccess)
                {
                    throw new Exception(size_ret.ErrMsg);
                }
                DataTable PointBoxdt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(size_ret.RetData);

                decimal VALS = 0M;
                
                string act = string.Empty;
                string ret0 = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    VALS = Convert.ToDecimal(dt.Rows[0]["VALS"].ToString());//双数
                    //act = dt.Rows[0]["ac"].ToString();
                    //ret0 = (int.Parse(dt.Rows[0]["ac"].ToString())+1).ToString();
                }

                CalculateEvenNumbers_Print(PointBoxdt, VALS.ToString(), num);

                string sample_proportion = Math.Round((VALS / decimal.Parse(num)) * 100, 2).ToString() + "%"; ;//抽样比例
                string ac1 = string.Empty;
                string ac2 = string.Empty;
                string re2 = string.Empty;
                string re1 = string.Empty;
                if (dt1213.Rows.Count > 0)
                {
                    ac1 = dt1213.Rows[0]["AC13"].ToString();//ac1
                    ac2 = dt1213.Rows[0]["AC12"].ToString();//ac2
                    re2 = (Convert.ToInt32(dt1213.Rows[0]["AC12"].ToString()) + 1).ToString();//re2

                    
                    re1 = (int.Parse(dt1213.Rows[0]["AC13"].ToString()) + 1).ToString();//re1


                    ret0 = re1;//ac1
                    act = ac1;//ac2
                }
                //string act = dt.Rows[0]["ac"].ToString();//act

                string ac3 = "0";//ac3

                
                //string re1 = dt1213.Rows[0]["AC13"].ToString();//re1
               
                string re3 = "1";//re3


                //类型 
                p = new Dictionary<string, object>();
                p.Add("ENUM_CODE", boxtype);
                p.Add("ENUM_TYPE", "boxtype");
                p.Add("LANGUAGE", _Language);
                string type_retdata1 = WebAPIHelper.Post(
                                          _APIURL,
                                          "SJ_AQLAPI",//类库名
                                          "SJ_AQLAPI.AQL_CmaTask_TaskList",//类名
                                          "GetEnum",//方法名
                                          _token,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject type_ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(type_retdata1);
                string boxtyperes = string.Empty;
                if (!type_ret.IsSuccess)
                {}
                else
                {
                    DataTable dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(type_ret.RetData);

                    if (dt1.Rows.Count > 0)
                    {
                        boxtyperes = dt1.Rows[0]["ENUM_VALUE"].ToString();
                    }
                }
               
                ReportViewer r = new ReportViewer();
                r.Name = "ReportViewer" + x;
                List<Microsoft.Reporting.WinForms.ReportParameter> PS = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("art", art));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("shoe_name", shoe_name));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("num", num));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("po", po));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("guojia", guojia));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("level", level));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("sample_proportion", sample_proportion));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("VALS", VALS.ToString()));

                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("act", act));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ac1", ac1));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ac2", ac2));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ac3", ac3));

                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("ret", ret0));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("re1", re1));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("re2", re2));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("re3", re3));
                //PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("boxtype", boxtype));

                //点箱记录
                for (int i = 0; i < PointBoxdt.Rows.Count; i++)
                {
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"case_no{i + 1}", ""));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"cr_size{i + 1}", PointBoxdt.Rows[i]["CR_SIZE"].ToString()));
                    PS.Add(new Microsoft.Reporting.WinForms.ReportParameter($@"se_qty{i + 1}", PointBoxdt.Rows[i]["SE_QTY"].ToString()));
                }
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("curr_date", DateTime.Now.ToString("yyyyMMdd")));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("num_total", num_total));
                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("boxtype", boxtyperes));

                PS.Add(new Microsoft.Reporting.WinForms.ReportParameter("xiangshu", ""));

                r.LocalReport.ReportPath = Application.StartupPath + "\\rdlcReport\\AQL\\PointBoxReport.rdlc";
                r.LocalReport.SetParameters(PS);
                r.RefreshReport();

                r.Width = plWidth;
                r.Height = plHeight;
                this.flp_rv_list.Controls.Add(r);
                r.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

                x++;
            }
        }

        public void CalculateEvenNumbers_Print(DataTable pointBoxdt,string sampleSizeStr ,string actualEvenNumberStr)
        {
            decimal sampleSize = 0;//样本量
            bool sampleSize_bool = decimal.TryParse(sampleSizeStr, out sampleSize);
            decimal actualEvenNumber = 0;//实际双数
            bool actualEvenNumber_bool = decimal.TryParse(actualEvenNumberStr, out actualEvenNumber);

            if (sampleSize_bool && actualEvenNumber_bool)
            {
                Dictionary<int, decimal> evenNumbersDic = new Dictionary<int, decimal>();
                if (pointBoxdt != null && pointBoxdt.Rows.Count > 0)
                {
                    int datatable_index = 0;
                    foreach (DataRow item in pointBoxdt.Rows)
                    {
                        //当前行的双数
                        decimal curr_evenNumber = (Convert.ToDecimal(item["SE_QTY"].ToString()) / actualEvenNumber) * sampleSize;
                        evenNumbersDic.Add(datatable_index, curr_evenNumber);
                        datatable_index++;
                    }
                }

                //计算余数差值
                int addOne_count = Convert.ToInt32(sampleSize - evenNumbersDic.Sum(x => Math.Floor(x.Value)));

                evenNumbersDic = evenNumbersDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

                int[] keys = evenNumbersDic.Keys.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    if (i < addOne_count)
                    {
                        evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]) + 1;
                    }
                    else
                    {
                        evenNumbersDic[keys[i]] = Math.Floor(evenNumbersDic[keys[i]]);
                    }
                }

                foreach (var item in evenNumbersDic)
                {
                    pointBoxdt.Rows[item.Key]["SE_QTY"] = item.Value;
                }

            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string filePath = string.Empty;
        private void btn_download_Click(object sender, EventArgs e)
        {
            //创建文件弹出选择窗口（包括文件名）对象
            FolderBrowserDialog dilog = new FolderBrowserDialog();

            dilog.Description = "请选择文件夹";

            if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
            {

                filePath = dilog.SelectedPath;
                string time = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                string subPath = Path.Combine(filePath, "点箱打印" + time);//存放PDF文件 //检查是否存在文件夹
                if (!string.IsNullOrWhiteSpace(subPath))   //创建文件夹
                {
                    Directory.CreateDirectory(subPath);
                }
                //string savePath = $@"C:\Users\admin\Desktop\app2报告";
                int index = 1;
                foreach (var rdlcItem in this.flp_rv_list.Controls)
                {
                    ReportViewer reportViewer = (ReportViewer)rdlcItem;
                    RDLCExport.CreateFile(reportViewer, FileType.PDF, $@"点箱打印_{index}.pdf", subPath);
                    index++;
                }
            }
        }

        private void PrintMultiPdf(string path)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
            string subPath = Path.Combine(path, "点箱打印" + time);//存放PDF文件 //检查是否存在文件夹
            if (!string.IsNullOrWhiteSpace(subPath))   //创建文件夹
            {
                Directory.CreateDirectory(subPath);
            }
            //string savePath = $@"C:\Users\admin\Desktop\app2报告";
            int index = 1;
            foreach (var rdlcItem in this.flp_rv_list.Controls)
            {
                ReportViewer reportViewer = (ReportViewer)rdlcItem;
                RDLCExport.CreateFile(reportViewer, FileType.PDF, $@"点箱打印_{index}.pdf", subPath);
                index++;
            }

            //合并多个文件
            SJeMES_Control_Library.PdfHelper.PdfTool.MergePdf(subPath, $@"点箱打印_{time}");

            string print_path = Path.Combine(subPath, $@"点箱打印_{time}.pdf");
            pdfPrint(print_path);
        }

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
        private void pdfPrint(string filePath)
        {

            SetDefaultPrinter(comboBox1.Text);

            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = comboBox1.Text;
            //pd.PrinterSettings.PrinterName = "ZDesigner ZT411-300dpi ZPL";
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = true;
            startInfo.FileName = filePath;
            startInfo.Verb = "print";
            startInfo.Arguments = @"/p /h \" + filePath + "\"\"" + pd.PrinterSettings.PrinterName + "\"";
            //startInfo.Arguments = $@"/p /h \{filePath}\{pd.PrinterSettings.PrinterName}\";

            p.StartInfo = startInfo;
            p.Start();
            p.WaitForExit();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "请选择打印机");
                return;
            }
            string csPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "dianxiang_print_file");
            PrintMultiPdf(csPath);

            if (!string.IsNullOrEmpty(CurrDefaultPrinter))
                SetDefaultPrinter(CurrDefaultPrinter);
        }

        public string CurrDefaultPrinter = "";
        private void PointBoxPrintMulti_Load(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            CurrDefaultPrinter = printDocument.PrinterSettings.PrinterName;

            if (!string.IsNullOrEmpty(CurrDefaultPrinter))
                comboBox1.SelectedItem = CurrDefaultPrinter;
        }
    }

}
