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

namespace KaizenForm
{
    public partial class Others_Savings : Form
    {
        public string Result { get; private set; }
        public Others_Savings(string kaizen_no)
        {
            InitializeComponent();
            textBox1.Text = kaizen_no;
        }



        private void Others_Savings_Load(object sender, EventArgs e)
        {

        }

        public class Data
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Kaizen number");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please_enter_name");
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please_enter_value");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Kaizen_Number", textBox1.Text);
            p.Add("name", textBox2.Text);
            p.Add("Value", textBox3.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_other_savings", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var responseDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
       

            if (Convert.ToBoolean(responseDict["IsSuccess"]))
            {
                // Extract RetData1 (which should be of type 'Data')
                var retData1 = responseDict["RetData1"];

                // If RetData1 is not null, attempt to deserialize it into the 'Data' class
                if (retData1 != null)
                {
                    // Deserialize retData1 into a 'Data' object
                    Data retData1Object = JsonConvert.DeserializeObject<Data>(retData1.ToString());

                    // If deserialization is successful, you can access 'Name' and 'Value'
                    if (retData1Object != null)
                    {
                        string name = retData1Object.Name;
                        string value = retData1Object.Value;

                        // Combine name and value into one string
                        Result = $"{name} - {value}";

                        
                    }
                    else
                    {
                        Console.WriteLine("Failed to deserialize RetData1 into Data object.");
                    }
                }
                else
                {
                    Console.WriteLine("RetData1 is null.");
                }

                // If operation is successful, show a success message
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data inserted successfully");

                // Clear any data after successful insertion
                cleardata();
                this.Close();
            }
            else
            {
                Console.WriteLine("API call failed or IsSuccess is false.");
            }
        }

    
            public void cleardata()
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }

        private void Button2_Click(object sender, EventArgs e)
        {

            try
            {
                string kaizenNum = textBox1.Text.Trim();

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
                                    "Get_other_Savings_Data",
                                    Program.client.UserToken,
                                    JsonConvert.SerializeObject(requestData)
                                    );

                ResultObject result = JsonConvert.DeserializeObject<ResultObject>(response);
                if (result.IsSuccess)
                {
                    List<Dictionary<string, object>> dataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.RetData);
                    if (dataList.Count > 0)
                    {
                        textBox2.Text = dataList[0]["NAME"].ToString();
                        textBox3.Text = dataList[0]["VALUE"].ToString();
                       
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
    }
    }


