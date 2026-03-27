using AutocompleteMenuNS;
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
    public partial class Quality_Kaizen : Form
    {
        string mergedFileName = string.Empty;
        string mergedFileName1 = string.Empty;
        string mergedFileName2 = string.Empty;
        string fileName = string.Empty;
        public object Items { get; private set; }



        public Quality_Kaizen()
        {
            InitializeComponent();
        }
        public Quality_Kaizen(string KAIZEN_HEADING, string DEPARTMENT_CODE, string MODEL, string PROPOSER_DESIGNATION, string PROPOSER_LINE, string BEFORE_INSPECTED_QTY, string AFTER_INSPECTED_QTY,
            string BEFORE_INSPECTED, string AFTER_INSPECTED, string RFT_BEFORE, string RFT_AFTER,
                        string RFT_BEFORE_textBox11, string RFT_AFTER_textBox12, string RFT_SAVINGS, string RFT_IMPROVED, string KAIZEN_NUMBER, string KAIZEN_DATE, string KAIZEN_TYPE, string PROPOSER_NAME, string PROPOSER_BARCODE, string PROPOSER_AREA, string PROPOSER_DEPARTMENT, string CW_NAME, string CW_BARCODE,
          string PROJECTED_AREA, string PROJECTED_DEPARTMENT, string PROJECTED_LINE, string BEFORE_KAIZEN, string AFTER_KAIZEN,  string TYPE_ECRS, string STATUS)
        {
            InitializeComponent();

            // Set the values in the textboxes
            textBox1.Text = KAIZEN_HEADING;
            textBox3.Text = DEPARTMENT_CODE;
            textBox29.Text = MODEL;         
            comboBox8.Text = PROPOSER_LINE;
            textBox4.Text = BEFORE_INSPECTED;
            textBox13.Text = AFTER_INSPECTED;
            textBox14.Text = BEFORE_INSPECTED_QTY;
            textBox15.Text = AFTER_INSPECTED_QTY;
            textBox17.Text = RFT_BEFORE;
            //textBox16.Text = RFT_SAVINGS;
            textBox18.Text = RFT_IMPROVED;
            textBox19.Text = RFT_AFTER;
            textBox11.Text = RFT_BEFORE_textBox11;
            textBox12.Text = RFT_AFTER_textBox12;
            textBox16.Text = RFT_SAVINGS;
            textBox18.Text = RFT_IMPROVED;
            textBox2.Text = KAIZEN_NUMBER;
            dateTimePicker1.Text = KAIZEN_DATE;
            dateTimePicker1.Enabled = false;
            comboBox4.Text = KAIZEN_TYPE;
            textBox6.Text = CW_BARCODE;
            textBox5.Text = CW_NAME;
            comboBox5.Text = PROPOSER_DEPARTMENT;
            comboBox7.Text = PROJECTED_AREA;
            comboBox9.Text = PROJECTED_LINE;
            comboBox2.Text = TYPE_ECRS;  
            comboBox1.Text = PROJECTED_DEPARTMENT;
            comboBox3.Text = PROJECTED_AREA;
            comboBox9.Text = PROJECTED_LINE;
            textBox8.Text = PROPOSER_BARCODE;
            textBox7.Text = PROPOSER_NAME;
            textBox9.Text = PROPOSER_DESIGNATION;
            richTextBox1.Text = BEFORE_KAIZEN;
            richTextBox2.Text = AFTER_KAIZEN;
            comboBox7.Text = PROPOSER_AREA;
            comboBox6.Text = STATUS;
        }

        private void Quality_Kaizen_Load(object sender, EventArgs e)
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
                        mergedFileName2 = $"{kaizenID}_AfterKaizen{fileExtension}";
                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(Program.client.UploadUrl, filePath, Program.client.UserToken);
                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            string Descipline_Score_Card = resultDIC["guid"].ToString();
                            UploadDescipline_Score_Card(Descipline_Score_Card);
                        }
                        Dictionary<string, object> filedic = new Dictionary<string, object>
            {
                { "file_content", fileContent },
                { "file_name", mergedFileName2 }
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

            }
        }

        private void TableLayoutPanel18_Paint(object sender, PaintEventArgs e)
        {

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

      


       

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox11.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox12.Text, out var result12) ? result12 : 0;
            // Perform subtraction and set the result to textBox16
            textBox16.Text = (value11 - value12).ToString();
            double value16 = double.TryParse(textBox12.Text, out var result16) ? result16 : 0;
            textBox18.Text = (value16 / value11).ToString();

        }

        

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {


            double value11 = double.TryParse(textBox14.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox15.Text, out var result12) ? result12 : 0;
            // Perform subtraction and set the result to textBox16
            textBox19.Text = (value12 - value11).ToString();
            double value15 = double.TryParse(textBox15.Text, out var result16) ? result16 : 0;
            textBox17.Text = (value15 / value11).ToString();
 
        }

        private void TextBox22_TextChanged(object sender, EventArgs e)
        {
              

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter the Kaizen ID in textBox2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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




     

        private void clear()
        {
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox16.Text = string.Empty;
            textBox14.Text = string.Empty;
            textBox18.Text = Convert.ToString(string.Empty);
            textBox15.Text = Convert.ToString(string.Empty);
            textBox19.Text = Convert.ToString(string.Empty);
            textBox17.Text = Convert.ToString(string.Empty);
            textBox13.Text = Convert.ToString(string.Empty);
            textBox4.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox5.Text = string.Empty;
            comboBox6.Text = string.Empty;
            comboBox7.Text = string.Empty;
            comboBox8.Text = string.Empty;
            textBox17.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox1.Text = string.Empty;
            //comboBox8.Text = string.Empty;
            comboBox9.Text = string.Empty;
            textBox29.Text = string.Empty;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
        }

        private void TableLayoutPanel7_Paint(object sender, PaintEventArgs e)
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

        private void TableLayoutPanel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label21_Click(object sender, EventArgs e)
        {

        }

        private void Label37_Click(object sender, EventArgs e)
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

        private void TextBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox9.Clear();
           
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {


            textBox5.Clear();
           

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox29_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click_1(object sender, EventArgs e)
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

        private void TableLayoutPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PictureBox3_Click(object sender, EventArgs e)
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

        private void TableLayoutPanel14_Paint(object sender, PaintEventArgs e)
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

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel18_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click_1(object sender, EventArgs e)
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

        private void TableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
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

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label53_Click(object sender, EventArgs e)
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

        private void Label10_Click_1(object sender, EventArgs e)
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

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click_1(object sender, EventArgs e)
        {

        }

        private void ComboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void ComboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel4_Paint(object sender, PaintEventArgs e)
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

        private void TableLayoutPanel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox12_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void TextBox11_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Label33_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {


        }

        private void TextBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {




        }



        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Label16_Click(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label17_Click(object sender, EventArgs e)
        {

        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label19_Click(object sender, EventArgs e)
        {

        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string value11Input = textBox4.Text.Trim();
                string value12Input = textBox14.Text.Trim();
                decimal value11 = ParsePercentage(value11Input);
                decimal value12 = ParsePercentage(value12Input);
                if (value11 == 0)
                {
                   // MessageBox.Show("Value11 cannot be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }
                decimal result = ((value11 - value12) / value11) * 100;
                textBox11.Text = result.ToString("F2") + "%";
                textBox17.Text = result.ToString("F2") + "%";
            }
            catch (FormatException ex)
            {
               //MessageBox.Show(ex.Message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            decimal ParsePercentage(string input)
            {
                if (input.EndsWith("%"))
                {
                    input = input.TrimEnd('%');
                    if (decimal.TryParse(input, out decimal percentage))
                    {
                        return percentage / 100; 
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


        private void Label20_Click(object sender, EventArgs e)
        {

        }

        private void TextBox15_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                string value11Input = textBox13.Text.Trim();
                string value12Input = textBox15.Text.Trim();
                decimal value11 = ParsePercentage(value11Input);
                decimal value12 = ParsePercentage(value12Input);
                if (value11 == 0)
                {
                    //MessageBox.Show("Value11 cannot be zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method to prevent division by zero
                }

                decimal result = ((value11 - value12) / value11) * 100;
                textBox19.Text = result.ToString("F2") + "%";
                textBox12.Text = result.ToString("F2") + "%";
            }
            catch (FormatException ex)
            {
                //MessageBox.Show(ex.Message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            decimal ParsePercentage(string input)
            {
                if (input.EndsWith("%"))
                {
                    input = input.TrimEnd('%');
                    if (decimal.TryParse(input, out decimal percentage))
                    {
                        return percentage / 100; // Convert percentage to decimal form
                    }
                    else
                    {
                        throw new FormatException($"Invalid percentage format: {input}");
                    }
                }
                else if (decimal.TryParse(input, out decimal value))
                {
                    return value; // Return raw value if no '%' sign
                }
                else
                {
                    throw new FormatException($"Invalid number format: {input}");
                }
            }
        }

        private void Label34_Click(object sender, EventArgs e)
        {

        }

        private void Label35_Click(object sender, EventArgs e)
        {

        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label36_Click(object sender, EventArgs e)
        {

        }

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string value11Input = textBox17.Text.Trim();
                string value12Input = textBox19.Text.Trim();

                decimal value11 = ParsePercentage(value11Input, out bool isPercentage11);
                decimal value12 = ParsePercentage(value12Input, out bool isPercentage12);


                if (isPercentage11 != isPercentage12)
                {
                    // MessageBox.Show("Both inputs must either be percentages or numeric values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal result = ((value12 - value11) *100);
                textBox16.Text = result.ToString("F2") + "%";
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
























        //private void TextBox20_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {

        //        Dictionary<string, object> p = new Dictionary<string, object>();
        //        p.Add("kaizen_number", textBox20.Text);
        //        string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Kaizen_details", Program.client.UserToken, JsonConvert.SerializeObject(p));
        //        if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //        {
        //            string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
        //            DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
        //            for (int i = 0; i < dtJson.Rows.Count; i++)
        //            {
        //                textBox1.Text = dtJson.Rows[0]["KAIZEN_HEADING"].ToString();
        //                textBox23.Text = dtJson.Rows[0]["BEFORE_KAIZEN"].ToString();
        //                textBox24.Text = dtJson.Rows[0]["AFTER_KAIZEN"].ToString();
        //                textBox3.Text = dtJson.Rows[0]["DEPARTMENT_CODE"].ToString();
        //                textBox6.Text = dtJson.Rows[0]["CW_BARCODE"].ToString();
        //                textBox5.Text = dtJson.Rows[0]["CW_NAME"].ToString();
        //                //textBox30.Text = dtJson.Rows[0]["PROJECTED_LINE"].ToString();
        //                textBox29.Text = dtJson.Rows[0]["MODEL"].ToString();
        //                textBox7.Text = dtJson.Rows[0]["PROPOSER_NAME"].ToString();
        //                textBox8.Text = dtJson.Rows[0]["PROPOSER_BARCODE"].ToString();
        //                textBox9.Text = dtJson.Rows[0]["PROPOSER_DESIGNATION"].ToString();
        //                textBox14.Text = dtJson.Rows[0]["BEFORE_INSPECTED_QTY"].ToString();
        //                textBox15.Text = dtJson.Rows[0]["AFTER_INSPECTED_QTY"].ToString();
        //                textBox4.Text = dtJson.Rows[0]["BEFORE_INSPECTED"].ToString();
        //                textBox13.Text = dtJson.Rows[0]["AFTER_INSPECTED"].ToString();
        //                textBox17.Text = dtJson.Rows[0]["RFT_BEFORE"].ToString() + "%";
        //                textBox19.Text = dtJson.Rows[0]["RFT_AFTER"].ToString() + "%";
        //                textBox11.Text = dtJson.Rows[0]["RFT_BEFORE"].ToString() + "%";
        //                textBox12.Text = dtJson.Rows[0]["RFT_AFTER"].ToString() + "%";
        //                textBox16.Text = dtJson.Rows[0]["RFT_SAVINGS"].ToString();
        //                textBox18.Text = dtJson.Rows[0]["RFT_IMPROVED"].ToString() + "%";
        //                comboBox5.Items.Clear();
        //                comboBox5.Items.Add(dtJson.Rows[0]["PROPOSER_DEPARTMENT"].ToString());
        //                comboBox5.SelectedIndex = 0; 
        //                comboBox4.Items.Clear();
        //                comboBox4.Items.Add(dtJson.Rows[0]["KAIZEN_TYPE"].ToString());
        //                comboBox4.SelectedIndex = 0;
        //                textBox2.Text = dtJson.Rows[0]["KAIZEN_NUMBER"].ToString();
        //                comboBox2.Items.Clear();
        //                comboBox2.Items.Add(dtJson.Rows[0]["TYPE_ECRS"].ToString());
        //                comboBox2.SelectedIndex = 0;
        //                comboBox1.Items.Clear();
        //                comboBox1.Items.Add(dtJson.Rows[0]["PROJECTED_DEPARTMENT"].ToString());
        //                comboBox1.SelectedIndex = 0;
        //                comboBox3.Items.Clear();
        //                comboBox3.Items.Add(dtJson.Rows[0]["PROJECTED_AREA"].ToString());
        //                comboBox3.SelectedIndex = 0;
        //                comboBox6.Items.Clear();
        //                comboBox6.Items.Add(dtJson.Rows[0]["STATUS"].ToString());
        //                comboBox6.SelectedIndex = 0;

        //            }
        //        }
        //        else
        //        {
        //            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //        }

        //    }
        //}

        private void Button1_Click(object sender, EventArgs e)
        {

            string RFTpair_Before = textBox11.Text;
            string RFTpair_After = textBox12.Text;
            if (string.IsNullOrEmpty(Name))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your Name");
                return;
            }

            if (string.IsNullOrEmpty(RFTpair_Before))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "RFTpair_Beforer");
                return;
            }
            if (string.IsNullOrEmpty(RFTpair_After))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your after_CTpair");
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

            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select TypeECRS ");
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
            p.Add("Proposer_line", comboBox8.Text);
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
            p.Add("Proposer_Barcode", textBox8.Text);
            p.Add("Proposer_Name", textBox7.Text);
            p.Add("Proposer_Designation", textBox9.Text);
            string RFT_Before = textBox11.Text;   
            if (RFT_Before.EndsWith("%"))
            {
                RFT_Before = RFT_Before.TrimEnd('%');
            }
            p.Add("RFT_Before", RFT_Before);
            string RFT_After = textBox12.Text;
            if (RFT_After.EndsWith("%"))
            {
                RFT_After = RFT_Before.TrimEnd('%');
            }
            p.Add("RFT_After", RFT_After);
            string RFT_Savings = textBox16.Text;
            if (RFT_Savings.EndsWith("%"))
            {
                RFT_Savings = RFT_Savings.TrimEnd('%');
            }
            p.Add("RFT_Savings", RFT_Savings);
           string RFT_Improved = textBox18.Text;
            if (RFT_Improved.EndsWith("%"))
            {
                RFT_Improved = RFT_Before.TrimEnd('%');
            }
            p.Add("RFT_Improved", RFT_Improved);
            p.Add("Before_Kaizen", richTextBox1.Text);
            p.Add("After_Kaizen", richTextBox2.Text);
            string Before_Inspected_qty = textBox14.Text;
            if (Before_Inspected_qty.EndsWith("%"))
            {
                Before_Inspected_qty = Before_Inspected_qty.TrimEnd('%');
            }
            p.Add("Before_Inspected_qty", Before_Inspected_qty);
            string After_Inspected_qty = textBox15.Text;
            if (After_Inspected_qty.EndsWith("%"))
            {
                After_Inspected_qty = After_Inspected_qty.TrimEnd('%');
            }
            p.Add("After_Inspected_qty", After_Inspected_qty);
            string Before_Inspected = textBox4.Text;
            if (Before_Inspected.EndsWith("%"))
            {
                Before_Inspected = Before_Inspected.TrimEnd('%');
            }
            p.Add("Before_Inspected", Before_Inspected);
            string After_Inspected = textBox13.Text;
            if (After_Inspected.EndsWith("%"))
            {
                After_Inspected = After_Inspected.TrimEnd('%');
            }
            p.Add("After_Inspected", After_Inspected);
            p.Add("model", textBox29.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Quality_kaizen", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private void Button6_Click_1(object sender, EventArgs e)
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

        private void TextBox8_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("CW_Barcode", textBox8.Text);
                LoadImageToPictureBox(textBox8.Text);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.Kaizenserver", "Details", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    for (int i = 0; i < dtJson.Rows.Count; i++)

                    {
                        textBox7.Text = dtJson.Rows[0]["EMP_NAME"].ToString();
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
            barcode = textBox8.Text.Trim();
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
        private void Button3_Click_2(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            string path = Path.Combine(Application.StartupPath, "KaizenForm", "Quality.frx");

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
                dt.Columns.Add("BeforeRft");
                dt.Columns.Add("AfterRft");
                dt.Columns.Add("SavingsRft");
                dt.Columns.Add("ImprovedRft");
                dt.Columns.Add("Before_inspected");
                dt.Columns.Add("After_inspected");
                dt.Columns.Add("Before_Defects_QTY");
                dt.Columns.Add("After_Defectes_QTY");
                dt.Columns.Add("Before_RFT(%)");
                dt.Columns.Add("After_RFT(%)");
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

                row["BeforeText"] = richTextBox1.Text;
                row["AfterText"] = richTextBox2.Text;
                row["BeforeRft"] = textBox11.Text;
                row["AfterRft"] = textBox12.Text;
                row["SavingsRft"] = textBox16.Text;
                row["ImprovedRft"] = textBox18.Text;
                row["Before_inspected"] = textBox4.Text;
                row["After_inspected"] = textBox13.Text;
                row["Before_Defects_QTY"] = textBox14.Text;
                row["After_Defectes_QTY"] = textBox15.Text;
                row["Before_RFT(%)"] = textBox17.Text;
                row["After_RFT(%)"] = textBox19.Text;
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
                Quality_Preview file = new Quality_Preview(dt, path);
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

        private void Button4_Click_1(object sender, EventArgs e)
        {
            comboBox5.Text = string.Empty;
            comboBox8.Text = string.Empty;
            textBox2.Text = string.Empty;
            comboBox9.Text = string.Empty;
            comboBox4.Text = string.Empty;
            comboBox1.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox7.Text = string.Empty;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;

        }
    }
    }


