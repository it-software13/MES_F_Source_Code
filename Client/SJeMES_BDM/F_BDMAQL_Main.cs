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
    public partial class F_BDMAQL_Main : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_BDMAQL_Main()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_BDMAQL_Main_Load(object sender, EventArgs e)
        { 
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            pageControl1.BindPageEvent += GitAQL;
            LoadPage();
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        //委托查询
        public void LoadPage()
        {
            pageControl1.PageSize = int.Parse(enum_page.enum_PageSize);
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }

        public void GitAQL(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("pageSize", pageSize);
                data.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.AQLStandard", "GitAQL", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                var dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dataGridView1.Rows.Count >= 0)
                {
                    dataGridView1.Rows.Clear();
                }
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dataGridView1.Rows.Add();
                        DataGridViewRow dgvr = dataGridView1.Rows[i];
                        dgvr.Cells["ID"].Value = dr["ID"].ToString();
                        dgvr.Cells["START_QTY"].Value = dr["START_QTY"].ToString();
                        dgvr.Cells["END_QTY"].Value = dr["END_QTY"].ToString();
                        dgvr.Cells["SAMPLE_QTY"].Value = dr["SAMPLE_QTY"].ToString();
                        dgvr.Cells["AC01"].Value = dr["AC01"].ToString();
                        dgvr.Cells["AC01_1"].Value = dr["RE01"].ToString();
                        dgvr.Cells["AC02"].Value = dr["AC02"].ToString();
                        dgvr.Cells["AC02_1"].Value = dr["RE02"].ToString();
                        dgvr.Cells["AC03"].Value = dr["AC03"].ToString();
                        dgvr.Cells["AC03_1"].Value = dr["RE03"].ToString();
                        dgvr.Cells["AC04"].Value = dr["AC04"].ToString();
                        dgvr.Cells["AC04_1"].Value = dr["RE04"].ToString();
                        dgvr.Cells["AC05"].Value = dr["AC05"].ToString();
                        dgvr.Cells["AC05_1"].Value = dr["RE05"].ToString();
                        dgvr.Cells["AC06"].Value = dr["AC06"].ToString();
                        dgvr.Cells["AC06_1"].Value = dr["RE06"].ToString();
                        dgvr.Cells["AC07"].Value = dr["AC07"].ToString();
                        dgvr.Cells["AC07_1"].Value = dr["RE07"].ToString();
                        dgvr.Cells["AC08"].Value = dr["AC08"].ToString();
                        dgvr.Cells["AC08_1"].Value = dr["RE08"].ToString();
                        dgvr.Cells["AC09"].Value = dr["AC09"].ToString();
                        dgvr.Cells["AC09_1"].Value = dr["RE09"].ToString();
                        dgvr.Cells["AC10"].Value = dr["AC10"].ToString();
                        dgvr.Cells["AC10_1"].Value = dr["RE10"].ToString();
                        dgvr.Cells["AC11"].Value = dr["AC11"].ToString();
                        dgvr.Cells["AC11_1"].Value = dr["RE11"].ToString();
                        dgvr.Cells["AC12"].Value = dr["AC12"].ToString();
                        dgvr.Cells["AC12_1"].Value = dr["RE12"].ToString();
                        dgvr.Cells["AC13"].Value = dr["AC13"].ToString();
                        dgvr.Cells["AC13_1"].Value = dr["RE13"].ToString();
                        dgvr.Cells["AC14"].Value = dr["AC14"].ToString();
                        dgvr.Cells["AC14_1"].Value = dr["RE14"].ToString();
                        dgvr.Cells["AC15"].Value = dr["AC15"].ToString();
                        dgvr.Cells["AC15_1"].Value = dr["RE15"].ToString();
                        dgvr.Cells["AC16"].Value = dr["AC16"].ToString();
                        dgvr.Cells["AC16_1"].Value = dr["RE16"].ToString();
                        dgvr.Cells["AC17"].Value = dr["AC17"].ToString();
                        dgvr.Cells["AC17_1"].Value = dr["RE17"].ToString();
                        dgvr.Cells["AC18"].Value = dr["AC18"].ToString();
                        dgvr.Cells["AC18_1"].Value = dr["RE18"].ToString();
                        dgvr.Cells["AC19"].Value = dr["AC19"].ToString();
                        dgvr.Cells["AC19_1"].Value = dr["RE19"].ToString();
                        dgvr.Cells["AC20"].Value = dr["AC20"].ToString();
                        dgvr.Cells["AC20_1"].Value = dr["RE20"].ToString();
                        dgvr.Cells["AC21"].Value = dr["AC21"].ToString();
                        dgvr.Cells["AC21_1"].Value = dr["RE21"].ToString();
                        dgvr.Cells["AC22"].Value = dr["AC22"].ToString();
                        dgvr.Cells["AC22_1"].Value = dr["RE22"].ToString();
                        dgvr.Cells["AC23"].Value = dr["AC23"].ToString();
                        dgvr.Cells["AC23_1"].Value = dr["RE23"].ToString();
                        dgvr.Cells["AC24"].Value = dr["AC24"].ToString();
                        dgvr.Cells["AC24_1"].Value = dr["RE24"].ToString();
                        dgvr.Cells["AC25"].Value = dr["AC25"].ToString();
                        dgvr.Cells["AC25_1"].Value = dr["RE25"].ToString();
                        dgvr.Cells["AC26"].Value = dr["AC26"].ToString();
                        dgvr.Cells["AC26_1"].Value = dr["RE26"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
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

        private void btninsert_Click(object sender, EventArgs e)
        {
            using (F_BDMAQL_Edit ff = new F_BDMAQL_Edit())
            {
                ff.ShowDialog();
                LoadPage();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
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
                    if (cell.CurrentItem.Equals("UPDATE"))
                    {
                        string id = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        using (F_BDMAQL_Edit ff = new F_BDMAQL_Edit(id))
                        {
                            ff.ShowDialog();
                            LoadPage();
                        }
                    }
                    else if (cell.CurrentItem.Equals("DELETE"))
                    {
                        string id = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        try
                        {
                            if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "是否删除").ToString().ToLower() == "ok")
                            {
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                data.Add("ID", id);
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                     "SJ_QCMAPI", "SJ_QCMAPI.AQLStandard", "DeleteAQL", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                                {

                                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("successfully deleted!", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                    SJeMES_Control_Library.MessageHelper.ShowOK(this, msg);
                                    LoadPage();
                                }
                                else
                                    throw new Exception(j["ErrMsg"].ToString());
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
}
