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
    public partial class F_QCM_RQCPatrol_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_RQCPatrol_Main()
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

        public static DataTable InitializeData()
        {
            #region 初始化数据
            DataTable dt = new DataTable();
            dt.Columns.Add("vendor");
            dt.Columns.Add("inspection_no");
            dt.Columns.Add("inspection_type");
            dt.Columns.Add("date");
            dt.Columns.Add("region");
            dt.Columns.Add("Productionline");
            dt.Columns.Add("machine");
            dt.Columns.Add("timequantum");
            dt.Columns.Add("order");
            dt.Columns.Add("Codenumber");
            dt.Columns.Add("art");
            dt.Columns.Add("shoes");
            dt.Columns.Add("parts");
            dt.Columns.Add("Theoperator");
            dt.Columns.Add("vendorhead");
            dt.Columns.Add("QIP");
            dt.Columns.Add("state");
            dt.Rows.Add();
            dt.Rows[0]["vendor"] = "创达";
            dt.Rows[0]["inspection_no"] = "20211117";
            dt.Rows[0]["inspection_type"] = "日常抽检";
            dt.Rows[0]["region"] = "国内";
            dt.Rows[0]["date"] = "2021-12-13";
            dt.Rows[0]["Productionline"] = "一课";
            dt.Rows[0]["machine"] = "缝线机";
            dt.Rows[0]["timequantum"] = "13:00-14:00";
            dt.Rows[0]["order"] = "132534we";
            dt.Rows[0]["Codenumber"] = "5.5";
            dt.Rows[0]["art"] = "AUI213";
            dt.Rows[0]["shoes"] = "黑武士";
            dt.Rows[0]["parts"] = "鞋面";
            dt.Rows[0]["Theoperator"] = "王王王";
            dt.Rows[0]["vendorhead"] = "刘刘";
            dt.Rows[0]["QIP"] = "是";
            dt.Rows[0]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[1]["vendor"] = "万国";
            dt.Rows[1]["inspection_no"] = "20211117";
            dt.Rows[1]["inspection_type"] = "日常抽检";
            dt.Rows[1]["region"] = "国内";
            dt.Rows[1]["date"] = "2021-11-15";
            dt.Rows[1]["Productionline"] = "一课";
            dt.Rows[1]["machine"] = "缝线机";
            dt.Rows[1]["timequantum"] = "13:00-14:00";
            dt.Rows[1]["order"] = "132534we";
            dt.Rows[1]["Codenumber"] = "5.5";
            dt.Rows[1]["art"] = "AUI312";
            dt.Rows[1]["shoes"] = "黑武士";
            dt.Rows[1]["parts"] = "鞋面";
            dt.Rows[1]["Theoperator"] = "王王王";
            dt.Rows[1]["vendorhead"] = "刘刘";
            dt.Rows[1]["QIP"] = "是";
            dt.Rows[1]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[2]["vendor"] = "禾云";
            dt.Rows[2]["inspection_no"] = "20211117";
            dt.Rows[2]["inspection_type"] = "日常抽检";
            dt.Rows[2]["region"] = "国内";
            dt.Rows[2]["date"] = "2021-11-15";
            dt.Rows[2]["Productionline"] = "一课";
            dt.Rows[2]["machine"] = "缝线机";
            dt.Rows[2]["timequantum"] = "13:00-14:00";
            dt.Rows[2]["order"] = "132534re";
            dt.Rows[2]["Codenumber"] = "5.5";
            dt.Rows[2]["art"] = "AUI941";
            dt.Rows[2]["shoes"] = "黑武士";
            dt.Rows[2]["parts"] = "鞋面";
            dt.Rows[2]["Theoperator"] = "王王王";
            dt.Rows[2]["vendorhead"] = "刘刘";
            dt.Rows[2]["QIP"] = "是";
            dt.Rows[2]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[3]["vendor"] = "大辉";
            dt.Rows[3]["inspection_no"] = "20211117";
            dt.Rows[3]["inspection_type"] = "日常抽检";
            dt.Rows[3]["region"] = "国内";
            dt.Rows[3]["date"] = "2021-11-15";
            dt.Rows[3]["Productionline"] = "一课";
            dt.Rows[3]["machine"] = "缝线机";
            dt.Rows[3]["timequantum"] = "13:00-14:00";
            dt.Rows[3]["order"] = "132534rw";
            dt.Rows[3]["Codenumber"] = "5.5";
            dt.Rows[3]["art"] = "AUI471";
            dt.Rows[3]["shoes"] = "黑武士";
            dt.Rows[3]["parts"] = "鞋面";
            dt.Rows[3]["Theoperator"] = "王王王";
            dt.Rows[3]["vendorhead"] = "刘刘";
            dt.Rows[3]["QIP"] = "是";
            dt.Rows[3]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[4]["vendor"] = "万丰";
            dt.Rows[4]["inspection_no"] = "20211117";
            dt.Rows[4]["inspection_type"] = "日常抽检";
            dt.Rows[4]["region"] = "国内";
            dt.Rows[4]["date"] = "2021-11-15";
            dt.Rows[4]["Productionline"] = "一课";
            dt.Rows[4]["machine"] = "缝线机";
            dt.Rows[4]["timequantum"] = "13:00-14:00";
            dt.Rows[4]["order"] = "132857fj";
            dt.Rows[4]["Codenumber"] = "5.5";
            dt.Rows[4]["art"] = "AUI923";
            dt.Rows[4]["shoes"] = "黑武士";
            dt.Rows[4]["parts"] = "鞋面";
            dt.Rows[4]["Theoperator"] = "王王王";
            dt.Rows[4]["vendorhead"] = "刘刘";
            dt.Rows[4]["QIP"] = "是";
            dt.Rows[4]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[5]["vendor"] = "众联";
            dt.Rows[5]["inspection_no"] = "20211117";
            dt.Rows[5]["inspection_type"] = "日常抽检";
            dt.Rows[5]["region"] = "国内";
            dt.Rows[5]["date"] = "2021-11-15";
            dt.Rows[5]["Productionline"] = "一课";
            dt.Rows[5]["machine"] = "缝线机";
            dt.Rows[5]["timequantum"] = "13:00-14:00";
            dt.Rows[5]["order"] = "142471jf";
            dt.Rows[5]["Codenumber"] = "5.5";
            dt.Rows[5]["art"] = "AUI875";
            dt.Rows[5]["shoes"] = "黑武士";
            dt.Rows[5]["parts"] = "鞋面";
            dt.Rows[5]["Theoperator"] = "王王王";
            dt.Rows[5]["vendorhead"] = "刘刘";
            dt.Rows[5]["QIP"] = "是";
            dt.Rows[5]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[6]["vendor"] = "丰泰";
            dt.Rows[6]["inspection_no"] = "20211117";
            dt.Rows[6]["inspection_type"] = "日常抽检";
            dt.Rows[6]["region"] = "国内";
            dt.Rows[6]["date"] = "2021-11-15";
            dt.Rows[6]["Productionline"] = "一课";
            dt.Rows[6]["machine"] = "缝线机";
            dt.Rows[6]["timequantum"] = "13:00-14:00";
            dt.Rows[6]["order"] = "123141fa";
            dt.Rows[6]["Codenumber"] = "5.5";
            dt.Rows[6]["art"] = "AUI723";
            dt.Rows[6]["shoes"] = "黑武士";
            dt.Rows[6]["parts"] = "鞋面";
            dt.Rows[6]["Theoperator"] = "王王王";
            dt.Rows[6]["vendorhead"] = "刘刘";
            dt.Rows[6]["QIP"] = "是";
            dt.Rows[6]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[7]["vendor"] = "Sadase";
            dt.Rows[7]["inspection_no"] = "20211117";
            dt.Rows[7]["inspection_type"] = "日常抽检";
            dt.Rows[7]["region"] = "国内";
            dt.Rows[7]["date"] = "2021-11-15";
            dt.Rows[7]["Productionline"] = "二课";
            dt.Rows[7]["machine"] = "造纸机";
            dt.Rows[7]["timequantum"] = "14:00-19:00";
            dt.Rows[7]["order"] = "751933gi";
            dt.Rows[7]["Codenumber"] = "5.5";
            dt.Rows[7]["art"] = "AUI385";
            dt.Rows[7]["shoes"] = "火麒麟";
            dt.Rows[7]["parts"] = "鞋面";
            dt.Rows[7]["Theoperator"] = "王王王";
            dt.Rows[7]["vendorhead"] = "刘刘";
            dt.Rows[7]["QIP"] = "是";
            dt.Rows[7]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[8]["vendor"] = "Prime";
            dt.Rows[8]["inspection_no"] = "20211117";
            dt.Rows[8]["inspection_type"] = "日常抽检";
            dt.Rows[8]["region"] = "国内";
            dt.Rows[8]["date"] = "2021-11-15";
            dt.Rows[8]["Productionline"] = "三课";
            dt.Rows[8]["machine"] = "打印机";
            dt.Rows[8]["timequantum"] = "18:00-21:00";
            dt.Rows[8]["order"] = "571310fa";
            dt.Rows[8]["Codenumber"] = "5.5";
            dt.Rows[8]["art"] = "AUI851";
            dt.Rows[8]["shoes"] = "游骑兵";
            dt.Rows[8]["parts"] = "鞋面";
            dt.Rows[8]["Theoperator"] = "王王王";
            dt.Rows[8]["vendorhead"] = "刘刘";
            dt.Rows[8]["QIP"] = "是";
            dt.Rows[8]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[9]["vendor"] = "香洲";
            dt.Rows[9]["inspection_no"] = "20211117";
            dt.Rows[9]["inspection_type"] = "日常抽检";
            dt.Rows[9]["region"] = "国内";
            dt.Rows[9]["date"] = "2021-11-15";
            dt.Rows[9]["Productionline"] = "四课";
            dt.Rows[9]["machine"] = "粉碎机";
            dt.Rows[9]["timequantum"] = "15:00-19:00";
            dt.Rows[9]["order"] = "759823ja";
            dt.Rows[9]["Codenumber"] = "5.5";
            dt.Rows[9]["art"] = "AUI903";
            dt.Rows[9]["shoes"] = "巴雷特";
            dt.Rows[9]["parts"] = "鞋面";
            dt.Rows[9]["Theoperator"] = "小卢";
            dt.Rows[9]["vendorhead"] = "汪汪";
            dt.Rows[9]["QIP"] = "是";
            dt.Rows[9]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[10]["vendor"] = "东红";
            dt.Rows[10]["inspection_no"] = "20211117";
            dt.Rows[10]["inspection_type"] = "日常抽检";
            dt.Rows[10]["region"] = "国内";
            dt.Rows[10]["date"] = "2021-11-15";
            dt.Rows[10]["Productionline"] = "五课";
            dt.Rows[10]["machine"] = "搅拌机";
            dt.Rows[10]["timequantum"] = "14:00-15:00";
            dt.Rows[10]["order"] = "659812hf";
            dt.Rows[10]["Codenumber"] = "5.5";
            dt.Rows[10]["art"] = "AUI824";
            dt.Rows[10]["shoes"] = "黑骑士";
            dt.Rows[10]["parts"] = "鞋面";
            dt.Rows[10]["Theoperator"] = "小卢";
            dt.Rows[10]["vendorhead"] = "汪汪";
            dt.Rows[10]["QIP"] = "是";
            dt.Rows[10]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[11]["vendor"] = "宏国";
            dt.Rows[11]["inspection_no"] = "20211117";
            dt.Rows[11]["inspection_type"] = "日常抽检";
            dt.Rows[11]["region"] = "国内";
            dt.Rows[11]["date"] = "2021-11-15";
            dt.Rows[11]["Productionline"] = "六课";
            dt.Rows[11]["machine"] = "榨汁机";
            dt.Rows[11]["timequantum"] = "19:00-21:00";
            dt.Rows[11]["order"] = "752193fw";
            dt.Rows[11]["Codenumber"] = "5.5";
            dt.Rows[11]["art"] = "AUI298";
            dt.Rows[11]["shoes"] = "汤姆逊";
            dt.Rows[11]["parts"] = "鞋面";
            dt.Rows[11]["Theoperator"] = "小卢";
            dt.Rows[11]["vendorhead"] = "小美";
            dt.Rows[11]["QIP"] = "是";
            dt.Rows[11]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[12]["vendor"] = "良甲";
            dt.Rows[12]["inspection_no"] = "20211117";
            dt.Rows[12]["inspection_type"] = "日常抽检";
            dt.Rows[12]["region"] = "国内";
            dt.Rows[12]["date"] = "2021-11-15";
            dt.Rows[12]["Productionline"] = "七课";
            dt.Rows[12]["machine"] = "咖啡机";
            dt.Rows[12]["timequantum"] = "12:00-14:00";
            dt.Rows[12]["order"] = "156703si";
            dt.Rows[12]["Codenumber"] = "5.5";
            dt.Rows[12]["art"] = "AUI034";
            dt.Rows[12]["shoes"] = "加特林";
            dt.Rows[12]["parts"] = "鞋背";
            dt.Rows[12]["Theoperator"] = "小李";
            dt.Rows[12]["vendorhead"] = "小艾";
            dt.Rows[12]["QIP"] = "是";
            dt.Rows[12]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[13]["vendor"] = "先峰";
            dt.Rows[13]["inspection_no"] = "20211117";
            dt.Rows[13]["inspection_type"] = "日常抽检";
            dt.Rows[13]["region"] = "国内";
            dt.Rows[13]["date"] = "2021-11-15";
            dt.Rows[13]["Productionline"] = "八课";
            dt.Rows[13]["machine"] = "破壁机";
            dt.Rows[13]["timequantum"] = "21:00-23:00";
            dt.Rows[13]["order"] = "164384hg";
            dt.Rows[13]["Codenumber"] = "5.5";
            dt.Rows[13]["art"] = "AUI928";
            dt.Rows[13]["shoes"] = "手榴弹";
            dt.Rows[13]["parts"] = "鞋背";
            dt.Rows[13]["Theoperator"] = "小孙";
            dt.Rows[13]["vendorhead"] = "小齐";
            dt.Rows[13]["QIP"] = "是";
            dt.Rows[13]["state"] = "完成";
            dt.Rows.Add();
            dt.Rows[14]["vendor"] = "兴艺";
            dt.Rows[14]["inspection_no"] = "20211117";
            dt.Rows[14]["inspection_type"] = "日常抽检";
            dt.Rows[14]["region"] = "国内";
            dt.Rows[14]["date"] = "2021-11-15";
            dt.Rows[14]["Productionline"] = "九课";
            dt.Rows[14]["machine"] = "魔幻手机";
            dt.Rows[14]["timequantum"] = "23:00-24:00";
            dt.Rows[14]["order"] = "143984oh";
            dt.Rows[14]["Codenumber"] = "5.5";
            dt.Rows[14]["art"] = "AUI198";
            dt.Rows[14]["shoes"] = "散光弹";
            dt.Rows[14]["parts"] = "鞋背";
            dt.Rows[14]["Theoperator"] = "小刘";
            dt.Rows[14]["vendorhead"] = "小黑";
            dt.Rows[14]["QIP"] = "是";
            dt.Rows[14]["state"] = "完成";
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
                dgvr.Cells["vendor"].Value = dr["vendor"].ToString();
                dgvr.Cells["inspection_no"].Value = dr["inspection_no"].ToString();
                dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString();
                dgvr.Cells["region"].Value = dr["region"].ToString();
                dgvr.Cells["date"].Value = dr["date"].ToString();
                dgvr.Cells["Productionline"].Value = dr["Productionline"].ToString();
                dgvr.Cells["machine"].Value = dr["machine"].ToString();
                dgvr.Cells["timequantum"].Value = dr["timequantum"].ToString();
                dgvr.Cells["order"].Value = dr["order"].ToString();
                dgvr.Cells["shoes"].Value = dr["shoes"].ToString();
                dgvr.Cells["art"].Value = dr["art"].ToString();
                dgvr.Cells["Codenumber"].Value = dr["Codenumber"].ToString();
                dgvr.Cells["parts"].Value = dr["parts"].ToString();
                dgvr.Cells["Theoperator"].Value = dr["Theoperator"].ToString();
                dgvr.Cells["vendorhead"].Value = dr["vendorhead"].ToString();
                dgvr.Cells["QIP"].Value = dr["QIP"].ToString();
                dgvr.Cells["state"].Value = dr["state"].ToString();
                i++;
            }
            GenClass.AutoSizeColumn(dataGridView1);
        }

        public void BindingData2(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 1;
        }

        private void btnGit_Click(object sender, EventArgs e)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(txtVendor.Text))
            {
                where += $@" and vendor like '%{txtVendor.Text}%'";
            }
            if (!string.IsNullOrEmpty(txtProductionLine.Text))
            {
                where += $@" and Productionline like '%{txtProductionLine.Text}%'";
            }
            if (!string.IsNullOrEmpty(txtart.Text))
            {
                where += $@" and art like '%{txtart.Text}%'";
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
                dgvr.Cells["vendor"].Value = dr["vendor"].ToString();
                dgvr.Cells["inspection_no"].Value = dr["inspection_no"].ToString();
                dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString();
                dgvr.Cells["region"].Value = dr["region"].ToString();
                dgvr.Cells["date"].Value = dr["date"].ToString();
                dgvr.Cells["Productionline"].Value = dr["Productionline"].ToString();
                dgvr.Cells["machine"].Value = dr["machine"].ToString();
                dgvr.Cells["timequantum"].Value = dr["timequantum"].ToString();
                dgvr.Cells["order"].Value = dr["order"].ToString();
                dgvr.Cells["shoes"].Value = dr["shoes"].ToString();
                dgvr.Cells["art"].Value = dr["art"].ToString();
                dgvr.Cells["Codenumber"].Value = dr["Codenumber"].ToString();
                dgvr.Cells["parts"].Value = dr["parts"].ToString();
                dgvr.Cells["Theoperator"].Value = dr["Theoperator"].ToString();
                dgvr.Cells["vendorhead"].Value = dr["vendorhead"].ToString();
                dgvr.Cells["QIP"].Value = dr["QIP"].ToString();
                dgvr.Cells["state"].Value = dr["state"].ToString();
                a++;
            }
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            F_QCM_RQCPatrol_Edit fq = new F_QCM_RQCPatrol_Edit();
            fq.ShowDialog();
            int a = dataGridView1.Rows.Count;
            foreach (DataRow dr in fq.ha.Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[a];
                dgvr.Cells["vendor"].Value = dr["vendor"].ToString();
                dgvr.Cells["inspection_no"].Value = dr["inspection_no"].ToString();
                dgvr.Cells["inspection_type"].Value = dr["inspection_type"].ToString();
                dgvr.Cells["region"].Value = dr["region"].ToString();
                dgvr.Cells["date"].Value = dr["date"].ToString();
                dgvr.Cells["Productionline"].Value = dr["Productionline"].ToString();
                dgvr.Cells["machine"].Value = dr["machine"].ToString();
                dgvr.Cells["timequantum"].Value = dr["timequantum"].ToString();
                dgvr.Cells["order"].Value = dr["order"].ToString();
                dgvr.Cells["shoes"].Value = dr["shoes"].ToString();
                dgvr.Cells["art"].Value = dr["art"].ToString();
                dgvr.Cells["Codenumber"].Value = dr["Codenumber"].ToString();
                dgvr.Cells["parts"].Value = dr["parts"].ToString();
                dgvr.Cells["Theoperator"].Value = "小卢";
                dgvr.Cells["vendorhead"].Value = "小卢";
                dgvr.Cells["QIP"].Value = "0";
                dgvr.Cells["state"].Value = "FAIL";
                a++;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("SELECT"))//查看
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("inspection_no");
                            dt.Columns.Add("inspection_type");
                            dt.Columns.Add("vendor");
                            dt.Columns.Add("date");
                            dt.Columns.Add("region");
                            dt.Columns.Add("Productionline");
                            dt.Columns.Add("machine");
                            dt.Columns.Add("timequantum");
                            dt.Columns.Add("order");
                            dt.Columns.Add("Codenumber");
                            dt.Columns.Add("art");
                            dt.Columns.Add("shoes");
                            dt.Columns.Add("parts");
                            dt.Columns.Add("Theoperator"); 
                            dt.Columns.Add("vendorhead");
                            dt.Rows.Add();
                            dt.Rows[0]["vendor"] = dataGridView1.Rows[e.RowIndex].Cells["vendor"].Value;
                            dt.Rows[0]["inspection_no"] = dataGridView1.Rows[e.RowIndex].Cells["inspection_no"].Value;
                            dt.Rows[0]["inspection_type"] = dataGridView1.Rows[e.RowIndex].Cells["inspection_type"].Value;
                            dt.Rows[0]["region"] = dataGridView1.Rows[e.RowIndex].Cells["region"].Value;
                            dt.Rows[0]["date"] = dataGridView1.Rows[e.RowIndex].Cells["date"].Value;
                            dt.Rows[0]["Productionline"] = dataGridView1.Rows[e.RowIndex].Cells["Productionline"].Value;
                            dt.Rows[0]["machine"] = dataGridView1.Rows[e.RowIndex].Cells["machine"].Value;
                            dt.Rows[0]["timequantum"] = dataGridView1.Rows[e.RowIndex].Cells["timequantum"].Value;
                            dt.Rows[0]["order"] = dataGridView1.Rows[e.RowIndex].Cells["order"].Value;
                            dt.Rows[0]["Codenumber"] = dataGridView1.Rows[e.RowIndex].Cells["Codenumber"].Value;
                            dt.Rows[0]["art"] = dataGridView1.Rows[e.RowIndex].Cells["art"].Value;
                            dt.Rows[0]["shoes"] = dataGridView1.Rows[e.RowIndex].Cells["shoes"].Value;
                            dt.Rows[0]["parts"] = dataGridView1.Rows[e.RowIndex].Cells["parts"].Value;
                            dt.Rows[0]["Theoperator"] = dataGridView1.Rows[e.RowIndex].Cells["Theoperator"].Value;
                            dt.Rows[0]["vendorhead"] = dataGridView1.Rows[e.RowIndex].Cells["vendorhead"].Value;
                            F_QCM_RQCPatrol_List fq = new F_QCM_RQCPatrol_List(dt);
                            fq.ShowDialog();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
            }
        }

        private void F_QCM_RQCPatrol_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
    }
}
