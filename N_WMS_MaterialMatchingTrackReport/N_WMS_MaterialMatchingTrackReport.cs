using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N_WMS_MaterialMatchingTrackReport
{
    public partial class N_WMS_MaterialMatchingTrackReport : MaterialForm
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
        private DataTable originalTable;
        private DataTable originalTable2;
        public N_WMS_MaterialMatchingTrackReport()
        {
            InitializeComponent();
            LoadYear();
            LoadMonths(Year);
            StyleLabel(label1);
            StyleLabel(label2);
            StyleLabel(label3);
            StyleLabel(label4);
            StyleLabel(label8);
            StyleLabel(label7);
            StyleLabel(label10); 
            StyleComboBox(comboBox1);
            StyleComboBox(comboBox2);
            StyleComboBox(comboBox3);
            StyleButton(button4);
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
            if( string.IsNullOrEmpty(year))
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* if (string.IsNullOrEmpty(comboBox3.Text.ToString()))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select CRD!");
            }   
            else
            {
                if(tabcontroller.SelectedTab == UWH) 
                {
                    ULoadGridData();
                }
                if(tabcontroller.SelectedTab == BWH)
                {
                    BLoadGridData(); 
                } 
             
            }  */
        }  

        private void ULoadGridData()
        {
            try
            {
                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("crd", comboBox3.Text);
                Data.Add("po", textBox2.Text);
                Data.Add("so", textBox1.Text);

                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                         "KZ_CUTMNT.Controllers.MaterialMatchingTrackingController",
                         "UTrackingData", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);

               var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                    && result.ContainsKey("RetData") && result["RetData"] != "")
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();

                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Columns.Contains("CRD"))
                    {
                        foreach (DataRow row in dtJson.Rows)
                        {
                            if (row["CRD"] != DBNull.Value)
                            {
                                DateTime dt;

                                if (DateTime.TryParse(row["CRD"].ToString(), out dt))
                                {
                                    row["CRD"] = dt.ToString("yyyy/MM/dd");
                                }
                            }
                        }
                    }  
                    dataGridView1.DataSource = dtJson;
                    originalTable = dtJson; 
                    dataGridView1.DataSource = originalTable; 
                    StyleGridAGU(dataGridView1);  
                    if (dtJson.Rows.Count <=0)
                    {
                        MessageHelper.ShowErr(this, "No data found"); 
                    } 
                }
                else if (result["ErrMsg"] != "")
                {
                    dataGridView1.Rows.Clear();
                    MessageHelper.ShowErr(this, "Please Contact With IT Department");
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    MessageHelper.ShowErr(this, "No data found");
                }
            }
            catch (Exception e)
            {
                dataGridView1.Rows.Clear();
                MessageHelper.ShowErr(this, "Please Contact With IT Department");
            }
        }
        private void BLoadGridData()
        {
            try
            {
                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("crd", comboBox3.Text);
                Data.Add("po", textBox2.Text);
                Data.Add("so", textBox1.Text);

                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                         "KZ_CUTMNT.Controllers.MaterialMatchingTrackingController", 
                         "BTrackingData", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                    && result.ContainsKey("RetData") && result["RetData"] != "")
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Columns.Contains("CRD"))
                    {
                        foreach (DataRow row in dtJson.Rows)
                        {
                            if (row["CRD"] != DBNull.Value)
                            {
                                DateTime dt;

                                if (DateTime.TryParse(row["CRD"].ToString(), out dt))
                                {
                                    row["CRD"] = dt.ToString("yyyy/MM/dd");
                                }
                            }
                        } 
                    }
                     
                    dataGridView2.DataSource = dtJson;
                    originalTable2 = dtJson;
                    dataGridView2.DataSource = originalTable2; 
                    StyleGridAGB(dataGridView2);
                }
                else if (result["ErrMsg"] != "")
                {
                    dataGridView2.Rows.Clear();
                    MessageHelper.ShowErr(this, "Please Contact With IT Department");
                }
                else
                {
                    dataGridView2.Rows.Clear();
                    MessageHelper.ShowErr(this, "No data found");
                }
            }
            catch (Exception e)
            {
                dataGridView2.Rows.Clear();
                MessageHelper.ShowErr(this, "Please Contact With IT Department");
            } 
        } 

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is RichTextBox)) { return; }
            RichTextBox richText = sender as RichTextBox;
            if (richText.Text.Contains("\n"))
            {
                ExcelFormat(richText);
            }
        }
        private void ExcelFormat(RichTextBox richText)
        {
            string[] str = richText.Text.Split(new string[] { "\t\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (str.Length <= 1)
                str = richText.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            string se_id = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (se_id.Length > 0)
                {
                    se_id += ",";
                }
                se_id += str[i];

            }
            richText.Text = se_id;
            richText.Font = new System.Drawing.Font("Tahoma", 10);

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is RichTextBox)) { return; }
            RichTextBox richText = sender as RichTextBox;
            if (richText.Text.Contains("\n"))
            {
                ExcelFormat(richText);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void N_WMS_MaterialMatchingTrackReport_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

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

        private void StyleLabel(Label lbl)
        {
            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbl.ForeColor = Color.DarkBlue;
            lbl.AutoSize = true;
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
        private void StyleGridAGU(DataGridView dgv)  
        { 
            dgv.EnableHeadersVisualStyles = false; 
            var headerFont = new Font("Segoe UI", 10F, FontStyle.Bold); 
            for (int i = 0; i < 5; i++)
            {
                dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(0, 120, 215);
                dgv.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                dgv.Columns[i].HeaderCell.Style.Font = headerFont;
            } 
            for (int i = 5; i < 8; i++)
            {
                dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(0, 153, 76);
                dgv.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                dgv.Columns[i].HeaderCell.Style.Font = headerFont;
            } 
            for (int i = 8; i < 11; i++)
            {
                dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(255, 140, 0);
                dgv.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                dgv.Columns[i].HeaderCell.Style.Font = headerFont;
            }
            dgv.ColumnHeadersHeight = 35;
            dgv.ScrollBars = ScrollBars.Both;   // Horizontal + Vertical 
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        }
        private void StyleGridAGB(DataGridView dgv) 
        {
            dgv.EnableHeadersVisualStyles = false;
            var headerFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            for (int i = 0; i < 5; i++)
            {
                dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(0, 120, 215);
                dgv.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                dgv.Columns[i].HeaderCell.Style.Font = headerFont;
            }
            for (int i = 5; i < 8; i++)
            {
                dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(0, 153, 76);
                dgv.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                dgv.Columns[i].HeaderCell.Style.Font = headerFont;
            }
            for (int i = 8; i < 11; i++)
            {
                dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(255, 140, 0);
                dgv.Columns[i].HeaderCell.Style.ForeColor = Color.White;
                dgv.Columns[i].HeaderCell.Style.Font = headerFont;
            }
            dgv.ColumnHeadersHeight = 35;
            dgv.ScrollBars = ScrollBars.Both;   // Horizontal + Vertical
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Search Button 
            if (string.IsNullOrEmpty(comboBox3.Text.ToString()))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select CRD!");
            }
            else 
            {
                if (tabcontroller.SelectedTab == UWH)
                {
                    ULoadGridData();
                }
                if (tabcontroller.SelectedTab == BWH)
                {
                    BLoadGridData();
                }

            } 
        } 

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Outsourcing Upper 
            // Available Stock 
            Reset();
            if (tabcontroller.SelectedTab == UWH)
            {
                if (originalTable != null)
                {
                    DataView dv = new DataView(originalTable);
                    dv.RowFilter = "MATERIAL_STAGE LIKE '%STOCK IN 2009%'";
                    dataGridView1.DataSource = dv;
                } 
            }
            if (tabcontroller.SelectedTab == BWH)
            {
                if (originalTable2 != null)
                {
                    DataView dv = new DataView(originalTable2);
                    dv.RowFilter = "MATERIAL_STAGE LIKE '%STOCK IN 2008%'";
                    dataGridView2.DataSource = dv; 
                }
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reset();
            if (tabcontroller.SelectedTab == UWH)
            {
                if (originalTable != null)
                {
                    DataView dv = new DataView(originalTable);
                    dv.RowFilter = "MATERIAL_STAGE LIKE '%NO STOCK%'";
                    dataGridView1.DataSource = dv;
                } 
            }
            if (tabcontroller.SelectedTab == BWH)
            {
                if (originalTable2 != null)
                {
                    DataView dv = new DataView(originalTable2);
                    dv.RowFilter = "MATERIAL_STAGE LIKE '%NO STOCK%'"; 
                    dataGridView2.DataSource = dv;
                }
            }             
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // In line Stock 
            // Available Stock 
            Reset();
            if (tabcontroller.SelectedTab == UWH)
            {
                if (originalTable != null)
                {
                    DataView dv = new DataView(originalTable);
                    dv.RowFilter = "MATERIAL_STAGE LIKE '%STOCK IN 3001%'";
                    dataGridView1.DataSource = dv;
                }
            }
            if (tabcontroller.SelectedTab == BWH)
            {
                if (originalTable2 != null)
                {
                    DataView dv = new DataView(originalTable2);
                    dv.RowFilter = "MATERIAL_STAGE LIKE '%STOCK IN 3002%'"; 
                    dataGridView2.DataSource = dv; 
                }
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Available Stock 
            Reset();
            if (tabcontroller.SelectedTab == UWH)
            {
                if (originalTable != null)
                {
                    DataView dv = new DataView(originalTable);
                    dv.RowFilter = "MATERIAL_STAGE LIKE '%STOCK IN 2009%' OR MATERIAL_STAGE LIKE '%STOCK IN 3001%'";
                    dataGridView1.DataSource = dv;
                } 
            }
            if (tabcontroller.SelectedTab == BWH)
            {
                if (originalTable2 != null)
                {
                    DataView dv = new DataView(originalTable2);
                    dv.RowFilter = "MATERIAL_STAGE LIKE '%STOCK IN 2008%' OR MATERIAL_STAGE LIKE '%STOCK IN 3002%'";
                    dataGridView2.DataSource = dv;
                }
            } 
        }
        private void Reset()
        {
            if (originalTable != null)
            {
                dataGridView1.DataSource = originalTable;
            }
            if (originalTable2 != null)
            {
                dataGridView2.DataSource = originalTable2;
            }
        } 

    }
}
