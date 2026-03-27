using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_TQC_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_TQC_Main()
        {
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
        Program.SkinThemes, materialSkinManager, this);
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_TQC_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //GetArtImageUrl();
            txt_PO.Text = "PO20211114001";
            #region TestData
            Dgv_TestData.Rows.Add();
            DataGridViewRow TestData = Dgv_TestData.Rows[0];
            TestData.Cells[0].Value = "RET(首次合格率)";
            TestData.Cells[1].Value = "90.00%";
            TestData.Cells[2].Value = "首检合格数";
            TestData.Cells[3].Value = "900";


            Dgv_TestData.Rows.Add();
            DataGridViewRow TestData1 = Dgv_TestData.Rows[1];
            TestData1.Cells[0].Value = "产线合格率";
            TestData1.Cells[1].Value = "94.00%";
            TestData1.Cells[2].Value = "检验数量";
            TestData1.Cells[3].Value = "1000";

            Dgv_TestData.Rows.Add();
            DataGridViewRow TestData2 = Dgv_TestData.Rows[2];
            TestData2.Cells[2].Value = "B品数量";
            TestData2.Cells[3].Value = "60";

            Dgv_TestData.Rows.Add();
            DataGridViewRow TestData3 = Dgv_TestData.Rows[3];
            TestData3.Cells[0].Value = "返修合格率";
            TestData3.Cells[1].Value = "96%";
            TestData3.Cells[2].Value = "返修数量";
            TestData3.Cells[3].Value = "239";

            #endregion

            #region BadRecord
            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord = Dgv_BadRecord.Rows[0];
            BadRecord.Cells[0].Value = "1";
            BadRecord.Cells[1].Value = "脱胶";
            BadRecord.Cells[2].Value = "1";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord1 = Dgv_BadRecord.Rows[1];
            BadRecord1.Cells[0].Value = "2";
            BadRecord1.Cells[1].Value = "漏缝线";
            BadRecord1.Cells[2].Value = "2";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord2 = Dgv_BadRecord.Rows[2];
            BadRecord2.Cells[0].Value = "3";
            BadRecord2.Cells[1].Value = "走线不齐";
            BadRecord2.Cells[2].Value = "3";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord3 = Dgv_BadRecord.Rows[3];
            BadRecord3.Cells[0].Value = "4";
            BadRecord3.Cells[1].Value = "塑料凸起";
            BadRecord3.Cells[2].Value = "4";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord4 = Dgv_BadRecord.Rows[4];
            BadRecord4.Cells[0].Value = "5";
            BadRecord4.Cells[1].Value = "褶皱";
            BadRecord4.Cells[2].Value = "5";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord5 = Dgv_BadRecord.Rows[5];
            BadRecord5.Cells[0].Value = "6";
            BadRecord5.Cells[1].Value = "脱胶";
            BadRecord5.Cells[2].Value = "6";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord6 = Dgv_BadRecord.Rows[6];
            BadRecord6.Cells[0].Value = "7";
            BadRecord6.Cells[1].Value = "漏缝线";
            BadRecord6.Cells[2].Value = "7";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord7 = Dgv_BadRecord.Rows[7];
            BadRecord7.Cells[0].Value = "8";
            BadRecord7.Cells[1].Value = "走线不齐";
            BadRecord7.Cells[2].Value = "8";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord8 = Dgv_BadRecord.Rows[8];
            BadRecord8.Cells[0].Value = "9";
            BadRecord8.Cells[1].Value = "塑料凸起";
            BadRecord8.Cells[2].Value = "9";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord9 = Dgv_BadRecord.Rows[9];
            BadRecord9.Cells[0].Value = "10";
            BadRecord9.Cells[1].Value = "褶皱";
            BadRecord9.Cells[2].Value = "+";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord10 = Dgv_BadRecord.Rows[10];
            BadRecord10.Cells[0].Value = "11";
            BadRecord10.Cells[1].Value = "脱胶";
            BadRecord10.Cells[2].Value = "/";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord11 = Dgv_BadRecord.Rows[11];
            BadRecord11.Cells[0].Value = "12";
            BadRecord11.Cells[1].Value = "漏缝线";
            BadRecord11.Cells[2].Value = "*";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord12 = Dgv_BadRecord.Rows[12];
            BadRecord12.Cells[0].Value = "13";
            BadRecord12.Cells[1].Value = "走线不齐";
            BadRecord12.Cells[2].Value = "-";

            Dgv_BadRecord.Rows.Add();
            DataGridViewRow BadRecord13 = Dgv_BadRecord.Rows[13];
            BadRecord13.Cells[0].Value = "14";
            BadRecord13.Cells[1].Value = "塑料凸起";
            BadRecord13.Cells[2].Value = ".";
            #endregion

            #region Button
            Dgv_Button.Rows.Add();
            DataGridViewRow button = Dgv_Button.Rows[0];
            button.Cells[0].Value = "1";
            button.Cells[1].Value = "无瑕疵";
            button.Cells[2].Value = "0";

            Dgv_Button.Rows.Add();
            DataGridViewRow button1 = Dgv_Button.Rows[1];
            button1.Cells[0].Value = "2";
            button1.Cells[1].Value = "提交（下一双）";
            button1.Cells[2].Value = "Enter";

            Dgv_Button.Rows.Add();
            DataGridViewRow button2 = Dgv_Button.Rows[2];
            button2.Cells[0].Value = "3";
            button2.Cells[1].Value = "撤回一次";
            button2.Cells[2].Value = "C";

            Dgv_Button.Rows.Add();
            DataGridViewRow button3 = Dgv_Button.Rows[3];
            button3.Cells[0].Value = "4";
            button3.Cells[1].Value = "不良通过（B品）";
            button3.Cells[2].Value = "B";
            #endregion

            Dgv_BadRecord.ClearSelection();

            Dgv_Button.ClearSelection();

            Dgv_TestData.ClearSelection();

            decimal firstInspe = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString());//首次合格数
            decimal amountInspe = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString());//检验数
            decimal FirstPassRate = firstInspe / amountInspe * 100;
            Dgv_TestData.Rows[0].Cells[1].Value = FirstPassRate + "%";

            decimal Bqty = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString());//B品数量
            decimal ProlinePass = (amountInspe - Bqty) / amountInspe * 100;//产线合格率
            Dgv_TestData.Rows[1].Cells[1].Value = ProlinePass + "%";

        }
        //获取ART图片地址
        private void GetArtImageUrl()
        {
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("PROD_NO", txt_ART.Text.Trim().ToString());

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.TQCBase",//类名
                                            "GetArtImageUrl",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                if (dt.Rows.Count > 0)
                {
                    var webC = new System.Net.WebClient();
                    try
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            txt_PO.Text = dr["MER_PO"].ToString();
                            string art_imgurl = "http://192.168.1.123:8066" + dr["IMG_URL"].ToString();
                            //http://192.168.1.123:8066/pictrue/ArtImage/20211116113503880.jpg
                            //@"http://192.168.1.123:8088/pictrue/ArtImage/20211028162844195.jpg";
                            //
                            Image image1 = new Bitmap(webC.OpenRead(art_imgurl));
                            PictureBox pb = new PictureBox();
                            pb.Image = image1;
                            //pb.Size = new Size(flowLayoutPanel1.Height + 20, flowLayoutPanel1.Height);
                            //pb.SizeMode = PictureBoxSizeMode.Zoom;
                            //this.flowLayoutPanel1.Controls.Add(pb);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //提交+1
        private void btn_qty_Click(object sender, EventArgs e)
        {
            Dgv_BadRecord.ClearSelection();
            int p = 14;
            for (int i = 0; i < p; i++)
            {
                Dgv_BadRecord.Rows[i].Cells[1].Style.BackColor = Color.White;
            }

            Dgv_TestData.Rows[1].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString()) + 1;//检验数量+1
            decimal First_pass = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString());
            decimal amount = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString());
            decimal rejects = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString());
            Dgv_TestData.Rows[0].Cells[1].Value = Math.Round(First_pass / amount * 100, 2) + "%";
            Dgv_TestData.Rows[1].Cells[1].Value = Math.Round((amount - rejects) / amount * 100, 2) + "%";

            type = "0";
        }
        //B品提交+1
        private void btn_Bqty_Click(object sender, EventArgs e)
        {
            Dgv_TestData.Rows[1].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString()) + 1;//检验数量+1
            Dgv_TestData.Rows[2].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString()) + 1;//B品数量+1
            decimal First_pass = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString());
            decimal amount = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString());
            decimal rejects = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString());
            Dgv_TestData.Rows[0].Cells[1].Value = Math.Round(First_pass / amount * 100, 2) + "%";
            Dgv_TestData.Rows[1].Cells[1].Value = Math.Round((amount - rejects) / amount * 100, 2) + "%";

            type = "2";
        }

        string type = "";//0:仅提交  1:无瑕疵提交   2:不良品提交
        private void F_QCM_TQC_Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad1:
                    Dgv_BadRecord.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad2:
                    Dgv_BadRecord.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad3:
                    Dgv_BadRecord.Rows[2].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad4:
                    Dgv_BadRecord.Rows[3].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad5:
                    Dgv_BadRecord.Rows[4].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad6:
                    Dgv_BadRecord.Rows[5].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad7:
                    Dgv_BadRecord.Rows[6].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad8:
                    Dgv_BadRecord.Rows[7].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad9:
                    Dgv_BadRecord.Rows[8].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.Add:
                    Dgv_BadRecord.Rows[9].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.Divide:
                    Dgv_BadRecord.Rows[10].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.Multiply:
                    Dgv_BadRecord.Rows[11].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.Subtract:
                    Dgv_BadRecord.Rows[12].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.Decimal:
                    Dgv_BadRecord.Rows[13].Cells[1].Style.BackColor = Color.LightGray;
                    break;
                case Keys.NumPad0:
                    Dgv_BadRecord.ClearSelection();
                    int p = 14;
                    for (int i = 0; i < p; i++)
                    {
                        Dgv_BadRecord.Rows[i].Cells[1].Style.BackColor = Color.White;
                    }
                    Dgv_TestData.Rows[0].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString()) + 1;//首次合格数+1
                    Dgv_TestData.Rows[1].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString()) + 1;//检验数量+1
                    decimal First_pass = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString());
                    decimal amount = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString());
                    Dgv_TestData.Rows[0].Cells[1].Value = Math.Round((First_pass / amount * 100), 2) + "%";

                    decimal rejects = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString());
                    Dgv_TestData.Rows[1].Cells[1].Value = Math.Round(((amount - rejects) / amount * 100), 2) + "%";

                    type = "1";
                    break;
                case Keys.B:
                    btn_Bqty_Click(sender, e);
                    break;
                case Keys.Enter:
                    btn_qty_Click(sender, e);
                    break;
                case Keys.C:
                    btn_recall_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void btn_recall_Click(object sender, EventArgs e)
        {
            try
            {
                if (type == "1")
                {
                    Dgv_TestData.Rows[0].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString()) - 1;//首次合格数+1
                    Dgv_TestData.Rows[1].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString()) - 1;//检验数量+1
                    decimal First_pass1 = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString());
                    decimal amount1 = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString());
                    Dgv_TestData.Rows[0].Cells[1].Value = Math.Round((First_pass1 / amount1 * 100), 2) + "%";
                    decimal rejects1 = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString());
                    Dgv_TestData.Rows[1].Cells[1].Value = Math.Round(((amount1 - rejects1) / amount1 * 100), 2) + "%";

                    type = "";
                }
                else
                if (type == "0")
                {
                    Dgv_TestData.Rows[1].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString()) - 1;//检验数量+1
                    decimal First_pass = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString());
                    decimal amount = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString());
                    decimal rejects = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString());
                    Dgv_TestData.Rows[0].Cells[1].Value = Math.Round(First_pass / amount * 100, 2) + "%";
                    Dgv_TestData.Rows[1].Cells[1].Value = Math.Round((amount - rejects) / amount * 100, 2) + "%";

                    type = "";
                }
                else
                if (type == "2")
                {
                    Dgv_TestData.Rows[1].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString()) - 1;//检验数量+1
                    Dgv_TestData.Rows[2].Cells[3].Value = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString()) - 1;//B品数量+1
                    decimal First_pass = Convert.ToDecimal(Dgv_TestData.Rows[0].Cells[3].Value.ToString());
                    decimal amount = Convert.ToDecimal(Dgv_TestData.Rows[1].Cells[3].Value.ToString());
                    decimal rejects = Convert.ToDecimal(Dgv_TestData.Rows[2].Cells[3].Value.ToString());
                    Dgv_TestData.Rows[0].Cells[1].Value = Math.Round(First_pass / amount * 100, 2) + "%";
                    Dgv_TestData.Rows[1].Cells[1].Value = Math.Round((First_pass - rejects) / First_pass * 100, 2) + "%";

                    type = "";
                }
            }
            catch
            {
                MessageBox.Show("仅撤回一次");
            }
        }

        private void btn_selectBqty_Click(object sender, EventArgs e)
        {

        }




        //相关实验结果
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("file_name");
            dt.Columns.Add("file_url");
            dt.Columns.Add("net_file_url");//完整链接
            dt.Columns.Add("id");
            dt.Columns.Add("tablename");

            string url1 = Program.Client.PicUrl + @"/file/FGT.pdf";
            string url2 = Program.Client.PicUrl + @"/file/拉力.pdf";
            DataRow dr1 = dt.NewRow();
            dr1["file_name"] = "FGT";
            dr1["file_url"] = url1;
            dr1["net_file_url"] = url1;
            dr1["id"] = "0";
            dr1["tablename"] = "";

            DataRow dr2 = dt.NewRow();
            dr2["file_name"] = "拉力";
            dr2["file_url"] = url2;
            dr2["net_file_url"] = url2;
            dr2["id"] = "0";
            dr2["tablename"] = "";

            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);

            FrmFileList frmFileList = new FrmFileList(dt,Program.Client.UploadUrl, Program.Client.UserToken,"");
            frmFileList.ShowDialog();

        }
        //产品标准
        private void button4_Click(object sender, EventArgs e)
        {
            #region 
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
            //    dt.Columns.Add("net_file_url", typeof(string));
            //    foreach (DataRow dr3 in dt.Rows)
            //    {
            //        dr3["net_file_url"] = Program.Client.PicUrl + dr3["file_url"];
            //    }
            //}
            //FrmFileList add = new FrmFileList(dt, Program.Client.APIURL, Program.Client.UserToken);
            //add.ShowDialog();
            //F_QCM_ReportPrint reportPrint = new F_QCM_ReportPrint(INSPECTION_NO);
            //reportPrint.ShowDialog(); 
            #endregion

            string url = Program.Client.PicUrl + @"/file/PB.pdf";
            
            FrmShowFile frmShowFile = new FrmShowFile(url,"");
            frmShowFile.ShowDialog();

        }  
        //安全合规文件
        private void button5_Click(object sender, EventArgs e)
        {
            string url = Program.Client.PicUrl + @"/file/Adidas International Marketing B V FAC-021936 Apache FootwearLimited.pdf";
            FrmShowFile frmShowFile = new FrmShowFile(url, "");
            frmShowFile.ShowDialog();
        }

        private void Dgv_Button_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void Dgv_BadRecord_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);


            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            FrmShowImg ff = new FrmShowImg(Program.Client.PicUrl + "/File/TQC任务进度.png", "TQC任务进度");
            ff.ShowDialog();
        }
    }
}
