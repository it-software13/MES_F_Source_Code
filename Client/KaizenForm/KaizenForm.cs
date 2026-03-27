using AutocompleteMenuNS;
using Newtonsoft.Json;
using SJeMES_Control_Library;
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
    public partial class KaizenForm : Form
    {
        string mergedFileName = string.Empty;
        string mergedFileName1 = string.Empty;
        string mergedFileName2 = string.Empty;
        string fileName = string.Empty;
        public object Items { get; private set; }
        public KaizenForm()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
        }

        public KaizenForm(string KAIZEN_HEADING, string CT_BEFORE, string CT_AFTER, string MANPOWER_BEFORE, string MANPOWER_AFTER, string MONTHLY_ORDER_QUANTITY, string DEPARTMENT_CODE, string MANPOWER_SAVED, string MANPOWER_IMPROVED, string MODEL, string KAIZEN_NUMBER, string KAIZEN_DATE, string KAIZEN_TYPE, string PROPOSER_NAME, string PROPOSER_BARCODE, string PROPOSER_AREA, string PROPOSER_DEPARTMENT, string CW_NAME, string CW_BARCODE,
            string PROJECTED_AREA, string PROJECTED_DEPARTMENT, string PROJECTED_LINE, string BEFORE_KAIZEN, string AFTER_KAIZEN, string OVERALL_CT_SAVINGS, string KAIZEN_BONUS, string TYPE_ECRS, string STATUS, string PROPOSER_DESIGNATION, string CT_SAVINGS, string BONUS_EVALUATION, string PROPOSER_LINE)
        {
            InitializeComponent();
            // Set the values in the textboxes
            textBox1.Text = KAIZEN_HEADING;
            textBox2.Text = KAIZEN_NUMBER;
            comboBox5.Text = PROPOSER_DEPARTMENT;
            comboBox5.SelectedValue = PROPOSER_DEPARTMENT.ToString() ;
            dateTimePicker1.Text = KAIZEN_DATE;
            dateTimePicker1.Enabled = false;
            comboBox4.Text = KAIZEN_TYPE; 
            comboBox7.Text = PROPOSER_AREA;
            comboBox9.Text = PROPOSER_LINE;
            comboBox2.Text = TYPE_ECRS;
            textBox6.Text = CW_BARCODE;
            textBox5.Text = CW_NAME;
            comboBox1.Text = PROJECTED_DEPARTMENT;          
            comboBox3.Text = PROJECTED_AREA;
            comboBox10.Text = PROJECTED_LINE;
            textBox7.Text = PROPOSER_BARCODE;
            textBox8.Text = PROPOSER_NAME;
            richTextBox1.Text = BEFORE_KAIZEN;
            richTextBox2.Text = AFTER_KAIZEN;
            //textBox9.Text = PROPOSER_AREA;
            textBox33.Text = OVERALL_CT_SAVINGS;
            textBox10.Text = KAIZEN_BONUS;
            comboBox6.Text = STATUS;
            textBox11.Text = CT_BEFORE;
            textBox12.Text = CT_AFTER;
            textBox13.Text = MANPOWER_BEFORE;
            textBox21.Text = MANPOWER_AFTER;
            textBox4.Text = MONTHLY_ORDER_QUANTITY;
            textBox3.Text = DEPARTMENT_CODE;
            textBox22.Text = MANPOWER_SAVED;
            textBox20.Text = MANPOWER_IMPROVED;
            textBox29.Text = MODEL;
            textBox9.Text = PROPOSER_DESIGNATION;
            textBox33.Text = CT_SAVINGS;
            textBox34.Text = BONUS_EVALUATION;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {

        }
        private void Upload_imagebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please Select Kaizen_Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            UploadDescipline_Score_Card(Descipline_Score_Card);
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

        private void TableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }

        public void UploadDescipline_Score_Card(string file_guid)
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


        public void Images_Data()
        {
            try
            {
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

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please Select Kaizen_Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            UploadDescipline_Score_Card(Descipline_Score_Card);
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

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please Select Kaizen_Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (string.IsNullOrWhiteSpace(pictureBox3.Image)))
            ////{
            ////     pictureBox3.Image
            ////}

            else {
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
                                UploadDescipline_Score_Card(Descipline_Score_Card);
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

                    // Optional: Display selected file names (trimming the last comma)
                    // MessageBox.Show("Files uploaded: " + selectedFiles.TrimEnd(','));
                }
            }
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void Kaizen_Form_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            // Define restricted period (26th of current month to 4th of next month)
            DateTime startDate = new DateTime(today.Year, today.Month, 26);
            DateTime endDate = startDate.AddMonths(1).AddDays(3); // 26th → +1 month → 26th next month, +3 days = 29th → adjust to 4th
            if (today >= startDate && today <= endDate)
            {
                MessageBox.Show("This form is not available between 26th and 4th.", "Access Restricted",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.Hide();   // Hide the form
                               // OR this.Close();  // If you want to completely close it
                return;
            }

            LoadQueryItem();
            Loadstandard();
            Images_Data();


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

        public void Loadstandard()
        {
            //label44.Visible = false;
            //textBox28.Visible = false;
            //label45.Visible = false;
            //textBox30.Visible = false;
            //label46.Visible = false;
            //textBox31.Visible = false;
            //label49.Visible = false;
            //textBox25.Visible = false;
            //label50.Visible = false;
            //textBox27.Visible = false;
            //label51.Visible = false;
            //textBox32.Visible = false;
            //textBox35.Visible = false;
            //textBox37.Visible = false;
            //textBox36.Visible = false;
            //label57.Visible = false;
            //label56.Visible = false;
            //label38.Visible = false;
            label48.Visible = false;
            label36.Visible = false;
            textBox26.Visible = false;

        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {

            double value11 = double.TryParse(textBox11.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox12.Text, out var result12) ? result12 : 0;
            textBox16.Text = (value11 - value12).ToString();
            double value16 = double.TryParse(textBox16.Text, out var result16) ? result16 : 0;
            textBox18.Text = ((value16 / value11) * 100).ToString() + "%";

            double result1 = (value16 / value11) * 100;
            if (double.IsInfinity(result1) || double.IsNaN(result1))
            {
                textBox18.Text = "0";
            }
            else
            {
                textBox18.Text = result1.ToString("0.00") + "%";
            }

            textBox33.Text = (value11 - value12).ToString();

            textBox15.Text = (3600 / value12).ToString("0.00");

            double result = 3600 / value12;
            if (Double.IsInfinity(result))
            {
                textBox15.Text = "0";
            }
            else
            {
                textBox15.Text = result.ToString();
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

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox14.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox15.Text, out var result12) ? result12 : 0;
            textBox19.Text = (value12 - value11).ToString();
            double value15 = double.TryParse(textBox19.Text, out var result16) ? result16 : 0;
            double percentage = ((value15 / value11) * 100);
            textBox17.Text = percentage.ToString("0.00") + "%";

            if (double.IsInfinity(percentage) || double.IsNaN(percentage))
            {
                textBox17.Text = "0";
            }
            else
            {
                textBox17.Text = percentage.ToString("0.00") + "%";
            }


        }

        private void TextBox22_TextChanged(object sender, EventArgs e)
        {

            double value11 = double.TryParse(textBox21.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox22.Text, out var result12) ? result12 : 0;         
            textBox20.Text = (value12 / value11).ToString();

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            //List<string> strList = new List<string>();
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
                    if (rowCount < 9)
                    {
                        ID += "0";
                    }
                    ID += (rowCount + 1).ToString();
                    textBox2.Text = ID;

                }
            }

        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox11.Text, out var result11) ? result11 : 0;
            // double value12 = double.TryParse(textBox15.Text, out var result12) ? result12 : 0;
            //textBox14.Text = ((3600 / value11)).ToString();


            double result = 3600 / value11;
            if (Double.IsInfinity(result))
            {
                textBox14.Text = "0";
            }
            else
            {
                textBox14.Text = result.ToString("0.00");

            }


        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox25.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox26.Text, out var result12) ? result12 : 0;
            textBox4.Text = ((value11 * value12) / 3600 * 49).ToString();
            if (decimal.TryParse(textBox4.Text, out decimal textBox4Value))
            {

                decimal percentageValue = textBox4Value * 3.5m / 100;
                textBox27.Text = percentageValue.ToString("F2");
                textBox10.Text = percentageValue.ToString("F2");
            }
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please Select Kaizen_Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Select a File",
                Filter = "All Files (*.*)|*.*",
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
                        fileName = $"{kaizenID}_Attachment_file{fileExtension}";
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            string Descipline_Score_Card = resultDIC["guid"].ToString();
                            //    //var webC = new System.Net.WebClient();
                            //    //string url = Program.client.PicUrl + Convert.ToString(resultDIC["url"].ToString());
                            //    //Image image = new Bitmap(webC.OpenRead(url));
                            UploadDescipline_Score_Card(Descipline_Score_Card);
                            label48.Text = fileName;

                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }


            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {




        }




        private void Button7_Click(object sender, EventArgs e)
        {
                string CTpair_Before = textBox11.Text;
            string CTpair_After = textBox12.Text;
            string order_quantity = textBox4.Text;
            if (string.IsNullOrEmpty(Name))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your Name");
                return;
            }

            if (string.IsNullOrEmpty(CTpair_Before))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your CTpair");
                return;
            }
            if (string.IsNullOrEmpty(CTpair_After))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your after_CTpair");
                return;
            }
            if (string.IsNullOrEmpty(order_quantity))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your order_quantity");
                return;
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select TypeECRS ");
                return;
            }
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Before Kaizen ");
                return;
            }
            if (string.IsNullOrEmpty(richTextBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter After Kaizen ");
                return;
            }

            if (string.IsNullOrEmpty(comboBox6.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, " Please select Status ");
                return;
            }



            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("KaizenHeading", textBox1.Text);
            p.Add("Kaizen_number", textBox2.Text);
            p.Add("Kaizen_Type", comboBox4.Text);
            p.Add("Proposer_Department", comboBox5.Text);
            p.Add("Proposer_area", comboBox7.Text);
            p.Add("Proposer_line", comboBox9.Text);
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
            p.Add("Bonus", textBox10.Text);
            p.Add("CT_Before", textBox11.Text);
            p.Add("CT_After", textBox12.Text);
            p.Add("CT_Savings", textBox16.Text);
            string ct_improved = textBox18.Text;
            if (ct_improved.EndsWith("%"))
            {
                ct_improved = ct_improved.TrimEnd('%');
            }
            p.Add("CT_Improved", ct_improved);
            p.Add("Output_Before", textBox14.Text);
            p.Add("Output_After", textBox15.Text);
            p.Add("Output_Saved", textBox19.Text);
            string output_improve = textBox17.Text;
            if (output_improve.EndsWith("%"))
            {
                output_improve = output_improve.TrimEnd('%');
            }
            p.Add("Output_Improve", output_improve);
            p.Add("Manpower_Before", textBox13.Text);
            p.Add("Manpower_After", textBox21.Text);
            p.Add("Manpower_Saved", textBox22.Text);
            string manpower_improved = textBox20.Text;
            if (manpower_improved.EndsWith("%"))
            {
                manpower_improved = manpower_improved.TrimEnd('%');
            }
            p.Add("Manpower_Improved", manpower_improved);
            p.Add("Monthly_Order_Quantity", textBox4.Text);
            p.Add("Overall_CT_Savings", textBox33.Text);
            p.Add("Overall_Savings", textBox26.Text);
            p.Add("Bonus_Evalution", textBox34.Text);
            p.Add("Attachments", label48.Text);
            p.Add("Before_Kaizen", richTextBox1.Text);
            p.Add("After_Kaizen", richTextBox2.Text);
            p.Add("model", textBox29.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_save_form", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
                }

            }
        }

        private void clear()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox16.Text = string.Empty;
            textBox18.Text = string.Empty;
            textBox14.Text = string.Empty;
            textBox15.Text = string.Empty;
            textBox19.Text = string.Empty;
            textBox17.Text = string.Empty;
            textBox21.Text = string.Empty;
            textBox29.Text = string.Empty;
            textBox22.Text = string.Empty;
            textBox25.Text = string.Empty;
            comboBox9.Text = string.Empty;
            comboBox10.Text = string.Empty;
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            comboBox6.Text = string.Empty;
            comboBox7.Text = string.Empty;
            comboBox8.Text = string.Empty;
            textBox27.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            textBox4.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            textBox20.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox22.Text = string.Empty;
            textBox20.Text = string.Empty;
            textBox33.Text = string.Empty;
            textBox34.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox26.Text = string.Empty;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            label48.Text = string.Empty;
        }

        private void TextBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label36_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label55_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {   
                textBox5.Clear();
        }

        private void Label53_Click(object sender, EventArgs e)
        {

        }



        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label18_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label54_Click(object sender, EventArgs e)
        {

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
                comboBox10.Items.Clear();
                if (dtJson.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtJson.Rows)
                    {
                        comboBox10.Items.Add(dr["DEPARTMENT_CODE"].ToString());

                    }
                }
            }


            }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void TextBox29_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TextBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label48_Click(object sender, EventArgs e)
        {

        }

        private void Label38_Click(object sender, EventArgs e)
        {

        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label37_Click(object sender, EventArgs e)
        {

        }

        private void Label44_Click(object sender, EventArgs e)
        {

        }

        private void Label43_Click(object sender, EventArgs e)
        {

        }

        private void Label45_Click(object sender, EventArgs e)
        {

        }

        private void Label46_Click(object sender, EventArgs e)
        {

        }

        private void Label49_Click(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label21_Click(object sender, EventArgs e)
        {




        }

        private void TableLayoutPanel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label33_Click(object sender, EventArgs e)
        {

        }

        private void Label32_Click(object sender, EventArgs e)
        {

        }

        private void Label31_Click(object sender, EventArgs e)
        {

        }

        private void Label30_Click(object sender, EventArgs e)
        {

        }

        private void Label29_Click(object sender, EventArgs e)
        {

        }

        private void Label28_Click(object sender, EventArgs e)
        {

        }

        private void Label34_Click(object sender, EventArgs e)
        {

        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox21_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox13.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox21.Text, out var result12) ? result12 : 0;
            textBox22.Text = (value11 - value12).ToString("0.00");
            double value16 = double.TryParse(textBox22.Text, out var result16) ? result16 : 0;
            textBox20.Text = ((value16 / value11) * 100).ToString("0.00") + "%";
            double percentage = ((value16 / value11) * 100);
            //textBox17.Text = percentage.ToString("0.00") + "%";

            if (double.IsInfinity(percentage) || double.IsNaN(percentage))
            {
                textBox20.Text = "0";
            }
            else
            {
                textBox20.Text = percentage.ToString("0.00") + "%";


            }

        }

        private void Label35_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label22_Click(object sender, EventArgs e)
        {

        }

        private void Label23_Click(object sender, EventArgs e)
        {

        }

        private void TextBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label24_Click(object sender, EventArgs e)
        {

        }

        private void Label25_Click(object sender, EventArgs e)
        {

        }

        private void Label27_Click(object sender, EventArgs e)
        {


        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label40_Click(object sender, EventArgs e)
        {

        }

        private void Label39_Click(object sender, EventArgs e)
        {

        }

        private void Label47_Click(object sender, EventArgs e)
        {

        }

        private void Label42_Click(object sender, EventArgs e)
        {

        }

        private void Label41_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Label26_Click(object sender, EventArgs e)
        {

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

            textBox8.Clear();
            textBox9.Clear();
            

        }

        private void TableLayoutPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            try
            {

                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Kaizen_number", textBox31.Text);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver",
                    "Get_images",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
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
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBox31_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("kaizen_number", textBox31.Text);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_details", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        textBox1.Text = dtJson.Rows[0]["KAIZEN_HEADING"].ToString();
                        richTextBox1.Text = dtJson.Rows[0]["BEFORE_KAIZEN"].ToString();
                        richTextBox2.Text = dtJson.Rows[0]["AFTER_KAIZEN"].ToString();
                        textBox11.Text = dtJson.Rows[0]["CT_BEFORE"].ToString();
                        textBox12.Text = dtJson.Rows[0]["CT_AFTER"].ToString();
                        textBox21.Text = dtJson.Rows[0]["MANPOWER_BEFORE"].ToString();
                        textBox25.Text = dtJson.Rows[0]["MONTHLY_ORDER_QUANTITY"].ToString();
                        textBox3.Text = dtJson.Rows[0]["DEPARTMENT_CODE"].ToString();
                        textBox6.Text = dtJson.Rows[0]["CW_BARCODE"].ToString();
                        textBox5.Text = dtJson.Rows[0]["CW_NAME"].ToString();
                        comboBox9.Text = dtJson.Rows[0]["PROJECTED_LINE"].ToString();
                        textBox7.Text = dtJson.Rows[0]["PROPOSER_NAME"].ToString();
                        textBox8.Text = dtJson.Rows[0]["PROPOSER_BARCODE"].ToString();
                        textBox9.Text = dtJson.Rows[0]["PROPOSER_DESIGNATION"].ToString();
                        textBox10.Text = dtJson.Rows[0]["KAIZEN_BONUS"].ToString();
                        textBox22.Text = dtJson.Rows[0]["MANPOWER_SAVED"].ToString();
                        textBox29.Text = dtJson.Rows[0]["MODEL"].ToString();
                        comboBox5.Items.Clear();
                        comboBox5.Enabled = false;
                        comboBox5.Items.Add(dtJson.Rows[0]["PROPOSER_DEPARTMENT"].ToString());
                        comboBox5.SelectedIndex = 0;
                        comboBox4.Items.Clear();
                        comboBox4.Enabled = false;
                        comboBox4.Items.Add(dtJson.Rows[0]["KAIZEN_TYPE"].ToString());
                        comboBox4.SelectedIndex = 0;
                        textBox2.Text = dtJson.Rows[0]["KAIZEN_NUMBER"].ToString();
                        comboBox2.Items.Clear();
                        comboBox2.Items.Add(dtJson.Rows[0]["TYPE_ECRS"].ToString());
                        comboBox2.SelectedIndex = 0;

                        comboBox1.Items.Clear();
                        comboBox1.Items.Add(dtJson.Rows[0]["PROJECTED_DEPARTMENT"].ToString());
                        comboBox1.SelectedIndex = 0;

                        comboBox3.Items.Clear();
                        comboBox3.Items.Add(dtJson.Rows[0]["PROJECTED_AREA"].ToString());
                        comboBox3.SelectedIndex = 0;

                        comboBox6.Items.Clear();
                        comboBox6.Items.Add(dtJson.Rows[0]["STATUS"].ToString());
                        comboBox6.SelectedIndex = 0;

                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }

        }

        private void TextBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CW_Barcode", textBox6.Text);
                p.Add("Proposer_Department", comboBox5.Text);
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

        private void ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void ComboBox8_SelectedIndexChanged_1(object sender, EventArgs e)
        {


        }


        private void ComboBox8_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Plase enter Kaizen_number");
                return;
            }

            if (comboBox8.Text == "Pallet_Savings")
            {
                Pallet_Saving_s Form = new Pallet_Saving_s(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                label44.Visible = true;
                textBox28.Visible = true;
                textBox28.Text = value;
            }
            else if (comboBox8.Text == "Power_Savings")
            {

                PowerSavings Form = new PowerSavings(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                label45.Visible = true;
                textBox30.Visible = true;
                textBox30.Text = value;
                //Form.Hide();                            
            }
            else if (comboBox8.Text == "Single Needle to CS M/C")
            {
                SingleNeedle Form = new SingleNeedle(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                label56.Visible = true;
                textBox36.Visible = true;
                textBox36.Text = value;

            }
            else if (comboBox8.Text == "Glue Savings")
            {
                Glue_Savings Form = new Glue_Savings(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                label49.Visible = true;
                textBox25.Visible = true;
                textBox25.Text = value;
            }
            else if (comboBox8.Text == "Material savings ")
            {
                Material_Savings Form = new Material_Savings(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                label46.Visible = true;
                textBox31.Visible = true;
                textBox31.Text = value;
            }

            else if (comboBox8.Text == "Chemical Savings")
            {
                Chemical_Savings Form = new Chemical_Savings(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                label38.Visible = true;
                textBox35.Visible = true;

                textBox35.Text = value;

            }

            else if (comboBox8.Text == "Tape Savings")
            {
                Tape_Savings Form = new Tape_Savings(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                label50.Visible = true;
                textBox32.Visible = true;
                textBox32.Text = value;

            }
            else if (comboBox8.Text == "Other_Savings")
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
                    label51.Visible = true;
                    textBox27.Visible = true;
                    // Bind name to textBox27 and value to textBox32
                    label51.Text = name;
                    textBox27.Text = valuePart;
                }
                else
                {

                    Console.WriteLine("Error: The result string does not have the expected format.");
                }

            }
            else if (comboBox8.Text == "ThreadSavings")

            {
                ThreadSavings Form = new ThreadSavings(textBox2.Text);
                Form.ShowDialog();
                string value = Form.Result;
                label57.Visible = true;
                textBox37.Visible = true;
                textBox37.Text = value;
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

        private void TextBox4_TextChanged_2(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox4.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox33.Text, out var result12) ? result12 : 0;
            textBox34.Text = ((value11 * value12) / 3600 * 49).ToString("0.00");
            //if (decimal.TryParse(textBox4.Text, out decimal textBox4Value))
            //{

            //    decimal percentageValue = textBox4Value * 3.5m / 100;
            //    //textBox34.Text = percentageValue.ToString("F2");
            //    textBox26.Text = percentageValue.ToString("F2");
            //    textBox10.Text = percentageValue.ToString("F2");
            //}
        }

        private void Button1_Click_2(object sender, EventArgs e)
        {

            try
            {
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


        private void TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please Select Kaizen_Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CW_Barcode", textBox7.Text);
                p.Add("Proposer_Department", comboBox5.Text);
                if (comboBox5.Text == "APEX")
                {

                }
                else
                {
                    LoadImageToPictureBox(textBox7.Text);
                }
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

        private void LoadImageToPictureBox(string barcode)
        {
             barcode = textBox7.Text.Trim();
            string kaizenID = textBox2.Text.Trim();
            string url = $@"http://10.3.0.208:8089/api/idmiss/GetEmployeeImagepop?barcode={barcode}";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (WebResponse response = request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (Image originalImage = Image.FromStream(stream))
                {
                    // Resize and display
                    Image resized = ResizeImageToFit(originalImage, pictureBox3.Width, pictureBox3.Height);
                    pictureBox3.Image = resized;
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Convert to byte array
                    using (MemoryStream ms = new MemoryStream())
                    {
                        string fileExtension = ".jpg"; // Assuming JPG from the API
                        //string mergedFileName2 = $"{kaizenID}_Proposer_pic{fileExtensi
                        mergedFileName2 = $"{kaizenID}_Proposer_pic{fileExtension}";
                         fileName = "Proposer_pic";
                        // Save resized image to memory stream
                        resized.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] fileContent = ms.ToArray();

                        // Write byte array to a temporary file for upload
                        string tempPath = Path.Combine(Path.GetTempPath(), mergedFileName2);
                        File.WriteAllBytes(tempPath, fileContent);

                        // Upload file
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(
                            Program.client.UploadUrl,
                            tempPath,
                            Program.client.UserToken
                        );

                        if (res.IsSuccess)
                        {
                            var resultDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            string Descipline_Score_Card = resultDict["guid"].ToString();

                            // Save reference
                            UploadDescipline_Score_Card(Descipline_Score_Card);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                pictureBox3.Image = null;
            }

        }
        private Image ResizeImageToFit(Image image, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);
            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
        private void TextBox28_TextChanged_1(object sender, EventArgs e)
        {

            standard_savings();
        }


        public void standard_savings()
        {
            double value11 = double.TryParse(textBox34.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox28.Text, out var result12) ? result12 : 0;
            double value13 = double.TryParse(textBox37.Text, out var result13) ? result13 : 0;
            double value14 = double.TryParse(textBox30.Text, out var result14) ? result14 : 0;
            double value15 = double.TryParse(textBox31.Text, out var result15) ? result15 : 0;
            double value16 = double.TryParse(textBox36.Text, out var result16) ? result16 : 0;
            double value17 = double.TryParse(textBox32.Text, out var result17) ? result17 : 0;
            double value18 = double.TryParse(textBox25.Text, out var result18) ? result18 : 0;
            double value19 = double.TryParse(textBox27.Text, out var result19) ? result19 : 0;
            double value20 = double.TryParse(textBox35.Text, out var result20) ? result20 : 0;
            textBox34.Text = (value11 + value12 + value13 + value14 + value15 + value16 + value17 + value18 + value19 + value20).ToString();

        }

        private void TextBox37_TextChanged(object sender, EventArgs e)
        {

            standard_savings();
        }

        private void TextBox30_TextChanged_1(object sender, EventArgs e)
        {
            standard_savings();
        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {
            standard_savings();
        }

        private void TextBox36_TextChanged(object sender, EventArgs e)
        {
            standard_savings();
        }

        private void TextBox32_TextChanged(object sender, EventArgs e)
        {
            standard_savings();
        }

        private void TextBox25_TextChanged_1(object sender, EventArgs e)
        {
            standard_savings();

        }

        private void TextBox27_TextChanged_1(object sender, EventArgs e)
        {
            standard_savings();

        }

        private void Label17_Click(object sender, EventArgs e)
        {

        }

        private void TextBox35_TextChanged(object sender, EventArgs e)
        {


        }

        //private void Button4_Click_1(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
        //       string.IsNullOrWhiteSpace(textBox11.Text) || string.IsNullOrWhiteSpace(textBox11.Text) ||
        //       string.IsNullOrWhiteSpace(textBox4.Text))
        //    {
        //        MessageBox.Show("Please fill in all required fields.");
        //        return;
        //    }

        //    // string path = @"E:\AEQS_Project_Related_Files\AEQS_Source_Code\deliveryFile\Client\T_Quick_Changeover\Qco_Sample_shoe_Fast_report.frx";
        //    string path = Path.Combine(Application.StartupPath, "KaizenForm", "Kaizen_form.frx");
        //    try
        //    {
        //        // Create and populate the DataTable
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("Kaizen #");
        //        dt.Columns.Add("KaizenHeading");
        //        dt.Columns.Add("Proposer_Department");
        //        dt.Columns.Add("Proposer_area");
        //        dt.Columns.Add("Proposer_line");
        //        dt.Columns.Add("Kaizen_Type");
        //        dt.Columns.Add("Dept_Code");
        //        dt.Columns.Add("Date");
        //        dt.Columns.Add("Projected_Area");
        //        dt.Columns.Add("Projected_Line");
        //        dt.Columns.Add("Status");
        //        dt.Columns.Add("Before_Image");
        //        dt.Columns.Add("After_Image");
        //        dt.Columns.Add("Proposer_Pic");
        //        dt.Columns.Add("Proposer_Barcode");
        //        dt.Columns.Add("Proposer_Name");
        //        dt.Columns.Add("Proposer_Designation");
        //        dt.Columns.Add("Bonus");
        //        dt.Columns.Add("CT_Before");
        //        dt.Columns.Add("CT_After");
        //        dt.Columns.Add("CT_Savings");
        //        dt.Columns.Add("CT_Improved");
        //        dt.Columns.Add("Output_Before");
        //        dt.Columns.Add("Output_After");
        //        dt.Columns.Add("Output_Saved");
        //        dt.Columns.Add("Output_Improve");
        //        dt.Columns.Add("Manpower_Before");
        //        dt.Columns.Add("Manpower_After");
        //        dt.Columns.Add("Manpower_Saved");
        //        dt.Columns.Add("Manpower_Improved");
        //        dt.Columns.Add("Monthly_Order_Quantity");
        //        dt.Columns.Add("Overall_CT_Savings");
        //        dt.Columns.Add("Overall_Savings");
        //        dt.Columns.Add("Bonus_Evalution");
        //        dt.Columns.Add("Before_Kaizen");
        //        dt.Columns.Add("After_Kaizen");
        //        dt.Columns.Add("model");
        //        dt.Columns.Add("Type(ECRS)");
        //        dt.Columns.Add("Projected_Department");
        //        dt.Columns.Add("CW_Barcode");
        //        dt.Columns.Add("CW_Name");

        //        DataRow newRow = dt.NewRow();
        //        newRow["Kaizen #"] = textBox2.Text;
        //        newRow["KaizenHeading"] = textBox1.Text;
        //        newRow["Proposer_Department"] = comboBox5.Text;
        //        newRow["Proposer_area"] = comboBox7.Text;
        //        newRow["Proposer_line"] = comboBox9.Text;
        //        newRow["Kaizen_Type"] = comboBox4.Text;
        //        newRow["Dept_Code"] = textBox3.Text;
        //        newRow["Date"] = dateTimePicker1.Text;
        //        newRow["Projected_Area"] = comboBox3.Text;
        //        newRow["Projected_Line"] = comboBox9.Text;
        //        newRow["Status"] = comboBox6.Text;
        //        newRow["Before_Image"] = pictureBox1.Image;
        //        newRow["After_Image"] = pictureBox2.Image;
        //        newRow["Proposer_Pic"] = pictureBox3.Image;
        //        newRow["Proposer_Barcode"] = textBox8.Text;
        //        newRow["Proposer_Name"] = textBox7.Text;
        //        newRow["Proposer_Designation"] = textBox9.Text;
        //        newRow["Bonus"] = textBox10.Text;
        //        newRow["CT_Before"] = textBox11.Text;
        //        newRow["CT_After"] = textBox12.Text;
        //        newRow["CT_Savings"] = textBox16.Text;
        //        newRow["CT_Improved"] = textBox18.Text;
        //        newRow["Output_Before"] = textBox14.Text;
        //        newRow["Output_After"] = textBox15.Text;
        //        newRow["Output_Saved"] = textBox19.Text;
        //        newRow["Output_Improve"] = textBox17.Text;
        //        newRow["Manpower_Before"] = textBox13.Text;
        //        newRow["Manpower_After"] = textBox21.Text;
        //        newRow["Manpower_Saved"] = textBox22.Text;
        //        newRow["Manpower_Improved"] = textBox20.Text;
        //        newRow["Monthly_Order_Quantity"] = textBox4.Text;
        //        newRow["Overall_CT_Savings"] = textBox33.Text;
        //        newRow["Overall_Savings"] = textBox26.Text;
        //        newRow["Bonus_Evalution"] = textBox34.Text;
        //        newRow["Before_Kaizen"] = textBox23.Text;
        //        newRow["After_Kaizen"] = textBox24.Text;
        //        newRow["model"] = textBox29.Text;
        //        newRow["Type(ECRS)"] = comboBox2.Text;
        //        newRow["Projected_Department"] = comboBox1.Text;
        //        newRow["CW_Barcode"] = textBox6.Text;
        //        newRow["CW_Name"] = textBox5.Text;


        //        dt.Rows.Add(newRow);
        //        Preview file = new Preview(dt, path);
        //        file.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}. Object not found: {ex.TargetSite?.Name}");
        //    }


        //}

        private void Button4_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Kaizen_form.frx");

            try
            {
                // Create and populate the DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("Kaizen #");
                dt.Columns.Add("KaizenHeading");
                dt.Columns.Add("Proposer_Department");
                dt.Columns.Add("Proposer_area");
                dt.Columns.Add("Proposer_line");
                dt.Columns.Add("Kaizen_Type");
                dt.Columns.Add("Dept_Code");
                dt.Columns.Add("Date");
                dt.Columns.Add("Projected_Area");
                dt.Columns.Add("Projected_Line");
                dt.Columns.Add("Status");
                dt.Columns.Add("Before_Image", typeof(byte[]));
                dt.Columns.Add("After_Image", typeof(byte[]));
                dt.Columns.Add("Proposer_Pic", typeof(byte[]));
                dt.Columns.Add("Proposer_Barcode");
                dt.Columns.Add("Proposer_Name");
                dt.Columns.Add("Proposer_Designation");
                dt.Columns.Add("Bonus");
                dt.Columns.Add("CT_Before");
                dt.Columns.Add("CT_After");
                dt.Columns.Add("CT_Savings");
                dt.Columns.Add("CT_Improved");
                dt.Columns.Add("Output_Before");
                dt.Columns.Add("Output_After");
                dt.Columns.Add("Output_Saved");
                dt.Columns.Add("Output_Improve");
                dt.Columns.Add("Manpower_Before");
                dt.Columns.Add("Manpower_After");
                dt.Columns.Add("Manpower_Saved");
                dt.Columns.Add("Manpower_Improved");
                dt.Columns.Add("Monthly_Order_Quantity");
                dt.Columns.Add("Overall_CT_Savings");
                dt.Columns.Add("Overall_Savings");
                dt.Columns.Add("Bonus_Evalution");
                dt.Columns.Add("Before_Kaizen");
                dt.Columns.Add("After_Kaizen");
                dt.Columns.Add("model");
                dt.Columns.Add("Type(ECRS)");
                dt.Columns.Add("Projected_Department");
                dt.Columns.Add("CW_Barcode");
                dt.Columns.Add("CW_Name");

                DataRow newRow = dt.NewRow();
                newRow["Kaizen #"] = textBox2.Text;
                newRow["KaizenHeading"] = textBox1.Text;
                newRow["Proposer_Department"] = comboBox5.Text;
                newRow["Proposer_area"] = comboBox7.Text;
                newRow["Proposer_line"] = comboBox9.Text;
                newRow["Kaizen_Type"] = comboBox4.Text;
                newRow["Dept_Code"] = textBox3.Text;
                newRow["Date"] = dateTimePicker1.Text;
                newRow["Projected_Area"] = comboBox3.Text;
                newRow["Projected_Line"] = comboBox9.Text;
                newRow["Status"] = comboBox6.Text;

                // Safe image conversion
                if (pictureBox1.Image != null)
                    newRow["Before_Image"] = ImageToByteArray(pictureBox1.Image);
                else
                    newRow["Before_Image"] = DBNull.Value;

                if (pictureBox2.Image != null)
                    newRow["After_Image"] = ImageToByteArray(pictureBox2.Image);
                else
                    newRow["After_Image"] = DBNull.Value;

                if (pictureBox3.Image != null)
                    newRow["Proposer_Pic"] = ImageToByteArray(pictureBox3.Image);
                else
                    newRow["Proposer_Pic"] = DBNull.Value;

                newRow["Proposer_Barcode"] = textBox7.Text;
                newRow["Proposer_Name"] = textBox8.Text;
                newRow["Proposer_Designation"] = textBox9.Text;
                newRow["Bonus"] = textBox10.Text;
                newRow["CT_Before"] = textBox11.Text;
                newRow["CT_After"] = textBox12.Text;
                newRow["CT_Savings"] = textBox16.Text;
                newRow["CT_Improved"] = textBox18.Text;
                newRow["Output_Before"] = textBox14.Text;
                newRow["Output_After"] = textBox15.Text;
                newRow["Output_Saved"] = textBox19.Text;
                newRow["Output_Improve"] = textBox17.Text;
                newRow["Manpower_Before"] = textBox13.Text;
                newRow["Manpower_After"] = textBox21.Text;
                newRow["Manpower_Saved"] = textBox22.Text;
                newRow["Manpower_Improved"] = textBox20.Text;
                newRow["Monthly_Order_Quantity"] = textBox4.Text;
                newRow["Overall_CT_Savings"] = textBox33.Text;
                newRow["Overall_Savings"] = textBox26.Text;
                newRow["Bonus_Evalution"] = textBox34.Text;
                newRow["Before_Kaizen"] = richTextBox1.Text;
                newRow["After_Kaizen"] = richTextBox2.Text;
                newRow["model"] = textBox29.Text;
                newRow["Type(ECRS)"] = comboBox2.Text;
                newRow["Projected_Department"] = comboBox1.Text;
                newRow["CW_Barcode"] = textBox6.Text;
                newRow["CW_Name"] = textBox5.Text;
                dt.Rows.Add(newRow);
                Preview file = new Preview(dt, path);
                file.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}. Object not found: {ex.TargetSite?.Name}");
            }
        }

        // Helper method to convert an image to a byte array
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



        private void Button8_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ThreadSavings", textBox37.Text);
            p.Add("Chemical_Savings", textBox35.Text);
            p.Add("Pallet_Savings", textBox28.Text);
            p.Add("Power_savings", textBox30.Text);
            p.Add("Material_savings", textBox31.Text);
            p.Add("Single_Needle_to_CS", textBox36.Text);
            p.Add("Tape_Savings", textBox32.Text);
            p.Add("Glue_Savings", textBox25.Text);
            p.Add("Other_Savings", textBox27.Text);
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
                    textBox37.Visible = false;
                    textBox35.Visible = false;
                    textBox28.Visible = false;
                    textBox30.Visible = false;
                    textBox31.Visible = false;

                    textBox36.Visible = false;
                    textBox32.Visible = false;
                    textBox27.Visible = false;
                    textBox25.Visible = false;
                    label51.Visible = false;
                    label49.Visible = false;
                    label50.Visible = false;
                    label56.Visible = false;
                    label46.Visible = false;
                    label45.Visible = false;
                    label44.Visible = false;
                    label57.Visible = false;
                    label38.Visible = false;

                }












            }
        }

        private void Button6_Click_1(object sender, EventArgs e)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Kaizen_number", textBox2.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_Savings_details", Program.client.UserToken, JsonConvert.SerializeObject(p));
             if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    textBox37.Text = dtJson.Rows[0]["THREAD_SAVINGS"].ToString();
                    textBox35.Text = dtJson.Rows[0]["CHEMICAL_SAVINGS"].ToString();
                    textBox28.Text = dtJson.Rows[0]["PALLET_SAVINGS"].ToString();
                    textBox30.Text = dtJson.Rows[0]["POWER_SAVINGS"].ToString();
                    textBox36.Text = dtJson.Rows[0]["SINGLE_NEEDLE_TO_CS"].ToString();
                    textBox31.Text = dtJson.Rows[0]["MATERIAL_SAVINGS"].ToString();
                    textBox32.Text = dtJson.Rows[0]["TAPE_SAVINGS"].ToString();
                    textBox25.Text = dtJson.Rows[0]["GLUE_SAVINGS"].ToString();
                    textBox27.Text = dtJson.Rows[0]["OTHER_SAVINGS"].ToString();
                    textBox37.Visible = !string.IsNullOrWhiteSpace(textBox37.Text); // THREAD_SAVINGS 
                    label57.Visible = !string.IsNullOrWhiteSpace(textBox37.Text);
                    textBox35.Visible = !string.IsNullOrWhiteSpace(textBox35.Text);
                    label38.Visible = !string.IsNullOrWhiteSpace(textBox35.Text);// CHEMICAL_SAVINGS
                    textBox28.Visible = !string.IsNullOrWhiteSpace(textBox28.Text);
                    label44.Visible = !string.IsNullOrWhiteSpace(textBox28.Text);// PALLET_SAVINGS
                    textBox30.Visible = !string.IsNullOrWhiteSpace(textBox30.Text);
                    label45.Visible = !string.IsNullOrWhiteSpace(textBox30.Text);  // POWER_SAVINGS
                    textBox36.Visible = !string.IsNullOrWhiteSpace(textBox36.Text);
                    label56.Visible = !string.IsNullOrWhiteSpace(textBox36.Text);// SINGLE_NEEDLE_TO_CS
                    textBox31.Visible = !string.IsNullOrWhiteSpace(textBox31.Text);
                    label46.Visible = !string.IsNullOrWhiteSpace(textBox31.Text);// MATERIAL_SAVINGS
                    textBox32.Visible = !string.IsNullOrWhiteSpace(textBox32.Text);
                    label50.Visible = !string.IsNullOrWhiteSpace(textBox32.Text);// TAPE_SAVINGS (was overwritten before)
                    textBox25.Visible = !string.IsNullOrWhiteSpace(textBox25.Text);
                    label49.Visible = !string.IsNullOrWhiteSpace(textBox25.Text);// GLUE_SAVINGS
                    textBox27.Visible = !string.IsNullOrWhiteSpace(textBox27.Text);
                    label51.Visible = !string.IsNullOrWhiteSpace(textBox27.Text);// OTHER_SAVINGS
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }






        }

        private void TextBox34_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox34.Text, out var value34))
            {
                double percentage = value34 * 0.035;
                textBox26.Text = percentage.ToString("0.00");
                textBox10.Text = percentage.ToString("0.00");
            }
            else
            {
                textBox26.Text = "0.00";
                textBox10.Text = "0.00";
            }

        }

        private void TableLayoutPanel3_Paint_1(object sender, PaintEventArgs e)
        {


        }

        private void Button1_Click_3(object sender, EventArgs e)
        {
            comboBox5.Text = string.Empty;
            comboBox9.Text = string.Empty;
            textBox2.Text = string.Empty;
            comboBox7.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox1.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox10.Text = string.Empty;
            comboBox4.Text = string.Empty;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;


        }
    }
}
    


