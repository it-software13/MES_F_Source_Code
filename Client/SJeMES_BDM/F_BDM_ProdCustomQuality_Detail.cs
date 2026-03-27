using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_BDM
{
    public partial class F_BDM_ProdCustomQuality_Detail : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string _prod_no { get; set; }
        public string _develop_season { get; set; }
        public string _series { get; set; }
        public string _shoe_no { get; set; }
        public string _PRODUCT_MONTH { get; set; }
        public string _img_url { get; set; }
        public F_BDM_ProdCustomQuality_Detail(string prod_no, string develop_season, string series, string shoe_no, string PRODUCT_MONTH, string img_url)
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
          Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            _prod_no = prod_no;
            _develop_season = develop_season;
            _series = series;
            _shoe_no = shoe_no;
            _PRODUCT_MONTH = PRODUCT_MONTH;
            _img_url = img_url;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        public string Type { get; set; }
        //记录当前页签
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (tabControl1.SelectedTab.Name)
            {
                case "tabPagebydevelop":
                    Type = "0";
                    break;
                case "tabPagebycwa":
                    Type = "1";
                    break;
                case "tabPagebymaterial":
                    Type = "2";
                    break;
                case "tabPagebytry":
                    Type = "3";
                    break;
                case "tabPagebycut":
                    Type = "4";
                    break;
                case "tabPagebytechnology":
                    Type = "5";
                    break;
                case "tabPagebycar":
                    Type = "6";
                    break;
                case "tabPagebypaste":
                    Type = "7";
                    break;
                case "tabPagebywork":
                    Type = "8";
                    break;
                case "tabPagebyaql":
                    Type = "9";
                    break;
                case "tabPagebysale":
                    Type = "10";
                    break;
                default:
                    break;
            }
        }

        private void F_BDM_ProdCustomQuality_Detail_Load(object sender, EventArgs e)
        {
            #region 头部 

            this.txt_art.Text = _prod_no;
            this.txt_season.Text = _develop_season;
            this.txt_serial.Text = _series;
            this.txt_shoes.Text = _shoe_no;
            this.txt_area.Text = "万邦鞋厂";
            //如果存在就展示，并隐藏上传图片
            try
            {
                this.pictureBox1.Image = Image.FromStream(System.Net.WebRequest.Create(Program.Client.PicUrl + Convert.ToString(_img_url)).GetResponse().GetResponseStream());
            }
            catch
            {
            }
            #endregion
            #region 开发
            var dt = GetData();

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[i];
                dgvr.Cells["日期"].Value = dr["日期"].ToString();
                dgvr.Cells["阶段"].Value = dr["阶段"].ToString();
                dgvr.Cells["报告"].Value = dr["报告/文件"].ToString();
                dgvr.Cells["结果"].Value = dr["结果"].ToString();
                dgvr.Cells["文件路径"].Value = dr["文件路径"].ToString();
                i++;
            }
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation3"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            #endregion

            #region 原材料

            var dt4 = GetData4();

            int x = 0;
            foreach (DataRow drx in dt4.Rows)
            {
                dataGridView4.Rows.Add();
                DataGridViewRow dgvr4 = dataGridView4.Rows[x];
                dgvr4.Cells["日期2"].Value = drx["日期"].ToString();
                dgvr4.Cells["材料名称"].Value = drx["材料名称"].ToString();
                dgvr4.Cells["实验室报告"].Value = drx["实验室报告"].ToString();
                dgvr4.Cells["外观报告"].Value = drx["外观报告"].ToString();
                x++;
            }
            //GenClass.AutoSizeColumn(dataGridView4);
            this.dataGridView4.ClearSelection();
            this.dataGridView4.Columns["原材料检验结果"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            this.dataGridView4.Columns["实验室检验结果"].DefaultCellStyle.SelectionBackColor = Color.Transparent;

            #endregion

            #region 量试
            var dt5 = GetData5();

            int r = 0;
            foreach (DataRow dr in dt5.Rows)
            {
                dataGridView5.Rows.Add();
                DataGridViewRow dgvr5 = dataGridView5.Rows[r];
                dgvr5.Cells["日期3"].Value = dr["日期"].ToString();
                dgvr5.Cells["结果3"].Value = dr["结果"].ToString();
                r++;
            }
            this.dataGridView5.ClearSelection();
            this.dataGridView5.Columns["operation5"].DefaultCellStyle.SelectionBackColor = Color.Transparent;

            #endregion

            #region 加工

            var dt6 = GetData6();

            int c = 0;
            foreach (DataRow dr in dt6.Rows)
            {
                dataGridView6.Rows.Add();
                DataGridViewRow dgvr = dataGridView6.Rows[c];
                dgvr.Cells["日期4"].Value = dr["日期"].ToString();
                dgvr.Cells["产线"].Value = dr["产线"].ToString();
                dgvr.Cells["RFT"].Value = dr["RFT"].ToString();
                c++;
            }
            this.dataGridView6.ClearSelection();
            this.dataGridView6.Columns["operation6"].DefaultCellStyle.SelectionBackColor = Color.Transparent;


            #endregion 

            #region AQL

            var dt2 = GetData2();

            int z = 0;
            foreach (DataRow dr in dt2.Rows)
            {
                dataGridView2.Rows.Add();
                DataGridViewRow dgvr2 = dataGridView2.Rows[z];
                dgvr2.Cells["txt_date"].Value = dr["日期"].ToString();
                dgvr2.Cells["txt_PO"].Value = dr["PO号"].ToString();
                dgvr2.Cells["txt_type"].Value = dr["验货类型"].ToString();
                dgvr2.Cells["txt_result"].Value = dr["结果"].ToString();
                z++;
            }
            this.dataGridView2.ClearSelection();
            this.dataGridView2.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            #endregion

            #region 售后
            var dt3 = GetData3();

            int j = 0;
            foreach (DataRow dr in dt3.Rows)
            {
                dataGridView3.Rows.Add();
                DataGridViewRow dgvr3 = dataGridView3.Rows[j];
                dgvr3.Cells["txt_datesale"].Value = dr["日期"].ToString();
                dgvr3.Cells["txt_customer"].Value = dr["退货客户"].ToString();
                dgvr3.Cells["qty"].Value = dr["退货数量"].ToString();
                dgvr3.Cells["code"].Value = dr["问题代号(code)"].ToString();
                dgvr3.Cells["txt_problem_name"].Value = dr["问题名称"].ToString();
                j++;
            }
            this.dataGridView3.ClearSelection();
            this.dataGridView3.Columns["operation2"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            #endregion

            GenClass.AutoSizeColumn(dataGridView1);
            GenClass.AutoSizeColumn(dataGridView2);
            GenClass.AutoSizeColumn(dataGridView3);
            GenClass.AutoSizeColumn(dataGridView4);
            GenClass.AutoSizeColumn(dataGridView5);
            GenClass.AutoSizeColumn(dataGridView6);

        }
        public DataTable GetData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("日期", typeof(string));
            dt.Columns.Add("阶段", typeof(string));
            dt.Columns.Add("报告/文件", typeof(string));
            dt.Columns.Add("结果", typeof(string));
            dt.Columns.Add("文件路径", typeof(string));

            Random ran = new Random();
            for (int i = 1; i < 11; i++)
            {
                DataRow dr = dt.NewRow();
                dr["日期"] = i < 6 ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                dr["阶段"] = "CR" + (i + 1);
                dr["报告/文件"] = i < 6 ? "CR" + i : "CS2";
                dr["结果"] = "PASS";
                dr["文件路径"] = "/File/FGT.pdf";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable GetData2()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("日期", typeof(string));
            dt.Columns.Add("PO号", typeof(string));
            dt.Columns.Add("验货类型", typeof(string));
            dt.Columns.Add("结果", typeof(string));

            Random ran = new Random();
            for (int i = 1; i < 11; i++)
            {
                DataRow dr = dt.NewRow();
                dr["日期"] = i < 4 ? DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") : DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
                dr["PO号"] = "JHK" + DateTime.Now.ToString("yyyyMMdd") + (1000 + i);
                dr["验货类型"] = "AQL验货";
                dr["结果"] = "PASS";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable GetData3()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("日期", typeof(string));
            dt.Columns.Add("退货客户", typeof(string));
            dt.Columns.Add("退货数量", typeof(string));
            dt.Columns.Add("问题代号(code)", typeof(string));
            dt.Columns.Add("问题名称", typeof(string));

            Random ran = new Random();
            for (int i = 1; i < 11; i++)
            {
                DataRow dr = dt.NewRow();
                dr["日期"] = i < 6 ? DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"): DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                dr["退货客户"] = "中国";
                dr["退货数量"] = new Random(Guid.NewGuid().GetHashCode()).Next(10, 99);
                dr["问题代号(code)"] = i > 4 ? "88a" : "88b";
                dr["问题名称"] = i > 4 ? "大底与斜面开放" : "斜面与中底开胶";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable GetData4()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("日期", typeof(string));
            dt.Columns.Add("材料名称", typeof(string));
            dt.Columns.Add("实验室报告", typeof(string));
            dt.Columns.Add("外观报告", typeof(string));

            Random ran = new Random();
            for (int i = 1; i < 11; i++)
            {
                DataRow dr = dt.NewRow();
                dr["日期"] = i < 7 ? DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") : DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                dr["材料名称"] = "网布";
                dr["实验室报告"] = "PASS";
                dr["外观报告"] = "PASS";
                dt.Rows.Add(dr);
            }
            return dt;
        }
        //量试
        public DataTable GetData5()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("日期", typeof(string));
            dt.Columns.Add("结果", typeof(string));

            Random ran = new Random();
            for (int i = 1; i < 11; i++)
            {
                DataRow dr = dt.NewRow();
                dr["日期"] = i < 7 ? DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd") : DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                dr["结果"] = "PASS";

                dt.Rows.Add(dr);
            }
            return dt;
        }
        //加工
        public DataTable GetData6()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("日期", typeof(string));
            dt.Columns.Add("产线", typeof(string));
            dt.Columns.Add("RFT", typeof(string));

            Random ran = new Random();
            for (int i = 1; i < 11; i++)
            {
                DataRow dr = dt.NewRow();
                dr["日期"] = i < 6 ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                dr["产线"] = i < 6 ? "1PL1" : "1PL2";
                dr["RFT"] = i < 6 ? "70%" : "85%";

                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                //string INSPECTION_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["INSPECTION_NO"].Value);
                string name = this.dataGridView2.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView2.Rows[this.dataGridView2.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                        return;
                    if (cell.CurrentItem.Equals("filebtn"))//AQL
                    {
                        //DataTable dt = new DataTable();
                        //dt.Columns.Add("id", typeof(string));
                        //dt.Columns.Add("file_name", typeof(string));
                        //dt.Columns.Add("file_url", typeof(string));
                        //dt.Columns.Add("net_file_url", typeof(string));
                        //dt.Columns.Add("tablename", typeof(string));


                        //DataRow dr = dt.NewRow();
                        //dr["id"] = "1";
                        //dr["file_name"] = "中英文版IR新模板（空白）";
                        //dr["file_url"] = "/File/中英文版IR新模板（空白）.xlsx";
                        //dr["net_file_url"] = "";
                        //dr["tablename"] = "XXX";
                        //dt.Rows.Add(dr);
                        //if (dt.Rows.Count > 0)
                        //{
                        //    //dt.Columns.Add("net_file_url", typeof(string));
                        //    foreach (DataRow dr3 in dt.Rows)
                        //    {
                        //        dr3["net_file_url"] = Program.Client.PicUrl + dr3["file_url"];
                        //    }
                        //}
                        string url = Convert.ToString(dataGridView1.CurrentRow.Cells["文件路径"].Value);

                        //FrmShowFile add2 = new FrmShowFile(@"http://192.168.1.123:8066/" + url);
                        FrmShowFile add2 = new FrmShowFile(Program.Client.PicUrl + "/" + url);
                        add2.ShowDialog();
                        //FrmFileList add = new FrmFileList(dt, Program.Client.APIURL, Program.Client.UserToken);
                        //add.ShowDialog();
                        //F_QCM_ReportPrint reportPrint = new F_QCM_ReportPrint(INSPECTION_NO);
                        //reportPrint.ShowDialog();
                    } //AQL
                    

                }
            }
        }
        //开发
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string INSPECTION_NO = Convert.ToString(dataGridView1.CurrentRow.Cells["INSPECTION_NO"].Value);
            string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
            if (name == "operation3")
            {
                DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation3"] as DataGridViewOperationCell;
                if (cell.CurrentItem == null)
                    return;

                if (cell.CurrentItem.Equals("selectfile"))//开发
                {

                    string url = Convert.ToString(dataGridView1.CurrentRow.Cells["文件路径"].Value);

                    //FrmShowFile add2 = new FrmShowFile(@"http://192.168.1.123:8066/" + url);
                    FrmShowFile add2 = new FrmShowFile(Program.Client.PicUrl + "/" + url);
                    add2.ShowDialog();
                }
            }
        }

        //售后
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = this.dataGridView3.Columns[e.ColumnIndex].Name;
            if (name == "operation2")
            {
                DataGridViewOperationCell cell = this.dataGridView3.Rows[this.dataGridView3.CurrentRow.Index].Cells["operation2"] as DataGridViewOperationCell;
                if (cell.CurrentItem == null)
                    return;

                if (cell.CurrentItem.Equals("imgbtn2"))//售后
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id", typeof(string));
                    dt.Columns.Add("file_name", typeof(string));
                    dt.Columns.Add("file_url", typeof(string)); 
                    dt.Columns.Add("net_file_url", typeof(string));
                    dt.Columns.Add("tablename", typeof(string));
                    
                    #region 添加图片
                    DataRow dr = dt.NewRow();
                    dr["id"] = "1";
                    dr["file_name"] = "ULTRABOOST 22 J 新款运动鞋(GW6364).jpg";
                    dr["file_url"] = "/File/ULTRABOOST 22 J 新款运动鞋(GW6364).jpg";
                    dr["net_file_url"] = "";
                    dr["tablename"] = "XXX";
                    dt.Rows.Add(dr);

                    DataRow dr2 = dt.NewRow();
                    dr2["id"] = "2";
                    dr2["file_name"] = "POSTMOVE MID K 新款篮球鞋(GW6359).jpg";
                    dr2["file_url"] = "/File/POSTMOVE MID K 新款篮球鞋(GW6359).jpg";
                    dr2["net_file_url"] = "";
                    dr2["tablename"] = "CCC";
                    dt.Rows.Add(dr2);

                    DataRow dr3 = dt.NewRow();
                    dr3["id"] = "3";
                    dr3["file_name"] = "POSTMOVE MID K 新款篮球鞋1(GW6351).jpg";
                    dr3["file_url"] = "/File/POSTMOVE MID K 新款篮球鞋1(GW6351).jpg";
                    dr3["net_file_url"] = "";
                    dr3["tablename"] = "VVV";
                    dt.Rows.Add(dr3);

                    DataRow dr4 = dt.NewRow();
                    dr4["id"] = "4";
                    dr4["file_name"] = "FORUM LOW 新款休闲篮球鞋(GW6353).jpg";
                    dr4["file_url"] = "/File/FORUM LOW 新款休闲篮球鞋(GW6353).jpg";
                    dr4["net_file_url"] = "";
                    dr4["tablename"] = "XXX";
                    dt.Rows.Add(dr4);

                    DataRow dr5 = dt.NewRow();
                    dr5["id"] = "5";
                    dr5["file_name"] = "D.O.N. ISSUE 3 GCA 新款篮球运动鞋(GW6350).jpg";
                    dr5["file_url"] = "/File/D.O.N. ISSUE 3 GCA 新款篮球运动鞋(GW6350).jpg";
                    dr5["net_file_url"] = "";
                    dr5["tablename"] = "XXX";
                    dt.Rows.Add(dr5);
                    #endregion

                    if (dt.Rows.Count > 0)
                    {
                        //dt.Columns.Add("net_file_url", typeof(string));
                        foreach (DataRow cc in dt.Rows)
                        {
                            cc["net_file_url"] = Program.Client.PicUrl + cc["file_url"];
                        }
                    }
                    FrmFileList add = new FrmFileList(dt, Program.Client.UploadUrl, Program.Client.UserToken);
                    add.ShowDialog();

                }
            }
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = this.dataGridView6.Columns[e.ColumnIndex].Name;
            if (name == "operation6")
            {
                DataGridViewOperationCell cell = this.dataGridView6.Rows[this.dataGridView6.CurrentRow.Index].Cells["operation6"] as DataGridViewOperationCell;
                if (cell.CurrentItem == null)
                    return;
                if (cell.CurrentItem.Equals("select"))//售后
                {///File/TQC任务进度.png
                    FrmShowImg ff = new FrmShowImg(Program.Client.PicUrl + "/File/TQC任务进度.png", "TQC任务进度");
                    ff.ShowDialog();
                }
            }
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = this.dataGridView4.Columns[e.ColumnIndex].Name;
            if (name == "原材料检验结果")
            {
                DataGridViewOperationCell cell = this.dataGridView4.Rows[this.dataGridView4.CurrentRow.Index].Cells["原材料检验结果"] as DataGridViewOperationCell;
                if (cell.CurrentItem == null)
                    return;
                if (cell.CurrentItem.Equals("img"))//售后
                {///File/TQC任务进度.png
                    FrmShowImg ff = new FrmShowImg(Program.Client.PicUrl + "/File/原材料检验结果.png", "原材料检验结果");
                    ff.ShowDialog();
                }
            }
            if (name == "实验室检验结果")
            {
                DataGridViewOperationCell cell = this.dataGridView4.Rows[this.dataGridView4.CurrentRow.Index].Cells["实验室检验结果"] as DataGridViewOperationCell;
                if (cell.CurrentItem == null)
                    return;
                if (cell.CurrentItem.Equals("img2"))//售后
                {///File/TQC任务进度.png
                    //FrmShowFile ff = new FrmShowFile(Program.Client.PicUrl + "/File/实验室检验报告.png", "实验室检验报告");
                    FrmShowImg ff = new FrmShowImg(Program.Client.PicUrl + "/File/实验室检验报告.png", "实验室检验报告");
                    ff.ShowDialog();
                }
            }
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex<0 || e.ColumnIndex<0)
            {
                return;
            }
            string name = this.dataGridView5.Columns[e.ColumnIndex].Name;
            if (name == "operation5")
            {
                DataGridViewOperationCell cell = this.dataGridView5.Rows[this.dataGridView5.CurrentRow.Index].Cells["operation5"] as DataGridViewOperationCell;
                if (cell.CurrentItem == null)
                    return;
                if (cell.CurrentItem.Equals("select"))
                {
                    FrmShowImg ff = new FrmShowImg(Program.Client.PicUrl + "/File/量产试做详情.png", "量产试做详情");
                    ff.ShowDialog();
                }
            }
            
        }
    }
}
        
