using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
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

namespace SJeMES_AQL
{
    public partial class F_AQL_CMAThetestshoesMin : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        private string art_no = string.Empty;
        public F_AQL_CMAThetestshoesMin(string _art_no)
        {
            InitializeComponent();
            art_no = _art_no;
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void F_AQL_CMAThetestshoesMin_Load(object sender, EventArgs e)
        {
            GenClass.AutoSizeColumnStyle(dataGridView1);
           /* this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;*/
            pageControl1.BindPageEvent += GetMain_List;
            LoadPage();

            int widths = 0;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);  // 自动调整列宽
                widths += dataGridView1.Columns[i].Width;   // 计算调整列后单元列的宽度和                     
            }
            if (widths >= dataGridView1.Size.Width)  // 如果调整列的宽度大于设定列宽
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;  // 调整列的模式 自动
            else
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;  // 如果小于 则填充
        }
        public void LoadPage()
        {
            pageControl1.PageSize = 10;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        public void GetMain_List(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
               
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("art_no", art_no);
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_AQLAPI",//类库名
                                            "SJ_AQLAPI.AQL_CMAThetestshoes",//类名
                                            "Get_MainHistory",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];

                        dgvr.Cells["internal_test_date"].Value = dr["internal_test_date"].ToString();//实验室实际内测日期
                        dgvr.Cells["id"].Value = dr["id"].ToString();//id
                        dgvr.Cells["internal_test_res"].Value = dr["internal_test_res"].ToString();
                        dgvr.Cells["external_test_res"].Value = dr["external_test_res"].ToString();
                        dgvr.Cells["external_test_date"].Value = dr["external_test_date"].ToString();//外部送测日期
                        dgvr.Cells["re_delivery_date"].Value = dr["re_delivery_date"].ToString();//再次送测日期
                        dgvr.Cells["import_date"].Value = dr["import_date"].ToString();//导入日期
                        dgvr.Cells["RE_TEST_RES"].Value = dr["RE_TEST_RES"].ToString();//导入日期
                        dgvr.Cells["prod_no"].Value = dr["art_no"].ToString();//导入日期
                        i++;
                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
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
                        if (cell.CurrentItem.Equals("delete"))//删除
                        {
                            bool flag = false;
                            string MessText = string.Empty;
                            string id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                            string prod_no = dataGridView1.CurrentRow.Cells["prod_no"].Value.ToString();
                            if (MessageBox.Show("confirm deletion? ", "This delete cannot be undone", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                try
                                {

                                    Dictionary<string, object> p = new Dictionary<string, object>();
                                    p.Add("id", id);
                                    p.Add("prod_no", prod_no);
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                         "SJ_AQLAPI", "SJ_AQLAPI.AQL_CMAThetestshoes", "Main_Delete", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (ret.IsSuccess)
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        MessText = ret.ErrMsg;
                                    }
                                    if (flag)
                                    {
                                        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                                        dataGridView1.Rows.Remove(row);
                                        //MessageHelper.ShowSuccess(this, "删除成功");
                                        MessageHelper.ShowSuccess(this, "successfully deleted");
                                    }
                                    else
                                    {
                                        //MessageHelper.ShowErr(this, "删除失败:" + MessText);
                                        MessageHelper.ShowErr(this, "failed to delete:" + MessText);
                                    }


                                }
                                catch (Exception ex)
                                {
                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
