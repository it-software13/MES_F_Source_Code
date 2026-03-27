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
using SJeMES_Framework.WebAPI;

namespace SJeMES_TSM
{
    public partial class Training_Emp_Attendance : MaterialForm
    {
        public Training_Emp_Attendance()
        {
            InitializeComponent();
        }
        
        private void txtbcode_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                string ProcessType = comboBox1.Text;
                if (string.IsNullOrEmpty(ProcessType))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select process type");
                    txtbcode.Text = "";
                    return;
                }
                Remove_Trainig_Emp(ProcessType);
                GetOnlineOfflineCount(ProcessType);
                GetOnlineOfflineList(ProcessType);
                txtbcode.Text = ""; 
            }
        }

        public void Remove_Trainig_Emp( string ProcessType)
        {

            try
            { 
                string Barcode = txtbcode.Text;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Barcode", Barcode);
                data.Add("ProcessType", ProcessType);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",
                                            "SJ_TSMAPI.Training_Emp_Attendance",
                                            "Remove_Trainig_Emp",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    GetOnlineOfflineList(ProcessType);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    GetOnlineOfflineList(ProcessType);
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

       

        public void GetOnlineOfflineCount(string ProcessType)
        {

            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ProcessType", ProcessType);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.Training_Emp_Attendance",//类名
                                            "Get_TrainingEmp_Count",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    string TotalCount = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(dic["TotalCount"].ToString());
                    string OnlineCount = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(dic["OnlineCount"].ToString());
                    string OfflineCount = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(dic["OfflineCount"].ToString());
                    txttotal.Text = TotalCount;
                    txtonline.Text = OnlineCount;
                    txtoffline.Text = OfflineCount;
                }
                
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ex.Message);
            }
        }

        public void GetOnlineOfflineList(string ProcessType)
        {

            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ProcessType", ProcessType);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",
                                            "SJ_TSMAPI.Training_Emp_Attendance",
                                            "GetOnlineOfflineList",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable OnlineList = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["OnlineList"].ToString());
                    DataTable OfflineList = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["OfflineList"].ToString());
                    if(OnlineList.Rows.Count>0)
                    {
                        dataGridView1.DataSource = OnlineList;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                    if (OfflineList.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = OfflineList;
                    }
                    else
                    {
                        dataGridView2.DataSource = null;
                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ex.Message); 
            }
        }

        private void Training_Emp_Attendance_Load(object sender, EventArgs e)
        {
            string ProcessType = comboBox1.Text;
            GetOnlineOfflineCount(ProcessType);
            GetOnlineOfflineList(ProcessType);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string Barcode = textBox1.Text;
                string Processtype = cbprocesstype.Text;
                string sdate = dateTimePicker1.Text;
                string edate = dateTimePicker2.Text;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Barcode", Barcode);
                data.Add("Processtype", Processtype);
                data.Add("sdate", sdate);
                data.Add("edate", edate);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.Training_Emp_Attendance",//类名
                                            "Training_Attendance_Report",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable Attendance = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Attendance"].ToString());
                    if(Attendance.Rows.Count>0)
                    {
                        dataGridView3.DataSource = null;
                        dataGridView3.DataSource = Attendance;
                    }
                    else
                    {
                        dataGridView3.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this,"No Data Found");
                    }
                   
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    //GetTC_InwardEmployee();
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Attendance_Report.xls";
                ExportExcels.Export(a, dataGridView3);
            }
        }

        private void btnattandance_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            string ProcessType = comboBox1.Text;
            if (string.IsNullOrEmpty(ProcessType))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select process type");
                return;
            }
            //Online Data
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                    dt1.Columns.Add(col.Name);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                        DataRow dRow = dt1.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt1.Rows.Add(dRow);
                }
            }
            if(dt1.Rows.Count>0)
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("dt1", dt1);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                               Program.Client.APIURL,
                                               "SJ_TSMAPI",
                                               "SJ_TSMAPI.Training_Emp_Attendance",
                                               "Recieve_Training_Emp",
                                               Program.Client.UserToken,
                                               Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    GetOnlineOfflineCount(ProcessType);
                    GetOnlineOfflineList(ProcessType);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    GetOnlineOfflineCount(ProcessType);
                    GetOnlineOfflineList(ProcessType);
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Employee Available");
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProcessType = comboBox1.Text;
            GetOnlineOfflineCount(ProcessType);
            GetOnlineOfflineList(ProcessType);
        }
    }
}
