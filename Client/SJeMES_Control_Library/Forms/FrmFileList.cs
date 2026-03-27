using DataGrid.DataGridViewCustomColumn;
using GDSJ_Framework;
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
using SJeMES_Framework;

namespace SJeMES_Control_Library.Forms
{
    public partial class FrmFileList : Form
    {
        private DataTable data;
        private string stypes;
        private string _apiurl;
        private string _usertoken;
        private bool _isDelete;
        private bool _DeleteEnable;
        private string _DeleteWhereKey;
        public FrmFileList(DataTable dt, string apiurl, string usertoken, string stype = "", bool isDelete = true, bool DeleteEnable = true, string DeleteWhereKey = "")
        {
            InitializeComponent();
            data = dt;
            stypes = stype;
            _apiurl = apiurl;
            _usertoken = usertoken;
            _isDelete = isDelete;
            _DeleteEnable = DeleteEnable;
            _DeleteWhereKey = DeleteWhereKey;
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language); 
        }

        private void FrmFile_Load(object sender, EventArgs e)
        {

            if (_isDelete)
            {
                switch (stypes)
                {
                    default:
                        foreach (DataRow dr in data.Rows)
                        {
                            int i = dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["file_name"].Value = dr["file_name"].ToString();
                            dgvr.Cells["file_url"].Value = dr["file_url"].ToString();
                            dgvr.Cells["net_file_url"].Value = dr["net_file_url"].ToString();
                            dgvr.Cells["id"].Value = dr["id"].ToString();
                            dgvr.Cells["tablename"].Value = dr["tablename"].ToString();
                            dgvr.Cells["guid"].Value = "";
                        }
                        break;


                }
            }
            else
            {
                switch (stypes)
                {
                    default:
                        foreach (DataRow dr in data.Rows)
                        {
                            int i = dataGridView1.Rows.Add();
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells["file_name"].Value = dr["file_name"].ToString();
                            dgvr.Cells["file_url"].Value = dr["file_url"].ToString();
                            dgvr.Cells["net_file_url"].Value = dr["net_file_url"].ToString();
                            dgvr.Cells["id"].Value = dr["id"].ToString();
                            dgvr.Cells["tablename"].Value = dr["tablename"].ToString();
                            dgvr.Cells["guid"].Value = dr["guid"].ToString();
                        }
                        break;


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
                            string file_url = Convert.ToString(dataGridView1.CurrentRow.Cells["net_file_url"].Value);
                            string file_name = Convert.ToString(dataGridView1.CurrentRow.Cells["file_name"].Value);
                            //ShowFileHelper.ShowFile(file_url, file_name);
                            FrmShowFile frmShowFile = new FrmShowFile(file_url, file_name);
                            frmShowFile.ShowDialog();
                        }
                        else if (cell.CurrentItem.Equals("Delete"))//删除文件
                        {
                            if (!_DeleteEnable)
                            {
                                MessageBox.Show("The deletion function is restricted due to permission issues!");
                                return;
                            }
                            if (SJeMES_Control_Library.MessageHelper.ShowWarning(this, "Whether to delete the selected data？") == DialogResult.OK)
                            {
                                bool del_res = true;
                                string errMsg = "";
                                if (string.IsNullOrWhiteSpace(this.dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString()))
                                {
                                    _isDelete = false;
                                }
                                if (_isDelete)
                                {
                                    Dictionary<string, object> data = new Dictionary<string, object>();
                                    data.Add("tablename", this.dataGridView1.Rows[e.RowIndex].Cells["tablename"].Value.ToString());
                                    data.Add("id", this.dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
                                    data.Add("file_url", this.dataGridView1.Rows[e.RowIndex].Cells["file_url"].Value.ToString());
                                    if (!string.IsNullOrEmpty(_DeleteWhereKey))
                                    {
                                        string[] delArr = _DeleteWhereKey.Split('&');
                                        data.Add("guid", this.data.Rows[e.RowIndex]["GUID"].ToString());
                                        data.Add("del_key", delArr[1]);
                                        data["tablename"] = delArr[0];

                                    }
                                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(_apiurl,
                                         "SJ_QCMAPI", "SJ_QCMAPI.BASE", "DeleteFile", _usertoken, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                    if (ret.IsSuccess)
                                        del_res = true;
                                    else
                                    {
                                        del_res = false;
                                        errMsg = ret.ErrMsg;
                                    }
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
