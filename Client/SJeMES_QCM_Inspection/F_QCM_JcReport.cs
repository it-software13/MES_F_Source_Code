using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM_Inspection
{
    public partial class F_QCM_JcReport : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        /// <summary>
        /// 检验单
        /// </summary>
        public string INSPECTION_NO { get; set; }
        public F_QCM_JcReport(string _INSPECTION_NO)
        {
            InitializeComponent();
            INSPECTION_NO = _INSPECTION_NO;
            this.printDocument1.OriginAtMargins = true;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);

        }

        private void Report_Load(object sender, EventArgs e)
        {
            this.DesktopBounds = Screen.GetWorkingArea(this); // 在桌面区域全屏显示。
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("INSPECTION_NO", INSPECTION_NO);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                       Program.Client.APIURL,
                                                       "SJ_QCMAPI",//类库名
                                                       "SJ_QCMAPI.InspectionResult",//类名
                                                       "GetReportHead",//方法名
                                                       Program.Client.UserToken,//token
                                                       Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            var datasource = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret["RetData"].ToString());
            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                //表头
                UcReport ucReport = new UcReport();

                foreach (DataRow item in datasource.Rows)
                {
                    ucReport.txt_sjType.Text = "实验室送检";// item["GENERAL_TESTTYPE_NAME"].ToString();
                    ucReport.txt_Submitdate.Text = item["INSPECTION_DATE"].ToString();
                    ucReport.txt_art.Text = item["ART_CODE"].ToString();
                    ucReport.txt_kind.Text = item["CATEGORY_NAME"].ToString();
                    ucReport.txt_finishdate.Text = item["INSPECTION_ENDDATE"].ToString();
                    ucReport.txt_jd.Text = item["DEPARTMENT_NO"].ToString();
                    ucReport.txt_area.Text = item["PLANTAREA_NAME"].ToString();
                    
                }
                ucReport.Dock = DockStyle.Fill;//填充panel
                this.splitContainer2.Panel2.Controls.Add(ucReport);

                //表身
                Dictionary<string, object> datadetail = new Dictionary<string, object>();
                datadetail.Add("INSPECTION_NO", INSPECTION_NO);
                string retdatadetail = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                           Program.Client.APIURL,
                                                           "SJ_QCMAPI",//类库名
                                                           "SJ_QCMAPI.InspectionResult",//类名
                                                           "GetReportBody",//方法名
                                                           Program.Client.UserToken,//token
                                                           Newtonsoft.Json.JsonConvert.SerializeObject(datadetail));

                var retdetail = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdatadetail);
                var datasourcedetail = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retdetail["RetData"].ToString());

                for (int i = 0; i < 3; i++)
                { 
                    foreach (DataRow item in datasourcedetail.Rows)
                    {
                        UcReportDetail ucReportDetail = new UcReportDetail();
                        ucReportDetail.txt_no.Text = item["TESTITEM_CODE"].ToString();
                        ucReportDetail.txt_name.Text = item["TESTITEM_NAME"].ToString();
                        ucReportDetail.txt_rank.Text = item["REFERENCE_LEVEL"].ToString();

                        if (string.IsNullOrEmpty(item["check_result"].ToString()))
                        {
                            ucReportDetail.txt_Result.Text = "FAIL";
                            ucReportDetail.txt_Result.ForeColor = Color.Red;
                        }
                        else
                        {
                            ucReportDetail.txt_Result.Text = item["check_result"].ToString(); // 大pass/fail
                            ucReportDetail.txt_Result.ForeColor = Color.Green;
                        }
                        // ucReportDetail.txt_Result.Text = item["check_result"].ToString();//小pass/fail

                        ucReportDetail.txt_pd.Text = item["T_CHECK_ITEM"].ToString();//判断(通用)
                        ucReportDetail.txt_cl_ty.Text = item["T_CHECK_VALUE"].ToString();//测量标准(通用)
                        ucReportDetail.txt_pd_dz.Text = item["D_CHECK_ITEM"].ToString();//测量标准(定制)
                        ucReportDetail.txt_cl_dz.Text = item["D_CHECK_VALUE"].ToString();//测量标准(定制)

                        ucReportDetail.txt_unit.Text = item["UNIT"].ToString();//单位
                        ucReportDetail.txt_num.Text = item["SAMPLE_NUM"].ToString();//试样数量
                        ucReportDetail.txt_cl_data.Text = item["RESULT_VALUE"].ToString();//结果值

                        ucReportDetail.txt_Remark.Text = item["TEST_REMARKS"].ToString();//测试备注
                        ucReportDetail.txt_cl_data.Text = item["FORMULA_CONTENT"].ToString();//公式展示

                        if (string.IsNullOrEmpty(item["check_result"].ToString()))
                        {
                            ucReportDetail.txt_is_pass.Text = "FAIL";
                            ucReportDetail.txt_is_pass.ForeColor = Color.Red;
                        }
                        else
                        {
                            ucReportDetail.txt_is_pass.Text = item["check_result"].ToString(); // 大pass/fail
                            ucReportDetail.txt_is_pass.ForeColor = Color.Green;
                        }
                        // ucReportDetail.Dock = DockStyle.Fill;//填充panel
                        ucReportDetail.Width = this.Width;
                        this.flowLayoutPanel1.Controls.Add(ucReportDetail);
                    }
                }

            }
        }

        private void Returnbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //打印报告
        private void Print_Report(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
            //ReportPrint reportPrint = new ReportPrint(INSPECTION_NO);
            //reportPrint.Show();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //打印内容 为 整个Form
            //Image myFormImage;
            //myFormImage = new Bitmap(splitContainer1.Width, splitContainer1.Height);
            //Graphics g = Graphics.FromImage(myFormImage);
            //g.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            //e.Graphics.DrawImage(myFormImage, 0, 0);

            ////打印内容 为 局部的 this.groupBox1
            //int width = this.splitContainer1.Width;
            //int height = this.splitContainer1.Height;
            //Bitmap _NewBitmap = new Bitmap(width, height);
            //this.splitContainer1.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
            //e.Graphics.DrawImage(_NewBitmap, 0, 0, 791, 133);


            //打印内容 为 局部的 this.splitContainer1 
            Bitmap _NewBitmap = new Bitmap(this.splitContainer1.Width, this.splitContainer1.Height);
            this.splitContainer1.DrawToBitmap(_NewBitmap, new Rectangle(0, 0, _NewBitmap.Width, _NewBitmap.Height));
            _NewBitmap = KiResizeImage(_NewBitmap, 700, 900);
            e.Graphics.DrawImage(_NewBitmap, 0, 0, _NewBitmap.Width, _NewBitmap.Height);
        }


        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <returns>处理以后的Bitmap</returns>
        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
