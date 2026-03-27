using Newtonsoft.Json;
using SJeMES_Control_Library;
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

namespace SJeMES_TSM
{
    public partial class Signature_Upload : Form
    {
        public List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();
        public Dictionary<string, object> filedic = new Dictionary<string, object>();
        byte[] fileContent;
        DataTable dt;
        public int receivedIndex;
        public int recieved;
        public Signature_Upload(int index, int f)
        {
            InitializeComponent();
            receivedIndex = index;
            recieved = f;
        } 
        private void SaveToDatabase(string username, string password,string Designation_Code, string Designation_Name,byte[] image)
        {
            try
            {
                string base64Image = Convert.ToBase64String(image);

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("username", username);
                data.Add("password", password);
                data.Add("Designation_Code", Designation_Code);
                data.Add("Designation_Name", Designation_Name);
                data.Add("image", fileContent);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJ_TSMAPI",//类库名
                                       "SJ_TSMAPI.Skill_Score_Evaluation",//类名
                                       "InsertSignature",//方法名s
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg); 
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg); 
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            Barcodetxt.Text = "";
            Pwdtxt.Text = "";
            cbdesignation.Text = "";
            pictureBox1.Image = null;

        }
        private bool ValidateCredentials(string username, string password,string receivedIndex)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("username", username);
                data.Add("password", password);
                data.Add("receivedIndex", receivedIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJ_TSMAPI",//类库名
                                       "SJ_TSMAPI.Skill_Score_Evaluation",//类名
                                       "ValidateCredentials",//方法名s
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(data));  
                return Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating credentials: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //private void DisplayImage1(string username, string password)
        //{
        //    try
        //    {
        //        byte[] imageData = GetImageFromDatabase(username, password);

