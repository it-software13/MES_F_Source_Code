using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Quick_Changeover
{
    public partial class T_QCO_Schedule_Report : Form
    {
        DataTable dt;
        DataTable selectedRowData;
        public T_QCO_MAIN Main;
        public T_QCO_Schedule_Report(T_QCO_MAIN F)
        {
            InitializeComponent();
            monthCalendar1.Visible = false;
            Main = F;
        }
        private void UpdateDateTimePickers(int year, int month)
        {
            // Calculate the start and end dates for the selected month
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Update the DateTimePicker controls
            startDateTimePicker.Value = startDate;
            endDateTimePicker.Value = endDate;
        }
        private void monthCalendar1_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            //DataEventArgs dataEventArgs = new DataEventArgs();
            int selectedYear = monthCalendar1.SelectionStart.Year;
            int selectedMonth = monthCalendar1.SelectionStart.Month;

            UpdateDateTimePickers(selectedYear, selectedMonth);
            Load_Schedule_Data();
        }
        public void Load_Schedule_Data()
        {
            Dictionary<object, string> kk = new Dictionary<object, string>();
            kk.Add("Fromdate", startDateTimePicker.Value.ToString());
            kk.Add("Todate", endDateTimePicker.Value.ToString());
            string plants = "";
            kk.Add("Plants", plants);
            try
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "GetSPlan", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(kk));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    dt = new DataTable();
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No Data Available"); 
                        return;
                    }
                    dataGridView1.DataSource = dt;
                    dt = dataGridView1.DataSource as DataTable;
                    if (dt.Rows.Count == 0)
                    {
                        //MessageBox.Show("No Such Data!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int numRows = dataGridView1.Rows.Count;
                      //  MessageBox.Show("Total Record count: " + numRows, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }
        public void Load_Monthwise_complete_Schedule_Data()
        {
            Dictionary<object, string> kk = new Dictionary<object, string>();
            kk.Add("Fromdate", startDateTimePicker.Value.ToString());
            kk.Add("Todate", endDateTimePicker.Value.ToString());
            try
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "Monthwise_complete_Schedule_Data", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(kk));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    dt = new DataTable();
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No Data Available");
                        return;
                    }
                    dataGridView1.DataSource = dt;
                    dt = dataGridView1.DataSource as DataTable;
                    if (dt.Rows.Count == 0)
                    {
                        //MessageBox.Show("No Such Data!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int numRows = dataGridView1.Rows.Count;
                        //  MessageBox.Show("Total Record count: " + numRows, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            Load_Schedule_Data();
        }

        private void T_QCO_Schedule_Report_Load(object sender, EventArgs e)
        {
           
            int selectedYear = monthCalendar1.SelectionStart.Year;
            int selectedMonth = monthCalendar1.SelectionStart.Month;
            UpdateDateTimePickers(selectedYear, selectedMonth);
            Load_Schedule_Data();
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold); // Set the font size and style for all column headers
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectedRowIndex = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                if (selectedRowData == null)
                {
                    selectedRowData = new DataTable();
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        selectedRowData.Columns.Add(dataGridView1.Columns[i].HeaderText);
                    }
                }
                else
                {
                    selectedRowData.Clear();
                }

                DataRow newRow = selectedRowData.NewRow();

                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    if (selectedRow.Cells[i].Value != null)
                    {
                        newRow[i] = selectedRow.Cells[i].Value.ToString();
                    }
                }

                selectedRowData.Rows.Add(newRow);

                string departmentCode = null; // Declare it outside of the if block

                if (dataGridView1.Columns.Contains("DEPARTMENT_CODE"))
                {
                    int departmentCodeIndex = dataGridView1.Columns["DEPARTMENT_CODE"].Index;
                    object departmentCodeValue = selectedRow.Cells[departmentCodeIndex].Value;

                    if (departmentCodeValue != null)
                    {
                        departmentCode = departmentCodeValue.ToString();
                    }
                }

                T_QCO_Checklist2 f = new T_QCO_Checklist2();


                if (departmentCode != null)
                {
                    if (departmentCode.Contains("L"))
                    {
                        f.SetData(selectedRowData);
                        f.SetOpenTabPage1(true);

                    }
                    else
                    {
                        f.SetData2(selectedRowData);
                        f.SetOpenTabPage2(true);

                    }
                }
                f.Show();
                T_QCO_Equipment_Request ER = new T_QCO_Equipment_Request();
                ER.Fitdata(selectedRowData);

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<object, string> kk = new Dictionary<object, string>();
            kk.Add("Fromdate", startDateTimePicker.Value.ToString());
            kk.Add("Todate", endDateTimePicker.Value.ToString());
            object Plnats = plantscombo.SelectedItem;
            kk.Add("Plants", Plnats.ToString());
            try
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "GetSPlan", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(kk));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    dt = new DataTable();
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No Data Available");
                        return;
                    }
                    dataGridView1.DataSource = dt;
                    dt = dataGridView1.DataSource as DataTable;
                    if (dt.Rows.Count == 0)
                    {
                        //MessageBox.Show("No Such Data!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int numRows = dataGridView1.Rows.Count;
                        //  MessageBox.Show("Total Record count: " + numRows, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }
    }
}
