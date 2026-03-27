using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using SJeMES_Framework.Common;
using SJeMES_Control_Library;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using LicenseContext = OfficeOpenXml.LicenseContext;
using System.IO; 

namespace PlanningSchedule_Reports 
{

    public partial class PlanningSchdule_Reports : Form 
    {
        public class ComboboxEntry 
        { 
            public string Code { get; set; } 
            public string Name { get; set; } 
        } 
         
       
        private string CompanyCode = null;
        private string PlantCode = null;
        private string ProcessCode = null;
      


        public PlanningSchdule_Reports() 
        { 
            InitializeComponent();
            //premika
            LoadOrg() ;  
            LoadPlants(CompanyCode) ;   
            LoadProcess();  
            comboBox3.SelectedValue = "";  
            comboBox4.SelectedValue = "";  
            StyleRoundedButton(button3, Color.FromArgb(0, 153, 0)); // search btn
            StyleRoundedButton(button5, Color.FromArgb(0, 0, 204)); //excel button
            StyleRoundedButton(button1, Color.FromArgb(204, 0, 204)); //clear button
            StyleComboBox(comboBox1);
            StyleComboBox(comboBox2);
            StyleComboBox(comboBox3);
            StyleComboBox(comboBox4);
            DesignDataGridView(dataGridView1);
          
            dataGridView1.Refresh();
            dataGridView1.DefaultCellStyle.Padding = new Padding(5);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            textBox2.Multiline = true;
            textBox2.AcceptsReturn = true;
            textBox2.AcceptsTab = false;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.WordWrap = false;
            textBox2.Font = new Font("Consolas", 11, FontStyle.Regular);
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox2.Padding = new Padding(5);
            textBox2.MinimumSize = new Size(200, 100);

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
        private void LoadLines(string OrgId, String Plant, String Process)
        {
            if (string.IsNullOrEmpty(OrgId))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Factory!");
            }
            else if (string.IsNullOrEmpty(Plant))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Plant!");
            }
            else if (string.IsNullOrEmpty(Process))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Process!");
            }
            else
            {
                try
                {
                    List<ComboboxEntry> Lines = new List<ComboboxEntry> { };
                    Dictionary<string, object> Data = new Dictionary<string, object>();
                    Data.Add("OrgId", OrgId);
                    Data.Add("Plant", Plant);
                    Data.Add("Process", Process);
                    string retdata = WebAPIHelper.Post(SizeWisePlanningScheduleReports.Program.client.APIURL, "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.SizePlanningController",
                        "LoadLines", SizeWisePlanningScheduleReports.Program.client.UserToken, JsonConvert.SerializeObject(Data));
                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                       && result.ContainsKey("RetData") && result["RetData"] != "")
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                        DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                        Lines.Add(new ComboboxEntry() { Code = "", Name = "" });
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            Lines.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["department_code"].ToString(), Name = dtJson.Rows[i]["department_name"].ToString() });
                        }
                        comboBox2.DataSource = Lines;
                        comboBox2.DisplayMember = "Name";
                        comboBox2.ValueMember = "Code";



                        List<string> selectedCodes = new List<string>();

                        comboBox2.SelectedIndexChanged += (s, e) =>
                        {
                            if (comboBox2.SelectedValue != null)
                            {
                                string selectedCode = comboBox2.SelectedValue.ToString().Trim();

                                // If empty selection, clear all
                                if (string.IsNullOrEmpty(selectedCode))
                                {
                                    selectedCodes.Clear();
                                    textBox1.Clear();
                                    return;
                                }

                                // Add if not already selected
                                if (!selectedCodes.Contains(selectedCode))
                                {
                                    selectedCodes.Add(selectedCode);
                                }

                                // Update textBox with comma-separated list
                                textBox1.Text = string.Join(",", selectedCodes);
                            }
                        };


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadProcess()
        {
            List<ComboboxEntry> Process = new List<ComboboxEntry> { };

            Process.Add(new ComboboxEntry() { Code = "", Name = "" });
            Process.Add(new ComboboxEntry() { Code = "C", Name = "Cutting" });
            Process.Add(new ComboboxEntry() { Code = "S", Name = "Stitching" });
            Process.Add(new ComboboxEntry() { Code = "L", Name = "Assembly" });

            comboBox4.DataSource = Process;
            comboBox4.DisplayMember = "Name";
            comboBox4.ValueMember = "Code";

            comboBox4.SelectedIndexChanged += (s, e) =>
            {
                if (comboBox4.SelectedIndex >= 0 && comboBox4.SelectedValue != null)
                {
                    string selectedCode = comboBox4.SelectedValue.ToString();
                    string selectedName = comboBox4.Text;
                    ProcessCode = selectedCode;
                    LoadLines(CompanyCode, PlantCode, ProcessCode);

                }
            };


        }
        private void LoadOrg()   
        {
            try
            {
                List<ComboboxEntry> Companies = new List<ComboboxEntry> { };
                string retdata = WebAPIHelper.Post(SizeWisePlanningScheduleReports.Program.client.APIURL, "KZ_CUTMNT", 
                    "KZ_CUTMNT.Controllers.SizePlanningController",  
                    "LoadOrg", SizeWisePlanningScheduleReports.Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));   

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"]) 
                   && result.ContainsKey("RetData") && result["RetData"] != "") 
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);

                    
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        Companies.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["ORG_CODE"].ToString(), Name = dtJson.Rows[i]["ORG_NAME"].ToString() });
                    }
                    comboBox1.DataSource = Companies;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "Code";
                 
                    if (dtJson.Rows.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                        string defaultOrgCode = comboBox1.SelectedValue.ToString();
                        CompanyCode = defaultOrgCode;

                    }

                    comboBox1.SelectedIndexChanged += (s, e) =>
                    {
                        if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedValue != null)
                        {
                            string selectedCode = comboBox1.SelectedValue.ToString();
                            string selectedName = comboBox1.Text;
                            CompanyCode = selectedCode;
                            LoadPlants(CompanyCode);
                        }
                    };
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        } 

        private void LoadPlants(string orgId)
        {
            try
            {
                List<ComboboxEntry> Plants = new List<ComboboxEntry> { };
                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("OrgId", orgId);
                string retdata = WebAPIHelper.Post(SizeWisePlanningScheduleReports.Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController",
                    "LoadPlant", SizeWisePlanningScheduleReports.Program.client.UserToken, JsonConvert.SerializeObject(Data));
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
                        Plants.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["CODE"].ToString(), Name = dtJson.Rows[i]["NAME"].ToString() });
                    }
                    comboBox3.DataSource = Plants;
                    comboBox3.DisplayMember = "Name";
                    comboBox3.ValueMember = "Code";

                    if (dtJson.Rows.Count > 0)
                    {
                        comboBox3.SelectedIndex = 0;
                        string defaultPlantCode = comboBox3.SelectedValue.ToString();
                        PlantCode = defaultPlantCode;

                    }

                    comboBox3.SelectedIndexChanged += (s, e) =>
                    {
                        if (comboBox3.SelectedIndex >= 0 && comboBox3.SelectedValue != null)
                        {
                            string selectedCode = comboBox3.SelectedValue.ToString();
                            string selectedName = comboBox3.Text;
                            PlantCode = selectedCode;
                            LoadLines(CompanyCode, PlantCode, ProcessCode);
                        }
                    };

                }

            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void button3_Click(object sender, EventArgs e)
        {
            SearchFunction();

        }

        private void SearchFunction()
        {
            string inputText = textBox2.Text.Trim();
            try
            {
                List<string> SOList = inputText
        .Split(new[] { ',', ';', '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(a => a.Trim())
        .Distinct()
        .ToList();


                List<string> SOvalues = SOList;

                string Lines = "";
                string input = textBox1.Text;
                string[] parts = input.Split(',');

                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].Length > 4)
                        parts[i] = parts[i].Substring(4);
                    else
                        parts[i] = "";
                }

                string Lresult = string.Join(",", parts);

                Lines = Lresult;
                if (PlantCode==null)
                {
                    PlantCode = string.Empty;
                }
                if (ProcessCode == null)
                {
                    ProcessCode = string.Empty;
                }

                Dictionary<string, object> Data = new Dictionary<string, object>
                {
 
                            {"orgid", CompanyCode },
                            {"plant", PlantCode },
                            {"process", ProcessCode },
                            {"line", Lines },
                            {"startdate", dateTimePicker5.Text},
                            {"enddate",dateTimePicker6.Text },
                            {"seid", SOvalues }
                };

                string retdata = WebAPIHelper.Post(
                    SizeWisePlanningScheduleReports.Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController", 
                    "PlanningSizewiseReport",
                    SizeWisePlanningScheduleReports.Program.client.UserToken,
                    JsonConvert.SerializeObject(Data)
                );
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (result != null
                    && result.ContainsKey("IsSuccess")
                    && Convert.ToBoolean(result["IsSuccess"])
                    && result.ContainsKey("RetData")
                    && !string.IsNullOrEmpty(result["RetData"].ToString()))
                {
                    string json = result["RetData"].ToString();

                    // Convert JSON to DataTable
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);

                    // dtJson columns: WEEK, SO, QTY, LINEVAL, PROCESS, ORG, SIZE_NO, SEID, SCH_QTY, LINE

                    string pivotColumn = "SIZE_NO";   // Sizes become columns
                    string valueColumn = "SCH_QTY";   // Values to show for each size
                    int defaultValue = 0;             // Fill 0 if size missing

                    // Key columns define uniqueness per row
                    var keyColumns = new string[] { "WEEK", "SO", "QTY", "LINEVAL", "PROCESS", "ORG", "LINE" };

                  
                    // ✅ Get distinct SIZE_NO values and sort numerically (handles integers and decimals)
                    var pivotValues = dtJson.AsEnumerable()
                        .Select(r => r[pivotColumn]?.ToString()?.Trim())
                        .Where(v => !string.IsNullOrEmpty(v))
                        .Distinct()
                        .OrderBy(v =>
                        {
                            // Try to parse integer or decimal uniformly
                            decimal num;
                            return decimal.TryParse(v, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture, out num)
                                ? num
                                : decimal.MaxValue;
                        })
                        .ToList();

                    // Create pivot DataTable
                    DataTable pivot = new DataTable();

                    // Add key columns first
                    foreach (var key in keyColumns)
                        pivot.Columns.Add(key);

                    // Add dynamic numeric size columns
                    foreach (var size in pivotValues)
                        pivot.Columns.Add(size.ToString(), typeof(int));

                    // Group by key columns to get unique rows per combination
                    var groups = dtJson.AsEnumerable()
                        .GroupBy(r => string.Join("|", keyColumns.Select(k => r[k].ToString())));

                    // Fill pivot table
                    // Fill pivot table
                    foreach (var g in groups)
                    {
                        DataRow newRow = pivot.NewRow();
                        var firstRow = g.First();

                        // Fill key columns
                        foreach (var key in keyColumns)
                            newRow[key] = firstRow[key];

                        // Fill dynamic size columns
                        foreach (var size in pivotValues)
                        {
                            decimal sizeNum;
                            decimal currentSizeNum;

                            var rowForSize = g.FirstOrDefault(r =>
                            {
                                var cellValue = r[pivotColumn]?.ToString()?.Trim();
                                if (decimal.TryParse(cellValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out currentSizeNum)
                                    && decimal.TryParse(size, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out sizeNum))
                                {
                                    return currentSizeNum == sizeNum;
                                }
                                return string.Equals(cellValue, size, StringComparison.OrdinalIgnoreCase);
                            });

                            newRow[size] = rowForSize != null ? Convert.ToInt32(rowForSize[valueColumn]) : defaultValue;
                        }

                        pivot.Rows.Add(newRow);
                    }

                    // ✅ Add total column and calculate size sum
                    if (!pivot.Columns.Contains("TOTAL_QTY"))
                        pivot.Columns.Add("TOTAL_QTY", typeof(int));

                    foreach (DataRow row in pivot.Rows)
                    {
                        int total = 0;
                        foreach (var size in pivotValues)
                        {
                            int val = 0;
                            int.TryParse(row[size]?.ToString(), out val);
                            total += val;
                        }
                        row["TOTAL_QTY"] = total;
                    }

                    // ✅ Bind to DataGridView
                    dataGridView1.DataSource = pivot;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                 
                }
                else if (result != null && result.ContainsKey("ErrMsg") && !string.IsNullOrEmpty(result["ErrMsg"].ToString()))
                {
                    dataGridView1.DataSource = null;
                    MessageHelper.ShowErr(this, "Please Contact With IT Department");
                }
                else
                {
                    dataGridView1.DataSource = null;
                    MessageHelper.ShowErr(this, "No data found");
                }

            }
            catch (Exception e)
            {
                dataGridView1.Rows.Clear();
                MessageHelper.ShowErr(this, "Please Contact With IT Department");
            }
        }

      


        private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Prevent typing, select only
            comboBox.FlatStyle = FlatStyle.Flat;                 // Flat modern look
            comboBox.BackColor = Color.White;                    // Background color
            comboBox.ForeColor = Color.Black;                    // Text color
            comboBox.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Font style
            comboBox.Margin = new Padding(2);
            comboBox.Cursor = Cursors.Hand;                      // Change cursor to hand
            comboBox.Width = 200;                                // Optional width
        

            // Optional: Add a border (simulate since WinForms ComboBox doesn’t support border color)
            comboBox.Region = new Region(comboBox.ClientRectangle);

            comboBox.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                if (e.Index >= 0)
                {
                    string text = comboBox.Items[e.Index].ToString();
                    Brush brush = new SolidBrush(e.ForeColor);
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
                e.DrawFocusRectangle();
            };
        }
        private void StyleRoundedButton(Button btn, Color color)
        {
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Rounded corners using GraphicsPath
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 10;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            btn.Region = new Region(path);
        }

        private void DesignDataGridView(DataGridView dgv)
        {


           
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 35;

            // Row style
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Alternate row color
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            // Selection color
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(125, 50, 80);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Column header border and row height
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.RowTemplate.Height = 30;

            // Optional: auto-size for large datasets
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Optional: sort and alignment for numeric columns
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.ValueType == typeof(decimal) || col.ValueType == typeof(int))
                {
                    col.DefaultCellStyle.Format = "N0"; // comma separated format
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }
        private void ExportExcel()
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No data available to export!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        // Create Excel Package
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            // Add a worksheet
                            ExcelWorksheet ws = excel.Workbook.Worksheets.Add("Data");

                            // Add header row
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                ws.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderText;
                                ws.Cells[1, i + 1].Style.Font.Bold = true;
                                ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                ws.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }

                            // Add data rows
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                {
                                    ws.Cells[i + 2, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                                }
                            }

                            // Auto-fit columns
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();

                            // Save Excel file
                            FileInfo fi = new FileInfo(sfd.FileName);
                            excel.SaveAs(fi);

                            MessageBox.Show("Excel file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Clear textbox
            textBox1.Clear();
            textBox2.Clear();

            // Clear combobox selection
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            dateTimePicker5.Value = DateTime.Now;
            dateTimePicker6.Value = DateTime.Now;
            // Clear all rows in grid 
            dataGridView1.DataSource = null;
            PlantCode = "";
            ProcessCode = "";

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PlanningSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void week_Click(object sender, EventArgs e)
        {

        }

        private void Delete_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void SaveOrUpdate_Click(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Planning_Schedule_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void PlanningScheduleTab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
