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
    public partial class Tape_Savings : Form
    {
        public string Result { get; private set; }
        public Tape_Savings(string kaizen_no)
        {
            InitializeComponent();
            KN.Text = kaizen_no;
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

            //this.Close();
        }
        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(DB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CB.Text, out var result12) ? result12 : 0;

            if (!string.IsNullOrWhiteSpace(DB.Text) && !string.IsNullOrWhiteSpace(CB.Text))
            {
                if (value12 != 0)
                {
                    EB.Text = (value11 / value12).ToString("0.0000");
                }
                else
                {
                    EB.Text = "0"; // or "" if you prefer
                }
            }
            else
            {
                EB.Text = "";
            }
            if (!double.TryParse(EB.Text, out var ebValue) || double.IsInfinity(ebValue) || double.IsNaN(ebValue))
            {
                EB.Text = "0";
            }

        }

        private void DA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(DA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CA.Text, out var result12) ? result12 : 0;

            if (!string.IsNullOrWhiteSpace(DA.Text) && !string.IsNullOrWhiteSpace(CA.Text))
            {
                if (value12 != 0)
                {
                    EA.Text = (value11 / value12).ToString("0.0000");
                }
                else
                {
                    EA.Text = "0"; // Or "" depending on your logic
                }
            }
            else
            {
                EA.Text = "";
            }
            if (!double.TryParse(EA.Text, out var eaValue) || double.IsInfinity(eaValue) || double.IsNaN(eaValue))
            {
                EA.Text = "0";
            }

        }

        private void CB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(DB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(DB.Text) && !string.IsNullOrEmpty(CB.Text))
            {

                EB.Text = (value11 / value12).ToString("0.0000");
            }
            if (DB.Text == "" || CB.Text == "")
            {
                EB.Text = "";
            }
        }

        private void CA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(CA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(DA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(CA.Text) && !string.IsNullOrEmpty(DA.Text))
            {

                EA.Text = (value12 / value11).ToString("0.0000");
            }
            if (CA.Text == "" || DA.Text == "")
            {
                EA.Text = "";


            }
        }

        private void FB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BB.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(EB.Text, out var result13) ? result13 : 0;

            if (!string.IsNullOrWhiteSpace(FB.Text) && !string.IsNullOrWhiteSpace(BB.Text) && !string.IsNullOrWhiteSpace(EB.Text))
            {
                if (value11 != 0 && value12 != 0 && value13 != 0)
                {
                    GB.Text = (value11 * value12 * value13).ToString("0.0000");
                }
                else
                {
                    GB.Text = "0";
                }
            }
            else
            {
                GB.Text = "";
            }
            if (!double.TryParse(GB.Text, out var gbValue) || double.IsInfinity(gbValue) || double.IsNaN(gbValue))
            {
                GB.Text = "0";
            }



        }

        private void BB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BB.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(EB.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(FB.Text) && !string.IsNullOrEmpty(BB.Text) && !string.IsNullOrEmpty(EB.Text))
            {
                GB.Text = (value11 * value12 * value13).ToString("0.0000");
            }
            if (FB.Text == "" || BB.Text == "" || EB.Text == "")
            {
                GB.Text = "";
            }
        }

        private void EB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BB.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(EB.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(FB.Text) && !string.IsNullOrEmpty(BB.Text) && !string.IsNullOrEmpty(EB.Text))
            {
                GB.Text = (value11 * value12 * value13).ToString("0.0000");
            }
            if (FB.Text == "" || BB.Text == "" || EB.Text == "")
            {
                GB.Text = "";
            }
        }

        private void FA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BA.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(EA.Text, out var result13) ? result13 : 0;

            if (!string.IsNullOrWhiteSpace(FA.Text) && !string.IsNullOrWhiteSpace(BA.Text) && !string.IsNullOrWhiteSpace(EA.Text))
            {
                if (value11 != 0 && value12 != 0 && value13 != 0)
                {
                    GA.Text = (value11 * value12 * value13).ToString("0.0000");
                }
                else
                {
                    GA.Text = "0";
                }
            }
            else
            {
                GA.Text = "";
            }

            if (!double.TryParse(GA.Text, out var gaValue) || double.IsInfinity(gaValue) || double.IsNaN(gaValue))
            {
                GA.Text = "0";
            }

        }

        private void BA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BA.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(EA.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(FA.Text) && !string.IsNullOrEmpty(BA.Text) && !string.IsNullOrEmpty(EA.Text))
            {
                GA.Text = (value11 * value12 * value13).ToString("0.0000");
            }
            if (FA.Text == "" || BA.Text == "" || EA.Text == "")
            {
                GA.Text = "";
            }
        }

        private void EA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BA.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(EA.Text, out var result13) ? result13 : 0;
            if (!string.IsNullOrEmpty(FA.Text) && !string.IsNullOrEmpty(BA.Text) && !string.IsNullOrEmpty(EA.Text))
            {
                GA.Text = (value11 * value12 * value13).ToString("0.0000");
            }
            if (FA.Text == "" || BA.Text == "" || EA.Text == "")
            {
                GA.Text = "";
            }
        }

        private void AB_TextChanged(object sender, EventArgs e)
        {

        }

        private void BB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void BA_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CA_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void DA_KeyPress(object sender, KeyPressEventArgs e)
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

        private void FB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void FA_KeyPress(object sender, KeyPressEventArgs e)
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

        private void GB_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(KN.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Kaizen number");
                return;
            }
            if (string.IsNullOrEmpty(AB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Tape Per Pair Consumption Before Kaizen ");
                return;
            }
            if (string.IsNullOrEmpty(AA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Tape Per Pair Consumption After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter In Converted to Meter or Pair Meters  Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter In Converted to Meter or Pair Meters  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(CB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Total Tape Rolls in Meters Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Total Tape Rolls in Meters After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(DB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Total Roll Cost in Rupees Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(DA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Total Roll Cost in rupees  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(FB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Order Qty Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(FA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Order Qty  After Kaizen");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Kaizen_Number", KN.Text);
            p.Add("Tape_Consumption_B", AB.Text);
            p.Add("Tape_Consumption_A", AA.Text);
            p.Add("Tape_In_Meters_B", BB.Text);
            p.Add("Tape_In_Meters_A", BA.Text);
            p.Add("Total_Rolls_B", CB.Text);
            p.Add("Total_Rolls_A", CA.Text);
            p.Add("Total_Roll_Cost_B", DB.Text);
            p.Add("Total_Roll_Cost_A", DA.Text);
            p.Add("One_Meter_Cost_B", EB.Text);
            p.Add("One_Meter_Cost_A", EA.Text);
            p.Add("Order_Qty_B", FB.Text);
            p.Add("Order_Qty_A", FA.Text);
            p.Add("Overall_Savings_B", GB.Text);
            p.Add("Overall_Savings_A", GA.Text);
            p.Add("Overall_Savings", textBox1.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_Tape_Savings_Data", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
        private void Button3_Click_1(object sender, EventArgs e)
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
                                    "Get_Tape_Savings_Data",
                                    Program.client.UserToken,
                                    JsonConvert.SerializeObject(requestData)
                                    );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);
                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {
                        AB.Text = dataList[0]["TAPE_CONSUMPTION_B"].ToString();
                        AA.Text = dataList[0]["TAPE_CONSUMPTION_A"].ToString();
                        BB.Text = dataList[0]["TAPE_IN_METERS_B"].ToString();
                        BA.Text = dataList[0]["TAPE_IN_METERS_A"].ToString();
                        CB.Text = dataList[0]["TOTAL_ROLLS_B"].ToString();
                        CA.Text = dataList[0]["TOTAL_ROLLS_A"].ToString();
                        DB.Text = dataList[0]["TOTAL_ROLL_COST_B"].ToString();
                        DA.Text = dataList[0]["TOTAL_ROLL_COST_A"].ToString();
                        EB.Text = dataList[0]["ONE_METER_COST_B"].ToString();
                        EA.Text = dataList[0]["ONE_METER_COST_A"].ToString();
                        FB.Text = dataList[0]["ORDER_QTY_B"].ToString();
                        FA.Text = dataList[0]["ORDER_QTY_A"].ToString();
                        GB.Text = dataList[0]["OVERALL_SAVINGS_B"].ToString();
                        GA.Text = dataList[0]["OVERALL_SAVINGS_A"].ToString();
                        textBox1.Text = dataList[0]["OVERALL_SAVING"].ToString();


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

        private void GA_TextChanged(object sender, EventArgs e)
        {

            double value11 = double.TryParse(GA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(GB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(GA.Text) && !string.IsNullOrEmpty(GB.Text))
            {
                textBox1.Text = (value12 - value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(GB.Text) && !string.IsNullOrEmpty(GA.Text))
            {
                textBox1.Text = (-value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(GA.Text) && !string.IsNullOrEmpty(GB.Text))
            {
                textBox1.Text = (value11).ToString("0.0000");
            }
            if (GA.Text == "" && GB.Text == "")
            {
                textBox1.Text = "";
            }




        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredFields())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Tape Savings.frx");

            try
            {
                DataTable dt = CreateTapeDataTable();
                DataRow row = dt.NewRow();
                FillTapeRow(row);
                dt.Rows.Add(row);

                Tape_Preview previewForm = new Tape_Preview(dt, path);
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
                KN,AB,BB,CB,DB, EB, FB, GB, AA, BA, CA, DA, EA, FA, GA, textBox1,

            };

            return requiredFields.All(tb => !string.IsNullOrWhiteSpace(tb.Text.Trim()));
        }

        private DataTable CreateTapeDataTable()
        {
            DataTable dt = new DataTable();

            string[] columns = {
                 "kaizen_num", "Tape_PairConsumption_Before", "Tape_PairConsumption_After",
                 "Converted_Before", "Converted_After", "TapeRoll_Before", "TapeRoll_After",
                 "RollCost_Before", "RollCost_After", "1mTape_Cost_Before", "1mTape_Cost_After",
                 "OrderQty_Before", "OrderQty_After", "Total_Cost_Before", "Total_Cost_After", "overall_savings"
            };

            foreach (var col in columns)
                dt.Columns.Add(col, typeof(string));

            return dt;
        }


        private void FillTapeRow(DataRow row)
        {
            row["kaizen_num"] = KN.Text.Trim();
            row["Tape_PairConsumption_Before"] = AB.Text.Trim();
            row["Tape_PairConsumption_After"] = AA.Text.Trim();

            row["Converted_Before"] = BB.Text.Trim();
            row["Converted_After"] = BA.Text.Trim();
            row["TapeRoll_Before"] = CB.Text.Trim();
            row["TapeRoll_After"] = CA.Text.Trim();

            row["RollCost_Before"] = DB.Text.Trim();
            row["RollCost_After"] = DA.Text.Trim();
            row["1mTape_Cost_Before"] = EB.Text.Trim();
            row["1mTape_Cost_After"] = EA.Text.Trim();

            row["OrderQty_Before"] = FB.Text.Trim();
            row["OrderQty_After"] = FA.Text.Trim();
            row["Total_Cost_Before"] = GB.Text.Trim();
            row["Total_Cost_After"] = GA.Text.Trim();

            row["overall_savings"] = textBox1.Text.Trim();


        }
    }
}
