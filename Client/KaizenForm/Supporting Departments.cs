using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaizenForm
{
    public partial class Supporting_Departments : Form
    {
        string mergedFileName = string.Empty;
        string mergedFileName1 = string.Empty;
        string mergedFileName2 = string.Empty;
        string fileName = string.Empty;

        public Supporting_Departments()
        {
            InitializeComponent();
        }
       
            public Supporting_Departments(string KAIZEN_HEADING, string DEPARTMENT_CODE, string MODEL, string PROPOSER_DESIGNATION, string SIX_S_BEFORE, string SIX_S_AFTER, string SIX_S_SAVINGS, string SIX_S_IMPROVED, string KAIZEN_NUMBER, string KAIZEN_DATE, string KAIZEN_TYPE, string PROPOSER_NAME, string PROPOSER_BARCODE, string PROPOSER_AREA, string PROPOSER_DEPARTMENT, string CW_NAME, string CW_BARCODE,
               string PROJECTED_AREA, string PROJECTED_DEPARTMENT, string PROJECTED_LINE, string BEFORE_KAIZEN, string AFTER_KAIZEN, string PROPOSER_LINE, string TYPE_ECRS, string STATUS)
            {

                InitializeComponent();
                textBox1.Text = KAIZEN_HEADING;
                textBox3.Text = DEPARTMENT_CODE;
                textBox29.Text = MODEL;
                textBox9.Text = PROPOSER_DESIGNATION;
                textBox11.Text = SIX_S_BEFORE;
                textBox12.Text = SIX_S_AFTER;
                textBox16.Text = SIX_S_SAVINGS;
                textBox18.Text = SIX_S_IMPROVED;
                textBox2.Text = KAIZEN_NUMBER;
                dateTimePicker1.Text = KAIZEN_DATE;
                dateTimePicker1.Enabled = false;
                comboBox4.Text = KAIZEN_TYPE;
                textBox5.Text = CW_NAME;
                comboBox5.Text = PROPOSER_DEPARTMENT;
                comboBox3.Text = PROJECTED_AREA;
                comboBox9.Text = PROJECTED_LINE;
                comboBox2.Text = TYPE_ECRS;
                textBox6.Text = CW_BARCODE;
                comboBox1.Text = PROJECTED_DEPARTMENT;
                comboBox7.Text = PROPOSER_AREA;
                comboBox8.Text = PROPOSER_LINE;
                textBox8.Text = PROPOSER_NAME;  // modified by hemanth
                textBox7.Text = PROPOSER_BARCODE; // modified by hemanth
                textBox23.Text = BEFORE_KAIZEN;
                textBox24.Text = AFTER_KAIZEN;

                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    //textBox3.Text = "";
                    //textBox4.Text = "";
                    // textBox3.Enabled = false;
                    //textBox4.Enabled = false;
                }
                else
                {
                    //textBox3.Enabled = true;
                    //textBox4.Enabled = true;
                }
                comboBox6.Text = STATUS;


            }
            private void _6S__Report_Load(object sender, EventArgs e)
            {
                LoadQueryItem();



            }



            public void LoadQueryItem()
            {
                Dictionary<string, object> p = new Dictionary<string, object>();

                p.Add("Proposer_department", comboBox5.Text);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        comboBox5.Items.Add(dtJson.Rows[i]["DEPARTMENT"].ToString());
                        comboBox1.Items.Add(dtJson.Rows[i]["DEPARTMENT"].ToString());


                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }



            }









            private void TableLayoutPanel4_Paint(object sender, PaintEventArgs e)
            {

            }

            private void Label21_Click(object sender, EventArgs e)
            {


            }





            private void TextBox12_TextChanged(object sender, EventArgs e)
            {
                double value11 = double.TryParse(textBox11.Text, out var result11) ? result11 : 0;
                double value12 = double.TryParse(textBox12.Text, out var result12) ? result12 : 0;
                textBox16.Text = (value12 - value11).ToString();
                double value16 = double.TryParse(textBox12.Text, out var result16) ? result16 : 0;
                textBox18.Text = (value16 / value11).ToString();


            }

            private void Button1_Click(object sender, EventArgs e)
            {

                string S6_Before = textBox11.Text;
                string S6_After = textBox12.Text;
                if (string.IsNullOrEmpty(Name))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your Name");
                    return;
                }

                if (string.IsNullOrEmpty(S6_Before))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "S6_Before");
                    return;
                }
                if (string.IsNullOrEmpty(S6_After))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your after_CTpair");
                    return;
                }

                Dictionary<string, object> p = new Dictionary<string, object>();

                p.Add("KaizenHeading", textBox1.Text);
                p.Add("Kaizen_number", textBox2.Text);
                p.Add("Kaizen_Type", comboBox4.Text);
                p.Add("Proposer_Department", comboBox5.Text);
                p.Add("Proposer_Area", comboBox7.Text);
                p.Add("Proposer_Line", comboBox8.Text);
                p.Add("Dept_Code", textBox3.Text);
                p.Add("Type(ECRS)", comboBox2.Text);
                p.Add("CW_Barcode", textBox6.Text);
                p.Add("CW_Name", textBox5.Text);
                p.Add("Projected_Department", comboBox1.Text);
                p.Add("Kaizen_Date", dateTimePicker1.Text);
                p.Add("Projected_Area", comboBox3.Text);
                p.Add("Projected_Line", comboBox9.Text);
                p.Add("Status", comboBox6.Text);
                p.Add("Before_Image", pictureBox1.Text);
                p.Add("After_Image", pictureBox2.Text);
                p.Add("Proposer_Pic", pictureBox3.Text);
                p.Add("Proposer_Barcode", textBox7.Text);
                p.Add("Proposer_Name", textBox8.Text);
                p.Add("Proposer_Designation", textBox9.Text);
                string Six_S_Before = textBox11.Text;
                if (Six_S_Before.EndsWith("%"))
                {
                    Six_S_Before = Six_S_Before.TrimEnd('%');
                }
                p.Add("Six_S_Before", Six_S_Before);
                string Six_S_After = textBox12.Text;
                if (Six_S_After.EndsWith("%"))
                {
                    Six_S_After = Six_S_After.TrimEnd('%');
                }
                p.Add("Six_S_After", Six_S_After);
                string Six_S_Savings = textBox16.Text;
                if (Six_S_Savings.EndsWith("%"))
                {
                    Six_S_Savings = Six_S_Savings.TrimEnd('%');
                }
                p.Add("Six_S_Savings", Six_S_Savings);
                string six_s_improved = textBox18.Text;
                if (six_s_improved.EndsWith("%"))
                {
                    six_s_improved = six_s_improved.TrimEnd('%');
                }
                p.Add("Six_S_Improved", six_s_improved);

                p.Add("Before_Kaizen", textBox23.Text);
                p.Add("After_Kaizen", textBox24.Text);
                p.Add("model", textBox29.Text);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "six_s_kaizen", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))

                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    if (json == "Failed")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data");
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data inserted Successfully");
                    }
                    clear();
                }
            }
            private void clear()
            {
                textBox2.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox5.Text = string.Empty;
                textBox6.Text = string.Empty;
                textBox8.Text = string.Empty;
                textBox7.Text = string.Empty;
                textBox9.Text = string.Empty;
                textBox23.Text = string.Empty;
                textBox24.Text = string.Empty;
                comboBox1.Text = string.Empty;
                comboBox2.Text = string.Empty;
                comboBox3.Text = string.Empty;
                comboBox4.Text = string.Empty;
                comboBox5.Text = string.Empty;
                comboBox6.Text = string.Empty;
                comboBox7.Text = string.Empty;
                comboBox8.Text = string.Empty;
                comboBox9.Text = string.Empty;
                textBox29.Text = string.Empty;
                textBox11.Text = string.Empty;
                textBox12.Text = string.Empty;
                textBox16.Text = string.Empty;
                textBox18.Text = string.Empty;
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                pictureBox3.Image = null;


            }

            private void Upload_imagebtn_Click(object sender, EventArgs e)
            {

                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Multiselect = true,
                    Title = "Please select image files",
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string kaizenID = textBox2.Text.Trim();
                    List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                    foreach (string file in ofd.FileNames)
                    {
                        try
                        {
                            byte[] fileContent = File.ReadAllBytes(file);
                            string safeFileName = Path.GetFileName(file);
                            string filePath = file;
                            string fileExtension = Path.GetExtension(file);
                            mergedFileName = $"{kaizenID}_BeforeKaizen{fileExtension}";
                            fileName = "BeforeKaizen";
                            UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                            if (res.IsSuccess)
                            {
                                var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                string Descipline_Score_Card = resultDIC["guid"].ToString();
                                //    //var webC = new System.Net.WebClient();
                                //    //string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                                //    //Image image = new Bitmap(webC.OpenRead(url));
                                Uploadimage_Score_Card(Descipline_Score_Card);
                            }
                            Dictionary<string, object> filedic = new Dictionary<string, object>
            {
                { "file_content", fileContent },
                { "file_name", mergedFileName }
            };

                            filediclist.Add(filedic);
                            using (MemoryStream ms = new MemoryStream(fileContent))
                            {
                                pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                            }
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            Console.WriteLine($"File processed: {mergedFileName}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //MessageBox.Show($"{filediclist.Count} files processed and renamed with Kaizen ID.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            public void Uploadimage_Score_Card(string file_guid)
            {
                try
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("Kaizen_number", textBox2.Text);
                    //data.Add("ProdMonth", dateTimePicker2.Text);
                    p.Add("file_guid", file_guid);
                    p.Add("mergedFileName", mergedFileName);
                    p.Add("mergedFileName1", mergedFileName1);
                    p.Add("mergedFileName2", mergedFileName2);
                    p.Add("fileName", fileName);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "InsertImages", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

                    if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Uploaded successfully!");
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, j["ErrMsg"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.client, Program.client.WebServiceUrl, Program.client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }

            private void Button2_Click(object sender, EventArgs e)
            {



                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Multiselect = true,
                    Title = "Please select image files",
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string kaizenID = textBox2.Text.Trim();
                    List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                    foreach (string file in ofd.FileNames)
                    {
                        try
                        {
                            byte[] fileContent = File.ReadAllBytes(file);
                            string safeFileName = Path.GetFileName(file);
                            string filePath = file;
                            string fileExtension = Path.GetExtension(file);
                            mergedFileName1 = $"{kaizenID}_AfterKaizen{fileExtension}";
                            fileName = "AfterKaizen";
                            UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                            if (res.IsSuccess)
                            {
                                var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                string Descipline_Score_Card = resultDIC["guid"].ToString();
                                //    //var webC = new System.Net.WebClient();
                                //    //string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                                //    //Image image = new Bitmap(webC.OpenRead(url));
                                Uploadimage_Score_Card(Descipline_Score_Card);
                            }
                            Dictionary<string, object> filedic = new Dictionary<string, object>
            {
                { "file_content", fileContent },
                { "file_name", mergedFileName1 }
            };

                            filediclist.Add(filedic);
                            using (MemoryStream ms = new MemoryStream(fileContent))
                            {
                                pictureBox2.Image = System.Drawing.Image.FromStream(ms);
                            }
                            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                            Console.WriteLine($"File processed: {mergedFileName}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // Optional: Display selected file names (trimming the last comma)
                    // MessageBox.Show("Files uploaded: " + selectedFiles.TrimEnd(','));
                }









            }

            private void Button5_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Multiselect = true,
                    Title = "Please select image files",
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string kaizenID = textBox2.Text.Trim();
                    List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                    foreach (string file in ofd.FileNames)
                    {
                        try
                        {
                            byte[] fileContent = File.ReadAllBytes(file);
                            string safeFileName = Path.GetFileName(file);
                            string filePath = file;
                            string fileExtension = Path.GetExtension(file);
                            mergedFileName2 = $"{kaizenID}_Proposer_pic{fileExtension}";
                            fileName = "Proposer_pic";
                            UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                            if (res.IsSuccess)
                            {
                                var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                                string Descipline_Score_Card = resultDIC["guid"].ToString();
                                //    //var webC = new System.Net.WebClient();
                                //    //string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                                //    //Image image = new Bitmap(webC.OpenRead(url));
                                Uploadimage_Score_Card(Descipline_Score_Card);
                            }
                            Dictionary<string, object> filedic = new Dictionary<string, object>
            {
                { "file_content", fileContent },
                { "file_name", mergedFileName1 }
            };

                            filediclist.Add(filedic);
                            using (MemoryStream ms = new MemoryStream(fileContent))
                            {
                                pictureBox3.Image = System.Drawing.Image.FromStream(ms);
                            }
                            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                            Console.WriteLine($"File processed: {mergedFileName}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }


                }
            }


        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();
            //List<string> strList = new List<string>();
            p.Add("department", comboBox5.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetAllcode", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                foreach (DataRow dr in dtJson.Rows)
                {

                    if (dtJson.Rows.Count > 0)
                    {
                        string departmentCodes = string.Empty; // Initialize an empty string


                        departmentCodes += dr["DEPARTMENT_CODE"].ToString() + "\n"; // Add each department code to the string, separated by new lines


                        textBox3.Text = departmentCodes.TrimEnd('\n'); // Set the concatenated string as the text of the TextBox
                    }
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }







        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("department", comboBox5.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "KaizenID", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {

                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                // Proceed with ID generation regardless of the row count
                string ID = string.Empty;
                int selectedIndex = comboBox4.SelectedIndex;
                if (selectedIndex == 0)
                {
                    ID = "A";
                }
                else
                {
                    ID = "B";
                }


                if (comboBox5.SelectedIndex == -1)  // ComboBox5 has no selection
                {
                    //MessageBox.Show("Select Department");
                }
                else
                {
                    // Append text from textBox3 if ComboBox5 is selected
                    ID += textBox3.Text;
                    ID += DateTime.Now.ToString("yyMMdd");
                    int rowCount = dtJson.Rows.Count;
                    if (rowCount < 10)
                    {
                        ID += "0";
                    }
                    ID += (rowCount + 1).ToString();
                    textBox2.Text = ID;

                }
            }

        }

        private void TextBox12_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                string value11Input = textBox11.Text.Trim();
                string value12Input = textBox12.Text.Trim();

                decimal value11 = ParsePercentage(value11Input, out bool isPercentage11);
                decimal value12 = ParsePercentage(value12Input, out bool isPercentage12);
                if (isPercentage11 != isPercentage12)
                {
                    // MessageBox.Show("Both inputs must either be percentages or numeric values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal result = ((value12 - value11) * 100);
                textBox16.Text = result.ToString("F2") + "%";
                //textBox16.Text = isPercentage11 ? (result * 100).ToString("F0") : result.ToString("F0")+ "%";
                decimal result1 = ((value12 - value11) / value11 * 100);
                textBox18.Text = result1.ToString("F2") + "%";
            }
            catch (FormatException ex)
            {
                //MessageBox.Show(ex.Message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            decimal ParsePercentage(string input, out bool isPercentage)
            {
                isPercentage = false;
                if (input.EndsWith("%"))
                {
                    isPercentage = true;
                    input = input.TrimEnd('%');
                    if (decimal.TryParse(input, out decimal percentage))
                    {
                        return percentage / 100; // Return decimal form of percentage
                    }
                    else
                    {
                        throw new FormatException($"Invalid percentage format: {input}");
                    }
                }
                else if (decimal.TryParse(input, out decimal value))
                {
                    return value;
                }
                else
                {
                    throw new FormatException($"Invalid number format: {input}");
                }
            }








        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "NO_images_Uploaded");
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Kaizen_number", textBox2.Text);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver",
                    "Get_images",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    foreach (DataRow row in dtJson.Rows)
                    {
                        string FILENAME = row["FILENAME"].ToString();
                        string FILEGUID = row["FILE_URL"].ToString();
                        string url = Program.client.PicUrl + FILEGUID;
                        string M = "BeforeKaizen";
                        string n = "AfterKaizen";

                        using (WebClient webClient = new WebClient())
                        {
                            byte[] imageBytes = webClient.DownloadData(url);
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                Image image = Image.FromStream(ms);

                                if (FILENAME == M)
                                {
                                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                    pictureBox1.Image = image;
                                }
                                else if (FILENAME == n)
                                {
                                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                                    pictureBox2.Image = image;
                                }
                                else
                                {
                                    pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                                    pictureBox3.Image = image;
                                }
                            }
                        }
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["ErrMsg"].ToString());
                }

            }


            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            //List<string> strList = new List<string>();
            string department = comboBox7.Text;

            if (!string.IsNullOrEmpty(department))
            {
                department = department.Substring(0, 1);
            }

            p.Add("department", department);
            string text = comboBox5.Text;

            if (!string.IsNullOrEmpty(text))
            {

                text = text.Replace("-", "").ToUpper();
            }

            p.Add("Plant", text);


            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetAllcode2", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                comboBox8.Items.Clear();

                if (dtJson.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtJson.Rows)
                    {
                        comboBox8.Items.Add(dr["DEPARTMENT_CODE"].ToString());

                    }
                }



            }







        }

        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CW_Barcode", textBox6.Text);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Details", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    for (int i = 0; i < dtJson.Rows.Count; i++)

                    {
                        textBox5.Text = dtJson.Rows[0]["EMP_NAME"].ToString();


                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }








            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();
            //List<string> strList = new List<string>();
            string department = comboBox3.Text;

            if (!string.IsNullOrEmpty(department))
            {
                department = department.Substring(0, 1);
            }

            p.Add("department", department);
            string text = comboBox1.Text;

            if (!string.IsNullOrEmpty(text))
            {

                text = text.Replace("-", "").ToUpper();
            }

            p.Add("Plant", text);


            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "GetAllcode2", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                comboBox9.Items.Clear();

                if (dtJson.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtJson.Rows)
                    {
                        comboBox9.Items.Add(dr["DEPARTMENT_CODE"].ToString());

                    }
                }



            }








        }

        private void TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CW_Barcode", textBox7.Text);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Details", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    for (int i = 0; i < dtJson.Rows.Count; i++)

                    {
                        textBox8.Text = dtJson.Rows[0]["EMP_NAME"].ToString();
                        textBox9.Text = dtJson.Rows[0]["WORK_NAME"].ToString();

                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }





            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "6s.frx");

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("KaizenNumber");
                dt.Columns.Add("KaizenHeading");

                dt.Columns.Add("DepartmentCode");
                dt.Columns.Add("ProjectedDept");
                dt.Columns.Add("ProposerDept");
                dt.Columns.Add("Date");
                dt.Columns.Add("Kaizen");
                dt.Columns.Add("ProjectArea");
                dt.Columns.Add("ProposerArea");
                dt.Columns.Add("TypeECRS");
                dt.Columns.Add("ProjectLine");
                dt.Columns.Add("ProposerLine");
                dt.Columns.Add("CWBarcode");
                dt.Columns.Add("CWName");
                dt.Columns.Add("Status");
                dt.Columns.Add("Model");

                dt.Columns.Add("ProposerName");
                dt.Columns.Add("ProposerBarcode");
                dt.Columns.Add("ProposerDesignation");

                dt.Columns.Add("BeforeText");
                dt.Columns.Add("AfterText");

                dt.Columns.Add("BeforeValue");
                dt.Columns.Add("AfterValue");
                dt.Columns.Add("Savings");
                dt.Columns.Add("ImprovementPercent");

                dt.Columns.Add("BeforeImage", typeof(byte[]));
                dt.Columns.Add("AfterImage", typeof(byte[]));
                dt.Columns.Add("ProposerImage", typeof(byte[]));

                DataRow row = dt.NewRow();
                row["KaizenNumber"] = textBox2.Text;
                row["KaizenHeading"] = textBox1.Text;
                row["DepartmentCode"] = textBox3.Text;
                row["ProjectedDept"] = comboBox1.Text;
                row["ProposerDept"] = comboBox5.Text;
                row["Date"] = dateTimePicker1.Text;
                row["Kaizen"] = comboBox4.Text;
                row["ProjectArea"] = comboBox3.Text;
                row["ProposerArea"] = comboBox7.Text;
                row["TypeECRS"] = comboBox2.Text;
                row["ProjectLine"] = comboBox9.Text;
                row["ProposerLine"] = comboBox8.Text;
                row["CWBarcode"] = textBox6.Text;
                row["CWName"] = textBox5.Text;
                row["Status"] = comboBox6.Text;
                row["Model"] = textBox29.Text;

                row["ProposerName"] = textBox8.Text;
                row["ProposerBarcode"] = textBox7.Text;
                row["ProposerDesignation"] = textBox9.Text;

                row["BeforeText"] = textBox23.Text;
                row["AfterText"] = textBox24.Text;
                row["BeforeValue"] = textBox11.Text;
                row["AfterValue"] = textBox12.Text;
                row["Savings"] = textBox16.Text;
                row["ImprovementPercent"] = textBox18.Text;

                if (pictureBox1.Image != null)
                    row["BeforeImage"] = ImageToByteArray(pictureBox1.Image);
                else
                    row["BeforeImage"] = DBNull.Value;

                if (pictureBox2.Image != null)
                    row["AfterImage"] = ImageToByteArray(pictureBox2.Image);
                else
                    row["AfterImage"] = DBNull.Value;

                if (pictureBox3.Image != null)
                    row["ProposerImage"] = ImageToByteArray(pictureBox3.Image);
                else
                    row["ProposerImage"] = DBNull.Value;

                dt.Rows.Add(row);
                //dt.Rows.Add(newRow);
                _6s_Previewcs file = new _6s_Previewcs(dt, path);
                file.Show();
                //Report report = new Report();
                //report.Load(path);

                //DataSet ds = new DataSet();
                //ds.Tables.Add(dt);
                //report.RegisterData(ds, "Proposal");
                //report.GetDataSource("Proposal").Enabled = true;

                //report.Show(); // use report.Print() to print directly
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(image)) // Clone the image safely
                {
                    bmp.Save(ms, ImageFormat.Png);
                }
                return ms.ToArray();
            }
        }

        private void ComboBox10_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Kaizen_number");
                return;
            }

             if (comboBox8.Text == "Other_Savings")
            {
                Others_Savings Form = new Others_Savings(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                string[] parts = value.Split(new string[] { " - " }, StringSplitOptions.None);

                if (parts.Length == 2)
                {
                    string name = parts[0];
                    // Assign the second part (value) to textBox32
                    string valuePart = parts[1];
                    label18.Visible = true;
                    textBox4.Visible = true;
                    // Bind name to textBox27 and value to textBox32
                    label18.Text = name;
                    textBox4.Text = valuePart;
                }
                else
                {
                    Console.WriteLine("Error: The result string does not have the expected format.");
                }

            }





        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Other_Savings", textBox4.Text);
            p.Add("Kaizen_number", textBox2.Text);                                                                                                                                                      
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_Savings_form", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))

            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                if (json == "Failed")
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data");
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data inserted Successfully");
                    clear();
                    textBox4.Visible = false;
                    label18.Visible = false;
                }

            }




        }
    }
    }
