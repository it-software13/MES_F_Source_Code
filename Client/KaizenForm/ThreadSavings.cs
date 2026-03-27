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
    public partial class ThreadSavings : Form
    {
        public string Result { get; private set; }
        public ThreadSavings(string kaizen_no)
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
            HB.Text = "";
            HA.Text = "";
            Total_Savings.Text = "";

            //this.Close();
        }
        private void EB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(DB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(DB.Text))
            {

                FB.Text = (value11 * value12).ToString("0.0000");
            }
            if (Double.IsInfinity(Convert.ToDouble(FB.Text)) || Double.IsNaN(Convert.ToDouble(FB.Text)))
            {
                FB.Text = "0";
            }

            if (EB.Text == "" || DB.Text == "")
            {
                FB.Text = "";
            }
        }

        private void DB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(DB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(DB.Text) && !string.IsNullOrEmpty(EB.Text))
            {

                FB.Text = (value12 * value11).ToString("0.0000");
            }
            if (DB.Text == "" || EB.Text == "")
            {
                FB.Text = "";
            }
        }

        private void EA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(DA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(DA.Text))
            {

                FA.Text = (value11 * value12).ToString("0.0000");
            }

            if (Double.IsInfinity(Convert.ToDouble(FA.Text)) || Double.IsNaN(Convert.ToDouble(FA.Text)))
            {
                FA.Text = "0";
            }

            if (EA.Text == "" || DA.Text == "")
            {
                FA.Text = "";
            }

        }

        private void DA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(DA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(DA.Text) && !string.IsNullOrEmpty(EA.Text))
            {

                FA.Text = (value12 * value11).ToString("0.0000");
            }
            if (DA.Text == "" || EA.Text == "")
            {
                FA.Text = "";
            }
        }

        private void FB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(FB.Text) && !string.IsNullOrEmpty(BB.Text))
            {

                GB.Text = (value11 / value12).ToString("0.0000");
            }
            if (Double.IsInfinity(Convert.ToDouble(GB.Text)) || Double.IsNaN(Convert.ToDouble(GB.Text)))
            {
                GB.Text = "0";
            }

            if (FB.Text == "" || BB.Text == "")
            {
                GB.Text = "";


            }
        }

        private void BB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(BB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(FB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(BB.Text) && !string.IsNullOrEmpty(FB.Text))
            {

                GB.Text = (value12 / value11).ToString("0.0000");
            }
            if (BB.Text == "" || FB.Text == "")
            {
                GB.Text = "";


            }
        }

        private void FA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(FA.Text) && !string.IsNullOrEmpty(BA.Text))
            {

                GA.Text = (value11 / value12).ToString("0.0000");
            }


            if (Double.IsInfinity(Convert.ToDouble(GA.Text)) || Double.IsNaN(Convert.ToDouble(GA.Text)))
            {
                GA.Text = "0";
            }

            if (FA.Text == "" || BA.Text == "")
            {
                GA.Text = "";


            }
        }

        private void BA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(BA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(FA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(BA.Text) && !string.IsNullOrEmpty(FA.Text))
            {

                GA.Text = (value12 / value11).ToString("0.0000");
            }
            if (BA.Text == "" || FA.Text == "")
            {
                GA.Text = "";


            }
        }

        private void GB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(GB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(GB.Text) && !string.IsNullOrEmpty(CB.Text))
            {

                HB.Text = (value11 * value12).ToString("0.0000");
            }

            if (Double.IsInfinity(Convert.ToDouble(HB.Text)) || Double.IsNaN(Convert.ToDouble(HB.Text)))
            {
                HB.Text = "0";
            }

            if (GB.Text == "" || CB.Text == "")
            {
                HB.Text = "";
            }
        }

        private void CB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(CB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(GB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(CB.Text) && !string.IsNullOrEmpty(GB.Text))
            {

                HB.Text = (value12 * value11).ToString("0.0000");
            }
            if (CB.Text == "" || GB.Text == "")
            {
                HB.Text = "";
            }
        }

        private void GA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(GA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(GA.Text) && !string.IsNullOrEmpty(CA.Text))
            {

                HA.Text = (value11 * value12).ToString("0.0000");
            }
            if (Double.IsInfinity(Convert.ToDouble(HA.Text)) || Double.IsNaN(Convert.ToDouble(HA.Text)))
            {
                HA.Text = "0";
            }

            if (GA.Text == "" || CA.Text == "")
            {
                HA.Text = "";
            }
        }

        private void CA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(CA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(GA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(CA.Text) && !string.IsNullOrEmpty(GA.Text))
            {

                HA.Text = (value11 * value12).ToString("0.0000");
            }
            if (CA.Text == "" || GA.Text == "")
            {
                HA.Text = "";
            }
        }

        private void HB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(HB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(HA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(HB.Text) && !string.IsNullOrEmpty(HA.Text))
            {
                Total_Savings.Text = (value11 - value12).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(HA.Text) && !string.IsNullOrEmpty(HB.Text))
            {
                Total_Savings.Text = (value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(HB.Text) && !string.IsNullOrEmpty(HA.Text))
            {
                Total_Savings.Text = (-value12).ToString("0.0000");
            }

            if (Double.IsInfinity(Convert.ToDouble(Total_Savings.Text)) || Double.IsNaN(Convert.ToDouble(Total_Savings.Text)))
            {
                Total_Savings.Text = "0";
            }

            if (HB.Text == "" && HA.Text == "")
            {
                Total_Savings.Text = "";
            }
        }

        private void HA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(HA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(HB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(HA.Text) && !string.IsNullOrEmpty(HB.Text))
            {
                Total_Savings.Text = (value12 - value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(HB.Text) && !string.IsNullOrEmpty(HA.Text))
            {
                Total_Savings.Text = (-value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(HA.Text) && !string.IsNullOrEmpty(HB.Text))
            {
                Total_Savings.Text = (value12).ToString("0.0000");
            }


            if (Double.IsInfinity(Convert.ToDouble(Total_Savings.Text)) || Double.IsNaN(Convert.ToDouble(Total_Savings.Text)))
            {
                Total_Savings.Text = "0";
            }


            if (HA.Text == "" && HB.Text == "")
            {
                Total_Savings.Text = "";
            }
        }

        private void Total_Savings_TextChanged(object sender, EventArgs e)
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

        private void EB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void EA_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(KN.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Kaizen number");
                return;
            }
            if (string.IsNullOrEmpty(AB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Item Before Kaizen ");
                return;
            }
            if (string.IsNullOrEmpty(AA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Item After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Roll In Meters  Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Roll In Meters  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(CB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Roll Cost Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Roll Cost  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(DB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Pair Consumption in Meters Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(DA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Pair Consumption in Meters  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(EB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Order Qty Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(EA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Order Qty  After Kaizen");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Kaizen_Number", KN.Text);
            p.Add("Item_B", AB.Text);
            p.Add("Item_A", AA.Text);
            p.Add("Total_Roll_B", BB.Text);
            p.Add("Total_Roll_A", BA.Text);
            p.Add("Roll_Cost_B", CB.Text);
            p.Add("Roll_Cost_A", CA.Text);
            p.Add("Pair_Consumption_B", DB.Text);
            p.Add("Pair_Consumption_A", DA.Text);
            p.Add("Order_Qty_B", EB.Text);
            p.Add("Order_Qty_A", EA.Text);
            p.Add("Based_On_Order_Qty_B", FB.Text);
            p.Add("Based_On_Order_Qty_A", FA.Text);
            p.Add("Required_Thread_B", GB.Text);
            p.Add("Required_Thread_A", GA.Text);
            p.Add("Total_Cost_B", HB.Text);
            p.Add("Total_Cost_A", HA.Text);
            p.Add("Overall_Savings", Total_Savings.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_Thread_Savings_Data", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
                                    "Get_THREAD_Savings_Data",
                                    Program.client.UserToken,
                                    JsonConvert.SerializeObject(requestData)
                                    );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);
                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {
                        AB.Text = dataList[0]["THREAD_ITEM_B"].ToString();
                        AA.Text = dataList[0]["THREAD_ITEM_A"].ToString();
                        BB.Text = dataList[0]["TOTAL_ROLL_B"].ToString();
                        BA.Text = dataList[0]["TOTAL_ROLL_A"].ToString();
                        CB.Text = dataList[0]["ROLL_COST_B"].ToString();
                        CA.Text = dataList[0]["ROLL_COST_A"].ToString();
                        DB.Text = dataList[0]["PAIR_CONSUMPTION_B"].ToString();
                        DA.Text = dataList[0]["PAIR_CONSUMPTION_A"].ToString();
                        EB.Text = dataList[0]["ORDER_QTY_B"].ToString();
                        EA.Text = dataList[0]["ORDER_QTY_A"].ToString();
                        FB.Text = dataList[0]["BASED_ON_ORDER_QTY_B"].ToString();
                        FA.Text = dataList[0]["BASED_ON_ORDER_QTY_A"].ToString();
                        GB.Text = dataList[0]["REQUIRED_THREAD_B"].ToString();
                        GA.Text = dataList[0]["REQUIRED_THREAD_A"].ToString();
                        HB.Text = dataList[0]["TOTAL_COST_B"].ToString();
                        HA.Text = dataList[0]["TOTAL_COST_A"].ToString();
                        Total_Savings.Text = dataList[0]["OVERALL_SAVINGS"].ToString();
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

        private void Print_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredFields())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Thread Savings.frx");

            try
            {
                DataTable dt = CreateThreadDataTable();
                DataRow row = dt.NewRow();
                FillThreadRow(row);
                dt.Rows.Add(row);

                Thread_Preview previewForm = new Thread_Preview(dt, path);
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
                KN,AB,BB,CB,DB, EB, FB, GB, HB, AA, BA, CA, DA, EA, FA, GA, HA, Total_Savings,

            };

            return requiredFields.All(tb => !string.IsNullOrWhiteSpace(tb.Text.Trim()));
        }

        private DataTable CreateThreadDataTable()
        {
            DataTable dt = new DataTable();

            string[] columns = {
                "kaizen_num", "Item_Before", "Item_After", "TotalRoll_Before", "TotalRoll_After", "RollCost_Before", "RollCost_After",
                "PairConsumption_Before", "PairConsumption_After",
                "OrderQty_Before", "OrderQty_After", "BasedOn_Order_Before", "BasedOn_Order_After", "ReqThreads_Before", "ReqThreads_After",
                "Total_Cost_Before", "Total_Cost_After", "overall_savings"
            };

            foreach (var col in columns)
                dt.Columns.Add(col, typeof(string));

            return dt;
        }


        private void FillThreadRow(DataRow row)
        {
            row["kaizen_num"] = KN.Text.Trim();
            row["Item_Before"] = AB.Text.Trim();
            row["Item_After"] = AA.Text.Trim();

            row["TotalRoll_Before"] = BB.Text.Trim();
            row["TotalRoll_After"] = BA.Text.Trim();
            row["RollCost_Before"] = CB.Text.Trim();
            row["RollCost_After"] = CA.Text.Trim();

            row["PairConsumption_Before"] = DB.Text.Trim();
            row["PairConsumption_After"] = DA.Text.Trim();
            row["OrderQty_Before"] = EB.Text.Trim();
            row["OrderQty_After"] = EA.Text.Trim();

            row["BasedOn_Order_Before"] = FB.Text.Trim();
            row["BasedOn_Order_After"] = FA.Text.Trim();
            row["ReqThreads_Before"] = GB.Text.Trim();
            row["ReqThreads_After"] = GA.Text.Trim();
            row["Total_Cost_Before"] = HB.Text.Trim();
            row["Total_Cost_After"] = HA.Text.Trim();

            row["overall_savings"] = Total_Savings.Text.Trim();


        }
    }





}

