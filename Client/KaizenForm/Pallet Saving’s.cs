using MaterialSkin.Controls;
using Newtonsoft.Json;
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
    public partial class Pallet_Saving_s : MaterialForm
    {
        private bool isUpdating = false;
        public string Result { get; private set; }
        public Pallet_Saving_s(string kaizen_no)
        {
            InitializeComponent();
            textBox46.Text = kaizen_no;
        }


        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox5.Text = "";
                //textBox6.Text = "";
                textBox5.Enabled = false;
                //textBox6.Enabled = false;
            }
            else
            {
                textBox5.Enabled = true;
                //textBox6.Enabled = true;
            }

            Group_Methods();

        }



        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox3.Text = "";
                textBox3.Enabled = false;
            }
            else
            {
                textBox3.Enabled = true;
            }

            Group_Methods();

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {


            if (!string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox6.Text = "";
                textBox6.Enabled = false;
            }
            else
            {
                textBox6.Enabled = true;
            }

            Group_Methods();

        }


        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox4.Text = "";
                textBox4.Enabled = false;
            }
            else
            {
                textBox4.Enabled = true;
            }

            Group_Methods();

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox8.Text))
            {
                textBox9.Text = "";
                textBox9.Enabled = false;
            }
            else
            {
                textBox9.Enabled = true;
            }

            Group_Methods();

        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox9.Text))
            {
                textBox8.Text = "";
                textBox8.Enabled = false;
            }
            else
            {
                textBox8.Enabled = true;
            }

            Group_Methods();

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox7.Text))
            {
                textBox10.Text = "";
                textBox10.Enabled = false;
            }
            else
            {
                textBox10.Enabled = true;
            }

            Group_Methods();

        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox10.Text))
            {
                textBox7.Text = "";
                textBox7.Enabled = false;
            }
            else
            {
                textBox7.Enabled = true;
            }

            Group_Methods();

        }
        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox11.Text))
            {
                textBox13.Text = "";
                textBox13.Enabled = false;
            }
            else
            {
                textBox13.Enabled = true;
            }

            Group_Methods();

        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox13.Text))
            {
                textBox11.Text = "";
                textBox11.Enabled = false;
            }
            else
            {
                textBox11.Enabled = true;
            }

            Group_Methods();

        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox12.Text))
            {
                textBox14.Text = "";
                textBox14.Enabled = false;
            }
            else
            {
                textBox14.Enabled = true;
            }

            Group_Methods();

        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox14.Text))
            {
                textBox12.Text = "";
                textBox12.Enabled = false;
            }
            else
            {
                textBox12.Enabled = true;
            }

            Group_Methods();

        }

        private void Each_Line_Output()
        {
            if (isUpdating) return;
            isUpdating = true;

            // Step 1: Before Each Line Output
            if (double.TryParse(textBox27.Text, out double work_Hours) &&
                double.TryParse(textBox23.Text, out double PerHour_Output))
            {
                textBox29.Text = (work_Hours * PerHour_Output).ToString("0.00");
            }
            else
            {
                textBox29.Clear();
            }

            // Step 2: After Each Line Output
            if (double.TryParse(textBox28.Text, out double After_work_Hours) &&
                double.TryParse(textBox24.Text, out double After_PerHour_Output))
            {
                textBox30.Text = (After_work_Hours * After_PerHour_Output).ToString("0.00");
            }
            else
            {
                textBox30.Clear();
            }

            // Step 3: Before Required Machines
            if (double.TryParse(textBox25.Text, out double Before_Ord_Qty) &&
                double.TryParse(textBox29.Text, out double Before_Line_Output))
            {
                double result = (Before_Line_Output == 0) ? 0 : (Before_Ord_Qty / Before_Line_Output);
                textBox31.Text = result.ToString("0.00");
            }
            else
            {
                textBox31.Clear();
            }

            // Step 4: After Required Machines
            if (double.TryParse(textBox26.Text, out double After_Ord_Qty) &&
                double.TryParse(textBox30.Text, out double After_Line_Output))
            {
                double result = (After_Line_Output == 0) ? 0 : (After_Ord_Qty / After_Line_Output);
                textBox32.Text = result.ToString("0.00");
            }
            else
            {
                textBox32.Clear();
            }

            isUpdating = false;
        }


        private void TextBox23_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox27_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox24_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox28_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox25_TextChanged(object sender, EventArgs e) { Group_Methods(); }


        private void TextBox26_TextChanged(object sender, EventArgs e) { Group_Methods(); }


        



        private void Top_Pallets()
        {
            if (isUpdating) return;
            isUpdating = true;

            double beforeTotal = 0;
            double afterTotal = 0;

            bool hasValidBefore = false;
            bool hasValidAfter = false;

            // Case - 1
            if (double.TryParse(textBox3.Text, out double Before_Top_Pallet1) &&
                double.TryParse(textBox31.Text, out double Before_Required_Machines1))
            {
                beforeTotal += (Before_Top_Pallet1 * 1) * Before_Required_Machines1;
                hasValidBefore = true;
            }

            if (double.TryParse(textBox4.Text, out double After_Top_Pallet1) &&
                double.TryParse(textBox32.Text, out double After_Required_Machines1))
            {
                afterTotal += (After_Top_Pallet1 * 1) * After_Required_Machines1;
                hasValidAfter = true;
            }

            // Case - 2
            if (double.TryParse(textBox5.Text, out double Before_Top_Pallet2) &&
                double.TryParse(textBox31.Text, out double Before_Required_Machines2))
            {
                beforeTotal += (Before_Top_Pallet2 * 2) * Before_Required_Machines2;
                hasValidBefore = true;
            }

            if (double.TryParse(textBox6.Text, out double After_Top_Pallet2) &&
                double.TryParse(textBox32.Text, out double After_Required_Machines2))
            {
                afterTotal += (After_Top_Pallet2 * 2) * After_Required_Machines2;
                hasValidAfter = true;
            }

            // Only show result if at least one valid pair exists
            textBox33.Text = hasValidBefore ? beforeTotal.ToString("0.00") : string.Empty;
            textBox34.Text = hasValidAfter ? afterTotal.ToString("0.00") : string.Empty;

            isUpdating = false;
        }


        private void Inside_Pallets()
        {
            if (isUpdating) return;
            isUpdating = true;

            double beforeTotal = 0;
            double afterTotal = 0;

            bool hasValidBefore = false;
            bool hasValidAfter = false;

            // Case 1
            if (double.TryParse(textBox8.Text, out double Before_inside_Pallet1) &&
                double.TryParse(textBox31.Text, out double Before_Required_Machines1))
            {
                beforeTotal += (Before_inside_Pallet1 * 1) * Before_Required_Machines1;
                hasValidBefore = true;
            }

            if (double.TryParse(textBox7.Text, out double After_inside_Pallet1) &&
                double.TryParse(textBox32.Text, out double After_Required_Machines1))
            {
                afterTotal += (After_inside_Pallet1 * 1) * After_Required_Machines1;
                hasValidAfter = true;
            }

            // Case 2
            if (double.TryParse(textBox9.Text, out double Before_inside_Pallet2) &&
                double.TryParse(textBox31.Text, out double Before_Required_Machines2))
            {
                beforeTotal += (Before_inside_Pallet2 * 2) * Before_Required_Machines2;
                hasValidBefore = true;
            }

            if (double.TryParse(textBox10.Text, out double After_inside_Pallet2) &&
                double.TryParse(textBox32.Text, out double After_Required_Machines2))
            {
                afterTotal += (After_inside_Pallet2 * 2) * After_Required_Machines2;
                hasValidAfter = true;
            }

            textBox35.Text = hasValidBefore ? beforeTotal.ToString("0.00") : string.Empty;
            textBox36.Text = hasValidAfter ? afterTotal.ToString("0.00") : string.Empty;

            isUpdating = false;
        }



        private void Bottom_Pallets()
        {
            if (isUpdating) return;
            isUpdating = true;
            double beforeTotal = 0;
            double afterTotal = 0;
            bool hasValidBefore = false;
            bool hasValidAfter = false;
            // Case 1
            if (double.TryParse(textBox11.Text, out double Before_Bottom_Pallet1) &&
                double.TryParse(textBox31.Text, out double Before_Required_Machines1))
            {
                beforeTotal += (Before_Bottom_Pallet1 * 1) * Before_Required_Machines1;
                hasValidBefore = true;
            }

            if (double
                .TryParse(textBox12.Text, out double After_Bottom_Pallet1) &&
                double.TryParse(textBox32.Text, out double After_Required_Machines1))
            {
                afterTotal += (After_Bottom_Pallet1 * 1) * After_Required_Machines1;
                hasValidAfter = true;
            }

            // Case 2
            if (double.TryParse(textBox13.Text, out double Before_Bottom_Pallet2) &&
                double.TryParse(textBox31.Text, out double Before_Required_Machines2))
            {
                beforeTotal += (Before_Bottom_Pallet2 * 2) * Before_Required_Machines2;
                hasValidBefore = true;
            }

            if (double.TryParse(textBox14.Text, out double After_Bottom_Pallet2) &&
                double.TryParse(textBox32.Text, out double After_Required_Machines2))
            {
                afterTotal += (After_Bottom_Pallet2 * 2) * After_Required_Machines2;
                hasValidAfter = true;
            }

            textBox37.Text = hasValidBefore ? beforeTotal.ToString("0.00") : string.Empty;
            textBox38.Text = hasValidAfter ? afterTotal.ToString("0.00") : string.Empty;

            isUpdating = false;
        }



        private void No_Of_FiberBoards()
        {
            if (isUpdating) return;
            isUpdating = true;

            // Before
            if (double.TryParse(textBox33.Text, out double B_Top_Pallets) &&
                double.TryParse(textBox17.Text, out double B_Top_Pallet_Dimension) &&
                double.TryParse(textBox15.Text, out double B_FB_Dimen) &&
                double.TryParse(textBox35.Text, out double Before_Inside_Pallets) &&
                double.TryParse(textBox19.Text, out double B_Inside_Pallet_Dimen) &&
                double.TryParse(textBox37.Text, out double Before_Bottom_Pallets) &&
                double.TryParse(textBox21.Text, out double B_Bottom_Pallet_Dimen))
            {
                double result = (B_FB_Dimen == 0) ? 0 : ((B_Top_Pallets * B_Top_Pallet_Dimension) / B_FB_Dimen)
                              + ((Before_Inside_Pallets * B_Inside_Pallet_Dimen) / B_FB_Dimen)
                              + ((Before_Bottom_Pallets * B_Bottom_Pallet_Dimen) / B_FB_Dimen);
                textBox39.Text = result.ToString("0.00");
            }
            else
            {
                textBox39.Clear();
            }

            // After
            if (double.TryParse(textBox34.Text, out double A_Top_Pallets) &&
                double.TryParse(textBox18.Text, out double A_Top_Pallet_Dimension) &&
                double.TryParse(textBox16.Text, out double A_FB_Dimen) &&
                double.TryParse(textBox36.Text, out double After_Inside_Pallets) &&
                double.TryParse(textBox20.Text, out double A_Inside_Pallet_Dimen) &&
                double.TryParse(textBox38.Text, out double After_Bottom_Pallets) &&
                double.TryParse(textBox22.Text, out double A_Bottom_Pallet_Dimen))
            {
                double result = (A_FB_Dimen == 0) ? 0 : ((A_Top_Pallets * A_Top_Pallet_Dimension) / A_FB_Dimen)
                                                          + ((After_Inside_Pallets * A_Inside_Pallet_Dimen) / A_FB_Dimen)
                                                          + ((After_Bottom_Pallets * A_Bottom_Pallet_Dimen) / A_FB_Dimen);
                textBox40.Text = result.ToString("0.00");
            }
            else
            {
                textBox40.Clear();
            }

            // Before Total Cost
            if (double.TryParse(textBox41.Text, out double B_FB_Cost) &&
                double.TryParse(textBox39.Text, out double B_No_Of_FB))
            {
                textBox43.Text = (B_FB_Cost * B_No_Of_FB).ToString("0.00");
            }
            else
            {
                textBox43.Clear();
            }

            // After Total Cost
            if (double.TryParse(textBox42.Text, out double A_FB_Cost) &&
                double.TryParse(textBox40.Text, out double A_No_Of_FB))
            {
                textBox44.Text = (A_FB_Cost * A_No_Of_FB).ToString("0.00");
            }
            else
            {
                textBox44.Clear();
            }

            // Overall Savings
            if (double.TryParse(textBox43.Text, out double Before_Total_Cost) &&
                double.TryParse(textBox44.Text, out double After_Total_Cost))
            {
                textBox45.Text = (Before_Total_Cost - After_Total_Cost).ToString("0.00");
            }
            else
            {
                textBox45.Clear();
            }

            isUpdating = false;
        }


        private void Group_Methods()
        {
            Each_Line_Output();

            Top_Pallets();
            Inside_Pallets();
            Bottom_Pallets();

            No_Of_FiberBoards();
        }
        private void TextBox31_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox32_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox15_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox17_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox19_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox21_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox16_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox18_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox20_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox22_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox33_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox35_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox37_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox34_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox36_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox38_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox39_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox40_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox41_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox42_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox43_TextChanged(object sender, EventArgs e) { Group_Methods(); }

        private void TextBox44_TextChanged(object sender, EventArgs e) { Group_Methods(); }


        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Allow control keys (like backspace)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // Allow digits and a single decimal point
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void Pallet_Saving_s_Load(object sender, EventArgs e)
        {
            textBox1.KeyPress += NumericTextBox_KeyPress;
            textBox2.KeyPress += NumericTextBox_KeyPress;
            textBox3.KeyPress += NumericTextBox_KeyPress;
            textBox4.KeyPress += NumericTextBox_KeyPress;
            textBox5.KeyPress += NumericTextBox_KeyPress;
            textBox6.KeyPress += NumericTextBox_KeyPress;
            textBox7.KeyPress += NumericTextBox_KeyPress;
            textBox8.KeyPress += NumericTextBox_KeyPress;
            textBox9.KeyPress += NumericTextBox_KeyPress;
            textBox10.KeyPress += NumericTextBox_KeyPress;
            textBox11.KeyPress += NumericTextBox_KeyPress;
            textBox12.KeyPress += NumericTextBox_KeyPress;
            textBox13.KeyPress += NumericTextBox_KeyPress;
            textBox14.KeyPress += NumericTextBox_KeyPress;
            textBox15.KeyPress += NumericTextBox_KeyPress;
            textBox16.KeyPress += NumericTextBox_KeyPress;
            textBox17.KeyPress += NumericTextBox_KeyPress;
            textBox18.KeyPress += NumericTextBox_KeyPress;
            textBox19.KeyPress += NumericTextBox_KeyPress;
            textBox20.KeyPress += NumericTextBox_KeyPress;
            textBox21.KeyPress += NumericTextBox_KeyPress;
            textBox22.KeyPress += NumericTextBox_KeyPress;
            textBox23.KeyPress += NumericTextBox_KeyPress;
            textBox24.KeyPress += NumericTextBox_KeyPress;
            textBox25.KeyPress += NumericTextBox_KeyPress;
            textBox26.KeyPress += NumericTextBox_KeyPress;
            textBox27.KeyPress += NumericTextBox_KeyPress;
            textBox28.KeyPress += NumericTextBox_KeyPress;
            textBox41.KeyPress += NumericTextBox_KeyPress;
            textBox42.KeyPress += NumericTextBox_KeyPress;

            Update.Visible = false;
        }


        private void HandleData(string apiMethod, bool isUpdate)
        {
            if (isUpdating) return;
            isUpdating = true;

            try
            {
                string kaizenNum = textBox46.Text.Trim();
                if (string.IsNullOrWhiteSpace(kaizenNum))
                {
                    MessageBox.Show("Kaizen Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isUpdating = false;
                    return;
                }

                // Required textboxes validation
                List<TextBox> requiredFields = new List<TextBox>
        {
            textBox1, textBox2, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20,
            textBox21, textBox22, textBox23, textBox24, textBox25, textBox26, textBox27, textBox28,
            textBox29, textBox30, textBox31, textBox32, textBox33, textBox34, textBox35, textBox36,
            textBox37, textBox38, textBox39, textBox40, textBox41, textBox42
        };

                foreach (TextBox field in requiredFields)
                {

                    if (string.IsNullOrWhiteSpace(field.Text.Trim()))
                    {
                        MessageBox.Show($"Please fill all required fields. Missing: {field.Name}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        isUpdating = false;
                        return;
                    }
                }

                Dictionary<string, object> requestData = new Dictionary<string, object>
        {
            { "kaizen_num", kaizenNum },
            { "total_sizes_before", textBox1.Text.Trim() },
            { "total_sizes_after", textBox2.Text.Trim() },

            { "top_pallet1_before", string.IsNullOrWhiteSpace(textBox3.Text) ? null : textBox3.Text.Trim() },
            { "top_pallet1_after", string.IsNullOrWhiteSpace(textBox4.Text) ? null : textBox4.Text.Trim() },
            { "top_pallet2_before", string.IsNullOrWhiteSpace(textBox5.Text) ? null : textBox5.Text.Trim() },
            { "top_pallet2_after", string.IsNullOrWhiteSpace(textBox6.Text) ? null : textBox6.Text.Trim() },

            { "inside_pallet1_before", string.IsNullOrWhiteSpace(textBox8.Text) ? null : textBox8.Text.Trim() },
            { "inside_pallet1_after", string.IsNullOrWhiteSpace(textBox7.Text) ? null : textBox7.Text.Trim() },
            { "inside_pallet2_before", string.IsNullOrWhiteSpace(textBox9.Text) ? null : textBox9.Text.Trim() },
            { "inside_pallet2_after", string.IsNullOrWhiteSpace(textBox10.Text) ? null : textBox10.Text.Trim() },

            { "bottom_pallet1_before", string.IsNullOrWhiteSpace(textBox11.Text) ? null : textBox11.Text.Trim() },
            { "bottom_pallet1_after", string.IsNullOrWhiteSpace(textBox12.Text) ? null : textBox12.Text.Trim() },
            { "bottom_pallet2_before", string.IsNullOrWhiteSpace(textBox13.Text) ? null : textBox13.Text.Trim() },
            { "bottom_pallet2_after", string.IsNullOrWhiteSpace(textBox14.Text) ? null : textBox14.Text.Trim() },

            { "fb_dimension_before", textBox15.Text.Trim() },
            { "fb_dimension_after", textBox16.Text.Trim() },

            { "top_pallet_dimension_before", textBox17.Text.Trim() },
            { "top_pallet_dimension_after", textBox18.Text.Trim() },
            { "inside_pallet_dimension_before", textBox19.Text.Trim() },
            { "inside_pallet_dimension_after", textBox20.Text.Trim() },
            { "bottom_pallet_dimension_before", textBox21.Text.Trim() },
            { "bottom_pallet_dimension_after", textBox22.Text.Trim() },

            { "per_hour_output_before", textBox23.Text.Trim() },
            { "per_hour_output_after", textBox24.Text.Trim() },
            { "order_qty_before", textBox25.Text.Trim() },
            { "order_qty_after", textBox26.Text.Trim() },
            { "working_hours_before", textBox27.Text.Trim() },
            { "working_hours_after", textBox28.Text.Trim() },

            { "each_line_output_before", textBox29.Text.Trim() },
            { "each_line_output_after", textBox30.Text.Trim() },

            { "required_machines_before", textBox31.Text.Trim() },
            { "required_machines_after", textBox32.Text.Trim() },

            { "top_pallets_before", textBox33.Text.Trim() },
            { "top_pallets_after", textBox34.Text.Trim() },

            { "inside_pallets_before", textBox35.Text.Trim() },
            { "inside_pallets_after", textBox36.Text.Trim() },

            { "bottom_pallets_before", textBox37.Text.Trim() },
            { "bottom_pallets_after", textBox38.Text.Trim() },

            { "no_of_fiber_board_before", textBox39.Text.Trim() },
            { "no_of_fiber_board_after", textBox40.Text.Trim() },

            { "fiber_board_cost_before", textBox41.Text.Trim() },
            { "fiber_board_cost_after", textBox42.Text.Trim() },
            { "total_cost_before", textBox43.Text.Trim() },
            { "total_cost_after", textBox44.Text.Trim() },
            { "overall_savings", textBox45.Text.Trim() }
        };

                string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                                                                            "KZ_RTDMAPI",
                                                                            "KZ_RTDMAPI.Controllers.Kaizenserver",
                                                                            apiMethod,
                                                                            Program.client.UserToken,
                                                                            Newtonsoft.Json.JsonConvert.SerializeObject(requestData));

                SJeMES_Framework.WebAPI.ResultObject result = Newtonsoft.Json.JsonConvert.DeserializeObject<SJeMES_Framework.WebAPI.ResultObject>(response);

                var responseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

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
                        ShowMessage(isUpdate ? "Data Updated Successfully!" : "Data Inserted Successfully!", "Success", MessageBoxIcon.Information);
                        //this.Hide();
                        // Clear any data after successful insertion
                        ClearFields();
                        this.Close();
                    }


                    //if (result.IsSuccess)
                    //{
                    //    ShowMessage(isUpdate ? "Data Updated Successfully!" : "Data Inserted Successfully!", "Success", MessageBoxIcon.Information);
                    //    ClearFields();
                    //    this.Close();
                    //}
                    //else
                    //{
                    //    ShowMessage("Failed to process data. Error: " + result.ErrMsg, "Error", MessageBoxIcon.Error);
                    //}
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, "Error", MessageBoxIcon.Error);
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }


        private void ClearFields()
        {
            ClearAllTextBoxes(this);
            textBox1.Focus();
        }

        private void ClearAllTextBoxes(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (ctrl.HasChildren)
                {
                    ClearAllTextBoxes(ctrl); // Recursive call for nested containers
                }
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            HandleData("InsertOrUpdate_PalletSavings", isUpdate: false);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                string kaizenNum = textBox46.Text.Trim();

                if (string.IsNullOrWhiteSpace(kaizenNum))
                {
                    MessageBox.Show("Enter a valid Kaizen Number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<string, string> requestData = new Dictionary<string, string>
        {
            { "kaizen_num", kaizenNum }
        };

                string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                    Program.client.APIURL,
                                    "KZ_RTDMAPI",
                                    "KZ_RTDMAPI.Controllers.Kaizenserver",
                                    "Get_Pallet_Analysis_Data",
                                    Program.client.UserToken,
                                    Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
                                    );

                SJeMES_Framework.WebAPI.ResultObject result = Newtonsoft.Json.JsonConvert.DeserializeObject<SJeMES_Framework.WebAPI.ResultObject>(response);

                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {
                        var data = dataList[0];

                        textBox1.Text = data["TOTAL_SIZES_B"].ToString();
                        textBox2.Text = data["TOTAL_SIZES_A"].ToString();

                        textBox3.Text = data["TOP_PALLET1_B"].ToString();
                        textBox4.Text = data["TOP_PALLET1_A"].ToString();
                        textBox5.Text = data["TOP_PALLET2_B"].ToString();
                        textBox6.Text = data["TOP_PALLET2_A"].ToString();

                        textBox8.Text = data["INSIDE_PALLET1_B"].ToString();
                        textBox7.Text = data["INSIDE_PALLET1_A"].ToString();
                        textBox9.Text = data["INSIDE_PALLET2_B"].ToString();
                        textBox10.Text = data["INSIDE_PALLET2_A"].ToString();

                        textBox11.Text = data["BOTTOM_PALLET1_B"].ToString();
                        textBox12.Text = data["BOTTOM_PALLET1_A"].ToString();
                        textBox13.Text = data["BOTTOM_PALLET2_B"].ToString();
                        textBox14.Text = data["BOTTOM_PALLET2_A"].ToString();

                        textBox15.Text = data["FB_DIMEN_B"].ToString();
                        textBox16.Text = data["FB_DIMEN_A"].ToString();

                        textBox17.Text = data["TOP_PALLET_DIMEN_B"].ToString();
                        textBox18.Text = data["TOP_PALLET_DIMEN_A"].ToString();
                        textBox19.Text = data["INSIDE_PALLET_DIMEN_B"].ToString();
                        textBox20.Text = data["INSIDE_PALLET_DIMEN_A"].ToString();
                        textBox21.Text = data["BOTTOM_PALLET_DIMEN_B"].ToString();
                        textBox22.Text = data["BOTTOM_PALLET_DIMEN_A"].ToString();

                        textBox23.Text = data["PER_HOUR_OUTPUT_B"].ToString();
                        textBox24.Text = data["PER_HOUR_OUTPUT_A"].ToString();
                        textBox25.Text = data["ORDER_QTY_B"].ToString();
                        textBox26.Text = data["ORDER_QTY_A"].ToString();

                        textBox27.Text = data["WORKING_HOURS_B"].ToString();
                        textBox28.Text = data["WORKING_HOURS_A"].ToString();
                        textBox29.Text = data["EACH_LINE_OUTPUT_B"].ToString();
                        textBox30.Text = data["EACH_LINE_OUTPUT_A"].ToString();

                        textBox31.Text = data["REQ_MACHINES_B"].ToString();
                        textBox32.Text = data["REQ_MACHINES_A"].ToString();

                        textBox33.Text = data["TOP_PALLETS_B"].ToString();
                        textBox34.Text = data["TOP_PALLETS_A"].ToString();
                        textBox35.Text = data["INSIDE_PALLETS_B"].ToString();
                        textBox36.Text = data["INSIDE_PALLETS_A"].ToString();
                        textBox37.Text = data["BOTTOM_PALLETS_B"].ToString();
                        textBox38.Text = data["BOTTOM_PALLETS_A"].ToString();

                        textBox39.Text = data["NO_OF_FB_B"].ToString();
                        textBox40.Text = data["NO_OF_FB_A"].ToString();

                        textBox41.Text = data["FB_COST_B"].ToString();
                        textBox42.Text = data["FB_COST_A"].ToString();
                        textBox43.Text = data["TOTAL_COST_B"].ToString();
                        textBox44.Text = data["TOTAL_COST_A"].ToString();

                        textBox45.Text = data["OVERALL_SAVINGS"].ToString();

                        Edit.Visible = false;
                        Submit.Visible = false;
                        Update.Visible = true;

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

        private void Update_Click(object sender, EventArgs e)
        {
            HandleData("InsertOrUpdate_PalletSavings", isUpdate: true);

            Edit.Visible = true;
            Submit.Visible = true;
            Update.Visible = false;
        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredFields())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Pallet Analysis.frx");

            try
            {
                DataTable dt = CreatePalletDataTable();
                DataRow row = dt.NewRow();
                FillPalletRow(row);
                dt.Rows.Add(row);

                Pallet_Preview previewForm = new Pallet_Preview(dt, path);
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
                textBox1, textBox2, textBox15, textBox16, textBox17, textBox18,
                textBox19, textBox20, textBox21, textBox22, textBox23, textBox24,
                textBox25, textBox26, textBox27, textBox28, textBox29, textBox30,
                textBox31, textBox32, textBox33, textBox34, textBox35, textBox36,
                textBox37, textBox38, textBox39, textBox40, textBox41, textBox42
            };

            return requiredFields.All(tb => !string.IsNullOrWhiteSpace(tb.Text.Trim()));
        }

        private DataTable CreatePalletDataTable()
        {
            DataTable dt = new DataTable();
           string[] columns = {
                "kaizen_num", "total_sizes_before", "total_sizes_after",
                "top_pallet1_before", "top_pallet1_after", "top_pallet2_before", "top_pallet2_after",
                "inside_pallet1_before", "inside_pallet1_after", "inside_pallet2_before", "inside_pallet2_after",
                "bottom_pallet1_before", "bottom_pallet1_after", "bottom_pallet2_before", "bottom_pallet2_after",
                "fb_dimension_before", "fb_dimension_after",
                "top_pallet_dimension_before", "top_pallet_dimension_after",
                "inside_pallet_dimension_before", "inside_pallet_dimension_after",
                "bottom_pallet_dimension_before", "bottom_pallet_dimension_after",
                "per_hour_output_before", "per_hour_output_after",
                "order_qty_before", "order_qty_after",
                "working_hours_before", "working_hours_after",
                "each_line_output_before", "each_line_output_after",
                "required_machines_before", "required_machines_after",
                "top_pallets_before", "top_pallets_after",
                "inside_pallets_before", "inside_pallets_after",
                "bottom_pallets_before", "bottom_pallets_after",
                "no_of_fiber_board_before", "no_of_fiber_board_after",
                "fiber_board_cost_before", "fiber_board_cost_after",
                "total_cost_before", "total_cost_after",
                "overall_savings"
            };
           foreach (var col in columns)
           dt.Columns.Add(col, typeof(string));
            return dt;


        }


        private void FillPalletRow(DataRow row)
        {
            row["kaizen_num"] = textBox46.Text.Trim();
            row["total_sizes_before"] = textBox1.Text.Trim();
            row["total_sizes_after"] = textBox2.Text.Trim();
            row["top_pallet1_before"] = textBox3.Text.Trim();
            row["top_pallet1_after"] = textBox4.Text.Trim();
            row["top_pallet2_before"] = textBox5.Text.Trim();
            row["top_pallet2_after"] = textBox6.Text.Trim();
            row["inside_pallet1_before"] = textBox8.Text.Trim();
            row["inside_pallet1_after"] = textBox7.Text.Trim();
            row["inside_pallet2_before"] = textBox9.Text.Trim();
            row["inside_pallet2_after"] = textBox10.Text.Trim();
            row["bottom_pallet1_before"] = textBox11.Text.Trim();
            row["bottom_pallet1_after"] = textBox12.Text.Trim();
            row["bottom_pallet2_before"] = textBox13.Text.Trim();
            row["bottom_pallet2_after"] = textBox14.Text.Trim();
            row["fb_dimension_before"] = textBox15.Text.Trim();
            row["fb_dimension_after"] = textBox16.Text.Trim();
            row["top_pallet_dimension_before"] = textBox17.Text.Trim();
            row["top_pallet_dimension_after"] = textBox18.Text.Trim();
            row["inside_pallet_dimension_before"] = textBox19.Text.Trim();
            row["inside_pallet_dimension_after"] = textBox20.Text.Trim();
            row["bottom_pallet_dimension_before"] = textBox21.Text.Trim();
            row["bottom_pallet_dimension_after"] = textBox22.Text.Trim();
            row["per_hour_output_before"] = textBox23.Text.Trim();
            row["per_hour_output_after"] = textBox24.Text.Trim();
            row["order_qty_before"] = textBox25.Text.Trim();
            row["order_qty_after"] = textBox26.Text.Trim();
            row["working_hours_before"] = textBox27.Text.Trim();
            row["working_hours_after"] = textBox28.Text.Trim();
            row["each_line_output_before"] = textBox29.Text.Trim();
            row["each_line_output_after"] = textBox30.Text.Trim();
            row["required_machines_before"] = textBox31.Text.Trim();
            row["required_machines_after"] = textBox32.Text.Trim();
            row["top_pallets_before"] = textBox33.Text.Trim();
            row["top_pallets_after"] = textBox34.Text.Trim();
            row["inside_pallets_before"] = textBox35.Text.Trim();
            row["inside_pallets_after"] = textBox36.Text.Trim();
            row["bottom_pallets_before"] = textBox37.Text.Trim();
            row["bottom_pallets_after"] = textBox38.Text.Trim();
            row["no_of_fiber_board_before"] = textBox39.Text.Trim();
            row["no_of_fiber_board_after"] = textBox40.Text.Trim();
            row["fiber_board_cost_before"] = textBox41.Text.Trim();
            row["fiber_board_cost_after"] = textBox42.Text.Trim();
            row["total_cost_before"] = textBox43.Text.Trim();
            row["total_cost_after"] = textBox44.Text.Trim();
            row["overall_savings"] = textBox45.Text.Trim();
        }

        private void TableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {




        }
    }

}
