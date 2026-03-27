using MaterialSkin.Controls;
using Newtonsoft.Json;
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
    public partial class Employee_Absent_Entry : MaterialForm
    {
        private DataView dv;
        public Employee_Absent_Entry()
        {
            InitializeComponent();
        }

        private void Employee_Absent_Entry_Load(object sender, EventArgs e)
        {
            string Plant = CheckPlantIncharge();
            DateTime dt = GetNextWorkingDay();
            dt_prod_date.MinDate = dt;
            dt_prod_date.MaxDate = dt;
            tabControl1.TabPages.RemoveAt(1);
            txt_ProdPlant.Text = Plant;
            cb_process.Items.Insert(0, "");
            cb_plant.Items.Insert(0, "");
            comboBox4.Items.Insert(0, "");
        }

        public string CheckPlantIncharge()
        {
            string Plant = string.Empty;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",
                                          "SJ_TSMAPI.Production_Adjustment",
                                          "Getplant",
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Plant = ret.RetData;
            }
            return Plant;
        }
        //public void Getplant()
        //{
        //    Dictionary<string, object> data = new Dictionary<string, object>();
        //    data.Add("ProdDate", dateTimePicker1.Text);
        //    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
        //                                  Program.Client.APIURL,
        //                                  "SJ_TSMAPI",
        //                                  "SJ_TSMAPI.Production_Adjustment",
        //                                  "Getplant",
        //                                  Program.Client.UserToken,//token
        //                                  Newtonsoft.Json.JsonConvert.SerializeObject(data));
        //    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
        //    if (Convert.ToBoolean(ret.IsSuccess))
        //    {
        //        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
        //        DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow blankRow = dt.NewRow();
        //            blankRow["plant"] = "";
        //            dt.Rows.InsertAt(blankRow, 0);
        //            comboBox1.DataSource = dt;
        //            comboBox1.DisplayMember = "plant";
        //            comboBox1.ValueMember = "plant";
        //        }
        //        else
        //        {
        //            MessageHelper.ShowErr(this, "No Data Found");
        //            comboBox1.DataSource = null;
        //            comboBox1.Items.Clear();
        //        }
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, ret.ErrMsg);
        //    }
        //}
        public DateTime GetNextWorkingDay()
        {
            DateTime NextWorkingDay = DateTime.Now;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",
                                          "SJ_TSMAPI.Production_Adjustment",
                                          "GetNextWorkingDay",
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

            
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                NextWorkingDay = ret.RetData.ToDate();
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }


            return NextWorkingDay;

        }
        public string GetUserLine()
        {
            string ProdLine = string.Empty;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",//类库名
                                          "SJ_TSMAPI.Production_Adjustment",//类名
                                          "GetUserLine",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());

           // string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                ProdLine = ret.RetData;
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }

           
            return ProdLine;
        }

        public void GetLineEmployee(string ProdPlant,string Prod_Date,string Process,string ProdLine) 
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("ProdPlant", ProdPlant);
            data.Add("Prod_Date", Prod_Date);
            data.Add("Process", Process);
            data.Add("ProdLine", ProdLine);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",
                                          "SJ_TSMAPI.Production_Adjustment",
                                          "GetLineEmployee",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count>0)
                {
                    dv = new DataView(dt);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dv;
                    comboBox4.Text = "";
                }
                else
                {
                    MessageHelper.ShowErr(this, "No Data Found");
                    dataGridView1.DataSource = null;
                    comboBox4.Text = "";
                }
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
                comboBox4.Text = "";
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // 🔹 Ignore new row
            if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                return;
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Select")
            {
                if (string.IsNullOrEmpty(comboBox3.Text))
                {
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    MessageHelper.ShowErr(this, "Please select Selection Type");
                    dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = false;
                    dataGridView1.EndEdit();
                    dataGridView1.RefreshEdit();
                    return;
                }
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    // Commit checkbox edit (required)
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    // Read the current row values
                    bool isChecked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Select"].Value);
                    string barcode = dataGridView1.Rows[e.RowIndex].Cells["emp_no"].Value?.ToString();
                    string Selection_Type = comboBox3.Text;

                    if (isChecked)
                    {
                        if (Selection_Type == "Submit_Absent")
                        {
                            using (SelectSkill popup = new SelectSkill(barcode, txt_ProdPlant.Text))
                            {
                                popup.ShowDialog(this);
                                string result = popup.Result;
                                if (!string.IsNullOrEmpty(result))
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["Working_Skill"].Value = result;
                                }
                                else
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = false;
                                    dataGridView1.EndEdit();
                                    dataGridView1.RefreshEdit();
                                }
                            }
                        }
                        else if (Selection_Type == "Withdraw_Absent")
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["Working_Skill"].Value = "";
                        }

                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["Working_Skill"].Value = "";
                    }
                }
            }
        }

        private void Btn_submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageHelper.ShowErr(this, "Please select Selection Type");
                return;
            }
            TimeSpan cutoffTime = new TimeSpan(16, 30, 0); // 03:30 PM

            // Get current time
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            if (currentTime > cutoffTime)
            {
                MessageBox.Show("You cannot submit data after 04:30 PM.",
                        "Submission Blocked",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                GetLineEmployee(txt_ProdPlant.Text, dt_prod_date.Text,cb_process.Text, cb_Prodline.Text);
                return; // stop further execution
            }
            else
            {
                Submit_Absent_report(comboBox3.Text);
            }

        }

        public void Submit_Absent_report(string Type)
        {
            DataTable dt = new DataTable();
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    dt.Columns.Add(column.Name);


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                    if (isSelected)
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);

                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("dt", dt);
                data.Add("Prod_Date", dt_prod_date.Text);
                data.Add("Type", Type);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                              Program.Client.APIURL,
                                              "SJ_TSMAPI",
                                              "SJ_TSMAPI.Production_Adjustment",
                                              "SaveAbsentEmployee",
                                              Program.Client.UserToken,
                                              Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (Convert.ToBoolean(ret.IsSuccess))
                {
                    MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    GetLineEmployee(txt_ProdPlant.Text, dt_prod_date.Text, cb_process.Text, cb_Prodline.Text);
                }
                else
                {
                    MessageHelper.ShowErr(this, ret.ErrMsg);
                    GetLineEmployee(txt_ProdPlant.Text, dt_prod_date.Text, cb_process.Text, cb_Prodline.Text);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "Please select absent employee");
            }
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("From_Date", datetime_s.Text);
            data.Add("To_Date", datetime_e.Text);
            data.Add("ProdPlant", cb_plant.Text);
            data.Add("Status", cb_status.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",
                                          "SJ_TSMAPI.Production_Adjustment",
                                          "GetAbsentReport",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    MessageHelper.ShowErr(this, "No Data Found");
                    dataGridView2.DataSource = null;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }
        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            GetLineEmployee(txt_ProdPlant.Text, dt_prod_date.Text,cb_process.Text,cb_Prodline.Text);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            GetSupApplyList();
        }

        public void GetSupApplyList()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Prod_Date", dateTimePicker1.Text);
            data.Add("ProdPlant", comboBox1.Text);
            data.Add("Status", comboBox2.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",
                                          "SJ_TSMAPI.Production_Adjustment",
                                          "GetSupApplyList",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dataGridView3.DataSource = dt;
                }
                else
                {
                    MessageHelper.ShowErr(this, "No Data Found");
                    dataGridView3.DataSource = null;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }
        }

        private void Btn_accept_Click(object sender, EventArgs e)
        {
            TimeSpan cutoffTime = new TimeSpan(15, 30, 0); // 03:30 PM

            // Get current time
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            if (currentTime > cutoffTime)
            {
                MessageBox.Show("You cannot submit data after 03:30 PM.",
                        "Submission Blocked",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                GetLineEmployee(txt_ProdPlant.Text, dt_prod_date.Text, cb_process.Text, cb_Prodline.Text);
                return; // stop further execution
            }
            else
            {
                Submit_PlantAsst_Approval(btn_accept.Text);
            }
        }

        private void Btn_reject_Click(object sender, EventArgs e)
        {
            TimeSpan cutoffTime = new TimeSpan(15, 30, 0); // 03:30 PM

            // Get current time
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            if (currentTime > cutoffTime)
            {
                MessageBox.Show("You cannot submit data after 03:30 PM.",
                        "Submission Blocked",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                GetLineEmployee(txt_ProdPlant.Text, dt_prod_date.Text, cb_process.Text, cb_Prodline.Text);
                return; // stop further execution
            }
            else
            {
                Submit_PlantAsst_Approval(btn_reject.Text);
            }
        }

        public void Submit_PlantAsst_Approval(string Approval)
        {
            DataTable dt = new DataTable();
            if (dataGridView3.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView3.Columns)
                    dt.Columns.Add(column.Name);
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["Select2"].Value);
                    if (isSelected)
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("dt", dt);
                data.Add("Prod_Date", dateTimePicker1.Text);
                data.Add("Approval", Approval);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                              Program.Client.APIURL,
                                              "SJ_TSMAPI",
                                              "SJ_TSMAPI.Production_Adjustment",
                                              "Submit_PlantAsst_Approval",
                                              Program.Client.UserToken,
                                              Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (Convert.ToBoolean(ret.IsSuccess))
                {
                    MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    GetSupApplyList();
                }
                else
                {
                    MessageHelper.ShowErr(this, ret.ErrMsg);
                    GetSupApplyList();
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "Please select absent employee");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GetSupApplyList();
            comboBox1.SelectedIndex = 0;
        }

        private void Cb_process_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_ProdLines(cb_process.Text);
        }
        public void Get_ProdLines(string Process)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Process", Process);
            data.Add("Plant", txt_ProdPlant.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                         Program.Client.APIURL,
                                         "SJ_TSMAPI",
                                         "SJ_TSMAPI.Production_Adjustment",
                                         "Get_ProdLines",//方法名
                                         Program.Client.UserToken,//token
                                         Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    cb_Prodline.DataSource = null;
                    DataRow blankRow = dt.NewRow();
                    blankRow["department_code"] = ""; 
                    dt.Rows.InsertAt(blankRow, 0);
                    cb_Prodline.DataSource = dt;
                    cb_Prodline.DisplayMember = "department_code";
                    cb_Prodline.ValueMember = "department_code";
                    cb_Prodline.SelectedIndex = 0;
                }
                else
                {
                    cb_Prodline.DataSource = null;
                    cb_Prodline.Items.Clear();
                }
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns.Contains("status"))
            {
                string statusValue = Convert.ToString(
                    dataGridView1.Rows[e.RowIndex].Cells["status"].Value);

                if (statusValue == "Plant_Inchage_Applied")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor =
    Color.LightSkyBlue;

                }
                else
                {
                    // Optional: reset for other rows
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        Color.White;
                }
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (dv == null) return;

            string selectedValue = comboBox4.Text;

            if (selectedValue == "Not_Applied_Yet")
            {
                // Show rows where status is NULL or empty
                dv.RowFilter = "status IS NULL OR status = ''";
            }
            else if (selectedValue == "Plant_Inchage_Applied")
            {
                dv.RowFilter = "status = 'Plant_Inchage_Applied'";
            }
            else
            {
                // Optional: show all rows
                dv.RowFilter = "";
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineEmployee(txt_ProdPlant.Text, dt_prod_date.Text, cb_process.Text, cb_Prodline.Text);
        }
    }
}
