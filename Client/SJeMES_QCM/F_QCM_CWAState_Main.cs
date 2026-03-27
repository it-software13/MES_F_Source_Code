using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_CWAState_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_CWAState_Main()
        {
            InitializeComponent();
            BindingData();
            pageControl1.BindPageEvent += BindingData2;
            FormLoad();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public void FormLoad()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        public void BindingData2(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 1;
        }

        public static DataTable InitializeData()
        {
            #region 初始化数据
            DataTable dt = new DataTable();
            dt.Columns.Add("Shoes_figure");
            dt.Columns.Add("art");
            dt.Columns.Add("shoes");
            dt.Columns.Add("quarter");
            dt.Columns.Add("series");
            dt.Columns.Add("fgt");
            dt.Columns.Add("dtr");
            dt.Columns.Add("fittingtest");
            dt.Columns.Add("vs");
            dt.Columns.Add("fd");
            dt.Columns.Add("jhdate");
            dt.Columns.Add("sjdate");
            dt.Rows.Add();
            dt.Rows[0]["Shoes_figure"] = "/file/D.O.N. ISSUE 3 GCA 新款篮球运动鞋(GW6350).jpg";
            dt.Rows[0]["art"] = "GW6350";
            dt.Rows[0]["shoes"] = "D.O.N. ISSUE 3 GCA";
            dt.Rows[0]["quarter"] = "FW21";
            dt.Rows[0]["series"] = "运动鞋";
            dt.Rows[0]["fgt"] = "pass";
            dt.Rows[0]["dtr"] = "pass";
            dt.Rows[0]["fittingtest"] = "pass";
            dt.Rows[0]["vs"] = "有";
            dt.Rows[0]["fd"] = "有";
            dt.Rows[0]["jhdate"] = "2021-11-18";
            dt.Rows[0]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[1]["Shoes_figure"] = "/file/EQ21 RUN EL K 跑步鞋(GW6349).jpg";
            dt.Rows[1]["art"] = "GW6349";
            dt.Rows[1]["shoes"] = "EQ21 RUN EL K";
            dt.Rows[1]["quarter"] = "FW23";
            dt.Rows[1]["series"] = "跑步鞋";
            dt.Rows[1]["fgt"] = "pass";
            dt.Rows[1]["dtr"] = "pass";
            dt.Rows[1]["fittingtest"] = "pass";
            dt.Rows[1]["vs"] = "有";
            dt.Rows[1]["fd"] = "有";
            dt.Rows[1]["jhdate"] = "2021-11-14";
            dt.Rows[1]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[2]["Shoes_figure"] = "/file/FORTARUN ATR EL K 跑步鞋(GW6348).jpg";
            dt.Rows[2]["art"] = "GW6348";
            dt.Rows[2]["shoes"] = "FORTARUN ATR EL K";
            dt.Rows[2]["quarter"] = "FW21";
            dt.Rows[2]["series"] = "跑步鞋";
            dt.Rows[2]["fgt"] = "pass";
            dt.Rows[2]["dtr"] = "pass";
            dt.Rows[2]["fittingtest"] = "pass";
            dt.Rows[2]["vs"] = "有";
            dt.Rows[2]["fd"] = "有";
            dt.Rows[2]["jhdate"] = "2021-11-14";
            dt.Rows[2]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[3]["Shoes_figure"] = "/file/FORUM 84 LOW 休闲篮球鞋(GW6357).jpg";
            dt.Rows[3]["art"] = "GW6357";
            dt.Rows[3]["shoes"] = "FORUM 84 LOW";
            dt.Rows[3]["quarter"] = "FW22";
            dt.Rows[3]["series"] = "篮球鞋";
            dt.Rows[3]["fgt"] = "pass";
            dt.Rows[3]["dtr"] = "pass";
            dt.Rows[3]["fittingtest"] = "pass";
            dt.Rows[3]["vs"] = "有";
            dt.Rows[3]["fd"] = "有";
            dt.Rows[3]["jhdate"] = "2021-11-14";
            dt.Rows[3]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[4]["Shoes_figure"] = "/file/FORUM LOW 新款休闲篮球鞋(GW6353).jpg";
            dt.Rows[4]["art"] = "GW6353";
            dt.Rows[4]["shoes"] = "FORUM LOW";
            dt.Rows[4]["quarter"] = "FW21";
            dt.Rows[4]["series"] = "篮球鞋";
            dt.Rows[4]["fgt"] = "pass";
            dt.Rows[4]["dtr"] = "pass";
            dt.Rows[4]["fittingtest"] = "pass";
            dt.Rows[4]["vs"] = "有";
            dt.Rows[4]["fd"] = "有";
            dt.Rows[4]["jhdate"] = "2021-11-14";
            dt.Rows[4]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[5]["Shoes_figure"] = "/file/FORUM LOW 新款休闲篮球鞋(GW6358).jpg";
            dt.Rows[5]["art"] = "GW6358";
            dt.Rows[5]["shoes"] = "FORUM LOW";
            dt.Rows[5]["quarter"] = "FW22";
            dt.Rows[5]["series"] = "运动鞋";
            dt.Rows[5]["fgt"] = "pass";
            dt.Rows[5]["dtr"] = "pass";
            dt.Rows[5]["fittingtest"] = "pass";
            dt.Rows[5]["vs"] = "有";
            dt.Rows[5]["fd"] = "有";
            dt.Rows[5]["jhdate"] = "2021-11-14";
            dt.Rows[5]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[6]["Shoes_figure"] = "/file/POSTMOVE MID K 新款篮球鞋(GW6359).jpg";
            dt.Rows[6]["art"] = "GW6359";
            dt.Rows[6]["shoes"] = "POSTMOVE MID K";
            dt.Rows[6]["quarter"] = "FW23";
            dt.Rows[6]["series"] = "篮球鞋";
            dt.Rows[6]["fgt"] = "pass";
            dt.Rows[6]["dtr"] = "pass";
            dt.Rows[6]["fittingtest"] = "pass";
            dt.Rows[6]["vs"] = "有";
            dt.Rows[6]["fd"] = "有";
            dt.Rows[6]["jhdate"] = "2021-11-14";
            dt.Rows[6]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[7]["Shoes_figure"] = "/file/POSTMOVE MID K 新款篮球鞋1(GW6351).jpg";
            dt.Rows[7]["art"] = "GW6351";
            dt.Rows[7]["shoes"] = "POSTMOVE MID K ";
            dt.Rows[7]["quarter"] = "FW22";
            dt.Rows[7]["series"] = "篮球鞋";
            dt.Rows[7]["fgt"] = "pass";
            dt.Rows[7]["dtr"] = "pass";
            dt.Rows[7]["fittingtest"] = "pass";
            dt.Rows[7]["vs"] = "有";
            dt.Rows[7]["fd"] = "有";
            dt.Rows[7]["jhdate"] = "2021-11-14";
            dt.Rows[7]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[8]["Shoes_figure"] = "/file/RAPIDAZEN MID I 学步鞋(GW6352).jpg";
            dt.Rows[8]["art"] = "GW6352";
            dt.Rows[8]["shoes"] = "RAPIDAZEN MID I";
            dt.Rows[8]["quarter"] = "FW21";
            dt.Rows[8]["series"] = "学步鞋";
            dt.Rows[8]["fgt"] = "pass";
            dt.Rows[8]["dtr"] = "pass";
            dt.Rows[8]["fittingtest"] = "pass";
            dt.Rows[8]["vs"] = "有";
            dt.Rows[8]["fd"] = "有";
            dt.Rows[8]["jhdate"] = "2021-11-14";
            dt.Rows[8]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[9]["Shoes_figure"] = "/file/SUPERSTAR W 新款经典贝壳头板鞋(GW6360).jpg";
            dt.Rows[9]["art"] = "GW6360";
            dt.Rows[9]["shoes"] = "SUPERSTAR W ";
            dt.Rows[9]["quarter"] = "FW23";
            dt.Rows[9]["series"] = "头板鞋";
            dt.Rows[9]["fgt"] = "pass";
            dt.Rows[9]["dtr"] = "pass";
            dt.Rows[9]["fittingtest"] = "pass";
            dt.Rows[9]["vs"] = "有";
            dt.Rows[9]["fd"] = "有";
            dt.Rows[9]["jhdate"] = "2021-11-14";
            dt.Rows[9]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[10]["Shoes_figure"] = "/file/SUPERSTAR 新款经典运动板鞋(GW6361).jpg";
            dt.Rows[10]["art"] = "GW6361";
            dt.Rows[10]["shoes"] = "SUPERSTAR";
            dt.Rows[10]["quarter"] = "FW23";
            dt.Rows[10]["series"] = "运动鞋";
            dt.Rows[10]["fgt"] = "pass";
            dt.Rows[10]["dtr"] = "pass";
            dt.Rows[10]["fittingtest"] = "pass";
            dt.Rows[10]["vs"] = "有";
            dt.Rows[10]["fd"] = "有";
            dt.Rows[10]["jhdate"] = "2021-11-14";
            dt.Rows[10]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[11]["Shoes_figure"] = "/file/TRAE YOUNG 1 特雷·杨第一代新款篮球鞋(GW6362).jpg";
            dt.Rows[11]["art"] = "GW6362";
            dt.Rows[11]["shoes"] = "TRAE YOUNG 1";
            dt.Rows[11]["quarter"] = "FW22";
            dt.Rows[11]["series"] = "篮球鞋";
            dt.Rows[11]["fgt"] = "pass";
            dt.Rows[11]["dtr"] = "pass";
            dt.Rows[11]["fittingtest"] = "pass";
            dt.Rows[11]["vs"] = "有";
            dt.Rows[11]["fd"] = "有";
            dt.Rows[11]["jhdate"] = "2021-11-14";
            dt.Rows[11]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[12]["Shoes_figure"] = "/file/UB ATR 新款实用舒适跑步鞋(GW6363).jpg";
            dt.Rows[12]["art"] = "GW6363";
            dt.Rows[12]["shoes"] = "UB ATR";
            dt.Rows[12]["quarter"] = "FW21";
            dt.Rows[12]["series"] = "跑步鞋";
            dt.Rows[12]["fgt"] = "pass";
            dt.Rows[12]["dtr"] = "pass";
            dt.Rows[12]["fittingtest"] = "pass";
            dt.Rows[12]["vs"] = "有";
            dt.Rows[12]["fd"] = "有";
            dt.Rows[12]["jhdate"] = "2021-11-14";
            dt.Rows[12]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[13]["Shoes_figure"] = "/file/ULTRABOOST 22 J 新款运动鞋(GW6364).jpg";
            dt.Rows[13]["art"] = "GW6364";
            dt.Rows[13]["shoes"] = "ULTRABOOST 22 J ";
            dt.Rows[13]["quarter"] = "FW24";
            dt.Rows[13]["series"] = "运动鞋";
            dt.Rows[13]["fgt"] = "pass";
            dt.Rows[13]["dtr"] = "pass";
            dt.Rows[13]["fittingtest"] = "pass";
            dt.Rows[13]["vs"] = "有";
            dt.Rows[13]["fd"] = "有";
            dt.Rows[13]["jhdate"] = "2021-11-14";
            dt.Rows[13]["sjdate"] = "2021-11-16";
            dt.Rows.Add();
            dt.Rows[14]["Shoes_figure"] = "/file/ULTRABOOST MADE TO BE REMADE 新款实用舒适跑步鞋(GW6365).jpg";
            dt.Rows[14]["art"] = "GW6355";
            dt.Rows[14]["shoes"] = "ULTRABOOST MADE TO BE REMADE";
            dt.Rows[14]["quarter"] = "FW21";
            dt.Rows[14]["series"] = "跑步鞋";
            dt.Rows[14]["fgt"] = "pass";
            dt.Rows[14]["dtr"] = "pass";
            dt.Rows[14]["fittingtest"] = "pass";
            dt.Rows[14]["vs"] = "有";
            dt.Rows[14]["fd"] = "有";
            dt.Rows[14]["jhdate"] = "2021-11-14";
            dt.Rows[14]["sjdate"] = "2021-11-16";
            #endregion

            return dt;
        }

        public void BindingData()
        {
            int i = 0;
            foreach (DataRow dr in InitializeData().Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[i];
                dgvr.Height = 45;
                dgvr.Cells["art"].Value = dr["art"].ToString();
                dgvr.Cells["shoes"].Value = dr["shoes"].ToString();
                dgvr.Cells["quarter"].Value = dr["quarter"].ToString();
                dgvr.Cells["series"].Value = dr["series"].ToString();
                dgvr.Cells["fgt"].Value = dr["fgt"].ToString();
                dgvr.Cells["dtr"].Value = dr["dtr"].ToString();
                dgvr.Cells["fittingtest"].Value = dr["fittingtest"].ToString();
                dgvr.Cells["vs"].Value = dr["vs"].ToString();
                dgvr.Cells["fd"].Value = dr["fd"].ToString();
                dgvr.Cells["jhdate"].Value = dr["jhdate"].ToString();
                dgvr.Cells["sjdate"].Value = dr["sjdate"].ToString();
                if (!string.IsNullOrEmpty(dr["Shoes_figure"].ToString()))
                {
                    try
                    {
                        var webC = new System.Net.WebClient();
                        string url = Program.Client.PicUrl + Convert.ToString(dr["Shoes_figure"].ToString());
                        Image image = new Bitmap(webC.OpenRead(url));
                        dgvr.Cells["Shoes_figure"].Value = image;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    dgvr.Cells["Shoes_figure"].Value = null;
                }
                i++;
            }
            GenClass.AutoSizeColumn(dataGridView1);
        }

        private void F_QCM_CWAState_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                where += $@" and art like '%{textBox1.Text}%'";
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                where += $@" and quarter like '%{textBox2.Text}%'";
            }
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                where += $@" and series like '%{textBox3.Text}%'";
            }
            if (!string.IsNullOrEmpty(dateTimePicker1.Value.ToString("yyyy-MM-dd"))&& !string.IsNullOrEmpty(dateTimePicker2.Value.ToString("yyyy-MM-dd")))
            {
                where += $@" and jhdate>='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and jhdate<='{dateTimePicker2.Value.ToString("yyyy-MM-dd")}'";
            }
            
            DataRow[] drr = InitializeData().Select($@"1=1 {where}");
            DataTable dt = InitializeData().Clone();
            for (int i = 0; i < drr.Length; i++)
            {
                dt.ImportRow(drr[i]);
            }

            if (dataGridView1.Rows.Count >= 0)
            {
                dataGridView1.Rows.Clear();
            }

            int a = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[a];
                dgvr.Height = 45;
                dgvr.Cells["art"].Value = dr["art"].ToString();
                dgvr.Cells["shoes"].Value = dr["shoes"].ToString();
                dgvr.Cells["quarter"].Value = dr["quarter"].ToString();
                dgvr.Cells["series"].Value = dr["series"].ToString();
                dgvr.Cells["fgt"].Value = dr["fgt"].ToString();
                dgvr.Cells["dtr"].Value = dr["dtr"].ToString();
                dgvr.Cells["fittingtest"].Value = dr["fittingtest"].ToString();
                dgvr.Cells["vs"].Value = dr["vs"].ToString();
                dgvr.Cells["fd"].Value = dr["fd"].ToString();
                dgvr.Cells["jhdate"].Value = dr["jhdate"].ToString();
                dgvr.Cells["sjdate"].Value = dr["sjdate"].ToString();
                if (!string.IsNullOrEmpty(dr["Shoes_figure"].ToString()))
                {
                    try
                    {
                        var webC = new System.Net.WebClient();
                        string url = Program.Client.PicUrl + Convert.ToString(dr["Shoes_figure"].ToString());
                        Image image = new Bitmap(webC.OpenRead(url));
                        dgvr.Cells["Shoes_figure"].Value = image;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    dgvr.Cells["Shoes_figure"].Value = null;
                }
                a++;
            }
        }

    }
}
