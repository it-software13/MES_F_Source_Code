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
    public partial class PowerSavings : Form
    {
        // private KaizenForm _mainForm;
        public string Result { get; private set; }
        public PowerSavings(string kaizen_no)

        {
            InitializeComponent();
            KN.Text = kaizen_no;
            // _mainForm = _kmainForm;
        }
        public void cleardata()
        {
            KN.Text = "";
            AB.Text = "";
            AA.Text = "";
            BB.Text = "";
            BA.Text = "";
            CB.Text = "";
            CA.Text = "";
            DB.Text = "";
            DA.Text = "";
            EB.Text = "";
            EA.Text = "";
            FB.Text = "";
            FA.Text = "";
            GB.Text = "";
            GA.Text = "";
            HB.Text = "";
            HA.Text = "";
            KB.Text = "";
            KA.Text = "";
            LC.Text = "";

            //this.Close();
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

            double value11 = double.TryParse(EB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(CB.Text))
            {

                FB.Text = (value12 / value11).ToString("0.0000");
            }
            if (EB.Text == "" || CB.Text == "")
            {
                FB.Text = "";
            }




        }

        private void D_TextChanged(object sender, EventArgs e)
        {

        }

        private void F_TextChanged(object sender, EventArgs e)
        {

            double value11 = double.TryParse(FB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(AB.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(DB.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(FB.Text) && !string.IsNullOrEmpty(AB.Text) && !string.IsNullOrEmpty(DB.Text))
            {
                GB.Text = ((value11 * value12) * value13).ToString("0.0000");
            }
            if (FB.Text == "" || AB.Text == "" || DB.Text == "")
            {
                GB.Text = "";
            }
        }

        private void H_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(HB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(GB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(HB.Text) && !string.IsNullOrEmpty(GB.Text))
            {
                KB.Text = (value12 * value11).ToString();
            }

            if (Double.IsInfinity(Convert.ToDouble(KB.Text)) || Double.IsNaN(Convert.ToDouble(KB.Text)))
            {
                KB.Text = "0";
            }
            if (HB.Text == "" || GB.Text == "")
            {
                KB.Text = "";
            }

        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void C_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(CB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(CB.Text) && !string.IsNullOrEmpty(EB.Text))
            {

                FB.Text = (value11 / value12).ToString("0.0000");
            }
            if (Double.IsInfinity(Convert.ToDouble(FB.Text)) || Double.IsNaN(Convert.ToDouble(FB.Text)))
            {
                FB.Text = "0";
            }
            if (CB.Text == "" || EB.Text == "")
            {
                FB.Text = "";
            }

        }

        private void B_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(BB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(DB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(BB.Text) && !string.IsNullOrEmpty(DB.Text))
            {

                EB.Text = ((3600 / value11) * value12).ToString();
            }
            if (Double.IsInfinity(Convert.ToDouble(EB.Text)) || Double.IsNaN(Convert.ToDouble(EB.Text)))
            {
                KA.Text = "0";
            }

            if (Double.IsInfinity(Convert.ToDouble(EB.Text)) || Double.IsNaN(Convert.ToDouble(EB.Text)))
            {
                EB.Text = "0";
            }
            if (BB.Text == "")
            {
                EB.Text = "";
            }
        }

        private void G_TextChanged(object sender, EventArgs e)
        {

        }

        private void Total_Cost_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(KB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(KA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(KB.Text) && !string.IsNullOrEmpty(KA.Text))
            {
                LC.Text = (value11 - value12).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(KA.Text) && !string.IsNullOrEmpty(KB.Text))
            {
                LC.Text = (value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(KB.Text) && !string.IsNullOrEmpty(KA.Text))
            {
                LC.Text = (-value11).ToString("0.0000");
            }
            if (KB.Text == "" && KA.Text == "")
            {
                LC.Text = "";
            }
        }

        private void PowerSavings_Load(object sender, EventArgs e)
        {

        }

        private void C_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void H_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void Total_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(AA.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(DA.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(FA.Text) && !string.IsNullOrEmpty(AA.Text) && !string.IsNullOrEmpty(DA.Text))
            {
                GA.Text = ((value11 * value12) * value13).ToString("0.0000");
            }
            if (Double.IsInfinity(Convert.ToDouble(GA.Text)) || Double.IsNaN(Convert.ToDouble(GA.Text)))
            {
                GA.Text = "0";
            }
            if (FA.Text == "" || AA.Text == "" || DA.Text == "")
            {
                GA.Text = "";
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(DA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BA.Text, out var result12) ? result12 : 0;

            if (!string.IsNullOrEmpty(DA.Text) && !string.IsNullOrEmpty(BA.Text))
            {
                EA.Text = ((3600 / value12) * value11).ToString();
            }

            if (Double.IsInfinity(Convert.ToDouble(EA.Text)) || Double.IsNaN(Convert.ToDouble(EA.Text)))
            {
                EA.Text = "0";
            }

            if (DA.Text == "")
            {
                EA.Text = "";
            }

            double value13 = double.TryParse(FA.Text, out var result13) ? result13 : 0;
            double value14 = double.TryParse(AA.Text, out var result14) ? result14 : 0;

            if (!string.IsNullOrEmpty(FA.Text) && !string.IsNullOrEmpty(AA.Text) && !string.IsNullOrEmpty(DA.Text))
            {
                GA.Text = ((value13 * value14) * value11).ToString("0.0000");
            }

            if (string.IsNullOrEmpty(GA.Text) || Double.IsInfinity(Convert.ToDouble(GA.Text)) || Double.IsNaN(Convert.ToDouble(GA.Text)))
            {
                GA.Text = "0";
            }

            if (FA.Text == "" || AA.Text == "" || DA.Text == "")
            {
                GA.Text = "";
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void A_TextChanged(object sender, EventArgs e)
        {

        }

        private void BA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(BA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(DA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(BA.Text) && !string.IsNullOrEmpty(DA.Text))
            {

                EA.Text = ((3600 / value11) * value12).ToString();
            }
            if (Double.IsInfinity(Convert.ToDouble(EA.Text)) || Double.IsNaN(Convert.ToDouble(EA.Text)))
            {
                EA.Text = "0";
            }
            if (DA.Text == "")
            {
                EA.Text = "";
            }
        }

        private void CA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(CA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(CA.Text) && !string.IsNullOrEmpty(EA.Text))
            {

                FA.Text = (value11 / value12).ToString("0.0000");
            }
            if (Double.IsInfinity(Convert.ToDouble(FA.Text)) || Double.IsNaN(Convert.ToDouble(FA.Text)))
            {
                FA.Text = "0";
            }
            if (CA.Text == "" || EA.Text == "")
            {
                FA.Text = "";
            }
        }

        private void EA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(CA.Text))
            {

                FA.Text = (value12 / value11).ToString("0.0000");
            }
            if (Double.IsInfinity(Convert.ToDouble(FA.Text)) || Double.IsNaN(Convert.ToDouble(FA.Text)))
            {
                FA.Text = "0";
            }
            if (EA.Text == "" || CA.Text == "")
            {
                FA.Text = "";
            }
        }

        private void AA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(AA.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(DA.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(FA.Text) && !string.IsNullOrEmpty(AA.Text) && !string.IsNullOrEmpty(DA.Text))
            {
                GA.Text = ((value11 * value12) * value13).ToString("0.0000");
            }

            if (Double.IsInfinity(Convert.ToDouble(GA.Text)) || Double.IsNaN(Convert.ToDouble(GA.Text)))
            {
                GA.Text = "0";
            }
            if (FA.Text == "" || AA.Text == "" || DA.Text == "")
            {
                GA.Text = "";
            }
        }

        private void GA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(GA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(HA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(GA.Text) && !string.IsNullOrEmpty(HA.Text))
            {

                KA.Text = (value11 * value12).ToString("0.0000");
            }

            if (Double.IsInfinity(Convert.ToDouble(KA.Text)) || Double.IsNaN(Convert.ToDouble(KA.Text)))
            {
                KA.Text = "0";
            }
            if (GA.Text == "" || HA.Text == "")
            {
                KA.Text = "";
            }
        }

        private void HA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(HA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(GA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(HA.Text) && !string.IsNullOrEmpty(GA.Text))
            {
                KA.Text = (value12 * value11).ToString();
            }

            if (Double.IsInfinity(Convert.ToDouble(KA.Text)) || Double.IsNaN(Convert.ToDouble(KA.Text)))
            {
                KA.Text = "0";
            }
            if (HA.Text == "" || GA.Text == "")
            {
                KA.Text = "";
            }

        }

        private void KA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(KA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(KB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(KA.Text) && !string.IsNullOrEmpty(KB.Text))
            {
                LC.Text = (value12 - value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(KB.Text) && !string.IsNullOrEmpty(KA.Text))
            {
                LC.Text = (-value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(KA.Text) && !string.IsNullOrEmpty(KB.Text))
            {
                LC.Text = (value11).ToString("0.0000");

            }
            if (KA.Text == "" && KB.Text == "")
            {
                LC.Text = "";
            }

        }

        //private void SendDataToMainForm(string value)
        //{
        //    // Let's assume you want to send the text from textbox1 to the MainForm's textbox28
        //    _mainForm.SetTextBoxValue(value);
        //}
        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Power Consumption for machine per hour Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(AA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Power Consumption for machine per hour  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter CT  Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter CT  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(CB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Order Qty Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Order Qty  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(DB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Working Hrs Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(DA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Working Hrs  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(HB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter 1 KW Price Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(HA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter 1 KW Price  After Kaizen");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Kaizen_Number", KN.Text);
            p.Add("Power_Consumption_B", AB.Text);
            p.Add("Power_Consumption_A", AA.Text);
            p.Add("CT_B", BB.Text);
            p.Add("CT_A", BA.Text);
            p.Add("Order_Qty_B", CB.Text);
            p.Add("Order_Qty_A", CA.Text);
            p.Add("Work_Hrs_B", DB.Text);
            p.Add("Work_Hrs_A", DA.Text);
            p.Add("W_Total_Output_B", EB.Text);
            p.Add("W_Total_Output_A", EA.Text);
            p.Add("Required_Machines_B", FB.Text);
            p.Add("Required_Machines_A", FA.Text);
            p.Add("Total_Power_B", GB.Text);
            p.Add("Total_Power_A", GA.Text);
            p.Add("OneKW_Price_B", HB.Text);
            p.Add("OneKW_Price_A", HA.Text);
            p.Add("Total_Cost_B", KB.Text);
            p.Add("Total_Cost_A", KA.Text);
            p.Add("Overall_Savings", LC.Text);
            //string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_Power_Savings_Data", Program.client.UserToken, JsonConvert.SerializeObject(p));
            //if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))

            //{
            //    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //    if (json == "Failed")
            //    {
            //        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data");
            //    }
            //    else
            //    {
            //       // SendDataToMainForm();
            //        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data inserted Successfully");

            //        cleardata();


            //    }

            //}
            // Send the request to the API and get the response string 'ret'
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_Power_Savings_Data", Program.client.UserToken, JsonConvert.SerializeObject(p));

            // Deserialize the response into a Dictionary<string, object>
            var responseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            // Check if the response indicates success
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
                    // If RetData is not "Failed", pass the value to SendDataToMainForm
                    // SendDataToMainForm(value);
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
                                    "Get_Power_Savings_Data",
                                    Program.client.UserToken,
                                    JsonConvert.SerializeObject(requestData)
                                    );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);
                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {
                        AB.Text = dataList[0]["POWER_CONSUMPTION_B"].ToString();
                        AA.Text = dataList[0]["POWER_CONSUMPTION_A"].ToString();
                        BB.Text = dataList[0]["CT_B"].ToString();
                        BA.Text = dataList[0]["CT_A"].ToString();
                        CB.Text = dataList[0]["ORDER_QTY_B"].ToString();
                        CA.Text = dataList[0]["ORDER_QTY_A"].ToString();
                        DB.Text = dataList[0]["WORK_HRS_B"].ToString();
                        DA.Text = dataList[0]["WORK_HRS_A"].ToString();
                        EB.Text = dataList[0]["W_TOTAL_OUTPUT_PER_HOUR_B"].ToString();
                        EA.Text = dataList[0]["W_TOTAL_OUTPUT_PER_HOUR_A"].ToString();
                        FB.Text = dataList[0]["REQUIRED_MACHINES_B"].ToString();
                        FA.Text = dataList[0]["REQUIRED_MACHINES_A"].ToString();
                        GB.Text = dataList[0]["TOTAL_POWER_B"].ToString();
                        GA.Text = dataList[0]["TOTAL_POWER_A"].ToString();
                        HB.Text = dataList[0]["ONE_KW_PRICE_B"].ToString();
                        HA.Text = dataList[0]["ONE_KW_PRICE_A"].ToString();
                        KB.Text = dataList[0]["TOTAL_COST_B"].ToString();
                        KA.Text = dataList[0]["TOTAL_COST_A"].ToString();
                        LC.Text = dataList[0]["OVERALL_SAVINGS"].ToString();
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
            

        private void Button3_Click_1(object sender, EventArgs e)
        {
            cleardata();
        }

        private void Button4_Click(object sender, EventArgs e)
        {

            if (!ValidateRequiredFields())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Power Savings.frx");

            try
            {
                DataTable dt = CreatePowerDataTable();
                DataRow row = dt.NewRow();
                FillPowerRow(row);
                dt.Rows.Add(row);

                Power_preview previewForm = new Power_preview(dt, path);
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
        AB, AA, BB, BA, CB, CA, DB, DA,
        EB, EA, FB, FA, GB, GA, HB, HA,
        KB, KA, LC
    };
            return requiredFields.All(tb => !string.IsNullOrWhiteSpace(tb.Text.Trim()));
        }


        private DataTable CreatePowerDataTable()
        {
            DataTable dt = new DataTable();

            string[] columns = {
    "KAIZEN_NUMBER",
    "POWER_CONSUMPTION_B", "POWER_CONSUMPTION_A",
    "CT_B", "CT_A",
    "ORDER_QTY_B", "ORDER_QTY_A",
    "WORK_HRS_B", "WORK_HRS_A",
    "W_TOTAL_OUTPUT_PER_HOUR_B", "W_TOTAL_OUTPUT_PER_HOUR_A",
    "REQUIRED_MACHINES_B", "REQUIRED_MACHINES_A",
    "TOTAL_POWER_B", "TOTAL_POWER_A",
    "ONE_KW_PRICE_B", "ONE_KW_PRICE_A",
    "TOTAL_COST_B", "TOTAL_COST_A",
    "OVERALL_SAVINGS"
};

            foreach (var col in columns)
                dt.Columns.Add(col, typeof(string));

            return dt;
        }


        private void FillPowerRow(DataRow row)
        {
            row["KAIZEN_NUMBER"] = KN.Text.Trim();
            row["POWER_CONSUMPTION_B"] = AB.Text.Trim();
            row["POWER_CONSUMPTION_A"] = AA.Text.Trim();
            row["CT_B"] = BB.Text.Trim();
            row["CT_A"] = BA.Text.Trim();
            row["ORDER_QTY_B"] = CB.Text.Trim();
            row["ORDER_QTY_A"] = CA.Text.Trim();
            row["WORK_HRS_B"] = DB.Text.Trim();
            row["WORK_HRS_A"] = DA.Text.Trim();
            row["W_TOTAL_OUTPUT_PER_HOUR_B"] = EB.Text.Trim();
            row["W_TOTAL_OUTPUT_PER_HOUR_A"] = EA.Text.Trim();
            row["REQUIRED_MACHINES_B"] = FB.Text.Trim();
            row["REQUIRED_MACHINES_A"] = FA.Text.Trim();
            row["TOTAL_POWER_B"] = GB.Text.Trim();
            row["TOTAL_POWER_A"] = GA.Text.Trim();
            row["ONE_KW_PRICE_B"] = HB.Text.Trim();
            row["ONE_KW_PRICE_A"] = HA.Text.Trim();
            row["TOTAL_COST_B"] = KB.Text.Trim();
            row["TOTAL_COST_A"] = KA.Text.Trim();
            row["OVERALL_SAVINGS"] = LC.Text.Trim();
        }






    }
}
