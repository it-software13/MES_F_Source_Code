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
    public partial class F_QCM_ComplianceManagement_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_ComplianceManagement_Main()
        {
            InitializeComponent();
            BindingData();
            pageControl1.BindPageEvent += BindingData2;
            FormLoad();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
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
            dt.Columns.Add("Item_no");
            dt.Columns.Add("item_name");
            dt.Columns.Add("supplier");
            dt.Columns.Add("state");
            dt.Columns.Add("start_date");
            dt.Columns.Add("end_date");
            dt.Rows.Add();
            dt.Rows[0]["Item_no"] = "4010200074";
            dt.Rows[0]["item_name"] = "黑白076A 1.4-1.6MM PHOENIX EPM3牛皮(软硬度:3.3-4.3)";
            dt.Rows[0]["supplier"] = "禾云";
            dt.Rows[0]["state"] = "正常";
            dt.Rows[0]["start_date"] = "2021-11-08";
            dt.Rows[0]["end_date"] = "2021-11-18";
            dt.Rows.Add();
            dt.Rows[1]["Item_no"] = "4010200125";
            dt.Rows[1]["item_name"] = "容易黄A9MU底/专金06FO面 1.4-1.6MM PHOENIX LASEREFM3牛皮(软硬度3.3-4.3)";
            dt.Rows[1]["supplier"] = "大辉";
            dt.Rows[1]["state"] = "正常";
            dt.Rows[1]["start_date"] = "2021-10-17";
            dt.Rows[1]["end_date"] = "2021-10-28";
            dt.Rows.Add();
            dt.Rows[2]["Item_no"] = "4010200122";
            dt.Rows[2]["item_name"] = "容易黄A9MJ底/茄紫051A面 1.4-1.6MM PHOENIX LASERPEFM3牛皮 (软硬度:3.3-4.3)";
            dt.Rows[2]["supplier"] = "万国";
            dt.Rows[2]["state"] = "正常";
            dt.Rows[2]["start_date"] = "2021-11-18";
            dt.Rows[2]["end_date"] = "2022-02-24";
            dt.Rows.Add();
            dt.Rows[3]["Item_no"] = "4010200124";
            dt.Rows[3]["item_name"] = "容易黄A9MU底/粟红013A面 1.4-1.6MM PHOENIX LASEREFM3牛皮 (软硬度:3.3-4.3)";
            dt.Rows[3]["supplier"] = "创达";
            dt.Rows[3]["state"] = "临期";
            dt.Rows[3]["start_date"] = "2021-01-08";
            dt.Rows[3]["end_date"] = "2021-11-05";
            dt.Rows.Add();
            dt.Rows[4]["Item_no"] = "4010200123";
            dt.Rows[4]["item_name"] = "容易黄A9MU底/新黑AOCM面 1.4-1.6MM PHOENIX LASEEREPM3牛皮 (软硬度:3.3-4.3)";
            dt.Rows[4]["supplier"] = "万丰";
            dt.Rows[4]["state"] = "已失效";
            dt.Rows[4]["start_date"] = "2021-08-04";
            dt.Rows[4]["end_date"] = "2022-04-08";
            dt.Rows.Add();
            dt.Rows[5]["Item_no"] = "4010200073";
            dt.Rows[5]["item_name"] = "遗白08S1 1.2-1.4MM HELIOS EPM3皮 (软硬度:2.8-3.6)";
            dt.Rows[5]["supplier"] = "众联";
            dt.Rows[5]["state"] = "已失效";
            dt.Rows[5]["start_date"] = "2022-01-02";
            dt.Rows[5]["end_date"] = "2022-04-06";
            dt.Rows.Add();
            dt.Rows[6]["Item_no"] = "4010100450";
            dt.Rows[6]["item_name"] = "古典灰A5J4 1.1-1.3MM CONTACTO EPM3牛皮(软硬度:3.8-4.6/含油量:7-10%)";
            dt.Rows[6]["supplier"] = "丰泰";
            dt.Rows[6]["state"] = "临期";
            dt.Rows[6]["start_date"] = "2021-02-09";
            dt.Rows[6]["end_date"] = "2021-08-06";
            dt.Rows.Add();
            dt.Rows[7]["Item_no"] = "A008";
            dt.Rows[7]["item_name"] = "织带";
            dt.Rows[7]["supplier"] = "Sadase";
            dt.Rows[7]["state"] = "正常";
            dt.Rows[7]["start_date"] = "2020-08-07";
            dt.Rows[7]["end_date"] = "2020-11-19";
            dt.Rows.Add();
            dt.Rows[8]["Item_no"] = "A009";
            dt.Rows[8]["item_name"] = "车线";
            dt.Rows[8]["supplier"] = "Prime";
            dt.Rows[8]["state"] = "临期";
            dt.Rows[8]["start_date"] = "2021-08-04";
            dt.Rows[8]["end_date"] = "2021-11-18";
            dt.Rows.Add();
            dt.Rows[9]["Item_no"] = "A010";
            dt.Rows[9]["item_name"] = "鞋垫";
            dt.Rows[9]["supplier"] = "香洲";
            dt.Rows[9]["state"] = "正常";
            dt.Rows[9]["start_date"] = "2021-01-06";
            dt.Rows[9]["end_date"] = "2021-06-07";
            dt.Rows.Add();
            dt.Rows[10]["Item_no"] = "A011";
            dt.Rows[10]["item_name"] = "中底板";
            dt.Rows[10]["supplier"] = "东红";
            dt.Rows[10]["state"] = "正常";
            dt.Rows[10]["start_date"] = "2021-05-07";
            dt.Rows[10]["end_date"] = "2022-01-08";
            dt.Rows.Add();
            dt.Rows[11]["Item_no"] = "A012";
            dt.Rows[11]["item_name"] = "铜材";
            dt.Rows[11]["supplier"] = "宏国";
            dt.Rows[11]["state"] = "正常";
            dt.Rows[11]["start_date"] = "2021-08-09";
            dt.Rows[11]["end_date"] = "2021-11-06";
            dt.Rows.Add();
            dt.Rows[12]["Item_no"] = "A013";
            dt.Rows[12]["item_name"] = "木材";
            dt.Rows[12]["supplier"] = "良甲";
            dt.Rows[12]["state"] = "正常";
            dt.Rows[12]["start_date"] = "2021-03-04";
            dt.Rows[12]["end_date"] = "2021-04-05";
            dt.Rows.Add();
            dt.Rows[13]["Item_no"] = "A014";
            dt.Rows[13]["item_name"] = "挂钩";
            dt.Rows[13]["supplier"] = "栢鑫";
            dt.Rows[13]["state"] = "正常";
            dt.Rows[13]["start_date"] = "2021-11-05";
            dt.Rows[13]["end_date"] = "2022-05-09";
            dt.Rows.Add();
            dt.Rows[14]["Item_no"] = "A015";
            dt.Rows[14]["item_name"] = "万邦拖鞋";
            dt.Rows[14]["supplier"] = "先峰";
            dt.Rows[14]["state"] = "正常";
            dt.Rows[14]["start_date"] = "2021-02-05";
            dt.Rows[14]["end_date"] = "2021-06-04";
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
                dgvr.Cells["Item_no"].Value = dr["Item_no"].ToString();
                dgvr.Cells["item_name"].Value = dr["item_name"].ToString();
                dgvr.Cells["supplier"].Value = dr["supplier"].ToString();
                dgvr.Cells["state"].Value = dr["state"].ToString();
                dgvr.Cells["start_date"].Value = dr["start_date"].ToString();
                dgvr.Cells["end_date"].Value = dr["end_date"].ToString();
                i++;
            }

            dataGridView1.Rows[3].Cells["state"].Style.ForeColor = Color.Red;
            dataGridView1.Rows[3].Cells["end_date"].Style.ForeColor = Color.Red;

            dataGridView1.Rows[6].Cells["state"].Style.ForeColor = Color.Red;
            dataGridView1.Rows[6].Cells["end_date"].Style.ForeColor = Color.Red;

            dataGridView1.Rows[8].Cells["state"].Style.ForeColor = Color.Red;
            dataGridView1.Rows[8].Cells["end_date"].Style.ForeColor = Color.Red;

            GenClass.AutoSizeColumn(dataGridView1);

            dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        public class Info
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        private void F_QCM_ComplianceManagement_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            IList<Info> infoList = new List<Info>();
            Info info1 = new Info() { code = "临期", name = "临期" };
            Info info2 = new Info() { code = "已失效", name = "已失效" };
            Info info3 = new Info() { code = "正常", name = "正常" };
            infoList.Add(info1);
            infoList.Add(info2);
            infoList.Add(info3);
            comzt.DataSource = infoList;
            comzt.ValueMember = "code";
            comzt.DisplayMember = "name";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(txtph.Text))
            {
                where += $@" and Item_no like '%{txtph.Text}%'";
            }
            if (!string.IsNullOrEmpty(txtpm.Text))
            {
                where += $@" and item_name  '%{txtpm.Text}%'";
            }
            if (!string.IsNullOrEmpty(txtgys.Text))
            {
                where += $@" and supplier like '%{txtgys.Text}%'";
            }
            if (!string.IsNullOrEmpty(comzt.SelectedValue.ToString()))
            {
                where += $@" and state = '{comzt.SelectedValue.ToString()}'";
            }
            where += $@" and end_date>='{date1.Value.ToString("yyyy-MM-dd")}' and end_date<='{date2.Value.ToString("yyyy-MM-dd")}'";
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
                dgvr.Cells["Item_no"].Value = dr["Item_no"].ToString();
                dgvr.Cells["item_name"].Value = dr["item_name"].ToString();
                dgvr.Cells["supplier"].Value = dr["supplier"].ToString();
                dgvr.Cells["state"].Value = dr["state"].ToString();
                dgvr.Cells["start_date"].Value = dr["start_date"].ToString();
                dgvr.Cells["end_date"].Value = dr["end_date"].ToString();
                a++;
            }
        }

        public string filePath = string.Empty;
        public string SafeFileName = string.Empty;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = this.dataGridView1.Columns[e.ColumnIndex].Name;
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                if (name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("selectImg"))
                    {
                        FrmShowFile ff = new FrmShowFile(Program.Client.PicUrl+ "/File/A-01报告(原材料).PDF", "A-01报告");
                        ff.ShowDialog();
                    }
                    else if (cell.CurrentItem.Equals("UploadIMG"))
                    {
                        //创建文件弹出选择窗口（包括文件名）对象
                        OpenFileDialog ofd = new OpenFileDialog();
                        //判断选择的路径
                        string path = string.Empty;
                        ofd.Title = "请选择文件夹";
                        ofd.Filter = "图像文件(.jpg;.jpg;.jpeg;.gif;.png;.jpg)|.jpg;.jpeg;.gif; *.png;*.jpg";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            SafeFileName = System.IO.Path.GetFileName(ofd.FileName);
                            filePath = ofd.FileName;
                            string res = "ok";
                            if (res == "ok")
                            {
                                MessageBox.Show("上传文件成功！");
                            }
                            else
                            {
                                MessageBox.Show("上传文件失败！");
                            }
                        }
                    }
                }
            }
        }

        private void btnsc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("上传成功!");
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

        private void btndc_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDgvToTable(dataGridView1);

            Dictionary<string, string> Execldic = new Dictionary<string, string>();
            Execldic.Add("Item_no", "品号");
            Execldic.Add("item_name", "品名");
            Execldic.Add("supplier", "供应商");
            Execldic.Add("state", "状态");
            Execldic.Add("start_date", "A-01起始时间");
            Execldic.Add("end_date", "A-01到期时间");


            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.ShowDialog();
            string path = ofd.SelectedPath;
            SJeMES_Framework.Common.NPOIHelper.TableToExcel(dt, path + @"\" + $"A-01合规管理{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx", Execldic);
            MessageBox.Show("导出成功!");
        }
    }
}
