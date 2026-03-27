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
    public partial class Glue_Savings : Form
    {

        public string Result { get; private set; }
        public Glue_Savings(string kaizen_no)
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
            Total_Savings.Text = "";

            //this.Close();
        }

        private void CB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(CB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(CB.Text) && !string.IsNullOrEmpty(BB.Text))
            {

                DB.Text = (value11 * value12).ToString("0.0000");
            }
            if (CB.Text == "" || BB.Text == "")
            {
                DB.Text = "";
            }
        }

        private void BB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(BB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(BB.Text) && !string.IsNullOrEmpty(CB.Text))
            {

                DB.Text = (value12 * value11).ToString("0.0000");
            }
            if (BB.Text == "" || CB.Text == "")
            {
                DB.Text = "";
            }
        }

        private void CA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(CA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(BA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(CA.Text) && !string.IsNullOrEmpty(BA.Text))
            {

                DA.Text = (value11 * value12).ToString("0.0000");
            }
            if (CA.Text == "" || BA.Text == "")
            {
                DA.Text = "";
            }
        }

        private void BA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(BA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(CA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(BA.Text) && !string.IsNullOrEmpty(CA.Text))
            {

                DA.Text = (value12 * value11).ToString("0.0000");
            }
            if (BA.Text == "" || CA.Text == "")
            {
                DA.Text = "";
            }
        }

        private void DB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(DB.Text, out var result11) ? result11 : 0;
            if (!string.IsNullOrEmpty(DB.Text))
            {
                EB.Text = (value11 / 1000).ToString("0.0000");
            }
            if (DB.Text == "")
            {
                EB.Text = "";
            }

        }

        private void EA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(FA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EA.Text) && !string.IsNullOrEmpty(FA.Text))
            {

                GA.Text = (value12 * value11).ToString("0.0000");
            }
            if (EA.Text == "" || FA.Text == "")
            {
                GA.Text = "";
            }
        }

        private void DA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(DA.Text, out var result11) ? result11 : 0;
            if (!string.IsNullOrEmpty(DA.Text))
            {
                EA.Text = (value11 / 1000).ToString("0.0000");
            }
            if (DB.Text == "")
            {
                EA.Text = "";
            }

        }

        private void FB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(FB.Text) && !string.IsNullOrEmpty(EB.Text))
            {

                GB.Text = (value12 * value11).ToString("0.0000");
            }
            if (FB.Text == "" || EB.Text == "")
            {
                GB.Text = "";
            }
            if (string.IsNullOrEmpty(GB.Text) && string.IsNullOrEmpty(GA.Text))
            {
                Total_Savings.Text = "";
            }


        }

        private void FA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(FA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(EA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(FA.Text) && !string.IsNullOrEmpty(EA.Text))
            {

                GA.Text = (value12 * value11).ToString("0.0000");
            }
            if (FA.Text == "" || EA.Text == "")
            {
                GA.Text = "";
            }
            if (string.IsNullOrEmpty(GA.Text) && string.IsNullOrEmpty(GB.Text))
            {
                Total_Savings.Text = "";
            }

        }

        private void EB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(EB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(FB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(EB.Text) && !string.IsNullOrEmpty(FB.Text))
            {

                GB.Text = (value12 * value11).ToString("0.0000");
            }
            if (EB.Text == "" || FB.Text == "")
            {
                GB.Text = "";
            }
        }

        private void GB_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(GB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(GA.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(GB.Text) && !string.IsNullOrEmpty(GA.Text))
            {
                Total_Savings.Text = (value11 - value12).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(GA.Text) && !string.IsNullOrEmpty(GB.Text))
            {
                Total_Savings.Text = (value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(GB.Text) && !string.IsNullOrEmpty(GA.Text))
            {
                Total_Savings.Text = (-value11).ToString("0.0000");
            }
            if (GB.Text == "" && GA.Text == "")
            {
                Total_Savings.Text = "";
            }
        }

        private void GA_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(GA.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(GB.Text, out var result12) ? result12 : 0;
            if (!string.IsNullOrEmpty(GA.Text) && !string.IsNullOrEmpty(GB.Text))
            {
                Total_Savings.Text = (value12 - value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(GB.Text) && !string.IsNullOrEmpty(GA.Text))
            {
                Total_Savings.Text = (-value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(GA.Text) && !string.IsNullOrEmpty(GB.Text))
            {
                Total_Savings.Text = (value11).ToString("0.0000");
            }
            if (GA.Text == "" && GB.Text == "")
            {
                Total_Savings.Text = "";
            }
        }

        private void Total_Savings_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(GB.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(GA.Text, out var result12) ? result12 : 0;
            if (string.IsNullOrEmpty(GA.Text) && !string.IsNullOrEmpty(GB.Text))
            {
                Total_Savings.Text = (value11).ToString("0.0000");
            }
            if (string.IsNullOrEmpty(GB.Text) && !string.IsNullOrEmpty(GA.Text))
            {
                Total_Savings.Text = (-value12).ToString("0.0000");
            }
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(KN.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Kaizen number");
                return;
            }
            if (string.IsNullOrEmpty(AB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Item Name in  Before Kaizen ");
                return;
            }
            if (string.IsNullOrEmpty(AA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Item Name in  After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Pair Consumption in grams in  Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Pair Consumption in grams in After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(CB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Order Qty Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(BA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Order Qty After Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(FB.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Per KG Cost in Before Kaizen");
                return;
            }
            if (string.IsNullOrEmpty(FA.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase Enter Per KG Cost After Kaizen");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Kaizen_Number", KN.Text);
            p.Add("Item_B", AB.Text);
            p.Add("Item_A", AA.Text);
            p.Add("Pair_Consumption_B", BB.Text);
            p.Add("Pair_Consumption_A", BA.Text);
            p.Add("Order_Qty_B", CB.Text);
            p.Add("Order_Qty_A", CA.Text);
            p.Add("Total_Consumption_B", DB.Text);
            p.Add("Total_Consumption_A", DA.Text);
            p.Add("Converted_Kg_B", EB.Text);
            p.Add("Converted_Kg_A", EA.Text);
            p.Add("Per_Kg_Cost_B", FB.Text);
            p.Add("Per_Kg_Cost_A", FA.Text);
            p.Add("Total_Cost_B", GB.Text);
            p.Add("Total_Cost_A", GA.Text);
            p.Add("Overall_Savings", Total_Savings.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_Glue_Savings_Data", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
                                    "Get_GLUE_Savings_Data",
                                    Program.client.UserToken,
                                    JsonConvert.SerializeObject(requestData)
                                    );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);
                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {
                        AB.Text = dataList[0]["GLUE_ITEM_B"].ToString();
                        AA.Text = dataList[0]["GLUE_ITEM_A"].ToString();
                        BB.Text = dataList[0]["PAIR_CONSUMPTION_B"].ToString();
                        BA.Text = dataList[0]["PAIR_CONSUMPTION_A"].ToString();
                        CB.Text = dataList[0]["ORDER_QTY_B"].ToString();
                        CA.Text = dataList[0]["ORDER_QTY_A"].ToString();
                        DB.Text = dataList[0]["TOTAL_CONSUMPTION_B"].ToString();
                        DA.Text = dataList[0]["TOTAL_CONSUMPTION_A"].ToString();
                        EB.Text = dataList[0]["CONVERTED_KG_B"].ToString();
                        EA.Text = dataList[0]["CONVERTED_KG_A"].ToString();
                        FB.Text = dataList[0]["PER_KG_COST_B"].ToString();
                        FA.Text = dataList[0]["PER_KG_COST_A"].ToString();
                        GB.Text = dataList[0]["TOTAL_COST_B"].ToString();
                        GA.Text = dataList[0]["TOTAL_COST_A"].ToString();
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

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredFields())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Glue Savings.frx");

            try
            {
                DataTable dt = CreatePowerDataTable();
                DataRow row = dt.NewRow();
                FillPowerRow(row);
                dt.Rows.Add(row);

                Glue_Preview previewForm = new Glue_Preview(dt, path);
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
        EB, EA, FB, FA, GB, GA, Total_Savings
    };

            return requiredFields.All(tb => !string.IsNullOrWhiteSpace(tb.Text.Trim()));
        }



        private DataTable CreatePowerDataTable()
        {
            DataTable dt = new DataTable();

            string[] columns = {
        "KAIZEN_NUMBER",
        "GLUE_ITEM_B", "GLUE_ITEM_A",
        "PAIR_CONSUMPTION_B", "PAIR_CONSUMPTION_A",
        "ORDER_QTY_B", "ORDER_QTY_A",
        "TOTAL_CONSUMPTION_B", "TOTAL_CONSUMPTION_A",
        "CONVERTED_KG_B", "CONVERTED_KG_A",
        "PER_KG_COST_B", "PER_KG_COST_A",
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
            row["GLUE_ITEM_B"] = AB.Text.Trim();
            row["GLUE_ITEM_A"] = AA.Text.Trim();
            row["PAIR_CONSUMPTION_B"] = BB.Text.Trim();
            row["PAIR_CONSUMPTION_A"] = BA.Text.Trim();
            row["ORDER_QTY_B"] = CB.Text.Trim();
            row["ORDER_QTY_A"] = CA.Text.Trim();
            row["TOTAL_CONSUMPTION_B"] = DB.Text.Trim();
            row["TOTAL_CONSUMPTION_A"] = DA.Text.Trim();
            row["CONVERTED_KG_B"] = EB.Text.Trim();
            row["CONVERTED_KG_A"] = EA.Text.Trim();
            row["PER_KG_COST_B"] = FB.Text.Trim();
            row["PER_KG_COST_A"] = FA.Text.Trim();
            row["TOTAL_COST_B"] = GB.Text.Trim();
            row["TOTAL_COST_A"] = GA.Text.Trim();
            row["OVERALL_SAVINGS"] = Total_Savings.Text.Trim();

        }





    }
}
