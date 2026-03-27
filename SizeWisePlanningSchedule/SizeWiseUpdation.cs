using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SJeMES_Framework.WebAPI;
using SJeMES_Framework.Common;
using SJeMES_Control_Library;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;
using OfficeOpenXml.Style;
using NewExportExcels;
using Microsoft.Office.Interop.Excel;
using LicenseContext = OfficeOpenXml.LicenseContext;
using NPOI.SS.UserModel;
using System.Net.Sockets;
using System.Net;
using NPOI.SS.Formula.Functions;
using static OfficeOpenXml.ExcelErrorValue;
using Newtonsoft.Json;
using PlanningSchedule;
using SJeMES_Control_Library.Controls;

namespace SizeWisePlanningSchedule
{
    public partial class SizeWiseUpdation : Form
    {
        public class ComboboxEntry
        {
            public string Code { get; set; }
            public string Name { get; set; }
        } 
        private string CompanyCode = null;
        private string PlantCode = null;
        private string ProcessCode = null; 
        public SizeWiseUpdation()
        {
            InitializeComponent();
            StyleComboBox(comboBox1);
            StyleComboBox(comboBox2); 
            StyleComboBox(comboBox3);
            StyleComboBox(comboBox4);
            StyleRoundedButton(button3, Color.FromArgb(155, 89, 182)); 
            StyleRoundedButton(button2, Color.FromArgb(46, 204, 113)); 
            LoadOrg();
            LoadPlants(CompanyCode);
            LoadProcess();
            textBox2.Multiline = true;
            textBox2.AcceptsReturn = true;
            textBox2.AcceptsTab = false;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.WordWrap = false;
            textBox2.Font = new Font("Consolas", 11, FontStyle.Regular);
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox2.Padding = new Padding(5);
            textBox2.MinimumSize = new Size(300, 100);
            DesignDataGridView(dataGridView1); 
        }
        private void StyleComboBox(System.Windows.Forms.ComboBox comboBox) 
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
        private void StyleRoundedButton(System.Windows.Forms.Button btn, Color color) 
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

