using DataGrid.DataGridViewCustomColumn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Control_Library.Forms
{
    public partial class FrmFileListReadOnly : Form
    {
        private DataTable data;
        private string _apiurl;
        private string _usertoken;

        public FrmFileListReadOnly(DataTable dt, string apiurl, string usertoken)
        {
            InitializeComponent();
            data = dt;
            _apiurl = apiurl;
            _usertoken = usertoken;

        }

        private void FrmFileListReadOnly_Load(object sender, EventArgs e)
        {
            foreach (DataRow dr in data.Rows)
            {
                int i = dataGridView1.Rows.Add();
                DataGridViewRow dgvr = dataGridView1.Rows[i];
                dgvr.Cells["file_name"].Value = dr["file_name"].ToString();
                dgvr.Cells["UploadDatetime"].Value = dr["UPLOAD_TIME"].ToString();
                dgvr.Cells["file_url"].Value = dr["file_url"].ToString();
                dgvr.Cells["net_file_url"].Value = dr["net_file_url"].ToString();
                //dgvr.Cells["id"].Value = dr["id"].ToString();
                //dgvr.Cells["tablename"].Value = dr["tablename"].ToString();
                dgvr.Cells["guid"].Value = "";
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
