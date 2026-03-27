using DataGrid.DataGridViewCustomColumn;
using MaterialSkin.Controls;
using NewExportExcels;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sterilization_Alert
{
    public partial class Sterilization_Alert_System : MaterialForm
    {
        string mergedFileName = string.Empty;
        private string Doc_guid;
        private string Doc_name;
        private string Img_guid;
        private string Img_name;
        private string Image_download;
        public Sterilization_Alert_System()
        {
            InitializeComponent();
        }
        public class BGradeReasonData
        {
            public List<Dictionary<string, object>> Data { get; set; }
        }
        public void DepartmentLoad()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Dept", comboBox1.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Departmentload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox1.Text = "";
                comboBox6.Text = "";
                comboBox1.Items.Clear();
                comboBox6.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("DEPARTMENT"))
                        {
                            var factory = row["DEPARTMENT"];
                            if (factory != null)
                            {
                                comboBox1.Items.Add(factory.ToString());
                                comboBox6.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox1.Text = " ";
                    comboBox6.Text = " ";

                }
            }




        }
        public void Dept_Pic_Load()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Department", comboBox1.Text);
            data.Add("Location", comboBox2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Deptpicload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox3.Text = "";
                comboBox3.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("DEPT_PIC"))
                        {
                            var factory = row["DEPT_PIC"];
                            if (factory != null)
                            {
                                comboBox3.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox3.Text = " ";
                }
            }




        }
        public void Plandateload()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Department", comboBox1.Text);
            data.Add("Location", comboBox2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Plandateload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    var row = reasonData.Data[0];  // take the first row

                    if (row.ContainsKey("PLAN_DATE") && row["PLAN_DATE"] != null)
                    {
                        if (DateTime.TryParse(row["PLAN_DATE"].ToString(), out DateTime dueDate))
                        {
                            dateTimePicker1.Value = dueDate;
                        }
                        else
                        {
                            // fallback if parse fails (optional)
                            MessageBox.Show("Invalid date format in Plan date");
                        }
                    }
                }
            }




        }
        public void Imp_Pic_Load()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Department", comboBox1.Text);
            data.Add("Location", comboBox2.Text);
            data.Add("Deptpic", comboBox3.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Imppicload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox4.Text = "";
                comboBox4.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("IMP_PIC"))
                        {
                            var factory = row["IMP_PIC"];
                            if (factory != null)
                            {
                                comboBox4.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox4.Text = " ";
                }
            }




        }
        public void Location2load()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Department", comboBox6.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Locationload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox7.Text = "";
                comboBox7.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("LOCATION"))
                        {
                            var factory = row["LOCATION"];
                            if (factory != null)
                            {
                                comboBox7.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox7.Text = " ";
                }
            }
        }
        public void Locationload()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.Add("Department", comboBox1.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Locationload", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);

            if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
            {
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                {
                    foreach (var row in reasonData.Data)
                    {
                        if (row != null && row.ContainsKey("LOCATION"))
                        {
                            var factory = row["LOCATION"];
                            if (factory != null)
                            {
                              comboBox2.Items.Add(factory.ToString());
                            }
                        }
                    }
                }
                else
                {
                    comboBox2.Text = " ";
                }
            }
        }
        private void Sterilization_Alert_System_Load(object sender, EventArgs e)
        {
            DepartmentLoad();
           
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Please select image files",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                
                List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();

                foreach (string file in ofd.FileNames)
                {
                    try
                    {
                        byte[] fileContent = File.ReadAllBytes(file);

                        string originalFileName = Path.GetFileNameWithoutExtension(file); // without extension
                        string fileExtension = Path.GetExtension(file); // .jpg, .png, etc.

                        // Add current date (yyyyMMdd) to filename
                        string currentDate = DateTime.Now.ToString("yyyyMMdd"); // e.g. 20250905
                        string safeFileName = $"{currentDate}_{originalFileName}{fileExtension}";

                        string filePath = file;

                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(
                            Program.Client.UploadUrl,
                            filePath,
                            Program.Client.UserToken
                        );

                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            Img_guid = resultDIC["guid"].ToString();
                            Img_name = safeFileName;
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Image uploaded successfully!");
                        }

                        Dictionary<string, object> filedic = new Dictionary<string, object>
        {
            { "file_content", fileContent },
            { "file_name", safeFileName }
        };

                        filediclist.Add(filedic);

                        using (MemoryStream ms = new MemoryStream(fileContent))
                        {
                            pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                        }
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        Console.WriteLine($"File processed: {safeFileName}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
        public void InsertSterilizationdata()
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Department");
                    return;
                }
                if (string.IsNullOrEmpty(comboBox2.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Location");
                    return;
                }

                if (string.IsNullOrEmpty(comboBox3.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Department PIC name");
                    return;
                }
                if (string.IsNullOrEmpty(comboBox4.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Implementation PIC name");
                    return;
                }
                if (string.IsNullOrEmpty(comboBox5.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Status");
                    return;
                }
                if (string.IsNullOrEmpty(Img_guid))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Upload IMage");
                    return;
                }
                if (string.IsNullOrEmpty(Doc_guid))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Upload Document");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Department", comboBox1.Text);
                p.Add("Location", comboBox2.Text);
                p.Add("Plandate", dateTimePicker1.Text);
                p.Add("Finishdate", dateTimePicker2.Text);
                p.Add("Nextduedate", dateTimePicker3.Text);
                p.Add("Deptpic", comboBox3.Text);
                p.Add("Imppic", comboBox4.Text);
                p.Add("Status", comboBox5.Text);
                p.Add("Img_guid", Img_guid);
                p.Add("Img_name", Img_name);
                p.Add("Doc_guid", Doc_guid);
                p.Add("Doc_name", Doc_name);
                string ret = WebAPIHelper.Post(Program.Client.APIURL, "SJ_MESAPI", "SJ_MESAPI.Sterilization", "InsertSterilizationdata", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                var j = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (Convert.ToBoolean(j["IsSuccess"].ToString()))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Uploaded successfully!");
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                    comboBox3.SelectedIndex = -1;
                    comboBox4.SelectedIndex = -1;
                    comboBox5.SelectedIndex = -1;
                    comboBox4.Text = string.Empty;
                    comboBox3.Text = string.Empty;
                    Doc_guid = "";
                    Doc_name = "";
                    Img_guid = "";
                    Img_name = "";
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, j["ErrMsg"].ToString());
                }

            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
        }
        public DataTable File_list(string file_name)
        {
            DataTable dt = new DataTable();
            try
            {
                //请求api的数据展示
                Dictionary<string, object> data = new Dictionary<string, object>();
                //键值对传值
                data.Add("file_name", file_name);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.APC_Supplementary_Data",//类名
                                            "Main_ListFile",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    throw new Exception(ret.ErrMsg);
                }
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
            }
            catch (Exception ex)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    item["FILE_URL"] = Program.Client.PicUrl + item["FILE_URL"];
                }
            }
            return dt;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Select a File",
                Filter = "All Files (*.*)|*.*",
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                List<Dictionary<string, object>> filediclist = new List<Dictionary<string, object>>();
                foreach (string file in ofd.FileNames)
                {
                    try
                    {
                        byte[] fileContent = File.ReadAllBytes(file);
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file);
                        string fileExtension = Path.GetExtension(file);

                        // Add current date to file name (format: yyyyMMdd)
                        string dateString = DateTime.Now.ToString("yyyyMMdd");
                        string safeFileName = $"{fileNameWithoutExt}_{dateString}{fileExtension}";

                        string filePath = file; // original file path (optional, depends if your upload needs original or modified name)

                        UploadFileResultDto res = SJeMES_Framework.Common.HttpHelper.UpLoadCommon(
                            Program.Client.UploadUrl,
                            filePath,
                            Program.Client.UserToken
                        );

                        if (res.IsSuccess)
                        {
                            var resultDIC = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res.ReturnObj.ToString());
                            Doc_guid = resultDIC["guid"].ToString();
                            Doc_name = safeFileName; // now includes date
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Document Uploaded successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error uploading file {Path.GetFileName(file)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }



            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "Imgview")
                {

                    string name = dataGridView2.Rows[e.RowIndex].Cells["IMAGE_NAME"].Value.ToString();
                    Image_download = name;
                    string Location = dataGridView2.Rows[e.RowIndex].Cells["LOCATION"].Value.ToString();
                    string department = dataGridView2.Rows[e.RowIndex].Cells["DEPARTMENT"].Value.ToString();                                                                                                                                                                                    //}
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("Img_name", name);
                        p.Add("Location", Location);
                        p.Add("Department", department);
                        string ret = WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Getimageguid",
                                                    Program.Client.UserToken,
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);
                        if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
                        {
                            var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                            if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                            {
                                var row = reasonData.Data[0]; // take the first row

                                if (row != null && row.ContainsKey("FILE_URL"))
                                {
                                    string IMAGE_GUID = row["FILE_URL"].ToString();
                                    string baseUrl = Program.Client.PicUrl.TrimEnd('/');
                                    string url = $"{baseUrl}/{IMAGE_GUID}";

                                    try
                                    {
                                        using (WebClient webClient = new WebClient())
                                        {
                                            byte[] imageBytes = webClient.DownloadData(url);
                                            using (MemoryStream ms = new MemoryStream(imageBytes))
                                            {
                                                Image image = Image.FromStream(ms);
                                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                                pictureBox1.Image = image;
                                            }
                                        }
                                        tabControl1.SelectedIndex = 0;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error loading image: " + ex.Message + "\nURL: " + url);
                                    }
                                }
                                else
                                {
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Image not found");
                                }
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Image not Uploaded");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    }
                }
                if (dataGridView2.Columns[e.ColumnIndex].Name == "DOC_VIEW")
                {
                    string Dept = dataGridView2.Rows[e.RowIndex].Cells["DEPARTMENT"].Value.ToString();
                    string Loc = dataGridView2.Rows[e.RowIndex].Cells["LOCATION"].Value.ToString();              
                    string Filename = dataGridView2.Rows[e.RowIndex].Cells["FILE_NAME"].Value.ToString();

                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Dictionary<string, object> p = new Dictionary<string, object>();                 
                        p.Add("Location", Loc);
                        p.Add("Department", Dept);
                        p.Add("Filename", Filename);
                        string ret = WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                   "SJ_MESAPI", "SJ_MESAPI.Sterilization", "Getfileurl",
                                                    Program.Client.UserToken,
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject result = JsonConvert.DeserializeObject<ResultObject>(ret);


                        if (result.IsSuccess && !string.IsNullOrEmpty(result.RetData))
                        {
                            var reasonData = JsonConvert.DeserializeObject<BGradeReasonData>(result.RetData);

                            if (reasonData != null && reasonData.Data != null && reasonData.Data.Count > 0)
                            {
                                var row = reasonData.Data[0]; 

                                if (row != null && row.ContainsKey("FILE_URL"))
                                {
                                    string FILE_GUID = row["FILE_URL"].ToString();
                                    string baseUrl = Program.Client.PicUrl.TrimEnd('/');
                                    string url = $"{baseUrl}/{FILE_GUID}";
                                    using (WebClient wc = new WebClient())
                                    {
                                        string tempFile = Path.Combine(Path.GetTempPath(), Path.GetFileName(FILE_GUID));
                                        wc.DownloadFile(url, tempFile);
                                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                                        {
                                            FileName = tempFile,
                                            UseShellExecute = true
                                        });
                                    }
                                }
                                else
                                {
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "File not found");
                                }
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Document not Uploaded");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    }
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Locationload();
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Location2load();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dept_Pic_Load();
            Plandateload();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Imp_Pic_Load();
            dateTimePicker3.Value = dateTimePicker2.Value.AddMonths(3);
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Value = dateTimePicker2.Value.AddMonths(3);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose(); 
                pictureBox1.Image = null; 
            }
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            Doc_guid = "";
            Doc_name = "";
            Img_guid = "";
            Img_name = "";

        }

        private void DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("From", dateTimePicker4.Text);
                data.Add("To", dateTimePicker5.Text);
                data.Add("Dept", comboBox6.Text);
                data.Add("Location", comboBox7.Text);
                data.Add("Status", comboBox8.Text);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                          "SJ_MESAPI", "SJ_MESAPI.Sterilization","Viewsterilizationdata", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);


                var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);

                if (dic.ContainsKey("data") || dic.ContainsKey("Data"))
                {
                    string dataString = dic.ContainsKey("data") ? dic["data"].ToString() : dic["Data"].ToString();
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dataString);

                    if (dtJson1.Rows.Count > 0)
                        dataGridView2.DataSource = dtJson1;
                    else
                    {
                        dataGridView2.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                {
                    dataGridView2.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Key 'data' not found in response.");
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            InsertSterilizationdata();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1) 
            {
                pictureBox1.Image = null;
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "SterilizationData.xls";
                ExportExcels.Export(a, dataGridView2);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    sfd.FileName = $"{Image_download}_{timestamp}";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (Bitmap bmp = new Bitmap(pictureBox1.Image))
                        {
                            bmp.Save(sfd.FileName);
                        }
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Image downloaded successfully!");
                      
                    }
                }

            }
            else
            {
                MessageBox.Show("No image available to download.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "Done")
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
