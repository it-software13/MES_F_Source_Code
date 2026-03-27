using DataGrid.DataGridViewCustomColumn;
using MaterialSkin;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace SjeMES_QCM_Ex
{
    public partial class F_QCM_Ex_file : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public F_QCM_Ex_file()
        {
            InitializeComponent();
            materialSkinManager = SJeMES_Control_Library.MaterialSkin.MaterialSkinHelper.MaterialSkinManagerSetDefault(
            Program.SkinThemes, materialSkinManager, this);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "   ";
            this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker2.CustomFormat = "   ";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void F_QCM_Ex_file_Load(object sender, EventArgs e)
        {
            pageControl1.BindPageEvent += GetDataList;
            //FormLoad();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        public void GetDataList(int pageSize, int pageIndex, out int totalCount)
        {
            totalCount = 0;
            try
            {
                //请求api的数据展示
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("taskno", txtTaskNo.Text.Trim());//实验室编号
                p.Add("stock", stock.Text.Trim());//存放位置
                p.Add("artno", txtart.Text.Trim());//状态
                p.Add("taskname", txttaskname.Text.Trim());//送检物名称
                if (!string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString()))
                {
                    p.Add("wh_date_start", dateTimePicker1.Value.ToString("yyyy-MM-dd"));//范围开始时间
                }
                if (!string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    p.Add("wh_date_end", dateTimePicker2.Value.ToString("yyyy-MM-dd"));//范围结束时间
                }
                if (string.IsNullOrWhiteSpace(dateTimePicker1.Text.ToString())||
                    string.IsNullOrWhiteSpace(dateTimePicker2.Text.ToString()))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please fill in the necessary conditions and then execute the query, prompt: storage time！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                p.Add("pageSize", pageSize);
                p.Add("pageIndex", pageIndex);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_QCMAPI",//类库名
                                            "SJ_QCMAPI.ExShose",//类名
                                            "GetExARCList",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                //视图数据显示
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int i = dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells["ID"].Value = dr["ID"].ToString();
                        dataGridView1.Rows[i].Cells["TASK_NO"].Value = dr["TASK_NO"].ToString();
                        dataGridView1.Rows[i].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString();
                        dataGridView1.Rows[i].Cells["WAREHOUSING_DATE"].Value = dr["WAREHOUSING_DATE"].ToString();
                        dataGridView1.Rows[i].Cells["ART_NO"].Value = dr["ART_NO"].ToString();
                        dataGridView1.Rows[i].Cells["TASKNAME"].Value = dr["TASKNAME"].ToString();
                        dataGridView1.Rows[i].Cells["DUE_DATE"].Value = dr["DUE_DATE"].ToString();
                        dataGridView1.Rows[i].Cells["color_code"].Value = dr["COLOUR_TYPE"].ToString();
                        dataGridView1.Rows[i].Cells["review_date"].Value = dr["NEXT_REVIEW_DATE"].ToString();
                        dataGridView1.Rows[i].Cells["remarks"].Value = dr["REMARKS"].ToString();
                       //if(dr["LATEST_REVIEW_DATE"].ToDate()<DateTime.Now.AddDays(2))
                       // {
                       //     string user_no = dr["Staff_No"].ToString();
                       //     string user_name = dr["Staff_Name"].ToString();
                       //     string ART_NO = dr["ART_NO"].ToString();
                       //     string STOCK_CODE = dr["STOCK_CODE"].ToString();
                       //     Escalate_Message(user_no, user_name, ART_NO, STOCK_CODE);
                       // }

                    }
                }
                totalCount = int.Parse(dic["rowCount"].ToString());
                this.dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FormLoad()
        {
            pageControl1.PageSize = 15;
            pageControl1.PageIndex = int.Parse(enum_page.PageIndex);
            pageControl1.SetPage();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Long;
            this.dateTimePicker1.CustomFormat = null;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker2.Format = DateTimePickerFormat.Long;
            this.dateTimePicker2.CustomFormat = null;
        }

        private void bthinsert_Click(object sender, EventArgs e)
        {
            F_QCM_Ex_file_Edit edit = new F_QCM_Ex_file_Edit();
            edit.StartPosition = FormStartPosition.CenterScreen;
            edit.ShowDialog();
            if (edit.bl)
            {
                FormLoad();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "operation")
                {
                    DataGridViewOperationCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["operation"] as DataGridViewOperationCell;
                    if (cell.CurrentItem == null)
                    {
                        return;
                    }
                    if (cell.CurrentItem.Equals("update"))
                    {
                        F_QCM_Ex_file_Edit edit = new F_QCM_Ex_file_Edit(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["TASK_NO"].Value.ToString());
                        edit.Text = "Laboratory Location Management - Edit";//实验室库位管理—编辑
                        edit.StartPosition = FormStartPosition.CenterScreen;
                        edit.ShowDialog();
                        if (edit.bl)
                        {
                            FormLoad();
                        }
                    }
                    else if (cell.CurrentItem.Equals("delete"))
                    {
                        DialogResult dr = MessageBox.Show("Are you sure you want to delete!", "Prompt", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (dr == DialogResult.OK)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("ids", dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJ_QCMAPI",//类库名
                                                        "SJ_QCMAPI.ExShose",//类名
                                                        "DeleteARC",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                MessageBox.Show("successfully deleted");
                                FormLoad();
                            }
                        }
                    }
                    else if (cell.CurrentItem.Equals("Save"))
                    {
                        bool isSelected = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["check"].Value);
                        if (!isSelected)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select the row to update");
                        }
                        else
                        {
                            if (dataGridView1.CurrentRow.Cells["color_code"].Value.IsEmpty())
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select Colour_Type！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            }
                            else if (dataGridView1.CurrentRow.Cells["review_date"].Value.IsEmpty())
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please mention Review Date！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            }
                            else if (dataGridView1.CurrentRow.Cells["remarks"].Value.IsEmpty())
                            {
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please enter Remarks！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            }
                            else
                            {
                                string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                                string TASK_NO = dataGridView1.CurrentRow.Cells["TASK_NO"].Value.ToString();
                                string TASKNAME = dataGridView1.CurrentRow.Cells["TASKNAME"].Value.ToString();
                                string STOCK_CODE = dataGridView1.CurrentRow.Cells["STOCK_CODE"].Value.ToString();
                                string ART_NO = dataGridView1.CurrentRow.Cells["ART_NO"].Value.ToString();
                                string COLOUR_TYPE = dataGridView1.CurrentRow.Cells["color_code"].Value.ToString();
                                string WAREHOUSING_DATE = dataGridView1.CurrentRow.Cells["WAREHOUSING_DATE"].Value.ToString();
                                string REVIEW_DATE = dataGridView1.CurrentRow.Cells["review_date"].Value.ToString();
                                string DUE_DATE = dataGridView1.CurrentRow.Cells["DUE_DATE"].Value.ToString();
                                string REMARKS = dataGridView1.CurrentRow.Cells["remarks"].Value.ToString();

                                Dictionary<string, object> p = new Dictionary<string, object>();
                                p.Add("ID", ID);
                                p.Add("TASK_NO", TASK_NO);
                                p.Add("TASKNAME", TASKNAME);
                                p.Add("STOCK_CODE", STOCK_CODE);
                                p.Add("ART_NO", ART_NO);
                                p.Add("COLOUR_TYPE", COLOUR_TYPE);
                                p.Add("WAREHOUSING_DATE", WAREHOUSING_DATE);
                                p.Add("REVIEW_DATE", REVIEW_DATE);
                                p.Add("DUE_DATE", DUE_DATE);
                                p.Add("REMARKS", REMARKS);
                                p.Add("MAIL_STATUS", "0");
                                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                            Program.Client.APIURL,
                                                            "SJ_QCMAPI",//类库名
                                                            "SJ_QCMAPI.ExShose",//类名
                                                            "EditARC2",//方法名
                                                            Program.Client.UserToken,//token
                                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                                if (ret.IsSuccess)
                                {
                                    MessageBox.Show("successfully Updated");
                                    FormLoad();
                                }
                                else
                                {
                                    MessageBox.Show(ret.ErrMsg);
                                    FormLoad();
                                }
                            }
                        }
                    }
                    else if (cell.CurrentItem.Equals("View")) 
                    {
                        string TASK_NO = dataGridView1.CurrentRow.Cells["TASK_NO"].Value.ToString();
                        //string TASKNAME = dataGridView1.CurrentRow.Cells["TASKNAME"].Value.ToString();
                        // STOCK_CODE = dataGridView1.CurrentRow.Cells["STOCK_CODE"].Value.ToString();
                        F_QCM_Ex_file_Previous frm = new F_QCM_Ex_file_Previous(TASK_NO);
                        frm.ShowDialog();
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "review_date")
                {
                   if(dataGridView1.Rows[e.RowIndex].Cells["color_code"].Value.IsEmpty())
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please select Colour_Type！", Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg); 
                    }
                    else
                    {
                        string Colour = dataGridView1.Rows[e.RowIndex].Cells["color_code"].Value.ToString();
                        if (Colour == "Light_Colour")
                        { 
                            dataGridView1.Rows[e.RowIndex].Cells["review_date"].Value = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                        }
                        else if (Colour == "Dark_Colour")
                        { 
                            dataGridView1.Rows[e.RowIndex].Cells["review_date"].Value = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd");
                        }
                    } 
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cell1 = row.Cells[9];
                var cell2 = row.Cells[8];
                DateTime d1 = cell1.Value.ToDate();
                DateTime d2;
                if (String.IsNullOrEmpty(cell2.Value.ToString()))
                {
                    d2 = DateTime.Now;
                }
                else
                {
                   d2 = cell2.Value.ToDate();
                }
                DateTime d3 = d1.AddMonths(-1);
                DateTime d4 = d2.AddDays(-5);
                DateTime d5 = DateTime.Now;
                int i = DateTime.Compare(d3, d5);
                if (i > 0)
                {
                    cell1.Style.BackColor = Color.Green;
                }
                else if (i < 0)
                {
                    cell1.Style.BackColor = Color.Red;
                }
                else
                {
                    cell1.Style.BackColor = Color.Yellow;
                }

                int i2 = DateTime.Compare(d4, d5);
                if (i2 > 0)
                {
                    cell2.Style.BackColor = Color.Green;
                }
                else if (i2 < 0)
                {
                    cell2.Style.BackColor = Color.Red;
                }
                else
                {
                    cell2.Style.BackColor = Color.Yellow;
                }
            }
        }

        public async void Escalate_Message(string user_no,string user_name,string ART_NO,string STOCK_CODE)
        {
            //string usertoken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJ1bmlhcHAiLCJuYmYiOjE2OTcxMDcyMjQsImV4cCI6MTY5Nzk3MTIyNCwiaWF0IjoxNjk3MTA3MjI0fQ.aqcZGdNgJVZIXR-zovTm9jw9hJo6aPmoFgysAH-6hY0".ToString();
            string usertoken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJ1bmlhcHAiLCJuYmYiOjE2OTcxMDcyMjQsImV4cCI6MTY5Nzk3MTIyNCwiaWF0IjoxNjk3MTA3MjI0fQ.aqcZGdNgJVZIXR-zovTm9jw9hJo6aPmoFgysAH-6hY0".ToString();
            string subject = "Review Alert";
            string body = "Dear "+ user_name+" Review Date is arrived for article number "+ ART_NO+" located in "+ STOCK_CODE+"";
            string sendAll = "0";
            string empnopz = "N";
            string orgidpz = "N";
            string deptnopz = "N";
            string otherspz = "N";
            string Reciever = "A"+ user_no;

            // Call SendApiRequestAsync with parameters
            string response = await SendApiRequestAsync(usertoken, subject, body, sendAll, empnopz, orgidpz, deptnopz, otherspz, Reciever);

            // Display the API response in the read-only textbox
            //Responcetxt.Text = response;
            //MessageBox.Show(response);
        }

        public async Task<string> SendApiRequestAsync(string usertoken, string subject, string body, string sendAll, string empnopz, string orgidpz, string deptnopz, string otherspz, string Reciever)
        {
            try
            {
                // Create an object to hold your request parameters
                var requestPayload = new
                {
                    subject,
                    body,
                    sendAll,
                    empnopz,
                    orgidpz,
                    deptnopz,
                    otherspz,
                    userList = new[] { Reciever }
                };
                //List<string> receivers = new List<string> { Reciever };
                //RequestPayload requestPayload = new RequestPayload
                //{
                //    Subject = subject,
                //    Body = body,
                //    SendAll = sendAll,
                //    EmpNoPz = empnopz,
                //    OrgIdPz = orgidpz,
                //    DeptNoPz = deptnopz,
                //    OthersPz = otherspz,
                //    UserList = receivers
                //}; 



                // Serialize the object to JSON
               // string jsonPayload = JsonSerializer.Serialize(requestPayload);
                string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(requestPayload);

                // Create an HttpClient instance
                using (HttpClient client = new HttpClient())
                {
                    // Set the API endpoint URL//Replace with your actual API URL
                    string apiUrl = "https://apc.apachefootwear.com/Platform/message/EscalateAppMessgae";

                    // Add the usertoken to the HTTP headers
                    client.DefaultRequestHeaders.Add("Token", usertoken);

                    // Create a JSON content from the serialized payload
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    // Send a POST request to the API
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read and return the response content
                        string responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        // Handle error responses here
                        return "API request failed: " + response.ReasonPhrase;
                    }
                }
            }
            catch (Exception e)
            {
                // Handle exceptions here
                return "Error: " + e.Message;
            }
        }
        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
