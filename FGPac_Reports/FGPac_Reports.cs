using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;

namespace FGPac_Reports
{
    public partial class FGPac_Reports : Form
    {
        public class ComboboxEntry
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
        private string CompanyCode = null;
        private string PlantCode = null;
        private string ProcessCode = null;
        private string Year = null;
        private string Month = null;

        public FGPac_Reports()
        {

            InitializeComponent();
            LoadYear();
            LoadMonths(Year);
            StyleComboBox(comboBox1);
            StyleComboBox(comboBox2);
            StyleComboBox(comboBox3);
            StyleComboBox(comboBox4);
            StyleLabel(label1);
            StyleLabel(label2);
            StyleLabel(label3);
            StyleLabel(label4);
            StyleLabel(label5);
            StyleLabel(label7);
            StyleLabel(label8);
            StyleLabel(label10); 
            StyleButton(button1);
            StyleGrid(dataGridView1);
            StyleGrid(dataGridView2);
            textBox2.Multiline = true;
            textBox2.AcceptsReturn = true;
            textBox2.AcceptsTab = false;
            textBox2.ScrollBars = ScrollBars.Vertical; 
            textBox2.WordWrap = false;
            textBox2.Font = new Font("Consolas", 11, FontStyle.Regular); 
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox2.Padding = new Padding(5);
            textBox2.MinimumSize = new Size(200, 50);      

            textBox1.Multiline = true;
            textBox1.AcceptsReturn = true;
            textBox1.AcceptsTab = false;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.WordWrap = false;
            textBox1.Font = new Font("Consolas", 11, FontStyle.Regular);
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox1.Padding = new Padding(5);
            textBox1.MinimumSize = new Size(200, 50);
            LoadPlants();
            // dataGridView1.CellClick += dataGridView1_CellClick; 
            dataGridView1.CellClick -= dataGridView1_CellClick;
            dataGridView1.CellClick += dataGridView1_CellClick;

        }
        private void LoadYear()
        {
            try
            {
                List<ComboboxEntry> years = new List<ComboboxEntry> { };
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.MaterialMatchingTrackingController",
                    "GETYearss", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty)); 

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                   && result.ContainsKey("RetData") && result["RetData"] != "")
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);

                    years.Add(new ComboboxEntry() { Code = "", Name = "" });
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        years.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["CRD"].ToString(), Name = dtJson.Rows[i]["CRD"].ToString() });
                    } 
                    comboBox1.DataSource = years;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "Code";
                    //// Default: select the first item
                    //if (dtJson.Rows.Count > 0)
                    //{
                    //    comboBox1.SelectedIndex = 0;
                    //    string defaultOrgCode = comboBox1.SelectedValue.ToString();
                    //    Year = defaultOrgCode;

                    //}

                    // Handle selection changes
                    comboBox1.SelectedIndexChanged += (s, e) =>
                    {
                        if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedValue != null)
                        {
                            string selectedCode = comboBox1.SelectedValue.ToString();
                            string selectedName = comboBox1.Text;
                            Year = selectedCode;
                            LoadMonths(Year);
                        }
                    };
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        private void LoadMonths(string year)
        {
            if (string.IsNullOrEmpty(year))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Year!");
            }
            else
            {
                int Year_int = int.Parse(year);

                var months = Enumerable.Range(1, 12)
                    .Select(m => new
                    {
                        MonthNo = m,
                        MonthName = new DateTime(Year_int, m, 1).ToString("MMMM")
                    })
                    .ToList();
                months.Insert(0, new { MonthNo = 0, MonthName = "" });
                comboBox2.DataSource = months;
                comboBox2.DisplayMember = "MonthName";
                comboBox2.ValueMember = "MonthNo";
                comboBox2.SelectedIndexChanged += (s, e) =>
                {
                    if (comboBox2.SelectedIndex >= 0 && comboBox2.SelectedValue != null)
                    {
                        string selectedCode = comboBox2.SelectedValue.ToString();
                        string selectedName = comboBox2.Text;
                        Month = selectedCode;
                        LoadCRDWeek(Month);
                    }
                };

            }

        }
        private void LoadCRDWeek(string month)
        {

            if (string.IsNullOrEmpty(Year))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Factory!");
            }
            else if (string.IsNullOrEmpty(Month))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Plant!");
            }
            else
            {
                try
                {
                    List<ComboboxEntry> Weeks = new List<ComboboxEntry> { };
                    Dictionary<string, object> Data = new Dictionary<string, object>();
                    Data.Add("year", Year);
                    Data.Add("month", Month);
                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.MaterialMatchingTrackingController",
                    "GetCRD", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                       && result.ContainsKey("RetData") && result["RetData"] != "")
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                        DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                        Weeks.Add(new ComboboxEntry() { Code = "", Name = "" });
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            Weeks.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["CRD"].ToString(), Name = dtJson.Rows[i]["CRD"].ToString() });
                        }
                        comboBox3.DataSource = Weeks;
                        comboBox3.DisplayMember = "Name";
                        comboBox3.ValueMember = "Code";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadPlants() 
        {
                try
                {
                    List<ComboboxEntry> Plants = new List<ComboboxEntry> { };
                    Dictionary<string, object> Data = new Dictionary<string, object>();
                    Data.Add("OrgId", 5001); 
                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.MaterialMatchingTrackingController",
                    "LoadPlant", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                       && result.ContainsKey("RetData") && result["RetData"] != "")
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                        DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                    Plants.Add(new ComboboxEntry() { Code = "", Name = "" });
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                        Plants.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["code"].ToString(), Name = dtJson.Rows[i]["name"].ToString() });
                        }
                        comboBox4.DataSource = Plants;
                        comboBox4.DisplayMember = "Name";
                        comboBox4.ValueMember = "Code"; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
        private void StyleComboBox(ComboBox cb)
        {
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            cb.BackColor = Color.White;
            cb.ForeColor = Color.Black;
            // cb.FlatStyle = FlatStyle.Flat;

            cb.Height = 30;
            cb.Width = 200;
        }
        private void StyleButton(Button btn)
        {
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(0, 120, 215); // Blue
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Height = 35;
            btn.Width = 120;
            btn.Cursor = Cursors.Hand;
        }
        private void StyleGrid(DataGridView dgv)
        {
            // Read-only
           // dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // General look
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.Gainsboro;

            // Header style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215); // Blue
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;

            // Row style
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Alternate row color
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 250);

            // Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Remove row header
            dgv.RowHeadersVisible = false;

            // Row height
            dgv.RowTemplate.Height = 30;
        }
        private void StyleLabel(System.Windows.Forms.Label lbl) 
        {
            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbl.ForeColor = Color.DarkBlue;
            lbl.AutoSize = true;
        } 
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox3.Text.ToString()))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select CRD!");
            }
            if (tabControl1.SelectedTab == tabPage1) 
            {
                LoadGridData();
            }
            if (tabControl1.SelectedTab == tabPage2)  
            {
                FGReasonListData(); 
            } 
        } 

        private void LoadGridData()
        {
            try
            {
                Dictionary<string, object> Data = new Dictionary<string, object>()
               {
            { "crd", comboBox3.Text },
            { "po", textBox2.Text },
            { "so", textBox1.Text },
            { "plant", comboBox4.Text }
             };

                string retdata = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.MaterialMatchingTrackingController",
                    "TrackingData",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(Data)
                );

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (result != null &&
                    result.ContainsKey("IsSuccess") &&
                    Convert.ToBoolean(result["IsSuccess"]) &&
                    result.ContainsKey("RetData") &&
                    result["RetData"] != null)
                {
                    string json = result["RetData"].ToString();

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.Columns.Clear(); // 🔥 Important to avoid duplicates
                        dataGridView1.DataSource = dt;

                        AddOrUpdateReasonColumn();
                        AddActionColumn();

                        MessageHelper.ShowSuccess(this, "Data Retrieved Successfully...");
                    }
                    else
                    {
                        ClearGrid();
                        MessageHelper.ShowErr(this, "No data found");
                    }
                }
                else
                {
                    ClearGrid();

                    if (result != null && result.ContainsKey("ErrMsg") && result["ErrMsg"] != null)
                        MessageHelper.ShowErr(this, result["ErrMsg"].ToString());
                    else
                        MessageHelper.ShowErr(this, "No data found");
                }
            }
            catch (Exception ex)
            {
                ClearGrid();
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }
        }
        private void FGReasonListData()  
        {
            try
            {
                Dictionary<string, object> Data = new Dictionary<string, object>()
               {
            { "crd", comboBox3.Text },
            { "po", textBox2.Text },
            { "so", textBox1.Text },
            { "plant", comboBox4.Text }
             };

                string retdata = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.MaterialMatchingTrackingController",
                    "FGReasonListData",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(Data) 
                );

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (result != null &&
                    result.ContainsKey("IsSuccess") &&
                    Convert.ToBoolean(result["IsSuccess"]) &&
                    result.ContainsKey("RetData") &&
                    result["RetData"] != null)
                {
                    string json = result["RetData"].ToString();

                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = null;
                        dataGridView2.Columns.Clear(); 
                        dataGridView2.DataSource = dt;
                        MessageHelper.ShowSuccess(this, "Data Retrieved Successfully...");
                    }
                    else
                    {
                        ClearGrid2();
                        MessageHelper.ShowErr(this, "No data found");
                    }
                }
                else
                {
                    ClearGrid2();

                    if (result != null && result.ContainsKey("ErrMsg") && result["ErrMsg"] != null)
                        MessageHelper.ShowErr(this, result["ErrMsg"].ToString());
                    else
                        MessageHelper.ShowErr(this, "No data found");
                }
            }
            catch (Exception ex)
            {
                ClearGrid2();
                MessageHelper.ShowErr(this, "Error: " + ex.Message); 
            }
        }

        private void AddOrUpdateReasonColumn()
        {
            if (dataGridView1.Columns.Contains("REASON"))
            {
                int index = dataGridView1.Columns["REASON"].Index;

                // Store existing values
                List<string> values = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    values.Add(row.Cells["REASON"].Value?.ToString());
                }

                dataGridView1.Columns.Remove("REASON");

                DataGridViewComboBoxColumn comboCol = new DataGridViewComboBoxColumn();
                comboCol.Name = "REASON";
                comboCol.HeaderText = "REASON";
                comboCol.FlatStyle = FlatStyle.Flat;

                // Static values
                comboCol.Items.Add("");
                comboCol.Items.Add("Lack of Packing Lables"); 
                comboCol.Items.Add("Without Inner Boxes");
                comboCol.Items.Add("Without Outer Cartons");
                comboCol.Items.Add("Without Wrapping paper");
                comboCol.Items.Add("Mixture Packing");
                comboCol.Items.Add("Special Packing");
                comboCol.Items.Add("With out sockliners");
                comboCol.Items.Add("With out lace");
                comboCol.Items.Add("Customer hold");
                comboCol.Items.Add("Dummy Price Issue");
                comboCol.Items.Add("Pack Plan not Added");
                comboCol.Items.Add("PO Cancel");
                comboCol.Items.Add("Shot Shipment Balance Pairs");
                comboCol.Items.Add("Combined Mixtures"); 

                dataGridView1.Columns.Insert(index, comboCol);

                // Restore values
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["REASON"].Value = values[i];
                }
            }
        }
        private void AddActionColumn()
        {
            if (dataGridView1.Columns.Contains("ACTION"))
            {
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.Name = "Action";
                btnCol.HeaderText = "Action";
                btnCol.Text = "Save";
                btnCol.UseColumnTextForButtonValue = true;
                btnCol.DefaultCellStyle.ForeColor = Color.Black;
                btnCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btnCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Optional: Button background color
                btnCol.DefaultCellStyle.BackColor = Color.LightGray;

                dataGridView1.Columns.Add(btnCol);
                dataGridView1.Columns.Remove("ACTION");  
            }
        } 

        private void ClearGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
        }
        private void ClearGrid2()
        {
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear(); 
            dataGridView2.Columns.Clear();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Action") 
            {
                try
                {
                    string seid = dataGridView1.Rows[e.RowIndex].Cells["Sales_Order"].Value?.ToString();
                    string pacqty = dataGridView1.Rows[e.RowIndex].Cells["PAC_QTY"].Value?.ToString();
                    string line = dataGridView1.Rows[e.RowIndex].Cells["Plant"].Value?.ToString();
                    string fgqty = dataGridView1.Rows[e.RowIndex].Cells["FG_Qty"].Value?.ToString();
                    string reason = dataGridView1.Rows[e.RowIndex].Cells["REASON"].Value?.ToString();
                    string crd = dataGridView1.Rows[e.RowIndex].Cells["CRD"].Value?.ToString(); 
                    if (string.IsNullOrEmpty(reason))
                    {
                        MessageBox.Show("Please select a reason.");
                        return;
                    }
                    dataGridView1.Rows[e.RowIndex].Cells["Action"].ReadOnly = true; 
                    Dictionary<string, object> Data = new Dictionary<string, object>()
                                                      {
                                                        { "seid", seid },
                                                        { "pacqty", pacqty },
                                                        { "line", line } ,
                                                        { "fgqty", fgqty },
                                                        { "reason", reason } ,
                                                        { "crd", crd } 
                                                      };

                    string retdata = WebAPIHelper.Post(
                        Program.client.APIURL,
                        "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.MaterialMatchingTrackingController",
                        "InsertReasonData",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(Data)
                    );

                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"]))
                    {
                        MessageHelper.ShowSuccess(this , "Saved successfully"); 

                        // Optional UI change after save
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen; 
                    }
                    else
                    {
                        string err = result.ContainsKey("RetData") ? result["RetData"]?.ToString() : "Error occurred";
                        if(err == "ORA-00001: unique constraint (MES00.SEID_CT) violated")
                        {
                            MessageHelper.ShowErr(this, "The SO Status Already Saved..");  
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, err);
                        }

                        // Enable button again if failed
                        dataGridView1.Rows[e.RowIndex].Cells["Action"].ReadOnly = false;
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); 
                } 
            }
        }  


    }
}
