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
    public partial class F_QCM_Chemical_information_create : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Chemical_information_create()
        {
            InitializeComponent();
            BindingData();
            pageControl1.BindPageEvent += BindingData2;
            FormLoad();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public static DataTable InitializeData()
        {
            #region 初始化数据
            DataTable dt = new DataTable();
            dt.Columns.Add("number");
            dt.Columns.Add("chemicals_no");
            dt.Columns.Add("chemicals_name");
            dt.Columns.Add("validtime");
            dt.Rows.Add();
            dt.Rows[0]["number"] = "1";
            dt.Rows[0]["chemicals_no"] = "HXP111801";
            dt.Rows[0]["chemicals_name"] = "胶水T63";
            dt.Rows[0]["validtime"] = "03";
            dt.Rows.Add();
            dt.Rows[1]["number"] = "2";
            dt.Rows[1]["chemicals_no"] = "HXP111802";
            dt.Rows[1]["chemicals_name"] = "胶水R83";
            dt.Rows[1]["validtime"] = "12";
            dt.Rows.Add();
            dt.Rows[2]["number"] = "3";
            dt.Rows[2]["chemicals_no"] = "HXP111803";
            dt.Rows[2]["chemicals_name"] = "胶水Y89";
            dt.Rows[2]["validtime"] = "14";
            dt.Rows.Add();
            dt.Rows[3]["number"] = "4";
            dt.Rows[3]["chemicals_no"] = "HXP111804";
            dt.Rows[3]["chemicals_name"] = "胶水I99";
            dt.Rows[3]["validtime"] = "01";
            dt.Rows.Add();
            dt.Rows[4]["number"] = "5";
            dt.Rows[4]["chemicals_no"] = "HXP111805";
            dt.Rows[4]["chemicals_name"] = "胶水U02";
            dt.Rows[4]["validtime"] = "05";
            dt.Rows.Add();
            dt.Rows[5]["number"] = "6";
            dt.Rows[5]["chemicals_no"] = "HXP111806";
            dt.Rows[5]["chemicals_name"] = "胶水k97";
            dt.Rows[5]["validtime"] = "13";
            dt.Rows.Add();
            dt.Rows[6]["number"] = "7";
            dt.Rows[6]["chemicals_no"] = "HXP111807";
            dt.Rows[6]["chemicals_name"] = "胶水J83";
            dt.Rows[6]["validtime"] = "02";
            dt.Rows.Add();
            dt.Rows[7]["number"] = "8";
            dt.Rows[7]["chemicals_no"] = "HXP111808";
            dt.Rows[7]["chemicals_name"] = "胶水N23";
            dt.Rows[7]["validtime"] = "16";
            dt.Rows.Add();
            dt.Rows[8]["number"] = "9";
            dt.Rows[8]["chemicals_no"] = "HXP111809";
            dt.Rows[8]["chemicals_name"] = "胶水H87";
            dt.Rows[8]["validtime"] = "16";
            dt.Rows.Add();
            dt.Rows[9]["number"] = "10";
            dt.Rows[9]["chemicals_no"] = "HXP111810";
            dt.Rows[9]["chemicals_name"] = "胶水H74";
            dt.Rows[9]["validtime"] = "07";
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
                dgvr.Cells["chemicals_no"].Value = dr["chemicals_no"].ToString();
                dgvr.Cells["chemicals_name"].Value = dr["chemicals_name"].ToString();
                dgvr.Cells["validtime"].Value = dr["validtime"].ToString();
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
            if (!string.IsNullOrEmpty(txtchemicals_name.Text))
            {
                where += $@" and chemicals_name like '%{txtchemicals_name.Text}%'";
            }
            if (!string.IsNullOrEmpty(datevalidtime.Value.ToString("HH")))
            {
                where += $@" and validtime = '{datevalidtime.Value.ToString("HH")}'";
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
                dgvr.Cells["chemicals_no"].Value = dr["chemicals_no"].ToString();
                dgvr.Cells["chemicals_name"].Value = dr["chemicals_name"].ToString();
                dgvr.Cells["validtime"].Value = dr["validtime"].ToString();
                a++;
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            F_QCM_Chemical_information_create_Edit fq = new F_QCM_Chemical_information_create_Edit();
            fq.ShowDialog();
            DataTable dt = GetDgvToTable(dataGridView1);
            int a = dataGridView1.Rows.Count;
            foreach (DataRow dr in fq.ha.Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[a];
                dgvr.Cells["number"].Value = dt.AsEnumerable().Max(s => Convert.ToInt32(s.Field<string>("number"))+1);
                dgvr.Cells["chemicals_no"].Value = dr["chemicals_no"].ToString();
                dgvr.Cells["chemicals_name"].Value = dr["chemicals_name"].ToString();
                dgvr.Cells["validtime"].Value = dr["validtime"].ToString();
                a++;
            }
        }
        //dgv转datatable
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void F_QCM_Chemical_information_create_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
    }
}
