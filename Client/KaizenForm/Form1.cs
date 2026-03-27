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


namespace Kaizan_Project
{
    public partial class Power_Analysis : MaterialForm
    {
        private bool isUpdating = false; // Prevent infinite loop

        public Power_Analysis()
        {
            InitializeComponent();
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
            if (double.TryParse(CT, out double ct) && double.TryParse(Work_Hrs, out double total_Work_Hrs) && ct > 0 && total_Work_Hrs > 0)
                textBox5.Text = ((3600.0 / ct) * total_Work_Hrs).ToString("0.00");
            else
                textBox5.Clear();

            // Step 2: Required Machines
            if (double.TryParse(OrderQty, out double orderQty) && double.TryParse(textBox5.Text, out double TotalWork_Hours) && orderQty > 0 && TotalWork_Hours > 0)
                textBox6.Text = (orderQty / TotalWork_Hours).ToString("0.00");
            else
                textBox6.Clear();

            // Step 3: Total Power
            if (double.TryParse(textBox6.Text, out double ReqMachines) &&
                double.TryParse(PowerConsumption, out double power) &&
                double.TryParse(Work_Hrs, out double Work_Hours) &&
                ReqMachines > 0 && power > 0 && Work_Hours > 0)
                textBox7.Text = (ReqMachines * power * Work_Hours).ToString("0.00");
            else
                textBox7.Clear();

            // Step 4: Total Cost
            if (double.TryParse(textBox7.Text, out double totalPower) && double.TryParse(KW_Price, out double KWprice) && totalPower > 0 && KWprice > 0)
                textBox9.Text = (totalPower * KWprice).ToString("0.00");
            else
                textBox9.Clear();

            isUpdating = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }
        private void textBox2_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }
        private void textBox3_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }
        private void textBox4_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }
        private void textBox8_TextChanged(object sender, EventArgs e) { SN_Work_Hours_Output(); }


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
            if (double.TryParse(CT, out double ct) && double.TryParse(Work_Hrs, out double total_Work_Hrs) && ct > 0 && total_Work_Hrs > 0)
                textBox14.Text = ((3600.0 / ct) * total_Work_Hrs).ToString("0.00");
            else
                textBox14.Clear();

            // Step 2: Required Machines
            if (double.TryParse(OrderQty, out double orderQty) && double.TryParse(textBox14.Text, out double TotalWork_Hours) && orderQty > 0 && TotalWork_Hours > 0)
                textBox15.Text = (orderQty / TotalWork_Hours).ToString("0.00");
            else
                textBox15.Clear();

            // Step 3: Total Power
            if (double.TryParse(textBox15.Text, out double ReqMachines) &&
                double.TryParse(PowerConsumption, out double power) &&
                double.TryParse(Work_Hrs, out double Work_Hours) &&
                ReqMachines > 0 && power > 0 && Work_Hours > 0)
                textBox16.Text = (ReqMachines * power * Work_Hours).ToString("0.00");
            else
                textBox16.Clear();

            // Step 4: Total Cost
            if (double.TryParse(textBox16.Text, out double totalPower) && double.TryParse(KW_Price, out double KWprice) && totalPower > 0 && KWprice > 0)
                textBox18.Text = (totalPower * KWprice).ToString("0.00");
            else
                textBox18.Clear();

            isUpdating = false;
        }

        private void textBox10_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }

        private void textBox11_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }

        private void textBox12_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }

        private void textBox13_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }

        private void textBox17_TextChanged(object sender, EventArgs e) { CS_Power_Analysis(); }



        private void Pallet_Analysis()
        {
            if (isUpdating) { return; }
            isUpdating = true;

            string Total_Sizes = textBox19.Text;
            string FB_Dimensions = textBox20.Text;   // FB = Fiber Boards
            string Pallet_Dimension = textBox21.Text;
            string Hour_Output = textBox22.Text;
            string Order_Qty = textBox23.Text;
            string Working_Hours = textBox24.Text;
            //string No_Of_Pallets = textBox27.Text;
            string FB_Cost = textBox29.Text;

            // Step1: Each Line Output

            if (double.TryParse(Hour_Output, out double Per_Hour_Output) && double.TryParse(Working_Hours, out double WorkingHours) && Per_Hour_Output > 0 && WorkingHours > 0)

                textBox25.Text = (Per_Hour_Output * WorkingHours).ToString("0.00");
            else
                textBox25.Clear();

            // Step2: Required Machines
            if (double.TryParse(Order_Qty, out double OrderQty) && double.TryParse(textBox25.Text, out double LineOutput) && OrderQty > 0 && LineOutput > 0)
                textBox26.Text = (OrderQty / LineOutput).ToString("0.00");
            else
                textBox26.Clear();

            // Step3: No Of Pallets
            if (double.TryParse(textBox26.Text, out double RequiredMachines) && double.TryParse(Total_Sizes, out double TotalSizes) && RequiredMachines > 0 && TotalSizes > 0)
                textBox27.Text = ((RequiredMachines * 1) * TotalSizes).ToString("0.00");
            else
                textBox27.Clear();

            // Step4: No Of Fiber Boards
            if (double.TryParse(textBox27.Text, out double Pallets) && double.TryParse(Pallet_Dimension, out double PalletDimension) && double.TryParse(FB_Dimensions, out double FBdimensions) && Pallets > 0 && PalletDimension > 0 && FBdimensions > 0)
                textBox28.Text = ((Pallets * PalletDimension) / FBdimensions).ToString("0.00");
            else
                textBox28.Clear();

            // Step5: New Pallets Making Cost
            if (double.TryParse(textBox28.Text, out double NO_FB) && double.TryParse(FB_Cost, out double FBcost) && NO_FB > 0 && FBcost > 0)
                textBox30.Text = (NO_FB * FBcost).ToString("0.00");
            else
                textBox30.Clear();

            isUpdating = false;




        }

        private void textBox19_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        private void textBox20_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        private void textBox21_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        private void textBox22_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        private void textBox23_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        private void textBox24_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }
        private void textBox29_TextChanged(object sender, EventArgs e) { Pallet_Analysis(); }



        private void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Power_Analysis_Load(object sender, EventArgs e)
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
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (isUpdating) return;
            isUpdating = true;

            //  Power Analysis
            
            string powerConsumption = textBox1.Text.Trim();
            string cycleTime = textBox2.Text.Trim();
            string orderQty = textBox3.Text.Trim();
            string workHours = textBox4.Text.Trim();
            string totalWorkHours = textBox5.Text.Trim();
            string reqMachines = textBox6.Text.Trim();
            string totalPower = textBox7.Text.Trim();
            string kwPrice = textBox8.Text.Trim();
            string totalCost = textBox9.Text.Trim();

            //  CS Power Analysis
            string csPowerConsumption = textBox10.Text.Trim();
            string csCycleTime = textBox11.Text.Trim();
            string csOrderQty = textBox12.Text.Trim();
            string csWorkHours = textBox13.Text.Trim();
            string csTotalWorkHours = textBox14.Text.Trim();
            string csReqMachines = textBox15.Text.Trim();
            string csTotalPower = textBox16.Text.Trim();
            string csKwPrice = textBox17.Text.Trim();
            string csTotalCost = textBox18.Text.Trim();

            //  Pallet Analysis
            string totalSizes = textBox19.Text.Trim();
            string fbDimensions = textBox20.Text.Trim();
            string palletDimensions = textBox21.Text.Trim();
            string hourOutput = textBox22.Text.Trim();
            string orderQtyPallet = textBox23.Text.Trim();
            string workingHours = textBox24.Text.Trim();
            string eachLineOutput = textBox25.Text.Trim();
            string requiredMachines = textBox26.Text.Trim();
            string noOfPallets = textBox27.Text.Trim();
            string noOfFiberBoards = textBox28.Text.Trim();
            string fbCost = textBox29.Text.Trim();
            string newPalletCost = textBox30.Text.Trim();

            
            Dictionary<string, object> requestData = new Dictionary<string, object>
            {
                // Power Analysis Data
        
                { "power_consumption", powerConsumption },
                { "cycle_time", cycleTime },
                { "order_qty", orderQty },
                { "work_hours", workHours },
                { "total_work_hours", totalWorkHours },
                { "req_machines", reqMachines },
                { "total_power", totalPower },
                { "kw_price", kwPrice },
                { "total_cost", totalCost },

                // CS Power Analysis Data
                { "cs_power_consumption", csPowerConsumption },
                { "cs_cycle_time", csCycleTime },
                { "cs_order_qty", csOrderQty },
                { "cs_work_hours", csWorkHours },
                { "cs_total_work_hours", csTotalWorkHours },
                { "cs_req_machines", csReqMachines },
                { "cs_total_power", csTotalPower },
                { "cs_kw_price", csKwPrice },
                { "cs_total_cost", csTotalCost },

                // Pallet Analysis Data
                { "total_sizes", totalSizes },
                { "fb_dimensions", fbDimensions },
                { "pallet_dimensions", palletDimensions },
                { "hour_output", hourOutput },
                { "order_qty_pallet", orderQtyPallet },
                { "working_hours", workingHours },
                { "each_line_output", eachLineOutput },
                { "required_machines", requiredMachines },
                { "no_of_pallets", noOfPallets },
                { "no_of_fiber_boards", noOfFiberBoards },
                { "fb_cost", fbCost },
                { "new_pallet_cost", newPalletCost }
            };

            
            string response = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                                                                        "KZ_SFCAPI",
                                                                        "KZ_SFCAPI.Controllers.Machine_Power_AnalysisServer",
                                                                        "Insert_MachinePower",
                                                                        Program.client.UserToken,
                                                                        Newtonsoft.Json.JsonConvert.SerializeObject(requestData));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);

            if (result.IsSuccess)
            {
                MessageBox.Show("Data Inserted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ClearFields(); 
            }
            else
            {
                MessageBox.Show("Failed to Insert Data. Error: " + result.ErrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            isUpdating = false;

        }
    }
}
