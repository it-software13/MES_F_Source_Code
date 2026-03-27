using MaterialSkin.Controls;
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
    public partial class MPAC_Allocation : MaterialForm
    {
        public MPAC_Allocation()
        {
            InitializeComponent();
        }
        public void ReceiveSupportFromDataTable(DataTable dt)
        {
            try
            {

                dataGridView1.CurrentRow.Cells["SUPPORT_EMP_NO"].Value = dt.Rows[0]["EMP_NO"];
                dataGridView1.CurrentRow.Cells["SUPPORT_EMP_NAME"].Value = dt.Rows[0]["EMP_NAME"];
                dataGridView1.CurrentRow.Cells["SUPPORT_EMP_DEPT"].Value = dt.Rows[0]["DEPARTMENT"];

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void MPAC_Allocation_Load(object sender, EventArgs e)
        {
            DateTime dt = GetNextWorkingDay();
            dt_prod_date.MinDate = dt;
            dt_prod_date.MaxDate = dt;
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker1.MaxDate = DateTime.Now;
            GetUserPlant("NxtWorkingDay");
            cb_allocation_status.Items.Insert(0, "");
            //cb_allocation_status.SelectedIndex = 0;
            cb_punching.Items.Insert(0, "");
           // cb_punching.SelectedIndex = 0;
            comboBox7.Items.Insert(0, "");
            //comboBox7.SelectedIndex = 0;
            cb_select_type.Items.Insert(0, "");
            comboBox5.Items.Insert(0, "");
            comboBox6.Items.Insert(0, "");
            cb_process.Items.Insert(0, "");
            //cb_select_type.SelectedIndex = 0;
        }
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
        public void GetUserPlant(string ProdDay)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("ProdDay", ProdDay);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                    Program.Client.APIURL,
                                    "SJ_TSMAPI",
                                    "SJ_TSMAPI.Production_Adjustment",
                                    "GetUserPlant",
                                    Program.Client.UserToken,
                                   Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (Convert.ToBoolean(ret.IsSuccess))
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        DataRow blankRow = dt.NewRow();
                        blankRow["PLANT"] = "";
                        dt.Rows.InsertAt(blankRow, 0);
                        if (ProdDay == "Today")
                        {
                            comboBox4.DataSource = null;
                            comboBox4.DataSource = dt;
                            comboBox4.DisplayMember = "PLANT";
                            comboBox4.ValueMember = "PLANT";
                        }
                        else if (ProdDay == "NxtWorkingDay")
                        {
                            comboBox1.DataSource = null;
                            comboBox1.DataSource = dt;
                            comboBox1.DisplayMember = "PLANT";
                            comboBox1.ValueMember = "PLANT";
                        }

                    }
                    else
                    {
                        if (ProdDay == "Today")
                        {
                            comboBox4.DataSource = null;
                            comboBox4.Items.Clear();
                        }
                        else if (ProdDay == "NxtWorkingDay")
                        {
                            comboBox1.DataSource = null;
                            comboBox1.Items.Clear();
                        }
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch
            {
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
                comboBox4.DataSource = null;
                comboBox4.Items.Clear();
            }
        }


        public string Skill_Details_Excess(string Status,string ProdPlant, string Prod_Date,string Punch_Status)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Prod_Date", Prod_Date);
            data.Add("ProdPlant", ProdPlant);
            data.Add("Status", Status);
            data.Add("Punch_Status", Punch_Status);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",//类库名
                                          "SJ_TSMAPI.Production_Adjustment",//类名
                                          "Skill_Details",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dataGridView4.DataSource = null;
                    dataGridView4.DataSource = dt;
                    foreach (DataGridViewColumn col in dataGridView4.Columns)
                    {
                        col.ReadOnly = col.Name != "Select2";
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "No Data Found");
                    dataGridView4.DataSource = null;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }
            return ProdPlant;
        }

        public string Skill_Details_MPAC(string Status, string ProdPlant, string Prod_Date,string Process)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Prod_Date", Prod_Date);
            data.Add("ProdPlant", ProdPlant);
            data.Add("Status", Status);
            data.Add("Process", Process);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",//类库名
                                          "SJ_TSMAPI.Production_Adjustment",//类名
                                          "Skill_Details",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.ReadOnly = col.Name != "Select";
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "No Data Found");
                    dataGridView1.DataSource = null;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }
            return ProdPlant;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Select")
            {
                if (e.RowIndex < 0)
                    return;
                string clickedSkill = dataGridView1.Rows[e.RowIndex].Cells["WORKING_SKILL"]
                                            .Value?.ToString();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Working_Skill", clickedSkill);
                data.Add("Type", "MPAC");
                data.Add("Prod_Date", dt_prod_date.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                    Program.Client.APIURL,
                                    "SJ_TSMAPI",
                                    "SJ_TSMAPI.Production_Adjustment",
                                    "Skill_Details2",
                                    Program.Client.UserToken,
                                    Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (Convert.ToBoolean(ret.IsSuccess))
                {
                    Dictionary<string, object> dic =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        MPAC_DATA mpacForm = new MPAC_DATA(dt);
                        mpacForm.ShowDialog();
                        DataTable SelectedData = mpacForm.Selected_Data;

                        if (SelectedData !=null && SelectedData.Rows.Count > 0)
                        {
                            string newBarcode = SelectedData.Rows[0]["EMP_NO"].ToString();
                            bool exists = false;
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells["SUPPORT_EMP_NO"].Value != null &&
                                    row.Cells["SUPPORT_EMP_NO"].Value.ToString() == newBarcode)
                                {
                                    exists = true;
                                    break;
                                }
                            }

                            if (exists)
                            {
                                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                dataGridView1.EndEdit();
                                MessageBox.Show("This employee is already selected!");
                                dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = "";
                                dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = "";
                                dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = "";
                                dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = false;
                                dataGridView1.RefreshEdit();
                            }
                            else
                            {

                                dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = SelectedData.Rows[0]["EMP_NO"].ToString();
                                dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = SelectedData.Rows[0]["EMP_NAME"].ToString();
                                dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = SelectedData.Rows[0]["DEPARTMENT"].ToString();
                            }
                        }
                        else
                        {
                            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            dataGridView1.EndEdit();
                            dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = false;
                            dataGridView1.RefreshEdit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Data Found");
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        dataGridView1.EndEdit();
                        dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = "";
                        dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = "";
                        dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = "";
                        dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = false;
                        dataGridView1.RefreshEdit();
                    }
                }
                else
                {
                    MessageBox.Show(ret.ErrMsg);
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dataGridView1.EndEdit();
                    dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = "";
                    dataGridView1.Rows[e.RowIndex].Cells["Select"].Value = false;
                    dataGridView1.RefreshEdit();
                }
            }
           
        }

        private void Button1_Click(object sender, EventArgs e)
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
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                              Program.Client.APIURL,
                                              "SJ_TSMAPI",
                                              "SJ_TSMAPI.Production_Adjustment",
                                              "SaveAbsentEmployee2",
                                              Program.Client.UserToken,
                                              Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (Convert.ToBoolean(ret.IsSuccess))
                {
                    MessageBox.Show("Updated Sucessfully");
                    Skill_Details_MPAC(comboBox7.Text, comboBox1.Text, dt_prod_date.Text, cb_process.Text);
                }
                else
                {
                    MessageHelper.ShowErr(this, ret.ErrMsg);
                    Skill_Details_MPAC(comboBox7.Text, comboBox1.Text, dt_prod_date.Text,cb_process.Text);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "Please select absent employee");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Skill_Details_Excess(cb_allocation_status.Text, comboBox4.Text, dateTimePicker1.Text, cb_punching.Text);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Skill_Details_MPAC(comboBox7.Text, comboBox1.Text, dt_prod_date.Text, cb_process.Text);//1

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            GetMPACReport();
        }

        public void GetMPACReport()
        {
            string Start_Date = dateTimePicker2.Text;
            string End_Date = dateTimePicker3.Text;
            string ProdPlant = comboBox6.Text;
            string Status = comboBox5.Text;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Start_Date", Start_Date);
            data.Add("End_Date", End_Date);
            data.Add("ProdPlant", ProdPlant);
            data.Add("Status", Status);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",//类库名
                                          "SJ_TSMAPI.Production_Adjustment",//类名
                                          "GetMPACReport",//方法名
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
            if (Convert.ToBoolean(ret.IsSuccess))
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                if (dt.Rows.Count > 0)
                {
                    dataGridView3.DataSource = null;
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

        private void DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (string.IsNullOrEmpty(cb_select_type.Text))
            {
                dataGridView4.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dataGridView4.EndEdit();
                MessageHelper.ShowErr(this, "Please select Selection_Type");
                dataGridView4.Rows[e.RowIndex].Cells["Select2"].Value = false;
                dataGridView4.RefreshEdit();
                return;
            }
            if (cb_select_type.Text == "Allocate")
            {
                if (dataGridView4.Columns[e.ColumnIndex].Name == "Select2")
                {
                    if (e.RowIndex < 0)
                        return;
                    string clickedSkill = dataGridView4.Rows[e.RowIndex].Cells["WORKING_SKILL"]
                                                .Value?.ToString();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Working_Skill", clickedSkill);
                    data.Add("Type", "MPAC");
                    data.Add("Prod_Date", dateTimePicker1.Text);
                    string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TSMAPI",
                                        "SJ_TSMAPI.Production_Adjustment",
                                        "Skill_Details2",
                                        Program.Client.UserToken,
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (Convert.ToBoolean(ret.IsSuccess))
                    {
                        Dictionary<string, object> dic =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        DataTable dt =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            MPAC_DATA mpacForm = new MPAC_DATA(dt);
                            mpacForm.ShowDialog();
                            DataTable SelectedData = mpacForm.Selected_Data;

                            if (SelectedData != null && SelectedData.Rows.Count > 0)
                            {
                                string newBarcode = SelectedData.Rows[0]["EMP_NO"].ToString();
                                bool exists = false;
                                foreach (DataGridViewRow row in dataGridView4.Rows)
                                {
                                    if (row.Cells["SUPPORT_EMP_NO"].Value != null &&
                                        row.Cells["SUPPORT_EMP_NO"].Value.ToString() == newBarcode)
                                    {
                                        exists = true;
                                        break;
                                    }
                                }

                                if (exists)
                                {
                                    dataGridView4.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                    dataGridView4.EndEdit();
                                    MessageBox.Show("This employee is already selected!");
                                    if (string.IsNullOrEmpty(dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value.ToString()))
                                    {
                                        dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = "";
                                        dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = "";
                                        dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = "";
                                    }
                                    dataGridView4.Rows[e.RowIndex].Cells["Select2"].Value = false;
                                    dataGridView4.RefreshEdit();
                                }
                                else
                                {

                                    dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = SelectedData.Rows[0]["EMP_NO"].ToString();
                                    dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = SelectedData.Rows[0]["EMP_NAME"].ToString();
                                    dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = SelectedData.Rows[0]["DEPARTMENT"].ToString();
                                }
                            }
                            else
                            {
                                dataGridView4.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                dataGridView4.EndEdit();
                                if (string.IsNullOrEmpty(dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value.ToString()))
                                {
                                    dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = "";
                                    dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = "";
                                    dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = "";
                                }
                                dataGridView4.Rows[e.RowIndex].Cells["Select2"].Value = false;
                                dataGridView4.RefreshEdit();
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Data Found");
                        }
                    }
                    else
                    {
                        MessageBox.Show(ret.ErrMsg);
                    }
                }
                
            }
            else if (cb_select_type.Text == "De_Allocate")
            {
                dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NO"].Value = "";
                dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_NAME"].Value = "";
                dataGridView4.Rows[e.RowIndex].Cells["SUPPORT_EMP_DEPT"].Value = "";
            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (dataGridView4.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView4.Columns)
                    dt.Columns.Add(column.Name);


                foreach (DataGridViewRow row in dataGridView4.Rows)
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
                data.Add("Select_Type", cb_select_type.Text);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                              Program.Client.APIURL,
                                              "SJ_TSMAPI",
                                              "SJ_TSMAPI.Production_Adjustment",
                                              "SaveAbsentEmployee2",
                                              Program.Client.UserToken,
                                              Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (Convert.ToBoolean(ret.IsSuccess))
                {
                    MessageBox.Show("Updated Sucessfully");
                    Skill_Details_Excess(cb_allocation_status.Text, comboBox4.Text, dateTimePicker1.Text, cb_punching.Text);
                }
                else
                {
                    MessageHelper.ShowErr(this, ret.ErrMsg);
                    Skill_Details_Excess(cb_allocation_status.Text, comboBox4.Text, dateTimePicker1.Text, cb_punching.Text);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "Please select absent employee");
            }
        }

        private void Regular_allowcatation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Regular_allowcatation.SelectedIndex == 1)
            {
                GetUserPlant("Today");
            }
        }

        private void Cb_select_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Skill_Details_Excess(cb_allocation_status.Text, comboBox4.Text, dateTimePicker1.Text, cb_punching.Text);
        }

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
