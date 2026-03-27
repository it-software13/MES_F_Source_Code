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
    public partial class F_QCM_RQCPatrol_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_RQCPatrol_List(DataTable dt)
        {
            InitializeComponent();
            textBox1.Text = dt.Rows[0]["inspection_type"].ToString();
            textBox2.Text = dt.Rows[0]["vendor"].ToString();
            textBox3.Text = dt.Rows[0]["date"].ToString();
            textBox4.Text = dt.Rows[0]["region"].ToString();
            textBox8.Text = dt.Rows[0]["Productionline"].ToString();
            textBox7.Text = dt.Rows[0]["machine"].ToString();
            textBox6.Text = dt.Rows[0]["timequantum"].ToString();
            textBox5.Text = dt.Rows[0]["order"].ToString();
            textBox12.Text = dt.Rows[0]["Codenumber"].ToString();
            textBox11.Text = dt.Rows[0]["art"].ToString();
            textBox10.Text = dt.Rows[0]["shoes"].ToString();
            textBox9.Text = dt.Rows[0]["parts"].ToString();
            textBox14.Text = dt.Rows[0]["Theoperator"].ToString();
            textBox13.Text = dt.Rows[0]["vendorhead"].ToString();
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

        private void F_QCM_RQCPatrol_List_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("测试项名称");
            dt.Columns.Add("检测标准");
            dt.Columns.Add("抽样数量");
            dt.Columns.Add("AQL级别");
            dt.Columns.Add("问题点");
            dt.Columns.Add("改善方法");
            dt.Columns.Add("是否按SOP");
            dt.Columns.Add("检验结果");
            dt.Columns.Add("备注");
            dt.Rows.Add();
            dt.Rows[0]["序号"] = "001";
            dt.Rows[0]["测试项名称"] = "外观检测";
            dt.Rows[0]["检测标准"] = ">=";
            dt.Rows[0]["抽样数量"] = "20";
            dt.Rows[0]["AQL级别"] = "1000";
            dt.Rows[0]["问题点"] = "无";
            dt.Rows[0]["改善方法"] = "无";
            dt.Rows[0]["是否按SOP"] = "FAIL";
            dt.Rows[0]["检验结果"] = "FAIL";
            dt.Rows[0]["备注"] = "wwww";
            dataGridView1.DataSource = dt;

            GenClass.AutoSizeColumn(dataGridView1);
        }


    }
}
