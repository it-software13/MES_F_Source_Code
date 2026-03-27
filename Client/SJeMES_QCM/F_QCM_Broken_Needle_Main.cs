using DataGrid.DataGridViewCustomColumn;
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
using System.Windows.Forms;

namespace SJeMES_QCM
{
    public partial class F_QCM_Broken_Needle_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Broken_Needle_Main()
        {
            InitializeComponent();
            BindingData();
            pageControl1.BindPageEvent += BindingData2;
            FormLoad();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public class info
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        private void F_QCM_Broken_Needle_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            List<info> ii = new List<info>();
            info info1 = new info() { code = "一线", name = "一线" };
            info info2 = new info() { code = "二线", name = "二线" };
            info info3 = new info() { code = "三线", name = "三线" };
            ii.Add(info1);
            ii.Add(info2);
            ii.Add(info3);
            comProduction_line.DataSource = ii;
            comProduction_line.ValueMember = "code";
            comProduction_line.DisplayMember = "name";
        }

        public static DataTable InitializeData()
        {
            #region 初始化数据
            DataTable dt = new DataTable();
            dt.Columns.Add("number");
            dt.Columns.Add("plant");
            dt.Columns.Add("Production_line");
            dt.Columns.Add("recipients_num");
            dt.Columns.Add("inuse_num");
            dt.Columns.Add("surplus_needlenum");
            dt.Columns.Add("Broken_needle_num");
            dt.Rows.Add();
            dt.Rows[0]["number"] = "1";
            dt.Rows[0]["plant"] = "香洲";
            dt.Rows[0]["Production_line"] = "一线";
            dt.Rows[0]["recipients_num"] = "20";
            dt.Rows[0]["inuse_num"] = "10";
            dt.Rows[0]["surplus_needlenum"] = "42";
            dt.Rows[0]["Broken_needle_num"] = "12";
            dt.Rows.Add();
            dt.Rows[1]["number"] = "2";
            dt.Rows[1]["plant"] = "东红";
            dt.Rows[1]["Production_line"] = "一线";
            dt.Rows[1]["recipients_num"] = "26";
            dt.Rows[1]["inuse_num"] = "20";
            dt.Rows[1]["surplus_needlenum"] = "70";
            dt.Rows[1]["Broken_needle_num"] = "32";
            dt.Rows.Add();
            dt.Rows[2]["number"] = "3";
            dt.Rows[2]["plant"] = "宏国";
            dt.Rows[2]["Production_line"] = "一线";
            dt.Rows[2]["recipients_num"] = "31";
            dt.Rows[2]["inuse_num"] = "20";
            dt.Rows[2]["surplus_needlenum"] = "40";
            dt.Rows[2]["Broken_needle_num"] = "30";
            dt.Rows.Add();
            dt.Rows[3]["number"] = "4";
            dt.Rows[3]["plant"] = "香洲";
            dt.Rows[3]["Production_line"] = "二线";
            dt.Rows[3]["recipients_num"] = "60";
            dt.Rows[3]["inuse_num"] = "23";
            dt.Rows[3]["surplus_needlenum"] = "62";
            dt.Rows[3]["Broken_needle_num"] = "23";
            dt.Rows.Add();
            dt.Rows[4]["number"] = "5";
            dt.Rows[4]["plant"] = "东红";
            dt.Rows[4]["Production_line"] = "二线";
            dt.Rows[4]["recipients_num"] = "62";
            dt.Rows[4]["inuse_num"] = "42";
            dt.Rows[4]["surplus_needlenum"] = "62";
            dt.Rows[4]["Broken_needle_num"] = "32";
            dt.Rows.Add();
            dt.Rows[5]["number"] = "6";
            dt.Rows[5]["plant"] = "宏国";
            dt.Rows[5]["Production_line"] = "二线";
            dt.Rows[5]["recipients_num"] = "72";
            dt.Rows[5]["inuse_num"] = "34";
            dt.Rows[5]["surplus_needlenum"] = "13";
            dt.Rows[5]["Broken_needle_num"] = "2";
            dt.Rows.Add();
            dt.Rows[6]["number"] = "7";
            dt.Rows[6]["plant"] = "香洲";
            dt.Rows[6]["Production_line"] = "三线";
            dt.Rows[6]["recipients_num"] = "73";
            dt.Rows[6]["inuse_num"] = "71";
            dt.Rows[6]["surplus_needlenum"] = "12";
            dt.Rows[6]["Broken_needle_num"] = "3";
            dt.Rows.Add();
            dt.Rows[7]["number"] = "8";
            dt.Rows[7]["plant"] = "东红";
            dt.Rows[7]["Production_line"] = "三线";
            dt.Rows[7]["recipients_num"] = "62";
            dt.Rows[7]["inuse_num"] = "23";
            dt.Rows[7]["surplus_needlenum"] = "72";
            dt.Rows[7]["Broken_needle_num"] = "23";
            dt.Rows.Add();
            dt.Rows[8]["number"] = "9";
            dt.Rows[8]["plant"] = "宏国";
            dt.Rows[8]["Production_line"] = "三线";
            dt.Rows[8]["recipients_num"] = "83";
            dt.Rows[8]["inuse_num"] = "34";
            dt.Rows[8]["surplus_needlenum"] = "82";
            dt.Rows[8]["Broken_needle_num"] = "34";
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
                dgvr.Cells["number"].Value = dr["number"].ToString();
                dgvr.Cells["plant"].Value = dr["plant"].ToString();
                dgvr.Cells["Production_line"].Value = dr["Production_line"].ToString();
                dgvr.Cells["recipients_num"].Value = dr["recipients_num"].ToString();
                dgvr.Cells["inuse_num"].Value = dr["inuse_num"].ToString();
                dgvr.Cells["surplus_needlenum"].Value = dr["surplus_needlenum"].ToString();
                dgvr.Cells["Broken_needle_num"].Value = dr["Broken_needle_num"].ToString();
                i++;
            }
            GenClass.AutoSizeColumn(dataGridView1);
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

