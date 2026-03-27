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

namespace SJeMES_QCM
{
    public partial class F_QCM_Broken_Needle_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Broken_Needle_List()
        {
            InitializeComponent();
            BindingData();
            pageControl1.BindPageEvent += BindingData2;
            FormLoad();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        public static DataTable InitializeData()
        {
            #region 初始化数据
            DataTable dt = new DataTable();
            dt.Columns.Add("number");
            dt.Columns.Add("Breakage_time");
            dt.Columns.Add("remark");
            dt.Rows.Add();
            dt.Rows[0]["number"] = "1";
            dt.Rows[0]["Breakage_time"] = "2021-11-08 15:50:30";
            dt.Rows[0]["remark"] = "操作小心点";
            dt.Rows.Add();
            dt.Rows[1]["number"] = "2";
            dt.Rows[1]["Breakage_time"] = "2021-11-12 14:55:26";
            dt.Rows[1]["remark"] = "随手留下个备注";
            dt.Rows.Add();
            dt.Rows[2]["number"] = "3";
            dt.Rows[2]["Breakage_time"] = "2021-01-11 14:43:43";
            dt.Rows[2]["remark"] = "";
            dt.Rows.Add();
            dt.Rows[3]["number"] = "4";
            dt.Rows[3]["Breakage_time"] = "2021-04-12 05:30:23";
            dt.Rows[3]["remark"] = "没啥问题";
            dt.Rows.Add();
            dt.Rows[4]["number"] = "5";
            dt.Rows[4]["Breakage_time"] = "2021-09-08 17:25:12";
            dt.Rows[4]["remark"] = "";
            dt.Rows.Add();
            dt.Rows[5]["number"] = "6";
            dt.Rows[5]["Breakage_time"] = "2021-02-08 09:10:23";
            dt.Rows[5]["remark"] = "无";
            dt.Rows.Add();
            dt.Rows[6]["number"] = "7";
            dt.Rows[6]["Breakage_time"] = "2021-05-08 04:20:36";
            dt.Rows[6]["remark"] = "www";
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
                dgvr.Cells["Breakage_time"].Value = dr["Breakage_time"].ToString();
                dgvr.Cells["remark"].Value = dr["remark"].ToString();
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

        private void F_QCM_Broken_Needle_List_Load(object sender, EventArgs e)
        {

        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(dateBreakagetimeD.Value.ToString("yyyy-MM-dd HH:mm:ss"))|| !string.IsNullOrEmpty(dateBreakagetimeX.Value.ToString("yyyy-MM-dd HH:mm:ss")))
            {
                where += $@" and Breakage_time>='{dateBreakagetimeD.Value.ToString("yyyy-MM-dd HH:mm:ss")}' and Breakage_time<='{dateBreakagetimeX.Value.ToString("yyyy-MM-dd HH:mm:ss")}'";
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
                dgvr.Cells["Breakage_time"].Value = dr["Breakage_time"].ToString();
                dgvr.Cells["remark"].Value = dr["remark"].ToString();
                a++;
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
                    if (cell.CurrentItem.Equals("SelectImg"))
                    {

                        FrmShowImg f = new FrmShowImg(Program.Client.PicUrl+ "/File/断针照片.png", "断针照片");
                        f.ShowDialog();
                    }
                }
            }
        }
    }
}
