using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using NewExportExcels;

namespace SJeMES_SM
{
    public partial class MPAC_Allocation : MaterialForm
    {
        public MPAC_Allocation()
        {
            InitializeComponent();
        }
        private void MPAC_Allocation_Load(object sender, EventArgs e)
        {
            string ProcessType = comboBox2.Text;
            GetPlants();
            GetOnlineOfflineCount(ProcessType);
            GetOnlineOfflineList(ProcessType);
        }

        ComboBox ProcessList;
        List<string> lt = new List<string>();

        public void GetPlants()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI",
                                            "SJ_TSMAPI.Skill_Matrix",
                                            "Get_Prod_Plant",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dtJson1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtJson1.Rows)
                        {
                            comboBox1.Items.Add(dr["department_code"].ToString());
                            comboBox3.Items.Add(dr["department_code"].ToString());
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

        public void GetProcessList()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                                            "SJ_SMAPI.MPAC_Allocation",
                                            "GetProcessList",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    lt.Clear();
                    foreach(DataRow dr in dt.Rows)
                    {
                        lt.Add(dr["NAME"].ToString());
                    }
                    ProcessList = new ComboBox();
                    ProcessList.DataSource = lt;


                    //DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
                    //{
                    //    Name = "Replaced_Skill_Name", 
                    //    HeaderText = "REPLACED_SKILL_NAME", 
                    //    DataSource = dt, 
                    //    DisplayMember = "NAME", 
                    //    //ValueMember = "Id", 
                    //    DataPropertyName = "NAME"
                    //}; 
                    //if (dataGridView1.Columns.Count > 9)
                    //{
                    //    dataGridView1.Columns.RemoveAt(9); 
                    //}
                    //dataGridView1.Columns.Insert(9, comboBoxColumn);
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
        public void GetOnlineOfflineCount(string ProcessType)
        {

            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ProcessType", ProcessType);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_SMAPI",//类库名
                                            "SJ_SMAPI.MPAC_Allocation",//类名
                                            "Get_MPAC_Emp_Count",//方法名
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
                                            "SJ_SMAPI",
                                            "SJ_SMAPI.MPAC_Allocation",
                                            "Get_MPAC_Emp_List",
                                            Program.Client.UserToken,
                                            Newtonsoft.Json.JsonConvert.SerializeObject(p));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable OnlineList = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["OnlineList"].ToString());
                    DataTable OfflineList = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["OfflineList"].ToString());
                    if (OnlineList.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = OnlineList;
                    }
                    else
                    {
                        dataGridView2.DataSource = null;
                    }
                    if (OfflineList.Rows.Count > 0)
                    {
                        dataGridView3.DataSource = OfflineList;
                    }
                    else
                    {
                        dataGridView3.DataSource = null;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Get_Requested_List();
        }

        public void Get_Requested_List()
        {
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Production_plant", comboBox1.Text);
                string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                                            "SJ_SMAPI.MPAC_Allocation",
                                            "Get_Requested_List",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if(dtJson1.Rows.Count>0)
                    {
                        dataGridView1.DataSource = null;
                        //if (dataGridView1.Columns.Count == 2)
                        //{
                        //    dataGridView1.Columns.RemoveAt(1);
                        //}
                        dataGridView1.DataSource = dtJson1;
                        lblcount.Text = dtJson1.Rows.Count.ToString();
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                        GetProcessList();

                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }
        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.ColumnIndex == 6 )
                {
                    Dictionary<string, object> retData = new Dictionary<string, object>();
                    string Barcode = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex - 1].Cells[dataGridView1.CurrentCell.ColumnIndex].Value?.ToString();
                    string Skill_Name = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex - 1].Cells[dataGridView1.CurrentCell.ColumnIndex-1].Value?.ToString();
                    retData.Add("Barcode", Barcode);
                    retData.Add("Skill_Name", Skill_Name);
                    string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                        "SJ_SMAPI.MPAC_Allocation",
                        "Get_Employee",
                        Program.Client.UserToken,
                        Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                    );

                    ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                    if (ret.IsSuccess)
                    {
                        Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                        DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                        if(dtJson1.Rows.Count>0)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsNewRow)
                                    continue;
                                string cellValue1 = row.Cells[7].Value?.ToString();
                                string cellValue2 = row.Cells[8].Value?.ToString();
                                if (cellValue1 == dtJson1.Rows[0]["emp_name"].ToString() && cellValue2 == dtJson1.Rows[0]["dept_name"].ToString())
                                {
                                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "This employee is already assigned above");
                                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex - 1].Cells[dataGridView1.CurrentCell.ColumnIndex];
                                    dataGridView1.CurrentCell.Value = "";
                                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex + 1].Value = "";
                                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex + 2].Value = "";

                                    return;
                                }
                                
                                   
                            }
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex - 1].Cells[dataGridView1.CurrentCell.ColumnIndex + 1].Value = dtJson1.Rows[0]["emp_name"].ToString();
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex - 1].Cells[dataGridView1.CurrentCell.ColumnIndex + 2].Value = dtJson1.Rows[0]["dept_name"].ToString();

                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "No such employee found");
                            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex - 1].Cells[dataGridView1.CurrentCell.ColumnIndex];
                            dataGridView1.CurrentCell.Value = "";
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex + 1].Value = "";
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex + 2].Value = "";
                        } 
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex - 1].Cells[dataGridView1.CurrentCell.ColumnIndex];
                        dataGridView1.CurrentCell.Value = "";
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex + 1].Value = "";
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex + 2].Value = "";
                    }
                }
                else
                {
                    //SJeMES_Control_Library.MessageHelper.ShowErr(this, "No cell is currently selected.");
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    dt1.Columns.Add(column.Name);


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
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dt1", dt1);
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                          "SJ_SMAPI.MPAC_Allocation",
                          "Auto_Allocate_Employee",
                          Program.Client.UserToken,
                          Newtonsoft.Json.JsonConvert.SerializeObject(p)
                      );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                Get_Requested_List();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                Get_Requested_List();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    dt1.Columns.Add(column.HeaderText);


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
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dt1", dt1);
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                          "SJ_SMAPI.MPAC_Allocation",
                          "Manual_Allocate_Employee",
                          Program.Client.UserToken,
                          Newtonsoft.Json.JsonConvert.SerializeObject(p)
                      );
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                Get_Requested_List();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                Get_Requested_List();
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProcessType = comboBox2.Text;
            GetOnlineOfflineCount(ProcessType);
            GetOnlineOfflineList(ProcessType);
        }

        private void txtbcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string ProcessType = comboBox2.Text;
                if (string.IsNullOrEmpty(ProcessType))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select process type");
                    txtbcode.Text = "";
                    return;
                }
                Remove_MPAC_Emp(ProcessType);
                GetOnlineOfflineCount(ProcessType);
                GetOnlineOfflineList(ProcessType);
                txtbcode.Text = "";
            }
        }

        public void Remove_MPAC_Emp(string ProcessType)
        {

            try
            {
                string Barcode = txtbcode.Text;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Barcode", Barcode);
                data.Add("ProcessType", ProcessType);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_SMAPI",
                                            "SJ_SMAPI.MPAC_Allocation",
                                            "Remove_MPAC_Emp",
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "deallocate")
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to de_allocate the employee!", "Prompt", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.OK)
                    {
                        Dictionary<string, object> p = new Dictionary<string, object>();
                        string Emp_No = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        p.Add("Emp_No", Emp_No);
                        string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                    Program.Client.APIURL,
                                                     "SJ_SMAPI",
                                                     "SJ_SMAPI.MPAC_Allocation",
                                                     "De_AllocateEmployee",//方法名
                                                    Program.Client.UserToken,//token
                                                    Newtonsoft.Json.JsonConvert.SerializeObject(p));
                        ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                        if (ret.IsSuccess)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                            Get_Requested_List();
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                            Get_Requested_List();
                        }
                    }

                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "REPLACED_SKILL_NAME")
                {
                    ProcessList = new ComboBox();
                    ProcessList.Enabled = true;
                    ProcessList.DropDownStyle = ComboBoxStyle.DropDownList;
                    ProcessList.DataSource = lt;
                    ProcessList.DisplayMember = "NAME";
                    ProcessList.ValueMember = "NAME";

                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    ProcessList.Left = rect.Left;
                    ProcessList.Top = rect.Top;
                    ProcessList.Width = rect.Width;
                    ProcessList.Height = rect.Height;
                    ProcessList.Visible = true;
                    dataGridView1.Controls.Add(ProcessList);
                    if (dataGridView1.Rows[e.RowIndex].Cells["REPLACED_SKILL_NAME"].Value != null && !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells["REPLACED_SKILL_NAME"].Value.ToString()))
                    {
                        ProcessList.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["REPLACED_SKILL_NAME"].Value.ToString();
                    }
                    else
                    {
                        ProcessList.SelectedIndex = 0;
                    }
                    ProcessList.Focus();
                    ProcessList.SelectedIndexChanged += ProcessList_SelectedIndexChanged1;
                    dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
                }
            }
        }

        private void ProcessList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = ProcessList.Text;
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["REPLACED_SKILL_NAME"].Value = ProcessList.SelectedValue;
            ProcessList.Visible = false;
            ProcessList.Dispose();
        }
        private void button4_Click(object sender, EventArgs e)
        {
           
            try
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();
                retData.Add("Production_plant", comboBox3.Text);
                retData.Add("SDate", dateTimePicker1.Text);
                retData.Add("EDate", dateTimePicker2.Text);
                string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_SMAPI",
                                            "SJ_SMAPI.MPAC_Allocation",
                                            "Get_Final_report",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
                );
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess)
                {
                    Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                    DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                    if (dtJson1.Rows.Count > 0)
                    {
                        dataGridView4.DataSource = null;
                        dataGridView4.DataSource = dtJson1;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                    }

                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "MPAC_Allocation_Report.xls";
                ExportExcels.Export(a, dataGridView4);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

    }
}