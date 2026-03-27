using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class Registration : MaterialForm
    {
        public string EndDate { get; private set; }
        Boolean IsExist = false;
        public Registration()
        {
            InitializeComponent();
            dateTimePicker3.MinDate = DateTime.Now;
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            string Barcode = txt_Barcode.Text;
            string Name = textBox2.Text;
            string Department = textBox3.Text;
            string Position = textBox4.Text;

            string Trainer = textBox5.Text;
            string Process_Type = comboBox1.Text;
            string Process_Name = comboBox4.Text;
            string Training_Types = comboBox3.Text;
            string EndDate = dateTimePicker3.Text;
            if (string.IsNullOrEmpty(Name))
            { 
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your Name");
                return;
            }

            if (string.IsNullOrEmpty(Department))
            { 
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your  Department");
                return;
            }
            if (string.IsNullOrEmpty(Position))
            { 
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your position");
                return;
            }

            if (string.IsNullOrEmpty(Trainer))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Trainer"); 
                return;
            }
            if (string.IsNullOrEmpty(Process_Type))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Process_Type"); 
                return;
            } 
            if (string.IsNullOrEmpty(Training_Types))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Training_Type"); 
                return;
            }
            if (string.IsNullOrEmpty(Process_Name))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Process_Name"); 
                return;
            }
            if (string.IsNullOrEmpty(EndDate))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select from EndDate"); 
                return;
            }

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Barcode", Barcode);
            p.Add("Name", Name);
            p.Add("Department", Department);
            p.Add("Position", Position);
            p.Add("Trainer", Trainer);
            p.Add("Process_Type", Process_Type);
            p.Add("Process_Name", Process_Name);
            p.Add("Training_Types", Training_Types);
            p.Add("EndDate", EndDate);
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Registration", "InsertDetails", Program.Client.UserToken, JsonConvert.SerializeObject(p));
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

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            { 
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("TYPE", comboBox1.Text); 
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Registration",
                    "GetTypeOfProcess",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);

                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    comboBox4.Items.Clear();

                    if (dtJson1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            comboBox4.Items.Add(dr["NAME"].ToString()); 
                        } 
                    } 
                    else
                    { 
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                { 
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {

                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void Button1_Click_1(object sender, EventArgs e) 
        {
            Get_Registration_details();
        }

        public void Get_Registration_details()
        {
            try

            {
                Cursor.Current = Cursors.WaitCursor;
                string fromDate = dateTimePicker1.Text;
                string toDate = dateTimePicker2.Text;
                string Process_Type = comboBox5.Text;
                string Process_Name = comboBox2.Text;
                string Training_Type = comboBox6.Text;
                string Barcode = textBox1.Text;
                dataGridView1.DataSource = null;
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("fromDate", fromDate);
                p.Add("toDate", toDate);
                p.Add("Process_Type", Process_Type);
                p.Add("Process_Name", Process_Name);
                p.Add("Training_Type", Training_Type);
                p.Add("Barcode", Barcode);
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                       Program.Client.APIURL,
                       "SJ_TSMAPI",
                       "SJ_TSMAPI.Registration",
                       "Getdatadetails",
                       Program.Client.UserToken,
                       Newtonsoft.Json.JsonConvert.SerializeObject(p)
                   );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);


                if (ret.IsSuccess)
                {

                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString()); 
                    if (dtJson1.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dtJson1;
                        for (int i = 0; i < dtJson1.Rows.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells["DEPARTMENT"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["POSITION"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["EMP_NO"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["EMP_NAME"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["PROCESS_TYPE"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["PROCESS_NAME"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["TRAINING_TYPE"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["TRAINING_S_DATE"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["TRAINING_E_DATE"].ReadOnly = false;
                            dataGridView1.Rows[i].Cells["TRAINING_E_DATE"].Style.BackColor = Color.Violet;
                            dataGridView1.Rows[i].Cells["TRAINING_E_DATE"].Style.ForeColor = Color.White;
                            if (!string.IsNullOrEmpty(dtJson1.Rows[i]["STATUS"].ToString()) && (dtJson1.Rows[i]["STATUS"].ToString()!="Extended"))
                            {
                                dataGridView1.Rows[i].Cells["STATUS"].ReadOnly = true;
                            }
                        }
                    }

                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }

            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("TYPE", comboBox5.Text); 
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Registration",
                    "GetTypeOfProcess",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);

                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    comboBox2.Items.Clear();

                    if (dtJson1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            comboBox2.Items.Add(dr["NAME"].ToString()); 
                        } 
                    } 
                    else
                    { 
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                { 
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            { 
                string a = "Get_Registration_Data.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                comboBox1.Enabled = false;
                comboBox4.Enabled = false;
                comboBox3.Enabled = false;
                clear();
            }
            else
            {
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = false;
                comboBox1.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                clear();
            }

        }

        private void Submit_btn_Click_1(object sender, EventArgs e) 
        {
            Insert_User(); 
        }

        private void Clear_btn_Click_1(object sender, EventArgs e)
        {
            clear();
        } 

        private void clear()
        {
            txt_Barcode.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
        }
        private void Txt_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(checkBox2.Checked)
                {
                    GetNewEmp();
                }
                else
                {
                    GetExistingEmp();
                }
            }
        }

        public void GetNewEmp()
        {
            if(string.IsNullOrEmpty(txt_Barcode.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter Barcode");
                return;
            }
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Barcode", txt_Barcode.Text);
            string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.Client.APIURL,
                "SJ_TSMAPI",
                "SJ_TSMAPI.Registration",
                "Get_NewUser_Details",
                Program.Client.UserToken,
                Newtonsoft.Json.JsonConvert.SerializeObject(retData)

                );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

            if (ret.IsSuccess)
            {

                if (dtJson1.Rows.Count > 0)
                {  
                        textBox2.Text = dtJson1.Rows[0]["EMP_NAME"].ToString();
                        textBox3.Text = dtJson1.Rows[0]["DEPARTMENT"].ToString();
                        textBox4.Text = dtJson1.Rows[0]["POSITION"].ToString();
                        comboBox3.Text = dtJson1.Rows[0]["TRAINING_TYPE"].ToString();
                        textBox5.Text = "";
                        comboBox4.Text = "";
                        comboBox1.Text = "";
                }
                else
                {
                    clear();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                }
            }
        }

        public void GetExistingEmp()
        {
            if (string.IsNullOrEmpty(txt_Barcode.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter Barcode");
                return;
            }
            if (!hidingdetails(txt_Barcode.Text))
            {
                DataTable dt = new DataTable();
                string status = string.Empty;
                try
                {
                    if (checkBox1.Checked)
                    {
                        status = "0";
                    }
                    else
                    {
                        status = "1";
                    }
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("Barcode", txt_Barcode.Text);
                    retData.Add("status", status);


                    string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "SJ_TSMAPI",
                        "SJ_TSMAPI.Registration",
                        "GetUserDetails",
                        Program.Client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)

                        );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    if (ret.IsSuccess)
                    {

                        if (dtJson1.Rows.Count > 0)
                        {

                            if (status == "0")
                            {
                                textBox2.Text = dtJson1.Rows[0]["EMP_NAME"].ToString();
                                textBox3.Text = dtJson1.Rows[0]["DEPARTMENT"].ToString();
                                textBox4.Text = dtJson1.Rows[0]["POSITION"].ToString();
                                textBox5.Text = dtJson1.Rows[0]["TRAINER"].ToString();
                                comboBox3.Text = dtJson1.Rows[0]["TRAINING_TYPE"].ToString();
                                comboBox4.Text = dtJson1.Rows[0]["PROCESS_NAME"].ToString();
                                comboBox1.Text = dtJson1.Rows[0]["PROCESS_TYPE"].ToString();
                            }
                            else
                            {
                                textBox2.Text = dtJson1.Rows[0]["EMP_NAME"].ToString();
                                textBox3.Text = dtJson1.Rows[0]["DEPARTMENT"].ToString();
                                textBox4.Text = dtJson1.Rows[0]["POSITION"].ToString();
                                textBox5.Text = "";
                                comboBox3.Text = "";
                                comboBox4.Text = "";
                                comboBox1.Text = "";
                            }
                        }
                        else
                        {
                            clear();
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                        }
                    }
                    else
                    {

                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    }
                }
                catch (Exception ex)
                {

                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Traininig is not completed for this user");
                txt_Barcode.Text = "";
            }
        }

        private bool hidingdetails(string Barcode)
        {
            //string Barcode = txt_Barcode.Text;

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Barcode", Barcode);

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Registration", "Hidingdetails", Program.Client.UserToken, JsonConvert.SerializeObject(p));

            bool result = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            return result;
        }

        private void TextBox5_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable(); 
                try
                { 
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("Trainer", textBox5.Text); 
                    string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "SJ_TSMAPI",
                        "SJ_TSMAPI.Registration",
                        "GetTrainerDetails",
                        Program.Client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData) 
                        );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                    if (ret.IsSuccess)
                    {

                        if (dtJson1.Rows.Count > 0)
                        {
                            textBox5.Text = dtJson1.Rows[0]["EMP_NAME"].ToString(); 
                        }
                        else
                        { 
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                        }
                    }
                    else
                    { 
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    }
                }

                catch (Exception ex)
                { 
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                } 
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("TYPE", comboBox1.Text); 
                string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "SJ_TSMAPI",
                    "SJ_TSMAPI.Registration",
                    "GetTypeOfProcess",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData); 
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    comboBox4.Items.Clear();

                    if (dtJson1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            comboBox4.Items.Add(dr["NAME"].ToString());

                        } 
                    }

                    else
                    { 
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                    }
                }
                else
                { 
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                }
            }
            catch (Exception ex)
            { 
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        } 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex >-1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "delete")
                {
                    IsExist = Deletebyuser();
                    if (!IsExist)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "dont have permission to delete");
                    }
                    else
                    {
                        string Barcode = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        string Process_Name = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        p.Add("Barcode", Barcode);
                        p.Add("Process_Name", Process_Name);

                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJ_TSMAPI",//类库名
                                                        "SJ_TSMAPI.Registration",//类名
                                                        "DeleteData",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (ret.IsSuccess)
                        {
                            MessageBox.Show("successfully deleted");
                            Get_Registration_details();
                        }
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "submit") 
                {
                    DataGridViewComboBoxCell comboBoxCell = dataGridView1.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell;
                    string Status = comboBoxCell?.FormattedValue?.ToString();
                    if (string.IsNullOrEmpty(Status))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Update Status");
                        return;
                    }
                    string Barcode = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string Process_Name = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    string EndDate = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    DateTime endDate = DateTime.Parse(EndDate);  
                    DateTime Now = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));

                    if ((Status == "Drop" || Status == "Completed" || Status == "Terminated") && Now == endDate)
                    {
                        UpdateStatus(Barcode, Process_Name, Status, EndDate);
                    }
                    else if ((Status == "Extended") && Now <= endDate)
                    {
                        UpdateStatus(Barcode, Process_Name, Status, EndDate);
                    }
                    else if ((Status == "Extended") && Now < endDate)
                    {
                        UpdateStatus(Barcode, Process_Name, Status, EndDate);
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select proper end date");
                    }
                    //if (endDate < Now)
                    //{
                    //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "EndDate should be greater than SysDate");
                    //} 
                    //else if((Status == "Drop" || Status == "Completed" || Status == "Terminated") && Now == endDate)
                    //{
                    //    UpdateStatus(Barcode, Process_Name, Status,EndDate);
                    //}
                    //else if ((Status == "Extended") && Now <= endDate)
                    //{
                    //    UpdateStatus(Barcode, Process_Name, Status, EndDate);
                    //}
                    //else
                    //{
                    //    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Proper End Date");
                    //}
                }
            }
        }

        public void UpdateStatus(string Barcode,string Process_Name,string Status,string EndDate)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Barcode", Barcode);
            p.Add("Process_Name", Process_Name);
            p.Add("EndDate", EndDate);
            p.Add("Status", Status);
            string responseData = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Registration", "Savedetails", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
            bool result = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData)["IsSuccess"]);
            if (result)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Date Updated Successfully");
                Get_Registration_details();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                Get_Registration_details();
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if(checkBox1.Checked==false)
            {
                try
                {
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("process name", comboBox4.Text);
                    retData.Add("Barcode", txt_Barcode.Text);
                    string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "SJ_TSMAPI",
                        "SJ_TSMAPI.Registration",
                        "GetProcessName",
                        Program.Client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                    );
                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                    if (ret.IsSuccess)
                    {
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());
                        if (dtJson1.Rows.Count > 0)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "This Employee already known this process");
                            comboBox4.SelectedIndex = -1;
                        }
                    }
                    else
                    {

                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    }
                }
                catch (Exception ex)
                {

                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            
        }

        public Boolean Deletebyuser()
        {
            //string WORK_NAME = string.Empty;
            //Dictionary<string, object> p = new Dictionary<string, object>();
            //p.Add("WORK_NAME", WORK_NAME);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                               Program.Client.APIURL,
                                               "SJ_TSMAPI",//class library name
                                               "SJ_TSMAPI.Registration",//class name
                                               "CheckModifybyUser",//method name
                                               Program.Client.UserToken,//token
                                               Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            return ret.IsSuccess;
        }

        public void Insert_User()
        {
            string Barcode = txt_Barcode.Text;
            string Name = textBox2.Text;
            string Department = textBox3.Text;
            string Position = textBox4.Text;
            string Trainer = textBox5.Text;
            string Process_Type = comboBox1.Text;
            string Process_Name = comboBox4.Text;
            string Training_Types = comboBox3.Text;
            string EndDate = dateTimePicker3.Text;
            string status = string.Empty;

            if (string.IsNullOrEmpty(Barcode))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter barcode");
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your Name");
                return;
            }

            if (string.IsNullOrEmpty(Department))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your  Department");
                return;
            }
            if (string.IsNullOrEmpty(Position))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your position");
                return;
            }

            if (string.IsNullOrEmpty(Trainer))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Trainer");
                return;
            }
            if (string.IsNullOrEmpty(Process_Type))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Process_Type");
                return;
            }

            if (string.IsNullOrEmpty(Training_Types))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Training_Type");
                return;
            }
            if (string.IsNullOrEmpty(Process_Name))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Process_Name");
                return;
            }
            if (string.IsNullOrEmpty(EndDate))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select from EndDate");
                return;
            }
            if (checkBox1.Checked)
            {
                status = "0";
            }
            else if(checkBox2.Checked)
            {
                status = "2";
            }
            else
            {
                status = "1";
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Barcode", Barcode);
            p.Add("Name", Name);
            p.Add("Department", Department);
            p.Add("Position", Position);
            p.Add("Trainer", Trainer);
            p.Add("Process_Type", Process_Type);
            p.Add("Process_Name", Process_Name);
            p.Add("Training_Types", Training_Types);
            p.Add("EndDate", EndDate);
            p.Add("status", status);
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Registration", "InsertDetails", Program.Client.UserToken, JsonConvert.SerializeObject(p));

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

        private void button4_Click(object sender, EventArgs e)
        {
            Submit_New_Employee();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear2();
        }

        public void clear2()
        {
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            comboBox7.Text = "";
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Register_New_Emp();//new
            }
        }

        public void Register_New_Emp()
        {
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please enter Barcode");
                return;
            }
            if (!hidingdetails(textBox6.Text))
            {  
                DataTable dt = new DataTable();
                string status = "1";
                try
                {
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    retData.Add("Barcode", textBox6.Text);
                    retData.Add("status", status);
                    string responseData = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.Client.APIURL,
                        "SJ_TSMAPI",
                        "SJ_TSMAPI.Registration",
                        "GetUserDetails",
                        Program.Client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                        );

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(responseData);
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());

                    if (ret.IsSuccess)
                    {

                        if (dtJson1.Rows.Count > 0)
                        {
                            textBox7.Text = dtJson1.Rows[0]["EMP_NAME"].ToString();
                            textBox8.Text = dtJson1.Rows[0]["DEPARTMENT"].ToString();
                            textBox9.Text = dtJson1.Rows[0]["POSITION"].ToString();
                        }
                        else
                        {
                            clear();
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found.");
                        }
                    }
                    else
                    {

                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    }
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Traininig is not completed for this user");
                textBox6.Text = "";
            }

        }
        public void Submit_New_Employee()
        {
            string Barcode = textBox6.Text;
            string Name = textBox7.Text;
            string Department = textBox8.Text;
            string Position = textBox9.Text;
            string Training_Types = comboBox7.Text;
            string EndDate2 = dateTimePicker4.Text;
            string Process_Type = comboBox8.Text;
            string status = string.Empty;

            if (string.IsNullOrEmpty(Barcode))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter barcode");
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your Name");
                return;
            }

            if (string.IsNullOrEmpty(Department))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your  Department");
                return;
            }
            if (string.IsNullOrEmpty(Position))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Enter Your position");
                return;
            }

            if (string.IsNullOrEmpty(Training_Types))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select Your Training_Type");
                return;
            }
            if (string.IsNullOrEmpty(Process_Type))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Process Type");
                return;
            }

            if (string.IsNullOrEmpty(EndDate2))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Select from EndDate");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Barcode", Barcode);
            p.Add("Name", Name);
            p.Add("Department", Department);
            p.Add("Position", Position);
            p.Add("Training_Types", Training_Types);
            p.Add("Process_Type", Process_Type);
            p.Add("EndDate", EndDate2);
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI", "SJ_TSMAPI.Registration", "Insert_New_User_Details", Program.Client.UserToken, JsonConvert.SerializeObject(p));

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
                clear2();
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                comboBox3.Enabled = false;
                clear();
            }
            else
            {
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = false;
                comboBox1.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                clear();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }
    }
}