        private void btnselect_Click(object sender, EventArgs e)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(txtplant.Text))
            {
                where += $@" and plant like '%{txtplant.Text}%'";
            }
            if (!string.IsNullOrEmpty(comProduction_line.SelectedValue.ToString()))
            {
                where += $@" and Production_line = '{comProduction_line.SelectedValue.ToString()}'";
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
                dgvr.Cells["number"].Value = dr["number"].ToString();
                dgvr.Cells["plant"].Value = dr["plant"].ToString();
                dgvr.Cells["Production_line"].Value = dr["Production_line"].ToString();
                dgvr.Cells["recipients_num"].Value = dr["recipients_num"].ToString();
                dgvr.Cells["inuse_num"].Value = dr["inuse_num"].ToString();
                dgvr.Cells["surplus_needlenum"].Value = dr["surplus_needlenum"].ToString();
                dgvr.Cells["Broken_needle_num"].Value = dr["Broken_needle_num"].ToString();
                a++;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            F_QCM_Broken_Needle_Edit ee = new F_QCM_Broken_Needle_Edit();
            ee.ShowDialog();
            string plant = ee.pa;
            string Production_line = ee.pl;
            switch (plant)
            {
                case "香洲":
                    switch (Production_line)
                    {
                        case "一线":
                            dataGridView1.Rows[0].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[0].Cells["Broken_needle_num"].Value)+1;
                            break;
                        case "二线":
                            dataGridView1.Rows[3].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[3].Cells["Broken_needle_num"].Value) + 1;
                            break;
                        case "三线":
                            dataGridView1.Rows[6].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[6].Cells["Broken_needle_num"].Value) + 1;
                            break;
                        default:
                            break;
                    }
                    break;
                case "东红":
                    switch (Production_line)
                    {
                        case "一线":
                            dataGridView1.Rows[1].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[1].Cells["Broken_needle_num"].Value) + 1;
                            break;
                        case "二线":
                            dataGridView1.Rows[4].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[4].Cells["Broken_needle_num"].Value) + 1;
                            break;
                        case "三线":
                            dataGridView1.Rows[7].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[7].Cells["Broken_needle_num"].Value) + 1;
                            break;
                        default:
                            break;
                    }
                    break;
                case "宏国":
                    switch (Production_line)
                    {
                        case "一线":
                            dataGridView1.Rows[2].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[2].Cells["Broken_needle_num"].Value) + 1;
                            break;
                        case "二线":
                            dataGridView1.Rows[5].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[5].Cells["Broken_needle_num"].Value) + 1;
                            break;
                        case "三线":
                            dataGridView1.Rows[8].Cells["Broken_needle_num"].Value = Convert.ToDouble(dataGridView1.Rows[8].Cells["Broken_needle_num"].Value) + 1;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("SELECT"))
                    {
                        F_QCM_Broken_Needle_List l = new F_QCM_Broken_Needle_List();
                        l.ShowDialog();
                    }
                }
            }
        }
    }
}