        private void LoadOrg()
        {
            try
            {
                List<ComboboxEntry> Companies = new List<ComboboxEntry> { };
                string retdata = WebAPIHelper.Post(PlanningSchedule.Program.client.APIURL, "KZ_CUTMNT", 
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "LoadOrg", PlanningSchedule.Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));

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
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }
        }
        private void LoadPlants(string orgId)
        {
            try
            {
                List<ComboboxEntry> Plants = new List<ComboboxEntry> { };
                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("OrgId", orgId);
                string retdata = WebAPIHelper.Post(PlanningSchedule.Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "LoadPlant", PlanningSchedule.Program.client.UserToken, JsonConvert.SerializeObject(Data));
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
                        LoadLines(CompanyCode, PlantCode, ProcessCode); 
                    }

                    comboBox3.SelectedIndexChanged += (s, e) =>
                    {
                        if (comboBox3.SelectedIndex >= 0 && comboBox3.SelectedValue != null)
                        {
                            string selectedCode = comboBox3.SelectedValue.ToString();
                            string selectedName = comboBox3.Text;
                            PlantCode = selectedCode; 
                        }
                    };

                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
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
        private void DesignDataGridView(DataGridView dgv)
        { 
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 35;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(125, 50, 80);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.RowTemplate.Height = 30;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.ValueType == typeof(decimal) || col.ValueType == typeof(int))
                {
                    col.DefaultCellStyle.Format = "N0"; 
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
        private void LoadLines(string OrgId, String Plant, String Process)
        {
            if (string.IsNullOrEmpty(OrgId))
            {
                MessageHelper.ShowErr(this, "Please select Factory!");
            }
            else if (string.IsNullOrEmpty(Plant))
            {
                MessageHelper.ShowErr(this, "Please select Plant!");
            }
            else if (string.IsNullOrEmpty(Process))
            {
                MessageHelper.ShowErr(this, "Please select Process!");
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
                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.PlanningController",
                        "LoadLines", Program.client.UserToken, JsonConvert.SerializeObject(Data));
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
                            }
                        };
                    }
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, "Error: " + ex.Message);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SearchFunction();  
        }
        private void SearchFunction() 
        {
            try 
            {
                var data = new Dictionary<string, object>
                {
                    ["startdate"] = dateTimePicker5.Text, 
                    ["enddate"] = dateTimePicker6.Text, 
                    ["orgid"] = CompanyCode,
                    ["plant"] = PlantCode,
                    ["process"] = ProcessCode , 
                    ["line"] = comboBox2.SelectedValue.ToString() , 
                }; 

                if (string.IsNullOrEmpty(data["plant"]?.ToString()) || string.IsNullOrEmpty(data["process"]?.ToString()))
                { 
                    MessageHelper.ShowErr(this, "Please select Plant and Process first."); 
                    return; 
                } ; 

                List<string> seidList = GetSOList(); 

                // Add seid list to request body
                data["soList"] = seidList; 
                string gridresponse = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController",
                    "SearchLineChangeSchedule", 
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );
                string sizeresponse = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController",
                    "LineSearchLineChangeSchedule",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );
                var gridresult = JsonConvert.DeserializeObject<Dictionary<string, object>>(gridresponse) ; 
                var sizeresult = JsonConvert.DeserializeObject<Dictionary<string, object>>(sizeresponse) ;

                if (gridresult != null && gridresult.ContainsKey("IsSuccess") && Convert.ToBoolean(gridresult["IsSuccess"]))
                {
                    if (gridresult.ContainsKey("RetData") && !string.IsNullOrEmpty(gridresult["RetData"]?.ToString()))
                    {
                        string json = gridresult["RetData"].ToString();
                        DataTable dt = JsonHelper.GetDataTableByJson(json);
                        // dataGridView1.AutoGenerateColumns = false;
                        if(dt.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dt;
                           
                        } 
                        else
                        {
                            MessageHelper.ShowErr(this, "NO Data Found"); 
                        }
                        
                        /// dataGridView1.AllowUserToAddRows = false;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        MessageHelper.ShowErr(this, "No data found for grid.");
                    }
                }
                else if (gridresult != null && gridresult.ContainsKey("ErrMsg"))
                {
                    MessageHelper.ShowErr(this, $"Grid API Error: {gridresult["ErrMsg"]}");
                }
                if (sizeresult != null && sizeresult.ContainsKey("IsSuccess") && Convert.ToBoolean(sizeresult["IsSuccess"]))
                {
                    if (sizeresult.ContainsKey("RetData") && !string.IsNullOrEmpty(sizeresult["RetData"]?.ToString()))
                    {
                        string jsonSize = sizeresult["RetData"].ToString();
                        DataTable sizeTable = JsonHelper.GetDataTableByJson(jsonSize);
                        DesignCheckedListBox(checkedListBox1); 
                        checkedListBox1.Items.Clear();

                        foreach (DataRow row in sizeTable.Rows)
                        {
                            // Change "SIZE_NAME" to your actual column name
                            string sizeName = row["SIZE_NO"].ToString();
                            checkedListBox1.Items.Add(sizeName, false);
                        }
                    } 
                    else
                    {
                        checkedListBox1.Items.Clear(); 
                        MessageHelper.ShowErr(this, "No size data found for the selected line.");
                    }
                }
                else if (sizeresult != null && sizeresult.ContainsKey("ErrMsg"))
                {
                    MessageHelper.ShowErr(this, $"Size API Error: {sizeresult["ErrMsg"]}");
                }
            }
            catch (Exception ex)
            {

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                }
                MessageHelper.ShowErr(this, $"An error occurred. Please contact IT Department.\nDetails: {ex.Message}");
            }
        }
        private List<string> GetSOList()
        {
            List<string> so = new List<string>();

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                string[] soArray = textBox2.Text.Split(new[] { ',', ';', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in soArray)
                {
                    string trimmed = item.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                        so.Add(trimmed);
                }
            }

            return so;
        } 
        private void DesignCheckedListBox(CheckedListBox clb)
        {
            // Clear existing items and settings if needed
            clb.Items.Clear();

            // Appearance settings
            clb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; 
            clb.CheckOnClick = true;                // Check items with a single click
            clb.BackColor = Color.White;            // Background color
            clb.ForeColor = Color.Black;            // Text color
            clb.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Font style
            clb.SelectionMode = SelectionMode.One;  // Only one item selected at a time

            // Scrollbar & layout
            clb.HorizontalScrollbar = true;
            clb.IntegralHeight = false;             // Makes scrollbar consistent
            // clb.Height = 200;                       // Default height (adjust as needed)

            // Optional: custom selection colors
            clb.DrawMode = DrawMode.OwnerDrawFixed;
            clb.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                bool isChecked = clb.GetItemChecked(e.Index);

                // Set background color when selected
                Color bgColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                    ? Color.LightBlue
                    : Color.White;

                using (SolidBrush backgroundBrush = new SolidBrush(bgColor))
                using (SolidBrush textBrush = new SolidBrush(Color.Black))
                {
                    e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
                    string text = clb.Items[e.Index].ToString();
                    e.Graphics.DrawString(text, e.Font, textBrush, e.Bounds.Left + 2, e.Bounds.Top + 2);
                }

                e.DrawFocusRectangle();
            };
        }
        string selecteCodevalue = ""; 
        private void button2_Click(object sender, EventArgs e)
        {
            var plants = comboBox2.DataSource as List<SizeWiseUpdation.ComboboxEntry>;

            if (plants != null && dataGridView1.Rows.Count > 0 ) 
            {

                ChangeLInecs form = new ChangeLInecs(plants);
                if (form.ShowDialog() == DialogResult.OK) 
                { 
                    string selectedCode = form.SelectedCode;
                    selecteCodevalue = selectedCode;  
                    string columnName = "changeLine";
                    if (!dataGridView1.Columns.Contains(columnName))
                    {
                        dataGridView1.Columns.Add(columnName, "changeLine");  
                    } 

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            row.Cells[columnName].Value = selectedCode;
                        }
                    }
                     
                }
            }
            else
            {
                MessageHelper.ShowErr(this , "Invalid data source type for comboBox3  and grid.");
            }
        }
        private void ChangeLineSchedule(List<string> weekList, List<string> soList , List<string> changeLineList , List<string> PresentLineList , string reason  )  
        {
            List<string> selectedSizes = new List<string>();

            foreach (var item in checkedListBox1.CheckedItems)
            {
                selectedSizes.Add(item.ToString());
            }

            if (selectedSizes.Count == 0)
            {
                MessageHelper.ShowErr(this , "Please select at least one size." );
                return;
            }
            
            ProcessSelectedSizes(weekList , soList , changeLineList , selectedSizes , PresentLineList , reason ); 
        }
        private string GetLocalIPAddress()
        {
            string localIP = string.Empty;
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // Only IPv4
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        private void ProcessSelectedSizes(List<string> weekList , List<string> soList , List<string> chageLIneList  
            , List<string> sizes , List<string> PresentLineList
            , string reason 
            )  
        {
            try
            {
                string ipaddress = GetLocalIPAddress();
                var data = new Dictionary<string, object>
                {
                    ["weekList"] = weekList,
                    ["soList"] = soList,
                    ["sizes"] = sizes,
                    ["changeLine"] = chageLIneList,
                    ["presentsizes"] = PresentLineList,
                    ["ipaddress"] = ipaddress ,
                    ["reason"] = reason , 
                }; 

                string response = WebAPIHelper.Post(  
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.SizePlanningController",
                    "LineChangeSchedule",  
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if(result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])) 
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result["RetData"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int updatedCount = dt.AsEnumerable().Count(r => r["STATUS"].ToString() == "Updated");
                        int failedCount = dt.AsEnumerable().Count(r => r["STATUS"].ToString() == "Failed");
                        int skippedCount = dt.AsEnumerable().Count(r => r["STATUS"].ToString().StartsWith("Skipped"));

                        string message = $"Line Change Schedule Result:\n" +  
                                         $"✅ Updated: {updatedCount}\n" +   
                                         $"❌ Failed: {failedCount}\n" +   
                                         $"⏭️ Skipped: {skippedCount}";  

                        MessageBox.Show(message, "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No records found in the response.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if(chageLIneList.Count > 0)
                    { 
                        comboBox2.SelectedValue = chageLIneList[0];
                    }  
                    SearchFunction();   
                }  

            } catch(Exception ex ) 
            {
                MessageHelper.ShowErr(this, "An Error Acuured : " + ex); 
                return; 
            }   
        }
        private string PromptReason(string title, string prompt)
        {
            using (Form inputForm = new Form())
            {
                inputForm.Text = title;
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.ClientSize = new Size(350, 150);
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                Label lblPrompt = new Label() { Text = prompt, Left = 15, Top = 15, Width = 320 };
                System.Windows.Forms.TextBox txtReason = new System.Windows.Forms.TextBox() { Left = 15, Top = 45, Width = 320 };
                System.Windows.Forms.Button btnOk = 
                    new System.Windows.Forms.Button() { Text = "OK", Left = 170, Width = 75, Top = 90, DialogResult = DialogResult.OK };
                System.Windows.Forms.Button btnCancel = 
                    new System.Windows.Forms.Button() { Text = "Cancel", Left = 260, Width = 75, Top = 90, DialogResult = DialogResult.Cancel };

                inputForm.Controls.AddRange(new Control[] { lblPrompt, txtReason, btnOk, btnCancel });
                inputForm.AcceptButton = btnOk;
                inputForm.CancelButton = btnCancel;

                DialogResult dialogResult = inputForm.ShowDialog();

                return dialogResult == DialogResult.OK ? txtReason.Text.Trim() : string.Empty;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.DataSource == null)
            {
                MessageHelper.ShowErr(this , "No data available in DataGridView."); 
                return;
            }
            if(selecteCodevalue == "")
            {
                MessageHelper.ShowErr(this, "Please set Changed Line");
                return;
            }
            string reason = PromptReason("Reason Required", "Please enter the reason for saving:");
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageHelper.ShowErr(this, "❌ Save cancelled. Reason is required.");
                return;
            }

            List<string> weekList = new List<string>(); 
            List<string> soList = new List<string>();
            List<string> changeLineList = new List<string>();  
            List<string> PresentLineList = new List<string>(); 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            { 
                if (!row.IsNewRow) 
                { 
                    var weekValue = row.Cells["WEEK"].Value?.ToString();  
                    var soValue = row.Cells["SALES_ORDER"].Value?.ToString();
                    var changeLine = row.Cells["changeLine"].Value?.ToString();
                    var presentline = row.Cells["LINE1"].Value?.ToString(); 
                    if (!string.IsNullOrEmpty(weekValue) && !string.IsNullOrEmpty(soValue) && !string.IsNullOrEmpty(changeLine) && !string.IsNullOrEmpty(presentline)) 
                    {
                        weekList.Add(weekValue);
                        soList.Add(soValue);
                        changeLineList.Add(changeLine);
                        PresentLineList.Add(presentline); 
                    } 
                }
            } 
            ChangeLineSchedule( weekList , soList , changeLineList, PresentLineList , reason  );    
        } 
 

    }
}
