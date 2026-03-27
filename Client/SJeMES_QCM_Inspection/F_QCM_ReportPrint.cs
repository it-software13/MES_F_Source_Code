
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM_Inspection
{
    public partial class F_QCM_ReportPrint : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _INSPECTION_NO;
        public F_QCM_ReportPrint(string INSPECTION_NO)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
             Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _INSPECTION_NO = INSPECTION_NO;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void Print_Load(object sender, EventArgs e)
        {
            //this.DesktopBounds = Screen.GetWorkingArea(this); // 在桌面区域全屏显示。

            this.panel1.Width = this.Width / 2;

            this.panel1.Controls.Add(this.reportViewer1);
            //表头
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("INSPECTION_NO", _INSPECTION_NO);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.InspectionResult",//类名
                                                       "GetReportHead",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            var datasource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData"].ToString());


            //表身
            Dictionary<string, object> datadetail = new Dictionary<string, object>();
            datadetail.Add("INSPECTION_NO", _INSPECTION_NO);
            string retdatadetail = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.InspectionResult",//类名
                                                       "GetReportBody",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(datadetail));

            var retdetail = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdatadetail);
            var datasourcedetail = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retdetail["RetData"].ToString());

            DirectoryInfo path_exe = new DirectoryInfo(Application.StartupPath); //exe目录
            String path = path_exe.FullName; //上级的目录

            this.reportViewer1.LocalReport.ReportPath = path + @"\RDLC\ReportPrint.rdlc";  //查找要绑定的报表

            this.reportViewer1.Width = this.Width;
            this.reportViewer1.Height = this.Height;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SJeMES_QCM_Inspection.RDLC.ReportPrint.rdlc"; //"SJeMES_QCM_Inspection.RDLC.ReportPrint.rdlc";
            this.reportViewer1.LocalReport.DataSources.Clear();

            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetHead",datasource));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetBody", datasourcedetail));

            //ReportDataSource rdsItem = new ReportDataSource("DataSetBody", datasourcedetail);
            //this.reportViewer1.LocalReport.DataSources.Add(rdsItem);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;

            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();




        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap _NewBitmap = new Bitmap(panel1.Width, panel1.Height);
            //panel1.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
            //e.Graphics.DrawImage(_NewBitmap, 0, 0, _NewBitmap.Width, _NewBitmap.Height);
        }
    }
}
