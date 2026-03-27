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

namespace SJeMES_TSM
{
    public partial class MPAC_DATA : Form
    {
        public DataTable Selected_Data { get; private set; }
        public MPAC_DATA(DataTable dt)
        {
            InitializeComponent();
            BindData(dt);
        }
        public void BindData(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data received to display.");
                    return;
                }
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding data: " + ex.Message);
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            
                var src = this.dataGridView1; // source datagrid
                if (src == null) return;

                // create DataTable that matches your dtJson1 style
                DataTable dtSelected = new DataTable();
                dtSelected.Columns.Add("EMP_NO");
                dtSelected.Columns.Add("EMP_NAME");
                dtSelected.Columns.Add("DEPARTMENT");

                int idxSelect = src.Columns.Contains("Select") ? src.Columns["Select"].Index : -1;
                int idxEmpNo = src.Columns.Contains("EMP_NO") ? src.Columns["EMP_NO"].Index :
                               (src.Columns.Contains("Emp_No") ? src.Columns["Emp_No"].Index : -1);
                int idxEmpName = src.Columns.Contains("EMP_NAME") ? src.Columns["EMP_NAME"].Index :
                                 (src.Columns.Contains("EMP_NAME") ? src.Columns["EMP_NAME"].Index : -1);
                int idxDept = src.Columns.Contains("DEPARTMENT") ? src.Columns["DEPARTMENT"].Index :
                              (src.Columns.Contains("DEPT_NAME") ? src.Columns["DEPT_NAME"].Index : -1);

                foreach (DataGridViewRow row in src.Rows)
                {
                    if (row.IsNewRow) continue;

                    bool selected = false;
                    if (idxSelect >= 0)
                    {
                        var c = row.Cells[idxSelect];
                        if (c?.Value != null) bool.TryParse(c.Value.ToString(), out selected);
                    }

                    if (!selected) continue;

                    var r = dtSelected.NewRow();
                    r["EMP_NO"] = (idxEmpNo >= 0) ? (row.Cells[idxEmpNo].Value?.ToString() ?? "") : "";
                    r["EMP_NAME"] = (idxEmpName >= 0) ? (row.Cells[idxEmpName].Value?.ToString() ?? "") : "";
                    r["DEPARTMENT"] = (idxDept >= 0) ? (row.Cells[idxDept].Value?.ToString() ?? "") : "";
                    dtSelected.Rows.Add(r);
                }

                if (dtSelected.Rows.Count != 1)
                {
                    MessageBox.Show("Please select one employee in the source grid.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
               else
               {
                Selected_Data = dtSelected;
                this.Close();
               }
        }
           
    }
}

