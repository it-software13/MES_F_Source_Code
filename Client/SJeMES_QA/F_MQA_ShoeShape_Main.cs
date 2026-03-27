using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library.Forms;
using SJeMES_Framework.Common;
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

namespace SJeMES_QA
{
    public partial class F_MQA_ShoeShape_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_MQA_ShoeShape_Main()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = 25;//int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// MQA鞋型管理主页面查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetMQAMain(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                if (string.IsNullOrWhiteSpace(textBox1.Text) &&
                    string.IsNullOrWhiteSpace(textBox2.Text) &&
                    string.IsNullOrWhiteSpace(textBox3.Text) &&
                    string.IsNullOrWhiteSpace(textBox4.Text) &&
                    string.IsNullOrWhiteSpace(textBox5.Text) &&
                    string.IsNullOrWhiteSpace(textBox6.Text) &&
                    string.IsNullOrWhiteSpace(textBox7.Text) &&
                    string.IsNullOrWhiteSpace(textBox8.Text)

                    )
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please add conditions to search again！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("PROD_NO", textBox5.Text);//art
                data.Add("SHOE_NO", textBox3.Text);//鞋型编号
                data.Add("PRODUCT_MONTH", textBox8.Text);//量产月份
                data.Add("DEVELOP_SEASON", textBox1.Text);//季度

                data.Add("user_section", textBox2.Text);//开发课
                data.Add("rule_no", textBox4.Text);//Category
                data.Add("cwa_date", textBox7.Text);//CWA日期
                data.Add("qa_principal", textBox6.Text);//qa负责人
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.MQA_ShoeShape",//类名
                                            "GetMQAMain",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                //ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示

                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["PROD_NO"].Value = dr["PROD_NO"].ToString();
                        dgvr.Cells["SHOE_NO"].Value = dr["SHOE_NO"].ToString();
                        dgvr.Cells["PRODUCT_MONTH"].Value = dr["PRODUCT_MONTH"].ToString();
                        dgvr.Cells["develop_season"].Value = dr["DEVELOP_SEASON"].ToString();
                        dgvr.Cells["develop_season"].Value = dr["DEVELOP_SEASON"].ToString();

                        dgvr.Cells["Category"].Value = dr["rule_no"].ToString();
                        dgvr.Cells["user_section"].Value = dr["user_section"].ToString();
                        dgvr.Cells["TEST_LEVEL"].Value = dr["TEST_LEVEL"].ToString();
                        dgvr.Cells["PB_Type"].Value = dr["develop_type"].ToString();
                        dgvr.Cells["COL1"].Value = dr["COL1"].ToString();
                        dgvr.Cells["bom_date"].Value = dr["BOM_DATE"].ToString();
                        dgvr.Cells["cwa_date"].Value = dr["cwa_date"].ToString();
                        dgvr.Cells["user_fdd"].Value = dr["user_fdd"].ToString();
                        dgvr.Cells["user_technical"].Value = dr["user_technical"].ToString();

                        dgvr.Cells["qa_principal"].Value = dr["qa_principal"].ToString();

                        dgvr.Cells["name_t"].Value = dr["name_t"].ToString();


                        var webC = new System.Net.WebClient();
                        string url = Program.Client.PicUrl + Convert.ToString(dr["FILE_URL"].ToString());
                        try
                        {
                            Image image = new Bitmap(webC.OpenRead(url));
                            dgvr.Cells["鞋图"].Value = image;
                        }
                        catch (Exception)
                        { }
                        dgvr.Cells["xt_url"].Value = Convert.ToString(dr["FILE_URL"].ToString());//鞋图路径
                        //dgvr.Cells["user_section"].Value = dr["user_section"].ToString();
                        //dgvr.Cells["bom_date"].Value = dr["bom_date"].ToString();
                        //dgvr.Cells["cwa_date"].Value = dr["cwa_date"].ToString();
                        //dgvr.Cells["user_fdd"].Value = dr["user_fdd"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["QA文件管理"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_MQA_ShoeShape_Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GetMQAMain;
            //LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["QA文件管理"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "QA文件管理")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["QA文件管理"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    else if (cell.CurrentItem.Equals("查看详情"))//查看详情
                    {
                        string shoe_no = dataGridView1.Rows[e.RowIndex].Cells["SHOE_NO"].Value.ToString();
                        string user_fdd = dataGridView1.Rows[e.RowIndex].Cells["user_fdd"].Value.ToString();
                        string frmName = $@"F_MQA_ShoeShape_List_{shoe_no}";
                        var findFrm = Application.OpenForms[frmName];
                        if (findFrm == null)
                        {
                            F_MQA_ShoeShape_List update = new F_MQA_ShoeShape_List(shoe_no, user_fdd);
                            update.Name = frmName;
                            update.Show();
                        }
                        else
                        {
                            findFrm.Activate();
                        }
                    }
                    else if (cell.CurrentItem.Equals("MQA管理"))//DQA管理
                    {
                        string shoe_no = dataGridView1.Rows[e.RowIndex].Cells["SHOE_NO"].Value.ToString();
                        string user_fdd = dataGridView1.Rows[e.RowIndex].Cells["user_fdd"].Value.ToString();
                        string frmName = $@"F_MQA_ShoeShape_Edit_{shoe_no}";
                        var findFrm = Application.OpenForms[frmName];
                        if (findFrm == null)
                        {
                            F_MQA_ShoeShape_Edit update = new F_MQA_ShoeShape_Edit(shoe_no, user_fdd);
                            update.Name = frmName;
                            update.Show();
                        }
                        else
                        {
                            findFrm.Activate();
                        }
                    }

                }
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "鞋图")
                {
                    if (!string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["xt_url"].Value.ToString()))
                    {
                        string url = Program.Client.PicUrl + dataGridView1.Rows[e.RowIndex].Cells["xt_url"].Value.ToString();
                        FrmShowImg add = new FrmShowImg(url, "");
                        add.StartPosition = FormStartPosition.CenterParent;
                        add.Width = 459;
                        add.Height = 549;
                        add.Show();
                    }
                    else
                    {
                        MessageBox.Show("No Path!");
                    }
                }
            }
        }
    }
}