        //        if (imageData != null && imageData.Length > 0)
        //        {
        //            Image img = ByteArrayToImage(imageData);
        //            Skill_Score_Evaluation existingForm = Application.OpenForms.OfType<Skill_Score_Evaluation>().FirstOrDefault();
        //            if (existingForm != null)
        //            {
        //                if (receivedIndex == 1 && existingForm != null && recieved == 5)
        //                {
        //                    existingForm.Showimage1(img);
        //                    existingForm.Focus();
        //                }
        //                else if (receivedIndex == 2 && existingForm != null && recieved == 6)
        //                {
        //                    existingForm.Showimage2(img);
        //                    existingForm.Focus();
        //                }
        //                else if (receivedIndex == 3 && existingForm != null && recieved == 7)
        //                {
        //                    existingForm.Showimage3(img);
        //                    existingForm.Focus();
        //                }
        //                else if (receivedIndex == 4 && existingForm != null && recieved == 8)
        //                {
        //                    existingForm.Showimage4(img);
        //                    existingForm.Focus();
        //                }
        //                else if (receivedIndex == 2 && existingForm != null && recieved == 6)
        //                {
        //                    existingForm.Showimage5(img);
        //                    existingForm.Focus();
        //                }
        //                else if (receivedIndex == 3 && existingForm != null && recieved == 7)
        //                {
        //                    existingForm.Showimage6(img);
        //                    existingForm.Focus();
        //                }
        //                else if (receivedIndex == 4 && existingForm != null && recieved == 8)
        //                {
        //                    existingForm.Showimage7(img);
        //                    existingForm.Focus();
        //                }
        //                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        //                pictureBox1.Image = img;
        //                //QCO_Prop.Param = "Checklist";
        //                //QCO_Prop.image = img;
        //                this.Hide();

        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("No image found for the user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private void DisplayImageFromDatabase(string username, string password)
        //{
        //    try
        //    {
        //        byte[] imageData = GetImageFromDatabase(username, password);

        //        if (imageData != null && imageData.Length > 0)
        //        {
        //            Image image1 = ByteArrayToImage(imageData);
        //            Skill_Score_Evaluation existingForm = Application.OpenForms.OfType<Skill_Score_Evaluation>().FirstOrDefault();

        //            if (receivedIndex == 1 && existingForm != null && recieved == 1)
        //            {
        //                existingForm.Showimage1(image1);
        //                existingForm.Focus();
        //            }
        //            else if (receivedIndex == 2 && existingForm != null && recieved == 2)
        //            {
        //                existingForm.Showimage2(image1);
        //                existingForm.Focus();
        //            }
        //            else if (receivedIndex == 3 && existingForm != null && recieved == 3)
        //            {
        //                existingForm.Showimage3(image1);
        //                existingForm.Focus();
        //            }
        //            else if (receivedIndex == 4 && existingForm != null && recieved == 4)
        //            {
        //                existingForm.Showimage4(image1);
        //                existingForm.Focus();
        //            }
        //            else if (receivedIndex == 5 && existingForm != null && recieved == 5)
        //            {
        //                existingForm.Showimage5(image1);
        //                existingForm.Focus();
        //            }
        //            else if (receivedIndex == 6 && existingForm != null && recieved == 6)
        //            {
        //                existingForm.Showimage6(image1);
        //                existingForm.Focus();
        //            }
        //            else if (receivedIndex == 7 && existingForm != null && recieved == 7)
        //            {
        //                existingForm.Showimage7(image1);
        //                existingForm.Focus();
        //            }
        //            else if (receivedIndex == 8 && existingForm != null && recieved == 8)
        //            {
        //                existingForm.Showimage8(image1);
        //                existingForm.Focus();
        //            }
        //            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        //            pictureBox1.Image = image1;
        //            //QCO_Prop.Param = "Checklist";
        //            //QCO_Prop.image = image1;
        //            this.Hide();
        //        }
        //        else
        //        {
        //            MessageBox.Show("No image found for the user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private byte[] GetImageFromDatabase(string username, string password)
        {
            byte[] imageData = new byte[0];
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("username", username);
                data.Add("password", password);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                       Program.Client.APIURL,
                                       "SJ_TSMAPI",//类库名
                                       "SJ_TSMAPI.Skill_Score_Evaluation",//类名
                                       "GetImageFromDatabase",//方法名s
                                       Program.Client.UserToken,//token
                                       Newtonsoft.Json.JsonConvert.SerializeObject(data));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
                {
                    string base64Image = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    base64Image = base64Image.Replace("\"", "");
                    imageData = Convert.FromBase64String(base64Image);
                }
                else
                {
                    MessageBox.Show("RetData does not contain the expected property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return imageData;
        }



        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string userInput = textBox1.Text;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("username", userInput);
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_QCO", "KZ_QCO.Controllers.GeneralServer", "Get_PWD",
                 Program.Client.UserToken, JsonConvert.SerializeObject(data));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    string password = dt.Rows[0][0].ToString();
                    pdtxt.Text = password;
                    textBox1.ReadOnly = true;
                }
            }
            else
            {
                MessageBox.Show("Invalid password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        private void Apprbtn_Click_1(object sender, EventArgs e)
        { 
            string username = Bartxt.Text;
            string password = passtxt.Text;

            if (ValidateCredentials(username, password,receivedIndex.ToString()))
            {

                //DisplayImageFromDatabase(username, password);

                //DisplayImage1(username, password);
            }
            else
            {
                MessageHelper.ShowErr(this, "No such employee found");
            }

        }

        private void uploadbtn_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string text = string.Empty;
                filediclist = new List<Dictionary<string, object>>();

                foreach (string file in openFileDialog.FileNames)
                {
                    fileContent = File.ReadAllBytes(file);
                    filedic = new Dictionary<string, object>();
                    filedic.Add("file_content", fileContent);
                    filediclist.Add(filedic);
                }
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }

        }

        private void savebtn_Click_1(object sender, EventArgs e)
        {

            string username = Barcodetxt.Text;
            string password = Pwdtxt.Text;
            string Designation_Code = (cbdesignation.SelectedIndex + 1).ToString();
            string Designation_Name = cbdesignation.Text; 
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(Designation_Name))
            {
                MessageBox.Show("Please provide All details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please upload an image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveToDatabase(username, password, Designation_Code, Designation_Name,fileContent);
        }

        private void Updatebtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                string user = textBox1.Text;
                string pass = pdtxt.Text;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("username", user);
                data.Add("password", pass);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                          Program.Client.APIURL,
                                          "SJ_TSMAPI",//类库名
                                          "SJ_TSMAPI.Skill_Score_Evaluation",//类名
                                          "Update_password",//方法名s
                                          Program.Client.UserToken,//token
                                          Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this,ex.Message);
            }
            finally
            {
                textBox1.Text = "";
                pdtxt.Text = "";
            } 
            
        }
    }
}
