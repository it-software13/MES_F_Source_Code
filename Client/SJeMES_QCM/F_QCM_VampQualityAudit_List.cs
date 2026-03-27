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

namespace SJeMES_QCM
{
    public partial class F_QCM_VampQualityAudit_List : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_VampQualityAudit_List()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void F_QCM_VampQualityAudit_List_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            GitM_VAMP_QUALITY_M("","");
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation1"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        //鞋面品质审核列表查询
        public void GitM_VAMP_QUALITY_M(string QUALITY_DATE,string SUPPLIERS_NAME)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("QUALITY_DATE", QUALITY_DATE);
                data.Add("SUPPLIERS_NAME", SUPPLIERS_NAME);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                     "SJ_QCMAPI", "SJ_QCMAPI.VampQualityAudit", "GitM_VAMP_QUALITY_M", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
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
                        dgvr.Cells["SUPPLIERS_CODE"].Value = dr["SUPPLIERS_CODE"].ToString();
                        dgvr.Cells["SUPPLIERS_NAME"].Value = dr["SUPPLIERS_NAME"].ToString();
                        dgvr.Cells["QUALITY_DATE"].Value = dr["QUALITY_DATE"].ToString();
                        dgvr.Cells["SOCRE"].Value = dr["SOCRE"].ToString();
                        dgvr.Cells["GUID"].Value = dr["GUID"].ToString();
                        i++;
                    }
                    GenClass.AutoSizeColumn(dataGridView1);
                }
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            string QUALITY_DATE = datatxt.Value.ToString("yyyy-MM-dd").Trim();
            string SUPPLIERS_NAME = txtcs.Text.Trim();
            GitM_VAMP_QUALITY_M(QUALITY_DATE, SUPPLIERS_NAME);
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation1"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "operation1")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation1"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("SELECT"))//查看
                    {
                        string guid = this.dataGridView1.Rows[e.RowIndex].Cells["GUID"].Value.ToString().Trim();
                        string SUPPLIERS_NAME = this.dataGridView1.Rows[e.RowIndex].Cells["SUPPLIERS_NAME"].Value.ToString().Trim();
                        string QUALITY_DATE = this.dataGridView1.Rows[e.RowIndex].Cells["QUALITY_DATE"].Value.ToString().Trim();
                        F_QCM_VampQualityAudit_List_D dd = new F_QCM_VampQualityAudit_List_D(guid, SUPPLIERS_NAME, QUALITY_DATE);
                        dd.Show();
                    }
                }
            }
        }
    }
}
