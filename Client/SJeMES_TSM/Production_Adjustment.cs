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
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using AutocompleteMenuNS;
using NewExportExcels;

namespace SJeMES_TSM
{
    public partial class Production_Adjustment : MaterialForm
    {
        AutoCompleteStringCollection Autodata;
        public Production_Adjustment()
        {
            InitializeComponent();
            checkBox2.CheckedChanged += CheckBox2_CheckedChanged;
            checkBox2.Checked = false;
            LoadProd_Line();
            autocompleteMenu1.SetAutocompleteMenu(txtsupdept, autocompleteMenu1);
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true) 
            {
                autocompleteMenu1.Items = new string[0];
                LoadSupport_Dept();
                autocompleteMenu1.SetAutocompleteMenu(txtsupdept, autocompleteMenu1);
                cb_processname.Text = "";
                txtsupdept.Text = "";
            }
            else
            {
                autocompleteMenu1.Items = new string[0];
                LoadProd_Line();
                autocompleteMenu1.SetAutocompleteMenu(txtsupdept, autocompleteMenu1);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    AddEmployee();
                    textBox1.Text = "";
            }
        }
        public void AddEmployee()
        {
             
            try
            {    
                string Barcode = textBox1.Text;
                Barcode = Barcode.Substring(1, 5);
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Barcode", Barcode); 
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.Production_Adjustment",//类名
                                            "AddEmployee",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    GetTC_InwardEmployee();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    GetTC_InwardEmployee();
                }
               
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ex.Message);
            }
        }

        public string GetEmpDept()
        {
            string Dept_Name = string.Empty; 
            try
            {
                string Barcode = textBox1.Text;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Barcode", Barcode); 
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.Production_Adjustment",//类名
                                            "GetEmpDept",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata.ToString());
                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg); 
                }
                else
                {
                    Dept_Name=ret.RetData;
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ex.Message);
            }
            return Dept_Name;
        }

        public void GetTC_InwardEmployee() 
        {

            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI",//类库名
                                            "SJ_TSMAPI.Production_Adjustment",//类名
                                            "GetTC_InwardEmployee",//方法名
                                            Program.Client.UserToken,//token
                                            Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ex.Message);
            }
        }
        private void Production_Adjustment_Load(object sender, EventArgs e)
        {
            //LoadProd_Line();      
            //autocompleteMenu1.SetAutocompleteMenu(txtsupdept, autocompleteMenu1);
            GetTC_InwardEmployee(); 
            
        }
        public void LoadProd_Line()   
        {
            txtsupdept.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtsupdept.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI",
                                            "SJ_TSMAPI.Production_Adjustment",
                                            "Get_Prod_line",
           Program.Client.UserToken, JsonConvert.SerializeObject(kk));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata); 
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count <= 0)
            {
                txtsupdept.AutoCompleteCustomSource.Clear();
            }
            else
            {
                autocompleteMenu1.MaximumSize = new Size(250, 350);
               
                var columnWidth = new[] { 50, 200 };
                int n = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["department_code"].ToString() }, dt.Rows[i]["department_code"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                    n++;
                }
            }
        }

        private void txtsupdept_TextChanged(object sender, EventArgs e)
        {
            GetProcessList();
        }

        public void GetProcessList()
        {
            string Type = string.Empty;
            if (checkBox2.Checked)
            {
                Type = "Supporting";
            }
            DataTable dt = new DataTable();
            string Supp_Dept = txtsupdept.Text;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Supp_Dept", Supp_Dept);
            data.Add("Type", Type);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TSMAPI",//类库名
                                        "SJ_TSMAPI.Production_Adjustment",//类名
                                        "GetProcessList",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data)); 
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            cb_processname.Text = "";
            cb_processname.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cb_processname.Items.Add(dr["name"].ToString());
            }
        }

        private void cb_processname_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAvailableEmployee();
        }
        public void GetAvailableEmployee()
        {
            string Barcode = txtbcode.Text;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Barcode", Barcode);
            DataTable dt = new DataTable();
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TSMAPI",//类库名
                                        "SJ_TSMAPI.Production_Adjustment",//类名
                                        "GetAvailableEmployee",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if(dt.Rows.Count>0)
            {
                dataGridView2.DataSource = dt;
                if (dt.Rows.Count == 1 && dt.Rows[0]["STATUS"].ToString() == "0")
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        row.Cells["choose"].Value = true;
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["STATUS"].ToString() == "1")
                    {
                        DataGridViewCellStyle style1 = new DataGridViewCellStyle();
                        style1.BackColor = Color.LightGreen;
                        int a = i;
                        dataGridView2.Rows[a].DefaultCellStyle = style1;
                        dataGridView2.Rows[a].ReadOnly = true;
                    }
                }
            }
            else
            {
                dataGridView2.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");

            }
           
        }

        public void GetMPACEmployee()
        {
            string Barcode = txtbcode.Text;
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Barcode", Barcode);
            DataTable dt = new DataTable();
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TSMAPI",//类库名
                                        "SJ_TSMAPI.Production_Adjustment",//类名
                                        "GetMPACEmployee",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count > 0)
            {
                dataGridView2.DataSource = dt;
                if (dt.Rows.Count == 1 && !(dt.Rows[0]["STATUS"].ToString() == "1"))
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        row.Cells["choose"].Value = true;
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["STATUS"].ToString() == "1")
                    {
                        DataGridViewCellStyle style1 = new DataGridViewCellStyle();
                        style1.BackColor = Color.LightGreen;
                        int a = i;
                        dataGridView2.Rows[a].DefaultCellStyle = style1;
                        dataGridView2.Rows[a].ReadOnly = true;
                    }
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (dataGridView2.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView2.Columns)
                    dt.Columns.Add(column.HeaderText);

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["choose"].Value);
                    if (isSelected)
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);

                    }
                }
            }
            if(dt.Rows.Count>0)
            {
                if(string.IsNullOrEmpty(txtsupdept.Text)|| string.IsNullOrEmpty(cb_processname.Text))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please fill necessary conditions");
                    return;
                }
                string supdept = txtsupdept.Text;
                string process_name = cb_processname.Text;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("supdept", supdept);
                data.Add("process_name", process_name);
                data.Add("dt", dt);
                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                            Program.Client.APIURL,
                                            "SJ_TSMAPI", 
                                            "SJ_TSMAPI.Production_Adjustment", 
                                            "AllocateEmployee", 
                                            Program.Client.UserToken, 
                                            Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (!ret.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                    if(checkBox1.Checked)
                    {
                        GetMPACEmployee();
                    }
                    else
                    {
                        GetAvailableEmployee();
                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret.ErrMsg);
                    if (checkBox1.Checked)
                    {
                        GetMPACEmployee();
                    }
                    else
                    {
                        GetAvailableEmployee();
                    }
                }
                txtsupdept.Text = "";
                cb_processname.Text = "";
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this,"Please select supporting employees");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Production_Support_List.xls";
                ExportExcels.Export(a, dataGridView2);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "btn_de_allocate")
                { 
                        DialogResult dr = MessageBox.Show("Are you sure you want to de_allocate the employee!", "Prompt", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (dr == DialogResult.OK)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            string Emp_No = dataGridView2.Rows[e.RowIndex].Cells["empno"].Value.ToString();
                            p.Add("Emp_No", Emp_No);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                                        Program.Client.APIURL,
                                                        "SJ_TSMAPI",//类库名
                                                        "SJ_TSMAPI.Production_Adjustment",//类名
                                                        "De_AllocateEmployee",//方法名
                                                        Program.Client.UserToken,//token
                                                        Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                               SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                            if (checkBox1.Checked)
                            {
                                GetMPACEmployee();
                            }
                            else
                            {
                                GetAvailableEmployee();
                            }
                        }
                        }

                    }
                }
            }

        private void txtbcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(checkBox1.Checked)
                {
                    GetMPACEmployee();
                }
                else
                {
                    GetAvailableEmployee();
                }
               
                txtbcode.Text = "";
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tagName = tabControl1.SelectedTab.Name;
            TagDataLoad(tagName);
        }
        public void TagDataLoad(string tagName)
        {  
            switch (tagName)
            {
                case "tabPage2":
                    checkBox1.Checked = false;
                    GetAvailableEmployee();
                    break; 
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string SDate = dateTimePicker1.Text;
            string EDate = dateTimePicker2.Text; 
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("SDate", SDate);
            data.Add("EDate", EDate);
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.Client.APIURL,
                                        "SJ_TSMAPI",//类库名
                                        "SJ_TSMAPI.Production_Adjustment",//类名
                                        "GetProductionSupport_Report",//方法名
                                        Program.Client.UserToken,//token
                                        Newtonsoft.Json.JsonConvert.SerializeObject(data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt2 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data2"].ToString());
            dt1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data1"].ToString());
            if (dt1.Rows.Count > 0 || dt2.Rows.Count > 0)
            {
                dataGridView3.DataSource = dt1;
                dataGridView4.DataSource = dt2;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                dataGridView3.DataSource = null;
                dataGridView4.DataSource = null;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtbcode.Text = "";
            txtsupdept.Text = "";
            if (checkBox1.Checked)
            {
                GetMPACEmployee();
            }
            else
            {
                GetAvailableEmployee();
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Excess_employee_report.xls";
                ExportExcels.Export(a, dataGridView3);
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
                string a = "MPAC_employee_report.xls";
                ExportExcels.Export(a, dataGridView4);
            }
        }

        //private void checkBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    if(checkBox2.Checked)
        //    {
        //        LoadSupport_Dept();
        //        autocompleteMenu1.SetAutocompleteMenu(txtsupdept, autocompleteMenu1);
        //        cb_processname.Text = "";
        //        txtsupdept.Text = "";
        //    }
        //    else
        //    {
        //        LoadProd_Line();
        //        autocompleteMenu1.SetAutocompleteMenu(txtsupdept, autocompleteMenu1);
        //        cb_processname.Text = "";
        //        txtsupdept.Text = "";
        //    }

            
        //}

        public void LoadSupport_Dept()
        {
            txtsupdept.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtsupdept.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            string retdata = WebAPIHelper.Post(Program.Client.APIURL, "SJ_TSMAPI",
                                            "SJ_TSMAPI.Production_Adjustment",
                                            "LoadSupport_Dept",
           Program.Client.UserToken, JsonConvert.SerializeObject(kk));

            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
            dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
            if (dt.Rows.Count <= 0)
            {
                txtsupdept.AutoCompleteCustomSource.Clear();
            }
            else
            {
                autocompleteMenu1.MaximumSize = new Size(250, 350);

                var columnWidth = new[] { 50, 200 };
                int n = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_NAME"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                    n++;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    

