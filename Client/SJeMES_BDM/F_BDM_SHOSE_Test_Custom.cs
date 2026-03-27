using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
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

namespace SJeMES_BDM
{
    public partial class F_BDM_SHOSE_Test_Custom : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDM_SHOSE_Test_Custom()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        /// <summary>
        /// 查询-检测项目-成品鞋-测试-定制类型
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetShoseTestInspection_Custom(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {

                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                //data.Add("pb_type_level", textBox1.Text);//新旧级别
                //data.Add("product_level_value", textBox2.Text);//产品级别
                //data.Add("category_name", textBox3.Text);//用途类别
                data.Add("fgt_name", textBox4.Text);//FGT测试类型
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_BDMAPI",//类库名
                                            "SJ_BDMAPI.BDM_Inspection",//类名
                                            "GetShoseTestInspection_Custom",//方法名
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
                        dgvr.Cells["id"].Value = dr["id"].ToString();
                        dgvr.Cells["c_no"].Value = dr["c_no"].ToString();
                        dgvr.Cells["finished_product_name"].Value = dr["finished_product_name"].ToString();
                        dgvr.Cells["pb_type_level"].Value = dr["pb_type_level"].ToString();
                        dgvr.Cells["product_level_value"].Value = dr["product_level_value"].ToString();
                        dgvr.Cells["category_name"].Value = dr["category_name"].ToString();
                        dgvr.Cells["age_gender_name"].Value = dr["age_gender_name"].ToString();
                        dgvr.Cells["fgt_name"].Value = dr["fgt_name"].ToString();
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
                this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void F_BDM_SHOSE_Test_Custom_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetShoseTestInspection_Custom;
            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (F_BDM_SHOSE_Test_Custom_Insert f = new F_BDM_SHOSE_Test_Custom_Insert())
            {
                f.ShowDialog();
            }
            LoadPage();
        }

        /// <summary>
        /// 删除-检测项目-成品鞋-测试-定制类型
        /// </summary>
        public void DeleteShoseTestInspection_Custom(string id)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                data.Add("id", id);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                         "SJ_BDMAPI", "SJ_BDMAPI.BDM_Inspection", "DeleteShoseTestInspection_Custom", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    LoadPage();
                }
                else
                    throw new Exception(j["ErrMsg"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("CHECK"))
                    {
                        string id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        using (F_BDM_SHOSE_Test_Custom_Check f = new F_BDM_SHOSE_Test_Custom_Check(id))
                        {
                            f.ShowDialog();
                        }
                    }
                    else if (cell.CurrentItem.Equals("DELETE"))
                    {
                        string id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                        DeleteShoseTestInspection_Custom(id);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadPage();
        }
    }
}
