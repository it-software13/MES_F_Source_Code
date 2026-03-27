using DataGrid.DataGridViewCustomColumn;
using SJeMES_Control_Library;
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
    public partial class F_AQL_SpcPkgFile_Upload_View : Form
    {
        private DataTable data;
        public F_AQL_SpcPkgFile_Upload_View(DataTable dt)
        {
            InitializeComponent();
            data = dt;
        }

        private void F_QCM_FilesuploadView_Load(object sender, EventArgs e)
        {
            if (data.Rows.Count > 0)
            {
               
                foreach (DataRow dr in data.Rows)
                {
                    int i = dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    dgvr.Cells["file_name"].Value = dr["file_name"].ToString();
                    dgvr.Cells["file_url"].Value = dr["file_url"].ToString();
                    dgvr.Cells["id"].Value = dr["id"].ToString();
                    dgvr.Cells["upload_time"].Value = dr["upload_time"].ToString();
                   
                }
            }
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operation"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
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
                        if (cell.CurrentItem.Equals("DETAIL"))//查看文件
                        {
                            string file_url = Convert.ToString(dataGridView1.CurrentRow.Cells["file_url"].Value);
                            string file_name = Convert.ToString(dataGridView1.CurrentRow.Cells["file_name"].Value);
                            ShowFileHelper.ShowFile(file_url, file_name);
                        }
                        else if (cell.CurrentItem.Equals("Delete"))//删除文件
                        {
                            if (MessageHelper.ShowWarning(this, "Whether to delete the selected data？") == DialogResult.OK)
                            {
                                bool del_res = true;
                                string errMsg = "";
                                string id = Convert.ToString(dataGridView1.CurrentRow.Cells["id"].Value);
                                string file_name = Convert.ToString(dataGridView1.CurrentRow.Cells["file_name"].Value);

                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("id", id);
                                p.Add("file_name", file_name);
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                            "SJ_AQLAPI",//类库名
                                                            "SJ_AQLAPI.AQL_SpcPkgFile",//类名
                                                            "DeleteByDId",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));

                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (ret.IsSuccess)
                                {
                                    del_res = true;
                                }
                                else
                                {
                                    del_res = false;
                                    errMsg = ret.ErrMsg;
                                }
                                if (del_res)
                                {
                                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                                    dataGridView1.Rows.Remove(row);
                                    data.Rows.RemoveAt(e.RowIndex);
                                    MessageHelper.ShowSuccess(this, "successfully deleted");
                                }
                                else
                                {
                                    MessageHelper.ShowErr(this, "failed to delete:" + errMsg);
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
