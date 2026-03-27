using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using MaterialSkin;
using MaterialSkin.Controls;
using NewExportExcels;
using SJeMES_BDM;


namespace SJeMES_TQC
{
    public partial class Manual_RFT : MaterialForm
    {
        public AutoCompleteStringCollection Autodata { get; private set; }

        public Manual_RFT()
        {
            InitializeComponent();
        }

        private void TQC_Data_Edit_Load(object sender, EventArgs e)
        {
            Update.Visible = false;
            Edit.Visible = true;
            textBox2.KeyPress += NumericTextBox_KeyPress;
            textBox3.KeyPress += NumericTextBox_KeyPress;
        }

        public void LoadProd_Line(string RFT_Type)
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBoxEx1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxEx1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            kk.Add("RFT_Type", RFT_Type);
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TQCAPI",
                                            "SJ_TQCAPI.TQC_Task",
                                            "Get_Prod_line_For_Manual_RFT",
           Program.Client.UserToken, JsonConvert.SerializeObject(kk));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count <= 0)
            {

            }
            else
            {
                autocompleteMenu1.Items = null;
                autocompleteMenu1.MaximumSize = new Size(250, 350);
                var columnWidth = new[] { 50, 200 };
                int n = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["department_code"].ToString() }, dt.Rows[i]["department_code"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                    n++;
                }
            }
        }

        private void CalculateRFT()
        {
            string inspectionQty = textBox2.Text;
            string passQty = textBox3.Text;
            string rft = textBox4.Text;

            if (string.IsNullOrWhiteSpace(inspectionQty) || string.IsNullOrWhiteSpace(passQty))
            {
                textBox4.Clear();
                return;
            }

            if (int.TryParse(inspectionQty, out int inspecQty) && int.TryParse(passQty, out int total_PassQty))
            {
                if (inspecQty == 0)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Inspection Quantity cannot be zero.");
                    //MessageBox.Show("Inspection Quantity cannot be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                double rftValue = (double)total_PassQty / inspecQty * 100;
                textBox4.Text = rftValue.ToString("0.00");
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter valid numbers.");
                //MessageBox.Show("Please Enter valid numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Focus();
        }


        private void Fetch_RFT(string RFT_Type)
        {
            try
            {

                string selectedDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                Dictionary<string, string> requestData = new Dictionary<string, string>
                {
                    {"prod_date", selectedDate},
                    {"RFT_Type", RFT_Type}
                };

                string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_TQCAPI",
                                                "SJ_TQCAPI.TQC_Task",
                                                "Fetch_RFT",
                                                Program.Client.UserToken,
                                                Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
                                                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(response);
                if (ret.IsSuccess)
                {
                    //Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    if(dt.Rows.Count>0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                        dataGridView1.DataSource = null;
                    }
                    
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to fetch data. Error: " + ret.ErrMsg);
                    //MessageBox.Show("Failed to fetch data. Error: " + ret.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Error Fetching Data: " + ex.Message);
               // MessageBox.Show("Error Fetching Data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Fetch_RFT_Data(string RFT_type)
        {
            try
            {
                string fromDate = this.fromDate.Value.ToString("yyyy-MM-dd");
                string toDate = this.toDate.Value.ToString("yyyy-MM-dd");
                string prodLine = textBoxEx1.Text.Trim();

                
                if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select both From Date and To Date.");
                    //MessageBox.Show("Please select both From Date and To Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<string, string> requestData = new Dictionary<string, string>
                {   
                    { "RFT_type", RFT_type },
                    { "FROM_DATE", fromDate },
                    { "TO_DATE", toDate }
                };

                
                if (!string.IsNullOrEmpty(prodLine))
                {
                    requestData.Add("PROD_LINE", prodLine);
                }
                string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                Program.Client.APIURL,
                                                "SJ_TQCAPI",
                                                "SJ_TQCAPI.TQC_Task",
                                                "Fetch_RFT_Data",
                                                Program.Client.UserToken,
                                                Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
                                                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(response);
                if (ret.IsSuccess)
                {
                   // Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dt;
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                        dataGridView2.DataSource = null;
                    }
                    
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to fetch data. Error: " + ret.ErrMsg);
                    //MessageBox.Show("Failed to fetch data. Error: " + ret.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Error Fetching Data: " + ex.Message);
               // MessageBox.Show("Error Fetching Data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select RFT Type");
                //MessageBox.Show("Please select RFT Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Fetch_RFT_Data(comboBox2.Text);
            }
               
        }
        

        private void Export_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "ManDay_Hours_Data.xls";
                ExportExcels.Export(a, dataGridView2);
                // SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox4.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Either RFT or Production Line or RFT Type is Empty");
                    //MessageBox.Show("Either RFT or Production Line or RFT Type is Empty", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(textBox2.Text, out int inspecQty) || !int.TryParse(textBox3.Text, out int totalPassQty))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter valid numbers for Inspection and Pass Quantity.");
                    //MessageBox.Show("Enter valid numbers for Inspection and Pass Quantity.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (totalPassQty > inspecQty)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Pass Quantity cannot be greater than Inspection Quantity.");
                    //MessageBox.Show("Pass Quantity cannot be greater than Inspection Quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Focus();
                    return;
                }

                Dictionary<string, object> requestData = new Dictionary<string, object>
                {
                    {"prod_date", dateTimePicker1.Value.ToString("yyyy-MM-dd") },
                    {"prod_line", textBox1.Text.Trim() },
                    {"rft_type", comboBox1.Text.Trim() },
                    {"inspection_qty", Convert.ToInt32(textBox2.Text) },
                    {"total_pass_qty", Convert.ToInt32(textBox3.Text) },
                    {"rft", Convert.ToDouble(textBox4.Text) }
                };

                string response = WebAPIHelper.Post(
                Program.Client.APIURL,
                        "SJ_TQCAPI",
                        "SJ_TQCAPI.TQC_Task",
                        "Update_Prod_RFT",
                        Program.Client.UserToken,
                        JsonConvert.SerializeObject(requestData)
                        );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);

                if (result.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data Updated Successfully!");
                    MessageBox.Show("Data Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Fetch_RFT(comboBox1.Text);
                    Update.Visible = false;
                    submit.Visible = true;
                    Edit.Visible = true;
                    dateTimePicker1.Enabled = true;
                    textBox1.Enabled = true;
                    comboBox1.Enabled = true;
                    ClearFields();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to Update Data. Error: " + result.ErrMsg);
                    //MessageBox.Show("Failed to Update Data. Error: " + result.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Error Updating Data: " + ex.Message);
                //MessageBox.Show("Error Updating Data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            LoadProd_Line(comboBox1.Text);
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            CalculateRFT();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int inspectQty) && int.TryParse(textBox3.Text, out int passQty))
            {
                if (passQty > inspectQty)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Pass Quantity cannot be greater than Inspection Quantity.");
                    //MessageBox.Show("Pass Quantity cannot be greater than Inspection Quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    textBox3.Clear();
                    textBox4.Clear();
                }
                else
                {
                    CalculateRFT();
                }
            }
        }

        private void submit_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(textBox1.Text)|| string.IsNullOrEmpty(textBox4.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Either RFT or Production Line or RFT Type is Empty");
               // MessageBox.Show("Either RFT or Production Line or RFT Type is Empty", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string prodDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string prodLine = textBox1.Text.Trim();
            string rft_type = comboBox1.Text.Trim();
            string inspectionQty = textBox2.Text.Trim();
            string passQty = textBox3.Text.Trim();
            string rft = textBox4.Text.Trim();
            Dictionary<string, object> requestData = new Dictionary<string, object>
            {
                {"prod_date", prodDate },
                {"prod_line", prodLine },
                {"rft_type", rft_type },
                {"inspection_qty", inspectionQty},
                {"total_pass_qty", passQty},
                {"rft", rft},
            };

            string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                                                        "SJ_TQCAPI",
                                                                        "SJ_TQCAPI.TQC_Task",
                                                                        "InsertAndFetchData",
                                                                        Program.Client.UserToken,
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(requestData));
            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);

            if (result.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data Inserted Successfully!");
                //MessageBox.Show("Data Inserted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Fetch_RFT(comboBox1.Text);
                ClearFields();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Failed to Insert Data. Error: " + result.ErrMsg);
                //MessageBox.Show("Failed to Insert Data. Error: " + result.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Edit_Click_1(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(comboBox1.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select RFT Type.");
                    //MessageBox.Show("Please select RFT Type.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string prodDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string prodLine = textBox1.Text.Trim();
                string rft_type = comboBox1.Text.Trim();

                if (string.IsNullOrWhiteSpace(prodLine))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter a valid Production Line.");
                    //MessageBox.Show("Enter a valid Production Line.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<string, string> requestData = new Dictionary<string, string>
                {
                    { "rft_type", rft_type },
                    { "prod_date", prodDate },
                    { "prod_line", prodLine }
                };

                string response = WebAPIHelper.Post(
                                    Program.Client.APIURL,
                                    "SJ_TQCAPI",
                                    "SJ_TQCAPI.TQC_Task",
                                    "Get_RFT_Data_ForEdit",
                                    Program.Client.UserToken,
                                    JsonConvert.SerializeObject(requestData)
                                    );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);

                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {
                        textBox2.Text = dataList[0]["INSPECTION_QTY"].ToString();
                        textBox3.Text = dataList[0]["TOTAL_PASS_QTY"].ToString();
                        textBox4.Text = dataList[0]["RFT"].ToString();

                        Edit.Visible = false;
                        submit.Visible = false;
                        Update.Visible = true;
                        comboBox1.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        textBox1.Enabled = false;

                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No record found for the entered Prod Date and Prod Line.");
                    //MessageBox.Show("No record found for the entered Prod Date and Prod Line.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Error fetching data: " + ex.Message);
                //MessageBox.Show("Error fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select RFT Type");
                //MessageBox.Show("Please select RFT Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Fetch_RFT(comboBox1.Text);
            }
            
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "delete")
                {
                    string Prod_Date = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["prod_date"].Value).ToString("yyyy/MM/dd");
                    string Prod_Line = dataGridView1.Rows[e.RowIndex].Cells["prod_line"].Value.ToString();
                    string Created_Date = dataGridView1.Rows[e.RowIndex].Cells["createdate"].Value.ToString();
                    if (Created_Date != DateTime.Now.Date.ToString("yyyy-MM-dd"))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "You can't delete this data");
                        return;
                    }

                    DialogResult Dialog_result = MessageBox.Show($@"Are you sure you want to delete the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Dialog_result == DialogResult.Yes)
                    {

                        Dictionary<string, string> requestData = new Dictionary<string, string>
                {
                    { "rft_type", comboBox1.Text.Trim() },
                    { "prod_date", Prod_Date },
                    { "prod_line", Prod_Line }
                };

                        string response = WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TQCAPI",
                                            "SJ_TQCAPI.TQC_Task",
                                            "Delete_RFT_Data",
                                            Program.Client.UserToken,
                                            JsonConvert.SerializeObject(requestData)
                                            );

                        ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);
                        if (result.IsSuccess)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Deleted Successfully");
                            Fetch_RFT(comboBox1.Text);
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, result.ErrMsg);
                            Fetch_RFT(comboBox1.Text);
                        }

                    }
                }
            }
        }
    }
}
