using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenForm
{
    public partial class Material_Savings : Form
    {
        public string Result { get; private set; }
        public Material_Savings(string kaizen_no)
        {
            InitializeComponent();
            KN.Text = kaizen_no;
        }

        public void cleardata()
        {
            KN.Text = "";
            AB.Text = "";
            AA.Text = "";
            BC.Text = "";
            CB.Text = "";
            CA.Text = "";
            DC.Text = "";
            EB.Text = "";
            EA.Text = "";
            FC.Text = "";

            //this.Close();
        }

        private void AB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(BC.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(AB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(BC.Text) && !string.IsNullOrEmpty(AB.Text))
            {

                CB.Text = (value11 / value12).ToString("0.0000");
            }
            if (BC.Text == "" || AB.Text == "")
            {
                CB.Text = "";
            }


        }

        private void AA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(BC.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(AA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(BC.Text) && !string.IsNullOrEmpty(AA.Text))
            {

                CA.Text = (value11 / value12).ToString("0.0000");
            }
            if (BC.Text == "" || AA.Text == "")
            {
                CA.Text = "";
            }

        }

        private void BC_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(AB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BC.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(AB.Text) && !string.IsNullOrEmpty(BC.Text))
            {

                CB.Text = (value12 / value11).ToString("0.0000");
            }
            if (AB.Text == "" || BC.Text == "")
            {
                CB.Text = "";
            }

            double value13 = double.TryParse(AA.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(BC.Text) && !string.IsNullOrEmpty(AA.Text))
            {

                CA.Text = (value12 / value13).ToString("0.0000");
            }
            if (BC.Text == "" || AA.Text == "")
            {
                CA.Text = "";
            }
            if (EB.Text == "" && EA.Text == "")
            {
                FC.Text = "";
            }


        }

        private void DC_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(CB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CA.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(DC.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(CB.Text) && !string.IsNullOrEmpty(DC.Text))
            {

                EB.Text = (value13 * value11).ToString("0.0000");
            }
            if (CB.Text == "" || DC.Text == "")
            {
                EB.Text = "";
            }
            if (!string.IsNullOrEmpty(CA.Text) && !string.IsNullOrEmpty(DC.Text))
            {

                EA.Text = (value13 * value12).ToString("0.0000");
            }
            if (CA.Text == "" || DC.Text == "")
            {
                EA.Text = "";
            }
        }

        private void EB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(EA.Text))
            {
                FC.Text = (value11 - value12).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(EB.Text))
            {
                FC.Text = (value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(EA.Text))
            {
                FC.Text = (-value11).ToString("0.0000");
            }
            if (EB.Text == "" && EA.Text == "")
            {
                FC.Text = "";
            }

        }

        private void EA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(EB.Text))
            {
                FC.Text = (value12 - value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(EA.Text))
            {
                FC.Text = (-value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(EB.Text))

            {
                FC.Text = (value11).ToString("0.0000");
            }
            if (EA.Text == "" && EB.Text == "")
            {
                FC.Text = "";
            }
        }

        private void CB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(EA.Text))
            {
                FC.Text = (value11 - value12).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(EB.Text))
            {
                FC.Text = (value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(EA.Text))
            {
                FC.Text = (-value11).ToString("0.0000");
            }
            if (EB.Text == "" && EA.Text == "")
            {
                FC.Text = "";
            }
        }

        private void CA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(EB.Text))
            {
                FC.Text = (value12 - value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(EA.Text))
            {
                FC.Text = (-value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(EB.Text))
            {
                FC.Text = (value11).ToString("0.0000");
            }
            if (EA.Text == "" && EB.Text == "")
            {
                FC.Text = "";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter 1 Yard Pairs Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(AA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter 1 Yard Pairs  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BC.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Order Qty");
                return;
            }

            if (string.IsNullOrEmpty(DC.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter 1 Yard Cost");
                return;
            }

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Kaizen_Number", KN.Text);
            p.Add("One_Yard_cost_B", AB.Text);
            p.Add("One_Yard_cost_A", AA.Text);
            p.Add("Order_Qty", BC.Text);
            p.Add("Required_yards_B", CB.Text);
            p.Add("Required_yards_A", CA.Text);
            p.Add("One_Yard_Cost", DC.Text);
            p.Add("Required_Yard_Cost_B", EB.Text);
            p.Add("Required_Yard_Cost_A", EA.Text);
            p.Add("Overall_Yard_Saving_Cost", FC.Text);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_Material_Savings_Data", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var responseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(responseDict["IsSuccess"]))
            {
                // Extract the RetData from the response
                string value = responseDict["RetData"].ToString();

                if (value == "Failed")
                {
                    // If RetData is "Failed", show an error message
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data");
                }
                else
                {

                    Result = value;
                    // Show success message
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data inserted Successfully");
                    //this.Hide();
                    // Clear any data after successful insertion
                    cleardata();
                    this.Close();
                }

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            cleardata();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            try
            {
                string kaizenNum = KN.Text.Trim();

                if (string.IsNullOrWhiteSpace(kaizenNum))
                {
                    MessageBox.Show("Enter a valid Kaizen Number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<string, string> requestData = new Dictionary<string, string>
                {
                    { "kaizen_num", kaizenNum }
                };

                string response = WebAPIHelper.Post(
                                    Program.client.APIURL,
                                    "KZ_RTDMAPI",
                                    "KZ_RTDMAPI.Controllers.Kaizenserver",
                                    "Get_material_Data",
                                    Program.client.UserToken,
                                    JsonConvert.SerializeObject(requestData)
                                    );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);
                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {
                        AB.Text = dataList[0]["ONE_YARD_PAIRS_B"].ToString();
                        AA.Text = dataList[0]["ONE_YARD_PAIRS_A"].ToString();
                        BC.Text = dataList[0]["ORDER_QTY"].ToString();
                        CB.Text = dataList[0]["REQUIRED_YARDS_B"].ToString();
                        CA.Text = dataList[0]["REQUIRED_YARDS_A"].ToString();
                        DC.Text = dataList[0]["ONE_YARD_COST"].ToString();
                        EB.Text = dataList[0]["REQUIRED_YARD_COST_B"].ToString();
                        EA.Text = dataList[0]["REQUIRED_YARD_COST_A"].ToString();
                        FC.Text = dataList[0]["OVERALL_YARD_SAVING_COST"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("No record found for the entered Kaizen Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error: " + result.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredFields())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Material Savings.frx");

            try
            {
                DataTable dt = CreatePowerDataTable();
                DataRow row = dt.NewRow();
                FillPowerRow(row);
                dt.Rows.Add(row);

                Material_Preview previewForm = new Material_Preview(dt, path);
                previewForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}. Object not found: {ex.TargetSite?.Name}");
            }

        }

        private bool ValidateRequiredFields()
        {
            List<TextBox> requiredFields = new List<TextBox>
    {
        AB, AA, BC, CB, CA, DC, EB, EA, FC
    };

            return requiredFields.All(tb => !string.IsNullOrWhiteSpace(tb.Text.Trim()));
        }



        private DataTable CreatePowerDataTable()
        {
            DataTable dt = new DataTable();

            string[] columns = {
        "KAIZEN_NUMBER",
        "ONE_YARD_PAIRS_B", "ONE_YARD_PAIRS_A",
        "ORDER_QTY",
        "REQUIRED_YARDS_B", "REQUIRED_YARDS_A",
        "ONE_YARD_COST",
        "REQUIRED_YARD_COST_B", "REQUIRED_YARD_COST_A",
        "OVERALL_YARD_SAVING_COST"
    };

            foreach (var col in columns)
                dt.Columns.Add(col, typeof(string));

            return dt;
        }



        private void FillPowerRow(DataRow row)
        {
            row["KAIZEN_NUMBER"] = KN.Text.Trim();
            row["ONE_YARD_PAIRS_B"] = AB.Text.Trim();
            row["ONE_YARD_PAIRS_A"] = AA.Text.Trim();
            row["ORDER_QTY"] = BC.Text.Trim();
            row["REQUIRED_YARDS_B"] = CB.Text.Trim();
            row["REQUIRED_YARDS_A"] = CA.Text.Trim();
            row["ONE_YARD_COST"] = DC.Text.Trim();
            row["REQUIRED_YARD_COST_B"] = EB.Text.Trim();
            row["REQUIRED_YARD_COST_A"] = EA.Text.Trim();
            row["OVERALL_YARD_SAVING_COST"] = FC.Text.Trim();
        }



    }
}
