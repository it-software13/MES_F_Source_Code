using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;

namespace SJeMES_SM
{
    public partial class Production_Request : Form
    {
        public Production_Request()
        {
            InitializeComponent();
            Get_Plants();
            Barcode.KeyPress += new KeyPressEventHandler(Barcode_KeyPress);
           // Barcode.KeyDown += new KeyEventHandler(Barcode_KeyDown);
        }

        public void Get_Plants()
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                                                "SJ_SMAPI.Production_Request",
                                                "Get_Plants",
                                                Program.Client.UserToken, JsonConvert.SerializeObject(kk));

            ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("Error: No data returned.");
            }
            else
            {
               
                foreach (DataRow row in dt.Rows)
                {
                    Plants_Combo.Items.Add(row["UDF05"].ToString());
                }
            }

        }

        private void Plants_Combo_SelectedValueChanged(object sender, EventArgs e)
        {
            // Clear any existing items in Lines_combo
            Lines_combo.Items.Clear();
            Lines_combo.Text = string.Empty;
            // Get the selected plant
            string selectedPlant = Plants_Combo.SelectedItem?.ToString();
            Plants_Combo.Text = selectedPlant;

            if (string.IsNullOrEmpty(selectedPlant))
            {
                MessageBox.Show("Please select a valid plant.");
                return;
            }

            DataTable dt = new DataTable();
            Dictionary<string, object> retData = new Dictionary<string, object>
                            {
                                { "Plant", selectedPlant }
                            };

            try
            {
                string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                                                    "SJ_SMAPI.Production_Request",
                                                    "Get_Production_Lines",
                                                    Program.Client.UserToken, JsonConvert.SerializeObject(retData));

                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Error: No data returned.");
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Lines_combo.Items.Add(row["Department_code"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}");
            }
        }



        private void Barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numeric input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the input
            }
        }

        private void Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Plants_Combo.Text == null)
                {
                    MessageBox.Show("Select The Plant");
                    return;
                }
                else if (Barcode.Text == null)
                {
                    MessageBox.Show("Enter Barcode");
                    return;
                }
                else
                {
                    DataTable dt = new DataTable();
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("Barcode", Barcode.Text);
                    retData.Add("Plant", Plants_Combo.SelectedItem?.ToString());
                    string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                                                         "SJ_SMAPI.Production_Request",
                                                         "Get_Emp_Deatils",
                                                         Program.Client.UserToken, JsonConvert.SerializeObject(retData));

                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                    Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());

                    if (dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("NO DATA FOUND");
                        Emp_name.Text = null;
                        Dept_Code.Text = null;
                        Dept_Name.Text = null;
                    }
                    else
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            // Assuming there is only one row of interest for emp_name and dept_name
                            Emp_name.Text = row["EMP_NAME"].ToString();
                            Dept_Name.Text = row["DEPARTMENT_NAME"].ToString();
                            Dept_Code.Text = row["DEPARTMENT"].ToString();
                            Emp_name.ReadOnly = true;
                            Dept_Code.ReadOnly = true;
                            Dept_Name.ReadOnly = true;
                        }
                    }
                    e.Handled = true; // Prevent beep sound on Enter key press
                }
            }
            
        }

    }
}