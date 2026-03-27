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

namespace SJeMES_TQC
{
    public partial class ClainListView : Form
    {
        DataTable data;
        public ClainListView(DataTable dt)
        {
            InitializeComponent();
            data = dt;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void ClainListView_Load(object sender, EventArgs e)
        {
            if (data.Rows.Count > 0)
            {

                foreach (DataRow dr in data.Rows)
                {
                    int i = dataGridView1.Rows.Add();
                    DataGridViewRow dgvr = dataGridView1.Rows[i];
                    dgvr.Cells["file_name"].Value = dr["file_name"].ToString();
                    dgvr.Cells["file_url"].Value = dr["net_file_url"].ToString();
                    dgvr.Cells["id"].Value = dr["id"].ToString();
                }
            }
            this.dataGridView1.ClearSelection();
            this.dataGridView1.Columns["operate"].DefaultCellStyle.SelectionBackColor = Color.Transparent;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
                    if (name == "operate")
                    {
                        DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operate"] as DataGridViewOperationCell;

                        if (cell.CurrentItem == null)
                        {
                            return;
                        }
                        if (cell.CurrentItem.Equals("view"))//查看文件
                        {
                            string file_url = Convert.ToString(dataGridView1.CurrentRow.Cells["file_url"].Value);
                            string file_name = Convert.ToString(dataGridView1.CurrentRow.Cells["file_name"].Value);
                            ShowFileHelper.ShowFile(file_url, file_name);
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
