using MaterialSkin.Controls;
using Newtonsoft.Json;
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
using System.IO;

namespace KaizenForm
{
    public partial class SingleNeedle : MaterialForm
    {
        private bool isUpdating = false;
        public string Result { get; private set; }
        public SingleNeedle(string kaizen_no)
        {
            InitializeComponent();
            textBox32.Text = kaizen_no;
        }

        private void SN_Work_Hours_Output()
        {
            if (isUpdating) return;
            isUpdating = true;

            string PowerConsumption = textBox1.Text;
            string CT = textBox2.Text;
            string OrderQty = textBox3.Text;
            string Work_Hrs = textBox4.Text;
            string KW_Price = textBox8.Text;

            // Step 1: Total Work Hours
            if (double.TryParse(CT, out double ct) && double.TryParse(Work_Hrs, out double total_Work_Hrs))
            {
                double totalOutput = (ct == 0) ? 0 : (3600.0 / ct) * total_Work_Hrs;
                textBox5.Text = totalOutput.ToString("0.00");
            }

            else
                textBox5.Clear();

            // Step 2: Required Machines
            if (double.TryParse(OrderQty, out double orderQty) && double.TryParse(textBox5.Text, out double TotalWork_Hours))
            {
                double machines = (TotalWork_Hours == 0) ? 0 : orderQty / TotalWork_Hours;
                textBox6.Text = machines.ToString("0.00");
            }

            else
                textBox6.Clear();

            // Step 3: Total Power
            if (double.TryParse(textBox6.Text, out double ReqMachines) &&
                double.TryParse(PowerConsumption, out double power) &&
                double.TryParse(Work_Hrs, out double Work_Hours))

                textBox7.Text = (ReqMachines * power * Work_Hours).ToString("0.00");
            else
                textBox7.Clear();

            // Step 4: Total Cost
            if (double.TryParse(textBox7.Text, out double totalPower) && double.TryParse(KW_Price, out double KWprice))
                textBox9.Text = (totalPower * KWprice).ToString("0.00");
            else
                textBox9.Clear();

            isUpdating = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }
        private void textBox2_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }
        private void textBox3_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }
        private void textBox4_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }
        private void TextBox8_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }


        private void CS_Power_Analysis()
        {
            if (isUpdating) return;
            isUpdating = true;

            string PowerConsumption = textBox10.Text;
            string CT = textBox11.Text;
            string OrderQty = textBox12.Text;
            string Work_Hrs = textBox13.Text;
            string KW_Price = textBox17.Text;

            // Step 1: Total Work Hours
            if (double.TryParse(CT, out double ct) && double.TryParse(Work_Hrs, out double total_Work_Hrs))
            {
                double totalOutput = (ct == 0) ? 0 : (3600.0 / ct) * total_Work_Hrs;
                textBox14.Text = totalOutput.ToString("0.00");
            }
            else
            {
                textBox14.Clear();
            }

            // Step 2: Required Machines
            if (double.TryParse(OrderQty, out double orderQty) && double.TryParse(textBox14.Text, out double totalWorkHours))
            {
                double machines = (totalWorkHours == 0) ? 0 : orderQty / totalWorkHours;
                textBox15.Text = machines.ToString("0.00");
            }
            else
            {
                textBox15.Clear();
            }

            // Step 3: Total Power
            if (double.TryParse(textBox15.Text, out double reqMachines) &&
                double.TryParse(PowerConsumption, out double power) &&
                double.TryParse(Work_Hrs, out double workHours))
            {
                double total_power = reqMachines * power * workHours;
                textBox16.Text = total_power.ToString("0.00");
            }
            else
            {
                textBox16.Clear();
            }
            // Step 4: Total Cost
            if (double.TryParse(textBox16.Text, out double totalPower) && double.TryParse(KW_Price, out double KWprice))
                textBox18.Text = (totalPower * KWprice).ToString("0.00");
            else
                textBox18.Clear();

            isUpdating = false;
        }

        private void TextBox10_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }

        private void TextBox11_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }

        private void TextBox12_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }

        private void TextBox13_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }

        private void TextBox17_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }



        //private void Pallet_Analysis()
        //{
        //    if (isUpdating) { return; }
        //    isUpdating = true;

        //    string Total_Sizes = textBox19.Text;
        //    string FB_Dimensions = textBox20.Text;   // FB = Fiber Boards
        //    string Pallet_Dimension = textBox21.Text;
        //    string Hour_Output = textBox22.Text;
        //    string Order_Qty = textBox23.Text;
        //    string Working_Hours = textBox24.Text;
        //    //string No_Of_Pallets = textBox27.Text;
        //    string FB_Cost = textBox29.Text;

        //    // Step1: Each Line Output

        //    if (double.TryParse(Hour_Output, out double Per_Hour_Output) && double.TryParse(Working_Hours, out double WorkingHours))

        //        textBox25.Text = (Per_Hour_Output * WorkingHours).ToString("0.00");
        //    else
        //        textBox25.Clear();

        //    // Step2: Required Machines
        //    if (double.TryParse(Order_Qty, out double OrderQty) && double.TryParse(textBox25.Text, out double LineOutput))
        //    {
        //        double result = LineOutput == 0 ? 0 : (OrderQty / LineOutput);
        //        textBox26.Text = result.ToString("0.00");
        //    }
        //    else
        //        textBox26.Clear();

        //    // Step3: No Of Pallets
        //    if (double.TryParse(textBox26.Text, out double RequiredMachines) && double.TryParse(textBox34.Text, out double TotalTopPallet) && double.TryParse(textBox19.Text, out double TotalSizes)
        //        && double.TryParse(textBox35.Text, out double TotalBottomPallet))
        //        textBox27.Text = ((RequiredMachines * TotalTopPallet) * TotalSizes + (RequiredMachines * TotalBottomPallet) * TotalSizes).ToString("0.00");
        //    else
        //        textBox27.Clear();

        //    // Step4: No Of Fiber Boards
        //    if (double.TryParse(textBox27.Text, out double Pallets) &&
        //        double.TryParse(textBox33.Text, out double TopPalletDimension) &&
        //        double.TryParse(textBox21.Text, out double BottomPalletDimension) &&
        //        double.TryParse(FB_Dimensions, out double FBdimensions))
        //    {
        //        double result = (FBdimensions == 0) ? 0 : ((Pallets * TopPalletDimension) / FBdimensions + (Pallets * BottomPalletDimension) / FBdimensions);
        //        textBox28.Text = result.ToString("0.00");
        //    }
        //    else
        //    {
        //        textBox28.Clear();
        //    }

        //    // Step5: New Pallets Making Cost
        //    if (double.TryParse(textBox28.Text, out double NO_FB) && double.TryParse(FB_Cost, out double FBcost))
        //        textBox30.Text = (NO_FB * FBcost).ToString("0.00");
        //    else
        //        textBox30.Clear();

        //    isUpdating = false;




        //}

        //private void TextBox19_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox20_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox21_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox33_TextChanged_1(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox34_TextChanged_1(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox35_TextChanged_1(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox22_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox23_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox24_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        //private void TextBox29_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }



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

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.KeyPress += NumericTextBox_KeyPress;
            textBox2.KeyPress += NumericTextBox_KeyPress;
            textBox3.KeyPress += NumericTextBox_KeyPress;
            textBox4.KeyPress += NumericTextBox_KeyPress;
            textBox8.KeyPress += NumericTextBox_KeyPress;
            textBox10.KeyPress += NumericTextBox_KeyPress;
            textBox11.KeyPress += NumericTextBox_KeyPress;
            textBox12.KeyPress += NumericTextBox_KeyPress;
            textBox13.KeyPress += NumericTextBox_KeyPress;
            textBox17.KeyPress += NumericTextBox_KeyPress;
            textBox19.KeyPress += NumericTextBox_KeyPress;
            textBox20.KeyPress += NumericTextBox_KeyPress;
            textBox21.KeyPress += NumericTextBox_KeyPress;
            textBox22.KeyPress += NumericTextBox_KeyPress;
            textBox23.KeyPress += NumericTextBox_KeyPress;
            textBox24.KeyPress += NumericTextBox_KeyPress;
            textBox29.KeyPress += NumericTextBox_KeyPress;
            textBox33.KeyPress += NumericTextBox_KeyPress;
            textBox34.KeyPress += NumericTextBox_KeyPress;
            textBox35.KeyPress += NumericTextBox_KeyPress;

            Update.Visible = false;
        }

        private void Submit_Click(object sender, EventArgs e)
        {

            HandleData("Insert_MachinePower", isUpdate: false);
        }


        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox24.Clear();
            textBox25.Clear();
            textBox26.Clear();
            textBox27.Clear();
            textBox28.Clear();
            textBox29.Clear();
            textBox30.Clear();
            textBox31.Clear();
            textBox32.Clear();
            textBox33.Clear();
            textBox34.Clear();
            textBox35.Clear();

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                string kaizenNum = textBox32.Text.Trim();

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
                                    "Get_MachinePower_Data",
                                    Program.client.UserToken,
                                    JsonConvert.SerializeObject(requestData)
                                    );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);

                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {

                        textBox1.Text = dataList[0]["SN_POWER_KW"].ToString();
                        textBox2.Text = dataList[0]["SN_CT"].ToString();
                        textBox3.Text = dataList[0]["SN_ORDER_QTY"].ToString();
                        textBox4.Text = dataList[0]["SN_WORK_HOURS"].ToString();
                        textBox5.Text = dataList[0]["SN_TOTAL_OUTPUT"].ToString();
                        textBox6.Text = dataList[0]["SN_REQ_MACHINES"].ToString();
                        textBox7.Text = dataList[0]["SN_TOTAL_POWER"].ToString();
                        textBox8.Text = dataList[0]["SN_KW_PRICE"].ToString();
                        textBox9.Text = dataList[0]["SN_TOTAL_COST"].ToString();

                        textBox10.Text = dataList[0]["CS_POWER_KW"].ToString();
                        textBox11.Text = dataList[0]["CS_CT"].ToString();
                        textBox12.Text = dataList[0]["CS_ORDER_QTY"].ToString();
                        textBox13.Text = dataList[0]["CS_WORK_HOURS"].ToString();
                        textBox14.Text = dataList[0]["CS_TOTAL_OUTPUT"].ToString();
                        textBox15.Text = dataList[0]["CS_REQ_MACHINES"].ToString();
                        textBox16.Text = dataList[0]["CS_TOTAL_POWER"].ToString();
                        textBox17.Text = dataList[0]["CS_KW_PRICE"].ToString();
                        textBox18.Text = dataList[0]["CS_TOTAL_COST"].ToString();

                        textBox19.Text = dataList[0]["PA_TOTAL_SIZES"].ToString();
                        textBox20.Text = dataList[0]["PA_FB_DIMENSION"].ToString();
                        textBox21.Text = dataList[0]["PA_DIMENSION"].ToString();
                        textBox33.Text = dataList[0]["PA_TOP_PALLET_DIMEN"].ToString();
                        textBox34.Text = dataList[0]["PA_TOP_PALLET"].ToString();
                        textBox35.Text = dataList[0]["PA_BOTTOM_PALLET"].ToString();
                        textBox22.Text = dataList[0]["PA_HOUR_OUTPUT"].ToString();
                        textBox23.Text = dataList[0]["PA_ORDER_QTY"].ToString();
                        textBox24.Text = dataList[0]["PA_WORKING_HRS"].ToString();
                        textBox25.Text = dataList[0]["PA_LINE_OUTPUT"].ToString();
                        textBox26.Text = dataList[0]["PA_REQ_MACHINES"].ToString();
                        textBox27.Text = dataList[0]["PA_NO_OF_PALLETS"].ToString();
                        textBox28.Text = dataList[0]["PA_NO_OF_FB"].ToString();
                        textBox29.Text = dataList[0]["PA_FB_COST"].ToString();
                        textBox30.Text = dataList[0]["PA_MAKING_COST"].ToString();

                        Edit.Visible = false;
                        Submit.Visible = false;
                        Update.Visible = true;

                        textBox32.Enabled = false;
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
            HandleData("Update_MachinePower", isUpdate: true);

            Edit.Visible = true;
            Submit.Visible = true;
            Update.Visible = false;

            textBox32.Enabled = true;
        }


        private void HandleData(string apiMethod, bool isUpdate)
        {
            if (isUpdating) return;
            isUpdating = true;

            try
            {
                string kaizenNum = textBox32.Text.Trim();
                if (string.IsNullOrWhiteSpace(kaizenNum))
                {
                    MessageBox.Show("Kaizen Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isUpdating = false;
                    return;
                }

                // Collect values
                string KaizenNum = textBox32.Text.Trim();
                string powerConsumption = textBox1.Text.Trim();
                string cycleTime = textBox2.Text.Trim();
                string orderQty = textBox3.Text.Trim();
                string workHours = textBox4.Text.Trim();
                string totalWorkHours = textBox5.Text.Trim();
                string reqMachines = textBox6.Text.Trim();
                string totalPower = textBox7.Text.Trim();
                string kwPrice = textBox8.Text.Trim();
                string totalCost = textBox9.Text.Trim();

                string csPowerConsumption = textBox10.Text.Trim();
                string csCycleTime = textBox11.Text.Trim();
                string csOrderQty = textBox12.Text.Trim();
                string csWorkHours = textBox13.Text.Trim();
                string csTotalWorkHours = textBox14.Text.Trim();
                string csReqMachines = textBox15.Text.Trim();
                string csTotalPower = textBox16.Text.Trim();
                string csKwPrice = textBox17.Text.Trim();
                string csTotalCost = textBox18.Text.Trim();

                string totalSizes = textBox19.Text.Trim();
                string Top_pallet_size1 = textBox20.Text.Trim();
                string Top_pallet_size2 = textBox21.Text.Trim();
                string Inside_Pallet_size1 = textBox33.Text.Trim();
                string Inside_Pallet_size2 = textBox34.Text.Trim();
                string Bottom_Pallet_Size1 = textBox35.Text.Trim();
                string Bottom_Pallet_Size2 = textBox22.Text.Trim();
                string Fiber_Board = textBox23.Text.Trim();
                string Top_Pallet_Dimension = textBox24.Text.Trim();
                string Inside_Pallet_Dimension = textBox25.Text.Trim();
                string Bottom_Pallet_Dimension = textBox26.Text.Trim();
                string Per_Hour_Output = textBox27.Text.Trim();
                string Order_Qty = textBox28.Text.Trim();
                string Working_Hours = textBox29.Text.Trim();
                string Each_line_Output = textBox30.Text.Trim();
                string Required_Machines = textBox36.Text.Trim();
                string No_Of_Top_Pallets = textBox37.Text.Trim();
                string No_Of_Inside_Pallets = textBox38.Text.Trim();
                string No_Of_Bottom_Pallets = textBox39.Text.Trim();
                string No_Of_Fiber_Boards = textBox40.Text.Trim();
                string Each_Fiber_Board_Cost = textBox41.Text.Trim();
                string Total_Cost = textBox42.Text.Trim();
                string total = textBox31.Text.Trim();

                // Unique keys dictionary
                Dictionary<string, object> requestData = new Dictionary<string, object>
        {
            // Power Analysis
            { "kaizen_num", KaizenNum },
            { "sn_power_consumption", powerConsumption },
            { "sn_cycle_time", cycleTime },
            { "sn_order_qty", orderQty },
            { "sn_work_hours", workHours },
            { "sn_total_output", totalWorkHours },
            { "sn_req_machines", reqMachines },
            { "sn_total_power", totalPower },
            { "sn_kw_price", kwPrice },
            { "sn_total_cost", totalCost },

            // CS Power Analysis
            { "cs_power_consumption", csPowerConsumption },
            { "cs_cycle_time", csCycleTime },
            { "cs_order_qty", csOrderQty },
            { "cs_work_hours", csWorkHours },
            { "cs_total_output", csTotalWorkHours },
            { "cs_req_machines", csReqMachines },
            { "cs_total_power", csTotalPower },
            { "cs_kw_price", csKwPrice },
            { "cs_total_cost", csTotalCost },

            // Pallet Analysis
            { "pa_total_sizes", totalSizes },
            { "pa_top_pallet_size1", Top_pallet_size1 },
            { "pa_top_pallet_size2", Top_pallet_size2 },
            { "pa_inside_pallet_size1", Inside_Pallet_size1 },
            { "pa_inside_pallet_size2", Inside_Pallet_size2 },
            { "pa_bottom_pallet_size1", Bottom_Pallet_Size1 },
            { "pa_bottom_pallet_size2", Bottom_Pallet_Size2 },
            { "pa_fiber_board", Fiber_Board },
            { "pa_top_pallet_dimen", Top_Pallet_Dimension },
            { "pa_inside_pallet_dimen", Inside_Pallet_Dimension },
            { "pa_bottom_pallet_dimen", Bottom_Pallet_Dimension },
            { "pa_hour_output", Per_Hour_Output },
            { "pa_order_qty", Order_Qty },
            { "pa_working_hrs", Working_Hours },
            { "pa_line_output", Each_line_Output },
            { "pa_req_machines", Required_Machines },
            { "pa_no_of_top_pallets", No_Of_Top_Pallets },
            { "pa_no_of_inside_pallets", No_Of_Inside_Pallets },
            { "pa_no_of_bottom_pallets", No_Of_Bottom_Pallets },
            { "pa_no_of_fb", No_Of_Fiber_Boards },
            { "pa_fb_cost", Each_Fiber_Board_Cost },
            { "pa_total_cost", Total_Cost },

            { "total", total }
        };

                string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_RTDMAPI",
                    "KZ_RTDMAPI.Controllers.Kaizenserver",
                    apiMethod,
                    Program.client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(requestData));

                var responseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

                if (Convert.ToBoolean(responseDict["IsSuccess"]))
                {
                    string value = responseDict["RetData"].ToString();
                    if (value == "Failed")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data");
                    }
                    else
                    {
                        Result = value;
                        ShowMessage(isUpdate ? "Data Updated Successfully!" : "Data Inserted Successfully!", "Success", MessageBoxIcon.Information);
                        ClearFields();
                        this.Close();
                    }
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

        private void TextBox41_TextChanged(object sender, EventArgs e) { total(); }

        private void TextBox9_TextChanged(object sender, EventArgs e) { total(); }

        private void TextBox18_TextChanged(object sender, EventArgs e) { total(); }


        private void total()
        {

            if (double.TryParse(textBox9.Text, out double SN_Cost) && double.TryParse(textBox18.Text, out double CS_Cost) && double.TryParse(textBox30.Text, out double Pallet_Cost))
                textBox31.Text = ((SN_Cost - CS_Cost) - Pallet_Cost).ToString("0.00");
            else
                textBox31.Clear();
        }

        private void Label37_Click(object sender, EventArgs e)
        {

        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {

        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredFields())
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Single Needle.frx");

            try
            {
                DataTable dt = CreateSingleNeedleDataTable();
                DataRow row = dt.NewRow();
                FillSingleNeedleRow(row);
                dt.Rows.Add(row);

                SingleNeedle_Preview previewForm = new SingleNeedle_Preview(dt, path);
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
                textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10,
                textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20,
                textBox21, textBox22, textBox23, textBox24, textBox25, textBox26, textBox27, textBox28, textBox29, textBox30, textBox31,textBox33, textBox34, textBox35

            };

            return requiredFields.All(tb => !string.IsNullOrWhiteSpace(tb.Text.Trim()));
        }

        private DataTable CreateSingleNeedleDataTable()
        {
            DataTable dt = new DataTable();

            string[] columns = {
                "KaizenNum", "powerConsumption", "cycleTime", "orderQty", "workHours", "totalWorkHours", "reqMachines", "totalPower", "kwPrice", "totalCost",
                "csPowerConsumption", "csCycleTime", "csOrderQty", "csWorkHours", "csTotalWorkHours", "csReqMachines", "csTotalPower", "csKwPrice", "csTotalCost",
                "totalSizes", "fbDimensions", "palletDimensions", "Top_Pallet_Dimen", "Top_Pallet", "Bottom_Pallet",
                "hourOutput", "orderQtyPallet", "workingHours", "eachLineOutput", "requiredMachines", "noOfPallets", "noOfFiberBoards", "fbCost", "newPalletCost", "total",
            };

            foreach (var col in columns)
                dt.Columns.Add(col, typeof(string));

            return dt;
        }


        private void FillSingleNeedleRow(DataRow row)
        {

            row["KaizenNum"] = textBox32.Text.Trim();
            row["powerConsumption"] = textBox1.Text.Trim();
            row["cycleTime"] = textBox2.Text.Trim();
            row["orderQty"] = textBox3.Text.Trim();
            row["workHours"] = textBox4.Text.Trim();
            row["totalWorkHours"] = textBox5.Text.Trim();
            row["reqMachines"] = textBox6.Text.Trim();
            row["totalPower"] = textBox7.Text.Trim();
            row["kwPrice"] = textBox8.Text.Trim();
            row["totalCost"] = textBox9.Text.Trim();

            //  CS Power Analysis
            row["csPowerConsumption"] = textBox10.Text.Trim();
            row["csCycleTime"] = textBox11.Text.Trim();
            row["csOrderQty"] = textBox12.Text.Trim();
            row["csWorkHours"] = textBox13.Text.Trim();
            row["csTotalWorkHours"] = textBox14.Text.Trim();
            row["csReqMachines"] = textBox15.Text.Trim();
            row["csTotalPower"] = textBox16.Text.Trim();
            row["csKwPrice"] = textBox17.Text.Trim();
            row["csTotalCost"] = textBox18.Text.Trim();

            //  Pallet Analysis
            row["totalSizes"] = textBox19.Text.Trim();
            row["fbDimensions"] = textBox20.Text.Trim();
            row["palletDimensions"] = textBox21.Text.Trim();
            row["Top_Pallet_Dimen"] = textBox33.Text.Trim();
            row["Top_Pallet"] = textBox34.Text.Trim();
            row["Bottom_Pallet"] = textBox35.Text.Trim();
            row["hourOutput"] = textBox22.Text.Trim();
            row["orderQtyPallet"] = textBox23.Text.Trim();
            row["workingHours"] = textBox24.Text.Trim();
            row["eachLineOutput"] = textBox25.Text.Trim();
            row["requiredMachines"] = textBox26.Text.Trim();
            row["noOfPallets"] = textBox27.Text.Trim();
            row["noOfFiberBoards"] = textBox28.Text.Trim();
            row["fbCost"] = textBox29.Text.Trim();
            row["newPalletCost"] = textBox30.Text.Trim();

            row["total"] = textBox31.Text.Trim();


        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox20.Text))
            {
                textBox21.Text = "";
                //textBox6.Text = "";
                textBox21.Enabled = false;
                //textBox6.Enabled = false;
            }
            else
            {
                textBox5.Enabled = true;
                //textBox6.Enabled = true;
            }

            Group_Methods();
        }





        private void Group_Methods()
        {
           

            Top_Pallets();
            Inside_Pallets();
            Bottom_Pallets();

            No_Of_FiberBoards();
        }

        private void Inside_Pallets()
        {
            if (isUpdating) return;
            isUpdating = true;

            double beforeTotal = 0;
           // double afterTotal = 0;

            bool hasValidBefore = false;
            //bool hasValidAfter = false;

            // Case 1
            if (double.TryParse(textBox33.Text, out double Before_inside_Pallet1) &&
                double.TryParse(textBox36.Text, out double Before_Required_Machines1))
            {
                beforeTotal += (Before_inside_Pallet1 * 1) * Before_Required_Machines1;
                hasValidBefore = true;
            }

           

            // Case 2
            if (double.TryParse(textBox34.Text, out double Before_inside_Pallet2) &&
                double.TryParse(textBox36.Text, out double Before_Required_Machines2))
            {
                beforeTotal += (Before_inside_Pallet2 * 2) * Before_Required_Machines2;
                hasValidBefore = true;
            }

            textBox38.Text = hasValidBefore ? beforeTotal.ToString("0.00") : string.Empty;
            

            isUpdating = false;
        }







        private void Bottom_Pallets()
        {
            if (isUpdating) return;
            isUpdating = true;
            double beforeTotal = 0;
            //double afterTotal = 0;
            bool hasValidBefore = false;
           // bool hasValidAfter = false;
            // Case 1
            if (double.TryParse(textBox35.Text, out double Before_Bottom_Pallet1) &&
                double.TryParse(textBox36.Text, out double Before_Required_Machines1))
            {
                beforeTotal += (Before_Bottom_Pallet1 * 1) * Before_Required_Machines1;
                hasValidBefore = true;
            }

            // Case 2
            if (double.TryParse(textBox22.Text, out double Before_Bottom_Pallet2) &&
                double.TryParse(textBox36.Text, out double Before_Required_Machines2))
            {
                beforeTotal += (Before_Bottom_Pallet2 * 2) * Before_Required_Machines2;
                hasValidBefore = true;
            }
            textBox39.Text = hasValidBefore ? beforeTotal.ToString("0.00") : string.Empty;
            //textBox38.Text = hasValidAfter ? afterTotal.ToString("0.00") : string.Empty;

            isUpdating = false;
        }



        private void No_Of_FiberBoards()
        {
            if (isUpdating) return;
            isUpdating = true;

            // Before
            if (double.TryParse(textBox37.Text, out double B_Top_Pallets) &&
                double.TryParse(textBox24.Text, out double B_Top_Pallet_Dimension) &&
                double.TryParse(textBox23.Text, out double B_FB_Dimen) &&
                double.TryParse(textBox38.Text, out double Before_Inside_Pallets) &&
                double.TryParse(textBox25.Text, out double B_Inside_Pallet_Dimen) &&
                double.TryParse(textBox39.Text, out double Before_Bottom_Pallets) &&
                double.TryParse(textBox26.Text, out double B_Bottom_Pallet_Dimen))
            {
                double result = (B_FB_Dimen == 0) ? 0 : ((B_Top_Pallets * B_Top_Pallet_Dimension) / B_FB_Dimen)
                              + ((Before_Inside_Pallets * B_Inside_Pallet_Dimen) / B_FB_Dimen)
                              + ((Before_Bottom_Pallets * B_Bottom_Pallet_Dimen) / B_FB_Dimen);
                textBox40.Text = result.ToString("0.00");
            }
            else
            {
                textBox40.Clear();
            }

            // After
            //if (double.TryParse(textBox34.Text, out double A_Top_Pallets) &&
            //    double.TryParse(textBox18.Text, out double A_Top_Pallet_Dimension) &&
            //    double.TryParse(textBox16.Text, out double A_FB_Dimen) &&
            //    double.TryParse(textBox36.Text, out double After_Inside_Pallets) &&
            //    double.TryParse(textBox20.Text, out double A_Inside_Pallet_Dimen) &&
            //    double.TryParse(textBox38.Text, out double After_Bottom_Pallets) &&
            //    double.TryParse(textBox22.Text, out double A_Bottom_Pallet_Dimen))
            //{
            //    double result = (A_FB_Dimen == 0) ? 0 : ((A_Top_Pallets * A_Top_Pallet_Dimension) / A_FB_Dimen)
            //                                              + ((After_Inside_Pallets * A_Inside_Pallet_Dimen) / A_FB_Dimen)
            //                                              + ((After_Bottom_Pallets * A_Bottom_Pallet_Dimen) / A_FB_Dimen);
            //    textBox40.Text = result.ToString("0.00");
            //}
            //else
            //{
            //    textBox40.Clear();
            //}

            // Before Total Cost
            if (double.TryParse(textBox41.Text, out double B_FB_Cost) &&
                double.TryParse(textBox39.Text, out double B_No_Of_FB))
            {
                //textBox43.Text = (B_FB_Cost * B_No_Of_FB).ToString("0.00");
            }
            else
            {
                //textBox43.Clear();
            }

            // After Total Cost
            

            isUpdating = false;
        }









        private void Top_Pallets()
        {
            if (isUpdating) return;
            isUpdating = true;

            double beforeTotal = 0;
           // double afterTotal = 0;

            bool hasValidBefore = false;
            //bool hasValidAfter = false;

            // Case - 1
            if (double.TryParse(textBox20.Text, out double Before_Top_Pallet1) &&
                double.TryParse(textBox36.Text, out double Before_Required_Machines1))
            {
                beforeTotal += (Before_Top_Pallet1 * 1) * Before_Required_Machines1;
                hasValidBefore = true;
            }

           

            // Case - 2
            if (double.TryParse(textBox21.Text, out double Before_Top_Pallet2) &&
                double.TryParse(textBox36.Text, out double Before_Required_Machines2))
            {
                beforeTotal += (Before_Top_Pallet2 * 2) * Before_Required_Machines2;
                hasValidBefore = true;
            }

            // Only show result if at least one valid pair exists
            textBox37.Text = hasValidBefore ? beforeTotal.ToString("0.00") : string.Empty;
            //textBox34.Text = hasValidAfter ? afterTotal.ToString("0.00") : string.Empty;

            isUpdating = false;
        }

        private void TextBox29_TextChanged(object sender, EventArgs e)
        {

            if (double.TryParse(textBox27.Text, out double work_Hours) &&
               double.TryParse(textBox29.Text, out double PerHour_Output))
            {
                textBox30.Text = (work_Hours * PerHour_Output).ToString("0.00");
            }
            else
            {
                textBox30.Clear();
            }


        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox28.Text, out double Before_Ord_Qty) &&
               double.TryParse(textBox30.Text, out double Before_Line_Output))
            {
                double result = (Before_Line_Output == 0) ? 0 : (Before_Ord_Qty / Before_Line_Output);
                textBox36.Text = result.ToString("0.00");
            }
            else
            {
                textBox36.Clear();
            }
        }

        private void TextBox42_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox42.Text, out double A_FB_Cost) &&
                double.TryParse(textBox40.Text, out double A_No_Of_FB))
            {
                textBox41.Text = (A_FB_Cost * A_No_Of_FB).ToString("0.00");
            }
            else
            {
                textBox41.Clear();
            }





        }

        private void TextBox36_TextChanged(object sender, EventArgs e)
        {
            Group_Methods();
        }

        private void TextBox37_TextChanged(object sender, EventArgs e)
        {
            Group_Methods();
        }

        private void TextBox38_TextChanged(object sender, EventArgs e)
        {
            Group_Methods();
        }

        private void TextBox39_TextChanged(object sender, EventArgs e)
        {
            Group_Methods();
        }

        private void TextBox40_TextChanged(object sender, EventArgs e)
        {
            Group_Methods();
        }

        private void TextBox33_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox33.Text))
            {
                textBox34.Text = "";
                textBox34.Enabled = false;
            }
            else
            {
                textBox34.Enabled = true;
            }

            Group_Methods();
        }

        private void TextBox35_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox35.Text))
            {
                textBox22.Text = "";
                textBox22.Enabled = false;
            }
            else
            {
                textBox22.Enabled = true;
            }

            Group_Methods();
        }

       
    }
}
